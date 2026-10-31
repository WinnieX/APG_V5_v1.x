using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Comm.FirmwareUpdate
{
    public class BitHelper
    {
        public const int BitsPerByte = 8;
        public const int BitsPerUint = BitsPerByte * sizeof(uint);

        public static byte GetByte(int index, bool flag)
        {
            Debug.Assert(index >= 0 && index < BitsPerByte);
            if(flag)
            {
                return (byte)((1 << index) & 0xff);
            }
            else
            {
                return (byte)~((1 << index) & 0xff);
            }
        }

        public static uint GetUint(int index, bool flag)
        {
            Debug.Assert(index >= 0 && index < BitsPerUint);
            if (flag)
            {
                return (uint)((1 << index) & 0xffffffff);
            }
            else
            {
                return (uint)~((1 << index) & 0xffffffff);
            }
        }
    }
}
