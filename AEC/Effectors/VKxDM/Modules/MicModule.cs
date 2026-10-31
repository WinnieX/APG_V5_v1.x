using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.VKxDMComm;

namespace AEC.Effectors.VKxDM.Modules
{
    public class MicModule : ModuleBase
    {
        public static readonly double[] initFreqList = new double[] { 86.4, 125.0, 177.0, 250.0, 354.0, 500.0, 707.0, 1000.0, 1410.0, 2000.0, 2830.0, 4000.0, 5660.0, 8000.0, 11300.0 };
        
        public const double HPFMaxFreq = 303;
        public const double HPFInitFreq = 50.6;
        public const double LPFMinFreq = 1000;

        public const short MaxFbe = 3;
        public const short DefFbe = 0;

        public const short MaxLevel = 10;
        public const short DefLevel = MaxLevel;

        public const short MaxNoiseGate = 10;
        public const short DefNoiseGate = 2;

        public readonly ShortParam micNoiseGate;
        public readonly ShortParam fbe;
        public readonly ShortParam micLevel;
        public readonly ShortParam effectLevel;

        public readonly Compressor compressor;

        public readonly HighPassFilter highPassFilter;
        private readonly ShortParam hpfFreqParam;
        
        private readonly BoolParam eqBypassParam;
        public readonly EQGroup _eqGroup;

        public MicModule(byte moduleIndex_, EventBus effectorEvent_) :
            base(moduleIndex_, effectorEvent_)
        {
            //_themeColor = Color.FromArgb(168, 68, 0);
            base._themeColor = Color.FromArgb(230, 81, 0);

            this.micNoiseGate = new ShortParam(
                base.moduleIndex,
                (byte)VKxDMCommProtocol.MicParamAddr.noiseGate,
                (short)MaxNoiseGate,
                this.OnParamChanged);

            this.fbe = new ShortParam(
                base.moduleIndex,
                (byte)VKxDMCommProtocol.MicParamAddr.fbe,
                MaxFbe,
                this.OnParamChanged);

            this.micLevel = new ShortParam(
                base.moduleIndex,
                (byte)VKxDMCommProtocol.MicParamAddr.micLevel,
                MaxLevel,
                this.OnParamChanged
                );

            this.effectLevel = new ShortParam(
                base.moduleIndex,
                (byte)VKxDMCommProtocol.MicParamAddr.effectLevel,
                MaxLevel,
                this.OnParamChanged);

            this.compressor = new Compressor(
                this.moduleIndex,
                (byte)VKxDMCommProtocol.MicParamAddr.attack, 
                this.OnParamChanged);

            this.highPassFilter = XPFilterFactory.CreateImmutableTypeHighPassFilter(HPFMaxFreq, HPFInitFreq, FilterType.Butterworth12dB);

            this.hpfFreqParam = new ShortParam(
                base.moduleIndex,
                (byte)VKxDMCommProtocol.MicParamAddr.hpfFreq,
                this.highPassFilter.freqMaxIndex,
                this.OnParamChanged);

            
            this._eqGroup = new EQGroup(initFreqList);

            this.micNoiseGate.initV = DefNoiseGate;
            this.fbe.initV = DefFbe;
            this.micLevel.initV = DefLevel;
            this.effectLevel.initV = DefLevel;

            this.eqBypassParam = new BoolParam(this.moduleIndex, (byte)VKxDMCommProtocol.MicParamAddr.eqPass, this.OnParamChanged);

            this.highPassFilter.BindingParams(hpfFreqParam);

            this.eqGroup.BindingBypassParam(eqBypassParam);
        }

        public void CopyFrom(MicModule other)
        {
            this.micNoiseGate.initV = other.micNoiseGate.v;
            this.fbe.initV = other.fbe.v;
            this.micLevel.initV = other.micLevel.v;
            this.effectLevel.initV = other.effectLevel.v;
            this.compressor.CopyFrom(other.compressor);
            this.highPassFilter.CopyFrom(other.highPassFilter);
            
            this._eqGroup.CopyFrom(other._eqGroup);
        }

        public void CopyFrom(AEC.Effectors.X5.Modules.MicModule other)
        {
            this.micNoiseGate.initV = DefNoiseGate;
            this.fbe.initV = other.fbe.v;
            this.highPassFilter.CopyFrom(other.highPassFilter);
            this.micLevel.initV = DefLevel;
            this.effectLevel.initV = DefLevel;
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
            this.effectLevel.initV = DefLevel;
            this.micNoiseGate.v = DefNoiseGate;
            this.highPassFilter.Reset();
            this._eqGroup.ResetData();
        }

#if COMM_TEST
        public override void RandomTest()
        {
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

    public class MicAModule : MicModule
    {
        public MicAModule(EventBus effectorEvent_) :
            base(VKxDMCommProtocol.micAModuleIndex, effectorEvent_)
        {

        }
    }

    public class MicBModule : MicModule
    {
        public MicBModule(EventBus effectorEvent_) :
            base(VKxDMCommProtocol.micBModuleIndex, effectorEvent_)
        {

        }
    }
}
