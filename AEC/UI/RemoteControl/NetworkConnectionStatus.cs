using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using AEC.Core;
using AEC.RemoteControl;
using AEC.Misc;

namespace AEC.UI.RemoteControl
{
    public partial class NetworkConnectionStatus : UserControl
    {
        private enum ConnectionStatus
        {
            NotConnected,
            Connecting,
            SuccessfullyConnected,
            FailedToConnect,
            Disconnect,
            ConnectionIsInterrupted,
        }

        private static readonly Localization localization = Config.GetInstance().localization;

        private ConnectionStatus status = ConnectionStatus.NotConnected;

        private RCClient client;
        private SynchronizationContext syncContext = null;

        public NetworkConnectionStatus()
        {
            InitializeComponent();
            this.syncContext = SynchronizationContext.Current;
            this.InitFont();
            this.Init();
            this.UpdateDisplayLanguage();
        }

        private void InitFont()
        {
            this.Font = FontHelper.CreateFont10();
        }

        private void Init()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.SetStyle(ControlStyles.StandardDoubleClick, false);

            this.BackColor = Color.Transparent;
        }

        public void Binding(RCClient client_)
        {
            this.client = client_;
            this.client.eventBus.RegisterHandler(ClientEventBusHandler);
        }

        public void Unbind()
        {
            if(this.client != null)
            {
                this.client.eventBus.RemoveHandler(ClientEventBusHandler);
            }
        }

        private void ClientEventBusHandler(object sender, EventBusArgs e)
        {
            syncContext.Post(ClientEventBusCallback, e);
        }

        private void ClientEventBusCallback(object state)
        {
            EventBusArgs e = state as EventBusArgs;
            if (e.type == EventType.RCConnectToServer)
            {
                //UpdateStatus("正在连接服务器...", RCClient.config.ServerInfo);
                UpdateStatus(ConnectionStatus.Connecting);
                this.linkLabelReconnect.Visible = false;
                this.statusLight.status = StatusLight.Status.Connecting;
            }
            else if (e.type == EventType.RCHandshakeCompleted)
            {
                RCHandshakeCompletedArgs hsc = e as RCHandshakeCompletedArgs;
                
                if (hsc.success)
                {
                    //UpdateStatus("连接服务器成功");
                    UpdateStatus(ConnectionStatus.SuccessfullyConnected);
                    this.statusLight.status = StatusLight.Status.Connected;
                }
                else
                {
                    //UpdateStatus("连接服务器 {0} 失败！", RCClient.config.ServerInfo);
                    UpdateStatus(ConnectionStatus.FailedToConnect);
                    this.linkLabelReconnect.Visible = true;
                    this.statusLight.status = StatusLight.Status.Unconnected;
                }
            }
            else if (e.type == EventType.RCDisconnectFromServer)
            {
                //UpdateStatus("断开与服务器连接");
                UpdateStatus(ConnectionStatus.Disconnect);
                this.linkLabelReconnect.Visible = true;
                this.statusLight.status = StatusLight.Status.Unconnected;
            }
            else if (e.type == EventType.RCDisconnectedFromServer)
            {
                //UpdateStatus("与服务器连接中断！");
                UpdateStatus(ConnectionStatus.ConnectionIsInterrupted);
                this.linkLabelReconnect.Visible = true;
                this.statusLight.status = StatusLight.Status.Unconnected;
            }
        }

        //private void UpdateStatus(string format, params object[] arg)
        //{
        //    this.lblStatus.Text = string.Format(format, arg);
        //    this.lblStatus.UpdateDisplay();
        //}

        private void linkLabelReconnect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Connect();
        }

        public void Connect()
        {
            if (this.client != null)
            {
                this.client.ConnectToServer();
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            FormRCSettings form = new FormRCSettings();
            form.ShowDialog();
        }

        private static string GetStatusLabel(ConnectionStatus status)
        {
            Dictionary<ConnectionStatus, string> statusMap = new Dictionary<ConnectionStatus,string>()
            {
                {ConnectionStatus.NotConnected, localization[Localization.panelNetworkConnectionStatus_NotConnectedToTheServer]},
                {ConnectionStatus.Connecting, localization[Localization.panelNetworkConnectionStatus_ConnectingToTheServer]},
                {ConnectionStatus.SuccessfullyConnected, localization[Localization.panelNetworkConnectionStatus_SuccessfullyConnectedToTheServer]},
                {ConnectionStatus.FailedToConnect, string.Format(localization[Localization.panelNetworkConnectionStatus_FailedToConnectToTheServer], RCClient.config.ServerInfo)},
                {ConnectionStatus.Disconnect, localization[Localization.panelNetworkConnectionStatus_DisconnectFromTheServer]},
                {ConnectionStatus.ConnectionIsInterrupted, localization[Localization.panelNetworkConnectionStatus_ConnectionToTheServerIsInterrupted]},
            };

            if(statusMap.ContainsKey(status))
            {
                return statusMap[status];
            }
            else
            {
                return string.Empty;
            }
        }

        private void UpdateStatus(ConnectionStatus connectionStatus)
        {
            this.status = connectionStatus;
            this.UpdateStatusLabel();
        }

        private void UpdateStatusLabel()
        {
            this.lblStatus.Text = GetStatusLabel(this.status);
            this.lblStatus.UpdateDisplay();
        }

        public void UpdateDisplayLanguage()
        {
            UpdateStatusLabel();
            this.linkLabelReconnect.Text = localization[Localization.panelNetworkConnectionStatus_ReconnectToTheServer];
        }
    }
}
