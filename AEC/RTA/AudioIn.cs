using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.DirectX.DirectSound;
using AEC.RTA.Core;
using AEC.RTA.Utils;

namespace AEC.RTA
{
    public enum CaptureState
    {
        /// <summary>
        /// Stopped
        /// </summary>
        Stopped,
        /// <summary>
        /// Playing
        /// </summary>
        Capturing,
        /// <summary>
        /// Paused
        /// </summary>
        //Paused
    }

    public class AudioIn : ThreadBase
    {
        //public const int notifyNum = 2;          //缓冲队列的数目
        private Capture capture = null;       //音频捕捉设备
        private WaveFormat waveFormat;              //录音的格式
        private CaptureBuffer buffer = null;    // 缓冲区对象
        private Notify notify = null;               // 消息通知对象

        private int captureBufferSize = 0;       // 缓冲队列大小
        private int nextCaptureOffset = 0;       // 该次录音缓冲区的起始点 
        //private int notifySize = 0;              // 每次通知大小  
        private MemoryStream memStream;
        private bool readFirstBufferPart = true;

        private CaptureState captureState = CaptureState.Stopped;

        
        //private int desiredLatency = 100;

        private BitsPerSample bitsPerSample
        {
            get
            {
                return this.waveFormat.BitsPerSample ==  AudioRTAParam.BitsPerSample16 ? BitsPerSample.BitsPerSample_16Bit : BitsPerSample.BitsPerSample_24Bit;
            }
        }

        //初始化音频捕捉设备  
        private void InitDevcie(AudioDevice device)
        {
            if (capture != null && !capture.Disposed)
            {
                capture.Dispose();
            }

            capture = new Capture(device.Clsid);
        }

        private void InitWaveFormat(CaptureFormat captureFormat)
        {
            waveFormat = CreateWaveFormat(captureFormat);
        }

        public void StartCapture(AudioDevice device, CaptureFormat captureFormat)
        {
            if (captureState != CaptureState.Stopped)
            {
                return;
            }

            InitDevcie(device); //初始化设备
            InitWaveFormat(captureFormat);  //初始化波形格式
            CreateCaptureBuffer();  //创建一个录音缓冲区，并开始录音
            InitNotifications();     // 建立通知消息,当缓冲区满的时候处理方法
            buffer.Start(true);     //开始录音
            captureState = CaptureState.Capturing;
        }

        public void StopCapture()
        {
            if (captureState == CaptureState.Stopped)
            {
                return;
            }

            captureState = CaptureState.Stopped;

            // 停止录音
            try
            {
                if(buffer != null && !buffer.Disposed)
                {
                    buffer.Stop();
                }
                
                nextCaptureOffset = 0;
                readFirstBufferPart = true;
                base.run_event_.Reset();
            }
            catch
            {

            }
            // 写入缓冲区最后的数据
            //CaptureData();
        }

        private WaveFormat CreateWaveFormat(CaptureFormat fmt)
        {
            short bits_per_sample = fmt.bitsPerSample;
            if (bits_per_sample != AudioRTAParam.BitsPerSample24)
            {
                bits_per_sample = AudioRTAParam.BitsPerSample16;
            }

            WaveFormat format = new WaveFormat();
            format.FormatTag = WaveFormatTag.Pcm;               // PCM  WaveFormatTag
            format.SamplesPerSecond = fmt.sampleRate;       // 采样频率
            format.BitsPerSample = bits_per_sample;             // 16Bit  
            format.Channels = AudioRTAParam.AudioChannels;       // 双声道立体声
            format.BlockAlign = (short)(format.Channels * (format.BitsPerSample / 8));
            format.AverageBytesPerSecond = format.BlockAlign * format.SamplesPerSecond;
            return format;
        }

        //private int MsToBytes(int ms)
        //{
        //    int bytes = ms * (waveFormat.AverageBytesPerSecond / 1000);
        //    bytes -= bytes % waveFormat.BlockAlign;
        //    return bytes;
        //}

