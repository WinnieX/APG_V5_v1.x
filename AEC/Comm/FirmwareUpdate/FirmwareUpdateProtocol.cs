using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Comm.FirmwareUpdate
{
    public class FirmwareUpdateProtocol
    {
        public enum FUCommmand_V1 : byte
        {
            Handshake = 0x00,
            //ReadState = 0x00,               //读取状态以及版本
            //ReadRegister = 0x01,            //读寄存器
            //WriteRegister = 0x02,           //写寄存器
            //SetBaudRate = 0x03,               //设置波特率
            //DSPPowerControl = 0x04,         //DSP电源控制
            //ClearDSPRecvBuffer = 0x05,      //清除DSP接收缓冲区

            //DSP编程相关
            DSPPreDownload = 0x10,          //预下载
            DSPDownload = 0x11,             //下载
            DSPFlashCheckCRC = 0x12,        //检测CRC
            DSPEndDownload = 0x13,          //完成下载

            //MCU编程相关
            MCUDownload = 0x20,               //下载
            MCUCheckFlashCRC = 0x21,          //校验CRC

            MCUExternalFlashDownload = 0x30,
            MCUExternalFlashUpload = 0x31,
            WriteDSPFirmwareInfo = 0x32,
            ReadDSPFirmwareInfo = 0x33,
            WriteEfuseKeyHash = 0x34,
            ReadEfuseKeyHash = 0x35,
            ReadMCUUID = 0x36,

            //APP中需要处理的命令
            APPJumpToBootloader = 0xA0,     //APP跳转到Bootloader
            APPGetFirmwareCode = 0xA1,      //APP中获取产品固件代号
            APPGetVersion = 0xA1,           //APP中获取版本
            APPGetDSPVersionAndId = 0xA2,   //获取DSP固件版本和id
            APPHardwareOutputMute = 0xA3,   //控制硬件输出静音

            //Bootloader中需要处理的命令
            BootloaderJumpToAPP = 0xB0,     //Bootloader跳转到APP
            //BootloaderGetVersion = 0xB1,    //Bootloader获取MCU固件版本
            //BootloaderGetDSPVersion = 0xB2, //Bootloader获取MCU固件版本
            BootloaderReadFlash = 0xB1,     //Bootloader读取Flash
            BootloaderUpdateLCDShow = 0xB2, //更新LCD显示
            BootloaderGetFirmwareFlag = 0xB3, //获取固件标志位
            BootloaderUpdateFirmwareFlag = 0xB4, //更新固件标志位

            DSPCommSetUartBaudRate = 0xD0,  //设置和DSP通信的UART波特率
            DSPPowerSupplyControl = 0xD1,   //DSP电源控制
            DSPCommDirectly = 0xD2,         //直接和DSP通信
            DSPLinkToBootROM = 0xD3,        //连接到DSPBootROM
            DSPWriteRegister = 0xD4,        //DSP写寄存器
            DSPReadRegister = 0xD5,         //DSP读寄存器
            DSPBootProtectFlash = 0xD6,     //启动保护Flash保护   
            DSPEraseFlash = 0xD7,           //擦除Flash
            //DSPWriteFlash4K = 0xD8,         //写入Flash
            DSPReadFlash4K = 0xD8,          //读取Flash
            //DSPGetFlashCheckCRC = 0xDA,     //检测FlashCRC
            DSPReboot = 0xD9,               //重启DSP

            //以下命令尽在bootloader中处理
            //LoadApp = 0xd0,     //从 bootloader 启动app 0xd0

            //以下命令仅在app中处理
            //MCUResetToBootloader = 0xe0,
            //ReadMCUPrototype = 0xe1,
            //ReadMCUVersion = 0xe2,
            //MCUClearPresetData = 0xe3,
            //MCUMuteControl = 0xe4,
        }

        public enum FUCommmand_V2 : byte
        {
            LoadMCUModel = 0x01,

            //DSP编程相关
            DSPLinkToBootROM = 0x10,        //连接到DSPBootROM
            DSPBootProtectFlash = 0x11,     //启动保护Flash保护   
            DSPEraseFlash = 0x12,           //擦除Flash
            DSPWriteFlash4K = 0x13,           //写入4k数据到Flash
            DSPCheckFlashCRC = 0x14,        //检测CRC
            DSPDownloadCompleted = 0x15,    //完成下载

            BootloaderJumpToAPP = 0xB0,     //Bootloader跳转到APP

            //DSPCommSetUartBaudRate = 0xD0,  //设置和DSP通信的UART波特率
            //DSPPowerSupplyControl = 0xD1,   //DSP电源控制
            //DSPCommDirectly = 0xD2,         //直接和DSP通信
            DSPWriteRegister = 0xD3,        //DSP写寄存器
            DSPReadRegister = 0xD4,         //DSP读寄存器            
            DSPReadFlash4K = 0xD5,          //读取Flash
            DSPReboot = 0xD6,               //重启DSP
        }

        protected enum CmdFrame : int
        {
            Header1Index = 0x00,
            Header2Index = 0x01,
            LengthIndex = 0x02,
            CmdIndex = 0x04,
            PayloadIndex = 0x05,
        };

        public enum DSPPreDownloadStatusCode : uint
        {
            IAP_DSP_PRE_DOWNLOAD_STATUS_OK = 0x0000,

            IAP_DSP_PRE_DOWNLOAD_STATUS_CMD_LENGTH_ERROR = 0x00000100,
            IAP_DSP_PRE_DOWNLOAD_STATUS_CMD_RETRIES_ERROR = 0x00000101,
            IAP_DSP_PRE_DOWNLOAD_STATUS_CMD_DSP_INDEX_ERROR = 0x00000102,
            IAP_DSP_PRE_DOWNLOAD_STATUS_CMD_DSP_POWER_RESET_DURATION_ERROR = 0x00000103,
            IAP_DSP_PRE_DOWNLOAD_STATUS_CMD_DSP_BAUDRATE_ERROR = 0x00000104,
            IAP_DSP_PRE_DOWNLOAD_STATUS_CMD_DSP_IMAGE_SIZE_ERROR = 0x00000105,
            IAP_DSP_PRE_DOWNLOAD_STATUS_CHECK_LINK_ERROR = 0x00000200,
            IAP_DSP_PRE_DOWNLOAD_STATUS_STAY_BOOT_ROM_ERROR = 0x00000201,
            IAP_DSP_PRE_DOWNLOAD_STATUS_GPIO15_PULL_UP_ERROR = 0x00000202,
            IAP_DSP_PRE_DOWNLOAD_STATUS_SET_BAUDRATE_ERROR = 0x00000203,
            IAP_DSP_PRE_DOWNLOAD_STATUS_GET_FLASH_MID_ERROR = 0x00000204,
            IAP_DSP_PRE_DOWNLOAD_STATUS_GET_FLASH_INFO_ERROR = 0x00000205,
            IAP_DSP_PRE_DOWNLOAD_STATUS_FLASH_BOOT_UNPROTECT_ERROR = 0x00000206,
            IAP_DSP_PRE_DOWNLOAD_STATUS_FLASH_ERASE_ERROR = 0x00000207,

            IAP_DSP_PRE_DOWNLOAD_STATUS_UNKONOWN_ERROR = 0xffffffff,
        };

        public enum LinkToDSPBootRomStatusCodeV2 : uint
        {
            IAP_DSP_LINK_TO_BOOTROM_STATUS_OK = 0x0000,

            IAP_DSP_LINK_TO_BOOTROM_STATUS_CMD_LENGTH_ERROR = 0x00000100,
            IAP_DSP_LINK_TO_BOOTROM_STATUS_CMD_DSP_INDEX_ERROR = 0x00000101,
            IAP_DSP_LINK_TO_BOOTROM_STATUS_CMD_DSP_BAUDRATE_ERROR = 0x00000102,
            IAP_DSP_LINK_TO_BOOTROM_STATUS_CHECK_LINK_ERROR = 0x00000200,
            IAP_DSP_LINK_TO_BOOTROM_STATUS_STAY_BOOT_ROM_ERROR = 0x00000201,
            IAP_DSP_LINK_TO_BOOTROM_STATUS_GPIO15_PULL_UP_ERROR = 0x00000202,
            IAP_DSP_LINK_TO_BOOTROM_STATUS_SET_BAUDRATE_ERROR = 0x00000203,
            IAP_DSP_LINK_TO_BOOTROM_STATUS_GET_FLASH_MID_ERROR = 0x00000204,

            IAP_DSP_LINK_TO_BOOTROM_STATUS_UNKONOWN_ERROR = 0xffffffff,
        };

        public enum DSPDownloadStatusCode : uint
        {
            IAP_DSP_DOWNLOAD_STATUS_OK = 0x0000,

            IAP_DSP_DOWNLOAD_STATUS_CMD_LENGTH_ERROR = 0x00001100,
            IAP_DSP_DOWNLOAD_STATUS_CMD_DSP_INDEX_ERROR = 0x00001101,
            IAP_DSP_DOWNLOAD_STATUS_CMD_ADDRESS_ERROR = 0x00001102,

            IAP_DSP_DOWNLOAD_STATUS_CMD_WRITE_FLASH_ERROR = 0x00001200,

            IAP_DSP_DOWNLOAD_STATUS_UNKONOWN_ERROR = 0xffffffff,
        };

        public enum DSPWriteFlashStatusCodeV2 : uint
        {
            IAP_DSP_WRITE_FLASH_STATUS_OK = 0x0000,

            IAP_DSP_WRITE_FLASH_STATUS_CMD_LENGTH_ERROR = 0x00001100,
            IAP_DSP_WRITE_FLASH_STATUS_CMD_DSP_INDEX_ERROR = 0x00001101,
            IAP_DSP_WRITE_FLASH_STATUS_CMD_ADDRESS_ERROR = 0x00001102,

            IAP_DSP_WRITE_FLASH_STATUS_CMD_WRITE_FLASH_ERROR = 0x00001200,

            IAP_DSP_WRITE_FLASH_STATUS_UNKONOWN_ERROR = 0xffffffff,
        };

        public enum DSPCheckFlashCRCStatusCode : uint
        {
            IAP_DSP_CHECK_FLASH_CRC_STATUE_OK = 0x0000,

            IAP_DSP_CHECK_FLASH_CRC_STATUE_CMD_LENGTH_ERROR = 0x00002100,
            IAP_DSP_CHECK_FLASH_CRC_STATUE_DSP_INDEX_ERROR = 0x00002101,

            IAP_DSP_CHECK_FLASH_CRC_STATUE_GET_CRC_ERROR = 0x00002200,

            IAP_DSP_CHECK_FLASH_CRC_STATUE_UNKONOWN_ERROR = 0xffffffff,
        };

        public enum DSPCheckFlashCRCStatusCodeV2 : uint
        {
            IAP_DSP_CHECK_FLASH_CRC_STATUE_OK = 0x0000,

            IAP_DSP_CHECK_FLASH_CRC_STATUE_CMD_LENGTH_ERROR = 0x00002100,
            IAP_DSP_CHECK_FLASH_CRC_STATUE_DSP_INDEX_ERROR = 0x00002101,

            IAP_DSP_CHECK_FLASH_CRC_STATUE_GET_CRC_ERROR = 0x00002200,
            IAP_DSP_CHECK_FLASH_CRC_STATUE_CHECK_CRC_ERROR = 0x00002201,

            IAP_DSP_CHECK_FLASH_CRC_STATUE_UNKONOWN_ERROR = 0xffffffff,
        };

        public enum DSPEndDownloadStatusCode : uint
        {
            IAP_DSP_END_DOWNLOAD_STATUS_OK = 0x0000,

            IAP_DSP_END_DOWNLOAD_STATUS_CMD_LENGTH_ERROR = 0x00003100,
            IAP_DSP_END_DOWNLOAD_STATUS_CMD_RETRIES_ERROR = 0x00003101,
            IAP_DSP_END_DOWNLOAD_STATUS_CMD_DSP_INDEX_ERROR = 0x00003102,

            IAP_DSP_END_DOWNLOAD_STATUS_FLASH_BOOT_PROTECT_ERROR = 0x00003200,


            IAP_DSP_END_DOWNLOAD_STATUS_UNKONOWN_ERROR = 0xffffffff,
        };

        public enum DSPDownloadCompletedStatusCodeV2 : uint
        {
            IAP_DSP_DOWNLOAD_COMPLETED_STATUS_OK = 0x0000,

            IAP_DSP_DOWNLOAD_COMPLETED_STATUS_CMD_LENGTH_ERROR = 0x00003100,
            IAP_DSP_DOWNLOAD_COMPLETED_STATUS_CMD_DSP_INDEX_ERROR = 0x00003101,

            IAP_DSP_DOWNLOAD_COMPLETED_STATUS_FLASH_BOOT_PROTECT_ERROR = 0x00003200,
            IAP_DSP_DOWNLOAD_COMPLETED_STATUS_REBOOT_ERROR = 0x00003201,
            
            IAP_DSP_DOWNLOAD_COMPLETED_STATUS_UNKONOWN_ERROR = 0xffffffff,
        };

        public enum ProgrammerMCUFlashDownloadStatusCode : uint
        {
            IAP_DSP_MCU_FLASH_DOWNLOAD_STATUS_OK = 0x0000,

            IAP_DSP_MCU_FLASH_DOWNLOAD_STATUS_CMD_LENGTH_ERROR = 0x00000100,
            IAP_DSP_MCU_FLASH_DOWNLOAD_STATUS_CMD_DSP_INDEX_ERROR = 0x00000101,
            IAP_DSP_MCU_FLASH_DOWNLOAD_STATUS_CMD_ADDRESS_ERROR = 0x00000102,

            IAP_DSP_MCU_FLASH_DOWNLOAD_STATUS_CMD_WRITE_FLASH_ERROR = 0x00000200,

            IAP_DSP_MCU_FLASH_DOWNLOAD_STATUS_UNKONOWN_ERROR = 0xffffffff,
        };

        public const int CHARS_NUMBER_OF_MCU_LCD_PER_LINE = 21;

        public const byte CmdHeader1 = 0xFD;
        public const byte CmdHeader2 = 0xCC;
        public const byte ResponseHeader1 = 0xFB;
        public const byte ResponseHeader2 = 0xEE;

        private const int Size1K = 0x0400;
        private const int Size4K = 0x1000;


        public static byte[] GetHandshakeCmd()
        {
            byte[] cmd = GetCmd(FUCommmand_V1.Handshake, 0);
            return cmd;
        }

        public static byte[] GetLoadMCUModelCmd()
        {
            byte[] cmd = GetCmd(FUCommmand_V2.LoadMCUModel, 0);
            return cmd;
        }

        //public static byte[] GetUpdateBaudRateCmd(uint baudRate, uint delay)
        //{
        //    byte[] cmd = GetCmd(FUCommmand.SetBaudRate, sizeof(uint) * 2);
        //    int index = (int)CmdFrame.PayloadIndex;
        //    index = FillUint32ToBuffer(cmd, index, baudRate);
        //    index = FillUint32ToBuffer(cmd, index, delay);

        //    return cmd;
        //}

        public static byte[] GetDSPPreDownloadCmd_V1(uint dspIndex, uint retries, uint powerResetDuration, uint dspBaudRate, uint imageBufferLength)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.DSPPreDownload, sizeof(uint) * 5);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, retries);
            index = FillUint32ToBuffer(cmd, index, powerResetDuration);
            index = FillUint32ToBuffer(cmd, index, dspBaudRate);
            index = FillUint32ToBuffer(cmd, index, imageBufferLength);

            return cmd;
        }

        public static byte[] GetLinkToDSPBootRomCmd_V2(uint dspIndex, uint retries, uint powerResetDuration, uint dspBaudRate)
        {
            byte[] cmd = GetCmd(FUCommmand_V2.DSPLinkToBootROM, sizeof(uint) * 4);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, retries);
            index = FillUint32ToBuffer(cmd, index, powerResetDuration);
            index = FillUint32ToBuffer(cmd, index, dspBaudRate);

            return cmd;
        }

        public static byte[] GetDSPEndDownloadCmd(uint retries, uint dspIndex)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.DSPEndDownload, sizeof(uint) * 2);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, retries);

            return cmd;
        }

        public static byte[] GetDSPDownloadCompletedCmd_V2(uint retries, uint dspIndex)
        {
            byte[] cmd = GetCmd(FUCommmand_V2.DSPDownloadCompleted, sizeof(uint) * 2);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, retries);

            return cmd;
        }

        public static byte[] GetDSPFlashCheckCRCCmd(uint dspIndex, uint addr, uint length)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.DSPFlashCheckCRC, sizeof(uint) * 3);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, addr);
            index = FillUint32ToBuffer(cmd, index, length);

            return cmd;
        }

        public static byte[] GetDSPCheckFlashCRCCmdV2(uint dspIndex, uint addr, uint length)
        {
            byte[] cmd = GetCmd(FUCommmand_V2.DSPCheckFlashCRC, sizeof(uint) * 3);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, addr);
            index = FillUint32ToBuffer(cmd, index, length);

            return cmd;
        }

        public static byte[] GetDSPDownload4kCmd(uint dspIndex, byte[] buffer, int bufferIndex, uint address)
        {
            Debug.Assert(buffer.Length >= address + Size4K);

            byte[] cmd = GetCmd(FUCommmand_V1.DSPDownload, sizeof(uint) + sizeof(uint) + Size4K);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, address);

            Array.Copy(buffer, bufferIndex, cmd, index, Size4K);

            return cmd;
        }

        public static byte[] GetDSPWriteFlash4kCmdV2(uint dspIndex, byte[] buffer, int bufferIndex, uint address)
        {
            Debug.Assert(buffer.Length >= bufferIndex + Size4K);

            byte[] cmd = GetCmd(FUCommmand_V2.DSPWriteFlash4K, sizeof(uint) + sizeof(uint) + Size4K);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, address);

            Array.Copy(buffer, bufferIndex, cmd, index, Size4K);

            return cmd;
        }

        public static byte[] GetDSPDownloadDataToMCUCmd(uint dspIndex, byte[] buffer, uint address, int length)
        {
            Debug.Assert(length > 0 && length % Size4K == 0 && buffer.Length >= address + length);

            byte[] cmd = GetCmd(FUCommmand_V1.MCUExternalFlashDownload, sizeof(uint) + sizeof(uint) + length);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, address);

            Array.Copy(buffer, address, cmd, index, length);

            return cmd;
        }

        public static byte[] GetWriteDSPFirmwareInfoCmd(DSPFirmwareInfo dspFirmwareInfo)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.WriteDSPFirmwareInfo, DSPFirmwareInfo.GetSize());
            int index = (int)CmdFrame.PayloadIndex;

            index = FillUint32ToBuffer(cmd, index, dspFirmwareInfo.flashSize);
            index = FillUint32ToBuffer(cmd, index, dspFirmwareInfo.exeSize);
            index = FillUint32ToBuffer(cmd, index, dspFirmwareInfo.exeCrc);

            index = FillUint32ToBuffer(cmd, index, dspFirmwareInfo.wavAddress);
            index = FillUint32ToBuffer(cmd, index, dspFirmwareInfo.wavSize);
            index = FillUint32ToBuffer(cmd, index, dspFirmwareInfo.wavCrc);

            index = FillUint32ToBuffer(cmd, index, dspFirmwareInfo.envAddress);
            index = FillUint32ToBuffer(cmd, index, dspFirmwareInfo.envSize);
            index = FillUint32ToBuffer(cmd, index, dspFirmwareInfo.envCrc);

            index = FillUint32ToBuffer(cmd, index, dspFirmwareInfo.tpmAddress);
            index = FillUint32ToBuffer(cmd, index, dspFirmwareInfo.lastModifiedTimeStamp);

            index = FillUint32ToBuffer(cmd, index, Utils.BoolToUint32(dspFirmwareInfo.isTpmEncrypted));
            index = FillUint32ToBuffer(cmd, index, Utils.BoolToUint32(dspFirmwareInfo.isEfuseEncrypted));

            index = FillUint32ToBuffer(cmd, index, dspFirmwareInfo.version);

            if(!string.IsNullOrEmpty(dspFirmwareInfo.fileName))
            {
                byte[] fileName = Encoding.UTF8.GetBytes(dspFirmwareInfo.fileName);
                int length = AEC.Core.Utils.ConstrainMax(fileName.Length, DSPFirmwareInfo.MaxFileNameSize);
                Array.Copy(fileName, 0, cmd, index, length);
            }

            index += DSPFirmwareInfo.MaxFileNameSize;

            if (!string.IsNullOrEmpty(dspFirmwareInfo.comments))
            {
                byte[] remarks = Encoding.UTF8.GetBytes(dspFirmwareInfo.comments);
                int length = AEC.Core.Utils.ConstrainMax(remarks.Length, DSPFirmwareInfo.MaxCommentsSize);
                Array.Copy(remarks, 0, cmd, index, length);
            }

            index += DSPFirmwareInfo.MaxCommentsSize;

            return cmd;
        }

        public static byte[] GetWriteDSPKeyHashCmd(byte[] keyHash)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.WriteEfuseKeyHash, keyHash.Length);
            int index = (int)CmdFrame.PayloadIndex;

            Array.Copy(keyHash, 0, cmd, index, keyHash.Length);
            index += keyHash.Length;

            return cmd;
        }

        public static byte[] GetReadMCUProgrammingParamsCmd()
        {
            byte[] cmd = GetCmd(FUCommmand_V1.ReadDSPFirmwareInfo, 0);

            return cmd;
        }

        public static byte[] GetReadDSPKeyHashCmd()
        {
            byte[] cmd = GetCmd(FUCommmand_V1.ReadEfuseKeyHash, 0);

            return cmd;
        }

        public static byte[] GetReadMCUUIDCmd()
        {
            byte[] cmd = GetCmd(FUCommmand_V1.ReadMCUUID, 0);

            return cmd;
        }

        public static byte[] GetDSPCommSetUartBaudRateCmd(uint dspIndex, uint baudRate)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.DSPCommSetUartBaudRate, sizeof(uint) * 2);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, baudRate);

            return cmd;
        }

        public static byte[] GetDSPPowerSupplyControlCmd(uint dspIndex, bool isOn)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.DSPPowerSupplyControl, sizeof(uint) * 2);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, BoolToUint(isOn));

            return cmd;
        }

        public static byte[] GetDSPCommDirectlyCmd(uint dspIndex, uint expectResponseLength, uint timeoutInMs, byte[] dspCommCmd)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.DSPCommDirectly, sizeof(uint) * 3 + dspCommCmd.Length);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, expectResponseLength);
            index = FillUint32ToBuffer(cmd, index, timeoutInMs);

            Array.Copy(dspCommCmd, 0, cmd, index, dspCommCmd.Length);

            return cmd;
        }

        public static byte[] GetDSPLinkToBootROMCmd(uint dspIndex, uint powerResetDuration, uint dspBaudRate)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.DSPLinkToBootROM, sizeof(uint) * 3);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, powerResetDuration);
            index = FillUint32ToBuffer(cmd, index, dspBaudRate);

            return cmd;
        }

        public static byte[] GetDSPWriteRegisterCmd(uint dspIndex, uint address, uint data)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.DSPWriteRegister, sizeof(uint) * 3);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, address);
            index = FillUint32ToBuffer(cmd, index, data);

            return cmd;
        }

        public static byte[] GetDSPWriteRegisterCmdV2(uint dspIndex, uint address, uint data)
        {
            byte[] cmd = GetCmd(FUCommmand_V2.DSPWriteRegister, sizeof(uint) * 3);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, address);
            index = FillUint32ToBuffer(cmd, index, data);

            return cmd;
        }

        public static byte[] GetDSPReadRegisterCmd(uint dspIndex, uint address)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.DSPReadRegister, sizeof(uint) * 2);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, address);

            return cmd;
        }

        public static byte[] GetDSPReadRegisterCmdV2(uint dspIndex, uint address)
        {
            byte[] cmd = GetCmd(FUCommmand_V2.DSPReadRegister, sizeof(uint) * 2);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, address);

            return cmd;
        }

        public static byte[] GetDSPBootProtectFlashCmd(uint dspIndex, bool isProtect)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.DSPBootProtectFlash, sizeof(uint) * 2);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, BoolToUint(isProtect));

            return cmd;
        }

        public static byte[] GetDSPBootProtectFlashCmdV2(uint dspIndex, bool isProtect)
        {
            byte[] cmd = GetCmd(FUCommmand_V2.DSPBootProtectFlash, sizeof(uint) * 2);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, BoolToUint(isProtect));

            return cmd;
        }

        public static byte[] GetDSPEraseFlashCmd(uint dspIndex, uint startAddress, uint size)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.DSPEraseFlash, sizeof(uint) * 3);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, startAddress);
            index = FillUint32ToBuffer(cmd, index, size);

            return cmd;
        }

        public static byte[] GetDSPEraseFlashCmdV2(uint dspIndex, uint startAddress, uint size)
        {
            byte[] cmd = GetCmd(FUCommmand_V2.DSPEraseFlash, sizeof(uint) * 3);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, startAddress);
            index = FillUint32ToBuffer(cmd, index, size);

            return cmd;
        }

        //public static byte[] GetDSPWriteFlash4KCmd(uint dspIndex, uint address, byte[] buffer, uint bufferIndex = 0)
        //{
        //    Debug.Assert(buffer != null && buffer.Length >= bufferIndex + Size4K);

        //    byte[] cmd = GetCmd(FUCommmand.DSPWriteFlash4K, sizeof(uint) * 2 + Size4K);
        //    int index = (int)CmdFrame.PayloadIndex;
        //    index = FillUint32ToBuffer(cmd, index, dspIndex);
        //    index = FillUint32ToBuffer(cmd, index, address);

        //    Array.Copy(buffer, bufferIndex, cmd, index, Size4K);

        //    return cmd;
        //}

        public static byte[] GetDSPReadFlash4KCmd(uint dspIndex, uint address)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.DSPReadFlash4K, sizeof(uint) * 2);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, address);

            return cmd;
        }

        public static byte[] GetDSPReadFlash4KCmdV2(uint dspIndex, uint address)
        {
            byte[] cmd = GetCmd(FUCommmand_V2.DSPReadFlash4K, sizeof(uint) * 2);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);
            index = FillUint32ToBuffer(cmd, index, address);

            return cmd;
        }

        //public static byte[] GetDSPGetFlashCheckCRCCmd(uint dspIndex, uint address, uint length)
        //{
        //    byte[] cmd = GetCmd(FUCommmand.DSPGetFlashCheckCRC, sizeof(uint) * 3);
        //    int index = (int)CmdFrame.PayloadIndex;
        //    index = FillUint32ToBuffer(cmd, index, dspIndex);
        //    index = FillUint32ToBuffer(cmd, index, address);
        //    index = FillUint32ToBuffer(cmd, index, length);

        //    return cmd;
        //}

        public static byte[] GetDSPRebootCmd(uint dspIndex)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.DSPReboot, sizeof(uint) * 1);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);

            return cmd;
        }

        public static byte[] GetDSPRebootCmdV2(uint dspIndex)
        {
            byte[] cmd = GetCmd(FUCommmand_V2.DSPReboot, sizeof(uint) * 1);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, dspIndex);

            return cmd;
        }

        //public static byte[] GetMCUPreDownloadCmd(uint binSzie)
        //{
        //    byte[] cmd = GetCmd(FUCommmand.MCUPreDownload, sizeof(uint) * 1);
        //    int index = (int)CmdFrame.PayloadIndex;
        //    index = FillUint32ToBuffer(cmd, index, binSzie);

        //    return cmd;
        //}

        public static byte[] GetMCUDownloadCmd(uint address, byte[] buffer)
        {
            Debug.Assert(buffer != null && (buffer.Length % Size1K == 0 && buffer.Length >= Size1K && buffer.Length <= Size4K));

            byte[] cmd = GetCmd(FUCommmand_V1.MCUDownload, sizeof(uint) * 1 + buffer.Length);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, address);

            Array.Copy(buffer, 0, cmd, index, buffer.Length);

            return cmd;
        }

        public static byte[] GetMCUFlashCheckCRCCmd(uint address, uint length)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.MCUCheckFlashCRC, sizeof(uint) * 2);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, address);
            index = FillUint32ToBuffer(cmd, index, length);

            return cmd;
        }

        public static byte[] MCU_BootloaderReadFlashCmd(uint address, uint length)
        {
            Debug.Assert(length > 0 && length % Size1K == 0 && length <= Size4K);

            byte[] cmd = GetCmd(FUCommmand_V1.BootloaderReadFlash, sizeof(uint) * 2);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, address);
            index = FillUint32ToBuffer(cmd, index, length);

            return cmd;
        }


        public static byte[] MCU_BootloaderUpdateLCDShowCmd(uint line, byte[] str)
        {
            Debug.Assert((line == 0 || line == 1) && str != null && str.Length <= CHARS_NUMBER_OF_MCU_LCD_PER_LINE);
            byte[] cmd = GetCmd(FUCommmand_V1.BootloaderUpdateLCDShow, sizeof(uint) + str.Length);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, line);

            Array.Copy(str, 0, cmd, index, str.Length);

            return cmd;
        }

        public static byte[] MCU_BootloaderGetFirmwareFlagCmd(uint firmwareType)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.BootloaderGetFirmwareFlag, sizeof(uint) * 1);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, firmwareType);

            return cmd;
        }

        public static byte[] MCU_BootloaderUpdateFirmwareFlagCmd(uint firmwareType, bool invalid)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.BootloaderUpdateFirmwareFlag, sizeof(uint) * 2);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, firmwareType);
            index = FillUint32ToBuffer(cmd, index, BoolToUint(invalid));
            
            return cmd;
        }
        
        public static byte[] MCU_BootloaderJumpToAPPCmd(uint delay, bool isResetDSPower)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.BootloaderJumpToAPP, sizeof(uint) * 2);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, delay);
            index = FillUint32ToBuffer(cmd, index, BoolToUint(isResetDSPower));

            return cmd;
        }

        public static byte[] MCU_BootloaderJumpToAPPCmdV2(uint delay)
        {
            byte[] cmd = GetCmd(FUCommmand_V2.BootloaderJumpToAPP, sizeof(uint) * 1);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, delay);

            return cmd;
        }

        public static byte[] MCU_AppJumpToBootloaderCmd(uint delay)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.APPJumpToBootloader, sizeof(uint) * 1);
            int index = (int)CmdFrame.PayloadIndex;
            index = FillUint32ToBuffer(cmd, index, delay);

            return cmd;
        }



        public static uint BoolToUint(bool flag)
        {
            return flag ? 1u : 0u;
        }

        public static FUResponse ParseResponse(List<byte> buffer)
        {
            if (!CheckResponseIsCompleted(buffer))
            {
                return null;
            }

            byte[] data = buffer.ToArray();
            int frameLength = (int)CmdFrame.CmdIndex + BitConverter.ToUInt16(data, (int)CmdFrame.LengthIndex);
            byte cmd = data[(int)CmdFrame.CmdIndex];
            byte[] payload = new byte[frameLength - (int)CmdFrame.PayloadIndex];
            Array.Copy(data, (int)CmdFrame.PayloadIndex, payload, 0, payload.Length);

            return new FUResponse((FUCommmand_V1)cmd, payload);
        }

        public static FUResponseV2 ParseResponseV2(List<byte> buffer)
        {
            if (!CheckResponseIsCompleted(buffer))
            {
                return null;
            }

            byte[] data = buffer.ToArray();
            int frameLength = (int)CmdFrame.CmdIndex + BitConverter.ToUInt16(data, (int)CmdFrame.LengthIndex);
            byte cmd = data[(int)CmdFrame.CmdIndex];
            byte[] payload = new byte[frameLength - (int)CmdFrame.PayloadIndex];
            Array.Copy(data, (int)CmdFrame.PayloadIndex, payload, 0, payload.Length);

            return new FUResponseV2((FUCommmand_V2)cmd, payload);
        }

        public static DSPFirmwareInfo ParseDSPFirmwareInfoFromBuffer(byte[] buffer)
        {
            if (buffer.Length != DSPFirmwareInfo.GetSize())
            {
                return null;
            }

            DSPFirmwareInfo dspFirmwareInfo = new DSPFirmwareInfo();
            int index = 0;

            dspFirmwareInfo.flashSize = ParseUint32FromBuffer(buffer, ref index);
            dspFirmwareInfo.exeSize = ParseUint32FromBuffer(buffer, ref index);
            dspFirmwareInfo.exeCrc = ParseUint32FromBuffer(buffer, ref index);

            dspFirmwareInfo.wavAddress = ParseUint32FromBuffer(buffer, ref index);
            dspFirmwareInfo.wavSize = ParseUint32FromBuffer(buffer, ref index);
            dspFirmwareInfo.wavCrc = ParseUint32FromBuffer(buffer, ref index);

            dspFirmwareInfo.envAddress = ParseUint32FromBuffer(buffer, ref index);
            dspFirmwareInfo.envSize = ParseUint32FromBuffer(buffer, ref index);
            dspFirmwareInfo.envCrc = ParseUint32FromBuffer(buffer, ref index);

            dspFirmwareInfo.tpmAddress = ParseUint32FromBuffer(buffer, ref index);
            dspFirmwareInfo.lastModifiedTimeStamp = ParseUint32FromBuffer(buffer, ref index);

            dspFirmwareInfo.isTpmEncrypted = Utils.Uint32ToBool(ParseUint32FromBuffer(buffer, ref index));
            dspFirmwareInfo.isEfuseEncrypted = Utils.Uint32ToBool(ParseUint32FromBuffer(buffer, ref index));

            dspFirmwareInfo.version = ParseUint32FromBuffer(buffer, ref index);

            dspFirmwareInfo.fileName = Encoding.UTF8.GetString(buffer, index, DSPFirmwareInfo.MaxFileNameSize);

            index += DSPFirmwareInfo.MaxFileNameSize;

            dspFirmwareInfo.comments = Encoding.UTF8.GetString(buffer, index, DSPFirmwareInfo.MaxCommentsSize);

            index += DSPFirmwareInfo.MaxCommentsSize;

            return dspFirmwareInfo;
        }

        public static DeviceID ParseMCUUIDDataFromBuffer(byte[] buffer)
        {
            if (buffer.Length != DeviceID.UID_LENGTH)
            {
                return null;
            }

            DeviceID deviceId = new DeviceID();
            Array.Copy(buffer, deviceId.uid, buffer.Length);

            return deviceId;
        }

        public static bool CheckResponseIsCompleted(List<byte> buffer)
        {
            System.Diagnostics.Debug.Assert(buffer != null);

            if (buffer.Count < (int)CmdFrame.PayloadIndex)
            {
                return false;
            }

            byte[] data = buffer.ToArray();

            if (data[(int)CmdFrame.Header1Index] == ResponseHeader1 && data[(int)CmdFrame.Header2Index] == ResponseHeader2)
            {
                ushort dataLength = BitConverter.ToUInt16(data, (int)CmdFrame.LengthIndex);

                return (int)CmdFrame.CmdIndex + dataLength <= data.Length;
            }

            return false;
        }

        public static byte[] GetDSPDownloadCmd(byte dspIndex)
        {
            byte[] cmd = GetCmd(FUCommmand_V1.DSPPreDownload, 0);
            return cmd;
        }

        public static byte[] GetDSPEndDownloadCmd()
        {
            byte[] cmd = GetCmd(FUCommmand_V1.DSPPreDownload, 0);
            return cmd;
        }

        private static byte[] GetCmd(FUCommmand_V1 command, int payloadLength)
        {
            int length = GetCmdLength(payloadLength);
            byte[] cmd = new byte[length];
            int index = 0;
            index = FillUint8ToBuffer(cmd, index, CmdHeader1);
            index = FillUint8ToBuffer(cmd, index, CmdHeader2);
            index = FillUint16ToBuffer(cmd, index, (ushort)(payloadLength + 1));
            index = FillUint8ToBuffer(cmd, index, (byte)command);

            return cmd;
        }

        private static byte[] GetCmd(FUCommmand_V2 command, int payloadLength)
        {
            int length = GetCmdLength(payloadLength);
            byte[] cmd = new byte[length];
            int index = 0;
            index = FillUint8ToBuffer(cmd, index, CmdHeader1);
            index = FillUint8ToBuffer(cmd, index, CmdHeader2);
            index = FillUint16ToBuffer(cmd, index, (ushort)(payloadLength + 1));
            index = FillUint8ToBuffer(cmd, index, (byte)command);

            return cmd;
        }

        public static int GetCmdLength(int payloadLength)
        {
            //头码(2字节) + 长度码(2字节) + 命令(1字节) + payload(N字节) + crc(校验)
            //头码(2字节) + 长度码(2字节) + 命令(1字节) + payload(N字节)
            return 2 + 2 + 1 + payloadLength;
        }

        private static int FillBytesToBuffer(byte[] buffer, int index, byte[] bytes)
        {
            Array.Copy(bytes, 0, buffer, index, bytes.Length);

            return index + bytes.Length;
        }

        private static int FillUint8ToBuffer(byte[] buffer, int index, byte data)
        {
            buffer[index + 0] = (byte)((data >> 0) & 0xff);

            return index + sizeof(byte);
        }

        private static int FillUint16ToBuffer(byte[] buffer, int index, UInt16 data)
        {
            buffer[index + 0] = (byte)((data >> 0) & 0xff);
            buffer[index + 1] = (byte)((data >> 8) & 0xff);

            return index + sizeof(UInt16);
        }

        public static int FillUint32ToBuffer(byte[] buffer, int index, UInt32 data)
        {
            buffer[index + 0] = (byte)((data >> 0) & 0xff);
            buffer[index + 1] = (byte)((data >> 8) & 0xff);
            buffer[index + 2] = (byte)((data >> 16) & 0xff);
            buffer[index + 3] = (byte)((data >> 24) & 0xff);

            return index + sizeof(UInt32);
        }

        private static int FillUint64ToBuffer(byte[] buffer, int index, UInt64 data)
        {
            byte[] arr = BitConverter.GetBytes(data);

            Array.Copy(arr, 0, buffer, index, arr.Length);

            return index + arr.Length;
        }

        private static UInt32 ParseUint32FromBuffer(byte[] buffer, ref int index)
        {
            int oldIndex = index;
            index += sizeof(UInt32);
            return BitConverter.ToUInt32(buffer, oldIndex);
        }

        private static UInt64 ParseUint64FromBuffer(byte[] buffer, ref int index)
        {
            int oldIndex = index;
            index += sizeof(UInt64);
            return BitConverter.ToUInt64(buffer, oldIndex);
        }

        public static DSPPreDownloadStatusCode GetPreDownloadStatusCode(uint status)
        {
            foreach (var v in Enum.GetValues(typeof(DSPPreDownloadStatusCode)))
            {
                if((uint)v == status)
                {
                    return (DSPPreDownloadStatusCode)v;
                }
            }

            return DSPPreDownloadStatusCode.IAP_DSP_PRE_DOWNLOAD_STATUS_UNKONOWN_ERROR;
        }

        public static LinkToDSPBootRomStatusCodeV2 GetLinkToDSPBootRomStatusCodeV2(uint status)
        {
            foreach (var v in Enum.GetValues(typeof(LinkToDSPBootRomStatusCodeV2)))
            {
                if ((uint)v == status)
                {
                    return (LinkToDSPBootRomStatusCodeV2)v;
                }
            }

            return LinkToDSPBootRomStatusCodeV2.IAP_DSP_LINK_TO_BOOTROM_STATUS_UNKONOWN_ERROR;
        }

        public static DSPDownloadStatusCode GetDownloadStatusCode(uint status)
        {
            foreach (var v in Enum.GetValues(typeof(DSPDownloadStatusCode)))
            {
                if ((uint)v == status)
                {
                    return (DSPDownloadStatusCode)v;
                }
            }

            return DSPDownloadStatusCode.IAP_DSP_DOWNLOAD_STATUS_UNKONOWN_ERROR;
        }

        public static DSPWriteFlashStatusCodeV2 GetWriteFlashStatusCodeV2(uint status)
        {
            foreach (var v in Enum.GetValues(typeof(DSPWriteFlashStatusCodeV2)))
            {
                if ((uint)v == status)
                {
                    return (DSPWriteFlashStatusCodeV2)v;
                }
            }

            return DSPWriteFlashStatusCodeV2.IAP_DSP_WRITE_FLASH_STATUS_UNKONOWN_ERROR;
        }

        public static DSPCheckFlashCRCStatusCode GetCheckFlashCRCStatusCode(uint status)
        {
            foreach (var v in Enum.GetValues(typeof(DSPCheckFlashCRCStatusCode)))
            {
                if ((uint)v == status)
                {
                    return (DSPCheckFlashCRCStatusCode)v;
                }
            }

            return DSPCheckFlashCRCStatusCode.IAP_DSP_CHECK_FLASH_CRC_STATUE_UNKONOWN_ERROR;
        }

        public static DSPCheckFlashCRCStatusCodeV2 GetCheckFlashCRCStatusCodeV2(uint status)
        {
            foreach (var v in Enum.GetValues(typeof(DSPCheckFlashCRCStatusCodeV2)))
            {
                if ((uint)v == status)
                {
                    return (DSPCheckFlashCRCStatusCodeV2)v;
                }
            }

            return DSPCheckFlashCRCStatusCodeV2.IAP_DSP_CHECK_FLASH_CRC_STATUE_UNKONOWN_ERROR;
        }

        public static DSPEndDownloadStatusCode GetEndDownloadStatusCode(uint status)
        {
            foreach (var v in Enum.GetValues(typeof(DSPEndDownloadStatusCode)))
            {
                if ((uint)v == status)
                {
                    return (DSPEndDownloadStatusCode)v;
                }
            }

            return DSPEndDownloadStatusCode.IAP_DSP_END_DOWNLOAD_STATUS_UNKONOWN_ERROR;
        }

        public static DSPDownloadCompletedStatusCodeV2 GetDownloadCompletedStatusCodeV2(uint status)
        {
            foreach (var v in Enum.GetValues(typeof(DSPDownloadCompletedStatusCodeV2)))
            {
                if ((uint)v == status)
                {
                    return (DSPDownloadCompletedStatusCodeV2)v;
                }
            }

            return DSPDownloadCompletedStatusCodeV2.IAP_DSP_DOWNLOAD_COMPLETED_STATUS_UNKONOWN_ERROR;
        }

        public static ProgrammerMCUFlashDownloadStatusCode GetMCUFlashDownloadStatusCode(uint status)
        {
            foreach (var v in Enum.GetValues(typeof(ProgrammerMCUFlashDownloadStatusCode)))
            {
                if ((uint)v == status)
                {
                    return (ProgrammerMCUFlashDownloadStatusCode)v;
                }
            }

            return ProgrammerMCUFlashDownloadStatusCode.IAP_DSP_MCU_FLASH_DOWNLOAD_STATUS_UNKONOWN_ERROR;
        }

        public class FUResponse
        {
            public readonly FUCommmand_V1 cmd;
            public readonly byte[] payloadData;

            public FUResponse(FUCommmand_V1 cmd_, byte[] payloadData_)
            {
                this.cmd = cmd_;
                this.payloadData = payloadData_;
            }
        }

        public class FUResponseV2
        {
            public readonly FUCommmand_V2 cmd;
            public readonly byte[] payloadData;

            public FUResponseV2(FUCommmand_V2 cmd_, byte[] payloadData_)
            {
                this.cmd = cmd_;
                this.payloadData = payloadData_;
            }
        }
    }
}
