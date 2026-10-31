using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RTA.Core
{
    class BufferedWaveProvider
    {
        private readonly int bufferLength;
        private CircularBuffer<byte> circularBuffer;
        public BufferedWaveProvider(int bufferLength_ = 65536)
        {
            bufferLength = bufferLength_;
        }

        public int BufferedBytes
        {
            get 
            {
                if (circularBuffer == null)
                {
                    return 0;
                }

                return circularBuffer.Count;
            }
        }

        public void AddSamples(byte[] buffer, int offset, int count)
        {
            // create buffer here to allow user to customise buffer length
            if (this.circularBuffer == null)
            {
                this.circularBuffer = new CircularBuffer<byte>(this.bufferLength);
            }

            int written = this.circularBuffer.Write(buffer, offset, count);
            
            if (written < count)
            {
                throw new InvalidOperationException("Buffer full");
            }
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            //return this.circularBuffer.Read(buffer, offset, count);
            int read = 0;
            if (this.circularBuffer != null) // not yet created
            {
                read = this.circularBuffer.Read(buffer, offset, count);
            }
            if (read < count)
            {
                // zero the end of the buffer
                Array.Clear(buffer, offset + read, count - read);
            }
            return count;
        }
    }
}
