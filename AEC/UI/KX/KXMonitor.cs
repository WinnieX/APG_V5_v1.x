using System;
using System.Collections.Generic;
using System.Text;
using AEC.Common;

namespace AEC.UI.KX
{
    public class KXMonitor
    {
        public const int LevelRange = 48;       //总共有48个LED灯，不能随意修改
        public const int LevelUpperLimit = 0;
        public const int LevelLowerLimit = LevelUpperLimit - LevelRange;
        
        public const int DynamicValueUpperLimit = 12;
        public const int DynamicValueLowerLimit = 0;
        public const int DynamicValueRange = DynamicValueUpperLimit - DynamicValueLowerLimit;

        public const int LevelHoldTimeInMs = 256; //电平保持时间
        public const int LevelPeakHoldTimeInMs = 3 * LevelHoldTimeInMs; //峰值电平保持时间

        public const int DynamicValueHoldTimeInMs = 32; //压限保持时间
        public const int DynamicValueFallAcceleratedSpeed = 64;


        public static PeakValueHolder CreateLevelHolder()
        {
            return new PeakValueHolder(KXMonitor.LevelLowerLimit, KXMonitor.LevelUpperLimit, KXMonitor.LevelHoldTimeInMs); //做通道电平保持
        }

        public static PeakValueHolder CreatePeakLevelHolder()
        {
            return new PeakValueHolder(KXMonitor.LevelLowerLimit, KXMonitor.LevelUpperLimit, KXMonitor.LevelPeakHoldTimeInMs); //做通道峰值电平保持
        }

        //第一个为电平保持，第二哥为峰值保持
        public static PeakValueHolder[] CreateLevelHolderPair()
        {
            return new PeakValueHolder[]
            {
                CreateLevelHolder(),
                CreatePeakLevelHolder(),
            };
        }

        public static PeakValueHolder CreateDynamicValueHolder()
        {
            return new PeakValueHolder(KXMonitor.DynamicValueLowerLimit, KXMonitor.DynamicValueUpperLimit, KXMonitor.DynamicValueHoldTimeInMs, KXMonitor.DynamicValueFallAcceleratedSpeed); //做压限电平保持
        }
    }
}
