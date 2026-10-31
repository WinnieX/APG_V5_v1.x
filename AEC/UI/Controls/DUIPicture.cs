using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using AEC.UI.Controls;

namespace AEC.UI.Controls
{
    public class DUIPicture : DUIControlBase
    {
        public Image background { get; set; }
        public Point topLeft { get; set; }
        //public Size 

        public override void OnPaint(Graphics g)
        {
            g.TranslateTransform(this.Location.X, this.Location.Y);

            if (this.background != null)
            {
                Rectangle destRect = new Rectangle(topLeft, this.background.Size);
                Rectangle srcRect = new Rectangle(Point.Empty, this.background.Size);
                g.DrawImage(this.background, destRect, srcRect, GraphicsUnit.Pixel);
            }

            g.ResetTransform();
        }
    }
}
