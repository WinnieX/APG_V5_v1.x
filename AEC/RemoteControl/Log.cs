using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RemoteControl
{
    class Log
    {
        public static void Output(string format, params object[] arg)
        {
            Console.Write(format, arg);
#if DEBUG
            Console.Write(format, arg);
#endif
        }

        public static void OutputLine(string format, params object[] arg)
        {
            Console.WriteLine(format, arg);
#if DEBUG
            Console.WriteLine(format, arg);
#endif
        }
    }
}
