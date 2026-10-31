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
    public partial class KXMonitorMonoOutputControlPanel : AEC.UI.KX.KXMonitorOutputPanelBase
    {
        private const int LayoutLevelMeterX = 106;
        private const int LayoutTicksX = 124;

        private MonoOutputModule output;
        private EventBus eventBus;

        private DUILevelMeter levelMeter;
        private DUIPicture levelMeterTicks;
        private DUILevelMeter limiterMeter;
        private DUIPicture limiterTicks;

        private LinearLevelIndicator levelIndicator = new LinearLevelIndicator();
        private DynamicValueIndicator limiterIndicator = new DynamicValueIndicator();

        private PopMenuChannelDelayUint popMenuChannelDelayUint;

        public override bool monitorIsON
        {
            set
            {
                if (this.levelMeter != null)
                {
                    this.levelMeter.isOn = value;
                }
                if (this.limiterMeter != null)
                {
                    this.limiterMeter.isOn = value;
                }

                if (!value)
                {
                    this.levelIndicator.Clear();
                    this.limiterIndicator.Clear();
                }

                this.Invalidate();
            }
        }

        public KXMonitorMonoOutputControlPanel()
        {
            InitializeComponent();
            Init();
            InitFont();
            this.paramDelay.ValueChanged += param_ValueChanged;
            this.chkMute.CheckedChanged += param_ValueChanged;
            this.chkPhase.CheckedChanged += param_ValueChanged;

            this.components = new System.ComponentModel.Container();
            this.popMenuChannelDelayUint = new PopMenuChannelDelayUint(this.components);
        }

        protected override void OnInitDuiControls()
        {
            base.OnInitDuiControls();

            if (this.levelMeter == null)
            {
                this.levelMeter = new DUIMaxLevelMeter();
                this.levelMeter.ledLightImageModel = KXLevelMeterImageProvider.levelMeterLEDLightImageModel;
                this.levelMeter.levelBackground = KXLevelMeterImageProvider.GetLevelMeterBackground();
                this.levelMeter.levelForeground = KXLevelMeterImageProvider.GetLevelMeterForeground();
                this.levelMeter.Size = this.levelMeter.levelBackground.Size;
                this.levelMeter.Location = new Point(LayoutLevelMeterX, KXLevelMeterImageProvider.YOffsetFromTickToLed + LayoutLevelTicksY);
                this.levelMeter.Visible = true;
            }

            if (this.limiterMeter == null)
            {
                this.limiterMeter = new DUILevelMeter0_12dB();
                this.limiterMeter.ledLightImageModel = KXLevelMeterImageProvider.limiterLEDLightImageModel;
                this.limiterMeter.levelBackground = KXLevelMeterImageProvider.GetLimiterMeterBackground();
                this.limiterMeter.levelForeground = KXLevelMeterImageProvider.GetLimiterMeterForeground();
                this.limiterMeter.Size = this.limiterMeter.levelBackground.Size;
                this.limiterMeter.Location = new Point(LayoutLevelMeterX, KXLevelMeterImageProvider.YOffsetFromTickToLed + LayoutLimiterTicksY);
                this.limiterMeter.Visible = true;
            }

            if (this.levelMeterTicks == null)
            {
                this.levelMeterTicks = new DUIPicture();
                this.levelMeterTicks.background = KXLevelMeterImageProvider.GetLevelMeterRightTicks(base.effectorThreshold.maxLevel);
                this.levelMeterTicks.Size = this.levelMeterTicks.background.Size;
                this.levelMeterTicks.Location = new Point(LayoutTicksX, LayoutLevelTicksY);
                this.levelMeterTicks.Visible = true;
            }

            if (this.limiterTicks == null)
            {
                this.limiterTicks = new DUIPicture();
                this.limiterTicks.background = KXLevelMeterImageProvider.GetLimiterMeterTicks();
                this.limiterTicks.Size = this.limiterTicks.background.Size;
                this.limiterTicks.Location = new Point(LayoutTicksX, LayoutLimiterTicksY);
                this.limiterTicks.Visible = true;
            }

            base.AddDuiControl(this.levelMeter);
            base.AddDuiControl(this.levelMeterTicks);
            base.AddDuiControl(this.limiterMeter);
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
            this.lblDelay.Font = FontHelper.CreateFont10();
            this.chkMute.Font = FontHelper.CreateFont10();
        }

        public void Binding(MonoOutputModule output_, EventBus eventBus_, MEffectorThreshold effectorThreshold_)
        {
            System.Diagnostics.Debug.Assert(output_ != null && eventBus_ != null && effectorThreshold_ != null);

            base.BindingModule(output_, effectorThreshold_);

            this.output = output_;
            this.eventBus = eventBus_;

            this.popMenuChannelDelayUint.BindingEventBus(eventBus_);

            this.panelCL.Binding(this.output, eventBus_, this.output.outputChannelType, effectorThreshold_);
            this.paramDelay.Init(StereoOutputModule.MaxDelayIndex, this.output.monoChannel.delay.v, OutputBaseModule.channelDelayParamLabelProvider.GetDelayLabel, OutputBaseModule.channelDelayParamLabelProvider.CalcDelayIndexFromText);
            this.chkMute.isChecked = this.output.monoChannel.mute.value;
            this.UpdatePhaseText();

            if (this.eventBus != null)
            {
                this.eventBus.RegisterHandler(EventBusHandler);
            }
        }

        public void UpdateStatus(double outputLevel, double limiterLevel)
        {
            Rectangle invalidRect = Rectangle.Empty;

            if(this.levelIndicator.UpdateStatus(this.levelMeter, outputLevel))
            {
                invalidRect = this.levelMeter.ClientRectangle;
            }

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
            if (this.levelMeter == null || this.limiterMeter == null)
            {
                return;
            }

            Rectangle invalidRect = Rectangle.Empty;

            if(this.levelIndicator.DoLevelFreeFall(this.levelMeter))
            {
                invalidRect = this.levelMeter.ClientRectangle;
            }

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
                            UpdateMuteBtn();
                        }
                    }
                    break;

                case EventType.ChannelDelayUnitChanged:
                    this.paramDelay.UpdateLabelDisplay();
                    break;
            }
        }

        private void UpdateOutputParamUI(OutputParam param)
        {
            switch (param)
            {
                case OutputParam.Delay:
                    this.paramDelay.initV = this.output.monoChannel.delay.v;
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

        private void param_ValueChanged(object sender, EventArgs e)
        {
            if (sender.Equals(this.paramDelay))
            {
                this.output.monoChannel.delay.v = this.paramDelay.v;
                this.eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.MonitorPanel, OutputParam.Delay));
            }
            else if (sender.Equals(this.chkPhase))
            {
                this.output.monoChannel.phase.value = !this.chkPhase.isChecked;
                this.UpdatePhaseText();
                this.eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.MonitorPanel, OutputParam.Phase));
            }
            else if (sender.Equals(this.chkMute))
            {
                this.output.monoChannel.mute.value = this.chkMute.isChecked;
                this.eventBus.NotifyObeserver(new OutputParamChangedArgs(output.outputChannelType, OutputParamChangedSource.MonitorPanel, OutputParam.Mute));
                this.eventBus.NotifyObeserver(new OutputMuteChangedFromPanelArgs(output.outputChannelType));
            }
        }

        private void UpdatePhaseText()
        {
            this.chkPhase.Text = string.Format("{0} {1}", localization[Localization.panelOutput_Phase], AEC.Core.Utils.PhaseToStr(this.output.monoChannel.phase.value));
            this.chkPhase.UpdateDisplay();
        }

        public override void UpdateUIFromData()
        {
            this.panelCL.UpdateUIFromData();
            this.paramDelay.initV = this.output.monoChannel.delay.v;
            this.chkMute.isChecked = this.output.monoChannel.mute.value;
            this.UpdatePhaseText();
            base.UpdateUIFromData();
        }

        public override void UpdateEffectorModel()
        {
            this.panelCL.UpdateThresholdUI();

            if (base.effectorThreshold == null)
            {
                return;
            }

            if (this.levelMeterTicks != null)
            {
                this.levelMeterTicks.background = KXLevelMeterImageProvider.GetLevelMeterLeftTicks(base.effectorThreshold.maxLevel);
            }
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.lblDelay.Text = localization[Localization.panelOutput_Delay];
            this.chkMute.Text = localization[Localization.panelOutput_Mute];

            this.UpdatePhaseText();

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
