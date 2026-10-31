using System;
using System.Collections.Generic;
using System.Text;
using AEC.Misc;

namespace AEC.AudioProducts
{
    public class PowerAmplifier : AudicProductBase
    {
        public short power;                 //功率
        public short impedance;
        public float sensitivity;        //灵敏度

        private Localization localization = Config.GetInstance().localization;

        public PowerAmplifier(string brand_, string model_, short power_, short impedance_, float sensitivity_) :
            base(brand_, model_)
        {
            this.power = power_;
            this.impedance = impedance_;
            this.sensitivity = sensitivity_;
        }

        public string label
        {
            get
            {
                return string.Format("{0}: {1}, {2}: {3}{4}{5}: {6} W {7}: {8} ohms{9}{10}: {11} V",
                    localization[Localization.panelSystemSelection_Brand],
                    this.brand,
                    localization[Localization.panelSystemSelection_Model],
                    this.model,
                    System.Environment.NewLine,
                    localization[Localization.panelSystemSelection_Power],
                    this.power,
                    localization[Localization.panelSystemSelection_Impedance],
                    this.impedance,
                    System.Environment.NewLine,
                    localization[Localization.panelSystemSelection_Sensitivity],
                    this.sensitivity);
            }
        }

        public override string basicInfo
        {
            get
            {
                return string.Format("{0}: {1}", localization[Localization.panelSystemSelection_PowerAmplifier], base.basicInfo);
            }
        }

        public string powerLabel
        {
            get
            {
                return string.Format("{0} W", this.power);
            }
        }

        public string sensitivityLabel
        {
            get
            {
                return string.Format("{0} V", this.sensitivity);
            }
        }

        public string impedanceLabel
        {
            get
            {
                return string.Format("{0} ohms", this.impedance);
            }
        }


        private const float MinSensitivity = 0.01f;
        private const float MaxSensitivity = 5;
        private const short MinPower = 100;
        private const short MaxPower = 2000;
        private const short MinImpedance = 1;
        private const short MaxImpedance = 100;

        public static bool PowerIsValid(short power)
        {
            return power >= MinPower && power <= MaxPower;
        }

        public static bool SensitivityIsValid(float sensitivity)
        {
            return sensitivity >= MinSensitivity && sensitivity <= MaxSensitivity;
        }

        public static bool ImpedanceIsValid(short impedance)
        {
            return impedance >= MinImpedance && impedance <= MaxImpedance;
        }
    }
}
