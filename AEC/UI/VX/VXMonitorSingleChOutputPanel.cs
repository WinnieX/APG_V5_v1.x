using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AEC.Core;
using AEC.Effectors;
using AEC.Effectors.VX.Modules;
using AEC.Misc;
using AEC.Common;

namespace AEC.UI.VX
{
    public partial class VXMonitorSingleChOutputPanel : UserControl
    {
        private AuxOutputModule output;
        private EventBus eventBus;

        private LimiterStatusUIValue limiterStatusUIValue;

        public bool monitorIsON
        {
            set
            {
                this.ratioIndicator.isOn = value;
                this.levelIndicator.isOn = value;
            }
        }

        public void UpdateStatus(LimiterStatus status)
        {
            if (this.limiterStatusUIValue != null)
            {
                this.limiterStatusUIValue.Update(status);

                this.levelIndicator.level = this.limiterStatusUIValue.levelPeakHold;
                this.ratioIndicator.level = this.limiterStatusUIValue.compressed;
            }
        }

        public VXMonitorSingleChOutputPanel()
        {
            InitializeComponent();
            Init();
            InitFont();
            this.paramDelay.ValueChanged += param_ValueChanged;
            this.chkMute.CheckedChanged += param_ValueChanged;
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

        public void Binding(AuxOutputModule output_, EventBus eventBus_, MEffectorThreshold effectorThreshold_)
        {
            System.Diagnostics.Debug.Assert(output_ != null && eventBus_ != null && effectorThreshold_ != null);

            this.output = output_;
            this.eventBus = eventBus_;

            this.panelCL.Binding(this.output, eventBus_, this.output.channel, effectorThreshold_);
            this.paramDelay.Init(OutputModule.MaxDelay, this.output.delay.v, OutputBaseModule.GetDelayLabel, OutputBaseModule.CalcDelayIndexFromText);
            this.chkMute.isChecked = this.output.mute.value;

            this.limiterStatusUIValue = new LimiterStatusUIValue(this.output.limiter);
            
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

                        if (op.channel == output.channel && op.source == OutputParamChangedSource.OutputPanel)
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
                    this.paramDelay.initV = this.output.delay.v;
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

        private void param_ValueChanged(object sender, EventArgs e)
        {
            if (sender.Equals(this.paramDelay))
            {
                this.output.delay.v = this.paramDelay.v;
                this.eventBus.NotifyObeserver(new OutputParamChangedArgs(output.channel, OutputParamChangedSource.MonitorPanel, OutputParam.Delay));
            }
            if (sender.Equals(this.chkMute))
            {
                this.output.mute.value = this.chkMute.isChecked;
                this.eventBus.NotifyObeserver(new OutputParamChangedArgs(output.channel, OutputParamChangedSource.MonitorPanel, OutputParam.Mute));
                this.eventBus.NotifyObeserver(new OutputMuteChangedFromPanelArgs(output.channel));
            }
        }

        public void UpdateUIFromData()
        {
            this.panelCL.UpdateUIFromData();
            this.paramDelay.initV = this.output.delay.v;
            this.chkMute.isChecked = this.output.mute.value;
        }

        public void UpdateEffectorModel()
        {
            this.panelCL.UpdateThresholdUI();
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.lblDelay.Text = localization[Localization.panelOutput_Delay];
            this.chkMute.Text = localization[Localization.panelOutput_Mute];

            this.panelCL.UpdateDisplayLanguage();

            this.panelDelay.Invalidate();
            this.Invalidate();
        }
    }
}
