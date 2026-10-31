using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.MXComm;

namespace AEC.Effectors.MX.Modules
{
    public class OutputModule : OutputBaseModule
    {
        private static readonly Color leftChColor = Color.FromArgb(255, 156, 0);
        private static readonly Color rightChColor = Color.FromArgb(231, 0, 0);
        
        public readonly ShortParam leftChDelay;
        public readonly ShortParam rightChDelay;
        public readonly BoolParam leftChMute;
        public readonly BoolParam rightChMute;

        public readonly ShortParam eqControlMode;

        public readonly EQGroup leftEQGroup = null;
        public readonly EQGroup rightEQGroup = null;

        public EQControlMode eqMode
        {
            get
            {
                return DataConverter.Short2EQControlMode(eqControlMode.v);
            }
            set
            {
                EQControlMode prev = this.eqMode;
                this.eqControlMode.v = (short)value;
                if(this.eqMode == EQControlMode.Linkage)
                {
                    this.rightEQGroup.CopyFrom(this.leftEQGroup, true);
                }
            }
        }

        public override Color themeColor
        {
            get
            {
                switch(this.eqMode)
                {
                    case EQControlMode.Linkage:
                        return this._themeColor;

                    case EQControlMode.Left:
                        return leftChColor;

                    case EQControlMode.Right:
                        return rightChColor;

                    default:
                        return this._themeColor;
                }
                
            }
        }

        protected override void UpdateEQ(EQ eq)
        {
            base.UpdateEQ(eq);
            if(this.eqMode == EQControlMode.Linkage)
            {
                this.rightEQGroup.currEQIndex = eq.index;
                this.rightEQGroup.currEQ.CopyFrom(eq, false);
                this.rightEQGroup.UpdateEQFreqResp(this.rightEQGroup.currEQ);
            }
        }

        public override void ResetEQGroup()
        {
            base.ResetEQGroup();
            if (this.eqMode == EQControlMode.Linkage)
            {
                this.rightEQGroup.Reset();
            }
        }

        //public override short currEQIndex
        //{
        //    get
        //    {
        //        return base.currEQIndex;
        //    }
        //    set
        //    {
        //        base.currEQIndex = value;
        //        if (this.eqMode == EQControlMode.Linkage)
        //        {
        //            this.rightEQGroup.currEQIndex = base.currEQIndex;
        //        }
        //    }
        //}

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

        public OutputModule(OutputChannelType outputChannel, byte index, EventBus effectorEvent_, MusicModule music_) :
            base(outputChannel, index, effectorEvent_, music_)
        {
            this.leftChDelay = new ShortParam(
                this.moduleIndex, 
                (byte)MXCommProtocol.OutputParamAddr.leftChDelay,
                MaxDelay,
                this.OnParamChanged);
            this.rightChDelay = new ShortParam(
                this.moduleIndex,
                (byte)MXCommProtocol.OutputParamAddr.rightChDelay,
                MaxDelay,
                this.OnParamChanged);
            this.leftChMute = new BoolParam(
                this.moduleIndex,
                (byte)MXCommProtocol.OutputParamAddr.leftChMute,
                this.OnParamChanged);
            this.rightChMute = new BoolParam(
                this.moduleIndex,
                (byte)MXCommProtocol.OutputParamAddr.rightChMute,
                this.OnParamChanged);

            this.eqControlMode = new ShortParam(this.moduleIndex, (byte)MXCommProtocol.OutputParamAddr.eqControlMode, (short)EQControlMode.Right, this.OnParamChanged);

            this.eqBypassParam = new BoolParam(this.moduleIndex, (byte)MXCommProtocol.OutputParamAddr.eqPass, this.OnParamChanged);

            this.leftEQGroup = new EQGroup(OutputBaseModule.initFreqList);
            this.rightEQGroup = new EQGroup(OutputBaseModule.initFreqList);
            this.leftEQGroup.BindingBypassParam(eqBypassParam);
            this.rightEQGroup.BindingBypassParam(eqBypassParam);

            this.eqBypassParam.AddListener(OnBypassChanged);
        }

        private void OnBypassChanged(object sender, DataParamChangedArgs e)
        {
            if(sender.Equals(this.eqBypassParam) && this.eqMode == EQControlMode.Linkage)
            {
                this.rightEQGroup.isBypass = this.leftEQGroup.isBypass;
            }
        }

        public void CopyFrom(OutputModule other)
        {
            base.CopyFrom(other);
            this.leftChDelay.initV = other.leftChDelay.v;
            this.rightChDelay.initV = other.rightChDelay.v;
            this.leftChMute.initV = other.leftChMute.v;
            this.rightChMute.initV = other.rightChMute.v;
            this.eqControlMode.initV = other.eqControlMode.v;
            this.leftEQGroup.CopyFrom(other.leftEQGroup);
            this.rightEQGroup.CopyFrom(other.rightEQGroup);
        }

        public override EQGroup eqGroup
        {
            get
            {
                return this.eqMode != EQControlMode.Right ? this.leftEQGroup : this.rightEQGroup;
            }
        }

        public bool isMute
        {
            get
            {
                return this.leftChMute.value || this.rightChMute.value;
            }
            set
            {
                if(this.leftChMute.value = value)
                {
                    this.leftChMute.value = value;
                }
                if(this.rightChMute.value = value)
                {
                    this.rightChMute.value = value;
                }
            }
        }

        protected override void InitOutputEQGroupFreqResp()
        {
            this.leftEQGroup.InitFreqResp();
            this.rightEQGroup.InitFreqResp();
        }

        public void ConstraintParam()
        {
            if(this.eqMode == EQControlMode.Linkage)
            {
                this.rightEQGroup.CopyFrom(this.leftEQGroup, false);
            }
        }

        public override void ResetData()
        {
            this.leftEQGroup.ResetData();
            this.rightEQGroup.ResetData();
            this.leftChDelay.initV = 0;
            this.rightChDelay.initV = 0;
            this.leftChMute.initV = 0;
            this.rightChMute.initV = 0;

            base.ResetData();
        }
    }

    class MainOutputModule : OutputModule
    {
        public MainOutputModule(EventBus effectorEvent_, MusicModule music_) :
            base(OutputChannelType.MainOutput, MXCommProtocol.mainOutputModuleIndex, effectorEvent_, music_: music_)
        {
            _themeColor = Color.FromArgb(72, 208, 7);
        }
    }

    class SurOutputModule : OutputModule
    {
        public SurOutputModule(EventBus effectorEvent_, MusicModule music_) :
            base(OutputChannelType.SurOutput, MXCommProtocol.surOutputModuleIndex, effectorEvent_, music_: music_)
        {
            _themeColor = Color.FromArgb(189, 33, 237);
        }
    }
}
