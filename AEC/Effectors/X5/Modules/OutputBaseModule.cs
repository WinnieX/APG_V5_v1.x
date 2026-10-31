using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Common;
using AEC.Comm.X5Comm;

namespace AEC.Effectors.X5.Modules
{
    public abstract class OutputBaseModule : ModuleBase
    {
        public const double MaxDelayInms = 50;
        public const double DelayStep = 0.1;
        public static readonly short MaxDelayIndex = (short)Math.Round(MaxDelayInms / DelayStep, 1, MidpointRounding.AwayFromZero);
        public const short MaxDefOutputLevelIndex = 100;
        public const short MaxSubOutputLevelIndex = 200;
        public const double HPFMaxFreq = 303;
        public const double LPFMaxFreq = 303;
        public const double HPFInitFreq = 0;

        public static readonly double[] initFreqList_5_Band = new double[] { 62.5, 250, 1000.0, 4000, 16000.0 };
        public static readonly double[] initFreqList_3_Band = new double[] { 99, 1000, 10100 };
        
        public const string DelayUnit = "ms";        

        public readonly X5_CompressorLimiter limiter;

        protected BoolParam eqIsPassParam;

        protected readonly BoolParam singEQBypassParam;
        protected readonly BoolParam discoEQBypassParam;

        public readonly AutoWorkMode autoWorkMode;

        public readonly OutputChannelType outputChannelType;

        public abstract bool outputIsMute
        {
            get;
            set;
        }

        public override bool eqCanBeChanged
        {
            get
            {
                return !eqGroup.isBypass && isManualMode;
                //return !eqGroup.isPass;
            }
        }

        public X5Mixer mixer
        {
            get;
            protected set;
        }

        //public static readonly ChannelDelayParamLabelProvider channelDelayParamLabelProvider = new ChannelDelayParamLabelProvider(DelayStep, MaxDelayInms);

        public OutputBaseModule(OutputChannelType outputChannelType, byte index, EventBus effectorEvent_, AutoWorkMode autoWorkMode_) :
            base(index:index, effectorEvent_:effectorEvent_)
        {
            this.outputChannelType = outputChannelType;

            this.limiter = new X5_CompressorLimiter(
                this.moduleIndex,
                (byte)X5CommProtocol.OutputBaseParamAddr.attack,
                this.OnParamChanged);

            this.singEQBypassParam = new BoolParam(this.moduleIndex, (byte)X5CommProtocol.OutputBaseParamAddr.eqPass, this.OnParamChanged);
            this.discoEQBypassParam = new BoolParam(this.moduleIndex, (byte)X5CommProtocol.OutputBaseParamAddr.eqPass, this.OnParamChanged);
            
            this.autoWorkMode = autoWorkMode_;
        }

        public void CopyFrom(OutputBaseModule other)
        {
            this.limiter.CopyFrom(other.limiter);
        }

        protected bool isSingMode
        {
            get
            {
                return this.autoWorkMode.isSing;
            }
        }

        protected bool isManualMode
        {
            get
            {
                return this.autoWorkMode.isManual;
            }
        }

        public override void ResetData()
        {
            this.limiter.Reset();
        }

        //public static string GetDelayLabel(short index)
        //{
        //    return string.Format("{0:0.0} {1}", index * DelayStep, DelayUnit);
        //}

        //public static short CalcDelayIndexFromText(string text)
        //{
        //    string str = Utils.RemoveStringSuffix(text, DelayUnit);
        //    double value;
        //    if (double.TryParse(str, out value))
        //    {
        //        short index = (short)Math.Round(value / DelayStep, 0, MidpointRounding.AwayFromZero);
        //        return Utils.ConstrainMax(index, OutputBaseModule.MaxDelayIndex);
        //    }

        //    return -1;
        //}

        public override bool hpfFreqCanBeChanged
        {
            get
            {
                return this.hpf != null && this.autoWorkMode.isManual;
            }
        }

        public override bool lpfFreqCanBeChanged
        {
            get
            {
                return this.lpf != null && this.autoWorkMode.isManual;
            }
        }

#if COMM_TEST
        public override void RandomTest()
        {
            //if (isManualMode)
            //{
            //    this.musicLevelParam.RandomTest();
            //    this.micLevelParam.RandomTest();
            //    this.echoLevelParam.RandomTest();
            //    this.reverbLevelParam.RandomTest();
            //}

            //this.limiter.RandomTest();

            //this.hpfFreqParam.RandomTest();
            //this.hpfTypeParam.RandomTest();
            //this.lpfFreqParam.RandomTest();
            //this.lpfTypeParam.RandomTest();

            //this.hpf.freqIndex = this.hpfFreqParam.v;
            //this.hpf.typeIndex = this.hpfTypeParam.v;
            //this.lpf.freqIndex = this.lpfFreqParam.v;
            //this.lpf.typeIndex = this.lpfTypeParam.v;

            //this.eqGroup.RandomTest();
            //base.UpdateEQGroup();
            //this.eqIsPassParam.RandomTest();
            //this.eqGroup.isBypass = this.eqIsPassParam.value;
        }
#endif
    }
}
