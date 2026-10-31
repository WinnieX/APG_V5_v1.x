using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using AEC.Comm.Net;

namespace AEC.Comm.CommDevices
{
    class UDPDevice : CommDevice
    {
        public const string DEF_REMOTE_IPADDRESS = "0.0.0.0";
        public const int DEF_REMOTE_PORT = 0;

        private UDPClient udpClient = null;

        private IPAddress remoteIpAddress = null;
        private int remotePort = 0;

        private IPEndPoint remoteEndPoint = null;

        public override string connectionInfo
        {
            get
            {
                return string.Format("UDP {0}:{1}", remoteIpAddress.ToString(), remotePort);
            }
        }

        public UDPDevice(CommProtocolDetector detector_, List<byte> recvBuffer_) :
            base(detector_, recvBuffer_)
        {
            Init();
        }

        private void Init()
        {
            udpClient = new UDPClient();
            udpClient.DatagramReceived += udpClient_DatagramReceived;

            remoteIpAddress = IPAddress.Parse(DEF_REMOTE_IPADDRESS);
            remotePort = DEF_REMOTE_PORT;
        }

        private void udpClient_DatagramReceived(object sender, UdpClientDatagramReceivedEventArgs<byte[]> e)
        {
            System.Diagnostics.Debug.Assert(e.remoteEndPoint != null);

            if(!e.remoteEndPoint.Address.Equals(this.remoteIpAddress) || e.remoteEndPoint.Port != remotePort)
            {
                return;
            }

            byte[] data = e.Datagram;
            if (data != null && data.Length > 0)
            {
                AEC.Tools.LogHelper.WriteDebug("UDPDevice.Recv()... Receive Data (Length: {0})", data.Length);

                base.AppendRecvData(data);

                AEC.Tools.LogHelper.WriteDebug("UDPDevice.Recv(). Data Received OK : {0}", AEC.Core.Utils.ByteArrayToHexStr(data));
            }
        }

        public void UpdateRemoteEP(IPAddress ip, int port)
        {
            this.remoteIpAddress = ip;
            this.remotePort = port;

            this.remoteEndPoint = new IPEndPoint(ip, remotePort);
        }

        public void UpdateRemoteEP(string ip, int port)
        {
            IPAddress ipAddress;
            if (IPAddress.TryParse(ip, out ipAddress))
            {
                remoteIpAddress = ipAddress;
            }
            remotePort = port;

            this.remoteEndPoint = new IPEndPoint(ipAddress, remotePort);
        }

        private void InitClient()
        {
            AEC.Tools.LogHelper.WriteInfo("UDPDevice.InitClient()...port:{0}", UDPClient.DefBindPort);

            this.udpClient.Init();

            AEC.Tools.LogHelper.WriteInfo("UDPDevice.InitClient()...Bind port:{0}", this.udpClient.bindPort);
        }

        #region 重写基类方法
        public override bool Open()
        {
            try
            {
                AEC.Tools.LogHelper.WriteDebug("UDPDevice.Open()...");

                InitClient();

                return true;
            }
            catch (Exception ex)
            {
                AEC.Tools.LogHelper.WriteDebug("UDPDevice.Open()... Exception: ", ex);

                return false;
            }
        }

        public override void Close()
        {
            if (this.udpClient != null)
            {
                AEC.Tools.LogHelper.WriteDebug("UDPDevice.Close()...");

                this.udpClient.Close();

                AEC.Tools.LogHelper.WriteDebug("UDPDevice.Close(). OK");

                this.udpClient = null;
            }
        }

        protected override void Send(byte[] data)
        {
            try
            {
                AEC.Tools.LogHelper.WriteDebug("UDPDevice.Send()... Send Data : {0}", AEC.Core.Utils.ByteArrayToHexStr(data));

                if (this.udpClient != null)
                {
                    this.udpClient.SyncSendData(this.remoteEndPoint, data);
                }

                AEC.Tools.LogHelper.WriteDebug("UDPDevice.Send(). Send Data (Length: {0}) OK.", data.Length);
            }
            catch (Exception ex)
            {
                AEC.Tools.LogHelper.WriteDebug("UDPDevice.Send().... Exception: ", ex);
            }
        }
        #endregion
    }
}
