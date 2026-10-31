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
using AEC.Effectors.VKxDM;
using AEC.Effectors.VKxDM.Modules;
using AEC.Common;
using AEC.Comm;
using AEC.Misc;

namespace AEC.UI.VKxDM
{
    public partial class VKxDMMicPanel : PanelBase
    {
        private readonly MicChannel micChannel = MicChannel.MicA;
        private MicModule mic;

        public VKxDMMicPanel(MicChannel micInput_)
            : base()
        {
            InitializeComponent();
            InitFont();

            micChannel = micInput_;

            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true); //默认启动双缓冲
            //SetStyle(ControlStyles.DoubleBuffer, true);
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.ResizeRedraw, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.

            //this.syncContext = WindowsFormsSynchronizationContext.Current;
            this.InitControl(this.eqGroupPanel, this.hpfFreqSlider, null);
            this.eqResetAndByPassPanel.ResetEQ += base.ResetEQGroup;
            this.eqResetAndByPassPanel.EQPassChanged += base.SwitchEQByPass;
            //this.micLevel.Init(micLevelPeak, thresholdLevel);
            //this.micLevel.isShowLevel = true;
            //this.micLevel.level = 10;

            //this.sliderMicLevel.ValueChanged += sliderMicLevel_ValueChanged;
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

            Debug.Assert(base.mController != null && base.mController.vkxsdmEffectorModel != null);

            this.mic = this.micChannel == MicChannel.MicA ? base.mController.vkxsdmEffectorModel.micA : base.mController.vkxsdmEffectorModel.micB;
            base.BindingModule(this.mic, MEffectorFamily.VKxDM);

            this.micFbe.Binding(mic.fbe, ParamGetLabelHelper.ClassicMicFbeGetLabel);

            this.sliderMicLevel.Binding(mic.micLevel, ParamGetLabelHelper.ClassicMicEffectLevelLabel);
            this.sliderEffectLevel.Binding(mic.effectLevel, ParamGetLabelHelper.ClassicMicEffectLevelLabel);
            
            this.micNoiseGate.Binding(mic.micNoiseGate, ParamGetLabelHelper.MicNoiseGateGetLabel);
            this.compressorPanel.Binding(mic.compressor, base.mController.mEffectorThreshold);
            this.eqResetAndByPassPanel.Init(this.mic.eqGroup.isBypass, this.mic.themeColor);

            this.RegisterCmbSelectedIndexChangedHandler();
        }

        public override void UpdateUIFromData()
        {
            base.UpdateUIFromData();

            this.micFbe.UpdateUIFromData();
            this.sliderMicLevel.UpdateUIFromData();
            this.sliderEffectLevel.UpdateUIFromData();
            this.micNoiseGate.UpdateUIFromData();
            this.compressorPanel.UpdateUIFromData();
            this.eqResetAndByPassPanel.isPass = this.mic.eqGroup.isBypass;
        }

        private void RegisterCmbSelectedIndexChangedHandler()
        {
            //this.cmbLpfType.SelectedIndexChanged += cmb_SelectedIndexChanged;
        }

        private void RemoveCmbSelectedIndexChangedHandler()
        {
            //this.cmbLpfType.SelectedIndexChanged -= cmb_SelectedIndexChanged;
        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            gtComboBox cmb = sender as gtComboBox;
            //if(sender.Equals(this.cmbLpfType))
            //{
            //    this.mic.lpf.typeIndexParam = (short)this.cmbLpfType.SelectedIndex;
            //    this.mic.UpdateXPFromControlPanel(XPFilterType.LowPass, FilterElement.Type);
            //}
        }

        public override void UpdateDisplayLanguage()
        {
            this.RemoveCmbSelectedIndexChangedHandler();

            this.RegisterCmbSelectedIndexChangedHandler();

            this.panelNoiseGate.title = localization[Localization.panelMic_MicNoiseGate];
            this.panelFbe.title = localization[Localization.panelMic_MicFBE];
            this.lblMicHpfFreq.Text = localization[Localization.panelMic_MicHpf];
            this.lblMicLevel.Text = localization[Localization.panelMic_MicLevel];
            this.lblEffectLevel.Text = localization[Localization.panelMic_EffectLevel];
            this.compressorPanel.UpdateDisplayLanguage();
            this.eqGroupPanel.UpdateDisplayLanguage();
            this.eqResetAndByPassPanel.UpdateDisplayLanguage();

            this.Invalidate();
        }

        public override void UpdateEffectorModel()
        {
            this.compressorPanel.UpdateEffectorModel();
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

    public class VKSxDMMicAPanel : VKxDMMicPanel
    {
        public VKSxDMMicAPanel()
            : base(MicChannel.MicA)
        {
            
        }
    }

    public class VKSxDMMicBPanel : VKxDMMicPanel
    {
        public VKSxDMMicBPanel()
            : base(MicChannel.MicB)
        {
            
        }
    }
}
