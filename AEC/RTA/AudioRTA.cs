using System;
using System.Collections.Generic;
using System.Text;
using AEC.RTA.Core;
using AEC.RTA.FFTW;
using System.Threading;
using AEC.RTA.Utils;

namespace AEC.RTA
{
    public class AudioRTA : ThreadBase
    {
        public static readonly float ZeroMagIndB = -300;

        private static object paramLock = new object();

        #region fft工具
        private StereoFFTHelper stereoFFT32K = null;
        private StereoFFTHelper stereoFFT16K = null;
        private StereoFFTHelper stereoFFTK8K = null;
        private StereoFFTHelper currStereoFFT = null;
        #endregion

        private StereoBuffer inputSignal = null;                //输入立体声信号
        public StereoBuffer stereoAmp { get; private set; }        //信号频率域原始幅度
        public FrequencyResponse freqResp { get; private set; } //系统频率响应

        private StereoRMSAverager sp_stereoRMSAverager = null;     //求频谱均值处理器
        private StereoRMSAverager tf_stereoRMSAverager = null;     //求频谱均值处理器

        private Complex[] divisions = null;
        private double[] tf_mag = null;

        private MagSmoother tf_mag_smoother = null;             //传递行数幅度平滑器
        private AngleSmoother tf_angle_smoother = null;         //传递函数角度平滑器

        private ComplexAverager tf_complexAverager = null;      //传递函数复数均值处理器
        private ComplexAverager fppo_complexAverager = null;    //FPPO复数均值处理器

        public FPPO.FreqResp fppoFreqResp { get; private set; }

        private FPPO fppo = null;

        private Mutex rtaLock = new Mutex();

        //public RTAMode _rtaMode = RTAMode.Spectrum;

        private bool _needToUpdateFftPlan = false;
        private WindowFunction lastWindowFunction = WindowFunction.RectangleWindow;

        public readonly RTAContext rtaContext = new RTAContext();

        private EventHandler OutputHandler = null;
        public void RegisterOutputHandler(EventHandler handler)
        {
            OutputHandler += handler;
        }

        public void RemoveOutputHandler(EventHandler handler)
        {
            OutputHandler -= handler;
        }

        static AudioRTA()
        {
            ZeroMagIndB = (float)(20 * Math.Log10(0.000000000001));
        }

        public AudioRTA()
        {
        }

        //private int fftSize
        //{
        //    get
        //    {
        //        return AudioAnalysis.rtaParam.fftSize;
        //    }
        //}

        //private RTASampleRate sr
        //{
        //    get
        //    {
        //        return AudioAnalysis.rtaParam.sr;
        //    }
        //}

        //private FFTSize tfFftSize
        //{
        //    get
        //    {
        //        return AudioAnalysis.rtaParam.tfFftSize;
        //    }
        //}

        private bool isFPPO
        {
            get
            {
                return rtaContext.rtaMode == RTAMode.Transfer && rtaContext.rtaFftSize == FFTSize.FPPO;
            }
        }

        private WindowFunction windowFunction
        {
            get
            {
                if (AudioAnalysis.rtaParam.inputSignal == InputSignal.Sweep)
                {
                    return WindowFunction.RectangleWindow;
                }

                return AudioAnalysis.rtaParam.rtaWindow;
            }
        }

        public Averager.AverageMode spAverageMode
        {
            get
            {
                return AudioAnalysis.rtaParam.spAverageMode;
            }
        }

        public Averager.AverageMode tfAverageMode
        {
            get
            {
                return AudioAnalysis.rtaParam.tfAverageMode;
            }
        }

        public Smoother.SmoothPoint tfSmoothMode
        {
            get
            {
                return AudioAnalysis.rtaParam.tfSmooth;
            }
        }

        public float magOffset
        {
            get
            {
                return AudioAnalysis.rtaParam.tfMagOffset;
            }
            set
            {
                AudioAnalysis.rtaParam.tfMagOffset = value;
            }
        }

        private bool needToUpdateFftPlan
        {
            get
            {
                lock (paramLock)
                {
                    return _needToUpdateFftPlan;
                }
            }
            set
            {
                lock (paramLock)
                {
                    _needToUpdateFftPlan = value;
                }
            }
        }

        private void InitInputSignal()
        {
            int fftLength = rtaContext.fftSize;

            if (inputSignal == null || inputSignal.length != fftLength)
            {
                inputSignal = new StereoBuffer(fftLength);
                needToUpdateFftPlan = true;
            }
        }

        private void InitStereoAmp()
        {
            int fftLength = rtaContext.fftSize;
            if (stereoAmp == null || stereoAmp.length != fftLength)
            {
                stereoAmp = new StereoBuffer(fftLength);
            }
        }

