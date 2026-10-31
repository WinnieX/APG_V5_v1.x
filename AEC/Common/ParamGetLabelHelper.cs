using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;

namespace AEC.Common
{
    public class ParamGetLabelHelper
    {
        public const string Off = "Off";
        public const string levelUnitInPercentage = "%";
        public const string levelUnitIndB = "dB";
        public const string MuteLevel = "-∞";
        public const short OutputLevelStep = 2;
        public const double Hundred = 100;
        public const double OutputLevelStepInPercentage = OutputLevelStep / Hundred;
        public const double OutputLevelStepIndB = 0.1;
        public const double OutputLevelBaseIndB = -40;
        
        public static string GetVolumeLabel(short index)
        {
            return index.ToString();
        }

        public static string ClassicalMicFbeGetLabel(short index)
        {
            if (index == 0)
            {
                return Off;
            }
            else
            {
                return string.Format("{0} Hz", index + 1);
            }
        }

        public static string ProfessinalMicFbeGetLabel(short index)
        {
            if (index == 0)
            {
                return Off;
            }
            else
            {
                return string.Format("{0} Hz", index);
            }
        }

        public static string ClassicMicFbeGetLabel(short index)
        {
            if (index == 0)
            {
                return Off;
            }
            else
            {
                return index.ToString();
            }
        }

        public static string ClassicMicEffectLevelLabel(short index)
        {
            return string.Format("{0}%", index * 10);
        }


        public static string MicNoiseGateGetLabel(short index)
        {
            if (index == 0)
            {
                return Off;
            }
            else
            {
                return string.Format("{0}", index);
            }
        }

        public static string EffectGetMixerLevel(short index)
        {
            return string.Format("{0} {1}", index, levelUnitInPercentage);
        }

        public static string X5OutputSignalMixerLevelInPercentage(short index)
        {
            return string.Format("{0} {1}", index * OutputLevelStep, levelUnitInPercentage);
        }

        public static string X5OutputSignalMixerLevelIndB(short index)
        {
            if(index <= 0)
            {
                return string.Format("{0} {1}", MuteLevel, levelUnitIndB); 
            }
            else
            {
                double linear = index * OutputLevelStepInPercentage;
                return string.Format("{0:0.##} {1}", Core.DSP.DSPHelper.LinearToDecibels(linear), levelUnitIndB);
            }
        }

        public static string OutputSignalMixerLevelIndB(short index)
        {
            if (index <= 0)
            {
                return string.Format("{0} {1}", MuteLevel, levelUnitIndB);
            }
            else
            {
                double levelIndB = index * OutputLevelStepIndB - 40.0;
                return string.Format("{0:0.##} {1}", levelIndB, levelUnitIndB);
            }
        }

        public static string OutputSignalMixerLevelInPercentage(short index)
        {
            if (index <= 0)
            {
                return string.Format("0 {1}", MuteLevel, levelUnitInPercentage);
            }
            else
            {
                double levelIndB = index * 0.1 + OutputLevelBaseIndB;
                double linearInPercentage = Core.DSP.DSPHelper.DecibelsToLinear(levelIndB) * Hundred;
                if (linearInPercentage < 75)
                {
                    return string.Format("{0:f2} {1}", linearInPercentage, levelUnitInPercentage);
                }
                else if (linearInPercentage < 100)
                {
                    return string.Format("{0:f1} {1}", linearInPercentage, levelUnitInPercentage);
                }
                else
                {
                    return string.Format("{0:f0} {1}", linearInPercentage, levelUnitInPercentage);
                }
            }
        }

        public static string EchoPreDelayGetLabel(short index)
        {
            return string.Format("{0} ms", index);
        }

        public static string EchoDelayGetLabel(short index)
        {
            return string.Format("{0} ms", index * 2);
        }

        public static string EchoRepeatGetLabel(short index)
        {
            return string.Format("{0} %", index);
        }

        public static string EchoRChPreDelayGetLabel(short index)
        {
            return string.Format("{0} %", index);
        }

        public static string EchoRChDelayGetLabel(short index)
        {
            return string.Format("{0} %", index - 50);
        }

        public static string ReverbPreDelayGetLabel(short index)
        {
            return string.Format("{0} ms", index);
        }

        public static readonly double reverbDelayStep = 5000.0 / 80.0;
        public static string ReverbDelayGetLabel(short index)
        {
            return string.Format("{0:0} ms", Math.Round(reverbDelayStep * index, 0, MidpointRounding.AwayFromZero));
        }

        public static string ReverbGetDampLabel(short index)
        {
            if (index == 0)
            {
                return Off;
            }
            else
            {
                return (-index).ToString();
            }
        }

        public static readonly short MaxAttack = (short)(DataUtils.compressorAttackList.Length - 1);
        protected const string attackUnit = "ms";
        public static string GetAttackLabel(short index)
        {
            index = Utils.ConstrainMax(index, MaxAttack);
            double at = DataUtils.compressorAttackList[index];
            if (at <= 8.0)
            {
                return String.Format("{0:f1} {1}", at, attackUnit);
            }
            else
            {
                return String.Format("{0:f0} {1}", at, attackUnit);
            }
        }


        public static readonly short MaxRelease = (short)(DataUtils.compressorReleaseList.Length - 1);
        protected const string releaseUnit = "×";
        public static string GetReleaseLabel(short index)
        {
            index = Utils.ConstrainMax(index, MaxRelease);
            int rt = DataUtils.compressorReleaseList[index];
            return String.Format("{0} {1}", rt, releaseUnit);
        }

        public static readonly short MaxRatio = (short)(DataUtils.compressorRatioList.Length - 1);
        public static string GetRatioLabel(short index)
        {
            index = Utils.ConstrainMax(index, MaxRatio);
            double ra = DataUtils.compressorRatioList[index];
            if (ra < 12)
            {
                return String.Format("{0:0.0}", ra);
            }
            else
            {
                return String.Format("{0}", ra);
            }
        }

        private static int[] dsp_level_meter_uart_baud_rate_table = 
        {
            4950,   5180,   5430,   5690,   5950,   6240,   6530,   6840,   7160,   7500,
            7860,   8230,   8620,   9030,   9450,   9900,   10400,  10900,  11400,  11900,
            12500,  13100,  13700,  14300,  15000,  15700,  16500,  17200,  18100,  18900,
            19800,  20700,  21700,  22700,  23800,  24900,  26100,  27400,  28700,  30000,
            31400,  32900,  34500,  36100,  37800,  39600,  41500,  43400,  45500,  47600,
            49900,  52200,  54700,  57300,  60000,  62800,  65800,  68900,  72200,  75600,
            79200,  82900,  86800,  91000,  95300,  99800,  104000, 109000, 115000, 120000,
            126000, 132000, 138000, 144000, 151000, 158000, 166000, 174000, 182000, 191000,
            200000, 209000, 219000, 229000, 240000, 251000, 263000, 276000, 289000, 302000,
            317000, 332000, 347000, 364000, 381000, 399000, 418000, 438000, 458000, 480000,
        };

        public static string GetTimbreLabel(short index)
        {
            index = Core.Utils.ConstrainMax(index, (short)(dsp_level_meter_uart_baud_rate_table.Length - 1));
            return (dsp_level_meter_uart_baud_rate_table[index] / 10).ToString();

            //return string.Format("{0}", index + 1);
        }
    }
}
