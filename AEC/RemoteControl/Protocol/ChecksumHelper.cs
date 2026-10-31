using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.RemoteControl.Protocol
{
    class ChecksumHelper
    {
        public static byte CalcChecksum(byte[] buffer, int length, int startIndex = 0)
        {
            Debug.Assert(buffer != null && length > 0 && startIndex >= 0 && buffer.Length >= startIndex + length);

            byte sum = 0x00;
            for (int i = startIndex; i < length + startIndex && i < buffer.Length; ++i)
            {
                sum += buffer[i];
            }

            return sum;
        }

        public static byte CalcChecksum(byte[] buffer, int startIndex = 0)
        {
            return CalcChecksum(buffer, buffer.Length - startIndex, startIndex);
        }

        public static byte CalcChecksum(byte[] buffer)
        {
            return CalcChecksum(buffer, buffer.Length, 0);
        }
    }
}
