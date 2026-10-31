using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.VKxDCMComm;

namespace AEC.Effectors.VKxDCM.Modules
{
    public class ReverbModule : ModuleBase
    {
        public static readonly double[] initFreqList = new double[] { 250, 500.0, 1000.0, 2000.0, 4000.0 };
        //public static readonly double[] initFreqList = new double[] { 500.0, 1000.0, 2000.0 };
        public const short MaxMixerLevel = 100;
        public const double MaxHpfFreq = 1000;
        public const double MinLpfFreq = 1000;
        public const short DefMicLPFTypeIndex = 2;
        public const short MaxPreDelay = 200;
        public const short MaxDelay = 80;
        public const short MaxDamp = 10;
        public const double DefToneControlBassCutoffFreq = 200;         //低音衰减频率
        public const double DefToneControlTrebleCutoffFreq = 3000;      //高音衰减频率

        public const short DefDampIndex = 6;

        public readonly SignalMixer reverbMixer;
        public readonly SignalMixer directMixer;

        private readonly ReverbParam micHpfFreqParam;
        private readonly ReverbParam micHpfTypeParam;
        private readonly ReverbParam micLpfFreqParam;
        private readonly ReverbParam micLpfTypeParam;

        public readonly HighPassFilter highPassFilter;
        public readonly LowPassFilter lowPassFilter;

        public readonly ReverbParam preDelay;
        public readonly ReverbParam reverbTime;

        private readonly BoolParam eqBypassParam;
        public EQGroup _eqGroup = null;

        public ReverbModule(EventBus commEvent_) :
            base(VKxDCMCommProtocol.reverbModuleIndex, commEvent_)
        {
            //_themeColor = Color.FromArgb(248, 187, 208);
            //_themeColor = Color.FromArgb(244, 179, 179);
            _themeColor = Color.FromArgb(228, 93, 80);
            activeItem = ActiveItem.HPF;

            this.reverbMixer = new SignalMixer(
                this.moduleIndex,
                (byte)VKxDCMCommProtocol.ReverbParamAddr.reverbMicLevel,
                (byte)VKxDCMCommProtocol.ReverbParamAddr.reverbMicLevelPhase,
                MaxMixerLevel,
                this.OnParamChanged);
            this.directMixer = new SignalMixer(
                this.moduleIndex,
                (byte)VKxDCMCommProtocol.ReverbParamAddr.directMicLevel,
                (byte)VKxDCMCommProtocol.ReverbParamAddr.directMicLevelPhase,
                MaxMixerLevel,
                this.OnParamChanged);

            this.highPassFilter = XPFilterFactory.CreateImmutableTypeHighPassFilter(MaxHpfFreq, FilterType.Butterworth18dB);
            this.lowPassFilter = XPFilterFactory.CreateImmutableTypeLowPassFilter(MinLpfFreq, FilterType.FirstOrder);
            
            this.micHpfFreqParam = new ReverbParam((byte)VKxDCMCommProtocol.ReverbParamAddr.reverbMicHPFFreq, this.highPassFilter.freqMaxIndex, this.OnParamChanged);
            this.micHpfTypeParam = new ReverbParam((byte)VKxDCMCommProtocol.ReverbParamAddr.reverbMicHPFType, this.highPassFilter.typeMaxIndex, this.OnParamChanged);
            this.micLpfFreqParam = new ReverbParam((byte)VKxDCMCommProtocol.ReverbParamAddr.reverbMicLPFFreq, this.lowPassFilter.freqMaxIndex, this.OnParamChanged);
            this.micLpfTypeParam = new ReverbParam((byte)VKxDCMCommProtocol.ReverbParamAddr.reverbMicLPFType, this.lowPassFilter.typeMaxIndex, this.OnParamChanged);

            this.highPassFilter.BindingParams(this.micHpfFreqParam, this.micHpfTypeParam);
            this.lowPassFilter.BindingParams(this.micLpfFreqParam, this.micLpfTypeParam);

            //this.highPassFilter.typeIndex = DefMicHPFTypeIndex;
            this.lowPassFilter.typeIndex = DefMicLPFTypeIndex;

            this.highPassFilter.SetGetTypeAsSlopeIndexLabel();
            this.lowPassFilter.SetGetTypeAsSlopeIndexLabel();

            this.reverbTime = new ReverbParam((byte)VKxDCMCommProtocol.ReverbParamAddr.reverbTime, MaxDelay, this.OnParamChanged);
            this.preDelay = new ReverbParam((byte)VKxDCMCommProtocol.ReverbParamAddr.reverbPreDelay, MaxPreDelay, this.OnParamChanged);

            this._eqGroup = new EQGroup(initFreqList);

            this.eqBypassParam = new BoolParam(this.moduleIndex, (byte)VKxDCMCommProtocol.ReverbParamAddr.eqPass, this.OnParamChanged);

            this.eqGroup.BindingBypassParam(eqBypassParam);
        }

        public void CopyFrom(ReverbModule other)
        {
            this.reverbMixer.CopyFrom(other.reverbMixer);
            this.directMixer.CopyFrom(other.directMixer);

            this.highPassFilter.CopyFrom(other.highPassFilter);
            this.lowPassFilter.CopyFrom(other.lowPassFilter);

            this.reverbTime.initV = other.reverbTime.v;
            this.preDelay.initV = other.preDelay.v;

            this._eqGroup.CopyFrom(other._eqGroup);
            //this.dataIsPrivateParam = other.dataIsPrivateParam;
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

        public override void UpdateXPFromControlPanel(XPFilterType xpFilterType, FilterElement filterElement)
        {
            this.UpdateXPFilter(xpFilterType, filterElement);
            this.activeItem = xpFilterType == XPFilterType.HighPass ? ActiveItem.HPF : ActiveItem.LPF;
        }

        public override void ResetData()
        {
            this.reverbMixer.Reset();
            this.directMixer.Reset();

            this.highPassFilter.Reset();
            this.lowPassFilter.Reset();

            this.reverbTime.initV = 0;
            this.preDelay.initV = 0;

            this._eqGroup.ResetData();
        }

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

        //public short hpfInit
        //{
        //    get
        //    {
        //        return this.highPassFilter.freqIndex;
        //    }
        //    set
        //    {
        //        this.highPassFilter.freqIndex = value;
        //    }
        //}

        //public short lpfInit
        //{
        //    get
        //    {
        //        return this.lowPassFilter.freqIndex;
        //    }
        //    set
        //    {
        //        this.lowPassFilter.freqIndex = (short)(minLPFFreqIndex + value);
        //    }
        //}

#if COMM_TEST
        public override void RandomTest()
        {
            this.reverbMixer.RandomTest();
            this.directMixer.RandomTest();

            this.preDelay.RandomTest();
            this.reverbTime.RandomTest();

            this.micHpfFreqParam.RandomTest();
            this.micLpfFreqParam.RandomTest();

            this.hpf.freqIndex = this.micHpfFreqParam.v;
            this.lpf.freqIndex = this.micLpfFreqParam.v;

            this.eqGroup.RandomTest();
            base.UpdateEQGroup();
            this.eqBypassParam.RandomTest();
            this.eqGroup.isBypass = this.eqBypassParam.value;
        }
#endif
    }
}
