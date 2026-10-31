using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace AEC.Tools
{
    public class HashHelper
    {
        public static string GetStringMD5(string str)
        {
            byte[] input = Encoding.Default.GetBytes(str);
            byte[] output = MD5(input);

            return AEC.Core.Utils.BytesToHexStr(output);
        }

        public static byte[] MD5(byte[] data)
        {
            MD5 hash = new MD5CryptoServiceProvider();
            return hash.ComputeHash(data);
        }

        public static byte[] GetFileMD5(string path)
        {
            try
            {
                using (var fileStream = System.IO.File.Open(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    int length = (int)fileStream.Length;
                    byte[] buffer = new byte[length];
                    fileStream.Read(buffer, 0, (int)fileStream.Length);

                    return MD5(buffer);
                }
            }
            catch(Exception e)
            {
                LogHelper.WriteInfo("GetFileMD5()...异常:{0}", e.Message);
            }

            return null;
        }

        public static byte[] SHA1(byte[] data)
        {
            SHA1 hash = new SHA1CryptoServiceProvider();
            return hash.ComputeHash(data);
        }

        public static byte[] SHA256(byte[] data)
        {
            SHA256 hash = new SHA256Managed();
            return hash.ComputeHash(data);
        }
    }
}
