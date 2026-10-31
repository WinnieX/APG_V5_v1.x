using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.VKxSComm;

namespace AEC.Effectors.VKxS.Modules
{
    public class MicModule : ModuleBase
    {
        public static readonly double[] initFreqList = new double[] { 86.4, 125.0, 177.0, 250.0, 354.0, 500.0, 707.0, 1000.0, 1410.0, 2000.0, 2830.0, 4000.0, 5660.0, 8000.0, 11300.0 };
        
        public const double HPFMaxFreq = 303;
        public const double HPFInitFreq = 50.6;
        public const double LPFMinFreq = 1000;

        public const short MaxFbe = 3;
        public const short DefFbe = 0;

        public const short MaxNoiseGate = 10;
        public const short DefNoiseGate = 2;

        public readonly MicParam micNoiseGate;
        public readonly MicParam fbe;

        public readonly Compressor compressor;

        public readonly HighPassFilter highPassFilter;
        private readonly MicParam hpfFreqParam;
        
        private readonly BoolParam eqBypassParam;
        public readonly EQGroup _eqGroup;

        public MicModule(EventBus effectorEvent_) :
            base(VKxSCommProtocol.micModuleIndex, effectorEvent_)
        {
            //_themeColor = Color.FromArgb(168, 68, 0);
            base._themeColor = Color.FromArgb(230, 81, 0);

            this.micNoiseGate = new MicParam(
                (byte)VKxSCommProtocol.MicParamAddr.noiseGate,
                (short)MaxNoiseGate,
                this.OnParamChanged);

            this.fbe = new MicParam(
                (byte)VKxSCommProtocol.MicParamAddr.fbe,
                MaxFbe,
                this.OnParamChanged);

            this.compressor = new Compressor(
                this.moduleIndex,
                (byte)VKxSCommProtocol.MicParamAddr.attack, 
                this.OnParamChanged);

            this.highPassFilter = XPFilterFactory.CreateImmutableTypeHighPassFilter(HPFMaxFreq, HPFInitFreq, FilterType.Butterworth12dB);
            
            this.hpfFreqParam = new MicParam(
                (byte)VKxSCommProtocol.MicParamAddr.hpfFreq,
                this.highPassFilter.freqMaxIndex,
                this.OnParamChanged);

            
            this._eqGroup = new EQGroup(initFreqList);

            this.micNoiseGate.initV = DefNoiseGate;
            this.fbe.initV = DefFbe;
           
            this.eqBypassParam = new BoolParam(this.moduleIndex, (byte)VKxSCommProtocol.MicParamAddr.eqPass, this.OnParamChanged);

            this.highPassFilter.BindingParams(hpfFreqParam);

            this.eqGroup.BindingBypassParam(eqBypassParam);
        }

        public void CopyFrom(MicModule other)
        {
            this.micNoiseGate.initV = other.micNoiseGate.v;
            this.fbe.initV = other.fbe.v;
            this.compressor.CopyFrom(other.compressor);
            this.highPassFilter.CopyFrom(other.highPassFilter);
            
            this._eqGroup.CopyFrom(other._eqGroup);
        }

        public void CopyFrom(AEC.Effectors.X5.Modules.MicModule other)
        {
            this.micNoiseGate.initV = DefNoiseGate;
            this.fbe.initV = other.fbe.v;
            this.highPassFilter.CopyFrom(other.highPassFilter);

            this.compressor.CopyFrom(other.compressor);
            
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

            EQGroupAddFilterFreqResp(this.freqResp, eqGroup, highPassFilter);
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
            this.micNoiseGate.v = DefNoiseGate;
            this.highPassFilter.Reset();
            this._eqGroup.ResetData();
        }

#if COMM_TEST
        public override void RandomTest()
        {
            //this.micInput.RandomTest();
            //this.micNoiseGate.RandomTest();
            //this.fbe.RandomTest();
            //this.compressor.RandomTest();

            //this.hpfFreqParam.RandomTest();
            //this.lpfFreqParam.RandomTest();
            //this.lpfTypeParam.RandomTest();

            //this.hpf.freqIndex = this.hpfFreqParam.v;
            //this.lpf.freqIndex = this.lpfFreqParam.v;
            //this.lpf.typeIndex = this.lpfTypeParam.v;

            //this.eqGroup.RandomTest();
            //base.UpdateEQGroup();
            //this.eqBypassParam.RandomTest();
            //this.eqGroup.isBypass = this.eqBypassParam.value;
        }
#endif
    }
}
