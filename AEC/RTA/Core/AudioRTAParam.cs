using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;

namespace AEC.RTA.Core
{
    public class AudioRTAParam
    {
        public const int AudioChannels = 2;          //双声道立体声，请勿修改
        public const int SampleRate44100 = 44100;
        public const int SampleRate48k = 48000;
        public const int SampleRate96k = 96000;
        public const short BitsPerSample16 = 16;
        public const short BitsPerSample24 = 24;
        public const int FFTSize1K = 1024;
        public const int FFTSize8K = FFTSize1K * 8;
        public const int FFTSize16K = FFTSize1K * 16;
        public const int FFTSize32K = FFTSize1K * 32;

        public const short TransferAmpOffsetIndexLowerLimit = -360;
        public const short TransferAmpOffsetIndexUpperLimit = 360;
        public const short TransferAmpOffsetIndexMax = TransferAmpOffsetIndexUpperLimit - TransferAmpOffsetIndexLowerLimit;
        public const float AmpOffsetScale = 0.1f;

        private const float MaxDelay = 500;

        private const string GainUnit = "dB";

        public static Dictionary<FFTSize, int> fftSizeMap;
        private static Dictionary<RTASampleRate, int> sampleRateMap;
        private static Dictionary<RTASampleBitDepth, short> sampleBitDepthMap;

        public static AudioRTAParam audioParam = null;

        static AudioRTAParam()
        {
            fftSizeMap = new Dictionary<FFTSize, int>()
            {
                {FFTSize.fftSize32k, FFTSize32K},
                {FFTSize.fftSize16k, FFTSize16K},
                {FFTSize.fftSize8k, FFTSize8K},
                {FFTSize.FPPO, FFTSize32K},
            };

            sampleRateMap = new Dictionary<RTASampleRate, int>()
            {
                {RTASampleRate.sr48k, SampleRate48k},
                {RTASampleRate.sr96k, SampleRate96k},
            };

            sampleBitDepthMap = new Dictionary<RTASampleBitDepth, short>()
            {
                {RTASampleBitDepth.bd16Bit, BitsPerSample16},
                {RTASampleBitDepth.bd24Bit, BitsPerSample24},
            };
        }

        private string _inputDevice = string.Empty;
        private string _outputDevice = string.Empty;

        //输入设备
        public string inputDeviceDescription 
        { 
            get
            {
                return _inputDevice;
            }
            set
            {
                _inputDevice = value;
            }
        }
        //输出设备
        public string outputDeviceDescription 
        {
            get
            {
                return _outputDevice;
            }
            set
            {
                _outputDevice = value;
            }
        } 
        public RTASampleRate sr { get; set; }  //采样频率
        public RTASampleBitDepth sbd { get; set; }  //采样位深度
        public RTAMode rtaMode { get; set; }    //实时分析模式

        //频谱相关参数
        public FFTSize spFftSize { get; set; }  //频谱模式fft size
        public WindowFunction spWindow { get; set; } //默认汉宁窗
        public Averager.AverageMode spAverageMode { get; set; }
        public SpectrumScale spScale { get; set; }
        public SpectrumMap spMap { get; set; }

        //传递函数相关参数
        public FFTSize tfFftSize { get; set; }  //频谱模式fft size
        public WindowFunction tfWindow { get; set; } //默认汉宁窗
        public Averager.AverageMode tfAverageMode { get; set; }
        public Smoother.SmoothPoint tfSmooth { get; set; }
        private short _tfAmpOffset = 0;

        public FreqRespMap freqRespMap { get; set; }

        //输出信号
        public OutputSignalType outputSignalType { get; set; }

        public OutputSignalLevel pinkNoiseLevel = new OutputSignalLevel();  //粉噪声输出电平
        public OutputSignalLevel pinkSweepLevel = new OutputSignalLevel();  //粉扫频输出电平
        public OutputSignalLevel sineLevel = new OutputSignalLevel();       //正弦输出电平
        public OutputSignalLevel dualSine1Level = new OutputSignalLevel();  //双正弦输出电平1
        public OutputSignalLevel dualSine2Level = new OutputSignalLevel();  //双正弦输出电平2
        public OutputSineSignalFreq sineFreq = new OutputSineSignalFreq();
        public OutputSineSignalFreq dualSine1Freq = new OutputSineSignalFreq();
        public OutputSineSignalFreq dualSine2Freq = new OutputSineSignalFreq();

        //系统延时
        private float _delay = 0;

        //输入信号
        public InputSignal inputSignal { get; set; }

