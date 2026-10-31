using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Misc;
using AEC.Comm.VKxDCMComm;

namespace AEC.Effectors.VKxDCM.Modules
{
    public class EchoModule : ModuleBase
    {
        public static readonly double[] initFreqList = new double[] { 250, 500.0, 1000.0, 2000.0, 4000.0 };
        public const short MaxMixerLevel = 100;
        public const double MaxHpfFreq = 1000;
        public const double MinLpfFreq = 1000;
        public const short MaxPreDelay = 250;
        public const short MaxDelay = 250;
        public const short MaxRepeat = 90;
        public const short MaxRChDelay = 100;
        public const short MaxRChPreDelay = 50;
        
        public const short DefDelay = 100;
        public const short DefRepeat = 60;
        public const short DefRChDelay = 50;
        public const short DefHPFTypeIndex = 2;
        public const short DefLPFTypeIndex = 8;

        public readonly SignalMixer echoMixer;
        public readonly SignalMixer directMixer;

        private readonly EchoParam hpfFreqParam;
        private readonly EchoParam hpfTypeParam;
        private readonly EchoParam lpfFreqParam;
        private readonly EchoParam lpfTypeParam;

        public readonly HighPassFilter highPassFilter;
        public readonly LowPassFilter lowPassFilter;
        
        public readonly EchoParam preDelay;
        public readonly EchoParam delayTime;
        public readonly EchoParam repeat;
        public readonly EchoParam rChPreDelay;
        public readonly EchoParam rChDelayTime;
        private readonly BoolParam eqBypassParam;
        public EQGroup _eqGroup = null;

        public EchoModule(EventBus effectorEvent_, ModuleBase secondaryModule_ = null) :
            base(VKxDCMCommProtocol.echoModuleIndex, effectorEvent_)
        {
            //_themeColor = Color.FromArgb(202, 143, 36);
            _themeColor = Color.FromArgb(255, 171, 0);

            base.secondaryModule = secondaryModule_;

            this.echoMixer = new SignalMixer(
                this.moduleIndex,
                (byte)VKxDCMCommProtocol.EchoParamAddr.echoLevel,
                (byte)VKxDCMCommProtocol.EchoParamAddr.echoLevelPhase,
                MaxMixerLevel,
                this.OnParamChanged);
            this.directMixer = new SignalMixer(
                this.moduleIndex,
                (byte)VKxDCMCommProtocol.EchoParamAddr.directLevel,
                (byte)VKxDCMCommProtocol.EchoParamAddr.directLevelPhase,
                MaxMixerLevel,
                this.OnParamChanged);

            this.highPassFilter = XPFilterFactory.CreateImmutableTypeHighPassFilter(MaxHpfFreq);
            this.lowPassFilter = XPFilterFactory.CreateImmutableTypeLowPassFilter(MinLpfFreq, FilterType.Butterworth12dB);

            this.hpfFreqParam = new EchoParam((byte)VKxDCMCommProtocol.EchoParamAddr.echoHpfFreq, this.highPassFilter.freqMaxIndex, this.OnParamChanged);
            this.hpfTypeParam = new EchoParam((byte)VKxDCMCommProtocol.EchoParamAddr.echoHpfType, this.highPassFilter.typeMaxIndex, this.OnParamChanged);
            this.lpfFreqParam = new EchoParam((byte)VKxDCMCommProtocol.EchoParamAddr.echoLpfFreq, this.lowPassFilter.freqMaxIndex, this.OnParamChanged);
            this.lpfTypeParam = new EchoParam((byte)VKxDCMCommProtocol.EchoParamAddr.echoLpfType, this.lowPassFilter.typeMaxIndex, this.OnParamChanged);

            this.preDelay = new EchoParam((byte)VKxDCMCommProtocol.EchoParamAddr.echoPreDelay, MaxPreDelay, this.OnParamChanged);
            this.delayTime = new EchoParam((byte)VKxDCMCommProtocol.EchoParamAddr.echoDelay, MaxDelay, this.OnParamChanged);
            this.repeat = new EchoParam((byte)VKxDCMCommProtocol.EchoParamAddr.echoRepeat, MaxRepeat, this.OnParamChanged);
            this.rChDelayTime = new EchoParam((byte)VKxDCMCommProtocol.EchoParamAddr.echoRChDelay, MaxRChDelay, this.OnParamChanged);
            this.rChPreDelay = new EchoParam((byte)VKxDCMCommProtocol.EchoParamAddr.echoRChPreDelay, MaxRChPreDelay, this.OnParamChanged);

            this.delayTime.initV = DefDelay;
            this.repeat.initV = DefRepeat;
            this.rChDelayTime.initV = DefRChDelay;

            this._eqGroup = new EQGroup(initFreqList);

            this.eqBypassParam = new BoolParam(this.moduleIndex, (byte)VKxDCMCommProtocol.EchoParamAddr.eqPass, this.OnParamChanged);

            this.highPassFilter.BindingParams(this.hpfFreqParam, this.hpfTypeParam);
            this.lowPassFilter.BindingParams(this.lpfFreqParam, this.lpfTypeParam);

            this.highPassFilter.typeIndex = DefHPFTypeIndex;
            this.lowPassFilter.typeIndex = DefLPFTypeIndex;

            this.highPassFilter.SetGetTypeAsSlopeIndexLabel();
            this.lowPassFilter.SetGetTypeAsSlopeIndexLabel();

            this.eqGroup.BindingBypassParam(eqBypassParam);
        }

