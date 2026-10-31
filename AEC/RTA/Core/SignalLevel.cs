using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RTA.Core
{
    class SignalLevel
    {
        private static readonly float ZerosLevel_16Bit = 1 / (float)32768;
        private static readonly float ZerosLevel_24Bit = -1 / (float)8388608;
        public static readonly float MinLevel_16Bit = 20 * (float)Math.Log10(ZerosLevel_16Bit);
        public static readonly float MinLevel_24Bit = 20 * (float)Math.Log10(ZerosLevel_24Bit);

        public static double CalcdBFS16Bit(double[] data)
        {
            double peak = GetPeakValue(data);
            if (peak == 0)
            {
                return MinLevel_16Bit;
            }

            return 20 * Math.Log10(peak);
        }

        public static double CalcdBFS24Bit(double[] data)
        {
            double peak = GetPeakValue(data);
            if (peak == 0)
            {
                return MinLevel_24Bit;
            }

            return 20 * Math.Log10(peak);
        }

        private static double GetPeakValue(double[] data)
        {
            double max = Math.Abs(data[0]);
            for (int i = 1; i < data.Length; ++i)
            {
                double abs = Math.Abs(data[i]);
                if (max < abs)
                {
                    max = abs;
                }
            }

            return max;
        }
    }
}
