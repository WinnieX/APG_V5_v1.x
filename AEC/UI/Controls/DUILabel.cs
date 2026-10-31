using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using AEC.UI.Utility;

namespace AEC.UI.Controls
{
    public class DUILabel : DUIControlBase
    {
        private ContentAlignment alignment = ContentAlignment.MiddleCenter;
        protected Color _borderColor = Color.FromArgb(47, 100, 189);
        protected Color _fillColor = Color.FromArgb(48, 48, 48);
        protected Color _foreColor = Color.FromArgb(224, 224, 224);
        protected int _radius = 16;
        protected float _borderWidth = 1;

        protected string _text = string.Empty;
        protected bool _showBorder = false;
        protected bool _showText = true;

        private bool showBackground = false;

        public ContentAlignment Alignment
        {
            get
            {
                return this.alignment;
            }
            set
            {
                this.alignment = value;
            }
        }

        public bool ShowBackground
        {
            get
            {
                return this.showBackground;
            }
            set
            {
                if (this.showBackground != value)
                {
                    this.showBackground = value;
                }
            }
        }

        public bool ShowBorder
        {
            get
            {
                return _showBorder;
            }
            set
            {
                if (_showBorder != value)
                {
                    _showBorder = value;
                }
            }
        }

        public Color ForeColor
        {
            get
            {
                return this._foreColor;
            }
            set
            {
                this._foreColor = value;
            }
        }

        public Color BorderColor
        {
            get
            {
                return this._borderColor;
            }
            set
            {
                this._borderColor = value;
            }
        }

        public Color FillColor
        {
            get
            {
                return this._fillColor;
            }
            set
            {
                this._fillColor = value;
            }
        }

        public int Radius
        {
            get
            {
                return this._radius;
            }
            set
            {
                this._radius = value;
            }
        }

        public float BorderWidth
        {
            get
            {
                return this._borderWidth;
            }
            set
            {
                this._borderWidth = value;
            }
        }

        public override void OnPaint(Graphics g)
        {
            if(this.showBackground)
            {
                GraphicsPath roundedRect = RoundRectangle.CreateRoundedRectPath(this.ClientRectangle, this._radius);

                GDIHelper.FillPath(g, this._fillColor, roundedRect);

                if (_showBorder)
                {
                    GDIHelper.DrawPath(g, this._borderColor, roundedRect, _borderWidth);
                }
            }

            if (!string.IsNullOrEmpty(this.Text) && this.Font != null)
            {
                TextFormatFlags tff = TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak;

                switch (alignment)
                {
                    case ContentAlignment.TopLeft:
                        tff |= TextFormatFlags.Top | TextFormatFlags.Left;
                        break;

                    case ContentAlignment.TopCenter:
                        tff |= TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
                        break;

                    case ContentAlignment.TopRight:
                        tff |= TextFormatFlags.Top | TextFormatFlags.Right;
                        break;

                    case ContentAlignment.MiddleLeft:
                        tff |= TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                        break;

                    case ContentAlignment.MiddleCenter:
                        tff |= TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
                        break;

                    case ContentAlignment.MiddleRight:
                        tff |= TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                        break;

                    case ContentAlignment.BottomLeft:
                        tff |= TextFormatFlags.Bottom | TextFormatFlags.Left;
                        break;

                    case ContentAlignment.BottomCenter:
                        tff |= TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
                        break;

                    case ContentAlignment.BottomRight:
                        tff |= TextFormatFlags.Bottom | TextFormatFlags.Right;
                        break;
                }

                TextRenderer.DrawText(g, this.Text, this.Font, this.ClientRectangle, this.ForeColor, tff);
            }
        }

        public static DUILabel Create(MLabel label, bool Visible = true)
        {
            DUILabel duiLabel = new DUILabel();
            duiLabel.Location = label.Location;
            duiLabel.Size = label.Size;
            duiLabel.Font = label.Font;
            duiLabel.Text = label.Text;
            duiLabel.ForeColor = label.ForeColor;
            duiLabel.Alignment = label.Alignment;
            duiLabel.ShowBackground = label.ShowBackground;
            duiLabel.ShowBorder = label.ShowBorder;
            duiLabel.BorderColor = label.BorderColor;
            duiLabel.FillColor = label.FillColor;
            duiLabel.Visible = Visible;

            return duiLabel;
        }
    }
}
