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
using AEC.Effectors.VKxS;
using AEC.Effectors.VKxS.Modules;
using AEC.Misc;
using AEC.UI.Common;
using AEC.UI.Skin;

namespace AEC.UI.VKxS
{
    public partial class VKxSMonoOutputPanel : VKxSOutputBasePanel
    {
        private MonoOutputModule output;

        public VKxSMonoOutputPanel()
            : base()
        {
            InitializeComponent();
            InitFont();
            this.sliderDelay.ValueChanged += param_ValueChanged;
            this.chkPhase.CheckedChanged += param_ValueChanged;
            this.chkMute.CheckedChanged += param_ValueChanged;
        }

        protected override void InitFont()
        {
            base.InitFont();

            this.lblDelay.Font = FontHelper.CreateFont10();
            this.chkPhase.Font = FontHelper.CreateFont10();
            this.chkMute.Font = FontHelper.CreateFont10();
        }

        protected void BindingOutputModule(MonoOutputModule output_)
        {
            this.output = output_;
            base.BindingOutputBaseModule(this.output);
            this.sliderDelay.Binding(this.output.monoChannel.delay, channelDelayParamLabelProvider.GetDelayLabel, channelDelayParamLabelProvider.CalcDelayIndexFromText);
            this.chkPhase.isChecked = !this.output.monoChannel.phase.value;
            this.chkMute.isChecked = this.output.monoChannel.mute.value;

            this.UpdatePhaseText();

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

                        if (op.channel == output.outputChannelType && op.source == OutputParamChangedSource.MonitorPanel)
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

                        if (omc.channel == output.outputChannelType)
                        {
                            UpdateMuteBtn();
                        }
                    }
                    break;
            }
        }

        protected override void OnChannelDelayUintChangedHandler()
        {
            this.sliderDelay.UpdateUIFromData();
        }

        private void UpdateOutputParamUI(OutputParam param)
        {
            switch (param)
            {
                case OutputParam.Delay:
                    this.sliderDelay.UpdateUIFromData();
                    break;

                case OutputParam.Mute:
                    this.chkMute.isChecked = this.output.monoChannel.mute.value;
                    break;

                case OutputParam.Phase:
                    this.chkPhase.isChecked = !this.output.monoChannel.phase.value;
                    this.UpdatePhaseText();
                    break;
            }
        }

        private void UpdateMuteBtn()
        {
            this.chkMute.isChecked = this.output.monoChannel.mute.value;
        }

        public override void UpdateUIFromData()
        {
            base.UpdateUIFromData();
            this.sliderDelay.UpdateUIFromData();
            this.chkPhase.isChecked = !this.output.monoChannel.phase.value;
            this.chkMute.isChecked = this.output.monoChannel.mute.value;

            this.UpdatePhaseText();
        }

        private void param_ValueChanged(object sender, EventArgs e)
        {
            if(sender.Equals(this.sliderDelay))
            {
                this._eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.OutputPanel, OutputParam.Delay));
            }
            else if (sender.Equals(this.chkPhase))
            {
                this.output.monoChannel.phase.value = !this.chkPhase.isChecked;
                this.UpdatePhaseText();
                this._eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.OutputPanel, OutputParam.Phase));
            }
            else if (sender.Equals(this.chkMute))
            {
                this.output.monoChannel.mute.value = this.chkMute.isChecked;
                this._eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.OutputPanel, OutputParam.Mute));
                this._eventBus.NotifyObeserver(new OutputMuteChangedFromPanelArgs(output.outputChannelType));
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

        private void UpdatePhaseText()
        {
            this.chkPhase.Text = string.Format("{0} {1}", localization[Localization.panelOutput_Phase], AEC.Core.Utils.PhaseToStr(this.output.monoChannel.phase.value));
            this.chkPhase.UpdateDisplay();
        }

        public override void UpdateDisplayLanguage()
        {
            this.lblDelay.Text = localization[Localization.panelOutput_Delay];
            //this.chkPhase.Text = localization[Localization.panelOutput_Phase];
            this.UpdatePhaseText();
            this.chkMute.Text = localization[Localization.panelOutput_Mute];
            this.panelDelayAndMute.Invalidate();

            base.UpdateDisplayLanguage();
        }

        private void panelDelayAndMute_MouseUp(object sender, MouseEventArgs e)
        {
            base.MouseUpHandler(e);
        }

        private void btnSwitchUnit_Click(object sender, EventArgs e)
        {
            base.SwitchChannelDelayUnit();
        }

        protected override void InitTheme()
        {
            base.InitTheme();

            this.panelDelayAndMute.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.panelDelayAndMute.FillColor = SkinManager.CurrentSkin.PanelBackColor;

            this.lblDelay.ForeColor = SkinManager.CurrentSkin.TextForeColor;

            this.sliderDelay.BackColor = SkinManager.CurrentSkin.PanelBackColor;
        }
    }

    public class VKxSCenterOutputPanel : VKxSMonoOutputPanel
    {
        public VKxSCenterOutputPanel()
            : base()
        {
            this.mixerSliderBack = global::AEC.Properties.Resources.aux1_output_slider_back;
            this.sliderNormalBack = global::AEC.Properties.Resources.aux1_output_slider_back;
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.vkxsEffectorModel != null);

            base.BindingOutputModule(base.mController.vkxsEffectorModel.aux1Output);
        }
    }

    public class VKxSSubOutputPanel : VKxSMonoOutputPanel
    {
        public VKxSSubOutputPanel()
            : base()
        {
            this.mixerSliderBack = global::AEC.Properties.Resources.aux2_output_slider_back;
            this.sliderNormalBack = global::AEC.Properties.Resources.aux2_output_slider_back;
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.vkxsEffectorModel != null);

            base.BindingOutputModule(base.mController.vkxsEffectorModel.aux2Output);
        }
    }
}
