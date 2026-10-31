using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AEC.Core;
using AEC.Effectors;
using AEC.Effectors.X5;
using AEC.Effectors.X5.Modules;
using AEC.Misc;
using AEC.UI.Common;

namespace AEC.UI.X5
{
    public partial class X5SubOutputPanel : X5OutputBasePanel
    {
        //public const string DelayUnit = "ms";
        private SubOutputModule output;
        //private Image _sliderNormalBack;

        //protected virtual Image mixerSliderBack
        //{
        //    set
        //    {
        //        this.mixerPanel.backgroundNormal = value;
        //    }
        //}

        //protected virtual Image sliderNormalBack
        //{ 
        //    get
        //    {
        //        return _sliderNormalBack;
        //    }
        //    set
        //    {
        //        this._sliderNormalBack = value;
        //        this.limiterPanel.sliderNormalBack = value;
        //        this.sliderHpfFreq.backgroundNormal = value;
        //        this.sliderLpfFreq.backgroundNormal = value;
        //    }
        //}

        public X5SubOutputPanel()
            : base()
        {
            InitializeComponent();
            this.InitControl(this.eqGroupPanel, this.sliderHpfFreq, this.sliderLpfFreq);
            //this.cmbHpfMode.DrawMode = DrawMode.OwnerDrawFixed;

            this.eqResetAndByPassPanel.ResetEQ += base.ResetEQGroup;
            this.eqResetAndByPassPanel.EQPassChanged += base.SwitchEQByPass;

            this.sliderHpfQ.Actived += FilterQ_Actived;
            this.sliderHpfQ.ValueChanged += FilterQ_ValueChanged;
            this.sliderLpfQ.Actived += FilterQ_Actived;
            this.sliderLpfQ.ValueChanged += FilterQ_ValueChanged;

            this.chkMute.CheckedChanged += param_ValueChanged;
        }

        private void FilterQ_Actived(object sender, EventArgs e)
        {
            if (this.output == null)
            {
                return;
            }

            if (sender.Equals(this.sliderHpfQ))
            {
                if (this.output.activeItem != ActiveItem.HPF)
                {
                    this.output.activeItem = ActiveItem.HPF;
                    this.output.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
                }
            }
            else if (sender.Equals(this.sliderLpfQ))
            {
                if (this.output.activeItem != ActiveItem.LPF)
                {
                    this.output.activeItem = ActiveItem.LPF;
                    this.output.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
                }
            }
        }

        private void FilterQ_ValueChanged(object sender, EventArgs e)
        {
            if (this.output == null)
            {
                return;
            }

            if (sender.Equals(this.sliderHpfQ))
            {
                output.hpf.UpdateQIndexFromParam();
                output.UpdateXPFromControlPanel(XPFilterType.HighPass, FilterElement.Q);
            }
            else if (sender.Equals(this.sliderLpfQ))
            {
                output.lpf.UpdateQIndexFromParam();
                output.UpdateXPFromControlPanel(XPFilterType.LowPass, FilterElement.Q);
            }

            //this.outputBase.eventBus.NotifyObeserver(new EQGraphicNeedToBeUpdatedArgs());
        }

