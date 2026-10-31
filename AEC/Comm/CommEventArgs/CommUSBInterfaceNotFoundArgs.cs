using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;

namespace AEC.Comm.CommEventArgs
{
    public class CommUSBInterfaceNotFoundArgs : MEventBusArgs
    {
        public const string CommUSBInterfaceNotFound = "CommUSBInterfaceNotFound";
        public CommUSBInterfaceNotFoundArgs()
            : base(CommUSBInterfaceNotFound)
        {
        }
    }
}
