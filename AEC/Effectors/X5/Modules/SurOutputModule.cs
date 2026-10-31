using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.X5Comm;

namespace AEC.Effectors.X5.Modules
{
    public class SurOutputModule : OutputBaseModule
    {
        public readonly OutputMixer musicMixer;
        public readonly OutputMixer micMixer;
        public readonly OutputMixer echoMixer;
        public readonly OutputMixer reverbMixer;

        public readonly X5OutputChannel leftChannel;
        public readonly X5OutputChannel rightChannel;

        public readonly HighPassFilter singHPF = null;
        public readonly HighPassFilter discoHPF = null;
        private readonly ShortParam singHpfFreqParam = null;
        private readonly ShortParam discoHpfFreqParam = null;

        public readonly EQGroup singEQGroup = null;
        public readonly EQGroup discoEQGroup = null;

        public override bool outputIsMute
        {
            get
            {
                return this.leftChannel.mute.value || this.rightChannel.mute.value;
            }
            set
            {
                this.leftChannel.mute.value = value;
                this.rightChannel.mute.value = value;
            }
        }

        public SurOutputModule(EventBus effectorEvent_, AutoWorkMode autoWorkModule_) :
            base(OutputChannelType.SurOutput, X5CommProtocol.surOutputModuleIndex, effectorEvent_, autoWorkModule_)
        {
            _themeColor = Color.FromArgb(189, 33, 237);

            this.musicMixer = new OutputMixer(
                this.moduleIndex,
                (byte)X5CommProtocol.OutputBaseParamAddr.musicMixerLevel,
                (byte)X5CommProtocol.OutputBaseParamAddr.musicMixerPhase,
                MaxDefOutputLevelIndex,
                this.OnParamChanged);

            this.micMixer = new OutputMixer(
                this.moduleIndex,
                (byte)X5CommProtocol.OutputBaseParamAddr.micMixerLevel,
                (byte)X5CommProtocol.OutputBaseParamAddr.micMixerPhase,
                MaxDefOutputLevelIndex,
                this.OnParamChanged);

            this.echoMixer = new OutputMixer(
                this.moduleIndex,
                (byte)X5CommProtocol.OutputBaseParamAddr.echoMixerLevel,
                (byte)X5CommProtocol.OutputBaseParamAddr.echoMixerPhase,
                MaxDefOutputLevelIndex,
                this.OnParamChanged);

            this.reverbMixer = new OutputMixer(
                this.moduleIndex,
                (byte)X5CommProtocol.OutputBaseParamAddr.reverbMixerLevel,
                (byte)X5CommProtocol.OutputBaseParamAddr.reverbMixerPhase,
                MaxDefOutputLevelIndex,
                this.OnParamChanged);

            this.leftChannel = new X5OutputChannel(
                this.moduleIndex,
                (byte)X5CommProtocol.StereoOutputParamAddr.leftChDelay,
                MaxDelayIndex,
                this.OnParamChanged);

            this.rightChannel = new X5OutputChannel(
                this.moduleIndex,
                (byte)X5CommProtocol.StereoOutputParamAddr.rightChDelay,
                MaxDelayIndex,
                this.OnParamChanged);

            this.singHPF = XPFilterFactory.CreateImmutableTypeHighPassFilter(HPFMaxFreq, FilterType.Butterworth12dB);// new HighPassFilter(maxHPFFreqIndex);
            this.discoHPF = XPFilterFactory.CreateImmutableTypeHighPassFilter(HPFMaxFreq, FilterType.Butterworth12dB);

            this.singHpfFreqParam = new SurOutputParam((byte)X5CommProtocol.OutputBaseParamAddr.hpfFreq, this.singHPF.freqMaxIndex, OnParamChanged);
            this.discoHpfFreqParam = new SurOutputParam((byte)X5CommProtocol.OutputBaseParamAddr.hpfFreq, this.discoHPF.freqMaxIndex, OnParamChanged);
            
            this.singHPF.BindingParams(this.singHpfFreqParam);
            this.discoHPF.BindingParams(this.discoHpfFreqParam);

            this.singEQGroup = new EQGroup(initFreqList_3_Band);
            this.discoEQGroup = new EQGroup(initFreqList_3_Band);

            this.singEQGroup.BindingBypassParam(this.singEQBypassParam);
            this.discoEQGroup.BindingBypassParam(this.discoEQBypassParam);

            base.mixer = new X5Mixer(base.autoWorkMode, this.musicMixer, this.micMixer, this.echoMixer, this.reverbMixer);
        }

        public void CopyFrom(SurOutputModule other)
        {
            base.CopyFrom(other);

            this.musicMixer.CopyFrom(other.musicMixer);
            this.micMixer.CopyFrom(other.micMixer);
            this.echoMixer.CopyFrom(other.echoMixer);
            this.reverbMixer.CopyFrom(other.reverbMixer);

            this.leftChannel.CopyFrom(other.leftChannel);
            this.rightChannel.CopyFrom(other.rightChannel);

            this.singHPF.CopyFrom(other.singHPF);
            this.discoHPF.CopyFrom(other.discoHPF);

            this.singEQGroup.CopyFrom(other.singEQGroup);
            this.discoEQGroup.CopyFrom(other.discoEQGroup);
        }

        public override void InitFreqResp()
        {
            base.InitFreqResp();

            this.singEQGroup.InitFreqResp();
            this.discoEQGroup.InitFreqResp();
            this.singHPF.UpdateFreqResp();
            this.discoHPF.UpdateFreqResp();

            this.UpdateFreqResp();
        }

        public override void UpdateFreqResp()
        {
            base.UpdateFreqResp();

            EQGroupAddFilterFreqResp(this.freqResp, this.eqGroup, this.hpf);
        }

        public SignalMixer musicSignalMixer
        {
            get
            {
                return base.isSingMode ? this.musicMixer.singMixer : this.musicMixer.discoMixer;
            }
        }

        public SignalMixer micSignalMixer
        {
            get
            {
                return base.isSingMode ? this.micMixer.singMixer : this.micMixer.discoMixer;
            }
        }

        public SignalMixer echoSignalMixer
        {
            get
            {
                return base.isSingMode ? this.echoMixer.singMixer : this.echoMixer.discoMixer;
            }
        }

        public SignalMixer reverbSignalMixer
        {
            get
            {
                return base.isSingMode ? this.reverbMixer.singMixer : this.reverbMixer.discoMixer;
            }
        }

        public override EQGroup eqGroup
        {
            get
            {
                return base.isSingMode ? this.singEQGroup : this.discoEQGroup;
            }
        }

        public override HighPassFilter hpf
        {
            get
            {
                return this.isSingMode ? this.singHPF : this.discoHPF;
            }
        }

#if COMM_TEST
        public override void RandomTest()
        {
            base.RandomTest();

            this.leftChannel.RandomTest();
            this.rightChannel.RandomTest();
        }
#endif
    }
}
