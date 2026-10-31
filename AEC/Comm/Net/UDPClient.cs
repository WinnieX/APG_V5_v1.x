using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace AEC.Comm.Net
{
    public class UDPClient : IDisposable
    {
        public const int DefBindPort = 0;
        public int bindPort
        {
            get;
            private set;
        }

        private bool _socketIsValid = false;

        //public int 

        #region Fields
        private System.Net.Sockets.UdpClient _client = null;
        private bool disposed = false;
        #endregion

        public UDPClient()
        {
            this.bindPort = DefBindPort;
        }

        public void Init(int bindPort = DefBindPort)
        {
            this.InitClient(new IPEndPoint(IPAddress.Any, bindPort));
        }

        public void Init(IPAddress ipAddress)
        {
            this.InitClient(new IPEndPoint(ipAddress, DefBindPort));
        }

        private void InitClient(IPEndPoint localEP)
        {
            this._client = new UdpClient(localEP);

            this._socketIsValid = true;

            this.bindPort = (this._client.Client.LocalEndPoint as IPEndPoint).Port;

            this._client.EnableBroadcast = true;

            this._client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, true);

            this._client.BeginReceive(new AsyncCallback(ReceiveCallback), this._client);
        }

        public void Close()
        {
            this._socketIsValid = false;

            if (this._client != null)
            {
                this._client.Close();
                this._client = null;
            }

            this.bindPort = DefBindPort;
        }

        // 接收回调函数
        private void ReceiveCallback(IAsyncResult iar)
        {
            if (!this._socketIsValid || this._client == null || this._client.Client == null)
            {
                return;
            }

            if (iar.IsCompleted)
            {
                IPEndPoint remoteEndPoint = null;
                byte[] receivedBytes = null;

                try
                {
                    receivedBytes = this._client.EndReceive(iar, ref remoteEndPoint);
                }
                catch (Exception ex)
                {
                    Tools.LogHelper.WriteDebug("UDPClient.ReceiveCallback()...Exception: ", ex);
                    return;
                }

                if (receivedBytes == null || remoteEndPoint == null)
                {
                    return;
                }

                this.RaiseDataReceived(remoteEndPoint, receivedBytes);

                try
                {
                    this._client.BeginReceive(new AsyncCallback(ReceiveCallback), this._client);
                }
                catch (Exception ex)
                {
                    Tools.LogHelper.WriteDebug("UDPClient.ReceiveCallback()...Exception: ", ex);
                }
            }
        }

        // 发送函数
        public void SyncSendData(IPEndPoint remoteEndPoint, byte[] datagram)
        {
            System.Diagnostics.Debug.Assert(remoteEndPoint != null && datagram != null);

            if(this._client != null)
            {
                this._client.Send(datagram, datagram.Length, remoteEndPoint);
            }
        }

        public void SendData(IPEndPoint remoteEndPoint, byte[] datagram)
        {
            System.Diagnostics.Debug.Assert(remoteEndPoint != null && datagram != null);

            if (this._client != null)
            {
                this._client.BeginSend(datagram, datagram.Length, remoteEndPoint, new AsyncCallback(SendCallback), this._client);
            }
        }

        public void Broadcast(int port, byte[] datagram)
        {
            System.Diagnostics.Debug.Assert(datagram != null);

            if (this._client != null)
            {
                this._client.BeginSend(datagram, datagram.Length, new IPEndPoint(IPAddress.Broadcast, port), new AsyncCallback(SendCallback), this._client);
            }
        }

        // 发送回调函数
        private void SendCallback(IAsyncResult iar)
        {
            try
            {
                System.Net.Sockets.UdpClient client = iar.AsyncState as System.Net.Sockets.UdpClient;
                if (client != null)
                {
                    client.EndSend(iar);
                    RaiseCompletedSend();
                }
            }
            catch (Exception ex)
            {
                Tools.LogHelper.WriteDebug("SendCallback()...Exception: ", ex);
            }
        }

        #region 事件        
        /// <summary>
        /// 接收到数据事件
        /// </summary>
        //public event EventHandler DatagramReceived;
        public event EventHandler<UdpClientDatagramReceivedEventArgs<byte[]>> DatagramReceived;

        private void RaiseDataReceived(IPEndPoint remoteEndPoint, byte[] datagram)
        {
            if (DatagramReceived != null)
            {
                DatagramReceived(this, new UdpClientDatagramReceivedEventArgs<byte[]>(remoteEndPoint, datagram));
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
                        if (_client != null)
                        {
                            this._client.Close();
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

    public class UdpClientDatagramReceivedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// 接收到数据报文事件参数
        /// </summary>
        /// <param name="tcpClient">客户端</param>
        /// <param name="datagram">报文</param>
        public UdpClientDatagramReceivedEventArgs(IPEndPoint remoteEndPoint, T datagram)
        {
            this.remoteEndPoint = remoteEndPoint;
            Datagram = datagram;
        }

        /// <summary>
        /// 客户端
        /// </summary>
        public IPEndPoint remoteEndPoint { get; private set; }
        /// <summary>
        /// 报文
        /// </summary>
        public T Datagram { get; private set; }
    }
}
