using System;
using System.Collections.Generic;
using System.Text;
using AEC.Misc;

namespace AEC.AudioProducts
{
    public class Microphone : AudicProductBase
    {
        public short recommendedVolume = 0;  //建议电平值
        private Localization localization = Config.GetInstance().localization;

        public Microphone(string brand_, string model_, short recommendedVolume_ = 0) :
            base(brand_, model_)
        {
            this.recommendedVolume = recommendedVolume_;
        }

        public string label
        {
            get
            {
                string lbl = string.Format("{0}: {1}, {2}: {3}",
                    localization[Localization.panelSystemSelection_Brand],
                    this.brand,
                    localization[Localization.panelSystemSelection_Model],
                    this.model);

                if (recommendedVolume > 0)
                {
                    lbl += string.Format("{0}{1}: {2}",
                    System.Environment.NewLine,
                    localization[Localization.panelSystemSelection_RecommendedVolume],
                    this.recommendedVolume);
                }

                return lbl;
            }
        }

        public override string basicInfo
        {
            get
            {
                return string.Format("{0}: {1}", localization[Localization.panelSystemSelection_Microphone], base.basicInfo);
            }
        }


        private const short MinRecommendedVolume = 0;
        private const short MaxRecommendedVolume = 100;

        public static bool RecommendedVolumeIsValid(short recommendedVolume)
        {
            return recommendedVolume >= MinRecommendedVolume && recommendedVolume <= MaxRecommendedVolume;
        }
    }
}
