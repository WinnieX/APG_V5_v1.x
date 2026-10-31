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
using AEC.Effectors.VX;
using AEC.Effectors.VX.Modules;
using AEC.Misc;

namespace AEC.UI.VX
{
    public partial class VXOutputPanel : VXOutputBasePanel
    {
        private OutputModule output;
        public VXOutputPanel()
            : base()
        {
            InitializeComponent();
            InitFont();
            this.sliderLeftDelay.ValueChanged += param_ValueChanged;
            this.sliderRightDelay.ValueChanged += param_ValueChanged;
            this.chkLeftMute.CheckedChanged += param_ValueChanged;
            this.chkRightMute.CheckedChanged += param_ValueChanged;
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

            base.cmbHpfType.DropDownBtnColor = this.output.themeColor;
            base.cmbLpfType.DropDownBtnColor = this.output.themeColor;
            
            if (this._eventBus != null)
            {
                this._eventBus.RegisterHandler(EventBusHandler);
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
            this.panelDelayAndMute.Invalidate();

            base.UpdateDisplayLanguage();
        }
    }

    public class VXMainOutputPanel : VXOutputPanel
    {
        public VXMainOutputPanel()
            : base()
        {
            this.mixerSliderBack = global::AEC.Properties.Resources.main_output_slider_back;
            this.sliderNormalBack = global::AEC.Properties.Resources.main_output_slider_back;
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.vxEffectorModel != null);

            base.BindingOutputModule(base.mController.vxEffectorModel.mainOutput);
        }
    }

    public class VXSurOutputPanel : VXOutputPanel
    {
        public VXSurOutputPanel()
            : base()
        {
            this.mixerSliderBack = global::AEC.Properties.Resources.main_output_slider_back;
            this.sliderNormalBack = global::AEC.Properties.Resources.main_output_slider_back;
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.vxEffectorModel != null);

            base.BindingOutputModule(base.mController.vxEffectorModel.surOutput);
        }
    }
}
