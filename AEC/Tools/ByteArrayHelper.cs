using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Tools
{
    public class ByteArrayHelper
    {
        public static string ByteArrayToHexStr(byte[] bytes, string span = " ")
        {
            StringBuilder sb = new StringBuilder();
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("X").PadLeft(2, '0'));
                    if(i < bytes.Length - 1)
                    {
                        sb.Append(span);
                    }
                }
            }

            return sb.ToString();
        }

        //
        public static byte[] HexStrToByteArray(string hexSring)
        {
            hexSring = hexSring.Trim().Replace(" ", "");
            byte[] bytes = new byte[hexSring.Length / 2];
            try
            {
                for (int i = 0; i < bytes.Length; ++i)
                {
                    bytes[i] = Convert.ToByte(hexSring.Substring(i * 2, 2), 16);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return bytes;
        }

        public static string ByteArrayToDecStr(byte[] bytes, string span = " ")
        {
            StringBuilder sb = new StringBuilder();
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString(""));
                    if (i < bytes.Length - 1)
                    {
                        sb.Append(span);
                    }
                }
            }

            return sb.ToString();
        }
    }
}
