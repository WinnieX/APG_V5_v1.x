using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.Common;

namespace AEC.UI.Common
{
    public partial class X5OutputSignalMixerPanel : AEC.UI.SignalMixerPanel
    {
        private static AEC.Misc.Config config = AEC.Misc.Config.GetInstance();

        public X5OutputSignalMixerPanel()
        {
            InitializeComponent();
        }

        protected override string GetLevelLabel(short index)
        {
            if (config.mixerLevelUnit == AEC.Misc.MixerLevelUnit.IndB)
            {
                return ParamGetLabelHelper.X5OutputSignalMixerLevelIndB(index);
            }
            else
            {
                return ParamGetLabelHelper.X5OutputSignalMixerLevelInPercentage(index);
            }
        }

        protected override short CalcLevelIndexFromText(string text)
        {
            if (config.mixerLevelUnit == AEC.Misc.MixerLevelUnit.IndB)
            {
                return ParamGetIndexHelper.X5CalcOutputSignalMixerLevelIndexFromTextIndB(text);
            }
            else
            {
                return ParamGetIndexHelper.X5CalcOutputSignalMixerLevelIndexFromTextInPercentage(text);
            }
        }

        public void UpdateLabel()
        {
            base.paramLabel.UpdateLabel();
            base.paramLabel.UpdateDisplay();
        }
    }
}
