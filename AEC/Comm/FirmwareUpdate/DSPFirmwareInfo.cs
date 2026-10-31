using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm.FirmwareUpdate
{
    public class DSPFirmwareInfo
    {
        public const uint DefaultAddress = 0xffffffff;
        public const uint DefaultCRC = 0xffffffff;
        public const uint DefEnvSize = 0x1000;

        public const int MaxFileNameSize = 128;
        public const int MaxCommentsSize = 128;

        public uint flashSize { get; set; }
        public uint exeSize { get; set; }
        public uint exeCrc { get; set; }

        public uint wavAddress { get; set; }
        public uint wavSize { get; set; }
        public uint wavCrc { get; set; }

        public uint envAddress { get; set; }
        public uint envSize { get; set; }
        public uint envCrc { get; set; }

        public uint tpmAddress { get; set; }
        public uint lastModifiedTimeStamp { get; set; }

        public bool isTpmEncrypted { get; set; }
        public bool isEfuseEncrypted { get; set; }

        public uint version { get; set; }

        public string fileName { get; set; }        
        public string comments { get; set; }

        public DSPFirmwareInfo()
        {
            this.Reset();
        }

        public void Reset()
        {
            this.flashSize = 0;
            this.exeSize = 0;
            this.exeCrc = DefaultCRC;
        
            this.wavAddress = DefaultAddress;
            this.wavSize = 0;
            this.wavCrc = DefaultCRC;

            this.envAddress = DefaultAddress;
            this.envSize = DefEnvSize;
            this.envCrc = DefaultCRC;

            this.tpmAddress = DefaultAddress;
            this.lastModifiedTimeStamp = 0;

            this.isTpmEncrypted = true;
            this.isEfuseEncrypted = false;

            this.version = 0;

            this.fileName = string.Empty;
            this.comments = string.Empty;
        }

        public void SyncData(DSPFirmwareInfo other)
        {
            this.version = other.version;
            this.isTpmEncrypted = other.isTpmEncrypted;
            this.lastModifiedTimeStamp = other.lastModifiedTimeStamp;
            this.fileName = other.fileName;
            this.comments = other.comments;
        }

        public static int GetSize()
        {
            return 12 + 12 + 12 + 8 + 8 + 4 + MaxFileNameSize + MaxCommentsSize;
        }
    }
}
