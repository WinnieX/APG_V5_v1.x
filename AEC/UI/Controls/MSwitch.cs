using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AEC.UI.Utility;

namespace AEC.UI.Controls
{
    public class MSwitch : MCheckBox
    {
        private const int DefHeight = 29;
        private const int btnPadding = 3;
        private const int textMargin = 4;
        private static readonly Size DefSwitchSize = new Size(48, DefHeight);
        private static readonly Size DefSize = new Size(128, DefHeight);
        private Size _switchSize = DefSwitchSize;

        public SwitchDisplayOrder DisplayOrder{get;set;}

        //private Color _btnNormalFillColor = Color.FromArgb(192, 192, 192);
        //private Color _btnHoverFillColor = Color.FromArgb(224, 224, 224);
        //private Color _btnPressedFillColor = Color.FromArgb(168, 168, 168);


        public override int Radius
        {
            get
            {
                return base.Radius;
            }
            set
            {
                base.Radius = value;
                this._switchSize.Height = this.Radius;
            }
        }

        public int SwitchWdith
        {
            get
            {
                return this._switchSize.Width;
            }
            set
            {
                this._switchSize.Width = value;
            }
        }
        //public Color BtnFillColor
        //{
        //    get
        //    {
        //        switch (base.state)
        //        {
        //            case MButtonState.normal:
        //                return this._btnNormalFillColor;

        //            case MButtonState.hover:
        //                return this._btnHoverFillColor;

        //            case MButtonState.pressed:
        //                return this._btnPressedFillColor;
        //        }

        //        return this._btnNormalFillColor;
        //    }
        //}

        public override Rectangle TextRectangle
        {
            get
            {
                if (DisplayOrder == SwitchDisplayOrder.BoxText)
                {
                    return new Rectangle(new Point(this._switchSize.Width + textMargin, 0), new Size(this.Width - this._switchSize.Width, this.Height - 1));
                }
                else
                {
                    return new Rectangle(new Point(0, 0), new Size(this.Width - this._switchSize.Width - textMargin, this.Height - 1));
                }
            }
        }

        public MSwitch()
        {
            this.Size = DefSize;
            this.Radius = DefHeight;
            this.ShowText = true;

            base.UncheckedDisbaledFillColor = Color.FromArgb(64, 64, 64);
            base.UncheckedHoverFillColor = Color.FromArgb(128, 128, 128);
            base.UncheckedNormalFillColor = Color.FromArgb(96, 96, 96);
            base.UncheckedPressedFillColor = Color.FromArgb(80, 80, 80);

            base.CheckedDisbaledFillColor = Color.FromArgb(6, 162, 78);
            base.CheckedHoverFillColor = Color.FromArgb(107, 213, 106);
            base.CheckedNormalFillColor = Color.FromArgb(7, 193, 96);
            base.CheckedPressedFillColor = Color.FromArgb(6, 175, 87);
        }

        private Point btnCenter
        {
            get
            {
                int y = (this._switchSize.Height - 1) / 2;

                if(DisplayOrder == SwitchDisplayOrder.BoxText)
                {
                    if (this._isChecked)
                    {
                        return new Point(this._switchSize.Width - 1 - y, y);
                    }
                    else
                    {
                        return new Point(y, y);
                    }
                }
                else
                {
                    if (this._isChecked)
                    {
                        return new Point(this.Width - 3 - y, y);
                    }
                    else
                    {
                        return new Point(this.Width - this._switchSize.Width + y, y);
                    }
                }
            }
        }

        protected override void PaintBackground(Graphics g)
        {
            if (this.Visible)
            {
                Point location = this.DisplayOrder == SwitchDisplayOrder.BoxText ?  new Point(0, 0) : new Point(this.Width - _switchSize.Width - 1, 0);

                GraphicsPath roundedRect = RoundRectangle.CreateRoundedRectPath(new Rectangle(location, _switchSize), this._radius);

                GDIHelper.FillPath(g, this.FillColor, roundedRect);

                roundedRect.Dispose();
            }
        }

        protected override void PaintForeground(Graphics g)
        {
            if (this.Visible)
            {
                GDIHelper.SetHighQuality(g);

                int btnRadius = (this._switchSize.Height - 1) / 2 - btnPadding;
                using (Brush btnBrush = new SolidBrush(this.TextColor))
                {
                    Rectangle btnRect = RectangleHelper.CreateRectangle(btnCenter, new Size(2 * btnRadius, 2 * btnRadius));
                    g.FillEllipse(btnBrush, btnRect);
                }

                if (_showText && !string.IsNullOrEmpty(this.Text))
                {
                    TextRenderer.DrawText(g, this.Text, this.Font, this.TextRectangle, this.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
                }
            }
        }
    }
}
