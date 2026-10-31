using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AEC.Core;
using AEC.Common;

namespace AEC.Comm
{
    public class SystemCommProtocol
    {
        public enum SystemParamAddr
        {
            deviceModeName = 0x00,
            deviceModeSave = 0x01,
            deviceModeCall = 0x02,
            musicInputGain = 0x03,
            systemPassword = 0x04,
            keyboardLockPassword = 0x05,
            musicStartVolume = 0x06,
            musicMaxVolume = 0x07,
            micStartVolume = 0x08,
            micMaxVolume = 0x09,
            effectStartVolume = 0x0a,
            effectMaxVolume = 0x0b,
            user = 0x0c,
            isLockKeyboard = 0x0d,
            musicInputPort = 0x0e,
            pcDataDownload2Device = 0x0f,
            deviceDataUpload2Pc = 0x10,
            isEnableRomoteControl = 0x11,
            musicVolume = 0x20,
            micVolume = 0x21,
            effectVolume = 0x22,

            timbre = 0x23,

            deviceWorkMode = 0x24,

            clarity = 0x25,     //清晰度参数

            //systemEndLevel = 0x23,
            //systemFrontLevel = 0x24,

            musicMute = 0x26,
            micMute = 0x27,
            effectMute = 0x28,

            systemStatus = 0x29,

            connectionState = 0x30,
            deviceId = 0x40,                //设备id，只读
            activationCode = 0x48,          //激活码，只读
            randKey = 0x4e,                 //加密随机密钥，只写
            deviceModel = 0x50,             //设备型号，只读

            prototypeModel = 0x70,         //产品原型
            firmwareVersion = 0x71,        //固件版本
            firmwareBuildNumbers = 0x72,   //固件Build
            //massProductionDownloadPresetMode = 0x73, //量产下载预设数据模式
            customDeviceInfoEnable = 0x74,  //自定义设备信息使能
            customDeviceInfoData = 0x75,    //自定义设备信息

            deviceHardwareState = 0x80,     //硬件状态，只读
            deviceName = 0x81,              //设备名称
            usageLimitLock = 0x82,          //使用限制锁
        }

        public enum ConnectionFlag
        {
            Connect = 0x11,
            Online = 0x66,
            Disconnect = 0xaa,
        }

        public enum SystemCustomDeviceInfoData
        {
            custom_manufacturer_data = 0x00,
            custom_device_model_data = 0x01,
            custom_device_version_data = 0x02,
            custom_device_description_data = 0x03,
        }

        private const byte ASCIICodeOffset = 0x20;
        private const byte PasswordOffset = 0x30;

        public const byte PrototypeModelLength = 0x10;
        public const byte FirmwareVersionLength = 0x04;
        public const byte FirmwareBuildVersionLength = 0x04;
        public const byte EnableExtendedFunctionLength = 0x02;
        public const byte CustomManufacturerLength = 0x14;
        public const byte CustomDeviceModelLength = 0x14;
        public const byte CustomDeviceVersionLength = 0x14;
        public const byte CustomDeviceDescriptionLength = 0x14;

        public static byte[] CustomDeviceInfoDataConvertToBytes(SystemCommProtocol.SystemCustomDeviceInfoData efd, Object data)
        {
            byte[] bytes = null;
            switch (efd)
            {
                case SystemCommProtocol.SystemCustomDeviceInfoData.custom_manufacturer_data:
                    {
                        string manufacturer = data as string;
                        if (manufacturer != null)
                        {
                            bytes = String2Binary(manufacturer, SystemCommProtocol.CustomManufacturerLength);
                        }
                    }
                    break;

                case SystemCommProtocol.SystemCustomDeviceInfoData.custom_device_model_data:
                    {
                        string model = data as string;
                        if (model != null)
                        {
                            bytes = String2Binary(model, SystemCommProtocol.CustomDeviceModelLength);
                        }
                    }
                    break;

                case SystemCommProtocol.SystemCustomDeviceInfoData.custom_device_version_data:
                    {
                        string version = data as string;
                        if (version != null)
                        {
                            bytes = String2Binary(version, SystemCommProtocol.CustomDeviceVersionLength);
                        }
                    }
                    break;

                case SystemCommProtocol.SystemCustomDeviceInfoData.custom_device_description_data:
                    {
                        string description = data as string;
                        if (description != null)
                        {
                            bytes = String2Binary(description, SystemCommProtocol.CustomDeviceDescriptionLength);
                        }
                    }
                    break;

                default:
                    return null;
            }

            return bytes;
        }

