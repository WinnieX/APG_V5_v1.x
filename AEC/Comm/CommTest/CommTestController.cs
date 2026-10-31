using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using AEC.Core;
using AEC.Comm.CommDevices;
using AEC.Comm.CommEventArgs;

namespace AEC.Comm.CommTest
{
    public class CommTestController
    {
        public enum CommProtocolVersion
        {
            Default,
            Secondary,
        };

        public const int BaudRate_Default = SerialPort.defaultBaudRate;
        public const int BaudRate_Secondary = SerialPort.secondaryBaudRate;

        private const string vkx = "VKx";
        private const string vkxS = "VKxS";

        private const int CommTimeOut = 0x400;

        private const int HandshakeRetries = 2;

        //protected CommDevice commDevice;
        private readonly System.ComponentModel.BackgroundWorker bgwComm;
        public readonly MEventBus eventBus = new MEventBus();

        private readonly List<byte> _recvBuffer = new List<byte>();

        public delegate bool ParseDeviceModelCodeCallback(byte[] buffer, out uint deviceModel);

        private static readonly CommProtocolDetector defaultProtocolDetector = new CommProtocolDetector(CommProtocolBase.CheckCommCmdType, CommProtocolBase.CheckReadResponseIsCompleted, CommProtocolBase.CheckWriteResponseIsOk);
        private static readonly CommProtocolDetector secondaryCommProtocolDetector = new CommProtocolDetector(CommProtocolVKx.CheckCommCmdType, CommProtocolVKx.CheckReadResponseIsCompleted, CommProtocolVKx.CheckWriteResponseIsOk);

        private static readonly byte[] defaultHandshakeCmd = CommProtocolBase.GetHandshakeCmd();
        private static readonly byte[] defaultOfflineCmd = CommProtocolBase.GetOfflineCmd();
        private static readonly byte[] defaultReadDeviceModelCmd = CommProtocolBase.GetLoadDeviceModelCodeCmd();

        private static readonly byte[] secondaryHandshakeCmd = CommProtocolVKx.handshakeCmd;
        private static readonly byte[] secondaryOfflineCmd = CommProtocolVKx.offlineCmd;
        private static readonly byte[] secondaryLoadDeviceModelCmd = CommProtocolVKx.loadDeviceModelCmd;

        private static readonly ParseDeviceModelCodeCallback defaultParseDeviceModelHandler = CommProtocolBase.ParseDeviceModelCode;
        private static readonly ParseDeviceModelCodeCallback secondaryParseDeviceModelHandler = CommProtocolVKx.ParseDeviceModelCode;

        private readonly static Dictionary<uint, string> vkxModelMap = new Dictionary<uint, string>()
        {
            {0x01, "VHi"},
            {0x03, "V5"},
            {0x10, "M5Pro"},
            {0x11, "A100"},
            {0x20, "E300"},
            {0x30, "VK5"},
            {0x40, "VK3"},
            {0x50, "VK5 Lite"},
            {0x60, "VK3 Lite"},
        };

        //VKxS
        //FC 4D 06 00 A7 00 30 11 00 A9
        //FC 4D 04 00 A0 00 50 A1
        //FC 4D 06 00 A7 00 30 AA 00 30


        //VKx
        //FC 31 06 A7 0A 30 11 00 DF
        //FC 31 04 A0 0A 50 C7
        //FC 31 06 A7 0A 30 AA 00 B6 

        private CommDevice commDevice = null;

        public CommTestController()
        {
            this.bgwComm = new BackgroundWorker();
            this.bgwComm.DoWork += bgwComm_DoWork;
            this.bgwComm.RunWorkerCompleted += bgwComm_RunWorkerCompleted;
        }

        public bool RunTest(CommInterfaceBase commInterface)
        {
            if(this.bgwComm.IsBusy)
            {
                return false;
            }

            this.bgwComm.RunWorkerAsync(commInterface);

            return true;
        }

        private void bgwComm_DoWork(object sender, DoWorkEventArgs e)
        {
            CommInterfaceBase commInterface = e.Argument as CommInterfaceBase;

            e.Result = this.RunCommTest(commInterface);
        }

