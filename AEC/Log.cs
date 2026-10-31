using System;
using System.Collections.Generic;
using System.Text;

namespace AEC
{
    class Log
    {
        public static bool isOutput = true;

        public static void OutputLine(string format, params object[] arg)
        {
            if(isOutput)
            {
                try
                {
                    string log = string.Format("{0:HH:mm:ss:fff} {1}", DateTime.Now, string.Format(format, arg));
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(log);
                    Console.ResetColor();
                }
                catch(Exception)
                {

                }
            }
        }

        public static void Clear()
        {
            if (isOutput)
            {
                try
                {
                    Console.Clear();
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
