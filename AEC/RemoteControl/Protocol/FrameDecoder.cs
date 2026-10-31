using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
namespace AEC.RemoteControl.Protocol
{
    class FrameDecoder
    {
        private const int MinDataLength = 4;
        private const int ChecksumFieldLength = 1;
        private const int LengthFieldLength = sizeof(ushort);

        public static CommMessage Decode(byte[] buffer)
        {
            Debug.Assert(buffer != null);
            if (buffer.Length <= LengthFieldLength)
            {
                return null;
            }

            int frameStartIndex = LengthFieldLength;
            int frameLength = BufferLengthHelper.DualByteDecodeToLength(buffer);

            if (frameLength < MinDataLength)
            {
                throw(new Exception("数据长度错误"));
            }

            if (buffer.Length < frameStartIndex + frameLength)
            {
                return null;
            }

            int checksumIndex = frameStartIndex + frameLength - ChecksumFieldLength;
            byte checksum = buffer[checksumIndex];
            int messageLength = frameLength - ChecksumFieldLength;

            if (!(ChecksumHelper.CalcChecksum(buffer, messageLength, frameStartIndex) == checksum))
            {
                throw (new Exception("数据校验错误"));
            }

            int messageNameLength = BufferLengthHelper.DualByteDecodeToLength(buffer, frameStartIndex);
            int msgDataLength = frameLength - (LengthFieldLength + messageNameLength + ChecksumFieldLength);
            if (messageNameLength == 0 || msgDataLength < 0)
            {
                throw (new Exception("消息长度错误"));
            }

            string msgName = StringBufferHelper.BytesDncodeToString(buffer, frameStartIndex + frameStartIndex, messageNameLength);
            byte[] msgData = null;
            if(msgDataLength > 0)
            {
                msgData = new byte[msgDataLength];
                int srcStartIndex = frameStartIndex + frameStartIndex + messageNameLength;
                Array.Copy(buffer, srcStartIndex, msgData, 0, msgDataLength);
            }

            return new CommMessage(msgName, msgData);
        }

        public static int GetFrameLength(CommMessage message)
        {
            Debug.Assert(message != null);

            return message.bufferLength + ChecksumFieldLength + LengthFieldLength;
        }
    }
}
