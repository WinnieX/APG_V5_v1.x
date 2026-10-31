using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm.FirmwareUpdate
{
    public class DspProgrammerDownloadParams
    {
        public const uint KeySize = 0x10;
        public const uint KeyHashSize = 0x20;

        public const uint MAX_CRC_BIN_FILE_SIZE = 0x200000;

        public const int MaxRemarkLength = 64;

        public byte[] key { get; private set; }
        public byte[] keyHash { get; private set; }
        public uint efuseControl { get; set; }	//版本
        public uint version{get;set;}	//版本
        public uint timeStamp { get; set; }	//时间戳
        //public uint retries { get; set; }	//重试次数
        //public uint dspPowerResetDuration { get; set; }//电源复位时间
        //public uint dspBaudRate { get; set; }	//波特率
        public uint crcBinFileLength { get; set; }	//文件长度
        public uint crcBinSize { get; set; }	//bin文件size
        public uint envAddr { get; set; }		//envAddr
        public uint binCrc { get; set; }		//bin文件crc值
        public string remarks { get; set; }     //说明

        public void SetKey(byte[] data, int index)
        {
            System.Diagnostics.Debug.Assert(data != null && KeySize + index <= data.Length);

            if (this.key == null)
            {
                this.key = new byte[KeySize];
            }

            Array.Copy(data, index, this.key, 0, KeySize);
        }

        public void SetKeyHash(byte[] data, int index)
        {
            System.Diagnostics.Debug.Assert(data != null && KeyHashSize + index <= data.Length);

            if (this.keyHash == null)
            {
                this.keyHash = new byte[KeyHashSize];
            }

            Array.Copy(data, index, this.keyHash, 0, KeyHashSize);
        }

        public DspProgrammerDownloadParams()
        {
            this.remarks = string.Empty;
        }

        public static int GetSize()
        {
            return  16 + 32 + 4 + 40 + MaxRemarkLength;
        }

        public void CopyFrom(DspProgrammerDownloadParams other)
        {
            this.SetKey(other.key, 0);
            this.SetKeyHash(other.keyHash, 0);

            this.version = other.version;
            this.timeStamp = other.timeStamp;
            //this.retries = other.retries;
            //this.dspPowerResetDuration = other.dspPowerResetDuration;
            //this.dspBaudRate = other.dspBaudRate;
            this.crcBinFileLength = other.crcBinFileLength;
            this.crcBinSize = other.crcBinSize;
            this.envAddr = other.envAddr;
            this.binCrc = other.binCrc;
            this.remarks = other.remarks;
        }
    }
}
