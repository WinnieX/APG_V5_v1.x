using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Comm.FirmwareUpdate
{
    public class BTDeviceInfo
    {
        public const int BT_ADDR_SIZE = 6;
        public const int BT_NAME_MAX_SIZE = 32;
        public const string DefBTName = "MDSP_";

        public const byte BT_ADDR_MSB_MASK = 0xC0;
        public const UInt64 BT_ADDR_MASK = ((UInt64)0xC0) << 40;
        //public const UInt64 BT_ADDR_MASK = 0x00003FFFFFFFFFFF;

        public readonly byte[] addrBuffer = new byte[BT_ADDR_SIZE];//{0x01, 0x23, 0x45, 0x67, 0x89, 0xba};//大端字节序
        private string _name = string.Empty;

        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                this._name = Tools.StringHelper.GetMaxSizeUtf8String(value.Trim(), BT_NAME_MAX_SIZE);
            }
        }

        public byte[] nameBuffer
        {
            get
            {
                return System.Text.Encoding.UTF8.GetBytes(name);
            }
            set
            {
                if (value == null || value.Length == 0)
                {
                    this._name = string.Empty;
                    return;
                }

                this.name = System.Text.Encoding.UTF8.GetString(value);
            }
        }

        public string AddressLower16BitsDesp
        {
            get
            {
                byte[] lowerAddress = new byte[] { this.addrBuffer[0], this.addrBuffer[1] };
                return AddrBytesToString(ReverseBytesArray(lowerAddress), string.Empty);
            }
        }

        public string addressDesp
        {
            get
            {
                return AddrBytesToString(ReverseBytesArray(this.addrBuffer));
            }
            set
            {
                try
                {
                    byte[] data = ReverseBytesArray(HexStringToAddrBytes(value));
                    if (data.Length > this.addrBuffer.Length)
                    {
                        Array.Resize(ref data, this.addrBuffer.Length);
                    }

                    Array.Clear(this.addrBuffer, 0, this.addrBuffer.Length);

                    Array.Copy(data, this.addrBuffer, data.Length);
                }
                catch (Exception e)
                {

                }
            }
        }

        public BTDeviceInfo()
        {
            //MDSP_Download.Tools.RandomHelper.FillRandomNumber(this.addrBuffer);
            //this.addrBuffer[BT_ADDR_SIZE - 1] |= BT_ADDR_MSB_MASK;
            InitRandBTAddr(this.addrBuffer);
        }

        public BTDeviceInfo(byte[] addr_, byte[] name_)
        {
            if (addr_ != null && addr_.Length == BT_ADDR_SIZE)
            {
                Array.Copy(addr_, this.addrBuffer, this.addrBuffer.Length);
            }

            this.nameBuffer = name_;
        }

        public void AddressRollingCode()
        {
            //byte[] tempAddrBuffer = new byte[this.addrBuffer.Length];
            //Array.Copy(this.addrBuffer, tempAddrBuffer, tempAddrBuffer.Length);
            byte[] temp = new byte[sizeof(UInt64)];
            Array.Copy(this.addrBuffer, temp, this.addrBuffer.Length);

            UInt64 data8 = BitConverter.ToUInt64(temp, 0);
            data8 += 1;
            if ((data8 & BT_ADDR_MASK) != 0)
            {
                //MDSP_Download.Tools.RandomHelper.FillRandomNumber
                temp = BitConverter.GetBytes(data8);
                Array.Copy(temp, this.addrBuffer, this.addrBuffer.Length);
            }
            else
            {
                InitRandBTAddr(this.addrBuffer);
            }
        }

        private static void InitRandBTAddr(byte[] addr)
        {
            Debug.Assert(addr != null && addr.Length >= BT_ADDR_SIZE);

            AEC.Tools.RandNumberHelper.FillRandomNumber(addr);
            addr[BT_ADDR_SIZE - 1] |= BT_ADDR_MSB_MASK;
        }

        public static string AddrBytesToString(byte[] data, string separator = ":")
        {
            Debug.Assert(data != null);

            //StringBuilder sb = new StringBuilder();

            return BitConverter.ToString(data).Replace("-", separator);
        }

        public static byte[] HexStringToAddrBytes(string str)
        {
            Debug.Assert(str != null && str.Length > 0);
            str = str.Trim();
            string[] hexStrings = str.Split(' ', '-', ':', '.');

            byte[] bytes = new byte[hexStrings.Length];
            for (int i = 0; i < bytes.Length; ++i)
            {
                try
                {
                    bytes[i] = Convert.ToByte(hexStrings[i], 16);
                }
                catch (Exception e)
                {
                    bytes[i] = 0;
                }
            }

            return bytes;
        }

        public static bool CheckAddrString(string str)
        {
            try
            {
                byte[] data = HexStringToAddrBytes(str);
                if (data != null && data.Length == BT_ADDR_SIZE)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static byte[] ReverseBytesArray(byte[] buffer)
        {
            byte[] temp = (byte[])buffer.Clone();
            Array.Reverse(temp);
            return temp;
        }
    }
}
