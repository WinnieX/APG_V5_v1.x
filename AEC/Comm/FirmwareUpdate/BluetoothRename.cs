using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm.FirmwareUpdate
{
    public class BluetoothRename
    {
        public string btName { get; set; }
        public string mcuFirmwareSet { get; set; }
                
        public BluetoothRename()
        {
            btName = string.Empty;
            mcuFirmwareSet = string.Empty;
        }
    }

    //public class DspEnvData
    //{

    //}
}
