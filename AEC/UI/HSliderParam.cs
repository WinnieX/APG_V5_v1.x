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
    [ToolboxItem(true), DefaultEvent("ValueChanged")]
    public partial class HSliderParam : UserControl
    {
        private delegate void VoidCallback();

        public event EventHandler<EventArgs> ValueChanged;
        public event EventHandler<EventArgs> Actived;
        public event EventHandler<EventArgs> LabelDoubleClick;

        private ShortParam param;

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

        public int thumbCenterY
        {
            get
            {
                return this.hSlider.thumbCenterY;
            }
            set
            {
                this.hSlider.thumbCenterY = value;
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

        public int SliderWidth
        {
            get
            {
                return this.hSlider.Width;
            }
            set
            {
                this.hSlider.Anchor = AnchorStyles.None;
                this.paramLabel.Anchor = AnchorStyles.None;
                this.hSlider.Width = value;
                this.paramLabel.Location = new Point(this.hSlider.Width, this.paramLabel.Location.Y);
                this.paramLabel.Width = this.Width - this.hSlider.Width;
                this.hSlider.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                this.paramLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }
        }

        public short value
        {
            get
            {
                if(this.param != null)
                {
                    return this.param.v;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if(this.param != null)
                {
                    this.param.initV = value;
                    this.hSlider.initV = this.param.v;
                    this.paramLabel.initV = this.param.v;
                }
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
                this.hSlider.upperLimit = value;
                this.paramLabel.upperLimit = value;
            }
        }

        public HSliderParam()
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

            this.MouseDown += HSliderParam_MouseDown;
            this.hSlider.MouseDown += HSliderParam_MouseDown;
            this.paramLabel.MouseDown += HSliderParam_MouseDown;
            this.paramLabel.MouseDoubleClick += paramLabel_MouseDoubleClick;

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

        private void HSliderParam_MouseDown(object sender, MouseEventArgs e)
        {
            if(Actived != null)
            {
                Actived(this, EventArgs.Empty);
            }
        }

        //public void Init(
        //    short max_,
        //    short curr_,
        //    GetLabelDelegate getLabel_,
        //    CalcIndexFromEditTextDelegate calcIndexFromEditText_
        //    )
        //{
        //    Debug.Assert(
        //        max_ > 0 &&
        //        curr_ >= 0 &&
        //        curr_ <= max_ &&
        //        getLabel_ != null &&
        //        calcIndexFromEditText_ != null);

        //    this.hSlider.Init(max_, curr_);
        //    this.paramLabel.Init(max_, curr_, getLabel_, calcIndexFromEditText_);
        //}

        public void Binding(
            ShortParam param_,
            GetLabelDelegate getLabel_,
            CalcIndexFromEditTextDelegate calcIndexFromEditText_ = null
            )
        {
            Debug.Assert(
                param_ != null &&
                getLabel_ != null);

            this.param = param_;
            this.hSlider.Init(this.param.max, this.param.v);
            this.paramLabel.Init(this.param.max, this.param.v, getLabel_, calcIndexFromEditText_);
        }

        public void ReInitParamLabel(GetLabelDelegate getLabel_, CalcIndexFromEditTextDelegate calcIndexFromEditText_ = null)
        {
            this.paramLabel.Init(this.param.max, this.param.v, getLabel_, calcIndexFromEditText_);
        }

        public void UpdateUIFromData()
        {
            //AEC.Log.OutputLine("UpdateUIFromData Music hpfFreqParam.v = {0} hash = {1}", this.param.v, this.param.GetHashCode());
            this.hSlider.initV = this.param.v;
            this.paramLabel.initV = this.param.v;
        }

        private void Param_ValueChanged(object sender, EventArgs e)
        {
            if(this.param == null)
            {
                return;
            }

            this.Focus();

            if(sender.Equals(this.hSlider))
            {
                this.paramLabel.initV = this.hSlider.v;
            }
            else if(sender.Equals(this.paramLabel))
            {
                this.hSlider.initV = this.paramLabel.v;
            }

            this.param.v = this.hSlider.v;

            this.OnValueChanged();
        }

        private void OnValueChanged()
        {
            if(ValueChanged != null)
            {
                ValueChanged(this, EventArgs.Empty);
            }
        }

        private void paramLabel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(this.LabelDoubleClick != null)
            {
                this.LabelDoubleClick(this, EventArgs.Empty);
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
            if(this.param.v < this.param.max)
            {
                this.param.v += 1;
                this.paramLabel.initV = this.param.v;
                this.hSlider.initV = this.param.v;
                this.OnValueChanged();
            }
        }

        private void Decrease()
        {
            if (this.param.v > 0)
            {
                this.param.v -= 1;
                this.paramLabel.initV = this.param.v;
                this.hSlider.initV = this.param.v;
                this.OnValueChanged();
            }
        }

        public override Color BackColor 
        { 
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                this.paramLabel.BackColor = value;
                this.hSlider.BackColor = value;
            }
        }
    }
}
