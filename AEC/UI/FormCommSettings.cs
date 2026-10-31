using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.Misc;
using AEC.UI.Skin;
using AEC.UI.Utility;

namespace AEC.UI
{
    public partial class FormCommSettings : Form
    {
        AEC.Misc.Config config = AEC.Misc.Config.GetInstance();
        
        public FormCommSettings()
        {
            InitializeComponent();
            this.title.BindingForm(this);
            InitFont();
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
            this.title.Font = FontHelper.CreateFont10();
            this.Font = FontHelper.CreateFont10();
            this.lblCommMethod.Font = FontHelper.CreateFont10();
            this.lblCommRetransmission.Font = FontHelper.CreateFont10();
            this.cmbCommRetransmission.Font = FontHelper.CreateFont10();
            this.lblDefaultSerialPort.Font = FontHelper.CreateFont10();
            this.cmbDefaultComPort.Font = FontHelper.CreateFont10();

            this.mSwitchCommViaUSB.Font = FontHelper.CreateFont10();
            this.mSwitchCommViaRS232.Font = FontHelper.CreateFont10();
            this.mSwitchCommViaUDP.Font = FontHelper.CreateFont10();
            this.mSwitchCommViaTCP.Font = FontHelper.CreateFont10();
            this.mSwitchSerialPortManualMode.Font = FontHelper.CreateFont10();
            
            this.lblIPAddress.Font = FontHelper.CreateFont10();
            this.lblPort.Font = FontHelper.CreateFont10();
            this.lblUDP.Font = FontHelper.CreateFont10();
            this.lblTCP.Font = FontHelper.CreateFont10();
            this.btnRestoreToDefault.Font = FontHelper.CreateFont10();
            this.btnClose.Font = FontHelper.CreateFont10();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            GDIHelper.FillRectangle(e.Graphics, this.ClientRectangle, SkinManager.CurrentSkin.proModeBackgroundColor);

            int borderWidth = 1;
            using (Pen border = new Pen(Color.FromArgb(96, 96, 96), borderWidth))
            {
                Point[] points = new Point[]
                {
                    new Point(borderWidth - 1, borderWidth - 1),
                    new Point(borderWidth - 1, this.Height - (borderWidth)),
                    new Point(this.Width - (borderWidth), this.Height - (borderWidth)),
                    new Point(this.Width - (borderWidth), borderWidth - 1),
                };
                e.Graphics.DrawLines(border, points);
            }
        }

        private void FormConnectionSetting_Load(object sender, EventArgs e)
        {
            InitUI();
            RegisterEventHandler();
            this.UpdateDisplayLanguage();

            this.txtUDPIPAddress.LostFocus += txt_LostFocus;
            this.txtUDPPort.LostFocus += txt_LostFocus;
            this.txtTCPIPAddress.LostFocus += txt_LostFocus;
            this.txtTCPPort.LostFocus += txt_LostFocus;
        }

        private void FormConnectionSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            RemoveEventHandler();
        }


        private void txt_LostFocus(object sender, EventArgs e)
        {
            if (sender.Equals(this.txtUDPIPAddress))
            {
                CheckUDPIPAddress();
            }
            else if (sender.Equals(this.txtUDPPort))
            {
                CheckUDPPort();
            }
            else if (sender.Equals(this.txtTCPIPAddress))
            {
                CheckTCPIPAddress();
            }
            else if (sender.Equals(this.txtTCPPort))
            {
                CheckTCPPort();
            }
        }

        private void CheckUDPIPAddress()
        {
            System.Net.IPAddress ipAddress;
            if (System.Net.IPAddress.TryParse(this.txtUDPIPAddress.Text.Trim(), out ipAddress))
            {
                var config = AEC.Misc.Config.GetInstance();
                config.udp_ip = ipAddress.ToString();
            }
        }

        private void CheckUDPPort()
        {
            ushort port = 0;
            if (ushort.TryParse(this.txtUDPPort.Text.Trim(), out port))
            {
                var config = AEC.Misc.Config.GetInstance();
                config.udp_port = port;
            }
        }

