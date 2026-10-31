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
using AEC.Effectors.KX;
using AEC.Effectors.KX.Modules;
using AEC.Misc;
using AEC.UI.Common;

namespace AEC.UI.KX
{
    public partial class KXOutputPanel : KXOutputBasePanel
    {
        private StereoOutputModule output;

        private PopMenuChannelDelayUint popMenuChannelDelayUint;

        public KXOutputPanel()
            : base()
        {
            InitializeComponent();
            InitFont();
            this.sliderLeftDelay.ValueChanged += param_ValueChanged;
            this.sliderRightDelay.ValueChanged += param_ValueChanged;
            this.chkLeftPhase.CheckedChanged += param_ValueChanged;
            this.chkRightPhase.CheckedChanged += param_ValueChanged;
            this.chkLeftMute.CheckedChanged += param_ValueChanged;
            this.chkRightMute.CheckedChanged += param_ValueChanged;

            this.components = new System.ComponentModel.Container();
            this.popMenuChannelDelayUint = new PopMenuChannelDelayUint(this.components);
        }

        protected override void InitFont()
        {
            base.InitFont();

            this.lblLeftDelay.Font = FontHelper.CreateFont10();
            this.lblRightDelay.Font = FontHelper.CreateFont10();
            this.chkLeftPhase.Font = FontHelper.CreateFont10();
            this.chkRightPhase.Font = FontHelper.CreateFont10();
            this.chkLeftMute.Font = FontHelper.CreateFont10();
            this.chkRightMute.Font = FontHelper.CreateFont10();
        }

        protected void BindingOutputModule(StereoOutputModule output_)
        {
            this.output = output_;
            base.BindingOutputBaseModule(this.output);
            this.sliderLeftDelay.Binding(this.output.leftChannel.delay, OutputBaseModule.channelDelayParamLabelProvider.GetDelayLabel, OutputBaseModule.channelDelayParamLabelProvider.CalcDelayIndexFromText);
            this.sliderRightDelay.Binding(this.output.rightChannel.delay, OutputBaseModule.channelDelayParamLabelProvider.GetDelayLabel, OutputBaseModule.channelDelayParamLabelProvider.CalcDelayIndexFromText);

            this.chkLeftPhase.isChecked = !this.output.leftChannel.phase.value;
            this.chkRightPhase.isChecked = !this.output.rightChannel.phase.value;
            this.chkLeftMute.isChecked = this.output.leftChannel.mute.value;
            this.chkRightMute.isChecked = this.output.rightChannel.mute.value;

            this.UpdateLeftChPhaseText();
            this.UpdateRightChPhaseText();

            base.cmbHpfType.DropDownBtnColor = this.output.themeColor;
            base.cmbLpfType.DropDownBtnColor = this.output.themeColor;

            this.popMenuChannelDelayUint.BindingEventBus(this._eventBus);

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

                        if(op.channel == output.outputChannelType && op.source == OutputParamChangedSource.MonitorPanel)
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
                            UpdateMuteBtns();
                        }
                    }
                    break;

                case EventType.ChannelDelayUnitChanged:
                    this.sliderLeftDelay.UpdateUIFromData();
                    this.sliderRightDelay.UpdateUIFromData();
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
                    this.chkLeftMute.isChecked = this.output.leftChannel.mute.value;
                    break;

                case OutputParam.RightChMute:
                    this.chkRightMute.isChecked = this.output.rightChannel.mute.value;
                    break;

                case OutputParam.LeftChPhase:
                    this.chkLeftPhase.isChecked = !this.output.leftChannel.phase.value;
                    this.UpdateLeftChPhaseText();
                    break;

