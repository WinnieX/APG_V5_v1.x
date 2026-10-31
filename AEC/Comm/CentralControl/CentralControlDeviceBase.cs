using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AEC.Comm.CommDevices.HID;

namespace AEC.Comm.CentralControl
{
    public abstract class CentralControlDeviceBase
    {
        public delegate bool CheckResponseIsCompletedDelegate(List<byte> buffer);

        private const int DefWaitTime = 2 * 1024;
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
        public CentralControlDeviceBase(CheckResponseIsCompletedDelegate checkResponseIsCompletedCallback_, List<byte> recvBuffer_)
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

        public void SyncSend(byte[] data, int timeout = DefWaitTime)
        {
            responseEvent.Reset();

            ClearRecvBuffer();

            Send(data);

            if (timeout > 0)
            {
                responseEvent.WaitOne(timeout);
            }
        }

        public bool WaitResponse(int timeout)
        {
            return responseEvent.WaitOne(timeout);
        }

        public void CancelWait()
        {
            this.responseEvent.Set();
            Thread.Sleep(32);
            this.responseEvent.Reset();
        }

        public void ResetResponseEvent()
        {
            this.responseEvent.Reset();
        }
    }

    public class CentralControlSerialPortDevice : CentralControlDeviceBase
    {
        private System.IO.Ports.SerialPort sp = new System.IO.Ports.SerialPort();

        private static readonly string defPortName = "COM1";
        private static readonly int baudRate = 115200;
        private static readonly int dataBits = 8;
        private static readonly System.IO.Ports.StopBits stopBits = System.IO.Ports.StopBits.One;
        private static readonly System.IO.Ports.Parity parity = System.IO.Ports.Parity.None;
        private const int writeBufferSize = 0x10000;

        public CentralControlSerialPortDevice(CheckResponseIsCompletedDelegate checkResponseIsCompletedCallback_, List<byte> recvBuffer_) :
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
}
