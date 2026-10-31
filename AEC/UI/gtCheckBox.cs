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
    [ToolboxItem(true), DefaultEvent("CheckedChanged")]
    public partial class gtCheckBox : Control
    {
        public enum ButtonState
        {
            normal,
            hover,
            pressed,
            //disabled,
        }

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

        private ButtonState state = ButtonState.normal;
        private bool _isStretch = false;

        private bool _isChecked = false;
        private bool _isAutoCheck = true;
        private bool _isShowCheckedBackWhenDsiabled = true;

        public event EventHandler<EventArgs> CheckedChanged;

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

        public bool isChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                if( _isChecked != value)
                {
                    _isChecked = value;
                    this.UpdateDisplay();
                }
            }
        }

        public bool isAutoCheck
        {
            get
            {
                return _isAutoCheck;
            }
            set
            {
                _isAutoCheck = value;
            }
        }

        public bool isShowCheckedBackWhenDsiabled
        {
            get
            {
                return _isShowCheckedBackWhenDsiabled;
            }
            set
            {
                _isShowCheckedBackWhenDsiabled = value;
            }
        }

        public Image backNormal { get; set; }
        public Image backHover { get; set; }
        public Image backPressed { get; set; }
        public Image backDisabled { get; set; }

        public gtCheckBox()
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
        }

        #region 重写基类方法

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if(!this._isChecked)
            {
                state = ButtonState.hover;
                UpdateDisplay();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (!this._isChecked && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                state = ButtonState.pressed;
                UpdateDisplay();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (!this._isChecked)
            {
                state = ButtonState.hover;
                UpdateDisplay();
            }
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

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (_isAutoCheck && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this._isChecked = !this._isChecked;
                this.UpdateDisplay();
                if(this.CheckedChanged != null)
                {
                    this.CheckedChanged(this, EventArgs.Empty);
                }
            }
        }

        #endregion

        private void PaintBackground(Graphics g)
        {
            Image back = null;

            if(this.Enabled)
            {
                if (_isChecked)
                {
                    back = backPressed;
                }
                else
                {
                    switch (state)
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
            }
            else
            {
                if (_isChecked && _isShowCheckedBackWhenDsiabled)
                {
                    back = backPressed;
                }
                else
                {
                    back = backDisabled;
                }
            }

            //if(_isChecked)
            //{
            //    if (!this.Enabled && _isShowCheckedBackWhenDsiabled)
            //    {
            //        back = backDisabled;
            //    }
            //    else
            //    {
            //        back = backPressed;
            //    }
            //    back = backPressed;
            //}
            //else
            //{
            //    if(Enabled)
            //    {
            //        switch (state)
            //        {
            //            case ButtonState.normal:
            //                back = backNormal;
            //                break;

            //            case ButtonState.hover:
            //                back = backHover;
            //                break;

            //            case ButtonState.pressed:
            //                back = backPressed;
            //                break;
            //        }
            //    }
            //    else
            //    {
            //        back = backDisabled;
            //    }
            //}

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
            //TextRenderer.DrawText(g, this.Text, this.Font, this.ClientRectangle, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            if (this.Enabled || this.isChecked)
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
