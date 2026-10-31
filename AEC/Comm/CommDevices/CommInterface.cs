using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm.CommDevices
{
    public enum CommInterfaceType
    {
        USB,
        RS232,
        UDP,
        TCP,
    };

    public class CommInterfaceBase
    {
        public readonly CommInterfaceType commInterfaceType;

        public CommInterfaceBase(CommInterfaceType commInterfaceType_)
        {
            this.commInterfaceType = commInterfaceType_;
        }
    }

    public class USBCommInterface:CommInterfaceBase
    {
        public readonly string portName;
        public USBCommInterface(string portName_)
            : base(CommInterfaceType.USB)
        {
            this.portName = portName_;
        }
    }

    public class RS232CommInterface : CommInterfaceBase
    {
        public readonly string portName;
        public readonly int baudRate;

        public RS232CommInterface(string portName_, int baudRate_)
            : base(CommInterfaceType.RS232)
        {
            this.portName = portName_;
            this.baudRate = baudRate_;
        }
    }

    public class NetWrokCommInterface : CommInterfaceBase
    {
        public readonly System.Net.IPAddress ipAddress;
        public readonly int remotePort;

        public NetWrokCommInterface(CommInterfaceType type, System.Net.IPAddress ipAddress_, int remotePort_)
            : base(type)
        {
            this.ipAddress = ipAddress_;
            this.remotePort = remotePort_;
        }
    }
}
