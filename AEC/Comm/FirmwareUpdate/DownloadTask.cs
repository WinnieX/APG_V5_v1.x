using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm.FirmwareUpdate
{
    public class DownloadTask
    {
        public string CrcImageFilePath
        {
            get;
            set;
        }


        public string CommPortName
        {
            get;
            set;
        }
        public uint CommBaudRate
        {
            get;
            set;
        }

        public byte[] fileBuffer
        {
            get;
            set;
        }

        public DownloadTask()
        {
            this.CommBaudRate = FirmwareUpdateService.DefaultDSPBaudRate;
        }
    }
}
