using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.VKxSComm;

namespace AEC.Effectors.VKxS.Modules
{
    public class ReverbModule : ModuleBase
    {
        public const short MaxMixerLevel = 100;

        public const double MaxHpfFreq = 1000;
        public const double MinLpfFreq = 1000;
        public const short DefMicHPFTypeIndex = 2;
        public const short DefMicLPFTypeIndex = 7;
        public const short MaxPreDelay = 200;
        public const short MaxReverbTime = 80;
        public const short MaxDamp = 10;
        public const double DefToneControlBassCutoffFreq = 200;         //低音衰减频率
        public const double DefToneControlTrebleCutoffFreq = 3000;      //高音衰减频率

        public const short DefDampIndex = 6;

        public readonly SignalMixer reverbMixer;
        public readonly SignalMixer directMixer;
        
        public readonly HighPassFilter highPassFilter;
        public readonly LowPassFilter lowPassFilter;

        private readonly ReverbParam micHpfFreqParam;
        private readonly ReverbParam micLpfFreqParam;

        public readonly ReverbParam reverbTime;
        public readonly ReverbParam preDelay;
        
        public ReverbModule(EventBus commEvent_) :
            base(VKxSCommProtocol.reverbModuleIndex, commEvent_)
        {
            //_themeColor = Color.FromArgb(248, 187, 208);
            //_themeColor = Color.FromArgb(244, 179, 179);
            _themeColor = Color.FromArgb(228, 93, 80);
            activeItem = ActiveItem.HPF;

            this.reverbMixer = new SignalMixer(
                this.moduleIndex,
                (byte)VKxSCommProtocol.ReverbParamAddr.reverbMixerLevel,
                (byte)VKxSCommProtocol.ReverbParamAddr.reverbMixerPhase,
                MaxMixerLevel,
                this.OnParamChanged);
            this.directMixer = new SignalMixer(
                this.moduleIndex,
                (byte)VKxSCommProtocol.ReverbParamAddr.directMixerLevel,
                (byte)VKxSCommProtocol.ReverbParamAddr.directMixerPhase,
                MaxMixerLevel,
                this.OnParamChanged);

            this.highPassFilter = XPFilterFactory.CreateImmutableTypeHighPassFilter(MaxHpfFreq);
            this.lowPassFilter = XPFilterFactory.CreateImmutableTypeLowPassFilter(MinLpfFreq, FilterType.Butterworth12dB);

            this.micHpfFreqParam = new ReverbParam((byte)VKxSCommProtocol.ReverbParamAddr.reverbHpfFreq, this.highPassFilter.freqMaxIndex, this.OnParamChanged);
            this.micLpfFreqParam = new ReverbParam((byte)VKxSCommProtocol.ReverbParamAddr.reverbLpfFreq, this.lowPassFilter.freqMaxIndex, this.OnParamChanged);
            
            this.highPassFilter.BindingParams(this.micHpfFreqParam);
            this.lowPassFilter.BindingParams(this.micLpfFreqParam);

            this.highPassFilter.typeIndex = DefMicHPFTypeIndex;
            this.lowPassFilter.typeIndex = DefMicLPFTypeIndex;

            this.preDelay = new ReverbParam((byte)VKxSCommProtocol.ReverbParamAddr.reverbPreDelay, MaxPreDelay, this.OnParamChanged);
            this.reverbTime = new ReverbParam((byte)VKxSCommProtocol.ReverbParamAddr.reverbTime, MaxReverbTime, this.OnParamChanged);
        }

        public void CopyFrom(ReverbModule other)
        {
            this.reverbMixer.CopyFrom(other.reverbMixer);
            this.directMixer.CopyFrom(other.directMixer);

            this.highPassFilter.CopyFrom(other.highPassFilter);
            this.lowPassFilter.CopyFrom(other.lowPassFilter);

            this.reverbTime.initV = other.reverbTime.v;
            this.preDelay.initV = other.preDelay.v;
        }

        public void CopyFrom(AEC.Effectors.X5.Modules.ReverbModule other)
        {
            this.reverbMixer.CopyFrom(other.reverbMixer);
            this.directMixer.CopyFrom(other.directMixer);

            this.highPassFilter.CopyFrom(other.highPassFilter);
            this.lowPassFilter.freqIndex = (short)(other.lowPassFilter.freqIndex + DataUtils.Freq2FilterIndex(AEC.Effectors.X5.Modules.ReverbModule.MinLpfFreq) - DataUtils.Freq2FilterIndex(EchoModule.MinLpfFreq));

            this.reverbTime.initV = other.reverbTime.v;
            this.preDelay.initV = other.preDelay.v;
        }

        public override void InitFreqResp()
        {
            base.InitFreqResp();

            this.highPassFilter.UpdateFreqResp();
            this.lowPassFilter.UpdateFreqResp();

            this.UpdateFreqResp();
        }

        public override void UpdateFreqResp()
        {
            base.UpdateFreqResp();

            HLPFilterFreqResp(this.freqResp, this.lowPassFilter, this.highPassFilter);
        }

        public void UpdateToneControl()
        {
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
            this.reverbMixer.Reset();
            this.directMixer.Reset();

            this.highPassFilter.Reset();
            this.lowPassFilter.Reset();

            this.reverbTime.initV = 0;
            this.preDelay.initV = 0;
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
            //this.reverbLevel.RandomTest();
            //this.directLevel.RandomTest();

            //this.preDelay.RandomTest();
            //this.delayTime.RandomTest();
            //this.trebleDamp.RandomTest();
            //this.bassDamp.RandomTest();

            //this.micHpfFreqParam.RandomTest();
            //this.micHpfTypeParam.RandomTest();
            //this.micLpfFreqParam.RandomTest();
            //this.micLpfTypeParam.RandomTest();

            //this.hpf.freqIndex = this.micHpfFreqParam.v;
            //this.hpf.typeIndex = this.micHpfTypeParam.v;
            //this.lpf.freqIndex = this.micLpfFreqParam.v;
            //this.lpf.typeIndex = this.micLpfTypeParam.v;
        }
#endif
    }
}
