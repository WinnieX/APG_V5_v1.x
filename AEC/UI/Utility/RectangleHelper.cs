using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AEC.UI.Utility
{
    public class RectangleHelper
    {
        public static Rectangle CreateRectangle(Point center, Size size)
        {
            return new Rectangle(new Point(center.X - size.Width / 2, center.Y - size.Height / 2), size);
        }

        public static RectangleF CreateRectangle(PointF center, SizeF size)
        {
            return new RectangleF(new PointF(center.X - size.Width / 2.0f, center.Y - size.Height / 2.0f), size);
        }

        public static Rectangle CreateRectangle(RectangleF rect, Point offset)
        {
            Point location = new Point((int)Math.Round(rect.X, 0, MidpointRounding.AwayFromZero), (int)Math.Round(rect.Y, 0, MidpointRounding.AwayFromZero));
            Size size = new Size((int)Math.Round(rect.Width, 0, MidpointRounding.AwayFromZero), (int)Math.Round(rect.Height, 0, MidpointRounding.AwayFromZero));
            Rectangle rectangle = new Rectangle(location, size);
            rectangle.Offset(offset);
            return rectangle;
        }

        public static Rectangle CreateRectangle(Point pt1, Point pt2)
        {
            int left = Math.Min(pt1.X, pt2.X);
            int right = Math.Max(pt1.X, pt2.X);
            int top = Math.Min(pt1.Y, pt2.Y);
            int bottom = Math.Max(pt1.Y, pt2.Y);

            return Rectangle.FromLTRB(left, top, right, bottom);
        }
    }
}
