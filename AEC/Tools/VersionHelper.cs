using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Tools
{
    public class VersionHelper
    {
        public static string GetVersion()
        {
            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            version = version.Substring(0, version.LastIndexOf("."));
#if DEBUG_VERSION
            return "Debug v" + version;
#else
            return "v" + version;
#endif
        }
    }
}
