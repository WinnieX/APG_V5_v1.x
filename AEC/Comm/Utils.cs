using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Comm
{
    public class Utils
    {
        private const string HexStringPrefix = "0x";
        public static string BoolToString(bool flag)
        {
            return flag ? "1" : "0";
        }

        public static uint BoolToUint32(bool flag)
        {
            return flag ? 1u : 0u;
        }

        public static bool Uint32ToBool(uint v)
        {
            return v != 0;
        }

        public static string BoolToYesOrNo(bool flag)
        {
            return flag ? Misc.Config.GetInstance().localization[Misc.Localization.firmwareUpdate_Yes] : Misc.Config.GetInstance().localization[Misc.Localization.firmwareUpdate_No];
        }

        public static bool StringToBool(string s, out bool result)
        {
            result = false;
            try
            {
                result = Convert.ToInt32(s) != 0;
                return true;
            }
            catch(Exception )
            {
                return false;
            }
            //return s != "1" ? false : true;
        }

        public static ushort ReverseBytes(ushort value)
        {
          return (ushort)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);
        }

        // reverse byte order (32-bit)
        public static uint ReverseBytes(uint value)
        {
          return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
                 (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
        }

        // reverse byte order (64-bit)
        public static ulong ReverseBytes(ulong value)
        {
          return (value & 0x00000000000000FFUL) << 56 | (value & 0x000000000000FF00UL) << 40 |
                 (value & 0x0000000000FF0000UL) << 24 | (value & 0x00000000FF000000UL) << 8 |
                 (value & 0x000000FF00000000UL) >> 8 | (value & 0x0000FF0000000000UL) >> 24 |
                 (value & 0x00FF000000000000UL) >> 40 | (value & 0xFF00000000000000UL) >> 56;
        }

        public static int FillUint16ToBuffer(byte[] buffer, int index, ushort data)
        {
            buffer[index + 0] = (byte)(data >> 0 & 0xff);
            buffer[index + 1] = (byte)(data >> 8 & 0xff);

            return index + sizeof(ushort);
        }

        public static byte[] ReverseBytes(byte[] data)
        {
            Debug.Assert(data != null);

            byte[] temp = new byte[data.Length];

            if(data.Length > 0)
            {
                int maxIndex = temp.Length - 1;
                for (int i = 0; i <= maxIndex; ++i)
                {
                    temp[i] = data[maxIndex - i];
                }
            }

            return temp;
        }

        public static string ByteToHexString(byte data)
        {
            return string.Format("0x{0}", data.ToString("X").PadLeft(sizeof(byte) * 2, '0'));
        }

        public static string Uint16ToHexString(ushort data)
        {
            return string.Format("0x{0}", data.ToString("X").PadLeft(sizeof(ushort) * 2, '0'));
        }

        public static string Uint32ToHexString(uint data)
        {
            return string.Format("0x{0}", data.ToString("X").PadLeft(sizeof(uint) * 2, '0'));
        }

        public static string Int64ToHexString(long data)
        {
            return string.Format("0x{0}", Convert.ToString(data, 16).PadLeft(sizeof(long) * 2, '0'));
        }

        public static bool ParseHexStringToInt64(string str, out long data)
        {
            str = str.Trim().ToLower();
            if (str.StartsWith("0x"))
            {
                str = str.Substring(2);
            }

            try
            {
                data = Convert.ToInt64(str, 16);
                return true;
            }
            catch (Exception)
            {
                data = 0;
                return false;
            }
        }

        public static bool ParseHexStringToUint32(string str, out uint data)
        {
            str = str.Trim().ToLower();
            if(str.StartsWith("0x"))
            {
                str = str.Substring(2);
            }

            try
            {
                data = Convert.ToUInt32(str, 16);
                return true;
            }
            catch(Exception)
            {
                data = 0;
                return false;
            }
        }

        public static bool ParseHexStringToUint32(string str, out uint data, out string error)
        {
            str = str.Trim().ToLower();
            if (str.StartsWith("0x"))
            {
                str = str.Substring(2);
            }

            try
            {
                data = Convert.ToUInt32(str, 16);
                error = string.Empty;
                return true;
            }
            catch (Exception e)
            {
                data = 0;
                error = e.Message;
                return false;
            }
        }

        public static string ByteArrayToHexStr(byte[] bytes, string span = " ")
        {
            StringBuilder sb = new StringBuilder();
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("X").PadLeft(2, '0'));
                    sb.Append(span);
                }
            }

            return sb.ToString();
        }

        public static byte[] HexStringTorByteArray(string str)
        {
            Debug.Assert(str != null && str.Length > 0);
            str = str.Trim();

            byte[] bytes = new byte[str.Length / 2];
            for (int i = 0; i < str.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(str.Substring(i, 2), 16);
            }

            return bytes;
        }

        public static byte[] HexStringToBytes(string keyStr, out string errorMsg)
        {
            errorMsg = string.Empty;

            try
            {
                return HexStringTorByteArray(keyStr);
            }
            catch (Exception e)
            {
                errorMsg = string.Format("hex string 转 字节数组有误:{0}", e.Message);
                return null;
            }
        }

        public static bool ParseStringToByte(string str, out byte data, out string errorMsg)
        {
            data = 0;
            errorMsg = string.Empty;
            int fromBase = 10;

            try
            {
                if (str.StartsWith(HexStringPrefix))
                {
                    str = str.Substring(HexStringPrefix.Length);
                    fromBase = 16;
                }

                data = Convert.ToByte(str, fromBase);

                return true;
            }
            catch (Exception e)
            {
                errorMsg = string.Format(" 解析 string 至字节有误:{0}", e.Message);
                return false;
            }
        }

        public static bool CheckBinaryData(byte[] checkedData, int offset, byte[] data)
        {
            Debug.Assert(checkedData != null && data != null && offset >= 0 && offset + data.Length <= checkedData.Length);

            for(int i = 0; i < data.Length; ++i)
            {
                if(checkedData[i + offset] != data[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CheckBinaryData(byte[] checkedData, int offset, string text)
        {
            Debug.Assert(text != null);

            return CheckBinaryData(checkedData, offset, Encoding.ASCII.GetBytes(text));
        }

        public static bool CheckByteListLastData(List<byte> list, byte[] byteArray)
        {
            byte[] receivedData = list.ToArray();

            if (byteArray.Length <= receivedData.Length)
            {
                for (int i = 0; i < byteArray.Length; ++i)
                {
                    if (byteArray[i] != receivedData[receivedData.Length - byteArray.Length + i])
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        public static bool CheckByteListLastData(List<byte> list, byte[] byteArray, int length)
        {
            byte[] receivedData = list.ToArray();

            if (byteArray.Length <= receivedData.Length && byteArray.Length > length)
            {
                for (int i = 0; i < byteArray.Length && i < length; ++i)
                {
                    if (byteArray[i] != receivedData[receivedData.Length - byteArray.Length + i])
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        public static bool CompareByteArray(byte[] data1, byte[] data2)
        {
            if(data1.Length == data2.Length)
            {
                for(int i = 0; i < data1.Length; ++i)
                {
                    if(data1[i] != data2[i])
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        public static bool CompareByteArray(byte[] data1, int index, byte[] data2)
        {
            if (data1.Length >= index + data2.Length)
            {
                for (int i = 0; i < data2.Length && i < data1.Length - (index + data2.Length); ++i)
                {
                    if (data1[index + i] != data2[i])
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        public static byte[] CloneByteArray(byte[] data)
        {
            Debug.Assert(data != null);
            byte[] temp = new byte[data.Length];
            Array.Copy(data, temp, data.Length);

            return temp;
        }

        public static byte[] CreateBinData(byte[] binData, int alignSize, byte fill = 0xff)
        {
            Debug.Assert(binData != null && binData.Length > 0);

            int length = binData.Length;
            if ((length & alignSize - 1) != 0)
            {
                length = (length / alignSize + 1) * alignSize;
            }
            
            byte[] temp = CreateByteArray((uint)length);

            Array.Copy(binData, temp, binData.Length);

            return temp;
        }

        public static byte[] CreateByteArray(uint length, byte fill = 0xff)
        {
            byte[] data = new byte[length];
            for(int i = 0; i < data.Length; ++i)
            {
                data[i] = fill;
            }

            return data;
        }

        public static byte[] ConcatByteArray(byte[] data1, byte[] data2)
        {
            byte[] data = new byte[data1.Length + data2.Length];
            Array.Copy(data1, 0, data, 0, data1.Length);
            Array.Copy(data2, 0, data, data1.Length, data2.Length);

            return data;
        }

        public static uint GetAlignSize(uint size, uint align)
        {
            Debug.Assert(size > 0 && align > 0);

            return (size + align - 1) / align * align;
        }

        public static uint GetFileModifiedTimeStamp(string fileName)
        {
            try
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(fileName);
                if (fi.Exists)
                {
                    return Tools.TimeStampHelper.DateTimeToUint32TimeStamp(fi.LastWriteTime);
                }
            }
            catch (Exception)
            {

            }

            return 0;
        }

        public static byte[] GetSubByteArray(byte[] data, int index, int length)
        {
            Debug.Assert(data != null && length > 0 && data.Length >= index + length);
            byte[] temp = new byte[length];
            Array.Copy(data, index, temp, 0, length);

            return temp;
        }
    }
}
