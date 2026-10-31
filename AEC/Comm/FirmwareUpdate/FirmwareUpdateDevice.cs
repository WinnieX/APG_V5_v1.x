using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AEC.Comm.CommDevices.HID;

namespace AEC.Comm.FirmwareUpdate
{
    public abstract class FirmwareUpdateDevice
    {
        public delegate bool CheckResponseIsCompletedDelegate(List<byte> buffer);

        private const int DefWaitTime = 1024;
        private readonly List<byte> recvBuffer;
        private AutoResetEvent responseEvent = new AutoResetEvent(false);

        protected static readonly int writeTimeout = CommServiceBase.MCommWaitTimeUnit * 1;

        private CheckResponseIsCompletedDelegate checkResponseIsCompletedCallback { get; set; }

        public virtual string connectionInfo
        {
            get
            {
                return string.Empty;
            }
        }

        //public FirmwareUpgradeDeviceBase(CommProtocolDetector detector_, List<byte> recvBuffer_)
        public FirmwareUpdateDevice(CheckResponseIsCompletedDelegate checkResponseIsCompletedCallback_, List<byte> recvBuffer_)
        {
            //System.Diagnostics.Debug.Assert(detector_ != null);

            this.checkResponseIsCompletedCallback = checkResponseIsCompletedCallback_;
            this.recvBuffer = recvBuffer_;
        }

        #region 方法
        public abstract bool Open();
        public abstract void Close();
        protected abstract void Send(byte[] data);
        #endregion

        private void ClearRecvBuffer()
        {
            lock(this.recvBuffer)
            {
                recvBuffer.Clear();
                //Log.OutputLine("清空接收缓冲区");
            }
        }

        public void AppendRecvData(byte[] data)
        {
            List<byte> buffer = null;

            lock (this.recvBuffer)
            {
                recvBuffer.AddRange(data);
                buffer = new List<byte>(recvBuffer);
            }

            System.Diagnostics.Debug.Assert(buffer != null);

            if(this.checkResponseIsCompletedCallback != null && this.checkResponseIsCompletedCallback(buffer))
            {
                responseEvent.Set();
            }
        }

        public void SendDataTest(byte[] data)
        {
            Send(data);
        }

        public void SyncSend(byte[] data, int timeout = DefWaitTime)
        {
            responseEvent.Reset();

            ClearRecvBuffer();

            Send(data);

            if (timeout > 0)
            {
                //AEC.Tools.LogHelper.WriteInfo("FirmwareUpgradeDevice.SyncSend()...timeout = {0}", timeout);
                responseEvent.WaitOne(timeout);
            }
        }

        public bool WaitResponse(int timeout)
        {
            return responseEvent.WaitOne(timeout);
        }
    }

    public class FUSerialPortDevice : FirmwareUpdateDevice
    {
        private System.IO.Ports.SerialPort sp = new System.IO.Ports.SerialPort();

        private static readonly string defPortName = "COM1";
        private static readonly int baudRate = 57600;
        private static readonly int dataBits = 8;
        private static readonly System.IO.Ports.StopBits stopBits = System.IO.Ports.StopBits.One;
        private static readonly System.IO.Ports.Parity parity = System.IO.Ports.Parity.None;
        private const int writeBufferSize = 0x10000;

        public FUSerialPortDevice(CheckResponseIsCompletedDelegate checkResponseIsCompletedCallback_, List<byte> recvBuffer_) :
            base(checkResponseIsCompletedCallback_, recvBuffer_)
        {
            this.sp.DataReceived += serialPort_DataReceived;

            this.sp.PortName = defPortName;
            this.sp.BaudRate = baudRate;
            this.sp.DataBits = dataBits;
            this.sp.StopBits = stopBits;
            this.sp.Parity = parity;
            this.sp.WriteBufferSize = writeBufferSize;
            this.sp.WriteTimeout = writeTimeout;
        }

        public int buadRate
        {
            get
            {
                return this.sp.BaudRate;
            }
            set
            {
                this.sp.BaudRate = value;
            }
        }

        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            System.IO.Ports.SerialPort sp = (System.IO.Ports.SerialPort)sender;

            try
            {
                if (this.IsOpen && sp.BytesToRead > 0)
                {
                    this.Recv();
                }
            }
            catch (Exception ex)
            {
                AEC.Tools.LogHelper.WriteDebug("FUSerialPortDevice.serialPort_DataReceived()... Exception: ", ex);
            }

        }

        public override string connectionInfo
        {
            get
            {
                return portName;
            }
        }

        public string portName
        {
            get
            {
                return this.sp.PortName;
            }

            set
            {
                if (!this.sp.IsOpen)
                {
                    this.sp.PortName = value;
                }
            }
        }

        public int BaudRate
        {
            set
            {
                if (!this.sp.IsOpen)
                {
                    this.sp.BaudRate = value;
                }
            }
            get
            {
                return this.sp.BaudRate;
            }
        }

        public bool IsOpen
        {
            get
            {
                return this.sp.IsOpen;
            }
        }

        private int RecvBufferCount
        {
            get
            {
                return this.sp.BytesToRead;
            }
        }

        public override bool Open()
        {
            if (this.IsOpen)
            {
                return true;
            }
            try
            {
                AEC.Tools.LogHelper.WriteDebug("FUSerialPortDevice.Open()... Open Port: \"{0}\"", this.sp.PortName);

                this.sp.Open();

                AEC.Tools.LogHelper.WriteDebug("FUSerialPortDevice.Open(). Open Port: \"{0}\" OK.", this.sp.PortName);

                return true;
            }
            catch (Exception ex)
            {
                AEC.Tools.LogHelper.WriteDebug("FUSerialPortDevice.Open()... Exception: ", ex);

                return false;
            }
        }

