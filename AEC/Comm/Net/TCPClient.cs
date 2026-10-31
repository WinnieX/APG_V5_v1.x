using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace AEC.Comm.Net
{
    public class TCPClient : IDisposable
    {
        private bool noDelay = false;
        private int sendTimeout = 0;

        #region Fields
        private System.Net.Sockets.TcpClient _client = null;
        private byte[] recvBuffer = null;
        private bool disposed = false;
        #endregion

        #region Properties
        // 客户端是否正在运行
        public bool Connected { get { return _client != null ? _client.Client.Connected : false; } }

        // 远端服务器的IP地址
        public IPAddress Addresses { get; private set; }

        // 远端服务器的端口
        public int Port { get; private set; }
        #endregion

        #region 构造函数
        public TCPClient(bool noDelay_ = false, int sendTimeout_ = 0)
        {
            noDelay = noDelay_;
            sendTimeout = sendTimeout_;
        }
        #endregion

        #region Connect Close
        public IAsyncResult Connect(IPAddress remoteIPAddress, int remotePort)
        {
            if(Connected)
            {
                this.Close();
            }
            
            _client = new TcpClient();
            _client.NoDelay = noDelay;
            _client.SendTimeout = sendTimeout;

            Addresses = remoteIPAddress;
            Port = remotePort;

            return _client.BeginConnect(Addresses, Port, HandleTcpServerConnected, _client);
        }

        public void Close()
        {
            if (Connected)
            {
                _client.Close();
                _client = null;
            }
        }
        #endregion

        #region Connected And Receive
        // 处理客户端连接的函数
        private void HandleTcpServerConnected(IAsyncResult ar)
        {
            try
            {
                _client.EndConnect(ar);
                RaiseServerConnected();
            }
            catch (Exception)
            {
                //Log.OutputLine("HandleTcpServerConnected:" + ex.Message);
                RaiseServerRefused();
                return;
            }

            recvBuffer = new byte[_client.ReceiveBufferSize];
            _client.GetStream().BeginRead(recvBuffer, 0, recvBuffer.Length, HandleDataReceived, recvBuffer);
        }

        // 数据接受回调函数
        private void HandleDataReceived(IAsyncResult ar)
        {
            if (_client == null || !_client.Connected)
            {
                return;
            }

            NetworkStream stream = _client.GetStream();

            int numberOfReadBytes = 0;
            try
            {
                numberOfReadBytes = stream.EndRead(ar);
            }
            catch (Exception ex)
            {
                Tools.LogHelper.WriteDebug("TCPClient.ReceiveCallback()...Exception: ", ex);
                return;
            }

            if (numberOfReadBytes == 0)
            {
                Disconnected();
                return;
            }

            byte[] buffer = (byte[])ar.AsyncState;
            byte[] receivedBytes = new byte[numberOfReadBytes];
            Buffer.BlockCopy(buffer, 0, receivedBytes, 0, numberOfReadBytes);

            RaiseDataReceived(_client, receivedBytes);
            try
            {
                stream.BeginRead(recvBuffer, 0, recvBuffer.Length, HandleDataReceived, recvBuffer);
            }
            catch (Exception ex)
            {
                Tools.LogHelper.WriteDebug("TCPClient.HandleDataReceived()...Exception: ", ex);
                //Log.OutputLine(ex.Message);
            }
        }

        private void Disconnected()
        {
            if (Connected)
            {
                _client.Close();
                _client = null;
                RaiseServerDisconnected();
            }
        }

        public void Send(byte[] datagram)
        {
            if (datagram == null)
                throw new ArgumentNullException("datagram");

            if (!Connected)
            {
                return;
                //RaiseServerDisconnected();
                //throw new InvalidProgramException(
                //  "This client has not connected to server.");
            }

            _client.GetStream().BeginWrite(
              datagram, 0, datagram.Length, SendDataEnd, _client);
        }

        // 发送数据完成处理函数
        private void SendDataEnd(IAsyncResult ar)
        {
            try
            {
                TcpClient tcpClient = ar.AsyncState as TcpClient;
                if(tcpClient != null || !tcpClient.Connected)
                {
                    return;
                }

                //((TcpClient)ar.AsyncState).GetStream().EndWrite(ar);
                tcpClient.GetStream().EndWrite(ar);
                RaiseCompletedSend();
            }
            catch(Exception ex)
            {
                Tools.LogHelper.WriteDebug("SendDataEnd()...Exception: ", ex);
            }
        }
        #endregion

        #region 事件
        /// 与服务端的连接已建立事件
        public event EventHandler ServerConnected;
        /// 与服务端的连接被拒绝
        public event EventHandler ConnectionRefused;
        /// 与服务端的连接已断开事件
        public event EventHandler ServerDisconnected;

        /// 触发服务端连接事件
        private void RaiseServerConnected()
        {
            if (ServerConnected != null)
            {
                ServerConnected(this, EventArgs.Empty);
            }
        }

        /// 触发服务端拒绝事件
        private void RaiseServerRefused()
        {
            if (ConnectionRefused != null)
            {
                ConnectionRefused(this, EventArgs.Empty);
            }
        }

        /// 触发服务端连接断开事件
        private void RaiseServerDisconnected()
        {
            //Log.OutputLine("RaiseServerDisconnected()...");
            if (ServerDisconnected != null)
            {
                ServerDisconnected(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 接收到数据事件
        /// </summary>
        //public event EventHandler DatagramReceived;
        public event EventHandler<TcpClientDatagramReceivedEventArgs<byte[]>> DatagramReceived;

        private void RaiseDataReceived(TcpClient sender, byte[] datagram)
        {
            if (DatagramReceived != null)
            {
                DatagramReceived(this, new TcpClientDatagramReceivedEventArgs<byte[]>(sender, datagram));
            }
        }

        /// <summary>
        /// 数据发送完毕事件
        /// </summary>
        public event EventHandler CompletedSend;

        /// <summary>
        /// 触发数据发送完毕的事件
        /// </summary>
        /// <param name="state"></param>
        private void RaiseCompletedSend()
        {
            if (CompletedSend != null)
            {
                CompletedSend(this, EventArgs.Empty);
            }
        }
        #endregion

        #region 释放
        /// <summary>
        /// Performs application-defined tasks associated with freeing, 
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release 
        /// both managed and unmanaged resources; <c>false</c> 
        /// to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    try
                    {
                        Close();

                        if (_client != null)
                        {
                            _client = null;
                        }
                    }
                    catch (SocketException)
                    {
                        //TODO
                        //RaiseOtherException(null);
                    }
                }
                disposed = true;
            }
        }
        #endregion
    }

    public class TcpClientDatagramReceivedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// 接收到数据报文事件参数
        /// </summary>
        /// <param name="tcpClient">客户端</param>
        /// <param name="datagram">报文</param>
        public TcpClientDatagramReceivedEventArgs(TcpClient tcpClient, T datagram)
        {
            TcpClient = tcpClient;
            Datagram = datagram;
        }

        /// <summary>
        /// 客户端
        /// </summary>
        public TcpClient TcpClient { get; private set; }
        /// <summary>
        /// 报文
        /// </summary>
        public T Datagram { get; private set; }
    }
}
