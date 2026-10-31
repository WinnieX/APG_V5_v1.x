using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AEC.Core;
using AEC.Comm;
using AEC.Effectors.MX;
using AEC.Effectors.MX.Modules;
using AEC.Misc;

namespace AEC.UI.MX
{
    public partial class MXOutputPanel : MXOutputBasePanel
    {
        private OutputModule output;

        protected virtual Image eqcmLinkageSliderHover
        {
            set
            {
                this.rdoLinkage.backHover = value;
            }
        }

        protected virtual Image eqcmLinkageSliderChecked
        {
            set
            {
                this.rdoLinkage.backChecked = value;
            }
        }

        public MXOutputPanel()
            : base()
        {
            InitializeComponent();
            InitFont();
            base.outputPanel.Height = 262;
            this.sliderLeftDelay.ValueChanged += param_ValueChanged;
            this.sliderRightDelay.ValueChanged += param_ValueChanged;
            this.chkLeftMute.CheckedChanged += param_ValueChanged;
            this.chkRightMute.CheckedChanged += param_ValueChanged;

            this.rdoLinkage.CheckedChanged += eqControlMode_ValueChanged;
            this.rdoLeftCh.CheckedChanged += eqControlMode_ValueChanged;
            this.rdoRightCh.CheckedChanged += eqControlMode_ValueChanged;
        }

        protected override void InitFont()
        {
            base.InitFont();

            this.lblLeftDelay.Font = FontHelper.CreateFont10();
            this.lblRightDelay.Font = FontHelper.CreateFont10();
            this.chkLeftMute.Font = FontHelper.CreateFont10();
            this.chkRightMute.Font = FontHelper.CreateFont10();
        }

        protected void BindingOutputModule(OutputModule output_)
        {
            this.output = output_;
            base.BindingOutputBaseModule(this.output);
            this.sliderLeftDelay.Binding(this.output.leftChDelay, OutputBaseModule.GetDelayLabel, OutputBaseModule.CalcDelayIndexFromText);
            this.sliderRightDelay.Binding(this.output.rightChDelay, OutputBaseModule.GetDelayLabel, OutputBaseModule.CalcDelayIndexFromText);
            this.chkLeftMute.isChecked = this.output.leftChMute.value;
            this.chkRightMute.isChecked = this.output.rightChMute.value;
            this.UpdateEQControlMode();

            base.cmbHpfType.DropDownBtnColor = this.output.themeColor;
            base.cmbLpfType.DropDownBtnColor = this.output.themeColor;
            
            if (this._eventBus != null)
            {
                this._eventBus.RegisterHandler(EventBusHandler);
            }
        }

        public void UpdateEQControlMode()
        {
            System.Diagnostics.Debug.Assert(this.output != null);

            switch (this.output.eqMode)
            {
                case EQControlMode.Linkage:
                    this.rdoLinkage.isCheckInit = true;
                    break;

                case EQControlMode.Left:
                    this.rdoLeftCh.isCheckInit = true;
                    break;

                case EQControlMode.Right:
                    this.rdoRightCh.isCheckInit = true;
                    break;
            }
        }

        private void EventBusHandler(object sender, EventBusArgs e)
        {
            switch(e.type)
            {
                case EventType.OutputParamChanged:
                    {
                        OutputParamChangedArgs op = e as OutputParamChangedArgs;
                        if(op == null)
                        {
                            return;
                        }

                        if(op.channel == output.channel && op.source == OutputParamChangedSource.MonitorPanel)
                        {
                            UpdateOutputParamUI(op.param);
                        }
                    }
                    break;

                case EventType.OutputMuteChangedFromNavigator:
                    {
                        OutputMuteChangedFromNavigatorArgs omc = e as OutputMuteChangedFromNavigatorArgs;
                        if (omc == null)
                        {
                            return;
                        }

                        if (omc.channel == output.channel)
                        {
                            UpdateMuteBtns();
                        }
                    }
                    break;
            }
        }

        private void UpdateOutputParamUI(OutputParam param)
        {
            switch(param)
            {
                case OutputParam.LeftChDelay:
                    this.sliderLeftDelay.UpdateUIFromData();
                    break;

                case OutputParam.RightChDelay:
                    this.sliderRightDelay.UpdateUIFromData();
                    break;

                case OutputParam.LeftChMute:
                    this.chkLeftMute.isChecked = this.output.leftChMute.value;
                    break;

                case OutputParam.RightChMute:
                    this.chkRightMute.isChecked = this.output.rightChMute.value;
                    break;
            }
        }

        private void UpdateMuteBtns()
        {
            this.chkLeftMute.isChecked = this.output.leftChMute.value;
            this.chkRightMute.isChecked = this.output.rightChMute.value;
        }

