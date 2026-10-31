using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Comm.CommTest
{
    public class CommProtocolVKx
    {
        protected enum DataFrame : uint
        {
            HeaderIndex = 0x00,
            IdentityIndex = 0x01,
            LengthIndex = 0x02,
            CmdTypeIndex = 0x03,
            ModuleIndex = 0x04,
            DataAddressIndex = 0x05,
            ValidDataStartIndex = 0x06,
        };

        protected const byte Header = 0xFC;
        protected const byte BeforCmdLength = 0x03;          //控制命令之前的4个字节:起始标志\身份标识\数据包长度
        protected const byte ReadCmd = 0xA0;                 //读取命令
        protected const byte WriteCmd = 0xA7;                //写入命令

        protected const byte ReadResponseMinLength = 0x09;    //读取数据响应最短长度

        public const byte DefaultSystemModuleIndex = 0x0a;

        public static readonly byte[] handshakeCmd = new byte[] { 0xFC, 0x31, 0x06, 0xA7, 0x0A, 0x30, 0x11, 0x00, 0xDF };
        public static readonly byte[] offlineCmd = new byte[] { 0xFC, 0x31, 0x06, 0xA7, 0x0A, 0x30, 0xAA, 0x00, 0xB6 };
        public static readonly byte[] loadDeviceModelCmd = new byte[] { 0xFC, 0x31, 0x04, 0xA0, 0x0A, 0x50, 0xC7 };

        public static bool ParseDeviceModelCode(byte[] buffer, out uint deviceModel)
        {
            deviceModel = 0xffffffff;

            if (buffer.Length < ReadResponseMinLength || buffer[BeforCmdLength] != ReadCmd || buffer[(uint)DataFrame.ModuleIndex] != DefaultSystemModuleIndex)
            {
                return false;
            }

            //AEC.Log.OutputLine("CheckReadResponseIsOk()...");
            if (!CheckReadResponseIsCompleted(new List<byte>(buffer)))
            {
                Tools.LogHelper.WriteInfo("Failed to detect response data!");
                return false;
            }

            //AEC.Log.OutputLine("CheckReadResponseIsOk()...完成");
            byte[] payloadData = GetSystemModuleData(buffer);

            if (payloadData == null)
            {
                return false;
            }

            if (payloadData.Length == sizeof(uint))
            {
                deviceModel = BitConverter.ToUInt32(payloadData, 0);
            }
            else if (payloadData.Length == sizeof(ushort))
            {
                deviceModel = BitConverter.ToUInt16(payloadData, 0);
            }
            else
            {
                return false;
            }

            return true;
        }

        private static byte[] GetSystemModuleData(byte[] buffer)
        {
            if (buffer == null ||
                buffer.Length < ReadResponseMinLength ||
                buffer[BeforCmdLength] != ReadCmd)
            {
                return null;
            }

            int cmdLength = buffer[(uint)DataFrame.LengthIndex] + 3;
            int length = cmdLength - 1 - (int)DataFrame.ValidDataStartIndex;
            if (length <= 0)
            {
                return null;
            }

            byte[] data = new byte[length];
            Array.Copy(buffer, (int)DataFrame.ValidDataStartIndex, data, 0, length);

            return data;
        }
        
        public static CommCmdType CheckCommCmdType(byte[] data)
        {
            Debug.Assert(data != null);

            if (data.Length > BeforCmdLength + 1)
            {
                if (data[BeforCmdLength] == ReadCmd)
                {
                    return CommCmdType.Read;
                }
                else if (data[BeforCmdLength] == WriteCmd)
                {
                    return CommCmdType.Write;
                }
            }

            return CommCmdType.Unknown;
        }

        public static bool CheckReadResponseIsCompleted(List<byte> bytes)
        {
            byte[] byte_arr = bytes.ToArray();
            int length = byte_arr.Length;

            if (length >= ReadResponseMinLength &&
                byte_arr[0] == Header &&
                byte_arr[2] + BeforCmdLength <= length)
            {
                //AEC.Log.OutputLine("检测读取数据响应是否完成..{0}, length = {1}, lengthFlag = {2}", DateTime.Now.ToString("hh:mm:ss fff"), length, byte_arr[2]);
                return true;
            }

            return false;
        }

        //检测写入响应是否正确
        public static bool CheckWriteResponseIsOk(List<byte> bytes)
        {
            byte[] byte_arr = bytes.ToArray();
            if (byte_arr.Length == 2)
            {
                if (byte_arr[0] == 0xfe && byte_arr[1] == 0xbb)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
