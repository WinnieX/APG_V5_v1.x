using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AEC.Core;
using AEC.Misc;
using AEC.Effectors;
using AEC.Effectors.VKxS;
using AEC.Common;
using AEC.UI.Skin;

namespace AEC.UI.VKxS
{
    public partial class VKxSCompressorPanel : gtUserControl
    {
        private MEffectorThreshold effectorThreshold;

        public VKxSCompressorPanel()
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
        }

        private void InitFont()
        {
            this.lblThreshold.Font = FontHelper.CreateFont10();
            this.lblAttack.Font = FontHelper.CreateFont10();
            this.lblRelease.Font = FontHelper.CreateFont10();
            this.lblRatio.Font = FontHelper.CreateFont10();
        }

        public void InitTheme()
        {
            this.FillColor = SkinManager.CurrentSkin.PanelBackColor;
            this.ForeColor = SkinManager.CurrentSkin.PanelForeColor;

            this.lblThreshold.ForeColor = SkinManager.CurrentSkin.PanelForeColor;
            this.lblAttack.ForeColor = SkinManager.CurrentSkin.PanelForeColor;
            this.lblRelease.ForeColor = SkinManager.CurrentSkin.PanelForeColor;
            this.lblRatio.ForeColor = SkinManager.CurrentSkin.PanelForeColor;

            this.sliderThreshold.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.sliderAttack.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.sliderThreshold.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.sliderAttack.BackColor = SkinManager.CurrentSkin.PanelBackColor;
        }

        public void Binding(Compressor compressor, MEffectorThreshold effectorThreshold_)
        {
            this.effectorThreshold = effectorThreshold_;

            this.sliderThreshold.Binding(compressor.threshold, this.effectorThreshold.GetThresholdLabelIndB, this.effectorThreshold.CalcThresholdIndexFromTextIndB);
            this.sliderAttack.Binding(compressor.attack, Compressor.GetAttackLabel, Compressor.CalcAttackIndexFromText);
            this.sliderRelease.Binding(compressor.release, Compressor.GetReleaseLabel, Compressor.CalcReleaseIndexFromText);
            this.sliderRatio.Binding(compressor.ratio, ParamGetLabelHelper.GetRatioLabel, ParamGetIndexHelper.CalcRatioIndexFromText);
        }

        public void UpdateUIFromData()
        {
            this.sliderThreshold.ReInitParamLabel(this.effectorThreshold.GetThresholdLabelIndB, this.effectorThreshold.CalcThresholdIndexFromTextIndB);
            this.sliderThreshold.UpdateUIFromData();
            this.sliderAttack.UpdateUIFromData();
            this.sliderRelease.UpdateUIFromData();
            this.sliderRatio.UpdateUIFromData();
        }

        public void UpdateEffectorModel()
        {
            this.sliderThreshold.ReInitParamLabel(this.effectorThreshold.GetThresholdLabelIndB, this.effectorThreshold.CalcThresholdIndexFromTextIndB);
            this.sliderThreshold.UpdateUIFromData();
        }

        public Image sliderback
        {
            get
            {
                return this.sliderThreshold.backgroundNormal;
            }
            set
            {
                this.sliderThreshold.backgroundNormal = value;
                this.sliderAttack.backgroundNormal = value;
                this.sliderRelease.backgroundNormal = value;
                this.sliderRatio.backgroundNormal = value;
            }
        }

        public int sliderWidth
        {
            get
            {
                return this.sliderThreshold.SliderWidth;
            }
            set
            {
                this.sliderThreshold.SliderWidth = value;
                this.sliderAttack.SliderWidth = value;
                this.sliderRelease.SliderWidth = value;
                this.sliderRatio.SliderWidth = value;
            }
        }

        public int SliderParamWidth
        {
            get
            {
                return this.sliderThreshold.Width;
            }
            set
            {
                this.sliderThreshold.Width = value;
                this.sliderAttack.Width = value;
                this.sliderRelease.Width = value;
                this.sliderRatio.Width = value;
            }
        }

        public int ThumbEndX
        {
            get
            {
                return this.sliderThreshold.ThumbEndX;
            }
            set
            {
                this.sliderThreshold.ThumbEndX = value;
                this.sliderAttack.ThumbEndX = value;
                this.sliderRelease.ThumbEndX = value;
                this.sliderRatio.ThumbEndX = value;
            }
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.lblThreshold.Text = localization[Localization.compressorPanel_Threshold];
            this.lblAttack.Text = localization[Localization.compressorPanel_Attack];
            this.lblRelease.Text = localization[Localization.compressorPanel_Release];
            this.lblRatio.Text = localization[Localization.compressorPanel_Ratio];
        }
    }
}