        private void CheckTCPIPAddress()
        {
            System.Net.IPAddress ipAddress;
            if (System.Net.IPAddress.TryParse(this.txtTCPIPAddress.Text.Trim(), out ipAddress))
            {
                var config = AEC.Misc.Config.GetInstance();
                config.tcp_ip = ipAddress.ToString();
            }
        }

        private void CheckTCPPort()
        {
            ushort port = 0;
            if (ushort.TryParse(this.txtTCPPort.Text.Trim(), out port))
            {
                var config = AEC.Misc.Config.GetInstance();
                config.tcp_port = port;
            }
        }

        private void InitUI()
        {
            this.mSwitchCommViaUSB.IsCheckedInit = config.connectionViaUSB;
            this.mSwitchCommViaRS232.IsCheckedInit = config.connectionViaRS232;
            this.mSwitchCommViaUDP.IsCheckedInit = config.connectionViaUDP;
            this.mSwitchCommViaTCP.IsCheckedInit = config.connectionViaTCP;

            string[] allPorts = AEC.Comm.CommDevices.SerialPort.GetValidPorts();
            cmbDefaultComPort.Items.Clear();
            cmbDefaultComPort.Items.AddRange(allPorts);
            cmbDefaultComPort.Text = config.default_SerialPort;

            if (this.cmbCommRetransmission.Items.Count == Config.MaxCommRetries + 1)
            {
                this.cmbCommRetransmission.SelectedIndex = config.commRetries;
            }
            else
            {
                this.cmbCommRetransmission.Enabled = false;
            }

            this.mSwitchSerialPortManualMode.IsCheckedInit = config.isSpManualMode;

            this.txtUDPIPAddress.Text = config.udp_ip;
            this.txtUDPPort.Text = config.udp_port.ToString();

            this.txtTCPIPAddress.Text = config.tcp_ip;
            this.txtTCPPort.Text = config.tcp_port.ToString();
        }

        private void RegisterEventHandler()
        {
            this.cmbDefaultComPort.TextChanged += cmbDefaultComPort_TextChanged;
            this.cmbCommRetransmission.SelectedIndexChanged += cmbCommRetransmission_SelectedIndexChanged;            
        }

        private void RemoveEventHandler()
        {
            this.cmbDefaultComPort.TextChanged -= cmbDefaultComPort_TextChanged;
            this.cmbCommRetransmission.SelectedIndexChanged -= cmbCommRetransmission_SelectedIndexChanged;
        }

        private void cmbDefaultComPort_TextChanged(object sender, EventArgs e)
        {
            string port_num = this.cmbDefaultComPort.Text.Trim().ToUpper();

            if (port_num.StartsWith(Config.comPortPrefix) && port_num.Length > Config.comPortPrefix.Length)
            {
                string num = port_num.Substring(Config.comPortPrefix.Length);
                byte port = 0;
                if (byte.TryParse(num, out port))
                {
                    config.default_SerialPort = port_num;
                }
            }
        }

        private void cmbCommRetransmission_SelectedIndexChanged(object sender, EventArgs e)
        {
            config.commRetries = this.cmbCommRetransmission.SelectedIndex;
        }

