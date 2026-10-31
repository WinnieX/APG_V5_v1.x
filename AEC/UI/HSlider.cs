using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AEC.Core;
using AEC.UI.Utility;

namespace AEC.UI
{
    [ToolboxItem(true)]
    public partial class HSlider : Control
    {
        public enum ThumbState
        {
            Normal = 0,
            Hover = 1,
            Pressed = 2,
            PressLeave = 3,
        }

        private const int DefMaxValue = 100;
        private const int DefThumbWidth = 22;
        private const int DefThumbHeight = 18;
        private const int DefThumbStartX = 11;
        private const int DefThumbEndX = 176;
        private const int DefThumbCenterY = 11;

        protected Point _backLeftTop = new Point();
        private int _thumbWidth = DefThumbWidth;
        private int _thumbHeight = DefThumbHeight;
        private int _thumbStartX = DefThumbStartX;
        private int _thumbEndX = DefThumbEndX;
        private int _thumbCenterY = DefThumbCenterY;

        private ThumbState state = ThumbState.Normal;
        private short _value = 0;
        private short _maximum = DefMaxValue;
        private short _upperLimit = DefMaxValue;
        //private int thumbCenterX = 0;
        //private int thumbX = 0;
        private Point thumbCenter;
        private Point thumbLocation;

        private int mouseDownXOffset = 0;

        public event EventHandler<EventArgs> ValueChanged;

        public bool canBeChanged = true;

        private bool _userGDIPaint = true;
        public bool userGDIPaint
        {
            get
            {
                return _userGDIPaint;
            }
            set
            {
                _userGDIPaint = value;
            }
        }

        #region 属性
        public short initV
        {
            set
            {
                //value = Utils.ConstrainMax(value, this._maximum);
                value = Utils.ConstrainMax(value, this._upperLimit);
                if (this._value != value)
                {
                    this._value = value;
                    this.UpdateThumbPos();
                    this.UpdateDisplay();
                }
            }
        }

        public short maximum
        {
            get
            {
                return this._maximum;
            }
            set
            {
                _maximum = value;
                _upperLimit = _maximum;
            }
        }

        public short upperLimit
        {
            get
            {
                return this._upperLimit;
            }
            set
            {
                value = Utils.ConstrainMax(value, this._maximum);
                _upperLimit = value;
                if(_upperLimit < this._value)
                {
                    this.initV = this._upperLimit;
                }
            }
        }

