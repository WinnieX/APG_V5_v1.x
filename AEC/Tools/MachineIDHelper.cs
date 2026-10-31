using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using System.Security.Principal;
using System.Security.Cryptography;
using System.Security;

namespace AEC.Tools
{
    public class MachineIDHelper
    {
        private static byte[] fingerPrint = null;
        public static byte[] MachineCode
        {
            get
            {
                if (fingerPrint == null)
                {
                    string sid = GetSid();
                    string mn = Environment.MachineName;
                    if (sid != string.Empty)
                    {
                        fingerPrint = GetHash(sid);
                    }
                    else if (mn != string.Empty)
                    {
                        fingerPrint = GetHash(mn);
                    }
                    else
                    {
                        string mac = macId();
                        fingerPrint = GetHash(mac);
                    }
                }
                return fingerPrint;
            }

        }

        public static string GetMachineCode()
        {
            List<byte> datas = new List<byte>(Tools.MachineIDHelper.MachineCode);
            return ByteArrayHelper.ByteArrayToHexStr(HashHelper.MD5(datas.ToArray()), string.Empty);
        }

        private static string GetSid()
        {
            try
            {
                string path = string.Format("WinNT://{0},Computer", Environment.MachineName);
                DirectoryEntries entries = new DirectoryEntry(path).Children;
                foreach (DirectoryEntry entry in entries)
                {
                    SecurityIdentifier si = new SecurityIdentifier((byte[])entry.InvokeGet("objectSID"), 0);
                    if (si != null && si.AccountDomainSid != null)
                    {
                        return si.AccountDomainSid.Value;
                        //Console.WriteLine(si.AccountDomainSid.Value);
                    }

                    break;
                }
            }
            catch(Exception)
            {

            }

            return string.Empty;
        }

        //        public static uint GetID
        //        {
        //            get
        //            {
        //#if DEBUG
        //                return 0;
        //#else
        //                return Value;
        //#endif
        //            }
        //        }

        //private static string GetSid()
        //{
        //    string path = string.Format("WinNT://{0},Computer", Environment.MachineName);
        //    DirectoryEntries entries = new DirectoryEntry(path).Children;
        //    foreach (DirectoryEntry entry in entries)
        //    {
        //        SecurityIdentifier si = new SecurityIdentifier((byte[])entry.InvokeGet("objectSID"), 0);
        //        if (si != null && si.AccountDomainSid != null)
        //        {
        //            return si.AccountDomainSid.Value;
        //            //Console.WriteLine(si.AccountDomainSid.Value);
        //        }

        //        break;
        //    }

        //    return string.Empty;
        //}

        private static byte[] GetHash(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            return sec.ComputeHash(bt);
        }
        //private static string GetHexString(byte[] bt)
        //{
        //    string s = string.Empty;
        //    for (int i = 0; i < bt.Length; i++)
        //    {
        //        byte b = bt[i];
        //        int n, n1, n2;
        //        n = (int)b;
        //        n1 = n & 15;
        //        n2 = (n >> 4) & 15;
        //        if (n2 > 9)
        //            s += ((char)(n2 - 10 + (int)'A')).ToString();
        //        else
        //            s += n2.ToString();
        //        if (n1 > 9)
        //            s += ((char)(n1 - 10 + (int)'A')).ToString();
        //        else
        //            s += n1.ToString();
        //        if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
        //    }
        //    return s;
        //}
        #region Original Device ID Getting Code
        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    try
                    {
                        result += mo[wmiProperty].ToString();
                        //Console.WriteLine(result);
                        //break;
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }
        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                //Only get the first one
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }
        private static string cpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as very time consuming
            string retVal = identifier("Win32_Processor", "UniqueId");
            if (retVal == "") //If no UniqueID, use ProcessorID
            {
                retVal = identifier("Win32_Processor", "ProcessorId");
                if (retVal == "") //If no ProcessorId, use Name
                {
                    retVal = identifier("Win32_Processor", "Name");
                    if (retVal == "") //If no Name, use Manufacturer
                    {
                        retVal = identifier("Win32_Processor", "Manufacturer");
                    }
                    //Add clock speed for extra security
                    retVal += identifier("Win32_Processor", "MaxClockSpeed");
                }
            }
            return retVal;
        }
        //BIOS Identifier
        private static string biosId()
        {
            return identifier("Win32_BIOS", "Manufacturer")
            + identifier("Win32_BIOS", "SMBIOSBIOSVersion")
            + identifier("Win32_BIOS", "IdentificationCode")
            + identifier("Win32_BIOS", "SerialNumber")
            + identifier("Win32_BIOS", "ReleaseDate")
            + identifier("Win32_BIOS", "Version");

            //return identifier("Win32_BIOS", "IdentificationCode");
        }
        //Main physical hard drive ID
        private static string diskId()
        {
            return identifier("Win32_DiskDrive", "Model")
            + identifier("Win32_DiskDrive", "Manufacturer")
            + identifier("Win32_DiskDrive", "Signature")
            + identifier("Win32_DiskDrive", "TotalHeads");
        }
        //Motherboard ID
        private static string baseId()
        {
            return identifier("Win32_BaseBoard", "Model")
            + identifier("Win32_BaseBoard", "Manufacturer")
            + identifier("Win32_BaseBoard", "Name")
            + identifier("Win32_BaseBoard", "SerialNumber");
        }
        //Primary video controller ID
        private static string videoId()
        {
            return identifier("Win32_VideoController", "DriverVersion")
            + identifier("Win32_VideoController", "Name");
        }
        //First enabled network card ID
        private static string macId()
        {
            return identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
        }
        #endregion
    }
}
