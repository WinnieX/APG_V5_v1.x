using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Core
{
    public class HighPassFilter : XPFilterBase
    {
        public HighPassFilter(
            XPFilterFreqConverter freqConverter_,
            XPFilterTypeConverter typeConverter_,
            XPFilterQConverter qConverter_ = null)
            : base(XPFilterType.HighPass, freqConverter_, typeConverter_, qConverter_)
        {

        }

        //public HighPassFilter(double freqUpperLimit)
        //    : base(
        //    XPFilterType.HighPass,
        //    XPFilterFreqConverterFactory.CreateHighPassFilterFreqConverter(freqUpperLimit),
        //    XPFilterTypeConverterFactory.CreateImmutableFilterTypeConverter(FilterType.Butterworth12dB))
        //{

        //}
    }
}
