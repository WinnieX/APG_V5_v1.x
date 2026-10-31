using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using AEC.UI;
using AEC.UI.Utility;

namespace AEC.UI.RTA
{
    public partial class ProgressBar : Control
    {
        private static readonly Color startColor = Color.FromArgb(55, 140, 70);
        private static readonly Color endColor = Color.FromArgb(55, 234, 70);

        private float _progress = 0.0f;

        public float progress
        {
            set
            {
                _progress = Math.Min(Math.Max(value, 0.0f), 1.0f);
                this.Invalidate();
                this.Update();
            }
        }

        public void Reset()
        {
            _progress = 0;
        }

        public ProgressBar()
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
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            PaintProgress(pe.Graphics);
        }

        private void PaintProgress(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            //绘制背景
            Rectangle backRect = new Rectangle(0, 0, base.Width, base.Height);
            //Brush backBrush = new SolidBrush(Color.FromArgb(64, 64, 64));

            //using (Brush backBrush = new SolidBrush(Color.FromArgb(255, 255, 255)))
            //{
            //    g.FillRectangle(backBrush, this.ClientRectangle);
            //}

            //using (Brush backBrush = new SolidBrush(Color.FromArgb(0, 255, 0)))
            //{
            //    g.FillRectangle(backBrush, backRect);
            //}

            using (Brush backBrush = new SolidBrush(Color.FromArgb(168, 168, 168)))
            {
                GraphicsPath backPath = RoundRectangle.GetRoundedRectPath(backRect, backRect.Height);
                g.FillPath(backBrush, backPath);
                backPath.Dispose();
            }

            if(_progress > 0)
            {
                Rectangle foreRect = new Rectangle(2, 2, base.Width - 4, base.Height - 5);
                using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(foreRect, startColor, endColor, LinearGradientMode.Horizontal))
                {
                    foreRect.Width = (int)Math.Round(foreRect.Width * _progress, MidpointRounding.AwayFromZero);
                    GraphicsPath forePath = RoundRectangle.GetRoundedRectPath(foreRect, foreRect.Height);
                    g.FillPath(linearGradientBrush, forePath);
                    forePath.Dispose();
                }
            }
        }

        //private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        //{
        //    Rectangle rect2 = new Rectangle(rect.Location, new Size(radius, radius));
        //    GraphicsPath graphicsPath = new GraphicsPath();
        //    graphicsPath.AddArc(rect2, 180f, 90f);//左上角
        //    rect2.X = rect.Right - radius - 1;
        //    graphicsPath.AddArc(rect2, 270f, 90f);//右上角
        //    rect2.Y = rect.Bottom - radius;
        //    //rect2.Width += 1;
        //    //rect2.Height += 1;
        //    graphicsPath.AddArc(rect2, 360f, 90f);//右下角           
        //    rect2.X = rect.Left;
        //    graphicsPath.AddArc(rect2, 90f, 90f);//左下角
        //    graphicsPath.CloseFigure();
        //    return graphicsPath;
        //}
    }
}
