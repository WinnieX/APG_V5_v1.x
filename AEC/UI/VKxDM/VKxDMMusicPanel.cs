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
using AEC.Effectors.VKxDM;
using AEC.Effectors.VKxDM.Modules;
using AEC.Common;
using AEC.Comm;
using AEC.Misc;

namespace AEC.UI.VKxDM
{
    public partial class VKxDMMusicPanel : PanelBase
    {
        private MusicModule music;
        private SystemModuleBase system;

        public VKxDMMusicPanel()
            : base()
        {
            InitializeComponent();
            this.InitFont();

            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true); //默认启动双缓冲
            //SetStyle(ControlStyles.DoubleBuffer, true);
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.ResizeRedraw, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.

            this.InitControl(this.eqGroupPanel, this.sliderHpfFreq, null);
            this.eqResetAndByPassPanel.ResetEQ += base.ResetEQGroup;
            this.eqResetAndByPassPanel.EQPassChanged += base.SwitchEQByPass;
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

            Debug.Assert(base.mController != null && base.mController.vkxsdmEffectorModel != null);

            this.music = base.mController.vkxsdmEffectorModel.music;
            this.system = base.mController.vkxsdmEffectorModel.system;
            base.BindingModule(this.music, MEffectorFamily.VKxDM);
            this.eqResetAndByPassPanel.Init(this.music.eqGroup.isBypass, this.music.themeColor);
            //this.eqResetAndByPassPanel.Enabled = this.music.autoWorkMode.isManual;

            //this.workModePanel.Init(this.music, this._eventBus, this.music.themeColor);

            //this.hpfFreqSlider.Enabled = this.music.autoWorkMode.isManual;
            this.cmbHpfType.SelectedIndex = this.music.highPassFilter.typeIndex;

            this.UpdateMusicGainVisible();

            this.InitCmb();
            if (this.system.musicInputPort.v > this.cmbMusicInputPort.Items.Count)
            {
                this.system.musicInputPort.initV = 0;
            }
            this.cmbMusicInputPort.SelectedIndex = this.system.musicInputPort.v;
            this.cmbMusicInputGain.SelectedIndex = this.system.musicInputGain.v;
            this.cmbMusicInputGain.Enabled = !this.system.isLockUserOperation;

            if(this._eventBus != null)
            {
                //this._eventBus.RegisterHandler(WorkModeOrWorkSelectChanged);
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

            this.sliderHpfFreq.Binding(this.music.hpf.freqParam, GetHpfFreqLabel, CalcHPFFreqIndexFromText);
            
            this.eqResetAndByPassPanel.isPass = this.music.eqGroup.isBypass;
            
            this.UpdateMusicGainVisible();

            this.RemoveCmbSelectedIndexChangedHandler();

            this.InitCmb();
            if (this.system.musicInputPort.v > this.cmbMusicInputPort.Items.Count)
            {
                this.system.musicInputPort.initV = 0;
            }
            this.cmbMusicInputPort.SelectedIndex = this.system.musicInputPort.v;


            //this.cmbMusicInputPort.SelectedIndex = this.system.musicInputPort.v;
            this.cmbMusicInputGain.SelectedIndex = this.system.musicInputGain.v;
            this.cmbHpfType.SelectedIndex = this.music.highPassFilter.typeIndex;
            this.RegisterCmbSelectedIndexChangedHandler();

            this.cmbMusicInputGain.Enabled = !this.system.isLockUserOperation;

            this.sliderHpfFreq.UpdateUIFromData();
        }
        protected override void XPFChanged(XPFilterType filterType, FilterElement filterElement)
        {
            if (filterType == XPFilterType.HighPass)
            {
                if (filterElement == FilterElement.Type)
                {
                    this.RemoveCmbSelectedIndexChangedHandler();
                    this.cmbHpfType.SelectedIndex = this.music.highPassFilter.typeIndex;
                    this.RegisterCmbSelectedIndexChangedHandler();
                }
            }
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

        private void RegisterCmbSelectedIndexChangedHandler()
        {
            this.cmbMusicInputPort.SelectedIndexChanged += cmb_SelectedIndexChanged;
            this.cmbMusicInputGain.SelectedIndexChanged += cmb_SelectedIndexChanged;
            this.cmbHpfType.SelectedIndexChanged += cmb_SelectedIndexChanged;
        }

        private void RemoveCmbSelectedIndexChangedHandler()
        {
            this.cmbMusicInputPort.SelectedIndexChanged -= cmb_SelectedIndexChanged;
            this.cmbMusicInputGain.SelectedIndexChanged -= cmb_SelectedIndexChanged;
            this.cmbHpfType.SelectedIndexChanged -= cmb_SelectedIndexChanged;
        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
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
            else if (sender.Equals(this.cmbHpfType))
            {
                this.music.highPassFilter.typeIndexParam = (short)this.cmbHpfType.SelectedIndex;
                this.music.UpdateXPFromControlPanel(XPFilterType.HighPass, FilterElement.Type);
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
            //string[] ports = new string[SystemModule.DefMusicInputPortMax + 1];
            List<string> ports = new List<string>();
            for (int i = 0; i <= SystemModule.DefMusicInputPortMax; ++i)
            {
                ports.Add(string.Format("{0} {1}", localization[Localization.panelMusic_Port], (i + 1).ToString()));
            }

            if (base.mController.mEffectorFeatures.IsSupportBT())
            {
                ports.Add(localization[Localization.panelMusic_BT]);
            }

            if (base.mController.mEffectorFeatures.IsSupportSPDIF())
            {
                ports.Add(localization[Localization.panelMusic_SPDIF]);
            }
            

            if (cmbMusicInputPort.Items.Count > 0)
            {
                cmbMusicInputPort.Items.Clear();
            }

            cmbMusicInputPort.Items.AddRange(ports.ToArray());
        }

        public override void UpdateDisplayLanguage()
        {
            this.RemoveCmbSelectedIndexChangedHandler();
            this.InitCmb();
            this.cmbMusicInputPort.SelectedIndex = this.system.musicInputPort.v;
            this.RegisterCmbSelectedIndexChangedHandler();

            this.panelMusicInput.title = localization[Localization.panelMusic_MusicInput];
            this.lblMusicInputPort.Text = localization[Localization.panelMusic_MusicInputPort];
            this.lblMusicInputGain.Text = localization[Localization.panelMusic_MusicInputGain];

            this.panelHPF.title = localization[Localization.panelMusic_MusicToMainOutHpf];
            this.lblHPFSlope.Text = localization[Localization.panelMusic_MusicToMainOutHpfSlope];
            this.eqGroupPanel.UpdateDisplayLanguage();
            this.eqResetAndByPassPanel.UpdateDisplayLanguage();

            this.Invalidate();
        }
    }
}
