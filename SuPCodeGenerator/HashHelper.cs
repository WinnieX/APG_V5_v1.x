using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace AEC.Tools
{
    public class HashHelper
    {
        public static byte[] MD5(byte[] data)
        {
            MD5 hash = new MD5CryptoServiceProvider();
            return hash.ComputeHash(data);
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
