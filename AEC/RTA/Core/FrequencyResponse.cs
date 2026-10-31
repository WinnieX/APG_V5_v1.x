using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RTA.Core
{
    [Serializable]
    public class FrequencyResponse
    {
        public int length { get; private set; }
        public double[] amp { get; private set; }
        public double[] phase { get; private set; }

        public FrequencyResponse(int bufferLength)
        {
            System.Diagnostics.Debug.Assert(bufferLength > 0);
            length = bufferLength;
            amp = new double[length];
            phase = new double[length];
        }

        public void CopyFrom(FrequencyResponse other)
        {
            System.Diagnostics.Debug.Assert(this.length <= other.length);

            Array.Copy(other.amp, this.amp, length);
            Array.Copy(other.phase, this.phase, length);
        }
    }
}
