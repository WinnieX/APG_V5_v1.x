using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;

namespace AEC.Comm.FirmwareUpdate
{
    public class FirmwareUpdateOutputArgs : MEventBusArgs
    {
        public enum LineControl
        {
            None,
            NewLine,
            Return,
        };

        public const string FirmwareUpdateOutput = "FirmwareUpdateOutput";
        public readonly bool isError;
        public readonly LineControl lineControl;
        public readonly string info;

        public FirmwareUpdateOutputArgs(string info_, LineControl lineControl_, bool isError_) :
            base(FirmwareUpdateOutput)
        {
            this.info = info_;
            this.lineControl = lineControl_;
            this.isError = isError_;
        }
    }

    public class FirmwareUpdateCompletedArgs : MEventBusArgs
    {
        public const string FirmwareUpdateCompleted = "FirmwareUpdateCompleted";

        public readonly AEC.Comm.FirmwareUpdate.FirmwareUpdateProcess process;
        public readonly bool isSuccess;
        public FirmwareUpdateCompletedArgs(AEC.Comm.FirmwareUpdate.FirmwareUpdateProcess process_, bool isSuccess_) :
            base(FirmwareUpdateCompleted)
        {
            this.process = process_;
            this.isSuccess = isSuccess_;
        }
    }

    public class FirmwareUpdateProgressArgs : MEventBusArgs
    {
        public const string FirmwareUpdateProgress = "FirmwareUpdateProgress";

        public readonly float progressRatio;
        public readonly bool isShowProgress;


        public FirmwareUpdateProgressArgs(float progressRatio_, bool isShowProgress_ = true) :
            base(FirmwareUpdateProgress)
        {
            this.progressRatio = progressRatio_;
            this.isShowProgress = isShowProgress_;
        }

        public FirmwareUpdateProgressArgs(bool isShowProgress_) :
            base(FirmwareUpdateProgress)
        {
            this.progressRatio = 0;
            this.isShowProgress = isShowProgress_;
        }
    }

    public class FirmwareInfoLoadedArgs : MEventBusArgs
    {
        public const string FirmwareInfoLoaded = "FirmwareInfoLoaded";

        public readonly DSPFirmwareInfo dspFirmwareInfo;
        public FirmwareInfoLoadedArgs(DSPFirmwareInfo dspFirmwareInfo_) :
            base(FirmwareInfoLoaded)
        {
            this.dspFirmwareInfo = dspFirmwareInfo_;
        }
    }

    public class DownloaderDeviceIdLoadedArgs : MEventBusArgs
    {
        public const string DownloaderDeviceIdLoaded = "DownloaderDeviceIdLoaded";

        public readonly DeviceID deviceId;
        public DownloaderDeviceIdLoadedArgs(DeviceID deviceId_) :
            base(DownloaderDeviceIdLoaded)
        {
            this.deviceId = deviceId_;
        }
    }

    public class DSPKeyHashLoadedArgs : MEventBusArgs
    {
        public const string DSPKeyHashLoaded = "DSPKeyHashLoaded";

        public readonly byte[] keyHash;
        public DSPKeyHashLoadedArgs(byte[] keyHash_) :
            base(DSPKeyHashLoaded)
        {
            this.keyHash = keyHash_;
        }
    }
}
