using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using AEC.Comm.CommDevices;
using AEC.Tools;
using AEC.Core;
using AEC.Comm.ActivateDevice;
using AEC.Misc;

namespace AEC.Comm.FirmwareUpdate
{
    public class FirmwareUpdateService
    {
        public enum TargetDeviceFamily
        {
            Unknown,    //未知的,
            VKx,
            VKxS,
        };

        public enum FirmwareUpdateProtocolVersion
        {
            Unknown,    //未知的,
            V1,         //V1版本
            V2,         //V2版本
            V3,         //V3版本
        };

        public enum FirmwareType
        {
            MCU_APP = 0x0,
            DSP = 0x1,
        }
        public const int DefaultDSPBaudRate = 115200;

        public const int DSPDownloaderToUpcBaudRate = 1000000;
        //public const int DownloadDataMCUBaudRate = 115200 * 2;
        //public const int DownloadDataMCUBaudRate = 115200;

        public const int RS232BaudRateDefault = AEC.Comm.CommDevices.SerialPort.defaultBaudRate;
        public const int RS232BaudRateSecondary = AEC.Comm.CommDevices.SerialPort.secondaryBaudRate;

        public const int CommWaitTimeUnit = 1024;
        public const int PreDownloadWait = CommWaitTimeUnit * 0x04;
        public const int WriteFlash4KWait = CommWaitTimeUnit * 0x02;
        public const int ReadFlash4KWait = CommWaitTimeUnit * 0x02;
        public const int CheckFlashCRCWait = CommWaitTimeUnit * 0x04;
        public const int EndDownloadWait = CommWaitTimeUnit * 0x01;
        public const int DownloadCompletedWait = CommWaitTimeUnit * 0x01;

        public const int SetBaudRateDelay = 0x100;

        private static Localization localization = Config.GetInstance().localization;

        private const string SkipLoadMCUFirmwareVersionPassword = "*******";
        private const int CommTimeOut = 0x400;
        private const int HandshakeRetries = 2;
        private static readonly CommProtocolDetector defaultProtocolDetector = new CommProtocolDetector(CommProtocolBase.CheckCommCmdType, CommProtocolBase.CheckReadResponseIsCompleted, CommProtocolBase.CheckWriteResponseIsOk);

        private static readonly byte[] handshakeCmd = CommProtocolBase.GetHandshakeCmd();
        private static readonly byte[] offlineCmd = CommProtocolBase.GetOfflineCmd();
        //VKx Flash数据
        //flash_base 0x08000000
        //flash_size 0x20000
        //flash_end	0x08020000
        //device_info_data_start_addr 0x0801F800
        //device_custom_module_start_addr 0x0801F000
        //system_common_data_start_addr 0x801E800
        //preset_mode_data_start_addr  0x0801A000

        //VKxS Flash数据
        //flash_base 0x08000000
        //flash_size 0x20000
        //flash_end	0x08020000
        //device_info_data_start_addr 0x0801FC00
        //system_common_data_start_addr 0x0801F800
        //preset_mode_data_start_addr  0x0801B000
        //x5_preset_mode_data_start_addr  0x08017000
        
        public readonly byte[] dspkey = new byte[]
        {
            0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00,
        };

        public const int efuseControl = 0x26;

        private System.ComponentModel.BackgroundWorker bgwComm;

        protected FirmwareUpdateDevice fuDevice;

        private readonly List<byte> _recvBuffer = new List<byte>();

        public readonly MEventBus eventBus = new MEventBus();

        private const uint Retries = 3;
        private const uint DspIndex = 0;
        private const uint DSPResetDuration = 0x40;
        private const uint DownloadBaudRate = 1500000;
        //private const uint CrcFileLength = 0x001ff000;
        private const uint INVALID_DSP_BIN_SIZE = 0xffffffff;

        private const uint DSP_TRNG_ENABLE_ADDRESS = 0x018B8000;
        private const uint DSP_TRNG_DATA_ADDRESS = 0x018B8004;

        public const int Size1K = 1 << 10;
        public const int Size2K = Size1K * 2;
        public const int Size4K = Size1K * 4;
        private const byte DefaultFillData = 0xff;

        private const int BIN_WRITE_START_ADDRESS = 0x1000;

        private DateTime startTimeStamp = DateTime.Now;

        private BTDeviceInfo lastDeviceInfo;

        public static readonly byte[] keyHash = new byte[]
        {
            0x61, 0x32, 0x48, 0xEA, 0xB2, 0x6E, 0x44, 0xEA, 0x0A, 0x09, 0x32, 0x2C, 0x7C, 0x3E, 0xB0, 0xC5, 
            0x06, 0x0C, 0x14, 0x96, 0x42, 0xB9, 0x4B, 0x95, 0x75, 0xC3, 0xB6, 0xA1, 0x11, 0x2A, 0xE2, 0x9C,
        };

        public enum FlashOperateSize : int
        {
            Size64k = 0x10000,
            Size32k = 0x08000,
            Size16k = 0x04000,
            Size8k = 0x02000,
            Size4k = 0x01000,
            Size2k = 0x00800,
            Size1k = 0x00400,
        };

        public bool isBusy
        {
            get
            {
                return this.bgwComm.IsBusy;
            }
        }

        public FirmwareUpdateService()
        {
            this.bgwComm = new System.ComponentModel.BackgroundWorker();
            this.bgwComm.DoWork += bgwComm_DoWork;
            //this.bgwComm.RunWorkerCompleted += bgwComm_RunWorkerCompleted;
        }

        private void bgwComm_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            bool isSuccess = false;

            FirmwareUpdateWrokerArgs workerArgs = e.Argument as FirmwareUpdateWrokerArgs;
            if (workerArgs == null || workerArgs.state == null)
            {
                return;
            }

            try
            {
                switch (workerArgs.firmwareUpgradeProcess)
                {
                    case FirmwareUpdateProcess.BluetoothRename:
                        isSuccess = BluetoothRename((BluetoothRenameTask)workerArgs.state);                        
                        break;

                    case FirmwareUpdateProcess.FirmwareUpdate:
                        isSuccess = FirmwareUpdate((FirmwareUpdateTask)workerArgs.state);
                        if(isSuccess)
                        {
                            this.OutputInfoLine(localization[Localization.firmwareUpdate_UpdatedSuccessfully]);
                        }
                        break;

                    case FirmwareUpdateProcess.DSPProgrammerDownload:
                        isSuccess = DspProgrammerDownload((DspProgrammerDownloadTask)workerArgs.state);
                        break;

                    case FirmwareUpdateProcess.DSPProgrammerParamsUpload:
                        isSuccess = DspProgrammerParamsUpload((DspProgrammerParamsUploadTask)workerArgs.state);
                        break;

                    case FirmwareUpdateProcess.MCUFlashDataLoad:
                        isSuccess = MCUFlashDataLoad((MCUFlashDataLoadTask)workerArgs.state);
                        break;

                    case FirmwareUpdateProcess.MCUFlashDataSave:
                        isSuccess = MCUFlashDataSave((MCUFlashDataSaveTask)workerArgs.state);
                        break;

                    default:
                        this.OutputInfoLine("Firmware update : unsupported command!");
                        break;
                }                
            }
            catch(Exception ex)
            {
                LogHelper.WriteInfo("bgwComm_DoWork()...Exception : ", ex.Message);
            }

            this.eventBus.NotifyObeserver(new FirmwareUpdateCompletedArgs(workerArgs.firmwareUpgradeProcess, isSuccess));

            //DownloadTask task = e.Argument as DownloadTask;
            //this.RunTask(task);
            
            //Log.OutputLine("bgwComm_DoWork, thread id = {0}", Thread.CurrentThread.ManagedThreadId);
            //CommArge arge = e.Argument as CommArge;
            //bool result = false;
            //switch (arge.process)
            //{
            //    case CommProcess.ProModeConnectLoadData:
            //        result = ConnectAndLoadData(CommProcess.ProModeConnectLoadData);
            //        break;

            //    case CommProcess.ProModeUploadData:
            //        result = ProModeUploadAllModuleData();
            //        break;

            //    case CommProcess.ProModeCallDeviceMode:
            //        result = CallDeviceMode(CommProcess.ProModeCallDeviceMode, (short)(arge.state));
            //        break;

            //    case CommProcess.EasyModeConnectLoadData:
            //        result = ConnectAndLoadData(CommProcess.EasyModeConnectLoadData);
            //        break;

            //    case CommProcess.EasyModeUploadMultiModeData:
            //        result = EasyModeUploadMultiData(arge.state);
            //        break;

            //    case CommProcess.EasyModeCallDeviceMode:
            //        result = CallDeviceMode(CommProcess.ProModeCallDeviceMode, (short)(arge.state));
            //        break;
            //}

            ////Log.OutputLine("加载数据完成****************");
            //commEvent.NotifyObeserver(new CommCompletedArgs(new CommResult(arge.process, result)));
            ////Log.OutputLine("通讯结束!");
        }

        //private void bgwComm_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    //if (!e.Cancelled)
        //    //{
        //    //    //Console.WriteLine("WorkerCompleted");
        //    //    WorkCompletedHandler((bool)e.Result);
        //    //}
        //}

        public bool DspProgrammerParamsUpload(DspProgrammerParamsUploadTask task)
        {
            try
            {
                this.CloseCommDevice();

                //String[] ports = SerialPort.GetValidPorts();
                if (!this.InitPort(task.commPort))
                {
                    return false;
                }

                //if (!DSP_DownloadBinToMCUFlash(task))
                //{
                //    return false;
                //}

                byte[] keyHash = DSP_ReadDSPKeyHash();
                if (keyHash == null)
                {
                    return false;
                }
                this.eventBus.NotifyObeserver(new DSPKeyHashLoadedArgs(keyHash));
                
                DSPFirmwareInfo dspFirmwareInfo = DSP_ReadDSPFirmwareInfo();
                if (dspFirmwareInfo == null)
                {
                    return false;
                }
                this.eventBus.NotifyObeserver(new FirmwareInfoLoadedArgs(dspFirmwareInfo));

                DeviceID deviceId = DSP_ReadDeviceId();
                if (deviceId == null)
                {
                    return false;
                }
                this.eventBus.NotifyObeserver(new DownloaderDeviceIdLoadedArgs(deviceId));

                //if (!DSP_PreDownload())
                //{
                //    LogHelper.WriteInfo("预下载失败！");
                //    return false;
                //}

                //if (!DSP_Download(task))
                //{
                //    LogHelper.WriteInfo("下载失败！");
                //    return false;
                //}

                //if (!DSP_EndDownload(task))
                //{
                //    LogHelper.WriteInfo("结束下载失败！");
                //    return false;
                //}

                LogHelper.WriteInfo("发送数据完成");

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo("DspProgrammerParamsUpload()...异常：{0}", ex.Message);

                return false;
            }
            finally
            {
                CloseCommDevice();
            }
        }

        public bool DspProgrammerDownload(DspProgrammerDownloadTask task)
        {
            try
            {
                if (!CheckCrcFilePath(task))
                {
                    //Log.OutputLine("CRC文件检测失败！"); 
                    this.OutputErrorInfoLine("CRC文件检测失败！");
                    return false;
                }

                this.CloseCommDevice();

                //String[] ports = SerialPort.GetValidPorts();
                if(!this.InitPort(task.commPort))
                {
                    return false;
                }

                if (!task.isOnlyDownloadParams && !DSP_DownloadBinToMCUFlash(task))
                {
                    return false;
                }

                if (!DSP_WriteProgrammingParams(task))
                {
                    return false;
                }

                if (!DSP_WriteDSPKeyHashParams())
                {
                    return false;
                }

                //if (!DSP_PreDownload())
                //{
                //    LogHelper.WriteInfo("预下载失败！");
                //    return false;
                //}

                //if (!DSP_Download(task))
                //{
                //    LogHelper.WriteInfo("下载失败！");
                //    return false;
                //}

                //if (!DSP_EndDownload(task))
                //{
                //    LogHelper.WriteInfo("结束下载失败！");
                //    return false;
                //}

                LogHelper.WriteInfo("发送数据完成");

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo("DspProgrammerDownload()...异常：{0}", ex.Message);

                return false;
            }
            finally
            {
                CloseCommDevice();
            }
        }

        private bool ConnectToBootloader(CommPort commPort, out FirmwareUpdateProtocolVersion protocolVersion)
        {
            rs232BaudRateIsDefault = true;//使用默认波特率

            this.CloseCommDevice();

            protocolVersion = FirmwareUpdateService.FirmwareUpdateProtocolVersion.Unknown;

            if (commPort == null)       //如果是USB自动识别的情况
            {
                DateTime startTime = DateTime.Now;
                DateTime lastTime = startTime;

                int count = 0;

                while (true)
                {
                    DateTime currTime = DateTime.Now;
                    if ((currTime - startTime).TotalSeconds > 0x08)
                    {
                        break;
                    }

                    double interval = (currTime - lastTime).TotalMilliseconds;
                    if (interval < 64)
                    {
                        System.Threading.Thread.Sleep((int)(64 - interval));
                    }

                    lastTime = currTime;

                    ++count;
                    LogHelper.WriteInfo("第 {0} 次扫描USB端口...", count);
                    this.OutputInfoLineReturn("Scanning USB ports...{0}", count);

                    AEC.Comm.CommDevices.HID.HIDDevice.InterfaceDetails[] hidInterfaces = FUHIDDevice.GetValidDevices();
                    if (hidInterfaces.Length > 0)
                    {
                        var hid = hidInterfaces[0];
                        commPort = new HIDCommPort(hid.devicePath, hid.serialNumber);

                        LogHelper.WriteInfo("成功识别到USB端口 {0}...", hid.devicePath);

                        break;
                    }
                }
            }

            if (commPort == null)
            {
                //LogHelper.WriteInfo("扫描USB端口失败");
                this.OutputErrorInfoLine("扫描USB端口失败！");
                this.OutputInfoLine("Failed to scan USB port");
                return false;
            }
            //else
            //{
            //    LogHelper.WriteInfo("扫描USB端口 成功");
            //    return true;
            //}

            this.OutputInfoLine("Initializing port...");

            if (!this.InitPort(commPort))
            {
                return false;
            }

            //this.OutputInfoLine("Initialized {0} port successful.", commPort.displayName);

            LogHelper.WriteInfo("MCU 握手...1");
            this.OutputInfoLine("Handshaking with MCU...");

            uint programType = 0x00;

            bool handshakeIsOk = false;
            for (int i = 0; i < 3; ++i)
            {
                if (MCU_BootloaderHandshake(out programType, out protocolVersion))
                {
                    handshakeIsOk = true;
                    break;                    
                }
            }

            //如果第一次握手失败，且串口波特率为自动
            if (!handshakeIsOk && commPort.type == CommPortType.RS232 && (commPort as RS232CommPort).isAutoBaudRate)
            {
                for (int i = 0; i < 5; ++i)
                {
                    this.fuDevice.SyncSend(new byte[] { 0x00 }, 0);

                    if (MCU_BootloaderHandshake(out programType, out protocolVersion))
                    {
                        handshakeIsOk = true;
                        break;
                    }
                }

                if (!handshakeIsOk)
                {
                    this.CloseCommDevice();

                    rs232BaudRateIsDefault = !rs232BaudRateIsDefault; //使用其他波特率

                    if (!this.InitPort(commPort))
                    {
                        return false;
                    }

                    for (int i = 0; i < 3; ++i)
                    {
                        if (MCU_BootloaderHandshake(out programType, out protocolVersion))
                        {
                            handshakeIsOk = true;
                            break;
                        }
                    }

                    if (!handshakeIsOk)
                    {
                        for (int i = 0; i < 5; ++i)
                        {
                            this.fuDevice.SyncSend(new byte[] { 0x00 }, 0);

                            if (MCU_BootloaderHandshake(out programType, out protocolVersion))
                            {
                                handshakeIsOk = true;
                                break;
                            }
                        }
                    }
                }
            }

            if (!handshakeIsOk)
            {
                LogHelper.WriteInfo("MCU 握手失败！");
                this.OutputInfoLine("Failed to handshake with MCU !");
                return false;
            }                

            if (programType != 0)
            {
                LogHelper.WriteInfo("MCU 正在执行App状态，准备跳转到Bootloader");

                uint delay = 0x100;

                this.OutputInfoLine("Jumpping from App to Bootloader...");

                if (!MCU_AppJumpToBootloader(delay))
                {
                    this.OutputInfoLine("Failed to jump from App to Bootloader...");
                    return false;
                }

                //this.OutputInfoLine("MCU jumpped from App to Bootloader successful.");

                LogHelper.WriteInfo("关闭设备");
                this.CloseCommDevice();

                System.Threading.Thread.Sleep((int)delay);

                LogHelper.WriteInfo("准备再次初始化端口");

                int count = 0;
                bool portIsInit = false;
                while (++count < 0x80)
                {
                    System.Threading.Thread.Sleep(0x40);

                    LogHelper.WriteInfo("InitPort...{0}", count);

                    if (this.InitPort(commPort))
                    {
                        portIsInit = true;
                        LogHelper.WriteInfo("The port is initialized successful");
                        //this.OutputInfoLine("The port is initialized successful");
                        break;
                    }

                    LogHelper.WriteInfo("InitPort {0} failed!", count);
                }

                if (!portIsInit)
                {
                    LogHelper.WriteInfo("Failed to initialize {0}", commPort.displayName);
                    this.OutputInfoLine("Failed to initialize {0}", commPort.displayName);
                    return false;
                }

                LogHelper.WriteInfo("再次尝试握手");

                handshakeIsOk = false;
                for (int i = 0; i < 3; ++i)
                {
                    if (MCU_BootloaderHandshake(out programType, out protocolVersion))
                    {
                        handshakeIsOk = true;
                        break;

                    }
                }

                //如果握手失败，且串口波特率为自动
                if (!handshakeIsOk && commPort.type == CommPortType.RS232 && (commPort as RS232CommPort).isAutoBaudRate)
                {
                    for (int i = 0; i < 5; ++i)
                    {
                        this.fuDevice.SyncSend(new byte[] { 0x00 }, 0);

                        if (MCU_BootloaderHandshake(out programType, out protocolVersion))
                        {
                            handshakeIsOk = true;
                            break;
                        }
                    }

                    if (!handshakeIsOk)
                    {
                        this.CloseCommDevice();

                        rs232BaudRateIsDefault = !rs232BaudRateIsDefault; //继续切换波特率

                        if (!this.InitPort(commPort))
                        {
                            return false;
                        }

                        for (int i = 0; i < 5; ++i)
                        {
                            this.fuDevice.SyncSend(new byte[] { 0x00 }, 0);

                            if (MCU_BootloaderHandshake(out programType, out protocolVersion))
                            {
                                handshakeIsOk = true;
                                break;
                            }
                        }
                    }
                }

                if (!handshakeIsOk)
                {
                    LogHelper.WriteInfo("Faild to handshake with MCU !");
                    this.OutputInfoLine("Faild to handshake with MCU !");
                    return false;
                }

                //LogHelper.WriteInfo("再次尝试握手");
                //if (!MCU_BootloaderHandshake(out programType, out protocolVersion))
                //{
                //    LogHelper.WriteInfo("MCU 握手失败！");
                //    return false;
                //}

                if (programType != 0)
                {
                    LogHelper.WriteInfo("MCU is still running App, Failed !");
                    this.OutputInfoLine("MCU is still running App, Failed !");
                    return false;
                }
            }
            else
            {
                LogHelper.WriteInfo("MCU is running Bootloader");
            }

            System.Threading.Thread.Sleep(0x40);

            if (protocolVersion != FirmwareUpdateProtocolVersion.V1 && 
                protocolVersion != FirmwareUpdateProtocolVersion.V2 &&
                protocolVersion != FirmwareUpdateProtocolVersion.V3)
            {
                this.OutputInfoLine("Unsupported protocol version : {0} ", (int)protocolVersion);
                return false;
            }

            return true;
        }

        private CommDevice loadFirmwareCommDevice = null;
        private CommDevice CreateCommDevice(CommPort commPort)
        {
            Debug.Assert(commPort != null);
            if (commPort.type == CommPortType.USB)
            {
                HIDCommPort hidCommPort = commPort as HIDCommPort;
                AEC.Comm.CommDevices.HID.HIDDevice.InterfaceDetails[] hidInterfaces = FUHIDDevice.GetValidDevices();
                if (hidInterfaces.Length > 0)
                {
                    foreach (var hid in hidInterfaces)
                    {
                        if (hid.serialNumber == hidCommPort.serialNumber)
                        {
                            HIDDevice hidDevice = new HIDDevice(defaultProtocolDetector, this._recvBuffer);
                            hidDevice.devicePath = hid.devicePath;
                            hidDevice.serialNumber = hid.serialNumber;

                            this.OutputInfoLine("Create Device : {0}...Success.", hidDevice.connectionInfo);

                            return hidDevice;

                        }
                    }
                }

                this.OutputInfoLine("Create Device : {0}...Failed!", hidCommPort.displayName);

                return null;
            }
            else if(commPort.type == CommPortType.RS232)
            {
                RS232CommPort rs232CommPort = commPort as RS232CommPort;
                string[] ports = System.IO.Ports.SerialPort.GetPortNames();
                foreach (var port in ports)
                {
                    if (rs232CommPort.portName == port)
                    {
                        SerialPort sp = new SerialPort(defaultProtocolDetector, this._recvBuffer);
                        sp.portName = port;
                        sp.buadRate = rs232CommPort.baudRate;

                        this.OutputInfoLine("Init Device : {0}...Success.", sp.connectionInfo);

                        return sp;
                    }
                }

                this.OutputInfoLine("Init Device : {0}...Failed!", rs232CommPort.portName);
            }

            return null;
        }

