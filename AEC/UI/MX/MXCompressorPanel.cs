using System;
using System.Drawing;
using System.Windows.Forms;
using AEC.Core;
using AEC.Misc;
using AEC.Effectors;
using AEC.Common;

namespace AEC.UI.MX
{
    public partial class MXCompressorPanel : gtUserControl
    {
        private MXCompressor compressor;
        private EventBus eventBus;
        private MEffectorThreshold effectorThreshold;

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
                this.sliderRatio.backgroundNormal = value;
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
                this.sliderRatio.backgroundDisabled = value;
            }
        }


        public MXCompressorPanel()
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

            this.rdoFast.CheckedChanged += rdoFast_CheckedChanged;
            this.rdoMedium.CheckedChanged += rdoFast_CheckedChanged;
            this.rdoSlow.CheckedChanged += rdoFast_CheckedChanged;
        }

        private void InitFont()
        {
            this.lblThreshold.Font = FontHelper.CreateFont10();
            this.lblAttack.Font = FontHelper.CreateFont10();
            this.lblRelease.Font = FontHelper.CreateFont10();
            this.lblRatio.Font = FontHelper.CreateFont10();
            this.lblMode.Font = FontHelper.CreateFont10();
        }

        public void Binding(MXCompressor compressor_, EventBus eventBus_, MEffectorThreshold effectorThreshold_)
        {
            this.compressor = compressor_;
            this.eventBus = eventBus_;
            this.effectorThreshold = effectorThreshold_;

            this.sliderThreshold.Binding(this.compressor.threshold, this.effectorThreshold.GetThresholdLabel, this.effectorThreshold.CalcThresholdIndexFromText);
            this.sliderAttack.Binding(this.compressor.attack, ParamGetLabelHelper.GetAttackLabel, ParamGetIndexHelper.CalcAttackIndexFromText);
            this.sliderRelease.Binding(this.compressor.release, ParamGetLabelHelper.GetReleaseLabel, ParamGetIndexHelper.CalcReleaseIndexFromText);
            this.sliderRatio.Binding(this.compressor.ratio, ParamGetLabelHelper.GetRatioLabel, ParamGetIndexHelper.CalcRatioIndexFromText);
            this.UpdateCompressorMode();

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
            this.sliderRatio.UpdateUIFromData();
            this.UpdateCompressorMode();
        }

        public void UpdateCompressorMode()
        {
            System.Diagnostics.Debug.Assert(this.compressor != null);

            switch(this.compressor.cm)
            {
                case CompressorMode.Fast:
                    this.rdoFast.isCheckInit = true;
                    break;

                case CompressorMode.Medium:
                    this.rdoMedium.isCheckInit = true;
                    break;

                case CompressorMode.Slow:
                    this.rdoSlow.isCheckInit = true;
                    break;
            }
        }

        private void rdoFast_CheckedChanged(object sender, EventArgs e)
        {
            if (sender.Equals(this.rdoFast))
            {
                this.compressor.cm = CompressorMode.Fast;
            }
            else if (sender.Equals(this.rdoMedium))
            {
                this.compressor.cm = CompressorMode.Medium;
            }
            else if (sender.Equals(this.rdoSlow))
            {
                this.compressor.cm = CompressorMode.Slow;
            }
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
            this.lblThreshold.Text = localization[Localization.limiterPanel_Threshold];
            this.lblAttack.Text = localization[Localization.limiterPanel_Attack];
            this.lblRelease.Text = localization[Localization.limiterPanel_Release];
            this.lblRatio.Text = localization[Localization.limiterPanel_Ratio];
            this.lblMode.Text = localization[Localization.limiterPanel_Mode];
            this.rdoFast.Text = localization[Localization.fastMode];
            this.rdoMedium.Text = localization[Localization.mediumMode];
            this.rdoSlow.Text = localization[Localization.slowMode];
        }

        private static AEC.Misc.Config config = AEC.Misc.Config.GetInstance();

        private void btnSwitchUnit_Click(object sender, EventArgs e)
        {
            config.SwitchThresholdUnit();
            this.eventBus.NotifyObeserver(new LimiterThresholdUnitChangedArgs());
        }
    }
}
