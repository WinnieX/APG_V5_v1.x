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
    public partial class MPanel : Panel
    {
        private static readonly Color textColor = Color.FromArgb(224, 224, 224);

        private Color _borderColor = Color.FromArgb(47, 100, 189);
        private Color _fillColor = Color.FromArgb(48, 48, 48);
        private int _radius = 16;

        private string text = string.Empty;
        public bool _showBorder = true;
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
        public override string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (this.text != value)
                {
                    string oldText = this.text;

                    this.text = value;

                    Size requiredSize = TextRenderer.MeasureText(this.text.Length > oldText.Length ? this.text : oldText, this.Font);
                    if(!requiredSize.IsEmpty)
                    {
                        this.Invalidate(new Rectangle(Point.Empty, new Size(this.Width, requiredSize.Height)));
                        this.Update();
                    }
                }
            }
        }

        public MPanel()
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
            this.SetStyle(ControlStyles.CacheText, true);
            this.SetStyle(ControlStyles.StandardClick, true);
            this.SetStyle(ControlStyles.UserMouse, true);

            this.SetStyle(ControlStyles.StandardDoubleClick, false);

            this.DoubleBuffered = true;
            //this.UpdateStyles();
            this.BackColor = Color.Transparent;
            this.ShowBorder = true;
        }

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        var cp = base.CreateParams;
        //        cp.Style &= ~0x02000000;//Turn Off WS_CLIPCHILDREN
        //        return cp;
        //    }
        //}

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PaintBack(e.Graphics);
        }

        private void PaintBack(Graphics g)
        {
            if (this.Visible)
            {
                //this.UpdateRegion();

                GraphicsPath roundedRect = RoundRectangle.CreateRoundedRectPath(this, this._radius);

                GDIHelper.FillPath(g, this._fillColor, roundedRect, true);

                if (_showBorder)
                {
                    GDIHelper.DrawPath(g, this._borderColor, roundedRect);
                }

                if (!string.IsNullOrEmpty(this.Text))
                {
                    Rectangle textRect = new Rectangle(new Point(this.Padding.Left, this.Padding.Top), new Size(this.Width - this.Padding.Left - this.Padding.Right, this.Height - this.Padding.Top - this.Padding.Bottom));
                    TextRenderer.DrawText(g, this.Text, this.Font, textRect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.Top);
                }

                roundedRect.Dispose();
            }
        }

        private void UpdateRegion()
        {
            if (this._radius > 0)
            {
                GraphicsPath path = new GraphicsPath();
                Rectangle rect = new Rectangle(-1, -1, base.Width + 1, base.Height + 1);
                path = RoundRectangle.CreateRoundedRectPath(rect.Size, this._radius);
                base.Region = new Region(path);
            }
            else
            {
                GraphicsPath graphicsPath = new GraphicsPath();
                graphicsPath.AddRectangle(base.ClientRectangle);
                graphicsPath.CloseFigure();
                base.Region = new Region(graphicsPath);
            }
        }

        public void UpdateUI()
        {
            this.Invalidate();
            this.Update();
        }
    }
}
