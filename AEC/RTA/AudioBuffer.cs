using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AEC.RTA.Core;
using AEC.RTA.Utils;

namespace AEC.RTA
{
    public class AudioBuffer : ThreadBase
    {
        public const int BufferLength = 128 * 1024;    //128K长度
        public const int AudioChannels = AudioRTAParam.AudioChannels;

        private Mutex bufferLock = new Mutex();
        private double[] stereoAudioData;

        private StereoBuffer _audioDataBuffer = null;       //音频数据的缓冲区，左右声道音频数据
        private StereoBuffer _toBeProcessedData = null;     //音频待处理数据的缓冲区，左右声道数据

        private Delay _delay = new Delay();
        public MeasDelayAudioDataBuffer measDelayAudioDataBuffer = new MeasDelayAudioDataBuffer();
        public SteroLevel steroLevel = new SteroLevel();

        private EventHandler AudioBufferHandler = null;
        private EventHandler MeasDelayHandler = null;
        private EventHandler RecvMeasDelayDataProgress = null;
        private EventHandler UpdateSignalLevelUIHandler = null;

        public readonly RTAContext rtaContext = new RTAContext();

        private bool _suspend = false;
        private bool _paused = false;
        private object suspendLock = new object();

        public bool isSuspend
        {
            get
            {
                lock(suspendLock)
                {
                    return _suspend;
                }
            }
            set
            {
                lock (suspendLock)
                {
                    _suspend = value;
                }
            }
        }

        public bool isPaused
        {
            get
            {
                lock (suspendLock)
                {
                    return _paused;
                }
            }
            set
            {
                lock (suspendLock)
                {
                    _paused = value;
                }
            }
        }

        public AudioBuffer()
        {
            _audioDataBuffer = new StereoBuffer(BufferLength);
        }

        #region 是否处理
        //private int fftSize
        //{
        //    get
        //    {
        //        return AudioAnalysis.audioParam.fftSize;
        //    }
        //}

        //private int sampleRate
        //{
        //    get
        //    {
        //        return AudioAnalysis.audioParam.sampleRate;
        //    }
        //}
        #endregion

        public void RegisterAudioBufferHandler(EventHandler handler)
        {
            AudioBufferHandler += handler;
        }

        public void RemoveAudioBufferHandler(EventHandler handler)
        {
            AudioBufferHandler -= handler;
        }

        public void RegisterMeasDelayHandler(EventHandler handler)
        {
            MeasDelayHandler += handler;
        }

        public void RemoveMeasDelayHandler(EventHandler handler)
        {
            MeasDelayHandler -= handler;
        }

        public void RegisterRecvMeasDelayDataProgressHandler(EventHandler handler)
        {
            RecvMeasDelayDataProgress += handler;
        }

        public void RemoveRecvMeasDelayDataProgressHandler(EventHandler handler)
        {
            RecvMeasDelayDataProgress += handler;
        }

        public void RegisterUpdateSignalLevelUIHandler(EventHandler handler)
        {
            UpdateSignalLevelUIHandler += handler;
        }

        public void RemoveUpdateSignalLevelUIHandler(EventHandler handler)
        {
            UpdateSignalLevelUIHandler -= handler;
        }

        public void StartRecvDataToMeasDelay()
        {
            measDelayAudioDataBuffer.StartRecvAudioData();
        }

        public void UpdateDelay(Delay delay)
        {
            _delay.delayOffset = delay.delayOffset;
        }

        public Delay delay
        {
            get
            {
                return this._delay;
            }
        }

