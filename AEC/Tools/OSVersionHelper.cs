using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Tools
{
    public class OSVersionHelper
    {
        public static bool isWinXP()
        {
            Version ver = System.Environment.OSVersion.Version;
            if (ver.Major <= 5 && ver.Minor <= 1)
            {
                return true;
            }

            return false;
        }

        public static bool isWin7()
        {
            Version ver = System.Environment.OSVersion.Version;
            if (ver.Major == 6 && ver.Minor <= 1)
            {
                return true;
            }

            return false;
        }
    }
}
