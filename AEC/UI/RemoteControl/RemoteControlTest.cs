using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.RemoteControl;
using AEC.RemoteControl.Utility;
using System.Threading;
using AEC.Core;

namespace AEC.UI.RemoteControl
{
    public partial class RemoteControlTest : Form
    {
        private EventBus localEventBus = new EventBus();
        private EventBus remoteEventBus = new EventBus();

        private LocalClient localClient;
        private RemoteClient remoteClient;

        private SynchronizationContext syncContext = null;

        private const string unknownIdCode = "------";
        private const string unknownVCode = "----";

        public RemoteControlTest()
        {
            InitializeComponent();

            syncContext = SynchronizationContext.Current;

            localClient = new LocalClient(localEventBus, null);
            remoteClient = new RemoteClient(remoteEventBus, null);

            this.localClient.eventBus.RegisterHandler(LocalClientEventBusHandler);
            this.remoteClient.eventBus.RegisterHandler(RemoteClientEventBusHandler);

            this.LocalDisconnectedHandler();
            this.RemoteDisconnectedHandler();
        }

        private void LocalClientEventBusHandler(object sender, EventBusArgs e)
        {
            syncContext.Post(LocalClientEventBusCallback, e);
        }

        private void RemoteClientEventBusHandler(object sender, EventBusArgs e)
        {
            syncContext.Post(RemoteClientEventBusCallback, e);
        }

        private void LocalClientEventBusCallback(object state)
        {
            EventBusArgs e = state as EventBusArgs;
            if(e.type == EventType.RCMessage)
            {
                AEC.Tools.LogHelper.WriteInfo((e as RCMessageArgs).message);
            }
            else if(e.type == EventType.RCHandshakeCompleted)
            {
                RCHandshakeCompletedArgs hsc = e as RCHandshakeCompletedArgs;
                this.btnLocalConnectToServer.Enabled = true;
                if (hsc.success)
                {
                    LocalConnectedHandler();
                }
                else
                {
                    LocalDisconnectedHandler();
                    AEC.Tools.LogHelper.WriteInfo(hsc.message);
                }
            }
            else if(e.type == EventType.RCDisconnectFromServer)
            {
                LocalDisconnectedHandler();
            }
            else if (e.type == EventType.RCDisconnectedFromServer)
            {
                RCDisconnectedFromServerArgs d = e as RCDisconnectedFromServerArgs;
                LocalDisconnectedHandler();
                if(string.IsNullOrEmpty(d.message))
                {
                    AEC.Tools.LogHelper.WriteInfo(d.message);
                }
            }
            else if (e.type == EventType.RCLcConnectToRcToLcResp)
            {
                RCLcConnectToRcToLcRespArgs resp = e as RCLcConnectToRcToLcRespArgs;
                this.btnConnectRemoteClient.Enabled = true;
                if(resp.success)
                {
                    //MessageBox.Show(string.Format("连接到远程客户端:{0}, IP:{1}", localClient.targetIDCode, localClient.targetIP));
                    AEC.Tools.LogHelper.WriteInfo("Connect to Remote Clinet:{0}, IP:{1}", localClient.targetIDCode, localClient.targetIP);
                    this.btnConnectRemoteClient.Text = "断开远程控制连接";
                    this.buttonSwapData.Enabled = true;
                }
                else
                {
                    MessageBox.Show(string.Format("Connect to Remote Clinet {0} Failed:{1}", localClient.targetIDCode, resp.message));
                    this.btnConnectRemoteClient.Text = "远程控制";
                    this.buttonSwapData.Enabled = false;
                }
            }
            else if(e.type == EventType.RCDisconnectFromRc)
            {
                this.btnConnectRemoteClient.Text = "远程控制";
                this.btnConnectRemoteClient.Enabled = true;
                this.buttonSwapData.Enabled = false;
            }
            else if (e.type == EventType.RCDisconnectedFromRc)
            {
                RCDisconnectedFromRcArgs d = e as RCDisconnectedFromRcArgs;
                AEC.Tools.LogHelper.WriteInfo("Disconnect from the device:{0}", d.message);
                this.btnConnectRemoteClient.Enabled = true;
                this.btnConnectRemoteClient.Text = "远程控制";
                this.buttonSwapData.Enabled = false;
                //MessageBox.Show("remote client断开远程连接");
            }
        }

