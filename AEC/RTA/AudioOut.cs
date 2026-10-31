using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectSound;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AEC.RTA.Core;
using AEC.RTA.Utils;

namespace AEC.RTA
{
    public enum PlaybackState
    {
        /// <summary>
        /// Stopped
        /// </summary>
        Stopped,
        /// <summary>
        /// Playing
        /// </summary>
        Playing,
        /// <summary>
        /// Paused
        /// </summary>
        Paused
    }

    public delegate byte[] GetNextSamplesCallback(int length);

    public class AudioOut : ThreadBase
    {
        private Device device = null;
        private PlaybackState _playbackState;
        private WaveFormat waveFormat;
        private Notify notify = null;
        private Microsoft.DirectX.DirectSound.Buffer primaryBuffer = null;
        private Microsoft.DirectX.DirectSound.SecondaryBuffer secondaryBuffer = null;
        private int samplesTotalSize;
        private int samplesFrameSize;
        private int nextSamplesWriteIndex;
        private byte[] samples;
        private int indexHandle = 0;

        //private EventWaitHandle frameEventWaitHandle1;
        //private EventWaitHandle frameEventWaitHandle2;
        //private EventWaitHandle endEventWaitHandle;
        //private Thread notifyThread;

        //public event EventHandler PlaybackStopped;

        private object lockObject = new object();

        //private MemoryStream stream = null;
        private BufferedWaveProvider waveStream;

        private int desiredLatency = 100;

        private GetNextSamplesCallback GetNextSamples;
        //private int 

        public PlaybackState PlaybackState
        {
            get 
            { 
                return _playbackState; 
            }
        }

        public int SamplesTotalSize
        {
            get
            {
                return samplesTotalSize;
            }
        }

        public void Dispose()
        {
            StopPlayback();
            Stop();
            GC.SuppressFinalize(this);
        }

        ~AudioOut()
        {
            Dispose();
        }

        private void InitDevice(AudioDevice output_device)
        {
            if (device != null && !device.Disposed)
            {
                device.Dispose();
            }

            device = new Device(output_device.Clsid);
            device.SetCooperativeLevel(GetDesktopWindow(), CooperativeLevel.Priority);
        }

        private void InitWaveFormatAndStream(int sampleRate)
        {
            waveFormat = CreateWaveFormat(sampleRate);
            waveStream = new BufferedWaveProvider(waveFormat.AverageBytesPerSecond * 2);    //立体声双声道1s长度
        }

        public void RegisterGetNextSamplesHandler(GetNextSamplesCallback handler)
        {
            this.GetNextSamples += handler;
        }

        public void RemoveGetNextSamplesHandler(GetNextSamplesCallback handler)
        {
            this.GetNextSamples -= handler;
        }

        public void StartPlay(AudioDevice device, int sr)
        {
            if (_playbackState != PlaybackState.Stopped)
            {
                return;
            }

            indexHandle = 0;
            nextSamplesWriteIndex = 0;

            InitDevice(device);         //初始化设备
            InitWaveFormatAndStream(sr);        //初始化波形格式
            CreatePlayBuffer();     // 创建一个播放缓冲区
            InitNotifications();    // 建立通知消息,当播放到指定位置时候处理
            Feed(samplesFrameSize);     //先给缓冲区提供数据

            //并开始播放
            secondaryBuffer.Play(0, BufferPlayFlags.Looping);
            _playbackState = PlaybackState.Playing;
        }

        private void CreatePlayBuffer()
        {
            if (null != primaryBuffer && !primaryBuffer.Disposed)
            {
                primaryBuffer.Dispose();
                primaryBuffer = null;
            }

            if (null != secondaryBuffer && !secondaryBuffer.Disposed)
            {
                secondaryBuffer.Dispose();
                secondaryBuffer = null;
            }

            BufferDescription primaryBufferDesc = new BufferDescription();
            primaryBufferDesc.BufferBytes = 0;
            primaryBufferDesc.Flags = BufferDescriptionFlags.PrimaryBuffer;
            primaryBuffer = new Microsoft.DirectX.DirectSound.Buffer(primaryBufferDesc, device);
            primaryBuffer.Play(0, BufferPlayFlags.Looping);

            samplesFrameSize = MsToBytes(desiredLatency);

            BufferDescription secondaryBufferDesc = new BufferDescription();
            secondaryBufferDesc.BufferBytes = samplesFrameSize * 2;
            secondaryBufferDesc.Flags = BufferDescriptionFlags.CanGetCurrentPosition
                    | BufferDescriptionFlags.ControlPositionNotify
                    | BufferDescriptionFlags.GlobalFocus
                    | BufferDescriptionFlags.ControlVolume
                    | BufferDescriptionFlags.StickyFocus;
            secondaryBufferDesc.Format = waveFormat;

            secondaryBuffer = new Microsoft.DirectX.DirectSound.SecondaryBuffer(secondaryBufferDesc, device);
        }

        private bool InitNotifications()
        {
            System.Diagnostics.Debug.Assert(secondaryBuffer != null);

            if (null != notify && !notify.Disposed)
            {
                notify.Dispose();
                notify = null;
            }

            notify = new Notify(secondaryBuffer);

            nextSamplesWriteIndex = 0;
            samplesTotalSize = secondaryBuffer.Caps.BufferBytes;
            samples = new byte[samplesFrameSize];

            System.Diagnostics.Debug.Assert(samplesTotalSize == (2 * samplesFrameSize), "Invalid SamplesTotalSize vs SamplesFrameSize");

            BufferPositionNotify[] positionNotify = new BufferPositionNotify[2];
            positionNotify[0].Offset = 0;
            positionNotify[0].EventNotifyHandle = base.run_event_.SafeWaitHandle.DangerousGetHandle();

            positionNotify[1].Offset = samplesFrameSize;
            positionNotify[1].EventNotifyHandle = base.run_event_.SafeWaitHandle.DangerousGetHandle();

            notify.SetNotificationPositions(positionNotify);

            return true;
        }

