using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.UI.Utility;

namespace AEC.UI.Skin
{
    /// <summary>
    /// 主题色彩：面朝大海，春暖花开
    /// </summary>
    /// User:Ryan  CreateTime:2012-8-7 22:17.
    internal class SkinThemeBlueSea : SkinTheme
    {
        public SkinThemeBlueSea()
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
            this.ThemeStyle = EnumTheme.BlueSea;
            this.ThemeName = "面朝大海，春暖花开";

            this.BorderColor = Color.FromArgb(65, 157, 212);

            this.proModeBackgroundColor = new GradientColor(Color.FromArgb(16, 16, 16), Color.FromArgb(64, 64, 64), null, null);
            this.easyModeBackgroundColor = new GradientColor(Color.FromArgb(120, 255, 214), Color.FromArgb(0, 121, 145), null, null);
            this.mainTitleBackgroundColor = new GradientColor(Color.FromArgb(14, 137, 153), Color.FromArgb(103, 237, 204), null, null);
        }
    }
}