        public override void Close()
        {
            if (this.IsOpen)
            {
                try
                {
                    AEC.Tools.LogHelper.WriteDebug("FUSerialPortDevice.Close()...");
                    this.sp.Close();
                    AEC.Tools.LogHelper.WriteDebug("FUSerialPortDevice.Close(). OK");
                }
                catch (Exception ex)
                {
                    AEC.Tools.LogHelper.WriteDebug("FUSerialPortDevice.Close()... Exception: ", ex);
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
                AEC.Tools.LogHelper.WriteDebug("FUSerialPortDevice.Send()... Send Data : {0}", AEC.Core.Utils.ByteArrayToHexStr(data));

                this.sp.Write(data, 0, data.Length);

                AEC.Tools.LogHelper.WriteDebug("FUSerialPortDevice.Send(). Send Data (Length: {0}) OK.", data.Length);
            }
            catch (Exception ex)
            {
                AEC.Tools.LogHelper.WriteDebug("FUSerialPortDevice.Send().... Exception: ", ex);
            }
        }

        public void Recv()
        {
            int count = this.RecvBufferCount;
            if (count > 0)
            {
                AEC.Tools.LogHelper.WriteDebug("FUSerialPortDevice.Recv()... Receive Data (Length: {0})", count);

                byte[] buffer = new byte[count];
                this.sp.Read(buffer, 0, count);

                AEC.Tools.LogHelper.WriteDebug("FUSerialPortDevice.Recv(). Data Received OK : {0}", AEC.Core.Utils.ByteArrayToHexStr(buffer));

                base.AppendRecvData(buffer);
            }
        }
    }

    public class FUHIDDevice : FirmwareUpdateDevice
    {
        private AEC.Comm.CommDevices.HID.HIDDevice hid = new AEC.Comm.CommDevices.HID.HIDDevice();
        public int hid_index { get; set; }

        private Mutex mutex = null;
        private bool createdNewMutex = false;

        public override string connectionInfo
        {
            get
            {
                //return hid.productInfo.product;
                return string.Format("HID {0}", hid_index + 1);
            }
        }

        public FUHIDDevice(CheckResponseIsCompletedDelegate checkResponseIsCompletedCallback_, List<byte> recvBuffer_) :
            base(checkResponseIsCompletedCallback_, recvBuffer_)
        {
            this.hid.DataReceived += hid_DataReceived;       
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
                AEC.Tools.LogHelper.WriteDebug("FUHIDDevice.Recv(). Data Received OK : {0}", AEC.Core.Utils.ByteArrayToHexStr(data));

                byte[] payloadData = HIDProtocol.ParsePayloadData(data); 
                if(payloadData != null)
                {

                    //AEC.Tools.LogHelper.WriteDebug("FUHIDDevice.Recv()... Payload Data :{0}", AEC.Core.Utils.ByteArrayToHexStr(payloadData));
                    base.AppendRecvData(payloadData);
                }
                else
                {
                    AEC.Tools.LogHelper.WriteDebug("FUHIDDevice.Recv()... ParsePayloadData Error:{0}", AEC.Core.Utils.ByteArrayToHexStr(data));
                }
            }
            catch (Exception ex)
            {
                AEC.Tools.LogHelper.WriteDebug("FUHIDDevice.hid_DataReceived()... Exception: ", ex);
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

                AEC.Tools.LogHelper.WriteDebug("FUHIDDevice.Open()... Open Path: \"{0}\"", this.hid.devicePath);

                bool ret = this.hid.InitDevice();
                this.hid.WriteTimeout = writeTimeout;
                if(!ret)
                {
                    this.CloseMutex();
                }

                AEC.Tools.LogHelper.WriteDebug("FUHIDDevice.Open(). Open Path: \"{0}\" {1}.", this.hid.devicePath, ret ? "OK" : "Failed");

                return ret;
            }
            catch (Exception ex)
            {
                this.CloseMutex();

                this.hid.Close();

                AEC.Tools.LogHelper.WriteDebug("FUHIDDevice.Open()... Exception: {0}", ex);

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
                    AEC.Tools.LogHelper.WriteDebug("FUHIDDevice.Close()...");
                    this.hid.DataReceived -= hid_DataReceived; 
                    this.hid.Close();
                    AEC.Tools.LogHelper.WriteDebug("FUHIDDevice.Close(). OK");
                }
                catch (Exception ex)
                {
                    AEC.Tools.LogHelper.WriteDebug("FUHIDDevice.Close()... Exception: ", ex);
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
                AEC.Tools.LogHelper.WriteDebug("FUHIDDevice.Send()... Send Data : {0}", AEC.Core.Utils.ByteArrayToHexStr(data));

                //this.hid.Write(data, 0, data.Length);
                List<byte[]> list = HIDProtocol.SplitData(data);
                foreach(byte[] frame in list)
                {
                    //AEC.Tools.LogHelper.WriteDebug("HIDDevice.Send()...  hid Send Data : {0}", AEC.Core.Utils.ByteArrayToHexStr(frame));
                    this.hid.Write(frame);
                    //System.Threading.Thread.Sleep(16);
                }

                AEC.Tools.LogHelper.WriteDebug("FUHIDDevice.Send(). Send Data (Length: {0}) OK.", data.Length);
            }
            catch (Exception ex)
            {
                AEC.Tools.LogHelper.WriteDebug("FUHIDDevice.Send().... Exception: ", ex);
            }
        }
        #endregion

        public static AEC.Comm.CommDevices.HID.HIDDevice.InterfaceDetails[] GetValidDevices()
        {
            return AEC.Comm.CommDevices.HID.HIDDevice.GetConnectedDevices(HIDProtocol.VID, HIDProtocol.PID);
        }
    }
}
