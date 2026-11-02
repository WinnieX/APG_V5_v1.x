using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AEC.Core.DSP;

namespace AEC.Core
{
    public enum EQType
    {
        Peaking = 0,
        LowShelf,
        HighShelf,
    }

    public enum XPFilterType
    {
        LowPass = 0,
        HighPass,
    }

    public enum WorkMode
    {
        Auto = 0,
        Manual,
    }

    public enum WorkSelect
    {
        Sing = 0,
        Disco,
    }

    [Flags]
    public enum MicInput
    {
        SINGLE_A,
        SINGLE_B,
        MIXER_AB,
    }

    public enum DEQControlMode
    {
        Off = 0,
        Below,
        Above,
    }

    public enum FilterElement
    {
        Freq,
        Type,
        Q,
    }

    //必须从0开始，自动编号，最后一个TypeCount代表有效枚举数量
    public enum FilterType : short
    {
        FirstOrder = 0,
        Bessel12dB,
        Butterworth12dB,
        LinkwitzRiley12dB,
        Bessel18dB,
        Butterworth18dB,
        Bessel24dB,
        Butterworth24dB,
        LinkwitzRiley24dB,
        Bessel36dB,
        Butterworth36dB,
        LinkwitzRiley36dB,
        Bessel48dB,
        Butterworth48dB,
        LinkwitzRiley48dB,
        X5_Bessel24dB,
        X5_Butterworth24dB,
        X5_LinkwitzRiley24dB,
        X5_User12dB,
        X5_User24dB,
    }

    public enum CompressorMode
    {
        Fast = 0,
        Medium,
        Slow,
    }
    
    public enum SignalPhase
    {
        Negative = 0,
        Positive = 1,
    }

    public enum EQControlMode
    {
        Linkage = 0,
        Left,
        Right,
    }

    //public enum MusicInputGain
    //{
    //    Input_n6dB = 0,
    //    Input_n3dB,
    //}

    //public enum MusicInputPort
    //{
    //    Port1 = 0,
    //    Port2,
    //    Port3,
    //}

    public enum User
    {
        Normal = 0,
        Admin,
    }

    public enum EffectCurveColor
    {
        Def,
        Left,
        Right,
    }

    public enum ActiveItem
    {
        EQ,
        HPF,
        LPF,
        BassExciter,
        Exciter1,
        Exciter2,
    }

    public enum lockSystemType
    {
        deviceIsNotActivated = 0,
        keyboardIsLocked,
        userIsNotAdmin,
        dataIsPrivate,
        usageLimitLock,
    }

    public enum MuteSource
    {
        Music = 0,
        Mic,
        Echo,
        Reverb,
    }

    public enum InputMuteOperateSource
    {
        VolumePanel,
        MonitorPanel,
    }

    public enum DeqParamType
    {
        DeqIsPass,  //DEQ是否是Pass状态
        DCM,        //动态控制模式
        Attack,     //启动时间
        Release,    //恢复时间
        Threshold,  //启动电平
        Ratio,      //动态比例
        DM,         //动态模式
    }

    public enum MicChannel
    {
        MicA,
        MicB,
    }

    public enum CompressorParam
    {
        Threshold,
        Attack,
        Release,
        Ratio,
    }

    public enum OutputChannelType
    {
        MainOutput,
        Aux1Output,
        Aux2Output,
        SurOutput,
    }

    //public class DataTypeUtils
    //{
    //    public static bool Short2Bool(short value)
    //    {
    //        return value != 0;
    //    }

    //    public static short Bool2Short(bool value)
    //    {
    //        return value ? (short)1 : (short)0;
    //    }

    //    public static EQType Short2EQType(short value)
    //    {
    //        switch(value)
    //        {
    //            case 0:
    //                return EQType.Peaking;
    //            case 1:
    //                return EQType.LowShelf;
    //            case 2:
    //                return EQType.HighShelf;
    //        }

    //        return EQType.Peaking;
    //    }
    //}

    

    #region DEQ
    public class DEQ : EQ
    {
        //DEQParam
        public readonly BoolParam deqIsPass;                //EQPass
        public readonly ShortParam deqCM;                   //动态控制模式
        public readonly ShortParam threshold;               //启动电平
        public readonly ShortParam attack;                  //启动时间
        public readonly ShortParam release;                 //恢复时间
        public readonly ShortParam ratio;                   //压缩比例
        public readonly ShortParam deqDM;                   //动态模式
        
        public bool isPass
        {
            get
            {
                return this.deqIsPass.value;
            }
            set
            {
                this.deqIsPass.value = value;
            }
        }
            
        public DEQControlMode dcm
        {
            get
            {
                return DataConverter.Short2DEQControlMode(this.deqCM.v);
            }
            set
            {
                this.deqCM.v = (short)value;
            }
        }
                                  
        public CompressorMode dm
        {
            get
            {
                return DataConverter.Short2CompressorMode(this.deqDM.v);
            }
            set
            {
                this.deqDM.v = (short)value;
            }
        }

        //public static DEQ Create(short eqIndex)
        //{
        //    return new DEQ(eqIndex);
        //}

        public DEQ(short eqIndex):base(eqIndex)
        {
            this.deqIsPass = new BoolParam();
            this.deqCM = new ShortParam((short)DEQControlMode.Above);
            this.threshold = new ShortParam(MXCompressor.MaxThreshold);
            this.attack = new ShortParam(MXCompressor.MaxAttack);
            this.release = new ShortParam(MXCompressor.MaxRelease);
            this.ratio = new ShortParam(MXCompressor.MaxRatio);
            this.deqDM = new ShortParam((short)CompressorMode.Slow);
        }

        public override bool freqRespIsZero
        {
            get
            {
                return base.freqRespIsZero || this.deqIsPass.value;
            }
        }

        public override bool canBeChanged
        {
            get
            {
                return !this.deqIsPass.value;
            }
        }

