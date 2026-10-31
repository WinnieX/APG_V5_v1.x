using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace AEC.UI
{
    public partial class gtPanel : Panel
    {
        private int _titleHeight = 20;
        private int radius = 18;
        private Color _fillColor = Color.FromArgb(32, 32, 32);
        private Color _rectColor = Color.Black;
        private bool _showRect = true;


        public gtPanel()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, false);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.DoubleBuffered = true;
            this.UpdateStyles();
            this.ForeColor = Color.LightGray;
            this.Padding = new Padding(0, 8, 0, 0);
        }

        public int titleHeight
        {
            get
            {
                return this._titleHeight;
            }
            set
            {
                this._titleHeight = value;
            }
        }

        public string title {get;set;}


        [Description("圆角半径"), Category("自定义")]
        [DefaultValue(5)]
        public int Radius
        {
            get
            {
                return radius;
            }
            set
            {
                if (radius != value)
                {
                    radius = Math.Max(0, value);
                    Invalidate();
                }
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
                Refresh();
            }
        }

        public bool ShowRect
        {
            get
            {
                return this._showRect;
            }
            set
            {
                if (_showRect != value)
                {
                    _showRect = value;
                    Invalidate();
                }
            }
        }

        public Color RectColor
        {
            get
            {
                return this._rectColor;
            }
            set
            {
                this._rectColor = value;
                Refresh();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.radius > 0)
            {
                SetWindowRegion();
            }
            else
            {
                GraphicsPath graphicsPath = new GraphicsPath();
                graphicsPath.AddRectangle(base.ClientRectangle);
                graphicsPath.CloseFigure();
                base.Region = new Region(graphicsPath);
            }
        }

        public void UpdateDisplay()
        {
            this.Invalidate();
            this.Update();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg != 20)
            {
                base.WndProc(ref m);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //
            base.OnPaint(e);
            PaintPanel(e.Graphics);
        }

        private void PaintPanel(Graphics g)
        {
            if(this.Visible)
            {
                //if (this.radius > 0)
                //{
                //    SetWindowRegion();
                //}
                //else
                //{
                //    GraphicsPath graphicsPath = new GraphicsPath();
                //    graphicsPath.AddRectangle(base.ClientRectangle);
                //    graphicsPath.CloseFigure();
                //    base.Region = new Region(graphicsPath);
                //}

                //Rectangle backRect = new Rectangle(-1, -1, Width - 1, Height - 1);
                GraphicsPath backGP = CreateRoundedRectanglePath(base.ClientRectangle, radius);

                //base.Region = new Region(backGP);

                SetHighQuality(g);
                g.FillPath(new SolidBrush(_fillColor), backGP);
                if (this._showRect)
                {
                    using (Pen borderPen = new Pen(_rectColor, 2.0f))
                    {
                        g.DrawPath(borderPen, backGP);
                    }
                }
                SetDefaultQuality(g);

                backGP.Dispose();

                Rectangle titleRect = new Rectangle(new Point(this.Padding.Left, this.Padding.Top), new Size(this.Width - this.Padding.Horizontal, this.titleHeight));
                if (this.Enabled && !string.IsNullOrEmpty(this.title))
                {
                    TextRenderer.DrawText(g, this.title, this.Font, titleRect, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.Top);
                }
                else
                {
                    TextRenderer.DrawText(g, this.title, this.Font, titleRect, Color.LightGray, TextFormatFlags.HorizontalCenter | TextFormatFlags.Top);
                }
            }
        }

        private void SetWindowRegion()
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(-1, -1, base.Width + 1, base.Height);
            path = this.GetRoundedRectPath(rect, this.radius);
            base.Region = new Region(path);
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            Rectangle rect2 = new Rectangle(rect.Location, new Size(radius, radius));
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddArc(rect2, 180f, 90f);//左上角
            rect2.X = rect.Right - radius;
            graphicsPath.AddArc(rect2, 270f, 90f);//右上角
            rect2.Y = rect.Bottom - radius;
            rect2.Width += 1;
            rect2.Height += 1;
            graphicsPath.AddArc(rect2, 360f, 90f);//右下角           
            rect2.X = rect.Left;
            graphicsPath.AddArc(rect2, 90f, 90f);//左下角
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        public static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath graphicsPath = new GraphicsPath();

            if(radius == 0)
            {
                graphicsPath.AddRectangle(rect);
                graphicsPath.CloseFigure();
                return graphicsPath;
            }

            graphicsPath.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            graphicsPath.AddArc(rect.X + rect.Width - radius - 1, rect.Y, radius, radius, 270, 90);
            graphicsPath.AddArc(rect.X + rect.Width - radius - 1, rect.Y + rect.Height - radius - 1, radius, radius, 0, 90);
            graphicsPath.AddArc(rect.X, rect.Bottom - radius - 1, radius, radius, 90, 90);
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        /// <summary>
        /// 设置GDI高质量模式抗锯齿
        /// </summary>
        /// <param name="g"></param>
        public static void SetHighQuality(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
        }

        /// <summary>
        /// 设置GDI默认值
        /// </summary>
        /// <param name="g"></param>
        public static void SetDefaultQuality(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.Default;
            g.InterpolationMode = InterpolationMode.Default;
            g.CompositingQuality = CompositingQuality.Default;
        }
    }
}