        protected override void Run()
        {
            rtaContext.Update(AudioAnalysis.rtaParam.rtaMode, AudioAnalysis.rtaParam.sr, AudioAnalysis.rtaParam.rtaFftSize);
            
            //Console.WriteLine("AudioBuffer.Run()...0");
            if (isSuspend)
            {
                return;
            }

            //Console.WriteLine("AudioBuffer.Run()...1");

            //int length = 0;
            //double[] leftCh = null;
            //double[] rightCh = null;

            bufferLock.WaitOne();

            //先分离左右声道数据
            int length = stereoAudioData.Length / AudioRTAParam.AudioChannels;
            double[] leftCh = Utility.ExtractMonoFromStereo(stereoAudioData, AudioChannel.LeftCh);
            double[] rightCh = Utility.ExtractMonoFromStereo(stereoAudioData, AudioChannel.RightCh);

            bufferLock.ReleaseMutex();

            //try
            //{
            //    bufferLock.WaitOne();

            //    //先分离左右声道数据
            //    length = stereoAudioData.Length / AudioRTAParam.AudioChannels;
            //    leftCh = Utility.ExtractMonoFromStereo(stereoAudioData, AudioChannel.LeftCh);
            //    rightCh = Utility.ExtractMonoFromStereo(stereoAudioData, AudioChannel.RightCh);

            //    bufferLock.ReleaseMutex();
            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine("AudioBuffer.Run()...异常:{0}", e.Message);
            //    return;
            //}

            //Console.WriteLine("AudioBuffer.Run()...2");

            //计算延时
            if (measDelayAudioDataBuffer.isRecvData)
            {
                bool isCompleted = measDelayAudioDataBuffer.RecvData(leftCh, rightCh, length);

                //采集数据进度通知
                if (RecvMeasDelayDataProgress != null)
                {
                    RecvMeasDelayDataProgress(measDelayAudioDataBuffer, EventArgs.Empty);
                }

                //如果采集完成，则开始计算延时
                if (isCompleted)
                {
                    if (MeasDelayHandler != null)
                    {
                        //Console.WriteLine("采集数据完成");
                        MeasDelayHandler(this, EventArgs.Empty);
                    }
                }
            }
            else
            {
                if (isPaused)
                {
                    return;
                }

                //如果数据过长，则先剪裁数据，只取前一段有效数据
                leftCh = Utility.CutData(leftCh, BufferLength);
                rightCh = Utility.CutData(rightCh, BufferLength);
                //保存再缓冲区尾部
                Utility.CopyData2ArrayEnd(leftCh, _audioDataBuffer.left);
                Utility.CopyData2ArrayEnd(rightCh, _audioDataBuffer.right);
                
                //要处理数据
                int fftLength = rtaContext.fftSize;

                if (_toBeProcessedData == null || _toBeProcessedData.length != fftLength)
                {
                    _toBeProcessedData = new StereoBuffer(fftLength);
                }

                Buffer.BlockCopy(_audioDataBuffer.left, (BufferLength - fftLength) * sizeof(double), _toBeProcessedData.left, 0, fftLength * sizeof(double));
                int rightIndex = BufferLength - fftLength - _delay.delayOffset;
                if (rightIndex < 0)
                {
                    rightIndex = 0;
                }
                Buffer.BlockCopy(_audioDataBuffer.right, rightIndex * sizeof(double), _toBeProcessedData.right, 0, fftLength * sizeof(double));
                
                CalcdBFS(leftCh, rightCh);

                if (AudioBufferHandler != null)
                {
                    //Console.WriteLine("AudioBuffer.AudioBufferHandler()...1");
                    AudioBufferHandler(this, EventArgs.Empty);
                    //Console.WriteLine("AudioBuffer.AudioBufferHandler()...2");
                }
            }
        }

        public void InputStereoAudioData(double[] stereoData)
        {
            System.Diagnostics.Debug.Assert(stereoData != null && stereoData.Length > 0);

            //Console.WriteLine("AudioBuffer.InputStereoAudioData()...0");

            if(isSuspend)
            {
                return;
            }

            //Console.WriteLine("AudioBuffer.InputStereoAudioData()...1");

            if (bufferLock.WaitOne(0))
            {
                if (stereoAudioData == null || stereoAudioData.Length != stereoData.Length)
                {
                    stereoAudioData = new double[stereoData.Length];
                }

                Array.Copy(stereoData, stereoAudioData, stereoData.Length);

                //Console.WriteLine("AudioBuffer.InputStereoAudioData()...3");

                bufferLock.ReleaseMutex();

                //Console.WriteLine("AudioBuffer.InputStereoAudioData()...4");

                ExecuteOnce();

                //Console.WriteLine("AudioBuffer.InputStereoAudioData()...5");
            }
            else
            {
                Console.WriteLine("AudioBuffer.InputStereoAudioData() acquire mutex failed!");
            }

            //try
            //{
            //    //Console.WriteLine("AudioBuffer.InputStereoAudioData()...2");

                

            //    //if (bufferLock.WaitOne(0))
            //    //{
            //    //    Console.WriteLine("AudioBuffer.InputStereoAudioData()...2");

            //    //    if (stereoAudioData == null || stereoAudioData.Length != stereoData.Length)
            //    //    {
            //    //        stereoAudioData = new double[stereoData.Length];
            //    //    }

            //    //    Console.WriteLine("AudioBuffer.InputStereoAudioData()...3");

            //    //    Array.Copy(stereoData, stereoAudioData, stereoData.Length);

            //    //    Console.WriteLine("AudioBuffer.InputStereoAudioData()...4");

            //    //    bufferLock.ReleaseMutex();

            //    //    ExecuteOnce();
            //    //}
            //    //else
            //    //{
            //    //    bufferLock.ReleaseMutex();
            //    //}
            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine("InputStereoAudioData()...异常:{0}", e.Message);
            //}

            //Console.WriteLine("AudioBuffer.InputStereoAudioData()...5");
        }

        private void CalcdBFS(double[] left, double[] right)
        {
            steroLevel.UpdateLevel(left, right);

            if (UpdateSignalLevelUIHandler != null)
            {
                UpdateSignalLevelUIHandler(steroLevel, EventArgs.Empty);
            }
            //Console.WriteLine("left = {0}, right = {1}", steroLevel.leftCh, rightCh);
        }

        public void Reset()
        {
            measDelayAudioDataBuffer.StopRecvAudioData();
            steroLevel.Reset();
            this.isSuspend = false;
            this.isPaused = false;
        }

        public void AppendTestData(float[] floatData)
        {

        }

        public StereoBuffer toBeProcessedData
        {
            get
            {
                return this._toBeProcessedData;
            }
        }

        public void RecvAudioDataHandler(object sender, EventArgs e)
        {
            double[] stereoData = sender as double[];
            InputStereoAudioData(stereoData);
        }
    }
}
