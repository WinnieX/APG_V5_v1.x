using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Core
{
    public class ToneControlBoostConverter
    {
        public readonly ShortParam param;

        public ToneControlBoostConverter(ShortParam param_)
        {
            this.param = param_;
        }

        public double boost
        {
            get
            {
                return -this.param.v;
            }
        }
    }
}