        public static bool ParseDataToUsageLimitLockData(byte[] data, int startIndex, UsageLimitLock usageLimitLock)
        {
            if(data.Length < startIndex + UsageLimitLock.DataLength)
            {
                return false;
            }

            usageLimitLock.enable = AEC.Core.DataConverter.Short2Bool(BitConverter.ToInt16(data, startIndex));
            startIndex += sizeof(short);
            usageLimitLock.usageCount = BitConverter.ToUInt16(data, startIndex);
            startIndex += sizeof(short);

            byte[] password = new byte[UsageLimitLock.PasswordLength];
            Array.Copy(data, startIndex, password, 0, password.Length);

            byte[] decodeData = DataDecode(password, PasswordOffset);
            usageLimitLock.systemPasswordStr = Encoding.ASCII.GetString(decodeData);

            return true;
        }

        public static byte[] UsageLimitLockToByteArray(UsageLimitLock usageLimitLock)
        {
            byte[] data = new byte[UsageLimitLock.DataLength];

            int payloadIndex = 0;
            payloadIndex = Utils.FillUint16ToBuffer(data, payloadIndex, (ushort)DataConverter.Bool2Short(usageLimitLock.enable));
            payloadIndex = Utils.FillUint16ToBuffer(data, payloadIndex, (ushort)usageLimitLock.usageCount);

            byte[] password = Encoding.ASCII.GetBytes(usageLimitLock.systemPasswordStr);
            System.Diagnostics.Debug.Assert(password != null && password.Length == UsageLimitLock.PasswordLength);

            byte[] encodeData = DataEncode(password, PasswordOffset);

            Array.Copy(encodeData, 0, data, payloadIndex, encodeData.Length);

            return data;
        }
        
        public static string Binary2String(byte[] data)
        {
            Debug.Assert(data != null && data.Length > 0);

            Decoder decoder = Encoding.ASCII.GetDecoder();
            int charCount = decoder.GetCharCount(data, 0, data.Length);
            Char[] chars = new char[charCount];
            int charLength = decoder.GetChars(data, 0, data.Length, chars, 0);

            return new string(chars);
        }

        public static byte[] String2Binary(string str, int target_length)
        {
            Debug.Assert(target_length > 0);

            byte[] target_bytes = new byte[target_length];
            byte[] ascii_bytes = String2Binary(str);

            int copy_length = Math.Min(ascii_bytes.Length, target_length);

            Array.Copy(ascii_bytes, target_bytes, copy_length);

            return target_bytes;
        }

        public static byte[] String2Binary(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static byte[] StringEncode2BinaryData(string str)
        {
            byte[] strData = String2Binary(str);
            return DataEncode(strData, ASCIICodeOffset);
        }

        public static byte[] PassowrdEncode2PassowrdData(string password)
        {
            byte[] passowrdData = String2Binary(password);
            return DataEncode(passowrdData, PasswordOffset);
        }

        //数据解码成密码
        public static string PasswordDataDecode2Password(byte[] data)
        {
            byte[] decodeData = DataDecode(data, PasswordOffset);
            return Binary2String(decodeData);
        }

        //数据解码成字符串数据
        public static string StringDataDecode2String(byte[] data)
        {
            byte[] decodeData = DataDecode(data, ASCIICodeOffset);
            return Binary2String(decodeData);
        }

        //密码数据编码成数据
        public static byte[] PasswordDataEncode2Data(byte[] data)
        {
            return DataEncode(data, PasswordOffset);
        }

        //字符串数据编码成数据
        public static byte[] StringDataEncode2Data(byte[] data)
        {
            return DataEncode(data, ASCIICodeOffset);
        }

        

        //public static Char[] BinaryDecode2Chars(byte[] data)
        //{
        //    Debug.Assert(data != null && data.Length > 0);

        //    Decoder decoder = Encoding.ASCII.GetDecoder();
        //    int charCount = decoder.GetCharCount(data, 0, data.Length);
        //    Char[] chars = new char[charCount];
        //    int charLength = decoder.GetChars(data, 0, data.Length, chars, 0);

        //    return chars;
        //}

        //将数据解码成字符数组
        private static byte[] DataDecode(byte[] data, byte offset)
        {
            Debug.Assert(data != null && data.Length > 0);
            byte[] dataDecode = new byte[data.Length];
            for (int i = 0; i < data.Length; ++i)
            {
                dataDecode[i] = (byte)(data[i] + offset);
            }

            return dataDecode;
        }

        //将数据编码成字符数组
        private static byte[] DataEncode(byte[] data, byte offset)
        {
            Debug.Assert(data != null);
            byte[] dataEncode = new byte[data.Length];
            for (int i = 0; i < data.Length; ++i)
            {
                dataEncode[i] = (byte)(data[i] - offset);
            }

            return dataEncode;
        }
    }
}
