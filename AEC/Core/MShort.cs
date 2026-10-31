using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Core
{
    public class MShort
    {
        public readonly short max;
        private short _value;

        public MShort(short max, short value)
        {
            System.Diagnostics.Debug.Assert(max >= 0 && max >= value);

            this.max = max;
            this._value = value;
        }

        public short v
        {
            get
            {
                return _value;
            }
            set
            {
                this._value = Utils.ConstrainMax(value, this.max);
            }
        }
    }
}
