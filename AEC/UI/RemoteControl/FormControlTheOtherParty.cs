using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using AEC.Core;
using AEC.RemoteControl;
using AEC.RemoteControl.Utility;
using AEC.Misc;

namespace AEC.UI.RemoteControl
{
    public partial class FormControlTheOtherParty : Form
    {
        private enum ConnectionStatus
        {
            Connect,
            Connecting,
            Disconnect,
            Disconnecting,
        }

        private readonly Localization localization = Config.GetInstance().localization;

        private ConnectionStatus status = ConnectionStatus.Connect;
        private LocalClient localClient;
        private SynchronizationContext syncContext = null;

        public FormControlTheOtherParty(LocalClient localClient_)
        {
            InitializeComponent();
            this.title.BindingForm(this);
            this.InitFont();
            this.syncContext = SynchronizationContext.Current;
            this.localClient = localClient_;
            this.Reset();
        }

        private void InitFont()
        {
            this.Font = FontHelper.CreateFont10();
            this.title.Font = FontHelper.CreateFont10();
        }

        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.title.Text = value;
            }
        }

        private void Binding()
        {
            this.networkConnectionStatus.Binding(this.localClient);
            this.localClient.eventBus.RegisterHandler(ClientEventBusHandler);
        }

        private void Unbind()
        {
            this.networkConnectionStatus.Unbind();
        }

        private void FormControlTheOtherParty_Load(object sender, EventArgs e)
        {
            Binding();
            UpdateDisplayLanguage();
            this.localClient.eventBus.NotifyObeserver(new RCOperationStatusChangedArgs(true));
            this.localization.langEventBus.RegisterHandler(LangEventBusHandler);
            this.networkConnectionStatus.Connect();
        }

        private void FormControlTheOtherParty_FormClosing(object sender, FormClosingEventArgs e)
        {
            Unbind();
            if (this.localClient.Connected)
            {
                this.localClient.DisconnectFromServer();
            }
            this.localClient.eventBus.NotifyObeserver(new RCOperationStatusChangedArgs(false));

        }

        private void FormControlTheOtherParty_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.localClient.eventBus.RemoveHandler(ClientEventBusHandler);
            this.localization.langEventBus.RemoveHandler(LangEventBusHandler);
        }

        private void LangEventBusHandler(object sender, EventBusArgs e)
        {
            UpdateDisplayLanguage();
        }

        private void ClientEventBusHandler(object sender, EventBusArgs e)
        {
            syncContext.Post(ClientEventBusCallback, e);
        }

        private void ClientEventBusCallback(object state)
        {
            EventBusArgs e = state as EventBusArgs;
            if (e.type == EventType.RCHandshakeCompleted)   //握手完成
            {
                RCHandshakeCompletedArgs hsc = e as RCHandshakeCompletedArgs;
                if (hsc.success)
                {
                    this.chkRCConnect.isChecked = false;
                    this.chkRCConnect.Enabled = true;
                }
            }
            else if (e.type == EventType.RCDisconnectFromServer)        //主动断开连接
            {
                Reset();
            }
            else if (e.type == EventType.RCDisconnectedFromServer)      //被动断开连接
            {
                Reset();
                //var args = e as RCDisconnectedFromServerArgs;
                //if(!args.rcConnected)
                //{
                //    gtMessageBox.ShowMessage(this, args.message, localization[Localization.formControlTheOtherParty_RemoteControl], MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            else if (e.type == EventType.RCLcConnectToRcToLcResp)       //服务端到本地客户端响应
            {
                RCLcConnectToRcToLcRespArgs resp = e as RCLcConnectToRcToLcRespArgs;
                this.chkRCConnect.isChecked = resp.success;
                this.chkRCConnect.Enabled = true;
                if (resp.success)
                {
                    //MessageBox.Show(string.Format("连接到远程客户端:{0}, IP:{1}", localClient.targetIDCode, localClient.targetIP));
                    AEC.Tools.LogHelper.WriteInfo("Connect To Remote Client:{0}, IP:{1}", localClient.targetIDCode, localClient.targetIP);
                    UpdateStatus(ConnectionStatus.Disconnect);
                    //this.btnRCConnect.Text = "断开远程控制";
                }
                else
                {
                    UpdateStatus(ConnectionStatus.Connect);
                    //this.btnRCConnect.Text = "远程控制";
                    gtMessageBox.ShowMessage(this, string.Format(localization[Localization.formControlTheOtherParty_FailedToConnectToRemoteClient], localClient.targetIDCode, resp.message), localization[Localization.formControlTheOtherParty_RemoteControl], MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (e.type == EventType.RCDisconnectFromRc)        //主动断开远程连接
            {
                UpdateStatus(ConnectionStatus.Connect);
                //this.btnRCConnect.Text = "远程控制";
                this.chkRCConnect.isChecked =false;
                this.chkRCConnect.Enabled = true;
            }
            else if (e.type == EventType.RCDisconnectedFromRc)      //被动断开远程连接
            {
                RCDisconnectedFromRcArgs d = e as RCDisconnectedFromRcArgs;
                AEC.Tools.LogHelper.WriteInfo("Disconnect from the device:{0}", d.message);
                this.chkRCConnect.isChecked = false;
                this.chkRCConnect.Enabled = true;
                UpdateStatus(ConnectionStatus.Connect);
                this.Activate();
                gtMessageBox.ShowMessage(this, d.message, localization[Localization.formControlTheOtherParty_RemoteControl], MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Reset()
        {
            //this.btnRCConnect.Text = "远程控制";
            UpdateStatus(ConnectionStatus.Connect);
            this.chkRCConnect.isChecked = false;
            this.chkRCConnect.Enabled = false;
        }

        private void btnRemoteControl_Click(object sender, EventArgs e)
        {
            if (!this.localClient.Connected)
            {
                return;
            }

            if (!this.localClient.rcConnected)
            {
                uint targetId;
                if (!uint.TryParse(this.txtRemoteIdCode.Text, out targetId) ||
                   !SignatureCodeHelper.CheckCode(targetId))
                {
                    gtMessageBox.ShowMessage(this, localization[Localization.formControlTheOtherParty_IdCodeError], localization[Localization.formControlTheOtherParty_RemoteControl], MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                uint VCode;
                if (!uint.TryParse(this.txtRemoteVCode.Text, out VCode) ||
                   !VerificationCodeHelper.CheckCode(VCode))
                {
                    gtMessageBox.ShowMessage(this, localization[Localization.formControlTheOtherParty_VerificationCodeError], localization[Localization.formControlTheOtherParty_RemoteControl], MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (targetId == this.localClient.clientId)
                {
                    gtMessageBox.ShowMessage(this, localization[Localization.formControlTheOtherParty_EnterNonLocalIdCode], localization[Localization.formControlTheOtherParty_RemoteControl], MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.localClient.ConnectToRemoteClient(targetId, VCode);
                this.chkRCConnect.Enabled = false;
                //this.btnRCConnect.Text = "正在连接中...";
                UpdateStatus(ConnectionStatus.Connecting);
            }
            else
            {
                this.localClient.DisconnectFromRemoteClient();
                //this.btnRCConnect.Text = "正在断开连接...";
                UpdateStatus(ConnectionStatus.Disconnecting);
                this.chkRCConnect.Enabled = false;
            }
        }

        private string GetStatusLabel(ConnectionStatus status)
        {
            Dictionary<ConnectionStatus, string> statusMap = new Dictionary<ConnectionStatus, string>()
            {
                {ConnectionStatus.Connect, localization[Localization.formControlTheOtherParty_RemoteControlConnect]},
                {ConnectionStatus.Connecting, localization[Localization.formControlTheOtherParty_RemoteControlConnecting]},
                {ConnectionStatus.Disconnect, localization[Localization.formControlTheOtherParty_DisconnectRemoteControl]},
                {ConnectionStatus.Disconnecting, string.Format(localization[Localization.formControlTheOtherParty_DisconnectingRemoteControl], RCClient.config.ServerInfo)},
            };

            if (statusMap.ContainsKey(status))
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
            this.UpdateBtnStatus();
        }

        private void UpdateBtnStatus()
        {
            this.chkRCConnect.Text = GetStatusLabel(this.status);
            this.chkRCConnect.UpdateDisplay();
        }

        private void UpdateDisplayLanguage()
        {
            this.Text = localization[Localization.formControlTheOtherParty];
            this.lblRemoteId.Text = localization[Localization.formControlTheOtherParty_RemoteId];
            this.lblRemoteVCode.Text = localization[Localization.formControlTheOtherParty_RemoteVerificationCode];
            this.UpdateBtnStatus();
        }

        private void chkDisconnect_MouseClick(object sender, MouseEventArgs e)
        {
            if (!this.localClient.Connected)
            {
                return;
            }

            if (!this.localClient.rcConnected)
            {
                uint targetId;
                if (!uint.TryParse(this.txtRemoteIdCode.Text, out targetId) ||
                   !SignatureCodeHelper.CheckCode(targetId))
                {
                    gtMessageBox.ShowMessage(this, localization[Localization.formControlTheOtherParty_IdCodeError], localization[Localization.formControlTheOtherParty_RemoteControl], MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                uint VCode;
                if (!uint.TryParse(this.txtRemoteVCode.Text, out VCode) ||
                   !VerificationCodeHelper.CheckCode(VCode))
                {
                    gtMessageBox.ShowMessage(this, localization[Localization.formControlTheOtherParty_VerificationCodeError], localization[Localization.formControlTheOtherParty_RemoteControl], MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (targetId == this.localClient.clientId)
                {
                    gtMessageBox.ShowMessage(this, localization[Localization.formControlTheOtherParty_EnterNonLocalIdCode], localization[Localization.formControlTheOtherParty_RemoteControl], MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.localClient.ConnectToRemoteClient(targetId, VCode);
                this.chkRCConnect.Enabled = false;
                //this.btnRCConnect.Text = "正在连接中...";
                UpdateStatus(ConnectionStatus.Connecting);
            }
            else
            {
                this.localClient.DisconnectFromRemoteClient();
                //this.btnRCConnect.Text = "正在断开连接...";
                UpdateStatus(ConnectionStatus.Disconnecting);
                this.chkRCConnect.Enabled = false;
            }
        }
    }
}
