using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AEC.Core;
using AEC.Effectors;
using AEC.Effectors.KX.Modules;
using AEC.Misc;
using AEC.Common;
using AEC.UI.Controls;
using AEC.UI.Common;

namespace AEC.UI.KX
{
    public partial class KXMonitorStereoOutputControlPanel : AEC.UI.KX.KXMonitorOutputPanelBase
    {
        private const int LayoutLeftLevelMeterX = 48;
        private const int LayoutRightLevelMeterX = 198;
        private const int LayoutLeftTicksX = 6;
        private const int LayoutRightTicksX = 216;

        private StereoOutputModule output;
        private EventBus eventBus;

        private DUILabel labelLimiter;
        private DUILevelMeter leftChannelLevelMeter;
        private DUILevelMeter rightChannelLevelMeter;
        private DUILevelMeter limiterMeter;
        private DUIPicture levelMeterLeftTicks;
        private DUIPicture levelMeterRightTicks;
        private DUIPicture limiterTicks;

        private DualLinearLevelIndicator dualLinearLevelIndicator = new DualLinearLevelIndicator();
        private DynamicValueIndicator limiterIndicator = new DynamicValueIndicator();

        private PopMenuChannelDelayUint popMenuChannelDelayUint;

        public override bool monitorIsON
        {
            set
            {
                if (this.leftChannelLevelMeter != null)
                {
                    this.leftChannelLevelMeter.isOn = value;
                }
                if (this.rightChannelLevelMeter != null)
                {
                    this.rightChannelLevelMeter.isOn = value;
                }
                if (this.limiterMeter != null)
                {
                    this.limiterMeter.isOn = value;
                }

                if (!value)
                {
                    this.dualLinearLevelIndicator.Clear();
                    this.limiterIndicator.Clear();
                }

                this.Invalidate();
            }
        }

        
        
        public KXMonitorStereoOutputControlPanel()
        {
            InitializeComponent();
            Init();
            InitFont();
            this.paramLeftChDelay.ValueChanged += param_ValueChanged;
            this.paramRightChDelay.ValueChanged += param_ValueChanged;
            this.chkLeftMute.CheckedChanged += param_ValueChanged;
            this.chkRightMute.CheckedChanged += param_ValueChanged;
            this.chkLeftPhase.CheckedChanged += param_ValueChanged;
            this.chkRightPhase.CheckedChanged += param_ValueChanged;

            this.components = new System.ComponentModel.Container();
            this.popMenuChannelDelayUint = new PopMenuChannelDelayUint(this.components);
        }

