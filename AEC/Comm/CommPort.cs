using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm
{
    public abstract class CommPort
    {
        public readonly CommPortType type;

        public abstract string displayName
        {
            get;
        }

        public CommPort(CommPortType type)
        {
            this.type = type;
        }
    }

    public class HIDCommPort : CommPort
    {
        public readonly string devicePath;
        public readonly string serialNumber;

        public HIDCommPort(string devicePath_, string serialNumber_) :
            base(CommPortType.USB)
        {
            this.devicePath = devicePath_;
            this.serialNumber = serialNumber_;
;
        }

        public override string displayName
        {
            get
            {
                return GetUSBHIDPortName(this.serialNumber);
            }
        }

        public static string GetUSBHIDPortName(string serialNumber)
        {
            if (!string.IsNullOrEmpty(serialNumber))
            {
                string sn = string.Empty;
                if (serialNumber.Length > 4)
                {
                    sn = serialNumber.Substring(0, 4);
                }
                else
                {
                    sn = serialNumber;
                }

                return string.Format("USB : HID_{0}", sn);
            }
            else
            {
                return "USB : HID";
            }
        }
    }

    public class RS232CommPort : CommPort
    {
        public readonly string portName;

        public int baudRate
        {
            get;
            set;
        }

        public bool isAutoBaudRate
        {
            get
            {
                return this.baudRate == 0;
            }
        }

        public RS232CommPort(string portName_) :
            base(CommPortType.RS232)
        {
            this.portName = portName_;
        }

        public override string displayName
        {
            get
            {
                return string.Format("RS232 : {0}", this.portName);
            }
        }
    }
}