        public override void UpdateUIFromData()
        {
            base.UpdateUIFromData();
            this.sliderLeftDelay.UpdateUIFromData();
            this.sliderRightDelay.UpdateUIFromData();
            this.chkLeftMute.isChecked = this.output.leftChMute.value;
            this.chkRightMute.isChecked = this.output.rightChMute.value;
            this.UpdateEQControlMode();
        }

        private void eqControlMode_ValueChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.Assert(this.output != null);

            gtRadioButton rdo = sender as gtRadioButton;
            if(!rdo.isChecked)
            {
                return;
            }
            
            if(rdo.Equals(this.rdoLinkage))
            {
                this.output.eqMode = EQControlMode.Linkage;
            }
            else if(rdo.Equals(this.rdoLeftCh))
            {
                this.output.eqMode = EQControlMode.Left;
            }
            else if(rdo.Equals(this.rdoRightCh))
            {
                this.output.eqMode = EQControlMode.Right;
            }

            this.eqResetAndByPassPanel.isPass = this.output.eqGroup.isBypass;
            this.eqPanel.InitEqAdjusterList();
            this.output.UpdateFreqRespAndInitGraphic();
        }

        private void param_ValueChanged(object sender, EventArgs e)
        {
            if (sender.Equals(this.sliderLeftDelay))
            {
                this._eventBus.NotifyObeserver(new OutputParamChangedArgs(output.channel, OutputParamChangedSource.OutputPanel, OutputParam.LeftChDelay));
            }
            else if (sender.Equals(this.sliderRightDelay))
            {
                this._eventBus.NotifyObeserver(new OutputParamChangedArgs(output.channel, OutputParamChangedSource.OutputPanel, OutputParam.RightChDelay));
            }
            if(sender.Equals(this.chkLeftMute))
            {
                this.output.leftChMute.value = this.chkLeftMute.isChecked;
                this._eventBus.NotifyObeserver(new OutputParamChangedArgs(output.channel, OutputParamChangedSource.OutputPanel, OutputParam.LeftChMute));
                this._eventBus.NotifyObeserver(new OutputMuteChangedFromPanelArgs(output.channel));                
            }
            else if(sender.Equals(this.chkRightMute))
            {
                this.output.rightChMute.value = this.chkRightMute.isChecked;
                this._eventBus.NotifyObeserver(new OutputParamChangedArgs(output.channel, OutputParamChangedSource.OutputPanel, OutputParam.RightChMute));
                this._eventBus.NotifyObeserver(new OutputMuteChangedFromPanelArgs(output.channel));
            }
        }

        protected override Image sliderNormalBack
        {
            get
            {
                return base.sliderNormalBack;
            }
            set
            {
                base.sliderNormalBack = value;
                this.sliderLeftDelay.backgroundNormal = value;
                this.sliderRightDelay.backgroundNormal = value;
            }
        }

        public override void UpdateDisplayLanguage()
        {
            this.lblLeftDelay.Text = localization[Localization.panelOutput_LeftChDelay];
            this.lblRightDelay.Text = localization[Localization.panelOutput_RightChDelay];
            this.chkLeftMute.Text = localization[Localization.panelOutput_LeftChMute];
            this.chkRightMute.Text = localization[Localization.panelOutput_RightChMute]; 
            this.rdoLinkage.Text = localization[Localization.panelOutput_Linkage];
            this.rdoLeftCh.Text = localization[Localization.panelOutput_LeftCh];
            this.rdoRightCh.Text = localization[Localization.panelOutput_RightCh];
            this.panelDelayAndMute.Invalidate();

            base.UpdateDisplayLanguage();
        }
    }

    public class MXMainOutputPanel : MXOutputPanel
    {
        public MXMainOutputPanel()
            : base()
        {
            this.mixerSliderBack = global::AEC.Properties.Resources.main_output_slider_back;
            this.sliderNormalBack = global::AEC.Properties.Resources.main_output_slider_back;

            base.eqcmLinkageSliderHover = global::AEC.Properties.Resources.rdo_main_output_eqcm_linkage_hover;
            base.eqcmLinkageSliderChecked = global::AEC.Properties.Resources.rdo_main_output_eqcm_linkage_pressed;
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.mxEffectorModel != null);

            base.BindingOutputModule(base.mController.mxEffectorModel.mainOutput);
        }
    }

    public class MXSurOutputPanel : MXOutputPanel
    {
        public MXSurOutputPanel()
            : base()
        {
            this.mixerSliderBack = global::AEC.Properties.Resources.sur_output_slider_back;
            this.sliderNormalBack = global::AEC.Properties.Resources.sur_output_slider_back;

            base.eqcmLinkageSliderHover = global::AEC.Properties.Resources.rdo_sur_output_eqcm_linkage_hover;
            base.eqcmLinkageSliderChecked = global::AEC.Properties.Resources.rdo_sur_output_eqcm_linkage_pressed;
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.mxEffectorModel != null);

            base.BindingOutputModule(base.mController.mxEffectorModel.surOutput);
        }
    }
}