        private void RemoteClientEventBusCallback(object state)
        {
            EventBusArgs e = state as EventBusArgs;
            if (e.type == EventType.RCMessage)
            {
                AEC.Tools.LogHelper.WriteInfo((e as RCMessageArgs).message);
            }
            else if (e.type == EventType.RCHandshakeCompleted)
            {
                RCHandshakeCompletedArgs hsc = e as RCHandshakeCompletedArgs;
                this.btnRemoteConnectToServer.Enabled = true;
                if (hsc.success)
                {
                    this.RemoteConnectedHandler();
                }
                else
                {
                    this.RemoteDisconnectedHandler();
                    AEC.Tools.LogHelper.WriteInfo(hsc.message);
                }
            }
            else if (e.type == EventType.RCDisconnectFromServer)
            {
                RemoteDisconnectedHandler();
            }
            else if (e.type == EventType.RCDisconnectedFromServer)
            {
                RCDisconnectedFromServerArgs d = e as RCDisconnectedFromServerArgs;
                this.RemoteDisconnectedHandler();
                if (string.IsNullOrEmpty(d.message))
                {
                    AEC.Tools.LogHelper.WriteInfo(d.message);
                }
            }
            else if (e.type == EventType.RCLcConnectToRcToRcResp)
            {
                RCLcConnectToRcToRcRespArgs resp = e as RCLcConnectToRcToRcRespArgs;
                this.btnDisconnectRC.Enabled = true;
                //MessageBox.Show(string.Format("连接到远程客户端:{0}, IP:{1}", localClient.targetIDCode, localClient.targetIP));
                AEC.Tools.LogHelper.WriteInfo("Remote Client Connect to:{0}, IP:{1}", this.remoteClient.sourceIDCode, remoteClient.sourceIP);
                //this.btnDisconnectRC.Text = "断开远程控制连接";
            }
            //else if (e.type == EventType.RCDisconnectFromRc)
            //{
            //    this.btnConnectRemoteClient.Text = "远程控制";
            //    this.btnConnectRemoteClient.Enabled = true;
            //}
            else if(e.type == EventType.RCDisconnectedFromRc)
            {
                RCDisconnectedFromRcArgs d = e as RCDisconnectedFromRcArgs;
                AEC.Tools.LogHelper.WriteInfo("Disconnect from the device:{0}", d.message);
                this.btnDisconnectRC.Enabled = false;
                //MessageBox.Show("local client断开远程连接");
            }
        }

        private void RemoteDisconnectedHandler()
        {
            this.btnRemoteConnectToServer.Text = "连接到服务器";
            this.txtNativeIdCode.Text = unknownIdCode;
            this.txtNativeVC.Text = unknownVCode;
            this.chkAllowed.Checked = true;
            this.chkAllowed.Enabled = false;
            this.btnUpdateVCode.Enabled = false;
            this.btnDisconnectRC.Enabled = false;
        }

        private void RemoteConnectedHandler()
        {
            this.btnRemoteConnectToServer.Text = "断开连接";
            this.txtNativeIdCode.Text = this.remoteClient.ClientIdCode;
            this.txtNativeVC.Text = this.remoteClient.VCode;
            this.chkAllowed.Enabled = true;
            this.btnUpdateVCode.Enabled = true;
        }

        private void LocalDisconnectedHandler()
        {
            this.btnLocalConnectToServer.Text = "连接到服务器";
            this.btnConnectRemoteClient.Enabled = false;
            this.txtLocalIdCode.Text = unknownIdCode;
            this.buttonQueryServer.Enabled = false;
        }

        private void LocalConnectedHandler()
        {
            this.btnLocalConnectToServer.Text = "断开连接";
            this.btnConnectRemoteClient.Enabled = true;
            this.txtLocalIdCode.Text = localClient.ClientIdCode;
            this.buttonQueryServer.Enabled = true;
        }

        //private void client_ConnectionCompleted(object sender, EventArgs e)
        //{
        //    if(sender.Equals(this.localClient))
        //    {
        //        syncContext.Post(LocalConnectionCompleted, null);
        //    }
        //    else if(sender.Equals(this.remoteClient))
        //    {
        //        syncContext.Post(RemoteConnectionCompleted, null);
        //    }
        //}

        //private void LocalConnectionCompleted(object state)
        //{
        //    this.btnLocalConnectToServer.Enabled = true;
        //    this.btnLocalConnectToServer.Text = "断开连接";
        //    this.btnLocalHandshake.Enabled = true;
        //}

        //private void RemoteConnectionCompleted(object state)
        //{
        //    this.btnRemoteConnectToServer.Enabled = true;
        //    this.btnRemoteConnectToServer.Text = "断开连接";
        //    this.btnRemoteHandshake.Enabled = true;
        //}

        //private void client_ConnectionRefused(object sender, EventArgs e)
        //{
        //    //Console.WriteLine("连接被拒绝");
        //    if (sender.Equals(this.localClient))
        //    {
        //        syncContext.Post(LocalConnectionRefused, null);
        //    }
        //    else if (sender.Equals(this.remoteClient))
        //    {
        //        syncContext.Post(RemoteConnectionRefused, null);
        //    }
            
