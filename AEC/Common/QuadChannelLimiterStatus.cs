using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Common
{
    [Serializable()]
    public class QuadChannelLimiterStatus
    {
        public const int Quad = 4;
        private readonly LimiterStatus[] limiterStatus = new LimiterStatus[Quad];

        public QuadChannelLimiterStatus()
        {
            for (int i = 0; i < Quad; ++i)
            {
                limiterStatus[i] = new LimiterStatus();
            }
        }

        public LimiterStatus this[int index]
        {
            get
            {
                System.Diagnostics.Debug.Assert(index >= 0 && index < Quad);
                return limiterStatus[index];
            }
        }
    }
}