        public int fftSize
        {
            get
            {
                FFTSize fftSize = rtaMode == RTAMode.Spectrum ? spFftSize : tfFftSize;
                return fftSizeMap[fftSize];
            }
        }

        public int sampleRate
        {
            get
            {
                return sampleRateMap[sr];
            }
            set
            {
                if(value == SampleRate48k)
                {
                    sr = RTASampleRate.sr48k;
                }
                else if(value == SampleRate96k)
                {
                    sr = RTASampleRate.sr96k;
                }
                else
                {
                    sr = RTASampleRate.sr48k;
                }
            }
        }

        public short sampleBitDepth
        {
            get
            {
                return sampleBitDepthMap[sbd];
            }
            set
            {
                if (value == BitsPerSample16)
                {
                    sbd = RTASampleBitDepth.bd16Bit;
                }
                else if (value == BitsPerSample24)
                {
                    sbd = RTASampleBitDepth.bd24Bit;
                }
                else
                {
                    sbd = RTASampleBitDepth.bd16Bit;
                }
            }
        }

        public int rtaModeIndex
        {
            get
            {
                return (int)this.rtaMode;
            }
            set
            {
                this.rtaMode = value == 0 ? RTAMode.Spectrum : RTAMode.Transfer;
            }
        }

        public int spFftSizeIndex
        {
            set
            {
                if(value >= 0 && value <= (int)FFTSize.fftSize8k)
                {
                    spFftSize = (FFTSize)value;
                }
                else
                {
                    spFftSize = FFTSize.fftSize32k;
                }
            }
        }

        public int spWindowIndex
        {
            set
            {
                spWindow = Int2Window(value);
            }
        }

        public int spAverageModeIndex
        {
            set
            {
                spAverageMode = Int2AverageMode(value);
            }
        }

        public int spScaleIndex
        {
            set
            {
                if (value >= 0 && value <= (int)SpectrumScale.Octave24)
                {
                    spScale = (SpectrumScale)value;
                }
                else
                {
                    spScale = SpectrumScale.Log;
                }
            }
        }

        public int spMapIndex
        {
            set
            {
                if (value >= 0 && value <= (int)SpectrumMap.RightCh)
                {
                    spMap = (SpectrumMap)value;
                }
                else
                {
                    spMap = SpectrumMap.DoubleCh;
                }
            }
        }



        //***********
        public int tfFftSizeIndex
        {
            set
            {
                if (value >= 0 && value <= (int)FFTSize.FPPO)
                {
                    tfFftSize = (FFTSize)value;
                }
                else
                {
                    tfFftSize = FFTSize.fftSize32k;
                }
            }
        }

        public int tfWindowIndex
        {
            set
            {
                tfWindow = Int2Window(value);
            }
        }

        public int tfAverageModeIndex
        {
            set
            {
                tfAverageMode = Int2AverageMode(value);
            }
        }

        public int tfSmoothIndex
        {
            set
            {
                if (value >= 0 && value <= (int)Smoother.SmoothPoint.Point9)
                {
                    tfSmooth = (Smoother.SmoothPoint)value;
                }
                else
                {
                    tfSmooth = Smoother.SmoothPoint.None;
                }
            }
        }

        public short tfMagOffsetIndex
        {
            get
            {
                return this._tfAmpOffset;
            }
            set
            {
                this._tfAmpOffset = Math.Min(Math.Max(value, TransferAmpOffsetIndexLowerLimit), TransferAmpOffsetIndexUpperLimit);
            }
        }

        public int freqRespMapIndex
        {
            set
            {
                if (value >= 0 && value <= (int)FreqRespMap.PhaseResp)
                {
                    freqRespMap = (FreqRespMap)value;
                }
                else
                {
                    freqRespMap = FreqRespMap.AmpRespAndPhaseResp;
                }
            }
        }
        //***********

        public short outputSignalTypeIndex
        {
            set
            {
                if (value >= 0 && value <= (short)OutputSignalType.DualSine)
                {
                    outputSignalType = (OutputSignalType)value;
                }
                else
                {
                    outputSignalType = OutputSignalType.PinkNoise;
                }
            }
        }

        public FFTSize rtaFftSize
        {
            get
            {
                return rtaMode == RTAMode.Spectrum ? spFftSize : tfFftSize;
            }
            set
            {
                if(rtaMode == RTAMode.Spectrum)
                {
                    spFftSize = value;
                }
                else
                {
                    tfFftSize = value;
                }
            }
        }

