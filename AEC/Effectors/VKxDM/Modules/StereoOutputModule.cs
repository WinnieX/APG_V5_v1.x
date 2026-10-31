using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.VKxDMComm;

namespace AEC.Effectors.VKxDM.Modules
{
    public class StereoOutputModule : OutputBaseModule
    {
        public readonly OutputChannel leftChannel;
        public readonly OutputChannel rightChannel;

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

        public StereoOutputModule(OutputChannelType outputChannel, byte index, EventBus effectorEvent_, AutoWorkMode autoWorkMode_) :
            base(outputChannel, index, effectorEvent_, autoWorkMode_)
        {
            this.leftChannel = new OutputChannel(
                this.moduleIndex,
                (byte)VKxDMCommProtocol.OutputParamAddr.leftChDelay,
                MaxDelayIndex,
                this.OnParamChanged);

            this.rightChannel = new OutputChannel(
                this.moduleIndex,
                (byte)VKxDMCommProtocol.OutputParamAddr.rightChDelay,
                MaxDelayIndex,
                this.OnParamChanged);
        }

        public void CopyFrom(StereoOutputModule other)
        {
            base.CopyFrom(other);
            this.leftChannel.CopyFrom(other.leftChannel);
            this.rightChannel.CopyFrom(other.rightChannel);
        }

        public void CopyFrom(AEC.Effectors.X5.Modules.MainOutputModule other)
        {
            this.musicMixer.CopyFrom(other.musicMixer);
            this.micMixer.CopyFrom(other.micMixer);
            this.echoMixer.CopyFrom(other.echoMixer);
            this.reverbMixer.CopyFrom(other.reverbMixer);

            this.singHPF.Reset();
            this.discoHPF.Reset();
            this.singLPF.Reset();
            this.discoLPF.Reset();

            this.limiter.CopyFrom(other.limiter);

            this.singEQGroup.CopyFrom(other.singEQGroup);
            this.discoEQGroup.CopyFrom(other.discoEQGroup);

            this.leftChannel.CopyFrom(other.leftChannel);
            this.rightChannel.CopyFrom(other.rightChannel);
        }

        public void CopyFrom(AEC.Effectors.X5.Modules.SurOutputModule other)
        {
            this.musicMixer.CopyFrom(other.musicMixer);
            this.micMixer.CopyFrom(other.micMixer);
            this.echoMixer.CopyFrom(other.echoMixer);
            this.reverbMixer.CopyFrom(other.reverbMixer);

            this.singHPF.Reset();
            this.discoHPF.Reset();
            this.singLPF.Reset();
            this.discoLPF.Reset();

            this.singHPF.freqIndex = other.singHPF.freqIndex;
            this.singHPF.typeIndex = (short)FilterType.Butterworth12dB;
            this.discoHPF.freqIndex = other.discoHPF.freqIndex;
            this.discoHPF.typeIndex = (short)FilterType.Butterworth12dB;

            this.limiter.CopyFrom(other.limiter);

            this.singEQGroup.CopyFrom(other.singEQGroup);
            this.discoEQGroup.CopyFrom(other.discoEQGroup);

            this.leftChannel.CopyFrom(other.leftChannel);
            this.rightChannel.CopyFrom(other.rightChannel);
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

    class MainOutputModule : StereoOutputModule
    {
        public MainOutputModule(EventBus effectorEvent_, AutoWorkMode autoWorkMode_) :
            base(OutputChannelType.MainOutput, VKxDMCommProtocol.mainOutputModuleIndex, effectorEvent_, autoWorkMode_)
        {
            //_themeColor = Color.FromArgb(55, 150, 0);
            _themeColor = Color.FromArgb(72, 208, 7);
        }
    }

    class SurOutputModule : StereoOutputModule
    {
        public SurOutputModule(EventBus effectorEvent_, AutoWorkMode autoWorkMode_) :
            base(OutputChannelType.SurOutput, VKxDMCommProtocol.surOutputModuleIndex, effectorEvent_, autoWorkMode_)
        {
            //_themeColor = Color.FromArgb(107, 11, 137);
            _themeColor = Color.FromArgb(189, 33, 237);
        }
    }
}
