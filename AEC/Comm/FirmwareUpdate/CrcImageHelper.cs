using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace AEC.Comm.FirmwareUpdate
{
    public class CrcImageHelper
    {
        public const string ChipKeyword = "BK3288";
        public const string EnvAddrKeyword = "BK328801";
        private const int SIZE_4K = 0x1000;
        private const int SIZE_32K = 0x8000; 
        private const int TLV_BUFFER_SIZE = 0x1000;

        public const int ENV_ADDR_OFFSET1 = 0x5000;
        public const int ENV_ADDR_OFFSET2 = 0x2000;

        private const ushort INVALID_TLV_TYPE = 0xffff;
        private const int MIN_TLV_LENGTH = sizeof(ushort) * 2 + 1;

        public const ushort TLV_TYPE_BASE_INFO = 0xba10;
        public const ushort TLV_TYPE_CFG_TOTAL_V1 = 0xCF01;
        public const ushort TLV_TYPE_CFG_BT_NAME = 0xCF10;
        public const ushort TLV_TYPE_CFG_BT_ADDR = 0xCF11;

        public static byte[] GetImageData(string  filePath)
        {
            Debug.Assert(!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath));

            try
            {
                using (var fileStream = System.IO.File.Open(filePath, System.IO.FileMode.Open))
                {
                    uint length = Utils.GetAlignSize((uint)fileStream.Length, SIZE_32K);

                    byte[] buffer = Utils.CreateByteArray(length);
                    fileStream.Read(buffer, 0, (int)fileStream.Length);

                    return buffer;
                }
            }
            catch (Exception e)
            {

            }

            return null;
        }

        public static DSPFirmwarePackage ParseFromFile(string filePath)
        {
            Debug.Assert(!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath));

            try
            {
                using (var fileStream = System.IO.File.Open(filePath, System.IO.FileMode.Open))
                {
                    //uint length = Utils.GetAlignSize((uint)fileStream.Length, SIZE_4K);

                    //byte[] buffer = Utils.CreateByteArray(length);
                    //fileStream.Read(buffer, 0, (int)fileStream.Length);

                    //int flashSize = GetFDL(buffer);
                    //if(flashSize == 0)
                    //{
                    //    return null;
                    //}

                    //int envAddr = AEC.Comm.FirmwareUpdate.CrcImageHelper.GetBK3288EnvAddr(buffer);
                    //if(envAddr == -1 || envAddr % SIZE_4K != 0 || envAddr + SIZE_4K > fileStream.Length) 
                    //{
                    //    return null;
                    //}

                    //var tlvType = AEC.Comm.FirmwareUpdate.CrcImageHelper.TLV_FindBaseInfo(buffer, envAddr);
                    //if (tlvType == null)
                    //{
                    //    return null;
                    //}

                    //var binBaseInfo = AEC.Comm.FirmwareUpdate.BinBaseInfo.ParseFromBuffer(tlvType.value);
                    //if(binBaseInfo.szSrcBin > envAddr || binBaseInfo.szWav > envAddr ||binBaseInfo.szSrcBin + binBaseInfo.szWav > envAddr)
                    //{
                    //    return null;
                    //}

                    //uint crc =  DSPFirmwareInfo.DefaultCRC;
                    
                    //DSPFirmwareInfo dspFirmwareInfo = new DSPFirmwareInfo();

                    //dspFirmwareInfo.flashSize = (uint)flashSize;
                    //dspFirmwareInfo.tpmAddress = (uint)(flashSize - SIZE_4K);
                    //dspFirmwareInfo.envAddress = (uint)envAddr;
                    //dspFirmwareInfo.envSize = (uint)SIZE_4K;

                    //crc = DSPFirmwareInfo.DefaultCRC;
                    //dspFirmwareInfo.envCrc = CrcImageHelper.CheckCRC(crc, buffer, envAddr, SIZE_4K);

                    //dspFirmwareInfo.exeSize = Utils.GetAlignSize((uint)binBaseInfo.szSrcBin, SIZE_4K);                    
                    //crc = DSPFirmwareInfo.DefaultCRC;
                    //dspFirmwareInfo.exeCrc = CrcImageHelper.CheckCRC(crc, buffer, 0, (int)dspFirmwareInfo.exeSize);

                    //if(binBaseInfo.szWav > 0)
                    //{
                    //    dspFirmwareInfo.wavSize = Utils.GetAlignSize((uint)binBaseInfo.szWav, SIZE_4K);
                    //    dspFirmwareInfo.wavAddress = (uint)envAddr - dspFirmwareInfo.wavSize;

                    //    crc = DSPFirmwareInfo.DefaultCRC;
                    //    dspFirmwareInfo.wavCrc = CrcImageHelper.CheckCRC(crc, buffer, (int)dspFirmwareInfo.wavAddress, (int)dspFirmwareInfo.wavSize);                        
                    //}

                    DSPFirmwarePackage dsp_fw_package = ParseFromFile(fileStream);

                    dsp_fw_package.dspFirmwareInfo.lastModifiedTimeStamp = Utils.GetFileModifiedTimeStamp(filePath);

                    dsp_fw_package.dspFirmwareInfo.fileName = new System.IO.FileInfo(filePath).Name;

                    dsp_fw_package.dspFirmwareInfo.isEfuseEncrypted = BitConverter.ToUInt32(dsp_fw_package.dspFirmwareData, 0) != 0;

                    return dsp_fw_package;
                }
            }
            catch(Exception e)
            {

            }

            return null;
        }

        public static DSPFirmwarePackage ParseFromFile(Stream stream)
        {
            Debug.Assert(stream != null);

            try
            {
                uint length = Utils.GetAlignSize((uint)stream.Length, SIZE_4K);

                byte[] buffer = Utils.CreateByteArray(length);
                stream.Read(buffer, 0, (int)stream.Length);

                int flashSize = GetFDL(buffer);
                if (flashSize == 0)
                {
                    return null;
                }

                int envAddr = GetBK3288EnvAddr(buffer);
                if (envAddr == -1 || envAddr % SIZE_4K != 0 || envAddr + SIZE_4K > stream.Length)
                {
                    return null;
                }

                var tlvType = TLV_FindBaseInfo(buffer, envAddr);
                if (tlvType == null)
                {
                    return null;
                }

                var binBaseInfo = AEC.Comm.FirmwareUpdate.BinBaseInfo.ParseFromBuffer(tlvType.value);
                if (binBaseInfo.szSrcBin > envAddr || binBaseInfo.szWav > envAddr || binBaseInfo.szSrcBin + binBaseInfo.szWav > envAddr)
                {
                    return null;
                }

                uint crc = DSPFirmwareInfo.DefaultCRC;

                DSPFirmwareInfo dspFirmwareInfo = new DSPFirmwareInfo();

                dspFirmwareInfo.flashSize = (uint)flashSize;
                dspFirmwareInfo.tpmAddress = (uint)(flashSize - SIZE_4K);
                dspFirmwareInfo.envAddress = (uint)envAddr;
                dspFirmwareInfo.envSize = (uint)SIZE_4K;

                crc = DSPFirmwareInfo.DefaultCRC;
                dspFirmwareInfo.envCrc = CrcImageHelper.CheckCRC(crc, buffer, envAddr, SIZE_4K);

                dspFirmwareInfo.exeSize = Utils.GetAlignSize((uint)binBaseInfo.szSrcBin, SIZE_4K);
                crc = DSPFirmwareInfo.DefaultCRC;
                dspFirmwareInfo.exeCrc = CrcImageHelper.CheckCRC(crc, buffer, 0, (int)dspFirmwareInfo.exeSize);

                if (binBaseInfo.szWav > 0)
                {
                    dspFirmwareInfo.wavSize = Utils.GetAlignSize((uint)binBaseInfo.szWav, SIZE_4K);
                    dspFirmwareInfo.wavAddress = (uint)envAddr - dspFirmwareInfo.wavSize;

                    crc = DSPFirmwareInfo.DefaultCRC;
                    dspFirmwareInfo.wavCrc = CrcImageHelper.CheckCRC(crc, buffer, (int)dspFirmwareInfo.wavAddress, (int)dspFirmwareInfo.wavSize);
                }

                dspFirmwareInfo.isEfuseEncrypted = BitConverter.ToUInt32(buffer, 0) != 0;

                return new DSPFirmwarePackage(dspFirmwareInfo, buffer);
            }
            catch (Exception e)
            {

            }

            return null;
        }


        static uint crc_table(uint index)
        {
            uint c = index;
            uint poly = 0xedb88320;

            for (int k = 0; k < 8; k++)
                c = ((c & 1) != 0) ? poly ^ (c >> 1) : c >> 1;

            return c;
        }

        public static uint CheckCRC(uint crc, byte[] buffer, int index, int length)
        {
            Debug.Assert(buffer != null && buffer.Length >= index + length);

            for(int i = index; i < index + length; ++i)
            {
                crc = crc_table((crc ^ (buffer[i])) & 0xff) ^ (crc >> 8);
            }

            return crc;
        }

        public static int GetFDL(byte[] data)
        {
            int select = 0;
            if(Utils.CheckBinaryData(data, 0x100, ChipKeyword))
            {
                select = data[0x109];
            }
            else if (Utils.CheckBinaryData(data, 0x110, ChipKeyword))
            {
                select = data[0x119];
            }
            else
            {
                return 0;
            }


            switch(select)
            {
                case 1:
                    return (0x80000);

                case 2:
                    return (0x100000);

                case 3:
                    return (0x200000);

                case 4:
                    return (0x400000);

                default:
                    return (0);
            }
        }

        public static int GetBK3288EnvAddr(byte[] data)
        {
            int size = GetFDL(data);
            if(size == 0)
            {
                return -1;
            }

            if (Utils.CheckBinaryData(data, size - ENV_ADDR_OFFSET1, EnvAddrKeyword))
            {
                return size - ENV_ADDR_OFFSET1;
            }
            else if (Utils.CheckBinaryData(data, size - ENV_ADDR_OFFSET2, EnvAddrKeyword))
            {
                return size - ENV_ADDR_OFFSET2;
            }
            else
            {
                return -1;
            }
        }

        public static TLV_Type TLV_FindBaseInfo(byte[] buffer, int startIndex)
        {
            return TLV_Find(TLV_TYPE_BASE_INFO, buffer, startIndex);
        }

        public static TLV_Type TLV_BTDeviceAddr(byte[] buffer, int startIndex)
        {
            return TLV_Find(TLV_TYPE_CFG_BT_ADDR, buffer, startIndex);
        }

        public static TLV_Type TLV_BTDeviceName(byte[] buffer, int startIndex)
        {
            return TLV_Find(TLV_TYPE_CFG_BT_NAME, buffer, startIndex);
        }

        public static bool TLV_Modify(byte[] buffer, int startIndex, ushort TLV_targetType, byte[] data)
        {
            Debug.Assert(buffer != null && buffer.Length > 0);

            byte[] tempBuffer = new byte[buffer.Length];
            Array.Copy(buffer, tempBuffer, buffer.Length);

            int index = TLV_GetFirst(buffer, startIndex);
            if (index < 0)
            {
                return false;
            }

            for (int i = index; i < buffer.Length; ++i)
            {
                buffer[i] = 0xff;
            }

            int tlvIndex = index;
            int tempIndex = index;

            while (true)
            {
                if (TLV_IsEnd(tempBuffer, tempIndex))
                {
                    return true;
                }

                TLV_Type currTLV = ParseFormBuffer(tempBuffer, tempIndex);

                if (currTLV == null)
                {
                    return false;
                }

                if (currTLV.type != TLV_targetType)  //如果当前TLV不是目标TLV,则直接拷贝数据
                {
                    if (tlvIndex + currTLV.size > buffer.Length)    //超出范围
                    {
                        return false;
                    }

                    Array.Copy(tempBuffer, tempIndex, buffer, tlvIndex, currTLV.size);

                    tlvIndex += currTLV.size;
                }
                else
                {
                    TLV_Type targetTLV = new TLV_Type();
                    targetTLV.type = currTLV.type;
                    targetTLV.length = (ushort)data.Length;
                    targetTLV.value = data;

                    if (tlvIndex + targetTLV.size > buffer.Length)    //超出范围
                    {
                        return false;
                    }

                    byte[] newTLV = targetTLV.ToArrayBytes();

                    Array.Copy(newTLV, 0, buffer, tlvIndex, targetTLV.size);

                    tlvIndex += targetTLV.size;
                }

                tempIndex += currTLV.size;
            }
        }

        public static TLV_Type ParseFormBuffer(byte[] buffer, int index)
        {
            if (TLV_IsEnd(buffer, index))
            {
                return null;
            }

            TLV_Type tlv = new TLV_Type();
            tlv.type = BitConverter.ToUInt16(buffer, index);
            tlv.length = BitConverter.ToUInt16(buffer, index + sizeof(ushort));
            tlv.value = new byte[tlv.length];
            Array.Copy(buffer, index + sizeof(ushort) * 2, tlv.value, 0, tlv.length);

            return tlv;
        }

        public static bool TLV_IsEnd(byte[] buffer, int index)
        {
            Debug.Assert(buffer != null);

            if (index + MIN_TLV_LENGTH >= buffer.Length)
            {
                return true;
            }

            ushort type = BitConverter.ToUInt16(buffer, index);
            if (type == 0 || type == INVALID_TLV_TYPE)
            {
                return true;
            }

            ushort length = BitConverter.ToUInt16(buffer, index + sizeof(ushort));

            if (buffer.Length < (int)(index + sizeof(short) + sizeof(short) + length))
            {
                return true;
            }

            return false;
        }

        public static TLV_Type TLV_Find(ushort tlvType, byte[] buffer, int startIndex)
        {
            //ushort tlvType = TLV_TYPE_BASE_INFO;
            int index = TLV_GetFirst(buffer, startIndex);

            if(index < 0)
            {
                return null;
            }

            int tlv0Index = index;
            TLV_Type tlv0 = new TLV_Type();
            tlv0.type = BitConverter.ToUInt16(buffer, tlv0Index);
            tlv0.length = BitConverter.ToUInt16(buffer, tlv0Index + sizeof(ushort));
            tlv0.value = new byte[tlv0.length];
            Array.Copy(buffer, tlv0Index + sizeof(ushort) * 2, tlv0.value, 0, tlv0.length);

            if (tlv0.type == tlvType)
            {
                return tlv0;
            }

            while(true)
            {
                TLV_Type next_tlv = new TLV_Type();
                int nextIndex = tlv0Index + sizeof(ushort) * 2 + tlv0.length;
                if(nextIndex - startIndex >= TLV_BUFFER_SIZE - 5)
                {
                    return null;
                }

                next_tlv.type = BitConverter.ToUInt16(buffer, nextIndex);
                next_tlv.length = BitConverter.ToUInt16(buffer, nextIndex + sizeof(ushort));
                next_tlv.value = new byte[next_tlv.length];
                Array.Copy(buffer, nextIndex + sizeof(ushort) * 2, next_tlv.value, 0, next_tlv.length);

                if (next_tlv.type == tlvType)
                {
                    return next_tlv;
                }

                tlv0Index = nextIndex;
                tlv0 = next_tlv;
            }
        }

        public static int TLV_GetFirst(byte[] buffer, int startIndex)
        {
            int index = FindFromBuffer(buffer, startIndex, TLV_BUFFER_SIZE, EnvAddrKeyword);
            if (index < 0 || index + Encoding.ASCII.GetBytes(EnvAddrKeyword).Length >= buffer.Length)
            {
                return -1;
            }

            return index + Encoding.ASCII.GetBytes(EnvAddrKeyword).Length;
        }

        
        public static int FindFromBuffer(byte[] buffer, int startIndex, int size, string target)
        {
            byte[] tBuffer = Encoding.ASCII.GetBytes(target);

            for (int i = startIndex; i < startIndex + size - tBuffer.Length && i < (buffer.Length - tBuffer.Length); ++i)
            {
                if(Utils.CompareByteArray(buffer, i, tBuffer))
                {
                    return i;
                }
            }

            return -1;
        }
    }

    public class TLV_Type
    {
        public ushort type { get; set; }
        public ushort length { get; set; }
        public byte[] value { get; set; }

        public int size
        {
            get
            {
                return sizeof(ushort) + sizeof(ushort) + value.Length;
            }
        }

        public byte[] ToArrayBytes()
        {
            byte[] buffer = new byte[this.size];

            Utils.FillUint16ToBuffer(buffer, sizeof(ushort) * 0, this.type);
            Utils.FillUint16ToBuffer(buffer, sizeof(ushort) * 1, this.length);
            Array.Copy(this.value, 0, buffer, sizeof(ushort) * 2, this.value.Length);

            return buffer;
        }
    };

    public class BinBaseInfo
    {
        public uint szSrcBin { get; set; }      //程序文件在flash中的长度
        public uint szWav { get; set; }
        public ushort szCfg { get; set; }
        public uint crcSrcBin { get; set; }
        public ushort pid { get; set; }

        public static BinBaseInfo ParseFromBuffer(byte[] data)
        {
            Debug.Assert(data != null && data.Length >= 28);

            int index = 0;
            BinBaseInfo baseInfo = new BinBaseInfo()
            {
                szSrcBin = BitConverter.ToUInt32(data, sizeof(uint) * (index++)),
                szWav = BitConverter.ToUInt32(data, sizeof(uint) * (index++)),
                szCfg = BitConverter.ToUInt16(data, sizeof(uint) * (index++)),
                crcSrcBin = BitConverter.ToUInt32(data, sizeof(uint) * (index++)),
                pid = BitConverter.ToUInt16(data, sizeof(uint) * (index++)),
            };

            return baseInfo;
        }
    };
}
