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
    public partial class VXMonitorDualChOutputPanel : UserControl
    {
        private OutputModule output;
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
                //Log.OutputLine("levelIndicator:{0}dB, ratio:{1}dB", this.levelIndicator.level, this.ratioIndicator.level);
            }
        }
        
        public VXMonitorDualChOutputPanel()
        {
            InitializeComponent();
            Init();
            InitFont();
            this.paramLeftChDelay.ValueChanged += param_ValueChanged;
            this.paramRightChDelay.ValueChanged += param_ValueChanged;
            this.chkLeftMute.CheckedChanged += param_ValueChanged;
            this.chkRightMute.CheckedChanged += param_ValueChanged;
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
        }

        public void Binding(OutputModule output_, EventBus eventBus_, MEffectorThreshold effectorThreshold_)
        {
            System.Diagnostics.Debug.Assert(output_ != null && eventBus_ != null);

            this.output = output_;
            this.eventBus = eventBus_;

            this.panelCL.Binding(this.output, eventBus_, this.output.channel, effectorThreshold_);
            this.paramLeftChDelay.Init(OutputModule.MaxDelay, this.output.leftChDelay.v, OutputBaseModule.GetDelayLabel, OutputBaseModule.CalcDelayIndexFromText);
            this.paramRightChDelay.Init(OutputModule.MaxDelay, this.output.rightChDelay.v, OutputBaseModule.GetDelayLabel, OutputBaseModule.CalcDelayIndexFromText);
            this.chkLeftMute.isChecked = this.output.leftChMute.value;
            this.chkRightMute.isChecked = this.output.rightChMute.value;

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
                            UpdateMuteBtns();
                        }
                    }
                    break;
            }
        }

        private void UpdateOutputParamUI(OutputParam param)
        {
            switch (param)
            {
                case OutputParam.LeftChDelay:
                    this.paramLeftChDelay.initV = this.output.leftChDelay.v;
                    break;

                case OutputParam.RightChDelay:
                    this.paramRightChDelay.initV = this.output.rightChDelay.v;
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

        private void param_ValueChanged(object sender, EventArgs e)
        {
            if (sender.Equals(this.paramLeftChDelay))
            {
                this.output.leftChDelay.v = this.paramLeftChDelay.v;
                this.eventBus.NotifyObeserver(new OutputParamChangedArgs(output.channel, OutputParamChangedSource.MonitorPanel, OutputParam.LeftChDelay));
            }
            else if (sender.Equals(this.paramRightChDelay))
            {
                this.output.rightChDelay.v = this.paramRightChDelay.v;
                this.eventBus.NotifyObeserver(new OutputParamChangedArgs(output.channel, OutputParamChangedSource.MonitorPanel, OutputParam.RightChDelay));
            }
            if (sender.Equals(this.chkLeftMute))
            {
                this.output.leftChMute.value = this.chkLeftMute.isChecked;
                this.eventBus.NotifyObeserver(new OutputParamChangedArgs(output.channel, OutputParamChangedSource.MonitorPanel, OutputParam.LeftChMute));
                this.eventBus.NotifyObeserver(new OutputMuteChangedFromPanelArgs(output.channel));
            }
            else if (sender.Equals(this.chkRightMute))
            {
                this.output.rightChMute.value = this.chkRightMute.isChecked;
                this.eventBus.NotifyObeserver(new OutputParamChangedArgs(output.channel, OutputParamChangedSource.MonitorPanel, OutputParam.RightChMute));
                this.eventBus.NotifyObeserver(new OutputMuteChangedFromPanelArgs(output.channel));
            }

        }

        public void UpdateUIFromData()
        {
            this.panelCL.UpdateUIFromData();
            this.paramLeftChDelay.initV = this.output.leftChDelay.v;
            this.paramRightChDelay.initV = this.output.rightChDelay.v;
            this.chkLeftMute.isChecked = this.output.leftChMute.value;
            this.chkRightMute.isChecked = this.output.rightChMute.value;
        }

        public void UpdateEffectorModel()
        {
            this.panelCL.UpdateThresholdUI();
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.lblLeftDelay.Text = localization[Localization.panelOutput_LeftChDelay];
            this.lblRightDelay.Text = localization[Localization.panelOutput_RightChDelay];
            this.chkLeftMute.Text = localization[Localization.panelOutput_LeftChMute];
            this.chkRightMute.Text = localization[Localization.panelOutput_RightChMute];

            this.panelCL.UpdateDisplayLanguage();

            this.panelDelay.Invalidate();
            this.Invalidate();
        }
    }
}
