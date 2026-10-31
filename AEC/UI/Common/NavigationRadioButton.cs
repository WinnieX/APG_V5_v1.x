using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.UI.Controls;

namespace AEC.UI.Common
{
    public class NavigationRadioButton : MRadioButton
    {
        public override Color TextColor
        {
            get
            {
                if(this.IsChecked)
                {
                    return base.TextColor;
                }

                return this.ForeColor;
            }
        }
    }
}
