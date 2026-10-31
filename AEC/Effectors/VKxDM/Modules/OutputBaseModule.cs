using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Common;
using AEC.Comm.VKxDMComm;

namespace AEC.Effectors.VKxDM.Modules
{
    public abstract class OutputBaseModule : ModuleBase
    {
        public static readonly double[] initFreqList = new double[] {125.0, 177.0, 250.0, 354.0, 500.0, 707.0, 1000.0, 1410.0, 2000.0, 2830.0, 4000.0, 5660.0, 8000.0};

        public const short MaxDefOutputLevelIndex = 100;
        public const short MaxSubOutputLevelIndex = 200;

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

        public readonly HighPassFilter singHPF;
        public readonly HighPassFilter discoHPF;
        public readonly LowPassFilter singLPF;
        public readonly LowPassFilter discoLPF;

        private readonly ShortParam singHpfTypeParam;
        private readonly ShortParam singHpfFreqParam;
        private readonly ShortParam singHpfQParam;
        private readonly ShortParam discoHpfTypeParam;
        private readonly ShortParam discoHpfFreqParam;
        private readonly ShortParam discoHpfQParam;

        private readonly ShortParam singLpfTypeParam;
        private readonly ShortParam singLpfFreqParam;
        private readonly ShortParam singLpfQParam;
        private readonly ShortParam discoLpfTypeParam;
        private readonly ShortParam discoLpfFreqParam;
        private readonly ShortParam discoLpfQParam;

        public readonly Limiter limiter;

        protected readonly BoolParam singEQBypassParam;
        protected readonly BoolParam discoEQBypassParam;

        public readonly EQGroup singEQGroup = null;
        public readonly EQGroup discoEQGroup = null;

        public readonly AutoWorkMode autoWorkMode;

        public readonly OutputChannelType outputChannelType;

        //public static readonly ChannelDelayParamLabelProvider channelDelayParamLabelProvider = new ChannelDelayParamLabelProvider(1000.0 / AEC.Core.DSP.DSPHelper.sr_48k, MaxDelayInms);

        public abstract bool outputIsMute
        {
            get;
            set;
        }

