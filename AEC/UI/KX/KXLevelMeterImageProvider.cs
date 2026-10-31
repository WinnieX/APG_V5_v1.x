using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.UI.LevelMeter;
using AEC.Effectors;

namespace AEC.UI.KX
{
    public class KXLevelMeterImageProvider
    {
        public const int YOffsetFromTickToLed = 5;
        public static LevelMeterLEDLightImageModel levelMeterLEDLightImageModel = new LevelMeterLEDLightImageModel() { lightWidth = 16, lightCount = KXMonitor.LevelRange, lightHeight = 7, lightSpacing = 1, firstLightYOffset = 0 };
        public static LevelMeterLEDLightImageModel limiterLEDLightImageModel = new LevelMeterLEDLightImageModel() { lightWidth = 16, lightCount = KXMonitor.DynamicValueRange, lightHeight = 7, lightSpacing = 1, firstLightYOffset = 0 };

        public static readonly Dictionary<int, int[]> levelTicksMap = new Dictionary<int, int[]>()
        {
            {(int)MaxLevel.MaxLevel_14dBu, new int[] { -34, -30, -24, -18, -12, -6, 0, 6, (int)MaxLevel.MaxLevel_14dBu }},
            {(int)MaxLevel.MaxLevel_17dBu, new int[] { -31, -24, -18, -12, -6, 0, 6, 12, (int)MaxLevel.MaxLevel_17dBu }},
            {(int)MaxLevel.MaxLevel_21dBu, new int[] { -27, -24, -18, -12, -6, 0, 6, 12, 18, (int)MaxLevel.MaxLevel_21dBu }},
        };

        public static  readonly LevelMeterTicksModel _levelMeterTicksModel = new LevelMeterTicksModel() {width = 40, tickSpacing = 8, firstTickOffset = 5, smallTickWidth = 4, largeTickWidth = 8, };

        public static DynamicMeterTicksModel levelMeterLeftTicksModel = new DynamicMeterTicksModel() { tickStyle = LevelMeterTickStyle.Left, width = 30, maxLevel = KXMonitor.LevelUpperLimit, tickCount = KXMonitor.LevelRange, largeTickStep = 6, tickSpacing = 8, firstTickOffset = 5, smallTickWidth = 4, largeTickWidth = 8, };
        public static DynamicMeterTicksModel levelMeterRightTicksModel = new DynamicMeterTicksModel() { tickStyle = LevelMeterTickStyle.Right, width = 30, maxLevel = KXMonitor.LevelUpperLimit, tickCount = KXMonitor.LevelRange, largeTickStep = 6, tickSpacing = 8, firstTickOffset = 5, smallTickWidth = 4, largeTickWidth = 8, };
        public static DynamicMeterTicksModel limiterMeterRightTicksModel = new DynamicMeterTicksModel() { tickStyle = LevelMeterTickStyle.Right, width = 30, maxLevel = KXMonitor.DynamicValueUpperLimit, tickCount = KXMonitor.DynamicValueRange, largeTickStep = 6, tickSpacing = 8, firstTickOffset = 5, smallTickWidth = 4, largeTickWidth = 8, };

        private static Image _levelMeterBackground = null;
        private static Image _levelMeterForeground = null;
        private static Dictionary<int, Image> _levelMeterLeftTicksMap = new Dictionary<int, Image>();
        private static Dictionary<int, Image> _levelMeterRightTicksMap = new Dictionary<int, Image>();

        private static Image _limiterMeterBackground = null;
        private static Image _limiterMeterForeground = null;
        private static Image _limiterMeterRightTicks = null;

        public static Image GetLevelMeterBackground()
        {
            if (_levelMeterBackground == null)
            {
                _levelMeterBackground = LevelMeterImageFactory.CreateLevelMeterBackground(Color.Black, levelMeterLEDLightImageModel);
                //_levelMeterBackground.Save("20230520_back.png");
            }

            return _levelMeterBackground;
        }

        public static Image GetLevelMeterForeground()
        {
            if (_levelMeterForeground == null)
            {
                _levelMeterForeground = LevelMeterImageFactory.CreateLevelMeterForeground(levelMeterLEDLightImageModel);
                //_levelMeterForeground.Save("20230520_fore.png");
            }

            return _levelMeterForeground;
        }

        public static Image GetLevelMeterLeftTicks(int maxLevel = (int)MaxLevel.MaxLevel_14dBu)
        {
            if (!_levelMeterLeftTicksMap.ContainsKey(maxLevel))
            {
                _levelMeterTicksModel.tickStyle = LevelMeterTickStyle.Left;
                _levelMeterTicksModel.tickList = levelTicksMap.ContainsKey(maxLevel) ? levelTicksMap[maxLevel] : levelTicksMap[(int)MaxLevel.MaxLevel_14dBu];

                _levelMeterLeftTicksMap[maxLevel] = LevelMeterImageFactory.CreateLevelMeterTicks(Color.White, _levelMeterTicksModel);

                //_levelMeterLeftTicksMap[maxLevel].Save("20230520_left_ticks.png");
            }

            return _levelMeterLeftTicksMap[maxLevel];
        }

        public static Image GetLevelMeterRightTicks(int maxLevel = (int)MaxLevel.MaxLevel_14dBu)
        {
            if (!_levelMeterRightTicksMap.ContainsKey(maxLevel))
            {
                _levelMeterTicksModel.tickStyle = LevelMeterTickStyle.Right;
                _levelMeterTicksModel.tickList = levelTicksMap.ContainsKey(maxLevel) ? levelTicksMap[maxLevel] : levelTicksMap[(int)MaxLevel.MaxLevel_14dBu];

                _levelMeterRightTicksMap[maxLevel] = LevelMeterImageFactory.CreateLevelMeterTicks(Color.White, _levelMeterTicksModel);

                //_levelMeterRightTicksMap[maxLevel].Save("20230520_right_ticks.png");
            }

            return _levelMeterRightTicksMap[maxLevel];
        }
        
        public static Image GetLimiterMeterBackground()
        {
            if (_limiterMeterBackground == null)
            {
                _limiterMeterBackground = LevelMeterImageFactory.CreateLevelMeterBackground(Color.Black, limiterLEDLightImageModel);

                //_limiterMeterBackground.Save("20230414-limter_back.png");
            }

            return _limiterMeterBackground;
        }

        public static Image GetLimiterMeterForeground()
        {
            if (_limiterMeterForeground == null)
            {
                _limiterMeterForeground = LevelMeterImageFactory.CreateLimiterMeterForeground(limiterLEDLightImageModel);

                //_limiterMeterForeground.Save("20230414-limter_fore.png");
            }

            return _limiterMeterForeground;
        }

        public static Image GetLimiterMeterTicks()
        {
            if (_limiterMeterRightTicks == null)
            {
                _limiterMeterRightTicks = LevelMeterImageFactory.CreateLimiterMeterTicks(Color.White, limiterMeterRightTicksModel);

                //_limiterMeterRightTicks.Save("20230414-limter_ticks.png");
            }

            return _limiterMeterRightTicks;
        }
        
    }
}