        public void CopyFrom(DEQ other)
        {
            base.CopyFrom(other, false);
            this.deqIsPass.initV = other.deqIsPass.v;
            this.deqCM.initV = other.deqCM.v;
            this.threshold.initV = other.threshold.v;
            this.attack.initV = other.attack.v;
            this.release.initV = other.release.v;
            this.ratio.initV = other.ratio.v;
            this.deqDM.initV = other.deqDM.v;
        }

        public void ResetDEQ(short initFreqIndex)
        {
            base.ResetEQAndFreqResp(initFreqIndex);
        }

        public ShortParam GetParam(DeqParamType type)
        {
            switch(type)
            {
                case DeqParamType.DeqIsPass:
                    return this.deqIsPass;

                case DeqParamType.DCM:
                    return this.deqCM;

                case DeqParamType.Attack:
                    return this.attack;

                case DeqParamType.Release:
                    return this.release;

                case DeqParamType.Threshold:
                    return this.threshold;

                case DeqParamType.Ratio:
                    return this.ratio;

                case DeqParamType.DM:
                    return this.deqDM;

                default:
                    throw new Exception("DEQ.GetParam()...参数不在预期范围之内！");
            }
        }
    }
    #endregion

    

    

    

    

    public class Threshold
    {
        public readonly short thresholdBase;
        public readonly short thresholdMax;

        public Threshold(short thresholdBase_, short thresholdMax_)
        {
            this.thresholdBase = thresholdBase_;
            this.thresholdMax = thresholdMax_;
        }
    }

    public class ThresholdHelper
    {
        public const string levelUnitIndB = "dB";
        public const string levelUnitIndBu = "dBu";
        public const string levelUnitInV = "V";
        public const short DefMaxThreshold = 51;
        public const short DefThresholdBaseValue = -30;
        public const double refLevel = 0.775;
        public const double roundThreshold = 0.7;

        public readonly short thresholdBase = DefThresholdBaseValue;
        private readonly short maxThreshold = DefMaxThreshold;

        public ThresholdHelper(short thresholdBase_, short maxThreshold_ = DefMaxThreshold)
        {
            this.thresholdBase = thresholdBase_;
            this.maxThreshold = maxThreshold_;
        }

        public string GetThresholdLabelIndB(short index)
        {
            index = Utils.ConstrainMax(index, this.maxThreshold);
            return String.Format("{0} {1}", index - this.maxThreshold, levelUnitIndB);
        }

        public string GetThresholdLabelIndBu(short index)
        {
            index = Utils.ConstrainMax(index, this.maxThreshold);
            return String.Format("{0} {1}", index + this.thresholdBase, levelUnitIndBu);
        }

        public string GetThresholdLabelInV(short index)
        {
            index = Utils.ConstrainMax(index, this.maxThreshold);
            double levelIndB = index + this.thresholdBase;
            double linearlevel = GetLinearLevel(levelIndB);
            if (levelIndB >= -12)
            {
                return String.Format("{0:0.###} {1}", linearlevel, levelUnitInV);
            }
            else
            {
                return String.Format("{0:0.####} {1}", linearlevel, levelUnitInV);
            }
        }

        public short CalcThresholdIndexFromTextIndB(string text)
        {
            string str = Utils.RemoveStringSuffix(text, levelUnitIndB);
            double value;
            if (double.TryParse(str, out value))
            {
                short index = (short)Math.Round(value + this.maxThreshold, 1, MidpointRounding.AwayFromZero);
                return Utils.ConstrainMax(index, this.maxThreshold);
            }

            return -1;
        }

        public short CalcThresholdIndexFromTextIndBu(string text)
        {
            string str = Utils.RemoveStringSuffix(text, levelUnitIndBu);
            double value;
            if (double.TryParse(str, out value))
            {
                short index = (short)Math.Round(value - thresholdBase, 1, MidpointRounding.AwayFromZero);
                return Utils.ConstrainMax(index, this.maxThreshold);
            }

            return -1;
        }

        public short CalcThresholdIndexFromTextInV(string text)
        {
            string str = Utils.RemoveStringSuffix(text, levelUnitInV);
            double value;
            if (double.TryParse(str, out value) && value > 0)
            {
                double decibels = GetDecibelsLevel(value);
                if(decibels - Math.Floor(decibels) < roundThreshold)
                {
                    decibels = Math.Floor(decibels);
                }
                else
                {
                    decibels = Math.Ceiling(decibels);
                }

                short index = (short)Math.Round(decibels - thresholdBase, 1, MidpointRounding.AwayFromZero);
                return Utils.ConstrainMax(index, this.maxThreshold);
            }

            return -1;
        }

        //计算线性值电压
        private static double GetLinearLevel(double dB)
        {
            return Math.Pow(10, dB / 20) * refLevel;
        }

        private static double GetDecibelsLevel(double line)
        {
            System.Diagnostics.Debug.Assert(line > 0);
            return 20 * Math.Log10(line / refLevel);
        }
    }

    public class X5_CompressorLimiter
    {
	    public const short MaxThreshold = 34;
        public static readonly short MaxAttack = (short)(DataUtils.compressorAttackList.Length - 1);
        public static readonly short MaxRelease = (short)(DataUtils.compressorReleaseList.Length - 1);
        public static readonly short MaxRatio = (short)(DataUtils.compressorRatioList.Length - 1);

        public static readonly short DefaultThreshold = MaxThreshold;
        public static readonly short DefaultAttack = 13;
        public static readonly short DefaultRelease = 5;
        public static readonly short DefaultRatio = 0x0a;

        protected const string attackUnit = "ms";
        protected const string releaseUnit = "×";

        public readonly ShortParam threshold;       //启动电平
        public readonly ShortParam attack;          //启动时间
        public readonly ShortParam release;         //恢复时间
        public readonly ShortParam ratio;           //压缩比例
        //private CompressorMode _cm = CompressorMode.Fast;

