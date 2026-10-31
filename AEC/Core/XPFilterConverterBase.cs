using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Core
{
    public abstract class XPFilterConverterBase
    {
        public readonly short paramMaxIndex;
        public readonly short paramLowerLimitIndex;
        public readonly short paramUpperLimitIndex;
        public readonly short paramInitIndex;
        public readonly MShort param;

        public XPFilterConverterBase(
            short paramMaxIndex,
            short paramLowerLimitIndex_,
            short paramUpperLimitIndex_,
            short paramInitIndex_)
        {
            Debug.Assert(
                paramMaxIndex >= 0 &&
                paramLowerLimitIndex_ >= 0 &&
                paramLowerLimitIndex_ <= paramMaxIndex &&
                paramUpperLimitIndex_ >= paramLowerLimitIndex_ &&
                paramUpperLimitIndex_ <= paramMaxIndex &&
                paramInitIndex_ >= paramLowerLimitIndex_ &&
                paramInitIndex_ <= paramUpperLimitIndex_
                );

            this.paramLowerLimitIndex = paramLowerLimitIndex_;
            this.paramUpperLimitIndex = paramUpperLimitIndex_;
            this.paramInitIndex = paramInitIndex_;
            this.param = new MShort((short)(this.paramUpperLimitIndex - this.paramLowerLimitIndex), (short)(this.paramInitIndex - this.paramLowerLimitIndex));
        }

        public short index
        {
            get
            {
                return this.param.v;
            }
            set
            {
                this.param.v = value;
            }
        }

        public short max
        {
            get
            {
                return this.param.max;
            }
        }

        public short paramIndex
        {
            get
            {
                return Utils.ConstrainMax((short)(this.param.v + this.paramLowerLimitIndex), this.paramUpperLimitIndex);
            }
            set
            {
                int index = Utils.ConstrainValue(value, this.paramLowerLimitIndex, this.paramUpperLimitIndex);
                this.param.v = (short)(index - this.paramLowerLimitIndex);
            }
        }

        //是否是不可变的，如果上限和下限
        public bool isImmutable
        {
            get
            {
                return this.paramLowerLimitIndex == this.paramUpperLimitIndex;
            }
        }

        public void Reset()
        {
            this.param.v = (short)(this.paramInitIndex - this.paramLowerLimitIndex);
        }

        public void Increase(bool circular = false)
        {
            if(isImmutable)
            {
                return;
            }

            if(this.param.v < this.param.max)
            {
                this.param.v++;
            }
            else if(circular)
            {
                this.param.v = 0;
            }
        }

        public void Decrease(bool circular = false)
        {
            if (isImmutable)
            {
                return;
            }

            if (this.param.v > 0)
            {
                this.param.v--;
            }
            else if (circular)
            {
                this.param.v = this.param.max;
            }
        }
    }
}