        private void CreateCaptureBuffer()
        {
            if (null != buffer && !buffer.Disposed)
            {
                buffer.Dispose();
                buffer = null;
            }

            // 设定缓冲区大小  
            captureBufferSize = waveFormat.AverageBytesPerSecond / 5;
            
            // 缓冲区的描述对象  
            CaptureBufferDescription bufferdescription = new CaptureBufferDescription();
            bufferdescription.BufferBytes = captureBufferSize;
            bufferdescription.Format = waveFormat;           // 录音格式
            // 创建缓冲区  
            buffer = new CaptureBuffer(bufferdescription, capture);
            nextCaptureOffset = 0;
            readFirstBufferPart = true;
            memStream = new MemoryStream(captureBufferSize / 2);
        }

        private void InitNotifications()
        {
            System.Diagnostics.Debug.Assert(buffer != null);

            if (null != notify && !notify.Disposed)
            {
                notify.Dispose();
                notify = null;
            }

            notify = new Notify(buffer);

            BufferPositionNotify bufferPositionNotify1 = new BufferPositionNotify();
            bufferPositionNotify1.Offset = captureBufferSize / 2 - 1;
            bufferPositionNotify1.EventNotifyHandle = base.run_event_.SafeWaitHandle.DangerousGetHandle();

            BufferPositionNotify bufferPositionNotify2 = new BufferPositionNotify();
            bufferPositionNotify2.Offset = captureBufferSize - 1;
            bufferPositionNotify2.EventNotifyHandle = base.run_event_.SafeWaitHandle.DangerousGetHandle();

            notify.SetNotificationPositions(new BufferPositionNotify[] { bufferPositionNotify1, bufferPositionNotify2 });
        }

        protected override void Run()
        {
            CaptureData(); 
        }

        private void CaptureData()
        {
            try
            {
                memStream.Seek(0, SeekOrigin.Begin);
                buffer.Read(nextCaptureOffset, memStream, captureBufferSize / 2, LockFlag.None);
                readFirstBufferPart = !readFirstBufferPart;
                nextCaptureOffset = readFirstBufferPart ? 0 : captureBufferSize / 2;

                if(memStream.Length > 0)
                {
                    ProcessData(memStream.ToArray());
                }
                //byte[] data = memStream.ToArray();
                //Console.WriteLine("data length:{0}", data.Length);
            }
            catch(Exception ex)
            {
                AEC.Tools.LogHelper.WriteDebug("AudioIn.CaptureData().... Exception: ", ex);
            }

            //int capturePosition = 0;
            //int readPosition = 0;
            //int lockSize = 0;
            //buffer.GetCurrentPosition(out capturePosition, out readPosition);
            //lockSize = readPosition - nextCaptureOffset;
            
            //if (lockSize < 0)
            //{
            //    lockSize += captureBufferSize;
            //}

            //lockSize -= (lockSize % notifySize); 

            //if (0 == lockSize)
            //{
            //    return;
            //}

            //// 读取缓冲区内的数据  
            //byte[] captureData = (byte[])buffer.Read(nextCaptureOffset, typeof(byte), LockFlag.FromWriteCursor, lockSize);
            
            //ProcessData(captureData);

            //// 移动录制数据的起始点,通知消息只负责指示产生消息的位置,并不记录上次录制的位置  
            //nextCaptureOffset += captureData.Length;
            //nextCaptureOffset %= captureBufferSize; // Circular buffer  
        }

        private void ProcessData(byte[] data)
        {
            if (captureState == CaptureState.Capturing && AudioDataCapturedHandler != null)
            {
                //转换得到的浮点数为归一化的结果，数值范围在[-1,1]
                double[] stereoData = FixedPointHelper.FixedPoint2Double(data, bitsPerSample);
                //Console.WriteLine("AudioIn.ProcessData()...");
                AudioDataCapturedHandler(stereoData, EventArgs.Empty);
            }
        }

        private EventHandler AudioDataCapturedHandler = null;
        public void RegisterAudioDataHandler(EventHandler handler)
        {
            AudioDataCapturedHandler += handler;
        }

        public void RemoveAudioDataHandler(EventHandler handler)
        {
            AudioDataCapturedHandler -= handler;
        }

        public static IEnumerable<AudioDevice> Devices
        {
            get
            {
                List<AudioDevice> list = new List<AudioDevice>();
                foreach (DeviceInformation di in new CaptureDevicesCollection())
                {
                    //Capture captureDevice = new Capture(di.DriverGuid);
                    AudioDevice device = new AudioDevice(di.Description, di.DriverGuid);
                    list.Add(device);
                }
                return list;
            }
        }
    }
}
