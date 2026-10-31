using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.UI.Utility;

namespace AEC.UI.Skin
{
    class SkinThemeDefault : SkinTheme
    {
        public SkinThemeDefault()
            : base()
        {
            this.BorderColor = Color.FromArgb(182, 168, 192);

            this.proModeBackgroundColor = new GradientColor(Color.FromArgb(242, 242, 236), Color.FromArgb(242, 242, 236), null, null);
            this.easyModeBackgroundColor = new GradientColor(Color.FromArgb(120, 255, 214), Color.FromArgb(0, 121, 145), null, null);
            this.mainTitleBackgroundColor = new GradientColor(Color.FromArgb(14, 137, 153), Color.FromArgb(103, 237, 204), null, null);

            this.ToolbarBackColor = Color.FromArgb(192, 192, 192);

            this.PanelBackColor = Color.FromArgb(242, 242, 236);
            this.PanelForeColor = Color.FromArgb(32, 32, 64);
            this.PanelBorderColor = Color.Black;

            this.TextForeColor = Color.FromArgb(32, 32, 64);

            this.ScrollbarBackColor = Color.FromArgb(208, 208, 208);
            this.ScrollbarForeColor = Color.FromArgb(176, 177, 178);

            this.TextBoxBackColor = Color.FromArgb(242, 242, 236);
            this.TextBoxForeColor = Color.FromArgb(32, 32, 64);

            this.SystemDgvBackColor = Color.FromArgb(224, 224, 224);
            this.SystemDgvAlternatingRowsBackColor = Color.FromArgb(192, 192, 192);
            this.SystemDgvHeaderBackColor = Color.FromArgb(224, 224, 224);
            this.SystemDgvCellDefaultBackColor = Color.FromArgb(224, 224, 224);
            this.SystemFactoryModeForeColor = Color.FromArgb(51, 105, 30);
            this.SystemExternalModeForeColor = Color.FromArgb(230, 81, 0);
        }
    }
}
