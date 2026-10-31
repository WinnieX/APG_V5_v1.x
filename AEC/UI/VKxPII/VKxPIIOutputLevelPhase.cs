using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;

namespace AEC.UI.VKxPII
{
    public class VKxPIIOutputLevelPhase : LevelPhase
    {
        private static AEC.Misc.Config config = AEC.Misc.Config.GetInstance();
        protected override string GetLevelLabel(short index)
        {
            if (config.mixerLevelUnit == AEC.Misc.MixerLevelUnit.IndB)
            {
                return Level.GetOutputLevelIndBLabel(index);
            }
            else
            {
                return Level.GetOutputLevelInPercentageLabel(index);
            }
        }

        protected override short CalcLevelIndexFromText(string text)
        {
            if (config.mixerLevelUnit == AEC.Misc.MixerLevelUnit.IndB)
            {
                return base.level.CalcOutputLevelIndBIndexFromText(text);
            }
            else
            {
                return base.level.CalcOutputLevelInPercentageIndexFromText(text);
            }
        }

        public void UpdateLabel()
        {
            base.paramLabel.UpdateLabel();
            base.paramLabel.UpdateDisplay();
        }
    }
}
