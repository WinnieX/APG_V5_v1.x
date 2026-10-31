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
    public partial class FormMask : Form
    {
        private ColorBlend colorBlend = null;
        public LinearGradientBrush lgb = null;

        public FormMask()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            //this.Opacity = 0.98;

            InitColorBlend();
        }

        private void InitColorBlend()
        {
            if(colorBlend == null)
            {
                colorBlend = new ColorBlend(3);
                colorBlend.Colors = new Color[] { Color.FromArgb(2, 17, 30), Color.FromArgb(2, 80, 117), Color.FromArgb(2, 17, 30) };
                colorBlend.Positions = new float[] { 0, 0.5f, 1.0f };
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            InitColorBlend();
            lgb = new LinearGradientBrush(this.ClientRectangle, this.BackColor, this.BackColor, LinearGradientMode.Vertical);
            lgb.InterpolationColors = colorBlend;
            lgb.GammaCorrection = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.PaintBackground(e.Graphics);
        }

        private void PaintBackground(Graphics g)
        {
            if (lgb != null)
            {
                g.FillRectangle(lgb, this.ClientRectangle);
            }
            Image back = AEC.Properties.Resources.icon_form_mask;
            Rectangle srcRect = new Rectangle(Point.Empty, back.Size);
            Point offset = new Point((this.Width - back.Width) / 2, (this.Height - back.Height) / 2);
            Rectangle destRect = new Rectangle(offset, back.Size);
            g.DrawImage(back, destRect, srcRect, GraphicsUnit.Pixel);
            //g.DrawImage(back)
            //GDIHelper.FillRectangle(g, this.ClientRectangle, SkinManager.CurrentSkin.backgroundColor);
            //GDIHelper.QuickDrawImage(g, offset, back);
        }

        private void FormMask_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private const int WM_KEYDOWN = 0x0100;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == WM_KEYDOWN)
            {
                if (keyData == Keys.Escape)
                {
                    this.Close();
                    return false;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
