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
    public partial class X5MainOutputPanel : X5OutputBasePanel
    {
        //public const string DelayUnit = "ms";
        private MainOutputModule output;
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

        public X5MainOutputPanel()
            : base()
        {
            InitializeComponent();
            this.InitControl(this.eqGroupPanel, null, null);
            //this.cmbHpfMode.DrawMode = DrawMode.OwnerDrawFixed;

            this.eqResetAndByPassPanel.ResetEQ += base.ResetEQGroup;
            this.eqResetAndByPassPanel.EQPassChanged += base.SwitchEQByPass;

            this.chkLeftMute.CheckedChanged += param_ValueChanged;
            this.chkRightMute.CheckedChanged += param_ValueChanged;
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

            this.BindingOutputModule(base.mController.x5EffectorModel.mainOutput);
        }

        protected void BindingOutputModule(MainOutputModule output_)
        {
            this.output = output_;
            base.BindingModule(this.output, MEffectorFamily.X5);
            this.mixerPanel.Binding(this.output, this._eventBus);
            this.limiterPanel.Binding(this.output.limiter, this._eventBus, base.mController.mEffectorThreshold);
            //this.limiterPanel.Binding(this.outputBase.limiter, base._eventBus, this.outputBase.outputChannelType, base.mController.mEffectorThreshold);
            
            //this.cmbHpfType.SelectedIndex = this.outputBase.hpf.typeIndex;
            //this.cmbLpfType.SelectedIndex = this.outputBase.lpf.typeIndex;

            this.sliderLeftDelay.Binding(this.output.leftChannel.delay, channelDelayParamLabelProvider.GetDelayLabel, channelDelayParamLabelProvider.CalcDelayIndexFromText);
            this.sliderRightDelay.Binding(this.output.rightChannel.delay, channelDelayParamLabelProvider.GetDelayLabel, channelDelayParamLabelProvider.CalcDelayIndexFromText);

            this.chkLeftMute.isChecked = this.output.leftChannel.mute.value;
            this.chkRightMute.isChecked = this.output.rightChannel.mute.value;

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
                this.chkLeftMute.isChecked = this.output.leftChannel.mute.value;
                this.chkRightMute.isChecked = this.output.rightChannel.mute.value;
            }
        }

        protected override void OnChannelDelayUintChangedHandler()
        {
            this.sliderLeftDelay.UpdateUIFromData();
            this.sliderRightDelay.UpdateUIFromData();
        }

        public override void UpdateUIFromData()
        {
            if (this.eqPanel != null)
            {
                this.eqPanel.InitEqAdjusterList();
            }

            this.mixerPanel.UpdateUIFromData();
            this.limiterPanel.UpdateUIFromData();

            this.sliderLeftDelay.UpdateUIFromData();
            this.sliderRightDelay.UpdateUIFromData();
            this.chkLeftMute.isChecked = this.output.leftChannel.mute.value;
            this.chkRightMute.isChecked = this.output.rightChannel.mute.value;
            
            this.RemoveCmbSelectedIndexChangedHandler();
            //this.cmbHpfType.SelectedIndex = this.outputBase.hpf.typeIndex;
            //this.cmbLpfType.SelectedIndex = this.outputBase.lpf.typeIndex;
            this.RegisterCmbSelectedIndexChangedHandler();

            this.eqResetAndByPassPanel.isPass = this.output.eqGroup.isBypass;
            //this.mixerPanel.Enabled = this.outputBase.music.isManual;
            this.workModePanel.UpdateUIFromData();

            this.WrokModeChanged();
        }

        private void RegisterCmbSelectedIndexChangedHandler()
        {
            //this.cmbHpfType.SelectedIndexChanged += cmbFilterType_SelectedIndexChanged;
            //this.cmbLpfType.SelectedIndexChanged += cmbFilterType_SelectedIndexChanged;
        }

        private void RemoveCmbSelectedIndexChangedHandler()
        {
            //this.cmbHpfType.SelectedIndexChanged -= cmbFilterType_SelectedIndexChanged;
            //this.cmbLpfType.SelectedIndexChanged -= cmbFilterType_SelectedIndexChanged;
        }

        private void param_ValueChanged(object sender, EventArgs e)
        {
            if (sender.Equals(this.chkLeftMute))
            {
                this.output.leftChannel.mute.value = this.chkLeftMute.isChecked;
                this._eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.OutputPanel, OutputParam.LeftChMute));
                this._eventBus.NotifyObeserver(new OutputMuteChangedFromPanelArgs(output.outputChannelType));
            }
            else if (sender.Equals(this.chkRightMute))
            {
                this.output.rightChannel.mute.value = this.chkRightMute.isChecked;
                this._eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.OutputPanel, OutputParam.RightChMute));
                this._eventBus.NotifyObeserver(new OutputMuteChangedFromPanelArgs(output.outputChannelType));
            }
        }

        private void cmbFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            gtComboBox cmb = sender as gtComboBox;
            //if (sender.Equals(this.cmbHpfType))
            //{
            //    //this.outputBase.hpf.UpdateTypeParam() = (short)cmb.SelectedIndex;
            //    this.outputBase.hpf.typeIndexParam = (short)cmb.SelectedIndex;
            //    this.outputBase.UpdateXPFromControlPanel(XPFilterType.HighPass, FilterElement.Type);                
            //    //this.sliderHpfQ.Invalidate();
            //}
            //else if(sender.Equals(this.cmbLpfType))
            //{
            //    this.outputBase.lpf.typeIndexParam = (short)cmb.SelectedIndex;
            //    this.outputBase.UpdateXPFromControlPanel(XPFilterType.LowPass, FilterElement.Type);
            //    //this.sliderLpfQ.Invalidate();
            //}
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

        protected override void WrokModeChanged()
        {
            this.mixerPanel.Enabled = this.output.autoWorkMode.isManual;
            this.eqPanel.UpdateEqCanbeChanged();
        }

        protected override void WrokSelectChanged()
        {
            this.mixerPanel.UpdateUIFromData();

            this.eqResetAndByPassPanel.isPass = this.output.eqGroup.isBypass;

            this.eqPanel.InitEqAdjusterList();

            this.output.UpdateFreqRespAndInitGraphic();
        }

        protected override void XPFChanged(XPFilterType filterType, FilterElement filterElement)
        {
            //if (filterType == XPFilterType.HighPass)
            //{
            //    if (filterElement == FilterElement.Type)
            //    {
            //        this.RemoveCmbSelectedIndexChangedHandler();
            //        this.cmbHpfType.SelectedIndex = this.outputBase.hpf.typeIndex;
            //        this.RegisterCmbSelectedIndexChangedHandler();
            //    }
            //}
            //else if (filterType == XPFilterType.LowPass)
            //{
            //    if (filterElement == FilterElement.Type)
            //    {
            //        this.RemoveCmbSelectedIndexChangedHandler();
            //        this.cmbLpfType.SelectedIndex = this.outputBase.lpf.typeIndex;
            //        this.RegisterCmbSelectedIndexChangedHandler();
            //    }
            //}
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
            this.lblLeftDelay.Text = localization[Localization.panelOutput_LeftChDelay];
            this.lblRightDelay.Text = localization[Localization.panelOutput_RightChDelay];
            this.chkLeftMute.Text = localization[Localization.panelOutput_LeftChMute];
            this.chkRightMute.Text = localization[Localization.panelOutput_RightChMute];
            
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

        private void btnSwitchUnit_Click(object sender, EventArgs e)
        {
            base.SwitchChannelDelayUnit();
        }

        private void channelPanel_MouseUp(object sender, MouseEventArgs e)
        {
            base.MouseUpHandler(e);
        }
    }
}
