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
using AEC.Effectors.X5;
using AEC.Effectors.X5.Modules;
using AEC.Common;
using AEC.Comm;
using AEC.Misc;

namespace AEC.UI.X5
{
    public partial class X5MusicPanel : PanelBase
    {
        private MusicModule music;
        private SystemModuleBase system;

        public X5MusicPanel()
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

            this.rdoMusicInputPort1.CheckedChanged += rdoMusicInputPort_CheckedChanged;
            this.rdoMusicInputPort2.CheckedChanged += rdoMusicInputPort_CheckedChanged;
            this.rdoMusicInputBT.CheckedChanged += rdoMusicInputPort_CheckedChanged;
            this.rdoMusicInputSPDIF.CheckedChanged += rdoMusicInputPort_CheckedChanged;
            //this.InitCmb();
        }

        void rdoMusicInputPort_CheckedChanged(object sender, EventArgs e)
        {
            gtRadioButtonWithText rdo = sender as gtRadioButtonWithText;
            if(rdo == null || !rdo.isChecked)
            {
                return;
            }

            if(rdo == this.rdoMusicInputPort1)
            {
                this.system.musicInputPort.v = 0;
            }
            else if(rdo == this.rdoMusicInputPort2)
            {
                this.system.musicInputPort.v = 1;
            }
            else if (rdo == this.rdoMusicInputBT)
            {
                this.system.musicInputPort.v = 2;
            }
            else if (rdo == this.rdoMusicInputSPDIF)
            {
                this.system.musicInputPort.v = 3;
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

            Debug.Assert(base.mController != null && base.mController.x5EffectorModel != null);

            this.music = base.mController.x5EffectorModel.music;
            this.system = base.mController.x5EffectorModel.system;
            base.BindingModule(this.music, MEffectorFamily.X5);
            this.eqResetAndByPassPanel.Init(this.music.eqGroup.isBypass, this.music.themeColor);

            //this.UpdateMusicGainVisible();
            this.UpdateMusicInputUI();
            
            this.cmbMusicInputGain.SelectedIndex = this.system.musicInputGain.v;
            this.cmbMusicInputGain.Enabled = !this.system.isLockUserOperation;

            this.cmbHpfType.SelectedIndex = this.music.highPassFilter.typeIndex;

            if(this._eventBus != null)
            {
                //this._eventBus.RegisterHandler(WorkModeOrWorkSelectChanged);
                this._eventBus.RegisterHandler(SystemUserChanged);
            }

            this.RegisterCmbSelectedIndexChangedHandler();
        }

        public override void UpdateUIFromData()
        {
            base.UpdateUIFromData();

            this.eqResetAndByPassPanel.isPass = this.music.eqGroup.isBypass;

            //this.UpdateMusicGainVisible();
            this.UpdateMusicInputUI();

            this.RemoveCmbSelectedIndexChangedHandler();
            this.cmbMusicInputGain.SelectedIndex = this.system.musicInputGain.v;
            this.cmbHpfType.SelectedIndex = this.music.highPassFilter.typeIndex;
            this.RegisterCmbSelectedIndexChangedHandler();

            this.cmbMusicInputGain.Enabled = !this.system.isLockUserOperation;

            this.sliderHpfFreq.UpdateUIFromData();
        }

        private void UpdateMusicInputUI()
        {
            if (base.mController.mEffectorModel.currModel == DeviceModel.X5W)
            {
                this.rdoMusicInputBT.Visible = true;
                this.rdoMusicInputSPDIF.Visible = true;

                switch (this.system.musicInputPort.v)
                {
                    case 0:
                        this.rdoMusicInputPort1.isCheckInit = true;
                        break;

                    case 1:
                        this.rdoMusicInputPort2.isCheckInit = true;
                        break;

                    case 2:
                        this.rdoMusicInputBT.isCheckInit = true;
                        break;

                    case 3:
                        this.rdoMusicInputSPDIF.isCheckInit = true;
                        break;

                    default:
                        this.rdoMusicInputPort1.isCheckInit = true;
                        break;
                }
            }
            else
            {
                this.rdoMusicInputBT.Visible = false;
                this.rdoMusicInputSPDIF.Visible = false;

                if (this.system.musicInputPort.v == 0)
                {
                    this.rdoMusicInputPort1.isCheckInit = true;
                }
                else
                {
                    this.rdoMusicInputPort2.isCheckInit = true;
                }
            }
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
            this.cmbMusicInputGain.SelectedIndexChanged += cmb_SelectedIndexChanged;
            this.cmbHpfType.SelectedIndexChanged += cmb_SelectedIndexChanged;
        }

        private void RemoveCmbSelectedIndexChangedHandler()
        {
            this.cmbMusicInputGain.SelectedIndexChanged -= cmb_SelectedIndexChanged;
            this.cmbHpfType.SelectedIndexChanged -= cmb_SelectedIndexChanged;
        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            gtComboBox cmb = sender as gtComboBox;
            if (sender.Equals(this.cmbMusicInputGain))
            {
                this.system.musicInputGain.v = (short)this.cmbMusicInputGain.SelectedIndex;
            }
            else if(sender.Equals(this.cmbHpfType))
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

        //private void InitCmb()
        //{
        //    string[] ports = new string[SystemModule.DefMusicInputPortMax + 1];
        //    for (int i = 0; i < ports.Length; ++i)
        //    {
        //        ports[i] = string.Format("{0} {1}", localization[Localization.panelMusic_Port], (i + 1).ToString());
        //    }

        //    if (cmbMusicInputPort.Items.Count == 0)
        //    {
        //        cmbMusicInputPort.Items.AddRange(ports);
        //    }
        //    else
        //    {
        //        cmbMusicInputPort.Items.Clear();
        //        cmbMusicInputPort.Items.AddRange(ports);
        //    }
        //}

        public override void UpdateDisplayLanguage()
        {
            //this.RemoveCmbSelectedIndexChangedHandler();
            //this.InitCmb();
            //this.cmbMusicInputPort.SelectedIndex = this.system.musicInputPort.v;
            //this.RegisterCmbSelectedIndexChangedHandler();

            this.rdoMusicInputPort1.Text = "1";
            this.rdoMusicInputPort2.Text = "2";
            this.rdoMusicInputBT.Text = localization[Localization.panelMusic_BT];
            this.rdoMusicInputSPDIF.Text = localization[Localization.panelMusic_SPDIF];

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
