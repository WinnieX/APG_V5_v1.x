using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Test
{
    class PrintTest
    {
        public static void Print()
        {
            Tools.LogHelper.WriteDebug("Hello, AEC!");
        }

        public static void PointNow(string mark = "")
        {
            int s = DateTime.Now.Second;
            int m = DateTime.Now.Millisecond;
            Tools.LogHelper.WriteDebug("{0}: {1}s:{2}ms", mark, s, m);
        }
    }
}