        //}

        //private void LocalConnectionRefused(object state)
        //{
        //    this.btnLocalConnectToServer.Text = "连接到服务器";
        //    this.btnLocalConnectToServer.Enabled = true;
        //    this.btnLocalHandshake.Enabled = false;
        //}

        //private void RemoteConnectionRefused(object state)
        //{
        //    this.btnRemoteConnectToServer.Text = "连接到服务器";
        //    this.btnRemoteConnectToServer.Enabled = true;
        //    this.btnRemoteHandshake.Enabled = false;
        //}

        //private void client_Disconnected(object sender, EventArgs e)
        //{
        //    //Console.WriteLine("连接被断开");
        //    //syncContext.Post(Disconnected, null);
        //    if (sender.Equals(this.localClient))
        //    {
        //        syncContext.Post(LocalDisconnected, null);
        //    }
        //    else if (sender.Equals(this.remoteClient))
        //    {
        //        syncContext.Post(RemoteDisconnected, null);
        //    }
        //}

        //private void LocalDisconnected(object state)
        //{
        //    this.btnLocalConnectToServer.Text = "连接到服务器";
        //    this.btnLocalConnectToServer.Enabled = true;
        //    this.btnLocalHandshake.Enabled = false;
        //}

        //private void RemoteDisconnected(object state)
        //{
        //    this.btnRemoteConnectToServer.Text = "连接到服务器";
        //    this.btnRemoteConnectToServer.Enabled = true;
        //    this.btnRemoteHandshake.Enabled = false;
        //}

        private void btnConnectDevice_Click(object sender, EventArgs e)
        {
            if(sender.Equals(this.btnLocalConnectToServer))
            {
                if (this.localClient.Connected)
                {
                    this.localClient.DisconnectFromServer();
                    //this.btnLocalConnectToServer.Text = "连接到服务器";
                }
                else
                {
                    this.localClient.ConnectToServer();
                    this.btnLocalConnectToServer.Text = "正在连接...";
                    this.btnLocalConnectToServer.Enabled = false;
                }
            }
            else if(sender.Equals(this.btnRemoteConnectToServer))
            {
                if (this.remoteClient.Connected)
                {
                    this.remoteClient.DisconnectFromServer();
                    //this.btnRemoteConnectToServer.Text = "连接到服务器";
                }
                else
                {
                    this.remoteClient.ConnectToServer();
                    this.btnRemoteConnectToServer.Text = "正在连接...";
                    this.btnRemoteConnectToServer.Enabled = false;
                }
            }
        }

        private void chkAllowed_CheckedChanged(object sender, EventArgs e)
        {
            this.remoteClient.allowToBeControlled = chkAllowed.Checked;
        }

        private void btnUpdateVCode_Click(object sender, EventArgs e)
        {
            this.remoteClient.UpdateVerificationCode();
            this.txtNativeVC.Text = this.remoteClient.VCode;
        }

        private void btnDisconnectRC_Click(object sender, EventArgs e)
        {
            this.remoteClient.DisconnectFromRemoteClient();
            this.btnDisconnectRC.Enabled = false;
        }

        private void btnRemoteControl_Click(object sender, EventArgs e)
        {
            if (!this.localClient.rcConnected)
            {
                uint targetId;
                uint VCode;
                if (!uint.TryParse(this.txtIdCode.Text, out targetId) ||
                   !uint.TryParse(this.txtVC.Text, out VCode) ||
                   !SignatureCodeHelper.CheckCode(targetId) ||
                   !VerificationCodeHelper.CheckCode(VCode))
                {
                    MessageBox.Show("输入有误！");
                    return;
                }

                if (!this.localClient.Connected)
                {
                    MessageBox.Show("无法连接到服务器！");
                    return;
                }

                if (targetId == this.localClient.clientId)
                {
                    MessageBox.Show("请输入非本机识别码!");
                    return;
                }

                this.localClient.ConnectToRemoteClient(targetId, VCode);
                this.btnConnectRemoteClient.Enabled = false;
                this.btnConnectRemoteClient.Text = "正在连接中...";
            }
            else
            {
                this.localClient.DisconnectFromRemoteClient();
                this.btnConnectRemoteClient.Text = "正在断开连接...";
                this.btnConnectRemoteClient.Enabled = false;
            }
        }

        private void buttonQueryServer_Click(object sender, EventArgs e)
        {
            localClient.QueryServerStatus();
        }

        private void buttonSwapData_Click(object sender, EventArgs e)
        {
            this.localClient.SwapDataWithRemoteClient();
        }
    }
}
