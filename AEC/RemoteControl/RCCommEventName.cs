using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RemoteControl
{
    public class RCCommEventName
    {
        public const string CommInterrupted = "CommInterrupted";
        public const string CommStateChanged = "CommStateChanged";
        public const string CommProgressUpdated = "CommProgressUpdated";
        public const string CommStartSaveMode = "CommStartSaveMode";
        public const string CommSaveModeCompleted = "CommSaveModeComplete";
        public const string CommConnectionIsDisconnected = "CommConnectionIsDisconnected";
        public const string CommCompleted = "CommCompleted";
        public const string CommOutputLimiterStatusLoaded = "CommOutputLimiterStatusLoaded";
        public const string CommLoadEffectorModelCompleted = "CommLoadEffectorModelCompleted";
        public const string CommLoadDataCompleted = "CommLoadDataCompleted";
        public const string CommCallDeviceModeCompleted = "CommCallDeviceModeCompleted";
        public const string CommUploadDataCompleted = "CommUploadDataCompleted";
        public const string CommDataParamChanged = "CommDataParamChanged";
        public const string CommEQChanged = "CommEQChanged";
        public const string CommPasswordChanged = "CommPasswordChanged";
        public const string CommSaveDeviceMode = "CommSaveDeviceMode";
        public const string CommUpdateModuleAllData = "CommUpdateModuleAllData";
    }
}
