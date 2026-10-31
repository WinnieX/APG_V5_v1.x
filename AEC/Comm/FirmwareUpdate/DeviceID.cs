using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm.FirmwareUpdate
{
    public class DeviceID
    {
        public const int UID_LENGTH = 16;

        public readonly byte[] uid = new byte[UID_LENGTH];

        public override string ToString()
        {
            int i = UID_LENGTH / sizeof(uint) - 1;
            for(; i > 0; --i)
            {
                if(BitConverter.ToUInt32(uid, i * sizeof(uint)) != 0)
                {
                    break;
                }
            }

            byte[] validData = new byte[(i + 1) * sizeof(uint)];
            Array.Copy(this.uid, validData, validData.Length);

            return Utils.ByteArrayToHexStr(validData, string.Empty);
        }
    }
}
