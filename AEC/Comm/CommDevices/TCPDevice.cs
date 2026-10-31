using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using AEC.Comm.Net;

namespace AEC.Comm.CommDevices
{
    class TCPDevice : CommDevice
    {
        public const string DEF_REMOTE_IPADDRESS = "0.0.0.0";
        public const int DEF_REMOTE_PORT = 0;

        private const int TCP_SEND_TIME_OUT = 500;
        private const int TCP_CONNECT_TIME_OUT = 1000;

        private TCPClient tcpClient = null;
        //private TcpClient tcpClient = null;
        //private IPEndPoint remoteEP = null;
        private IPAddress remoteIpAddress = null;
        private int remotePort = 0;

        public override string connectionInfo
        {
            get
            {
                return string.Format("TCP {0}:{1}", remoteIpAddress.ToString(), remotePort);
            }
        }

        public TCPDevice(CommProtocolDetector detector_, List<byte> recvBuffer_) :
            base(detector_, recvBuffer_)
        {
            Init();
        }

        private void Init()
        {
            tcpClient = new TCPClient(true, TCP_SEND_TIME_OUT);
            tcpClient.DatagramReceived += tcpClient_DatagramReceived;

            remoteIpAddress = IPAddress.Parse(DEF_REMOTE_IPADDRESS);
            remotePort = DEF_REMOTE_PORT;
        }

        private void tcpClient_DatagramReceived(object sender, TcpClientDatagramReceivedEventArgs<byte[]> e)
        {
            byte[] data = e.Datagram;
            if (data != null && data.Length > 0)
            {
                AEC.Tools.LogHelper.WriteDebug("TCPDevice.Recv()... Receive Data (Length: {0})", data.Length);

                base.AppendRecvData(data);

                AEC.Tools.LogHelper.WriteDebug("TCPDevice.Recv(). Data Received OK : {0}", AEC.Core.Utils.ByteArrayToHexStr(data));
            }
        }

        public void UpdateRemoteEP(IPAddress ip, int port)
        {
            this.remoteIpAddress = ip;
            this.remotePort = port;
        }

        public void UpdateRemoteEP(string ip, int port)
        {
            IPAddress ipAddress;
            if (IPAddress.TryParse(ip, out ipAddress))
            {
                remoteIpAddress = ipAddress;
            }
            remotePort = port;
        }

        private bool AsynConnect()
        {
            if(tcpClient == null)
            {
                return false;
            }

            AEC.Tools.LogHelper.WriteInfo("TCPDevice.AsynConnect()...ip:{0}, port:{1}", remoteIpAddress.ToString(), remotePort);

            IAsyncResult result = tcpClient.Connect(remoteIpAddress, remotePort);
            result.AsyncWaitHandle.WaitOne(TCP_CONNECT_TIME_OUT, false);
            if(!result.IsCompleted)
            {
                tcpClient.Close();
                return false;
            }
            else
            {
                Thread.Sleep(0x40);
                return true;
            }
        }

        #region 重写基类方法
        public override bool Open()
        {
            if (this.tcpClient.Connected)
            {
                this.tcpClient.Close();
            }

            try
            {
                AEC.Tools.LogHelper.WriteDebug("TCPDevice.Open()...");

                return AsynConnect();
            }
            catch (Exception ex)
            {
                AEC.Tools.LogHelper.WriteDebug("TCPDevice.Open()... Exception: ", ex);

                return false;
            }
        }

        public override void Close()
        {
            AEC.Tools.LogHelper.WriteDebug("TCPDevice.Close()...");

            tcpClient.Close();

            AEC.Tools.LogHelper.WriteDebug("TCPDevice.Close(). OK");
        }

        protected override void Send(byte[] data)
        {
            if (!this.tcpClient.Connected)
            {
                return;
            }

            try
            {
                AEC.Tools.LogHelper.WriteDebug("TCPDevice.Send()... Send Data : {0}", AEC.Core.Utils.ByteArrayToHexStr(data));

                this.tcpClient.Send(data);

                AEC.Tools.LogHelper.WriteDebug("TCPDevice.Send(). Send Data (Length: {0}) OK.", data.Length);
            }
            catch (Exception ex)
            {
                AEC.Tools.LogHelper.WriteDebug("TCPDevice.Send().... Exception: ", ex);
            }
        }
        #endregion
    }
}
