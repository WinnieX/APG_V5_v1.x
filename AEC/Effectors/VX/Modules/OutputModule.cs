using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.VXComm;

namespace AEC.Effectors.VX.Modules
{
    public class OutputModule : OutputBaseModule
    {
        public readonly ShortParam leftChDelay;
        public readonly ShortParam rightChDelay;
        public readonly BoolParam leftChMute;
        public readonly BoolParam rightChMute;

        public override bool outputIsMute
        {
            get
            {
                return this.leftChMute.value || this.rightChMute.value;
            }
            set
            {
                this.leftChMute.value = value;
                this.rightChMute.value = value;
            }
        }

        public OutputModule(OutputChannelType outputChannel, byte index, EventBus effectorEvent_, EQGroup eqGroup_, MusicModule music_) :
            base(outputChannel, index, effectorEvent_, eqGroup_, music_)
        {
            this.leftChDelay = new ShortParam(
                this.moduleIndex,
                (byte)VXCommProtocol.OutputParamAddr.leftChDelay,
                MaxDelay,
                this.OnParamChanged);
            this.rightChDelay = new ShortParam(
                this.moduleIndex,
                (byte)VXCommProtocol.OutputParamAddr.rightChDelay,
                MaxDelay,
                this.OnParamChanged);
            this.leftChMute = new BoolParam(
                this.moduleIndex,
                (byte)VXCommProtocol.OutputParamAddr.leftChMute,
                this.OnParamChanged);
            this.rightChMute = new BoolParam(
                this.moduleIndex,
                (byte)VXCommProtocol.OutputParamAddr.rightChMute,
                this.OnParamChanged);

            this.eqIsPassParam = new BoolParam(this.moduleIndex, (byte)VXCommProtocol.OutputParamAddr.eqPass, this.OnParamChanged);

            this._eqGroup.BindingBypassParam(this.eqIsPassParam);
        }

        public void CopyFrom(OutputModule other)
        {
            base.CopyFrom(other);
            this.leftChDelay.initV = other.leftChDelay.v;
            this.rightChDelay.initV = other.rightChDelay.v;
            this.leftChMute.initV = other.leftChMute.v;
            this.rightChMute.initV = other.rightChMute.v;
        }
    }

    class MainOutputModule : OutputModule
    {
        public static readonly double[] initFreqList = new double[] { 80.3, 191.0, 463.0, 1100.0, 2620.0, 6350.0, 15100.0 };

        public MainOutputModule(EventBus effectorEvent_, MusicModule music_) :
            base(OutputChannelType.MainOutput, VXCommProtocol.mainOutputModuleIndex, effectorEvent_, new EQGroup(initFreqList), music_)
        {
            //_themeColor = Color.FromArgb(55, 150, 0);
            _themeColor = Color.FromArgb(72, 208, 7);
        }
    }

    class SurOutputModule : OutputModule
    {
        public static readonly double[] initFreqList = new double[] { 80.3, 297.0, 1100.0, 4080.0, 15100.0 };

        public SurOutputModule(EventBus effectorEvent_, MusicModule music_) :
            base(OutputChannelType.SurOutput, VXCommProtocol.surOutputModuleIndex, effectorEvent_, new EQGroup(initFreqList), music_: music_)
        {
            //_themeColor = Color.FromArgb(107, 11, 137);
            _themeColor = Color.FromArgb(72, 208, 7);
        }
    }
}
