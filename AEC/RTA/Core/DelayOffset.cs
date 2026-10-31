using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RTA.Core
{
    public class DelayOffset
    {
        private int maxOffset = AudioRTAParam.FFTSize32K / 2;
        private int _offset = 0;

        public int offset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset = Math.Min(Math.Max(value, 0), maxOffset);
            }
        }

        public DelayOffset(int offset_)
        {
            offset = offset_;
        }
    }
}
