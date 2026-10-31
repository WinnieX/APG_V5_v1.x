using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Core.DSP;
using AEC.Common;
using AEC.Comm.MXComm;

namespace AEC.Effectors.MX.Modules
{
    public class MusicModule : MusicModuleBase
    {
        //public static readonly double[] initFreqList = new double[] { 46.8, 111.0, 265.0, 642.0, 1530.0, 3630.0, 8640.0 };
        public static readonly double[] initFreqList = new double[] { 80.3, 154.0, 297.0, 572.0, 1100.0, 2120.0, 4080.0, 7850.0, 15100.0 };

        public readonly MusicParam wm = null;
        public readonly MusicParam ws = null;

        private readonly BoolParam singEQBypassParam;
        private readonly BoolParam discoEQBypassParam;

        public readonly EQGroup singEQGroup = null;
        public readonly EQGroup discoEQGroup = null;

        public MusicModule(EventBus effectorEvent_):
            base(MXCommProtocol.musicModuleIndex, effectorEvent_)
        {
            base._themeColor = Color.FromArgb(211, 14, 14);

            this.singEQGroup = new EQGroup(initFreqList);
            this.discoEQGroup = new EQGroup(initFreqList);

            this.wm = new MusicParam((byte)MXCommProtocol.MusicParamAddr.workMode, (short)WorkMode.Manual, OnParamChanged);
            this.ws = new MusicParam((byte)MXCommProtocol.MusicParamAddr.workSelect, (short)WorkSelect.Disco, OnParamChanged);
            this.singEQBypassParam = new BoolParam(this.moduleIndex, (byte)MXCommProtocol.MusicParamAddr.eqPass, this.OnParamChanged);
            this.discoEQBypassParam = new BoolParam(this.moduleIndex, (byte)MXCommProtocol.MusicParamAddr.eqPass, this.OnParamChanged);
            this.workModeInit = (short)WorkMode.Manual;
            this.workSelectInit = (short)WorkSelect.Sing;

            this.singEQGroup.BindingBypassParam(singEQBypassParam);
            this.discoEQGroup.BindingBypassParam(discoEQBypassParam);
        }

        public void CopyFrom(MusicModule other)
        {
            //this._workMode = other._workMode;
            //this._workSelect = other._workSelect;
            this.wm.initV = other.wm.v;
            this.ws.initV = other.ws.v;
            this.singEQGroup.CopyFrom(other.singEQGroup);
            this.discoEQGroup.CopyFrom(other.discoEQGroup);
        }

        public override void InitFreqResp()
        {
            base.InitFreqResp();

            this.singEQGroup.InitFreqResp();
            this.discoEQGroup.InitFreqResp();

            this.UpdateFreqResp();
        }

        public override void UpdateFreqResp()
        {
            base.UpdateFreqResp();

            EQGroupFreqResp(this.freqResp, eqGroup);
        }

        public override EQGroup eqGroup
        {
            get
            {
                return this.workSelect == WorkSelect.Sing ? this.singEQGroup : this.discoEQGroup;
            }
        }

        public override bool eqCanBeChanged
        {
            get
            {
                return !eqGroup.isBypass && workMode == WorkMode.Manual;
                //return !eqGroup.isPass;
            }
        }

        public bool isManual
        {
            get
            {
                return this.workMode == WorkMode.Manual;
            }
        }


        public override WorkMode workMode
        {
            get
            {
                return DataConverter.Short2WorkMode(this.wm.v);
            }
            set
            {
                this.wm.v = (short)value;
            }
        }

        public override WorkSelect workSelect
        {
            get
            {
                return DataConverter.Short2WorkSelect(this.ws.v);
            }
            set
            {
                this.ws.v = (short)value;
            }
        }

        public short workModeInit
        {
            set
            {
                this.wm.initV = value;
            }
        }

        public short workSelectInit
        {
            set
            {
                this.ws.initV = value;
            }
        }

        public override void ResetData()
        {
            workModeInit = (short)WorkMode.Manual;
            workSelectInit = (short)WorkSelect.Sing;

            singEQGroup.ResetData();
            discoEQGroup.ResetData();
        }
    }
}
