using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Core.DSP
{
    public class DSPHelper
    {
        public enum sampleRate
        {
            sr48K,
            sr96K,
        };

        public enum EQMode
        {
            Classical,
            Professional,
        };

        public static sampleRate currSR = sampleRate.sr48K;
        public const double sr_48k = 48000;
        public const double sr_96k = 96000;

        private static IIRFilter iir48k = null;
        private static IIRFilter iir96k = null;
        private static IIRFilter currIIRFilter = null;

        static DSPHelper()
        {
            iir48k = new IIRFilter(sr_48k);
            iir96k = new IIRFilter(sr_96k);
            currIIRFilter = iir96k;
        }

        public static void SwitchSampleRate(sampleRate sr)
        {
            if (sr == sampleRate.sr48K)
            {
                currIIRFilter = iir48k;
            }
            else
            {
                currIIRFilter = iir96k;
            }
        }

        public static void SwitchEQMode(EQMode mode)
        {
            IIRFilter.eqMode = mode;
        }

        public static void UpdateEQFreqResp(EQ eq)
        {
            Debug.Assert(eq != null);

            eq.CheckFreqResp();

            Debug.Assert(eq.freqResp != null);

            currIIRFilter.UpdateEQFreqResp(eq);
        }

        public static void UpdateToneControlFreqResp(ToneControlFilter filter)
        {
            Debug.Assert(filter != null);

            filter.CheckFreqResp();

            Debug.Assert(filter.freqResp != null);

            currIIRFilter.UpdateToneControlFilterFreqResp(filter);
        }

        public static void UpdateXPFilterFreqResp(XPFilterBase filter)
        {
            Debug.Assert(filter != null);

            filter.CheckFreqResp();

            Debug.Assert(filter.freqResp != null);

            currIIRFilter.UpdateXPFilterFreqResp(filter);
        }

        // 20 / ln( 10 )
        private const double LOG_2_DB = 8.6858896380650365530225783783321;

        // ln( 10 ) / 20
        private const double DB_2_LOG = 0.11512925464970228420089957273422;

        private const double dBLowerLimiter = -1024;

        public static double LinearToDecibels(double line)
        {
            if(line > 0)
            {
                return Math.Log(line) * LOG_2_DB;
            }
            else
            {
                return dBLowerLimiter;
            }
            
        }

        public static double DecibelsToLinear(double dB)
        {
            return Math.Exp(dB * DB_2_LOG);
        }
    }
}
