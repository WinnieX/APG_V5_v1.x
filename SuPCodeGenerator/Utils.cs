using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Core
{
    public class Utils
    {
        public static short ConstrainValue(short value, short min, short max)
        {
            return Math.Min(Math.Max(value, min), max);
        }

        public static short ConstrainMax(short value, short max)
        {
            return Math.Min(Math.Max(value, (short)0), max);
        }

        public static int ConstrainValue(int value, int min, int max)
        {
            return Math.Min(Math.Max(value, min), max);
        }

        public static int ConstrainMax(int value, int max)
        {
            return Math.Min(Math.Max(value, (int)0), max);
        }

        public static float ConstrainValue(float value, float min, float max)
        {
            return Math.Min(Math.Max(value, min), max);
        }

        public static string ByteToHexStr(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("X").PadLeft(2, '0'));
                }
            }

            return sb.ToString();
        }

        public static byte[] HexStringToByteArray(string hexSring)
        {
            //List<byte> byteList = new List<byte>();
            hexSring = hexSring.Trim().Replace(" ", "");
            byte[] bytes = new byte[hexSring.Length / 2 ];            
            try
            {
                for(int i = 0; i < bytes.Length; ++i)
                {
                    bytes[i] = Convert.ToByte(hexSring.Substring(i * 2, 2), 16);
                }
            }
            catch(Exception)
            {
                return null;
            }

            return bytes;
        }

        public static string ByteArrayToHexStr(byte[] bytes, string span = " ")
        {
            StringBuilder sb = new StringBuilder();
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("X").PadLeft(2, '0'));
                    sb.Append(span);
                }
            }

            return sb.ToString();
        }

        public static string RemoveStringPrefix(string text, string prefix)
        {
            Debug.Assert(text.Length > 0 && text.Length > 0);

            text = text.Replace(" ", "");
            text = text.ToLower();
            prefix = prefix.Replace(" ", "");
            prefix = prefix.ToLower();
            if (prefix.Length > 0 && text.StartsWith(prefix))
            {
                int index = text.IndexOf(prefix);
                if (index == 0)
                {
                    text = text.Remove(index, prefix.Length);
                    return text;
                }
            }

            return text;
        }

        public static string RemoveStringSuffix(string text, string suffix)
        {
            Debug.Assert(text.Length > 0 && suffix.Length > 0);

            text = text.Replace(" ", "");
            text = text.ToLower();
            suffix = suffix.Replace(" ", "");
            suffix = suffix.ToLower();
            if (text.EndsWith(suffix))
            {
                int index = text.IndexOf(suffix);
                if(index > 0)
                {
                    text = text.Remove(index);
                    return text;
                }
            }

            return text;
        }
    }
}
