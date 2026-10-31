using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.UI.Utility;

namespace AEC.UI.Skin
{
    /// <summary>
    /// 主题色彩：浅色
    /// </summary>
    /// User:Ryan  CreateTime:2012-8-7 22:17.
    internal class SkinThemeLight : SkinTheme
    {
        public SkinThemeLight()
            : base()
        {
        }

        /// <summary>
        /// 初始化主题色彩方案
        /// </summary>
        /// User:Ryan  CreateTime:2012-8-7 22:11.
        /// User:Ryan  CreateTime:2012-8-7 22:18.
        public override void InitSkinTheme()
        {
            this.ThemeStyle = EnumTheme.Light;
            this.ThemeName = "浅色";

            this.BorderColor = Color.FromArgb(65, 157, 212);

            this.proModeBackgroundColor = new GradientColor(Color.FromArgb(255, 255, 255), Color.FromArgb(224, 224, 224), null, null);
            this.easyModeBackgroundColor = new GradientColor(Color.FromArgb(120, 255, 214), Color.FromArgb(0, 121, 145), null, null);
            this.mainTitleBackgroundColor = new GradientColor(Color.FromArgb(14, 137, 153), Color.FromArgb(103, 237, 204), null, null);

            this.ToolbarBackColor = Color.FromArgb(160, 160, 160);

            this.PanelBackColor = Color.FromArgb(224, 224, 224);
            //this.PanelBackColor = Color.FromArgb(0xff, 0xff, 0xff);
            this.PanelForeColor = Color.FromArgb(0x20, 0x20, 0x40);
            this.PanelBorderColor = Color.Black;

            this.TextForeColor = Color.FromArgb(0x20, 0x20, 0x20);

            this.ScrollbarBackColor = Color.FromArgb(192, 192, 192);
            this.ScrollbarForeColor = Color.FromArgb(128, 128, 128);

            this.TextBoxBackColor = Color.FromArgb(224, 224, 224);
            this.TextBoxForeColor = Color.FromArgb(21, 21, 21);

            this.SystemDgvBackColor = Color.FromArgb(224, 224, 224);
            this.SystemDgvAlternatingRowsBackColor = Color.FromArgb(192, 192, 192);
            this.SystemDgvHeaderBackColor = Color.FromArgb(224, 224, 224);
            this.SystemDgvCellDefaultBackColor = Color.FromArgb(224, 224, 224);
            this.SystemFactoryModeForeColor = Color.FromArgb(51, 105, 30);
            this.SystemExternalModeForeColor = Color.FromArgb(230, 81, 0);
        }
    }
}
