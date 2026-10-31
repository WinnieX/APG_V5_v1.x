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
using AEC.Effectors.VX;
using AEC.Effectors.VX.Modules;
using AEC.Comm;
using AEC.Common;
using AEC.UI.Controls;
using AEC.Misc;

namespace AEC.UI.VX
{
    public partial class VXMonitorPanel : PanelBase
    {
        private OutputModule mainOutput;
        private AuxOutputModule aux1Output;
        private AuxOutputModule aux2Output;
        private OutputModule surOutput;

        private Dictionary<EQThumbnail, ModuleBase> eqThumbnail2ModuleMap;

        private SynchronizationContext syncContext;

        private const int readSystemStatusInterval = 200;
        private System.Windows.Forms.Timer readSystemStatusTimer;

        public VXMonitorPanel()
        {
            InitializeComponent();
            InitLightTheme();
            InitFont();

            this.syncContext = WindowsFormsSynchronizationContext.Current;
        }

        private void InitLightTheme()
        {
            Color normalColor1 = Color.FromArgb(224, 224, 220);
            Color normalColor2 = Color.FromArgb(242, 242, 236);
            Color hoverColor1 = Color.FromArgb(205, 231, 245);
            Color hoverColor2 = Color.FromArgb(185, 221, 240);
            Color pressedColor1 = Color.FromArgb(181, 213, 232);
            Color pressedColor2 = Color.FromArgb(159, 202, 226);

            InitThumbnailColors(this.eqThumbnailMainOutput, normalColor1, normalColor2, hoverColor1, hoverColor2, pressedColor1, pressedColor2);
            InitThumbnailColors(this.eqThumbnailAux1Output, normalColor1, normalColor2, hoverColor1, hoverColor2, pressedColor1, pressedColor2);
            InitThumbnailColors(this.eqThumbnailAux2Output, normalColor1, normalColor2, hoverColor1, hoverColor2, pressedColor1, pressedColor2);
            InitThumbnailColors(this.eqThumbnailSurOutput, normalColor1, normalColor2, hoverColor1, hoverColor2, pressedColor1, pressedColor2);
        }

        private static void InitThumbnailColors(
            EQThumbnail thumbnail,
            Color normalColor1,
            Color normalColor2,
            Color hoverColor1,
            Color hoverColor2,
            Color pressedColor1,
            Color pressedColor2)
        {
            thumbnail.SetBackgroundColors(normalColor1, normalColor2, hoverColor1, hoverColor2, pressedColor1, pressedColor2);
            thumbnail.fillColor = normalColor2;
            thumbnail.NormalBorderColor = Color.FromArgb(200, 200, 190);
            thumbnail.NormalFillColor = normalColor2;
            thumbnail.NormalTextColor = Color.FromArgb(32, 32, 64);
            thumbnail.HoverBorderColor = Color.FromArgb(0, 145, 234);
            thumbnail.HoverFillColor = hoverColor2;
            thumbnail.HoverTextColor = Color.FromArgb(32, 32, 64);
            thumbnail.PressedBorderColor = Color.FromArgb(0, 96, 168);
            thumbnail.PressedFillColor = pressedColor2;
            thumbnail.PressedTextColor = Color.FromArgb(32, 32, 64);
            thumbnail.DisabledBorderColor = Color.FromArgb(168, 168, 168);
            thumbnail.DisabledFillColor = Color.FromArgb(208, 208, 204);
            thumbnail.DisabledTextColor = Color.FromArgb(128, 128, 128);
        }

        private void InitFont()
        {
            this.Font = FontHelper.CreateFont10();
        }

        private bool monitorIsON
        {
            set
            {
                this.panelMonitorMainOutput.monitorIsON = value;
                this.panelMonitorAux1Output.monitorIsON = value;
                this.panelMonitorAux2Output.monitorIsON = value;
                this.panelMonitorSurOutput.monitorIsON = value;
            }
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.vxEffectorModel != null);

            this.mainOutput = base.mController.vxEffectorModel.mainOutput;
            this.aux1Output = base.mController.vxEffectorModel.aux1Output;
            this.aux2Output = base.mController.vxEffectorModel.aux2Output;
            this.surOutput = base.mController.vxEffectorModel.surOutput;

