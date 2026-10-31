using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.UI.Utility;

namespace AEC.UI.Skin
{
    public class SkinTheme
    {
        /// <summary>
        /// Gets or sets  the theme style
        /// </summary>
        /// <value>The theme style.</value>
        /// User:Ryan  CreateTime:2012-8-7 22:42.
        public EnumTheme ThemeStyle { get; set; }

        /// <summary>
        /// 主题名称
        /// </summary>
        /// <value>The name of the theme.</value>
        /// User:Ryan  CreateTime:2012-8-4 14:44.
        public string ThemeName { get; set; }

        /// <summary>
        /// 边框色彩
        /// </summary>
        /// User:Ryan  CreateTime:2011-07-28 14:07.
        public Color BorderColor { get; set; }
        
        //背景渐变色
        public GradientColor proModeBackgroundColor { get; set; }
        public GradientColor easyModeBackgroundColor { get; set; }
        public GradientColor mainTitleBackgroundColor { get; set; }

        public Color ToolbarBackColor { get; set; }

        public Color PanelBackColor { get; set; }
        public Color PanelForeColor { get; set; }
        public Color PanelBorderColor { get; set; }

        public Color TextForeColor { get; set; }

        public Color TextBoxBackColor { get; set; }
        public Color TextBoxForeColor { get; set; }

        public Color ScrollbarBackColor { get; set; }
        public Color ScrollbarForeColor { get; set; }

        public Color SystemDgvBackColor { get; set; }
        public Color SystemDgvAlternatingRowsBackColor { get; set; }
        public Color SystemDgvHeaderBackColor { get; set; }
        public Color SystemDgvCellDefaultBackColor { get; set; }

        public Color SystemFactoryModeForeColor { get; set; }
        public Color SystemExternalModeForeColor { get; set; }


        public SkinTheme()
        {
            this.InitSkinTheme();
        }

        public virtual void InitSkinTheme()
        {
        }
    }
}
