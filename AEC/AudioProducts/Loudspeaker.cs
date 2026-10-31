using System;
using System.Collections.Generic;
using System.Text;
using AEC.Misc;

namespace AEC.AudioProducts
{
    public class Loudspeaker : AudicProductBase
    {
        public short size;                //尺寸
        public short power;               //功率
        public short impedance;           //阻抗

        private Localization localization = Config.GetInstance().localization;

        public Loudspeaker(string brand_, string model_, short power_, short size_, short impedance_) :
            base(brand_, model_)
        {          
            this.power = power_;
            this.size = size_;
            this.impedance = impedance_;
        }

        public string label
        {
            get
            {
                return string.Format("{0}: {1}, {2}: {3}{4}{5}: {6}W, {7}: {8}″, {9}: {10} ohms",
                    localization[Localization.panelSystemSelection_Brand],
                    this.brand,
                    localization[Localization.panelSystemSelection_Model],
                    this.model,
                    System.Environment.NewLine,
                    localization[Localization.panelSystemSelection_Power],
                    this.power,
                    localization[Localization.panelSystemSelection_Size],
                    this.size,
                    localization[Localization.panelSystemSelection_Impedance],
                    this.impedance);
            }
        }

        public override string basicInfo
        {
            get
            {
                return string.Format("{0}: {1}", localization[Localization.panelSystemSelection_Loudspeaker], base.basicInfo);
            }
        }

        public string powerLabel
        {
            get
            {
                return string.Format("{0} W", this.power);
            }
        }

        public string sizeLabel
        {
            get
            {
                return string.Format("{0} ″", this.size);
            }
        }

        public string impedanceLabel
        {
            get
            {
                return string.Format("{0} ohms", this.impedance);
            }
        }

        private const short MinPower = 100;
        private const short MaxPower = 2000;
        private const short MinSize = 5;
        private const short MaxSize = 20;
        private const short MinImpedance = 1;
        private const short MaxImpedance = 100;

        public static bool PowerIsValid(short power)
        {
            return power >= MinPower && power <= MaxPower;
        }

        public static bool SizeIsValid(short size)
        {
            return size >= MinSize && size <= MaxSize;
        }

        public static bool ImpedanceIsValid(short impedance)
        {
            return impedance >= MinImpedance && impedance <= MaxImpedance;
        }
    }
}
