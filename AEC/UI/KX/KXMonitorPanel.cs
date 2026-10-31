using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using AEC.Core;
using AEC.Effectors;
using AEC.Comm;
using AEC.Common;
using AEC.Effectors.KX;
using AEC.Effectors.KX.Modules;
using AEC.UI.Controls;
using AEC.Misc;
using AEC.UI;

namespace AEC.UI.KX
{
    public partial class KXMonitorPanel : PanelBase
    {
        private MusicModule music;
        private MicModule mic;
        private StereoOutputModule mainOutput;
        private MonoOutputModule aux1Output;
        private MonoOutputModule aux2Output;
        private StereoOutputModule surOutput;

        private SynchronizationContext syncContext;

        private const int readSystemStatusInterval = 2 * 16;
        private System.Windows.Forms.Timer readSystemStatusTimer;

        public KXMonitorPanel()
        {
            InitializeComponent();
            InitFont();

            this.syncContext = WindowsFormsSynchronizationContext.Current;
        }

        private void InitFont()
        {
            this.Font = FontHelper.CreateFont10();
        }

        private bool monitorIsON
        {
            set
            {
                this.panelMonitorMusicInput.monitorIsON = value;
                this.panelMonitorMicInput.monitorIsON = value;

                this.panelMonitorMainOutput.monitorIsON = value;
                this.panelMonitorAux1Output.monitorIsON = value;
                this.panelMonitorAux2Output.monitorIsON = value;
                this.panelMonitorSurOutput.monitorIsON = value;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (base.mController.isConnected)
            {
                this.monitorIsON = true;
            }
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.kxEffectorModel != null);

            this.music = base.mController.kxEffectorModel.music;
            this.mic = base.mController.kxEffectorModel.mic;
            this.mainOutput = base.mController.kxEffectorModel.mainOutput;
            this.aux1Output = base.mController.kxEffectorModel.aux1Output;
            this.aux2Output = base.mController.kxEffectorModel.aux2Output;
            this.surOutput = base.mController.kxEffectorModel.surOutput;

            this.panelMonitorMusicInput.BindingModule(this.music, base.mController.mEffectorThreshold);
            this.panelMonitorMicInput.BindingModule(this.mic, base.mController.mEffectorThreshold);
            this.panelMonitorMainOutput.Binding(this.mainOutput, eventBus_, base.mController.mEffectorThreshold);
            this.panelMonitorAux1Output.Binding(this.aux1Output, eventBus_, base.mController.mEffectorThreshold);
            this.panelMonitorAux2Output.Binding(this.aux2Output, eventBus_, base.mController.mEffectorThreshold);
            this.panelMonitorSurOutput.Binding(this.surOutput, eventBus_, base.mController.mEffectorThreshold);

            UpdateEffectorModel();

            base.mController.controllerEvent.RegisterHandler(CommEventHandler);

            if (base.mController.isConnected)
            {
                this.monitorIsON = true;
                StartReadSystemStatus();
            }
        }

        private void CommEventHandler(object sender, EventBusArgs e)
        {
            syncContext.Post(CommEventHandlerCallback, e);
        }

        private void CommEventHandlerCallback(object state)
        {
            EventBusArgs e = state as EventBusArgs;
            if (e.type == EventType.DeviceStatusLoaded)
            {
                SystemStatusLoadedHandler((e as DeviceStatusLoadedArgs).deviceStatus);
            }
            else if(e.type == EventType.CommStateChanged)
            {
                if (base.mController != null)
                {
                    this.monitorIsON = base.mController.isConnected;

                    if (base.mController.isConnected)
                    {
                        StartReadSystemStatus();
                    }
                    else
                    {
                        StopReadSystemStatus();
                    }
                }
            }
        }

