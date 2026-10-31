using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Core.DSP;
using AEC.Common;
using AEC.Comm.VKxSCMComm;

namespace AEC.Effectors.VKxSCM.Modules
{
    public class MusicModule : MusicModuleBase
    {
        //public static readonly double[] initFreqList = new double[] { 46.8, 111.0, 265.0, 642.0, 1530.0, 3630.0, 8640.0 };
        public static readonly double[] initFreqList = new double[] { 62.5, 125.0, 250.0, 500.0, 1000.0, 2000.0, 4000.0 };
        //public static readonly double[] initFreqList = new double[] { 31.3, 62.5, 125.0, 250.0, 500.0, 1000.0, 2000.0, 4000.0, 8000.0 };

        public const double HPFMaxFreq = 303;
        public const double HPFInitFreq = 50.6;

        public readonly AutoWorkMode autoWorkMode = null;

        public readonly HighPassFilter singHPF = null;
        public readonly HighPassFilter discoHPF = null;

        private readonly MusicParam singHpfFreqParam;
        private readonly MusicParam singHpfTypeParam;
        private readonly MusicParam discoHpfFreqParam;
        private readonly MusicParam discoHpfTypeParam;
        private readonly BoolParam singEQBypassParam;
        private readonly BoolParam discoEQBypassParam;

        public readonly EQGroup singEQGroup = null;
        public readonly EQGroup discoEQGroup = null;

        public readonly BassExciter bassExciter = null;

        public MusicModule(EventBus effectorEvent_) :
            base(VKxSCMCommProtocol.musicModuleIndex, effectorEvent_)
        {
            base._themeColor = Color.FromArgb(72, 208, 7);

            this.singHPF = XPFilterFactory.Create4thOrderHighPassFilter(HPFMaxFreq);// new HighPassFilter(maxHPFFreqIndex);
            this.discoHPF = XPFilterFactory.Create4thOrderHighPassFilter(HPFMaxFreq);

            this.singHPF.typeIndex = 2;
            this.discoHPF.typeIndex = 2;

            this.singEQGroup = new EQGroup(initFreqList);
            this.discoEQGroup = new EQGroup(initFreqList);

            this.autoWorkMode = new AutoWorkMode(VKxSCMCommProtocol.musicModuleIndex, (byte)VKxSCMCommProtocol.MusicParamAddr.workMode, this.OnParamChanged);

            this.bassExciter = new BassExciter(VKxSCMCommProtocol.musicModuleIndex, (byte)VKxSCMCommProtocol.MusicParamAddr.bassExciterOn, this.OnParamChanged);

            this.singHpfFreqParam = new MusicParam((byte)VKxSCMCommProtocol.MusicParamAddr.hpfFreq, this.singHPF.freqMaxIndex, OnParamChanged);
            this.singHpfTypeParam = new MusicParam((byte)VKxSCMCommProtocol.MusicParamAddr.hpfType, this.singHPF.typeMaxIndex, OnParamChanged);

            this.discoHpfFreqParam = new MusicParam((byte)VKxSCMCommProtocol.MusicParamAddr.hpfFreq, this.discoHPF.freqMaxIndex, OnParamChanged);
            this.discoHpfTypeParam = new MusicParam((byte)VKxSCMCommProtocol.MusicParamAddr.hpfType, this.discoHPF.typeMaxIndex, OnParamChanged);

            this.singEQBypassParam = new BoolParam(this.moduleIndex, (byte)VKxSCMCommProtocol.MusicParamAddr.eqPass, this.OnParamChanged);
            this.discoEQBypassParam = new BoolParam(this.moduleIndex, (byte)VKxSCMCommProtocol.MusicParamAddr.eqPass, this.OnParamChanged);

            this.singHPF.BindingParams(this.singHpfFreqParam, this.singHpfTypeParam);
            this.discoHPF.BindingParams(this.discoHpfFreqParam, this.discoHpfTypeParam);
            this.singEQGroup.BindingBypassParam(this.singEQBypassParam);
            this.discoEQGroup.BindingBypassParam(this.discoEQBypassParam);
        }

        public void CopyFrom(MusicModule other)
        {
            this.autoWorkMode.CopyFrom(other.autoWorkMode);
            this.bassExciter.CopyFrom(other.bassExciter);
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

        public override BassExciter getBassExciter
        {
            get
            {
                return this.bassExciter;
            }
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
            //this.wm.RandomTest();
            //this.ws.RandomTest();

            //if (workMode == WorkMode.Manual)
            //{
            //    this.eqGroup.RandomTest();
            //    base.UpdateEQGroup();

            //    if (this.workSelect == WorkSelect.Sing)
            //    {
            //        this.singHpfFreqParam.RandomTest();
            //        this.singHPF.freqIndex = this.singHpfFreqParam.v;
            //        this.singEQBypassParam.RandomTest();
            //        this.eqGroup.isBypass = this.singEQBypassParam.value;
            //    }
            //    else
            //    {
            //        this.discoHpfFreqParam.RandomTest();
            //        this.discoHPF.freqIndex = this.discoHpfFreqParam.v;
            //        this.discoEQBypassParam.RandomTest();
            //        this.eqGroup.isBypass = this.discoEQBypassParam.value;
            //    }
            //}
        }
#endif
    }
}
