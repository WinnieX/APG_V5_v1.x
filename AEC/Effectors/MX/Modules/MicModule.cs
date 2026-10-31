using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.MXComm;

namespace AEC.Effectors.MX.Modules
{
    public class MicModule : ModuleBase
    {
        public static readonly double[] initFreqList = new double[] { 80.3, 116.0, 170.0, 245.0, 360.0, 520.0, 764.0, 1100.0, 1590.0, 2330.0, 3360.0, 4940.0, 7130.0, 10300.0, 15100.0 };

        public const double HPFMaxFreq = 303;
        public const double HPFInitFreq = 50.6;

        public const short MaxFbe = 7;
        public const short DefFbe = 2;

        public const short MaxNoiseGate = 10;
        public const short DefNoiseGate = 0;

        public readonly HighPassFilter highPassFilter;
        private readonly MicParam hpfFreqParam;
        public readonly MicParam fbe;
        public readonly MicParam noiseGate;
        private readonly BoolParam eqIsPassParam;
        public readonly EQGroup _eqGroup;

        public MicModule(EventBus effectorEvent_) :
            base(MXCommProtocol.micModuleIndex, effectorEvent_)
        {
            //_themeColor = Color.FromArgb(168, 68, 0);
            base._themeColor = Color.FromArgb(230, 81, 0);

            this.highPassFilter = XPFilterFactory.CreateImmutableTypeHighPassFilter(HPFMaxFreq, HPFInitFreq, FilterType.Butterworth12dB);
            this.hpfFreqParam = new MicParam(
                (byte)MXCommProtocol.MicParamAddr.hpfFreq,
                this.highPassFilter.freqMaxIndex,
                this.OnParamChanged);
            this.fbe = new MicParam(
                (byte)MXCommProtocol.MicParamAddr.fbe, 
                MaxFbe, 
                this.OnParamChanged);
            this.noiseGate = new MicParam(
                (byte)MXCommProtocol.MicParamAddr.noiseGate,
                MaxNoiseGate,
                this.OnParamChanged);

            this._eqGroup = new EQGroup(initFreqList);

            this.fbe.initV = DefFbe;
            this.noiseGate.initV = DefNoiseGate;
            this.eqIsPassParam = new BoolParam(this.moduleIndex, (byte)MXCommProtocol.MicParamAddr.eqPass, this.OnParamChanged);

            this.highPassFilter.BindingParams(hpfFreqParam);
        }

        public void CopyFrom(MicModule other)
        {   
            this.highPassFilter.CopyFrom(other.highPassFilter);
            this.fbe.initV = other.fbe.v;
            this.noiseGate.initV = other.noiseGate.v;
            this._eqGroup.CopyFrom(other._eqGroup);
        }

        public override void InitFreqResp()
        {
            base.InitFreqResp();

            this._eqGroup.InitFreqResp();

            this.highPassFilter.UpdateFreqResp();

            this.UpdateFreqResp();
        }

        public override void UpdateFreqResp()
        {
            base.UpdateFreqResp();

            EQGroupAddFilterFreqResp(this.freqResp, eqGroup, hpf);
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

        public override void ResetData()
        {
            this.fbe.initV = DefFbe;
            this.noiseGate.initV = DefNoiseGate;
            this.highPassFilter.Reset();
            this._eqGroup.ResetData();
        }
    }
}
