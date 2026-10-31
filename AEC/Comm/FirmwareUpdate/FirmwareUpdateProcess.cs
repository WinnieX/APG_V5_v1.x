using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm.FirmwareUpdate
{
    public enum FirmwareUpdateProcess
    {
        BluetoothRename = 0,
        FirmwareUpdate,
        DSPProgrammerDownload,
        DSPProgrammerParamsUpload,
        MCUFlashDataLoad,
        MCUFlashDataSave,
    }
}
