using System;
using System.Collections.Generic;
using System.Text;
using AEC.Common;
using AEC.UI.Controls;

namespace AEC.UI.KX
{
    public class LinearLevelIndicator
    {
        //private LinearValueSmoother levelSmoother = LinearValueSmoother.Create2ndOrderSmoother();
        private PeakValueHolder[] levelHolders = KXMonitor.CreateLevelHolderPair();

        public void Clear()
        {
            //this.levelSmoother.Clear();
        }

        public bool UpdateStatus(DUILevelMeter levelMeter, double level)
        {
            if(level == 0)
            {
                Log.OutputLine("Test()...");
            }
            //double level_IndB = AEC.Core.DSP.DSPHelper.LinearToDecibels(this.levelSmoother.GetSmoothValue(level));
            double level_IndB = AEC.Core.DSP.DSPHelper.LinearToDecibels(level);

            level_IndB = (double)Math.Round(level_IndB + 0.1, 1, MidpointRounding.AwayFromZero);

            //Log.OutputLine("UpdateStatus()...{0}dB", level_IndB);

            bool currLevelUpdated = levelHolders[0].UpdateCurrValue(level_IndB);
            bool peakLevelUpdated = levelHolders[1].UpdateCurrValue(level_IndB);

            //Log.OutputLine("UpdateStatus()...level: {0}dB, peak: {1}dB", levelHolders[0].currValue, levelHolders[1].currValue);

            levelMeter.currLevel = levelHolders[0].currValue;
            levelMeter.peakLevel = levelHolders[1].currValue;

            return currLevelUpdated || peakLevelUpdated;
        }

        public bool DoLevelFreeFall(DUILevelMeter levelMeter)
        {
            bool ret1 = this.levelHolders[0].FreeFall();
            bool ret2 = this.levelHolders[1].FreeFall();

            //Log.OutputLine("DoLevelFreeFall()...curr level:{0}dB, peak:{1}dB", levelHolders[0].currValue, levelHolders[1].currValue);

            levelMeter.currLevel = levelHolders[0].currValue;

            if (levelHolders[1].currValue < levelHolders[0].currValue)
            {
                ret2 = levelHolders[1].UpdateCurrValue(levelHolders[0].currValue);
            }

            levelMeter.peakLevel = levelHolders[1].currValue;

            return ret1 || ret2;
        }
    }
}
