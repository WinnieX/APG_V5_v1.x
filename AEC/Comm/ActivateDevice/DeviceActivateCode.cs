using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm.ActivateDevice
{
    public class DeviceActivateCode
    {
        public const uint ActivateCodeLength = 0x0C;
        public const uint ActivateFlag = 0x21571A56;
        public const ushort rand1 = 0x1A56;
        public const ushort rand2 = 0x2157;

        public static readonly byte[] activateData = new byte[] { 0x71, 0xB5, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF};

        public static bool CheckActivateCode(byte[] code)
        {
            System.Diagnostics.Debug.Assert(code != null);
            if (code.Length < ActivateCodeLength)
            {
                return false;
            }

            ushort t1 = (ushort)(code[0] + (code[1] << 8));
            ushort t2 = (ushort)(code[2] + (code[3] << 8));

            ushort check1 = CalcCheckCode(t1, t2, rand1, rand2);
            ushort check2 = CalcCheckCode(t2, t1, rand2, rand1);

            ushort t3 = (ushort)(code[4] + (code[5] << 8));
            ushort t4 = (ushort)(code[6] + (code[7] << 8));

            if (check1 != t3 || check2 != t4)
            {
                return false;
            }

            uint flag = BitConverter.ToUInt32(code, 8);
            if(flag != ActivateFlag)
            {
                return false;
            }

            return true;
        }

        public static byte[] GenerateActivateCode(byte[] deviceId)
        {
            System.Diagnostics.Debug.Assert(deviceId != null && deviceId.Length == sizeof(uint));

            byte[] code = new byte[ActivateCodeLength];

            int index = 0;
            Array.Copy(deviceId, code, deviceId.Length);
            index += deviceId.Length;

            ushort t1 = (ushort)(code[0] + (code[1] << 8));
            ushort t2 = (ushort)(code[2] + (code[3] << 8));

            ushort check1 = CalcCheckCode(t1, t2, rand1, rand2);
            ushort check2 = CalcCheckCode(t2, t1, rand2, rand1);

            code[index++] = (byte)(check1 & 0x00ff);
            code[index++] = (byte)((check1 >> 8) & 0x00ff);

            code[index++] = (byte)(check2 & 0x00ff);
            code[index++] = (byte)((check2 >> 8) & 0x00ff);

            byte[] activateFlagData = BitConverter.GetBytes(ActivateFlag);

            Array.Copy(activateFlagData, 0, code, index, activateFlagData.Length);

            return code;
        }

        public static byte[] GetSystemAcivateData(byte[] deviceId)
        {
            System.Diagnostics.Debug.Assert(deviceId != null && deviceId.Length == sizeof(uint));

            byte[] temp = new byte[activateData.Length];
            Array.Copy(activateData, temp, activateData.Length);

            byte[] code = GenerateActivateCode(deviceId);

            Array.Copy(code, 0, temp, sizeof(ushort), code.Length);

            return temp;
        }

        private static ushort CalcCheckCode(ushort c1, ushort c2, ushort c3, ushort c4)
        {
            ushort t1 = (ushort)(c1 ^ c3);
            ushort t2 = (ushort)(c1 + t1);
            ushort t3 = (ushort)(t2 ^ c4);
            return (ushort)(t3 + c2);
        }
    }
}
