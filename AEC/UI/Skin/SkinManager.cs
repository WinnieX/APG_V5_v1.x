using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.UI.Skin
{
    public class SkinManager
    {
        private static SkinTheme _currentSkin = null;
        public SkinManager()
        {

        }

        public static SkinTheme CurrentSkin
        {
            get
            {
                if (_currentSkin == null)
                {
                    _currentSkin = GetSkinTheme();
                }

                return _currentSkin;
            }
        }

        private static SkinTheme GetSkinTheme()
        {
            //return new SkinThemeLight();
            return new SkinThemeDefault();
        }
    }
}
