using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm.CentralControl
{
    public class CentralControlCmd
    {
        public readonly CentralControlProtocol.Commmand cmdType;
        public readonly byte cmdPayload;

        public CentralControlCmd(CentralControlProtocol.Commmand cmdType_, byte cmdPayload_)
        {
            this.cmdType = cmdType_;
            this.cmdPayload = cmdPayload_;
        }
    }
}
