using System;
using System.Collections.Generic;
using System.Text;
using AEC.RTA.FFTW;

namespace AEC.RTA.Core
{
    public class StereoFFTResult
    {
        public Complex[] left;
        public Complex[] right;

        public StereoFFTResult(int length)
        {
            System.Diagnostics.Debug.Assert(length > 0);

            left = new Complex[length];
            right = new Complex[length];
        }

        public void Clear()
        {
            Array.Clear(left, 0, left.Length);
            Array.Clear(right, 0, right.Length);
        }
    }
}
