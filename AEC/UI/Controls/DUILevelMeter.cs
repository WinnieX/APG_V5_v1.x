using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using AEC.UI.Controls;
using AEC.UI.LevelMeter;

namespace AEC.UI.Controls
{
    public class DUILevelMeter : DUIControlBase
    {
        public enum Direction
        {
            UpwardPositive,     //向上为正
            DownwardPositive,   //向下为正
        };

        private bool _isOn = false;
        //public Point imageLeftTop { get; set; }
        public Image levelBackground { get; set; }
        public Image levelForeground { get; set; }
        public LevelMeterLEDLightImageModel ledLightImageModel { get; set; }

        private readonly int maxLevel;
        private readonly int minLevel;
        private readonly Direction direction;

        //public int range { get; set; }        //范围，代表led的数量，默认是-50dB ~ 0dB.

        public int currLevel { get; set; }      //当前电平
        public int peakLevel { get; set; }      //峰值电平

        public bool isShowPeak { get; set; }    //是否显示峰值

        public bool isOn                        //是否打开
        { 
            get
            {
                return this._isOn;
            }
            set
            {
                if(this._isOn != value)
                {
                    this._isOn = value;

                    if(!this._isOn)
                    {
                        this.currLevel = this.minLevel;
                        this.peakLevel = this.minLevel;
                    }
                }
            }
        }

        public DUILevelMeter(int minValue, int maxValue, Direction direction)
        {
            this.minLevel = minValue;
            this.maxLevel = maxValue;
            this.direction = direction;
            this.currLevel = this.minLevel; //+ KXLevelMeterImageProvider.levelMeterLEDLightImageModel.lightCount;
            this.peakLevel = this.minLevel;
        }

        public override void OnPaint(Graphics g)
        {
            if (this.ledLightImageModel == null)
            {
                return;
            }

            g.TranslateTransform(this.Location.X, this.Location.Y);

            Point backgroundTopLeft = new Point(0, this.ledLightImageModel.firstLightYOffset);

            if(this.levelBackground != null)
            {
                Rectangle destRect = new Rectangle(backgroundTopLeft, this.levelBackground.Size);
                Rectangle srcRect = new Rectangle(Point.Empty, this.levelBackground.Size);
                g.DrawImage(this.levelBackground, destRect, srcRect, GraphicsUnit.Pixel);
            }

            if (this.levelForeground != null && this._isOn)
            {
                if(this.direction == Direction.UpwardPositive)
                {
                    int lightOffCount = this.maxLevel - Core.Utils.ConstrainValue(this.currLevel, this.minLevel, this.maxLevel);    //不点亮灯的数量
                    if (lightOffCount < this.ledLightImageModel.lightCount)
                    {
                        int offsetYFromCurrToTop = lightOffCount * this.ledLightImageModel.lightHeightCapacity + backgroundTopLeft.Y;

                        Point topLeft = new Point(backgroundTopLeft.X, offsetYFromCurrToTop);

                        Rectangle destRect = new Rectangle(topLeft, new Size(this.levelForeground.Width, this.levelForeground.Height - offsetYFromCurrToTop));
                        Rectangle srcRect = new Rectangle(new Point(0, offsetYFromCurrToTop), destRect.Size);
                        g.DrawImage(this.levelForeground, destRect, srcRect, GraphicsUnit.Pixel);
                    }

                    if (this.isShowPeak && this.peakLevel > this.currLevel && this.peakLevel > this.minLevel)
                    {
                        int lightPeakToTopCount = this.maxLevel - Core.Utils.ConstrainValue(this.peakLevel, this.minLevel, this.maxLevel);    //不点亮灯的数量
                        if (lightPeakToTopCount < this.ledLightImageModel.lightCount)
                        {
                            int offsetYFromPeakToTop = lightPeakToTopCount * this.ledLightImageModel.lightHeightCapacity + backgroundTopLeft.Y;

                            Point topLeft = new Point(backgroundTopLeft.X, offsetYFromPeakToTop);

                            Rectangle peakDestRect = new Rectangle(topLeft, this.ledLightImageModel.lightSize);
                            Rectangle peakSrcRect = new Rectangle(new Point(0, offsetYFromPeakToTop), this.ledLightImageModel.lightSize);
                            g.DrawImage(this.levelForeground, peakDestRect, peakSrcRect, GraphicsUnit.Pixel);
                        }
                    }
                }
                else if(direction == Direction.DownwardPositive)
                {
                    int lightOnCount = Core.Utils.ConstrainValue(this.currLevel, this.minLevel, this.maxLevel) - this.minLevel;    //不点亮灯的数量
                    if (lightOnCount > 0)
                    {
                        int bottomYFromCurrToTop = lightOnCount * this.ledLightImageModel.lightHeightCapacity + backgroundTopLeft.Y;

                        Rectangle destRect = new Rectangle(backgroundTopLeft, new Size(this.levelForeground.Width, bottomYFromCurrToTop));
                        Rectangle srcRect = new Rectangle(new Point(0, 0), destRect.Size);
                        
                        g.DrawImage(this.levelForeground, destRect, srcRect, GraphicsUnit.Pixel);
                    }

                    if (this.isShowPeak && this.peakLevel > this.currLevel && this.peakLevel > this.minLevel)
                    {
                        int lightPeakIndex = this.peakLevel - this.minLevel - 1;

                        int srcLightPeakY = lightPeakIndex * this.ledLightImageModel.lightHeightCapacity;
                        int destLightPeakY = srcLightPeakY + backgroundTopLeft.Y;

                        Point topLeft = new Point(backgroundTopLeft.X, destLightPeakY);

                        Rectangle peakDestRect = new Rectangle(topLeft, this.ledLightImageModel.lightSize);
                        Rectangle peakSrcRect = new Rectangle(new Point(0, srcLightPeakY), this.ledLightImageModel.lightSize);
                        
                        g.DrawImage(this.levelForeground, peakDestRect, peakSrcRect, GraphicsUnit.Pixel);
                    }
                }
            }

            g.ResetTransform();
        }
    }
}
