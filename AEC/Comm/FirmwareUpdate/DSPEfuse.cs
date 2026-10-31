using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Comm.FirmwareUpdate
{
    public class DSPEfuse
    {
        public const int EfuseSize = 0x20;
        public const int KeysStartIndex = 0x00;
        public const int KeysSize = 0x10;
        public const int IDSize = 0x10;
        public const int KeysNumber = KeysSize / sizeof(uint);
        public const int UidStartIndex = KeysStartIndex + KeysSize;
        public const int UidSize = 8;
        public const int MacStartIndex = UidStartIndex + UidSize;
        public const int MacSize = 6;
        public const int RawStartIndex = MacStartIndex + MacSize;
        public const int RawSize = 1;
        public const int ControlStartIndex = RawStartIndex + RawSize;
        public const int ControlSize = 1;
        public const int ControlBitNumber = 8;

        public static readonly Dictionary<int, string> controlBitMapDescription = new Dictionary<int,string>()
        {
            //{0, "disWriteEfuse"},
            //{1, "disWriteMAC"},
            //{2, "disWriteUID"},
            //{3, "disWriteEncryptWord"},
            //{4, "disReadEncryptWord"},
            //{5, "enEncryptWord"},
            //{6, "disSpiFlash"},
            //{7, "disJtag"},
            {0, "禁写Efuse"},
            {1, "禁写Mac"},
            {2, "禁写UID"},
            {3, "禁写加密字"},
            {4, "禁读加密字"},
            {5, "使能加密"},
            {6, "禁用SPIFlash"},
            {7, "禁用JTAG"},
        };

        public readonly byte[] data = new byte[EfuseSize];

        public uint coef1
        {
            get
            {
                return BitConverter.ToUInt32(data, 0 * sizeof(uint));
            }
            set
            {
                byte[] temp = BitConverter.GetBytes(value);
                Array.Copy(temp, 0, data, 0 * sizeof(uint), sizeof(uint));
            }
        }

        public uint coef2
        {
            get
            {
                return BitConverter.ToUInt32(data, 1 * sizeof(uint));
            }
            set
            {
                byte[] temp = BitConverter.GetBytes(value);
                Array.Copy(temp, 0, data, 1 * sizeof(uint), sizeof(uint));
            }
        }

        public uint coef3
        {
            get
            {
                return BitConverter.ToUInt32(data, 2 * sizeof(uint));
            }
            set
            {
                byte[] temp = BitConverter.GetBytes(value);
                Array.Copy(temp, 0, data, 2 * sizeof(uint), sizeof(uint));
            }
        }

        public uint coef4
        {
            get
            {
                return BitConverter.ToUInt32(data, 3 * sizeof(uint));
            }
            set
            {
                byte[] temp = BitConverter.GetBytes(value);
                Array.Copy(temp, 0, data, 3 * sizeof(uint), sizeof(uint));
            }
        }

        public uint GetEncryptKey(int index)
        {
            Debug.Assert(index >= 0 && index < KeysNumber);

            return BitConverter.ToUInt32(data, index * sizeof(uint));
        }

        public void SetEncryptKey(int index, uint key)
        {
            Debug.Assert(index >= 0 && index < KeysNumber);

            byte[] temp = BitConverter.GetBytes(key);
            Array.Copy(temp, 0, data, index * sizeof(uint), sizeof(uint));
        }

        public byte[] UID
        {
            get
            {
                byte[] idBuffer = new byte[UidSize];
                Array.Copy(this.data, UidStartIndex, idBuffer, 0, idBuffer.Length);

                return idBuffer;
            }
            set
            {
                Debug.Assert(value != null && value.Length == UidSize);
                Array.Copy(value, 0, this.data, UidStartIndex, UidSize);
            }
        }

        public byte[] MAC
        {
            get
            {
                byte[] buffer = new byte[MacSize];
                Array.Copy(this.data, MacStartIndex, buffer, 0, buffer.Length);

                return buffer;
            }
            set
            {
                Debug.Assert(value != null && value.Length == MacSize);
                Array.Copy(value, 0, this.data, MacStartIndex, MacSize);
            }
        }

        public byte[] id
        {
            get
            {
                byte[] buffer = new byte[IDSize];
                Array.Copy(UID, buffer, UID.Length);
                Array.Copy(MAC, 0, buffer, UidSize, MAC.Length);

                return buffer;
            }
        }

        public byte raw
        {
            get
            {
                return this.data[RawStartIndex];
            }
            set
            {
                this.data[RawStartIndex] = value;
            }
        }

        public byte control
        {
            get
            {
                return this.data[ControlStartIndex];
            }
            set
            {
                this.data[ControlStartIndex] = value;
            }
        }

        public bool GetControlBit(int index)
        {
            Debug.Assert(index >= 0 && index < 8);

            return (control & BitHelper.GetByte(index, true)) != 0;
        }

        public void SetControlBit(int index, bool flag)
        {
            Debug.Assert(index >= 0 && index < 8);

            if(flag)
            {
                control |= BitHelper.GetByte(index, flag);
            }
            else
            {
                control &= BitHelper.GetByte(index, flag);
            }
        }

        public override string ToString()
        {
            //return base.ToString();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("coef1 = {0}", Utils.Uint32ToHexString(coef1));
            sb.AppendLine(string.Empty);
            sb.AppendFormat("coef2 = {0}", Utils.Uint32ToHexString(coef2));
            sb.AppendLine(string.Empty);
            sb.AppendFormat("coef3 = {0}", Utils.Uint32ToHexString(coef3));
            sb.AppendLine(string.Empty);
            sb.AppendFormat("coef4 = {0}", Utils.Uint32ToHexString(coef4));
            sb.AppendLine(string.Empty);
            sb.AppendFormat("UID = {0}", Utils.ByteArrayToHexStr(UID));
            sb.AppendLine(string.Empty);
            sb.AppendFormat("MAC = {0}", Utils.ByteArrayToHexStr(MAC));
            sb.AppendLine(string.Empty);
            sb.AppendFormat("raw = {0}", Utils.ByteToHexString(raw));
            sb.AppendLine(string.Empty);
            sb.AppendFormat("ctrl = {0}", Utils.ByteToHexString(control));
            sb.AppendLine(string.Empty);
            for (int i = 0; i < ControlBitNumber; ++i)
            {
                if (controlBitMapDescription.ContainsKey(i))
                {
                    sb.AppendFormat("{0} = {1}", controlBitMapDescription[i], Utils.BoolToString(GetControlBit(i)));
                    if (i < ControlBitNumber - 1)
                    {
                        sb.AppendLine(string.Empty);
                    }
                }
            }

            return sb.ToString();
        }
        //public static byte 
    }
}
