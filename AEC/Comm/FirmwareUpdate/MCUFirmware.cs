using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm.FirmwareUpdate
{
    public class MCUFirmware
    {
        public const UInt32 DefaultVersion = 0xffffffff;

        public string prototype { get; set; }
        public uint version { get; set; }
        public uint buildNumber { get; set; }

        public MCUFirmware()
        {
            this.prototype = string.Empty;
            this.version = 0;
            this.buildNumber = 0;
        }

        public string GetPrototypeAndVersion()
        {
            return string.Format("{0}_v{1}", prototype, Version2String(this.version));
        }

        public static string Version2String(UInt32 var)
        {
            if (var == DefaultVersion)
            {
                return string.Empty;
            }

            byte[] data = BitConverter.GetBytes(var);
            byte version_major = data[3];
            byte version_minor = data[2];
            UInt16 version_patch = BitConverter.ToUInt16(data, 0);
            return string.Format("{0}.{1}.{2}", version_major, version_minor, version_patch);
        }
    }
}
