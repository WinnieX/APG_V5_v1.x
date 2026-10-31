using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Common;
using AEC.Comm.KXComm;

namespace AEC.Effectors.KX.Modules
{
    public abstract class OutputBaseModule : ModuleBase
    {
        public static readonly double[] initFreqList = new double[] {31.3, 62.5, 125.0, 250.0, 500.0, 1000.0, 2000.0, 4000.0, 8000.0};

        public const double MaxDelayInms = 80;
        public const double DelayStep = 1000 / AEC.Core.DSP.DSPHelper.sr_48k;
        public static readonly short MaxDelayIndex = (short)Math.Round(MaxDelayInms / DelayStep, 1, MidpointRounding.AwayFromZero);
        
        public const string DelayUnit = "ms";
        public const short maxCompressorThreshold = 36;
        public const short compressorThresholdBase = -22;

        public readonly OutputLevel musicLevel;
        public readonly OutputLevel micLevel;
        public readonly OutputLevel echoLevel;
        public readonly OutputLevel reverbLevel;
        
        public readonly HighPassFilter highPassFilter;
        public readonly LowPassFilter lowPassFilter;

        private readonly ShortParam hpfTypeParam;
        private readonly ShortParam hpfFreqParam;
        private readonly ShortParam lpfTypeParam;
        private readonly ShortParam lpfFreqParam;

        public readonly Limiter limiter;

        protected BoolParam eqIsPassParam;

        public readonly EQGroup _eqGroup = null;

        public readonly AutoWorkMode autoWorkMode;

        public readonly OutputChannelType outputChannelType;

        public static readonly ChannelDelayParamLabelProvider channelDelayParamLabelProvider = new ChannelDelayParamLabelProvider(AEC.Core.DSP.DSPHelper.sr_48k, MaxDelayInms);

        public abstract bool outputIsMute
        {
            get;
            set;
        }

        public OutputBaseModule(OutputChannelType outputChannelType, byte index, EventBus effectorEvent_, AutoWorkMode autoWorkMode_) :
            base(index:index, effectorEvent_:effectorEvent_)
        {
            this.outputChannelType = outputChannelType;

            this.musicLevel = new OutputLevel(
                this.moduleIndex,
                (byte)KXCommProtocol.OutputBaseParamAddr.musicSingLevel,
                this.OnParamChanged);
            this.micLevel = new OutputLevel(
                this.moduleIndex,
                (byte)KXCommProtocol.OutputBaseParamAddr.micSingLevel,
                this.OnParamChanged);
            this.echoLevel = new OutputLevel(
                this.moduleIndex,
                (byte)KXCommProtocol.OutputBaseParamAddr.echoSingLevel,
                this.OnParamChanged);
            this.reverbLevel = new OutputLevel(
                this.moduleIndex,
                (byte)KXCommProtocol.OutputBaseParamAddr.reverbSingLevel,
                this.OnParamChanged);

            this.highPassFilter = XPFilterFactory.CreateProfessinalXoverHighPassFilter();
            this.lowPassFilter = XPFilterFactory.CreateProfessinalXoverLowPassFilter();
            
            this.limiter = new Limiter(this.moduleIndex, (byte)KXCommProtocol.OutputBaseParamAddr.attack, this.OnParamChanged);

            this._eqGroup = new EQGroup(initFreqList) ;

            this.hpfTypeParam = new ShortParam(this.moduleIndex, (byte)KXCommProtocol.OutputBaseParamAddr.hpfMode, this.highPassFilter.typeMaxIndex, this.OnParamChanged);
            this.hpfFreqParam = new ShortParam(this.moduleIndex, (byte)KXCommProtocol.OutputBaseParamAddr.hpfFreq, this.highPassFilter.freqMaxIndex, this.OnParamChanged);
            this.lpfTypeParam = new ShortParam(this.moduleIndex, (byte)KXCommProtocol.OutputBaseParamAddr.lpfMode, this.lowPassFilter.typeMaxIndex, this.OnParamChanged);
            this.lpfFreqParam = new ShortParam(this.moduleIndex, (byte)KXCommProtocol.OutputBaseParamAddr.lpfFreq, this.lowPassFilter.freqMaxIndex, this.OnParamChanged);
            this.eqIsPassParam = new BoolParam(this.moduleIndex, (byte)KXCommProtocol.OutputBaseParamAddr.eqPass, this.OnParamChanged);

            this.highPassFilter.BindingParams(this.hpfFreqParam, this.hpfTypeParam, null);
            this.lowPassFilter.BindingParams(this.lpfFreqParam, this.lpfTypeParam, null);
            this._eqGroup.BindingBypassParam(this.eqIsPassParam);

            this.autoWorkMode = autoWorkMode_;
        }