        public X5_CompressorLimiter(byte moduleIndex, byte address, EventHandler<DataParamChangedArgs> dataChanged)
        {
            this.attack = new ShortParam(moduleIndex, address, MaxAttack, dataChanged);
            this.release = new ShortParam(moduleIndex, (byte)(address + 1), MaxRelease, dataChanged);
            this.threshold = new ShortParam(moduleIndex, (byte)(address + 2), MaxThreshold, dataChanged);
            this.ratio = new ShortParam(moduleIndex, (byte)(address + 3), MaxRatio, dataChanged);

            this.attack.initV = DefaultAttack;
            this.release.initV = DefaultRelease;
            this.threshold.initV = DefaultThreshold;
            this.ratio.initV = DefaultRatio;
        }

        public void CopyFrom(X5_CompressorLimiter other)
        {
            this.threshold.initV = other.threshold.v;
            this.attack.initV = other.attack.v;
            this.release.initV = other.release.v;
            this.ratio.initV = other.ratio.v;
        }

        public void Reset()
        {
            this.threshold.initV = MaxThreshold;
            this.attack.initV = 0;
            this.release.initV = 0;
            this.ratio.initV = 0;
        }
    }

    public class MXCompressor
    {
        public const short MaxThreshold = 50;
        public static readonly short MaxAttack = (short)(DataUtils.compressorAttackList.Length - 1);
        public static readonly short MaxRelease = (short)(DataUtils.compressorReleaseList.Length - 1);
        public static readonly short MaxRatio = (short)(DataUtils.compressorRatioList.Length - 1);

        protected const string attackUnit = "ms";
        protected const string releaseUnit = "×";

        public readonly ShortParam threshold;       //启动电平
        public readonly ShortParam attack;          //启动时间
        public readonly ShortParam release;         //恢复时间
        public readonly ShortParam ratio;           //压缩比例
        public readonly ShortParam mode;           //压缩模式
        //private CompressorMode _cm = CompressorMode.Fast;

        public MXCompressor(byte moduleIndex, byte address, EventHandler<DataParamChangedArgs> dataChanged)
        {
            this.attack = new ShortParam(moduleIndex, address, MaxAttack, dataChanged);
            this.release = new ShortParam(moduleIndex, (byte)(address + 1), MaxRelease, dataChanged);
            this.threshold = new ShortParam(moduleIndex, (byte)(address + 2), MaxThreshold, dataChanged);
            this.ratio = new ShortParam(moduleIndex, (byte)(address + 3), MaxRatio, dataChanged);
            this.mode = new ShortParam(moduleIndex, (byte)(address + 4), (short)CompressorMode.Slow, dataChanged);
        }

        public CompressorMode cm
        {
            get
            {
                return DataConverter.Short2CompressorMode(this.mode.v);
            }
            set
            {
                this.mode.v = (short)value;
            }
        }

        public void CopyFrom(MXCompressor other)
        {
            this.threshold.initV = other.threshold.v;
            this.attack.initV = other.attack.v;
            this.release.initV = other.release.v;
            this.ratio.initV = other.ratio.v;
            this.mode.initV = other.mode.v;
        }

        public void Reset()
        {
            this.threshold.initV = 0;
            this.attack.initV = 0;
            this.release.initV = 0;
            this.ratio.initV = 0;
            this.mode.initV = 0;
        }
    }

    public class CLBase
    {
        protected const string levelUnit = "dB";
        protected const string attackUnit = "ms";
        protected const string releaseUnit = "×";

        public const short MaxAttack = 989;
        public const short MaxRelease = 30;
        public const short MaxThreshold = ThresholdHelper.DefMaxThreshold;

        protected const short defaultAttack = 349;
        protected const short defaultRelease = 14;
        public const short defaultThreshold = MaxThreshold;

        public const short minThresholdValue = -30;
        public const float attackStep = 0.1f;
        public const float minAttackValue = 0.1f;
        public const short minReleaseValue = 2;

        public readonly ShortParam threshold;       //启动电平
        public readonly ShortParam attack;          //启动时间
        public readonly ShortParam release;         //恢复时间

        public CLBase(byte moduleIndex, byte address, EventHandler<DataParamChangedArgs> dataChanged)
        {
            this.attack = new ShortParam(moduleIndex, address, MaxAttack, dataChanged);
            this.release = new ShortParam(moduleIndex, (byte)(address + 1), MaxRelease, dataChanged);
            this.threshold = new ShortParam(moduleIndex, (byte)(address + 2), MaxThreshold, dataChanged);

            this.attack.initV = defaultAttack;
            this.release.initV = defaultRelease;
            this.threshold.initV = defaultThreshold;
        }

        public void CopyFrom(CLBase other)
        {
            this.attack.initV = other.attack.v;
            this.release.initV = other.release.v;
            this.threshold.initV = other.threshold.v;
        }

        public virtual void Reset()
        {
            this.attack.initV = defaultAttack;
            this.release.initV = defaultRelease;
            this.threshold.initV = defaultThreshold;
        }

        //public static string GetThresholdLabel(short index)
        //{
        //    index = Utils.ConstrainMax(index, MaxThreshold);
        //    return String.Format("{0} {1}", index + minThresholdValue, levelUnit);
        //}

        public static string GetAttackLabel(short index)
        {
            index = Utils.ConstrainMax(index, MaxAttack);
            return String.Format("{0:f1} {1}", index * attackStep + minAttackValue, attackUnit);
        }

        public static string GetReleaseLabel(short index)
        {
            index = Utils.ConstrainMax(index, MaxRelease);
            return String.Format("{0}{1}", index + minReleaseValue, releaseUnit);
        }

        //public static short CalcThresholdIndexFromText(string text)
        //{
        //    string str = Utils.RemoveStringSuffix(text, levelUnit);
        //    float value;
        //    if (float.TryParse(str, out value))
        //    {
        //        short index = (short)Math.Round(value - minThresholdValue, 1, MidpointRounding.AwayFromZero);
        //        return Utils.ConstrainMax(index, MaxThreshold);
        //    }

        //    return -1;
        //}

        public static short CalcAttackIndexFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, attackUnit);
            float value;
            if (float.TryParse(str, out value))
            {
                short index = (short)Math.Round((value - minAttackValue) / attackStep, 1, MidpointRounding.AwayFromZero);
                return Utils.ConstrainMax(index, MaxAttack);
            }

