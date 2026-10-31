using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Net;
using AEC.Comm.Net;
using AEC.Core;
using AEC.Effectors;
using AEC.RemoteControl.Protocol;
using AEC.RemoteControl.Message;
using AEC.RemoteControl.Utility;
using AEC.Misc;

namespace AEC.RemoteControl
{
    public abstract class RCClient
    {
        public delegate EventBusArgs DeserializeToEventBusArgsCallback(byte[] buffer);

        protected const uint ProtocolVersion = 0x00000001;
        private const int offLineCountThreshold = 1;

        protected static readonly uint signatureCode = SignatureCodeHelper.GetCode();

        public const string unknownIdCode = "------";
        public const string unknownVCode = "----";

        public static RCConfig config = new RCConfig();
        protected TCPClient tcpClient;
        private System.Timers.Timer heartbeatTimer = null;
        private uint heartbeatCount = 0;
        private List<byte> recvBuffer = new List<byte>();
        //private uint clientID = 0;
        
        protected string clientTag = string.Empty;
        private Dictionary<string, Action<byte[]>> MessageHandlerMap;

        private readonly HandshakeRequest.Types.ClientType clientType;

        protected readonly MEffectorSeralizer effectorSeralizer;

        private readonly Localization localization = Config.GetInstance().localization;

        public uint clientId
        {
            get;
            private set;
        }

        private string _ipAddress = string.Empty;
        public string ipAddress
        {
            get
            {
                return _ipAddress;
            }
            set
            {
                _ipAddress = value;
            }
        }

        public readonly EventBus eventBus = new EventBus();

        public bool _rcConnected = false;

        #region 事件
        //public event EventHandler ConnectionCompleted;      //连接完成事件
        //public event EventHandler ConnectionRefused;        //连接被拒绝事件
        //public event EventHandler Disconnected;             //远程服务器断开连接事件
        //public event EventHandler CommTimeoutClosed;        //心跳超时断开连接事件
        #endregion

        public bool Connected
        {
            get;
            private set;
        }

        public bool rcConnected
        {
            get
            {
                return this._rcConnected;
            }
            protected set
            {
                if(this._rcConnected != value)
                {
                    this._rcConnected = value;
                    this.eventBus.NotifyObeserver(new RCConnectionStatusChangedArgs(this._rcConnected));
                }
            }
        }

        public string ClientIdCode
        {
            get
            {
                return ClientIdCode2String(this.clientId);
            }
        }

        public RCClient(HandshakeRequest.Types.ClientType clientType_, MEffectorSeralizer effectorSeralizer_)
        {
            this.clientType = clientType_;
            this.effectorSeralizer = effectorSeralizer_;

            heartbeatTimer = new System.Timers.Timer();
            heartbeatTimer.Interval = RCConfig.Kilo * config.heartbeatInterval;
            heartbeatTimer.Elapsed += heartbeatTimer_Elapsed;

            MessageHandlerMap = new Dictionary<string, Action<byte[]>>()
            {
                {MessageType.HandshakeResponse, HandshakeResponseHandler},
                {MessageType.HeartbeatResponse, HeartbeatResponseHandler},
                {MessageType.LcDisconnectFromRcResponse, LcDisconnectFromRcResponseHandler},
            };
        }

        private void LcDisconnectFromRcResponseHandler(byte[] buffer)
        {
            DisconnectedFromRcHandler(localization[Localization.remoteControl_RemoteClientDisconnect]);
        }

        //被动断开远程控制连接
        private void DisconnectedFromRcHandler(string message)
        {
            if (!tcpCliented || !rcConnected)
            {
                return;
            }

            this.rcConnected = false;
            this.eventBus.NotifyObeserver(new RCDisconnectedFromRcArgs(message));
        }

        //主动断开远程控制连接
        public void DisconnectFromRemoteClient()
        {
            if (!tcpCliented || !rcConnected)
            {
                return;
            }

            CommMessage message = new CommMessage(MessageType.LcDisconnectFromRcRequest);
            byte[] request = FrameEncoder.Encode(message);
            //Log.OutputLine(clientTag + ":断开远程联机请求: {0}", AEC.Core.Utils.ByteArrayToHexStr(request));
            this.tcpClient.Send(request);

            this.rcConnected = false;
            //发送断开连接消息
            this.eventBus.NotifyObeserver(new RCDisconnectFromRcArgs());
        }

        //此方法可能抛出异常
        public void ConnectToServer()
        {
            tcpClient = new TCPClient(true);
            RegisterTcpClientHandler();
            IPAddress serverIP = IPAddress.Parse(config.serverIP);
            recvBuffer.Clear();
            this.tcpClient.Connect(serverIP, config.serverPort);
            this.eventBus.NotifyObeserver(new RCConnectToServerArgs());
        }

        protected string ToClientMessage(string format, params object[] arg)
        {
            return clientTag + " : " + string.Format(format, arg);
        }

