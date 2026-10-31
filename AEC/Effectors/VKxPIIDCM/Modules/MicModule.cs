using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.VKxPIIDCMComm;

namespace AEC.Effectors.VKxPIIDCM.Modules
{
    public class MicModule : ModuleBase
    {
        public static readonly double[] initFreqList = new double[] { 86.4, 125.0, 177.0, 250.0, 354.0, 500.0, 707.0, 1000.0, 1410.0, 2000.0, 2830.0, 4000.0, 5660.0, 8000.0, 11300.0 };
        
        public const double HPFMaxFreq = 303;
        public const double HPFInitFreq = 50.6;
        public const double LPFMinFreq = 1000;

        public const short MaxLevel = 10;
        public const short DefLevel = MaxLevel;

        public const short MaxFbe = 3;
        public const short DefFbe = 2;

        public const short MaxNoiseGate = 10;
        public const short DefNoiseGate = 4;

        public readonly ShortParam micLevel;

        public readonly ShortParam micNoiseGate;
        public readonly ShortParam fbe;

        public readonly Compressor compressor;

        public readonly ShortParam exciter1Select;        //激励器1功能选择，选择0为低频激励器
        public readonly BassExciter bassExciter;        //选择0为低音激励器
        public readonly Exciter exciter1;               //选择1频段激励器1
        public readonly Exciter exciter2;               //频段激励器2

        public readonly HighPassFilter highPassFilter;
        public readonly LowPassFilter lowPassFilter;
        private readonly ShortParam hpfFreqParam;
        private readonly ShortParam lpfFreqParam;
        private readonly ShortParam lpfTypeParam;
        
        private readonly BoolParam eqBypassParam;
        public readonly EQGroup _eqGroup;

        public MicModule(byte moduleIndex_, EventBus effectorEvent_) :
            base(moduleIndex_, effectorEvent_)
        {
            //_themeColor = Color.FromArgb(168, 68, 0);
            base._themeColor = Color.FromArgb(230, 81, 0);

            this.micLevel = new ShortParam(
                base.moduleIndex,
                (byte)VKxPIIDCMCommProtocol.MicParamAddr.micLevel,
                MaxLevel,
                this.OnParamChanged
                );

            this.micNoiseGate = new ShortParam(
                base.moduleIndex,
                (byte)VKxPIIDCMCommProtocol.MicParamAddr.noiseGate,
                (short)MaxNoiseGate,
                this.OnParamChanged);

            this.fbe = new ShortParam(
                base.moduleIndex,
                (byte)VKxPIIDCMCommProtocol.MicParamAddr.fbe,
                MaxFbe,
                this.OnParamChanged);

            this.compressor = new Compressor(
                this.moduleIndex,
                (byte)VKxPIIDCMCommProtocol.MicParamAddr.attack, 
                this.OnParamChanged);

            this.exciter1Select = new ShortParam(base.moduleIndex, (byte)VKxPIIDCMCommProtocol.MicParamAddr.exciter1Select, 1, this.OnParamChanged);
            this.bassExciter = new BassExciter(base.moduleIndex, (byte)VKxPIIDCMCommProtocol.MicParamAddr.bassExciterOn, this.OnParamChanged);
            this.exciter1 = new Exciter(base.moduleIndex, (byte)VKxPIIDCMCommProtocol.MicParamAddr.exciter1On, this.OnParamChanged);
            this.exciter2 = new Exciter(base.moduleIndex, (byte)VKxPIIDCMCommProtocol.MicParamAddr.exciter2On, this.OnParamChanged);

            this.highPassFilter = XPFilterFactory.CreateImmutableTypeHighPassFilter(HPFMaxFreq, HPFInitFreq, FilterType.Butterworth18dB);
            this.lowPassFilter = XPFilterFactory.Create2ndOrderLowPassFilter(LPFMinFreq, DataUtils.maxFilterFreq);

            this.hpfFreqParam = new ShortParam(
                base.moduleIndex,
                (byte)VKxPIIDCMCommProtocol.MicParamAddr.hpfFreq,
                this.highPassFilter.freqMaxIndex,
                this.OnParamChanged);

            this.lpfFreqParam = new ShortParam(
                base.moduleIndex,
                (byte)VKxPIIDCMCommProtocol.MicParamAddr.lpfFreq,
                this.lowPassFilter.freqMaxIndex,
                this.OnParamChanged);

            this.lpfTypeParam = new ShortParam(
                base.moduleIndex,
                (byte)VKxPIIDCMCommProtocol.MicParamAddr.lpfType,
                this.lowPassFilter.typeMaxIndex,
                this.OnParamChanged);
            
            this._eqGroup = new EQGroup(initFreqList);

            this.micLevel.initV = DefLevel;
            this.micNoiseGate.initV = DefNoiseGate;
            this.fbe.initV = DefFbe;

            this.eqBypassParam = new BoolParam(this.moduleIndex, (byte)VKxPIIDCMCommProtocol.MicParamAddr.eqPass, this.OnParamChanged);

            this.highPassFilter.BindingParams(hpfFreqParam);
            this.lowPassFilter.BindingParams(lpfFreqParam, this.lpfTypeParam);

            this.eqGroup.BindingBypassParam(eqBypassParam);
        }

