using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using AEC.UI.Utility;

namespace AEC.UI.Controls
{
    public partial class MLabel : Control
    {
        public ContentAlignment Alignment
        {
            get;
            set;
        }

        protected Color _borderColor = Color.FromArgb(47, 100, 189);
        protected Color _fillColor = Color.FromArgb(48, 48, 48);
        protected int _radius = 16;
        protected float _borderWidth = 1;

        protected string _text = string.Empty;
        protected bool _showBorder = false;
        protected bool _showText = true;

        private bool showBackground = false;

        public bool ShowBackground
        {
            get
            {
                return this.showBackground;
            }
            set
            {
                if(this.showBackground != value)
                {
                    this.showBackground = value;
                    this.Invalidate();
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
                    Invalidate();
                }
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

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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

        public MLabel()
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
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.StandardClick, true);
            this.SetStyle(ControlStyles.StandardDoubleClick, true);
            this.SetStyle(ControlStyles.UserMouse, true);
            this.SetStyle(ControlStyles.CacheText, true);
            this.SetStyle(ControlStyles.Selectable, true);

            this.Alignment = ContentAlignment.MiddleCenter;
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.FromArgb(224, 224, 224);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            PaintLabel(pe.Graphics);
        }

        private void PaintLabel(Graphics g)
        {
            if(!Visible)
            {
                return;
            }

            if(this.showBackground)
            {
                this.PaintBackground(g);
            }

            this.PaintForeground(g);
        }

        private void PaintBackground(Graphics g)
        {
            GraphicsPath roundedRect = RoundRectangle.CreateRoundedRectPath(this, this._radius);

            GDIHelper.FillPath(g, this._fillColor, roundedRect);

            if (_showBorder)
            {
                GDIHelper.DrawPath(g, this._borderColor, roundedRect, _borderWidth);
            }
        }

        private void PaintForeground(Graphics g)
        {
            TextFormatFlags tff = TextFormatFlags.Default;
            switch (Alignment)
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
            if (!string.IsNullOrEmpty(this.Text))
            {
                TextRenderer.DrawText(g, this.Text, this.Font, this.ClientRectangle, this.ForeColor, tff);
            }
        }

        public void UpdateUI()
        {
            this.Invalidate();
            this.Update();
        }

        public void UpdateText(string text, bool isUpdateUI)
        {
            this.Text = text;
            if (isUpdateUI)
            {
                this.UpdateUI();
            }
        }
    }
}
