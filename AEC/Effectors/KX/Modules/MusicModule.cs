using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Core.DSP;
using AEC.Common;
using AEC.Comm.KXComm;

namespace AEC.Effectors.KX.Modules
{
    public class MusicModule : MusicModuleBase
    {
        //public static readonly double[] initFreqList = new double[] { 46.8, 111.0, 265.0, 642.0, 1530.0, 3630.0, 8640.0 };
        public static readonly double[] initFreqList = new double[] { 125.0, 250.0, 500.0, 1000.0, 2000.0, 4000.0, 8000.0 };

        public const double HPFMaxFreq = 303;
        public const double HPFInitFreq = 50.6;

        public readonly AutoWorkMode autoWorkMode = null;

        public readonly HighPassFilter singHPF = null;
        public readonly HighPassFilter discoHPF = null;
        private readonly MusicParam singHpfFreqParam;
        private readonly MusicParam discoHpfFreqParam;
        private readonly BoolParam singEQBypassParam;
        private readonly BoolParam discoEQBypassParam;

        public readonly EQGroup singEQGroup = null;
        public readonly EQGroup discoEQGroup = null;

        public MusicModule(EventBus effectorEvent_) :
            base(KXCommProtocol.musicModuleIndex, effectorEvent_)
        {
            base._themeColor = Color.FromArgb(211, 14, 14);

            this.singHPF = XPFilterFactory.CreateImmutableTypeHighPassFilter(HPFMaxFreq, HPFInitFreq, FilterType.Butterworth12dB);// new HighPassFilter(maxHPFFreqIndex);
            this.discoHPF = XPFilterFactory.CreateImmutableTypeHighPassFilter(HPFMaxFreq, HPFInitFreq, FilterType.Butterworth12dB);
            this.singEQGroup = new EQGroup(initFreqList);
            this.discoEQGroup = new EQGroup(initFreqList);

            this.autoWorkMode = new AutoWorkMode(KXCommProtocol.musicModuleIndex, (byte)KXCommProtocol.MusicParamAddr.workMode, this.OnParamChanged);

            this.singHpfFreqParam = new MusicParam((byte)KXCommProtocol.MusicParamAddr.hpfFreq, this.singHPF.freqMaxIndex, OnParamChanged);
            this.discoHpfFreqParam = new MusicParam((byte)KXCommProtocol.MusicParamAddr.hpfFreq, this.discoHPF.freqMaxIndex, OnParamChanged);
            this.singEQBypassParam = new BoolParam(this.moduleIndex, (byte)KXCommProtocol.MusicParamAddr.eqPass, this.OnParamChanged);
            this.discoEQBypassParam = new BoolParam(this.moduleIndex, (byte)KXCommProtocol.MusicParamAddr.eqPass, this.OnParamChanged);

            this.singHPF.BindingParams(this.singHpfFreqParam);
            this.discoHPF.BindingParams(this.discoHpfFreqParam);
            this.singEQGroup.BindingBypassParam(this.singEQBypassParam);
            this.discoEQGroup.BindingBypassParam(this.discoEQBypassParam);
        }

        public void CopyFrom(MusicModule other)
        {
            this.autoWorkMode.CopyFrom(other.autoWorkMode);
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
                return DataConverter.Short2WorkMode(this.autoWorkMode.wm.v);
            }
            set
            {
                this.autoWorkMode.wm.v = (short)value;
            }
        }

        public override WorkSelect workSelect
        {
            get
            {
                return DataConverter.Short2WorkSelect(this.autoWorkMode.ws.v);
            }
            set
            {
                this.autoWorkMode.ws.v = (short)value;
            }
        }

        public override void ResetData()
        {
            this.autoWorkMode.Reset();
            singHPF.Reset();
            discoHPF.Reset();

            singEQGroup.ResetData();
            discoEQGroup.ResetData();
        }

#if COMM_TEST
        public override void RandomTest()
        {
            this.autoWorkMode.workMode = WorkMode.Manual;
            this.autoWorkMode.workSelect = WorkSelect.Sing;

            if (workMode == WorkMode.Manual)
            {
                this.eqGroup.RandomTest();
                base.UpdateEQGroup();

                if (this.workSelect == WorkSelect.Sing)
                {
                    this.singHpfFreqParam.RandomTest();
                    this.singHPF.freqIndex = this.singHpfFreqParam.v;
                    this.singEQBypassParam.RandomTest();
                    this.eqGroup.isBypass = this.singEQBypassParam.value;
                }
                else
                {
                    this.discoHpfFreqParam.RandomTest();
                    this.discoHPF.freqIndex = this.discoHpfFreqParam.v;
                    this.discoEQBypassParam.RandomTest();
                    this.eqGroup.isBypass = this.discoEQBypassParam.value;
                }
            }
        }
#endif
    }
}
