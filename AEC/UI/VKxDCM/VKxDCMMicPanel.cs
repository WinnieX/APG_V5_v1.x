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
using AEC.Effectors.VKxDCM;
using AEC.Effectors.VKxDCM.Modules;
using AEC.Common;
using AEC.Comm;
using AEC.Misc;

namespace AEC.UI.VKxDCM
{
    public partial class VKxDCMMicPanel : PanelBase
    {
        private readonly MicChannel micChannel = MicChannel.MicA;
        private readonly MicChannel anotherChanel = MicChannel.MicB;

        private MicModule mic;
        private MicModule anotherMic;

        private bool isMicABLink
        {
            get
            {
                return this.chkMicABLink.IsChecked;
            }
        }

        public VKxDCMMicPanel(MicChannel micInput_)
            : base()
        {
            InitializeComponent();
            InitFont();

            micChannel = micInput_;
            anotherChanel = micChannel == MicChannel.MicA ? MicChannel.MicB : MicChannel.MicA;

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
            //this.panelNoiseGate.Font = FontHelper.CreateFont10();
            //this.panelFbe.Font = FontHelper.CreateFont10();
            this.panelMic.Font = FontHelper.CreateFont10();
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.vkxdcmEffectorModel != null);

            this.mic = this.micChannel == MicChannel.MicA ? base.mController.vkxdcmEffectorModel.micA : base.mController.vkxdcmEffectorModel.micB;
            this.anotherMic = this.micChannel == MicChannel.MicA ? base.mController.vkxdcmEffectorModel.micB : base.mController.vkxdcmEffectorModel.micA;

            base.BindingModule(this.mic, MEffectorFamily.VKxDCM);

            this.cmbLpfType.SelectedIndex = this.mic.lpf.typeIndex;

            this.sliderMicLevel.Binding(mic.micLevel, ParamGetLabelHelper.ClassicMicEffectLevelLabel);

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

            this.sliderMicLevel.UpdateUIFromData();
            
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

            this.lblMicNoiseGate.Text = localization[Localization.panelMic_MicNoiseGate];
            this.lblMicFBE.Text = localization[Localization.panelMic_MicFBE];

            this.lblMicLevel.Text = localization[Localization.panelMic_MicLevel];
            this.lblMicHpfFreq.Text = localization[Localization.panelMic_MicHpf];
            this.lblMicLpfFreq.Text = localization[Localization.panelMic_MicLpf];
            this.lblMicLpfType.Text = localization[Localization.panelMic_MicLpfType];
            this.chkMicABLink.Text = localization[Localization.panelMic_MicABLink];
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

        public void UnlinkMicAB()
        {
            this.chkMicABLink.IsChecked = false;
        }

        private void chkMicABLink_CheckedChanged(object sender, EventArgs e)
        {
            Debug.Assert(_eventBus != null);
            base.mController.vkxdcmEffectorModel.isLinkMicAB = this.chkMicABLink.IsChecked;
            this._eventBus.NotifyObeserver(new MicABLinkedArgs(this.chkMicABLink.IsChecked, this.micChannel));
        }

        private void chkMicABLink_Click(object sender, EventArgs e)
        {
            Debug.Assert(this.mic != null && this.anotherMic != null);
            if (!this.chkMicABLink.IsChecked && !this.mic.IsEqual(this.anotherMic))
            {
                string text = string.Format(localization[Localization.panelMic_MicABLink_Tips], GetMicChannelName(micChannel), GetMicChannelName(anotherChanel));
                if (DialogResult.OK != gtMessageBox.ShowMessage(this.FindForm(), text, "AEC", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    return;
                }

                this.anotherMic.SynchronousFrom(this.mic);
            }

            this.chkMicABLink.IsChecked = !this.chkMicABLink.IsChecked;            
        }

        public static string GetMicChannelName(MicChannel channel)
        {
            return channel == MicChannel.MicA ? "A" : "B"; 
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

    public class VKxDCMMicAPanel : VKxDCMMicPanel
    {
        public VKxDCMMicAPanel()
            : base(MicChannel.MicA)
        {

        }
    }

    public class VKxDCMMicBPanel : VKxDCMMicPanel
    {
        public VKxDCMMicBPanel()
            : base(MicChannel.MicB)
        {

        }
    }
}
