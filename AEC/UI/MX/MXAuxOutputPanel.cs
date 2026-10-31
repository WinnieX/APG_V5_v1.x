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
    public partial class MXAuxOutputPanel : MXOutputBasePanel
    {
        private AuxOutputModule output;

        public MXAuxOutputPanel()
            : base()
        {
            InitializeComponent();
            InitFont();
            this.sliderDelay.ValueChanged += param_ValueChanged;
            this.chkMute.CheckedChanged += param_ValueChanged;
        }

        protected override void InitFont()
        {
            base.InitFont();

            this.lblDelay.Font = FontHelper.CreateFont10();
            this.chkMute.Font = FontHelper.CreateFont10();
        }

        protected void BindingOutputModule(AuxOutputModule output_)
        {
            this.output = output_;
            base.BindingOutputBaseModule(this.output);
            this.sliderDelay.Binding(this.output.delay, OutputBaseModule.GetDelayLabel, OutputBaseModule.CalcDelayIndexFromText);
            this.chkMute.isChecked = this.output.mute.value;

            base.cmbHpfType.DropDownBtnColor = this.output.themeColor;
            base.cmbLpfType.DropDownBtnColor = this.output.themeColor;

            if (this._eventBus != null)
            {
                this._eventBus.RegisterHandler(EventBusHandler);
            }
        }

        private void EventBusHandler(object sender, EventBusArgs e)
        {
            switch (e.type)
            {
                case EventType.OutputParamChanged:
                    {
                        OutputParamChangedArgs op = e as OutputParamChangedArgs;
                        if (op == null)
                        {
                            return;
                        }

                        if (op.channel == output.channel && op.source == OutputParamChangedSource.MonitorPanel)
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
                            UpdateMuteBtn();
                        }
                    }
                    break;
            }
        }

        private void UpdateOutputParamUI(OutputParam param)
        {
            switch (param)
            {
                case OutputParam.Delay:
                    this.sliderDelay.UpdateUIFromData();
                    break;

                case OutputParam.Mute:
                    this.chkMute.isChecked = this.output.mute.value;
                    break;
            }
        }

        private void UpdateMuteBtn()
        {
            this.chkMute.isChecked = this.output.mute.value;
        }

        public override void UpdateUIFromData()
        {
            base.UpdateUIFromData();
            this.sliderDelay.UpdateUIFromData();
            this.chkMute.isChecked = this.output.mute.value;
        }

        private void param_ValueChanged(object sender, EventArgs e)
        {
            if(sender.Equals(this.sliderDelay))
            {
                this._eventBus.NotifyObeserver(new OutputParamChangedArgs(output.channel, OutputParamChangedSource.OutputPanel, OutputParam.Delay));
            }
            else if (sender.Equals(this.chkMute))
            {
                this.output.mute.value = this.chkMute.isChecked;
                this._eventBus.NotifyObeserver(new OutputParamChangedArgs(output.channel, OutputParamChangedSource.OutputPanel, OutputParam.Mute));
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
                this.sliderDelay.backgroundNormal = value;
            }
        }

        public override void UpdateDisplayLanguage()
        {
            this.lblDelay.Text = localization[Localization.panelOutput_Delay];
            this.chkMute.Text = localization[Localization.panelOutput_Mute];
            this.panelDelayAndMute.Invalidate();

            base.UpdateDisplayLanguage();
        }
    }

    public class MXAux1OutputPanel : MXAuxOutputPanel
    {
        public MXAux1OutputPanel()
            : base()
        {
            this.mixerSliderBack = global::AEC.Properties.Resources.aux1_output_slider_back;
            this.sliderNormalBack = global::AEC.Properties.Resources.aux1_output_slider_back;
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.mxEffectorModel != null);

            base.BindingOutputModule(base.mController.mxEffectorModel.aux1Output);
        }
    }

    public class MXAux2OutputPanel : MXAuxOutputPanel
    {
        public MXAux2OutputPanel()
            : base()
        {
            this.mixerSliderBack = global::AEC.Properties.Resources.aux2_output_slider_back;
            this.sliderNormalBack = global::AEC.Properties.Resources.aux2_output_slider_back;
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.mxEffectorModel != null);

            base.BindingOutputModule(base.mController.mxEffectorModel.aux2Output);
        }
    }
}
