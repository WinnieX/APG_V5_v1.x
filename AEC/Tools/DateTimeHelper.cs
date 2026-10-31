using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Tools
{
    public class DateTimeHelper
    {
        public static string DateTime2String(DateTime dt)
        {
            return dt.ToString("yyyy/MM/dd HH:mm:ss:fff");
        }
    }
}
