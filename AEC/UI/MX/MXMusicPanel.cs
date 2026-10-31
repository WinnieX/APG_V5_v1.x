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
using AEC.Common;
using AEC.Comm;
using AEC.Effectors.MX;
using AEC.Effectors.MX.Modules;
using AEC.Misc;

namespace AEC.UI.MX
{
    public partial class MXMusicPanel : PanelBase
    {
        private MusicModule music;
        private SystemModuleBase system;

        public MXMusicPanel()
            : base()
        {
            InitializeComponent();
            this.InitFont();

            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true); //默认启动双缓冲
            //SetStyle(ControlStyles.DoubleBuffer, true);
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.ResizeRedraw, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.

            this.InitControl(this.eqGroupPanel, null, null);
            this.eqResetAndByPassPanel.ResetEQ += base.ResetEQGroup;
            this.eqResetAndByPassPanel.EQPassChanged += base.SwitchEQByPass;

            this.InitCmb();
        }

        private void InitFont()
        {
            this.panelMusicInput.Font = FontHelper.CreateFont10();
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.mxEffectorModel != null);

            this.music = base.mController.mxEffectorModel.music;
            this.system = base.mController.mxEffectorModel.system;
            base.BindingModule(this.music, MEffectorFamily.MX);
            this.eqResetAndByPassPanel.Init(this.music.eqGroup.isBypass, this.music.themeColor);
            this.eqResetAndByPassPanel.Enabled = this.music.isManual;
            
            //this.workModePanel.Init(this.music, this._eventBus, this.music.themeColor);

            this.cmbMusicInputPort.SelectedIndex = this.system.musicInputPort.v;
            this.cmbMusicInputGain.SelectedIndex = this.system.musicInputGain.v;
            
            if(this._eventBus != null)
            {
                this._eventBus.RegisterHandler(WorkModeOrWorkSelectChanged);
                //this._eventBus.RegisterHandler(SystemUserChanged);
            }

            this.RegisterCmbSelectedIndexChangedHandler();
        }

        public override void UpdateUIFromData()
        {
            base.UpdateUIFromData();
            this.eqResetAndByPassPanel.isPass = this.music.eqGroup.isBypass;
            this.eqResetAndByPassPanel.Enabled = this.music.isManual;
            this.workModePanel.UpdateUIFromData();

            this.RemoveCmbSelectedIndexChangedHandler();
            this.cmbMusicInputPort.SelectedIndex = this.system.musicInputPort.v;
            this.cmbMusicInputGain.SelectedIndex = this.system.musicInputGain.v;
            this.RegisterCmbSelectedIndexChangedHandler();
        }

        protected override void WrokModeChanged()
        {
            this.eqResetAndByPassPanel.Enabled = this.music.isManual;
            this.eqPanel.UpdateEqCanbeChanged();
        }

        protected override void WrokSelectChanged()
        {
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

        //protected void SystemUserChanged(object sender, EventBusArgs e)
        //{
        //    if (e.type == EventType.SystemUserChanged)
        //    {
        //        this.cmbMusicInputPort.Enabled = this.system.isLockUserOperation;
        //    }
        //}

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
            this.lblMusicInputGain.Text = localization[Localization.panelMusic_MusicInputGain];

            this.lblFiberPort.Text = localization[Localization.panelMusic_FiberPort];

            this.eqGroupPanel.UpdateDisplayLanguage();
            this.eqResetAndByPassPanel.UpdateDisplayLanguage();
            this.workModePanel.UpdateDisplayLanguage();

            this.Invalidate();
        }
    }
}
