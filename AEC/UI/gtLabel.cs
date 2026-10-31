using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AEC.UI
{
    public partial class gtLabel : Control
    {
        public enum AlignMent
        {
            Center = 0,
            Left,
            Right,
        }

        public AlignMent alignment
        {
            get;
            set;
        }

        public gtLabel()
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
            //this.SetStyle(ControlStyles.ResizeRedraw, true);
            //this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.StandardDoubleClick, false);

            this.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            //base.OnPaint(pe);
            PaintForeground(pe.Graphics);
        }

        private void PaintForeground(Graphics g)
        {
            TextFormatFlags tff = TextFormatFlags.VerticalCenter;
            switch (alignment)
            {
                case AlignMent.Center:
                    tff |= TextFormatFlags.HorizontalCenter;
                    break;

                case AlignMent.Left:
                    tff |= TextFormatFlags.Left;
                    break;

                case AlignMent.Right:
                    tff |= TextFormatFlags.Right;
                    break;
            }

            TextRenderer.DrawText(g, this.Text, this.Font, this.ClientRectangle, this.ForeColor, tff);
        }

        public void UpdateDisplay()
        {
            this.Invalidate();
            this.Update();
        }

        public void UpdateText(string txt)
        {
            this.Text = txt;
            this.UpdateDisplay();
        }
    }
}
