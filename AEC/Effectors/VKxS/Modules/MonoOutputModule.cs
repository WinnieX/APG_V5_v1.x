using System.Drawing;
using AEC.Core;
using AEC.Comm.VKxSComm;
using AEC.Common;

namespace AEC.Effectors.VKxS.Modules
{
    public class MonoOutputModule : OutputBaseModule
    {
        //public readonly ShortParam delay;
        //public readonly BoolParam mute;
        public readonly OutputChannel monoChannel;

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

        public MonoOutputModule(OutputChannelType outputChannelType, byte index, EventBus effectorEvent_, AutoWorkMode autoWorkMode_) :
            base(outputChannelType: outputChannelType, index: index, effectorEvent_: effectorEvent_, autoWorkMode_: autoWorkMode_)
        {
            this.monoChannel = new OutputChannel(
                this.moduleIndex,
                (byte)VKxSCommProtocol.AuxParamAddr.delay,
                MaxDelayIndex,
                this.OnParamChanged);
        }

        public void CopyFrom(MonoOutputModule other)
        {
            base.CopyFrom(other);
            this.monoChannel.CopyFrom(other.monoChannel);
        }

        public void CopyFrom(AEC.Effectors.X5.Modules.CenterOutputModule other)
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
            this.singHPF.typeIndex = other.singHPF.typeIndex == 0 ? (short)FilterType.Butterworth12dB : (short)FilterType.LinkwitzRiley24dB;
            this.discoHPF.freqIndex = other.discoHPF.freqIndex;
            this.discoHPF.typeIndex = other.discoHPF.typeIndex == 0 ? (short)FilterType.Butterworth12dB : (short)FilterType.LinkwitzRiley24dB;

            this.limiter.CopyFrom(other.limiter);

            this.singEQGroup.CopyFrom(other.singEQGroup);
            this.discoEQGroup.CopyFrom(other.discoEQGroup);

            this.monoChannel.CopyFrom(other.monoChannel);
        }

        public void CopyFrom(AEC.Effectors.X5.Modules.SubOutputModule other)
        {
            this.musicMixer.CopyFrom(other.musicMixer);
            this.micMixer.CopyFrom(other.micMixer);
            this.echoMixer.Reset();
            this.reverbMixer.Reset();

            //this.singHPF.Reset();
            //this.discoHPF.Reset();
            //this.singLPF.Reset();
            //this.discoLPF.Reset();

            this.singHPF.freqIndex = other.singHPF.freqIndex;
            this.singHPF.typeIndex = (short)((short)FilterType.LinkwitzRiley24dB + 1 + other.singHPF.typeIndex);
            this.singHPF.qIndex = other.singHPF.qIndex;
            this.discoHPF.freqIndex = other.discoHPF.freqIndex;
            this.discoHPF.typeIndex = (short)((short)FilterType.LinkwitzRiley24dB + 1 + other.discoHPF.typeIndex);
            this.discoHPF.qIndex = other.discoHPF.qIndex;

            this.singLPF.freqIndex = other.singLPF.freqIndex;
            this.singLPF.typeIndex = (short)((short)FilterType.LinkwitzRiley24dB + 1 + other.singLPF.typeIndex);
            this.singLPF.qIndex = other.singLPF.qIndex;
            this.discoLPF.freqIndex = other.discoLPF.freqIndex;
            this.discoLPF.typeIndex = (short)((short)FilterType.LinkwitzRiley24dB + 1 + other.discoLPF.typeIndex);
            this.discoLPF.qIndex = other.discoLPF.qIndex;

            this.limiter.CopyFrom(other.limiter);

            this.singEQGroup.CopyFrom(other.singEQGroup);
            this.discoEQGroup.CopyFrom(other.discoEQGroup);

            this.monoChannel.CopyFrom(other.monoChannel);
        }

#if COMM_TEST
        public override void RandomTest()
        {
            base.RandomTest();

            this.monoChannel.RandomTest();
        }
#endif
    }

    public class Aux1OutputModule : MonoOutputModule
    {
        public Aux1OutputModule(EventBus effectorEvent_, AutoWorkMode autoWorkMode_) :
            base(OutputChannelType.Aux1Output, VKxSCommProtocol.aux1ModuleIndex, effectorEvent_, autoWorkMode_)
        {
            //_themeColor = Color.FromArgb(2, 119, 189);
            //_themeColor = Color.FromArgb(1, 88, 198);
            _themeColor = Color.FromArgb(0, 186, 255);
        }
    }

    public class Aux2OutputModule : MonoOutputModule
    {
        public Aux2OutputModule(EventBus effectorEvent_, AutoWorkMode autoWorkMode_) :
            base(OutputChannelType.Aux2Output, VKxSCommProtocol.aux2ModuleIndex, effectorEvent_, autoWorkMode_)
        {
            //_themeColor = Color.FromArgb(0, 148, 148);
            _themeColor = Color.FromArgb(0, 188, 188);
        }
    }
}
