using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RTA.Core
{
    //幅度
    public class SignalMag
    {
        public double[] mags = null;

        //public SignalMag2(double[] signalMag2s)
        //{
        //    mag2s = new double[signalMag2s.Length];
        //    Buffer.BlockCopy(signalMag2s, 0, mag2s, 0, signalMag2s.Length * sizeof(double));
        //}

        public SignalMag(int length)
        {
            mags = new double[length];
        }
    }
}
