using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AEC.Core;
using AEC.Common;

namespace AEC.UI
{
    public partial class SignalMixerPanel : UserControl
    {
        private delegate void VoidCallback();

        protected const string Precent = "%";

        protected SignalMixer signalMixer;

        private Dictionary<Keys, VoidCallback> key2EventMap;
        private DateTime lastKeydownTimestamp = DateTime.Now;
        private Keys lastKey = Keys.None;

        public Image backgroundNormal
        {
            get
            {
                return this.hSlider.backgroundNormal;
            }
            set
            {
                this.hSlider.backgroundNormal = value;
            }
        }

        public Image backgroundDisabled
        {
            get
            {
                return this.hSlider.backgroundDisabled;
            }
            set
            {
                this.hSlider.backgroundDisabled = value;
            }
        }

        public int ThumbEndX
        {
            get
            {
                return this.hSlider.ThumbEndX;
            }
            set
            {
                this.hSlider.ThumbEndX = value;
            }
        }

        public int thumbCenterY
        {
            get
            {
                return this.hSlider.thumbCenterY;
            }
            set
            {
                this.hSlider.thumbCenterY = value;
                this.UpdateBtnLocation();
                //this.Invalidate();
                //this.Update();
            }
        }

        public Point backLeftTop
        {
            get
            {
                return this.hSlider.backLeftTop;
            }
            set
            {
                this.hSlider.backLeftTop = value;
            }
        }

        public int SliderWidth
        {
            get
            {
                return this.hSlider.Width;
            }
            set
            {
                this.hSlider.Anchor = AnchorStyles.None;
                this.btnPhase.Anchor = AnchorStyles.None;
                this.paramLabel.Anchor = AnchorStyles.None;
                this.hSlider.Width = value;
                this.btnPhase.Location = new Point(this.hSlider.Width, this.btnPhase.Location.Y);
                this.paramLabel.Location = new Point(this.hSlider.Width + this.btnPhase.Width, this.paramLabel.Location.Y);
                this.paramLabel.Width = this.Width - this.hSlider.Width - this.btnPhase.Width;
                this.hSlider.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                this.btnPhase.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                this.paramLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }
        }

        public Color FillColor
        {
            get
            {
                return this.BackColor;
            }
            set
            {
                this.BackColor = value;
                this.hSlider.BackColor = value;
                this.btnPhase.BackColor = value;
                this.paramLabel.BackColor = value;
            }
        }

        public SignalMixerPanel()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.hSlider.ValueChanged += Param_ValueChanged;
            this.paramLabel.ValueChanged += Param_ValueChanged;
            this.btnPhase.Click += btnPhase_Click;

            this.key2EventMap = new Dictionary<Keys, VoidCallback>()
            {
                {Keys.Up, IncreaseLevel},
                {Keys.Down, DecreaseLevel},
                {Keys.Left, DecreaseLevel},
                {Keys.Right, IncreaseLevel},
                {Keys.Add, PhasePositive},
                {Keys.Subtract, PhaseNegative},
            };
        }

        private void btnPhase_Click(object sender, EventArgs e)
        {
            this.Focus();

            if(this.signalMixer != null)
            {
                this.signalMixer.phase.value = !this.signalMixer.phase.value;
                this.UpdateButtonBack();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.hSlider.Height = this.Height;
            this.UpdateBtnLocation();
        }

        private void UpdateBtnLocation()
        {
            this.btnPhase.Location = new Point(this.btnPhase.Location.X, this.hSlider.thumbCenterY - this.btnPhase.Height / 2);
            this.paramLabel.Location = new Point(this.paramLabel.Location.X, this.hSlider.thumbCenterY - this.paramLabel.Height / 2);
        }

        public void Binding(SignalMixer level_)
        {
            Debug.Assert(level_ != null);

            this.signalMixer = level_;
            this.hSlider.Init(this.signalMixer.maxLevel, this.signalMixer.level.v);
            this.paramLabel.Init(this.signalMixer.maxLevel, this.signalMixer.level.v, GetLevelLabel, CalcLevelIndexFromText);
            this.UpdateButtonBack();
        }

        public void UpdateUIFromData()
        {
            this.hSlider.initV = this.signalMixer.level.v;
            this.paramLabel.initV = this.signalMixer.level.v;
            this.UpdateButtonBack();
            this.Invalidate(new Rectangle(this.btnPhase.Location, this.btnPhase.Size));
            this.Update();
        }

        private void Param_ValueChanged(object sender, EventArgs e)
        {
            if(this.signalMixer == null)
            {
                return;
            }

            if (sender.Equals(this.hSlider))
            {
                this.paramLabel.initV = this.hSlider.v;
            }
            else if (sender.Equals(this.paramLabel))
            {
                this.hSlider.initV = this.paramLabel.v;
            }

            this.signalMixer.level.v = this.hSlider.v;
        }

        protected virtual string GetLevelLabel(short index)
        {
            return ParamGetLabelHelper.EffectGetMixerLevel(index);
        }

        protected virtual short CalcLevelIndexFromText(string text)
        {
            return ParamGetIndexHelper.CalcEffectMixerLevelIndexFromText(text);
        }

        public void UpdateButtonBack()
        {
            if(this.signalMixer == null)
            {
                return;
            }

            bool isPositive = this.signalMixer.phase.value;

            this.btnPhase.backDisabled = isPositive ?  global::AEC.Properties.Resources.phase_positive_disabled : global::AEC.Properties.Resources.phase_negative_disabled;
            this.btnPhase.backHover = isPositive ? global::AEC.Properties.Resources.phase_positive_hover : global::AEC.Properties.Resources.phase_negative_hover;
            this.btnPhase.backNormal = isPositive ? global::AEC.Properties.Resources.phase_positive_normal : global::AEC.Properties.Resources.phase_negative_normal;
            this.btnPhase.backPressed = isPositive ? global::AEC.Properties.Resources.phase_positive_pressed : global::AEC.Properties.Resources.phase_negative_pressed;
            this.btnPhase.Invalidate();
            this.btnPhase.Update();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            //AEC.Log.OutputLine("HSliderParam ProcessDialogKey{0}", keyData);
            if (key2EventMap.ContainsKey(keyData))
            {
                if (this.lastKey != keyData || (DateTime.Now - lastKeydownTimestamp).Milliseconds >= 250)
                {
                    this.lastKey = keyData;
                    lastKeydownTimestamp = DateTime.Now;

                    this.key2EventMap[keyData]();
                }

                return false;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void IncreaseLevel()
        {
            if (this.signalMixer == null)
            {
                return;
            }

            if (this.signalMixer.level.v < this.signalMixer.level.max)
            {
                this.signalMixer.level.v += 1;
                this.paramLabel.initV = this.signalMixer.level.v;
                this.hSlider.initV = this.signalMixer.level.v;
            }
        }

        private void DecreaseLevel()
        {
            if (this.signalMixer == null)
            {
                return;
            }

            if (this.signalMixer.level.v > 0)
            {
                this.signalMixer.level.v -= 1;
                this.paramLabel.initV = this.signalMixer.level.v;
                this.hSlider.initV = this.signalMixer.level.v;
            }
        }

        public void PhasePositive()
        {
            if(this.signalMixer == null || this.signalMixer.phase.value)
            {
                return;
            }

            this.signalMixer.phase.value = true;
            this.UpdateButtonBack();
        }

        public void PhaseNegative()
        {
            if (this.signalMixer == null || !this.signalMixer.phase.value)
            {
                return;
            }

            this.signalMixer.phase.value = false;
            this.UpdateButtonBack();
        }
    }

    
}
