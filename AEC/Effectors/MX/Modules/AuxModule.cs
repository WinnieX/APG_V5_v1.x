using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.MXComm;

namespace AEC.Effectors.MX.Modules
{
    public class AuxOutputModule : OutputBaseModule
    {
        public readonly ShortParam delay;
        public readonly BoolParam mute;

        public readonly EQGroup _eqGroup = null;

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

        public AuxOutputModule(OutputChannelType outputChannel, byte index, EventBus effectorEvent_, MusicModule music_) :
            base(outputChannel: outputChannel, index: index, effectorEvent_: effectorEvent_, music_: music_)
        {
            this.delay = new ShortParam(
                this.moduleIndex,
                (byte)MXCommProtocol.AuxParamAddr.delay,
                MaxDelay,
                this.OnParamChanged);

            this.mute = new BoolParam(
                this.moduleIndex,
                (byte)MXCommProtocol.AuxParamAddr.mute,
                this.OnParamChanged);

            this.eqBypassParam = new BoolParam(this.moduleIndex, (byte)MXCommProtocol.AuxParamAddr.eqPass, this.OnParamChanged);

            this._eqGroup = new EQGroup(OutputBaseModule.initFreqList);
            this._eqGroup.BindingBypassParam(this.eqBypassParam);
        }

        public void CopyFrom(AuxOutputModule other)
        {
            base.CopyFrom(other);
            this.delay.initV = other.delay.v;
            this.mute.initV = other.mute.v;
            this._eqGroup.CopyFrom(other._eqGroup);
        }

        public override EQGroup eqGroup
        {
            get
            {
                return this._eqGroup;
            }
        }

        public override void ResetData()
        {
            this._eqGroup.ResetData();
            this.delay.initV = 0;
            this.mute.initV = 0;

            base.ResetData();
        }
    }

    public class Aux1OutputModule : AuxOutputModule
    {
        public Aux1OutputModule(EventBus effectorEvent_, MusicModule music_) :
            base(OutputChannelType.Aux1Output, MXCommProtocol.aux1ModuleIndex, effectorEvent_, music_)
        {
            //_themeColor = Color.FromArgb(1, 88, 198);
            _themeColor = Color.FromArgb(0, 186, 255);
        }
    }

    public class Aux2OutputModule : AuxOutputModule
    {
        public Aux2OutputModule(EventBus effectorEvent_, MusicModule music_) :
            base(OutputChannelType.Aux2Output, MXCommProtocol.aux2ModuleIndex, effectorEvent_, music_)
        {
            //_themeColor = Color.FromArgb(0, 148, 148);
            _themeColor = Color.FromArgb(0, 188, 188);
        }
    }
}
