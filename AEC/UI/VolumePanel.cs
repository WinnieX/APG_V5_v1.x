using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AEC.Core;

namespace AEC.UI
{
    public partial class VolumePanel : UserControl
    {
        private delegate void VoidCallback();

        private ShortParam volumeParam;
        private BoolParam muteParam;

        private Dictionary<Keys, VoidCallback> key2EventMap;
        private DateTime lastKeydownTimestamp = DateTime.Now;
        private Keys lastKey = Keys.None;

        public event EventHandler MuteChanged;

        public VolumePanel()
        {
            InitializeComponent();

            this.InitFont();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //this.BackColor = Color.Transparent;
            this.hSlider.ValueChanged += Volume_ValueChanged;
            this.volumeLabel.ValueChanged += Volume_ValueChanged;
            this.volumeLabel.MuteChanged += volumeLabel_MuteChanged;

            this.key2EventMap = new Dictionary<Keys, VoidCallback>()
            {
                {Keys.Up, Increase},
                {Keys.Down, Decrease},
                {Keys.Left, Decrease},
                {Keys.Right, Increase},
                {Keys.Add, Increase},
                {Keys.Subtract, Decrease},
            };
        }

        private void InitFont()
        {
            this.lblTag.Font = FontHelper.CreateFont10();
            this.volumeLabel.Font = FontHelper.CreateFont10();
        }

        public string tag
        {
            get
            {
                return this.lblTag.Text;
            }
            set
            {
                this.lblTag.UpdateText(value);
            }
        }

        public short upperLimit
        {
            get
            {
                return this.hSlider.upperLimit;
            }
            set
            {
                //Log.OutputLine("volume panel upperLimit:{0}", value);
                this.hSlider.upperLimit = value;
                this.volumeLabel.upperLimit = value;
                //Log.OutputLine("slider value:{0}, label value:{0}", this.hSlider.v, this.volumeLabel.v);
            }
        }

        public bool canBeChanged
        {
            get
            {
                return this.hSlider.canBeChanged;
            }
            set
            {
                this.hSlider.canBeChanged = value;
                this.volumeLabel.canBeChanged = value;
            }
        }

        public void Binding(ShortParam volumeParam_, BoolParam muteParam_)
        {
            Debug.Assert(volumeParam_ != null);
            this.volumeParam = volumeParam_;
            this.muteParam = muteParam_;
            this.hSlider.Init(volumeParam_);
            this.volumeLabel.Init(volumeParam_, muteParam_);

            this.hSlider.canBeChanged = this.muteParam != null ? !this.muteParam.value : true;
            this.hSlider.UpdateDisplay();
        }

        public void UpdateUIFromData()
        {
            //Log.OutputLine("Volume Panel UpdateUIFromData(), Level:{0} mute:{1}", volumeParam.v, muteParam.value);
            this.hSlider.initV = this.volumeParam.v;
            this.volumeLabel.initV = this.volumeParam.v;
            if (this.muteParam != null)
            {
                this.hSlider.canBeChanged = !this.muteParam.value;
                this.volumeLabel.canBeChanged = !this.muteParam.value;
            }
            this.hSlider.UpdateDisplay();
        }

        private void Volume_ValueChanged(object sender, EventArgs e)
        {
            if (this.volumeParam == null)
            {
                return;
            }

            this.Focus();

            if(sender.Equals(this.hSlider))
            {
                this.volumeLabel.initV = this.hSlider.v;
            }
            else if(sender.Equals(this.volumeLabel))
            {
                this.hSlider.initV = this.volumeLabel.v;
            }

            this.volumeParam.v = this.hSlider.v;
        }

        private void volumeLabel_MuteChanged(object sender, EventArgs e)
        {
            if (this.muteParam == null)
            {
                return;
            }

            this.hSlider.canBeChanged = !this.muteParam.value;
            this.hSlider.UpdateDisplay();

            if(MuteChanged != null)
            {
                MuteChanged(this, EventArgs.Empty);
            }
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

        private void Increase()
        {
            if(!this.canBeChanged)
            {
                return;
            }

            if (this.volumeParam.v < this.volumeParam.max)
            {
                this.volumeParam.v += 1;
                this.volumeLabel.initV = this.volumeParam.v;
                this.hSlider.initV = this.volumeParam.v;
            }
        }

        private void Decrease()
        {
            if (!this.canBeChanged)
            {
                return;
            }

            if (this.volumeParam.v > 0)
            {
                this.volumeParam.v -= 1;
                this.volumeLabel.initV = this.volumeParam.v;
                this.hSlider.initV = this.volumeParam.v;
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
                this.lblTag.BackColor = value;
                this.volumeLabel.BackColor = value;
            }
        }
    }
}