        private void InitFreqResp()
        {
            int fftLength = rtaContext.fftSize;
            if (freqResp == null || freqResp.length != fftLength)
            {
                freqResp = new FrequencyResponse(fftLength);
            }
        }

        private void InitDivisions()
        {
            int fftLength = rtaContext.fftSize;
            if (divisions == null || divisions.Length != fftLength)
            {
                divisions = new Complex[fftLength];
            }
        }

        private void InitTfMag()
        {
            int fftLength = rtaContext.fftSize;
            if (tf_mag == null || tf_mag.Length != fftLength)
            {
                tf_mag = new double[fftLength];
            }
        }

        private void UpdateParams()
        {
            InitInputSignal();
            InitStereoAmp();
            InitFreqResp();
            InitDivisions();
            InitTfMag();
        }

        private void UpdateFFTPlan()
        {
            if (lastWindowFunction != windowFunction)
            {
                lastWindowFunction = windowFunction;
                needToUpdateFftPlan = true;
            }

            if (!needToUpdateFftPlan && this.currStereoFFT != null)
            {
                return;
            }

            if (!isFPPO)
            {
                switch (rtaContext.fftSize)
                {
                    case AudioRTAParam.FFTSize8K:
                        if (stereoFFTK8K == null)
                        {
                            stereoFFTK8K = new StereoFFTHelper(rtaContext.fftSize);
                        }
                        this.currStereoFFT = stereoFFTK8K;
                        break;

                    case AudioRTAParam.FFTSize16K:
                        if (stereoFFT16K == null)
                        {
                            stereoFFT16K = new StereoFFTHelper(rtaContext.fftSize);
                        }
                        this.currStereoFFT = stereoFFT16K;
                        break;

                    case AudioRTAParam.FFTSize32K:
                        if (stereoFFT32K == null)
                        {
                            stereoFFT32K = new StereoFFTHelper(rtaContext.fftSize);
                        }
                        this.currStereoFFT = stereoFFT32K;
                        break;

                    default:
                        throw new Exception("FFT Size Exception!");
                }

                if (this.currStereoFFT.winFunction != windowFunction)
                {
                    this.currStereoFFT.winFunction = windowFunction;
                }
            }

            needToUpdateFftPlan = false;
        }

        protected override void Run()
        {
            rtaLock.WaitOne();
            //Console.WriteLine("run..1");
            //Console.WriteLine("AudioRTA.Run()...1");
            UpdateParams();
            //Console.WriteLine("AudioRTA.Run()...2");
            //Console.WriteLine("run..2");

            UpdateFFTPlan();
            //Console.WriteLine("AudioRTA.Run()...3");
            //Console.WriteLine("run..3");

            if (rtaContext.rtaMode == RTAMode.Spectrum)
            {
                //Console.WriteLine("AudioRTA.Run()...4");
                this.currStereoFFT.DoFFT(inputSignal);
                //Console.WriteLine("AudioRTA.Run()...5");
                this.CalcSignalAmp();
                //Console.WriteLine("AudioRTA.Run()...6");
                this.CalcSignalAmpAverage();
                //Console.WriteLine("AudioRTA.Run()...7");
                //Console.WriteLine("run..4");

                //if (OutputHandler != null)
                //{
                //    Console.WriteLine("AudioRTA.OutputHandler()...");
                //    OutputHandler(this, EventArgs.Empty);
                //}
            }
            else if (rtaContext.rtaMode == RTAMode.Transfer)
            {
                if (rtaContext.rtaFftSize != FFTSize.FPPO)
                {
                    this.currStereoFFT.DoFFT(inputSignal);
                    this.CalcFreqResp();
                    this.ProcessFreqRespResult();
                }
                else
                {
                    if (this.fppo == null || this.fppo.sampleRate != rtaContext.rtaSampleRate || this.fppo.winFunction != windowFunction)
                    {
                        this.fppo = new FPPO(rtaContext.rtaSampleRate, windowFunction);
                    }
                    this.fppoFreqResp = this.fppo.Execute(inputSignal, tfAverageMode);
                    this.ProcessFPPOTransferResult();
                }
                //if (OutputHandler != null)
                //{
                //    try
                //    {
                //        OutputHandler(this, EventArgs.Empty);
                //    }
                //    catch (Exception e)
                //    {
                //        Console.WriteLine("AudioRTA.Run()...异常:{0}", e.Message);
                //    }
                //}
            }

            rtaLock.ReleaseMutex();

            if (OutputHandler != null)
            {
                OutputHandler(this, EventArgs.Empty);
                //try
                //{
                //    //Console.WriteLine("AudioRTA.OutputHandler()...fft size = {0}", rtaContext.rtaFftSize);
                //    OutputHandler(this, EventArgs.Empty);
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine("AudioRTA.OutputHandler()...异常:{0}", e.Message);
                //}
            }
        }

