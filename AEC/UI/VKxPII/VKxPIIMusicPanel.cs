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
using AEC.Effectors.VKxPII;
using AEC.Effectors.VKxPII.Modules;
using AEC.Common;
using AEC.Comm;
using AEC.Misc;

namespace AEC.UI.VKxPII
{
    public partial class VKxPIIMusicPanel : PanelBase
    {
        private MusicModule music;
        private SystemModuleBase system;

        public VKxPIIMusicPanel()
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

            this.sliderBassExciterXoverFreq.ValueChanged += sliderBassExciterXoverFreq_ValueChanged;
            this.sliderBassExciterXoverFreq.Actived += sliderBassExciterXoverFreq_Actived;
            this.lblBassExciterXoverFreq.MouseDown += lblBassExciterXoverFreq_MouseDown;

            this.InitCmb();
        }

        private void lblBassExciterXoverFreq_MouseDown(object sender, MouseEventArgs e)
        {
            this.ActiveBassExciter();
        }

        private void sliderBassExciterXoverFreq_Actived(object sender, EventArgs e)
        {
            this.ActiveBassExciter();
        }

        private void sliderBassExciterXoverFreq_ValueChanged(object sender, EventArgs e)
        {
            this.ActiveBassExciter();
        }

        private void ActiveBassExciter()
        {
            if (music != null)
            {
                music.ExciterChanged(ActiveItem.BassExciter);
            }
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

            Debug.Assert(base.mController != null && base.mController.vkxPIIEffectorModel != null);

            this.music = base.mController.vkxPIIEffectorModel.music;
            this.system = base.mController.vkxPIIEffectorModel.system;
            base.BindingModule(this.music, MEffectorFamily.VKxPII);
            this.eqResetAndByPassPanel.Init(this.music.eqGroup.isBypass, this.music.themeColor);
            this.eqResetAndByPassPanel.Enabled = this.music.isManual;

            this.hpfFreqSlider.Enabled = this.music.isManual;

            this.cmbMusicInputPort.SelectedIndex = this.system.musicInputPort.v;
            this.cmbHpfType.SelectedIndex = this.music.hpf.typeIndex;

            if(this._eventBus != null)
            {
                this._eventBus.RegisterHandler(WorkModeOrWorkSelectChanged);
            }

            this.RegisterCmbSelectedIndexChangedHandler();

            this.chkBassExciterBypass.IsCheckedInit = this.music.bassExciter.on.value;

            this.sliderBassExciterXoverFreq.Binding(this.music.bassExciter.xoverFreq, BassExciter.GetXoverFreqLabel, BassExciter.CalcXOverFreqIndexFromText);
            this.sliderBassExciterFBGain.Binding(this.music.bassExciter.fbGain, BassExciter.GetFBGainLabel, BassExciter.CalcFBGainIndexFromText);
            this.sliderBassExciterBoostLevel.Binding(this.music.bassExciter.boostLevel, BassExciter.GetBoostLevelLabel, BassExciter.CalcBoostLevelIndexFromText);
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

            this.RemoveCmbSelectedIndexChangedHandler();
            this.cmbMusicInputPort.SelectedIndex = this.system.musicInputPort.v;
            this.cmbHpfType.SelectedIndex = this.music.hpf.typeIndex;
            this.RegisterCmbSelectedIndexChangedHandler();

            this.hpfFreqSlider.Enabled = this.music.isManual;


            this.chkBassExciterBypass.IsCheckedInit = this.music.bassExciter.on.value;

            this.sliderBassExciterXoverFreq.UpdateUIFromData();
            this.sliderBassExciterFBGain.UpdateUIFromData();
            this.sliderBassExciterBoostLevel.UpdateUIFromData();            
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

            this.RemoveCmbSelectedIndexChangedHandler();
            this.cmbHpfType.SelectedIndex = this.music.hpf.typeIndex;
            this.RegisterCmbSelectedIndexChangedHandler();

            this.eqResetAndByPassPanel.isPass = this.music.eqGroup.isBypass;

            this.eqPanel.InitEqAdjusterList();

            this.music.UpdateFreqRespAndInitGraphic();
        }

        private void RegisterCmbSelectedIndexChangedHandler()
        {
            this.cmbMusicInputPort.SelectedIndexChanged += cmb_SelectedIndexChanged;
            this.cmbHpfType.SelectedIndexChanged += cmb_SelectedIndexChanged;
        }

        private void RemoveCmbSelectedIndexChangedHandler()
        {
            this.cmbMusicInputPort.SelectedIndexChanged -= cmb_SelectedIndexChanged;
            this.cmbHpfType.SelectedIndexChanged += cmb_SelectedIndexChanged;
        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            gtComboBox cmb = sender as gtComboBox;
            if (sender.Equals(this.cmbMusicInputPort))
            {
                this.system.musicInputPort.v = (short)this.cmbMusicInputPort.SelectedIndex;
            }
            else if(sender.Equals(this.cmbHpfType))
            {
                this.music.hpf.typeIndexParam = (short)this.cmbHpfType.SelectedIndex;
                this.music.UpdateXPFromControlPanel(XPFilterType.HighPass, FilterElement.Type);
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

            this.lblBassExciterXoverFreq.Text = localization[Localization.exciterPanel_XoverFreq];
            this.lblSuperBassExciterFBGain.Text = localization[Localization.exciterPanel_FBGain];
            this.lblBassExciterBoostLevel.Text = localization[Localization.exciterPanel_BoostLevel];

            this.panelBassExciter.title = localization[Localization.panelMusic_SuperBass];

            this.panelHPF.title = localization[Localization.panelMusic_MusicToMainOutHpf];
            this.lblHpfType.Text = localization[Localization.panelMusic_MusicToMainOutHpfSlope];
            this.eqGroupPanel.UpdateDisplayLanguage();
            this.eqResetAndByPassPanel.UpdateDisplayLanguage();

            this.Invalidate();
        }

        private void chkBassExciterBypass_CheckedChanged(object sender, EventArgs e)
        {
            if (this.music != null && this.music.bassExciter != null)
            {
                this.music.bassExciter.on.value = this.chkBassExciterBypass.IsChecked;

                this.ActiveBassExciter();
            }            
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if(sender.Equals(this.btnResetXoverFreq))
            {
                this.ResetBassExciterXoverFreq();
            }
            else if (sender.Equals(this.btnResetFBGain))
            {
                this.ResetBassExciterFBGain();
            }
            else if (sender.Equals(this.btnResetBoostLevel))
            {
                this.ResetBassExciterBoostLevel();
            }
        }

        public void ResetBassExciterXoverFreq()
        {
            if(this.music == null)
            {
                return;
            }

            this.music.bassExciter.ResetXoverFreq();
            this.sliderBassExciterXoverFreq.UpdateUIFromData();
            this.music.ExciterChanged(ActiveItem.BassExciter);
        }

        public void ResetBassExciterFBGain()
        {
            if (this.music == null)
            {
                return;
            }

            this.music.bassExciter.ResetFBGain();
            this.sliderBassExciterFBGain.UpdateUIFromData();
        }

        public void ResetBassExciterBoostLevel()
        {
            if (this.music == null)
            {
                return;
            }

            this.music.bassExciter.ResetBoostLevel();
            this.sliderBassExciterBoostLevel.UpdateUIFromData(); 
        }
    }
}
