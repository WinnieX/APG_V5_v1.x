using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AEC.UI
{
    public partial class MaskLayer : Control
    {
        private int alpha = 192;
        public MaskLayer()
        {
            InitializeComponent();
            SetStyle(System.Windows.Forms.ControlStyles.Opaque, true);
            base.CreateControl();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Color color = Color.FromArgb(alpha, this.BackColor);
            using (SolidBrush brush = new SolidBrush(color))
            {
                e.Graphics.FillRectangle(brush, 0, 0, this.Size.Width, this.Size.Height);
            }
            if (!this.DesignMode)
            {
                using (Pen pen = new Pen(color))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, this.Width, this.Height);
                }
            }
            else
                e.Graphics.DrawRectangle(Pens.Black, 1, 1, this.Width - 2, this.Height - 2);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020;
                return cp;
            }
        }

        public int Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                if (value < 0) alpha = 0;
                else if (value > 255) alpha = 255;
                else alpha = value;
                this.Invalidate();
            }
        }
    }
}
