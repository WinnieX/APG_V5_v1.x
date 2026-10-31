using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.X5Comm;

namespace AEC.Effectors.X5.Modules
{
    public class CenterOutputModule : OutputBaseModule
    {
        public readonly OutputMixer musicMixer;
        public readonly OutputMixer micMixer;
        public readonly OutputMixer echoMixer;
        public readonly OutputMixer reverbMixer;

        public readonly X5OutputChannel monoChannel;

        public readonly HighPassFilter singHPF = null;
        public readonly HighPassFilter discoHPF = null;
        private readonly ShortParam singHpfFreqParam = null;
        private readonly ShortParam discoHpfFreqParam = null;
        private readonly ShortParam singHpfTypeParam = null;
        private readonly ShortParam discoHpfTypeParam = null;

        public readonly EQGroup singEQGroup = null;
        public readonly EQGroup discoEQGroup = null;

        public override bool outputIsMute
        {
            get
            {
                return this.monoChannel.mute.value;
            }
            set
            {
                this.monoChannel.mute.value = value;
            }
        }

        public CenterOutputModule(EventBus effectorEvent_, AutoWorkMode autoWorkModule_) :
            base(OutputChannelType.Aux1Output, X5CommProtocol.centerModuleIndex, effectorEvent_, autoWorkModule_)
        {
            _themeColor = Color.FromArgb(0, 186, 255);

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

            this.monoChannel = new X5OutputChannel(
                this.moduleIndex,
                (byte)X5CommProtocol.StereoOutputParamAddr.leftChDelay,
                MaxDelayIndex,
                this.OnParamChanged);

            this.singHPF = XPFilterFactory.CreateX5HighPassFilter(HPFMaxFreq);// new HighPassFilter(maxHPFFreqIndex);
            this.discoHPF = XPFilterFactory.CreateX5HighPassFilter(HPFMaxFreq);
            this.singHPF.SetGetTypeAsOrderLabel();
            this.discoHPF.SetGetTypeAsOrderLabel();

            this.singHpfFreqParam = new CenterOutputParam((byte)X5CommProtocol.OutputBaseParamAddr.hpfFreq, this.singHPF.freqMaxIndex, OnParamChanged);
            this.discoHpfFreqParam = new CenterOutputParam((byte)X5CommProtocol.OutputBaseParamAddr.hpfFreq, this.discoHPF.freqMaxIndex, OnParamChanged);
            this.singHpfTypeParam = new CenterOutputParam((byte)X5CommProtocol.OutputBaseParamAddr.hpfType, this.singHPF.typeMaxIndex, this.OnParamChanged);
            this.discoHpfTypeParam = new CenterOutputParam((byte)X5CommProtocol.OutputBaseParamAddr.hpfType, this.discoHPF.typeMaxIndex, this.OnParamChanged);

            this.singHPF.BindingParams(this.singHpfFreqParam, this.singHpfTypeParam);
            this.discoHPF.BindingParams(this.discoHpfFreqParam, this.discoHpfTypeParam);

            this.singEQGroup = new EQGroup(initFreqList_3_Band);
            this.discoEQGroup = new EQGroup(initFreqList_3_Band);

            this.singEQGroup.BindingBypassParam(this.singEQBypassParam);
            this.discoEQGroup.BindingBypassParam(this.discoEQBypassParam);

            base.mixer = new X5Mixer(base.autoWorkMode, this.musicMixer, this.micMixer, this.echoMixer, this.reverbMixer);
        }

        public void CopyFrom(CenterOutputModule other)
        {
            base.CopyFrom(other);

            this.musicMixer.CopyFrom(other.musicMixer);
            this.micMixer.CopyFrom(other.micMixer);
            this.echoMixer.CopyFrom(other.echoMixer);
            this.reverbMixer.CopyFrom(other.reverbMixer);

            this.monoChannel.CopyFrom(other.monoChannel);

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

            //this.leftChannel.RandomTest();
            //this.rightChannel.RandomTest();
        }
#endif
    }
}
