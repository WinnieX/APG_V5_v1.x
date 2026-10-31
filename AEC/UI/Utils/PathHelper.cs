using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace AEC.UI.Utils
{
    public class PathHelper
    {
        public static GraphicsPath CreatePathWithBottomArrow(Rectangle rect, int arrowX, int arrowSize = 8, int radius = 12)
        {
            Debug.Assert(rect.Width > 2 * arrowSize && rect.Width > 2 * radius && rect.Height > 2 * radius && arrowX >= radius + arrowSize && arrowX + arrowSize + radius <= rect.Width);

            RoundRectangle rr = new RoundRectangle(rect, radius);
            //return rr.ToGraphicsBezierPath();
            //return RoundRectangle.CreateRoundedRectPath(rect, radius);
            GraphicsPath path = new GraphicsPath();

            int x = rect.X;
            int y = rect.Y;
            int w = rect.Width;
            int h = rect.Height;
            arrowX += x;

            int arrowSizeX = arrowSize - 2;
            int arrowSizeY = arrowSize;

            path.AddBezier(x, y + radius, x, y, x + radius, y, x + radius, y);
            path.AddLine(x + radius, y, x + w - radius, y);
            path.AddBezier(x + w - radius, y, x + w, y, x + w, y + radius, x + w, y + radius);
            path.AddLine(x + w, y + radius, x + w, y + h - radius);
            path.AddBezier(x + w, y + h - radius, x + w, y + h, x + w - radius, y + h, x + w - radius, y + h);

            path.AddLine(x + w - radius, y + h, arrowX + arrowSizeX, y + h);
            path.AddLine(arrowX + arrowSizeX, y + h, arrowX, y + h + arrowSizeY);
            path.AddLine(arrowX, y + h + arrowSizeY, arrowX - arrowSizeX, y + h);
            path.AddLine(arrowX - arrowSizeX, y + h, x + radius, y + h);
            
            //path.AddLine(x + w - radius, y + h, x + radius, y + h);
            path.AddBezier(x + radius, y + h, x, y + h, x, y + h - radius, x, y + h - radius);
            path.AddLine(x, y + h - radius, x, y + radius);
            path.CloseFigure();

            return path;


            //GraphicsPath path = new GraphicsPath();
            //int x = this.Rect.X;
            //int y = this.Rect.Y;
            //int w = this.Rect.Width;
            //int h = this.Rect.Height;
            //path.AddBezier(x, y + this.CornerRadius.TopLeft, x, y, x + this.CornerRadius.TopLeft, y, x + this.CornerRadius.TopLeft, y);
            //path.AddLine(x + this.CornerRadius.TopLeft, y, x + w - this.CornerRadius.TopRight, y);
            //path.AddBezier(x + w - this.CornerRadius.TopRight, y, x + w, y, x + w, y + this.CornerRadius.TopRight, x + w, y + this.CornerRadius.TopRight);
            //path.AddLine(x + w, y + this.CornerRadius.TopRight, x + w, y + h - this.CornerRadius.BottomRigth);
            //path.AddBezier(x + w, y + h - this.CornerRadius.BottomRigth, x + w, y + h, x + w + this.CornerRadius.BottomRigth, y + h, x + w + this.CornerRadius.BottomRigth, y + h);
            //path.AddLine(x + w + this.CornerRadius.BottomRigth, y + h, x - this.CornerRadius.BottomLeft, y + h);
            //path.AddBezier(x - this.CornerRadius.BottomLeft, y + h, x, y + h, x, y + h - this.CornerRadius.BottomLeft, x, y + h - this.CornerRadius.BottomLeft);
            //path.AddLine(x, y + h - this.CornerRadius.BottomLeft, x, y + this.CornerRadius.TopLeft);
            //path.CloseFigure();
            //return path;
        }
    }
}
