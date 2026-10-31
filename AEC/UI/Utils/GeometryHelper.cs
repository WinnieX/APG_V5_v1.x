using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AEC.UI.Utils
{
    public class GeometryHelper
    {
        public static double Point2PointDistance(Point pt1, Point pt2)
        {
            int distanceX = pt1.X - pt2.X;
            int distanceY = pt1.Y - pt2.Y;

            return Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
        }
    }
}
