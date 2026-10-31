using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RTA.Core
{
    public enum RTASampleRate
    {
        sr48k = 0,
        sr96k,
        sr192k,
    }

    public enum RTASampleBitDepth
    {
        bd16Bit = 0,
        bd24Bit,
        //bd32Bit,
    }
}
