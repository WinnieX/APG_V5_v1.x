using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm
{
    public enum CommProcess
    {
        ProModeConnectLoadData = 0,
        ProModeCallDeviceMode,
        ProModeUploadData,
        ProModeSwitchDeviceWorkMode,
        EasyModeConnectLoadData,
        EasyModeUploadMultiModeData,
        EasyModeCallDeviceMode
    }
}