        protected override void OnInitDuiControls()
        {
            base.OnInitDuiControls();

            if (this.labelLimiter == null)
            {
                this.labelLimiter = new DUILabel();
                labelLimiter.Location = new Point(LayoutRightLevelMeterX, LayoutLimiterY);
                labelLimiter.Size = new Size(64, 16);
                labelLimiter.Font = this.Font;
                labelLimiter.Text = LimiterLabel;
                labelLimiter.ForeColor = this.ForeColor;
                labelLimiter.Alignment = ContentAlignment.MiddleLeft;
                labelLimiter.ShowBackground = false;
                labelLimiter.ShowBorder = false;
                labelLimiter.BorderColor = Color.Transparent;
                labelLimiter.FillColor = Color.Transparent;
                labelLimiter.Visible = true;
            }

            if (this.leftChannelLevelMeter == null)
            {
                this.leftChannelLevelMeter = new DUIMaxLevelMeter();
                this.leftChannelLevelMeter.ledLightImageModel = KXLevelMeterImageProvider.levelMeterLEDLightImageModel;
                this.leftChannelLevelMeter.levelBackground = KXLevelMeterImageProvider.GetLevelMeterBackground();
                this.leftChannelLevelMeter.levelForeground = KXLevelMeterImageProvider.GetLevelMeterForeground();
                this.leftChannelLevelMeter.Size = this.leftChannelLevelMeter.levelBackground.Size;
                this.leftChannelLevelMeter.Location = new Point(LayoutLeftLevelMeterX, KXLevelMeterImageProvider.YOffsetFromTickToLed + LayoutLevelTicksY);
                this.leftChannelLevelMeter.Visible = true;
            }

            if (this.rightChannelLevelMeter == null)
            {
                this.rightChannelLevelMeter = new DUIMaxLevelMeter();
                this.rightChannelLevelMeter.ledLightImageModel = KXLevelMeterImageProvider.levelMeterLEDLightImageModel;
                this.rightChannelLevelMeter.levelBackground = KXLevelMeterImageProvider.GetLevelMeterBackground();
                this.rightChannelLevelMeter.levelForeground = KXLevelMeterImageProvider.GetLevelMeterForeground();
                this.rightChannelLevelMeter.Size = this.rightChannelLevelMeter.levelBackground.Size;
                this.rightChannelLevelMeter.Location = new Point(LayoutRightLevelMeterX, KXLevelMeterImageProvider.YOffsetFromTickToLed + LayoutLevelTicksY);
                this.rightChannelLevelMeter.Visible = true;
            }

            if (this.limiterMeter == null)
            {
                this.limiterMeter = new DUILevelMeter0_12dB();
                this.limiterMeter.ledLightImageModel = KXLevelMeterImageProvider.limiterLEDLightImageModel;
                this.limiterMeter.levelBackground = KXLevelMeterImageProvider.GetLimiterMeterBackground();
                this.limiterMeter.levelForeground = KXLevelMeterImageProvider.GetLimiterMeterForeground();
                this.limiterMeter.Size = this.limiterMeter.levelBackground.Size;
                this.limiterMeter.Location = new Point(LayoutRightLevelMeterX, KXLevelMeterImageProvider.YOffsetFromTickToLed + LayoutLimiterTicksY);
                this.limiterMeter.Visible = true;
            }

            if (this.levelMeterLeftTicks == null)
            {
                this.levelMeterLeftTicks = new DUIPicture();
                this.levelMeterLeftTicks.background = KXLevelMeterImageProvider.GetLevelMeterLeftTicks(base.effectorThreshold.maxLevel);
                this.levelMeterLeftTicks.Size = this.levelMeterLeftTicks.background.Size;
                this.levelMeterLeftTicks.Location = new Point(LayoutLeftTicksX, LayoutLevelTicksY);
                this.levelMeterLeftTicks.Visible = true;
            }

            if (this.levelMeterRightTicks == null)
            {
                this.levelMeterRightTicks = new DUIPicture();
                this.levelMeterRightTicks.background = KXLevelMeterImageProvider.GetLevelMeterRightTicks(base.effectorThreshold.maxLevel);
                this.levelMeterRightTicks.Size = this.levelMeterRightTicks.background.Size;
                this.levelMeterRightTicks.Location = new Point(LayoutRightTicksX, LayoutLevelTicksY);
                this.levelMeterRightTicks.Visible = true;
            }

            if (this.limiterTicks == null)
            {
                this.limiterTicks = new DUIPicture();
                this.limiterTicks.background = KXLevelMeterImageProvider.GetLimiterMeterTicks();
                this.limiterTicks.Size = this.limiterTicks.background.Size;
                this.limiterTicks.Location = new Point(LayoutRightTicksX, LayoutLimiterTicksY);
                this.limiterTicks.Visible = true;
            }

            //base.AddDuiControl(this.labelLimiter);
            base.AddDuiControl(this.leftChannelLevelMeter);
            base.AddDuiControl(this.rightChannelLevelMeter);
            base.AddDuiControl(this.limiterMeter);
            base.AddDuiControl(this.levelMeterLeftTicks);
            base.AddDuiControl(this.levelMeterRightTicks);
            base.AddDuiControl(this.limiterTicks);
        }

