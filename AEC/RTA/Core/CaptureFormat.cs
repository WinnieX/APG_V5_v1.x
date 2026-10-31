using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RTA.Core
{
    public class CaptureFormat
    {
        public int sampleRate { get; private set; }
        public short bitsPerSample { get; private set; }

        public CaptureFormat(int sampleRate_ = AudioRTAParam.SampleRate48k, short bitsPerSample_ = AudioRTAParam.BitsPerSample16)
        {
            sampleRate = sampleRate_;
            bitsPerSample = bitsPerSample_;
        }
    }
}
