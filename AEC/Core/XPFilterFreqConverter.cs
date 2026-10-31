using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Core
{
    public class XPFilterFreqConverter : XPFilterConverterBase
    {
        public readonly MShort freq;

        public XPFilterFreqConverter(
            short freqLowerLimitIndex_,
            short freqUpperLimitIndex_,
            short freqInitIndex_) :
            base(DataUtils.maxFilterFreqIndex, freqLowerLimitIndex_, freqUpperLimitIndex_, freqInitIndex_)
        {
            this.freq = base.param;
        }

        public short filterFreqIndex
        {
            get
            {
                return base.paramIndex;
            }
            set
            {
                base.paramIndex = value;
            }
        }

        public short freqLowerLimitIndex
        {
            get
            {
                return base.paramLowerLimitIndex;
            }
        }

        public short freqUpperLimitIndex
        {
            get
            {
                return base.paramUpperLimitIndex;
            }
        }
    }

    //public class HighPassFilterFreqConverter : XPFilterFreqConverter
    //{
    //    //上限频率至freqUpperLimit
    //    public HighPassFilterFreqConverter(double freqUpperLimit):
    //        base(0, DataUtils.Freq2FilterIndex(freqUpperLimit), 0)
    //    {

    //    }

    //    //上限频率至freqUpperLimit
    //    public HighPassFilterFreqConverter() :
    //        base(0, (short)(DataUtils.maxFilterFreqIndex - 1), 0)
    //    {

    //    }
    //}

    //public class LowPassFilterFreqConverter : XPFilterFreqConverter
    //{
    //    //下限频率至freqUpperLimit
    //    public LowPassFilterFreqConverter(double freqLowerLimit) :
    //        base(DataUtils.Freq2FilterIndex(freqLowerLimit), DataUtils.maxFilterFreqIndex, DataUtils.maxFilterFreqIndex)
    //    {

    //    }

    //    //下限频率至freqUpperLimit
    //    public LowPassFilterFreqConverter() :
    //        base(1, DataUtils.maxFilterFreqIndex, DataUtils.maxFilterFreqIndex)
    //    {

    //    }
    //}
}
