using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AEC.UI.Controls
{
    public class DUIControlBase
    {
        public virtual Point absoluteLocation   //控件的绝对位置，相对于非DUI父控件
        {
            get
            {
                return Location;
            }
        }

        public Point Location   //相对父控件的位置
        {
            get;
            set;
        }
        
        public virtual Size Size
        {
            get;
            set;
        }

        public int Width
        {
            get
            {
                return this.Size.Width;
            }
        }

        public int Height
        {
            get
            {
                return this.Size.Height;
            }
        }

        public string Text
        {
            get;
            set;
        }

        public Font Font
        {
            get;
            set;
        }

        public Color BackColor
        {
            get;
            set;
        }

        public bool Visible
        {
            get;
            set;
        }

        public virtual Rectangle ClientRectangle
        {
            get
            {
                return new Rectangle(this.Location, this.Size);
            }
        }

        public virtual void OnPaint(Graphics g)
        {
        }
    }
}
