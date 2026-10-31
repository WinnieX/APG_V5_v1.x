using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace AEC.Tools
{
    public class EncryptHelper
    {
        public static byte[] Encrypt(byte[] data, byte[] key)
        {
            RijndaelManaged rm = new RijndaelManaged
            {
                Key = key,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateEncryptor();
            return cTransform.TransformFinalBlock(data, 0, data.Length);
        }

        public static byte[] Decrypt(byte[] data, byte[] key)
        {
            RijndaelManaged rm = new RijndaelManaged
            {
                Key = key,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateDecryptor();
            return cTransform.TransformFinalBlock(data, 0, data.Length);
        }

        public static byte[] AESEncrypt(byte[] bytesToBeEncrypted, byte[] key, byte[] iv, PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            byte[] encryptedBytes = null;

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    AES.Key = key;
                    AES.IV = iv;

                    AES.Mode = CipherMode.CBC;

                    AES.Padding = paddingMode;

                    using (CryptoStream cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        public static byte[] AESDecrypt(byte[] bytesToBeDecrypted, byte[] key, byte[] iv, PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            byte[] decryptedBytes = null;

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    AES.Key = key;
                    AES.IV = iv;

                    AES.Mode = CipherMode.CBC;

                    AES.Padding = paddingMode;

                    using (CryptoStream cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        public static void AESTest()
        {
            byte[] data1 = new byte[68968];
            byte[] key1 = new byte[0x10];
            byte[] iv = new byte[0x10];

            try
            {
                byte[] test = AESEncrypt(data1, key1, iv);
                Log.OutputLine("data length = {0}", test.Length);
            }
            catch (Exception e)
            {
                Log.OutputLine("Exception : {0}", e.Message);
            }
            
        }
    }
}