        public short v
        {
            get
            {
                return this._value;
            }
            set
            {
                //value = Utils.ConstrainMax(value, this._maximum);
                value = Utils.ConstrainMax(value, this._upperLimit);
                if (this._value != value)
                {
                    this._value = value;
                    this.UpdateThumbPos();
                    this.UpdateDisplay();
                    if (ValueChanged != null)
                    {
                        ValueChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public Point backLeftTop
        {
            get
            {
                return _backLeftTop;
            }
            set
            {
                _backLeftTop = value;
            }
        }

        public int thumbWidth 
        { 
            get
            {
                return this._thumbWidth;
            }
            set
            {
                this._thumbWidth = value;
            }
        }
        public int thumbHeight
        {
            get
            {
                return this._thumbHeight;
            }
            set
            {
                this._thumbHeight = value;
            }
        }
        public int thumbStartX
        {
            get
            {
                return this._thumbStartX;
            }
            set
            {
                this._thumbStartX = value;
            }
        }
        public int ThumbEndX
        {
            get
            {
                return this._thumbEndX;
            }
            set
            {
                this._thumbEndX = value;
            }
        }
        public int thumbCenterY
        {
            get
            {
                return this._thumbCenterY;
            }
            set
            {
                this._thumbCenterY = value;
                this.UpdateThumbPos();
                //this.UpdateDisplay();
            }
        }

        public Image backgroundNormal { get; set; }
        public Image backgroundDisabled { get; set; }
        public Image thumbNormal { get; set; }
        public Image thumbHover { get; set; }
        public Image thumbPressed { get; set; }
        public Image thumbDisabled { get; set; }

        protected Rectangle thumbRect
        {
            get
            {
                return new Rectangle(thumbCenter.X - thumbWidth / 2, thumbCenter.Y - thumbHeight / 2, thumbWidth, thumbHeight);
            }
        }
        #endregion

        public HSlider()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            this.UpdateThumbPos();
        }

        public void Init(short max, short value)
        {
            this.maximum = max;
            this.initV = value;
        }

        protected void UpdateThumbPos()
        {
            thumbCenter = new Point(CalcThumbCenterX(this._value), _thumbCenterY);
            thumbLocation = new Point(thumbCenter.X - thumbWidth / 2, thumbCenter.Y - thumbHeight / 2 + 1);
        }

        protected int CalcThumbCenterX(short value)
        {
            Debug.Assert(
                this._maximum > 0 &&
                this.ThumbEndX > thumbStartX);

            float scale = (float)(_thumbEndX - _thumbStartX) / (this._maximum);
            float x = scale * value + _thumbStartX;
            x = (float)Math.Round(x, 0, MidpointRounding.AwayFromZero);
            return (int)x;
        }

        protected short CalcValueFromX(int x)
        {
            Debug.Assert(
                this._maximum > 0 &&
                this.ThumbEndX > thumbStartX);

            x = Math.Min(Math.Max(x, _thumbStartX), _thumbEndX);
            float scale = (float)(this._maximum) / (_thumbEndX - _thumbStartX);
            float value = scale * (x - _thumbStartX);
            value = (float)Math.Round(value, 0, MidpointRounding.AwayFromZero);
            return (short)value;
        }

        #region 重写基类方法
        protected override void OnPaint(PaintEventArgs pe)
        {
            //base.OnPaint(pe);
            this.PaintSlider(pe.Graphics);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!canBeChanged || e.Button != System.Windows.Forms.MouseButtons.Left)
            {
                return;
            }

            this.Focus();
            
            if (thumbRect.Contains(e.Location))
            {
                OnMouseClickThumb(thumbCenter.X);

                this.mouseDownXOffset = thumbCenter.X - e.Location.X;
                this.state = ThumbState.Pressed;
                this.Invalidate();
                this.Update();
            }
            else
            {
                int x = Utils.ConstrainValue(e.X, _thumbStartX, _thumbEndX);
                if(x > this.thumbCenter.X)
                {
                    Increase();
                }
                else if (x < this.thumbCenter.X)
                {
                    Decrease();
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (thumbRect.Contains(e.Location))
            {
                this.state = ThumbState.Hover;
            }
            else
            {
                this.state = ThumbState.Normal;
            }
            this.Invalidate();
            this.OnCalcelMouseOperation();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            this.state = ThumbState.Normal;
            this.Invalidate();
            this.OnCalcelMouseOperation();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!canBeChanged)
            {
                return;
            }

            if (this.state == ThumbState.Pressed)
            {
                int thumbCenterX = mouseDownXOffset + e.X;

                OnMouseMoveThumb(thumbCenterX);
            }
            else
            {
                if (thumbRect.Contains(e.Location))
                {
                    this.state = ThumbState.Hover;
                    this.Invalidate();
                    //if (!mouseInBtn)
                    //{
                    //    this._btnState = lmButtonState.Hover;
                    //    this.Invalidate();
                    //    mouseInBtn = true;
                    //}
                }
                else
                {
                    this.state = ThumbState.Normal;
                    this.Invalidate();
                    //if (mouseInBtn)
                    //{
                    //    mouseInBtn = false;
                    //    this._btnState = lmButtonState.Normal;
                    //    this.Invalidate();
                    //}
                }
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (!canBeChanged)
            {
                return;
            }

            HandledMouseEventArgs h = e as HandledMouseEventArgs;
            if (h != null)
            {
                h.Handled = true;
            }

            if (e.Delta > 0)
            {
                this.Increase();
            }
            else
            {
                this.Decrease();
            }
        }
        #endregion

        protected virtual void OnMouseClickThumb(int thumbCenterX)
        {
            
        }

        protected virtual void OnMouseMoveThumb(int thumbCenterX)
        {
            this.v = CalcValueFromX(thumbCenterX);
        }

        protected virtual void OnCalcelMouseOperation()
        {
            
        }

        protected virtual void PaintSlider(Graphics g)
        {
            //GDIHelper.InitializeGraphics(g);
            PaintBackground(g);
            PaintForeground(g);
        }

        protected virtual void PaintBackground(Graphics g)
        {
            //g.FillRectangle(new SolidBrush(Color.Green), this.ClientRectangle);
            Image background = null;
            if (this.Enabled)
            {
                background = this.backgroundNormal;
            }
            else if (this.backgroundDisabled != null)
            {
                background = this.backgroundDisabled;
            }
            else
            {
                background = this.backgroundNormal;
            }

            if (background == null)
            {
                return;
            }

            if (_userGDIPaint)
            {
                GDIHelper.QuickDrawImage(g, _backLeftTop, background);
            }
            else
            {
                Rectangle destRect = new Rectangle(_backLeftTop, background.Size);
                Rectangle srcRect = new Rectangle(0, 0, background.Width, background.Height);

                g.DrawImage(background, destRect, srcRect, GraphicsUnit.Pixel);
            }
        }

        protected virtual void PaintForeground(Graphics g)
        {
            Image thumbImage = GetCurrThumbImage();
            if(thumbImage != null)
            {
                if (_userGDIPaint)
                {
                    GDIHelper.QuickDrawImage(g, thumbLocation, thumbImage);
                }
                else
                {
                    Rectangle destRect = new Rectangle(thumbLocation.X, thumbLocation.Y, thumbImage.Width, thumbImage.Height);
                    Rectangle srcRect = new Rectangle(0, 0, thumbImage.Width, thumbImage.Height);
                    g.DrawImage(thumbImage, destRect, srcRect, GraphicsUnit.Pixel);
                }
            }
        }

        private Image GetCurrThumbImage()
        {
            if(this.Enabled)
            {
                switch(state)
                {
                    case ThumbState.Normal:
                        return thumbNormal;

                    case ThumbState.Hover:
                        return thumbHover;

                    case ThumbState.Pressed:
                        return thumbPressed;

                    default:
                        return thumbNormal;
                }
            }
            else
            {
                return thumbDisabled;
            }
        }

        public void UpdateDisplay()
        {
            this.Invalidate();
            this.Update();
        }

        protected void Increase()
        {
            if (this._value < this._maximum && this._value < this._upperLimit)
            {
                v = (short)(this._value + 1);
            }
        }

        protected void Decrease()
        {
            if (this._value > 0)
            {
                v = (short)(this._value - 1);
            }
        }
    }

    public class VolumeSlider:HSlider
    {
        private ShortParam volumeParam;

        public Point levelLocation { get; set; }

        public VolumeSlider():
            base()
        {
        }

        public void Init(ShortParam volumeParam_)
        {
            Debug.Assert(volumeParam_ != null);
            this.volumeParam = volumeParam_;
            base.Init(volumeParam.max, volumeParam.v);
            base.UpdateThumbPos();
        }

        protected override void PaintBackground(Graphics g)
        {
            base.PaintBackground(g);
        }

        protected override void PaintForeground(Graphics g)
        {
            if(!base.canBeChanged)
            {
                return;
            }

            base.PaintForeground(g);
        }

        //private int thumbCenterXMouseDown = 0;
        private short targetV = 0;
        private Timer timer = null;
        //private bool firstOver = true;
        private const short step = 5;

        private void timer_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //Log.OutputLine("this is focus:{0}", this.Focused);
            if(!this.Focused)
            {
                CancelTimer();
                return;
            }

            if (base.v < this.targetV && base.v < base.upperLimit)
            {
                if(this.targetV - base.v > step)
                {
                    base.v += step;
                }
                else
                {
                    base.v = this.targetV;
                }
            }
        }

        protected override void OnMouseClickThumb(int thumbCenterX)
        {
            //thumbCenterXMouseDown = thumbCenterX;
            
            //AEC.Log.OutputLine("thumbCenterXMouseDown:{0}", thumbCenterXMouseDown);
            
            CancelTimer();

            this.targetV = base.v;
            timer = new Timer();
            timer.Interval = 30;
            timer.Tick += timer_Tick;
            timer.Start();

            //this.firstOver = true;
        }

        //private int count = 0;
        protected override void OnMouseMoveThumb(int thumbCenterX)
        {
            //Log.OutputLine("x:{0} [{1}]", thumbCenterX, ++count);
            this.targetV = CalcValueFromX(thumbCenterX);
            if(base.v > targetV)
            {
                base.v = targetV;
                //this.firstOver = true;
            }

            //int minX = CalcThumbCenterX(0);
            //int maxX = CalcThumbCenterX(82);
            //Log.OutputLine("min:{0} max{1}", minX, maxX);

            //else if(base.v > targetV)
            //{

            //}
            //if(thumbCenterX <= thumbCenterXMouseDown)
            //{
            //    base.v = CalcValueFromX(thumbCenterX);
            //    this.targetV = base.v;
            //    this.firstOver = true;
            //}
            //else
            //{
            //    if (this.firstOver)
            //    {
            //        base.v = CalcValueFromX(thumbCenterXMouseDown);
            //        this.targetV = CalcValueFromX(thumbCenterX);
            //        this.firstOver = false;
            //    }
            //    else
            //    {
            //        //AEC.Log.OutputLine("thumbCenterX:{0}", thumbCenterX);
            //        targetV = CalcValueFromX(thumbCenterX);
            //    }
            //}
        }

        protected override void OnCalcelMouseOperation()
        {
            CancelTimer();
        }

        private void CancelTimer()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
            }
        }
    }

