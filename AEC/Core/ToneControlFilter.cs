using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AEC.Core.DSP;

namespace AEC.Core
{
    public class ToneControlFilter : MFilterBase
    {
        public readonly double freqBassCutoff;
        public readonly ToneControlBoostConverter boostBassConverter;
        public readonly double freqTrebleCutoff;
        public readonly ToneControlBoostConverter boostTrebleConverter;

        public ToneControlFilter(double freqBassCutoff_, ToneControlBoostConverter boostBassConverter_,
                                double freqTrebleCutoff_, ToneControlBoostConverter boostTrebleConverter_)
        {
            this.freqBassCutoff = freqBassCutoff_;
            this.boostBassConverter = boostBassConverter_;
            this.freqTrebleCutoff = freqTrebleCutoff_;
            this.boostTrebleConverter = boostTrebleConverter_;
        }

        public double boostBass
        {
            get
            {
                return this.boostBassConverter.boost;
            }
        }

        public double boostTreble
        {
            get
            {
                return this.boostTrebleConverter.boost;
            }
        }

        public override bool freqRespIsZero
        {
            get
            {
                return !base.visible || (this.boostBassConverter.boost == 0 && this.boostTrebleConverter.boost == 0);
            }
        }

        public void UpdateFreqResp()
        {
            DSPHelper.UpdateToneControlFreqResp(this);
        }
    }
}
