using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Core
{
    public class LowPassFilter : XPFilterBase
    {
        public LowPassFilter(
            XPFilterFreqConverter freqConverter_,
            XPFilterTypeConverter typeConverter_,
            XPFilterQConverter qConverter_ = null)
            : base(XPFilterType.LowPass, freqConverter_, typeConverter_, qConverter_)
        {

        }

        //public LowPassFilter(double freqLowerLimit)
        //    : base(
        //    XPFilterType.LowPass,
        //    XPFilterFreqConverterFactory.CreateLowPassFilterFreqConverter(freqLowerLimit),
        //    XPFilterTypeConverterFactory.CreateImmutableFilterTypeConverter(FilterType.Butterworth12dB))
        //{
            
        //}
    }
}