    public class EasyModeVolumeSlider : HSlider
    {
        public Point foreImageLocation { get; set; }
        public Image foregroundNormal { get; set; }
        public Image foregroundDisabled { get; set; }

        public EasyModeVolumeSlider() :
            base()
        {
        }

        public void Init(ShortParam volumeParam_)
        {
            Debug.Assert(volumeParam_ != null);
            base.Init(volumeParam_.max, volumeParam_.v);
            base.UpdateThumbPos();
        }

        //protected override void PaintSlider(Graphics g)
        //{
        //    //GDIHelper.InitializeGraphics(g);
        //    this.PaintBackground(g);
        //    this.PaintForeground(g);
        //}

        //protected override void PaintBackground(Graphics g)
        //{
        //    base.PaintBackground(g);
        //}

        protected override void PaintForeground(Graphics g)
        {
            Image foreground = this.Enabled ? foregroundNormal : foregroundDisabled;
            if (foreground != null && base.v > 0 && base.maximum >= base.v)
            {
                int rectWidth = (int)Math.Round(((float)(base.v)) / base.maximum * foreground.Width, 0, MidpointRounding.AwayFromZero);
                Rectangle srcRect = new Rectangle(new Point(), new Size(rectWidth, foreground.Height));
                Rectangle destRect = new Rectangle(foreImageLocation, srcRect.Size);
                g.DrawImage(foreground, destRect, srcRect, GraphicsUnit.Pixel);
            }

            base.PaintForeground(g);
        }
    }
}