            this.eqThumbnailMainOutput.BindingModule(this.mainOutput);
            this.eqThumbnailAux1Output.BindingModule(this.aux1Output);
            this.eqThumbnailAux2Output.BindingModule(this.aux2Output);
            this.eqThumbnailSurOutput.BindingModule(this.surOutput);

            this.panelMonitorMainOutput.Binding(this.mainOutput, eventBus_, base.mController.mEffectorThreshold);
            this.panelMonitorAux1Output.Binding(this.aux1Output, eventBus_, base.mController.mEffectorThreshold);
            this.panelMonitorAux2Output.Binding(this.aux2Output, eventBus_, base.mController.mEffectorThreshold);
            this.panelMonitorSurOutput.Binding(this.surOutput, eventBus_, base.mController.mEffectorThreshold);

            this.eqThumbnail2ModuleMap = new Dictionary<EQThumbnail, ModuleBase>()
            {
                {this.eqThumbnailMainOutput, this.mainOutput},
                {this.eqThumbnailAux1Output, this.aux1Output},
                {this.eqThumbnailAux2Output, this.aux2Output},
                {this.eqThumbnailSurOutput, this.surOutput},
            };

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
            if (e.type == EventType.OutputLimiterStatusLoaded)
            {
                SystemStatusLoadedHandler((e as OutputLimiterStatusLoadedArgs).limiterStatus);
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

        private void SystemStatusLoadedHandler(QuadChannelLimiterStatus quadChannelLimiterStatus)
        {
            if (quadChannelLimiterStatus != null)
            {
                this.panelMonitorMainOutput.UpdateStatus(quadChannelLimiterStatus[0]);
                this.panelMonitorAux1Output.UpdateStatus(quadChannelLimiterStatus[1]);
                this.panelMonitorAux2Output.UpdateStatus(quadChannelLimiterStatus[2]);
                this.panelMonitorSurOutput.UpdateStatus(quadChannelLimiterStatus[3]);
            }
        }

        public override void UpdateUIFromData()
        {
            this.eqThumbnailMainOutput.Invalidate();
            this.eqThumbnailAux1Output.Invalidate();
            this.eqThumbnailAux2Output.Invalidate();
            this.eqThumbnailSurOutput.Invalidate();

            this.panelMonitorMainOutput.UpdateUIFromData();
            this.panelMonitorAux1Output.UpdateUIFromData();
            this.panelMonitorAux2Output.UpdateUIFromData();
            this.panelMonitorSurOutput.UpdateUIFromData();
        }

        public override void UpdateEffectorModel()
        {
            this.panelMonitorMainOutput.UpdateEffectorModel();
            this.panelMonitorAux1Output.UpdateEffectorModel();
            this.panelMonitorAux2Output.UpdateEffectorModel();
            this.panelMonitorSurOutput.UpdateEffectorModel();
        }

        public override void UpdateDisplayLanguage()
        {
            this.panelMainOutput.title = localization[Localization.formProMode_MainOutput];
            this.panelAux1Output.title = localization[Localization.formProMode_Aux1Output];
            this.panelAux2Output.title = localization[Localization.formProMode_Aux2Output];
            this.panelSurOutput.title = localization[Localization.formProMode_SurOutput];

            this.panelMonitorMainOutput.UpdateDisplayLanguage();
            this.panelMonitorAux1Output.UpdateDisplayLanguage();
            this.panelMonitorAux2Output.UpdateDisplayLanguage();
            this.panelMonitorSurOutput.UpdateDisplayLanguage();

            this.panelMainOutput.Invalidate();
            this.panelAux1Output.Invalidate();
            this.panelAux2Output.Invalidate();
            this.panelSurOutput.Invalidate();
        }

        private void eqThumbnail_MouseClick(object sender, MouseEventArgs e)
        {
            EQThumbnail eqThumbnail = sender as EQThumbnail;
            if (eqThumbnail == null || this.eqThumbnail2ModuleMap == null)
            {
                return;
            }

            if (this.eqThumbnail2ModuleMap.ContainsKey(eqThumbnail))
            {
                this._eventBus.NotifyObeserver(new EQGraphicPanelJumpArgs(this.eqThumbnail2ModuleMap[eqThumbnail]));
            }
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

        private void readSystemStatusTimer_Tick(object sender, EventArgs e)
        {
            //在V系列上需要读取系统状态
            if (this.mController.currEffectorFamily == MEffectorFamily.VX && this.Visible)
            {
                base.mController.ReadSystemSatatus();
            }
        }
    }
}
