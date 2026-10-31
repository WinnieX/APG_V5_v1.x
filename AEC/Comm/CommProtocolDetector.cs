using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm
{
    public class CommProtocolDetector
    {
        public delegate CommCmdType CheckCommCmdTypeDelegate(byte[] data);
        public delegate bool CheckResponseIsCompleteDelegate(List<byte> buffer);

        public readonly CheckCommCmdTypeDelegate checkCommCmdType;
        public readonly CheckResponseIsCompleteDelegate checkReadResponseIsCompleted;
        public readonly CheckResponseIsCompleteDelegate checkWriteResponseIsCompleted;

        public CommProtocolDetector(
            CheckCommCmdTypeDelegate checkCommCmdType_,
            CheckResponseIsCompleteDelegate checkReadResponseIsCompleted_,
            CheckResponseIsCompleteDelegate checkWriteResponseIsCompleted_)
        {
            this.checkCommCmdType = checkCommCmdType_;
            this.checkReadResponseIsCompleted = checkReadResponseIsCompleted_;
            this.checkWriteResponseIsCompleted = checkWriteResponseIsCompleted_;
        }
    }
}
