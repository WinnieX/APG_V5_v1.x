using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Core.DSP;
using AEC.Common;
using AEC.Comm.VKxSComm;

namespace AEC.Effectors.VKxS.Modules
{
    public class MusicModule : ModuleBase
    {
        //public static readonly double[] initFreqList = new double[] { 46.8, 111.0, 265.0, 642.0, 1530.0, 3630.0, 8640.0 };
        public static readonly double[] initFreqList = new double[] { 125.0, 250.0, 500.0, 1000.0, 2000.0, 4000.0, 8000.0 };

        public const double HPFMaxFreq = 303;
        public const double HPFInitFreq = 50.6;

        public readonly AutoWorkMode autoWorkMode = null;

        public readonly HighPassFilter highPassFilter;
        public readonly MusicParam hpfFreqParam;
        public readonly MusicParam hpfTypeParam;

        public readonly EQGroup _eqGroup = null;

        private readonly BoolParam eqBypassParam;

        public MusicModule(EventBus effectorEvent_) :
            base(VKxSCommProtocol.musicModuleIndex, effectorEvent_)
        {
            base._themeColor = Color.FromArgb(211, 14, 14);

            this.autoWorkMode = new AutoWorkMode(VKxSCommProtocol.musicModuleIndex, (byte)VKxSCommProtocol.MusicParamAddr.workMode, this.OnParamChanged);

            this.highPassFilter = XPFilterFactory.CreateX5HighPassFilter(HPFMaxFreq);
            this.highPassFilter.SetGetTypeAsOrderLabel();

            this.hpfFreqParam = new MusicParam((byte)VKxSCommProtocol.MusicParamAddr.hpfFreq, this.highPassFilter.freqMaxIndex, this.OnParamChanged);
            this.hpfTypeParam = new MusicParam((byte)VKxSCommProtocol.MusicParamAddr.hpfType, this.highPassFilter.typeMaxIndex, this.OnParamChanged);

            this.highPassFilter.BindingParams(this.hpfFreqParam, this.hpfTypeParam);

            this._eqGroup = new EQGroup(initFreqList);

            this.eqBypassParam = new BoolParam(this.moduleIndex, (byte)VKxSCommProtocol.MusicParamAddr.eqPass, this.OnParamChanged);

            this._eqGroup.BindingBypassParam(eqBypassParam);
        }

        public void CopyFrom(MusicModule other)
        {
            this.autoWorkMode.CopyFrom(other.autoWorkMode);
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

        public override HighPassFilter hpf
        {
            get
            {
                return this.highPassFilter;
            }
        }

        public override EQGroup eqGroup
        {
            get
            {
                return this._eqGroup;
            }
        }

        public override bool eqCanBeChanged
        {
            get
            {
                return !this._eqGroup.isBypass;
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
            this.autoWorkMode.workMode = WorkMode.Manual;
            this.autoWorkMode.workSelect = WorkSelect.Sing;

            if (this.autoWorkMode.workMode == WorkMode.Manual)
            {
                this.eqGroup.RandomTest();
                base.UpdateEQGroup();

                this.hpfFreqParam.RandomTest();
                this.hpfTypeParam.RandomTest();
                this.highPassFilter.freqIndex = this.hpfFreqParam.v;
                this.eqBypassParam.RandomTest();
                this.eqGroup.isBypass = this.eqBypassParam.value;
            }
        }
#endif
    }
}
