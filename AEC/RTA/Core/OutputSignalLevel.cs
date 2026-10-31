using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RTA.Core
{
    public class OutputSignalLevel
    {
        public const short OutputLevelLowerLimit = -96;
        public const short DefOutputLevel = -36;
        public const short MaxOutputLevel = -OutputLevelLowerLimit;
        private const string LevelUnit = "dB";

        private short _level = DefOutputLevel;

        public short level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = Math.Min(Math.Max(value, OutputLevelLowerLimit), (short)0);
            }
        }

        public short levelIndex
        {
            get
            {
                return (short)(_level - OutputLevelLowerLimit);
            }
            set
            {
                level = (short)(value + OutputLevelLowerLimit);
            }
        }

        public static string GetLabel(short index)
        {
            return string.Format("{0} {1}", index + OutputLevelLowerLimit, LevelUnit);
        }

        public static short CalcLevelIndex(string text)
        {
            string str = AEC.Core.Utils.RemoveStringSuffix(text, LevelUnit);

            short value;
            if (short.TryParse(str, out value))
            {
                short level = Math.Min(Math.Max(value, OutputLevelLowerLimit), (short)0);

                return (short)(level - OutputLevelLowerLimit);
            }

            return -1;
        }
    }
}
