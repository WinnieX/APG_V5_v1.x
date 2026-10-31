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
using AEC.Effectors.VKxPIIDCM;
using AEC.Effectors.VKxPIIDCM.Modules;
using AEC.Common;
using AEC.Comm;
using AEC.Misc;
using AEC.UI.Controls;

namespace AEC.UI.VKxPIIDCM
{
    public partial class VKxPIIDCMMicExciterPanel : PanelBase
    {
        private MicExciterModule micExciter;

        public VKxPIIDCMMicExciterPanel()
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
            //this.InitControl(this.eqGroupPanel, this.hpfFreqSlier, this.lpfFreqSlier);
            //this.eqResetAndByPassPanel.ResetEQ += base.ResetEQGroup;
            //this.eqResetAndByPassPanel.EQPassChanged += base.SwitchEQByPass;
            //this.micLevel.Init(micLevelPeak, thresholdLevel);
            //this.micLevel.isShowLevel = true;
            //this.micLevel.level = 10;

        }

        private void InitFont()
        {
            this.panelMicExciter1.Font = FontHelper.CreateFont10();
            this.panelMicExciter2.Font = FontHelper.CreateFont10();
        }

        public override void BindingController(MController mController_, EventBus eventBus_)
        {
            base.BindingController(mController_, eventBus_);

            Debug.Assert(base.mController != null && base.mController.vkxPIIdcmEffectorModel != null);

            this.micExciter = base.mController.vkxPIIdcmEffectorModel.micExciter;
            base.BindingModule(this.micExciter, MEffectorFamily.VKxPIIDCM);

            if (this.micExciter.isSelectExciter)
            {
                this.rdoExciter.IsCheckedInit = true;
            }
            else
            {
                this.rdoBassExciter.IsCheckedInit = true;
            }
            this.UpdatePanelVisibleState();

            this.panelBassExciter.Binding(this.micExciter, this.micExciter.exciter1.on, this.micExciter.bassExciter);
            this.panelExciter1.Binding(this.micExciter, this.micExciter.exciter1, VKxPIIDCMExciterPanel.ExciterType.Exciter1);
            this.panelExciter2.Binding(this.micExciter, this.micExciter.exciter2, VKxPIIDCMExciterPanel.ExciterType.Exciter2);

            this.micExciter.activeItem = this.micExciter.isSelectExciter ? ActiveItem.Exciter1 : ActiveItem.BassExciter;
        }

        public override void UpdateUIFromData()
        {
            base.UpdateUIFromData();

            if (this.micExciter.isSelectExciter)
            {
                this.rdoExciter.IsCheckedInit = true;
            }
            else
            {
                this.rdoBassExciter.IsCheckedInit = true;
            }
            this.UpdatePanelVisibleState();

            this.panelBassExciter.UpdateUIFromData();
            this.panelExciter1.UpdateUIFromData();
            this.panelExciter2.UpdateUIFromData();

            this.micExciter.activeItem = this.micExciter.isSelectExciter ? ActiveItem.Exciter1 : ActiveItem.BassExciter;
        }

        //private void RegisterCmbSelectedIndexChangedHandler()
        //{
            
        //}

        //private void RemoveCmbSelectedIndexChangedHandler()
        //{
            
        //}

        public override void UpdateDisplayLanguage()
        {
            this.panelMicExciter1.title = localization[Localization.exciterPanel_MicExciter1];
            this.panelMicExciter2.title = localization[Localization.exciterPanel_MicExciter2];
            this.rdoBassExciter.Text = localization[Localization.exciterPanel_BassExciter];
            this.rdoExciter.Text = localization[Localization.exciterPanel_Exciter];

            this.panelBassExciter.UpdateDisplayLanguage();
            this.panelExciter1.UpdateDisplayLanguage();
            this.panelExciter2.UpdateDisplayLanguage();
            
            this.Invalidate();
        }

        private void rdo_CheckedChanged(object sender, EventArgs e)
        {
            MRadioButton rdo = sender as MRadioButton;
            if(rdo == null || !rdo.IsChecked)
            {
                return;
            }

            if(sender.Equals(this.rdoBassExciter))
            {
                this.SelectBassExciter();
            }
            else if(sender.Equals(this.rdoExciter))
            {
                this.SelectExciter();
            }
        }

        private void SelectBassExciter()
        {
            this.micExciter.isSelectExciter = false;
            
            this.UpdatePanelVisibleState();
        }

        private void SelectExciter()
        {
            this.micExciter.isSelectExciter = true;
            this.UpdatePanelVisibleState();
        }

        public void UpdatePanelVisibleState()
        {
            this.panelBassExciter.Visible = !this.micExciter.isSelectExciter;
            this.panelExciter1.Visible = this.micExciter.isSelectExciter;

            if(this.micExciter.isSelectExciter)
            {
                this.panelExciter1.UpdateUIFromData();
            }
            else
            {
                this.micExciter.bassExciter.on.initV = this.micExciter.exciter1.on.v;
                this.panelBassExciter.UpdateUIFromData();
            }

            this.Invalidate();
        }
    }
}
