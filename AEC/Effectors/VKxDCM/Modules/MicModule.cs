using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.VKxDCMComm;

namespace AEC.Effectors.VKxDCM.Modules
{
    public class MicModule : ModuleBase
    {
        public static readonly double[] initFreqList = new double[] { 86.4, 125.0, 177.0, 250.0, 354.0, 500.0, 707.0, 1000.0, 1410.0, 2000.0, 2830.0, 4000.0, 5660.0, 8000.0, 11300.0 };
        
        public const double HPFMaxFreq = 303;
        public const double HPFInitFreq = 50.6;
        public const double LPFMinFreq = 1000;

        public const short MaxFbe = 3;
        public const short DefFbe = 2;

        public const short MaxNoiseGate = 10;
        public const short DefNoiseGate = 4;

        public const short MaxLevel = 10;
        public const short DefLevel = MaxLevel;

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

        public MicModule anotherMic { get; set; }

        public bool isLinkMicAB { get; set; }

        public MicModule(byte moduleIndex_, EventBus effectorEvent_) :
            base(moduleIndex_, effectorEvent_)
        {
            //_themeColor = Color.FromArgb(168, 68, 0);
            base._themeColor = Color.FromArgb(230, 81, 0);

            this.micLevel = new ShortParam(
                base.moduleIndex,
                (byte)VKxDCMCommProtocol.MicParamAddr.micLevel,
                MaxLevel,
                this.OnParamChanged
                );

            this.micNoiseGate = new ShortParam(
                base.moduleIndex,
                (byte)VKxDCMCommProtocol.MicParamAddr.noiseGate,
                (short)MaxNoiseGate,
                this.OnParamChanged);

            this.fbe = new ShortParam(
                base.moduleIndex,
                (byte)VKxDCMCommProtocol.MicParamAddr.fbe,
                MaxFbe,
                this.OnParamChanged);

            this.compressor = new Compressor(
                base.moduleIndex,
                (byte)VKxDCMCommProtocol.MicParamAddr.attack, 
                this.OnParamChanged);

            this.exciter1Select = new ShortParam(base.moduleIndex, (byte)VKxDCMCommProtocol.MicParamAddr.exciter1Select, 1, this.OnParamChanged);
            this.bassExciter = new BassExciter(base.moduleIndex, (byte)VKxDCMCommProtocol.MicParamAddr.bassExciterOn, this.OnParamChanged);
            this.exciter1 = new Exciter(base.moduleIndex, (byte)VKxDCMCommProtocol.MicParamAddr.exciter1On, this.OnParamChanged);
            this.exciter2 = new Exciter(base.moduleIndex, (byte)VKxDCMCommProtocol.MicParamAddr.exciter2On, this.OnParamChanged);

            this.highPassFilter = XPFilterFactory.CreateImmutableTypeHighPassFilter(HPFMaxFreq, HPFInitFreq, FilterType.Butterworth12dB);
            this.lowPassFilter = XPFilterFactory.Create2ndOrderLowPassFilter(LPFMinFreq, DataUtils.maxFilterFreq);

            this.hpfFreqParam = new ShortParam(
                base.moduleIndex,
                (byte)VKxDCMCommProtocol.MicParamAddr.hpfFreq,
                this.highPassFilter.freqMaxIndex,
                this.OnParamChanged);

            this.lpfFreqParam = new ShortParam(
                base.moduleIndex,
                (byte)VKxDCMCommProtocol.MicParamAddr.lpfFreq,
                this.lowPassFilter.freqMaxIndex,
                this.OnParamChanged);

            this.lpfTypeParam = new ShortParam(
                base.moduleIndex,
                (byte)VKxDCMCommProtocol.MicParamAddr.lpfType,
                this.lowPassFilter.typeMaxIndex,
                this.OnParamChanged);
            
            this._eqGroup = new EQGroup(initFreqList);

            this.micLevel.initV = DefLevel;
            this.micNoiseGate.initV = DefNoiseGate;
            this.fbe.initV = DefFbe;

            this.eqBypassParam = new BoolParam(this.moduleIndex, (byte)VKxDCMCommProtocol.MicParamAddr.eqPass, this.OnParamChanged);

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
            this.micLevel.initV = DefLevel;

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

        public bool IsEqual(MicModule other)
        {
            if(this.micLevel.v != other.micLevel.v ||
               this.micNoiseGate.v != other.micNoiseGate.v ||
               this.fbe.v != other.fbe.v ||
                this.highPassFilter.freqIndex != other.highPassFilter.freqIndex ||
                this.lowPassFilter.freqIndex != other.lowPassFilter.freqIndex ||
                this.lowPassFilter.typeIndex != other.lowPassFilter.typeIndex ||
                !this.compressor.IsEqual(other.compressor) ||
                !this.eqGroup.IsEqual(other.eqGroup))
            {
                return false;
            }

            return true;
        }

        public void SynchronousFrom(MicModule other)
        {
            this.micLevel.v = other.micLevel.v;
            this.micNoiseGate.v = other.micNoiseGate.v;
            this.fbe.v = other.fbe.v;

            if (this.highPassFilter.filterFreqIndex != other.highPassFilter.filterFreqIndex)
            {
                this.highPassFilter.filterFreqIndex = other.highPassFilter.filterFreqIndex;
            }

            if(this.lowPassFilter.filterFreqIndex != other.lowPassFilter.filterFreqIndex)
            {
                this.lowPassFilter.filterFreqIndex = other.lowPassFilter.filterFreqIndex;
            }

            if (this.lowPassFilter.typeIndexParam != other.lowPassFilter.typeIndexParam)
            {
                this.lowPassFilter.typeIndexParam = other.lowPassFilter.typeIndexParam;
            }

            this.compressor.SynchronousFrom(other.compressor);

            for (int i = 0; i < this.eqGroup.eqList.Length && i < other.eqGroup.eqList.Length; ++i)
            {
                if (i != other.eqGroup.currEQIndex && !this.eqGroup.eqList[i].IsEqual(other.eqGroup.eqList[i]))
                {
                    this.eqGroup.eqList[i].CopyFrom(other.eqGroup.eqList[i], false);
                    this.UpdateEQParam(this.eqGroup.eqList[i]);
                }
            }

            short currEQIndex = other.eqGroup.currEQIndex;
            this.eqGroup.currEQIndex = currEQIndex;
            this.eqGroup.eqList[currEQIndex].CopyFrom(other.eqGroup.eqList[currEQIndex], false);
            this.UpdateEQParam(this.eqGroup.eqList[currEQIndex]);

            this.eqGroup.isBypassParam = other.eqGroup.isBypassParam;
        }

        public override void ResetEQGroup()
        {
            base.ResetEQGroup();

            if (!isLinkMicAB)
            {
                return;
            }

            System.Diagnostics.Debug.Assert(anotherMic != null);

            this.anotherMic.eqGroup.Reset();

            if (this.effectorEvent != null)
            {
                for (int i = this.anotherMic.eqGroup.eqBandCount - 1; i >= 0; --i)
                {
                    this.effectorEvent.NotifyObeserver(new EQChangedArgs(this.anotherMic.moduleIndex, this.anotherMic.eqGroup.eqList[i]));
                }
            }
        }

        protected override void UpdateEQ(EQ eq)
        {
            base.UpdateEQ(eq);

            if (!isLinkMicAB)
            {
                return;
            }

            System.Diagnostics.Debug.Assert(anotherMic != null && eq.index < anotherMic.eqGroup.eqBandCount);

            anotherMic.eqGroup.currEQIndex = eq.index;
            anotherMic.eqGroup.eqList[eq.index].CopyFrom(eq, false);
            anotherMic.UpdateEQParam(anotherMic.eqGroup.eqList[eq.index]);
        }

        protected override void OnParamChanged(object sender, DataParamChangedArgs e)
        {
            base.OnParamChanged(sender, e);

            if(!isLinkMicAB)
            {
                return;
            }

            System.Diagnostics.Debug.Assert(anotherMic != null);

            ShortParam src = sender as ShortParam;
            if(src.Equals(this.micLevel))
            {
                anotherMic.micLevel.initV = this.micLevel.v;
                LinkedParamChanged(anotherMic.micLevel);
            }
            else if(src.Equals(this.micNoiseGate))
            {
                anotherMic.micNoiseGate.initV = this.micNoiseGate.v;
                LinkedParamChanged(anotherMic.micNoiseGate);
            }
            else if (src.Equals(this.fbe))
            {
                anotherMic.fbe.initV = this.fbe.v;
                LinkedParamChanged(anotherMic.fbe);
            }
            else if(src.Equals(this.hpfFreqParam))
            {
                //anotherMic.hpfFreqParam.initV = this.hpfFreqParam.v;
                anotherMic.highPassFilter.freqIndex = this.hpfFreqParam.v;
                LinkedParamChanged(anotherMic.hpfFreqParam);
            }
            else if (src.Equals(this.lpfFreqParam))
            {
                //anotherMic.hpfFreqParam.initV = this.hpfFreqParam.v;
                anotherMic.lowPassFilter.freqIndex = this.lpfFreqParam.v;
                LinkedParamChanged(anotherMic.lpfFreqParam);
            }
            else if (src.Equals(this.lpfTypeParam))
            {
                //anotherMic.hpfFreqParam.initV = this.hpfFreqParam.v;
                anotherMic.lowPassFilter.typeIndex = this.lpfTypeParam.v;
                LinkedParamChanged(anotherMic.lpfTypeParam);
            }
            else if(src.Equals(this.compressor.threshold))
            {
                anotherMic.compressor.threshold.initV = this.compressor.threshold.v;
                LinkedParamChanged(anotherMic.compressor.threshold);
            }
            else if (src.Equals(this.compressor.attack))
            {
                anotherMic.compressor.attack.initV = this.compressor.attack.v;
                LinkedParamChanged(anotherMic.compressor.attack);
            }
            else if (src.Equals(this.compressor.release))
            {
                anotherMic.compressor.release.initV = this.compressor.release.v;
                LinkedParamChanged(anotherMic.compressor.release);
            }
            else if (src.Equals(this.compressor.ratio))
            {
                anotherMic.compressor.ratio.initV = this.compressor.ratio.v;
                LinkedParamChanged(anotherMic.compressor.ratio);
            }
            else if(src.Equals(this.eqBypassParam))
            {
                anotherMic.eqGroup.eqBypassInit = this.eqBypassParam.value;
                LinkedParamChanged(anotherMic.eqBypassParam);
            }
        }

        private void LinkedParamChanged(ShortParam param)
        {
            System.Diagnostics.Debug.Assert(param != null);
            base.OnParamChanged(param, new DataParamChangedArgs(param));
        }

#if COMM_TEST
        public override void RandomTest()
        {
            this.micNoiseGate.RandomTest();
            this.fbe.RandomTest();
            this.compressor.RandomTest();
            this.micLevel.RandomTest();

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

            if(this.moduleIndex == VKxDCMCommProtocol.micAModuleIndex)
            {
                exciter1Select.RandomTest();

                exciter1.on.RandomTest();
                bassExciter.on.initV = exciter1.on.v;
                bassExciter.on.UpdateParam();
                Log.OutputLine("bassExciter : {0}, exciter1 switch : {1}", bassExciter.on.v, exciter1.on.v);
                if(exciter1Select.v == 0)
                {
                    bassExciter.RandomTest();
                }
                else
                {
                    exciter1.RandomTest();
                }

                exciter2.on.RandomTest();
                exciter2.RandomTest();
            }
        }
#endif
    }

    public class MicAModule : MicModule
    {
        public MicAModule(EventBus effectorEvent_) :
            base(VKxDCMCommProtocol.micAModuleIndex, effectorEvent_)
        {

        }
    }

    public class MicBModule : MicModule
    {
        public MicBModule(EventBus effectorEvent_) :
            base(VKxDCMCommProtocol.micBModuleIndex, effectorEvent_)
        {

        }
    }
}