            return -1;
        }

        public static short CalcReleaseIndexFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, releaseUnit);
            short value;
            if (short.TryParse(str, out value))
            {
                value = (short)(value - minReleaseValue);
                return Utils.ConstrainMax(value, MaxRelease);
            }

            return -1;
        }

#if COMM_TEST
        public readonly static Random random = new Random();
        public virtual void RandomTest()
        {
            this.threshold.RandomTest();       //启动电平
            this.attack.RandomTest();          //启动时间
            this.release.RandomTest();         //恢复时间
        }
#endif
    }

    public class Compressor : CLBase
    {
        public static readonly short MaxRatio = (short)(DataUtils.compressorRatioList.Length - 1);

        public const short defaultRatio = 10;
 
        public readonly ShortParam ratio;      //压缩比例

        public Compressor(byte moduleIndex, byte address, EventHandler<DataParamChangedArgs> dataChanged)
            : base(moduleIndex, address, dataChanged)
        {
            this.ratio = new ShortParam(moduleIndex, (byte)(address + 3), MaxRatio, dataChanged);

            this.ratio.initV = defaultRatio;
        }

        public void CopyFrom(Compressor other)
        {
            base.CopyFrom(other);            
            this.ratio.initV = other.ratio.v;
        }

        public void CopyFrom(X5_CompressorLimiter x5CL)
        {
            base.threshold.initV = (short)(x5CL.threshold.v + base.threshold.max - x5CL.threshold.max);
            base.attack.initV = (short)((DataUtils.compressorAttackList[x5CL.attack.v] - 0.1) * 10);
            base.release.initV = (short)(DataUtils.compressorReleaseList[x5CL.release.v] - 2);
            this.ratio.initV = x5CL.ratio.v;
        }

        public override void Reset()
        {
            base.Reset();

            this.ratio.initV = defaultRatio;
        }

        public bool IsEqual(Compressor other)
        {
            if(this.threshold.v != other.threshold.v ||
               this.attack.v != other.attack.v ||
                this.release.v != other.release.v ||
                this.ratio.v != other.ratio.v)
            {
                return false;
            }

            return true;
        }

        public void SynchronousFrom(Compressor other)
        {
            this.threshold.v = other.threshold.v;
            this.attack.v = other.attack.v;
            this.release.v = other.release.v;
            this.ratio.v = other.ratio.v;
        }
        

        //public static string GetRatioLabel(short index)
        //{
        //    index = Utils.ConstrainMax(index, MaxRatio);
        //    double ra = DataUtils.compressorRatioList[index];
        //    if (ra < 12)
        //    {
        //        return String.Format("{0:0.0}", ra);
        //    }
        //    else
        //    {
        //        return String.Format("{0}", ra);
        //    }
        //}

        //public static short CalcRatioIndexFromText(string text)
        //{
        //    double value;
        //    text = text.Trim();
        //    if (double.TryParse(text, out value))
        //    {
        //        return DataUtils.FindNearestIndexFromOrderedList(value, DataUtils.compressorRatioList);
        //    }

        //    return -1;
        //}
#if COMM_TEST
        public override void RandomTest()
        {
            base.RandomTest();
            this.ratio.RandomTest();
        }
#endif
    }

    public class Limiter : CLBase
    {
        public const short MaxClipThreshold = 10;
        public const short defaultClipThreshold = 2;
        
        public const short minClipThresholdValue = 2;
           
        public readonly ShortParam clipThreshold;   //削波启动电平
        public readonly ShortParam clipRelease;     //削波恢复时间

        public bool auto { get; set; }

        public Limiter(byte moduleIndex, byte address, EventHandler<DataParamChangedArgs> dataChanged):
            base(moduleIndex, address, dataChanged)
        {
            this.clipThreshold = new ShortParam(moduleIndex, (byte)(address + 3), MaxClipThreshold, dataChanged);
            this.clipRelease = new ShortParam(moduleIndex, (byte)(address + 4), MaxRelease, dataChanged);

            this.clipThreshold.initV = defaultClipThreshold;
            this.clipRelease.initV = defaultRelease;

            this.auto = false;
        }

        public void CopyFrom(Limiter other)
        {
            base.CopyFrom(other);
            this.clipThreshold.initV = other.clipThreshold.v;
            this.clipRelease.initV = other.clipRelease.v;
            this.auto = other.auto;
        }

        public void CopyFrom(X5_CompressorLimiter x5CL)
        {
            base.threshold.initV = (short)(x5CL.threshold.v + base.threshold.max - x5CL.threshold.max);
            base.attack.initV = (short)((DataUtils.compressorAttackList[x5CL.attack.v] - 0.1) * 10);
            base.release.initV = (short)(DataUtils.compressorReleaseList[x5CL.release.v] - 2);
            this.clipThreshold.initV = defaultClipThreshold;
            this.clipRelease.initV = defaultRelease;
        }

        public override void Reset()
        {
            base.Reset();
            this.clipThreshold.initV = defaultClipThreshold;
            this.clipRelease.initV = defaultRelease;
        }

        public static string GetClipThresholdLabel(short index)
        {
            index = Utils.ConstrainMax(index, MaxClipThreshold);
            return String.Format("{0} {1}", index + minClipThresholdValue, levelUnit);
        }

        public static short CalcClipThresholdIndexFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, levelUnit);
            float value;
            if (float.TryParse(str, out value))
            {
                short index = (short)Math.Round(value - minClipThresholdValue, 1, MidpointRounding.AwayFromZero);
                return Utils.ConstrainMax(index, MaxClipThreshold);
            }

            return -1;
        }

        public void AutoModeChangedHandler(double freq)
        {
            if (this.auto)
            {
                this.attack.v = AutoModeHelper.CalcLimiterAttackFromHpfFreq(freq);
                this.release.v = AutoModeHelper.defRelease;
                this.clipThreshold.v = AutoModeHelper.defClipThreshold;
                this.clipRelease.v = AutoModeHelper.defClipRelease;
            }
        }

        public void HpfFreqChangedWhenAutoMode(double freq)
        {
            if (this.auto)
            {
                this.attack.v = AutoModeHelper.CalcLimiterAttackFromHpfFreq(freq);
            }
        }

        public class AutoModeHelper
        {
            public const short defRelease = 14;
            public const short defClipThreshold = 2;
            public const short defClipRelease = 14;

            private static readonly Dictionary<double, short> hpfFreqMapAttak = new Dictionary<double, short>()
            {
                {31, 449},
                {63, 159},
                {125, 79},
                {250, 39},
                {500, 19},
                {1000, 9},
                {2000, 4},
                {32000, 2},
            };
            
            public static short CalcLimiterAttackFromHpfFreq(double freq)
            {
                Debug.Assert(freq > 0 && freq <= 32000);

                double[] freqs = new List<double>(hpfFreqMapAttak.Keys).ToArray();
                short freqIndex = 0;//DataUtils.FindIndexFromOrderedList(freq, freqs);
                if(freq > freqs[0])
                {
                    freqIndex = (short)(DataUtils.FindIndexFromOrderedList(freq, freqs) + 1);
                }

                if(hpfFreqMapAttak.ContainsKey(freqs[freqIndex]))
                {
                    return hpfFreqMapAttak[freqs[freqIndex]];
                }

                throw new Exception("CalcLimiterAttackFromHpfFreq()...计算频率错误!");
            }
        }
    }

    public class SignalMixer
    {
        public const string levelUnitInPercentage = "%";

        public readonly short maxLevel;
        public readonly ShortParam level;
        public readonly BoolParam phase;

        public SignalMixer(byte moduleIndex, byte levelAddress, byte phaseAddress, short maxLevel_, EventHandler<DataParamChangedArgs> dataChanged)
        {
            this.maxLevel = maxLevel_;
            this.level = new ShortParam(moduleIndex, levelAddress, maxLevel, dataChanged);
            this.phase = new BoolParam(moduleIndex, phaseAddress, dataChanged);
            this.phase.initV = (short)SignalPhase.Positive;
        }

        public void CopyFrom(SignalMixer other)
        {
            this.level.initV = other.level.v;
            this.phase.initV = other.phase.v;
        }

        public void Reset()
        {
            this.level.v = 0;
            this.phase.initV = (short)SignalPhase.Positive;
        }

#if COMM_TEST
        public void RandomTest()
        {
            this.level.RandomTest();
            this.phase.RandomTest();
        }
#endif
    }

    public class OutputMixer
    {
        public readonly SignalMixer singMixer;
        public readonly SignalMixer discoMixer;

        public OutputMixer(byte moduleIndex, byte levelAddress, byte phaseAddress, short maxLevel_, EventHandler<DataParamChangedArgs> dataChanged)
        {
            this.singMixer = new SignalMixer(moduleIndex, levelAddress, phaseAddress, maxLevel_, dataChanged);
            this.discoMixer = new SignalMixer(moduleIndex, levelAddress, phaseAddress, maxLevel_, dataChanged);
        }

        public void CopyFrom(OutputMixer other)
        {
            this.singMixer.CopyFrom(other.singMixer);
            this.discoMixer.CopyFrom(other.discoMixer);
        }

        public void Reset()
        {
            this.singMixer.Reset();
            this.discoMixer.Reset();
        }
    }

    public class Level
    {
        public const string levelUnitIndB = "dB";
        public const string levelUnitInPercentage = "%";
        public const string MuteLevel = "-∞";
        public const short Hundred = 100;

        public const short MaxPhaseIndex = 1;
        public const short defMaxLevel = 100;
        public const double levelBase = -40;
        public readonly short maxLevel;

        public readonly ShortParam level;
        public readonly BoolParam phase;

        public Level(byte moduleIndex, byte levelAddress, byte phaseAddress, EventHandler<DataParamChangedArgs> dataChanged, short maxLevel_ = defMaxLevel)
        {
            this.maxLevel = maxLevel_;
            this.level = new ShortParam(moduleIndex, levelAddress, maxLevel, dataChanged);
            this.phase = new BoolParam(moduleIndex, phaseAddress, dataChanged);
            this.phase.initV = 1;
        }

        public void CopyFrom(Level other)
        {
            //Debug.Assert(this._maxLevel == other._maxLevel);

            this.level.initV = other.level.v;
            this.phase.initV = other.phase.v;
        }

        public void Reset()
        {
            this.level.v = 0;
            this.phase.initV = 1;
        }

        public static string GetLevelLabel(short index)
        {
            return string.Format("{0} {1}", index, levelUnitInPercentage);
        }

        public static string GetOutputLevelIndBLabel(short index)
        {
            if (index <= 0)
            {
                return string.Format("{0} {1}", MuteLevel, levelUnitIndB);
            }
            else
            {
                return string.Format("{0:0.0} {1}", index * 0.1 + levelBase, levelUnitIndB);
            }
        }

        public static string GetOutputLevelInPercentageLabel(short index)
        {
            if (index == 0)
            {
                return string.Format("0 {0}", levelUnitInPercentage);
            }
            else
            {
                double levelIndB = index * 0.1 + levelBase;
                double linearInPercentage = DSP.DSPHelper.DecibelsToLinear(levelIndB) * Hundred;
                if (linearInPercentage < 75)
                {
                    return string.Format("{0:f2} {1}", linearInPercentage, levelUnitInPercentage);
                }
                else if (linearInPercentage < 100)
                {
                    return string.Format("{0:f1} {1}", linearInPercentage, levelUnitInPercentage);
                }
                else
                {
                    return string.Format("{0:f0} {1}", linearInPercentage, levelUnitInPercentage);
                }
            }
        }

        public short CalcOutputLevelIndBIndexFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, levelUnitIndB);
            double value;
            if (double.TryParse(str, out value))
            {
                short index = (short)Math.Round((value - Level.levelBase) * 10, 0, MidpointRounding.AwayFromZero);
                return Utils.ConstrainMax(index, this.maxLevel);
            }

            return -1;
        }

        public short CalcOutputLevelInPercentageIndexFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, levelUnitInPercentage);
            double value;
            if (double.TryParse(str, out value) && value >= 0)
            {
                double threshold = DSP.DSPHelper.DecibelsToLinear(0.1 + levelBase) * Hundred;
                if (value < threshold)
                {
                    return 0;
                }

                value /= Hundred;

                double decibels = GetDecibelsLevel(value);
                short index = (short)Math.Round((decibels - Level.levelBase) * 10, 0, MidpointRounding.AwayFromZero);

                return Utils.ConstrainMax(index, this.maxLevel);
            }

            return -1;
        }

        private static double GetDecibelsLevel(double line)
        {
            System.Diagnostics.Debug.Assert(line > 0);
            return 20 * Math.Log10(line);
        }

