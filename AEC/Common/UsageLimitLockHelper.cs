using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Common
{
    public class UsageLimitLockHelper
    {
        private const uint MaxPasswordNumber = 1000000;
        public static string GetSuperPassword(byte[] deviceId, uint index, uint randKey)
        {
            Debug.Assert(deviceId != null && deviceId.Length == Common.SystemModuleBase.DeviceIdLength);

            uint id = BitConverter.ToUInt32(deviceId, 0);
            id += index;
            id ^= randKey;
            byte[] hash = Tools.HashHelper.SHA256(BitConverter.GetBytes(id));
            uint temp = BitConverter.ToUInt32(hash, 0) % MaxPasswordNumber;

            Debug.Assert(temp < 1000000);

            string superPassword = temp.ToString().PadLeft(UsageLimitLock.PasswordLength, '0');

            Debug.Assert(superPassword.Length == UsageLimitLock.PasswordLength);

            return superPassword;
        }

        public static string GetRandomPassword()
        {
            byte[] data = Tools.RandNumberHelper.GetRandNumbers(sizeof(uint));
            uint rand = BitConverter.ToUInt32(data, 0) % MaxPasswordNumber;

            Debug.Assert(rand < 1000000);

            string password = rand.ToString().PadLeft(UsageLimitLock.PasswordLength, '0');

            Debug.Assert(password.Length == UsageLimitLock.PasswordLength);

            return password;
        }

        public static bool IsValidPassword(string password)
        {
            Debug.Assert(password != null);

            if (password.Length  == UsageLimitLock.PasswordLength)
            {
                uint temp = 0;
                return uint.TryParse(password, out temp);
            }

            return false;
        }

        public static string FilterDigitChar(string str)
        {
            List<char> list = new List<char>();
            foreach (Char c in str.ToCharArray())
            {
                if (Char.IsDigit(c))
                {
                    list.Add(c);
                }
            }

            return new string(list.ToArray());
        }
    }
}
