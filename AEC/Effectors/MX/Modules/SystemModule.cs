using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Common;
using AEC.Comm.MXComm;

namespace AEC.Effectors.MX.Modules
{
    public class SystemModule : SystemModuleBase
    {
        public SystemModule(EventBus effectorEvent_) :
            base(MXCommProtocol.systemModuleIndex, effectorEvent_)
        {
        }
    }
}
