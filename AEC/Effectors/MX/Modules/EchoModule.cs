using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.MXComm;
using AEC.Misc;

namespace AEC.Effectors.MX.Modules
{
    public class EchoModule : ModuleBase
    {
        public static readonly double[] initFreqList = new double[] { 80.3, 297.0, 1100.0, 4080.0, 15100.0 };
        public const double MaxHpfFreq = 1000;
        public const double MinLpfFreq = 5990;
        public const short MaxDelay = 250;
        public const short MaxPreDelay = 125;
        public const short MaxRepeat = 90;
        public const short MaxRChDelay = 100;
        public const short MaxRChPreDelay = 50;
        
        public const short DefDelay = 100;
        public const short DefRepeat = 60;
        public const short DefRChDelay = 50;

        public readonly Level echoLevel;
        public readonly Level directLevel;
        
        public readonly HighPassFilter highPassFilter;
        public readonly LowPassFilter lowPassFilter;
        private readonly EchoParam hpfFreqParam;
        private readonly EchoParam lpfFreqParam;

        public readonly EchoParam preDelay;
        public readonly EchoParam delayTime;
        public readonly EchoParam repeat;
        public readonly EchoParam rChDelayTime;
        public readonly EchoParam rChPreDelay;
        private readonly BoolParam eqBypassParam;
        public readonly EQGroup _eqGroup = null;

        public EchoModule(EventBus effectorEvent_) :
            base(MXCommProtocol.echoModuleIndex, effectorEvent_)
        {
            //_themeColor = Color.FromArgb(202, 143, 36);
            _themeColor = Color.FromArgb(255, 171, 0);

            this.echoLevel = new Level(
                this.moduleIndex,
                (byte)MXCommProtocol.EchoParamAddr.echoLevel,
                (byte)MXCommProtocol.EchoParamAddr.echoLevelPhase, 
                this.OnParamChanged);
            this.directLevel = new Level(
                this.moduleIndex,
                (byte)MXCommProtocol.EchoParamAddr.directLevel,
                (byte)MXCommProtocol.EchoParamAddr.directLevelPhase, 
                this.OnParamChanged);

            this.highPassFilter = XPFilterFactory.CreateImmutableTypeHighPassFilter(MaxHpfFreq, FilterType.Butterworth12dB);
            this.lowPassFilter = XPFilterFactory.CreateImmutableTypeLowPassFilter(MinLpfFreq, FilterType.Butterworth12dB);

            this.hpfFreqParam = new EchoParam((byte)MXCommProtocol.EchoParamAddr.echoHpfFreq, this.highPassFilter.freqMaxIndex, this.OnParamChanged);
            this.lpfFreqParam = new EchoParam((byte)MXCommProtocol.EchoParamAddr.echoLpfFreq, this.lowPassFilter.freqMaxIndex, this.OnParamChanged);
            
            this.preDelay = new EchoParam((byte)MXCommProtocol.EchoParamAddr.echoPreDelay, MaxPreDelay, this.OnParamChanged);
            this.delayTime = new EchoParam((byte)MXCommProtocol.EchoParamAddr.echoDelay, MaxDelay, this.OnParamChanged);
            this.repeat = new EchoParam((byte)MXCommProtocol.EchoParamAddr.echoRepeat, MaxRepeat, this.OnParamChanged);
            this.rChDelayTime = new EchoParam((byte)MXCommProtocol.EchoParamAddr.echoRChDelay, MaxRChDelay, this.OnParamChanged);
            this.rChPreDelay = new EchoParam((byte)MXCommProtocol.EchoParamAddr.echoRChPreDelay, MaxRChPreDelay, this.OnParamChanged);

            this.delayTime.initV = DefDelay;
            this.repeat.initV = DefRepeat;
            this.rChDelayTime.initV = DefRChDelay;

            this._eqGroup = new EQGroup(initFreqList);

            this.eqBypassParam = new BoolParam(this.moduleIndex, (byte)MXCommProtocol.EchoParamAddr.eqPass, this.OnParamChanged);

            this.highPassFilter.BindingParams(this.hpfFreqParam);
            this.lowPassFilter.BindingParams(this.lpfFreqParam);
            this._eqGroup.BindingBypassParam(eqBypassParam);
        }

        public void CopyFrom(EchoModule other)
        {
            this.echoLevel.CopyFrom(other.echoLevel);
            this.directLevel.CopyFrom(other.directLevel);

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

        public override string eqGroupLabel
        {
            get
            {
                return string.Format(localization[Localization.eqGroupPanel_XBandEchoEq], this._eqGroup.eqBandCount);
            }
        }

        public override void ResetData()
        {
            this.echoLevel.Reset();
            this.directLevel.Reset();
            this.highPassFilter.Reset();
            this.lowPassFilter.Reset();
            
            this.preDelay.initV = 0;
            this.delayTime.initV = DefDelay;
            this.repeat.initV = DefRepeat;
            this.rChDelayTime.initV = DefRChDelay;
            this.rChPreDelay.initV = 0;

            this._eqGroup.ResetData();
        }
    }
}