        private static void InitComboDataSource(ComboBox cmb, IDictionary<string, object> dict)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dict;
            cmb.DataSource = bs;
            cmb.DisplayMember = "key";
            cmb.ValueMember = "value";
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if(sender.Equals(this.btnClose))
            {
                this.Close();
            }
            else if(sender.Equals(this.btnRestoreToDefault))
            {
                this.RestoreToDefault();
            }
        }

        private void RestoreToDefault()
        {
            config.RestoreCommParamToDefault();
            RemoveEventHandler();
            InitUI();
            RegisterEventHandler();
        }

        private Localization localization = Config.GetInstance().localization;
        private void UpdateDisplayLanguage()
        {
            this.Text = localization[Localization.formCommSettings];
            this.lblCommMethod.Text = localization[Localization.formCommSettings_CommMethod];
            this.lblCommRetransmission.Text = localization[Localization.formCommSettings_CommRetransmission];

            this.lblDefaultSerialPort.Text = localization[Localization.formCommSettings_DefaultSerialPort];

            this.mSwitchSerialPortManualMode.Text = localization[Localization.formCommSettings_SerialPortManualMode];
            
            this.lblIPAddress.Text = localization[Localization.formCommSettings_IPAddress];
            this.lblPort.Text = localization[Localization.formCommSettings_Port];

            this.linkHelp.Text = localization[Localization.formCommSettings_Help];
            
            this.btnRestoreToDefault.Text = localization[Localization.formCommSettings_RestoreToDefault];
            this.btnClose.Text = localization[Localization.formCommSettings_Close];
        }

        private void switch_CheckedChanged(object sender, EventArgs e)
        {
            if(sender.Equals(this.mSwitchCommViaUSB))
            {
                config.connectionViaUSB = this.mSwitchCommViaUSB.IsChecked;
            }
            else if (sender.Equals(this.mSwitchCommViaRS232))
            {
                config.connectionViaRS232 = this.mSwitchCommViaRS232.IsChecked;
            }
            else if (sender.Equals(this.mSwitchCommViaUDP))
            {
                config.connectionViaUDP = this.mSwitchCommViaUDP.IsChecked;
            }
            else if (sender.Equals(this.mSwitchCommViaTCP))
            {
                config.connectionViaTCP = this.mSwitchCommViaTCP.IsChecked;
            }
            else if(sender.Equals(this.mSwitchSerialPortManualMode))
            {
                config.isSpManualMode = this.mSwitchSerialPortManualMode.IsChecked;
            }
        }

        private void switch_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender.Equals(this.mSwitchCommViaUSB))
            {
                if (!this.mSwitchCommViaUSB.IsChecked)
                {
                    this.mSwitchCommViaUSB.IsChecked = true;                    
                    return;
                }
                else if (config.CanBeUncheckedConnectionMethod(ConnectionMethod.USB))
                {
                    this.mSwitchCommViaUSB.IsChecked = false;
                    return;
                }
            }
            else if (sender.Equals(this.mSwitchCommViaRS232))
            {
                if (!this.mSwitchCommViaRS232.IsChecked)
                {
                    this.mSwitchCommViaRS232.IsChecked = true;
                    return;
                }
                else if (config.CanBeUncheckedConnectionMethod(ConnectionMethod.RS232))
                {
                    this.mSwitchCommViaRS232.IsChecked = false;
                    return;
                }
            }
            else if (sender.Equals(this.mSwitchCommViaUDP))
            {
                if (!this.mSwitchCommViaUDP.IsChecked)
                {
                    this.mSwitchCommViaUDP.IsChecked = true;
                    return;
                }
                else if (config.CanBeUncheckedConnectionMethod(ConnectionMethod.UDP))
                {
                    this.mSwitchCommViaUDP.IsChecked = false;
                    return;
                }
            }
            else if (sender.Equals(this.mSwitchCommViaTCP))
            {
                if (!this.mSwitchCommViaTCP.IsChecked)
                {
                    this.mSwitchCommViaTCP.IsChecked = true;
                    return;
                }
                else if (config.CanBeUncheckedConnectionMethod(ConnectionMethod.TCP))
                {
                    this.mSwitchCommViaTCP.IsChecked = false;
                    return;
                }
            }

            gtMessageBox.ShowMessage(this, localization[Localization.formCommSettings_SelectOneMethodAtLeast], this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void linkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.UdpAndTcpSettingHelp();
        }

        private void UdpAndTcpSettingHelp()
        {
            AEC.UI.Forms.FormRS232ToWifiConverter form = new AEC.UI.Forms.FormRS232ToWifiConverter();
            if(form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.mSwitchCommViaUDP.IsCheckedInit = config.connectionViaUDP;
                this.mSwitchCommViaTCP.IsCheckedInit = config.connectionViaTCP;

                this.txtUDPIPAddress.Text = config.udp_ip;
                this.txtUDPPort.Text = config.udp_port.ToString();

                this.txtTCPIPAddress.Text = config.tcp_ip;
                this.txtTCPPort.Text = config.tcp_port.ToString();
            }
        }
    }
}
