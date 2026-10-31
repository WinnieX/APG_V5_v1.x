using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.UI.LevelMeter
{
    public class LevelMeterTicksModelBase
    {
        public LevelMeterTickStyle tickStyle { get; set; }
        public int width { get; set; }
        public int tickSpacing { get; set; }
        public int firstTickOffset { get; set; }
        public int smallTickWidth { get; set; }
        public int largeTickWidth { get; set; }
    }

    public class LevelMeterTicksModel : LevelMeterTicksModelBase
    {
        public int[] tickList { get; set; }
    }

    public class DynamicMeterTicksModel : LevelMeterTicksModelBase
    {
        public int maxLevel { get; set; }
        public int tickCount { get; set; }        
        public int largeTickStep { get; set; }
    }
}
