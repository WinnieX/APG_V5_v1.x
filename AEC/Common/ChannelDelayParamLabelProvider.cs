using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;

namespace AEC.Common
{
    public class ChannelDelayParamLabelProvider
    {
        public const string ChannelDelayUnitInMs = "ms";
        public const string ChannelDelayUnitInM = "m";


        //public readonly double sr;
        public readonly double delayStep;
        public readonly short maxDelayIndex;

        private static AEC.Misc.Config config = AEC.Misc.Config.GetInstance();

        public ChannelDelayParamLabelProvider(double delayStep_, double maxDelayTime)
        {
            //this.sr = sr_;
            this.delayStep = delayStep_;
            this.maxDelayIndex = (short)Math.Round(maxDelayTime / delayStep, 0, MidpointRounding.AwayFromZero);
        }

        public string GetDelayLabel(short index)
        {
            if (AEC.Misc.Config.GetInstance().channelDelayUnit == Misc.ChannelDelayUnit.InMilliseconds)
            {
                return string.Format("{0:0.00} {1}", index * delayStep, ChannelDelayUnitInMs);
            }
            else
            {
                double distance_In_m = index * delayStep * config.speedOfSound_In_m;
                return string.Format("{0:F3} {1}", distance_In_m, ChannelDelayUnitInM);
            }
        }

        public short CalcDelayIndexFromText(string text)
        {
            if (AEC.Misc.Config.GetInstance().channelDelayUnit == Misc.ChannelDelayUnit.InMilliseconds)
            {
                string str = Utils.RemoveStringSuffix(text, ChannelDelayUnitInMs);
                double value;
                if (double.TryParse(str, out value))
                {
                    short index = (short)Math.Round(value / delayStep, 0, MidpointRounding.AwayFromZero);
                    return Utils.ConstrainMax(index, this.maxDelayIndex);
                }

                return -1;
            }
            else
            {
                string str = Utils.RemoveStringSuffix(text, ChannelDelayUnitInM);
                double delay_In_m = 0;
                if (!double.TryParse(str, out delay_In_m) || delay_In_m < 0)
                {
                    return -1;
                }
                double delay_In_ms = delay_In_m / config.speedOfSound_In_m;
                short delay_index = (short)Math.Round((delay_In_ms) / delayStep, 0, MidpointRounding.AwayFromZero);
                return AEC.Core.Utils.ConstrainMax(delay_index, this.maxDelayIndex);
            }
        }
    }
}