        public void QueryServerStatus()
        {
            if (tcpCliented)
            {
                //Log.OutputLine("request data 握手请求 数据: {0}", AEC.Core.Utils.ByteArrayToHexStr(data));
                CommMessage message = new CommMessage(MessageType.QueryServerStatusRequest);
                byte[] request = FrameEncoder.Encode(message);
                //Log.OutputLine(clientTag + ":查询服务端状态: {0}", AEC.Core.Utils.ByteArrayToHexStr(request));
                this.tcpClient.Send(request);
            }
        }

        private void DisconnectFromServerHandler()
        {
            this.CloseClient();
            this.ClearHeartbeatTime();
        }

        //客户端主动断开连接
        public void DisconnectFromServer()
        {
            AEC.Tools.LogHelper.WriteInfo(ToClientMessage("Disconnect From Server."));
            DisconnectFromServerHandler();
            this.eventBus.NotifyObeserver(new RCDisconnectFromServerArgs());
            this.DisconnectFromServerCallback();
        }

        //客户端被动断开连接
        private void DisconnectedFromServerHandler(string message)
        {
            AEC.Tools.LogHelper.WriteInfo(ToClientMessage(message));
            this.DisconnectFromServerHandler();
            this.eventBus.NotifyObeserver(new RCDisconnectedFromServerArgs(this.rcConnected, message));
            this.DisconnectedFromServerCallback(message);
        }

        //通讯出现异常
        protected void CommExceptionHandler(string message)
        {
            DisconnectedFromServerHandler(message);
        }

        //客户端主动断开远程连接
        private void DisconnectFromServerCallback()
        {
            if (this.rcConnected)
            {
                this.rcConnected = false;
                this.eventBus.NotifyObeserver(new RCDisconnectFromRcArgs());
            }
        }

        //客户端被动断开远程连接
        private void DisconnectedFromServerCallback(string message)
        {
            if (this.rcConnected)
            {
                this.rcConnected = false;
                this.eventBus.NotifyObeserver(new RCDisconnectedFromRcArgs(message));
            }
        }

        private void CloseClient()
        {
            if(this.tcpClient != null)
            {
                if(this.tcpClient.Connected)
                {
                    this.tcpClient.Close();
                }
                RemoveTcpClientHandler();
                this.tcpClient.Dispose();
                this.tcpClient = null;
                Connected = false;
            }
        }

        private void StartHeartbeat()
        {
            lock(this.heartbeatTimer)
            {
                //Console.WriteLine("开始心跳");
                heartbeatTimer.Start();
                this.heartbeatCount = 0;
            }
        }

        private void ClearHeartbeatTime()
        {
            lock (this.heartbeatTimer)
            {
                //Console.WriteLine("开始心跳");
                heartbeatTimer.Stop();
                this.heartbeatCount = 0;
            }
        }

        private void RegisterTcpClientHandler()
        {
            Debug.Assert(this.tcpClient != null);

            tcpClient.ServerConnected += tcpClient_ServerConnected;
            tcpClient.ConnectionRefused += tcpClient_ConnectionRefused;
            tcpClient.DatagramReceived += tcpClient_DatagramReceived;
            tcpClient.ServerDisconnected += tcpClient_ServerDisconnected;
        }

        private void RemoveTcpClientHandler()
        {
            Debug.Assert(this.tcpClient != null);

            tcpClient.ServerConnected -= tcpClient_ServerConnected;
            tcpClient.ConnectionRefused -= tcpClient_ConnectionRefused;
            tcpClient.DatagramReceived -= tcpClient_DatagramReceived;
            tcpClient.ServerDisconnected -= tcpClient_ServerDisconnected;
        }

        private void tcpClient_ServerConnected(object sender, EventArgs e)
        {
            if (tcpClient.Connected)
            {
                //Log.OutputLine(clientTag + ":连接服务器成功");
                //Handshake();
                
                this.eventBus.NotifyObeserver(new RCMessageArgs(clientTag + ":连接服务器成功"));

                this.SendHandshakeRequest();

                this.StartHeartbeat();
                //heartbeatTimer.Start();

                //if (ConnectionCompleted != null)
                //{
                //    ConnectionCompleted(this, EventArgs.Empty);
                //}
            }
            else
            {
                //Log.OutputLine(clientTag + ":连接服务器被拒绝");
                this.eventBus.NotifyObeserver(new RCHandshakeCompletedArgs(false, "连接服务器被拒绝!"));
                //if (ConnectionRefused != null)
                //{
                //    ConnectionRefused(this, EventArgs.Empty);
                //}
            }
        }

        private void tcpClient_ConnectionRefused(object sender, EventArgs e)
        {
            //Log.OutputLine(clientTag + ":连接服务器被拒绝");
            this.eventBus.NotifyObeserver(new RCHandshakeCompletedArgs(false, clientTag + ":连接服务器被拒绝"));

            //if (ConnectionRefused != null)
            //{
            //    ConnectionRefused(this, EventArgs.Empty);
            //}
        }

