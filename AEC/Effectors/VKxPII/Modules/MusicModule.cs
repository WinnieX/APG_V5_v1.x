using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Core.DSP;
using AEC.Common;
using AEC.Comm.VKxPIIComm;

namespace AEC.Effectors.VKxPII.Modules
{
    public class MusicModule : MusicModuleBase
    {
        //public static readonly double[] initFreqList = new double[] { 46.8, 111.0, 265.0, 642.0, 1530.0, 3630.0, 8640.0 };
        //public static readonly double[] initFreqList = new double[] { 31.3, 62.5, 125.0, 250.0, 500.0, 1000.0, 2000.0, 4000.0, 8000.0 };
        public static readonly double[] initFreqList = new double[] { 62.5, 125.0, 250.0, 500.0, 1000.0, 2000.0, 4000.0 };

        public const double HPFMaxFreq = 303;
        public const double HPFInitFreq = 50.6;

        public readonly AutoWorkMode autoWorkMode = null;

        public readonly HighPassFilter highPassFilter;
        public readonly MusicParam hpfFreqParam;
        public readonly MusicParam hpfTypeParam;

        private readonly BoolParam eqBypassParam;

        public readonly EQGroup _eqGroup = null;

        public readonly BassExciter bassExciter = null;

        public MusicModule(EventBus effectorEvent_) :
            base(VKxPIICommProtocol.musicModuleIndex, effectorEvent_)
        {
            base._themeColor = Color.FromArgb(211, 14, 14);

            this.autoWorkMode = new AutoWorkMode(VKxPIICommProtocol.musicModuleIndex, (byte)VKxPIICommProtocol.MusicParamAddr.workMode, this.OnParamChanged);

            this.highPassFilter = XPFilterFactory.CreateX5HighPassFilter(HPFMaxFreq);
            this.highPassFilter.SetGetTypeAsOrderLabel();

            this.bassExciter = new BassExciter(VKxPIICommProtocol.musicModuleIndex, (byte)VKxPIICommProtocol.MusicParamAddr.bassExciterOn, this.OnParamChanged);

            this.hpfFreqParam = new MusicParam((byte)VKxPIICommProtocol.MusicParamAddr.hpfFreq, this.highPassFilter.freqMaxIndex, this.OnParamChanged);
            this.hpfTypeParam = new MusicParam((byte)VKxPIICommProtocol.MusicParamAddr.hpfType, this.highPassFilter.typeMaxIndex, this.OnParamChanged);

            this.eqBypassParam = new BoolParam(this.moduleIndex, (byte)VKxPIICommProtocol.MusicParamAddr.eqPass, this.OnParamChanged);

            this.highPassFilter.BindingParams(this.hpfFreqParam, this.hpfTypeParam);

            this._eqGroup = new EQGroup(initFreqList);

            this._eqGroup.BindingBypassParam(eqBypassParam);
        }

        public void CopyFrom(MusicModule other)
        {
            this.autoWorkMode.CopyFrom(other.autoWorkMode);
            this.bassExciter.CopyFrom(other.bassExciter);
            this.highPassFilter.CopyFrom(other.highPassFilter);
            this._eqGroup.CopyFrom(other._eqGroup);
        }

        public void CopyFrom(AEC.Effectors.X5.Modules.MusicModule other)
        {
            this.autoWorkMode.CopyFrom(other.autoWorkMode);
            this.highPassFilter.CopyFrom(other.highPassFilter);
            this._eqGroup.CopyFrom(other._eqGroup);
        }

        public override void InitFreqResp()
        {
            base.InitFreqResp();

            this._eqGroup.InitFreqResp();
            this.highPassFilter.UpdateFreqResp();

            this.UpdateFreqResp();
        }

        public override void UpdateFreqResp()
        {
            base.UpdateFreqResp();

            EQGroupAddFilterFreqResp(this.freqResp, this._eqGroup, this.highPassFilter);
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
                return this._eqGroup;
            }
        }

        public override HighPassFilter hpf
        {
            get
            {
                return this.highPassFilter;
            }
        }

        public override bool eqCanBeChanged
        {
            get
            {
                return !this._eqGroup.isBypass;
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
            this.highPassFilter.Reset();
            this._eqGroup.ResetData();
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
