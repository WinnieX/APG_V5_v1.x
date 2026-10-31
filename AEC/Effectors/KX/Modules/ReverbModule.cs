using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.KXComm;

namespace AEC.Effectors.KX.Modules
{
    public class ReverbModule : ModuleBase
    {
        public const double MaxHpfFreq = 1000;
        public const double MinLpfFreq = 1000;
        public const short DefMicHPFTypeIndex = 2;
        public const short DefMicLPFTypeIndex = 7;
        public const short MaxPreDelay = 200;
        public const short MaxDelay = 80;
        public const short MaxDamp = 10;
        public const double DefToneControlBassCutoffFreq = 200;         //低音衰减频率
        public const double DefToneControlTrebleCutoffFreq = 3000;      //高音衰减频率

        public const short DefDampIndex = 6;
        
        public readonly Level reverbLevel;
        public readonly Level directLevel;
        
        public readonly HighPassFilter highPassFilter;
        public readonly LowPassFilter lowPassFilter;

        private readonly ReverbParam micHpfFreqParam;
        private readonly ReverbParam micHpfTypeParam;
        private readonly ReverbParam micLpfFreqParam;
        private readonly ReverbParam micLpfTypeParam;

        public readonly ReverbParam delayTime;
        public readonly ReverbParam preDelay;
        public readonly ReverbParam trebleDamp;
        public readonly ReverbParam bassDamp;

        public readonly ToneControlFilter toneControlFilter;

        public ReverbModule(EventBus commEvent_) :
            base(KXCommProtocol.reverbModuleIndex, commEvent_)
        {
            //_themeColor = Color.FromArgb(248, 187, 208);
            //_themeColor = Color.FromArgb(244, 179, 179);
            _themeColor = Color.FromArgb(228, 93, 80);
            activeItem = ActiveItem.HPF;

            this.reverbLevel = new Level(
                this.moduleIndex,
                (byte)KXCommProtocol.ReverbParamAddr.reverbMicLevel,
                (byte)KXCommProtocol.ReverbParamAddr.reverbMicLevelPhase, 
                this.OnParamChanged);
            this.directLevel = new Level(
                this.moduleIndex,
                (byte)KXCommProtocol.ReverbParamAddr.directMicLevel,
                (byte)KXCommProtocol.ReverbParamAddr.directMicLevelPhase, 
                this.OnParamChanged);

            this.highPassFilter = XPFilterFactory.Create2ndOrderHighPassFilter(MaxHpfFreq);
            this.lowPassFilter = XPFilterFactory.Create4thOrderLowPassFilter(MinLpfFreq);

            this.micHpfFreqParam = new ReverbParam((byte)KXCommProtocol.ReverbParamAddr.reverbMicHPF, this.highPassFilter.freqMaxIndex, this.OnParamChanged);
            this.micHpfTypeParam = new ReverbParam((byte)KXCommProtocol.ReverbParamAddr.reverbMicHPFType, this.highPassFilter.typeMaxIndex, this.OnParamChanged);
            this.micLpfFreqParam = new ReverbParam((byte)KXCommProtocol.ReverbParamAddr.reverbMicLPF, this.lowPassFilter.freqMaxIndex, this.OnParamChanged);
            this.micLpfTypeParam = new ReverbParam((byte)KXCommProtocol.ReverbParamAddr.reverbMicLPFType, this.lowPassFilter.typeMaxIndex, this.OnParamChanged);

            this.highPassFilter.BindingParams(this.micHpfFreqParam, this.micHpfTypeParam);
            this.lowPassFilter.BindingParams(this.micLpfFreqParam, this.micLpfTypeParam);

            this.highPassFilter.typeIndex = DefMicHPFTypeIndex;
            this.lowPassFilter.typeIndex = DefMicLPFTypeIndex;

            this.highPassFilter.SetGetTypeAsSlopeIndexLabel();
            this.lowPassFilter.SetGetTypeAsSlopeIndexLabel();

            this.delayTime = new ReverbParam((byte)KXCommProtocol.ReverbParamAddr.reverbTime, MaxDelay, this.OnParamChanged);
            this.preDelay = new ReverbParam((byte)KXCommProtocol.ReverbParamAddr.reverbPreDelay, MaxPreDelay, this.OnParamChanged);
            this.trebleDamp = new ReverbParam((byte)KXCommProtocol.ReverbParamAddr.reverbTrebleDamp, MaxDamp, this.OnParamChanged);
            this.bassDamp = new ReverbParam((byte)KXCommProtocol.ReverbParamAddr.reverbBassDamp, MaxDamp, this.OnParamChanged);

            this.trebleDamp.initV = DefDampIndex;
            this.bassDamp.initV = DefDampIndex;

            this.toneControlFilter = new ToneControlFilter(DefToneControlBassCutoffFreq, new ToneControlBoostConverter(this.bassDamp), DefToneControlTrebleCutoffFreq, new ToneControlBoostConverter(this.trebleDamp));
        }

        public void CopyFrom(ReverbModule other)
        {
            this.reverbLevel.CopyFrom(other.reverbLevel);
            this.directLevel.CopyFrom(other.directLevel);

            this.highPassFilter.CopyFrom(other.highPassFilter);
            this.lowPassFilter.CopyFrom(other.lowPassFilter);

            this.delayTime.initV = other.delayTime.v;
            this.preDelay.initV = other.preDelay.v;
            this.trebleDamp.initV = other.trebleDamp.v;
            this.bassDamp.initV = other.bassDamp.v;
            this.dataIsPrivateParam = other.dataIsPrivateParam;
        }

        public override void InitFreqResp()
        {
            base.InitFreqResp();

            this.highPassFilter.UpdateFreqResp();
            this.lowPassFilter.UpdateFreqResp();
            this.toneControlFilter.UpdateFreqResp();

            this.UpdateFreqResp();
        }

        public override void UpdateFreqResp()
        {
            base.UpdateFreqResp();

            ToneControlAddXPFilterFreqResp(this.freqResp, this.toneControlFilter, this.lowPassFilter, this.highPassFilter);
        }

        public void UpdateToneControl()
        {
            this.toneControlFilter.UpdateFreqResp();
            this.UpdateFreqResp();
            this.eqGraphic.UpdateNormalRespFreqCurve();
        }

        public override void UpdateXPFromControlPanel(XPFilterType xpFilterType, FilterElement filterElement)
        {
            this.UpdateXPFilter(xpFilterType, filterElement);
            this.activeItem = xpFilterType == XPFilterType.HighPass ? ActiveItem.HPF : ActiveItem.LPF;
        }

        public override void ResetData()
        {
            this.reverbLevel.Reset();
            this.directLevel.Reset();

            this.highPassFilter.Reset();
            this.lowPassFilter.Reset();

            this.delayTime.initV = 0;
            this.preDelay.initV = 0;
            this.trebleDamp.initV = 0;
            this.bassDamp.initV = 0;
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
            this.reverbLevel.RandomTest();
            this.directLevel.RandomTest();

            this.preDelay.RandomTest();
            this.delayTime.RandomTest();
            this.trebleDamp.RandomTest();
            this.bassDamp.RandomTest();

            this.micHpfFreqParam.RandomTest();
            this.micHpfTypeParam.RandomTest();
            this.micLpfFreqParam.RandomTest();
            this.micLpfTypeParam.RandomTest();

            this.hpf.freqIndex = this.micHpfFreqParam.v;
            this.hpf.typeIndex = this.micHpfTypeParam.v;
            this.lpf.freqIndex = this.micLpfFreqParam.v;
            this.lpf.typeIndex = this.micLpfTypeParam.v;
        }
#endif
    }
}