#if COMM_TEST
        public void RandomTest()
        {
            this.level.RandomTest();
            this.phase.RandomTest();
        }
#endif
    }

    public class BassExciter
    {
        protected const string FreqUnit = "Hz";
        protected const string LevelUnit = "dB";

        public const short XoverFreqBase = 30;
        public const short MaxXoverFreqIndex = 170;
        public const short DefaultXoverFreqIndex = 50;
        public const short MaxFBGainLevelIndex = 200;
        public const short DefaultFBGainLevelIndex = 0;
        public const short MaxBoostLevelIndex = 120;
        public const short DefaultBoostLevelIndex = 60;
        
        public readonly BoolParam on;           //On or Off
        public readonly ShortParam xoverFreq;   //分频点频率 30~200Hz,默认80Hz（0~170，50)
        public readonly ShortParam fbGain;      //反馈增益 1.00~2.00,默认1.00 (0~100、0)
        public readonly ShortParam boostLevel;  //增强电平 -6.0dB~+6.0dB,默认0dB (0~120、60)

        public string label
        {
            get
            {
                return string.Format("{0} Hz", freqValue);
            }
        }

        public float freqValue
        {
            get
            {
                return this.xoverFreq.v + XoverFreqBase;
            }
        }
        

        public BassExciter(byte moduleIndex, byte address, EventHandler<DataParamChangedArgs> dataChanged)
        {
            this.on = new BoolParam(moduleIndex, (byte)(address + 0), dataChanged);
            this.xoverFreq = new ShortParam(moduleIndex, (byte)(address + 1), MaxXoverFreqIndex, dataChanged);
            this.fbGain = new ShortParam(moduleIndex, (byte)(address + 2), MaxFBGainLevelIndex, dataChanged);
            this.boostLevel = new ShortParam(moduleIndex, (byte)(address + 3), MaxBoostLevelIndex, dataChanged);

            this.xoverFreq.initV = DefaultXoverFreqIndex;
            this.fbGain.initV = DefaultFBGainLevelIndex;
            this.boostLevel.initV = DefaultBoostLevelIndex;
        }

        public void CopyFrom(BassExciter other)
        {
            this.on.initV = other.on.v;
            this.xoverFreq.initV = other.xoverFreq.v;
            this.fbGain.initV = other.fbGain.v;
            this.boostLevel.initV = other.boostLevel.v;
        }

        public static string GetXoverFreqLabel(short index)
        {
            index = Utils.ConstrainMax(index, MaxXoverFreqIndex);
            return string.Format("{0} {1}", index + 30, FreqUnit);
        }

        public static short CalcXOverFreqIndexFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, FreqUnit);
            float value;
            if (float.TryParse(str, out value))
            {
                short index = (short)Math.Round((value - 30), 0, MidpointRounding.AwayFromZero);
                return Utils.ConstrainMax(index, MaxXoverFreqIndex);
            }

            return -1;
        }

        public static string GetFBGainLabel(short index)
        {
            index = Utils.ConstrainMax(index, MaxFBGainLevelIndex);
            return string.Format("{0:0.00}", index * 0.01);
        }

        public static short CalcFBGainIndexFromText(string text)
        {
            string str = text.Trim();
            float value;
            if (float.TryParse(str, out value))
            {
                short index = (short)Math.Round(value * 100, 0, MidpointRounding.AwayFromZero);
                return Utils.ConstrainMax(index, MaxFBGainLevelIndex);
            }

            return -1;
        }

        public static string GetBoostLevelLabel(short index)
        {
            index = Utils.ConstrainMax(index, MaxBoostLevelIndex);
            return string.Format("{0:0.0} {1}", index * 0.1 - 6, LevelUnit);
        }

        public static short CalcBoostLevelIndexFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, LevelUnit);
            float value;
            if (float.TryParse(str, out value))
            {
                short index = (short)Math.Round((value + 6.00) * 10, 0, MidpointRounding.AwayFromZero);
                return Utils.ConstrainMax(index, MaxBoostLevelIndex);
            }

            return -1;
        }

        public void ResetXoverFreq()
        {
            this.xoverFreq.v = DefaultXoverFreqIndex;
        }

        public void ResetFBGain()
        {
            this.fbGain.v = DefaultFBGainLevelIndex;
        }

        public void ResetBoostLevel()
        {
            this.boostLevel.v = DefaultBoostLevelIndex;
        }

