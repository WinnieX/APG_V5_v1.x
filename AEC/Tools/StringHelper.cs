using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Tools
{
    public class StringHelper
    {
        public static string RemoveConsecutiveDuplicates(string input, char c)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            StringBuilder result = new StringBuilder();
            char previousChar = input[0];
            result.Append(previousChar);

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] != previousChar || input[i] != c)
                {
                    previousChar = input[i];
                    result.Append(previousChar);
                }
            }

            return result.ToString();
        }

        public static bool StringIsDigit(string password)
        {
            Debug.Assert(password != null && password.Length > 0);
            //Char[] password
            bool isContainNonDigit = false;
            foreach (Char ch in password.ToCharArray())
            {
                if (!Char.IsDigit(ch))
                {
                    isContainNonDigit = true;
                    break;
                }
            }

            return !isContainNonDigit;
        }

        public static string DateTime2String(DateTime dt)
        {
            return dt.ToString("yyyyMMdd HHmmss.fff");
        }

        public static string GetMaxSizeUtf8String(string str, int maxSize)
        {
            while (true)
            {
                int size = Encoding.UTF8.GetByteCount(str);
                if (size <= maxSize)
                {
                    return str;
                }

                str = str.Substring(0, str.Length - 1);

                if (str.Length == 0)
                {
                    return string.Empty;
                }
            }
        }

        public static void StringBuilderAppendLine(StringBuilder sb, string format, params object[] args)
        {
            if(sb.Length > 0)
            {
                sb.AppendLine(string.Empty);
            }
            
            sb.AppendFormat(format, args);
        }
    }
}
