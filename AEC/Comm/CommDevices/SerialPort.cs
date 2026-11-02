using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Comm.CommDevices
{
    class SerialPort : CommDevice
    {
        private System.IO.Ports.SerialPort sp = new System.IO.Ports.SerialPort();

        public const int defaultBaudRate = 57600;
        public const int secondaryBaudRate = 115200;

        private static readonly string defPortName = "COM1";
        private static readonly int dataBits = 8;
        private static readonly System.IO.Ports.StopBits stopBits = System.IO.Ports.StopBits.One;
        private static readonly System.IO.Ports.Parity parity = System.IO.Ports.Parity.None;

        public override string connectionInfo
        {
            get
            {
                return portName;
            }
        }

        public SerialPort(CommProtocolDetector detector_, List<byte> recvBuffer_) :
            base(detector_, recvBuffer_)
        {
            this.sp.PortName = defPortName;
            this.sp.BaudRate = defaultBaudRate;
            this.sp.DataBits = dataBits;
            this.sp.StopBits = stopBits;
            this.sp.Parity = parity;
            this.sp.WriteTimeout = writeTimeout;

            this.sp.DataReceived += serialPort_DataReceived;
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
                AEC.Tools.LogHelper.WriteDebug("SerialPort.serialPort_DataReceived()... Exception: ", ex);
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

        public void Recv()
        {
            int count = this.RecvBufferCount;
            if (count > 0)
            {
                AEC.Tools.LogHelper.WriteDebug("SerialPort.Recv()... Receive Data (Length: {0})", count);

                byte[] buffer = new byte[count];
                this.sp.Read(buffer, 0, count);

                AEC.Tools.LogHelper.WriteDebug("SerialPort.Recv(). Data Received OK : {0}", AEC.Core.Utils.ByteArrayToHexStr(buffer));
                
                base.AppendRecvData(buffer);
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
                AEC.Tools.LogHelper.WriteDebug("SerialPort.Open()... Open Port: \"{0}\"", this.sp.PortName);

                this.sp.Open();

                AEC.Tools.LogHelper.WriteDebug("SerialPort.Open(). Open Port: \"{0}\" OK.", this.sp.PortName);

                return true;
            }
            catch (Exception ex)
            {
                AEC.Tools.LogHelper.WriteDebug("SerialPort.Open()... Exception: ", ex);

                return false;
            }
        }

        public override void Close()
        {
            if (this.IsOpen)
            {
                try
                {
                    AEC.Tools.LogHelper.WriteDebug("SerialPort.Close()...");
                    this.sp.Close();
                    AEC.Tools.LogHelper.WriteDebug("SerialPort.Close(). OK");
                }
                catch (Exception ex)
                {
                    AEC.Tools.LogHelper.WriteDebug("SerialPort.Close()... Exception: ", ex);
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
                AEC.Tools.LogHelper.WriteDebug("SerialPort.Send()... Send Data : {0}", AEC.Core.Utils.ByteArrayToHexStr(data));

                this.sp.Write(data, 0, data.Length);

                AEC.Tools.LogHelper.WriteDebug("SerialPort.Send(). Send Data (Length: {0}) OK.", data.Length);
            }
            catch (Exception ex)
            {
                AEC.Tools.LogHelper.WriteDebug("SerialPort.Send().... Exception: ", ex);
            }
        }
        #endregion

        public static string[] GetValidPorts()
        {
            string[] portNames = System.IO.Ports.SerialPort.GetPortNames();
            //Dictionary<string, string> portDict = new Dictionary<string, string>();
            //foreach (string port in portNames)
            //{
            //    portDict[port] = port;
            //}
            //List<string> portList = new List<string>();
            //if (LastValidPort != string.Empty)
            //{
            //    portList.Add(LastValidPort);
            //    if (portDict.ContainsKey(LastValidPort))        //如果包含上次有效端口
            //    {
            //        portDict.Remove(LastValidPort);
            //    }
            //}

            //foreach (string port in portDict.Keys)
            //{
            //    portList.Add(port);
            //}

            List<string> list = new List<string>();
            System.IO.Ports.SerialPort sp = new System.IO.Ports.SerialPort();
            foreach (string port in portNames)
            {
                sp.PortName = port;
                try
                {
                    sp.Open();
                    sp.Close();
                    list.Add(port);
                }
                catch(Exception)
                {

                }
            }

            return list.ToArray();
        }

        public static String[] GetOrderedPortName(String lastPort)
        {
            return GetValidPorts();
            //String[] ports = GetValidPorts();
            //if(ports.Length == 0 || lastPort.Length == 0)
            //{
            //    return ports;
            //}
            //Dictionary<String, String> portDict = new Dictionary<string, String>();
            //foreach(String port in ports)
            //{
            //    portDict[port] = port;
            //}
            //if(portDict.ContainsKey(lastPort))
            //{
            //    portDict.Remove(lastPort);
            //}
            //else
            //{
            //    return ports;
            //}

            //List<String> portList = new List<string>();
            //portList.Add(lastPort);
            //foreach(String key in portDict.Keys)
            //{
            //    portList.Add(key);
            //}
            //return portList.ToArray();
        }
    }
}
