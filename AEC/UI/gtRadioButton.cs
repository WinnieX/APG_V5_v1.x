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
    public partial class gtRadioButton : Control
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

        protected ButtonState state = ButtonState.normal;
        private bool _isStretch = false;

        protected bool _isChecked = false;
        private bool _isAutoCheck = true;
        private bool _isShowCheckedBackWhenDsiabled = true;

        public bool canBeChanged = true;

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

        public bool isCheckInit
        {
            get
            {
                return _isChecked;
            }
            set
            {
                if (_isChecked = value)
                {
                    _isChecked = value;
                    if (value)
                    {
                        UnCheckOtherRadioButton();
                    }
                    
                    this.UpdateDisplay();
                }
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
                if (_isChecked != value)
                {
                    _isChecked = value;
                    
                    if(value)
                    {
                        UnCheckOtherRadioButton();
                    }

                    this.UpdateDisplay();

                    if (this.CheckedChanged != null)
                    {
                        this.CheckedChanged(this, EventArgs.Empty);
                    }
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
        public Image backChecked { get; set; }
        public Image backDisabled { get; set; }

        public gtRadioButton()
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

            this.UpdateStyles();

            this.BackColor = Color.Transparent;
        }

        #region 重写基类方法

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            if (!canBeChanged)
            {
                return;
            }

            if(!this._isChecked)
            {
                state = ButtonState.hover;
                UpdateDisplay();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.Focus();
            if (!canBeChanged || e.Button != System.Windows.Forms.MouseButtons.Left)
            {
                return;
            }

            if (!this._isChecked)
            {
                state = ButtonState.pressed;
                UpdateDisplay();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!canBeChanged || e.Button != System.Windows.Forms.MouseButtons.Left)
            {
                return;
            }

            if (!this._isChecked)
            {
                state = ButtonState.hover;
                UpdateDisplay();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (!canBeChanged)
            {
                return;
            }

            state = ButtonState.normal;
            UpdateDisplay();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            this.PaintRadioButton(pe.Graphics);
        }

        protected virtual void PaintRadioButton(Graphics g)
        {
            PaintBackground(g);
            PaintForeground(g);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            //AEC.Log.OutputLine("OnMouseClick");
            base.OnMouseClick(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left && _isAutoCheck && !this._isChecked && canBeChanged)
            {
                this.isChecked = true;
            }
        }

        //protected override void OnClick(EventArgs e)
        //{
            
        //}

        #endregion

        protected virtual Image back
        {
            get
            {
                Image back = null;

                if (this.Enabled)
                {
                    if (_isChecked)
                    {
                        back = backChecked;
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
                                back = backChecked;
                                break;
                        }
                    }
                }
                else
                {
                    if (this._isChecked && _isShowCheckedBackWhenDsiabled)
                    {
                        back = backChecked;
                    }
                    else
                    {
                        back = backDisabled;
                    }
                }

                //if (_isChecked)
                //{
                //    back = backChecked;
                //}
                //else
                //{
                //    switch (state)
                //    {
                //        case ButtonState.normal:
                //            back = backNormal;
                //            break;

                //        case ButtonState.hover:
                //            back = backHover;
                //            break;

                //        case ButtonState.pressed:
                //            back = backChecked;
                //            break;
                //    }

                //    if (!this.Enabled)
                //    {
                //        back = backDisabled;
                //    }
                //}

                return back;
            }
        }

        protected virtual void PaintBackground(Graphics g)
        {
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

        protected virtual void PaintForeground(Graphics g)
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

        private void UnCheckOtherRadioButton()
        {
            Control parent = this.Parent;
            if (parent != null && parent.HasChildren)
            {
                foreach (Control control in parent.Controls)
                {
                    if (control != this && control is gtRadioButton)
                    {
                        gtRadioButton rdo = control as gtRadioButton;
                        if(rdo._isChecked)
                        {
                            rdo.isChecked = false;
                        }
                    }
                }
            }
        }

        public void UpdateDisplay()
        {
            this.Invalidate();
            this.Update();
        }
    }

    public enum DisplayOrder
    {
        BoxText,
        TextBox
    }

    [ToolboxBitmap(typeof(RadioButton))]
    public class gtRadioButtonWithText : gtRadioButton
    {
        private DisplayOrder _DisplayOrder = DisplayOrder.BoxText;
        //private Point _imageLeftTop = new Point(0, 0);

        [Browsable(true)]
        [DefaultValue(DisplayOrder.BoxText)]
        [Description("显示顺序")]
        public DisplayOrder DisplayOrder
        {
            get
            {
                return this._DisplayOrder;
            }
            set
            {
                this._DisplayOrder = value;
                this.Invalidate();
            }
        }

        //protected override Image back
        //{
        //    get
        //    {
        //        Image back = null;

        //        if (_isChecked)
        //        {
        //            back = backChecked;
        //            AEC.Log.OutputLine("back image is backChecked", back);
        //        }
        //        else
        //        {
        //            switch (state)
        //            {
        //                case ButtonState.normal:
        //                    back = backNormal;
        //                    break;

        //                case ButtonState.hover:
        //                    back = backHover;
        //                    break;

        //                case ButtonState.pressed:
        //                    back = backChecked;
        //                    break;
        //            }

        //            if (!this.Enabled)
        //            {
        //                back = backDisabled;
        //            }
        //        }

        //        return back;
        //    }
        //}

        //private Point imageLeftTop
        //{
        //    get
        //    {
        //        return _imageLeftTop;
        //    }
        //    set
        //    {
        //        _imageLeftTop = value;
        //    }
        //}

        protected override void PaintRadioButton(Graphics g)
        {
            DrawContent(g);
        }

        private void DrawContent(Graphics g)
        {
            if (this._DisplayOrder == UI.DisplayOrder.BoxText)
            {
                DrawBoxText(g);
            }
            else
            {
                DrawTextBox(g);
            }
        }

        private void DrawBoxText(Graphics g)
        {
            if (back != null)
            {
                Rectangle srcRect = new Rectangle(new Point(), back.Size);
                Rectangle destRect = new Rectangle(new Point(0, (this.Height - back.Height) / 2 + 1), srcRect.Size);
                g.DrawImage(back, destRect, srcRect, GraphicsUnit.Pixel);
            }

            Size textSize = Size.Ceiling(g.MeasureString(this.Text, this.Font));
            Color color = this.Enabled ? this.ForeColor : Color.LightGray;
            Rectangle textRect = new Rectangle(new Point(), new System.Drawing.Size(textSize.Width, this.Height));
            if(back != null)
            {
                textRect.Location = new Point(back.Width + 1, 1);
            }
            TextRenderer.DrawText(g, this.Text, this.Font, textRect, color, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private void DrawTextBox(Graphics g)
        {
            Size textSize = Size.Ceiling(g.MeasureString(this.Text, this.Font));
            Color color = this.Enabled ? this.ForeColor : Color.LightGray;
            Rectangle textRect = new Rectangle(new Point(), new System.Drawing.Size(textSize.Width, this.Height));
            TextRenderer.DrawText(g, this.Text, this.Font, textRect, color, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            if (back != null)
            {
                Rectangle srcRect = new Rectangle(new Point(), back.Size);
                Rectangle destRect = new Rectangle(new Point(textSize.Width, (this.Height - back.Height) / 2 + 1), srcRect.Size);
                g.DrawImage(back, destRect, srcRect, GraphicsUnit.Pixel);
            }
        }
    }

    [ToolboxBitmap(typeof(RadioButton))]
    public class ModeBox : gtRadioButton
    {
        public string indexLabel { get; set; }
        public bool dataIsPrivate{get;set;}
        public Image lockSymbol { get; set; }
        public Rectangle _indexRect = new Rectangle(0, 0, 200, 25);
        public Rectangle _textRect = new Rectangle(0, 25, 200, 42);
        public Rectangle _lockRect = new Rectangle(0, 0, 24, 24);
        public Rectangle indexRect
        {
            get
            {
                return _indexRect;
            }
            set
            {
                _indexRect = value;
            }
        }

        public Rectangle textRect
        {
            get
            {
                return _textRect;
            }
            set
            {
                _textRect = value;
            }
        }

        public Rectangle lockRect
        {
            get
            {
                return _lockRect;
            }
            set
            {
                _lockRect = value;
            }
        }

        [Localizable(true)]
        public Font indexFont { get; set; }
        
        public ModeBox():
            base()
        {
            base.isAutoCheck = false;
            base.userGDIPaint = false;
        }

        protected override void PaintForeground(Graphics g)
        {
            //TextRenderer.DrawText(g, this.Text, this.Font, this.ClientRectangle, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            //if (this.Enabled || this.isChecked)
            //{
            //    TextRenderer.DrawText(g, this.Text, this.Font, this.ClientRectangle, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            //}
            //else
            //{
            //    TextRenderer.DrawText(g, this.Text, this.Font, this.ClientRectangle, Color.Silver, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            //}

            if(this.isChecked)
            {
                if(this.dataIsPrivate && lockSymbol != null)
                {
                    Rectangle srcRect = new Rectangle(new Point(), lockSymbol.Size);
                    //Rectangle destRect = new Rectangle(new Point(6, (this.Height - lockSymbol.Height) /2), lockSymbol.Size);
                    g.DrawImage(lockSymbol, lockRect, srcRect, GraphicsUnit.Pixel);
                }

                if (!_indexRect.IsEmpty && indexFont != null && indexLabel.Length > 0)
                {
                    TextRenderer.DrawText(g, this.indexLabel, indexFont, indexRect, this.Enabled ? this.ForeColor : Color.Silver, TextFormatFlags.HorizontalCenter | TextFormatFlags.Bottom);
                }
                if (!_textRect.IsEmpty)
                {
                    TextRenderer.DrawText(g, this.Text, this.Font, _textRect, this.Enabled ? this.ForeColor : Color.Silver, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
            }
            else
            {
                if (!_indexRect.IsEmpty && indexFont != null && indexLabel.Length > 0)
                {
                    TextRenderer.DrawText(g, this.indexLabel, indexFont, indexRect, this.Enabled ? this.ForeColor : Color.Silver, TextFormatFlags.HorizontalCenter | TextFormatFlags.Bottom);
                }
                if (!_textRect.IsEmpty)
                {
                    TextRenderer.DrawText(g, this.Text, this.Font, _textRect, this.Enabled ? this.ForeColor : Color.Silver, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
            }
        }
    }
}
