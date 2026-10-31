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
    public partial class GainSlider : Control
    {
        public enum ThumbState
        {
            Normal = 0,
            Hover = 1,
            Pressed = 2,
            PressLeave = 3,
        }

        private const int DefMaxValue = 100;
        private const int DefThumbCenterX = 52;
        private const int DefThumbWidth = 16;
        private const int DefThumbHeight = 9;
        private const int DefThumbStartY = 16;
        private const int DefThumbEndY = 100;

        //private const int DefThumbY = 10;
        //private static readonly Point DefBackLeftTop = new Point();

        private Point _backLeftTop = new Point();
        private int _thubmCenterX = DefThumbCenterX;

        private int _thumbWidth = DefThumbWidth;
        private int _thumbHeight = DefThumbHeight;

        private int _thumbStartY = DefThumbStartY;
        private int _thumbEndY = DefThumbEndY;
        //private int _thumbY = DefThumbY;

        private ThumbState state = ThumbState.Normal;
        private short _value = 0;
        private short _maximum = DefMaxValue;
        private short _upperLimit = DefMaxValue;
        //private int thumbCenterX = 0;
        //private int thumbX = 0;
        private Point thumbCenter;
        private Point thumbLocation;
        private Rectangle thumbRect;

        private int mouseDownYOffset = 0;

        public bool canBeChanged = true;
        //private DateTime lastKeydownTimestamp = DateTime.Now;
        //private Keys lastKey = Keys.None;

        public event EventHandler<EventArgs> ValueChanged;

        #region 属性
        public short initV
        {
            set
            {
                value = Utils.ConstrainMax(value, this._maximum);
                value = Utils.ConstrainMax(value, this._upperLimit);
                //if (this._value != value)
                //{
                //    this._value = value;
                //    this.UpdateThumbPos();
                //    this.UpdateDisplay();
                //}

                this._value = value;
                this.UpdateThumbPos();
                this.UpdateDisplay();
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

        public short v
        {
            get
            {
                return this._value;
            }
            set
            {
                value = Utils.ConstrainMax(value, this._maximum);
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

        public int thubmCenterX
        {
            get
            {
                return this._thubmCenterX;
            }
            set
            {
                this._thubmCenterX = value;
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
        public int thumbStartY
        {
            get
            {
                return this._thumbStartY;
            }
            set
            {
                this._thumbStartY = value;
            }
        }
        public int ThumbEndY
        {
            get
            {
                return this._thumbEndY;
            }
            set
            {
                this._thumbEndY = value;
            }
        }

        public Image background { get; set; }
        public Image thumbNormal { get; set; }
        public Image thumbHover { get; set; }
        public Image thumbPressed { get; set; }
        public Image thumbDisabled { get; set; }

        
        #endregion

        public GainSlider()
        {
            InitializeComponent();
            this.Init();
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

        private void UpdateThumbPos()
        {
            thumbCenter = new Point(thubmCenterX, CalcThumbCenterY(this._value));
            thumbLocation = new Point(thumbCenter.X - thumbWidth / 2, thumbCenter.Y - thumbHeight / 2);
            thumbRect = new Rectangle(thumbLocation, new Size(thumbWidth, thumbHeight));
            //Log.OutputLine("thumb center = {0}, thumb location = {1}, thumb rect = {2}", thumbCenter, thumbLocation, thumbRect);
        }

        private int CalcThumbCenterY(short value)
        {
            Debug.Assert(
                this._maximum > 0 &&
                this._thumbEndY > this._thumbStartY);

            float scale = (float)(_thumbStartY - _thumbEndY) / this._maximum;
            float y = scale * value + _thumbEndY;
            y = (float)Math.Round(y, 0, MidpointRounding.AwayFromZero);
            //AEC.Log.OutputLine("center y = {0}", y);
            return (int)y;
        }

        private short CalcValueFromY(int y)
        {
            Debug.Assert(
                this._maximum > 0 &&
                this._thumbEndY > _thumbStartY);

            y = Math.Min(Math.Max(y, _thumbStartY), _thumbEndY);
            //AEC.Log.OutputLine("thumbY = {0}", y);
            float scale = (float)(this._maximum) / (_thumbStartY - _thumbEndY);
            float value = scale * (y - _thumbEndY);
            value = (float)Math.Round(value, 0, MidpointRounding.AwayFromZero);
            return (short)value;
        }

        #region 重写基类方法
        protected override void OnPaint(PaintEventArgs pe)
        {
            //base.OnPaint(pe);
            //pe.Graphics.FillRectangle(new SolidBrush(Color.Green), this.ClientRectangle);
            this.PaintSlider(pe.Graphics);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!canBeChanged)
            {
                return;
            }

            if (thumbRect.Contains(e.Location))
            {
                this.state = ThumbState.Pressed;
                this.mouseDownYOffset = thumbCenter.Y - e.Y;
                //AEC.Log.OutputLine("OnMouseDown {0}", this.mouseDownYOffset);
                this.Invalidate();
                this.Update();
            }
            else
            {
                //AEC.Log.OutputLine("OnMouseDown Point is not InRect");
                if(e.X >= _backLeftTop.X)
                {
                    if (e.Y > this.thumbCenter.Y)
                    {
                        Decrease();
                    }
                    else if (e.Y < this.thumbCenter.Y)
                    {
                        Increase();
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (!canBeChanged)
            {
                return;
            }

            if (thumbRect.Contains(e.Location))
            {
                this.state = ThumbState.Hover;
            }
            else
            {
                this.state = ThumbState.Normal;
            }
            this.Invalidate();

        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (!canBeChanged)
            {
                return;
            }

            this.state = ThumbState.Normal;
            this.Invalidate();
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
                int thumbY = mouseDownYOffset + e.Y;
                this.v = CalcValueFromY(thumbY);
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

        //protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        //{
        //    base.OnPreviewKeyDown(e);
        //    if (e.KeyCode == Keys.Up ||
        //        e.KeyCode == Keys.Down ||
        //        e.KeyCode == Keys.Left ||
        //        e.KeyCode == Keys.Right)
        //    {
        //        Console.WriteLine(e.KeyCode);
        //        if (this.lastKey != e.KeyCode || (DateTime.Now - lastKeydownTimestamp).Milliseconds >= 250)
        //        {
        //            this.lastKey = e.KeyCode;
        //            lastKeydownTimestamp = DateTime.Now;
        //            switch (e.KeyCode)
        //            {
        //                case Keys.Up:
        //                    this.Increase();
        //                    break;

        //                case Keys.Down:
        //                    this.Decrease();
        //                    break;
        //            }
        //            //AEC.Log.OutputLine("OnPreviewKeyDown{0}", e.KeyCode);
        //        }
        //    }
        //}
        #endregion

        private void PaintSlider(Graphics g)
        {
            //GDIHelper.InitializeGraphics(g);
            PaintBackground(g);
            PaintForeground(g);
        }

        private void PaintBackground(Graphics g)
        {
            if (this.background != null)
            {
                //Rectangle destRect = new Rectangle(_backLeftTop, this.background.Size);
                //Rectangle srcRect = new Rectangle(0, 0, background.Width, background.Height);

                //g.DrawImage(this.background, destRect, srcRect, GraphicsUnit.Pixel);
                GDIHelper.QuickDrawImage(g, _backLeftTop, this.background);
            }
        }

        private void PaintForeground(Graphics g)
        {
            Image thumbImage = GetCurrThumbImage();
            if (thumbImage != null)
            {
                //Rectangle destRect = new Rectangle(thumbLocation.X, thumbLocation.Y, thumbImage.Width, thumbImage.Height);
                //Rectangle srcRect = new Rectangle(0, 0, thumbImage.Width, thumbImage.Height);
                //g.DrawImage(thumbImage, destRect, srcRect, GraphicsUnit.Pixel);
                GDIHelper.QuickDrawImage(g, thumbLocation, thumbImage);
            }
        }

        private Image GetCurrThumbImage()
        {
            if (this.Enabled)
            {
                switch (state)
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

        private void UpdateDisplay()
        {
            //this.Invalidate();
            //this.Update();
            //1111

            Graphics graphics = this.CreateGraphics();
            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics buffer = currentContext.Allocate(graphics, this.ClientRectangle);
            Graphics g = buffer.Graphics;

            using(Brush backBrush = new SolidBrush(this.BackColor))
            {
                g.FillRectangle(backBrush, this.ClientRectangle);
            }
            
            this.PaintSlider(g);
            //this.RenderGraphic(g);

            buffer.Render(graphics);

            g.Dispose();
            buffer.Dispose();
            currentContext.Dispose();
            graphics.Dispose();
        }


        //private void UpOperation()
        //{
        //    if (module.activeItem == ActiveItem.EQ)
        //    {
        //        if (module.eqCanBeChangedByGraphicPanel)
        //        {
        //            if (module.currEQ.gainIndex < DataUtils.maxGainIndex)
        //            {
        //                module.currEQ.gainIndex++;
        //                module.UpdateEQ(module.currEQ);
        //                UpdateDisplay();
        //            }
        //        }
        //    }
        //}

        //private void DownOperation()
        //{
        //    if (module.activeItem == ActiveItem.EQ)
        //    {
        //        if (module.eqCanBeChangedByGraphicPanel)
        //        {
        //            if (module.currEQ.gainIndex > 0)
        //            {
        //                module.currEQ.gainIndex--;
        //                module.UpdateEQ(module.currEQ);
        //                UpdateDisplay();
        //            }
        //        }
        //    }
        //}

        private void Increase()
        {
            if (this._value < this._maximum && this._value < this._upperLimit)
            {
                v = (short)(this._value + 1);
            }
        }

        private void Decrease()
        {
            if (this._value > 0)
            {
                v = (short)(this._value - 1);
            }
        }

        //public static bool PointInRect(Point pt, Rectangle rect)
        //{
        //    if (pt.X >= rect.Left && pt.X <= rect.Right &&
        //        pt.Y >= rect.Top && pt.Y <= rect.Bottom)
        //    {
        //        return true;
        //    }

        //    return false;
        //}
    }
}