        public bool CheckMcuFirmwareVersin(CommPort commPort, string mcuFirmwareSet)
        {
            try
            {
                if (mcuFirmwareSet == SkipLoadMCUFirmwareVersionPassword)
                {
                    return true;
                }

                this.CloseDevice();

                loadFirmwareCommDevice = CreateCommDevice(commPort);
                if(loadFirmwareCommDevice == null)
                {
                    return false;
                }

                if (!loadFirmwareCommDevice.Open())
                {
                    this.OutputInfoLine("Init comm device failed!");
                    return false;
                }
                this.OutputInfoLine("Init comm device success.");

                bool commOk = false;
                for (int i = 0; i < HandshakeRetries; ++i)
                {
                    if(i == 0)
                    {
                        this.OutputInfoLine("Send handshke request...");
                    }
                    else
                    {
                        this.OutputInfoLineReturn("Send handshke request...");
                    }
                    
                    loadFirmwareCommDevice.SyncSend(handshakeCmd, CommTimeOut);

                    //System.Threading.Thread.Sleep(0x100);

                    commOk = loadFirmwareCommDevice.detector.checkWriteResponseIsCompleted(this._recvBuffer);

                    if (commOk)
                    {
                        break;
                    }

                    this.OutputInfoLineReturn("Failed to check handshke response!");
                }

                if (!commOk)
                {
                    this.OutputInfoLine("Handshake failed!");
                    return false;
                }
                this.OutputInfoLine("Handshake success.");

                MCUFirmware mcuFirmware = LoadDeviceFirmwareVersionData();
                if(mcuFirmware == null)
                {
                    return false;
                }
                
                this.OutputInfoLine("Send disconnect request...");
                loadFirmwareCommDevice.SyncSend(offlineCmd, CommTimeOut);
                if (!loadFirmwareCommDevice.detector.checkWriteResponseIsCompleted(this._recvBuffer))
                {
                    this.OutputInfoLine("Disconnect failed.");
                    return false;
                }
                this.OutputInfoLine("Disconnect success.");

                string prototypeVersion = mcuFirmware.GetPrototypeAndVersion();

                //Log.OutputLine(prototypeVersion);

                mcuFirmwareSet = StringHelper.RemoveConsecutiveDuplicates(mcuFirmwareSet, '*');
                string[] firmwares = mcuFirmwareSet.Split(new char[] { ' ', ';', '|' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var firmware in firmwares)
                {
                    if (firmware == "*" || firmware.Equals(prototypeVersion, StringComparison.OrdinalIgnoreCase))
                    {
                        this.OutputInfoLine("The firmware {0} is supported.", prototypeVersion);
                        return true;
                    }
                }

                this.OutputInfoLine("The firmware {0} is not supported!", prototypeVersion);

                return false;
            }
            catch(Exception e)
            {

            }
            finally
            {
                this.CloseDevice();
            }

            return false;
        }

        private void CloseDevice()
        {
            if (this.loadFirmwareCommDevice != null)
            {
                this.loadFirmwareCommDevice.Close();
                this.loadFirmwareCommDevice = null;
            }
        }

        private MCUFirmware LoadDeviceFirmwareVersionData()
        {
            MCUFirmware mcuFirmware = new MCUFirmware();

            if (!LoadDeviceFirmwarePrototype(mcuFirmware))
            {
                OutputInfoLine("Load firmware prototype failed!");
                return null;
            }

            OutputInfoLine("Load firmware prototype success.");

            if (!LoadDeviceFirmwareVersion(mcuFirmware))
            {
                OutputInfoLine("Load firmware version failed!");
                return null;
            }

            OutputInfoLine("Load firmware version success.");

            return mcuFirmware;
        }

        private bool LoadDeviceFirmwarePrototype(MCUFirmware mcuFirmware)
        {
            System.Diagnostics.Debug.Assert(this.loadFirmwareCommDevice != null);

            byte[] cmd = CommProtocolBase.GetLoadSystemDataCmd(SystemCommProtocol.SystemParamAddr.prototypeModel);

            this.loadFirmwareCommDevice.SyncSend(cmd, CommTimeOut);

            if (!loadFirmwareCommDevice.detector.checkReadResponseIsCompleted(this._recvBuffer))
            {
                //OutputInfo("读取设备原型失败！");
                return false;
            }

            byte[] data = CommProtocolBase.ParseSystemData(this._recvBuffer.ToArray(), SystemCommProtocol.SystemParamAddr.prototypeModel);
            if(data == null)
            {
                return false;
            }

            mcuFirmware.prototype = Encoding.ASCII.GetString(data).TrimEnd('\0');

            return true;
        }

        private bool LoadDeviceFirmwareVersion(MCUFirmware mcuFirmware)
        {
            System.Diagnostics.Debug.Assert(this.loadFirmwareCommDevice != null);

            byte[] cmd = CommProtocolBase.GetLoadSystemDataCmd(SystemCommProtocol.SystemParamAddr.firmwareVersion);

            this.loadFirmwareCommDevice.SyncSend(cmd, CommTimeOut);

            if (!loadFirmwareCommDevice.detector.checkReadResponseIsCompleted(this._recvBuffer))
            {
                //OutputInfo("读取设备原型失败！");
                return false;
            }

            byte[] data = CommProtocolBase.ParseSystemData(this._recvBuffer.ToArray(), SystemCommProtocol.SystemParamAddr.firmwareVersion);
            if (data == null)
            {
                return false;
            }

            mcuFirmware.version = BitConverter.ToUInt32(data, 0);

            return true;
        }

        public bool BluetoothRename(BluetoothRenameTask task)
        {
            try
            {
                if (!CheckMcuFirmwareVersin(task.commPort, task.bluetoothRename.mcuFirmwareSet))
                {
                    return false;
                }

                FirmwareUpdateProtocolVersion protocolVersion;

                if (!ConnectToBootloader(task.commPort, out protocolVersion))
                {
                    return false;
                }

                if (task.bluetoothRename != null && !string.IsNullOrEmpty(task.bluetoothRename.mcuFirmwareSet))
                {
                    LogHelper.WriteInfo("Start rename the Bluetooth name...");
                    this.OutputInfoLine("Start rename the Bluetooth name...");
                    if (!DSPBluetoothRename(task.bluetoothRename, protocolVersion))
                    {
                        return false;
                    }
                }
                else
                {
                    LogHelper.WriteInfo("Bluetooth rename task error!");
                    return false;
                }

                bool mcuFirmwareInvalid = false;
                //this.OutputInfoLine("Getting MCU firmware valid flag...");
                if (!MCU_BootloaderGetFirmwareFlagTest((uint)FirmwareType.MCU_APP, ref mcuFirmwareInvalid))
                {
                    LogHelper.WriteInfo("获取 MCU 固件无效标志 失败");
                    this.OutputInfoLine("Failed to get MCU firmware valid flag.");
                    return false;
                }

                if (mcuFirmwareInvalid)
                {
                    LogHelper.WriteInfo("MCU 固件 Error!");
                    this.OutputInfoLine("MCU firmware is invalid");
                    return false;
                }

                bool dspFirmwareInvalid = false;
                //this.OutputInfoLine("Getting DSP firmware valid flag...");
                if (!MCU_BootloaderGetFirmwareFlagTest((uint)FirmwareType.DSP, ref dspFirmwareInvalid))
                {
                    LogHelper.WriteInfo("获取 DSP 固件无效标志 失败");
                    this.OutputInfoLine("Failed to get DSP firmware valid flag.");
                    return false;
                }

                if (dspFirmwareInvalid)
                {
                    LogHelper.WriteInfo("DSP 固件 Error!");
                    this.OutputInfoLine("DSP firmware is invalid");
                    return false;
                }

                if (!mcuFirmwareInvalid && !dspFirmwareInvalid)
                {
                    LogHelper.WriteInfo("MCU  和 DSP 固件OK！");
                    //this.OutputInfoLine("Jumpping from Bootloader to App...");
                    if (!MCU_BootloaderJumpToAPP(protocolVersion))
                    {
                        LogHelper.WriteInfo("Bootloader跳转到App！");
                        this.OutputInfoLine("Failed to jump from Bootloader to App...");
                        return false;
                    }

                    //this.OutputInfoLine("MCU Jumpped from Bootloader to App successful");
                }

                LogHelper.WriteInfo("Bluetooth rename...finish");

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo("DspAndMcuProgramming()...异常：{0}", ex.Message);

                return false;
            }
            finally
            {
                CloseCommDevice();
            }
        }

        public bool FirmwareUpdate(FirmwareUpdateTask task)
        {
            try
            {
                FirmwareUpdateProtocolVersion protocolVersion;

                if (!ConnectToBootloader(task.commPort, out protocolVersion))
                {
                    return false;
                }

                if(protocolVersion == FirmwareUpdateProtocolVersion.V3)
                {
                    string mcuModel = string.Empty;
                    if (!MCU_BootloaderReadMCUModel(out mcuModel))
                    {
                        LogHelper.WriteInfo("Failed to load MCU model !");
                        this.OutputInfoLine("Failed to load MCU model !");
                        return false;
                    }

                    if(!string.IsNullOrEmpty(mcuModel) && mcuModel != task.firmware.mcuModel)
                    {
                        LogHelper.WriteInfo("MCU model does not match the target MCU model !");
                        this.OutputInfoLine("MCU model does not match the target MCU model !");
                        return false;
                    }
                }

                startTimeStamp = DateTime.Now;

                if (task.firmware != null && task.firmware.dspBin != null && task.firmware.dspBin.isDownloadBin)
                {
                    LogHelper.WriteInfo("Updating DSP firmware...");
                    this.OutputInfoLine("Updating DSP firmware...");
                    if (!DspFirmwareUpdate(task.firmware, protocolVersion))
                    {
                        return false;
                    }
                }

                if (task.firmware != null && task.firmware.mcuBin != null && task.firmware.mcuBin.isDownloadBin)
                {
                    LogHelper.WriteInfo("Updating MCU firmware...");
                    this.OutputInfoLine("Updating MCU firmware...");
                    if (!McuFirmwareUpdate(task.firmware.mcuBin))
                    {
                        return false;
                    }
                }

                DataBlock mcuDataBlcok = task.firmware.GetMergeDataBlock();
                if(mcuDataBlcok != null)
                {
                    if(task.firmware.isUpdateMCUAllData && mcuDataBlcok.address <= task.firmware.mcuAppActivateAddress &&
                        mcuDataBlcok.endAddress >= task.firmware.mcuAppActivateAddress)
                    {
                        byte[] activateCode = GetOriginalActivateCode();

                        if (activateCode == null || !DeviceActivateCode.CheckActivateCode(activateCode))
                        {
                            this.OutputInfoLine("Loading DSP id...");
                            byte[] deviceId = ReadDSPIDTest(task);
                            if (deviceId == null)
                            {
                                this.OutputInfoLine("Failed to load DSP id...");
                                return false;
                            }                            

                            activateCode = DeviceActivateCode.GenerateActivateCode(deviceId);

                            LogHelper.WriteInfo("生成激活码 = {0}", AEC.Core.Utils.ByteArrayToHexStr(activateCode));
                        }

                        DataBlock activateCodeDataBlock = new DataBlock(task.firmware.mcuAppActivateAddress, activateCode);
                        mcuDataBlcok.Update(activateCodeDataBlock);
                    }

                    if (!SaveFlashData(mcuDataBlcok))
                    {
                        return false;
                    }
                }

                bool mcuFirmwareInvalid = false;
                this.OutputInfoLine("Getting MCU firmware valid flag...");
                if (!MCU_BootloaderGetFirmwareFlagTest((uint)FirmwareType.MCU_APP, ref mcuFirmwareInvalid))
                {
                    LogHelper.WriteInfo("获取 MCU 固件无效标志 失败");
                    this.OutputInfoLine("Failed to get MCU firmware valid flag.");
                    return false;
                }

                if (mcuFirmwareInvalid)
                {
                    LogHelper.WriteInfo("MCU 固件 Error!");
                    this.OutputInfoLine("MCU firmware is invalid");
                    return false;
                }

                bool dspFirmwareInvalid = false;
                this.OutputInfoLine("Getting DSP firmware valid flag...");
                if (!MCU_BootloaderGetFirmwareFlagTest((uint)FirmwareType.DSP, ref dspFirmwareInvalid))
                {
                    LogHelper.WriteInfo("获取 DSP 固件无效标志 失败");
                    this.OutputInfoLine("Failed to get DSP firmware valid flag.");
                    return false;
                }

                if (dspFirmwareInvalid)
                {
                    LogHelper.WriteInfo("DSP 固件 Error!");
                    this.OutputInfoLine("DSP firmware is invalid");
                    return false;
                }

                if (!mcuFirmwareInvalid && !dspFirmwareInvalid)
                {
                    LogHelper.WriteInfo("MCU  和 DSP 固件OK！");
                    this.OutputInfoLine("Jumpping from Bootloader to App...");
                    if (!MCU_BootloaderJumpToAPP(protocolVersion))
                    {
                        LogHelper.WriteInfo("Bootloader跳转到App！");
                        this.OutputInfoLine("Failed to jump from Bootloader to App...");
                        return false;
                    }

                    //this.OutputInfoLine("MCU Jumpped from Bootloader to App successful");
                }

                LogHelper.WriteInfo("DspAndMcuProgramming()...完成");

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo("DspAndMcuProgramming()...异常：{0}", ex.Message);

                return false;
            }
            finally
            {
                CloseCommDevice();
            }
        }

        private const uint MCU_APP_DATA_INIT_FLAG = 0x4A4B4C4D;
        
        public byte[] GetOriginalActivateCode()
        {
            uint customDeviceDataBlockAddress = 0x0801F800;
            byte[] data;

            if(!MCU_BootloaderReadFlashTest(customDeviceDataBlockAddress, FlashOperateSize.Size2k, out data))
            {
                return null;
            }

            uint dataInitFlag = BitConverter.ToUInt32(data, 0);
            if(dataInitFlag == MCU_APP_DATA_INIT_FLAG)     //VKx数据
            {
                byte[] activateCode = Utils.CreateByteArray(DeviceActivateCode.ActivateCodeLength);
                Array.Copy(data, 8, activateCode, 0, activateCode.Length);
                return activateCode;
            }

            dataInitFlag = BitConverter.ToUInt32(data, 0x400);
            if (dataInitFlag == MCU_APP_DATA_INIT_FLAG)     //VKxS数据
            {
                byte[] activateCode = Utils.CreateByteArray(DeviceActivateCode.ActivateCodeLength);
                Array.Copy(data, 0x408, activateCode, 0, activateCode.Length);
                return activateCode;
            }

            return null;
        }

        

        //public bool DspAndMcuProgramming(DSPAndMCUProgrammingTask task)
        //{
        //    try
        //    {
        //        FirmwareUpdateProtocolVersion protocolVersion;

        //        if (!ConnectToBootloader(task.commPort, out protocolVersion))
        //        {
        //            return false;
        //        }

        //        //task.mcuProgrammingTask.restorePreset = false;
        //        //if(task.mcuProgrammingTask.restorePreset)
        //        //{
        //        //    System.Threading.Thread.Sleep(0x40);
        //        //    LogHelper.WriteInfo("读取VK设备信息参数");
        //        //    uint device_info_address = 0x0801F000;
        //        //    byte[] device_info_data = null;
        //        //    if (!MCU_BootloaderReadFlashTest(device_info_address, FlashOperateSize.Size4k, out device_info_data))
        //        //    {
        //        //        return false;
        //        //    }

        //        //    uint vksDeviceFlag = BitConverter.ToUInt32(device_info_data, 0xc00);

        //        //    byte[] deviceInfo = new byte[0x400];

        //        //    if (vksDeviceFlag != 0x4a4b4c4d)
        //        //    {
        //        //        Log.OutputLine("device data flag = {0}", Utils.Uint32ToHexString(vksDeviceFlag));

        //        //        byte[] customeDeviceModel = new byte[84];
        //        //        Array.Copy(device_info_data, customeDeviceModel, customeDeviceModel.Length);

        //        //        Array.Copy(device_info_data, 0x800, deviceInfo, 0, deviceInfo.Length);
        //        //        uint vksModelId = 0x00;
        //        //        uint x5ModelId = 0x20;
        //        //        if (deviceInfo[24] == 0x30 || deviceInfo[24] == 0x50)
        //        //        {
        //        //            customeDeviceModel[21] = 0x56;
        //        //            customeDeviceModel[22] = 0x4B;
        //        //            customeDeviceModel[23] = 0x35;
        //        //            customeDeviceModel[24] = 0x53;
        //        //            customeDeviceModel[25] = 0x00;
        //        //            vksModelId = 0x00;
        //        //        }
        //        //        else if (deviceInfo[24] == 0x40 || deviceInfo[24] == 0x60)
        //        //        {
        //        //            customeDeviceModel[21] = 0x56;
        //        //            customeDeviceModel[22] = 0x4B;
        //        //            customeDeviceModel[23] = 0x33;
        //        //            customeDeviceModel[24] = 0x53;
        //        //            customeDeviceModel[25] = 0x00;
        //        //            vksModelId = 0x10;
        //        //        }
        //        //        else if (deviceInfo[24] != 0x00 || deviceInfo[24] == 0x10)
        //        //        {
        //        //            LogHelper.WriteInfo("读取设备型号数据错误");
        //        //            return false;
        //        //        }

        //        //        int index = 24;
        //        //        index = FirmwareUpdateProtocol.FillUint32ToBuffer(deviceInfo, index, vksModelId);
        //        //        index = FirmwareUpdateProtocol.FillUint32ToBuffer(deviceInfo, index, x5ModelId);
        //        //        index = FirmwareUpdateProtocol.FillUint32ToBuffer(deviceInfo, index, 1);

        //        //        Array.Copy(customeDeviceModel, 0, deviceInfo, index, customeDeviceModel.Length);

        //        //        index += customeDeviceModel.Length;

        //        //        for (int i = index; i < deviceInfo.Length; ++i)
        //        //        {
        //        //            deviceInfo[i] = 0xff;
        //        //        }
        //        //    }
        //        //    else
        //        //    {
        //        //        Array.Copy(device_info_data, (int)0xc00, deviceInfo, 0, deviceInfo.Length);

        //        //        uint vksModelId = deviceInfo[24];
        //        //        uint x5ModelId = 0x20;

        //        //        byte[] vksCustomModel = new byte[]
        //        //        {
        //        //            0x4D ,0x65 ,0x6E ,0x67 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,
        //        //            0x56 ,0x4B ,0x35 ,0x53 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,
        //        //            0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,
        //        //            0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,
        //        //            0x00 ,0x00 ,0x00 ,0x00
        //        //        };

        //        //        if (vksModelId == 0x00)
        //        //        {
        //        //            vksCustomModel[23] = 0x35;
        //        //            LogHelper.WriteInfo("检测到当前设备是VK5S");
        //        //        }
        //        //        else if (vksModelId == 0x10)
        //        //        {
        //        //            vksCustomModel[23] = 0x33;
        //        //            LogHelper.WriteInfo("检测到当前设备是VK3S");
        //        //        }
        //        //        else
        //        //        {
        //        //            return false;
        //        //        }

        //        //        int index = 24;
        //        //        index = FirmwareUpdateProtocol.FillUint32ToBuffer(deviceInfo, index, vksModelId);
        //        //        index = FirmwareUpdateProtocol.FillUint32ToBuffer(deviceInfo, index, x5ModelId);
        //        //        index = FirmwareUpdateProtocol.FillUint32ToBuffer(deviceInfo, index, 1);

        //        //        Array.Copy(vksCustomModel, 0, deviceInfo, index, vksCustomModel.Length);

        //        //        index += vksCustomModel.Length;

        //        //        for (int i = index; i < deviceInfo.Length; ++i)
        //        //        {
        //        //            deviceInfo[i] = 0xff;
        //        //        }
        //        //    }

        //        //    LogHelper.WriteInfo("VK设备信息参数初始化完毕");

        //        //    uint deviceInfoAddress = 0x0801FC00;
        //        //    if (!MCU_Download(deviceInfoAddress, deviceInfo))
        //        //    {
        //        //        LogHelper.WriteInfo("写入设备信息数据失败！");
        //        //        return false;
        //        //    }

        //        //    uint systemCommonAddress = 0x0801F800;
        //        //    if (!MCU_Download(systemCommonAddress, system_common_data))
        //        //    {
        //        //        LogHelper.WriteInfo("写入公共参数失败！");
        //        //        return false;
        //        //    }

        //        //    LogHelper.WriteInfo("写入VKS 预设模式");
        //        //    uint vksPresetStartAddress = 0x0801B000;

        //        //    byte[] vks_2_presets = new byte[Size4K];

        //        //    for (int i = 0; i < vksPresets.Count; i++)
        //        //    {
        //        //        uint presetAddress = vksPresetStartAddress + (uint)i * Size2K;

        //        //        if(i + 1 < vksPresets.Count)
        //        //        {
        //        //            Array.Copy(vksPresets[i++], 0, vks_2_presets, Size2K * 0, Size2K);
        //        //            Array.Copy(vksPresets[i], 0, vks_2_presets, Size2K * 1, Size2K);

        //        //            if (!MCU_Download(presetAddress, vks_2_presets))
        //        //            {
        //        //                LogHelper.WriteInfo("写入VKS预设数据失败！");
        //        //                return false;
        //        //            }
        //        //        }
        //        //        else
        //        //        {
        //        //            if (!MCU_Download(presetAddress, vksPresets[i]))
        //        //            {
        //        //                LogHelper.WriteInfo("写入VKS预设数据失败！");
        //        //                return false;
        //        //            }
        //        //        }
        //        //    }

        //        //        //foreach (var presetIndex in vksPresetsMap.Keys)
        //        //        //{
        //        //        //    if (vksPresetsMap[presetIndex].Length != 0x800)
        //        //        //    {
        //        //        //        LogHelper.WriteInfo("VKS预设数据错误");
        //        //        //        return false;
        //        //        //    }

        //        //        //    uint presetAddress = vksPresetStartAddress + (uint)presetIndex * 0x800;
        //        //        //    if (!MCU_Download(presetAddress, vksPresetsMap[presetIndex]))
        //        //        //    {
        //        //        //        LogHelper.WriteInfo("写入VKS预设数据失败！");
        //        //        //        return false;
        //        //        //    }
        //        //        //}

        //        //    LogHelper.WriteInfo("写入X5 预设模式");
        //        //    uint x5PresetStartAddress = 0x08017000;
        //        //    if (x5_preset_data.Length != 0x400)
        //        //    {
        //        //        LogHelper.WriteInfo("X5预设数据错误");
        //        //        return false;
        //        //    }

        //        //    byte[] x5_4_presets = new byte[Size4K];
        //        //    Array.Copy(x5_preset_data, 0, x5_4_presets, Size1K * 0, x5_preset_data.Length);
        //        //    Array.Copy(x5_preset_data, 0, x5_4_presets, Size1K * 1, x5_preset_data.Length);
        //        //    Array.Copy(x5_preset_data, 0, x5_4_presets, Size1K * 2, x5_preset_data.Length);
        //        //    Array.Copy(x5_preset_data, 0, x5_4_presets, Size1K * 3, x5_preset_data.Length);
        //        //    for (int preset4Index = 0; preset4Index < 0x10 / 4; ++preset4Index)
        //        //    {
        //        //        uint presetAddress = x5PresetStartAddress + (uint)preset4Index * Size4K;
        //        //        if (!MCU_Download(presetAddress, x5_4_presets))
        //        //        {
        //        //            LogHelper.WriteInfo("写入X5预设数据失败！");
        //        //            return false;
        //        //        }
        //        //    }
        //        //}

        //        //this.ExportVKxFlashData();
        //        //this.ExportVKxSFlashData();

        //        //uint vkx_deviceInfoAddress = 0x0801F800;
        //        //byte[] data = new byte[(int)FlashOperateSize.Size2k];
        //        //for (int i = 0; i < data.Length; ++i)
        //        //{
        //        //    data[i] = 0xff;
        //        //}

        //        //if (!MCU_Download(vkx_deviceInfoAddress, data))
        //        //{
        //        //    LogHelper.WriteInfo("写入设备信息数据失败！");
        //        //    return false;
        //        //}

        //        //if (!DSP_LinkToBootROM())
        //        //{
        //        //    return false;
        //        //}

        //        //if (!DSP_Reboot(0))
        //        //{
        //        //    return false;
        //        //}

        //        //return true;

        //        //task.dspProgrammingTask.isDownload = false;
        //        if (task.dspProgrammingTask != null && task.dspProgrammingTask.isDownload)
        //        {
        //            LogHelper.WriteInfo("DSP 固件升级");
        //            if (!DspProgramming(task.dspProgrammingTask, protocolVersion))
        //            {
        //                return false;
        //            }
        //        }

        //        //System.Threading.Thread.Sleep(0x800);

        //        //if (!ReadDSPIDTest())
        //        //{
        //        //    return false;
        //        //}

        //        //return true;

        //        //task.mcuProgrammingTask.isDownload = false;
        //        if (task.mcuProgrammingTask != null && task.mcuProgrammingTask.isDownload)
        //        {
        //            LogHelper.WriteInfo("MCU 固件升级");
        //            if (!MCUProgramming(task.mcuProgrammingTask))
        //            {
        //                return false;
        //            }
        //        }

        //            //LogHelper.WriteInfo("读取App系统公共参数");
        //            //uint system_common_address = 0x0801F800;
        //            //byte[] system_common_data = null;
        //            //if (!MCU_BootloaderReadFlashTest(system_common_address, FlashOperateSize.Size1k, out system_common_data))
        //            //{
        //            //    return false;
        //            //}

        //            //LogHelper.WriteInfo("读取VKS模式参数");
        //            //uint vks_start_address = 0x0801B000;
        //            //Dictionary<uint, byte[]> vskData = new Dictionary<uint, byte[]>();
        //            //for (uint i = 0; i < 9; ++i)
        //            //{
        //            //    uint vks_mode_address = vks_start_address + 0x800 * i;
        //            //    LogHelper.WriteInfo("读取VKS模式 {0} 参数", i + 1);
        //            //    byte[] data = null;
        //            //    if (!MCU_BootloaderReadFlashTest(vks_mode_address, FlashOperateSize.Size2k, out data))
        //            //    {
        //            //        return false;
        //            //    }

        //            //    vskData[i] = data;
        //            //}

        //            //LogHelper.WriteInfo("读取X5模式参数");
        //            //uint x5_start_address = 0x08017000;
        //            //byte[] x5_preset;
        //            //for (uint i = 0; i < 1; ++i)
        //            //{
        //            //    uint x5_mode_address = x5_start_address + 0x400 * i;
        //            //    LogHelper.WriteInfo("读取X5模式 {0} 参数", i + 1);
        //            //    if (!MCU_BootloaderReadFlashTest(x5_mode_address, FlashOperateSize.Size1k, out x5_preset))
        //            //    {
        //            //        return false;
        //            //    }
        //            //}


        //            //uint address2 = 0x0801fc00;
        //            //if (!MCU_BootloaderReadFlashTest(address2, FlashOperateSize.Size1k))
        //            //{
        //            //    return false;
        //            //}

        //            //uint address3 = 0x08020000 - 0xC00;
        //            //if (!MCU_BootloaderReadFlashTest(address3))
        //            //{
        //            //    return false;
        //            //}

        //            //uint address4 = 0x08020000 - 0x1000;
        //            //if (!MCU_BootloaderReadFlashTest(address4))
        //            //{
        //            //    return false;
        //            //}

                

        //        bool mcuFirmwareInvalid = false;
        //        if (!MCU_BootloaderGetFirmwareFlagTest((uint)FirmwareType.MCU_APP, ref mcuFirmwareInvalid))
        //        {
        //            LogHelper.WriteInfo("获取 MCU 固件无效标志 失败");
        //            return false;
        //        }

        //        if (mcuFirmwareInvalid)
        //        {
        //            LogHelper.WriteInfo("MCU 固件 Error!");
        //        }

        //        bool dspFirmwareInvalid = false;
        //        if (!MCU_BootloaderGetFirmwareFlagTest((uint)FirmwareType.DSP, ref dspFirmwareInvalid))
        //        {
        //            LogHelper.WriteInfo("获取 DSP 固件无效标志 失败");
        //            return false;
        //        }

        //        if (dspFirmwareInvalid)
        //        {
        //            LogHelper.WriteInfo("DSP 固件 Error!");
        //        }

        //        //dspFirmwareInvalid = true;

        //        if (!mcuFirmwareInvalid && !dspFirmwareInvalid)
        //        {
        //            LogHelper.WriteInfo("MCU  和 DSP 固件OK！");
        //            if (!MCU_BootloaderJumpToAPP(protocolVersion))
        //            {
        //                LogHelper.WriteInfo("Bootloader跳转到App！");
        //                return false;
        //            }
        //        }




        //            //if (!MCU_BootloaderUpdateLCDShowTest())
        //            //{
        //            //    return false;
        //            //}

        //            //String[] ports = SerialPort.GetValidPorts();
        //            //if (!this.InitPort(task.commPort))
        //            //{
        //            //    return false;
        //            //}

        //            //if (!task.isOnlyDownloadParams && !DSP_DownloadBinToMCUFlash(task))
        //            //{
        //            //    return false;
        //            //}

        //            //if (!DSP_WriteProgrammingParams(task))
        //            //{
        //            //    return false;
        //            //}

        //            //if (!DSP_PreDownload())
        //            //{
        //            //    LogHelper.WriteInfo("预下载失败！");
        //            //    return false;
        //            //}

        //            //if (!DSP_Download(task))
        //            //{
        //            //    LogHelper.WriteInfo("下载失败！");
        //            //    return false;
        //            //}

        //            //if (!DSP_EndDownload(task))
        //            //{
        //            //    LogHelper.WriteInfo("结束下载失败！");
        //            //    return false;
        //            //}

        //            LogHelper.WriteInfo("DspAndMcuProgramming()...完成");

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteInfo("DspAndMcuProgramming()...异常：{0}", ex.Message);

        //        return false;
        //    }
        //    finally
        //    {
        //        CloseCommDevice();
        //    }
        //}

        public bool MCUFlashDataLoad(MCUFlashDataLoadTask task)
        {
            try
            {
                if(task.startAddress % Size1K != 0 ||
                    task.dataSize % Size1K != 0 ||
                    task.dataSize == 0)
                {
                    LogHelper.WriteInfo("地址数据或长度数据无效");
                    return false;
                }

                FirmwareUpdateProtocolVersion protocolVersion;

                if (!ConnectToBootloader(task.commPort, out protocolVersion))
                {
                    return false;
                }

                //this.ExportVKxFlashData();
                //this.ExportVKxSFlashData();
                task.dataBlcok = this.LoadFlashData(task);
                if (task.dataBlcok == null)
                {
                    LogHelper.WriteInfo("LoadFlashData()...失败");
                    return false;
                }

                if (task.DataLoaded != null)
                {
                    task.DataLoaded(task, EventArgs.Empty);
                }
                
                LogHelper.WriteInfo("MCUFalshDataExport()...完成");

                if (!MCU_BootloaderJumpToAPP(protocolVersion))
                {
                    LogHelper.WriteInfo("Bootloader跳转到App！");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo("MCUFalshDataExport()...异常：{0}", ex.Message);

                return false;
            }
            finally
            {
                CloseCommDevice();
                if(task.DataLoaded != null)
                {
                    task.DataLoaded = null;
                }
            }
        }

        public bool MCUFlashDataSave(MCUFlashDataSaveTask task)
        {
            try
            {
                FirmwareUpdateProtocolVersion protocolVersion;

                if (!ConnectToBootloader(task.commPort, out protocolVersion))
                {
                    return false;
                }

                if (!SaveFlashData(task.dataBlcok))
                {
                    return false;
                }

                if (!MCU_BootloaderJumpToAPP(protocolVersion))
                {
                    LogHelper.WriteInfo("Bootloader跳转到App！");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo("MCUFalshDataExport()...异常：{0}", ex.Message);

                return false;
            }
            finally
            {
                CloseCommDevice();                
            }
        }

        public bool DSPBluetoothRename(BluetoothRename bluetoothRename, FirmwareUpdateProtocolVersion protocolVersion)
        {
            if (protocolVersion == FirmwareUpdateProtocolVersion.V1)
            {
                LogHelper.WriteInfo("Unsupported DSP firmware version {0} !", protocolVersion);
                this.OutputInfoLine("Unsupported DSP firmware version {0} !", protocolVersion);
                return false;
            }

            FlashSR flashSR = null;
            string errorMsg = string.Empty;

            this.OutputInfoLine("Linking to DSP BootRom...");
            if (!DSP_LinkTobootRom_V2(out flashSR, out errorMsg))
            {
                LogHelper.WriteInfo("Failed to link to DSP BootRom! ...{0}", errorMsg);
                this.OutputInfoLine("Failed to link to DSP BootRom! ...{0}", errorMsg);
                return false;
            }

            if (flashSR == null)
            {
                LogHelper.WriteInfo("DSP FlashSR is null！");
                this.OutputInfoLine("DSP FlashSR is null！");
                return false;
            }

            uint flashSzie = flashSR.szMem / 8;
            uint envAddr = 0;
            this.OutputInfoLine("Reading DSP flash env data...");
            byte[] envData = DSP_ReadEnvData(flashSR, out envAddr);
            if (envData == null)
            {
                LogHelper.WriteInfo("Failed to read DSP Env data!");
                this.OutputInfoLine("Failed to read DSP Env data!");
                return false;
            }

            this.OutputInfoLine("Reading env data Bluetooth info...");
            BTDeviceInfo btDeviceInfo = GetBTDeviceInfoFromEnvData(envData);
            if (btDeviceInfo == null)
            {
                LogHelper.WriteInfo("Failed to read DSP Bluetooth device data");
                this.OutputInfoLine("Failed to read DSP Bluetooth device data!");
                return false;
            }
            else if (string.IsNullOrEmpty(btDeviceInfo.name))
            {
                LogHelper.WriteInfo("Current Bluetooth name is empty！");
                this.OutputInfoLine("Current Bluetooth name is empty！");
                return false;
            }
            else
            {
                LogHelper.WriteInfo("Current Bluetooth name : {0}", btDeviceInfo.name);
                this.OutputInfoLine("Current Bluetooth name : {0}", btDeviceInfo.name);
            }

            if (string.IsNullOrEmpty(bluetoothRename.btName))
            {
                //LogHelper.WriteInfo("Bluetooth name entered is empty!");
                //this.OutputInfoLine("Bluetooth name entered is empty!");
                if (!DSP_DownloadCompleted_V2(out errorMsg))
                {
                    LogHelper.WriteInfo("DSP reboot failed!");
                    this.OutputInfoLine("DSP reboot failed!");
                    return false;
                }

                return true;
            }

            if(bluetoothRename.btName.Contains("%SN%"))
            {
                bluetoothRename.btName = bluetoothRename.btName.Replace("%SN%", btDeviceInfo.AddressLower16BitsDesp);
            }

            btDeviceInfo.name = bluetoothRename.btName;
            this.OutputInfoLine("Updating env data Bluetooth info...");
            if (!CrcImageHelper.TLV_Modify(envData, 0, CrcImageHelper.TLV_TYPE_CFG_BT_NAME, btDeviceInfo.nameBuffer))
            {
                LogHelper.WriteInfo("Failed to update Bluetooth name data!");
                this.OutputErrorInfoLine("Failed to update Bluetooth name data!");
                return false;
            }

            this.OutputInfoLine("Updating DSP firmware valid flag...");
            if (!MCU_BootloaderUpdateFirmwareFlagTest((uint)FirmwareType.DSP, true))
            {
                LogHelper.WriteInfo("Failed to update DSP firmware valid flag!");
                this.OutputInfoLine("Failed to update DSP firmware valid flag！");
                return false;
            }

            this.OutputInfoLine("Unprotecting DSP flash...");
            if (!DSP_BootProtectFlashV2(DspIndex, false, out errorMsg))
            {
                LogHelper.WriteInfo("Failed to unprotect DSP flash! ...{0}!", errorMsg);
                this.OutputInfoLine("Failed to unprotect DSP flash! ...{0}!", errorMsg);
                return false;
            }

            this.OutputInfoLine("Erasing DSP flash...address : {0}, size = {1}", Utils.Uint32ToHexString(envAddr), AEC.Core.Utils.Uint32ToShortHexStr((uint)Size4K));
            if (!DSP_EraseFlashV2(DspIndex, envAddr, (uint)Size4K, out errorMsg))
            {
                LogHelper.WriteInfo("Failed to erase DSP flash! ...{0}!", errorMsg);
                this.OutputInfoLine("Failed to erase DSP flash! ...{0}!", errorMsg);
                return false;
            }

            this.OutputInfoLine("Downloading DSP flash env data...address : {0}, size = {1}", Utils.Uint32ToHexString(envAddr), AEC.Core.Utils.Uint32ToShortHexStr((uint)Size4K));
            if (!DSP_WriteFlash4K_V2(DspIndex, envData, 0, envAddr))
            {
                LogHelper.WriteInfo("Failed to download DSP env data...address : {0}, size : 4k!", Utils.Uint32ToHexString((uint)envAddr));
                this.OutputInfoLine("Failed to download DSP env data...address : {0}, size : 4k!", Utils.Uint32ToHexString((uint)envAddr));
                return false;
            }

            uint envCrc = 0xffffffff;
            envCrc = CrcImageHelper.CheckCRC(envCrc, envData, 0, Size4K);

            LogHelper.WriteInfo("Check env CRC...地址:{0}, 长度:{1}...", Utils.Uint32ToHexString((uint)envAddr), AEC.Core.Utils.Uint32ToShortHexStr((uint)Size4K));
            uint targetEnvCrc = 0xffffffff;
            this.OutputInfoLine("Get DSP flash env data CRC...address : {0}, size = {1}", Utils.Uint32ToHexString(envAddr), AEC.Core.Utils.Uint32ToShortHexStr((uint)Size4K));
            if (!GetDSPCheckFlashCRC_V2((int)envAddr, Size4K, ref targetEnvCrc))
            {
                LogHelper.WriteInfo("Failed to get DSP flash env CRC...address : {0}, size : 4k!", Utils.Uint32ToHexString((uint)envAddr));
                this.OutputInfoLine("Failed to get DSP flash env CRC...address : {0}, size : 4k!", Utils.Uint32ToHexString((uint)envAddr));
                return false;
            }

            if (envCrc != targetEnvCrc)
            {
                errorMsg = string.Format("DSP flash env file CRC ({0}) does not match target ({1})", Utils.Uint32ToHexString(envCrc), Utils.Uint32ToHexString(targetEnvCrc));
                LogHelper.WriteInfo("DSP env CheckCRC Error...env CRC = {0}, target env CRC = {1}!", Utils.Uint32ToHexString(envCrc), Utils.Uint32ToHexString(targetEnvCrc));
                this.OutputInfoLine("DSP env CheckCRC Error...env CRC = {0}, target env CRC = {1}!", Utils.Uint32ToHexString(envCrc), Utils.Uint32ToHexString(targetEnvCrc));
                return false;
            }

            this.OutputInfoLine("Completed DSP env data download...");
            if (!DSP_DownloadCompleted_V2(out errorMsg))
            {
                LogHelper.WriteInfo("Failed to complete DSP env data update! ...{0}!", errorMsg);
                this.OutputInfoLine("Failed to complete DSP env data update! ...{0}!", errorMsg);
                return false;
            }

            this.OutputInfoLine("Updating DSP firmware valid flag...");
            if (!MCU_BootloaderUpdateFirmwareFlagTest((uint)FirmwareType.DSP, false))
            {
                LogHelper.WriteInfo("Failed to update DSP firmware valid flag!");
                this.OutputInfoLine("Failed to update DSP firmware valid flag!");
                return false;
            }

            LogHelper.WriteInfo("The Bluetooth name has been renamed : {0}", btDeviceInfo.name);
            this.OutputInfoLine("The Bluetooth name has been renamed : {0}", btDeviceInfo.name);

            LogHelper.WriteInfo("OK");

            return true;
        }

        public bool DspFirmwareUpdate(FirmwarePackage fp, FirmwareUpdateProtocolVersion protocolVersion)
        {
            if (fp != null && fp.dspBin == null || fp.dspBin.binData == null)
            {
                return false;
            }

            FirmwareBin dsp = fp.dspBin;

            DSPProgrammingTask dspProgrammingTask = new DSPProgrammingTask();
            dspProgrammingTask.fileBuffer = Utils.CreateBinData(dsp.binData, Size4K);
            
            int envAddr = CrcImageHelper.GetBK3288EnvAddr(dspProgrammingTask.fileBuffer);
            if (envAddr == -1)
            {
                LogHelper.WriteInfo("Failed to get DSP env address !");
                this.OutputInfoLine("Failed to get DSP env address !");
                return false;
            }

            dspProgrammingTask.envAddr = (uint)envAddr;

            BinBaseInfo binBaseInfo = GetDspBinBaseInfo(dspProgrammingTask.fileBuffer);

            if (binBaseInfo == null)
            {
                LogHelper.WriteInfo("Failed to get DSP BaseInfo !");
                this.OutputInfoLine("Failed to get DSP BaseInfo !");
                return false;
            }

            dspProgrammingTask.binSize = (uint)(((int)binBaseInfo.szSrcBin) + ((int)FlashOperateSize.Size4k - 1)) / (uint)FlashOperateSize.Size4k * (uint)FlashOperateSize.Size4k;
            if (dspProgrammingTask.binSize == INVALID_DSP_BIN_SIZE || (dspProgrammingTask.binSize) % (int)(FlashOperateSize.Size4k) != 0 || dspProgrammingTask.binSize > dspProgrammingTask.fileBuffer.Length)
            {
                LogHelper.WriteInfo("DSP bin size is incorrect !");
                this.OutputInfoLine("DSP bin size is incorrect !");
                return false;
            }

            dspProgrammingTask.wavSize = (uint)(((int)binBaseInfo.szWav) + ((int)FlashOperateSize.Size4k - 1)) / (uint)FlashOperateSize.Size4k * (uint)FlashOperateSize.Size4k;

            if (dspProgrammingTask.binSize + dspProgrammingTask.wavSize > (uint)envAddr)
            {
                LogHelper.WriteInfo("The sum of the DSP bin executable file and wav file size exceeds the env address !");
                this.OutputInfoLine("The sum of the DSP bin executable file and wav file size exceeds the env address !");
                return false;
            }

            if (protocolVersion == FirmwareUpdateProtocolVersion.V1)
            {
                string errorMsg = string.Empty;
                this.OutputInfoLine("Predownloading dsp firmware...");
                if (!DSP_PreDownload_V1(dspProgrammingTask, out errorMsg))
                {
                    LogHelper.WriteInfo("Failed to predownload dsp firmware! ...{0}", errorMsg);
                    this.OutputInfoLine("Failed to predownload dsp firmware! ...{0}", errorMsg);
                    //this.OutputInfoLine("DSP PreDownload失败！");
                    return false;
                }

                if (!DSP_DownloadImage_V1(dspProgrammingTask))
                {
                    LogHelper.WriteInfo("下载失败！");
                    //this.OutputInfoLine("下载失败！");
                    return false;
                }

                //if (!DspWriteTPMData(dspProgrammingTask))
                //{
                //    LogHelper.WriteInfo("WriteTPM Data失败！");
                //    return false;
                //}

                this.OutputInfoLine("Checking DSP flash CRC...");
                if (!DSP_FlashCheckCRC_V1(dspProgrammingTask, out errorMsg))
                {
                    LogHelper.WriteInfo("DSP Check CRC失败！");
                    LogHelper.WriteInfo("Failed to check DSP Flash CRC! ...{0}", errorMsg);
                    return false;
                }

                this.OutputInfoLine("Completing DSP firmware update...");
                if (!DSP_EndDownload_V1(dspProgrammingTask, out errorMsg))
                {
                    LogHelper.WriteInfo("结束下载失败！");
                    this.OutputInfoLine("Failed to complete DSP firmware update! ...{0}", errorMsg);
                    return false;
                }

                //if (!DSP_PowerSupplyControl(DspIndex, false))
                //{
                //    return false;
                //}

                //System.Threading.Thread.Sleep(128);

                //if (!DSP_PowerSupplyControl(DspIndex, true))
                //{
                //    return false;
                //}

                //System.Threading.Thread.Sleep(1024*4);

                //if (!ReadDSPIDTest())
                //{
                //    return false;
                //}
            }
            else if (protocolVersion == FirmwareUpdateProtocolVersion.V2 || protocolVersion == FirmwareUpdateProtocolVersion.V3)
            {
                FlashSR flashSR = null;
                string errorMsg = string.Empty;

                this.OutputInfoLine("Linking to DSP BootRom...");
                if (!DSP_LinkTobootRom_V2(out flashSR, out errorMsg))
                {
                    LogHelper.WriteInfo("DSP LinkTobootRom失败！");
                    this.OutputInfoLine("Failed to link to DSP BootRom! ...{0}", errorMsg);
                    return false;
                }

                if (flashSR == null)
                {
                    LogHelper.WriteInfo("DSP FlashSR is null！");
                    this.OutputInfoLine("DSP FlashSR is null！");
                    return false;
                }

                int flashSzie = (int)flashSR.szMem / 8;
                if (dspProgrammingTask.fileBuffer.Length + Size4K != flashSzie)
                {
                    LogHelper.WriteInfo("DSP bin Size = {0} does not math DSP Flash Size = {1} !", Utils.Uint32ToHexString((uint)dspProgrammingTask.fileBuffer.Length), Utils.Uint32ToHexString((uint)flashSzie));
                    this.OutputInfoLine("DSP bin Size = {0} does not math DSP Flash Size = {1} !", Utils.Uint32ToHexString((uint)dspProgrammingTask.fileBuffer.Length), Utils.Uint32ToHexString((uint)flashSzie)); 
                    return false;
                }
                else
                {
                    LogHelper.WriteInfo("DSP Flash Size = {0}", Utils.Uint32ToHexString((uint)flashSzie));
                }

                BTDeviceInfo btDeviceInfo = null;
                if(fp.isForcedUpdateBTInfo) //如果强制更新蓝牙设备信息
                {
                    if(fp.isEnableRandomBTAddress)
                    {
                        btDeviceInfo = new BTDeviceInfo();
                    }
                    else
                    {
                        if(this.lastDeviceInfo == null)
                        {
                            this.lastDeviceInfo = new BTDeviceInfo();
                        }

                        btDeviceInfo = new BTDeviceInfo(this.lastDeviceInfo.addrBuffer, this.lastDeviceInfo.nameBuffer);
                    }
                }
                else if (fp.isRetainBTDeviceInfo)
                {
                    btDeviceInfo = DSP_ReadBTInfo(flashSR);

                    if(btDeviceInfo == null)
                    {
                        if (fp.isEnableRandomBTAddress)
                        {
                            btDeviceInfo = new BTDeviceInfo();
                        }
                        else
                        {
                            if (this.lastDeviceInfo == null)
                            {
                                this.lastDeviceInfo = new BTDeviceInfo();
                            }

                            btDeviceInfo = new BTDeviceInfo(this.lastDeviceInfo.addrBuffer, this.lastDeviceInfo.nameBuffer);
                        }
                    }
                }

                if(btDeviceInfo != null)
                {
                    if(string.IsNullOrEmpty(btDeviceInfo.name))
                    {
                        string btName = fp.BTName;
                        if(string.IsNullOrEmpty(btName))
                        {
                            btName = BTDeviceInfo.DefBTName;
                        }

                        btDeviceInfo.name = string.Format("{0}{1}", btName, btDeviceInfo.AddressLower16BitsDesp);
                    }
                    
                    LogHelper.WriteInfo("The target Bluetooth address : {0}，name : {1}", btDeviceInfo.addressDesp, btDeviceInfo.name);
                }

                //byte[] data = DSP_ReadFlash4K_V2(DspIndex, 0);
                //if (data == null)
                //{
                //    return false;
                //}

                ////LogHelper.WriteInfo("DSP Flash Data (addr:{0}) : {1}", Utils.Uint32ToHexString((uint)0), Utils.ByteArrayToHexStr(data));

                //uint dataCrc = 0xffffffff;
                //dataCrc = CrcImageHelper.CheckCRC(dataCrc, dspProgrammingTask.fileBuffer, 0, Size4K);
                //uint loadedDataCrc = 0xffffffff;
                //loadedDataCrc = CrcImageHelper.CheckCRC(loadedDataCrc, data, 0, Size4K);

                //if (dataCrc != loadedDataCrc)
                //{
                //    LogHelper.WriteInfo("数据校验错误");
                //    return false;
                //}

                //data = DSP_ReadFlash4K_V2(DspIndex, 0x1fb000);
                //if (data == null)
                //{
                //    return false;
                //}

                ////LogHelper.WriteInfo("DSP Flash Data (addr:{0}) : {1}", Utils.Uint32ToHexString((uint)0x1fb000), Utils.ByteArrayToHexStr(data));
                //dataCrc = 0xffffffff;
                //dataCrc = CrcImageHelper.CheckCRC(dataCrc, dspProgrammingTask.fileBuffer, 0x1fb000, Size4K);
                //loadedDataCrc = 0xffffffff;
                //loadedDataCrc = CrcImageHelper.CheckCRC(loadedDataCrc, data, 0, Size4K);

                //if (dataCrc != loadedDataCrc)
                //{
                //    LogHelper.WriteInfo("数据校验错误");
                //    return false;
                //}

                //return true;
                //if (!DSP_PowerSupplyControl(DspIndex, false))
                //{
                //    return false;
                //}

                //if (!DSP_CommSetUartBaudRate(DspIndex, DefaultDSPBaudRate))
                //{
                //    return false;
                //}

                //System.Threading.Thread.Sleep(0x100);

                //if (!DSP_PowerSupplyControl(DspIndex, true))
                //{
                //    return false;
                //}

                //System.Threading.Thread.Sleep(0xC00);

                //if (!ReadDSPIDTest())
                //{
                //    return false;
                //}

                //if (!DSP_WriteRegister_V2(DSP_TRNG_ENABLE_ADDRESS, 1))
                //{
                //    return false;
                //}

                //if (!DSP_CommSetUartBaudRate(DspIndex, DefaultDSPBaudRate))
                //{
                //    return false;
                //}

                //LogHelper.WriteInfo("DSP通讯波特率恢复默认");

                //uint data = 0;
                //if (!DSP_ReadRegister_V2(DspIndex, DSP_TRNG_DATA_ADDRESS, ref data))
                //{
                //    LogHelper.WriteInfo("读取随机数寄存器失败!");
                //    //return false;
                //}
                //else
                //{
                //    LogHelper.WriteInfo("随机数 = {0}", Utils.Uint32ToHexString((uint)data));
                //}

                ////return true;

                //if (!DSP_CommSetUartBaudRate(DspIndex, DownloadBaudRate))
                //{
                //    return false;
                //}

                //data = 0;
                //if (!DSP_ReadRegister_V2(DspIndex, DSP_TRNG_DATA_ADDRESS, ref data))
                //{
                //    LogHelper.WriteInfo("读取随机数寄存器失败!");
                //    //return false;
                //}
                //else
                //{
                //    LogHelper.WriteInfo("随机数 = {0}", Utils.Uint32ToHexString((uint)data));
                //}

                //return true;

                this.OutputInfoLine("Unprotecting DSP flash...");
                if (!DSP_BootProtectFlashV2(DspIndex, false, out errorMsg))
                {
                    this.OutputInfoLine("Failed to unprotect DSP flash! ...{0}", errorMsg);
                    return false;
                }

                this.OutputInfoLine("Erasing DSP flash...address : {0}, size = {1}", Utils.Uint32ToHexString((uint)0), Utils.Uint32ToHexString((uint)dspProgrammingTask.fileBuffer.Length));
                if (!DSP_EraseFlashV2(DspIndex, 0, (uint)dspProgrammingTask.fileBuffer.Length, out errorMsg))
                {
                    this.OutputInfoLine("Failed to erase DSP flash! ...{0}", errorMsg);
                    return false;
                }

                this.OutputInfoLine("Updating DSP firmware valid flag...");
                if (!MCU_BootloaderUpdateFirmwareFlagTest((uint)FirmwareType.DSP, true))
                {
                    this.OutputInfoLine("Failed to update DSP firmware valid flag.");
                    return false;
                }

                if (!DSP_DownloadImage_V2(dspProgrammingTask, btDeviceInfo))
                {
                    return false;
                }

                this.OutputInfoLine("Checking DSP flash CRC...");
                if (!DSP_FlashCheckCRC_V2(dspProgrammingTask, out errorMsg))
                {
                    LogHelper.WriteInfo("Failed to check DSP Flash CRC! ...{0}", errorMsg);
                    return false;
                }

                this.OutputInfoLine("Completing DSP firmware update...");
                if (!DSP_DownloadCompleted_V2(out errorMsg))
                {
                    this.OutputInfoLine("Failed to complete DSP firmware update! ...{0}", errorMsg);
                    return false;
                }

                this.OutputInfoLine("Updating DSP firmware valid flag...");
                if (!MCU_BootloaderUpdateFirmwareFlagTest((uint)FirmwareType.DSP, false))
                {
                    this.OutputInfoLine("Failed to update DSP firmware valid flag.");
                    return false;
                }

                if (btDeviceInfo != null)
                {
                    LogHelper.WriteInfo("The updated Bluetooth address : {0}，name : {1}", btDeviceInfo.addressDesp, btDeviceInfo.name);
                }

                //if (!DSP_Reboot_V2(DspIndex))
                //{
                //    return false;
                //}

                //System.Threading.Thread.Sleep(0x800);

                //if (!ReadDSPIDTest())
                //{
                //    return false;
                //}

                LogHelper.WriteInfo("Bootloader ProtocolVersion.V2");
            }
            else
            {
                LogHelper.WriteInfo("不支持此版本的 Bootloader 升级协议！");
                this.OutputInfoLine("Unsupported Bootloader update protocol! ");
                return false;
            }

            return true;
        }

        //public bool DspProgramming(DSPProgrammingTask dspProgrammingTask, FirmwareUpdateProtocolVersion protocolVersion)
        //{
        //    dspProgrammingTask.fileBuffer = GetDspBinFileBuffer(dspProgrammingTask.dspCrcImageFilePath);
        //    if (dspProgrammingTask.fileBuffer == null)
        //    {
        //        return false;
        //    }

        //    int envAddr = CrcImageHelper.GetBK3288EnvAddr(dspProgrammingTask.fileBuffer);
        //    if(envAddr == -1)
        //    {
        //        return false;
        //    }

        //    dspProgrammingTask.envAddr = (uint)envAddr;

        //    BinBaseInfo binBaseInfo = GetDspBinBaseInfo(dspProgrammingTask.fileBuffer);

        //    if(binBaseInfo == null)
        //    {
        //        LogHelper.WriteInfo("GetDspBinBaseInfo错误！");
        //        return false;
        //    }

        //    dspProgrammingTask.binSize = (uint)(((int)binBaseInfo.szSrcBin) + ((int)FlashOperateSize.Size4k - 1)) / (uint)FlashOperateSize.Size4k * (uint)FlashOperateSize.Size4k;
        //    if (dspProgrammingTask.binSize == INVALID_DSP_BIN_SIZE || (dspProgrammingTask.binSize) % (int)(FlashOperateSize.Size4k) != 0 || dspProgrammingTask.binSize > dspProgrammingTask.fileBuffer.Length)
        //    {
        //        LogHelper.WriteInfo("DSP binSize有误！");
        //        return false;
        //    }

        //    dspProgrammingTask.wavSize = (uint)(((int)binBaseInfo.szWav) + ((int)FlashOperateSize.Size4k - 1)) / (uint)FlashOperateSize.Size4k * (uint)FlashOperateSize.Size4k;

        //    if (dspProgrammingTask.binSize + dspProgrammingTask.wavSize > (uint)envAddr)
        //    {
        //        LogHelper.WriteInfo("DSP bin文件 可执行代码Size 和 音效文件 Size之和超过 Env 地址");
        //        return false;
        //    }

        //    //if (!DSP_CommSetUartBaudRate(DspIndex, 0xffffff))
        //    //{
        //    //    return false;
        //    //}

        //    //if (!DSP_PowerSupplyControl(DspIndex, false))
        //    //{
        //    //    return false;
        //    //}

        //    //System.Threading.Thread.Sleep(3000);

        //    //if (!DSP_PowerSupplyControl(DspIndex, true))
        //    //{
        //    //    return false;
        //    //}

        //    //if (!ReadDSPIDTest())
        //    //{
        //    //    LogHelper.WriteInfo("下载失败！");
        //    //    return false;
        //    //}

        //    //if (!DSP_LinkToBootROM())
        //    //{
        //    //    return false;
        //    //}

        //    //if (!DSP_WriteRegister(DSP_TRNG_ENABLE_ADDRESS, 1))
        //    //{
        //    //    return false;
        //    //}

        //    //uint data = 0;
        //    //if (!DSP_ReadRegister(DspIndex, DSP_TRNG_DATA_ADDRESS, ref data))
        //    //{
        //    //    return false;
        //    //}

        //    //if (!DSP_ReadRegister(DspIndex, DSP_TRNG_DATA_ADDRESS, ref data))
        //    //{
        //    //    return false;
        //    //}

        //    //if (!DSP_BootProtectFlash(DspIndex, false))
        //    //{
        //    //    return false;
        //    //}

        //    //if (!DSP_EraseFlash(DspIndex, 0, dspProgrammingTask.binSize))
        //    //{
        //    //    return false;
        //    //}

        //    //if (!DSP_DownloadImage(dspProgrammingTask))
        //    //{
        //    //    LogHelper.WriteInfo("下载失败！");
        //    //    return false;
        //    //}

        //    //if (!DSP_BootProtectFlash(DspIndex, true))
        //    //{
        //    //    return false;
        //    //}

        //    //if (!DSP_ReadFlash4KTest(dspProgrammingTask))
        //    //{
        //    //    LogHelper.WriteInfo("读取Flash 4K数据失败！");
        //    //    return false;
        //    //}

        //    //if (!DSP_FlashCheckCRC(dspProgrammingTask))
        //    //{
        //    //    LogHelper.WriteInfo("DSP Check CRC失败！");
        //    //    return false;
        //    //}

        //    //if (!DSP_Reboot(DspIndex))
        //    //{
        //    //    LogHelper.WriteInfo("DSP Reboot 失败！");
        //    //    return false;
        //    //}

        //    if (protocolVersion == FirmwareUpdateProtocolVersion.V1)
        //    {
        //        string errorMsg = string.Empty;
        //        if (!DSP_PreDownload_V1(dspProgrammingTask))
        //        {
        //            LogHelper.WriteInfo("DSP PreDownload失败！");
        //            return false;
        //        }

        //        if (!DSP_DownloadImage_V1(dspProgrammingTask))
        //        {
        //            LogHelper.WriteInfo("下载失败！");
        //            return false;
        //        }

        //        //if (!DspWriteTPMData(dspProgrammingTask))
        //        //{
        //        //    LogHelper.WriteInfo("WriteTPM Data失败！");
        //        //    return false;
        //        //}

        //        if (!DSP_FlashCheckCRC_V1(dspProgrammingTask))
        //        {
        //            LogHelper.WriteInfo("DSP Check CRC失败！");
        //            return false;
        //        }

        //        if (!DSP_EndDownload_V1(dspProgrammingTask))
        //        {
        //            LogHelper.WriteInfo("结束下载失败！");
        //            return false;
        //        }

        //        //if (!DSP_PowerSupplyControl(DspIndex, false))
        //        //{
        //        //    return false;
        //        //}

        //        //System.Threading.Thread.Sleep(128);

        //        //if (!DSP_PowerSupplyControl(DspIndex, true))
        //        //{
        //        //    return false;
        //        //}

        //        //System.Threading.Thread.Sleep(1024*4);

        //        //if (!ReadDSPIDTest())
        //        //{
        //        //    return false;
        //        //}
        //    }
        //    else if (protocolVersion == FirmwareUpdateProtocolVersion.V2)
        //    {
        //        FlashSR flashSR = null;
        //        string errorMsg = string.Empty;
        //        if (!DSP_LinkTobootRom_V2(dspProgrammingTask, out flashSR, out errorMsg))
        //        {
        //            LogHelper.WriteInfo("DSP LinkTobootRom失败！");
        //            return false;
        //        }

        //        if(flashSR == null)
        //        {
        //            LogHelper.WriteInfo("DSP FlashSR 为空！");
        //            return false;
        //        }

        //        int flashSzie = (int)flashSR.szMem / 8;
        //        if(dspProgrammingTask.fileBuffer.Length + Size4K != flashSzie)
        //        {
        //            LogHelper.WriteInfo("DSP bin Size = {0} 与 DSP Flash Size = {1} 不匹配！", Utils.Uint32ToHexString((uint)dspProgrammingTask.fileBuffer.Length), Utils.Uint32ToHexString((uint)flashSzie));
        //            return false;
        //        }
        //        else
        //        {
        //            LogHelper.WriteInfo("DSP Flash Size = {0}", Utils.Uint32ToHexString((uint)flashSzie));
        //        }

        //        //byte[] data = DSP_ReadFlash4K_V2(DspIndex, 0);
        //        //if (data == null)
        //        //{
        //        //    return false;
        //        //}

        //        ////LogHelper.WriteInfo("DSP Flash Data (addr:{0}) : {1}", Utils.Uint32ToHexString((uint)0), Utils.ByteArrayToHexStr(data));

        //        //uint dataCrc = 0xffffffff;
        //        //dataCrc = CrcImageHelper.CheckCRC(dataCrc, dspProgrammingTask.fileBuffer, 0, Size4K);
        //        //uint loadedDataCrc = 0xffffffff;
        //        //loadedDataCrc = CrcImageHelper.CheckCRC(loadedDataCrc, data, 0, Size4K);

        //        //if (dataCrc != loadedDataCrc)
        //        //{
        //        //    LogHelper.WriteInfo("数据校验错误");
        //        //    return false;
        //        //}

        //        //data = DSP_ReadFlash4K_V2(DspIndex, 0x1fb000);
        //        //if (data == null)
        //        //{
        //        //    return false;
        //        //}

        //        ////LogHelper.WriteInfo("DSP Flash Data (addr:{0}) : {1}", Utils.Uint32ToHexString((uint)0x1fb000), Utils.ByteArrayToHexStr(data));
        //        //dataCrc = 0xffffffff;
        //        //dataCrc = CrcImageHelper.CheckCRC(dataCrc, dspProgrammingTask.fileBuffer, 0x1fb000, Size4K);
        //        //loadedDataCrc = 0xffffffff;
        //        //loadedDataCrc = CrcImageHelper.CheckCRC(loadedDataCrc, data, 0, Size4K);

        //        //if (dataCrc != loadedDataCrc)
        //        //{
        //        //    LogHelper.WriteInfo("数据校验错误");
        //        //    return false;
        //        //}

        //        //return true;
        //        //if (!DSP_PowerSupplyControl(DspIndex, false))
        //        //{
        //        //    return false;
        //        //}

        //        //if (!DSP_CommSetUartBaudRate(DspIndex, DefaultDSPBaudRate))
        //        //{
        //        //    return false;
        //        //}

        //        //System.Threading.Thread.Sleep(0x100);

        //        //if (!DSP_PowerSupplyControl(DspIndex, true))
        //        //{
        //        //    return false;
        //        //}

        //        //System.Threading.Thread.Sleep(0xC00);

        //        //if (!ReadDSPIDTest())
        //        //{
        //        //    return false;
        //        //}

        //        //if (!DSP_WriteRegister_V2(DSP_TRNG_ENABLE_ADDRESS, 1))
        //        //{
        //        //    return false;
        //        //}

        //        //if (!DSP_CommSetUartBaudRate(DspIndex, DefaultDSPBaudRate))
        //        //{
        //        //    return false;
        //        //}

        //        //LogHelper.WriteInfo("DSP通讯波特率恢复默认");

        //        //uint data = 0;
        //        //if (!DSP_ReadRegister_V2(DspIndex, DSP_TRNG_DATA_ADDRESS, ref data))
        //        //{
        //        //    LogHelper.WriteInfo("读取随机数寄存器失败!");
        //        //    //return false;
        //        //}
        //        //else
        //        //{
        //        //    LogHelper.WriteInfo("随机数 = {0}", Utils.Uint32ToHexString((uint)data));
        //        //}

        //        ////return true;

        //        //if (!DSP_CommSetUartBaudRate(DspIndex, DownloadBaudRate))
        //        //{
        //        //    return false;
        //        //}

        //        //data = 0;
        //        //if (!DSP_ReadRegister_V2(DspIndex, DSP_TRNG_DATA_ADDRESS, ref data))
        //        //{
        //        //    LogHelper.WriteInfo("读取随机数寄存器失败!");
        //        //    //return false;
        //        //}
        //        //else
        //        //{
        //        //    LogHelper.WriteInfo("随机数 = {0}", Utils.Uint32ToHexString((uint)data));
        //        //}

        //        //return true;

        //        if (!DSP_BootProtectFlashV2(DspIndex, false))
        //        {
        //            return false;
        //        }

        //        if (!DSP_EraseFlashV2(DspIndex, 0, (uint)dspProgrammingTask.fileBuffer.Length))
        //        {
        //            return false;
        //        }

        //        if (!MCU_BootloaderUpdateFirmwareFlagTest((uint)FirmwareType.DSP, true))
        //        {
        //            return false;
        //        }

        //        if (!DSP_DownloadImage_V2(dspProgrammingTask))
        //        {
        //            return false;
        //        }

        //        if (!DSP_FlashCheckCRC_V2(dspProgrammingTask))
        //        {
        //            return false;
        //        }

        //        if (!DSP_DownloadCompleted_V2(dspProgrammingTask))
        //        {
        //            return false;
        //        }

        //        if (!MCU_BootloaderUpdateFirmwareFlagTest((uint)FirmwareType.DSP, false))
        //        {
        //            return false;
        //        }

        //        //if (!DSP_Reboot_V2(DspIndex))
        //        //{
        //        //    return false;
        //        //}

        //        //System.Threading.Thread.Sleep(0x800);

        //        //if (!ReadDSPIDTest())
        //        //{
        //        //    return false;
        //        //}

        //        LogHelper.WriteInfo("Bootloader ProtocolVersion.V2");
        //    }
        //    else
        //    {
        //        LogHelper.WriteInfo("不支持此版本的 Bootloader 升级协议");
        //        return false;
        //    }

        //    return true;
        //}

        public bool McuFirmwareUpdate(FirmwareBin mcu)
        {
            if (mcu == null || mcu.binData == null)
            {
                return false;
            }

            MCUProgrammingTask mcuProgrammingTask = new MCUProgrammingTask()
            {
                mcuAppVectorOffset = mcu.offsetAddress,
            };

            mcuProgrammingTask.fileBuffer = Utils.CreateBinData(mcu.binData, Size1K);
            if (mcuProgrammingTask.fileBuffer == null)
            {
                return false;
            }

            mcuProgrammingTask.binSize = (uint)mcuProgrammingTask.fileBuffer.Length;
            if (mcuProgrammingTask.binSize == INVALID_DSP_BIN_SIZE || (mcuProgrammingTask.binSize) % (int)(FlashOperateSize.Size1k) != 0)
            {
                LogHelper.WriteInfo("MCU binSize is valid! ");
                this.OutputInfoLine("MCU binSize is valid! ");
                return false;
            }

            //if (!MCU_PreDownload(mcuProgrammingTask))
            //{
            //    LogHelper.WriteInfo("MCU Pre Download失败！");
            //    return false;
            //}

            this.OutputInfoLine("Updating MCU firmware valid flag...");
            if (!MCU_BootloaderUpdateFirmwareFlagTest((uint)FirmwareType.MCU_APP, true))
            {
                this.OutputInfoLine("Failed to update MCU firmware valid flag.");
                return false;
            }

            if (!MCU_DownloadImage(mcuProgrammingTask))
            {
                LogHelper.WriteInfo("MCU Download失败！");
                return false;
            }

            this.OutputInfoLine("Checking MCU flash CRC...");
            string errorMsg = string.Empty;
            if (!MCU_FlashCheckCRC(mcuProgrammingTask, out errorMsg))
            {
                LogHelper.WriteInfo("Failed to check MCU Flash CRC! ...{0}", errorMsg);
                return false;
            }

            this.OutputInfoLine("Updating MCU firmware valid flag...");
            if (!MCU_BootloaderUpdateFirmwareFlagTest((uint)FirmwareType.MCU_APP, false))
            {
                this.OutputInfoLine("Failed to update MCU firmware valid flag.");
                return false;
            }

            //if (false && !MCU_BootloaderReadFlashTest(mcuProgrammingTask))
            //{
            //    LogHelper.WriteInfo("Bootloader Read Flash失败！");
            //    return false;
            //}

            return true;
        }

        //public bool MCUProgramming(MCUProgrammingTask mcuProgrammingTask)
        //{
        //    mcuProgrammingTask.fileBuffer = GetMcuAppBinFileBuffer(mcuProgrammingTask.mcuBinFilePath);
        //    if (mcuProgrammingTask.fileBuffer == null)
        //    {
        //        return false;
        //    }

        //    mcuProgrammingTask.binSize = (uint)mcuProgrammingTask.fileBuffer.Length;
        //    if (mcuProgrammingTask.binSize == INVALID_DSP_BIN_SIZE || (mcuProgrammingTask.binSize) % (int)(FlashOperateSize.Size1k) != 0)
        //    {
        //        LogHelper.WriteInfo("MCU binSize有误！");
        //        return false;
        //    }

        //    //if (!MCU_PreDownload(mcuProgrammingTask))
        //    //{
        //    //    LogHelper.WriteInfo("MCU Pre Download失败！");
        //    //    return false;
        //    //}

        //    if (!MCU_BootloaderUpdateFirmwareFlagTest((uint)FirmwareType.MCU_APP, true))
        //    {
        //        return false;
        //    }

        //    if (!MCU_DownloadImage(mcuProgrammingTask))
        //    {
        //        LogHelper.WriteInfo("MCU Download失败！");
        //        return false;
        //    }

        //    if (!MCU_FlashCheckCRC(mcuProgrammingTask))
        //    {
        //        LogHelper.WriteInfo("MCU Check CRC失败！");
        //        return false;
        //    }

        //    if (!MCU_BootloaderUpdateFirmwareFlagTest((uint)FirmwareType.MCU_APP, false))
        //    {
        //        return false;
        //    }

        //    //if (false && !MCU_BootloaderReadFlashTest(mcuProgrammingTask))
        //    //{
        //    //    LogHelper.WriteInfo("Bootloader Read Flash失败！");
        //    //    return false;
        //    //}

        //    return true;
        //}

        public bool StartWork(FirmwareUpdateWrokerArgs workerArgs)
        {
            if(this.bgwComm.IsBusy)
            {
                return false;
            }

            this.bgwComm.RunWorkerAsync(workerArgs);

            return true;
        }

        //private const string DefaultPort = "COM2";

        //private bool RunTask(DownloadTask task)
        //{
        //    try
        //    {
        //        if (!CheckCrcFilePath(task))
        //        {
        //            //Log.OutputLine("CRC文件检测失败！");        
        //            return false;
        //        }

        //        this.CloseCommDevice();

        //        //String[] ports = SerialPort.GetValidPorts();

        //        FUSerialPortDevice sp = new FUSerialPortDevice(FirmwareUpgradeProtocol.CheckResponseIsCompleted, _recvBuffer);
        //        this.fuDevice = sp;

        //        sp.portName = task.CommPortName;

        //        if (!sp.Open())
        //        {
        //            LogHelper.WriteInfo("打开端口失败！");                    
        //            return false;
        //        }

        //        //if (!UpdateBaudRate())
        //        //{
        //        //    return false;
        //        //}


        //        //if (!DSP_DownloadBinToMCUFlash(task))
        //        //{
        //        //    return false;
        //        //}

        //        if (!DSP_WriteProgrammingParams(task))
        //        {
        //            return false;
        //        }

        //        //if (!DSP_PreDownload())
        //        //{
        //        //    LogHelper.WriteInfo("预下载失败！");
        //        //    return false;
        //        //}

        //        //if (!DSP_Download(task))
        //        //{
        //        //    LogHelper.WriteInfo("下载失败！");
        //        //    return false;
        //        //}

        //        //if (!DSP_EndDownload(task))
        //        //{
        //        //    LogHelper.WriteInfo("结束下载失败！");
        //        //    return false;
        //        //}

        //        LogHelper.WriteInfo("发送数据完成");
        //    }
        //    catch(Exception ex)
        //    {
        //        LogHelper.WriteInfo("RunTask()...异常：{0}", ex.Message);

        //        return false;
        //    }
        //    finally
        //    {
        //        CloseCommDevice();
        //    }

        //    return true;
        //}

        private bool CheckCrcFilePath(DspProgrammerDownloadTask task)
        {
            if (task.dspFwPackage.dspFirmwareData == null)
            {
                LogHelper.WriteInfo("DSP固件数据为空！");

                return false;
            }

            if(task.dspFwPackage.dspFirmwareInfo == null)
            {
                LogHelper.WriteInfo("DSP固件信息为空！");
                return false;
            }

            this.OutputInfoLine("检测是数据OK！");

            return true;
        }

        private static byte[] GetDspBinFileBuffer(string dspBinPath)
        {
            if (!System.IO.File.Exists(dspBinPath))
            {
                LogHelper.WriteInfo("文件 {0} 不存在！", Path.GetFileName(dspBinPath));

                return null;
            }

            try
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(dspBinPath);

                using (var fileStream = File.Open(dspBinPath, FileMode.Open))
                {
                    int length = (int)fileStream.Length;
                    if ((length & (Size4K - 1)) != 0)
                    {
                        length = (length / Size4K + 1) * Size4K;
                    }
                    if (length < (int)fileStream.Length)
                    {
                        length = (int)fileStream.Length;
                    }

                    byte[] buffer = new byte[length];
                    fileStream.Read(buffer, 0, (int)fileStream.Length);
                    for (int i = (int)fileStream.Length; i < length; ++i)
                    {
                        buffer[i] = DefaultFillData;
                    }

                    if (buffer.Length < Size4K)
                    {
                        return null;
                    }

                    if (!Utils.CheckBinaryData(buffer, 0x110, CrcImageHelper.ChipKeyword))
                    {
                        LogHelper.WriteInfo("CRC镜像文件 {0} 错误！", fileInfo.Name);

                        return null;
                    }

                    return buffer;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("GetDspBinFileBuffer()...异常：{0}", e.Message);
            }

            return null;
        }

        private static byte[] GetMcuAppBinFileBuffer(string mcuBinPath)
        {
            if (!System.IO.File.Exists(mcuBinPath))
            {
                LogHelper.WriteInfo("文件 {0} 不存在！", Path.GetFileName(mcuBinPath));

                return null;
            }

            try
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(mcuBinPath);

                using (var fileStream = File.Open(mcuBinPath, FileMode.Open))
                {
                    int length = (int)fileStream.Length;
                    if ((length & (Size1K - 1)) != 0)
                    {
                        length = (length / Size1K + 1) * Size1K;
                    }
                    if (length < (int)fileStream.Length)
                    {
                        length = (int)fileStream.Length;
                    }

                    byte[] buffer = new byte[length];
                    fileStream.Read(buffer, 0, (int)fileStream.Length);
                    for (int i = (int)fileStream.Length; i < length; ++i)
                    {
                        buffer[i] = DefaultFillData;
                    }

                    if (buffer.Length < Size1K)
                    {
                        return null;
                    }

                    return buffer;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("GetMcuBinFileBuffer()...异常：{0}", e.Message);
            }

            return null;
        }

        public static BinBaseInfo GetDspBinBaseInfo(byte[] fileBuffer)
        {
            System.Diagnostics.Debug.Assert(fileBuffer != null);

            int envAddr = CrcImageHelper.GetBK3288EnvAddr(fileBuffer);
            TLV_Type tlvType = CrcImageHelper.TLV_FindBaseInfo(fileBuffer, envAddr);

            if (tlvType == null)
            {
                LogHelper.WriteInfo("查找 BaseInfo 失败");
                return null;
            }

            BinBaseInfo binBaseInfo = BinBaseInfo.ParseFromBuffer(tlvType.value);

            //计算src bin烧录长度 4k对齐
            //int srcBinSize = ((int)binBaseInfo.szSrcBin + ((int)FlashOperateSize.Size4k - 1)) / (int)FlashOperateSize.Size4k * (int)FlashOperateSize.Size4k;

            return binBaseInfo;
        }

        private bool GetCheckCRC_V1(int addr, int length, ref uint crc)
        {
            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPFlashCheckCRCCmd(DspIndex, (uint)addr, (uint)Size4K), CheckFlashCRCWait);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.DSPFlashCheckCRC || response.payloadData.Length < sizeof(uint))
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);
            FirmwareUpdateProtocol.DSPCheckFlashCRCStatusCode statusCode = FirmwareUpdateProtocol.GetCheckFlashCRCStatusCode(status);
            if (statusCode == FirmwareUpdateProtocol.DSPCheckFlashCRCStatusCode.IAP_DSP_CHECK_FLASH_CRC_STATUE_OK && response.payloadData.Length == sizeof(uint) * 2)
            {
                uint currCRC = BitConverter.ToUInt32(response.payloadData, sizeof(uint));

                LogHelper.WriteInfo("DSP CheckCRC：{0}", Utils.Uint32ToHexString(currCRC));

                crc = currCRC;

                return true;
            }

            LogHelper.WriteInfo("DSP CheckCRC 错误：{0}！", Utils.Uint32ToHexString(status));

            return false;
        }