        public void CopyFrom(OutputBaseModule other)
        {
            this.musicLevel.CopyFrom(other.musicLevel);
            this.micLevel.CopyFrom(other.micLevel);
            this.echoLevel.CopyFrom(other.echoLevel);
            this.reverbLevel.CopyFrom(other.reverbLevel);
            this.highPassFilter.CopyFrom(other.highPassFilter);
            this.lowPassFilter.CopyFrom(other.lowPassFilter);
            this.limiter.CopyFrom(other.limiter);
            this._eqGroup.CopyFrom(other._eqGroup);
            this.dataIsPrivateParam = other.dataIsPrivateParam;
        }

        public override void InitFreqResp()
        {
            base.InitFreqResp();

            this._eqGroup.InitFreqResp();
            this.highPassFilter.UpdateFreqResp();
            this.lowPassFilter.UpdateFreqResp();

            this.UpdateFreqResp();
        }

        public override void UpdateFreqResp()
        {
            base.UpdateFreqResp();

            EQGroupAddHLPFilterFreqResp(this.freqResp, eqGroup, lpf, hpf);
        }

        public override void UpdateXPFilter(XPFilterType xpFilterType, FilterElement filterElement)
        {
            base.UpdateXPFilter(xpFilterType, filterElement);
            if(xpFilterType == XPFilterType.HighPass && filterElement == FilterElement.Freq)
            {
                if (this.limiter.auto)
                {
                    this.limiter.HpfFreqChangedWhenAutoMode(this.highPassFilter.freq);
                }
            }
        }

        //public static string GetDelayLabel(short index)
        //{
        //    return string.Format("{0:0.00} {1}", index * DelayStep, DelayUnit);
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

        public override EQGroup eqGroup
        {
            get
            {
                return this._eqGroup;
            }
        }

        public override HighPassFilter hpf
        {
            get
            {
                return this.highPassFilter;
            }
        }

        public override LowPassFilter lpf
        {
            get
            {
                return this.lowPassFilter;
            }
        }

        private bool isSingMode
        {
            get
            {
                return autoWorkMode.workSelect == WorkSelect.Sing;
            }
        }

        private bool isManualMode
        {
            get
            {
                return autoWorkMode.workMode == WorkMode.Manual;
            }
        }

        public Level musicLevelParam
        {
            get
            {
                return isSingMode ? this.musicLevel.singLevel : this.musicLevel.discoLevel;
            }
        }

        public Level micLevelParam
        {
            get
            {
                return isSingMode ? this.micLevel.singLevel : this.micLevel.discoLevel;
            }
        }

        public Level echoLevelParam
        {
            get
            {
                return isSingMode ? this.echoLevel.singLevel : this.echoLevel.discoLevel;
            }
        }

        public Level reverbLevelParam
        {
            get
            {
                return isSingMode ? this.reverbLevel.singLevel : this.reverbLevel.discoLevel;
            }
        }

        public override void ResetData()
        {
            this.musicLevel.Reset();
            this.micLevel.Reset();
            this.echoLevel.Reset();
            this.reverbLevel.Reset();
            this.highPassFilter.Reset();
            this.lowPassFilter.Reset();
            this.limiter.Reset();

            this._eqGroup.ResetData();
        }

#if COMM_TEST
        public override void RandomTest()
        {
            if (isManualMode)
            {
                this.musicLevelParam.RandomTest();
                this.micLevelParam.RandomTest();
                this.echoLevelParam.RandomTest();
                this.reverbLevelParam.RandomTest();
            }

            this.limiter.RandomTest();

            this.hpfFreqParam.RandomTest();
            this.hpfTypeParam.RandomTest();
            this.lpfFreqParam.RandomTest();
            this.lpfTypeParam.RandomTest();

            this.hpf.freqIndex = this.hpfFreqParam.v;
            this.hpf.typeIndex = this.hpfTypeParam.v;
            this.lpf.freqIndex = this.lpfFreqParam.v;
            this.lpf.typeIndex = this.lpfTypeParam.v;

            this.eqGroup.RandomTest();
            base.UpdateEQGroup();
            this.eqIsPassParam.RandomTest();
            this.eqGroup.isBypass = this.eqIsPassParam.value;
        }
#endif
    }
}