        public WindowFunction rtaWindow
        {
            get
            {
                return rtaMode == RTAMode.Spectrum ? spWindow : tfWindow;
            }
            set
            {
                if (rtaMode == RTAMode.Spectrum)
                {
                    spWindow = value;
                }
                else
                {
                    tfWindow = value;
                }
            }
        }

        public Averager.AverageMode rtaAverageMode
        {
            get
            {
                return rtaMode == RTAMode.Spectrum ? spAverageMode : tfAverageMode;
            }
            set
            {
                if (rtaMode == RTAMode.Spectrum)
                {
                    spAverageMode = value;
                }
                else
                {
                    tfAverageMode = value;
                }
            }
        }

        public float tfMagOffset
        {
            get
            {
                return _tfAmpOffset * AmpOffsetScale;
            }
            set
            {
                short offset = (short)Math.Round(value / AmpOffsetScale, 1, MidpointRounding.AwayFromZero);
                this._tfAmpOffset = Math.Min(Math.Max(offset, TransferAmpOffsetIndexLowerLimit), TransferAmpOffsetIndexUpperLimit);
            }
        }

        public short tfAmpOffset
        {
            get
            {
                return (short)(_tfAmpOffset - TransferAmpOffsetIndexLowerLimit);
            }
            set
            {
                short offset = (short)(value + TransferAmpOffsetIndexLowerLimit);
                this._tfAmpOffset = Math.Min(Math.Max(offset, TransferAmpOffsetIndexLowerLimit), TransferAmpOffsetIndexUpperLimit);
            }
        }

        public float delay
        {
            get
            {
                return _delay;
            }
            set
            {
                _delay = Math.Min(Math.Max(value, 0), MaxDelay);
            }
        }

        public string GetTfAmpLabel(short index)
        {
            short offset = (short)(index + TransferAmpOffsetIndexLowerLimit);
            offset = Math.Min(Math.Max(offset, TransferAmpOffsetIndexLowerLimit), TransferAmpOffsetIndexUpperLimit);
            if (offset >= 0)
            {
                return string.Format("+{0:0.0} dB", offset * AmpOffsetScale);
            }
            else //if (offset < 0)
            {
                return string.Format("{0:0.0} dB", offset * AmpOffsetScale);
            }
            //else
            //{
            //    return string.Format("+{0} dB", offset * AmpOffsetScale);
            //}
        }

        public short CalcAmpOffsetIndex(string text)
        {
            string str = AEC.Core.Utils.RemoveStringSuffix(text, GainUnit);
            str = AEC.Core.Utils.RemoveStringPrefix(str, "+");

            float value;
            if (float.TryParse(str, out value))
            {
                short offset = (short)Math.Round(value / AmpOffsetScale, 1, MidpointRounding.AwayFromZero);
                short ampOffset = Math.Min(Math.Max(offset, TransferAmpOffsetIndexLowerLimit), TransferAmpOffsetIndexUpperLimit);

                return (short)(ampOffset - TransferAmpOffsetIndexLowerLimit);
            }

            return -1;
        }

        public SignalGenerator CreateSignalGenerator()
        {
            switch(outputSignalType)
            {
                case OutputSignalType.PinkNoise:
                    return new PinkNoiseGenerator(pinkNoiseLevel.level);

                case OutputSignalType.PinkSweep:
                    return new PinkSweepGenerator(rtaMode == RTAMode.Spectrum ? spFftSize : tfFftSize, pinkSweepLevel.level);

                case OutputSignalType.Sine:
                    return new SineGenerator(sampleRate, sineLevel.level, sineFreq.freq);

                case OutputSignalType.DualSine:
                    return new DualSineGenerator(sampleRate, dualSine1Level.level, dualSine1Freq.freq, dualSine2Level.level, dualSine2Freq.freq);
            
                default:
                    throw (new Exception("CreateSignalGenerator()...超出范围"));
            }
        }

        private static WindowFunction Int2Window(int value)
        {
            if (value >= 0 && value <= (int)WindowFunction.FlatTopWindow)
            {
                return (WindowFunction)value;
            }
            else
            {
                return WindowFunction.HannWindow;
            }
        }

        private static Averager.AverageMode Int2AverageMode(int value)
        {
            if (value >= 0 && value <= (int)Averager.AverageMode.SLOW)
            {
                return (Averager.AverageMode)value;
            }
            else
            {
                return Averager.AverageMode.FIFO1;
            }
        }

        public static int GetFftSize(FFTSize size)
        {
            return fftSizeMap[size];
        }

        public static int GetSampleRate(RTASampleRate sr)
        {
            return sampleRateMap[sr];
        }
    }
}
