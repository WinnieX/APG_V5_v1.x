using System.Drawing;
using AEC.Core;
using AEC.Comm.VXComm;
using AEC.Common;

namespace AEC.Effectors.VX.Modules
{
    public class AuxOutputModule : OutputBaseModule
    {
        public static readonly double[] initFreqList = new double[] { 80.3, 297.0, 1100.0, 4080.0, 15100.0 };

        public readonly ShortParam delay;
        public readonly BoolParam mute;

        public override bool outputIsMute
        {
            get
            {
                return this.mute.value;
            }
            set
            {
                this.mute.value = value;
            }
        }

        public AuxOutputModule(OutputChannelType outputChannel, byte index, EventBus effectorEvent_, AutoWorkMode autoWorkMode_) :
            base(outputChannel: outputChannel, index: index, effectorEvent_: effectorEvent_, eqGroup_: new EQGroup(initFreqList), autoWorkMode_: autoWorkMode_)
        {
            this.delay = new ShortParam(
                this.moduleIndex,
                (byte)VXCommProtocol.AuxParamAddr.delay,
                MaxDelay,
                this.OnParamChanged);

            this.mute = new BoolParam(
                this.moduleIndex,
                (byte)VXCommProtocol.AuxParamAddr.mute,
                this.OnParamChanged);

            this.eqIsPassParam = new BoolParam(this.moduleIndex, (byte)VXCommProtocol.AuxParamAddr.eqPass, this.OnParamChanged);

            this._eqGroup.BindingBypassParam(this.eqIsPassParam);
        }

        public void CopyFrom(AuxOutputModule other)
        {
            base.CopyFrom(other);
            this.delay.initV = other.delay.v;
            this.mute.initV = other.mute.v;
        }
    }

    public class Aux1OutputModule : AuxOutputModule
    {
        public Aux1OutputModule(EventBus effectorEvent_, AutoWorkMode autoWorkMode_) :
            base(OutputChannelType.Aux1Output, VXCommProtocol.aux1ModuleIndex, effectorEvent_, autoWorkMode_)
        {
            //_themeColor = Color.FromArgb(2, 119, 189);
            //_themeColor = Color.FromArgb(1, 88, 198);
            _themeColor = Color.FromArgb(72, 208, 7);
        }
    }

    public class Aux2OutputModule : AuxOutputModule
    {
        public Aux2OutputModule(EventBus effectorEvent_, AutoWorkMode autoWorkMode_) :
            base(OutputChannelType.Aux2Output, VXCommProtocol.aux2ModuleIndex, effectorEvent_, autoWorkMode_)
        {
            //_themeColor = Color.FromArgb(0, 148, 148);
            _themeColor = Color.FromArgb(72, 208, 7);
        }
    }
}
