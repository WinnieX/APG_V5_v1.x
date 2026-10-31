using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Drawing;

namespace AEC.UI
{
    public class FontHelper
    {
        public static FontFamily defFontFamily = null;
        public static FontFamily mainFontFamily = null;
        public static FontFamily labelFontFamily = null;

        static FontHelper()
        {
            byte[] mnjzyFont = global::AEC.Properties.Resources.迷你简准圆;
            IntPtr fontData = Marshal.AllocCoTaskMem(mnjzyFont.Length);
            Marshal.Copy(mnjzyFont, 0, fontData, mnjzyFont.Length);
            uint cFonts = 0;
            AddFontMemResourceEx(fontData, (uint)mnjzyFont.Length, IntPtr.Zero, ref cFonts);
            var fontCollectionFromMemory = new PrivateFontCollection();
            fontCollectionFromMemory.AddMemoryFont(fontData, mnjzyFont.Length);
            Marshal.FreeCoTaskMem(fontData);

            //mainFontFamily = fontCollectionFromMemory.Families[0];
            mainFontFamily = fontCollectionFromMemory.Families[0];
            defFontFamily = fontCollectionFromMemory.Families[0];
            labelFontFamily = fontCollectionFromMemory.Families[0];

            if(AEC.Tools.OSVersionHelper.isWin7())
            {
                InstalledFontCollection ifc = new InstalledFontCollection();
                foreach (FontFamily ff in ifc.Families)
                {
                    if (ff.Name == "微软雅黑")
                    {
                        //Log.OutputLine("font: {0}", ff.Name);
                        mainFontFamily = ff;
                        defFontFamily = ff;
                        labelFontFamily = ff;
                    }
                }
            }
            else if(AEC.Tools.OSVersionHelper.isWinXP())
            {
                defFontFamily = new FontFamily("宋体");
                labelFontFamily = new FontFamily("Microsoft Sans Serif");
            }
        }

        //public static Font CreateFont(float size, FontStyle style = FontStyle.Regular)
        //{
        //    return new Font(defFontFamily, size, style, System.Drawing.GraphicsUnit.Pixel);
        //}

        private static Font font10 = null;
        private static Font font12 = null;
        private static Font font14 = null;
        private static Font labelFont10 = null;

        public static Font CreatePixelFont(float size)
        {
            return new Font(defFontFamily, size, FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
        }

        public static Font CreatePointFont(float size)
        {
            return new Font(defFontFamily, size, FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        }

        public static Font CreateFont10()
        {
            if (font10 == null)
            {
                font10 = new Font(defFontFamily, 13.3f, FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            }

            return font10;
        }

        public static Font CreateFont12()
        {
            if (font12 == null)
            {
                font12 = new Font(defFontFamily, 14.6f, FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            }

            return font12;
        }

        public static Font CreateFont14()
        {
            if (font14 == null)
            {
                font14 = new Font(defFontFamily, 18.67f, FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            }

            return font14;
        }

        public static Font CreateLabelFont10()
        {
            if (labelFont10 == null)
            {
                labelFont10 = new Font(labelFontFamily, 13.3f, FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            }

            return labelFont10;
        }

        //public static Font CreateFont14()
        //{
        //    return new Font(defFontFamily, 14, FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        //}

        //public static Font CreateFont16()
        //{
        //    return new Font(defFontFamily, 16, FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
        //}

        //public static Font CreateFont18()
        //{
        //    return new Font(defFontFamily, 24, FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
        //}

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);
    }
}
