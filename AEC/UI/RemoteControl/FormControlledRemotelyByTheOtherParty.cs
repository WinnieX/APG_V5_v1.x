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
    public partial class FormControlledRemotelyByTheOtherParty : Form
    {
        private readonly Localization localization = Config.GetInstance().localization;

        private RemoteClient remoteClient;
        //private EventBus eventBus
        private SynchronizationContext syncContext = null;

        public FormControlledRemotelyByTheOtherParty(RemoteClient remoteClient_)
        {
            InitializeComponent();
            this.title.BindingForm(this);
            this.InitFont();
            this.syncContext = SynchronizationContext.Current;
            this.remoteClient = remoteClient_;
            this.Reset();
            this.UpdateDisplayLanguage();
            this.chkAllowToBeControlled.isChecked = this.remoteClient.allowToBeControlled;
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

        private void InitFont()
        {
            this.Font = FontHelper.CreateFont10();
            this.title.Font = FontHelper.CreateFont10();
        }

        private void Binding()
        {
            this.networkConnectionStatus.Binding(this.remoteClient);
            this.remoteClient.eventBus.RegisterHandler(ClientEventBusHandler);
        }

        private void Unbind()
        {
            this.networkConnectionStatus.Unbind();
        }

        private void FormControlledRemotelyByTheOtherParty_Load(object sender, EventArgs e)
        {
            Binding();
            this.remoteClient.eventBus.NotifyObeserver(new RCOperationStatusChangedArgs(true));
            this.networkConnectionStatus.Connect();
        }

        private void FormControlledRemotelyByTheOtherParty_FormClosing(object sender, FormClosingEventArgs e)
        {
            Unbind();
            if (this.remoteClient.Connected)
            {
                this.remoteClient.DisconnectFromServer();
            }
            this.remoteClient.eventBus.NotifyObeserver(new RCOperationStatusChangedArgs(false));
        }

        private void FormControlledRemotelyByTheOtherParty_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.remoteClient.eventBus.RemoveHandler(ClientEventBusHandler);
        }

        private void ClientEventBusHandler(object sender, EventBusArgs e)
        {
            syncContext.Post(ClientEventBusCallback, e);
        }

        private void ClientEventBusCallback(object state)
        {
            EventBusArgs e = state as EventBusArgs;
            if (e.type == EventType.RCHandshakeCompleted)    //握手完成
            {
                RCHandshakeCompletedArgs hsc = e as RCHandshakeCompletedArgs;
                if (hsc.success)
                {
                    this.txtLocalIdCode.Text = remoteClient.ClientIdCode;
                    this.txtLocalVCode.Text = remoteClient.VCode;
                    this.chkDisconnect.Enabled = false;
                }
            }
            else if (e.type == EventType.RCDisconnectFromServer)    //主动断开连接
            {
                Reset();
            }
            else if (e.type == EventType.RCDisconnectedFromServer)  //被动断开连接
            {
                Reset();
            }
            else if (e.type == EventType.RCLcConnectToRcToRcResp)   //服务端到本地客户端响应
            {
                RCLcConnectToRcToRcRespArgs resp = e as RCLcConnectToRcToRcRespArgs;
                this.chkDisconnect.isChecked = true;
                this.chkDisconnect.Enabled = true;
                //MessageBox.Show(string.Format("连接到远程客户端:{0}, IP:{1}", localClient.targetIDCode, localClient.targetIP));
                AEC.Tools.LogHelper.WriteInfo("Remote Client Connect To:{0}, IP:{1}", this.remoteClient.sourceIDCode, remoteClient.sourceIP);
                //this.btnDisconnectRC.Text = "断开远程控制连接";
            }
            else if (e.type == EventType.RCDisconnectFromRc)        //主动断开远程连接
            {
                this.chkDisconnect.Text = localization[Localization.formControlledRemotely_DisconnectRemoteControl];
                this.chkDisconnect.isChecked = false;
            }
            else if (e.type == EventType.RCDisconnectedFromRc)      //被动断开远程连接
            {
                this.chkDisconnect.Text = localization[Localization.formControlledRemotely_DisconnectRemoteControl];
                this.chkDisconnect.isChecked = false;
                this.chkDisconnect.Enabled = false;
                AEC.Tools.LogHelper.WriteInfo("Remote connection was disconnected!");
            }
        }

        private void Reset()
        {
            this.txtLocalIdCode.Text = RCClient.unknownIdCode;
            this.txtLocalVCode.Text = RCClient.unknownVCode;
            this.chkDisconnect.isChecked = false;
            this.chkDisconnect.Enabled = false;
        }

        private void UpdateDisplayLanguage()
        {
            this.Text = localization[Localization.formControlledRemotely];
            this.lblLocalId.Text = localization[Localization.formControlledRemotely_LocalId];
            this.lblLocalVCode.Text = localization[Localization.formControlledRemotely_LocalVerificationCode];
            this.chkDisconnect.Text = localization[Localization.formControlledRemotely_DisconnectRemoteControl];
            this.chkAllowToBeControlled.Text = localization[Localization.formControlledRemotely_AllowToBeControlled];
        }

        private void chkAllowToBeControlled_CheckedChanged(object sender, EventArgs e)
        {
            this.remoteClient.allowToBeControlled = this.chkAllowToBeControlled.isChecked;
        }

        private void chkDisconnect_MouseClick(object sender, MouseEventArgs e)
        {
            this.remoteClient.DisconnectFromRemoteClient();
            this.chkDisconnect.Text = localization[Localization.formControlledRemotely_DisconnectingRemoteControl];
            this.chkDisconnect.Enabled = false;
        }
    }
}