        public void CopyFrom(EchoModule other)
        {
            this.echoMixer.CopyFrom(other.echoMixer);
            this.directMixer.CopyFrom(other.directMixer);

            this.highPassFilter.CopyFrom(other.highPassFilter);
            this.lowPassFilter.CopyFrom(other.lowPassFilter);

            this.preDelay.initV = other.preDelay.v;
            this.delayTime.initV = other.delayTime.v;
            this.repeat.initV = other.repeat.v;
            this.rChDelayTime.initV = other.rChDelayTime.v;
            this.rChPreDelay.initV = other.rChPreDelay.v;

            this._eqGroup.CopyFrom(other._eqGroup);
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

        //public override void InitEQNormalGraphic()
        //{
        //    base.InitEQNormalGraphic();
            
        //    if(this.secondaryModule != null)
        //    {
        //        //this.secondaryModule.InitSecondaryNormalGraphic();
        //        this.secondaryModule.InitEQNormalGraphic();
        //    }
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

        //public override string eqGroupLabel
        //{
        //    get
        //    {
        //        return string.Format(localization[Localization.eqGroupPanel_XBandEchoEq], this._eqGroup.eqBandCount);
        //    }
        //}

        public override void ResetData()
        {
            this.echoMixer.Reset();
            this.directMixer.Reset();
            this.highPassFilter.Reset();
            this.lowPassFilter.Reset();

            this.preDelay.initV = 0;
            this.delayTime.initV = DefDelay;
            this.repeat.initV = DefRepeat;
            this.rChDelayTime.initV = DefRChDelay;
            this.rChPreDelay.initV = 0;

            this._eqGroup.ResetData();
        }

#if COMM_TEST
        public override void RandomTest()
        {
            this.echoMixer.RandomTest();
            this.directMixer.RandomTest();

            this.preDelay.RandomTest();
            this.delayTime.RandomTest();
            this.repeat.RandomTest();
            this.rChDelayTime.RandomTest();
            this.rChPreDelay.RandomTest();

            this.hpfFreqParam.RandomTest();
            this.lpfFreqParam.RandomTest();

            this.hpf.freqIndex = this.hpfFreqParam.v;
            this.lpf.freqIndex = this.lpfFreqParam.v;

            this.eqGroup.RandomTest();
            base.UpdateEQGroup();
            this.eqBypassParam.RandomTest();
            this.eqGroup.isBypass = this.eqBypassParam.value;
        }
#endif
    }
}