                case OutputParam.RightChPhase:
                    this.chkRightPhase.isChecked = !this.output.rightChannel.phase.value;
                    this.UpdateRightChPhaseText();
                    break;
            }
        }

        private void UpdateMuteBtns()
        {
            this.chkLeftMute.isChecked = this.output.leftChannel.mute.value;
            this.chkRightMute.isChecked = this.output.rightChannel.mute.value;
        }

        public override void UpdateUIFromData()
        {
            base.UpdateUIFromData();
            this.sliderLeftDelay.UpdateUIFromData();
            this.sliderRightDelay.UpdateUIFromData();
            this.chkLeftMute.isChecked = this.output.leftChannel.mute.value;
            this.chkRightMute.isChecked = this.output.rightChannel.mute.value;
            this.chkLeftPhase.isChecked = !this.output.leftChannel.phase.value;
            this.chkRightPhase.isChecked = !this.output.rightChannel.phase.value;

            this.UpdateLeftChPhaseText();
            this.UpdateRightChPhaseText();
        }

        private void param_ValueChanged(object sender, EventArgs e)
        {
            if (sender.Equals(this.sliderLeftDelay))
            {
                this._eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.OutputPanel, OutputParam.LeftChDelay));
            }
            else if (sender.Equals(this.sliderRightDelay))
            {
                this._eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.OutputPanel, OutputParam.RightChDelay));
            }
            else if(sender.Equals(this.chkLeftPhase))
            {
                this.output.leftChannel.phase.value = !this.chkLeftPhase.isChecked;
                this.UpdateLeftChPhaseText();
                this._eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.OutputPanel, OutputParam.LeftChPhase));
            }
            else if(sender.Equals(this.chkRightPhase))
            {
                this.output.rightChannel.phase.value = !this.chkRightPhase.isChecked;
                this.UpdateRightChPhaseText();
                this._eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.OutputPanel, OutputParam.RightChPhase));
            }
            else if(sender.Equals(this.chkLeftMute))
            {
                this.output.leftChannel.mute.value = this.chkLeftMute.isChecked;
                this._eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.OutputPanel, OutputParam.LeftChMute));
                this._eventBus.NotifyObeserver(new OutputMuteChangedFromPanelArgs(output.outputChannelType));
            }
            else if(sender.Equals(this.chkRightMute))
            {
                this.output.rightChannel.mute.value = this.chkRightMute.isChecked;
                this._eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.OutputPanel, OutputParam.RightChMute));
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
                this.sliderLeftDelay.backgroundNormal = value;
                this.sliderRightDelay.backgroundNormal = value;
            }
        }

        private void UpdateLeftChPhaseText()
        {
            this.chkLeftPhase.Text = string.Format("{0} {1}", localization[Localization.panelOutput_LeftChPhase], AEC.Core.Utils.PhaseToStr(this.output.leftChannel.phase.value));
            this.chkLeftPhase.UpdateDisplay();
        }

        private void UpdateRightChPhaseText()
        {
            this.chkRightPhase.Text = string.Format("{0} {1}", localization[Localization.panelOutput_RightChPhase], AEC.Core.Utils.PhaseToStr(this.output.rightChannel.phase.value));
            this.chkRightPhase.UpdateDisplay();
        }

        public override void UpdateDisplayLanguage()
        {
            this.lblLeftDelay.Text = localization[Localization.panelOutput_LeftChDelay];
            this.lblRightDelay.Text = localization[Localization.panelOutput_RightChDelay];
            this.chkLeftMute.Text = localization[Localization.panelOutput_LeftChMute];
            this.chkRightMute.Text = localization[Localization.panelOutput_RightChMute];
            //this.chkLeftPhase.Text = localization[Localization.panelOutput_LeftChPhase];
            //this.chkRightPhase.Text = localization[Localization.panelOutput_RightChPhase];
            this.UpdateLeftChPhaseText();
            this.UpdateRightChPhaseText();
            
            this.panelDelayAndMute.Invalidate();

            base.UpdateDisplayLanguage();
        }

        private void panelDelayAndMute_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.popMenuChannelDelayUint.Show();
            }
        }
    }

    public class KXMainOutputPanel : KXOutputPanel
    {
        public KXMainOutputPanel()
            : base()
        {
            this.mixerSliderBack = global::AEC.Properties.Resources.main_output_mixer_level_back;
            this.sliderNormalBack = global::AEC.Properties.Resources.main_output_slider_back;
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.kxEffectorModel != null);

            base.BindingOutputModule(base.mController.kxEffectorModel.mainOutput);
        }
    }

    public class KXSurOutputPanel : KXOutputPanel
    {
        public KXSurOutputPanel()
            : base()
        {
            this.mixerSliderBack = global::AEC.Properties.Resources.sur_output_mixer_level_back;
            this.sliderNormalBack = global::AEC.Properties.Resources.sur_output_slider_back;
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.kxEffectorModel != null);

            base.BindingOutputModule(base.mController.kxEffectorModel.surOutput);
        }
    }
}
