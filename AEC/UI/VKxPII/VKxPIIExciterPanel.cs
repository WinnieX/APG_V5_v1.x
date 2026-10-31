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
using AEC.Effectors.VKxPII;
using AEC.Common;

namespace AEC.UI.VKxPII
{
    public partial class VKxPIIExciterPanel : AEC.UI.Controls.MContainerControl
    {
        public enum ExciterType
        {
            Exciter1,
            Exciter2,
        };

        private ModuleBase module = null;
        private Exciter exciter = null;
        private ExciterType exciterType;

        public VKxPIIExciterPanel()
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

            this.sliderExciterFreq.ValueChanged += sliderExciter_ValueChanged;
            this.sliderExciterFreq.Actived += sliderExciter_Actived;
            this.lblExciterFreq.MouseDown += lbl_MouseDown;
            this.sliderExciterBandWidth.ValueChanged += sliderExciter_ValueChanged;
            this.sliderExciterBandWidth.Actived += sliderExciter_Actived;
            this.lblExciterBandWidth.MouseDown += lbl_MouseDown;

        }

        private void lbl_MouseDown(object sender, MouseEventArgs e)
        {
            this.ActiveExciter();
        }

        private void sliderExciter_Actived(object sender, EventArgs e)
        {
            this.ActiveExciter();
        }

        private void sliderExciter_ValueChanged(object sender, EventArgs e)
        {
            this.ActiveExciter();
        }

        private void ActiveExciter()
        {
            if (this.module != null)
            {
                if (exciterType == ExciterType.Exciter1)
                {
                    this.module.ExciterChanged(ActiveItem.Exciter1);
                }
                else
                {
                    this.module.ExciterChanged(ActiveItem.Exciter2);
                }
            }
        }

        private void InitFont()
        {
            this.chkExciterOn.Font = FontHelper.CreateFont10();
            this.lblExciterFreq.Font = FontHelper.CreateFont10();
            this.lblExciterBandWidth.Font = FontHelper.CreateFont10();
            this.lblExciterFBGain.Font = FontHelper.CreateFont10();
            this.lblExciterBoostLevel.Font = FontHelper.CreateFont10();
        }

        public void Binding(ModuleBase module_, Exciter exciter_, ExciterType type_)
        {
            Debug.Assert(module_ != null && exciter_ != null);

            this.module = module_;
            this.exciter = exciter_;
            this.exciterType = type_;

            this.chkExciterOn.IsCheckedInit = this.exciter.on.value;

            this.sliderExciterFreq.Binding(this.exciter.freq, Exciter.GetXoverFreqLabel, Exciter.CalcXOverFreqIndexFromText);
            this.sliderExciterBandWidth.Binding(this.exciter.bandWidth, Exciter.GetBandWidthLabel, Exciter.CalcBandWidthIndexFromText);
            this.sliderExciterFBGain.Binding(this.exciter.fbGain, Exciter.GetFBGainLabel, Exciter.CalcFBGainIndexFromText);
            this.sliderExciterBoostLevel.Binding(this.exciter.boostLevel, Exciter.GetBoostLevelLabel, Exciter.CalcBoostLevelIndexFromText);
            this.sliderExciterOffsetLevel.Binding(this.exciter.offsetLevel, Exciter.GetOffsetLevelLabel, Exciter.CalcOffsetLevelIndexFromText);
        }

        public void UpdateUIFromData()
        {
            if(this.exciter == null)
            {
                return;
            }

            this.chkExciterOn.IsCheckedInit = this.exciter.on.value;

            this.sliderExciterFreq.UpdateUIFromData();
            this.sliderExciterBandWidth.UpdateUIFromData();
            this.sliderExciterFBGain.UpdateUIFromData();
            this.sliderExciterBoostLevel.UpdateUIFromData();
            this.sliderExciterOffsetLevel.UpdateUIFromData();
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.lblExciterFreq.Text = localization[Localization.exciterPanel_Freq];
            this.lblExciterBandWidth.Text = localization[Localization.exciterPanel_BandWidth];
            this.lblExciterFBGain.Text = localization[Localization.exciterPanel_FBGain];
            this.lblExciterBoostLevel.Text = localization[Localization.exciterPanel_BoostLevel];

            this.Invalidate();
        }

        private void chkExciterOn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.module != null && this.exciter != null)
            {
                this.exciter.on.value = this.chkExciterOn.IsChecked;

                this.ActiveExciter();
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.btnResetFreq))
            {
                this.ResetExciterFreq();
            }
            else if(sender.Equals(this.btnResetBandWidth))
            {
                this.ResetExciterBandWidth();
            }
            else if (sender.Equals(this.btnResetFBGain))
            {
                this.ResetExciterFBGain();
            }            
            else if (sender.Equals(this.btnResetBoostLevel))
            {
                this.ResetExciterBoostLevel();
            }
        }

        public void ResetExciterFreq()
        {
            if (this.module != null && this.exciter == null)
            {
                return;
            }

            this.exciter.ResetFreq();
            this.sliderExciterFreq.UpdateUIFromData();

            this.ActiveExciter();
        }

        public void ResetExciterBandWidth()
        {
            if (this.module != null && this.exciter == null)
            {
                return;
            }

            this.exciter.ResetBandWidth();
            this.sliderExciterBandWidth.UpdateUIFromData();

            this.ActiveExciter();
        }

        public void ResetExciterFBGain()
        {
            if (this.exciter == null)
            {
                return;
            }

            this.exciter.ResetFBGain();
            this.sliderExciterFBGain.UpdateUIFromData();
        }

        public void ResetExciterBoostLevel()
        {
            if (this.exciter == null)
            {
                return;
            }

            this.exciter.ResetBoostLevel();
            this.sliderExciterBoostLevel.UpdateUIFromData();
        }
    }
}
