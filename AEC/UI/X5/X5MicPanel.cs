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
using AEC.Effectors.X5;
using AEC.Effectors.X5.Modules;
using AEC.Common;
using AEC.Comm;
using AEC.Misc;

namespace AEC.UI.X5
{
    public partial class X5MicPanel : PanelBase
    {
        private MicModule mic;

        public X5MicPanel()
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
            this.InitControl(this.eqGroupPanel, this.hpfFreqSlider, null);
            this.eqResetAndByPassPanel.ResetEQ += base.ResetEQGroup;
            this.eqResetAndByPassPanel.EQPassChanged += base.SwitchEQByPass;
            //this.micLevel.Init(micLevelPeak, thresholdLevel);
            //this.micLevel.isShowLevel = true;
            //this.micLevel.level = 10;
        }

        private void InitFont()
        {
            this.panelFbe.Font = FontHelper.CreateFont10();
            this.panelXPF.Font = FontHelper.CreateFont10();
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.x5EffectorModel != null);

            this.mic = base.mController.x5EffectorModel.mic;
            base.BindingModule(this.mic, MEffectorFamily.X5);

            this.cmbFbe.SelectedIndex = this.mic.fbe.v;

            this.compressorPanel.Binding(mic.compressor, this._eventBus, base.mController.mEffectorThreshold, true);
            this.eqResetAndByPassPanel.Init(this.mic.eqGroup.isBypass, this.mic.themeColor);

            this.RegisterCmbSelectedIndexChangedHandler();
        }

        public override void UpdateUIFromData()
        {
            base.UpdateUIFromData();

            this.RemoveCmbSelectedIndexChangedHandler();
            this.cmbFbe.SelectedIndex = this.mic.fbe.v;
            this.RegisterCmbSelectedIndexChangedHandler();
            
            this.compressorPanel.UpdateUIFromData();
            this.eqResetAndByPassPanel.isPass = this.mic.eqGroup.isBypass;
        }

        private void RegisterCmbSelectedIndexChangedHandler()
        {
            this.cmbFbe.SelectedIndexChanged += cmb_SelectedIndexChanged;
        }

        private void RemoveCmbSelectedIndexChangedHandler()
        {
            this.cmbFbe.SelectedIndexChanged -= cmb_SelectedIndexChanged;
        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            gtComboBox cmb = sender as gtComboBox;
            if (sender.Equals(this.cmbFbe))
            {
                this.mic.fbe.v = (short)this.cmbFbe.SelectedIndex;
            }
        }

        public override void UpdateDisplayLanguage()
        {
            this.lblMicFbe.Text = localization[Localization.panelMic_MicFBE];
            this.lblMicHpfFreq.Text = localization[Localization.panelMic_MicHpf];
            this.compressorPanel.UpdateDisplayLanguage();
            this.eqGroupPanel.UpdateDisplayLanguage();
            this.eqResetAndByPassPanel.UpdateDisplayLanguage();

            this.Invalidate();
        }

        public override void UpdateEffectorModel()
        {
            this.compressorPanel.UpdateEffectorModel();
        }
    }
}
