using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace AEC.UI.LevelMeter
{
    public class LevelMeterImageFactory
    {
        public static Font labelFont = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
        public static Image CreateLevelMeterBackground(Color color, LevelMeterLEDLightImageModel levelMeterImageModel)
        {
            int imageWith = levelMeterImageModel.lightWidth;
            int imageHeight = levelMeterImageModel.totalHeight;

            Bitmap bitmap = new Bitmap(imageWith, imageHeight);
            using (SolidBrush brush = new SolidBrush(color))
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                UI.Utility.GDIHelper.SetHighQuality(g);

                for (int i = 0; i < levelMeterImageModel.lightCount; ++i)
                {
                    Rectangle rect = levelMeterImageModel.GetRect(i);// new Rectangle(0, levelMeterImageModel.firstLightYOffset + i * (levelMeterImageModel.lightHeight + levelMeterImageModel.lightSpacing), imageWith, levelMeterImageModel.lightHeight);
                    GraphicsPath path = Utility.RoundRectangle.CreateRoundedRectPath(rect, 2);

                    g.FillPath(brush, path);
                    //g.FillRectangle(brush, rect);
                    path.Dispose();

                }

                UI.Utility.GDIHelper.SetDefaultQuality(g);
            }

            return bitmap;
        }

        public static Image CreateLevelMeterForeground(LevelMeterLEDLightImageModel levelMeterImageModel)
        {
            int imageWith = levelMeterImageModel.lightWidth;
            int imageHeight = levelMeterImageModel.totalHeight;

            Bitmap bitmap = new Bitmap(imageWith, imageHeight);

            Rectangle rect = new Rectangle(Point.Empty, bitmap.Size);

            Color startColor = Color.FromArgb(255, 0, 0);
            Color endColor = Color.FromArgb(0, 129, 0);

            Color gradientColor1 = Color.FromArgb(255, 0, 0);
            Color gradientColor2 = Color.FromArgb(255, 125, 0);
            Color gradientColor3 = Color.FromArgb(255, 252, 0);
            Color gradientColor4 = Color.FromArgb(64, 216, 0);
            Color gradientColor5= Color.FromArgb(0, 160, 0);

            using (Graphics g = Graphics.FromImage(bitmap))
            using (LinearGradientBrush lgBrush = new LinearGradientBrush(rect, startColor, endColor, LinearGradientMode.Vertical))
            {
                lgBrush.SetSigmaBellShape(0.5f);

                ColorBlend cb = new ColorBlend(3)
                {
                    Colors = new Color[]
                    {
                        startColor,
                        gradientColor1,
                        gradientColor2,
                        gradientColor3,
                        gradientColor4,
                        gradientColor5,
                        endColor,
                    },
                    Positions = new float[]
                    {
                        0,
                        0.04f,
                        0.10f,
                        0.2f,
                        0.3f,
                        0.5f,
                        1.0f,
                    },
                };

                lgBrush.InterpolationColors = cb;

                UI.Utility.GDIHelper.SetHighQuality(g);

                for (int i = 0; i < levelMeterImageModel.lightCount; ++i)
                {
                    Rectangle ledRect = levelMeterImageModel.GetRect(i);
                    GraphicsPath path = Utility.RoundRectangle.CreateRoundedRectPath(ledRect, 2);
                    g.FillPath(lgBrush, path);
                    path.Dispose();
                }

                

                //g.FillRegion(brush, region);

                UI.Utility.GDIHelper.SetDefaultQuality(g);
            }

            //using (Graphics g = Graphics.FromImage(bitmap))
            //{
                

            //    Region region = new Region();

            //    for (int i = 0; i < levelMeterImageModel.lightCount; ++i)
            //    {
            //        Rectangle ledRect = new Rectangle(0, levelMeterImageModel.firstLightYOffset + i * (levelMeterImageModel.lightHeight + levelMeterImageModel.lightSpacing), imageWith, levelMeterImageModel.lightHeight);
            //        GraphicsPath path = Utility.RoundRectangle.CreateRoundedRectPath(ledRect, 2);

            //        region.Union(path);
                    
            //        //g.FillRectangle(brush, rect);
            //        path.Dispose();
            //    }

            //    UI.Utility.GDIHelper.SetHighQuality(g);

            //    g.FillRegion(brush, region);

            //    UI.Utility.GDIHelper.SetDefaultQuality(g);

            //    region.Dispose();
            //}

            return bitmap;
        }

        public static Image CreateLimiterMeterForeground(LevelMeterLEDLightImageModel levelMeterImageModel)
        {
            int imageWith = levelMeterImageModel.lightWidth;
            int imageHeight = levelMeterImageModel.totalHeight;

            Bitmap bitmap = new Bitmap(imageWith, imageHeight);

            Rectangle rect = new Rectangle(Point.Empty, bitmap.Size);

            Color startColor = Color.FromArgb(255, 255, 0);
            Color endColor = Color.FromArgb(255, 0, 0);

            Color gradientColor1 = Color.FromArgb(255, 128, 0);
            Color gradientColor2 = Color.FromArgb(255, 64, 0);
            Color gradientColor3 = Color.FromArgb(255, 32, 0);

            using (Graphics g = Graphics.FromImage(bitmap))
            using (LinearGradientBrush lgBrush = new LinearGradientBrush(rect, startColor, endColor, LinearGradientMode.Vertical))
            {
                lgBrush.SetSigmaBellShape(0.5f);

                ColorBlend cb = new ColorBlend(3)
                {
                    Colors = new Color[]
                    {
                        startColor,
                        gradientColor1,
                        gradientColor2,
                        gradientColor3,
                        endColor,
                    },
                    Positions = new float[]
                    {
                        0,
                        0.25f,
                        0.5f,
                        0.75f,
                        1.0f,
                    },
                };

                lgBrush.InterpolationColors = cb;

                UI.Utility.GDIHelper.SetHighQuality(g);

                for (int i = 0; i < levelMeterImageModel.lightCount; ++i)
                {
                    Rectangle ledRect = levelMeterImageModel.GetRect(i);
                    GraphicsPath path = Utility.RoundRectangle.CreateRoundedRectPath(ledRect, 2);
                    g.FillPath(lgBrush, path);
                    path.Dispose();
                }

                UI.Utility.GDIHelper.SetDefaultQuality(g);
            }

            return bitmap;
        }

        public static Image CreateLevelMeterTicks(Color color, LevelMeterTicksModel ticksModel)
        {
            if(ticksModel == null || ticksModel.tickList == null)
            {
                return null;
            }

            int imageWith = ticksModel.width;
            
            int levelLowerLimit = ticksModel.tickList[0];
            int levelUpperLimit = ticksModel.tickList[ticksModel.tickList.Length - 1];

            System.Diagnostics.Debug.Assert(levelLowerLimit < levelUpperLimit);

            int levelRange = levelUpperLimit - levelLowerLimit;
            int imageHeight = ticksModel.firstTickOffset + ticksModel.tickSpacing * (levelRange + 1);

            //TextFormatFlags tff = TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak;

            Bitmap bitmap = new Bitmap(imageWith, imageHeight);
            using (Pen tickLinePen = new Pen(color))
            using (Brush labelBrush = new SolidBrush(color))
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                //AEC.UI.Utility.GDIHelper.SetHighQuality(g);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                for (int currLevel = levelUpperLimit; currLevel >= levelLowerLimit; --currLevel)
                {
                    Point tickLeft;
                    Point tickRight;

                    int tickLineY = ticksModel.firstTickOffset + (levelUpperLimit - currLevel) * ticksModel.tickSpacing;

                    bool isLargeTick = new List<int>(ticksModel.tickList).Contains(currLevel);

                    if(ticksModel.tickStyle == LevelMeterTickStyle.Left)
                    {
                        tickRight = new Point(ticksModel.width, tickLineY);
                        tickLeft = new Point(isLargeTick ? ticksModel.width - ticksModel.largeTickWidth : ticksModel.width - ticksModel.smallTickWidth, tickLineY);

                        if (isLargeTick)
                        {
                            string label = currLevel.ToString();
                            if (currLevel == 0)
                            {
                                label += "dBu";
                                //g.DrawString(label, labelFont, labelBrush, new Point(0, 30), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far });
                            }
                            Size labelSize = TextRenderer.MeasureText(label, labelFont);
                            TextRenderer.DrawText(g, label, labelFont, new Point(tickLeft.X - labelSize.Width + 1, tickLeft.Y), color, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                            //g.DrawString(label, labelFont, labelBrush, tickLeft, new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center });
                            //TextRenderer.DrawText(g, label, labelFont, tickLeft, color, tff | TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
                            //TextRenderer.DrawText(g, label, labelFont, tickLeft, color, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
                        }
                    }
                    else
                    {
                        tickLeft = new Point(0, tickLineY);
                        tickRight = new Point(isLargeTick ? ticksModel.largeTickWidth : ticksModel.smallTickWidth, tickLineY);

                        if (isLargeTick)
                        {
                            string label = currLevel.ToString();
                            if (currLevel == 0)
                            {
                                label += "dBu";
                            }

                            TextRenderer.DrawText(g, label, labelFont, tickRight, color, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

                            //g.DrawString(label, labelFont, labelBrush, tickRight, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far });
                            //TextRenderer.DrawText(g, label, labelFont, tickRight, color, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                        }
                    }

                    g.DrawLine(tickLinePen, tickLeft, tickRight);
                    
                    
                    //g.DrawLine(tickLinePen, tickLeft, tickRight);
                    //e
                    //Rectangle rect = new Rectangle(0, i * (lightHeight + lightSpacing), imageWith, lightHeight);
                    //g.FillRectangle(brush, rect);
                }
            }

            return bitmap;
        }

        public static Image CreateLimiterMeterTicks(Color color, DynamicMeterTicksModel ticksModel)
        {
            int imageWith = ticksModel.width;
            int imageHeight = ticksModel.firstTickOffset + ticksModel.tickSpacing * (ticksModel.tickCount + 1);

            //TextFormatFlags tff = TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak;

            Bitmap bitmap = new Bitmap(imageWith, imageHeight);
            using (Pen tickLinePen = new Pen(color))
            using (Brush labelBrush = new SolidBrush(color))
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                //AEC.UI.Utility.GDIHelper.SetHighQuality(g);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                for (int i = 0; i <= ticksModel.tickCount; ++i)
                {
                    Point tickLeft;
                    Point tickRight;

                    int tickLineY = ticksModel.firstTickOffset + i * ticksModel.tickSpacing;

                    bool isLargeTick = (i % ticksModel.largeTickStep) == 0;

                    int currLevel = i;

                    if (ticksModel.tickStyle == LevelMeterTickStyle.Left)
                    {
                        tickRight = new Point(ticksModel.width, tickLineY);
                        tickLeft = new Point(isLargeTick ? ticksModel.width - ticksModel.largeTickWidth : ticksModel.width - ticksModel.smallTickWidth, tickLineY);

                        if (isLargeTick)
                        {
                            string label = currLevel.ToString();
                            if (currLevel == 0)
                            {
                                label += "dB";
                                //g.DrawString(label, labelFont, labelBrush, new Point(0, 30), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far });
                            }

                            TextRenderer.DrawText(g, label, labelFont, tickLeft, color, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                            //g.DrawString(label, labelFont, labelBrush, tickLeft, new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center });
                            //TextRenderer.DrawText(g, label, labelFont, tickLeft, color, tff | TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
                            //TextRenderer.DrawText(g, label, labelFont, tickLeft, color, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
                        }
                    }
                    else
                    {
                        tickLeft = new Point(0, tickLineY);
                        tickRight = new Point(isLargeTick ? ticksModel.largeTickWidth : ticksModel.smallTickWidth, tickLineY);

                        if (isLargeTick)
                        {
                            string label = currLevel.ToString();
                            if (currLevel == 0)
                            {
                                label += "dB";
                            }

                            TextRenderer.DrawText(g, label, labelFont, tickRight, color, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);

                            //g.DrawString(label, labelFont, labelBrush, tickRight, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far });
                            //TextRenderer.DrawText(g, label, labelFont, tickRight, color, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                        }
                    }

                    g.DrawLine(tickLinePen, tickLeft, tickRight);


                    //g.DrawLine(tickLinePen, tickLeft, tickRight);
                    //e
                    //Rectangle rect = new Rectangle(0, i * (lightHeight + lightSpacing), imageWith, lightHeight);
                    //g.FillRectangle(brush, rect);
                }
            }

            return bitmap;
        }


        public static Image CreateTest(Rectangle rect, int radius)
        {
            //int imageWith = levelMeterImageModel.lightWidth;
            //int imageHeight = (levelMeterImageModel.lightHeight + levelMeterImageModel.lightSpacing) * levelMeterImageModel.lightCount;

            Bitmap bitmap = new Bitmap(rect.Width, rect.Height);
            using (SolidBrush brush = new SolidBrush(Color.Black))
            using (Pen pen = new Pen(Color.White))
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                //Rectangle rect = new Rectangle(0, levelMeterImageModel.firstLightYOffset + i * (levelMeterImageModel.lightHeight + levelMeterImageModel.lightSpacing), imageWith, levelMeterImageModel.lightHeight);
                GraphicsPath path = Utility.RoundRectangle.CreateRoundedRectPath(rect, radius);

                UI.Utility.GDIHelper.SetHighQuality(g);

                g.FillPath(brush, path);
                g.DrawPath(pen, path);

                UI.Utility.GDIHelper.SetDefaultQuality(g);

                path.Dispose();
            }

            return bitmap;
        }

        public static bool IntArrayContains(int[] arr, int v)
        {
            System.Diagnostics.Debug.Assert(arr != null);
            foreach (int i in arr)
            {
                if (i == v)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
