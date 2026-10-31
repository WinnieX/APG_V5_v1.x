using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using AEC.Core;
using AEC.Comm.CommDevices;
using AEC.Comm.CommEventArgs;

namespace AEC.Comm.ActivateDevice
{
    public class ActivateDeviceController
    {
        private const int CommTimeOut = 0x400;

        private const int HandshakeRetries = 2;

        //protected CommDevice commDevice;
        private readonly System.ComponentModel.BackgroundWorker bgwComm;
        public readonly MEventBus eventBus = new MEventBus();

        private readonly List<byte> _recvBuffer = new List<byte>();

        public delegate bool ParseDeviceModelCodeCallback(byte[] buffer, out uint deviceModel);

        private static readonly CommProtocolDetector defaultProtocolDetector = new CommProtocolDetector(CommProtocolBase.CheckCommCmdType, CommProtocolBase.CheckReadResponseIsCompleted, CommProtocolBase.CheckWriteResponseIsOk);
        
        private static readonly byte[] handshakeCmd = CommProtocolBase.GetHandshakeCmd();
        private static readonly byte[] offlineCmd = CommProtocolBase.GetOfflineCmd();
        private static readonly byte[] loadDeviceModelCmd = CommProtocolBase.GetLoadDeviceModelCodeCmd();
        private static readonly byte[] loadDeviceIdCmd = CommProtocolBase.GetLoadDeviceIdCmd();

        private static readonly ParseDeviceModelCodeCallback ParseDeviceModelHandler = CommProtocolBase.ParseDeviceModelCode;

        private CommDevice commDevice = null;

        public ActivateDeviceController()
        {
            this.bgwComm = new BackgroundWorker();
            this.bgwComm.DoWork += bgwComm_DoWork;
            this.bgwComm.RunWorkerCompleted += bgwComm_RunWorkerCompleted;
        }

        public bool RunTest(CommInterfaceBase commInterface)
        {
            if (this.bgwComm.IsBusy)
            {
                return false;
            }

            this.bgwComm.RunWorkerAsync(commInterface);

            return true;
        }

        private void bgwComm_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = false;

            CommInterfaceBase commInterface = e.Argument as CommInterfaceBase;

            try
            {
                this.OutputInfo("Activate Device Start.");

                if (commInterface != null)
                {
                    commDevice = CreateCommDevice(commInterface);

                    if (commDevice == null)
                    {
                        this.OutputInfoLine("Crate Device Failed.");

                        return;
                    }

                    e.Result = ActivateDevice(commDevice);
                }
                else
                {
                    this.OutputInfoLine("CommInterface cannot be null.");
                }
            }
            catch
            {

            }
            finally
            {
                if (commDevice != null)
                {
                    commDevice.Close();
                }
            }
        }

        private bool ActivateDevice(CommDevice commDevice)
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

                bool commOk = false;
                for (int i = 0; i < HandshakeRetries; ++i)
                {
                    this.OutputInfoLine("Send Handshke Request : {0}", AEC.Core.Utils.ByteArrayToHexStr(handshakeCmd));
                    commDevice.SyncSend(handshakeCmd, CommTimeOut);

                    //System.Threading.Thread.Sleep(0x100);

                    this.OutputRecvResponseData();

                    commOk = commDevice.detector.checkWriteResponseIsCompleted(this._recvBuffer);

                    if (commOk)
                    {
                        break;
                    }

                    this.OutputInfoLine("Check Handshke Response Failed!");
                }

                if (!commOk)
                {
                    this.OutputInfoLine("Handshake Failed.");
                    return false;
                }
                this.OutputInfoLine("Handshake Success.");

                this.OutputInfoLine("Send Load Model Code Request : {0}", AEC.Core.Utils.ByteArrayToHexStr(loadDeviceModelCmd));
                commDevice.SyncSend(loadDeviceModelCmd, CommTimeOut);
                this.OutputRecvResponseData();
                if (!commDevice.detector.checkReadResponseIsCompleted(this._recvBuffer))
                {
                    this.OutputInfoLine("Load Model Code Failed.");
                    return false;
                }

                uint deviceModel;
                if (!ParseDeviceModelHandler(this._recvBuffer.ToArray(), out deviceModel))
                {
                    this.OutputInfoLine("Parse Model Code Data Failed.");
                    return false;
                }
                this.OutputInfoLine("Parse Model Code : {0}...Success.", AEC.Core.Utils.Uint32ToHexStr(deviceModel));