        private void param_ValueChanged(object sender, EventArgs e)
        {
            if (sender.Equals(this.chkMute))
            {
                this.output.monoChannel.mute.value = this.chkMute.isChecked;
                this._eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.OutputPanel, OutputParam.Mute));
                this._eventBus.NotifyObeserver(new OutputMuteChangedFromPanelArgs(output.outputChannelType));
            }
        }

        protected virtual void InitFont()
        {
            //this.labelHpfFreq.Font = FontHelper.CreateFont10();
            //this.labelHpfMode.Font = FontHelper.CreateFont10();
            //this.cmbHpfMode.Font = FontHelper.CreateLabelFont10();
            //this.labelLpfFreq.Font = FontHelper.CreateFont10();
            //this.labelLpfMode.Font = FontHelper.CreateFont10();
            //this.cmbLpfMode.Font = FontHelper.CreateLabelFont10();
        }

        //public virtual void BindingEffector(Effector effector_)
        //{
        //    //Debug.Assert()
        //    //if (this.baseModule != null)
        //    //{
        //    //    this.baseModule.eventBus.RemoveHandler(HPFChangedFromGraphicHandler);
        //    //}
        //    //this.music = effector_.music;
        //    //this.baseModule = this.music;
        //    //base.BindingEffector(effector_);
        //    //this.
        //}

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.x5EffectorModel != null);

            this.BindingOutputModule(base.mController.x5EffectorModel.subOutput);
        }

        protected void BindingOutputModule(SubOutputModule output_)
        {
            this.output = output_;
            base.BindingModule(this.output, MEffectorFamily.X5);
            this.mixerPanel.Binding(this.output, this._eventBus);
            this.limiterPanel.Binding(this.output.limiter, this._eventBus, base.mController.mEffectorThreshold);

            this.sliderHpfQ.Binding(this.output.hpf.qParam, GetFilterQLabel, CalcFilterQIndexFromText);
            this.sliderLpfQ.Binding(this.output.lpf.qParam, GetFilterQLabel, CalcFilterQIndexFromText);

            this.cmbHpfType.SelectedIndex = this.output.hpf.typeIndex;
            this.cmbLpfType.SelectedIndex = this.output.lpf.typeIndex;

            this.sliderDelay.Binding(this.output.monoChannel.delay, channelDelayParamLabelProvider.GetDelayLabel, channelDelayParamLabelProvider.CalcDelayIndexFromText);

            this.chkMute.isChecked = this.output.monoChannel.mute.value;

            this.eqResetAndByPassPanel.Init(this.output.eqGroup.isBypass, this.output.themeColor);
            this.eqResetAndByPassPanel.Enabled = this.output.autoWorkMode.isManual;

            this.workModePanel.Init(this.output.autoWorkMode, this._eventBus, this.output.themeColor);

            this.WrokModeChanged();

            this.RegisterCmbSelectedIndexChangedHandler();
        }

        protected override void OnOutputMuteChangedFromNavigatorHandler(OutputChannelType channel)
        {
            if (channel == output.outputChannelType)
            {
                this.chkMute.isChecked = this.output.monoChannel.mute.value;
            }
        }

        protected override void OnChannelDelayUintChangedHandler()
        {
            this.sliderDelay.UpdateUIFromData();
        }

        public override void UpdateUIFromData()
        {
            if (this.eqPanel != null)
            {
                this.eqPanel.InitEqAdjusterList();
            }

            this.mixerPanel.UpdateUIFromData();
            this.limiterPanel.UpdateUIFromData();

            this.sliderHpfFreq.Binding(this.output.hpf.freqParam, GetHpfFreqLabel, CalcHPFFreqIndexFromText);
            this.sliderLpfFreq.Binding(this.output.lpf.freqParam, GetLpfFreqLabel, CalcLPFFreqIndexFromText);

            this.sliderHpfQ.Binding(this.output.hpf.qParam, GetFilterQLabel, CalcFilterQIndexFromText);
            this.sliderLpfQ.Binding(this.output.lpf.qParam, GetFilterQLabel, CalcFilterQIndexFromText);
            
            this.RemoveCmbSelectedIndexChangedHandler();
            this.cmbHpfType.SelectedIndex = this.output.hpf.typeIndex;
            this.cmbLpfType.SelectedIndex = this.output.lpf.typeIndex;
            this.RegisterCmbSelectedIndexChangedHandler();

            this.sliderHpfFreq.Enabled = this.output.autoWorkMode.isManual;
            this.sliderHpfQ.Enabled = this.output.autoWorkMode.isManual && this.output.hpf.qCanBeChanged;
            this.cmbHpfType.Enabled = this.output.autoWorkMode.isManual;

            this.sliderLpfFreq.Enabled = this.output.autoWorkMode.isManual;
            this.sliderLpfQ.Enabled = this.output.autoWorkMode.isManual && this.output.lpf.qCanBeChanged;
            this.cmbLpfType.Enabled = this.output.autoWorkMode.isManual;

            this.sliderDelay.UpdateUIFromData();

            this.chkMute.isChecked = this.output.monoChannel.mute.value;

            //Log.OutputLine("X5 Sub Output Panel：{0}", this.output.monoChannel.mute.value);

            this.eqResetAndByPassPanel.isPass = this.output.eqGroup.isBypass;
            //this.mixerPanel.Enabled = this.outputBase.music.isManual;
            this.workModePanel.UpdateUIFromData();

            this.WrokModeChanged();
        }

        private void RegisterCmbSelectedIndexChangedHandler()
        {
            this.cmbHpfType.SelectedIndexChanged += cmbFilterType_SelectedIndexChanged;
            this.cmbLpfType.SelectedIndexChanged += cmbFilterType_SelectedIndexChanged;
        }

        private void RemoveCmbSelectedIndexChangedHandler()
        {
            this.cmbHpfType.SelectedIndexChanged -= cmbFilterType_SelectedIndexChanged;
            this.cmbLpfType.SelectedIndexChanged -= cmbFilterType_SelectedIndexChanged;
        }

        private void cmbFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            gtComboBox cmb = sender as gtComboBox;
            if (sender.Equals(this.cmbHpfType))
            {
                //this.outputBase.hpf.UpdateTypeParam() = (short)cmb.SelectedIndex;
                this.output.hpf.typeIndexParam = (short)cmb.SelectedIndex;
                this.output.UpdateXPFromControlPanel(XPFilterType.HighPass, FilterElement.Type);
                this.sliderHpfQ.Enabled = this.output.hpf.qCanBeChanged;
                //this.sliderHpfQ.Invalidate();
            }
            else if (sender.Equals(this.cmbLpfType))
            {
                this.output.lpf.typeIndexParam = (short)cmb.SelectedIndex;
                this.output.UpdateXPFromControlPanel(XPFilterType.LowPass, FilterElement.Type);
                this.sliderLpfQ.Enabled = this.output.lpf.qCanBeChanged;
                //this.sliderLpfQ.Invalidate();
            }
        }

        protected override void WrokModeChanged()
        {
            this.mixerPanel.Enabled = this.output.autoWorkMode.isManual;
            this.eqPanel.UpdateEqCanbeChanged();

            this.sliderHpfFreq.Enabled = this.output.autoWorkMode.isManual;
            this.sliderHpfQ.Enabled = this.output.autoWorkMode.isManual && this.output.hpf.qCanBeChanged;
            this.cmbHpfType.Enabled = this.output.autoWorkMode.isManual;

            this.sliderLpfFreq.Enabled = this.output.autoWorkMode.isManual;
            this.sliderLpfQ.Enabled = this.output.autoWorkMode.isManual && this.output.lpf.qCanBeChanged;
            this.cmbLpfType.Enabled = this.output.autoWorkMode.isManual;
        }

        protected override void WrokSelectChanged()
        {
            this.sliderHpfFreq.Binding(this.output.hpf.freqParam, GetHpfFreqLabel, CalcHPFFreqIndexFromText);
            this.sliderLpfFreq.Binding(this.output.lpf.freqParam, GetLpfFreqLabel, CalcLPFFreqIndexFromText);

            this.sliderHpfQ.Binding(this.output.hpf.qParam, GetFilterQLabel, CalcFilterQIndexFromText);
            this.sliderLpfQ.Binding(this.output.lpf.qParam, GetFilterQLabel, CalcFilterQIndexFromText);

            this.RemoveCmbSelectedIndexChangedHandler();
            this.cmbHpfType.SelectedIndex = this.output.hpf.typeIndex;
            this.cmbLpfType.SelectedIndex = this.output.lpf.typeIndex;
            this.RegisterCmbSelectedIndexChangedHandler();

            this.sliderHpfQ.Enabled = this.output.hpf.qCanBeChanged;
            this.sliderLpfQ.Enabled = this.output.lpf.qCanBeChanged;

            this.mixerPanel.UpdateUIFromData();

            this.eqResetAndByPassPanel.isPass = this.output.eqGroup.isBypass;

            this.eqPanel.InitEqAdjusterList();

            this.output.UpdateFreqRespAndInitGraphic();
        }

        protected override void XPFChanged(XPFilterType filterType, FilterElement filterElement)
        {
            if (filterType == XPFilterType.HighPass)
            {
                if (filterElement == FilterElement.Type)
                {
                    this.RemoveCmbSelectedIndexChangedHandler();
                    this.cmbHpfType.SelectedIndex = this.output.hpf.typeIndex;
                    this.RegisterCmbSelectedIndexChangedHandler();

                    this.sliderHpfQ.Enabled = this.output.hpf.qCanBeChanged;
                }
                else if (filterElement == FilterElement.Q)
                {
                    this.sliderHpfQ.UpdateUIFromData();
                }
            }
            else if (filterType == XPFilterType.LowPass)
            {
                if (filterElement == FilterElement.Type)
                {
                    this.RemoveCmbSelectedIndexChangedHandler();
                    this.cmbLpfType.SelectedIndex = this.output.lpf.typeIndex;
                    this.RegisterCmbSelectedIndexChangedHandler();

                    this.sliderLpfQ.Enabled = this.output.lpf.qCanBeChanged;
                }
                else if (filterElement == FilterElement.Q)
                {
                    this.sliderLpfQ.UpdateUIFromData();
                }
            }
        }

        //protected override void HPFChanged(FilterElement filterElement)
        //{
        //    if(filterElement == FilterElement.Type)
        //    {
        //        this.outputBase.hpfTypeParam.v = this.outputBase.hpfModeInit;
        //        this.RemoveCmbSelectedIndexChangedHandler();
        //        this.cmbHpfMode.SelectedIndex = this.outputBase.hpfTypeParam.v;
        //        this.RegisterCmbSelectedIndexChangedHandler();
        //        this.sliderHpfQ.Enabled = this.outputBase.hpf.qCanBeChanged;
        //    }
        //    else if (filterElement == FilterElement.Q)
        //    {
        //        this.outputBase.hpfQParam.v = this.outputBase.hpf.qIndex;
        //        this.sliderHpfQ.UpdateUIFromData();
        //    }
        //}

        //protected override void LPFChanged(FilterElement filterElement)
        //{
        //    if (filterElement == FilterElement.Type)
        //    {
        //        this.outputBase.lpfTypeParam.v = this.outputBase.lpfModeInit;
        //        this.RemoveCmbSelectedIndexChangedHandler();
        //        this.cmbLpfMode.SelectedIndex = this.outputBase.lpfTypeParam.v;
        //        this.RegisterCmbSelectedIndexChangedHandler();
        //        this.sliderLpfQ.Enabled = this.outputBase.lpf.qCanBeChanged;
        //    }
        //    else if (filterElement == FilterElement.Q)
        //    {
        //        this.outputBase.lpfQParam.v = this.outputBase.lpfQIndex;
        //        this.sliderLpfQ.UpdateUIFromData();
        //    }
        //}

        public override void UpdateDisplayLanguage()
        {
            //this.labelHpfFreq.Text = localization[Localization.panelOutput_HpfFreq];
            //this.labelHpfMode.Text = localization[Localization.panelOutput_HpfMode];
            //this.labelLpfFreq.Text = localization[Localization.panelOutput_LpfFreq];
            //this.labelLpfMode.Text = localization[Localization.panelOutput_LpfMode];
            this.lblHpfFreq.Text = localization[Localization.panelOutput_HpfFreq];
            this.lblHpfQ.Text = localization[Localization.panelOutput_HpfQ];
            this.lblHpfType.Text = localization[Localization.panelOutput_HpfMode];

            this.lblLpfFreq.Text = localization[Localization.panelOutput_LpfFreq];
            this.lblLpfQ.Text = localization[Localization.panelOutput_LpfQ];
            this.lblLpfType.Text = localization[Localization.panelOutput_LpfMode];

            this.lblDelay.Text = localization[Localization.panelOutput_Delay];
            this.chkMute.Text = localization[Localization.panelOutput_Mute];
            
            this.mixerPanel.UpdateDisplayLanguage();
            this.limiterPanel.UpdateDisplayLanguage();
            this.eqGroupPanel.UpdateDisplayLanguage();
            this.eqResetAndByPassPanel.UpdateDisplayLanguage();
            this.workModePanel.UpdateDisplayLanguage();

            this.Invalidate();
        }

        public override void UpdateEffectorModel()
        {
            //this.limiterPanel.UpdateThresholdUI();
        }

        private static string GetFilterQLabel(short index)
        {
            return DataUtils.FilterQIndex2String(index);
        }

        protected short CalcFilterQIndexFromText(string text)
        {
            if (text.ToLower().Replace(" ", "") == Off.ToLower())
            {
                return 0;
            }

            text = text.Trim();
            double value;
            if (double.TryParse(text, out value))
            {
                return DataUtils.FilterQ2FilterIndex(value);
            }

            return -1;
        }

        private void btnSwitchUnit_Click(object sender, EventArgs e)
        {
            base.SwitchChannelDelayUnit();
        }

        private void channelPanel_MouseUp(object sender, MouseEventArgs e)
        {
            base.MouseUpHandler(e);
        }

        private void btnSwitchUnit_Click(object sender, KeyEventArgs e)
        {
            base.SwitchChannelDelayUnit();
        }
    }
}