        public void StopPlayback()
        {
            if (_playbackState == PlaybackState.Stopped)
            {
                return;
            }

            _playbackState = PlaybackState.Stopped;

            // 停止播放
            try
            {
                if(primaryBuffer != null && !primaryBuffer.Disposed)
                {
                    primaryBuffer.Stop();
                }
                if (secondaryBuffer != null && !secondaryBuffer.Disposed)
                {
                    secondaryBuffer.Stop();
                }
            }
            catch
            {

            }

            indexHandle = 0;
            nextSamplesWriteIndex = 0;
        }

        protected override void Run()
        {
            indexHandle = (indexHandle == 0) ? 1 : 0;
            nextSamplesWriteIndex = indexHandle * samplesFrameSize;
            Feed(samplesFrameSize);
        }

        private int MsToBytes(int ms)
        {
            int bytes = ms * (waveFormat.AverageBytesPerSecond / 1000);
            bytes -= bytes % waveFormat.BlockAlign;
            return bytes;
        }

        private int Feed(int bytesToCopy)
        {
            //Console.WriteLine("Feed bytesToCopy:{0}", bytesToCopy);
            if (waveStream == null)
            {
                return 0;
            }

            int bytesRead = bytesToCopy;

            // Restore the buffer if lost
            if (secondaryBuffer.Status.BufferLost)
            {
                secondaryBuffer.Restore();
            }

            // Clear the bufferSamples if in Paused
            if (_playbackState == PlaybackState.Paused)
            {
                Array.Clear(samples, 0, samples.Length);
            }
            else
            {
                // Read data from stream (Should this be inserted between the lock / unlock?)
                //stream.Seek(0, SeekOrigin.Begin);
                if (GetNextSamples != null && waveStream.BufferedBytes < bytesToCopy)
                {
                    byte[] buffer = GetNextSamples(bytesToCopy);
                    if (buffer != null)
                    {
                        //Console.WriteLine("AddSamples:{0}", buffer.Length);
                        waveStream.AddSamples(buffer, 0, buffer.Length);
                    }
                    //Console.WriteLine("BufferedBytes:{0}", waveStream.BufferedBytes);
                }
                
                bytesRead = waveStream.Read(samples, 0, bytesToCopy);

                if (bytesRead == 0)
                {
                    Array.Clear(samples, 0, samples.Length);
                    return 0;
                }
            }

            // Lock a portion of the SecondaryBuffer (starting from 0 or 1/2 the buffer)
            //IntPtr wavBuffer1;
            //int nbSamples1;
            //IntPtr wavBuffer2;
            //int nbSamples2;

            //Console.WriteLine("拷贝数据到secondaryBuffer index:{0}", nextSamplesWriteIndex);
            secondaryBuffer.Write(nextSamplesWriteIndex, samples, LockFlag.None);

            //secondaryBuffer.Lock(nextSamplesWriteIndex, (uint)bytesRead,  // (uint)bytesToCopy,
            //                     out wavBuffer1, out nbSamples1,
            //                     out wavBuffer2, out nbSamples2,
            //                     DirectSoundBufferLockFlag.None);

            //// Copy back to the SecondaryBuffer
            //if (wavBuffer1 != IntPtr.Zero)
            //{
            //    Marshal.Copy(samples, 0, wavBuffer1, nbSamples1);
            //    if (wavBuffer2 != IntPtr.Zero)
            //    {
            //        Marshal.Copy(samples, 0, wavBuffer1, nbSamples1);
            //    }
            //}

            //// Unlock the SecondaryBuffer
            //secondaryBuffer.Unlock(wavBuffer1, nbSamples1, wavBuffer2, nbSamples2);

            return bytesRead;
        }

        private WaveFormat CreateWaveFormat(int sampleRate)
        {
            WaveFormat format = new WaveFormat();
            format.FormatTag = WaveFormatTag.Pcm;   // PCM  WaveFormatTag
            format.SamplesPerSecond = sampleRate; // 采样频率
            //format.SamplesPerSecond = 96000; // 采样频率
            format.BitsPerSample = 16;              // 16Bit  
            format.Channels = 2;                    // Mono 
            format.BlockAlign = (short)(format.Channels * (format.BitsPerSample / 8));
            format.AverageBytesPerSecond = format.BlockAlign * format.SamplesPerSecond;

            return format;
        }

        //private bool InitDevice(Guid guid)
        //{
        //    // 用指定的捕捉设备创建Capture对象
        //    try
        //    {
        //        device = new Device(guid);
        //    }
        //    catch (Exception)
        //    {
        //        System.Windows.Forms.MessageBox.Show("Initialize the audio output device failed!", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        //        return false;
        //    }

        //    return true;
        //}

        public static IEnumerable<AudioDevice> Devices
        {
            get
            {
                List<AudioDevice> list = new List<AudioDevice>();
                foreach (DeviceInformation di in new DevicesCollection())
                {
                    //Capture captureDevice = new Capture(di.DriverGuid);
                    AudioDevice device = new AudioDevice(di.Description, di.DriverGuid);
                    list.Add(device);
                }
                return list;
            }
        }

        /// <summary>
        /// Gets the HANDLE of the desktop window.
        /// </summary>
        /// <returns>HANDLE of the Desktop window</returns>
        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();
    }
}
