using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.UI.Utility;

namespace AEC.UI
{
    public partial class gtButton : Control
    {
        public enum ButtonState
        {
            normal,
            hover,
            pressed,
            //disabled,
        }

        private ButtonState state = ButtonState.normal;

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
        
        private bool _isStretch = false;
        public bool isStretch
        {
            get
            {
                return _isStretch;
            }
            set
            {
                _isStretch = value;
            }
        }

        [Browsable(true)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        public Image backNormal { get; set; }
        public Image backHover { get; set; }
        public Image backPressed { get; set; }
        public Image backDisabled { get; set; }

        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.UpdateDisplay();
            }
        }

        public gtButton()
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

            this.SetStyle(ControlStyles.StandardDoubleClick, false);

            this.BackColor = Color.Transparent;
            this.ForeColor = Color.White;
        }

        #region 重写基类方法

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            state = ButtonState.hover;
            UpdateDisplay();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            state = ButtonState.pressed;
            UpdateDisplay();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            state = ButtonState.hover;
            UpdateDisplay();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            state = ButtonState.normal;
            UpdateDisplay();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            //base.OnPaint(pe);
            PaintBackground(pe.Graphics);
            PaintForeground(pe.Graphics);
        }

        #endregion

        private void PaintBackground(Graphics g)
        {
            Image back = null;
            if (this.Enabled)
            {
                switch(state)
                {
                    case ButtonState.normal:
                        back = backNormal;
                        break;

                    case ButtonState.hover:
                        back = backHover;
                        break;

                    case ButtonState.pressed:
                        back = backPressed;
                        break;
                }
            }
            else
            {
                back = backDisabled;
            }

            if(back != null)
            {
                if (_userGDIPaint)
                {
                    if (this._isStretch)
                    {
                        GDIHelper.QuickDrawImage(g, this.ClientRectangle, back);
                    }
                    else
                    {
                        GDIHelper.QuickDrawImage(g, Point.Empty, back);
                    }
                }
                else
                {
                    Rectangle srcRect = new Rectangle(new Point(), back.Size);
                    Rectangle destRect = _isStretch ? this.ClientRectangle : srcRect;
                    g.DrawImage(back, destRect, srcRect, GraphicsUnit.Pixel);
                }
            }
        }

        private void PaintForeground(Graphics g)
        {
            if (this.Enabled)
            {
                TextRenderer.DrawText(g, this.Text, this.Font, this.ClientRectangle, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
            else
            {
                TextRenderer.DrawText(g, this.Text, this.Font, this.ClientRectangle, Color.FromArgb(0xc0, this.ForeColor), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        public void UpdateDisplay()
        {
            this.Invalidate();
            this.Update();
        }
    }
}
