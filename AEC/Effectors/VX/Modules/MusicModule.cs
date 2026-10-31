using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Core.DSP;
using AEC.Common;
using AEC.Comm.VXComm;

namespace AEC.Effectors.VX.Modules
{
    public class MusicModule : MusicModuleBase
    {
        //public static readonly double[] initFreqList = new double[] { 46.8, 111.0, 265.0, 642.0, 1530.0, 3630.0, 8640.0 };
        public static readonly double[] initFreqList = new double[] { 80.3, 191.0, 463.0, 1100.0, 2620.0, 6350.0, 15100.0 };

        public const double HPFMaxFreq = 303;
        public const double HPFInitFreq = 50.6;
        
        public readonly MusicParam wm = null;
        public readonly MusicParam ws = null;

        public readonly HighPassFilter singHPF = null;
        public readonly HighPassFilter discoHPF = null;
        private readonly MusicParam singHpfFreqParam;
        private readonly MusicParam discoHpfFreqParam;
        private readonly BoolParam singEQBypassParam;
        private readonly BoolParam discoEQBypassParam;

        public readonly EQGroup singEQGroup = null;
        public readonly EQGroup discoEQGroup = null;

        public MusicModule(EventBus effectorEvent_) :
            base(VXCommProtocol.musicModuleIndex, effectorEvent_)
        {
            base._themeColor = Color.FromArgb(211, 14, 14);

            this.singHPF = XPFilterFactory.CreateImmutableTypeHighPassFilter(HPFMaxFreq, HPFInitFreq, FilterType.Butterworth12dB);// new HighPassFilter(maxHPFFreqIndex);
            this.discoHPF = XPFilterFactory.CreateImmutableTypeHighPassFilter(HPFMaxFreq, HPFInitFreq, FilterType.Butterworth12dB);
            this.singEQGroup = new EQGroup(initFreqList);
            this.discoEQGroup = new EQGroup(initFreqList);

            this.wm = new MusicParam((byte)VXCommProtocol.MusicParamAddr.workMode, (short)WorkMode.Manual, OnParamChanged);
            this.ws = new MusicParam((byte)VXCommProtocol.MusicParamAddr.workSelect, (short)WorkSelect.Disco, OnParamChanged);
            this.singHpfFreqParam = new MusicParam((byte)VXCommProtocol.MusicParamAddr.hpfFreq, this.singHPF.freqMaxIndex, OnParamChanged);
            this.discoHpfFreqParam = new MusicParam((byte)VXCommProtocol.MusicParamAddr.hpfFreq, this.discoHPF.freqMaxIndex, OnParamChanged);
            this.singEQBypassParam = new BoolParam(this.moduleIndex, (byte)VXCommProtocol.MusicParamAddr.eqPass, this.OnParamChanged);
            this.discoEQBypassParam = new BoolParam(this.moduleIndex, (byte)VXCommProtocol.MusicParamAddr.eqPass, this.OnParamChanged);
            workModeInit = (short)WorkMode.Manual;
            workSelectInit = (short)WorkSelect.Sing;

            this.singHPF.BindingParams(this.singHpfFreqParam);
            this.discoHPF.BindingParams(this.discoHpfFreqParam);
            this.singEQGroup.BindingBypassParam(this.singEQBypassParam);
            this.discoEQGroup.BindingBypassParam(this.discoEQBypassParam);
        }

        public void CopyFrom(MusicModule other)
        {
            //this._workMode = other._workMode;
            //this._workSelect = other._workSelect;
            this.wm.initV = other.wm.v;
            this.ws.initV = other.ws.v;
            this.singHPF.CopyFrom(other.singHPF);
            this.discoHPF.CopyFrom(other.discoHPF);            
            this.singEQGroup.CopyFrom(other.singEQGroup);
            this.discoEQGroup.CopyFrom(other.discoEQGroup);
            this.dataIsPrivateParam = other.dataIsPrivateParam;
        }

        public override void InitFreqResp()
        {
            base.InitFreqResp();

            this.singEQGroup.InitFreqResp();
            this.discoEQGroup.InitFreqResp();
            this.singHPF.UpdateFreqResp();
            this.discoHPF.UpdateFreqResp();

            this.UpdateFreqResp();
        }

        public override void UpdateFreqResp()
        {
            base.UpdateFreqResp();

            EQGroupAddFilterFreqResp(this.freqResp, eqGroup, hpf);
        }

        public override EQGroup eqGroup
        {
            get
            {
                return this.workSelect == WorkSelect.Sing ? this.singEQGroup : this.discoEQGroup;
            }
        }

        public override HighPassFilter hpf
        {
            get
            {
                return this.workSelect == WorkSelect.Sing ? this.singHPF : this.discoHPF;
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

        public override bool hpfFreqCanBeChanged
        {
            get
            {
                return this.isManual;
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
            singHPF.Reset();
            discoHPF.Reset();

            singEQGroup.ResetData();
            discoEQGroup.ResetData();
        }
    }
}
