using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm.CommDevices.HID
{
    public class HIDProtocol
    {
        public const int HID_FRAME_LENGTH = 64;
        public const int HID_PAYLOAD_LENGTH_SIZE = 2;
        public const int HID_PAYLOAD_MAX_LENGTH = HID_FRAME_LENGTH - HID_PAYLOAD_LENGTH_SIZE;

        public const ushort VID = 0x2E3C;
        public const ushort PID = 0x5745;

        public static byte[] ParsePayloadData
            (byte[] data)
        {
            System.Diagnostics.Debug.Assert(data != null && data.Length == HID_FRAME_LENGTH);

            int payloadLength = BitConverter.ToUInt16(data, 0);
            if (payloadLength > 0 && payloadLength <= HID_PAYLOAD_MAX_LENGTH)
            {
                byte[] payloadData = new byte[payloadLength];
                Buffer.BlockCopy(data, HID_PAYLOAD_LENGTH_SIZE, payloadData, 0, payloadData.Length);

                return payloadData;
            }

            return null;
        }

        public static List<byte[]> SplitData(byte[] data)
        {
            List<byte[]> list = new List<byte[]>();

            int srcIndex = 0;
            while(true)
            {
                int curr_frame_length = data.Length - srcIndex;
                if(curr_frame_length > HID_PAYLOAD_MAX_LENGTH)
                {
                    curr_frame_length = HID_PAYLOAD_MAX_LENGTH;
                }

                byte[] byteArray = new byte[HID_FRAME_LENGTH];

                byte[] lengthBuffer = BitConverter.GetBytes((ushort)curr_frame_length);
                Buffer.BlockCopy(lengthBuffer, 0, byteArray, 0, lengthBuffer.Length);
                Buffer.BlockCopy(data, srcIndex, byteArray, HID_PAYLOAD_LENGTH_SIZE, curr_frame_length);

                list.Add(byteArray);

                srcIndex += curr_frame_length;

                if(srcIndex >= data.Length)
                {
                    break;
                }
            }

            return list;
        }
    }
}
