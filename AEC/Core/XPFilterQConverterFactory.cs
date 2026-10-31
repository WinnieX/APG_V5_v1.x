using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Core
{
    public class XPFilterQConverterFactory
    {
        public const short DefFilterQIndex = 10;

        public static XPFilterQConverter CreateDefaultFilterQConverter()
        {
            return new XPFilterQConverter(0, DataUtils.maxFilterQIndex, DefFilterQIndex);
        }

        public static XPFilterQConverter CreateFilterQConverter(double qLowerLimit, double qUpperLimit, double qDefault)
        {
            Debug.Assert(
                qLowerLimit <= qUpperLimit &&
                qLowerLimit <= qDefault &&
                qUpperLimit >= qDefault
                );

            short qLowerLimitIndex = DataUtils.FilterQ2FilterIndex(qLowerLimit);
            short qUpperLimitIndex = DataUtils.FilterQ2FilterIndex(qUpperLimit);
            short qInitIndex = DataUtils.FilterQ2FilterIndex(qDefault);

            return new XPFilterQConverter(qLowerLimitIndex, qUpperLimitIndex, qInitIndex);
        }
    }
}
