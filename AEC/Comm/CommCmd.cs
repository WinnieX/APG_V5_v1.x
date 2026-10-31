using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm
{
    public enum CmdType
    {
        Normal = 0,
        ReadSystemStatus,
        UpdateKey,
        Heartbeat,
    }

    public class CommCmd
    {
        public delegate void VoidCallback(object obj);

        public const int defWaitTime = CommServiceBase.MCommWaitTimeUnit * 1;

        public byte[] cmd;
        public readonly int identity;
        public readonly CmdType type;
        public readonly int commWaitTime;
        public readonly short param;

        public readonly VoidCallback StartSendCmd;
        public readonly VoidCallback SendCmdComplete;
        public readonly object context;

        public CommCmd(byte[] cmd_, int identity_, CmdType type_ = CmdType.Normal, short param_ = 0, int commWaitTime_ = defWaitTime, VoidCallback StartSendCmd_ = null, VoidCallback SendCmdComplete_ = null, object context_ = null)
        {
            cmd = cmd_;
            identity = identity_;
            type = type_;
            param = param_;
            commWaitTime = commWaitTime_;
            this.StartSendCmd = StartSendCmd_;
            this.SendCmdComplete = SendCmdComplete_;
            this.context = context_;
        }
    }
}
