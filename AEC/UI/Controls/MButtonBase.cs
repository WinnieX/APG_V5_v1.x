using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using AEC.UI.Utility;

namespace AEC.UI.Controls
{
    public abstract class MButtonBase : MControl
    {
        //protected int _radius = 12;
        //protected float _borderWidth = 1;

        //protected bool _showBorder = true;
        protected bool _showText = true;
        private bool _mouseIsIn = false;

        protected MButtonState state
        {
            get;
            private set;
        }

        public bool ShowText
        {
            get
            {
                return _showText;
            }
            set
            {
                if (_showText != value)
                {
                    _showText = value;
                    Invalidate();
                }
            }
        }

        //public bool ShowBorder
        //{
        //    get
        //    {
        //        return _showBorder;
        //    }
        //    set
        //    {
        //        if (_showBorder != value)
        //        {
        //            _showBorder = value;
        //            Invalidate();
        //        }
        //    }
        //}

        //public abstract Color BorderColor
        //{
        //    get;
        //}

        public abstract Color FillColor
        {
            get;
        }

        public abstract Color TextColor
        {
            get;
        }

        public virtual Rectangle TextRectangle
        {
            get
            {
                //return new Rectangle(Point.Empty, new Size(this.Width - 1, this.Height - 1));
                int width = this.Width - this.Padding.Left - this.Padding.Right;
                int height = this.Height - this.Padding.Top - this.Padding.Bottom;
                Rectangle rect = new Rectangle(this.Padding.Left, this.Padding.Top, width, height);
                return rect;
            }
        }

        //[Browsable(true)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        //public int Radius
        //{
        //    get
        //    {
        //        return this._radius;
        //    }
        //    set
        //    {
        //        this._radius = value;
        //    }
        //}

        //[Browsable(true)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        //public float BorderWidth
        //{
        //    get
        //    {
        //        return this._borderWidth;
        //    }
        //    set
        //    {
        //        this._borderWidth = value;
        //    }
        //}

        public MButtonBase()
        {
            this.state = MButtonState.Normal;
        }

        private void Init()
        {
            this.SetStyle(ControlStyles.StandardDoubleClick, false);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this._mouseIsIn = true;
            state = MButtonState.Hover;
            UpdateUI();
            //Log.OutputLine("OnMouseEnter");
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if(e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                state = MButtonState.Pressed;
                UpdateUI();
            }
            
            //Log.OutputLine("OnMouseDown");
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (!this._mouseIsIn)
            {
                return;
            }

            //Log.OutputLine("OnMouseUp client:{0}, mouse location:{1}, {2}", this.ClientRectangle, e.Location, e.ToString());
            if(this.ClientRectangle.Contains(e.Location))
            {
                state = MButtonState.Hover;
                //Log.OutputLine("hover");
            }
            else
            {
                state = MButtonState.Normal;
                //Log.OutputLine("normal");
            }
            UpdateUI();
            //Log.OutputLine("OnMouseUp");
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this._mouseIsIn = false;
            state = MButtonState.Normal;
            UpdateUI();
            //Log.OutputLine("OnMouseLeave");
        }

        protected override void PaintBackground(Graphics g)
        {
            //Log.OutputLine("PaintBackground()...{0}", this.state);
            if (this.Visible)
            {
                GraphicsPath roundedRect = RoundRectangle.CreateRoundedRectPath(this, this._radius, base._roundStyle);

                GDIHelper.FillPath(g, this.FillColor, roundedRect);

                if (_showBorder)
                {
                    GDIHelper.DrawPath(g, this.BorderColor, roundedRect, _borderWidth);
                }

                roundedRect.Dispose();
            }
        }

        protected override void PaintForeground(Graphics g)
        {
            if (this.Visible)
            {
                if (_showText && !string.IsNullOrEmpty(this.Text))
                {
                    TextRenderer.DrawText(g, this.Text, this.Font, this.TextRectangle, this.TextColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
            }
        }
    }
}
