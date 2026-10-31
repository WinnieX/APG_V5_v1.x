using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.RemoteControl.Protocol
{
    public class CommMessage
    {
        public readonly string name;
        public readonly byte[] data;

        public CommMessage(string name_, byte[] data_ = null)
        {
            Debug.Assert(!string.IsNullOrEmpty(name_));

            this.name = name_;
            this.data = data_;
        }

        public List<byte> ToByteBuf()
        {
            List<byte> buffer = new List<byte>();
            buffer.AddRange(BufferLengthHelper.EncodeToDualByte(name.Length));
            buffer.AddRange(System.Text.Encoding.UTF8.GetBytes(name));
            if (this.data != null)
            {
                buffer.AddRange(data);
            }
            
            return buffer;
        }

        public int bufferLength
        {
            get
            {
                int length = sizeof(ushort) + System.Text.Encoding.UTF8.GetBytes(name).Length;
                if(data != null)
                {
                    length += data.Length;
                }

                return length;
            }
        }

        public void Print()
        {
            AEC.Tools.LogHelper.WriteInfo("Message name: {0}, data: {1}", this.name, data != null ? AEC.Core.Utils.ByteArrayToHexStr(data) : "null");
        }
    }
}