        private void SystemStatusLoadedHandler(DeviceStatus deviceStatus)
        {
            if (deviceStatus != null)
            {
                //Log.OutputLine("music left: {0} ({1}dB) , right: {2} ({3}dB)",
                //    deviceStatus.musicInputLevel[0],
                //    AEC.Core.DSP.DSPHelper.LinearToDecibels(deviceStatus.musicInputLevel[0]),
                //    deviceStatus.musicInputLevel[1],
                //    AEC.Core.DSP.DSPHelper.LinearToDecibels(deviceStatus.musicInputLevel[1]));

                //Log.OutputLine("mic A: {0} ({1}dB) , B: {2} ({3}dB), delta = {4} dB, Comp = {5} dB",
                //    deviceStatus.micInputLevel[0],
                //    AEC.Core.DSP.DSPHelper.LinearToDecibels(deviceStatus.micInputLevel[0]),
                //    deviceStatus.micInputLevel[1],
                //    AEC.Core.DSP.DSPHelper.LinearToDecibels(deviceStatus.micInputLevel[1]),
                //    Math.Abs(AEC.Core.DSP.DSPHelper.LinearToDecibels(deviceStatus.micInputLevel[0]) - AEC.Core.DSP.DSPHelper.LinearToDecibels(deviceStatus.micInputLevel[1])), deviceStatus.micCompressor);

                //Log.OutputLine("main output Left : {0} dB, Right: {1} dB, Limiter = {2} dB", deviceStatus.outputLevel[0], deviceStatus.outputLevel[1], deviceStatus.ouputLimiter[0]);
                //Log.OutputLine("aux1 output : {0} dB, Limiter = {1} dB", deviceStatus.outputLevel[2], deviceStatus.ouputLimiter[1]);
                //Log.OutputLine("aux2 output : {0} dB, Limiter = {1} dB", deviceStatus.outputLevel[3], deviceStatus.ouputLimiter[2]);
                //Log.OutputLine("sur output Left: {0} dB, Right: {1} dB, Limiter = {2} dB", deviceStatus.outputLevel[4], deviceStatus.outputLevel[5], deviceStatus.ouputLimiter[3]);

                this.panelMonitorMusicInput.UpdateStatus(deviceStatus.musicInputLeft, deviceStatus.musicInputRight);
                this.panelMonitorMicInput.UpdateStatus(deviceStatus.micInputA, deviceStatus.micInputB, deviceStatus.micCompressor);
                this.panelMonitorMainOutput.UpdateStatus(deviceStatus.mainOutputLeft, deviceStatus.mainOutputRight, deviceStatus.mainOutputLimiter);
                this.panelMonitorAux1Output.UpdateStatus(deviceStatus.aux1Output, deviceStatus.aux1OutputLimiter);
                this.panelMonitorAux2Output.UpdateStatus(deviceStatus.aux2Output, deviceStatus.aux2OutputLimiter);
                this.panelMonitorSurOutput.UpdateStatus(deviceStatus.surOutputLeft, deviceStatus.surOutputRight, deviceStatus.surOutputLimiter);

                //this.panelMonitorMainOutput.UpdateStatus(quadChannelLimiterStatus[0]);
                //this.panelMonitorAux1Output.UpdateStatus(quadChannelLimiterStatus[1]);
                //this.panelMonitorAux2Output.UpdateStatus(quadChannelLimiterStatus[2]);
                //this.panelMonitorSurOutput.UpdateStatus(quadChannelLimiterStatus[3]);
            }
        }

        public override void UpdateUIFromData()
        {
            this.panelMonitorMusicInput.UpdateUIFromData();
            this.panelMonitorMicInput.UpdateUIFromData();

            this.panelMonitorMainOutput.UpdateUIFromData();
            this.panelMonitorAux1Output.UpdateUIFromData();
            this.panelMonitorAux2Output.UpdateUIFromData();
            this.panelMonitorSurOutput.UpdateUIFromData();

            //this.panelMonitorMainOutput.Invalidate();
            //this.panelMonitorAux1Output.Invalidate();
            //this.panelMonitorAux2Output.Invalidate();
            //this.panelMonitorSurOutput.Invalidate();
        }

