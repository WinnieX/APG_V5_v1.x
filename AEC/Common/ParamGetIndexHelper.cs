using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;

namespace AEC.Common
{
    public class ParamGetIndexHelper
    {
        protected const string attackUnit = "ms";
        public const string levelUnitInPercentage = "%";
        public const double Hundred = 100;
        public const double OutputLevelStepIndB = 0.1;
        public const double OutputLevelBaseIndB = -40;
        public const short MaxOutputLevelIndexIndB = 520;

        public static short CalcAttackIndexFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, attackUnit);
            str = str.Trim();
            double value;
            if (double.TryParse(str, out value))
            {
                return DataUtils.FindNearestIndexFromOrderedList(value, DataUtils.compressorAttackList);
            }

            return -1;
        }

        protected const string releaseUnit = "×";
        public static short CalcReleaseIndexFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, releaseUnit);
            str = str.Trim();
            int value;
            if (int.TryParse(str, out value))
            {
                return DataUtils.FindNearestIndexFromOrderedList(value, DataUtils.compressorReleaseList);
            }

            return -1;
        }

        public static short CalcRatioIndexFromText(string text)
        {
            text = text.Trim();
            double value;
            if (double.TryParse(text, out value))
            {
                return DataUtils.FindNearestIndexFromOrderedList(value, DataUtils.compressorRatioList);
            }

            return -1;
        }

        public static short CalcEffectMixerLevelIndexFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, levelUnitInPercentage);
            str = str.Trim();
            short value;
            if (short.TryParse(str, out value))
            {
                return value;
            }

            return -1;
        }

        public static short X5CalcOutputSignalMixerLevelIndexFromTextInPercentage(string text)
        {
            string str = Utils.RemoveStringSuffix(text, ParamGetLabelHelper.levelUnitInPercentage);
            str = str.Trim();
            short value;
            if (short.TryParse(str, out value))
            {
                return (short)(value / ParamGetLabelHelper.OutputLevelStep);
            }

            return -1;
        }

        public static short X5CalcOutputSignalMixerLevelIndexFromTextIndB(string text)
        {
            string str = Utils.RemoveStringSuffix(text, ParamGetLabelHelper.levelUnitIndB);
            str = str.Trim();
            if(str == ParamGetLabelHelper.MuteLevel)
            {
                return 0;
            }

            double value;
            if (double.TryParse(str, out value))
            {
                double linear = AEC.Core.DSP.DSPHelper.DecibelsToLinear(value);
                return (short)Math.Round(linear / ParamGetLabelHelper.OutputLevelStepInPercentage, 0, MidpointRounding.AwayFromZero);
            }

            return -1;
        }

        public static short CalcOutputSignalMixerLevelIndexFromTextInPercentage(string text)
        {
            string str = Utils.RemoveStringSuffix(text, levelUnitInPercentage);
            double value;
            if (double.TryParse(str, out value) && value >= 0)
            {
                double threshold = Core.DSP.DSPHelper.DecibelsToLinear(OutputLevelStepIndB + OutputLevelBaseIndB) * Hundred;
                if (value < threshold)
                {
                    return 0;
                }

                value /= Hundred;

                double decibels = Core.DSP.DSPHelper.LinearToDecibels(value);
                short index = (short)Math.Round((decibels - OutputLevelBaseIndB) * 10, 0, MidpointRounding.AwayFromZero);

                return Utils.ConstrainValue(index, (short)0, MaxOutputLevelIndexIndB);
            }

            return -1;
        }

        public static short CalcOutputSignalMixerLevelIndexFromTextIndB(string text)
        {
            string str = Utils.RemoveStringSuffix(text, ParamGetLabelHelper.levelUnitIndB);
            str = str.Trim();
            if (str == ParamGetLabelHelper.MuteLevel)
            {
                return 0;
            }

            double value;
            if (double.TryParse(str, out value))
            {
                short index = (short)Math.Round((value - OutputLevelBaseIndB) / OutputLevelStepIndB, 0, MidpointRounding.AwayFromZero);
                return Utils.ConstrainMax(index, MaxOutputLevelIndexIndB);
            }

            return -1;
        }
    }
}