                this.OutputInfoLine("Send Load Device ID Request : {0}", AEC.Core.Utils.ByteArrayToHexStr(loadDeviceIdCmd));
                commDevice.SyncSend(loadDeviceIdCmd, CommTimeOut);
                this.OutputRecvResponseData();
                if (!commDevice.detector.checkReadResponseIsCompleted(this._recvBuffer))
                {
                    this.OutputInfoLine("Load Device ID Failed.");
                    return false;
                }

                byte[] deviceID = CommProtocolBase.ParseDeviceIdCode(this._recvBuffer.ToArray());
                if (deviceID == null || deviceID.Length != sizeof(uint))
                {
                    this.OutputInfoLine("Parse Device ID Data Failed.");
                    return false;
                }

                this.OutputInfoLine("Parse Device ID : {0}...Success.", AEC.Core.Utils.BytesToHexStr(deviceID));

                byte[] acivateData = DeviceActivateCode.GetSystemAcivateData(deviceID);
                byte[] updateAcivateDataCmd = CommProtocolBase.GetUpdateActivateDataCmd(acivateData);

                //this.OutputInfoLine("Send Update System Data Request : {0}", AEC.Core.Utils.ByteArrayToHexStr(updateAcivateDataCmd));
                this.OutputInfoLine("Send Update System Data Request...");
                commDevice.SyncSend(updateAcivateDataCmd, CommTimeOut);
                this.OutputRecvResponseData();
                if (!commDevice.detector.checkWriteResponseIsCompleted(this._recvBuffer))
                {
                    this.OutputInfoLine("Update System Data Failed.");
                    return false;
                }
                this.OutputInfoLine("Update System Data Success.");

                //byte[] offlineCmd = offlineCmd;
                this.OutputInfoLine("Send Offline Request : {0}", AEC.Core.Utils.ByteArrayToHexStr(offlineCmd));
                commDevice.SyncSend(offlineCmd, CommTimeOut);
                this.OutputRecvResponseData();
                if (!commDevice.detector.checkWriteResponseIsCompleted(this._recvBuffer))
                {
                    this.OutputInfoLine("Offline Failed.");
                    return false;
                }
                this.OutputInfoLine("Offline Success.");

                return true;
            }
            catch (Exception e)
            {
                Tools.LogHelper.WriteDebug("ActivateDevice()...异常:", e.Message);

                return false;
            }
            finally
            {
                commDevice.Close();

                this.OutputInfoLine("Close Device.");
            }
        }

        private void OutputRecvResponseData()
        {
            if (this._recvBuffer.Count > 0)
            {
                this.OutputInfoLine("Received response : {0}", AEC.Core.Utils.ByteArrayToHexStr(this._recvBuffer.ToArray()));
            }
        }

        private CommDevice CreateCommDevice(CommInterfaceBase commInterface)
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
                        HIDDevice hidDevice = new HIDDevice(defaultProtocolDetector, this._recvBuffer);
                        hidDevice.devicePath = hid.devicePath;
                        hidDevice.serialNumber = hid.serialNumber;

                        this.OutputInfoLine("Crate Device : {0}...Success.", hidDevice.connectionInfo);

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
                        SerialPort sp = new SerialPort(defaultProtocolDetector, this._recvBuffer);
                        sp.portName = port;
                        sp.buadRate = rs232CommInterface.baudRate;

                        this.OutputInfoLine("Crate Device : {0}...Success.", sp.connectionInfo);

                        return sp;
                    }
                }
            }
            else if (commInterface.commInterfaceType == CommInterfaceType.UDP)
            {
                NetWrokCommInterface netWorkCommInterface = commInterface as NetWrokCommInterface;

                UDPDevice udpDevice = new UDPDevice(defaultProtocolDetector, this._recvBuffer);
                udpDevice.UpdateRemoteEP(netWorkCommInterface.ipAddress, netWorkCommInterface.remotePort);

                this.OutputInfoLine("Crate Device : {0}...Success.", udpDevice.connectionInfo);

                return udpDevice;
            }
            else if (commInterface.commInterfaceType == CommInterfaceType.TCP)
            {
                NetWrokCommInterface netWorkCommInterface = commInterface as NetWrokCommInterface;

                TCPDevice tcpDevice = new TCPDevice(defaultProtocolDetector, this._recvBuffer);
                tcpDevice.UpdateRemoteEP(netWorkCommInterface.ipAddress, netWorkCommInterface.remotePort);

                this.OutputInfoLine("Crate Device : {0}...Success.", tcpDevice.connectionInfo);

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
    }
}
