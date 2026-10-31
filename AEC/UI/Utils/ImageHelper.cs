using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace AEC.UI.Utils
{
    public static class ImageHelper
    {
        public static Bitmap Rotate(Bitmap bmp, float angle, Color bkColor)
        {
            int w = bmp.Width;
            int h = bmp.Height;

            PixelFormat pf = bkColor == Color.Transparent ? PixelFormat.Format32bppArgb : bmp.PixelFormat;

            Bitmap tmp = new Bitmap(w, h, pf);
            Graphics g = System.Drawing.Graphics.FromImage(tmp);
            g.Clear(bkColor);
            g.DrawImageUnscaled(bmp, 0, 0);
            g.Dispose();

            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new RectangleF(0f, 0f, w, h));
            Matrix matrix = new Matrix();
            matrix.Rotate(angle);
            RectangleF rct = path.GetBounds(matrix);

            int newWidth = (int)Math.Ceiling(rct.Width);
            int newHeight = (int)Math.Ceiling(rct.Height);

            //mControl.Log.OutputLine("matrix bounds:{0}", rct.ToString());
            //Bitmap dst = new Bitmap((int)rct.Width, (int)rct.Height, pf);
            Bitmap dst = new Bitmap(newWidth, newHeight, pf);
            g = System.Drawing.Graphics.FromImage(dst);
            g.Clear(bkColor);
            //g.TranslateTransform(-rct.X, -rct.Y);
            g.TranslateTransform(-rct.X, -rct.Y);
            g.RotateTransform(angle);
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.DrawImageUnscaled(tmp, 0, 0);
            g.Dispose();

            tmp.Dispose();

            return dst;
        }

        public static Bitmap RotateImageN(Bitmap b, float angle)
        {
            //Create a new empty bitmap to hold rotated image.
            Bitmap returnBitmap = new Bitmap(b.Width, b.Height);
            //Make a graphics object from the empty bitmap.
            Graphics g = Graphics.FromImage(returnBitmap);
            //move rotation point to center of image.
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
            //Rotate.        
            g.RotateTransform(angle);
            //Move image back.
            g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
            //Draw passed in image onto graphics object.
            //Found ERROR 1: Many people do g.DwarImage(b,0,0); The problem is that you re giving just the position
            //Found ERROR 2: Many people do g.DrawImage(b, new Point(0,0)); The size still not present hehe :3

            g.DrawImage(b, 0, 0, b.Width, b.Height);  //My Final Solution :3
            g.Dispose();

            return returnBitmap;
        }

        public static Bitmap RotateImage(Bitmap image, float rotateAtX, float rotateAtY, float angle, bool bNoClip)
        {
            int W, H, X, Y;
            if (bNoClip)
            {
                double dW = (double)image.Width;
                double dH = (double)image.Height;

                double degrees = Math.Abs(angle);
                if (degrees <= 90)
                {
                    double radians = 0.0174532925 * degrees;
                    double dSin = Math.Sin(radians);
                    double dCos = Math.Cos(radians);
                    W = (int)(dH * dSin + dW * dCos);
                    H = (int)(dW * dSin + dH * dCos);
                    X = (W - image.Width) / 2;
                    Y = (H - image.Height) / 2;
                }
                else
                {
                    degrees -= 90;
                    double radians = 0.0174532925 * degrees;
                    double dSin = Math.Sin(radians);
                    double dCos = Math.Cos(radians);
                    W = (int)(dW * dSin + dH * dCos);
                    H = (int)(dH * dSin + dW * dCos);
                    X = (W - image.Width) / 2;
                    Y = (H - image.Height) / 2;
                }
            }
            else
            {
                W = image.Width;
                H = image.Height;
                X = 0;
                Y = 0;
            }

            //create a new empty bitmap to hold rotated image
            Bitmap bmpRet = new Bitmap(W, H);
            bmpRet.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(bmpRet);

            //Put the rotation point in the "center" of the image
            g.TranslateTransform(rotateAtX + X, rotateAtY + Y);

            //rotate the image
            g.RotateTransform(angle);

            //move the image back
            g.TranslateTransform(-rotateAtX - X, -rotateAtY - Y);

            //draw passed in image onto graphics object
            g.DrawImage(image, new PointF(0 + X, 0 + Y));

            return bmpRet;
        }

    }
}