        private bool RunCommTest(CommInterfaceBase commInterface)
        {
            try
            {
                this.OutputInfo("Comm Test Start.");

                if (commInterface != null)
                {
                    CommProtocolVersion firstProtocolVersion = CommProtocolVersion.Default;
                    bool tryNextCommProtocol = true;
                    if (commInterface.commInterfaceType == CommInterfaceType.RS232)
                    {
                        tryNextCommProtocol = false;
                        RS232CommInterface rs232CommInterface = commInterface as RS232CommInterface;
                        if (rs232CommInterface.baudRate != BaudRate_Default)
                        {
                            firstProtocolVersion = CommProtocolVersion.Secondary;
                        }
                    }

                    if (CreateDeviceAndCommTest(commInterface, firstProtocolVersion))
                    {
                        return true;
                    }

                    if (tryNextCommProtocol)
                    {
                        //System.Threading.Thread.Sleep(0x800);
                        if(CreateDeviceAndCommTest(commInterface, CommProtocolVersion.Secondary))
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    CommProtocolVersion[] protocols = new CommProtocolVersion[] { CommProtocolVersion.Default, CommProtocolVersion.Secondary };

                    foreach (CommProtocolVersion protocol in protocols)
                    {
                        //尝试USB
                        AEC.Comm.CommDevices.HID.HIDDevice.InterfaceDetails[] hidInterfaces = AEC.Comm.CommDevices.HIDDevice.GetValidDevices();
                        foreach (var hid in hidInterfaces)
                        {
                            HIDDevice hidDevice = new HIDDevice(GetCommProtocolDetector(protocol), this._recvBuffer);
                            hidDevice.devicePath = hid.devicePath;
                            hidDevice.serialNumber = hid.serialNumber;

                            this.OutputInfoLine("Crate Device : {0}, Protocol = {1}...Success.", hidDevice.connectionInfo, GetCommProtocolName(protocol));

                            commDevice = hidDevice;

                            if (CommTest(commDevice, protocol))
                            {
                                return true;
                            }
                        }

                        //然后尝试RS232
                        string[] ports = System.IO.Ports.SerialPort.GetPortNames();
                        foreach (var port in ports)
                        {
                            SerialPort sp = new SerialPort(GetCommProtocolDetector(protocol), this._recvBuffer);
                            sp.portName = port;
                            sp.buadRate = protocol == CommProtocolVersion.Default ? BaudRate_Default : BaudRate_Secondary;

                            this.OutputInfoLine("Crate Device : {0}, Protocol = {1}...Success.", sp.connectionInfo, GetCommProtocolName(protocol));

                            commDevice = sp;

                            if (CommTest(commDevice, protocol))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.OutputInfoLine("RunCommTest()....Exception:{0}", ex.Message);
            }
            finally
            {
                if (commDevice != null)
                {
                    commDevice.Close();
                }
            }

            return false;
        }

        private bool CreateDeviceAndCommTest(CommInterfaceBase commInterface, CommProtocolVersion protocolVersion)
        {
            commDevice = CreateCommDevice(commInterface, protocolVersion);

            if (commDevice == null)
            {
                return false;
            }

            return CommTest(commDevice, protocolVersion);
        }

        private bool CommTest(CommDevice commDevice, CommProtocolVersion protocolVersion)
        {
            Debug.Assert(commDevice != null);

            try
            {
                if (!commDevice.Open())
                {
                    this.OutputInfoLine("Init Device Failed.");
                    return false;
                }
                this.OutputInfoLine("Init Device Success.");

                this._recvBuffer.Clear();

                //System.Threading.Thread.Sleep(0x100);

                byte[] handshakeCmd = GetHandshakeCmd(protocolVersion);

                bool commIsOk = false;
                for (int i = 0; i < HandshakeRetries; ++i)
                {
                    this.OutputInfoLine("Send Handshke Request : {0}", AEC.Core.Utils.ByteArrayToHexStr(handshakeCmd));
                    commDevice.SyncSend(handshakeCmd, CommTimeOut);

                    //System.Threading.Thread.Sleep(0x100);
                    
                    this.OutputRecvResponseData();

                    commIsOk = commDevice.detector.checkWriteResponseIsCompleted(this._recvBuffer);
              
                    if(commIsOk)
                    {
                        break;
                    }

                    this.OutputInfoLine("Check Handshke Response Failed!");
                }

                if(!commIsOk)
                {
                    this.OutputInfoLine("Handshake Failed.");
                    return false;
                }               
                this.OutputInfoLine("Handshake Success.");

                //this.OutputInfoLine("Send Handshke Request : {0}", AEC.Core.Utils.ByteArrayToHexStr(handshakeCmd));
                //commDevice.SyncSend(handshakeCmd, CommTimeOut);
                //this.OutputRecvResponseData();
                //if (!commDevice.detector.checkWriteResponseIsCompleted(this._recvBuffer))
                //{
                //    this.OutputInfoLine("Handshake Failed.");
                //    return false;
                //}
                //this.OutputInfoLine("Handshake Success.");

                byte[] loadDeviceModelCmd = GetLoadModelCmd(protocolVersion);
                this.OutputInfoLine("Send Load Model Code Request : {0}", AEC.Core.Utils.ByteArrayToHexStr(loadDeviceModelCmd));
                commDevice.SyncSend(loadDeviceModelCmd, CommTimeOut);
                this.OutputRecvResponseData();
                if (!commDevice.detector.checkReadResponseIsCompleted(this._recvBuffer))
                {
                    this.OutputInfoLine("Load Model Code Failed.");
                    return false;
                }

                uint deviceModelCode;
                if (!ParseDeviceModelCode(protocolVersion, this._recvBuffer.ToArray(), out deviceModelCode))
                {
                    this.OutputInfoLine("Parse Model Code Data Failed.");
                    return false;
                }
                this.OutputInfoLine("Parse Model Code : {0}...Success.", AEC.Core.Utils.Uint32ToHexStr(deviceModelCode));

                byte[] offlineCmd = GetOfflineCmd(protocolVersion);
                this.OutputInfoLine("Send Offline Request : {0}", Utils.ByteArrayToHexStr(offlineCmd));
                commDevice.SyncSend(offlineCmd, CommTimeOut);
                this.OutputRecvResponseData();
                if (!commDevice.detector.checkWriteResponseIsCompleted(this._recvBuffer))
                {
                    this.OutputInfoLine("Offline Failed.");
                    return false;
                }
                this.OutputInfoLine("Offline Success.");

                this.UpdateDeviceModelResult(protocolVersion, deviceModelCode);

                return true;
            }
            catch(Exception e)
            {
                Tools.LogHelper.WriteDebug("CommTest()...异常:", e.Message);
            }
            finally
            {
                commDevice.Close();

                this.OutputInfoLine("Close Device.");
            }

            return false;
        }

        private void UpdateDeviceModelResult(CommProtocolVersion protocolVersion, uint deviceModelCode)
        {
            string model = "X";
            string modelCode = AEC.Core.Utils.Uint32ToShortHexStr(deviceModelCode);
            if(protocolVersion == CommProtocolVersion.Default)
            {
                if(CommProtocolBase.DefaultSystemModuleIndex == 0)
                {
                    model = vkxS;                    
                }
                else
                {
                    if(vkxModelMap.ContainsKey(deviceModelCode))
                    {
                        model = vkxModelMap[deviceModelCode];
                    }
                    else
                    {
                        model = vkx;
                    }
                }
            }
            else
            {
                if(CommProtocolBase.DefaultSystemModuleIndex == 0)
                {
                   if(vkxModelMap.ContainsKey(deviceModelCode))
                    {
                        model = vkxModelMap[deviceModelCode];
                    }
                    else
                    {
                        model = vkx;
                    }                 
                }
                else
                {
                    model = vkxS;
                }
            }

            DeviceModelLoadedEventArgs args = new DeviceModelLoadedEventArgs(model, modelCode);
            //DeviceModelLoadedEventArgs args = new DeviceModelLoadedEventArgs("VHi Pro", "0xAAABCDEF");
            this.eventBus.NotifyObeserver(args);
        }

        private void OutputRecvResponseData()
        {
            if (this._recvBuffer.Count > 0)
            {
                this.OutputInfoLine("Received response : {0}", AEC.Core.Utils.ByteArrayToHexStr(this._recvBuffer.ToArray()));
            }
        }

        private CommDevice CreateCommDevice(CommInterfaceBase commInterface, CommProtocolVersion protocolVersion)
        {
            Debug.Assert(commInterface != null);

            if (commInterface.commInterfaceType == CommInterfaceType.USB)
            {
                USBCommInterface usbCommInterface = commInterface as USBCommInterface;

                AEC.Comm.CommDevices.HID.HIDDevice.InterfaceDetails[] hidInterfaces = AEC.Comm.CommDevices.HIDDevice.GetValidDevices();

                foreach (var hid in hidInterfaces)
                {
                    if (HIDDevice.GetUSBHIDPortName(hid.serialNumber) == usbCommInterface.portName)
                    {
                        HIDDevice hidDevice = new HIDDevice(GetCommProtocolDetector(protocolVersion), this._recvBuffer);
                        hidDevice.devicePath = hid.devicePath;
                        hidDevice.serialNumber = hid.serialNumber;

                        this.OutputInfoLine("Crate Device : {0}, Protocol = {1}.", hidDevice.connectionInfo, GetCommProtocolName(protocolVersion));

                        return hidDevice;

                    }
                }

                this.OutputInfoLine("Crate Device : {0}...Failed!", usbCommInterface.portName);
                this.eventBus.NotifyObeserver(new CommUSBInterfaceNotFoundArgs());
            }
            else if (commInterface.commInterfaceType == CommInterfaceType.RS232)
            {
                RS232CommInterface rs232CommInterface = commInterface as RS232CommInterface;

                string[] ports = System.IO.Ports.SerialPort.GetPortNames();
                foreach (var port in ports)
                {
                    if (rs232CommInterface.portName == port)
                    {
                        SerialPort sp = new SerialPort(GetCommProtocolDetector(protocolVersion), this._recvBuffer);
                        sp.portName = port;
                        sp.buadRate = rs232CommInterface.baudRate;

                        this.OutputInfoLine("Crate Device : {0}, Protocol = {1}...Success.", sp.connectionInfo, GetCommProtocolName(protocolVersion));

                        return sp;
                    }
                }
            }
            else if (commInterface.commInterfaceType == CommInterfaceType.UDP)
            {
                NetWrokCommInterface netWorkCommInterface = commInterface as NetWrokCommInterface;

                UDPDevice udpDevice = new UDPDevice(GetCommProtocolDetector(protocolVersion), this._recvBuffer);
                udpDevice.UpdateRemoteEP(netWorkCommInterface.ipAddress, netWorkCommInterface.remotePort);

                this.OutputInfoLine("Crate Device : {0}, Protocol = {1}...Success.", udpDevice.connectionInfo, GetCommProtocolName(protocolVersion));

                return udpDevice;
            }
            else if (commInterface.commInterfaceType == CommInterfaceType.TCP)
            {
                NetWrokCommInterface netWorkCommInterface = commInterface as NetWrokCommInterface;

                TCPDevice tcpDevice = new TCPDevice(GetCommProtocolDetector(protocolVersion), this._recvBuffer);
                tcpDevice.UpdateRemoteEP(netWorkCommInterface.ipAddress, netWorkCommInterface.remotePort);

                this.OutputInfoLine("Crate Device : {0}, Protocol = {1}...Success.", tcpDevice.connectionInfo, GetCommProtocolName(protocolVersion));

                return tcpDevice;
            }

            return null;
        }

        public void OutputInfo(string format, params object[] arg)
        {
            string info = string.Format(format, arg);
            this.eventBus.NotifyObeserver(CommOutputMessageArgs.Create(info, false));
        }

        public void OutputInfoLine(string format, params object[] arg)
        {
            string info = string.Format(format, arg);
            this.eventBus.NotifyObeserver(CommOutputMessageArgs.Create(info, true));
        }

        public void bgwComm_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.eventBus.NotifyObeserver(new CommWrokCompletedArgs((bool)e.Result));
        }

        public static byte[] GetHandshakeCmd(CommProtocolVersion version)
        {
            if (version == CommProtocolVersion.Default)
            {
                return defaultHandshakeCmd;
            }
            else
            {
                return secondaryHandshakeCmd;
            }
        }

        public static byte[] GetOfflineCmd(CommProtocolVersion version)
        {
            if (version == CommProtocolVersion.Default)
            {
                return defaultOfflineCmd;
            }
            else
            {
                return secondaryOfflineCmd;
            }
        }

        public static byte[] GetLoadModelCmd(CommProtocolVersion version)
        {
            if (version == CommProtocolVersion.Default)
            {
                return defaultReadDeviceModelCmd;
            }
            else
            {
                return secondaryLoadDeviceModelCmd;
            }
        }

        public static bool ParseDeviceModelCode(CommProtocolVersion version, byte[] buffer, out uint deviceModel)
        {
            if (version == CommProtocolVersion.Default)
            {
                return defaultParseDeviceModelHandler(buffer, out deviceModel);
            }
            else
            {
                return secondaryParseDeviceModelHandler(buffer, out deviceModel);
            }
        }

        public static string GetCommProtocolName(CommProtocolVersion version)
        {
            bool defaultIsVKxS = CommProtocolBase.DefaultSystemModuleIndex == 0;
            if (version == CommProtocolVersion.Default)
            {
                return defaultIsVKxS ? vkxS : vkx;
            }
            else
            {
                return defaultIsVKxS ? vkx : vkxS;
            }
        }

        public static CommProtocolDetector GetCommProtocolDetector(CommProtocolVersion version)
        {
            if(version == CommProtocolVersion.Default)
            {
                return defaultProtocolDetector;
            }
            else
            {
                return secondaryCommProtocolDetector;
            }
        }

        //public static CommProtocolVersion GetDefaultProtocolVersion()
        //{
        //    if (CommProtocolBase.DefaultSystemModuleIndex == 0)
        //    {
        //        return CommProtocolVersion.Default;
        //    }
        //    else
        //    {
        //        return CommProtocolVersion.Secondary;
        //    }
        //}

        //public static CommProtocolVersion GetSecondaryProtocolVersion()
        //{
        //    if (CommProtocolBase.DefaultSystemModuleIndex == 0)
        //    {
        //        return CommProtocolVersion.Secondary;
        //    }
        //    else
        //    {
        //        return CommProtocolVersion.Default;
        //    }
        //}
    }
}
