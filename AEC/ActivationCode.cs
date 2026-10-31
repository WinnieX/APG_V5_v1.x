using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using AEC.Tools;
using AEC.Misc;

namespace AEC
{
    public class ActivationCode
    {
        public enum MLicenseType
        {
            Unauthorized,   //未授权的
            Temporary,      //临时的
            Limited,        //有限的    
            Unlimited,      //不受限制的
        };

        private const string ActivationCodeRegKey = @"SOFTWARE\Meng";
        private const string ActivationCodeKeyName = "MToolActivationCode";
        private const string MToolsLicense = "MToolsLicense_";

        private static readonly Dictionary<MLicenseType, string> licenseMap = new Dictionary<MLicenseType, string>()
        {
            {MLicenseType.Unlimited, "Unlimited"},
            {MLicenseType.Limited, "Limited"},
            {MLicenseType.Temporary, "Temporary_"},
            //{MLicenseType.Unauthorized, "Unauthorized"},
        };

        private const string unLicensed = "Unknown Authorization";

        private static readonly ulong randNumber = MRandom.GetRandUInt64();

        public static string GetRequestCode()
        {
            byte[] temp = HashHelper.MD5(BitConverter.GetBytes(randNumber));
            if (temp.Length > 0x08)
            {
                Array.Resize(ref temp, 0x08);
            }

            return AEC.Tools.ByteArrayHelper.ByteArrayToHexStr(temp, string.Empty);
        }

        protected static Localization localization = AEC.Misc.Config.GetInstance().localization;

        public static string GetLicenseDesc(MLicenseType licenseType)
        {
            Dictionary<MLicenseType, string> licenseDesc = new Dictionary<MLicenseType, string>()
            {
                {MLicenseType.Unauthorized, localization[Localization.licenseType_Unauthorized]},
                {MLicenseType.Temporary, localization[Localization.licenseType_Temporary]},
                {MLicenseType.Limited, localization[Localization.licenseType_Limited]},
                {MLicenseType.Unlimited, localization[Localization.licenseType_Unlimited]},                
            };

            if(licenseDesc.ContainsKey(licenseType))
            {
                return licenseDesc[licenseType];
            }

            return unLicensed;
        }

        public static MLicenseType CheckLicense()
        {
            try
            {
                using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(ActivationCodeRegKey, false))
                {
                    if(regKey == null)
                    {
                        return MLicenseType.Unauthorized;
                    }

                    string activationCode = regKey.GetValue(ActivationCodeKeyName) as string;

                    return CheckActivationCode(activationCode, string.Empty);
                }
            }
            catch(Exception )
            {
                return MLicenseType.Unauthorized;
            }            
        }

        public static MLicenseType CheckActivationCode(string activationCode, string requestCode)
        {
            if(string.IsNullOrEmpty(activationCode))
            {
                return MLicenseType.Unauthorized;
            }

            foreach(var type in licenseMap.Keys)
            {
                string licenseType = licenseMap[type];

                if(type == MLicenseType.Temporary)
                {
                    licenseType += requestCode;
                }

                string code = AEC.Tools.ByteArrayHelper.ByteArrayToHexStr(HashHelper.MD5(Encoding.UTF8.GetBytes(MToolsLicense + licenseType + "@" + MachineIDHelper.GetMachineCode())), string.Empty);

                if(code == activationCode.Trim())
                {
                    return type;
                }
            }

            return MLicenseType.Unauthorized;
        }

        //此方法可能抛出异常
        public static void SaveActivationCode(string activationCode)
        {
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey(ActivationCodeRegKey);

            //string[] subkeys = Registry.CurrentUser.GetSubKeyNames();

            //if(!subkeys.Contains(ActivationCodeRegKey))
            //{
            //    regKey 
            //}
            //else
            //{
            //    regKey = Registry.CurrentUser.OpenSubKey(ActivationCodeRegKey, true);
            //}

            //if(CheckActivationCode(activationCode) == MLicenseType.Temporary)     //如果许可证是临时的，则不保存
            //{
            //    return;
            //}

            regKey.SetValue(ActivationCodeKeyName, activationCode);
        }

        public static void ClearLicense()
        {
            try
            {
                Registry.CurrentUser.DeleteSubKey(ActivationCodeRegKey);
            }
            catch(Exception )
            {

            }            
        }

        internal class MRandom
        {
            public static ulong GetRandUInt64()
            {
                byte[] buffer = AEC.Tools.RandNumberHelper.GetRandNumbers(sizeof(ulong));                
                return BitConverter.ToUInt64(buffer, 0);
            }
        }
    }
}
