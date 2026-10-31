using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RTA.Core
{
    public class OutputSineSignalFreq
    {
        public const short FreqLowerLimit = 20;
        public const short FreqUpperLimit = 20000;
        public const short DefFreq = 1000;
        public const short MaxSineFreq = FreqUpperLimit - FreqLowerLimit;

        private const string FreqUnit = "Hz";

        private short _freq = DefFreq;

        public short freq
        {
            get
            {
                return _freq;
            }
            set
            {
                _freq = Math.Min(Math.Max(value, FreqLowerLimit), (short)FreqUpperLimit);
            }
        }

        public short freqIndex
        {
            get
            {
                return (short)(_freq - FreqLowerLimit);
            }
            set
            {
                freq = (short)(value + FreqLowerLimit);
            }
        }

        public static string GetLabel(short index)
        {
            return string.Format("{0} {1}", index + FreqLowerLimit, FreqUnit);
        }

        public static short CalcFreqIndex(string text)
        {
            string str = AEC.Core.Utils.RemoveStringSuffix(text, FreqUnit);

            short value;
            if (short.TryParse(str, out value))
            {
                short freq = Math.Min(Math.Max(value, FreqLowerLimit), (short)FreqUpperLimit);

                return (short)(freq - FreqLowerLimit);
            }

            return -1;
        }
    }
}