        public void CopyFrom(MicModule other)
        {
            this.micLevel.initV = other.micLevel.v;

            this.micNoiseGate.initV = other.micNoiseGate.v;
            this.fbe.initV = other.fbe.v;

            this.compressor.CopyFrom(other.compressor);

            this.exciter1Select.initV = other.exciter1Select.v;
            this.bassExciter.on.initV = other.exciter1.on.v;
            this.bassExciter.CopyFrom(other.bassExciter);
            this.exciter1.CopyFrom(other.exciter1);
            this.exciter2.CopyFrom(other.exciter2);

            this.highPassFilter.CopyFrom(other.highPassFilter);
            this.lowPassFilter.CopyFrom(other.lowPassFilter);
            
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

            EQGroupAddHLPFilterFreqResp(this.freqResp, eqGroup, hpf, lpf);
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

        public override void ResetData()
        {
            this.micNoiseGate.initV = DefNoiseGate;
            this.fbe.initV = DefFbe;
            this.compressor.Reset();

            this.exciter1Select.initV = 0;

            //this.bassExciter
            //this.exciter1.CopyFrom(other.exciter1);
            //this.exciter2.CopyFrom(other.exciter2);

            this.highPassFilter.Reset();
            this._eqGroup.ResetData();
        }

#if COMM_TEST
        public override void RandomTest()
        {
            //this.micInput.RandomTest();
            this.micNoiseGate.RandomTest();
            this.fbe.RandomTest();
            this.compressor.RandomTest();

            this.hpfFreqParam.RandomTest();
            this.lpfFreqParam.RandomTest();
            this.lpfTypeParam.RandomTest();

            this.hpf.freqIndex = this.hpfFreqParam.v;
            this.lpf.freqIndex = this.lpfFreqParam.v;
            this.lpf.typeIndex = this.lpfTypeParam.v;

            this.eqGroup.RandomTest();
            base.UpdateEQGroup();
            this.eqBypassParam.RandomTest();
            this.eqGroup.isBypass = this.eqBypassParam.value;
        }
#endif
    }

    public class MicAModule : MicModule
    {
        public MicAModule(EventBus effectorEvent_) :
            base(VKxPIIDCMCommProtocol.micAModuleIndex, effectorEvent_)
        {

        }
    }

    public class MicBModule : MicModule
    {
        public MicBModule(EventBus effectorEvent_) :
            base(VKxPIIDCMCommProtocol.micBModuleIndex, effectorEvent_)
        {

        }
    }
}
