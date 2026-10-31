using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AEC.Comm.CommDevices.HID;

namespace AEC.Comm.CommDevices
{
    public class HIDDevice : CommDevice
    {
        private AEC.Comm.CommDevices.HID.HIDDevice hid = new AEC.Comm.CommDevices.HID.HIDDevice();
        public string serialNumber { get; set; }

        private Mutex mutex = null;
        private bool createdNewMutex = false;

        public override string connectionInfo
        {
            get
            {
                return GetUSBHIDPortName(serialNumber);
            }
        }

        public HIDDevice(CommProtocolDetector detector_, List<byte> recvBuffer_) :
            base(detector_, recvBuffer_)
        {
            //this.hid.DataReceived += hid_DataReceived;       
        }

        public string devicePath
        {
            get
            {
                return this.hid.devicePath;
            }

            set
            {
                if (!this.hid.deviceConnected)
                {
                    this.hid.devicePath = value;
                }
            }            
        }

        private void hid_DataReceived(byte[] data)
        {
            //System.IO.Ports.SerialPort sp = (System.IO.Ports.SerialPort)sender;

            try
            {
                AEC.Tools.LogHelper.WriteDebug("HIDDevice.Recv(). Data Received OK : {0}", AEC.Core.Utils.ByteArrayToHexStr(data));              
                byte[] payloadData = HIDProtocol.ParsePayloadData(data); 
                if(payloadData != null)
                {

                    //AEC.Tools.LogHelper.WriteInfo("HIDDevice.Recv()... Payload Data :{0}", AEC.Core.Utils.ByteArrayToHexStr(payloadData));
                    base.AppendRecvData(payloadData);
                }
                else
                {
                    AEC.Tools.LogHelper.WriteDebug("HIDDevice.Recv()... ParsePayloadData Error:{0}", AEC.Core.Utils.ByteArrayToHexStr(data));
                }
            }
            catch (Exception ex)
            {
                AEC.Tools.LogHelper.WriteDebug("HIDDevice.hid_DataReceived()... Exception: ", ex);
            }

        }

        public bool IsOpen
        {
            get
            {
                return this.hid.deviceConnected;
            }
        }

        #region 重写基类方法
        public override bool Open()
        {
            if (this.IsOpen)
            {
                return true;
            }
            try
            {
                if (string.IsNullOrEmpty(this.hid.devicePath))
                {
                    return false;
                }

                string mutexName = AEC.Tools.HashHelper.GetStringMD5(this.hid.devicePath);
                this.mutex = new System.Threading.Mutex(false, mutexName, out createdNewMutex);
                if (!createdNewMutex)
                {
                    this.CloseMutex();
                    AEC.Tools.LogHelper.WriteDebug("hid设备:{0}被其他进程打开，本次打开失败！", this.hid.devicePath);
                    return false;
                }
                
                AEC.Tools.LogHelper.WriteDebug("HIDDevice.Open()... Open Path: \"{0}\"", this.hid.devicePath);

                bool ret = this.hid.InitDevice();
                this.hid.WriteTimeout = writeTimeout;
                if(!ret)
                {
                    this.CloseMutex();
                }

                this.hid.DataReceived += hid_DataReceived;

                AEC.Tools.LogHelper.WriteDebug("HIDDevice.Open(). Open Path: \"{0}\" {1}.", this.hid.devicePath, ret ? "OK" : "Failed");

                return ret;
            }
            catch (Exception ex)
            {
                this.CloseMutex();

                this.hid.Close();

                AEC.Tools.LogHelper.WriteDebug("HIDDevice.Open()... Exception: {0}", ex);

                return false;
            }
        }

        private void CloseMutex()
        {
            if (this.mutex != null && !this.mutex.SafeWaitHandle.IsClosed)
            {
                this.mutex.Close();
                this.mutex = null;
            }
        }

        public override void Close()
        {
            this.CloseMutex();

            if (this.IsOpen)
            {
                try
                {
                    AEC.Tools.LogHelper.WriteDebug("HIDDevice.Close()...");
                    this.hid.Close();
                    AEC.Tools.LogHelper.WriteDebug("HIDDevice.Close(). OK");

                    this.hid.DataReceived -= hid_DataReceived;
                }
                catch (Exception ex)
                {
                    AEC.Tools.LogHelper.WriteDebug("HIDDevice.Close()... Exception: ", ex);
                }
            }
        }

        protected override void Send(byte[] data)
        {
            if (!this.IsOpen)
            {
                return;
            }

            try
            {
                AEC.Tools.LogHelper.WriteDebug("HIDDevice.Send()... Send Data : {0}", AEC.Core.Utils.ByteArrayToHexStr(data));

                //this.hid.Write(data, 0, data.Length);
                List<byte[]> list = HIDProtocol.SplitData(data);
                foreach(byte[] frame in list)
                {
                    //AEC.Tools.LogHelper.WriteDebug("HIDDevice.Send()...  hid Send Data : {0}", AEC.Core.Utils.ByteArrayToHexStr(frame));
                    this.hid.Write(frame);
                    //System.Threading.Thread.Sleep(16);
                }

                AEC.Tools.LogHelper.WriteDebug("HIDDevice.Send(). Send Data (Length: {0}) OK.", data.Length);
            }
            catch (Exception ex)
            {
                AEC.Tools.LogHelper.WriteDebug("HIDDevice.Send().... Exception: ", ex);
            }
        }
        #endregion

        public static AEC.Comm.CommDevices.HID.HIDDevice.InterfaceDetails[] GetValidDevices()
        {
            return AEC.Comm.CommDevices.HID.HIDDevice.GetConnectedDevices(HIDProtocol.VID, HIDProtocol.PID);
        }

        public static string GetUSBHIDPortName(string serialNumber)
        {
            if (!string.IsNullOrEmpty(serialNumber))
            {
                string sn = string.Empty;
                if (serialNumber.Length > 4)
                {
                    sn = serialNumber.Substring(0, 4);
                }
                else
                {
                    sn = serialNumber;
                }

                return string.Format("USB HID_{0}", sn);
            }
            else
            {
                return "USB HID";
            }
        }
    }
}
