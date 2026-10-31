using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.RemoteControl.Protocol
{
    class BufferLengthHelper
    {
        //将长度编码成双字节(网络字节序)
        public static byte[] EncodeToDualByte(int length)
        {
            Debug.Assert(length >= 0 && length <= ushort.MaxValue);
            return BitConverter.GetBytes((ushort)System.Net.IPAddress.HostToNetworkOrder((short)length));
        }

        //将双字节解码成长度(网络字节序)
        public static int DualByteDecodeToLength(byte[] buffer, int startIndex = 0)
        {
            Debug.Assert(buffer != null && startIndex >= 0 && buffer.Length >= sizeof(uint) + startIndex);
            return (ushort)System.Net.IPAddress.NetworkToHostOrder((short)BitConverter.ToUInt16(buffer, startIndex));
        }
    }
}
