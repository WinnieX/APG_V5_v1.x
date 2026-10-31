using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using AEC.Misc;

namespace AEC.UI.RemoteControl
{
    public partial class FormRCSettings : Form
    {
        private readonly AEC.RemoteControl.RCConfig rcConfig = AEC.RemoteControl.RCClient.config;
        private static readonly Localization localization = Config.GetInstance().localization;

        public FormRCSettings()
        {
            InitializeComponent();
            this.title.BindingForm(this);
            this.UpdateDisplayLanguage();
            this.InitFont();
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

        private void InitUI()
        {
            this.txtIPAddress.Text = rcConfig.serverIP;
            this.txtPort.Text = rcConfig.serverPort.ToString();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.btnClose))
            {
                this.Close();
            }
            else if (sender.Equals(this.btnRestoreToDefault))
            {
                RestoreToDefault();
            }
        }

        private void FormRCSettings_Load(object sender, EventArgs e)
        {
            this.InitUI();
        }

        private void RestoreToDefault()
        {
            rcConfig.RestoreToDefault();
            InitUI();
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            if(sender.Equals(this.txtIPAddress))
            {
                string ip = this.txtIPAddress.Text.Trim();
                IPAddress ipAddress;
                this.lblIPError.Visible = !IPAddress.TryParse(ip, out ipAddress);
            }
            else if(sender.Equals(this.txtPort))
            {
                string portNum = this.txtPort.Text.Trim();
                ushort port = 0;
                this.lblPortError.Visible = !ushort.TryParse(portNum, out port);
            }
        }

        private void FormRCSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            string ip = this.txtIPAddress.Text.Trim();
            IPAddress ipAddress;
            if(IPAddress.TryParse(ip, out ipAddress))
            {
                this.rcConfig.serverIP = ip;
            }
            string portNum = this.txtPort.Text.Trim();
            ushort port = 0;
            if(ushort.TryParse(portNum, out port))
            {
                this.rcConfig.serverPort = port;
            }
        }

        private void UpdateDisplayLanguage()
        {
            this.Text = localization[Localization.formRCSettings];
            this.lblIPAddress.Text = localization[Localization.formRCSettings_ServerIPAddress];
            this.lblPort.Text = localization[Localization.formRCSettings_ServerPort];
            this.lblIPError.Text = localization[Localization.formRCSettings_EnterCorrentIP];
            this.lblPortError.Text = localization[Localization.formRCSettings_EnterCorrentPort];
            this.btnRestoreToDefault.Text = localization[Localization.formRCSettings_RestoreToDefault];
            this.btnClose.Text = localization[Localization.formRCSettings_Close];
        }
    }
}
