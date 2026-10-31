using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.UI.Utility;

namespace AEC.UI
{
    public class EQIndexLabel
    {
        private static readonly Image normalBackground = AEC.Properties.Resources.EQ_index_back_normal;
        private static readonly Point DefLabelLeftTop = new Point(6, 6);
        private static readonly Size DefLabelSize = new Size(28, 23);
        private static readonly Color NormalForeColor = Color.White;
        private static readonly Color DefForeColor = Color.White;
        //public static readonly Font LabelFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
        //public static readonly StringFormat sf = new StringFormat();

        //static EQIndexLabel()
        //{
        //    sf.Alignment = StringAlignment.Center;
        //    sf.LineAlignment = StringAlignment.Center;
        //}

        public Image activeBackground
        {
            get;
            set;
        }

        public Point labelLeftTop
        {
            get;
            set;
        }

        public Size labelSize
        {
            get;
            set;
        }

        public Rectangle labelRect
        {
            get
            {
                return new Rectangle(labelLeftTop, labelSize);
            }
        }

        public Color activeColor
        {
            get;
            set;
        }

        public short index
        {
            get;
            set;
        }

        private bool _isActive = false;
        public bool isActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                _isActive = value;
            }
        }

        public EQIndexLabel()
        {
            this.labelLeftTop = DefLabelLeftTop;
            this.labelSize = DefLabelSize;
            this.activeColor = DefForeColor;
        }

        public void PaintIndexLabel(Graphics g)
        {
            Image background = this._isActive ? activeBackground : normalBackground;

            if (background == null)
            {
                return;
            }

            Rectangle srcRect = new Rectangle(0, 0, background.Width, background.Height);
            Rectangle destRect = new Rectangle(labelLeftTop, srcRect.Size);
            GDIHelper.QuickDrawImage(g, labelLeftTop, background);

            Color foreColor = this._isActive ? activeColor : NormalForeColor;
            TextRenderHelper.RenderText(g, (index + 1).ToString(), FontHelper.CreateFont12(), destRect, foreColor, ContentAlignment.MiddleCenter);

            
            //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            //using (Brush brush = new SolidBrush(foreColor))
            //{
            //    g.DrawString((index + 1).ToString(), LabelFont, brush, destRect, sf);
            //}
        }
    }
}
