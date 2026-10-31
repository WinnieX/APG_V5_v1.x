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
using AEC.Effectors.VKxPIIDCM;
using AEC.UI.Common;
using AEC.Common;

namespace AEC.UI.VKxPIIDCM
{
    public partial class VKxPIIDCMLimiterPanel : gtUserControl
    {
        private Limiter limiter;
        private EventBus eventBus;
        private OutputChannelType outputCahnnel;
        private MEffectorThreshold effectorThreshold;

        private PopMenuThresholdUint popMenuThresholdUint;

        private Image _sliderNormalBack; 
        public Image sliderNormalBack
        { 
            get
            {
                return _sliderNormalBack;
            }
            set
            {
                this._sliderNormalBack = value;
                this.sliderThreshold.backgroundNormal = value;
                this.sliderAttack.backgroundNormal = value;
                this.sliderRelease.backgroundNormal = value;
                this.sliderClipThreshold.backgroundNormal = value;
                this.sliderClipRelease.backgroundNormal = value;
            }
        }

        public Image sliderbackgroundDisabled
        {
            get
            {
                return sliderThreshold.backgroundDisabled;
            }
            set
            {
                this.sliderThreshold.backgroundDisabled = value;
                this.sliderAttack.backgroundDisabled = value;
                this.sliderRelease.backgroundDisabled = value;
                this.sliderClipThreshold.backgroundDisabled = value;
                this.sliderClipRelease.backgroundDisabled = value;
            }
        }

        public VKxPIIDCMLimiterPanel()
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

            this.components = new System.ComponentModel.Container();
            this.popMenuThresholdUint = new PopMenuThresholdUint(this.components);
        }

        private void InitFont()
        {
            this.lblThreshold.Font = FontHelper.CreateFont10();
            this.lblAttack.Font = FontHelper.CreateFont10();
            this.lblRelease.Font = FontHelper.CreateFont10();
            this.lblClipThreshold.Font = FontHelper.CreateFont10();
            this.lblClipRelease.Font = FontHelper.CreateFont10();
        }

        public void Binding(Limiter limiter_, EventBus eventBus_, OutputChannelType channel_, MEffectorThreshold effectorThreshold_)
        {
            this.limiter = limiter_;
            this.eventBus = eventBus_;
            this.outputCahnnel = channel_;
            this.effectorThreshold = effectorThreshold_;

            this.popMenuThresholdUint.BindingEventBus(eventBus_);

            this.sliderThreshold.Binding(limiter.threshold, this.effectorThreshold.GetThresholdLabel, this.effectorThreshold.CalcThresholdIndexFromText);
            this.sliderAttack.Binding(limiter.attack, Limiter.GetAttackLabel, Limiter.CalcAttackIndexFromText);
            this.sliderRelease.Binding(limiter.release, Limiter.GetReleaseLabel, Limiter.CalcReleaseIndexFromText);
            this.sliderClipThreshold.Binding(limiter.clipThreshold, Limiter.GetClipThresholdLabel, Limiter.CalcClipThresholdIndexFromText);
            this.sliderClipRelease.Binding(limiter.clipRelease, Limiter.GetReleaseLabel, Limiter.CalcReleaseIndexFromText);
            
            this.eventBus.RegisterHandler(EventBusHandler);
        }

        private void EventBusHandler(object sender, EventBusArgs e)
        {
            if (e.type == EventType.LimiterThresholdUnitChanged)
            {
                LimiterThresholdUnitChangedHandler();
            }
        }

        private void LimiterThresholdUnitChangedHandler()
        {
            UpdateThresholdUI();
        }

        public void UpdateUIFromData()
        {
            this.sliderThreshold.ReInitParamLabel(this.effectorThreshold.GetThresholdLabel, this.effectorThreshold.CalcThresholdIndexFromText);
            this.sliderThreshold.UpdateUIFromData();
            this.sliderAttack.UpdateUIFromData();
            this.sliderRelease.UpdateUIFromData();
            this.sliderClipThreshold.UpdateUIFromData();
            this.sliderClipRelease.UpdateUIFromData();
        }

        public void UpdateThresholdUI()
        {
            this.sliderThreshold.ReInitParamLabel(this.effectorThreshold.GetThresholdLabel, this.effectorThreshold.CalcThresholdIndexFromText);
            this.sliderThreshold.UpdateUIFromData();
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
                this.sliderClipThreshold.SliderWidth = value;
                this.sliderClipRelease.SliderWidth = value;
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
                this.sliderClipThreshold.Width = value;
                this.sliderClipRelease.Width = value;
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
                this.sliderClipThreshold.ThumbEndX = value;
                this.sliderClipRelease.ThumbEndX = value;
            }
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.lblThreshold.Text = localization[Localization.limiterPanel_Threshold];
            this.lblAttack.Text = localization[Localization.limiterPanel_Attack];
            this.lblRelease.Text = localization[Localization.limiterPanel_Release];
            this.lblClipThreshold.Text = localization[Localization.limiterPanel_ClipThreshold];
            this.lblClipRelease.Text = localization[Localization.limiterPanel_ClipRelease];
        }

        private static AEC.Misc.Config config = AEC.Misc.Config.GetInstance();

        private void btnSwitchUnit_Click(object sender, EventArgs e)
        {
            config.SwitchThresholdUnit();
            this.eventBus.NotifyObeserver(new LimiterThresholdUnitChangedArgs());
        }

        private void KXLimiterPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.popMenuThresholdUint.Show();
            }
        }
    }
}
