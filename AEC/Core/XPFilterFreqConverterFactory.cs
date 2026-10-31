using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Core
{
    public class XPFilterFreqConverterFactory
    {
        public static XPFilterFreqConverter CreateHighPassFilterFreqConverter()
        {
            return new XPFilterFreqConverter(0, (short)(DataUtils.maxFilterFreqIndex - 1), 0);
        }

        public static XPFilterFreqConverter CreateHighPassFilterFreqConverter(double freqUpperLimit)
        {
            return new XPFilterFreqConverter(0, DataUtils.Freq2FilterIndex(freqUpperLimit), 0);
        }

        public static XPFilterFreqConverter CreateHighPassFilterFreqConverter(double freqUpperLimit, double freqInit)
        {
            Debug.Assert(freqUpperLimit >= freqInit);

            return new XPFilterFreqConverter(0, DataUtils.Freq2FilterIndex(freqUpperLimit), DataUtils.Freq2FilterIndex(freqInit));
        }

        public static XPFilterFreqConverter CreateHighPassFilterFreqConverter(double freqLowerLimit, double freqUpperLimit, double freqInit)
        {
            Debug.Assert(freqUpperLimit >= freqLowerLimit);

            return new XPFilterFreqConverter(DataUtils.Freq2FilterIndex(freqLowerLimit), DataUtils.Freq2FilterIndex(freqUpperLimit), DataUtils.Freq2FilterIndex(freqInit));
        }

        public static XPFilterFreqConverter CreateLowPassFilterFreqConverter()
        {
            return new XPFilterFreqConverter(1, DataUtils.maxFilterFreqIndex, DataUtils.maxFilterFreqIndex);
        }

        public static XPFilterFreqConverter CreateLowPassFilterFreqConverter(double freqLowerLimit)
        {
            return new XPFilterFreqConverter(DataUtils.Freq2FilterIndex(freqLowerLimit), DataUtils.maxFilterFreqIndex, DataUtils.maxFilterFreqIndex);
        }

        public static XPFilterFreqConverter CreateX5PassFilterFreqConverter(double freqLowerLimit, double freqUpperLimit)
        {
            return new XPFilterFreqConverter(DataUtils.Freq2FilterIndex(freqLowerLimit), DataUtils.Freq2FilterIndex(freqUpperLimit), DataUtils.Freq2FilterIndex(freqUpperLimit));
        }

        public static XPFilterFreqConverter CreateX5LowPassFilterFreqConverter(double freqUpperLimit)
        {
            return new XPFilterFreqConverter(1, DataUtils.Freq2FilterIndex(freqUpperLimit), DataUtils.Freq2FilterIndex(freqUpperLimit));
        }

        public static XPFilterFreqConverter CreateLowPassFilterFreqConverter(double freqLowerLimit, double freqInit)
        {
            return new XPFilterFreqConverter(DataUtils.Freq2FilterIndex(freqLowerLimit), DataUtils.maxFilterFreqIndex, DataUtils.Freq2FilterIndex(freqInit));
        }
    }
}
