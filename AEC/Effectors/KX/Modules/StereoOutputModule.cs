using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.KXComm;

namespace AEC.Effectors.KX.Modules
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
            base(outputChannel, index, effectorEvent_, autoWorkMode_: autoWorkMode_)
        {
            this.leftChannel = new OutputChannel(
                this.moduleIndex,
                (byte)KXCommProtocol.OutputParamAddr.leftChDelay,
                MaxDelayIndex,
                this.OnParamChanged);

            this.rightChannel = new OutputChannel(
                this.moduleIndex,
                (byte)KXCommProtocol.OutputParamAddr.rightChDelay,
                MaxDelayIndex,
                this.OnParamChanged);
        }

        public void CopyFrom(StereoOutputModule other)
        {
            base.CopyFrom(other);
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
            base(OutputChannelType.MainOutput, KXCommProtocol.mainOutputModuleIndex, effectorEvent_, autoWorkMode_:autoWorkMode_)
        {
            //_themeColor = Color.FromArgb(55, 150, 0);
            _themeColor = Color.FromArgb(72, 208, 7);
        }
    }

    class SurOutputModule : StereoOutputModule
    {
        public SurOutputModule(EventBus effectorEvent_, AutoWorkMode autoWorkMode_) :
            base(OutputChannelType.SurOutput, KXCommProtocol.surOutputModuleIndex, effectorEvent_, autoWorkMode_: autoWorkMode_)
        {
            //_themeColor = Color.FromArgb(107, 11, 137);
            _themeColor = Color.FromArgb(189, 33, 237);
        }
    }
}
