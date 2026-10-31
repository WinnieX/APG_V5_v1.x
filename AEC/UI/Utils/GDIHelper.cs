using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace AEC.UI.Utils
{
    public enum GraphDirection
    {
        /// <summary>
        /// The upward
        /// </summary>
        Upward = 1,
        /// <summary>
        /// The downward
        /// </summary>
        Downward,
        /// <summary>
        /// The leftward
        /// </summary>
        Leftward,
        /// <summary>
        /// The rightward
        /// </summary>
        Rightward
    }

    /// <summary>
    /// GDI绘制辅助类
    /// </summary>
    /// User:Ryan  CreateTime:2012-8-3 21:03.
    internal static class GDIHelper
    {
        //#region InitializeGraphics

        ///// <summary>
        ///// 初始化Graphics对象为高质量的绘制
        ///// </summary>
        ///// <param name="g">The g.</param>
        ///// User:Ryan  CreateTime:2011-08-19 16:53.
        //public static void InitializeGraphics(Graphics g)
        //{
        //    if (g != null)
        //    {
        //        g.SmoothingMode = SmoothingMode.AntiAlias;
        //        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        g.CompositingQuality = CompositingQuality.HighQuality;
        //    }
        //}

        //#endregion

        /// <summary>
        /// 设置GDI高质量模式抗锯齿
        /// </summary>
        /// <param name="g"></param>
        public static void SetHighQuality(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
        }

        /// <summary>
        /// 设置GDI默认值
        /// </summary>
        /// <param name="g"></param>
        public static void SetDefaultQuality(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.Default;
            g.InterpolationMode = InterpolationMode.Default;
            g.CompositingQuality = CompositingQuality.Default;
        }

        public static Rectangle GetRect(Rectangle rect, Padding padding)
        {
            return new Rectangle(
                new Point(rect.X + padding.Left, rect.Y + padding.Top),
                new Size(rect.Width - padding.Horizontal, rect.Height - padding.Vertical)
                );
        }

        public static void Smooth(Graphics graphics, bool smooth = true)
        {
            if (smooth)
            {
               SetHighQuality(graphics);
            }
            else
            {
                SetDefaultQuality(graphics);
            }
        }

        public static System.Drawing.Drawing2D.GraphicsPath CreateRoundedRectanglePath(System.Drawing.RectangleF rect, float cornerRadius)
        {
            System.Drawing.Drawing2D.GraphicsPath roundedRect = new System.Drawing.Drawing2D.GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        #region DrawImage

        /// <summary>
        /// 在指定区域绘制图片(可设置图片透明度) (平铺绘制）
        /// Draws the image.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="img">The img.</param>
        /// User:Ryan  CreateTime:2012-8-3 21:12.
        public static void DrawImage(Graphics g, Rectangle rect, Image img, float opacity)
        {
            if (opacity <= 0)
            {
                return;
            }

            using (ImageAttributes imgAttributes = new ImageAttributes())
            {
                GDIHelper.SetImageOpacity(imgAttributes, opacity >= 1 ? 1 : opacity);
                Rectangle imageRect = new Rectangle(rect.X, rect.Y + rect.Height / 2 - img.Size.Height / 2, img.Size.Width, img.Size.Height);
                g.DrawImage(img, rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttributes);
            }
        }

        /// <summary>
        /// 绘制简单图片(平铺绘制）
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="img">The img.</param>
        /// User:Ryan  CreateTime:2012-8-4 12:54.
        public static void DrawImage(Graphics g, Rectangle rect, Image img)
        {
            Rectangle imageRect = new Rectangle(rect.X, rect.Y + rect.Height / 2 - img.Size.Height / 2, img.Size.Width, img.Size.Height);
            g.DrawImage(img, rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
        }

        private const int SRCCOPY  = 0x00CC0020;
        public static void QuickDrawImage(Graphics g, Point pos, Bitmap bitmap)
        {
            IntPtr destDC = g.GetHdc();
            IntPtr destMemDC = CreateCompatibleDC(destDC);
            IntPtr srcHBitmap = bitmap.GetHbitmap();
            IntPtr hOldObject = SelectObject(destMemDC, srcHBitmap);
            BitBlt(destDC, pos.X, pos.Y, bitmap.Width, bitmap.Height, destMemDC, 0, 0, SRCCOPY);
            //SelectObject(destDC, hOldObject);
            DeleteDC(destMemDC);
            DeleteObject(srcHBitmap);
            g.ReleaseHdc();
        }

        public static Bitmap GetBitmapFromGraphics(Graphics g, Rectangle rect)
        {
            Bitmap bitmap = null;

            IntPtr srcDC = g.GetHdc();
            IntPtr srcMemDC = CreateCompatibleDC(srcDC);

            IntPtr hBitmap = CreateCompatibleBitmap(srcDC, rect.Width, rect.Height);
            IntPtr oldHBitmap = SelectObject(srcMemDC, hBitmap);

            BitBlt(srcMemDC, 0, 0, rect.Width, rect.Height, srcDC, rect.X, rect.Y, SRCCOPY);

            try
            {
                bitmap = Image.FromHbitmap(hBitmap);
            }
            finally
            {
                SelectObject(srcMemDC, oldHBitmap);
                DeleteObject(hBitmap);

                DeleteDC(srcMemDC);
                DeleteObject(srcDC);
                g.ReleaseHdc();
            }

            return bitmap;
        }

        public static Bitmap GetBitmapFromControl(Control control, Rectangle rect)
        {
            using(Graphics g = control.CreateGraphics())
            {
                if(rect == null)
                {
                    rect = control.ClientRectangle;
                }

                return GetBitmapFromGraphics(g, rect);
            }
        }

        public static void QuickDrawImage(Graphics g, Point pos, Image image)
        {
            using (Bitmap srcBitmap = new Bitmap(image))
            {
                IntPtr destDC = g.GetHdc();
                Graphics srcGraphics = Graphics.FromImage(image);
                IntPtr srcDC = srcGraphics.GetHdc();
               
                IntPtr srcHBitmap = srcBitmap.GetHbitmap();

                IntPtr hOldObject = SelectObject(srcDC, srcHBitmap);

                BitBlt(destDC, pos.X, pos.Y, image.Width, image.Height, srcDC, 0, 0, SRCCOPY);
                
                SelectObject(srcDC, hOldObject);
                
                DeleteObject(srcHBitmap);

                g.ReleaseHdc();
                srcGraphics.ReleaseHdc();
                srcGraphics.Dispose();
            }
        }

        //public static void QuickDrawImage(Graphics g, Point pos, Image image)
        //{
        //    using (Bitmap srcBitmap = new Bitmap(image))
        //    {
        //        IntPtr destDC = g.GetHdc();
        //        //IntPtr destMemDC = CreateCompatibleDC(destDC);
        //        //IntPtr oldDest = SelectObject(destMemDC, IntPtr.Zero);

        //        Graphics srcGraphics = Graphics.FromImage(image);
        //        IntPtr srcDC = srcGraphics.GetHdc();
        //        //IntPtr srcMemDC = CreateCompatibleDC(srcDC);
        //        IntPtr srcHBitmap = srcBitmap.GetHbitmap();

        //        IntPtr hOldObject = SelectObject(srcDC, srcHBitmap);

        //        BitBlt(destDC, pos.X, pos.Y, image.Width, image.Height, srcDC, 0, 0, SRCCOPY);

        //        SelectObject(srcDC, hOldObject);
        //        //SelectObject(destMemDC, oldDest);
        //        //SelectObject(srcMemDC, oldSrc);

        //        //DeleteObject(destMemDC);
        //        //DeleteObject(srcMemDC);
        //        DeleteObject(srcHBitmap);
        //        g.ReleaseHdc();
        //        srcGraphics.ReleaseHdc();
        //        srcGraphics.Dispose();
        //    }
        //}

        public static void QuickDrawImage(Graphics g, Rectangle destRect, Image image)
        {
            using (Bitmap srcBitmap = new Bitmap(image))
            {
                IntPtr destDC = g.GetHdc();
                //IntPtr destMemDC = CreateCompatibleDC(destDC);
                //IntPtr oldDest = SelectObject(destMemDC, IntPtr.Zero);

                Graphics srcGraphics = Graphics.FromImage(image);
                IntPtr srcDC = srcGraphics.GetHdc();
                //IntPtr srcMemDC = CreateCompatibleDC(srcDC);
                IntPtr srcHBitmap = srcBitmap.GetHbitmap();

                IntPtr hOldObject = SelectObject(srcDC, srcHBitmap);

                //BitBlt(destMemDC, pos.X, pos.Y, image.Width, image.Height, srcMemDC, 0, 0, SRCCOPY);
                //BitBlt(destDC, pos.X, pos.Y, image.Width, image.Height, srcDC, 0, 0, SRCCOPY);
                StretchBlt(destDC, destRect.X, destRect.Y, destRect.Width, destRect.Height, srcDC, 0, 0, image.Width, image.Height, SRCCOPY);

                SelectObject(srcDC, hOldObject);
                //SelectObject(destMemDC, oldDest);
                //SelectObject(srcMemDC, oldSrc);

                //DeleteObject(destMemDC);
                //DeleteObject(srcMemDC);
                DeleteObject(srcHBitmap);
                g.ReleaseHdc();
                srcGraphics.ReleaseHdc();
                srcGraphics.Dispose();
            }
        }

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int width, int nHeight);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern int BitBlt(
            IntPtr hdcDest,     // handle to destination DC (device context)
            int nXDest,         // x-coord of destination upper-left corner
            int nYDest,         // y-coord of destination upper-left corner
            int nWidth,         // width of destination rectangle
            int nHeight,        // height of destination rectangle
            IntPtr hdcSrc,      // handle to source DC
            int nXSrc,          // x-coordinate of source upper-left corner
            int nYSrc,          // y-coordinate of source upper-left corner
            System.Int32 dwRop  // raster operation code
            );

        [System.Runtime.InteropServices.DllImport("gdi32", EntryPoint = "StretchBlt")]
        public static extern int StretchBlt(
                IntPtr hdc,
                int x,
                int y,
                int nWidth,
                int nHeight,
                IntPtr hSrcDC,
                int xSrc,
                int ySrc,
                int nSrcWidth,
                int nSrcHeight,
                int dwRop
        );

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr obj);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        public static extern void DeleteObject(IntPtr obj);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        public static extern void DeleteDC(IntPtr hdcPtr);

        /// <summary>
        /// 按照指定区域绘制指定大小的图片
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="img">The img.</param>
        /// <param name="imgSize">Size of the img.</param>
        /// User:Ryan  CreateTime:2012-8-4 13:34.
        public static void DrawImage(Graphics g, Rectangle rect, Image img, Size imgSize)
        {
            if (g == null || img == null)
            {
                return;
            }

            int x = rect.X + rect.Width / 2 - imgSize.Width / 2, y = rect.Y;
            Rectangle imageRect = new Rectangle(x, y + rect.Height / 2 - imgSize.Height / 2, imgSize.Width, imgSize.Height);
            g.DrawImage(img, imageRect);
        }

        #endregion

        #region Draw string
        public static void DrawString(Graphics g, Rectangle rect, string text, Font font, Color forceColor)
        {
            //g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            using (SolidBrush brush = new SolidBrush(forceColor))
            {
                g.DrawString(text, font, brush, rect, sf);
                //g.DrawString(text, font, brush, x, y + rect.Height / 2 - Convert.ToInt32(sf.Height) / 2 + 2);
                //g.DrawString(text, font, brush, x, y + rect.Height / 2 - Convert.ToInt32(sf.Height) / 2 + 2);
            }
            sf.Dispose();
        }
        #endregion

        #region Draw image and string

        /// <summary>
        /// 在指定的区域绘制绘制图像和文字
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="roundRect">The roundRect.</param>
        /// <param name="image">The image.</param>
        /// <param name="imageSize">Size of the image.</param>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="forceColor">Color of the force.</param>
        /// User:K.Anding  CreateTime:2011-7-24 22:07.
        public static void DrawImageAndString(Graphics g, Rectangle rect, Image image, Size imageSize, string text, Font font, Color forceColor)
        {
            int x = rect.X, y = rect.Y, len;
            SizeF sf = g.MeasureString(text, font);
            len = Convert.ToInt32(sf.Width);
            x += rect.Width / 2 - len / 2;
            if (image != null)
            {
                x -= imageSize.Width / 2;
                Rectangle imageRect = new Rectangle(x, y + rect.Height / 2 - imageSize.Height / 2, imageSize.Width, imageSize.Height);
                g.DrawImage(image, imageRect);
                x += imageSize.Width + 2;
            }

            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            using (SolidBrush brush = new SolidBrush(forceColor))
            {
                //g.DrawString(text, font, brush, x, y + rect.Height / 2 - Convert.ToInt32(sf.Height) / 2 + 2);
                g.DrawString(text, font, brush, x, y + rect.Height / 2 - Convert.ToInt32(sf.Height) / 2  + 2);
            }
        }
        #endregion

        #region FillTriangle
        /// <summary>
        /// 渲染一个三角形区域
        /// Fills the rectangle.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="color">The color.</param>
        /// User:Ryan  CreateTime:2012-8-3 21:25.
        public static void PaintTriangle(Graphics g, System.Drawing.Brush brush, Point point, int size, GraphDirection direction)
        {
            Point[] array = new Point[4];
            switch (direction)
            {
                case GraphDirection.Leftward:
                    array[0] = new Point(point.X, point.Y - size);
                    array[1] = new Point(point.X, point.Y + size);
                    array[2] = new Point(point.X - 2 * size, point.Y);
                    break;
                case GraphDirection.Rightward:
                    array[0] = new Point(point.X, point.Y - size);
                    array[1] = new Point(point.X, point.Y + size);
                    array[2] = new Point(point.X + 2 * size, point.Y);
                    break;
                case GraphDirection.Upward:
                    array[0] = new Point(point.X - size, point.Y);
                    array[1] = new Point(point.X + size, point.Y);
                    array[2] = new Point(point.X, point.Y - 2 * size);
                    break;
                default:
                    array[0] = new Point(point.X - size, point.Y);
                    array[1] = new Point(point.X + size, point.Y);
                    array[2] = new Point(point.X, point.Y + 2 * size);
                    break;
            }
            array[3] = array[0];
            g.FillPolygon(brush, array);
        }
        #endregion

        #region FillRectangle

        /// <summary>
        /// 渲染一个矩形区域（渐变色）
        /// Fills the rectangle.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="color">The color.</param>
        /// User:Ryan  CreateTime:2012-8-3 21:25.
        public static void FillRectangle(Graphics g, Rectangle rect, GradientColor color)
        {
            if (rect.Width <= 0 || rect.Height <= 0 || g == null)
            {
                return;
            }

            using (LinearGradientBrush brush = new LinearGradientBrush(rect, color.First, color.Second, LinearGradientMode.Vertical))
            {
                brush.Blend.Factors = color.Factors;
                brush.Blend.Positions = color.Positions;
                g.FillRectangle(brush, rect);
            }
        }

        /// <summary>
        /// 渲染一个矩形区域（单色）
        /// Fills the rectangle.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="color">The color.</param>
        /// User:Ryan  CreateTime:2012-8-3 21:46.
        public static void FillRectangle(Graphics g, Rectangle rect, Color color)
        {
            if (rect.Width <= 0 || rect.Height <= 0 || g == null)
            {
                return;
            }

            using (Brush brush = new SolidBrush(color))
            {
                g.FillRectangle(brush, rect);
            }
        }

        /// <summary>
        /// 渲染一个圆角矩形区域（渐变色）
        /// Fills the rectangle.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="roundRect">The round rect.</param>
        /// <param name="color">The color.</param>
        /// User:Ryan  CreateTime:2012-8-3 21:45.
        public static void FillRectangle(Graphics g, RoundRectangle roundRect, GradientColor color)
        {
            if (roundRect.Rect.Width <= 0 || roundRect.Rect.Height <= 0)
            {
                return;
            }

            using (GraphicsPath path = roundRect.ToGraphicsBezierPath())
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(roundRect.Rect, color.First, color.Second, LinearGradientMode.Vertical))
                {
                    brush.Blend.Factors = color.Factors;
                    brush.Blend.Positions = color.Positions;
                    g.FillPath(brush, path);
                }
            }
        }

        /// <summary>
        /// 渲染一个圆角矩形区域（单色）
        /// Fills the rectangle.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="roundRect">The round rect.</param>
        /// <param name="color">The color.</param>
        /// User:Ryan  CreateTime:2012-8-3 21:45.
        public static void FillRectangle(Graphics g, RoundRectangle roundRect, Color color)
        {
            if (roundRect.Rect.Width <= 0 || roundRect.Rect.Height <= 0)
            {
                return;
            }

            using (GraphicsPath path = roundRect.ToGraphicsBezierPath())
            {
                using (Brush brush = new SolidBrush(color))
                {
                    g.FillPath(brush, path);
                }
            }
        }

        #endregion

        #region FillPath

        /// <summary>
        /// 使用渐变色渲染一个图形区域
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="path">The path.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="color">The color.</param>
        /// User:Ryan  CreateTime:2012-8-6 21:27.
        public static void FillPath(Graphics g, GraphicsPath path, Rectangle rect, GradientColor color)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, color.First, color.Second, LinearGradientMode.Vertical))
            {
                brush.Blend.Factors = color.Factors;
                brush.Blend.Positions = color.Positions;
                g.FillPath(brush, path);
            }
        }

        /// <summary>
        /// 渲染圆角矩形区域（级渲染）
        /// </summary>
        /// <param name="g">The Graphics.</param>
        /// <param name="roundRect">The RoundRectangle.</param>
        /// <param name="color1">The color1.</param>
        /// <param name="color2">The color2.</param>
        /// <param name="blend">色彩混合渲染方案</param>
        /// User:K.Anding  CreateTime:2011-7-20 23:27.
        public static void FillPath(Graphics g, RoundRectangle roundRect, Color color1, Color color2, Blend blend)
        {
            GradientColor color = new GradientColor(color1, color2, blend.Factors, blend.Positions);
            GDIHelper.FillRectangle(g, roundRect, color);
        }

        /// <summary>
        /// 使用渐变色填充区域
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="roundRect">The round rect.</param>
        /// <param name="color1">The color1.</param>
        /// <param name="color2">The color2.</param>
        /// User:Ryan  CreateTime:2012-8-4 19:15.
        public static void FillPath(Graphics g, RoundRectangle roundRect, Color color1, Color color2)
        {
            if (roundRect.Rect.Width <= 0 || roundRect.Rect.Height <= 0)
            {
                return;
            }

            using (GraphicsPath path = roundRect.ToGraphicsBezierPath())
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(roundRect.Rect, color1, color2, LinearGradientMode.Vertical))
                {
                    g.FillPath(brush, path);
                }
            }
        }



        #endregion

        public static void FillPath(Graphics g, Color color, GraphicsPath path, bool smooth = true)
        {
            using (SolidBrush sb = new SolidBrush(color))
            {
                //SetHighQuality(Graphics);
                Smooth(g, smooth);
                g.FillPath(sb, path);
                Smooth(g, false);
            }
        }

        public static void FillRadialGradientEllipse(Graphics g, Rectangle rect, Color centerColor, Color surroundColor)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(rect);

                PathGradientBrush brush = new PathGradientBrush(path);
                brush.CenterColor = centerColor;
                brush.SurroundColors = new Color[] { surroundColor };

                g.FillPath(brush, path);

                brush.Dispose();
            }
        }

        //public static void DrawPath(Graphics g, Color color, GraphicsPath path, bool smooth = true)
        //{
        //    using (Pen pen = new Pen(color))
        //    {
        //        //SetHighQuality(Graphics);
        //        Smooth(g, smooth);
        //        g.DrawPath(pen, path);
        //        Smooth(g, false);
        //    }
        //}

        public static void DrawPath(Graphics g, Color color, GraphicsPath path, float width = 1, bool smooth = true)
        {
            using (Pen pen = new Pen(color, width))
            {
                //SetHighQuality(Graphics);
                Smooth(g, smooth);
                g.DrawPath(pen, path);
                Smooth(g, false);
            }
        }

        #region DrawPathBorder

        //public static void DrawBorder(Graphics g, Control control, Color color)
        //{
        //    using (Pen pen = new Pen(color, 1.6f))
        //    {
        //        GDIHelper.SetHighQuality(g);
        //        Rectangle client = new Rectangle(Point.Empty, new Size(control.Width - 1, control.Height - 1));
        //        g.DrawRectangle(pen, client);
        //        //GraphicsPath border = new GraphicsPath();
        //        //Rectangle client = new Rectangle(Point.Empty, new Size(control.Width - 1, control.Height - 1));
        //        //border.AddRectangle(client);
        //        //g.DrawPath(pen, border);
        //        //border.Dispose();
        //    }
        //}

        public static void DrawBorder(Graphics g, Rectangle rect, Color color, float width)
        {
            using (Pen pen = new Pen(color, width))
            {
                Rectangle client = new Rectangle(rect.Location, new Size(rect.Width - 1, rect.Height - 1));
                g.DrawRectangle(pen, client);
            }
        }

        public static void DrawBorder(Graphics g, Control control, Color color, float width)
        {
            using (Pen pen = new Pen(color, width))
            {
                Rectangle client = new Rectangle(Point.Empty, new Size(control.Width - 1, control.Height - 1));
                g.DrawRectangle(pen, client);
            }
        }

        public static void DrawBorder(Graphics g, Control control, Color color, int width, int margin)
        {
            using (Pen pen = new Pen(color, width))
            {
                Rectangle client = new Rectangle(new Point(margin, margin), new Size(control.Width - 2 * margin, control.Height - 2 * margin));
                g.DrawRectangle(pen, client);
            }
        }

        /// <summary>
        /// 绘制一个图形区域的边框
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="path">The path.</param>
        /// <param name="color">The color.</param>
        /// User:Ryan  CreateTime:2012-8-6 21:31.
        public static void DrawPathBorder(Graphics g, GraphicsPath path, Color color)
        {
            using (Pen pen = new Pen(color, 1))
            {
                g.DrawPath(pen, path);
            }
        }

        /// <summary>
        /// 绘制指定区域路径的边框.
        /// 注意:若要设置边框宽度，需要自己调整区域，它是向外绘制的
        /// </summary>
        /// <param name="g">The Graphics.</param>
        /// <param name="roundRect">The RoundRectangle.</param>
        /// <param name="color">The color.</param>
        /// <param name="borderWidth">Width of the border.</param>
        /// User:Ryan  CreateTime:2011-07-28 16:11.
        public static void DrawPathBorder(Graphics g, RoundRectangle roundRect, Color color, int borderWidth)
        {
            using (GraphicsPath path = roundRect.ToGraphicsBezierPath())
            {
                using (Pen pen = new Pen(color, borderWidth))
                {
                    g.DrawPath(pen, path);
                }
            }
        }

        /// <summary>
        /// 绘制指定区域路径的边框
        /// </summary>
        /// User:Ryan  CreateTime:2011-07-28 16:11.
        public static void DrawPathBorder(Graphics g, RoundRectangle roundRect, Color color)
        {
            DrawPathBorder(g, roundRect, color, 1);
        }

        /// <summary>
        /// 绘制指定区域路径的边框
        /// </summary>
        public static void DrawPathInnerBorder(Graphics g, RoundRectangle roundRect, Color color)
        {
            Rectangle rect = roundRect.Rect;
            rect.X++; rect.Y++; rect.Width -= 2; rect.Height -= 2;
            DrawPathBorder(g, new RoundRectangle(rect, roundRect.CornerRadius), color);
        }

        /// <summary>
        /// 绘制指定区域路径的边框
        /// </summary>
        public static void DrawPathOuterBorder(Graphics g, RoundRectangle roundRect, Color color)
        {
            Rectangle rect = roundRect.Rect;
            rect.X--; rect.Y--; rect.Width += 2; rect.Height += 2;
            DrawPathBorder(g, new RoundRectangle(rect, roundRect.CornerRadius), color);
        }

        /// <summary>
        /// 绘制指定区域路径的边框
        /// </summary>
        public static void DrawPathOuterBorder(Graphics g, RoundRectangle roundRect, Color color, int borderWidth)
        {
            Rectangle rect = roundRect.Rect;
            rect.X--; rect.Y--; rect.Width += 2; rect.Height += 2;
            DrawPathBorder(g, new RoundRectangle(rect, roundRect.CornerRadius), color, borderWidth);
        }

        /// <summary>
        /// 绘制指定区域路径的边框
        /// </summary>
        //public static void DrawPathBorder(Graphics g, RoundRectangle roundRect, Color color)
        //{
        //    DrawPathBorder(g, roundRect, color);
        //}

        /// <summary>
        /// 绘制指定区域路径的边框
        /// </summary>
        //public static void DrawPathInnerBorder(Graphics g, RoundRectangle roundRect, Color color)
        //{
        //    //Color c = SkinManager.CurrentSkin.InnerBorderColor;
        //    ////c = Color.Green;
        //    DrawPathInnerBorder(g, roundRect, color);
        //}

        /// <summary>
        /// 绘制指定区域路径的边框
        /// </summary>
        //public static void DrawPathOuterBorder(Graphics g, RoundRectangle roundRect, Color color)
        //{
        //    DrawPathOuterBorder(g, roundRect, color);
        //}

        #endregion

        #region DrawGradientLine

        /// <summary>
        /// 绘制阶梯渐变的线条，可以在参数Blend对象中设置色彩混合规则
        /// </summary>
        public static void DrawGradientLine(Graphics g, Color lineColor, Blend blend, int angle, int lineWidth, int x1, int y1, int x2, int y2)
        {
            Color c1 = lineColor;
            Color c2 = Color.FromArgb(10, c1);
            Rectangle rect = new Rectangle(x1, y1, x2 - x1 + 1, y2 - y1 + 1);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, c1, c2, angle))
            {
                brush.Blend = blend;
                using (Pen pen = new Pen(brush, lineWidth))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.DrawLine(pen, x1, y1, x2, y2);
                }
            }
        }

        /// <summary>
        /// 绘制向两边阶梯渐变的线条
        /// </summary>
        public static void DrawGradientLine(Graphics g, Color lineColor, int angle, int x1, int y1, int x2, int y2)
        {
            Blend blend = new Blend();
            blend.Positions = new float[] { 0f, .15f, .5f, .85f, 1f };
            blend.Factors = new float[] { 1f, .4f, 0f, .4f, 1f };
            DrawGradientLine(g, lineColor, blend, angle, 1, x1, y1, x2, y2);
        }

        #endregion

        #region 设置图片透明度(SetImageOpacity)

        /// <summary>
        /// 设置图片透明度.
        /// </summary>
        /// <param name="imgAttributes">The ImageAttributes.</param>
        /// <param name="opacity">透明度，0完全透明，1不透明（The opacity.）</param>
        /// User:Ryan  CreateTime:2011-07-28 15:26.
        public static void SetImageOpacity(ImageAttributes imgAttributes, float opacity)
        {
            float[][] nArray ={ new float[] {1, 0, 0, 0, 0},
                                                new float[] {0, 1, 0, 0, 0},
                                                new float[] {0, 0, 1, 0, 0},
                                                new float[] {0, 0, 0, opacity, 0},
                                                new float[] {0, 0, 0, 0, 1}};
            ColorMatrix matrix = new ColorMatrix(nArray);
            imgAttributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        }

        #endregion

        #region DrawArrow

        /// <summary>
        /// 绘制指针（方向箭头）,默认指针颜色为（55, 63, 78）
        /// </summary>
        /// <param name="g">The Graphics.</param>
        /// <param name="direction">指针的方向.</param>
        /// <param name="rect">绘制的区域.</param>
        /// <param name="arrowSize">指针的大小，即长宽.</param>
        /// User:K.Anding  CreateTime:2011-8-9 23:33.
        public static void DrawArrow(Graphics g, ArrowDirection direction, Rectangle rect, Size arrowSize)
        {
            float offset = 1.8f;
            DrawArrow(g, direction, rect, arrowSize, offset, Color.FromArgb(55, 63, 78));
        }

        /// <summary>
        /// 绘制指针（方向箭头）
        /// </summary>
        /// <param name="g">The Graphics.</param>
        /// <param name="direction">指针的方向.</param>
        /// <param name="rect">绘制的区域.</param>
        /// <param name="arrowSize">指针的大小，即长宽.</param>
        /// <param name="c">指针颜色</param>
        /// User:K.Anding  CreateTime:2011-8-9 23:33.
        public static void DrawArrow(Graphics g, ArrowDirection direction, Rectangle rect, Size arrowSize, float offset, Color c)
        {
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2 + 1);
            using (GraphicsPath path = new GraphicsPath())
            {
                PointF[] points = null;
                switch (direction)
                {
                    case ArrowDirection.Down:
                        points = new PointF[] { 
                            new PointF(center.X,center.Y+arrowSize.Height/2),
                            new PointF(center.X-arrowSize.Width/2,center.Y-arrowSize.Height/2),
                            new PointF(center.X,center.Y-arrowSize.Height/2+offset),
                            new PointF(center.X+arrowSize.Width/2,center.Y-arrowSize.Height/2),};
                        break;
                    case ArrowDirection.Up:
                        points = new PointF[] { 
                            new PointF(center.X,center.Y-arrowSize.Height/2),
                            new PointF(center.X-arrowSize.Width/2,center.Y+arrowSize.Height/2),
                            new PointF(center.X,center.Y+arrowSize.Height/2-offset),
                            new Point(center.X+arrowSize.Width/2,center.Y+arrowSize.Height/2),};
                        break;
                    case ArrowDirection.Left:
                        points = new PointF[] { 
                            new PointF(center.X-arrowSize.Width/2,center.Y),
                            new PointF(center.X+arrowSize.Width/2,center.Y-arrowSize.Height/2),
                            new PointF(center.X+arrowSize.Width/2-offset,center.Y),
                            new PointF(center.X+arrowSize.Width/2,center.Y+arrowSize.Height/2),};
                        break;
                    case ArrowDirection.Right:
                        points = new PointF[] { 
                            new PointF(center.X+arrowSize.Width/2,center.Y),
                            new PointF(center.X-arrowSize.Width/2,center.Y-arrowSize.Height/2),
                            new PointF(center.X-arrowSize.Width/2+offset,center.Y),
                            new PointF(center.X-arrowSize.Width/2,center.Y+arrowSize.Height/2),};
                        break;
                }

                path.AddLines(points);
                using (Brush brush = new SolidBrush(c))
                {
                    g.FillPath(brush, path);
                }
            }
        }

        #endregion

        #region DrawCrystalButton

        /// <summary>
        /// 绘制水晶原形按钮
        /// </summary>
        /// <param name="g">The Graphics.</param>
        /// <param name="rect">The Rectangle.</param>
        /// <param name="surroundColor">Color of the surround.</param>
        /// <param name="centerColor">Color of the center.</param>
        /// <param name="lightColor">Color of the light.</param>
        /// <param name="blend">The blend.</param>
        /// User:K.Anding  CreateTime:2011-8-20 19:42.
        public static void DrawCrystalButton(Graphics g, Rectangle rect, Color surroundColor, Color centerColor, Color lightColor, Blend blend)
        {
            int sweep, start;
            Point p1, p2, p3;
            Rectangle rinner = rect;
            rinner.Inflate(-1, -1);
            using (GraphicsPath p = new GraphicsPath())
            {
                p.AddEllipse(rect);

                using (PathGradientBrush gradient = new PathGradientBrush(p))
                {
                    gradient.WrapMode = WrapMode.Clamp;
                    gradient.CenterPoint = new PointF(Convert.ToSingle(rect.Left + rect.Width / 2), Convert.ToSingle(rect.Bottom));
                    gradient.CenterColor = centerColor;
                    gradient.SurroundColors = new Color[] { surroundColor };
                    gradient.Blend = blend;
                    g.FillPath(gradient, p);
                }
            }

            // Bottom round shine
            Rectangle bshine = new Rectangle(0, 0, rect.Width / 2, rect.Height / 2);
            bshine.X = rect.X + (rect.Width - bshine.Width) / 2;
            bshine.Y = rect.Y + rect.Height / 2;

            using (GraphicsPath p = new GraphicsPath())
            {
                p.AddEllipse(bshine);

                using (PathGradientBrush gradient = new PathGradientBrush(p))
                {
                    gradient.WrapMode = WrapMode.Clamp;
                    gradient.CenterPoint = new PointF(Convert.ToSingle(rect.Left + rect.Width / 2), Convert.ToSingle(rect.Bottom));
                    gradient.CenterColor = Color.White;
                    gradient.SurroundColors = new Color[] { Color.Transparent };

                    g.FillPath(gradient, p);
                }
            }

            // Upper Glossy
            using (GraphicsPath p = new GraphicsPath())
            {
                sweep = 160;
                start = 180 + (180 - sweep) / 2;
                p.AddArc(rinner, start, sweep);

                p1 = Point.Round(p.PathData.Points[0]);
                p2 = Point.Round(p.PathData.Points[p.PathData.Points.Length - 1]);
                p3 = new Point(rinner.Left + rinner.Width / 2, p2.Y - 3);
                p.AddCurve(new Point[] { p2, p3, p1 });

                using (PathGradientBrush gradient = new PathGradientBrush(p))
                {
                    gradient.WrapMode = WrapMode.Clamp;
                    gradient.CenterPoint = p3;
                    gradient.CenterColor = Color.Transparent;
                    gradient.SurroundColors = new Color[] { lightColor };

                    blend = new Blend(3);
                    blend.Factors = new float[] { .3f, .8f, 1f };
                    blend.Positions = new float[] { 0, 0.50f, 1f };
                    gradient.Blend = blend;

                    g.FillPath(gradient, p);
                }

                using (LinearGradientBrush b = new LinearGradientBrush(new Point(rect.Left, rect.Top), new Point(rect.Left, p1.Y), Color.White, Color.Transparent))
                {
                    blend = new Blend(4);
                    blend.Factors = new float[] { 0f, .4f, .8f, 1f };
                    blend.Positions = new float[] { 0f, .3f, .4f, 1f };
                    b.Blend = blend;
                    g.FillPath(b, p);
                }
            }

            // Upper shine
            using (GraphicsPath p = new GraphicsPath())
            {
                sweep = 160;
                start = 180 + (180 - sweep) / 2;
                p.AddArc(rinner, start, sweep);

                using (Pen pen = new Pen(Color.White))
                {
                    g.DrawPath(pen, p);
                }
            }

            // Lower Shine
            using (GraphicsPath p = new GraphicsPath())
            {
                sweep = 160;
                start = (180 - sweep) / 2;
                p.AddArc(rinner, start, sweep);
                Point pt = Point.Round(p.PathData.Points[0]);

                Rectangle rrinner = rinner; rrinner.Inflate(-1, -1);
                sweep = 160;
                start = (180 - sweep) / 2;
                p.AddArc(rrinner, start, sweep);

                using (LinearGradientBrush b = new LinearGradientBrush(
                    new Point(rinner.Left, rinner.Bottom),
                    new Point(rinner.Left, pt.Y - 1),
                    lightColor, Color.FromArgb(50, lightColor)))
                {
                    g.FillPath(b, p);
                }
            }
        }

        #endregion

        #region EllipseRender

        /// <summary>
        ///绘制椭圆的边框
        /// </summary>
        /// User:Ryan  CreateTime:2011-07-29 17:10.
        public static void DrawEllipseBorder(Graphics g, Rectangle rect, Color color, int borderWidth)
        {
            using (Pen pen = new Pen(color, borderWidth))
            {
                g.DrawEllipse(pen, rect);
            }
        }

        /// <summary>
        /// 渲染一个圆形区域(简单渲染)
        /// </summary>
        /// User:K.Anding  CreateTime:2011-7-30 14:37.
        public static void FillEllipse(Graphics g, Rectangle rect, Color color)
        {
            using (SolidBrush brush = new SolidBrush(color))
            {
                g.FillEllipse(brush, rect);
            }
        }

        /// <summary>
        /// 渲染一个圆形区域（高级渲染）
        /// </summary>
        /// User:K.Anding  CreateTime:2011-7-30 14:37.
        public static void FillEllipse(Graphics g, Rectangle rect, Color color1, Color color2)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(rect);
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = color1;
                    brush.SurroundColors = new Color[] { color2 };
                    Blend blend = new Blend();
                    blend.Factors = new float[] { 0f, 0.8f, 1f };
                    blend.Positions = new float[] { 0f, 0.5f, 1f };
                    brush.Blend = blend;
                    g.FillPath(brush, path);
                }
            }
        }

        #endregion

        #region DrawCheckBox

        /// <summary>
        /// 绘制选择状态(勾选状态)
        /// 手动绘制勾勾
        /// </summary>
        /// User:K.Anding  CreateTime:2011-7-24 21:59.
        public static void DrawCheckedState(Graphics g, Rectangle rect, Color color)
        {
            PointF[] points = new PointF[3];
            points[0] = new PointF(
                rect.X + rect.Width / 5f,
                rect.Y + rect.Height / 2.5f);
            points[1] = new PointF(
                rect.X + rect.Width / 2.5f,
                rect.Bottom - rect.Height / 3.6f);
            points[2] = new PointF(
                rect.Right - rect.Width / 5.0f,
                rect.Y + rect.Height / 7.0f);
            using (Pen pen = new Pen(color, 2))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.DrawLines(pen, points);
            }
        }

        /// <summary>
        /// 绘制选择状态(勾选状态)
        /// 设计好的图片勾
        /// </summary>
        /// User:K.Anding  CreateTime:2011-7-24 21:59.
        //public static void DrawCheckedStateByImage(Graphics g, Rectangle rect)
        //{
        //    rect.Inflate(-1, -1);
        //    g.DrawImage(AEC.Properties.Resources.check, rect);
        //}

        /// <summary>
        /// 绘制选择状态
        /// </summary>
        /// User:K.Anding  CreateTime:2011-7-24 21:59.
        public static void DrawCheckedState(Graphics g, Rectangle rect)
        {
            DrawCheckedState(g, rect, Color.Green);
        }

        /// <summary>
        /// 绘制checkbox的边框和背景
        /// </summary>
        /// User:Ryan  CreateTime:2011-07-25 18:00.
        public static void DrawCheckBox(Graphics g, RoundRectangle roundRect, Color centerColor, Color borderColor)
        {
            using (GraphicsPath path = roundRect.ToGraphicsBezierPath())
            {
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = centerColor;
                    brush.SurroundColors = new Color[] { borderColor };
                    Blend blend = new Blend();
                    blend.Positions = new float[] { 0f, 0.18f, 1f };
                    blend.Factors = new float[] { 0f, 0.89f, 1f };
                    brush.Blend = blend;
                    g.FillPath(brush, path);
                }

                DrawPathBorder(g, roundRect, borderColor);
            }
        }

        #endregion

        #region GetOppositeColor

        /// <summary>
        /// 获取指定颜色的相反颜色
        /// </summary>
        /// <param name="sourceColor">Color of the source.</param>
        /// <returns></returns>
        /// User:Ryan  CreateTime:2012-11-24 15:42.
        public static Color GetOppositeColor(Color sourceColor)
        {
            return Color.FromArgb(255 - sourceColor.A, 255 - sourceColor.R, 255 - sourceColor.G, 255 - sourceColor.B);
        }

        #endregion

        #region

        #endregion
    }
}
