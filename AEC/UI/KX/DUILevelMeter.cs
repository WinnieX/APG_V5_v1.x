using System;
using System.Collections.Generic;
using System.Text;
using AEC.UI.Controls;
using AEC.UI.LevelMeter;

namespace AEC.UI.KX
{
    public class DUIMaxLevelMeter : DUILevelMeter
    {
        public DUIMaxLevelMeter(int maxLevel = 0)       //默认是0dB
            : base(maxLevel - KXMonitor.LevelRange, maxLevel, Direction.UpwardPositive)
        {
            base.isShowPeak = true;
        }
    }

    public class DUILevelMeter0_12dB : DUILevelMeter
    {
        public DUILevelMeter0_12dB()
            : base(KXMonitor.DynamicValueLowerLimit, KXMonitor.DynamicValueUpperLimit, Direction.DownwardPositive)
        {
            base.isShowPeak = true;
        }
    }
}
