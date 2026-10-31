using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace AEC.UI
{
    public partial class PanelWithTitle : Panel
    {
        private int _titleHeight = 16;
        //private bool _isRadius = false;
        //private int _cornerRadius = 7;

        //public bool IsRadius
        //{
        //    get
        //    {
        //        return this._isRadius;
        //    }
        //    set
        //    {
        //        this._isRadius = value;
        //        if(this._isRadius)
        //        {
        //            Rectangle rect = new Rectangle(-1, -1, base.Width + 1, base.Height);
        //            GraphicsPath path = this.GetRoundedRectPath(rect, this.ConerRadius);
        //            base.Region = new Region(path);
        //        }
        //        else
        //        {
        //            GraphicsPath g = new GraphicsPath();
        //            g.AddRectangle(base.ClientRectangle);
        //            g.CloseFigure();
        //            base.Region = new Region(g);
        //        }
        //    }
        //}

        //public int ConerRadius
        //{
        //    get
        //    {
        //        return this._cornerRadius;
        //    }
        //    set
        //    {
        //        this._cornerRadius = Math.Max(value, 1);
        //        if (this._isRadius)
        //        {
        //            Rectangle rect = new Rectangle(-1, -1, base.Width + 1, base.Height);
        //            GraphicsPath path = GDIHelper.CreateRoundedRectanglePath(rect, this.ConerRadius);
        //            base.Region = new Region(path);
        //        }
        //    }
        //}

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

        public PanelWithTitle()
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); //默认启动双缓冲
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            
            this.UpdateStyles();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle titleRect = new Rectangle(new Point(this.Padding.Left, this.Padding.Top), new Size(this.Width - this.Padding.Horizontal, this.titleHeight));
            if (this.Enabled)
            {
                TextRenderer.DrawText(e.Graphics, this.title, this.Font, titleRect, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
            else
            {
                TextRenderer.DrawText(e.Graphics, this.title, this.Font, titleRect, Color.LightGray, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
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
    }
}
