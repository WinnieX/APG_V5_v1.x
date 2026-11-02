using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AEC.Core;
using AEC.Effectors;
using AEC.Effectors.VX;
using AEC.Effectors.VX.Modules;
using AEC.Common;
using AEC.Comm;
using AEC.Misc;

namespace AEC.UI.VX
{
    public partial class VXMusicPanel : PanelBase
    {
        private MusicModule music;
        private SystemModuleBase system;

        public VXMusicPanel()
            : base()
        {
            InitializeComponent();
            this.InitFont();

            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true); //默认启动双缓冲
            //SetStyle(ControlStyles.DoubleBuffer, true);
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.ResizeRedraw, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.

            this.InitControl(this.eqGroupPanel, this.hpfFreqSlider, null);
            this.eqResetAndByPassPanel.ResetEQ += base.ResetEQGroup;
            this.eqResetAndByPassPanel.EQPassChanged += base.SwitchEQByPass;

            this.InitCmb();
        }

        private void InitFont()
        {
            this.panelMusicInput.Font = FontHelper.CreateFont10();
            this.panelHPF.Font = FontHelper.CreateFont10();
            //this.cmbMusicInputPort.Font = FontHelper.CreateLabelFont10();
            //this.cmbMusicInputGain.Font = FontHelper.CreateLabelFont10();
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.vxEffectorModel != null);

            this.music = base.mController.vxEffectorModel.music;
            this.system = base.mController.vxEffectorModel.system;
            base.BindingModule(this.music, MEffectorFamily.VX);
            this.eqResetAndByPassPanel.Init(this.music.eqGroup.isBypass, this.music.themeColor);
            this.eqResetAndByPassPanel.Enabled = this.music.isManual;

            //this.workModePanel.Init(this.music, this._eventBus, this.music.themeColor);
            this.workModePanel.Init(this.music.autoWorkMode, this._eventBus, this.music.themeColor);

            this.hpfFreqSlider.Enabled = this.music.isManual;

            this.UpdateMusicGainVisible();
            
            this.cmbMusicInputPort.SelectedIndex = this.system.musicInputPort.v;
            this.cmbMusicInputGain.SelectedIndex = this.system.musicInputGain.v;
            this.cmbMusicInputGain.Enabled = !this.system.isLockUserOperation;

            if(this._eventBus != null)
            {
                this._eventBus.RegisterHandler(WorkModeOrWorkSelectChanged);
                this._eventBus.RegisterHandler(SystemUserChanged);
            }

            this.RegisterCmbSelectedIndexChangedHandler();
        }

        public override void UpdateUIFromData()
        {
            if (this.eqPanel != null)
            {
                this.eqPanel.InitEqAdjusterList();
            }

            this.hpfFreqSlider.Binding(this.music.hpf.freqParam, GetHpfFreqLabel, CalcHPFFreqIndexFromText);

            this.eqResetAndByPassPanel.isPass = this.music.eqGroup.isBypass;
            this.eqResetAndByPassPanel.Enabled = this.music.isManual;
            this.workModePanel.UpdateUIFromData();

            this.UpdateMusicGainVisible();

            this.RemoveCmbSelectedIndexChangedHandler();
            this.cmbMusicInputPort.SelectedIndex = this.system.musicInputPort.v;
            this.cmbMusicInputGain.SelectedIndex = this.system.musicInputGain.v;
            this.RegisterCmbSelectedIndexChangedHandler();

            this.cmbMusicInputGain.Enabled = !this.system.isLockUserOperation;

            this.hpfFreqSlider.Enabled = this.music.isManual;
        }

        private bool MusicGainVisible
        {
            set
            {
                this.lblMusicInputGain.Visible = value;
                this.cmbMusicInputGain.Visible = value;
            }
        }

        private void UpdateMusicGainVisible()
        {
            this.MusicGainVisible = base.mController.mEffectorModel.currModel == DeviceModel.VHi || base.mController.mEffectorModel.currModel == DeviceModel.M_Audio;
        }

        protected override void WrokModeChanged()
        {
            this.eqResetAndByPassPanel.Enabled = this.music.isManual;
            this.eqPanel.UpdateEqCanbeChanged();
            this.hpfFreqSlider.Enabled = this.music.isManual;
        }

        protected override void WrokSelectChanged()
        {
            this.hpfFreqSlider.Binding(this.music.hpf.freqParam, GetHpfFreqLabel, CalcHPFFreqIndexFromText);

            this.eqResetAndByPassPanel.isPass = this.music.eqGroup.isBypass;

            this.eqPanel.InitEqAdjusterList();

            this.music.UpdateFreqRespAndInitGraphic();
        }

        private void RegisterCmbSelectedIndexChangedHandler()
        {
            this.cmbMusicInputPort.SelectedIndexChanged += cmbMusicInput_SelectedIndexChanged;
            this.cmbMusicInputGain.SelectedIndexChanged += cmbMusicInput_SelectedIndexChanged;
        }

        private void RemoveCmbSelectedIndexChangedHandler()
        {
            this.cmbMusicInputPort.SelectedIndexChanged -= cmbMusicInput_SelectedIndexChanged;
            this.cmbMusicInputGain.SelectedIndexChanged -= cmbMusicInput_SelectedIndexChanged;
        }

        private void cmbMusicInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            gtComboBox cmb = sender as gtComboBox;
            if (sender.Equals(this.cmbMusicInputPort))
            {
                this.system.musicInputPort.v = (short)this.cmbMusicInputPort.SelectedIndex;
            }
            else if (sender.Equals(this.cmbMusicInputGain))
            {
                this.system.musicInputGain.v = (short)this.cmbMusicInputGain.SelectedIndex;
            }
        }

        protected void SystemUserChanged(object sender, EventBusArgs e)
        {
            if (e.type == EventType.SystemUserChanged)
            {
                this.cmbMusicInputGain.Enabled = !this.system.isLockUserOperation;
            }
        }

        private void InitCmb()
        {
            string[] ports = new string[SystemModule.DefMusicInputPortMax + 1];
            for (int i = 0; i < ports.Length; ++i)
            {
                ports[i] = string.Format("{0} {1}", localization[Localization.panelMusic_Port], (i + 1).ToString());
            }

            if (cmbMusicInputPort.Items.Count == 0)
            {
                cmbMusicInputPort.Items.AddRange(ports);
            }
            else
            {
                cmbMusicInputPort.Items.Clear();
                cmbMusicInputPort.Items.AddRange(ports);
            }
        }

        public override void UpdateDisplayLanguage()
        {
            this.RemoveCmbSelectedIndexChangedHandler();
            this.InitCmb();
            this.cmbMusicInputPort.SelectedIndex = this.system.musicInputPort.v;
            this.RegisterCmbSelectedIndexChangedHandler();

            this.panelMusicInput.title = localization[Localization.panelMusic_MusicInput];
            this.lblMusicInputPort.Text = localization[Localization.panelMusic_MusicInputPort];
            this.lblMusicInputGain.Text = localization[Localization.panelMusic_MusicInputPort3Gain];

            this.panelHPF.title = localization[Localization.panelMusic_MusicToMainOutHpf];
            this.eqGroupPanel.UpdateDisplayLanguage();
            this.eqResetAndByPassPanel.UpdateDisplayLanguage();
            this.workModePanel.UpdateDisplayLanguage();

            this.Invalidate();
        }
    }
}