        public OutputBaseModule(OutputChannelType outputChannelType, byte index, EventBus effectorEvent_, AutoWorkMode autoWorkMode_) :
            base(index:index, effectorEvent_:effectorEvent_)
        {
            this.outputChannelType = outputChannelType;

            short maxOutputLevel = outputChannelType != OutputChannelType.Aux2Output ? MaxDefOutputLevelIndex : MaxSubOutputLevelIndex;

            this.autoWorkMode = autoWorkMode_;

            this.musicMixer = new OutputMixer(
                this.moduleIndex,
                (byte)VKxDMCommProtocol.OutputBaseParamAddr.musicMixerLevel,
                (byte)VKxDMCommProtocol.OutputBaseParamAddr.musicMixerPhase,
                maxOutputLevel,
                this.OnParamChanged);
            this.micMixer = new OutputMixer(
                this.moduleIndex,
                (byte)VKxDMCommProtocol.OutputBaseParamAddr.micMixerLevel,
                (byte)VKxDMCommProtocol.OutputBaseParamAddr.micMixerPhase,
                maxOutputLevel,
                this.OnParamChanged);
            this.echoMixer = new OutputMixer(
                this.moduleIndex,
                (byte)VKxDMCommProtocol.OutputBaseParamAddr.echoMixerLevel,
                (byte)VKxDMCommProtocol.OutputBaseParamAddr.echoMixerPhase,
                maxOutputLevel,
                this.OnParamChanged);
            this.reverbMixer = new OutputMixer(
                this.moduleIndex,
                (byte)VKxDMCommProtocol.OutputBaseParamAddr.reverbMixerLevel,
                (byte)VKxDMCommProtocol.OutputBaseParamAddr.reverbMixerPhase,
                maxOutputLevel,
                this.OnParamChanged);

            this.limiter = new Limiter(this.moduleIndex, (byte)VKxDMCommProtocol.OutputBaseParamAddr.attack, this.OnParamChanged);

            this.singHPF = XPFilterFactory.CreateVKxSOutputHighPassFilter();
            this.singHpfTypeParam = new ShortParam(this.moduleIndex, (byte)VKxDMCommProtocol.OutputBaseParamAddr.hpfType, this.singHPF.typeMaxIndex, this.OnParamChanged);
            this.singHpfFreqParam = new ShortParam(this.moduleIndex, (byte)VKxDMCommProtocol.OutputBaseParamAddr.hpfFreq, this.singHPF.freqMaxIndex, this.OnParamChanged);
            this.singHpfQParam = new ShortParam(this.moduleIndex, (byte)VKxDMCommProtocol.OutputBaseParamAddr.hpfQ, this.singHPF.qMaxIndex, this.OnParamChanged); 
            this.singHPF.BindingParams(this.singHpfFreqParam, this.singHpfTypeParam, this.singHpfQParam);

            this.discoHPF = XPFilterFactory.CreateVKxSOutputHighPassFilter();
            this.discoHpfTypeParam = new ShortParam(this.moduleIndex, (byte)VKxDMCommProtocol.OutputBaseParamAddr.hpfType, this.singHPF.typeMaxIndex, this.OnParamChanged);
            this.discoHpfFreqParam = new ShortParam(this.moduleIndex, (byte)VKxDMCommProtocol.OutputBaseParamAddr.hpfFreq, this.singHPF.freqMaxIndex, this.OnParamChanged);
            this.discoHpfQParam = new ShortParam(this.moduleIndex, (byte)VKxDMCommProtocol.OutputBaseParamAddr.hpfQ, this.singHPF.qMaxIndex, this.OnParamChanged);
            this.discoHPF.BindingParams(this.discoHpfFreqParam, this.discoHpfTypeParam, this.discoHpfQParam);

            this.singLPF = XPFilterFactory.CreateVKxSOutputLowPassFilter();
            this.singLpfTypeParam = new ShortParam(this.moduleIndex, (byte)VKxDMCommProtocol.OutputBaseParamAddr.lpfType, this.singLPF.typeMaxIndex, this.OnParamChanged);
            this.singLpfFreqParam = new ShortParam(this.moduleIndex, (byte)VKxDMCommProtocol.OutputBaseParamAddr.lpfFreq, this.singLPF.freqMaxIndex, this.OnParamChanged);
            this.singLpfQParam = new ShortParam(this.moduleIndex, (byte)VKxDMCommProtocol.OutputBaseParamAddr.lpfQ, this.singLPF.qMaxIndex, this.OnParamChanged);
            this.singLPF.BindingParams(this.singLpfFreqParam, this.singLpfTypeParam, this.singLpfQParam);

            this.discoLPF = XPFilterFactory.CreateVKxSOutputLowPassFilter();
            this.discoLpfTypeParam = new ShortParam(this.moduleIndex, (byte)VKxDMCommProtocol.OutputBaseParamAddr.lpfType, this.singLPF.typeMaxIndex, this.OnParamChanged);
            this.discoLpfFreqParam = new ShortParam(this.moduleIndex, (byte)VKxDMCommProtocol.OutputBaseParamAddr.lpfFreq, this.singLPF.freqMaxIndex, this.OnParamChanged);
            this.discoLpfQParam = new ShortParam(this.moduleIndex, (byte)VKxDMCommProtocol.OutputBaseParamAddr.lpfQ, this.singLPF.qMaxIndex, this.OnParamChanged);
            this.discoLPF.BindingParams(this.discoLpfFreqParam, this.discoLpfTypeParam, this.discoLpfQParam);

            this.singEQGroup = new EQGroup(initFreqList);
            this.singEQBypassParam = new BoolParam(this.moduleIndex, (byte)VKxDMCommProtocol.OutputBaseParamAddr.eqPass, this.OnParamChanged);
            this.singEQGroup.BindingBypassParam(this.singEQBypassParam);

            this.discoEQGroup = new EQGroup(initFreqList);
            this.discoEQBypassParam = new BoolParam(this.moduleIndex, (byte)VKxDMCommProtocol.OutputBaseParamAddr.eqPass, this.OnParamChanged);
            this.discoEQGroup.BindingBypassParam(this.discoEQBypassParam);
        }

        public void CopyFrom(OutputBaseModule other)
        {
            this.musicMixer.CopyFrom(other.musicMixer);
            this.micMixer.CopyFrom(other.micMixer);
            this.echoMixer.CopyFrom(other.echoMixer);
            this.reverbMixer.CopyFrom(other.reverbMixer);
            this.singHPF.CopyFrom(other.singHPF);
            this.discoHPF.CopyFrom(other.discoHPF);
            this.singLPF.CopyFrom(other.singLPF);
            this.discoLPF.CopyFrom(other.discoLPF);
            this.limiter.CopyFrom(other.limiter);
            this.singEQGroup.CopyFrom(other.singEQGroup);
            this.discoEQGroup.CopyFrom(other.discoEQGroup);
            this.dataIsPrivateParam = other.dataIsPrivateParam;
        }

        public override void InitFreqResp()
        {
            base.InitFreqResp();

            this.singEQGroup.InitFreqResp();
            this.discoEQGroup.InitFreqResp();
            this.singHPF.UpdateFreqResp();
            this.discoHPF.UpdateFreqResp();
            this.singLPF.UpdateFreqResp();
            this.discoLPF.UpdateFreqResp();

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
                    this.limiter.HpfFreqChangedWhenAutoMode(this.singHPF.freq);
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
                return isSingMode ? this.singEQGroup : this.discoEQGroup;
            }
        }

        public override HighPassFilter hpf
        {
            get
            {
                return isSingMode ? this.singHPF : this.discoHPF;
            }
        }

        public override LowPassFilter lpf
        {
            get
            {
                return isSingMode ? this.singLPF : this.discoLPF;
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
            this.singHPF.Reset();
            this.discoHPF.Reset();
            this.singLPF.Reset();
            this.discoLPF.Reset();
            this.limiter.Reset();
            this.singEQGroup.ResetData();
            this.discoEQGroup.ResetData();
        }

        public override bool eqCanBeChanged
        {
            get
            {
                return !this.eqGroup.isBypass && this.isManualMode;
            }
        }

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
