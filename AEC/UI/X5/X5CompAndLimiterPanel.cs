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
using AEC.Effectors.X5;
using AEC.Common;

namespace AEC.UI.X5
{
    public partial class X5CompAndLimiterPanel : gtUserControl
    {
        private X5_CompressorLimiter cl;
        private EventBus eventBus;
        private MEffectorThreshold effectorThreshold;
        private bool isComp = false;

        private static AEC.Misc.Config config = AEC.Misc.Config.GetInstance();

        public X5CompAndLimiterPanel()
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

        public void Binding(X5_CompressorLimiter cl_, EventBus eventBus_, MEffectorThreshold effectorThreshold_, bool isComp_ = false)
        {
            this.cl = cl_;
            this.eventBus = eventBus_;
            this.effectorThreshold = effectorThreshold_;
            this.isComp = isComp_;
            this.btnSwitchUnit.Visible = !this.isComp;

            if (this.isComp)
            {
                this.sliderThreshold.Binding(cl.threshold, this.effectorThreshold.GetThresholdLabelIndB, this.effectorThreshold.CalcThresholdIndexFromTextIndB);
            }
            else
            {
                this.sliderThreshold.Binding(cl.threshold, this.effectorThreshold.GetThresholdLabel, this.effectorThreshold.CalcThresholdIndexFromText);
            }
            this.sliderAttack.Binding(cl.attack, ParamGetLabelHelper.GetAttackLabel, ParamGetIndexHelper.CalcAttackIndexFromText);
            this.sliderRelease.Binding(cl.release, ParamGetLabelHelper.GetReleaseLabel, ParamGetIndexHelper.CalcReleaseIndexFromText);
            this.sliderRatio.Binding(cl.ratio, ParamGetLabelHelper.GetRatioLabel, ParamGetIndexHelper.CalcRatioIndexFromText);

            this.eventBus.RegisterHandler(EventBusHandler);
        }

        private void EventBusHandler(object sender, EventBusArgs e)
        {
            if (e.type == EventType.LimiterThresholdUnitChanged && !this.isComp)
            {
                LimiterThresholdUnitChangedHandler();
            }
        }

        private void LimiterThresholdUnitChangedHandler()
        {
            UpdateThresholdUI();
        }

        public void UpdateThresholdUI()
        {
            this.sliderThreshold.ReInitParamLabel(this.effectorThreshold.GetThresholdLabel, this.effectorThreshold.CalcThresholdIndexFromText);
            this.sliderThreshold.UpdateUIFromData();
        }

        public void UpdateUIFromData()
        {
            if (this.isComp)
            {
                this.sliderThreshold.ReInitParamLabel(this.effectorThreshold.GetThresholdLabelIndB, this.effectorThreshold.CalcThresholdIndexFromTextIndB);
            }
            else
            {
                this.sliderThreshold.ReInitParamLabel(this.effectorThreshold.GetThresholdLabel, this.effectorThreshold.CalcThresholdIndexFromText);
            }

            this.sliderThreshold.UpdateUIFromData();
            this.sliderAttack.UpdateUIFromData();
            this.sliderRelease.UpdateUIFromData();
            this.sliderRatio.UpdateUIFromData();
        }

        public void UpdateEffectorModel()
        {
            if (this.isComp)
            {
                this.sliderThreshold.ReInitParamLabel(this.effectorThreshold.GetThresholdLabelIndB, this.effectorThreshold.CalcThresholdIndexFromTextIndB);
            }
            else
            {
                this.sliderThreshold.ReInitParamLabel(this.effectorThreshold.GetThresholdLabel, this.effectorThreshold.CalcThresholdIndexFromText);
            } 
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

        public int sliderX
        {
            get
            {
                return this.sliderThreshold.Location.X;
            }
            set
            {
                AnchorStyles sliderAnchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;

                this.sliderThreshold.Anchor = AnchorStyles.None;
                this.sliderAttack.Anchor = AnchorStyles.None;
                this.sliderRelease.Anchor = AnchorStyles.None;
                this.sliderRatio.Anchor = AnchorStyles.None;

                //int width = this.sliderThreshold.Width;

                this.sliderThreshold.Location = new Point(value, this.sliderThreshold.Location.Y);
                this.sliderAttack.Location = new Point(value, this.sliderAttack.Location.Y);
                this.sliderRelease.Location = new Point(value, this.sliderRelease.Location.Y);
                this.sliderRatio.Location = new Point(value, this.sliderRatio.Location.Y);

                this.sliderThreshold.Anchor = sliderAnchor;
                this.sliderAttack.Anchor = sliderAnchor;
                this.sliderRelease.Anchor = sliderAnchor;
                this.sliderRatio.Anchor = sliderAnchor;

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
            this.lblThreshold.Text = localization[Localization.x5_compAndLimiterPanel_Threshold];
            this.lblAttack.Text = localization[Localization.x5_compAndLimiterPanel_Attack];
            this.lblRelease.Text = localization[Localization.x5_compAndLimiterPanel_Release];
            this.lblRatio.Text = localization[Localization.x5_compAndLimiterPanel_Ratio];

            this.Invalidate();
            this.Update();
        }

        private void btnSwitchUnit_Click(object sender, EventArgs e)
        {
            config.SwitchThresholdUnit();
            this.eventBus.NotifyObeserver(new LimiterThresholdUnitChangedArgs());
        }
    }
}
