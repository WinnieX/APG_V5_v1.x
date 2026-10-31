using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

using System.Collections;
using AEC.Tools;

namespace AEC.RemoteControl.Utility
{
    class SignatureCodeHelper
    {
        private const int maxID = 1000000;

        public static uint GetCode()
        {
            byte[] id = MachineIDHelper.MachineCode;
            if (id.Length >= 4)
            {
                return BitConverter.ToUInt32(id, 0) % maxID;
            }
            else
            {
                Random random = new Random();
                return (uint)random.Next(maxID);
            }
        }

        public static bool CheckCode(uint id)
        {
            return id < maxID;
        }
    }
}
