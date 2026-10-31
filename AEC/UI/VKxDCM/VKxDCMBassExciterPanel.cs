using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AEC.Core;
using AEC.Misc;
using AEC.Effectors;
using AEC.Effectors.VKxDCM;
using AEC.Common;

namespace AEC.UI.VKxDCM
{
    public partial class VKxDCMBassExciterPanel : AEC.UI.Controls.MContainerControl
    {
        public ModuleBase module;
        public BoolParam on = null;
        public BassExciter bassExciter = null;

        public VKxDCMBassExciterPanel()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitFont();

            this.sliderExciterXoverFreq.ValueChanged += sliderExciterXoverFreq_ValueChanged;
            this.sliderExciterXoverFreq.Actived += sliderExciterXoverFreq_Actived;
            this.lblExciterXoverFreq.MouseDown += lblExciterXoverFreq_MouseDown;
        }

        private void lblExciterXoverFreq_MouseDown(object sender, MouseEventArgs e)
        {
            this.ActiveBassExciter();
        }

        private void sliderExciterXoverFreq_Actived(object sender, EventArgs e)
        {
            this.ActiveBassExciter();
        }

        private void sliderExciterXoverFreq_ValueChanged(object sender, EventArgs e)
        {
            this.ActiveBassExciter();
        }

        private void ActiveBassExciter()
        {
            if (this.module != null)
            {
                this.module.ExciterChanged(ActiveItem.BassExciter);
            }
        }

        private void InitFont()
        {
            this.chkBassExciterOn.Font = FontHelper.CreateFont10();
            this.lblExciterXoverFreq.Font = FontHelper.CreateFont10();
            this.lblExciterFBGain.Font = FontHelper.CreateFont10();
            this.lblExciterBoostLevel.Font = FontHelper.CreateFont10();
        }

        public void Binding(ModuleBase module_, BoolParam on_, BassExciter bassExciter_)
        {
            Debug.Assert(module_ != null && on_ != null && bassExciter_ != null);

            this.module = module_;
            this.on = on_;
            this.bassExciter = bassExciter_;

            this.chkBassExciterOn.IsCheckedInit = this.on.value;
            this.sliderExciterXoverFreq.Binding(this.bassExciter.xoverFreq, BassExciter.GetXoverFreqLabel, BassExciter.CalcXOverFreqIndexFromText);
            this.sliderExciterFBGain.Binding(this.bassExciter.fbGain, BassExciter.GetFBGainLabel, BassExciter.CalcFBGainIndexFromText);
            this.sliderExciterBoostLevel.Binding(this.bassExciter.boostLevel, BassExciter.GetBoostLevelLabel, BassExciter.CalcBoostLevelIndexFromText);
        }

        public void UpdateUIFromData()
        {
            if (this.on == null || this.bassExciter == null)
            {
                return;
            }

            this.chkBassExciterOn.IsCheckedInit = this.on.value;

            this.sliderExciterXoverFreq.UpdateUIFromData();
            this.sliderExciterFBGain.UpdateUIFromData();
            this.sliderExciterBoostLevel.UpdateUIFromData();    
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.lblExciterXoverFreq.Text = localization[Localization.exciterPanel_XoverFreq];
            this.lblExciterFBGain.Text = localization[Localization.exciterPanel_FBGain];
            this.lblExciterBoostLevel.Text = localization[Localization.exciterPanel_BoostLevel];

            this.Invalidate();
        }

        private void chkBassExciterOn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.module != null && this.on != null)
            {
                this.on.value = this.chkBassExciterOn.IsChecked;
                this.bassExciter.on.initV = this.on.v;

                this.ActiveBassExciter();
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.btnResetXoverFreq))
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
            if (this.module != null && this.bassExciter == null)
            {
                return;
            }

            this.bassExciter.ResetXoverFreq();
            this.sliderExciterXoverFreq.UpdateUIFromData();
            this.module.ExciterChanged(ActiveItem.BassExciter);
        }

        public void ResetBassExciterFBGain()
        {
            if (this.bassExciter == null)
            {
                return;
            }

            this.bassExciter.ResetFBGain();
            this.sliderExciterFBGain.UpdateUIFromData();
        }

        public void ResetBassExciterBoostLevel()
        {
            if (this.bassExciter == null)
            {
                return;
            }

            this.bassExciter.ResetBoostLevel();
            this.sliderExciterBoostLevel.UpdateUIFromData();
        }
    }
}
