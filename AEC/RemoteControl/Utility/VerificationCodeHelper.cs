using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RemoteControl.Utility
{
    class VerificationCodeHelper
    {
        private const int maxValue = 10000;

        public static uint GeneratorCode()
        {
            Random random = new Random();
            return (uint)random.Next(maxValue);
            //return 7777;
        }

        public static bool CheckCode(uint code)
        {
            return code < maxValue;
        }

        public static string Code2String(uint code)
        {
            return code.ToString("D4");
        }
    }
}
