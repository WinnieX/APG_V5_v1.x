using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using AEC.Core;

namespace AEC.UI
{
    public partial class CircleProgress : Control
    {
        private static readonly Size DefaultClientSize = new Size(128, 128);

        private float _progress = 0.0f;

        public CircleProgress()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Size = DefaultClientSize;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            //base.OnPaint(pe);
            this.PaintProgress(pe.Graphics);
        }

        private void PaintProgress(Graphics g)
        {
            //GDIHelper.InitializeGraphics(g);
            //g.FillRectangle(new SolidBrush(Color.Gray), this.ClientRectangle);
            Rectangle backRect = Rectangle.Inflate(this.ClientRectangle, -6, -6);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            using(Brush back = new SolidBrush(Color.FromArgb(64,64,64)))
            {
                g.FillEllipse(back, backRect);
            }
            Rectangle rect = Rectangle.FromLTRB(8, 8, this.Width - 1 - 8, this.Height - 1 - 8);
            //using (Pen innerPen = new Pen(Color.White, 17))
            //{
            //    g.DrawEllipse(innerPen, rect);
            //}
            //Rectangle rect = Rectangle.FromLTRB(10, 10, this.Width - 1 - 10, this.Height - 1 - 10);

            using (Pen outterPen = new Pen(Color.FromArgb(5, 141, 233), 13))
            {
                float sweepAngle = _progress * 360;
                g.DrawArc(outterPen, rect, -90, sweepAngle);
            }
            //Color.FromArgb(5, 141, 233)

            string label = Math.Round(_progress * 100, 0, MidpointRounding.AwayFromZero).ToString() + "%";
            TextRenderer.DrawText(g, label, this.Font, this.ClientRectangle, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        public void Append(float delta)
        {
            _progress += delta;
            //AEC.Log.OutputLine("progress:{0}", _progress);
            _progress = Math.Min(Math.Max(_progress, 0.0f), 1.0f);
            //AEC.Log.OutputLine("progress:{0}", _progress);
            //_progress = Utils.ConstrainValue(_progress, 0.0f, 1.0f);
            this.Invalidate();
            this.Update();
        }

        public void Init()
        {
            _progress = 0;
            this.Invalidate();
            this.Update();
        }
    }
}