        private void tcpClient_DatagramReceived(object sender, TcpClientDatagramReceivedEventArgs<byte[]> e)
        {
            //throw new NotImplementedException();
            //Log.OutputLine(clientTag + "接收到服务器数据: {0}", Utils.ByteArrayToHexStr(e.Datagram));
            recvBuffer.AddRange(e.Datagram);
            ServerDataHandler();
        }

        private void tcpClient_ServerDisconnected(object sender, EventArgs e)
        {
            CommExceptionHandler("服务端断开连接");
        }

        private void ServerDataHandler()
        {
            while (true)
            {
                try
                {
                    byte[] buffer = recvBuffer.ToArray();
                    CommMessage message = FrameDecoder.Decode(buffer);
                    if(message != null)
                    {
                        int frameLength = FrameDecoder.GetFrameLength(message);
                        recvBuffer.RemoveRange(0, frameLength);
                        MessageHandler(message);
                    }
                    else
                    {
                        break;
                    }
                }
                catch(Exception ex)
                {
                    CommExceptionHandler(ex.Message);
                    return;
                }
            }
        }

        private void MessageHandler(CommMessage message)
        {
            Debug.Assert(message != null);

            //message.Print();
            
            if(MessageHandlerMap.ContainsKey(message.name))
            {
                try
                {
                    MessageHandlerMap[message.name](message.data);
                }
                catch(Exception e)
                {
                    AEC.Tools.LogHelper.WriteDebug("RCClient.MessageHandler.... Exception: ", e.Message);
                    this.CommExceptionHandler("RCClient.MessageHandler.... Exception: " + e.Message);
                }
            }
            else
            {
                RCMessageHandler(message);
            }
        }

        protected abstract void RCMessageHandler(CommMessage message);
        
        private void HandshakeResponseHandler(byte[] buffer)
        {
            HandshakeResponse response = HandshakeResponse.ParseFrom(buffer);
            if(response.Status == HandshakeResponse.Types.StatusCode.Successful &&
                response.HasClientId && response.HasIpAddress)
            {
                this.Connected = true;
                this.clientId = response.ClientId;
                this.ipAddress = response.IpAddress;
                //Console.WriteLine("client id:{0}", this.clientId);                
                this.eventBus.NotifyObeserver(new RCHandshakeCompletedArgs(true, "连接服务器被拒绝!"));
            }
            else
            {
                Console.WriteLine("握手失败:",response.ToString());
                this.eventBus.NotifyObeserver(new RCHandshakeCompletedArgs(false, "连接服务器握手失败!"));
            }
        }

        private void HeartbeatResponseHandler(byte[] buffer)
        {
            heartbeatCount = 0;
        }

        protected bool tcpCliented
        {
            get
            {
                return this.tcpClient != null && this.tcpClient.Connected;
            }
        }

        private void SendHandshakeRequest()
        {
            if (tcpCliented)
            {
                HandshakeRequest.Builder builder = new HandshakeRequest.Builder()
                    .SetVersion(ProtocolVersion)
                    .SetClientType(this.clientType)
                    .SetSignatureCode(signatureCode);

                //Log.OutputLine("request data 握手请求 数据: {0}", AEC.Core.Utils.ByteArrayToHexStr(data));
                CommMessage message = new CommMessage(MessageType.HandshakeRequest, builder.Build().ToByteArray());
                byte[] request = FrameEncoder.Encode(message);
                //Log.OutputLine(clientTag + ":发送握手请求: {0}", AEC.Core.Utils.ByteArrayToHexStr(request));
                this.tcpClient.Send(request);
            }
        }

        public void heartbeatTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //Log.OutputLine("心跳定时器...{0}", this.Connected);
            if(!this.Connected)
            {
                //Log.OutputLine(clientTag + "握手通讯超时!");
                CommExceptionHandler(clientTag + "Handshake communication timeout!");
                return;
            }
            else if (CheckClientHeartbeatTimeout())
            {
                //Log.OutputLine("心跳检测超时:{0}", this.heartbeatCount);
                AEC.Tools.LogHelper.WriteInfo(ToClientMessage("Heartbeat detection timeout!"));
                CommExceptionHandler(localization[Localization.remoteControl_ConnectionIsInterrupted]);
                return;
            }
            
            this.SendHeartbeatMessage();
            ++heartbeatCount;
        }

        private void SendHeartbeatMessage()
        {
            if (tcpCliented)
            {
                //Log.OutputLine("request data 握手请求 数据: {0}", AEC.Core.Utils.ByteArrayToHexStr(data));
                CommMessage message = new CommMessage(MessageType.HeartbeatRequest);
                byte[] request = FrameEncoder.Encode(message);
                //Log.OutputLine(clientTag + ":发送心跳包: {0}", AEC.Core.Utils.ByteArrayToHexStr(request));
                this.tcpClient.Send(request);
            }
        }

        private bool CheckClientHeartbeatTimeout()
        {
            return this.heartbeatCount >= offLineCountThreshold;
        }

        public string ClientIdCode2String(uint code)
        {
            return code.ToString("D6");
        }
    }
}
