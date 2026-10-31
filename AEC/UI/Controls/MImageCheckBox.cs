using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AEC.UI.Controls
{
    public class MImageCheckBox : MCheckBox
    {
        public bool showBackFillColor { get; set; }
        public Image backUncheckedNormal { get; set; }
        public Image backUncheckedHover { get; set; }
        public Image backUncheckedPressed { get; set; }
        public Image backUncheckedDisabled { get; set; }

        public Image backCheckedNormal { get; set; }
        public Image backCheckedHover { get; set; }
        public Image backCheckedPressed { get; set; }
        public Image backCheckedDisabled { get; set; }

        public Rectangle ImageRectangle { get; set; }
        //{
        //    get
        //    {
        //        //return new Rectangle(Point.Empty, new Size(this.Width - 1, this.Height - 1));
        //        int width = this.Width - this.Padding.Left - this.Padding.Right;
        //        int height = this.Height - this.Padding.Top - this.Padding.Bottom;
        //        Rectangle rect = new Rectangle(this.Padding.Left, this.Padding.Top, width, height);
        //        return rect;
        //    }
        //}

        private Image back
        {
            get
            {
                if(base._isChecked)
                {
                    if(!this.Enabled)
                    {
                        return backCheckedDisabled;
                    }

                    switch(base.state)
                    {
                        case MButtonState.Hover:
                            return backCheckedHover;

                        case MButtonState.Pressed:
                            return backCheckedPressed;

                        case MButtonState.Normal:
                            return backCheckedNormal;

                        default:
                            return backCheckedNormal;
                    }
                }
                else
                {
                    if (!this.Enabled)
                    {
                        return backUncheckedDisabled;
                    }

                    switch (base.state)
                    {
                        case MButtonState.Hover:
                            return backUncheckedHover;

                        case MButtonState.Pressed:
                            return backUncheckedPressed;

                        case MButtonState.Normal:
                            return backUncheckedNormal;

                        default:
                            return backUncheckedNormal;
                    }
                }
            }
        }

        protected override void PaintBackground(Graphics g)
        {
            if (this.Visible)
            {
                if (showBackFillColor)
                {
                    base.PaintBackground(g);
                }
                
                Image backImage = back;
                if(backImage != null)
                {
                    Rectangle srcRect = new Rectangle(new Point(), backImage.Size);

                    Rectangle destRect;
                    if (ImageRectangle.IsEmpty)
                    {
                        destRect = new Rectangle((this.Width - srcRect.Width) / 2, (this.Height - srcRect.Height) / 2, srcRect.Width, srcRect.Height);
                    }
                    else
                    {
                        destRect = new Rectangle(this.ImageRectangle.X + (this.ImageRectangle.Width - srcRect.Width) / 2, this.ImageRectangle.Y + (this.ImageRectangle.Height - srcRect.Height) / 2, srcRect.Width, srcRect.Height);
                    }

                    g.DrawImage(back, destRect, srcRect, GraphicsUnit.Pixel);
                }
            }
        }
    }
}