        private void CalcSignalAmp()
        {
            for (int i = 0; i < this.rtaContext.fftSize / 2; ++i)
            {
                stereoAmp.left[i] = this.currStereoFFT.stereoResult.left[i].Magnitude * 2 / rtaContext.fftSize;
                stereoAmp.right[i] = this.currStereoFFT.stereoResult.right[i].Magnitude * 2 / rtaContext.fftSize;
            }
        }

        private void CalcSignalAmpAverage()
        {
            int averageLength = this.rtaContext.fftSize / 2;
            if (sp_stereoRMSAverager == null || sp_stereoRMSAverager.length != averageLength || sp_stereoRMSAverager.averageMode != spAverageMode)
            {
                sp_stereoRMSAverager = new StereoRMSAverager(averageLength, spAverageMode);
            }

            sp_stereoRMSAverager.Excete(stereoAmp.left, stereoAmp.right);
        }

        private void ProcessFreqRespResult()
        {
            //幅度处理 1、均值处理；2、转换为dB,加上幅度偏移量;3、平滑处理；
            //1、对幅度做均值处理，幅度平均
            int averageLength = this.rtaContext.fftSize / 2;

            if (tf_stereoRMSAverager == null || tf_stereoRMSAverager.length != averageLength || tf_stereoRMSAverager.averageMode != tfAverageMode)
            {
                tf_stereoRMSAverager = new StereoRMSAverager(averageLength, tfAverageMode);
            }
            tf_stereoRMSAverager.Excete(stereoAmp.left, stereoAmp.right);

            //先对频率域信号作均值处理,相位平均
            if (tf_complexAverager == null || tf_complexAverager._length != averageLength || tf_complexAverager.mode != tfAverageMode)
            {
                tf_complexAverager = ComplexAverager.CreateComplexAverager(averageLength, tfAverageMode);
            }
            tf_complexAverager.Execute(this.divisions);

            //计算幅值，转换为dB；加上幅度偏移量
            float ampOffset = magOffset;
            for (int i = 1; i < averageLength; ++i)
            {
                if (stereoAmp.left[i] > 0 && stereoAmp.right[i] > 0)
                {
                    freqResp.amp[i] = 20 * Math.Log10(stereoAmp.left[i] / stereoAmp.right[i]) + ampOffset;
                }
                else
                {
                    freqResp.amp[i] = ampOffset;
                }

                if (this.divisions[i].Magnitude > 0)
                {
                    //freqResp.amp[i] = 20 * Math.Log10(this.divisions[i].Magnitude) + magOffset;
                    freqResp.phase[i] = this.divisions[i].Phase * 180 / Math.PI;
                }
                else
                {
                    //freqResp.amp[i] = 0;
                    freqResp.phase[i] = 0;
                }
            }

            //对幅度进行平滑处理
            if (tf_mag_smoother == null || tf_mag_smoother.length != averageLength || tf_mag_smoother.smoothPoint != tfSmoothMode)
            {
                tf_mag_smoother = MagSmoother.CreateMagSmoother(averageLength, tfSmoothMode);
            }
            tf_mag_smoother.Execute(freqResp.amp);

            //对相位进行平滑处理
            if (tf_angle_smoother == null || tf_angle_smoother.length != averageLength || tf_angle_smoother.smoothPoint != tfSmoothMode)
            {
                tf_angle_smoother = AngleSmoother.CreateAngleSmoother(averageLength, tfSmoothMode);
            }
            tf_angle_smoother.Execute(freqResp.phase);
        }

