using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Comm.FirmwareUpdate
{
    public class ProgrammingTaskBase
    {
        public bool isDownload { get; set; }
    }

    public class DSPProgrammingTask : ProgrammingTaskBase
    {
        public string dspCrcImageFilePath { get; set; }
        public byte[] fileBuffer { get; set; }
        public uint binSize { get; set; }	//bin文件size
        public uint wavSize { get; set; }
        public uint envAddr { get; set; }
        public byte[] envConfigData { get; set; }
    }

    public class MCUProgrammingTask : ProgrammingTaskBase
    {
        public string mcuBinFilePath { get; set; }
        public byte[] fileBuffer { get; set; }
        public uint binSize { get; set; }	//bin文件size

        public uint mcuAppVectorOffset { get; set; }

        public bool restorePreset { get; set; }
    }

    public class DSPAndMCUProgrammingTask
    {
        public CommPort commPort{get;set;}

        public readonly DSPProgrammingTask dspProgrammingTask;
        public readonly MCUProgrammingTask mcuProgrammingTask;

        public DSPAndMCUProgrammingTask(CommPort commPort_, DSPProgrammingTask dspProgrammingTask_, MCUProgrammingTask mcuProgrammingTask_)
        {
            Debug.Assert(commPort_ != null && (dspProgrammingTask_ != null || mcuProgrammingTask_ != null));

            this.commPort = commPort_;
            this.dspProgrammingTask = dspProgrammingTask_;
            this.mcuProgrammingTask = mcuProgrammingTask_;
        }
    }

    public class DspProgrammerDownloadTask
    {
        public CommPort commPort { get; set; }
        public DSPFirmwarePackage dspFwPackage { get; set; }
        public bool isOnlyDownloadParams { get; set; }
        public bool isDownloadCompleteBin { get; set; }
    }

    public class DspProgrammerParamsUploadTask
    {
        public const int UID_LENGTH = 16;
        public CommPort commPort { get; set; }
        //public DspProgrammerDownloadParams downloadParams { get; set; }
        //public DeviceID deviceId { get; set; }
    }

    public class BluetoothRenameTask
    {
        public CommPort commPort { get; set; }

        public readonly BluetoothRename bluetoothRename;

        public BluetoothRenameTask(CommPort commPort_, BluetoothRename bluetoothRename_)
        {
            Debug.Assert(bluetoothRename_ != null);

            this.commPort = commPort_;
            this.bluetoothRename = bluetoothRename_;
        }
    }

    public class FirmwareUpdateTask
    {
        public CommPort commPort{get;set;}

        public readonly FirmwarePackage firmware;

        public FirmwareUpdateTask(CommPort commPort_, FirmwarePackage firmware_)
        {
            Debug.Assert(firmware_ != null);

            this.commPort = commPort_;
            this.firmware = firmware_;
        }
    }

    public class ExportDataProperties
    {
        public uint startAddress { get; set; }
        public uint dataSize { get; set; }
        public ByteArrayEncoding.Encoding encoding { get; set; }
    }

    public class MCUFlashDataLoadTask
    {

        public readonly ExportDataProperties exportDataProperties;
        public CommPort commPort { get; set; }
        public uint startAddress
        { 
            get
            {
                return this.exportDataProperties.startAddress;
            }
        }
        
        public uint dataSize 
        { 
            get
            {
                return this.exportDataProperties.dataSize;
            }
        }
        
        public ByteArrayEncoding.Encoding encoding 
        {
            get
            {
                return this.exportDataProperties.encoding;
            }
        }
        public DataBlock dataBlcok { get; set; }

        public EventHandler DataLoaded;

        public uint endAddress
        {
            get
            {
                return this.startAddress + dataSize;
            }
        }

        public MCUFlashDataLoadTask(ExportDataProperties exportDataProperties_)
        {
            Debug.Assert(exportDataProperties_ != null);
            this.exportDataProperties = exportDataProperties_;
        }
    }

    public class MCUFlashDataSaveTask
    {
        public CommPort commPort { get; set; }
        public DataBlock dataBlcok { get; set; }
    }
}
