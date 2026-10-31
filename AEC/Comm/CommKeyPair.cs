using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm
{
    public class CommKeyPair
    {
        public byte key1
        {
            get;
            private set;
        }

        public byte key2
        {
            get;
            private set;
        }

        public CommKeyPair()
        {
            this.key1 = 0;
            this.key2 = 0;
        }

        public void Update(short key)
        {
            key1 = (byte)(key & 0xff);
            key2 = (byte)((key >> 8) & 0xff);
        }
    }
}
