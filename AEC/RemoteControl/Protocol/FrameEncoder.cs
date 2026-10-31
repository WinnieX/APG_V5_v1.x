using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RemoteControl.Protocol
{
    class FrameEncoder
    {
        public static byte[] Encode(CommMessage message)
        {
            List<byte> buffer = message.ToByteBuf();
            buffer.Add(ChecksumHelper.CalcChecksum(buffer.ToArray()));
            buffer.InsertRange(0, BufferLengthHelper.EncodeToDualByte(buffer.Count));
            return buffer.ToArray();
        }
    }
}