#if COMM_TEST
        public void RandomTest()
        {
            //this.on.RandomTest();
            this.xoverFreq.RandomTest();
            this.fbGain.RandomTest();
            this.boostLevel.RandomTest();
        }
#endif
    }

    public class Exciter
    {
        protected const string FreqUnit = "Hz";
        protected const string LevelUnit = "dB";

        public const short FreqBase = 30;
        public const short BandWidthIndex = 30;
        public const float BandWidthStep = 0.005f;

        public const short MaxXoverFreqIndex = 5970;
        public const short DefaultXoverFreqIndex = 550;
        public const short MaxBandWidthIndex = 270;
        public const short DefaultBandWidthIndex = 70;
        public const short MaxFBGainLevelIndex = 200;
        public const short DefaultFBGainLevelIndex = 0;
        public const short MaxBoostLevelIndex = 120;
        public const short DefaultBoostLevelIndex = 60;
        public const short MaxOffsetLevelIndex = 1500;
        public const short DefaultOffsetLevelIndex = 0;

        public readonly BoolParam on;           //On or Off
        public readonly ShortParam freq;   //分频点频率 60~6000Hz,默认80Hz（0~5940，550)
        public readonly ShortParam bandWidth;   //带宽/倍频程
        public readonly ShortParam fbGain;      //反馈增益 1.00~2.00,默认1.00 (0~100、0)
        public readonly ShortParam boostLevel;  //增强电平 -6.0dB~+6.0dB,默认0dB (0~120、60)
        public readonly ShortParam offsetLevel; //补偿电平 1.000~+2.000,默认1.0（0~500，0）

        public static float GetBandWidthFromIndex(short index)
        {
            return (index + BandWidthIndex) * BandWidthStep;
        }

        public string label
        {
            get
            {
                return string.Format("Freq: {0} Hz{1}Band Width: {2:0.000}", centerfreqValue, Environment.NewLine, GetBandWidthFromIndex(this.bandWidth.v));
            }
        }

        public float centerfreqValue
        {
            get
            {
                return this.freq.v + FreqBase;
            }
        }

        public float freqBandLowerLimit
        {
            get
            {
                return this.centerfreqValue / (1.0f + GetBandWidthFromIndex(this.bandWidth.v));
            }
        }

        public float freqBandUpperLimit
        {
            get
            {
                return this.centerfreqValue * (1.0f + GetBandWidthFromIndex(this.bandWidth.v));
            }
        }

        public Exciter(byte moduleIndex, byte address, EventHandler<DataParamChangedArgs> dataChanged)
        {
            this.on = new BoolParam(moduleIndex, (byte)(address + 0), dataChanged);
            this.freq = new ShortParam(moduleIndex, (byte)(address + 1), MaxXoverFreqIndex, dataChanged);
            this.bandWidth = new ShortParam(moduleIndex, (byte)(address + 2), MaxBandWidthIndex, dataChanged);
            this.fbGain = new ShortParam(moduleIndex, (byte)(address + 3), MaxFBGainLevelIndex, dataChanged);
            this.boostLevel = new ShortParam(moduleIndex, (byte)(address + 4), MaxBoostLevelIndex, dataChanged);
            this.offsetLevel = new ShortParam(moduleIndex, (byte)(address + 5), MaxOffsetLevelIndex, dataChanged);

            this.freq.initV = DefaultXoverFreqIndex;
            this.bandWidth.initV = DefaultBandWidthIndex;
            this.fbGain.initV = DefaultFBGainLevelIndex;
            this.boostLevel.initV = DefaultBoostLevelIndex;
            this.offsetLevel.initV = DefaultOffsetLevelIndex;
        }

        public void Reset()
        {
            //this.xoverFreq.v = DefaultXoverFreqIndex;
            //this.bandWidth.v = DefaultBandWidthIndex;
            //this.fbGain.v = DefaultFBGainLevelIndex;
            //this.boostLevel.v = DefaultBoostLevelIndex;
            //this.offsetLevel.v = DefaultOffsetLevelIndex;
        }

        public void CopyFrom(Exciter other)
        {
            this.on.initV = other.on.v;
            this.freq.initV = other.freq.v;
            this.bandWidth.initV = other.bandWidth.v;
            this.fbGain.initV = other.fbGain.v;
            this.boostLevel.initV = other.boostLevel.v;
            this.offsetLevel.initV = other.offsetLevel.v;
        }

        public static string GetXoverFreqLabel(short index)
        {
            index = Utils.ConstrainMax(index, MaxXoverFreqIndex);
            return string.Format("{0} {1}", index + FreqBase, FreqUnit);
        }

        public static short CalcXOverFreqIndexFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, FreqUnit);
            float value;
            if (float.TryParse(str, out value))
            {
                short index = (short)Math.Round((value - FreqBase), 0, MidpointRounding.AwayFromZero);
                return Utils.ConstrainMax(index, MaxXoverFreqIndex);
            }

            return -1;
        }

        public static string GetBandWidthLabel(short index)
        {
            index = Utils.ConstrainMax(index, MaxBandWidthIndex);
            float bandWidth = GetBandWidthFromIndex(index);
            return string.Format("{0:0.000}", bandWidth);
        }

        public static short CalcBandWidthIndexFromText(string text)
        {
            string str = text.Trim();
            float value;
            if (float.TryParse(str, out value))
            {
                short index = (short)Math.Round((value / BandWidthStep - BandWidthIndex), 0, MidpointRounding.AwayFromZero);
                return Utils.ConstrainMax(index, MaxBandWidthIndex);
            }

            return -1;
        }

        public static string GetFBGainLabel(short index)
        {
            index = Utils.ConstrainMax(index, MaxFBGainLevelIndex);
            return string.Format("{0:0.00}", index * 0.01);
        }

        public static short CalcFBGainIndexFromText(string text)
        {
            string str = text.Trim();
            float value;
            if (float.TryParse(str, out value))
            {
                short index = (short)Math.Round(value * 100, 0, MidpointRounding.AwayFromZero);
                return Utils.ConstrainMax(index, MaxFBGainLevelIndex);
            }

            return -1;
        }

        public static string GetBoostLevelLabel(short index)
        {
            index = Utils.ConstrainMax(index, MaxBoostLevelIndex);
            return string.Format("{0:0.0} {1}", index * 0.1 - 6, LevelUnit);
        }

        public static short CalcBoostLevelIndexFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, LevelUnit);
            float value;
            if (float.TryParse(str, out value))
            {
                short index = (short)Math.Round((value + 6.00) * 10, 0, MidpointRounding.AwayFromZero);
                return Utils.ConstrainMax(index, MaxBoostLevelIndex);
            }

            return -1;
        }

        public static string GetOffsetLevelLabel(short index)
        {
            index = Utils.ConstrainMax(index, MaxOffsetLevelIndex);
            return string.Format("{0:0.000}", index * 0.002 + 1.0);
        }

        public static short CalcOffsetLevelIndexFromText(string text)
        {
            string str = text.Trim();
            float value;
            if (float.TryParse(str, out value))
            {
                short index = (short)Math.Round((value - 1.0) * 500, 0, MidpointRounding.AwayFromZero);
                return Utils.ConstrainMax(index, MaxOffsetLevelIndex);
            }

            return -1;
        }

        public void ResetFreq()
        {
            this.freq.v = DefaultXoverFreqIndex;
        }

        public void ResetBandWidth()
        {
            this.bandWidth.v = DefaultBandWidthIndex;
        }

        public void ResetFBGain()
        {
            this.fbGain.v = DefaultFBGainLevelIndex;
        }

        public void ResetBoostLevel()
        {
            this.boostLevel.v = DefaultBoostLevelIndex;
        }

