using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.X5Comm;

namespace AEC.Effectors.X5.Modules
{
    public class ReverbModule : ModuleBase
    {
        public const short MaxMixerLevel = 100;

        public const double MaxHpfFreq = 1000;
        public const double MinLpfFreq = 5990;
        public const double MaxLpfFreq = 20600;

        public const short MaxPreDelay = 100;
        public const short MaxReverbTime = 80;

        public readonly SignalMixer reverbMixer;
        public readonly SignalMixer directMixer;
        
        public readonly HighPassFilter highPassFilter;
        public readonly LowPassFilter lowPassFilter;

        private readonly ReverbParam micHpfFreqParam;
        private readonly ReverbParam micLpfFreqParam;

        public readonly ReverbParam preDelay;
        public readonly ReverbParam reverbTime;

        public ReverbModule(EventBus commEvent_) :
            base(X5CommProtocol.reverbModuleIndex, commEvent_)
        {
            //_themeColor = Color.FromArgb(248, 187, 208);
            //_themeColor = Color.FromArgb(244, 179, 179);
            _themeColor = Color.FromArgb(228, 93, 80);
            activeItem = ActiveItem.HPF;

            this.reverbMixer = new SignalMixer(
                this.moduleIndex,
                (byte)X5CommProtocol.ReverbParamAddr.reverbMixerLevel,
                (byte)X5CommProtocol.ReverbParamAddr.reverbMixerPhase, 
                MaxMixerLevel,
                this.OnParamChanged);
            this.directMixer = new SignalMixer(
                this.moduleIndex,
                (byte)X5CommProtocol.ReverbParamAddr.directMixerLevel,
                (byte)X5CommProtocol.ReverbParamAddr.directMixerPhase, 
                MaxMixerLevel,
                this.OnParamChanged);

            this.highPassFilter = XPFilterFactory.CreateImmutableTypeHighPassFilter(MaxHpfFreq, FilterType.Butterworth12dB);
            this.lowPassFilter = XPFilterFactory.CreateX5ImmutableTypeLowPassFilter(MinLpfFreq, MaxLpfFreq, FilterType.Butterworth12dB);

            this.micHpfFreqParam = new ReverbParam((byte)X5CommProtocol.ReverbParamAddr.reverbHpfFreq, this.highPassFilter.freqMaxIndex, this.OnParamChanged);
            this.micLpfFreqParam = new ReverbParam((byte)X5CommProtocol.ReverbParamAddr.reverbLpfFreq, this.lowPassFilter.freqMaxIndex, this.OnParamChanged);
            
            this.highPassFilter.BindingParams(this.micHpfFreqParam);
            this.lowPassFilter.BindingParams(this.micLpfFreqParam);

            this.preDelay = new ReverbParam((byte)X5CommProtocol.ReverbParamAddr.reverbPreDelay, MaxPreDelay, this.OnParamChanged);
            this.reverbTime = new ReverbParam((byte)X5CommProtocol.ReverbParamAddr.reverbTime, MaxReverbTime, this.OnParamChanged);
        }

        public void CopyFrom(ReverbModule other)
        {
            this.reverbMixer.CopyFrom(other.reverbMixer);
            this.directMixer.CopyFrom(other.directMixer);

            this.highPassFilter.CopyFrom(other.highPassFilter);
            this.lowPassFilter.CopyFrom(other.lowPassFilter);

            this.preDelay.initV = other.preDelay.v;
            this.reverbTime.initV = other.reverbTime.v;
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

            this.preDelay.initV = 0; 
            this.reverbTime.initV = 0;
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