        public override void UpdateEffectorModel()
        {
            this.panelMonitorMusicInput.UpdateEffectorModel();
            this.panelMonitorMicInput.UpdateEffectorModel();
            this.panelMonitorMainOutput.UpdateEffectorModel();
            this.panelMonitorAux1Output.UpdateEffectorModel();
            this.panelMonitorAux2Output.UpdateEffectorModel();
            this.panelMonitorSurOutput.UpdateEffectorModel();
        }

        public override void UpdateDisplayLanguage()
        {
            this.panelMonitorMusicInput.Text = localization[Localization.formProMode_Music];
            this.panelMonitorMicInput.Text = localization[Localization.formProMode_Mic];
            this.panelMonitorMainOutput.Text = localization[Localization.formProMode_MainOutput];
            this.panelMonitorAux1Output.Text = localization[Localization.formProMode_Aux1Output];
            this.panelMonitorAux2Output.Text = localization[Localization.formProMode_Aux2Output];
            this.panelMonitorSurOutput.Text = localization[Localization.formProMode_SurOutput];

            this.panelMonitorMainOutput.UpdateDisplayLanguage();
            this.panelMonitorAux1Output.UpdateDisplayLanguage();
            this.panelMonitorAux2Output.UpdateDisplayLanguage();
            this.panelMonitorSurOutput.UpdateDisplayLanguage();

            this.panelMonitorMusicInput.Invalidate();
            this.panelMonitorMicInput.Invalidate();
            this.panelMonitorMainOutput.Invalidate();
            this.panelMonitorAux1Output.Invalidate();
            this.panelMonitorAux2Output.Invalidate();
            this.panelMonitorSurOutput.Invalidate();
        }
        
        private void StartReadSystemStatus()
        {
            if (this.readSystemStatusTimer == null)
            {
                this.readSystemStatusTimer = new System.Windows.Forms.Timer();
                this.readSystemStatusTimer.Interval = readSystemStatusInterval;
                this.readSystemStatusTimer.Tick += readSystemStatusTimer_Tick;
            }

            this.readSystemStatusTimer.Enabled = true;
        }

        private void StopReadSystemStatus()
        {
            if (this.readSystemStatusTimer != null && this.readSystemStatusTimer.Enabled)
            {
                this.readSystemStatusTimer.Enabled = false;
            }
        }

        private int tickCount = 0;
        private const int ReadStatusCount = 2;

        private void readSystemStatusTimer_Tick(object sender, EventArgs e)
        {
            if (this.mController.currEffectorFamily != MEffectorFamily.KX || !this.Visible)
            {
                return;
            }

            //Tools.LogHelper.WriteDebug("KXMonitorPanel.readSystemStatusTimer_Tick()...");

            //this.monitorIsON = true;

            this.panelMonitorMusicInput.DoLevelFreeFall();
            this.panelMonitorMicInput.DoLevelFreeFall();
            this.panelMonitorMainOutput.DoLevelFreeFall();
            this.panelMonitorAux1Output.DoLevelFreeFall();
            this.panelMonitorAux2Output.DoLevelFreeFall();
            this.panelMonitorSurOutput.DoLevelFreeFall();

            //在K系列产品上需要读取系统状态
            if (++tickCount % ReadStatusCount == 0)
            {
                if(tickCount == ReadStatusCount)
                {
                    tickCount = 0;
                }
                
                base.mController.ReadSystemSatatus();
            }
        }

        private void panel_EQThumbnailMouseClick(object sender, EventArgs e)
        {
            KXMonitorModulePanelBase monitorModulePanel = sender as KXMonitorModulePanelBase;
            if (monitorModulePanel != null && monitorModulePanel.module != null && this._eventBus != null)
            {
                this._eventBus.NotifyObeserver(new EQGraphicPanelJumpArgs(monitorModulePanel.module));
            }
        }
    }
}
