using System.Drawing;
using AEC.Core;
using AEC.Comm.VKxPIIDCMComm;
using AEC.Common;

namespace AEC.Effectors.VKxPIIDCM.Modules
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
                (byte)VKxPIIDCMCommProtocol.AuxParamAddr.delay,
                MaxDelayIndex,
                this.OnParamChanged);
        }

        public void CopyFrom(MonoOutputModule other)
        {
            base.CopyFrom(other);
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
            base(OutputChannelType.Aux1Output, VKxPIIDCMCommProtocol.aux1ModuleIndex, effectorEvent_, autoWorkMode_: autoWorkMode_)
        {
            //_themeColor = Color.FromArgb(2, 119, 189);
            //_themeColor = Color.FromArgb(1, 88, 198);
            _themeColor = Color.FromArgb(0, 186, 255);
        }
    }

    public class Aux2OutputModule : MonoOutputModule
    {
        public Aux2OutputModule(EventBus effectorEvent_, AutoWorkMode autoWorkMode_) :
            base(OutputChannelType.Aux2Output, VKxPIIDCMCommProtocol.aux2ModuleIndex, effectorEvent_, autoWorkMode_: autoWorkMode_)
        {
            //_themeColor = Color.FromArgb(0, 148, 148);
            _themeColor = Color.FromArgb(0, 188, 188);
        }
    }
}