        private void Init()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //this.BackColor = Color.Transparent;
        }

        private void InitFont()
        {
            this.lblLeftDelay.Font = FontHelper.CreateFont10();
            this.lblRightDelay.Font = FontHelper.CreateFont10();
            this.chkLeftMute.Font = FontHelper.CreateFont10();
            this.chkRightMute.Font = FontHelper.CreateFont10();
            this.chkLeftPhase.Font = FontHelper.CreateFont10();
            this.chkRightPhase.Font = FontHelper.CreateFont10();
        }

        public void UpdateStatus(double leftChannelOutputLevel, double rightChannelOutputLevel, double limiterLevel)
        {
            Rectangle invalidRect = this.dualLinearLevelIndicator.UpdateStatus(
                this.leftChannelLevelMeter, leftChannelOutputLevel,
                this.rightChannelLevelMeter, rightChannelOutputLevel);

            if (this.limiterIndicator.UpdateStatus(this.limiterMeter, limiterLevel))
            {
                if (invalidRect.IsEmpty)
                {
                    invalidRect = this.limiterMeter.ClientRectangle;
                }
                else
                {
                    invalidRect = Rectangle.Union(invalidRect, this.limiterMeter.ClientRectangle);
                }
            }

            if (!invalidRect.IsEmpty)
            {
                this.Invalidate(invalidRect);
                this.Update();
            }
        }

        public override void DoLevelFreeFall()
        {
            if (this.leftChannelLevelMeter == null || this.rightChannelLevelMeter == null || this.limiterMeter == null)
            {
                return;
            }

            Rectangle invalidRect = this.dualLinearLevelIndicator.DoLevelFreeFall(
                this.leftChannelLevelMeter, this.rightChannelLevelMeter);

            if (limiterIndicator.DoValueFreeFall(this.limiterMeter))
            {
                if (invalidRect.IsEmpty)
                {
                    invalidRect = this.limiterMeter.ClientRectangle; ;
                }
                else
                {
                    invalidRect = Rectangle.Union(invalidRect, this.limiterMeter.ClientRectangle);
                }
            }

            if (!invalidRect.IsEmpty)
            {
                this.Invalidate(invalidRect);
                this.Update();
            }
        }

        public void Binding(StereoOutputModule output_, EventBus eventBus_, MEffectorThreshold effectorThreshold_)
        {
            System.Diagnostics.Debug.Assert(output_ != null && eventBus_ != null);

            base.BindingModule(output_, effectorThreshold_);

            this.output = output_;
            this.eventBus = eventBus_;

            this.popMenuChannelDelayUint.BindingEventBus(eventBus_);

            this.panelCL.Binding(this.output, eventBus_, this.output.outputChannelType, effectorThreshold_);
            this.paramLeftChDelay.Init(StereoOutputModule.MaxDelayIndex, this.output.leftChannel.delay.v, OutputBaseModule.channelDelayParamLabelProvider.GetDelayLabel, OutputBaseModule.channelDelayParamLabelProvider.CalcDelayIndexFromText);
            this.paramRightChDelay.Init(StereoOutputModule.MaxDelayIndex, this.output.rightChannel.delay.v, OutputBaseModule.channelDelayParamLabelProvider.GetDelayLabel, OutputBaseModule.channelDelayParamLabelProvider.CalcDelayIndexFromText);
            this.chkLeftMute.isChecked = this.output.leftChannel.mute.value;
            this.chkRightMute.isChecked = this.output.rightChannel.mute.value;

            this.UpdateLeftChPhaseText();
            this.UpdateRightChPhaseText();

            if (this.eventBus != null)
            {
                this.eventBus.RegisterHandler(EventBusHandler);
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

                        if (op.channel == output.outputChannelType && op.source == OutputParamChangedSource.OutputPanel)
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
                    this.paramLeftChDelay.UpdateLabelDisplay();
                    this.paramRightChDelay.UpdateLabelDisplay();                    
                    break;
            }
        }

