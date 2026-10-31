using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.VXComm;

namespace AEC.Effectors.VX.Modules
{
    public class MicModule : ModuleBase
    {
        public static readonly double[] initFreqList = new double[] { 80.3, 116.0, 170.0, 245.0, 360.0, 520.0, 764.0, 1100.0, 1590.0, 2330.0, 3360.0, 4940.0, 7130.0, 10300.0, 15100.0 };
        
        public const double HPFMaxFreq = 303;
        public const double HPFInitFreq = 50.6;

        public const short MaxFbe = 7;
        public const short DefFbe = 2;
        
        public readonly Compressor compressor;

        public readonly HighPassFilter highPassFilter;
        private readonly MicParam hpfFreqParam;
        public readonly MicParam fbe;
        private readonly BoolParam eqBypassParam;
        public readonly EQGroup _eqGroup;

        public MicModule(EventBus effectorEvent_) :
            base(VXCommProtocol.micModuleIndex, effectorEvent_)
        {
            //_themeColor = Color.FromArgb(168, 68, 0);
            base._themeColor = Color.FromArgb(230, 81, 0);

            this.compressor = new Compressor(
                this.moduleIndex,
                (byte)VXCommProtocol.MicParamAddr.attack, 
                this.OnParamChanged);
            this.highPassFilter = XPFilterFactory.CreateImmutableTypeHighPassFilter(HPFMaxFreq, HPFInitFreq, FilterType.Butterworth12dB);
            this.hpfFreqParam = new MicParam(
                (byte)VXCommProtocol.MicParamAddr.hpfFreq,
                this.highPassFilter.freqMaxIndex,
                this.OnParamChanged);
            this.fbe = new MicParam(
                (byte)VXCommProtocol.MicParamAddr.fbe, 
                MaxFbe, 
                this.OnParamChanged);

            this._eqGroup = new EQGroup(initFreqList);

            this.fbe.initV = DefFbe;
            this.eqBypassParam = new BoolParam(this.moduleIndex, (byte)VXCommProtocol.MicParamAddr.eqPass, this.OnParamChanged);

            this.highPassFilter.BindingParams(hpfFreqParam);
            this.eqGroup.BindingBypassParam(eqBypassParam);
        }

        public void CopyFrom(MicModule other)
        {
            this.compressor.CopyFrom(other.compressor);            
            this.highPassFilter.CopyFrom(other.highPassFilter);
            this.fbe.initV = other.fbe.v;
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
            this.compressor.Reset();
            this.fbe.initV = DefFbe;
            this.highPassFilter.Reset();
            this._eqGroup.ResetData();
        }
    }
}