        private bool GetDSPCheckFlashCRC_V2(int addr, int length, ref uint crc)
        {
            System.Diagnostics.Debug.Assert(this.fuDevice != null);

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPCheckFlashCRCCmdV2(DspIndex, (uint)addr, (uint)length), CheckFlashCRCWait);

            FirmwareUpdateProtocol.FUResponseV2 response = FirmwareUpdateProtocol.ParseResponseV2(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V2.DSPCheckFlashCRC || response.payloadData.Length < sizeof(uint))
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);
            FirmwareUpdateProtocol.DSPCheckFlashCRCStatusCodeV2 statusCode = FirmwareUpdateProtocol.GetCheckFlashCRCStatusCodeV2(status);
            if (statusCode == FirmwareUpdateProtocol.DSPCheckFlashCRCStatusCodeV2.IAP_DSP_CHECK_FLASH_CRC_STATUE_OK && response.payloadData.Length == sizeof(uint) * 2)
            {
                uint currCRC = BitConverter.ToUInt32(response.payloadData, sizeof(uint));

                LogHelper.WriteInfo("DSP CheckCRC：{0}", Utils.Uint32ToHexString(currCRC));

                crc = currCRC;

                return true;
            }

            LogHelper.WriteInfo("DSP CheckCRC 错误：{0}！", Utils.Uint32ToHexString(status));

            return false;
        }

        private const uint systemAddressBase = 0x01000000;
        private const uint systemClockGatingAddr = systemAddressBase + sizeof(uint) * 3;
        private const uint systemApbSubsysPowerAddr = systemAddressBase + sizeof(uint) * 5;
        private const uint systemApbSubsysClockGatingAddr = systemAddressBase + sizeof(uint) * 6;
        private const int systemSubsysEfuseIndex = 17;
        private const uint systemAnalogSPI5BAddr = systemAddressBase + sizeof(uint) * 0x5b;
        private const int systemAnalogSPI5BIndex = 30;
        private const uint efuseAddressBase = 0x01888000;
        private const uint efuseConfigAddr = efuseAddressBase + sizeof(uint) * 0;
        private const uint efuseReadDataAddr = efuseAddressBase + sizeof(uint) * 1;
        private const uint deviceIdRegisterAddr = 0x01000800 + 4 * 0x10;

        private bool ReadEfuse(DSPEfuse efuse)
        {
            this.OutputInfo("Read Efuse...");
            for (uint addr = 0; addr < DSPEfuse.EfuseSize; ++addr)
            {
                //uint addr = (i & 0x1f) << 8;
                uint data = BitHelper.GetUint(0, true) | ((addr & 0x1f) << 8) | BitHelper.GetUint(31, true);
                if (!this.DSP_WriteRegister(efuseConfigAddr, data))
                {
                    return false;
                }
                EfuseOperateState efuseOperateState = WaitEfuseOperateFinished();
                if (efuseOperateState == EfuseOperateState.Timeout)
                {
                    this.OutputInfo("Time Out...");
                    return false;
                }

                if (this.DSP_ReadRegister(0, efuseReadDataAddr, ref data))
                {
                    if ((data & BitHelper.GetUint(8, true)) != 0)
                    {
                        efuse.data[addr] = (byte)(data & 0xff);
                    }
                }
            }

            byte[] keys = new byte[DSPEfuse.KeysSize];
            Array.Copy(efuse.data, keys, keys.Length);
            byte[] keysHash = Tools.HashHelper.SHA256(keys);

            //Log.OutputLine("Efuse Config:");
            //Log.OutputLine(efuse.ToString());
            this.OutputInfoLine("Efuse Config:");
            //this.OutputInfoLine(efuse.ToString());
            Log.OutputLine(efuse.ToString());

            //Log.OutputLine("key data: {0}", Utils.ByteArrayToHexStr(keys, string.Empty));
            //Log.OutputLine("key hash: {0}", Utils.ByteArrayToHexStr(keysHash, string.Empty));

            this.OutputInfoLine("OK");

            return true;
        }

        public enum EfuseOperateState
        {
            Finished,       //已经完成
            Timeout,        //超时
        };

        private const int WaitEfuseOperateFinishRetries = 0x10;

        private EfuseOperateState WaitEfuseOperateFinished()
        {
            int count = 0;
            uint data = 0;
            while (count++ < WaitEfuseOperateFinishRetries)
            {
                if (this.DSP_ReadRegister(0, efuseConfigAddr, ref data))
                {
                    if ((data & BitHelper.GetUint(0, true)) == 0)
                    {
                        return EfuseOperateState.Finished;
                    }
                }
                else
                {
                    return EfuseOperateState.Timeout;
                }
            }

            return EfuseOperateState.Timeout;
        }

        private bool EnableEfuse(bool enable)
        {
            //this.OutputInfo("Enable Efuse...{0}", Utils.BoolToString(enable));

            //if (!WriteRegisterBit(systemClockGatingAddr, 0, enable))
            //{
            //    return false;
            //}

            //if (!WriteRegisterBit(systemApbSubsysPowerAddr, systemSubsysEfuseIndex, !enable))
            //{
            //    return false;
            //}

            //if (!WriteRegisterBit(systemApbSubsysClockGatingAddr, systemSubsysEfuseIndex, enable))
            //{
            //    return false;
            //}

            //if (!WriteRegisterBit(systemAnalogSPI5BAddr, systemAnalogSPI5BIndex, enable))
            //{
            //    return false;
            //}

            return true;
        }

        private bool DSP_DownloadImage_V1(DSPProgrammingTask dspProgrammingTask)
        {
            //计算src bin烧录长度 4k对齐
            int envAddr = (int)dspProgrammingTask.envAddr;
            int wavSize = (int)dspProgrammingTask.wavSize;
            int srcBinSize = (int)dspProgrammingTask.binSize;
            int binStartAddr = BIN_WRITE_START_ADDRESS;
            //int binStartAddr = 0;

            //写入数据总长度，bin文件size + wav文件size + env参数
            int writeDataSize = srcBinSize + wavSize + (int)FlashOperateSize.Size4k;
            int writenLength = 0;

            this.ShowProgress(true);

            for (int addr = binStartAddr; addr < srcBinSize; )
            {
                LogHelper.WriteInfo("Write DSP Flash Sector...地址：{0}", Utils.Uint32ToHexString((uint)addr));
                if ((addr & ((int)FlashOperateSize.Size4k - 1)) == 0)
                {
                    if (srcBinSize - addr >= (int)FlashOperateSize.Size4k)
                    {
                        LogHelper.WriteInfo("Write DSP Flash Sector...地址:{0}，长度4k...", Utils.Uint32ToHexString((uint)addr));

                        this.OutputInfoLine("Downloading DSP firmware...address : {0}, size : 4k...", Utils.Uint32ToHexString((uint)addr));

                        if (!DSP_Download4K_V1(DspIndex, dspProgrammingTask.fileBuffer, (int)addr, (uint)addr))
                        {
                            LogHelper.WriteInfo("失败！");
                            this.OutputInfoLine("Failed to download DSP firmware...address : {0}, size : 4k...", Utils.Uint32ToHexString((uint)addr));
                            return false;
                        }

                        LogHelper.WriteInfo("OK");
                    }
                    else
                    {
                        LogHelper.WriteInfo("无法处理尾部数据！");
                        return false;
                    }

                    addr += (int)FlashOperateSize.Size4k;

                    writenLength += (int)FlashOperateSize.Size4k;
                }
                else
                {
                    LogHelper.WriteInfo("Bin文件Size没有4K对齐处理！");
                    return false;
                }

                this.UpdateProgress((float)writenLength / (float)writeDataSize);
            }

            if(wavSize > 0)
            {
                int wavStartAddr = envAddr - wavSize;

                for (int addr = wavStartAddr; addr < envAddr; )
                {
                    LogHelper.WriteInfo("Write DSP Flash Sector...地址：{0}", Utils.Uint32ToHexString((uint)addr));
                    if ((addr & ((int)FlashOperateSize.Size4k - 1)) == 0)
                    {
                        if (envAddr - addr >= (int)FlashOperateSize.Size4k)
                        {
                            LogHelper.WriteInfo("Write DSP Flash Sector...地址:{0}，长度4k...", Utils.Uint32ToHexString((uint)addr));

                            this.OutputInfoLine("Downloading DSP firmware...address : {0}, size : 4k...", Utils.Uint32ToHexString((uint)addr));

                            if (!DSP_Download4K_V1(DspIndex, dspProgrammingTask.fileBuffer, (int)addr, (uint)addr))
                            {
                                this.OutputInfoLine("Failed to download DSP firmware...address : {0}, size : 4k...", Utils.Uint32ToHexString((uint)addr));
                                LogHelper.WriteInfo("失败！");
                                return false;
                            }

                            LogHelper.WriteInfo("OK");
                        }
                        else
                        {
                            LogHelper.WriteInfo("无法处理尾部数据！");
                            return false;
                        }

                        addr += (int)FlashOperateSize.Size4k;

                        writenLength += (int)FlashOperateSize.Size4k;
                    }
                    else
                    {
                        LogHelper.WriteInfo("Bin文件Size没有4K对齐处理！");
                        return false;
                    }

                    this.UpdateProgress((float)writenLength / (float)writeDataSize);
                }
            }

            LogHelper.WriteInfo("Write Flash Sector...（Env Config）地址:{0}，长度4k...", Utils.Uint32ToHexString((uint)envAddr));
            this.OutputInfoLine("Downloading DSP firmware...address : {0}, size : 4k...", Utils.Uint32ToHexString((uint)envAddr));
            if (!DSP_Download4K_V1(DspIndex, dspProgrammingTask.fileBuffer, (int)envAddr, (uint)envAddr))
            {
                this.OutputInfoLine("Failed to download DSP firmware...address : {0}, size : 4k...", Utils.Uint32ToHexString((uint)envAddr));
                LogHelper.WriteInfo("失败！");
                return false;
            }

            LogHelper.WriteInfo("OK");

            writenLength += (int)FlashOperateSize.Size4k;
            this.UpdateProgress((float)writenLength / (float)writeDataSize);

            LogHelper.WriteInfo("Write Flash Sector...（0-th Sector）地址:{0}，长度4k...", Utils.Uint32ToHexString((uint)0));
            this.OutputInfoLine("Downloading DSP firmware...address : {0}, size : 4k...", Utils.Uint32ToHexString((uint)0));
            if (!DSP_Download4K_V1(DspIndex, dspProgrammingTask.fileBuffer, 0, 0))
            {
                LogHelper.WriteInfo("失败！");
                this.OutputInfoLine("Failed to download DSP firmware...address : {0}, size : 4k...", Utils.Uint32ToHexString((uint)0));
                return false;
            }

            writenLength += (int)FlashOperateSize.Size4k;
            this.UpdateProgress((float)writenLength / (float)writeDataSize);

            System.Threading.Thread.Sleep(0x100);

            this.ShowProgress(false);

            return true;
        }

        private bool DSP_DownloadImage_V2(DSPProgrammingTask dspProgrammingTask, BTDeviceInfo btDeviceInfo)
        {
            //计算src bin烧录长度 4k对齐
            int envAddr = (int)dspProgrammingTask.envAddr;
            int wavSize = (int)dspProgrammingTask.wavSize;
            int srcBinSize = (int)dspProgrammingTask.binSize;
            int binStartAddr = BIN_WRITE_START_ADDRESS;
            //int binStartAddr = 0;

            //写入数据总长度，bin文件size + wav文件size + env参数
            int writeDataSize = srcBinSize + wavSize + (int)FlashOperateSize.Size4k;
            int writenLength = 0;

            this.ShowProgress(true);

            for (int addr = binStartAddr; addr < srcBinSize; )
            {
                LogHelper.WriteInfo("Write DSP Flash Sector...地址：{0}", Utils.Uint32ToHexString((uint)addr));
                if ((addr & ((int)FlashOperateSize.Size4k - 1)) == 0)
                {
                    if (srcBinSize - addr >= (int)FlashOperateSize.Size4k)
                    {
                        LogHelper.WriteInfo("Write DSP Flash Sector...地址:{0}，长度4k...", Utils.Uint32ToHexString((uint)addr));

                        this.OutputInfoLine("Downloading DSP firmware...address : {0}, size : 4k...", Utils.Uint32ToHexString((uint)addr));

                        if (!DSP_WriteFlash4K_V2(DspIndex, dspProgrammingTask.fileBuffer, (int)addr, (uint)addr))
                        {
                            LogHelper.WriteInfo("失败！");
                            this.OutputInfoLine("Failed to download DSP firmware...address : {0}, size : 4k...", Utils.Uint32ToHexString((uint)addr));
                            return false;
                        }

                        LogHelper.WriteInfo("OK");
                    }
                    else
                    {
                        LogHelper.WriteInfo("无法处理尾部数据！");
                        return false;
                    }

                    addr += (int)FlashOperateSize.Size4k;

                    writenLength += (int)FlashOperateSize.Size4k;
                }
                else
                {
                    LogHelper.WriteInfo("Bin文件Size没有4K对齐处理！");
                    return false;
                }

                this.UpdateProgress((float)writenLength / (float)writeDataSize);
            }

            if(wavSize > 0)
            {
                int wavStartAddr = envAddr - wavSize;

                for (int addr = wavStartAddr; addr < envAddr; )
                {
                    LogHelper.WriteInfo("Write DSP Flash Sector...地址：{0}", Utils.Uint32ToHexString((uint)addr));
                    if ((addr & ((int)FlashOperateSize.Size4k - 1)) == 0)
                    {
                        if (envAddr - addr >= (int)FlashOperateSize.Size4k)
                        {
                            LogHelper.WriteInfo("Write DSP Flash Sector...地址:{0}，长度4k...", Utils.Uint32ToHexString((uint)addr));

                            this.OutputInfoLine("Downloading DSP firmware...address : {0}, size : 4k...", Utils.Uint32ToHexString((uint)addr));

                            if (!DSP_WriteFlash4K_V2(DspIndex, dspProgrammingTask.fileBuffer, (int)addr, (uint)addr))
                            {
                                this.OutputInfoLine("Failed to download DSP firmware...address : {0}, size : 4k...", Utils.Uint32ToHexString((uint)addr));
                                LogHelper.WriteInfo("失败！");
                                return false;
                            }

                            LogHelper.WriteInfo("OK");
                        }
                        else
                        {
                            LogHelper.WriteInfo("无法处理尾部数据！");
                            return false;
                        }

                        addr += (int)FlashOperateSize.Size4k;

                        writenLength += (int)FlashOperateSize.Size4k;
                    }
                    else
                    {
                        LogHelper.WriteInfo("Bin文件Size没有4K对齐处理！");
                        return false;
                    }

                    this.UpdateProgress((float)writenLength / (float)writeDataSize);
                }
            }

            dspProgrammingTask.envConfigData = new byte[(int)FlashOperateSize.Size4k];
            Array.Copy(dspProgrammingTask.fileBuffer, envAddr, dspProgrammingTask.envConfigData, 0, dspProgrammingTask.envConfigData.Length);

            if (btDeviceInfo != null)
            {
                if (!CrcImageHelper.TLV_Modify(dspProgrammingTask.envConfigData, 0, CrcImageHelper.TLV_TYPE_CFG_BT_ADDR, btDeviceInfo.addrBuffer))
                {
                    this.OutputErrorInfoLine("更新蓝牙设备地址失败!");
                    return false;
                }

                if (!CrcImageHelper.TLV_Modify(dspProgrammingTask.envConfigData, 0, CrcImageHelper.TLV_TYPE_CFG_BT_NAME, btDeviceInfo.nameBuffer))
                {
                    this.OutputErrorInfoLine("更新蓝牙设备名称失败!");
                    return false;
                }
            }

            LogHelper.WriteInfo("Write Flash Sector...（Env Config）地址:{0}，长度4k...", Utils.Uint32ToHexString((uint)envAddr));
            this.OutputInfoLine("Downloading DSP firmware...address : {0}, size : 4k...", Utils.Uint32ToHexString((uint)envAddr));
            if (!DSP_WriteFlash4K_V2(DspIndex, dspProgrammingTask.envConfigData, 0, (uint)envAddr))
            {
                this.OutputInfoLine("Failed to download DSP firmware...address : {0}, size : 4k...", Utils.Uint32ToHexString((uint)envAddr));
                LogHelper.WriteInfo("失败！");
                return false;
            }

            if(this.lastDeviceInfo != null)
            {
                this.lastDeviceInfo.AddressRollingCode();
            }

            LogHelper.WriteInfo("OK");

            writenLength += (int)FlashOperateSize.Size4k;
            this.UpdateProgress((float)writenLength / (float)writeDataSize);

            LogHelper.WriteInfo("Write Flash Sector...（0-th Sector）地址:{0}，长度4k...", Utils.Uint32ToHexString((uint)0));
            this.OutputInfoLine("Downloading DSP firmware...address : {0}, size : 4k...", Utils.Uint32ToHexString((uint)0));
            if (!DSP_WriteFlash4K_V2(DspIndex, dspProgrammingTask.fileBuffer, 0, 0))
            {
                LogHelper.WriteInfo("失败！");
                this.OutputInfoLine("Failed to download DSP firmware...address : {0}, size : 4k...", Utils.Uint32ToHexString((uint)0));
                return false;
            }

            writenLength += (int)FlashOperateSize.Size4k;
            this.UpdateProgress((float)writenLength / (float)writeDataSize);

            System.Threading.Thread.Sleep(0x100);

            this.ShowProgress(false);

            return true;
        }

        private byte[] ReadDSPIDTest(FirmwareUpdateTask task)
        {
            if (task.firmware.mcuModel == "MH22D3RMU6")
            {
                if (!DSP_PowerSupplyControl(DspIndex, false))
                {
                    LogHelper.WriteInfo("断开DSP电源失败！");
                    return null;
                }

                LogHelper.WriteInfo("断开DSP电源");

                System.Threading.Thread.Sleep(64);
                if (!DSP_PowerSupplyControl(DspIndex, true))
                {
                    LogHelper.WriteInfo("开启DSP供电！");
                    return null;
                }

                LogHelper.WriteInfo("开启DSP供电");

                startTimeStamp = DateTime.Now;
            }

            byte[] dspCommCmd = new byte[]{0xDF, 0xBE, 0xAD, 0xDE, 0x0A, 0x01, 0x34, 0x68,};

            int timeHasPassed = (int)(DateTime.Now - startTimeStamp).TotalMilliseconds;
            int requiredTime = CommWaitTimeUnit * 2;
            if(timeHasPassed < requiredTime)
            {
                System.Threading.Thread.Sleep(requiredTime - timeHasPassed);
            }
            
            int count = 0;
            while(count++ <= 3)
            {
                byte[] dspResponse = DSP_CommDirectly(DspIndex, 8, CommWaitTimeUnit, dspCommCmd);
                //LogHelper.WriteInfo("读取Flash 4K数据完成！");
                if (dspResponse == null)
                {
                    LogHelper.WriteInfo("直接和DSP通信失败！");
                    return null;
                }
                else
                {
                    LogHelper.WriteInfo("DSP 回复 = {0}", AEC.Core.Utils.ByteArrayToHexStr(dspResponse));
                    if (dspResponse.Length == sizeof(uint) * 2)
                    {
                        uint deviceId = BitConverter.ToUInt32(dspResponse, sizeof(uint));

                        deviceId = Utils.ReverseBytes(deviceId);

                        LogHelper.WriteInfo("读取DSP ID 成功, id = {0}", AEC.Core.Utils.ByteArrayToHexStr(BitConverter.GetBytes(deviceId), string.Empty));

                        return BitConverter.GetBytes(deviceId);
                    }
                }

                if(!DSP_PowerSupplyControl(DspIndex, false))
                {
                    LogHelper.WriteInfo("断开DSP电源失败！");
                    return null;
                }

                LogHelper.WriteInfo("断开DSP电源");

                System.Threading.Thread.Sleep(64);
                if (!DSP_PowerSupplyControl(DspIndex, true))
                {
                    LogHelper.WriteInfo("开启DSP供电！");
                    return null;
                }

                LogHelper.WriteInfo("开启DSP供电");

                System.Threading.Thread.Sleep(CommWaitTimeUnit + CommWaitTimeUnit * count);
            }

            LogHelper.WriteInfo("读取DSP ID 失败");

            return null;
        }

        private bool DSP_ReadFlash4KTest(DSPProgrammingTask dspProgrammingTask)
        {
            uint address = 0x3000;
            byte[] data4K = DSP_ReadFlash4K(DspIndex, address);
            LogHelper.WriteInfo("读取Flash 4K数据完成！");
            if (data4K == null)
            {
                LogHelper.WriteInfo("读取Flash 4K数据失败！");
                return false;
            }

            LogHelper.WriteInfo("读取 addr = {0}, data : {1}", Utils.Uint32ToHexString(address), AEC.Core.Utils.ByteArrayToHexStr(data4K));

           
            uint binCrc = 0xffffffff;
            binCrc = CrcImageHelper.CheckCRC(binCrc, data4K, 0, Size4K);

            uint targetCRC = 0xffffffff;
            targetCRC = CrcImageHelper.CheckCRC(targetCRC, dspProgrammingTask.fileBuffer, (int)address, Size4K);

            LogHelper.WriteInfo("CRC = {0}, target CRC = {1}", Utils.Uint32ToHexString(binCrc), Utils.Uint32ToHexString(targetCRC));

            return true;
        }

        private bool DSP_DownloadBinToMCUFlash(DspProgrammerDownloadTask task)
        {
            System.Diagnostics.Debug.Assert(this.fuDevice != null && task.dspFwPackage.dspFirmwareData != null);

            //int envAddr = CrcImageHelper.GetBK3288EnvAddr(task.FileBuffer);
            //TLV_Type tlvType = CrcImageHelper.TLV_FindBaseInfo(task.FileBuffer, envAddr);

            //if (tlvType == null)
            //{
            //    LogHelper.WriteInfo("查找 BaseInfo 失败");
            //    this.OutputErrorInfoLine("查找 BaseInfo 失败!");
            //    return false;
            //}

            //BinBaseInfo binBaseInfo = BinBaseInfo.ParseFromBuffer(tlvType.value);

            //计算src bin烧录长度 4k对齐
            //int fileSize = ((int)task.FileBuffer.Length + ((int)FlashOperateSize.Size4k - 1)) / (int)FlashOperateSize.Size4k * (int)FlashOperateSize.Size4k;
            //int binStartAddr = 0;
            
            //写入数据总长度，bin文件size + env参数size + 第 0 个扇区
            //int writeDataSize = srcBinSize + (int)FlashOperateSize.Size4k + (int)FlashOperateSize.Size4k;
            //int writenLength = 0;

            int fileSize = task.dspFwPackage.dspFirmwareData.Length;
            if (fileSize == 0 || fileSize % (int)FlashOperateSize.Size4k != 0)
            {
                LogHelper.WriteInfo("Image 缓冲区长度错误或没有4k对齐");
                return false;
            }

            int addr = 0;
            while(true)
            {
                if(addr >= fileSize)
                {
                    //LogHelper.WriteInfo("addr:{0}, fileSize:{1}...", Utils.Uint32ToHexString((uint)addr), Utils.Uint32ToHexString((uint)fileSize));
                    break;
                }

                int remainder = fileSize - addr;                
                remainder = AEC.Core.Utils.ConstrainMax((int)remainder, (int)FlashOperateSize.Size32k);

                LogHelper.WriteInfo("Write Flash Sector...地址:{0}，长度:{1}...", Utils.Uint32ToHexString((uint)addr), Utils.Uint32ToHexString((uint)remainder));
                this.OutputInfoLine("Write Flash Sector...地址:{0}，长度:{1}...", Utils.Uint32ToHexString((uint)addr), Utils.Uint32ToHexString((uint)remainder));

                if (!DSP_DownloadDataToMCU(DspIndex, task.dspFwPackage.dspFirmwareData, (uint)addr, remainder))
                {
                    LogHelper.WriteInfo("失败！");
                    this.OutputErrorInfoLine("失败！");
                    return false;
                }

                this.OutputInfo("OK");
                addr += remainder;
            }

            //for (int addr = 0; addr < fileSize; )
            //{
            //    LogHelper.WriteInfo("Write Flash Sector...地址：{0}", Utils.Uint32ToHexString((uint)addr));
            //    if ((addr & ((int)FlashOperateSize.Size4k - 1)) == 0)
            //    {
            //        if (fileSize - addr >= (int)FlashOperateSize.Size4k)
            //        {
            //            LogHelper.WriteInfo("Write Flash Sector...地址:{0}，长度4k...", Utils.Uint32ToHexString((uint)addr));

            //            this.OutputInfo("Write Flash Sector...地址:{0}，长度4k...", Utils.Uint32ToHexString((uint)addr));

            //            if (!DSP_Download4KToMCU(DspIndex, (uint)addr, task.FileBuffer))
            //            {
            //                LogHelper.WriteInfo("失败！");
            //                this.OutputErrorInfoLine("失败！");
            //                return false;
            //            }

            //            LogHelper.WriteInfo("OK");
            //            this.OutputInfoLine("OK");
            //        }
            //        else
            //        {
            //            LogHelper.WriteInfo("无法处理尾部数据！");
            //            this.OutputErrorInfoLine("无法处理尾部数据！");
            //            return false;
            //        }

            //        addr += (int)FlashOperateSize.Size4k;
            //    }
            //    else
            //    {
            //        LogHelper.WriteInfo("Bin文件Size没有4K对齐处理！");
            //        this.OutputErrorInfoLine("Bin文件Size没有4K对齐处理！");
            //        return false;
            //    }
            //}


            this.OutputInfoLine("Download Bin 到 MCU Flash 成功.");

            //LogHelper.WriteInfo("Write Flash Sector...（Env Config）地址:{0}，长度4k...", Utils.Uint32ToHexString((uint)envAddr));
            //if (!DSP_Download4KToMCU(DspIndex, (uint)envAddr, task.FileBuffer))
            //{
            //    LogHelper.WriteInfo("失败！");
            //    return false;
            //}

            //LogHelper.WriteInfo("Write Flash Sector...（0-th Sector）地址:{0}，长度4k...", Utils.Uint32ToHexString((uint)0));
            //if (!DSP_Download4KToMCU(DspIndex, 0, task.FileBuffer))
            //{
            //    LogHelper.WriteInfo("失败！");
            //    return false;
            //}

            return true;
        }

        //private bool UpdateBaudRate()
        //{
        //    System.Diagnostics.Debug.Assert(this.fuDevice != null);

        //    //如果不是串口，则直接跳过
        //    if (this.fuDevice.GetType() != typeof(FUSerialPortDevice))
        //    {
        //        return true;
        //    }

        //    FUSerialPortDevice fuSP = this.fuDevice as FUSerialPortDevice;

        //    fuSP.SyncSend(FirmwareUpgradeProtocol.GetUpdateBaudRateCmd(DownloadDataMCUBaudRate, SetBaudRateDelay), 0);

        //    fuSP.Close();
        //    //System.Threading.Thread.Sleep(0x100);
        //    fuSP.BaudRate = DownloadDataMCUBaudRate;
        //    if (!fuSP.Open())
        //    {
        //        LogHelper.WriteInfo("更新波特率，打开端口：{0} 波特率：{1} 失败！", fuSP.portName, fuSP.BaudRate);
        //        return false;
        //    }

        //    fuSP.WaitResponse(SetBaudRateDelay * 20);

        //    FirmwareUpgradeProtocol.FUResponse response = FirmwareUpgradeProtocol.ParseResponse(this._recvBuffer);
        //    if (response == null)
        //    {
        //        LogHelper.WriteInfo("解析响应数据失败");
        //        return false;
        //    }

        //    if (response.cmd != FirmwareUpgradeProtocol.FUCommmand.SetBaudRate || response.payloadData.Length != sizeof(uint) * 2)
        //    {
        //        LogHelper.WriteInfo("设置波特率响应数据有误！");
        //        return false;
        //    }

        //    uint baudRate = BitConverter.ToUInt32(response.payloadData, 0);
        //    uint delay = BitConverter.ToUInt32(response.payloadData, 4);
        //    if (baudRate != DownloadDataMCUBaudRate || delay != SetBaudRateDelay)
        //    {
        //        LogHelper.WriteInfo("设置波特率响应数据有误！");
        //        return false;
        //    }

        //    LogHelper.WriteInfo("设置波特率 {0} 成功！", DownloadDataMCUBaudRate);

        //    return true;
        //}

        private bool DSP_CommSetUartBaudRate(uint dspIndex, uint baudRate)
        {
            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPCommSetUartBaudRateCmd(DspIndex, baudRate), CommWaitTimeUnit * 1);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.DSPCommSetUartBaudRate || response.payloadData.Length != sizeof(uint))
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);

            if(status == 0)
            {
                LogHelper.WriteInfo("设置波特率成功.");
                return true;
            }
            else
            {
                LogHelper.WriteInfo("设置波特率失败.");
                return false;
            }
        }

        private bool DSP_PowerSupplyControl(uint dspIndex, bool isON)
        {
            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPPowerSupplyControlCmd(DspIndex, isON), CommWaitTimeUnit);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.DSPPowerSupplyControl || response.payloadData.Length != sizeof(uint))
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);

            if (status == 0)
            {
                LogHelper.WriteInfo("DSP电源控制成功.");
                return true;
            }
            else
            {
                LogHelper.WriteInfo("DSP电源控制失败.");
                return false;
            }
        }

        private byte[] DSP_CommDirectly(uint dspIndex, uint expectResponseLength, uint timeoutInMs, byte[] dspCommCmd)
        {
            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPCommDirectlyCmd(dspIndex, expectResponseLength, timeoutInMs, dspCommCmd), CommWaitTimeUnit + (int)timeoutInMs);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return null;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.DSPCommDirectly || response.payloadData.Length < sizeof(uint))
            {
                LogHelper.WriteInfo("响应数据有误！");
                return null;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);

            if (status == 0)
            {
                byte[] dspResponse = new byte[response.payloadData.Length - sizeof(uint)];
                Array.Copy(response.payloadData, sizeof(uint), dspResponse, 0, dspResponse.Length);
                LogHelper.WriteInfo("DSP直接通讯成功.");

                return dspResponse;
            }
            else
            {
                LogHelper.WriteInfo("DSP直接通讯失败.");
                return null;
            }
        }

        private bool DSP_LinkToBootROM()
        {
            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPLinkToBootROMCmd(DspIndex, 64, DownloadBaudRate), CommWaitTimeUnit);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.DSPLinkToBootROM || response.payloadData.Length != sizeof(uint))
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);

            if (status == 0)
            {
                LogHelper.WriteInfo("Link To BootROM 成功.");
                return true;
            }
            else
            {
                LogHelper.WriteInfo("Link To BootROM 失败.");
                return false;
            }
        }

        private bool DSP_WriteRegister(uint address, uint data)
        {
            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPWriteRegisterCmd(DspIndex, address, data), CommWaitTimeUnit);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.DSPWriteRegister || response.payloadData.Length != sizeof(uint) * 1)
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);

            if (status == 0)
            {
                LogHelper.WriteInfo("Write Register addr = {0} data = {1} 成功.", Utils.Uint32ToHexString(address), Utils.Uint32ToHexString(data));
                return true;
            }
            else
            {
                LogHelper.WriteInfo("Write Register 失败.");
                return false;
            }
        }

        private bool DSP_WriteRegister_V2(uint address, uint data)
        {
            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPWriteRegisterCmdV2(DspIndex, address, data), CommWaitTimeUnit);

            FirmwareUpdateProtocol.FUResponseV2 response = FirmwareUpdateProtocol.ParseResponseV2(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V2.DSPWriteRegister || response.payloadData.Length != sizeof(uint) * 1)
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);

            if (status == 0)
            {
                LogHelper.WriteInfo("Write Register addr = {0} data = {1} 成功.", Utils.Uint32ToHexString(address), Utils.Uint32ToHexString(data));
                return true;
            }
            else
            {
                LogHelper.WriteInfo("Write Register 失败.");
                return false;
            }
        }

        private bool DSP_ReadRegister(uint dspIndex, uint address, ref uint data)
        {
            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPReadRegisterCmd(DspIndex, address), CommWaitTimeUnit);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.DSPReadRegister || response.payloadData.Length < sizeof(uint) * 1)
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);


            if (status == 0 && response.payloadData.Length == sizeof(uint) * 2)
            {
                data = BitConverter.ToUInt32(response.payloadData, 4);
                LogHelper.WriteInfo("Read Register addr = {0} data = {1} 成功.", Utils.Uint32ToHexString(address), Utils.Uint32ToHexString(data));
                return true;
            }
            else
            {
                LogHelper.WriteInfo("Read Register 失败.");
                return false;
            }
        }

        private bool DSP_ReadRegister_V2(uint dspIndex, uint address, ref uint data)
        {
            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPReadRegisterCmdV2(DspIndex, address), CommWaitTimeUnit);

            FirmwareUpdateProtocol.FUResponseV2 response = FirmwareUpdateProtocol.ParseResponseV2(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V2.DSPReadRegister || response.payloadData.Length < sizeof(uint) * 1)
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);


            if (status == 0 && response.payloadData.Length == sizeof(uint) * 2)
            {
                data = BitConverter.ToUInt32(response.payloadData, 4);
                LogHelper.WriteInfo("Read Register addr = {0} data = {1} 成功.", Utils.Uint32ToHexString(address), Utils.Uint32ToHexString(data));
                return true;
            }
            else
            {
                LogHelper.WriteInfo("Read Register 失败.");
                return false;
            }
        }

        private bool DSP_BootProtectFlash(uint dspIndex, bool isProtect)
        {
            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPBootProtectFlashCmd(dspIndex, isProtect), CommWaitTimeUnit * 4);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.DSPBootProtectFlash || response.payloadData.Length != sizeof(uint) * 1)
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);

            if (status == 0)
            {
                LogHelper.WriteInfo("Boot Protect Flash 成功.");
                return true;
            }
            else
            {
                LogHelper.WriteInfo("Boot Protect Flash 失败.");
                return false;
            }
        }

        private bool DSP_BootProtectFlashV2(uint dspIndex, bool isProtect, out string errorMsg)
        {
            errorMsg = string.Empty;

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPBootProtectFlashCmdV2(dspIndex, isProtect), CommWaitTimeUnit * 1);

            FirmwareUpdateProtocol.FUResponseV2 response = FirmwareUpdateProtocol.ParseResponseV2(this._recvBuffer);
            if (response == null)
            {
                errorMsg = "Failed to parse response data";
                LogHelper.WriteInfo(errorMsg);
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V2.DSPBootProtectFlash || response.payloadData.Length != sizeof(uint) * 1)
            {
                errorMsg = "Response data is incorrect";
                LogHelper.WriteInfo(errorMsg);				
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);

            if (status == 0)
            {
                LogHelper.WriteInfo("Boot Protect Flash 成功.");
                return true;
            }
            else
            {
                errorMsg = string.Format("Error Code ：{0}", Utils.Uint32ToHexString(status));
                LogHelper.WriteInfo(errorMsg);
                return false;
            }
        }

        private bool DSP_EraseFlash(uint dspIndex, uint startAddress, uint size)
        {
            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPEraseFlashCmd(dspIndex, startAddress, size), CommWaitTimeUnit * 4);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.DSPEraseFlash || response.payloadData.Length != sizeof(uint) * 1)
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);

            if (status == 0)
            {
                LogHelper.WriteInfo("Erase Flash 成功...startAddress = {0}, size = {1}", Utils.Uint32ToHexString(startAddress), Utils.Uint32ToHexString(size));
                return true;
            }
            else
            {
                LogHelper.WriteInfo("Erase Flash 失败.");
                return false;
            }
        }

        private bool DSP_EraseFlashV2(uint dspIndex, uint startAddress, uint size, out string errorMsg)
        {
            errorMsg = string.Empty;

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPEraseFlashCmdV2(dspIndex, startAddress, size), CommWaitTimeUnit * 4);

            FirmwareUpdateProtocol.FUResponseV2 response = FirmwareUpdateProtocol.ParseResponseV2(this._recvBuffer);
            if (response == null)
            {
                errorMsg = "Failed to parse response data";
                LogHelper.WriteInfo(errorMsg);
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V2.DSPEraseFlash || response.payloadData.Length != sizeof(uint) * 1)
            {
                errorMsg = "Response data is incorrect";
                LogHelper.WriteInfo(errorMsg);
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);

            if (status == 0)
            {
                LogHelper.WriteInfo("Erase Flash 成功...startAddress = {0}, size = {1}", Utils.Uint32ToHexString(startAddress), Utils.Uint32ToHexString(size));
                return true;
            }
            else
            {
                errorMsg = string.Format("Error Code ：{0}", Utils.Uint32ToHexString(status));
                return false;
            }
        }
        

        //private bool DSP_WriteFlash4K(uint dspIndex, uint address, byte[] buffer)
        //{
        //    System.Diagnostics.Debug.Assert(this.fuDevice != null);

        //    this.fuDevice.SyncSend(FirmwareUpgradeProtocol.GetDSPWriteFlash4KCmd(DspIndex, address, buffer, address), Download4KWait);

        //    FirmwareUpgradeProtocol.FUResponse response = FirmwareUpgradeProtocol.ParseResponse(this._recvBuffer);
        //    if (response == null)
        //    {
        //        LogHelper.WriteInfo("解析响应数据失败");
        //        return false;
        //    }

        //    if (response.cmd != FirmwareUpgradeProtocol.FUCommmand.DSPWriteFlash4K || response.payloadData.Length != sizeof(uint) * 1)
        //    {
        //        LogHelper.WriteInfo("响应数据有误！");
        //        return false;
        //    }

        //    uint status = BitConverter.ToUInt32(response.payloadData, 0);

        //    if (status == 0)
        //    {
        //        LogHelper.WriteInfo("Write Flash 4K 成功.");
        //        return true;
        //    }
        //    else
        //    {
        //        LogHelper.WriteInfo("Write Flash 4K 失败.");
        //        return false;
        //    }
        //}

        private byte[] DSP_ReadFlash4K(uint dspIndex, uint address)
        {
            System.Diagnostics.Debug.Assert(this.fuDevice != null);

            LogHelper.WriteInfo("DSP_ReadFlash4K()...SyncSend()...dspIndex = {0}, address = {1}", dspIndex, address);

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPReadFlash4KCmd(dspIndex, address), WriteFlash4KWait*8);

            LogHelper.WriteInfo("DSP_ReadFlash4K()...SyncSend()...{0}", this._recvBuffer.Count);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return null;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.DSPReadFlash4K || response.payloadData.Length < sizeof(uint) * 1)
            {
                LogHelper.WriteInfo("响应数据有误！");
                return null;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);

            if (status == 0 && response.payloadData.Length == sizeof(uint) + Size4K)
            {
                byte[] buffer = new byte[Size4K];
                Array.Copy(response.payloadData, sizeof(uint), buffer, 0, Size4K);
                LogHelper.WriteInfo("Read Flash 4K 成功.");
                return buffer;
            }
            else
            {
                LogHelper.WriteInfo("Read Flash 4K 失败.");
                return null;
            }
        }

        private byte[] DSP_ReadFlash4K_V2(uint dspIndex, uint address)
        {
            System.Diagnostics.Debug.Assert(this.fuDevice != null);

            LogHelper.WriteInfo("DSP_ReadFlash4K()...SyncSend()...dspIndex = {0}, address = {1}", dspIndex, Utils.Uint32ToHexString(address));

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPReadFlash4KCmdV2(dspIndex, address), ReadFlash4KWait);

            LogHelper.WriteInfo("DSP_ReadFlash4K()...SyncSend()...{0}", AEC.Core.Utils.Uint32ToShortHexStr((uint)this._recvBuffer.Count));

            FirmwareUpdateProtocol.FUResponseV2 response = FirmwareUpdateProtocol.ParseResponseV2(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return null;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V2.DSPReadFlash4K || response.payloadData.Length < sizeof(uint) * 1)
            {
                LogHelper.WriteInfo("响应数据有误！");
                return null;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);

            if (status == 0 && response.payloadData.Length == sizeof(uint) + Size4K)
            {
                byte[] buffer = new byte[Size4K];
                Array.Copy(response.payloadData, sizeof(uint), buffer, 0, Size4K);
                LogHelper.WriteInfo("Read Flash 4K 成功.");
                return buffer;
            }
            else
            {
                LogHelper.WriteInfo("Read Flash 4K 失败.");
                return null;
            }
        }

        //private bool DSP_GetFlashCheckCRC(uint dspIndex, uint address, uint size, ref uint data)
        //{
        //    System.Diagnostics.Debug.Assert(this.fuDevice != null);

        //    LogHelper.WriteInfo("DSP_GetFlashCheckCRC()...SyncSend()...dspIndex = {0}, address = {1}, size = {2}", dspIndex, address, size);

        //    this.fuDevice.SyncSend(FirmwareUpgradeProtocol.GetDSPGetFlashCheckCRCCmd(dspIndex, address, size), Download4KWait * 4);

        //    LogHelper.WriteInfo("DSP_GetFlashCheckCRC()...SyncSend()...{0}", this._recvBuffer.Count);

        //    FirmwareUpgradeProtocol.FUResponse response = FirmwareUpgradeProtocol.ParseResponse(this._recvBuffer);
        //    if (response == null)
        //    {
        //        LogHelper.WriteInfo("解析响应数据失败");
        //        return false;
        //    }

        //    if (response.cmd != FirmwareUpgradeProtocol.FUCommmand.DSPGetFlashCheckCRC || response.payloadData.Length < sizeof(uint) * 1)
        //    {
        //        LogHelper.WriteInfo("响应数据有误！");
        //        return false;
        //    }

        //    uint status = BitConverter.ToUInt32(response.payloadData, 0);

        //    if (status == 0 && response.payloadData.Length == sizeof(uint) * 2)
        //    {
        //        data = BitConverter.ToUInt32(response.payloadData, sizeof(uint));
        //        LogHelper.WriteInfo("Get Flash CRC  = {0}成功. ", Utils.Uint32ToHexString(data));
        //        return true;
        //    }
        //    else
        //    {
        //        LogHelper.WriteInfo("Get Flash CRC 失败.");
        //        return false;
        //    }
        //}

        private bool DSP_Reboot(uint dspIndex)
        {
            System.Diagnostics.Debug.Assert(this.fuDevice != null);

            //LogHelper.WriteInfo("DSP_Reboot()...SyncSend()...dspIndex = {0}", dspIndex);

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPRebootCmd(dspIndex), CommWaitTimeUnit * 0x40);

            //LogHelper.WriteInfo("DSP_Reboot()...SyncSend()...{0}", this._recvBuffer.Count);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.DSPReboot || response.payloadData.Length != sizeof(uint) * 1)
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);

            if (status == 0)
            {
                LogHelper.WriteInfo("DSP Reboot 成功. ");
                return true;
            }
            else
            {
                LogHelper.WriteInfo("DSP Reboot 失败.");
                return false;
            }
        }

        private bool DSP_Reboot_V2(uint dspIndex)
        {
            System.Diagnostics.Debug.Assert(this.fuDevice != null);

            //LogHelper.WriteInfo("DSP_Reboot()...SyncSend()...dspIndex = {0}", dspIndex);

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPRebootCmdV2(dspIndex), CommWaitTimeUnit);

            //LogHelper.WriteInfo("DSP_Reboot()...SyncSend()...{0}", this._recvBuffer.Count);

            FirmwareUpdateProtocol.FUResponseV2 response = FirmwareUpdateProtocol.ParseResponseV2(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V2.DSPReboot || response.payloadData.Length != sizeof(uint) * 1)
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);

            if (status == 0)
            {
                LogHelper.WriteInfo("DSP Reboot 成功. ");
                return true;
            }
            else
            {
                LogHelper.WriteInfo("DSP Reboot 失败.");
                return false;
            }
        }

        private bool DSP_PreDownload_V1(DSPProgrammingTask dspProgrammingTask, out string errorMsg)
        {
            System.Diagnostics.Debug.Assert(this.fuDevice != null);

            errorMsg = string.Empty;

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPPreDownloadCmd_V1(DspIndex, Retries, DSPResetDuration, DownloadBaudRate, dspProgrammingTask.binSize), PreDownloadWait);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                errorMsg = "Failed to parse response data";
                LogHelper.WriteInfo(errorMsg);
                return false;
            }

            if(response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.DSPPreDownload || response.payloadData.Length != sizeof(uint))
            {
                errorMsg = "Response data is incorrect";
                LogHelper.WriteInfo(errorMsg);
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);

            FirmwareUpdateProtocol.DSPPreDownloadStatusCode statusCode = FirmwareUpdateProtocol.GetPreDownloadStatusCode(status);
            if(statusCode != FirmwareUpdateProtocol.DSPPreDownloadStatusCode.IAP_DSP_PRE_DOWNLOAD_STATUS_OK)
            {
                errorMsg = string.Format("Error Code : {0}", Utils.Uint32ToHexString(status));
                LogHelper.WriteInfo(errorMsg);
                return false;
            }

            LogHelper.WriteInfo("预下载成功.");

            return true;
        }

        private byte[] DSP_ReadEnvData(FlashSR flashSR, out uint envAddr)
        {
            uint flashSize = flashSR.szMem / 8; //flash size / byte

            envAddr = 0;

            byte[] sectorData = DSP_ReadFlash4K_V2(DspIndex, 0);
            if (sectorData == null)
            {
                return null;
            }

            int size = CrcImageHelper.GetFDL(sectorData);
            if (size == 0)
            {
                LogHelper.WriteInfo("从扇区0中解析得到Flash Size数据错误.");
                return null;
            }

            envAddr = (uint)(size - CrcImageHelper.ENV_ADDR_OFFSET1);

            if (envAddr > flashSize - (int)FlashOperateSize.Size4k)
            {
                LogHelper.WriteInfo("试图从小Flash {0}读取超出范围的数据 {1}！", Utils.Uint32ToHexString((uint)flashSize), Utils.Uint32ToHexString((uint)envAddr));
                return null;
            }

            byte[] envData = DSP_ReadFlash4K_V2(DspIndex, envAddr);
            if (envData == null)
            {
                LogHelper.WriteInfo("读取 DSP Flash 地址 {0} 数据错误！", Utils.Uint32ToHexString((uint)envAddr));
                return null;
            }

            if (!Utils.CheckBinaryData(envData, 0, CrcImageHelper.EnvAddrKeyword))
            {
                envAddr = (uint)(size - CrcImageHelper.ENV_ADDR_OFFSET2);

                if (envAddr > flashSize - (int)FlashOperateSize.Size4k)
                {
                    LogHelper.WriteInfo("试图从小Flash {0}读取超出范围的数据 {1}！", Utils.Uint32ToHexString((uint)flashSize), Utils.Uint32ToHexString((uint)envAddr));
                    return null;
                }

                envData = DSP_ReadFlash4K_V2(DspIndex, envAddr);

                if (envData == null)
                {
                    LogHelper.WriteInfo("读取 DSP Flash 地址 {0} 数据错误！", Utils.Uint32ToHexString((uint)envAddr));
                    return null;
                }

                if (!Utils.CheckBinaryData(envData, 0, CrcImageHelper.EnvAddrKeyword))
                {
                    LogHelper.WriteInfo("Flash数据错误，没有找到 Env 数据段");
                    return null;
                }
            }

            return envData;
        }

        private BTDeviceInfo GetBTDeviceInfoFromEnvData(byte[] envData)
        {
            Debug.Assert(envData != null && envData.Length == Size4K);

            TLV_Type tlvBTAddr = CrcImageHelper.TLV_BTDeviceAddr(envData, 0);
            if (tlvBTAddr == null)
            {
                this.OutputErrorInfoLine("从Flash Env数据中解析蓝牙设备地址数据错误！");
                return null;
            }

            TLV_Type tlvBTName = CrcImageHelper.TLV_BTDeviceName(envData, 0);
            if (tlvBTName == null)
            {
                this.OutputErrorInfoLine("从Flash Env数据中解析蓝牙设备名称数据错误！");
                return null;
            }

            BTDeviceInfo deviceInfo = new BTDeviceInfo(tlvBTAddr.value, tlvBTName.value);

            return deviceInfo;
        }

        private BTDeviceInfo DSP_ReadBTInfo(FlashSR flashSR)
        {
            uint envAddr = 0;
            byte[] envData = DSP_ReadEnvData(flashSR, out envAddr);
            if(envData == null)
            {
                LogHelper.WriteInfo("读取 DSP Env 数据错误！");
                return null;
            }

            return GetBTDeviceInfoFromEnvData(envData);
        }

        private bool DSP_LinkTobootRom_V2(out FlashSR flashSR, out string errorMsg)
        {
            System.Diagnostics.Debug.Assert(this.fuDevice != null);

            flashSR = null;
            errorMsg = string.Empty;

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetLinkToDSPBootRomCmd_V2(DspIndex, Retries, DSPResetDuration, DownloadBaudRate), PreDownloadWait);

            FirmwareUpdateProtocol.FUResponseV2 response = FirmwareUpdateProtocol.ParseResponseV2(this._recvBuffer);
            if (response == null)
            {
                errorMsg = "Failed to parse response data";
                LogHelper.WriteInfo(errorMsg);
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V2.DSPLinkToBootROM || response.payloadData.Length != sizeof(uint) * 2)
            {
                errorMsg = "Response data is incorrect";
                LogHelper.WriteInfo(errorMsg);
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);

            FirmwareUpdateProtocol.LinkToDSPBootRomStatusCodeV2 statusCode = FirmwareUpdateProtocol.GetLinkToDSPBootRomStatusCodeV2(status);
            if (statusCode != FirmwareUpdateProtocol.LinkToDSPBootRomStatusCodeV2.IAP_DSP_LINK_TO_BOOTROM_STATUS_OK)
            {
                errorMsg = string.Format("Error Code ：{0}", Utils.Uint32ToHexString(status));
                LogHelper.WriteInfo(errorMsg);
                return false;
            }

            uint flashMID = BitConverter.ToUInt32(response.payloadData, 4);

            flashSR = FlashSR.GetFlashSR(flashMID);

            if (flashSR == null)
            {
                //LogHelper.WriteDebug("BootProtectFlash()...flashSR 为空");
                errorMsg = "Failed to get DSP FlashSR";
                LogHelper.WriteInfo(errorMsg);

                return false;
            }

            LogHelper.WriteInfo("连接到DSP BootROM 成功.");

            return true;
        }

        private bool DSP_FlashCheckCRC_V1(DSPProgrammingTask dspProgrammingTask, out string errorMsg)
        {
            errorMsg = string.Empty;

            LogHelper.WriteInfo("Check CRC...地址:{0}, 长度:{1}...", Utils.Uint32ToHexString((uint)0), Utils.Uint32ToHexString(dspProgrammingTask.binSize));

            uint binCrc = 0xffffffff;

            binCrc = CrcImageHelper.CheckCRC(binCrc, dspProgrammingTask.fileBuffer, 0, (int)dspProgrammingTask.binSize);

            System.Diagnostics.Debug.Assert(this.fuDevice != null);

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPFlashCheckCRCCmd(DspIndex, 0, dspProgrammingTask.binSize), CheckFlashCRCWait);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                errorMsg = "Failed to parse response data";
                LogHelper.WriteInfo(errorMsg);
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.DSPFlashCheckCRC || response.payloadData.Length < sizeof(uint))
            {
                errorMsg = "Response data is incorrect";
                LogHelper.WriteInfo(errorMsg);
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);           
            FirmwareUpdateProtocol.DSPCheckFlashCRCStatusCode statusCode = FirmwareUpdateProtocol.GetCheckFlashCRCStatusCode(status);
            if (statusCode == FirmwareUpdateProtocol.DSPCheckFlashCRCStatusCode.IAP_DSP_CHECK_FLASH_CRC_STATUE_OK && response.payloadData.Length == sizeof(uint) *2)
            {
                uint currCRC = BitConverter.ToUInt32(response.payloadData, sizeof(uint));

                LogHelper.WriteInfo("DSP CheckCRC：{0}, targetCRC = {1}", Utils.Uint32ToHexString(currCRC), Utils.Uint32ToHexString(binCrc));

                if (binCrc != currCRC)
                {
                    errorMsg = string.Format("DSP flash executable file CRC ({0}) does not match target ({1})", Utils.Uint32ToHexString(currCRC), Utils.Uint32ToHexString(binCrc));
                }

                return binCrc == currCRC;
            }

            LogHelper.WriteInfo("DSP CheckCRC 错误：{0}！", Utils.Uint32ToHexString(status));

            return false;
        }

        private bool DSP_FlashCheckCRC_V2(DSPProgrammingTask dspProgrammingTask, out string errorMsg)
        {
            errorMsg = string.Empty;

            int binStartAddr = 0;
            int binSize = (int)dspProgrammingTask.binSize;

            uint binCrc = 0xffffffff;
            binCrc = CrcImageHelper.CheckCRC(binCrc, dspProgrammingTask.fileBuffer, binStartAddr, binSize);

            LogHelper.WriteInfo("Check bin CRC...地址:{0}, 长度:{1}...", Utils.Uint32ToHexString((uint)binStartAddr), Utils.Uint32ToHexString((uint)binSize));
            uint targetBinCrc = 0xffffffff;
            if (!GetDSPCheckFlashCRC_V2(binStartAddr, binSize, ref targetBinCrc))
            {
                errorMsg = "Failed to Get DSP flash executable file CRC";
                return false;
            }

            if (binCrc != targetBinCrc)
            {
                errorMsg = string.Format("DSP flash executable file CRC ({0}) does not match target ({1})", Utils.Uint32ToHexString(binCrc), Utils.Uint32ToHexString(targetBinCrc));
                LogHelper.WriteInfo("DSP bin CheckCRC 错误...bin CRC = {0}, target bin CRC = {1}", Utils.Uint32ToHexString(binCrc), Utils.Uint32ToHexString(targetBinCrc));
                return false;
            }

            LogHelper.WriteInfo("OK");

            if (dspProgrammingTask.wavSize > 0)
            {
                int wavStartAddr = (int)(dspProgrammingTask.envAddr - dspProgrammingTask.wavSize);
                int wavSize = (int)dspProgrammingTask.wavSize;

                uint wavCrc = 0xffffffff;
                wavCrc = CrcImageHelper.CheckCRC(wavCrc, dspProgrammingTask.fileBuffer, wavStartAddr, wavSize);

                LogHelper.WriteInfo("Check wav CRC...地址:{0}, 长度:{1}...", Utils.Uint32ToHexString((uint)wavStartAddr), Utils.Uint32ToHexString((uint)wavSize));
                uint targetWavCrc = 0xffffffff;
                if (!GetDSPCheckFlashCRC_V2(wavStartAddr, wavSize, ref targetWavCrc))
                {
                    errorMsg = "Failed to Get DSP flash wav file CRC";
                    return false;
                }

                if (wavCrc != targetWavCrc)
                {
                    errorMsg = string.Format("DSP flash wav file CRC ({0}) does not match target ({1})", Utils.Uint32ToHexString(wavCrc), Utils.Uint32ToHexString(targetWavCrc));
                    LogHelper.WriteInfo("DSP wav CheckCRC 错误...wav CRC = {0}, target wav CRC = {1}", Utils.Uint32ToHexString(wavCrc), Utils.Uint32ToHexString(targetWavCrc));
                    return false;
                }

                LogHelper.WriteInfo("OK");
            }

            int envStartAddr = (int)dspProgrammingTask.envAddr;
            int envSize = (int)FlashOperateSize.Size4k;

            if(dspProgrammingTask.envConfigData == null)
            {
                errorMsg = "DSP envConfigData is NULL!";
                return false;
            }
            
            uint envCrc = 0xffffffff;
            envCrc = CrcImageHelper.CheckCRC(envCrc, dspProgrammingTask.envConfigData, 0, dspProgrammingTask.envConfigData.Length);

            LogHelper.WriteInfo("Check env CRC...地址:{0}, 长度:{1}...", Utils.Uint32ToHexString((uint)envStartAddr), Utils.Uint32ToHexString((uint)envSize));            
            uint targetEnvCrc = 0xffffffff;
            if (!GetDSPCheckFlashCRC_V2(envStartAddr, envSize, ref targetEnvCrc))
            {
                errorMsg = "Failed to Get DSP flash env file CRC";
                return false;
            }

            if (envCrc != targetEnvCrc)
            {
                errorMsg = string.Format("DSP flash env file CRC ({0}) does not match target ({1})", Utils.Uint32ToHexString(envCrc), Utils.Uint32ToHexString(targetEnvCrc));
                LogHelper.WriteInfo("DSP env CheckCRC 错误...env CRC = {0}, target env CRC = {1}", Utils.Uint32ToHexString(envCrc), Utils.Uint32ToHexString(targetEnvCrc));
                return false;
            }

            LogHelper.WriteInfo("OK");

            return true;
        }

        private bool DSP_EndDownload_V1(DSPProgrammingTask dspProgrammingTask, out string errorMsg)
        {
            errorMsg = string.Empty;

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPEndDownloadCmd(Retries, DspIndex), EndDownloadWait);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                errorMsg = "Failed to parse response data";
                LogHelper.WriteInfo(errorMsg);
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.DSPEndDownload || response.payloadData.Length != sizeof(uint))
            {
                errorMsg = "Response data is incorrect";
                LogHelper.WriteInfo(errorMsg);
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);

            FirmwareUpdateProtocol.DSPEndDownloadStatusCode statusCode = FirmwareUpdateProtocol.GetEndDownloadStatusCode(status);
            if (statusCode != FirmwareUpdateProtocol.DSPEndDownloadStatusCode.IAP_DSP_END_DOWNLOAD_STATUS_OK)
            {
                LogHelper.WriteInfo("结束下载错误代码：{0}！", Utils.Uint32ToHexString(status));
                errorMsg = string.Format("Error Code ：{0}！", Utils.Uint32ToHexString(status));
                return false;
            }

            LogHelper.WriteInfo("结束下载成功.");

            return true;
        }

        private bool DSP_DownloadCompleted_V2(out string errorMsg)
        {
            errorMsg = string.Empty;

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPDownloadCompletedCmd_V2(Retries, DspIndex), DownloadCompletedWait);

            FirmwareUpdateProtocol.FUResponseV2 response = FirmwareUpdateProtocol.ParseResponseV2(this._recvBuffer);
            if (response == null)
            {
                errorMsg = "Failed to parse response data";
                LogHelper.WriteInfo(errorMsg);
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V2.DSPDownloadCompleted || response.payloadData.Length != sizeof(uint))
            {
                errorMsg = "Response data is incorrect";
                LogHelper.WriteInfo(errorMsg);
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);

            FirmwareUpdateProtocol.DSPDownloadCompletedStatusCodeV2 statusCode = FirmwareUpdateProtocol.GetDownloadCompletedStatusCodeV2(status);
            if (statusCode != FirmwareUpdateProtocol.DSPDownloadCompletedStatusCodeV2.IAP_DSP_DOWNLOAD_COMPLETED_STATUS_OK)
            {
                if (statusCode == FirmwareUpdateProtocol.DSPDownloadCompletedStatusCodeV2.IAP_DSP_DOWNLOAD_COMPLETED_STATUS_REBOOT_ERROR)
                {
                    if (!DSP_PowerSupplyControl(DspIndex, false))
                    {
                        return false;
                    }
                    if (!DSP_PowerSupplyControl(DspIndex, true))
                    {
                        return false;
                    }
                }
                else
                {
                    errorMsg = string.Format("Error Code ：{0}！", Utils.Uint32ToHexString(status));
                    LogHelper.WriteInfo("结束下载错误代码：{0}！", Utils.Uint32ToHexString(status));
                    return false;
                }
            }

            LogHelper.WriteInfo("完成下载成功.");

            return true;
        }

        private bool DSP_Download4K_V1(uint dspIndex, byte[] buffer, int bufferIndex, uint address)
        {
            System.Diagnostics.Debug.Assert(this.fuDevice != null);

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPDownload4kCmd(DspIndex, buffer, bufferIndex, address), WriteFlash4KWait);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.DSPDownload || response.payloadData.Length != sizeof(uint) * 1)
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            //uint responseAddress = BitConverter.ToUInt32(response.payloadData, 0);            
            //if(responseAddress != address)
            //{
            //    LogHelper.WriteInfo("下载数据响应有误！");
            //    return false;
            //}

            uint status = BitConverter.ToUInt32(response.payloadData, 0);
            FirmwareUpdateProtocol.DSPDownloadStatusCode statusCode = FirmwareUpdateProtocol.GetDownloadStatusCode(status);
            if (statusCode != FirmwareUpdateProtocol.DSPDownloadStatusCode.IAP_DSP_DOWNLOAD_STATUS_OK)
            {
                LogHelper.WriteInfo("下载错误代码：{0}！", Utils.Uint32ToHexString(status));
                return false;
            }

            LogHelper.WriteInfo("下载数据成功，地址：{0}，长度：4K！", Utils.Uint32ToHexString(address));

            return true;
        }

        private bool DSP_WriteFlash4K_V2(uint dspIndex, byte[] buffer, int bufferIndex, uint address)
        {
            System.Diagnostics.Debug.Assert(this.fuDevice != null);

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPWriteFlash4kCmdV2(DspIndex, buffer, bufferIndex, address), WriteFlash4KWait);

            FirmwareUpdateProtocol.FUResponseV2 response = FirmwareUpdateProtocol.ParseResponseV2(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V2.DSPWriteFlash4K || response.payloadData.Length != sizeof(uint) * 1)
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            //uint responseAddress = BitConverter.ToUInt32(response.payloadData, 0);            
            //if(responseAddress != address)
            //{
            //    LogHelper.WriteInfo("下载数据响应有误！");
            //    return false;
            //}

            uint status = BitConverter.ToUInt32(response.payloadData, 0);
            FirmwareUpdateProtocol.DSPWriteFlashStatusCodeV2 statusCode = FirmwareUpdateProtocol.GetWriteFlashStatusCodeV2(status);
            if (statusCode != FirmwareUpdateProtocol.DSPWriteFlashStatusCodeV2.IAP_DSP_WRITE_FLASH_STATUS_OK)
            {
                LogHelper.WriteInfo("Write Flash错误代码：{0}！", Utils.Uint32ToHexString(status));
                return false;
            }

            LogHelper.WriteInfo("Write Flash成功，地址：{0}，长度：4K！", Utils.Uint32ToHexString(address));

            return true;
        }

        //private bool DSP_Download4KToMCU(uint dspIndex, uint address, byte[] buffer)
        //{
        //    System.Diagnostics.Debug.Assert(this.fuDevice != null);

        //    this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPDownload4kToMCUCmd(DspIndex, address, buffer), WriteFlash4KWait);

        //    FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
        //    if (response == null)
        //    {
        //        LogHelper.WriteInfo("解析响应数据失败");
        //        return false;
        //    }

        //    if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.MCUExternalFlashDownload || response.payloadData.Length != sizeof(uint) * 2)
        //    {
        //        LogHelper.WriteInfo("响应数据有误！");
        //        return false;
        //    }

        //    uint responseAddress = BitConverter.ToUInt32(response.payloadData, 0);
        //    if (responseAddress != address)
        //    {
        //        LogHelper.WriteInfo("下载数据响应有误！");
        //        return false;
        //    }

        //    uint status = BitConverter.ToUInt32(response.payloadData, 4);
        //    FirmwareUpdateProtocol.ProgrammerMCUFlashDownloadStatusCode statusCode = FirmwareUpdateProtocol.GetMCUFlashDownloadStatusCode(status);
        //    if (statusCode != FirmwareUpdateProtocol.ProgrammerMCUFlashDownloadStatusCode.IAP_DSP_MCU_FLASH_DOWNLOAD_STATUS_OK)
        //    {
        //        LogHelper.WriteInfo("下载错误代码：{0}！", Utils.Uint32ToHexString(status));
        //        return false;
        //    }

        //    LogHelper.WriteInfo("下载数据成功，地址：{0}，长度：4K！", Utils.Uint32ToHexString(address));

        //    return true;
        //}

        private bool DSP_DownloadDataToMCU(uint dspIndex, byte[] buffer, uint address, int length)
        {
            System.Diagnostics.Debug.Assert(this.fuDevice != null);

            int size4KMultiple = length / Size4K;
            
            //this.fuDevice.SendDataTest(new byte[] { 0xFD, 0xCC, 0x05, 0x00, 0x30, 0xef, 0xbe, 0xad, 0xde});

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetDSPDownloadDataToMCUCmd(DspIndex, buffer, address, length), WriteFlash4KWait * 100 + (size4KMultiple - 1) * 0x200);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.MCUExternalFlashDownload || response.payloadData.Length != sizeof(uint) * 2)
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint responseAddress = BitConverter.ToUInt32(response.payloadData, 0);
            if (responseAddress != address)
            {
                LogHelper.WriteInfo("下载数据响应有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 4);
            FirmwareUpdateProtocol.ProgrammerMCUFlashDownloadStatusCode statusCode = FirmwareUpdateProtocol.GetMCUFlashDownloadStatusCode(status);
            if (statusCode != FirmwareUpdateProtocol.ProgrammerMCUFlashDownloadStatusCode.IAP_DSP_MCU_FLASH_DOWNLOAD_STATUS_OK)
            {
                LogHelper.WriteInfo("下载错误代码：{0}！", Utils.Uint32ToHexString(status));
                return false;
            }

            LogHelper.WriteInfo("下载数据成功，地址：{0}，长度：4K！", Utils.Uint32ToHexString(address));

            return true;
        }

        private bool DSP_WriteProgrammingParams(DspProgrammerDownloadTask task)
        {
            System.Diagnostics.Debug.Assert(this.fuDevice != null && task.dspFwPackage.dspFirmwareInfo != null);

            this.OutputInfoLine("写入DSP固件信息参数...");

            //task.dspProgrammerDownloadParams.crcBinFileLength = (uint)task.FileBuffer.Length;

            //int envAddr = CrcImageHelper.GetBK3288EnvAddr(task.FileBuffer);
            //TLV_Type tlvType = CrcImageHelper.TLV_FindBaseInfo(task.FileBuffer, (int)envAddr);
            //if (tlvType == null)
            //{
            //    LogHelper.WriteInfo("查找 BaseInfo 失败");
            //    this.OutputErrorInfoLine("查找 BaseInfo 失败!");
            //    return false;
            //}

            //task.dspProgrammerDownloadParams.envAddr = (uint)envAddr;

            //BinBaseInfo binBaseInfo = BinBaseInfo.ParseFromBuffer(tlvType.value);
            ////计算src bin烧录长度 4k对齐
            //task.dspProgrammerDownloadParams.crcBinSize = (uint)((int)binBaseInfo.szSrcBin + ((int)FlashOperateSize.Size4k - 1)) / (int)FlashOperateSize.Size4k * (int)FlashOperateSize.Size4k;

            //uint binCrc = 0xffffffff;
            //task.dspProgrammerDownloadParams.binCrc = CrcImageHelper.CheckCRC(binCrc, task.FileBuffer, 0, (int)task.dspProgrammerDownloadParams.crcBinSize);

            //task.dspProgrammerDownloadParams.timeStamp = GetFileModifiedTimeStamp(task.CrcImageFilePath);

            //task.dspProgrammerDownloadParams.SetKey(dspkey, 0);
            //task.dspProgrammerDownloadParams.SetKeyHash(Tools.HashHelper.SHA256(dspkey), 0);
            //task.dspProgrammerDownloadParams.efuseControl = efuseControl;

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetWriteDSPFirmwareInfoCmd(task.dspFwPackage.dspFirmwareInfo), CommWaitTimeUnit);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                this.OutputErrorInfoLine("解析响应数据失败!");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.WriteDSPFirmwareInfo || response.payloadData.Length != sizeof(uint))
            {
                LogHelper.WriteInfo("响应数据有误！");
                this.OutputErrorInfoLine("响应数据有误!");
                return false;
            }
            
            uint status = BitConverter.ToUInt32(response.payloadData, 0);
            //FirmwareUpgradeProtocol.MCUFlashDownloadStatusCode statusCode = FirmwareUpgradeProtocol.GetMCUFlashDownloadStatusCode(status);
            if (status != 0)
            {
                LogHelper.WriteInfo("设置编程参数错误代码：{0}！", Utils.Uint32ToHexString(status));
                this.OutputErrorInfoLine("错误代码：{0}！", Utils.Uint32ToHexString(status));
                return false;
            }

            LogHelper.WriteInfo("写入DSP编程下载参数成功");

            this.OutputInfo("OK.");

            return true;
        }

        private bool DSP_WriteDSPKeyHashParams()
        {
            System.Diagnostics.Debug.Assert(this.fuDevice != null);

            this.OutputInfoLine("写入DSP Key Hash参数...");

            //task.dspProgrammerDownloadParams.crcBinFileLength = (uint)task.FileBuffer.Length;

            //int envAddr = CrcImageHelper.GetBK3288EnvAddr(task.FileBuffer);
            //TLV_Type tlvType = CrcImageHelper.TLV_FindBaseInfo(task.FileBuffer, (int)envAddr);
            //if (tlvType == null)
            //{
            //    LogHelper.WriteInfo("查找 BaseInfo 失败");
            //    this.OutputErrorInfoLine("查找 BaseInfo 失败!");
            //    return false;
            //}

            //task.dspProgrammerDownloadParams.envAddr = (uint)envAddr;

            //BinBaseInfo binBaseInfo = BinBaseInfo.ParseFromBuffer(tlvType.value);
            ////计算src bin烧录长度 4k对齐
            //task.dspProgrammerDownloadParams.crcBinSize = (uint)((int)binBaseInfo.szSrcBin + ((int)FlashOperateSize.Size4k - 1)) / (int)FlashOperateSize.Size4k * (int)FlashOperateSize.Size4k;

            //uint binCrc = 0xffffffff;
            //task.dspProgrammerDownloadParams.binCrc = CrcImageHelper.CheckCRC(binCrc, task.FileBuffer, 0, (int)task.dspProgrammerDownloadParams.crcBinSize);

            //task.dspProgrammerDownloadParams.timeStamp = GetFileModifiedTimeStamp(task.CrcImageFilePath);

            //task.dspProgrammerDownloadParams.SetKey(dspkey, 0);
            //task.dspProgrammerDownloadParams.SetKeyHash(Tools.HashHelper.SHA256(dspkey), 0);
            //task.dspProgrammerDownloadParams.efuseControl = efuseControl;

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetWriteDSPKeyHashCmd(keyHash), CommWaitTimeUnit * 1000);
            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                this.OutputErrorInfoLine("解析响应数据失败!");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.WriteEfuseKeyHash || response.payloadData.Length != sizeof(uint))
            {
                LogHelper.WriteInfo("响应数据有误！");
                this.OutputErrorInfoLine("响应数据有误!");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);
            //FirmwareUpgradeProtocol.MCUFlashDownloadStatusCode statusCode = FirmwareUpgradeProtocol.GetMCUFlashDownloadStatusCode(status);
            if (status != 0)
            {
                LogHelper.WriteInfo("设置编程参数错误代码：{0}！", Utils.Uint32ToHexString(status));
                this.OutputErrorInfoLine("错误代码：{0}！", Utils.Uint32ToHexString(status));
                return false;
            }

            LogHelper.WriteInfo("写入DSP Key Hash参数成功");

            this.OutputInfo("OK.");

            return true;
        }

        //private bool MCU_PreDownload(MCUProgrammingTask mcuProgrammingTask)
        //{
        //    System.Diagnostics.Debug.Assert(this.fuDevice != null);

        //    this.fuDevice.SyncSend(FirmwareUpgradeProtocol.GetMCUPreDownloadCmd(mcuProgrammingTask.binSize), PreDownloadWait);

        //    FirmwareUpgradeProtocol.FUResponse response = FirmwareUpgradeProtocol.ParseResponse(this._recvBuffer);
        //    if (response == null)
        //    {
        //        LogHelper.WriteInfo("解析响应数据失败");
        //        return false;
        //    }

        //    if (response.cmd != FirmwareUpgradeProtocol.FUCommmand.MCUPreDownload || response.payloadData.Length != sizeof(uint))
        //    {
        //        LogHelper.WriteInfo("响应数据有误！");
        //        return false;
        //    }

        //    uint status = BitConverter.ToUInt32(response.payloadData, 0);
        //    if(status != 0)
        //    {
        //        LogHelper.WriteInfo("MCU预下载错误代码：{0}！", Utils.Uint32ToHexString(status));
        //        return false;
        //    }
        //    //FirmwareUpgradeProtocol.DSPPreDownloadStatusCode statusCode = FirmwareUpgradeProtocol.GetPreDownloadStatusCode(status);
        //    //if (statusCode != FirmwareUpgradeProtocol.DSPPreDownloadStatusCode.IAP_DSP_PRE_DOWNLOAD_STATUS_OK)
        //    //{
        //    //    LogHelper.WriteInfo("预下载错误代码：{0}！", Utils.Uint32ToHexString(status));
        //    //    return false;
        //    //}

        //    LogHelper.WriteInfo("MCU 预下载成功.");

        //    return true;
        //}

        private bool MCU_DownloadImage(MCUProgrammingTask mcuProgrammingTask)
        {
            //计算src bin烧录长度 4k对齐
            int srcBinSize = (int)mcuProgrammingTask.binSize;
            //int binStartAddr = BIN_WRITE_START_ADDRESS;
            int binStartAddr = 0x0;

            this.ShowProgress(true);

            for (int addr = binStartAddr; addr < srcBinSize; )
            {
                uint mcuAddr = (uint)addr + mcuProgrammingTask.mcuAppVectorOffset;
                LogHelper.WriteInfo("Write MCU Flash Sector...地址：{0}", Utils.Uint32ToHexString((uint)mcuAddr));
                if ((addr & ((int)FlashOperateSize.Size4k - 1)) == 0 && srcBinSize - addr >= (int)FlashOperateSize.Size4k)
                {
                    LogHelper.WriteInfo("Write MCU Flash Sector...地址:{0}，长度4k...", Utils.Uint32ToHexString((uint)mcuAddr));

                    this.OutputInfoLine("Downloading MCU firmware...address : {0}，size : 4k...", Utils.Uint32ToHexString((uint)mcuAddr));

                    byte[] data = new byte[(int)FlashOperateSize.Size4k];
                    Array.Copy(mcuProgrammingTask.fileBuffer, addr, data, 0, (int)FlashOperateSize.Size4k);

                    //uint crc = 0xffffffff;
                    //crc = CrcImageHelper.CheckCRC(crc, data, 0, Size4K);

                    //LogHelper.WriteInfo("CRC = {0}", Utils.Uint32ToHexString(crc));

                    if (!MCU_Download((uint)mcuAddr, data))
                    //if (!DSP_WriteFlash4K(DspIndex, (uint)addr, dspProgrammingTask.fileBuffer))
                    {
                        LogHelper.WriteInfo("失败！");
                        return false;
                    }

                    addr += (int)FlashOperateSize.Size4k;

                    LogHelper.WriteInfo("OK");
                }
                else if ((addr & ((int)FlashOperateSize.Size2k - 1)) == 0 && srcBinSize - addr >= (int)FlashOperateSize.Size2k)
                {
                    LogHelper.WriteInfo("Write Flash Sector...地址:{0}，长度2k...", Utils.Uint32ToHexString((uint)mcuAddr));

                    this.OutputInfoLine("Downloading MCU firmware...address : {0}，size : 2k...", Utils.Uint32ToHexString((uint)mcuAddr));

                    byte[] data = new byte[(int)FlashOperateSize.Size2k];
                    Array.Copy(mcuProgrammingTask.fileBuffer, addr, data, 0, (int)FlashOperateSize.Size2k);


                    //uint crc = 0xffffffff;
                    //crc = CrcImageHelper.CheckCRC(crc, data, 0, (int)FlashOperateSize.Size2k);

                    //LogHelper.WriteInfo("CRC = {0}", Utils.Uint32ToHexString(crc));

                    if (!MCU_Download((uint)mcuAddr, data))
                    //if (!DSP_WriteFlash4K(DspIndex, (uint)addr, dspProgrammingTask.fileBuffer))
                    {
                        LogHelper.WriteInfo("失败！");
                        return false;
                    }

                    addr += (int)FlashOperateSize.Size2k;

                    LogHelper.WriteInfo("OK");                   
                }
                else if ((addr & ((int)FlashOperateSize.Size1k - 1)) == 0 && srcBinSize - addr >= (int)FlashOperateSize.Size1k)
                {
                    LogHelper.WriteInfo("Write Flash Sector...地址:{0}，长度1k...", Utils.Uint32ToHexString((uint)mcuAddr));

                    this.OutputInfoLine("Downloading MCU firmware...address : {0}，size : 1k...", Utils.Uint32ToHexString((uint)mcuAddr));

                    byte[] data = new byte[(int)FlashOperateSize.Size1k];
                    Array.Copy(mcuProgrammingTask.fileBuffer, addr, data, 0, (int)FlashOperateSize.Size1k);


                    //uint crc = 0xffffffff;
                    //crc = CrcImageHelper.CheckCRC(crc, data, 0, (int)FlashOperateSize.Size1k);

                    //LogHelper.WriteInfo("CRC = {0}", Utils.Uint32ToHexString(crc));

                    if (!MCU_Download((uint)mcuAddr, data))
                    //if (!DSP_WriteFlash4K(DspIndex, (uint)addr, dspProgrammingTask.fileBuffer))
                    {
                        LogHelper.WriteInfo("失败！");
                        return false;
                    }

                    addr += (int)FlashOperateSize.Size1k;

                    LogHelper.WriteInfo("OK");
                }
                else
                {
                    LogHelper.WriteInfo("Bin文件Size没有1K对齐处理！");
                    this.OutputInfoLine("The MCU firmware file size is not aligned with 1k");

                    return false;
                }

                float progressRatio = (float)addr / (float)srcBinSize;

                this.UpdateProgress(progressRatio);
            }

            this.ShowProgress(false);

            return true;
        }

        private bool MCU_FlashCheckCRC(MCUProgrammingTask mcuProgrammingTask, out string errorMsg)
        {
            errorMsg = string.Empty;

            //计算src bin烧录长度 4k对齐
            int srcBinSize = (int)mcuProgrammingTask.binSize;

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetMCUFlashCheckCRCCmd(mcuProgrammingTask.mcuAppVectorOffset, mcuProgrammingTask.binSize), CheckFlashCRCWait);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                errorMsg = "Failed to parse response data! ";
                LogHelper.WriteInfo(errorMsg);
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.MCUCheckFlashCRC || response.payloadData.Length < sizeof(uint))
            {
                errorMsg = "Response data is incorrect! ";
                LogHelper.WriteInfo(errorMsg);
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);
            if (status == 0 && response.payloadData.Length == sizeof(uint) * 2)
            {
                uint currCRC = BitConverter.ToUInt32(response.payloadData, sizeof(uint));

                uint targetCRC = 0xffffffff;
                targetCRC = CrcImageHelper.CheckCRC(targetCRC, mcuProgrammingTask.fileBuffer, 0, srcBinSize);

                //LogHelper.WriteInfo("MCU CheckCRC：{0}, targetCRC = {1}", Utils.Uint32ToHexString(currCRC), Utils.Uint32ToHexString(targetCRC));

                errorMsg = string.Format("MCU CheckCRC：{0}, targetCRC = {1}", Utils.Uint32ToHexString(currCRC), Utils.Uint32ToHexString(targetCRC));

                LogHelper.WriteInfo(errorMsg);

                return currCRC == targetCRC;
            }

            LogHelper.WriteInfo("MCU CRC Check错误代码：{0}！", Utils.Uint32ToHexString(status));

            return true;
        }

        private bool MCU_BootloaderJumpToAPP(FirmwareUpdateProtocolVersion protocolVersion)
        {
            if(protocolVersion == FirmwareUpdateProtocolVersion.V1)
            {
                return MCU_BootloaderJumpToAPP_V1();
            }
            else if (protocolVersion == FirmwareUpdateProtocolVersion.V2 || protocolVersion == FirmwareUpdateProtocolVersion.V3)
            {
                return MCU_BootloaderJumpToAPP_V2();
            }
            else
            {
                LogHelper.WriteInfo("不支持此版本的 Bootloader 升级协议");
                return false;
            }
            //this.fuDevice.SyncSend(FirmwareUpdateProtocol.MCU_BootloaderJumpToAPPCmd(0x40, true), PreDownloadWait * 1);

            //FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            //if (response == null)
            //{
            //    LogHelper.WriteInfo("解析响应数据失败");
            //    return false;
            //}

            //if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.BootloaderJumpToAPP || response.payloadData.Length != sizeof(uint))
            //{
            //    LogHelper.WriteInfo("响应数据有误！");
            //    return false;
            //}

            //uint status = BitConverter.ToUInt32(response.payloadData, 0);
            //if (status != 0)
            //{
            //    LogHelper.WriteInfo("MCU Bootloader 跳转到APP错误代码：{0}！", Utils.Uint32ToHexString(status));
            //    return false;
            //}
            
            //LogHelper.WriteInfo("MCU Bootloader 跳转到APP 成功.");

            //return true;
        }

        private bool MCU_BootloaderJumpToAPP_V1()
        {
            this.fuDevice.SyncSend(FirmwareUpdateProtocol.MCU_BootloaderJumpToAPPCmd(0x40, true), CommWaitTimeUnit * 1);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.BootloaderJumpToAPP || response.payloadData.Length != sizeof(uint))
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);
            if (status != 0)
            {
                LogHelper.WriteInfo("MCU Bootloader 跳转到APP错误代码：{0}！", Utils.Uint32ToHexString(status));
                return false;
            }

            LogHelper.WriteInfo("MCU Bootloader 跳转到APP 成功.");

            return true;
        }

        private bool MCU_BootloaderJumpToAPP_V2()
        {
            this.fuDevice.SyncSend(FirmwareUpdateProtocol.MCU_BootloaderJumpToAPPCmdV2(0x10), CommWaitTimeUnit);

            FirmwareUpdateProtocol.FUResponseV2 response = FirmwareUpdateProtocol.ParseResponseV2(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V2.BootloaderJumpToAPP || response.payloadData.Length != sizeof(uint))
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);
            if (status != 0)
            {
                LogHelper.WriteInfo("MCU Bootloader 跳转到APP错误代码：{0}！", Utils.Uint32ToHexString(status));
                return false;
            }

            LogHelper.WriteInfo("MCU Bootloader 跳转到APP 成功.");

            return true;
        }

        private bool MCU_AppJumpToBootloader(uint delay)
        {
            //计算src bin烧录长度 4k对齐
            //int srcBinSize = (int)mcuProgrammingTask.binSize;

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.MCU_AppJumpToBootloaderCmd(delay), CommWaitTimeUnit * 1);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.APPJumpToBootloader || response.payloadData.Length != sizeof(uint))
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);
            if (status != 0)
            {
                LogHelper.WriteInfo("MCU APP 跳转到Bootloader错误代码：{0}！", Utils.Uint32ToHexString(status));
                return false;
            }
            //FirmwareUpgradeProtocol.DSPPreDownloadStatusCode statusCode = FirmwareUpgradeProtocol.GetPreDownloadStatusCode(status);
            //if (statusCode != FirmwareUpgradeProtocol.DSPPreDownloadStatusCode.IAP_DSP_PRE_DOWNLOAD_STATUS_OK)
            //{
            //    LogHelper.WriteInfo("预下载错误代码：{0}！", Utils.Uint32ToHexString(status));
            //    return false;
            //}

            LogHelper.WriteInfo("MCU Jump from App to Bootloader successful.");
            //this.OutputInfoLine("MCU Jump from App to Bootloader successful.");

            return true;
        }

        private byte[] MCU_BootloaderReadFlash(uint address, FlashOperateSize size)
        {
            Debug.Assert(size == FlashOperateSize.Size1k || size == FlashOperateSize.Size2k || size == FlashOperateSize.Size4k);

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.MCU_BootloaderReadFlashCmd(address, (uint)size), CommWaitTimeUnit * 4);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("Bootloader读取Flash失败");
                return null;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.BootloaderReadFlash || response.payloadData.Length < sizeof(uint))
            {
                LogHelper.WriteInfo("Bootloader读取Flash失败");
                return null;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);
            int dataLength = response.payloadData.Length - sizeof(uint);
            if (status == 0 && dataLength == (int)size)
            {
                byte[] buffer = new byte[dataLength];
                Array.Copy(response.payloadData, sizeof(uint), buffer, 0, dataLength);
                LogHelper.WriteInfo("Bootloader读取Flash成功！");
                return buffer;
            }
            //FirmwareUpgradeProtocol.DSPPreDownloadStatusCode statusCode = FirmwareUpgradeProtocol.GetPreDownloadStatusCode(status);
            //if (statusCode != FirmwareUpgradeProtocol.DSPPreDownloadStatusCode.IAP_DSP_PRE_DOWNLOAD_STATUS_OK)
            //{
            //    LogHelper.WriteInfo("预下载错误代码：{0}！", Utils.Uint32ToHexString(status));
            //    return false;
            //}

            LogHelper.WriteInfo("Bootloader读取Flash失败.");

            return null;
        }

        private bool MCU_BootloaderUpdateLCDShow(uint line, byte[] str)
        {
            this.fuDevice.SyncSend(FirmwareUpdateProtocol.MCU_BootloaderUpdateLCDShowCmd(line, str), CommWaitTimeUnit * 1);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.BootloaderUpdateLCDShow || response.payloadData.Length != sizeof(uint))
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);
            if (status != 0)
            {
                LogHelper.WriteInfo("MCU Bootloader 更新显示失败！");
                return false;
            }
            //FirmwareUpgradeProtocol.DSPPreDownloadStatusCode statusCode = FirmwareUpgradeProtocol.GetPreDownloadStatusCode(status);
            //if (statusCode != FirmwareUpgradeProtocol.DSPPreDownloadStatusCode.IAP_DSP_PRE_DOWNLOAD_STATUS_OK)
            //{
            //    LogHelper.WriteInfo("预下载错误代码：{0}！", Utils.Uint32ToHexString(status));
            //    return false;
            //}

            LogHelper.WriteInfo("MCU Bootloader 更新显示成功.");


            return true;
        }

        private bool MCU_BootloaderGetFirmwareFlag(uint firmwareType, ref bool invalid)
        {
            this.fuDevice.SyncSend(FirmwareUpdateProtocol.MCU_BootloaderGetFirmwareFlagCmd(firmwareType), CommWaitTimeUnit * 1);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.BootloaderGetFirmwareFlag || response.payloadData.Length != sizeof(uint) * 2)
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);
            if (status != 0)
            {
                LogHelper.WriteInfo("MCU Bootloader Get Firmware Flag 失败！");
                return false;
            }

            invalid = BitConverter.ToUInt32(response.payloadData, 4) != 0;
            //FirmwareUpgradeProtocol.DSPPreDownloadStatusCode statusCode = FirmwareUpgradeProtocol.GetPreDownloadStatusCode(status);
            //if (statusCode != FirmwareUpgradeProtocol.DSPPreDownloadStatusCode.IAP_DSP_PRE_DOWNLOAD_STATUS_OK)
            //{
            //    LogHelper.WriteInfo("预下载错误代码：{0}！", Utils.Uint32ToHexString(status));
            //    return false;
            //}

            LogHelper.WriteInfo("MCU Bootloader Get Firmware Flag 成功.");

            return true;
        }

        private bool MCU_BootloaderUpdateFirmwareFlag(uint firmwareType, bool invalid)
        {
            this.fuDevice.SyncSend(FirmwareUpdateProtocol.MCU_BootloaderUpdateFirmwareFlagCmd(firmwareType, invalid), CommWaitTimeUnit * 1);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.BootloaderUpdateFirmwareFlag || response.payloadData.Length != sizeof(uint) * 1)
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);
            if (status != 0)
            {
                LogHelper.WriteInfo("MCU Bootloader Update Firmware Flag 失败！");
                return false;
            }
            //FirmwareUpgradeProtocol.DSPPreDownloadStatusCode statusCode = FirmwareUpgradeProtocol.GetPreDownloadStatusCode(status);
            //if (statusCode != FirmwareUpgradeProtocol.DSPPreDownloadStatusCode.IAP_DSP_PRE_DOWNLOAD_STATUS_OK)
            //{
            //    LogHelper.WriteInfo("预下载错误代码：{0}！", Utils.Uint32ToHexString(status));
            //    return false;
            //}

            LogHelper.WriteInfo("MCU Bootloader Update Firmware Flag 成功.");

            return true;
        }

        private bool MCU_BootloaderHandshake(out uint programType, out FirmwareUpdateProtocolVersion protocolVersion)
        {
            programType = 0;
            protocolVersion = FirmwareUpdateProtocolVersion.Unknown;

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetHandshakeCmd(), 0x200);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.Handshake || response.payloadData.Length != sizeof(uint) * 3)
            {
                LogHelper.WriteInfo("MCU Bootloader 握手 失败！");
                return false;
            }

            programType = BitConverter.ToUInt32(response.payloadData, 0);
            uint bootloaderType = BitConverter.ToUInt32(response.payloadData, 4);
            uint bootloaderVersion = BitConverter.ToUInt32(response.payloadData, 8);

            if (programType == 0)
            {
                uint bootloaderMajorVersion = bootloaderVersion >> 24;
                switch(bootloaderMajorVersion)
                {
                    case 1:
                        protocolVersion = FirmwareUpdateProtocolVersion.V1;
                        break;

                    case 2:
                        protocolVersion = FirmwareUpdateProtocolVersion.V2;
                        break;

                    case 3:
                        protocolVersion = FirmwareUpdateProtocolVersion.V3;
                        break;

                    default:
                        break;
                }

                LogHelper.WriteInfo("MCU Bootloader 版本:{0}", protocolVersion);
            }

            LogHelper.WriteInfo("MCU 握手 成功. programType = {0}, type = {1}, version = {2}", Utils.Uint32ToHexString(programType), Utils.Uint32ToHexString(bootloaderType), Utils.Uint32ToHexString(bootloaderVersion));

            return true;
        }

        private bool MCU_BootloaderReadMCUModel(out string mcuModel)
        {
            mcuModel = string.Empty;
            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetLoadMCUModelCmd(), 0x200);

            FirmwareUpdateProtocol.FUResponseV2 response = FirmwareUpdateProtocol.ParseResponseV2(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V2.LoadMCUModel || response.payloadData.Length == 0)
            {
                LogHelper.WriteInfo("MCU Bootloader 加载 MCU 型号 失败！");
                return false;
            }

            mcuModel = System.Text.Encoding.UTF8.GetString(response.payloadData).Trim('\0');

            //programType = BitConverter.ToUInt32(response.payloadData, 0);
            //uint bootloaderType = BitConverter.ToUInt32(response.payloadData, 4);
            //uint bootloaderVersion = BitConverter.ToUInt32(response.payloadData, 8);

            //if (programType == 0)
            //{
            //    uint bootloaderMajorVersion = bootloaderVersion >> 24;
            //    switch (bootloaderMajorVersion)
            //    {
            //        case 1:
            //            protocolVersion = FirmwareUpdateProtocolVersion.V1;
            //            break;

            //        case 2:
            //            protocolVersion = FirmwareUpdateProtocolVersion.V2;
            //            break;

            //        default:
            //            break;
            //    }

            //    LogHelper.WriteInfo("MCU Bootloader 版本:{0}", protocolVersion);
            //}

            LogHelper.WriteInfo("MCU Model ：{0}, 长度 ：{1}", mcuModel, mcuModel.Length);
            //Log.OutputLine("MCU Model ：{0}, 长度 ：{1}", mcuModel, mcuModel.Length);

            return true;
        }

        private bool MCU_Download(uint address, byte[] data)
        {
            System.Diagnostics.Debug.Assert(this.fuDevice != null);

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetMCUDownloadCmd(address, data), CommWaitTimeUnit * 2);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                return false;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.MCUDownload || response.payloadData.Length != sizeof(uint))
            {
                LogHelper.WriteInfo("响应数据有误！");
                return false;
            }

            uint status = BitConverter.ToUInt32(response.payloadData, 0);
            if (status != 0)
            {
                LogHelper.WriteInfo("MCU下载错误代码：{0}！", Utils.Uint32ToHexString(status));
                return false;
            }
            //FirmwareUpgradeProtocol.DSPPreDownloadStatusCode statusCode = FirmwareUpgradeProtocol.GetPreDownloadStatusCode(status);
            //if (statusCode != FirmwareUpgradeProtocol.DSPPreDownloadStatusCode.IAP_DSP_PRE_DOWNLOAD_STATUS_OK)
            //{
            //    LogHelper.WriteInfo("预下载错误代码：{0}！", Utils.Uint32ToHexString(status));
            //    return false;
            //}

            LogHelper.WriteInfo("下载数据成功，地址：{0}，长度：{1}", Utils.Uint32ToHexString(address), data.Length);

            return true;
        }

        private bool MCU_BootloaderUpdateLCDShowTest()
        {
            //string line1 = "0123456789ABCDEFGHIJ";
            //string line1 = "ABCDEFGHIJ          ";
            //string line1 = "          ABCDEFGHIJ";
            string line1 = "0123456789          ";
            //string line1 =   "                    ";
            byte[] buffer1 = Encoding.ASCII.GetBytes(line1);
            byte[] lcdBuffer1 = new byte[buffer1.Length + 1];
            Array.Copy(buffer1, 0, lcdBuffer1, 0, buffer1.Length <= lcdBuffer1.Length - 1 ? buffer1.Length : lcdBuffer1.Length - 1);

            if (!MCU_BootloaderUpdateLCDShow(0, lcdBuffer1))
            {
                LogHelper.WriteInfo("更新显示屏第一行 {0} 失败！", line1);
                return false;
            }

            LogHelper.WriteInfo("更新显示屏第一行 {0} 成功！", line1);

            //string line2 = "ABCDEFGHIJ0123456789";
            //string line2 = "          0123456789";
            //string line2 = "          ABCDEFGHIJ";
            string line2 = "ABCDEFGHIJ          ";
            //string line2 =   "                    ";
            byte[] buffer2 = Encoding.ASCII.GetBytes(line2);
            byte[] lcdBuffer2 = new byte[buffer2.Length + 1];
            //byte[] lcdBuffer = new byte[FirmwareUpgradeProtocol.CHARS_NUMBER_OF_MCU_LCD_PER_LINE];
            Array.Copy(buffer2, 0, lcdBuffer2, 0, buffer2.Length <= lcdBuffer2.Length - 1 ? buffer2.Length : lcdBuffer2.Length - 1);

            if (!MCU_BootloaderUpdateLCDShow(1, lcdBuffer2))
            {
                LogHelper.WriteInfo("更新显示屏第二行 {0} 失败！", line2);
                return false;
            }

            LogHelper.WriteInfo("更新显示屏第二行 {0} 成功！", line2);

            return true;
        }

        private bool MCU_BootloaderGetFirmwareFlagTest(uint firmwareType, ref bool invalid)
        {
            //uint firmwareType = 1;
            //bool invalid = false;
            if (!MCU_BootloaderGetFirmwareFlag(firmwareType, ref invalid))
            {
                LogHelper.WriteInfo("获取固件标志 type = {0} 失败！", firmwareType, invalid);
                return false;
            }

            LogHelper.WriteInfo("获取固件标志 type = {0}, invalid = {1} 成功", firmwareType, invalid);

            return true;
        }

        private bool MCU_BootloaderUpdateFirmwareFlagTest(uint firmwareType, bool invalid)
        {
            //uint firmwareType = 1;
            //bool invalid = true;
            if (!MCU_BootloaderUpdateFirmwareFlag(firmwareType, invalid))
            {
                LogHelper.WriteInfo("更新固件标志 type = {0}, invalid = {1} 失败！", firmwareType, invalid);
                return false;
            }

            LogHelper.WriteInfo("更新固件标志 type = {0}, invalid = {1} 成功", firmwareType, invalid);

            return true;
        }

        private bool MCU_BootloaderReadFlashTest(uint mcuFlashAddress, FlashOperateSize size, out byte[] data)
        {
            //uint addressOffset = 0x00019c00;
            //uint addressOffset = 0x0000000;
            //FlashOperateSize size = FlashOperateSize.Size1k;
            //uint mcuFlashAddress = mcuProgrammingTask.mcuAppVectorOffset + addressOffset;
            data = MCU_BootloaderReadFlash(mcuFlashAddress, size);
            LogHelper.WriteInfo("Bootloader读取Flash {0}数据完成！", size);
            if (data == null)
            {
                LogHelper.WriteInfo("Bootloader读取Flash {0}数据失败！", size);
                return false;
            }

            LogHelper.WriteDebug("读取 addr = {0}, data : {1}", Utils.Uint32ToHexString(mcuFlashAddress), AEC.Core.Utils.ByteArrayToHexStr(data));

            //uint binCrc = 0xffffffff;
            //binCrc = CrcImageHelper.CheckCRC(binCrc, data, 0, (int)size);

            //uint targetCRC = 0xffffffff;
            //targetCRC = CrcImageHelper.CheckCRC(targetCRC, mcuProgrammingTask.fileBuffer, (int)addressOffset, (int)size);

            //LogHelper.WriteInfo("CRC = {0}, target CRC = {1}", Utils.Uint32ToHexString(binCrc), Utils.Uint32ToHexString(targetCRC));

            return true;
        }

        public static uint GetFileModifiedTimeStamp(string fileName)
        {
            try
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(fileName);
                if (fi.Exists)
                {
                    return AEC.Tools.TimeStampHelper.DateTimeToUint32TimeStamp(fi.LastWriteTime);
                }
            }
            catch(Exception)
            {

            }
            

            return 0;
        }

        private DSPFirmwareInfo DSP_ReadDSPFirmwareInfo()
        {
            System.Diagnostics.Debug.Assert(this.fuDevice != null);

            this.OutputInfoLine("读取DSP固件信息...");

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetReadMCUProgrammingParamsCmd(), CommWaitTimeUnit);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                this.OutputErrorInfo("解析响应数据失败!");
                return null;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.ReadDSPFirmwareInfo)
            {
                LogHelper.WriteInfo("响应数据有误！");
                this.OutputErrorInfo("响应数据有误!");
                return null;
            }

            //if (!FirmwareUpdateProtocol.ParseDSPFirmwareInfoFromBuffer(response.payloadData, downloadParams))
            //{
            //    return false;
            //}

            DSPFirmwareInfo dspFirmwareInfo = FirmwareUpdateProtocol.ParseDSPFirmwareInfoFromBuffer(response.payloadData);
            if (dspFirmwareInfo == null)
            {
                LogHelper.WriteInfo("解析数据有误！");
                this.OutputErrorInfo("解析数据有误!");
            }

            LogHelper.WriteInfo("读取DSP固件信息成功");

            this.OutputInfo("OK.");

            return dspFirmwareInfo;
        }

        private byte[] DSP_ReadDSPKeyHash()
        {
            System.Diagnostics.Debug.Assert(this.fuDevice != null);

            this.OutputInfoLine("读取DSP Key Hash...");

            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetReadDSPKeyHashCmd(), CommWaitTimeUnit);

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                this.OutputErrorInfo("解析响应数据失败!");
                return null;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.ReadEfuseKeyHash)
            {
                LogHelper.WriteInfo("响应数据有误！");
                this.OutputErrorInfo("响应数据有误!");
                return null;
            }

            //if (!FirmwareUpdateProtocol.ParseDSPFirmwareInfoFromBuffer(response.payloadData, downloadParams))
            //{
            //    return false;
            //}
            if(response.payloadData.Length != keyHash.Length)
            {
                LogHelper.WriteInfo("响应数据长度有误！");
                this.OutputErrorInfo("响应数据长度有误!");
                return null;
            }

            

            LogHelper.WriteInfo("读取DSP Key Hash成功");

            this.OutputInfo("OK.");

            return response.payloadData;
        }

        private DeviceID DSP_ReadDeviceId()
        {
            System.Diagnostics.Debug.Assert(this.fuDevice != null);

            this.OutputInfoLine("读取MCU UID...");
            
            this.fuDevice.SyncSend(FirmwareUpdateProtocol.GetReadMCUUIDCmd(), CommWaitTimeUnit);

            //Log.OutputLine("读取MCU UID...recv buffer: = {0}", AEC.Core.Utils.ByteArrayToHexStr(this._recvBuffer.ToArray()));

            FirmwareUpdateProtocol.FUResponse response = FirmwareUpdateProtocol.ParseResponse(this._recvBuffer);
            if (response == null)
            {
                LogHelper.WriteInfo("解析响应数据失败");
                this.OutputErrorInfo("解析响应数据失败!");
                return null;
            }

            if (response.cmd != FirmwareUpdateProtocol.FUCommmand_V1.ReadMCUUID)
            {
                LogHelper.WriteInfo("响应数据有误！");
                this.OutputErrorInfo("响应数据有误!");
                return null;
            }

            DeviceID deviceId = FirmwareUpdateProtocol.ParseMCUUIDDataFromBuffer(response.payloadData);

            if (deviceId == null)
            {
                LogHelper.WriteInfo("响应数据长度有误！");
                this.OutputErrorInfo("响应数据长度有误!");
                return null;
            }

            LogHelper.WriteInfo("读取读取 MCU UID 成功");

            this.OutputInfo("OK. MCU UID = {0}.", deviceId.ToString());

            return deviceId;
        }

        private void ExportVKxFlashData()
        {
            VKxMCUFlashData vkxFlashData = new VKxMCUFlashData();

            byte[] vk_flash_buffer = new byte[VKxMCUFlashData.FLASH_END_ADDR - VKxMCUFlashData.PRESET_DATA_START_ADDR];
            uint curr_address = VKxMCUFlashData.PRESET_DATA_START_ADDR;
            for (uint addr = VKxMCUFlashData.PRESET_DATA_START_ADDR; addr < VKxMCUFlashData.FLASH_END_ADDR; )
            {
                byte[] temp_buffer = null;

                curr_address = addr;

                if (VKxMCUFlashData.FLASH_END_ADDR - addr >= (uint)FlashOperateSize.Size4k)
                {
                    if (!MCU_BootloaderReadFlashTest(addr, FlashOperateSize.Size4k, out temp_buffer))
                    {
                        return;
                    }

                    addr += (uint)FlashOperateSize.Size4k;
                }
                else if (VKxMCUFlashData.FLASH_END_ADDR - addr >= (uint)FlashOperateSize.Size2k)
                {
                    if (!MCU_BootloaderReadFlashTest(addr, FlashOperateSize.Size2k, out temp_buffer))
                    {
                        return;
                    }

                    addr += (uint)FlashOperateSize.Size2k;
                }
                else if (VKxMCUFlashData.FLASH_END_ADDR - addr >= (uint)FlashOperateSize.Size1k)
                {
                    if (!MCU_BootloaderReadFlashTest(addr, FlashOperateSize.Size1k, out temp_buffer))
                    {
                        return;
                    }

                    addr += (uint)FlashOperateSize.Size1k;
                }
                else
                {
                    LogHelper.WriteInfo("无法处理尾部数据 address = {0} size = {0}！", Utils.Uint32ToHexString(addr), Utils.Uint32ToHexString(VKxMCUFlashData.FLASH_END_ADDR - addr));
                    return;
                }

                Array.Copy(temp_buffer, 0, vk_flash_buffer, (int)(curr_address - VKxMCUFlashData.PRESET_DATA_START_ADDR), temp_buffer.Length);
            }

            Array.Copy(vk_flash_buffer, 0, vkxFlashData.presetsData.data, 0, vkxFlashData.presetsData.data.Length);
            Array.Copy(vk_flash_buffer, (int)(VKxMCUFlashData.SYSTEM_COMMON_DATA_START_ADDR - VKxMCUFlashData.PRESET_DATA_START_ADDR), vkxFlashData.systemCommonData.data, 0, vkxFlashData.systemCommonData.data.Length);
            Array.Copy(vk_flash_buffer, (int)(VKxMCUFlashData.DEVICE_CUSTOM_MODULE_START_ADDR - VKxMCUFlashData.PRESET_DATA_START_ADDR), vkxFlashData.deviceCustomData.data, 0, vkxFlashData.deviceCustomData.data.Length);
            Array.Copy(vk_flash_buffer, (int)(VKxMCUFlashData.DEVICE_INFO_DATA_START_ADDR - VKxMCUFlashData.PRESET_DATA_START_ADDR), vkxFlashData.deviceInfoData.data, 0, vkxFlashData.deviceInfoData.data.Length);

            VKxMCUFlashDataSeralizer.SeralizeToFile(vkxFlashData, ByteArrayEncoding.Encoding.Hex, "VKxFlashData_Hex.xml");
        }

        private bool SaveFlashData(DataBlock dataBlcok)
        {
            if(dataBlcok.address % Size1K != 0 ||
                dataBlcok.size % Size1K != 0)
            {
                LogHelper.WriteInfo("dataBlcok 地址数据或长度数据无效");
                this.OutputInfoLine("MCU flash data blcok address or size is invalid...");
                return false;
            }

            this.ShowProgress(true);

            uint writeDataSize = dataBlcok.endAddress - dataBlcok.address;
            uint writenLength = 0;

            for (uint addr = dataBlcok.address; addr < dataBlcok.endAddress; )
            {
                LogHelper.WriteInfo("Write MCU Flash Sector...地址：{0}", Utils.Uint32ToHexString((uint)addr));
                if ((addr & ((int)FlashOperateSize.Size4k - 1)) == 0 && dataBlcok.endAddress - addr >= (int)FlashOperateSize.Size4k)
                {
                    LogHelper.WriteInfo("Write MCU Flash Sector...地址:{0}，长度4k...", Utils.Uint32ToHexString((uint)addr));

                    this.OutputInfoLine("Updating MCU Flash...address : {0}, size : 4k...", Utils.Uint32ToHexString((uint)addr));

                    byte[] data = new byte[(int)FlashOperateSize.Size4k];
                    Array.Copy(dataBlcok.data, (int)(addr - dataBlcok.address), data, 0, (int)FlashOperateSize.Size4k);

                    if (!MCU_Download(addr, data))
                    //if (!DSP_WriteFlash4K(DspIndex, (uint)addr, dspProgrammingTask.fileBuffer))
                    {
                        LogHelper.WriteInfo("失败！");
                        this.OutputInfoLine("Failed to update MCU Flash...address : {0}, size : 4k...", Utils.Uint32ToHexString((uint)addr));
                        return false;
                    }

                    addr += (int)FlashOperateSize.Size4k;
                    writenLength += (uint)FlashOperateSize.Size4k;

                    LogHelper.WriteInfo("OK");
                }
                else if ((addr & ((int)FlashOperateSize.Size2k - 1)) == 0 && dataBlcok.endAddress - addr >= (int)FlashOperateSize.Size2k)
                {
                    LogHelper.WriteInfo("Write Flash Sector...地址:{0}，长度2k...", Utils.Uint32ToHexString((uint)addr));
                    this.OutputInfoLine("Updating MCU Flash...address : {0}, size : 2k...", Utils.Uint32ToHexString((uint)addr));

                    byte[] data = new byte[(int)FlashOperateSize.Size2k];
                    Array.Copy(dataBlcok.data, (int)(addr - dataBlcok.address), data, 0, (int)FlashOperateSize.Size2k);

                    if (!MCU_Download(addr, data))
                    {
                        LogHelper.WriteInfo("失败！");
                        this.OutputInfoLine("Failed to update MCU Flash...address : {0}, size : 2k...", Utils.Uint32ToHexString((uint)addr));
                        return false;
                    }

                    addr += (int)FlashOperateSize.Size2k;
                    writenLength += (uint)FlashOperateSize.Size2k;

                    LogHelper.WriteInfo("OK");
                }
                else if ((addr & ((int)FlashOperateSize.Size1k - 1)) == 0 && dataBlcok.endAddress - addr >= (int)FlashOperateSize.Size1k)
                {
                    LogHelper.WriteInfo("Write Flash Sector...地址:{0}，长度1k...", Utils.Uint32ToHexString((uint)addr));
                    this.OutputInfoLine("Updating MCU Flash...address : {0}, size : 1k...", Utils.Uint32ToHexString((uint)addr));

                    byte[] data = new byte[(int)FlashOperateSize.Size1k];
                    Array.Copy(dataBlcok.data, (int)(addr - dataBlcok.address), data, 0, (int)FlashOperateSize.Size1k);

                    if (!MCU_Download(addr, data))
                    //if (!DSP_WriteFlash4K(DspIndex, (uint)addr, dspProgrammingTask.fileBuffer))
                    {
                        LogHelper.WriteInfo("失败！");
                        this.OutputInfoLine("Failed to update MCU Flash...address : {0}, size : 1k...", Utils.Uint32ToHexString((uint)addr));
                        return false;
                    }

                    addr += (int)FlashOperateSize.Size1k;
                    writenLength += (uint)FlashOperateSize.Size1k;

                    LogHelper.WriteInfo("OK");
                }
                else
                {
                    LogHelper.WriteInfo("Bin文件Size没有1K对齐处理！");
                    return false;
                }

                this.UpdateProgress((float)writenLength / (float)writeDataSize);
            }

            this.ShowProgress(false);

            return true;
        }

        private DataBlock LoadFlashData(MCUFlashDataLoadTask task)
        {
            byte[] flash_buffer = new byte[task.dataSize];
            uint curr_address = task.startAddress;
            for (uint addr = task.startAddress; addr < task.endAddress; )
            {
                byte[] temp_buffer = null;

                curr_address = addr;

                if (task.endAddress - addr >= (uint)FlashOperateSize.Size4k)
                {
                    if (!MCU_BootloaderReadFlashTest(addr, FlashOperateSize.Size4k, out temp_buffer))
                    {
                        return null;
                    }

                    addr += (uint)FlashOperateSize.Size4k;
                }
                else if (task.endAddress - addr >= (uint)FlashOperateSize.Size2k)
                {
                    if (!MCU_BootloaderReadFlashTest(addr, FlashOperateSize.Size2k, out temp_buffer))
                    {
                        return null;
                    }

                    addr += (uint)FlashOperateSize.Size2k;
                }
                else if (task.endAddress - addr >= (uint)FlashOperateSize.Size1k)
                {
                    if (!MCU_BootloaderReadFlashTest(addr, FlashOperateSize.Size1k, out temp_buffer))
                    {
                        return null;
                    }

                    addr += (uint)FlashOperateSize.Size1k;
                }
                else
                {
                    LogHelper.WriteInfo("无法处理尾部数据 address = {0} size = {0}！", Utils.Uint32ToHexString(addr), Utils.Uint32ToHexString(VKxSMCUFlashData.FLASH_END_ADDR - addr));
                    return null;
                }

                Array.Copy(temp_buffer, 0, flash_buffer, (int)(curr_address - task.startAddress), temp_buffer.Length);
            }

            return new DataBlock(task.startAddress, flash_buffer);
        }

        private void ExportVKxSFlashData()
        {
            VKxSMCUFlashData vkxsFlashData = new VKxSMCUFlashData();

            byte[] flash_buffer = new byte[VKxSMCUFlashData.FLASH_END_ADDR - VKxSMCUFlashData.X5_PRESET_DATA_START_ADDR];
            uint curr_address = VKxSMCUFlashData.X5_PRESET_DATA_START_ADDR;
            for (uint addr = VKxSMCUFlashData.X5_PRESET_DATA_START_ADDR; addr < VKxSMCUFlashData.FLASH_END_ADDR; )
            {
                byte[] temp_buffer = null;

                curr_address = addr;

                if (VKxSMCUFlashData.FLASH_END_ADDR - addr >= (uint)FlashOperateSize.Size4k)
                {
                    if (!MCU_BootloaderReadFlashTest(addr, FlashOperateSize.Size4k, out temp_buffer))
                    {
                        return;
                    }

                    addr += (uint)FlashOperateSize.Size4k;
                }
                else if (VKxSMCUFlashData.FLASH_END_ADDR - addr >= (uint)FlashOperateSize.Size2k)
                {
                    if (!MCU_BootloaderReadFlashTest(addr, FlashOperateSize.Size2k, out temp_buffer))
                    {
                        return;
                    }

                    addr += (uint)FlashOperateSize.Size2k;
                }
                else if (VKxSMCUFlashData.FLASH_END_ADDR - addr >= (uint)FlashOperateSize.Size1k)
                {
                    if (!MCU_BootloaderReadFlashTest(addr, FlashOperateSize.Size1k, out temp_buffer))
                    {
                        return;
                    }

                    addr += (uint)FlashOperateSize.Size1k;
                }
                else
                {
                    LogHelper.WriteInfo("无法处理尾部数据 address = {0} size = {0}！", Utils.Uint32ToHexString(addr), Utils.Uint32ToHexString(VKxSMCUFlashData.FLASH_END_ADDR - addr));
                    return;
                }

                Array.Copy(temp_buffer, 0, flash_buffer, (int)(curr_address - VKxSMCUFlashData.X5_PRESET_DATA_START_ADDR), temp_buffer.Length);
            }

            Array.Copy(flash_buffer, 0, vkxsFlashData.x5_resetsData.data, 0, vkxsFlashData.x5_resetsData.data.Length);
            Array.Copy(flash_buffer, (int)(VKxSMCUFlashData.VKS_PRESET_DATA_START_ADDR - VKxSMCUFlashData.X5_PRESET_DATA_START_ADDR), vkxsFlashData.presetsData.data, 0, vkxsFlashData.presetsData.data.Length);
            Array.Copy(flash_buffer, (int)(VKxSMCUFlashData.SYSTEM_COMMON_DATA_START_ADDR - VKxSMCUFlashData.X5_PRESET_DATA_START_ADDR), vkxsFlashData.systemCommonData.data, 0, vkxsFlashData.systemCommonData.data.Length);
            Array.Copy(flash_buffer, (int)(VKxSMCUFlashData.DEVICE_INFO_DATA_START_ADDR - VKxSMCUFlashData.X5_PRESET_DATA_START_ADDR), vkxsFlashData.deviceInfoData.data, 0, vkxsFlashData.deviceInfoData.data.Length);

            VKxSMCUFlashDataSeralizer.SeralizeToFile(vkxsFlashData, ByteArrayEncoding.Encoding.Hex, "VKxSFlashData_Hex.xml");
        }

        private void CloseCommDevice()
        {
            if (this.fuDevice != null)
            {
                LogHelper.WriteInfo("关闭通讯端口.");
                this.fuDevice.Close();
                this.fuDevice = null;
            }
        }

        private bool rs232BaudRateIsDefault = true;

        private bool InitPort(CommPort commPort)
        {
            System.Diagnostics.Debug.Assert(commPort != null);
            if(commPort.type == CommPortType.USB)
            {
                HIDCommPort hidCommport = commPort as HIDCommPort;
                FUHIDDevice hidDevice = new FUHIDDevice(FirmwareUpdateProtocol.CheckResponseIsCompleted, _recvBuffer);
                hidDevice.devicePath = hidCommport.devicePath;

                if (!hidDevice.Open())
                {
                    LogHelper.WriteInfo("Faild to initialize {0} !", hidCommport.displayName);
                    this.OutputInfoLineReturn("Faild to init {0} !", hidCommport.displayName);

                    return false;
                }

                this.fuDevice = hidDevice;
            }
            else if(commPort.type == CommPortType.RS232)
            {
                RS232CommPort rs232CommPort = commPort as RS232CommPort;
                FUSerialPortDevice spDevice = new FUSerialPortDevice(FirmwareUpdateProtocol.CheckResponseIsCompleted, _recvBuffer);
                spDevice.portName = rs232CommPort.portName;
                spDevice.BaudRate = GetRS232BaudRate(rs232CommPort, rs232BaudRateIsDefault);// rs232CommPort.baudRate;

                LogHelper.WriteDebug("SerialPort {0} BaudRate = {1}", spDevice.portName, spDevice.BaudRate);

                if (!spDevice.Open())
                {
                    LogHelper.WriteInfo("Faild to initialize {0} !", rs232CommPort.displayName);
                    this.OutputInfoLineReturn("Faild to init {0} !", rs232CommPort.displayName);
                    return false;
                }

                this.fuDevice = spDevice;
            }
            else
            {
                LogHelper.WriteInfo("不支持的设备端口：{0}！", commPort.type);

                return false;
            }

            //this.OutputInfoLine("Initialize {0} port successful.", commPort.displayName);
            
            return true;
        }

        private void ShowProgress(bool showProgress)
        {
            this.eventBus.NotifyObeserver(new FirmwareUpdateProgressArgs(showProgress));
        }

        private void UpdateProgress(float progressRatio)
        {
            this.eventBus.NotifyObeserver(new FirmwareUpdateProgressArgs(progressRatio));
        }

        protected void OutputInfo(string format, params object[] arg)
        {
            string info = string.Format(format, arg);
            this.eventBus.NotifyObeserver(new FirmwareUpdateOutputArgs(info, FirmwareUpdateOutputArgs.LineControl.None, false));
        }

        protected void OutputInfoLine(string format, params object[] arg)
        {
            string info = string.Format(format, arg);
            this.eventBus.NotifyObeserver(new FirmwareUpdateOutputArgs(info, FirmwareUpdateOutputArgs.LineControl.NewLine, false));
        }

        protected void OutputInfoLineReturn(string format, params object[] arg)
        {
            string info = string.Format(format, arg);
            this.eventBus.NotifyObeserver(new FirmwareUpdateOutputArgs(info, FirmwareUpdateOutputArgs.LineControl.Return, false));
        }

        protected void OutputErrorInfo(string format, params object[] arg)
        {
            string info = string.Format(format, arg);
            this.eventBus.NotifyObeserver(new FirmwareUpdateOutputArgs(info, FirmwareUpdateOutputArgs.LineControl.None, true));
        }

        protected void OutputErrorInfoLine(string format, params object[] arg)
        {
            string info = string.Format(format, arg);
            this.eventBus.NotifyObeserver(new FirmwareUpdateOutputArgs(info, FirmwareUpdateOutputArgs.LineControl.NewLine, true));
        }

        public static int GetRS232BaudRate(RS232CommPort rs232CommPort, bool rs232BaudRateIsDefault)
        {
            Debug.Assert(rs232CommPort != null);

            if(rs232CommPort.isAutoBaudRate)
            {
                return rs232BaudRateIsDefault ? RS232BaudRateDefault : RS232BaudRateSecondary;
            }
            else
            {
                return rs232CommPort.baudRate;
            }
        }
    }
}
