using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.X5Comm;

namespace AEC.Effectors.X5.Modules
{
    public class SubOutputModule : OutputBaseModule
    {
        public readonly OutputMixer musicMixer;
        public readonly OutputMixer micMixer;

        public readonly X5OutputChannel monoChannel;

        public readonly HighPassFilter singHPF = null;
        public readonly HighPassFilter discoHPF = null;
        public readonly LowPassFilter singLPF = null;
        public readonly LowPassFilter discoLPF = null;

        private readonly ShortParam singHpfFreqParam = null;
        private readonly ShortParam discoHpfFreqParam = null;
        private readonly ShortParam singHpfTypeParam = null;
        private readonly ShortParam discoHpfTypeParam = null;
        private readonly ShortParam singHpfQParam = null;
        private readonly ShortParam discoHpfQParam = null;

        private readonly ShortParam singLpfFreqParam = null;
        private readonly ShortParam discoLpfFreqParam = null;
        private readonly ShortParam singLpfTypeParam = null;
        private readonly ShortParam discoLpfTypeParam = null;
        private readonly ShortParam singLpfQParam = null;
        private readonly ShortParam discoLpfQParam = null;

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

        public SubOutputModule(EventBus effectorEvent_, AutoWorkMode autoWorkModule_) :
            base(OutputChannelType.Aux2Output, X5CommProtocol.subModuleIndex, effectorEvent_, autoWorkModule_)
        {
            _themeColor = Color.FromArgb(0, 188, 188);

            this.musicMixer = new OutputMixer(
                this.moduleIndex,
                (byte)X5CommProtocol.OutputBaseParamAddr.musicMixerLevel,
                (byte)X5CommProtocol.OutputBaseParamAddr.musicMixerPhase,
                MaxSubOutputLevelIndex,
                this.OnParamChanged);

            this.micMixer = new OutputMixer(
                this.moduleIndex,
                (byte)X5CommProtocol.OutputBaseParamAddr.micMixerLevel,
                (byte)X5CommProtocol.OutputBaseParamAddr.micMixerPhase,
                MaxSubOutputLevelIndex,
                this.OnParamChanged);

            this.monoChannel = new X5OutputChannel(
                this.moduleIndex,
                (byte)X5CommProtocol.StereoOutputParamAddr.leftChDelay,
                MaxDelayIndex,
                this.OnParamChanged);

            this.singHPF = XPFilterFactory.CreateX5XoverHighPassFilter(HPFMaxFreq);// new HighPassFilter(maxHPFFreqIndex);
            this.discoHPF = XPFilterFactory.CreateX5XoverHighPassFilter(HPFMaxFreq);

            this.singHpfFreqParam = new SubOutputParam((byte)X5CommProtocol.OutputBaseParamAddr.hpfFreq, this.singHPF.freqMaxIndex, OnParamChanged);
            this.discoHpfFreqParam = new SubOutputParam((byte)X5CommProtocol.OutputBaseParamAddr.hpfFreq, this.discoHPF.freqMaxIndex, OnParamChanged);
            this.singHpfTypeParam = new SubOutputParam((byte)X5CommProtocol.OutputBaseParamAddr.hpfType, this.singHPF.typeMaxIndex, this.OnParamChanged);
            this.discoHpfTypeParam = new SubOutputParam((byte)X5CommProtocol.OutputBaseParamAddr.hpfType, this.discoHPF.typeMaxIndex, this.OnParamChanged);
            this.singHpfQParam = new SubOutputParam((byte)X5CommProtocol.OutputBaseParamAddr.hpfQ, this.singHPF.qMaxIndex, this.OnParamChanged);
            this.discoHpfQParam = new SubOutputParam((byte)X5CommProtocol.OutputBaseParamAddr.hpfQ, this.discoHPF.qMaxIndex, this.OnParamChanged);

            this.singHPF.BindingParams(this.singHpfFreqParam, this.singHpfTypeParam, this.singHpfQParam);
            this.discoHPF.BindingParams(this.discoHpfFreqParam, this.discoHpfTypeParam, this.discoHpfQParam);

            this.singLPF = XPFilterFactory.CreateX5XoverLowPassFilter(LPFMaxFreq);// new HighPassFilter(maxHPFFreqIndex);
            this.discoLPF = XPFilterFactory.CreateX5XoverLowPassFilter(LPFMaxFreq);

            this.singLpfFreqParam = new SubOutputParam((byte)X5CommProtocol.OutputBaseParamAddr.lpfFreq, this.singLPF.freqMaxIndex, OnParamChanged);
            this.discoLpfFreqParam = new SubOutputParam((byte)X5CommProtocol.OutputBaseParamAddr.lpfFreq, this.discoLPF.freqMaxIndex, OnParamChanged);
            this.singLpfTypeParam = new SubOutputParam((byte)X5CommProtocol.OutputBaseParamAddr.lpfType, this.singLPF.typeMaxIndex, this.OnParamChanged);
            this.discoLpfTypeParam = new SubOutputParam((byte)X5CommProtocol.OutputBaseParamAddr.lpfType, this.discoLPF.typeMaxIndex, this.OnParamChanged);
            this.singLpfQParam = new SubOutputParam((byte)X5CommProtocol.OutputBaseParamAddr.lpfQ, this.singLPF.qMaxIndex, this.OnParamChanged);
            this.discoLpfQParam = new SubOutputParam((byte)X5CommProtocol.OutputBaseParamAddr.lpfQ, this.discoLPF.qMaxIndex, this.OnParamChanged);

            this.singLPF.BindingParams(this.singLpfFreqParam, this.singLpfTypeParam, this.singLpfQParam);
            this.discoLPF.BindingParams(this.discoLpfFreqParam, this.discoLpfTypeParam, this.discoLpfQParam);

            this.singEQGroup = new EQGroup(initFreqList_3_Band);
            this.discoEQGroup = new EQGroup(initFreqList_3_Band);

            this.singEQGroup.BindingBypassParam(this.singEQBypassParam);
            this.discoEQGroup.BindingBypassParam(this.discoEQBypassParam);

            base.mixer = new X5Mixer(base.autoWorkMode, this.musicMixer, this.micMixer);
        }

        public void CopyFrom(SubOutputModule other)
        {
            base.CopyFrom(other);

            this.musicMixer.CopyFrom(other.musicMixer);
            this.micMixer.CopyFrom(other.micMixer);

            this.monoChannel.CopyFrom(other.monoChannel);

            this.singHPF.CopyFrom(other.singHPF);
            this.discoHPF.CopyFrom(other.discoHPF);
            this.singLPF.CopyFrom(other.singLPF);
            this.discoLPF.CopyFrom(other.discoLPF);

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
            this.singLPF.UpdateFreqResp();
            this.discoLPF.UpdateFreqResp();

            this.UpdateFreqResp();
        }

        public override void UpdateFreqResp()
        {
            base.UpdateFreqResp();

            EQGroupAddHLPFilterFreqResp(this.freqResp, this.eqGroup, this.lpf, this.hpf);
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

        public override LowPassFilter lpf
        {
            get
            {
                return this.isSingMode ? this.singLPF : this.discoLPF;
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
