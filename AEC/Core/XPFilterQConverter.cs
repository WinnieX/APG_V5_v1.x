using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Core
{
    public class XPFilterQConverter : XPFilterConverterBase
    {
        public readonly MShort qFactor;

        public XPFilterQConverter(
            short qFactorLowerLimitIndex_,
            short qFactorUpperLimitIndex_,
            short qFactorInitIndex_) :
            base(DataUtils.maxFilterQIndex, qFactorLowerLimitIndex_, qFactorUpperLimitIndex_, qFactorInitIndex_)
        {
            this.qFactor = base.param;
        }

        public short filterQFactorIndex
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

        public short qFactorLowerLimitIndex
        {
            get
            {
                return base.paramLowerLimitIndex;
            }
        }

        public short qFactorUpperLimitIndex
        {
            get
            {
                return base.paramUpperLimitIndex;
            }
        }
    }
}
