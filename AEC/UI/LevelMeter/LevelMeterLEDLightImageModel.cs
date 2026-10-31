using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AEC.UI.LevelMeter
{
    public class LevelMeterLEDLightImageModel
    {
        public int lightWidth { get; set; }
        public int lightHeight { get; set; }
        public int lightCount { get; set; }
        public int lightSpacing { get; set; }
        public int firstLightYOffset { get; set; }

        //每个led所占高度=灯的高度 + 灯的间距
        public int lightHeightCapacity
        {
            get
            {
                return this.lightHeight + this.lightSpacing;
            }
        }

        public int totalHeight
        {
            get
            {
                return this.firstLightYOffset + (this.lightHeight + this.lightSpacing) * this.lightCount;
            }
        }


        public Size lightSize
        {
            get
            {
                return new Size(this.lightWidth, this.lightHeight);
            }
        }

        public Rectangle GetRect(int index)
        {
            System.Diagnostics.Debug.Assert(index >= 0 && index < lightCount);

            return new Rectangle(0, this.firstLightYOffset + index * (this.lightHeight + this.lightSpacing), lightWidth, this.lightHeight);
        }
    }
}
