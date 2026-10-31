using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AEC.UI.Utils
{
    public class RegionHelper
    {
        public static Region CreateBorderRegion(Control control, int borderWidth, int margin)
        {
            GraphicsPath path = new GraphicsPath();

            int halfWidth = (borderWidth + 1) / 2;
            Rectangle left = new Rectangle(margin - halfWidth, margin + halfWidth, 2 * halfWidth, control.Height - (margin + halfWidth) * 2);
            Rectangle right = new Rectangle(control.Width - margin - halfWidth, margin + halfWidth, 2 * halfWidth, control.Height - (margin + halfWidth) * 2);
            Rectangle top = new Rectangle(margin - halfWidth, margin - halfWidth, control.Width, 2 * halfWidth);
            Rectangle bottom = new Rectangle(margin - halfWidth, control.Height - margin - halfWidth, control.Width, 2 * halfWidth);

            //Region region = new Region();

            path.AddRectangle(left);
            path.AddRectangle(top);
            path.AddRectangle(right);
            path.AddRectangle(bottom);

            //region.Union(left);
            //region.Union(top);
            //region.Union(right);
            //region.Union(bottom);

            return new Region(path);
        }
    }
}