        private void UpdateOutputParamUI(OutputParam param)
        {
            switch (param)
            {
                case OutputParam.LeftChDelay:
                    this.paramLeftChDelay.initV = this.output.leftChannel.delay.v;
                    break;

                case OutputParam.RightChDelay:
                    this.paramRightChDelay.initV = this.output.rightChannel.delay.v;
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

        private void param_ValueChanged(object sender, EventArgs e)
        {
            if (sender.Equals(this.paramLeftChDelay))
            {
                this.output.leftChannel.delay.v = this.paramLeftChDelay.v;
                this.eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.MonitorPanel, OutputParam.LeftChDelay));
            }
            else if (sender.Equals(this.paramRightChDelay))
            {
                this.output.rightChannel.delay.v = this.paramRightChDelay.v;
                this.eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.MonitorPanel, OutputParam.RightChDelay));
            }
            if (sender.Equals(this.chkLeftMute))
            {
                this.output.leftChannel.mute.value = this.chkLeftMute.isChecked;
                this.eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.MonitorPanel, OutputParam.LeftChMute));
                this.eventBus.NotifyObeserver(new OutputMuteChangedFromPanelArgs(output.outputChannelType));
            }
            else if (sender.Equals(this.chkRightMute))
            {
                this.output.rightChannel.mute.value = this.chkRightMute.isChecked;
                this.eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.MonitorPanel, OutputParam.RightChMute));
                this.eventBus.NotifyObeserver(new OutputMuteChangedFromPanelArgs(output.outputChannelType));
            }
            else if(sender.Equals(this.chkLeftPhase))
            {
                this.output.leftChannel.phase.value = !this.chkLeftPhase.isChecked;
                this.eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.MonitorPanel, OutputParam.LeftChPhase));
                this.UpdateLeftChPhaseText();
            }
            else if (sender.Equals(this.chkRightPhase))
            {
                this.output.rightChannel.phase.value = !this.chkRightPhase.isChecked;
                this.eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.MonitorPanel, OutputParam.RightChPhase));
                this.UpdateRightChPhaseText();
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

        public override void UpdateUIFromData()
        {
            this.panelCL.UpdateUIFromData();
            this.paramLeftChDelay.initV = this.output.leftChannel.delay.v;
            this.paramRightChDelay.initV = this.output.rightChannel.delay.v;
            this.chkLeftMute.isChecked = this.output.leftChannel.mute.value;
            this.chkRightMute.isChecked = this.output.rightChannel.mute.value;
            this.UpdateLeftChPhaseText();
            this.UpdateRightChPhaseText();
            base.UpdateUIFromData();
        }

        public override void UpdateEffectorModel()
        {
            this.panelCL.UpdateThresholdUI();

            if (base.effectorThreshold == null)
            {
                return;
            }

            if (this.levelMeterLeftTicks != null)
            {
                this.levelMeterLeftTicks.background = KXLevelMeterImageProvider.GetLevelMeterLeftTicks(base.effectorThreshold.maxLevel);
            }

            if (this.levelMeterRightTicks != null)
            {
                this.levelMeterRightTicks.background = KXLevelMeterImageProvider.GetLevelMeterRightTicks(base.effectorThreshold.maxLevel);
            }
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.lblLeftDelay.Text = localization[Localization.panelOutput_LeftChDelay];
            this.lblRightDelay.Text = localization[Localization.panelOutput_RightChDelay];
            this.chkLeftMute.Text = localization[Localization.panelOutput_LeftChMute];
            this.chkRightMute.Text = localization[Localization.panelOutput_RightChMute];

            this.UpdateLeftChPhaseText();
            this.UpdateRightChPhaseText();

            this.panelCL.UpdateDisplayLanguage();

            this.panelDelay.Invalidate();
            this.Invalidate();
        }

        private void panelDelay_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.popMenuChannelDelayUint.Show();
            }
        }
    }
}
