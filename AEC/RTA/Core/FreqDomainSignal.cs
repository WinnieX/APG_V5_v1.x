using System;
using System.Collections.Generic;
using System.Text;
using AEC.RTA.FFTW;

namespace AEC.RTA.Core
{
    public class FreqDomainSignal
    {
        public Complex[] signals = null;
        public FreqDomainSignal(int length)
        {
            signals = new Complex[length];
        }
    }
}
