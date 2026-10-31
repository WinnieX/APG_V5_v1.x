using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Comm.CentralControl
{
    public class CentralControlProtocol
    {
        public enum Commmand : byte
        {
            SetMusicVolume = 0x22,               //读取状态以及版本
            SetMicVolume = 0x33,            //读寄存器
            SetEffectVolume = 0x44,           //写寄存器
            RecallPreset = 0x55,             //设置波特率
            SetMusicInputPort = 0x66,         //DSP电源控制
        }

        protected enum CmdFrame : int
        {
            Header1Index = 0x00,
            Header2Index = 0x01,
            Header3Index = 0x02,
            CmdIndex = 0x03,
            PayloadIndex = 0x04,
            VerificationIndex = 0x05,
        };

        public const byte CmdHeader1 = 0xFE;
        public const byte CmdHeader2 = 0xFD;
        public const byte CmdHeader3 = 0xFB;
        public const byte ResponseHeader1 = 0xFB;

        private const int CmdLength = 6;

        public static byte[] GetCommand(CentralControlCmd cmd)
        {
            switch(cmd.cmdType)
            {
                case Commmand.SetMusicVolume:
                    return GetSetMusicVolumeCmd(cmd.cmdPayload);

                case Commmand.SetMicVolume:
                    return GetSetMicVolumeCmd(cmd.cmdPayload);

                case Commmand.SetEffectVolume:
                    return GetSetEffectVolumeCmd(cmd.cmdPayload);

                case Commmand.RecallPreset:
                    return GetRecallPresetCmd(cmd.cmdPayload);

                case Commmand.SetMusicInputPort:
                    return GetSetMusicInputPortCmd(cmd.cmdPayload);
            }

            return null;
        }

        public static bool CheckResponseIsCompleted(List<byte> buffer)
        {
            System.Diagnostics.Debug.Assert(buffer != null);

            return buffer.Count == CmdLength;

            //if (buffer.Count != CmdLength)
            //{
            //    return false;
            //}

            //byte[] data = buffer.ToArray();

            //if (data[(int)CmdFrame.Header1Index] == ResponseHeader1 &&
            //    data[(int)CmdFrame.Header2Index] == CmdHeader2 &&
            //    data[(int)CmdFrame.Header3Index] == CmdHeader3)
            //{
            //    return GetVerification(data) == data[(int)CmdFrame.VerificationIndex];
            //}

            //return false;
        }

        public static bool CheckResponseIsOK(byte[] cmd, byte[] response)
        {
            if(cmd.Length != CmdLength || response.Length != CmdLength)
            {
                return false;
            }

            if(response[(int)CmdFrame.Header1Index] != ResponseHeader1)
            {
                return false;
            }

            for (int i = (int)CmdFrame.Header2Index; i < CmdLength; ++i)
            {
                if(response[i] != cmd[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static byte[] GetSetMusicVolumeCmd(byte volume)
        {
            byte[] cmd = GetCmd(Commmand.SetMusicVolume);

            FillUint8ToBuffer(cmd, (int)CmdFrame.PayloadIndex, volume);
            FillVerification(cmd);

            return cmd;
        }

        public static byte[] GetSetMicVolumeCmd(byte volume)
        {
            byte[] cmd = GetCmd(Commmand.SetMicVolume);

            FillUint8ToBuffer(cmd, (int)CmdFrame.PayloadIndex, volume);
            FillVerification(cmd);

            return cmd;
        }

        public static byte[] GetSetEffectVolumeCmd(byte volume)
        {
            byte[] cmd = GetCmd(Commmand.SetEffectVolume);

            FillUint8ToBuffer(cmd, (int)CmdFrame.PayloadIndex, volume);
            FillVerification(cmd);

            return cmd;
        }

        public static byte[] GetRecallPresetCmd(byte presetModeIndex)
        {
            byte[] cmd = GetCmd(Commmand.RecallPreset);

            FillUint8ToBuffer(cmd, (int)CmdFrame.PayloadIndex, presetModeIndex);
            FillVerification(cmd);

            return cmd;
        }

        public static byte[] GetSetMusicInputPortCmd(byte musicInputPortIndex)
        {
            byte[] cmd = GetCmd(Commmand.SetMusicInputPort);

            FillUint8ToBuffer(cmd, (int)CmdFrame.PayloadIndex, musicInputPortIndex);
            FillVerification(cmd);

            return cmd;
        }

        public static byte[] GetResponse(byte[] cmd)
        {
            cmd[(int)CmdFrame.Header1Index] = ResponseHeader1;
            return cmd;
        }

        private static byte[] GetCmd(Commmand command)
        {
            byte[] cmd = new byte[CmdLength];
            int index = 0;
            index = FillUint8ToBuffer(cmd, index, CmdHeader1);
            index = FillUint8ToBuffer(cmd, index, CmdHeader2);
            index = FillUint8ToBuffer(cmd, index, CmdHeader3);
            index = FillUint8ToBuffer(cmd, index, (byte)command);

            return cmd;
        }

        private static byte GetVerification(byte[] data)
        {
            System.Diagnostics.Debug.Assert(data != null && data.Length >= CmdLength);

            byte verification = (byte)(data[(int)CmdFrame.CmdIndex] ^ data[(int)CmdFrame.PayloadIndex]);

            return verification;
        }

        private static void FillVerification(byte[] cmd)
        {
            cmd[(int)CmdFrame.VerificationIndex] = GetVerification(cmd);
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

        private static int FillUint32ToBuffer(byte[] buffer, int index, UInt32 data)
        {
            buffer[index + 0] = (byte)((data >> 0) & 0xff);
            buffer[index + 1] = (byte)((data >> 8) & 0xff);
            buffer[index + 2] = (byte)((data >> 16) & 0xff);
            buffer[index + 3] = (byte)((data >> 24) & 0xff);

            return index + sizeof(UInt32);
        }
    }
}
