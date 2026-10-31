using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using AEC.Core;
using AEC.Effectors;
using AEC.Effectors.VKxSCM;
using AEC.Effectors.VKxSCM.Modules;
using AEC.Common;
using AEC.Comm;
using AEC.Misc;

namespace AEC.UI.VKxSCM
{
    public partial class VKxSCMMicPanel : PanelBase
    {
        private MicModule mic;

        public VKxSCMMicPanel()
            : base()
        {
            InitializeComponent();
            InitFont();

            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true); //默认启动双缓冲
            //SetStyle(ControlStyles.DoubleBuffer, true);
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.ResizeRedraw, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.

            //this.syncContext = WindowsFormsSynchronizationContext.Current;
            this.InitControl(this.eqGroupPanel, this.hpfFreqSlier, this.lpfFreqSlier);
            this.eqResetAndByPassPanel.ResetEQ += base.ResetEQGroup;
            this.eqResetAndByPassPanel.EQPassChanged += base.SwitchEQByPass;
            //this.micLevel.Init(micLevelPeak, thresholdLevel);
            //this.micLevel.isShowLevel = true;
            //this.micLevel.level = 10;
        }

        private void InitFont()
        {
            this.panelNoiseGate.Font = FontHelper.CreateFont10();
            this.panelFbe.Font = FontHelper.CreateFont10();
            this.panelXPF.Font = FontHelper.CreateFont10();
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.vkxscmEffectorModel != null);

            this.mic = base.mController.vkxscmEffectorModel.mic;
            base.BindingModule(this.mic, MEffectorFamily.VKxSCM);

            this.cmbLpfType.SelectedIndex = this.mic.lpf.typeIndex;

            this.micFbe.Binding(mic.fbe, ParamGetLabelHelper.ClassicMicFbeGetLabel);
            this.micNoiseGate.Binding(mic.micNoiseGate, ParamGetLabelHelper.MicNoiseGateGetLabel);
            this.compressorPanel.Binding(mic.compressor, base.mController.mEffectorThreshold);
            this.eqResetAndByPassPanel.Init(this.mic.eqGroup.isBypass, this.mic.themeColor);


            this.RegisterCmbSelectedIndexChangedHandler();
        }

        public override void UpdateUIFromData()
        {
            base.UpdateUIFromData();

            this.RemoveCmbSelectedIndexChangedHandler();
            this.cmbLpfType.SelectedIndex = this.mic.lpf.typeIndex;
            this.RegisterCmbSelectedIndexChangedHandler();
            
            this.micFbe.UpdateUIFromData();
            this.micNoiseGate.UpdateUIFromData();
            this.compressorPanel.UpdateUIFromData();
            this.eqResetAndByPassPanel.isPass = this.mic.eqGroup.isBypass;
        }

        private void RegisterCmbSelectedIndexChangedHandler()
        {
            this.cmbLpfType.SelectedIndexChanged += cmb_SelectedIndexChanged;
        }

        private void RemoveCmbSelectedIndexChangedHandler()
        {
            this.cmbLpfType.SelectedIndexChanged -= cmb_SelectedIndexChanged;
        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            gtComboBox cmb = sender as gtComboBox;
            if(sender.Equals(this.cmbLpfType))
            {
                this.mic.lpf.typeIndexParam = (short)this.cmbLpfType.SelectedIndex;
                this.mic.UpdateXPFromControlPanel(XPFilterType.LowPass, FilterElement.Type);
            }
        }

        public override void UpdateDisplayLanguage()
        {
            this.RemoveCmbSelectedIndexChangedHandler();

            this.RegisterCmbSelectedIndexChangedHandler();

            this.panelNoiseGate.title = localization[Localization.panelMic_MicNoiseGate];
            this.panelFbe.title = localization[Localization.panelMic_MicFBE];
            this.lblMicHpfFreq.Text = localization[Localization.panelMic_MicHpf];
            this.lblMicLpfFreq.Text = localization[Localization.panelMic_MicLpf];
            this.lblMicLpfType.Text = localization[Localization.panelMic_MicLpfType];
            this.compressorPanel.UpdateDisplayLanguage();
            this.eqGroupPanel.UpdateDisplayLanguage();
            this.eqResetAndByPassPanel.UpdateDisplayLanguage();

            this.Invalidate();
        }

        public override void UpdateEffectorModel()
        {
            this.compressorPanel.UpdateEffectorModel();
        }

        protected override void XPFChanged(XPFilterType filterType, FilterElement filterElement)
        {
            if (filterType == XPFilterType.LowPass)
            {
                if (filterElement == FilterElement.Type)
                {
                    this.RemoveCmbSelectedIndexChangedHandler();
                    this.cmbLpfType.SelectedIndex = this.mic.lpf.typeIndex;
                    this.RegisterCmbSelectedIndexChangedHandler();
                }
            }
        }

        //private void EventHandlerChanged(object sender, EventBusArgs e)
        //{
        //   if(e.type == EventType.ShowSignalLevel)
        //   {
        //       this.micLevel.isShowLevel = (e as ShowSignalLevelArgs).isShow;
        //   }
        //}

        //private void CommStateChanged(object sender, EventBusArgs e)
        //{
        //    CommEventHandler(e);
        //}

        //private void CommEventHandler(EventBusArgs e)
        //{
        //    syncContext.Post(ProcessCommEvent, e);
        //}

        //private void ProcessCommEvent(object state)
        //{
        //    EventBusArgs e = state as EventBusArgs;
        //    //if (e.type == EventType.CommStateChanged || e.type == EventType.CommInterrupted)
        //    if (e.type == EventType.CommStateChanged)
        //    {
        //        //AEC.Log.OutputLine("SystemThread:{0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
        //        if (!this._commSvc.isConnected)
        //        {
        //            this.micLevel.isShowLevel = false;
        //        }
        //    }
        //    //else if (e.type == EventType.SystemLevelLoaded)
        //    //{
        //    //    if(!this.Visible)
        //    //    {
        //    //        return;
        //    //    }

        //    //    //SystemLevel systemLevel = (e as SystemLevelLoadedArgs).systemLevel;
        //    //    //AEC.Log.OutputLine("level:{0}", systemLevel.rawMicLevel);
        //    //    //systemLevel.Test();

        //    //    //AEC.Log.OutputLine("Music Level:{0}dB, Mic Level:{1}dB, Effect Level:{2}dB, Mic Raw Level:{3}dBu",
        //    //    //    systemLevel.musicLevel,
        //    //    //    systemLevel.micLevel,
        //    //    //    systemLevel.effectLevel,
        //    //    //    systemLevel.rawMicLevel);

        //    //    //systemLevel.musicLevel = 0;

        //    //    if (micLevelPeak.Update(systemLevel.rawMicLevel) || micLevelPeak.CheckPeakFall())
        //    //    {
        //    //        this.micLevel.level = micLevelPeak.peak;
        //    //    }
        //    //}
        //}

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    Log.OutputLine("Mic Panel Paint");
        //}

        //private int count = 0;
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    Log.OutputLine("Mic Panel Paint:{0}", ++count);
        //}
    }
}