        private void ProcessFPPOTransferResult()
        {
            if (fppoFreqResp == null)
            {
                return;
            }

            int averageLength = fppoFreqResp.length;

            //对幅度做均值处理，幅度平均
            //if (tf_mag_averager == null || tf_mag_averager._length != averageLength || tf_mag_averager.mode != tf_averageMode)
            //{
            //    tf_mag_averager = VectorAverager.CreateVectorAverager(averageLength, tf_averageMode);
            //}
            //tf_mag_averager.Execute(fppoFreqResp.magnitudes);

            //对复数做均值处理，相位平均
            if (fppo_complexAverager == null || fppo_complexAverager._length != averageLength || fppo_complexAverager.mode != tfAverageMode)
            {
                fppo_complexAverager = ComplexAverager.CreateComplexAverager(averageLength, tfAverageMode);
            }
            fppo_complexAverager.Execute(fppoFreqResp.signals);

            float ampOffset = magOffset;
            for (int i = 1; i < averageLength; ++i)
            {
                fppoFreqResp.magnitudes[i] += ampOffset;
                fppoFreqResp.phases[i] = this.fppoFreqResp.signals[i].Phase * 180 / Math.PI;
                //if (this.fppoFreqResp.signals[i].Magnitude > 0)
                //{
                //    //fppoFreqResp.magnitudes[i] = 20 * Math.Log10(this.fppoFreqResp.signals[i].Magnitude) + magOffset;                  
                //    fppoFreqResp.phases[i] = this.fppoFreqResp.signals[i].Phase * 180 / Math.PI;
                //}
                //else
                //{
                //    //fppoFreqResp.magnitudes[i] = 0;
                //    fppoFreqResp.phases[i] = 0;
                //}

                //if (double.IsInfinity(fppoFreqResp.magnitudes[i]))
                //{
                //    Console.WriteLine("幅值计算错误:{0}", i);
                //}
                //if (double.IsInfinity(fppoFreqResp.phases[i]))
                //{
                //    Console.WriteLine("相位计算错误:{0}", i);
                //}
            }

            //对幅度进行平滑处理
            if (tf_mag_smoother == null || tf_mag_smoother.length != averageLength || tf_mag_smoother.smoothPoint != tfSmoothMode)
            {
                tf_mag_smoother = MagSmoother.CreateMagSmoother(averageLength, tfSmoothMode);
            }
            tf_mag_smoother.Execute(fppoFreqResp.magnitudes);

            //对相位进行平滑处理
            if (tf_angle_smoother == null || tf_angle_smoother.length != averageLength || tf_angle_smoother.smoothPoint != tfSmoothMode)
            {
                tf_angle_smoother = AngleSmoother.CreateAngleSmoother(averageLength, tfSmoothMode);
            }
            tf_angle_smoother.Execute(fppoFreqResp.phases);
        }

        private void AddMagOffset(double[] mags, int length, float offset)
        {
            for (int i = 0; i < mags.Length && i < length; ++i)
            {
                mags[i] += offset;
            }
        }

        private void CalcFreqResp()
        {
            for (int i = 1; i < this.rtaContext.fftSize / 2; ++i)
            {
                stereoAmp.left[i] = this.currStereoFFT.stereoResult.left[i].Magnitude;
                stereoAmp.right[i] = this.currStereoFFT.stereoResult.right[i].Magnitude;

                if (this.currStereoFFT.stereoResult.right[i].Magnitude == 0)
                {
                    divisions[i] = Complex.Zero;
                    continue;
                }

                divisions[i] = this.currStereoFFT.stereoResult.left[i] / this.currStereoFFT.stereoResult.right[i];
            }
        }

        public void InputData(RTAContext context, StereoBuffer stereoBuffer)
        {
            System.Diagnostics.Debug.Assert(rtaContext.fftSize == stereoBuffer.length);

            if (rtaLock.WaitOne(0))
            {
                rtaContext.CopyFrom(context);

                //Console.WriteLine("AudioRTA rta mode:{0}", this.rtaContext.rtaMode);

                InitInputSignal();

                //int bytes_count = rtaContext.fftSize * sizeof(double);

                inputSignal.CopyFrom(stereoBuffer);

                //Buffer.BlockCopy(stereoBuffer.left, 0, inputSignal.left, 0, bytes_count);
                //Buffer.BlockCopy(stereoBuffer.right, 0, inputSignal.right, 0, bytes_count);

                rtaLock.ReleaseMutex();

                ExecuteOnce();
            }
            //else
            //{
            //    Console.WriteLine("AudioRTA Wait mutex failed!");
            //    //Utility.OutputLine("Wait mutex failed!");
            //}

            //try
            //{
            //    //rtaLock.WaitOne();
            //    //Console.WriteLine("AudioRTA.InputData()...1");
            //    //rtaContext.CopyFrom(context);

            //    //InitInputSignal();

            //    //int bytes_count = rtaContext.fftSize * sizeof(double);

            //    //Buffer.BlockCopy(stereoBuffer.left, 0, inputSignal.left, 0, bytes_count);
            //    //Buffer.BlockCopy(stereoBuffer.right, 0, inputSignal.right, 0, bytes_count);
            //    //Console.WriteLine("AudioRTA.InputData()...2");
            //    //rtaLock.ReleaseMutex();
            //    //Console.WriteLine("AudioRTA.InputData()...3");
            //    //ExecuteOnce();
                
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("AudioRTA.AudioDataHandler()...异常:{0}", ex.Message);
            //}
        }

        public void AudioDataHandler(object sender, EventArgs e)
        {
            AudioBuffer audioBuffer = sender as AudioBuffer;

            if (audioBuffer != null && audioBuffer.toBeProcessedData != null)
            {
                InputData(audioBuffer.rtaContext, audioBuffer.toBeProcessedData);
            }
        }

        private double Mag2dB(double mag)
        {
            return 20 * Math.Log10(mag);
        }
    }
}