#if COMM_TEST
        public void RandomTest()
        {
            //this.on.RandomTest();
            this.freq.RandomTest();
            this.bandWidth.RandomTest();
            this.fbGain.RandomTest();
            this.boostLevel.RandomTest();
            this.offsetLevel.RandomTest();
        }
#endif
    }

    public class OutputLevel
    {
        private const short MaxLevel = 500;

        public readonly Level singLevel;
        public readonly Level discoLevel;

        public short maxLevel
        {
            get;
            private set;
        }

        public OutputLevel(byte moduleIndex, byte address, EventHandler<DataParamChangedArgs> dataChanged, short maxLevel_ = MaxLevel)
        {
            this.maxLevel = maxLevel_;

            this.singLevel = new Level(moduleIndex, address, (byte)(address + 1), dataChanged, this.maxLevel);
            this.discoLevel = new Level(moduleIndex, address, (byte)(address + 1), dataChanged, this.maxLevel);
        }

        public OutputLevel(byte moduleIndex, byte levelAddress, byte phaseAddress, EventHandler<DataParamChangedArgs> dataChanged, short maxLevel_ = MaxLevel)
        {
            this.maxLevel = maxLevel_;

            this.singLevel = new Level(moduleIndex, levelAddress, phaseAddress, dataChanged, this.maxLevel);
            this.discoLevel = new Level(moduleIndex, levelAddress, phaseAddress, dataChanged, this.maxLevel);
        }

        public void CopyFrom(OutputLevel other)
        {
            //Debug.Assert(this._maxLevel == other._maxLevel);

            //this.singLevel.initV = other.singLevel.v;
            //this.discoLevel.initV = other.discoLevel.v;
            //this.singPhase.initV = other.singPhase.v;
            //this.discoPhase.initV = other.discoPhase.v;

            this.singLevel.CopyFrom(other.singLevel);
            this.discoLevel.CopyFrom(other.discoLevel);
        }

        public void Reset()
        {
            this.singLevel.Reset();
            this.discoLevel.Reset();
        }
    }

    public class BoolParam : ShortParam
    {
        private const short MaxValue = 1;

        public BoolParam(byte moduleIndex_, byte address, EventHandler<DataParamChangedArgs> dataChangedHandler = null):
            base(moduleIndex_, address, MaxValue, dataChangedHandler)
        {
            
        }

        public BoolParam():base(MaxValue)
        {

        }

        public bool value
        {
            get
            {
                return this._value > 0;
            }
            set
            {
                this.v = value ? (short)1 : (short)0;
            }
        }
    }

    //public class SystemFrontLevel
    //{
    //    public float musicLeftLevel = 0;
    //    public float musicRightLevel = 0;
    //    public float micLevel = 0;

    //    public SystemFrontLevel(float musicLeftLevel_, float musicRightLevel_, float micLevel_)
    //    {
    //        musicLeftLevel = musicLeftLevel_;
    //        musicRightLevel = musicRightLevel_;
    //        micLevel = micLevel_;
    //    }

    //    public float musicLevel
    //    {
    //        get
    //        {
    //            return (float)(20 * Math.Log10(Math.Pow(10, musicLeftLevel / 20) + Math.Pow(10, musicRightLevel / 20)));
    //        }
    //    }

    //    public void AddOffset(float offset)
    //    {
    //        musicLeftLevel += offset;
    //        musicRightLevel += offset;
    //        micLevel += offset;
    //    }
    //}

    public class SystemEndLevel
    {
        public float musicLevel = 0;
        public float micLevel = 0;
        public float effectLevel = 0;

        public SystemEndLevel(float musicLevel_, float micLevel_, float effectLevel_)
        {
            musicLevel = musicLevel_;
            micLevel = micLevel_;
            effectLevel = effectLevel_;
        }
    }

    

    

    

    //public class SystemStatus
    //{
    //    //public readonly SystemFrontLevel frontLevel;
    //    public readonly QuadChannelLimiterStatus qcls;

    //    public SystemStatus(QuadChannelLimiterStatus qcls_)
    //    {
    //        this.qcls = qcls_;
    //    }
    //}

    //public class SystemLevel
    //{
    //    public float musicLevel = 0;
    //    public float micLevel = 0;
    //    public float effectLevel = 0;
    //    public float rawMicLevel = 0;

    //    public SystemLevel(float musicLevel_, float micLevel_, float effectLevel_, float rawMicLevel_)
    //    {
    //        this.musicLevel = musicLevel_;
    //        this.micLevel = micLevel_;
    //        this.effectLevel = effectLevel_;
    //        this.rawMicLevel = rawMicLevel_;
    //    }

    //    public void Test()
    //    {
    //        float offset = 80;
    //        this.musicLevel += offset;
    //        this.micLevel += offset;
    //        this.effectLevel += offset;
    //        this.rawMicLevel += offset;
    //    }
    //}

    

    //public class DEQParam
    //{
    //    public readonly byte moduleIndex;
    //    public readonly byte addr;
    //    public readonly short deqIndex;
    //    public readonly short value;

    //    public DEQParam(byte moduleIndex_, byte address, short deqIndex_, short value_)
    //    {
    //        this.moduleIndex = moduleIndex_;
    //        this.addr = address;
    //        this.deqIndex = deqIndex_;
    //        this.value = value_;
    //    }
    //}

    
}
