using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Common;
using AEC.Comm.VKxDCMComm;

namespace AEC.Effectors.VKxDCM.Modules
{
    public abstract class OutputBaseModule : ModuleBase
    {
        //public static readonly double[] initFreqList = new double[] {125.0, 177.0, 250.0, 354.0, 500.0, 707.0, 1000.0, 1410.0, 2000.0, 2830.0, 4000.0, 5660.0, 8000.0};
        public static readonly double[] initFreqList = new double[] { 62.5, 125.0, 250.0, 500.0, 1000.0, 2000.0, 4000.0 };
        public const short MaxOutputLevelIndex = 500;
        public const double MaxDelayInms = 80;
        public const double DelayStep = 1000 / AEC.Core.DSP.DSPHelper.sr_48k;
        public static readonly short MaxDelayIndex = (short)Math.Round(MaxDelayInms / DelayStep, 1, MidpointRounding.AwayFromZero);
        
        public const string DelayUnit = "ms";
        public const short maxCompressorThreshold = 36;
        public const short compressorThresholdBase = -22;

        public readonly OutputMixer musicMixer;
        public readonly OutputMixer micMixer;
        public readonly OutputMixer echoMixer;
        public readonly OutputMixer reverbMixer;
        
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

        public static readonly ChannelDelayParamLabelProvider channelDelayParamLabelProvider = new ChannelDelayParamLabelProvider(1000.0 / AEC.Core.DSP.DSPHelper.sr_48k, MaxDelayInms);

        public abstract bool outputIsMute
        {
            get;
            set;
        }

        public OutputBaseModule(OutputChannelType outputChannelType, byte index, EventBus effectorEvent_, AutoWorkMode autoWorkMode_) :
            base(index:index, effectorEvent_:effectorEvent_)
        {
            this.outputChannelType = outputChannelType;

            this.musicMixer = new OutputMixer(
                this.moduleIndex,
                (byte)VKxDCMCommProtocol.OutputBaseParamAddr.musicSingLevel,
                (byte)VKxDCMCommProtocol.OutputBaseParamAddr.musicSingLevelPhase,
                MaxOutputLevelIndex,
                this.OnParamChanged);
            this.micMixer = new OutputMixer(
                this.moduleIndex,
                (byte)VKxDCMCommProtocol.OutputBaseParamAddr.micSingLevel,
                (byte)VKxDCMCommProtocol.OutputBaseParamAddr.micSingLevelPhase,
                MaxOutputLevelIndex, 
                this.OnParamChanged);
            this.echoMixer = new OutputMixer(
                this.moduleIndex,
                (byte)VKxDCMCommProtocol.OutputBaseParamAddr.echoSingLevel,
                (byte)VKxDCMCommProtocol.OutputBaseParamAddr.echoSingLevelPhase,
                MaxOutputLevelIndex, 
                this.OnParamChanged);
            this.reverbMixer = new OutputMixer(
                this.moduleIndex,
                (byte)VKxDCMCommProtocol.OutputBaseParamAddr.reverbSingLevel,
                (byte)VKxDCMCommProtocol.OutputBaseParamAddr.reverbSingLevelPhase,
                MaxOutputLevelIndex, 
                this.OnParamChanged);

            this.highPassFilter = XPFilterFactory.Create4thOrderHighPassFilter(DataUtils.maxValidFilterFreq); //XPFilterFactory.CreateProfessinalXoverHighPassFilter();
            this.lowPassFilter = XPFilterFactory.Create4thOrderLowPassFilter(DataUtils.minValidFilterFreq);//XPFilterFactory.CreateProfessinalXoverLowPassFilter();

            this.limiter = new Limiter(this.moduleIndex, (byte)VKxDCMCommProtocol.OutputBaseParamAddr.attack, this.OnParamChanged);

            this._eqGroup = new EQGroup(initFreqList) ;

            this.hpfFreqParam = new ShortParam(this.moduleIndex, (byte)VKxDCMCommProtocol.OutputBaseParamAddr.hpfFreq, this.highPassFilter.freqMaxIndex, this.OnParamChanged);
            this.hpfTypeParam = new ShortParam(this.moduleIndex, (byte)VKxDCMCommProtocol.OutputBaseParamAddr.hpfType, this.highPassFilter.typeMaxIndex, this.OnParamChanged);

            this.lpfFreqParam = new ShortParam(this.moduleIndex, (byte)VKxDCMCommProtocol.OutputBaseParamAddr.lpfFreq, this.lowPassFilter.freqMaxIndex, this.OnParamChanged);
            this.lpfTypeParam = new ShortParam(this.moduleIndex, (byte)VKxDCMCommProtocol.OutputBaseParamAddr.lpfType, this.lowPassFilter.typeMaxIndex, this.OnParamChanged);

            this.eqIsPassParam = new BoolParam(this.moduleIndex, (byte)VKxDCMCommProtocol.OutputBaseParamAddr.eqPass, this.OnParamChanged);

            this.highPassFilter.BindingParams(this.hpfFreqParam, this.hpfTypeParam, null);
            this.lowPassFilter.BindingParams(this.lpfFreqParam, this.lpfTypeParam, null);
            this._eqGroup.BindingBypassParam(this.eqIsPassParam);

            this.autoWorkMode = autoWorkMode_;
        }

        public void CopyFrom(OutputBaseModule other)
        {
            this.musicMixer.CopyFrom(other.musicMixer);
            this.micMixer.CopyFrom(other.micMixer);
            this.echoMixer.CopyFrom(other.echoMixer);
            this.reverbMixer.CopyFrom(other.reverbMixer);
            this.limiter.CopyFrom(other.limiter);
            this.highPassFilter.CopyFrom(other.highPassFilter);
            this.lowPassFilter.CopyFrom(other.lowPassFilter);
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

        public SignalMixer musicSignalMixer
        {
            get
            {
                return isSingMode ? this.musicMixer.singMixer : this.musicMixer.discoMixer;
            }
        }

        public SignalMixer micSignalMixer
        {
            get
            {
                return isSingMode ? this.micMixer.singMixer : this.micMixer.discoMixer;
            }
        }

        public SignalMixer echoSignalMixer
        {
            get
            {
                return isSingMode ? this.echoMixer.singMixer : this.echoMixer.discoMixer;
            }
        }

        public SignalMixer reverbSignalMixer
        {
            get
            {
                return isSingMode ? this.reverbMixer.singMixer : this.reverbMixer.discoMixer;
            }
        }

        public override void ResetData()
        {
            this.musicMixer.Reset();
            this.micMixer.Reset();
            this.echoMixer.Reset();
            this.reverbMixer.Reset();
            this.limiter.Reset();
            this.highPassFilter.Reset();
            this.lowPassFilter.Reset();

            this._eqGroup.ResetData();
        }

#if COMM_TEST
        public override void RandomTest()
        {
            if (isManualMode)
            {
                this.musicSignalMixer.RandomTest();
                this.micSignalMixer.RandomTest();
                this.echoSignalMixer.RandomTest();
                this.reverbSignalMixer.RandomTest();
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
