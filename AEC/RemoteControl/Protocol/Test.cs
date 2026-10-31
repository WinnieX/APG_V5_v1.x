using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RemoteControl.Protocol
{
    class Test
    {
        public static void TestFrameEncoderAndFrameDecoder()
        {
            string name = "handshake";
            byte[] data = null;
            CommMessage msg1 = new CommMessage(name, data);
            CommMessage msg2 = FrameDecoder.Decode(FrameEncoder.Encode(msg1));
            Console.WriteLine(msg2.name);
        }
    }
}
