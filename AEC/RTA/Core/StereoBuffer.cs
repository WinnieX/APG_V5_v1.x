using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RTA.Core
{
    public class StereoBuffer
    {
        public int length { get; private set; }
        public double[] left { get; private set; }
        public double[] right { get; private set; }

        public StereoBuffer(int bufferLength)
        {
            System.Diagnostics.Debug.Assert(bufferLength > 0);
            length = bufferLength;
            left = new double[length];
            right = new double[length];
        }

        public void CopyFrom(StereoBuffer other)
        {
            System.Diagnostics.Debug.Assert(this.length <= other.length);

            Array.Copy(other.left, this.left, length);
            Array.Copy(other.right, this.right, length);
        }
    }
}
