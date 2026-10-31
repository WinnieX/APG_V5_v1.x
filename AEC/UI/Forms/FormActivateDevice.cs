using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.Misc;
using AEC.Core;
using AEC.Comm.ActivateDevice;
using AEC.Comm.CommDevices;
using AEC.Comm.CommEventArgs;

namespace AEC.UI.Forms
{
    public partial class FormActivateDevice : AEC.UI.MFormBase
    {
        public const string CommInterface_USB = "USB";
        public const string CommInterface_RS232 = "RS232";
        public const string CommInterface_UDP = "UDP";
        public const string CommInterface_TCP = "TCP";

        private string lastUSBPort = string.Empty;
        private string lastComPort = string.Empty;

        private static AEC.Misc.Config config = AEC.Misc.Config.GetInstance();

        private readonly ActivateDeviceController commTestController = new ActivateDeviceController();

        private static readonly Color unknownColor = Color.FromArgb(255, 165, 0);
        private static readonly Color okColor = Color.FromArgb(50, 205, 50);
        private static readonly Color ngColor = Color.Red;
        private const string unknown = "...";
        private const string ok = "OK";
        private const string ng = "NG";

        public FormActivateDevice()
        {
            InitializeComponent();
        }

        protected override void UpdateDisplayLanguage()
        {
            this.Text = localization[Localization.formActivateDevice];
            this.lblCommInterface.Text = localization[Localization.formActivateDevice_CommInterface];
            this.btnRun.Text = localization[Localization.formActivateDevice_Activate];
        }

        private void InitUI()
        {
            this.cmbCommInterface.Items.AddRange(new string[] { CommInterface_USB, CommInterface_RS232, CommInterface_UDP, CommInterface_TCP });
            this.cmbCommInterface.SelectedIndex = 0;

            this.InitResultUI();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.InitUI();
            this.InitEventBusHandler();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            this.DeinitEventBusHandler();
        }

        void InitEventBusHandler()
        {
            this.commTestController.eventBus.RegisterHandler(EventBusHandler);
        }

        private void DeinitEventBusHandler()
        {
            this.commTestController.eventBus.RemoveHandler(EventBusHandler);
        }

        public void EventBusHandler(object sender, MEventBusArgs e)
        {
            base.syncContext.Post(EventBusUIHandler, e);
        }

        private void EventBusUIHandler(object state)
        {
            MEventBusArgs e = state as MEventBusArgs;
            if (e == null)
            {
                return;
            }

            switch (e.type)
            {
                case CommOutputMessageArgs.CommOutputMessage:
                    {
                        CommOutputMessageArgs args = e as CommOutputMessageArgs;
                        if(args != null)
                        {
                            this.OutputMsg(args.message);
                        }
                    }
                    break;

                case CommWrokCompletedArgs.CommWrokCompleted:
                    {
                        CommWrokCompletedArgs args = e as CommWrokCompletedArgs;                        
                        this.UpdateResultUI(args.result);
                        this.btnRun.Enabled = true;
                    }                    
                    break;

                case CommUSBInterfaceNotFoundArgs.CommUSBInterfaceNotFound:
                    {
                        this.USBInterfaceNotFoundHandler();
                    }
                    break;

                default:
                    break;
            }
        }

        private void InitResultUI()
        {
            this.lblResult.ForeColor = unknownColor;
            this.lblResult.Text = unknown;
        }

        private void UpdateResultUI(bool isOK)
        {
            if(isOK)
            {
                this.lblResult.ForeColor = okColor;
                this.lblResult.Text = ok;
            }
            else
            {
                this.lblResult.ForeColor = ngColor;
                this.lblResult.Text = ng;
            }
        }

        private void USBInterfaceNotFoundHandler()
        {
            if (this.cmbCommInterface.Text == CommInterface_USB)
            {
                this.InitUSBPortUI();
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            this.Run();
        }

        private void Run()
        {
            try
            {
                this.InitResultUI();
                this.ClearOutput();

                CommInterfaceBase commInterface = GetInterfaceFromUI();

                if(this.commTestController.RunTest(commInterface))
                {
                    this.btnRun.Enabled = false;
                }
            }
            catch(Exception e)
            {
                this.OutputMsg(e.Message);
            }
        }

        private CommInterfaceBase GetInterfaceFromUI()
        {
            switch(this.cmbCommInterface.Text)
            {
                case CommInterface_USB:
                    {
                        if(string.IsNullOrEmpty(this.cmbCOMPort.Text))
                        {
                            throw new Exception("The USB you entered is incorrect!");
                        }

                        return new USBCommInterface(this.cmbCOMPort.Text);
                    }                    
                    break;

                case CommInterface_RS232:
                    {
                        string portName = this.cmbCOMPort.Text;
                        return new RS232CommInterface(this.cmbCOMPort.Text, AEC.Comm.CommDevices.SerialPort.defaultBaudRate);
                    }                    
                    break;

                case CommInterface_UDP:
                case CommInterface_TCP:
                    {
                        CommInterfaceType type = this.cmbCommInterface.Text == CommInterface_UDP ? CommInterfaceType.UDP : CommInterfaceType.TCP;
                        System.Net.IPAddress ipAddress;
                        if (!System.Net.IPAddress.TryParse(this.txtIPAddress.Text, out ipAddress))
                        {
                            throw new Exception("The IP Address you entered is incorrect!");
                        }
                        int port;
                        if(!int.TryParse(this.txtPort.Text, out port))
                        {
                            throw new Exception("The Port you entered is incorrect!");
                        }

                        return new NetWrokCommInterface(type, ipAddress, port);
                    }
                    break;
                
                default:
                    return null;
            }
        }

        private void lblCOMPort_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(this.cmbCOMPort.Visible)
            {
                if (this.cmbCommInterface.Text == CommInterface_USB)
                {
                    this.InitUSBPortUI();
                }
                else if (this.cmbCommInterface.Text == CommInterface_RS232)
                {
                    this.InitComPortUI();
                }
            }
        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if(cmb == null)
            {
                return;
            }

            if(sender.Equals(this.cmbCommInterface))
            {
                this.CommInterfaceChanged();
            }   
            else if(sender.Equals(this.cmbCOMPort))
            {
                this.ComPortChanged();
            }
        }

        private void CommInterfaceChanged()
        {
            string commInterface = this.cmbCommInterface.Text.Trim();
            switch(commInterface)
            {
                case CommInterface_USB:
                    this.lblInterfaceProperty1.Visible = true;
                    this.lblInterfaceProperty2.Visible = false;
                    this.cmbCOMPort.Visible = true;
                    this.txtIPAddress.Visible = false;
                    this.txtPort.Visible = false;
                    this.lblInterfaceProperty1.UpdateText(localization[Localization.formActivateDevice_Port], true);
                    this.InitUSBPortUI();
                    break;

                case CommInterface_RS232:
                    this.lblInterfaceProperty1.Visible = true;
                    this.lblInterfaceProperty2.Visible = false;
                    this.cmbCOMPort.Visible = true;
                    this.txtIPAddress.Visible = false;
                    this.txtPort.Visible = false;
                    this.lblInterfaceProperty1.UpdateText(localization[Localization.formActivateDevice_Port], true);
                    this.InitComPortUI();
                    break;

                case CommInterface_UDP:
                case CommInterface_TCP:
                    this.lblInterfaceProperty1.Visible = true;
                    this.lblInterfaceProperty2.Visible = true;
                    this.cmbCOMPort.Visible = false;
                    this.txtIPAddress.Visible = true;
                    this.txtPort.Visible = true;
                    this.lblInterfaceProperty1.UpdateText(localization[Localization.formActivateDevice_IpAddress], true);
                    this.lblInterfaceProperty2.UpdateText(localization[Localization.formActivateDevice_Port], true);
                    this.UpdateIPAddressAndPort(commInterface);
                    break;
            }
        }

        private void UpdateIPAddressAndPort(string commInterface)
        {
            if(commInterface == CommInterface_UDP)
            {
                this.txtIPAddress.Text = config.udp_ip;
                this.txtPort.Text = config.udp_port.ToString();
            }
            else if(commInterface == CommInterface_TCP)
            {
                this.txtIPAddress.Text = config.tcp_ip;
                this.txtPort.Text = config.tcp_port.ToString();
            }

            this.NetIpOrPortChanged();
        }

        private void ComPortChanged()
        {
            if(this.cmbCommInterface.Text == CommInterface_USB)
            {
                if(this.cmbCOMPort.SelectedIndex >= 0)
                {
                    this.lastUSBPort = this.cmbCOMPort.Text;
                }
            }
            else if(this.cmbCommInterface.Text == CommInterface_RS232)
            {
                if(this.cmbCOMPort.SelectedIndex >= 0)
                {
                    this.lastComPort = this.cmbCOMPort.Text;
                }
            }
            
        }

        private void InitUSBPortUI()
        {
            this.cmbCOMPort.Items.Clear();
            List<string> portList = new List<string>();
            AEC.Comm.CommDevices.HID.HIDDevice.InterfaceDetails[] hidInterfaces = AEC.Comm.CommDevices.HIDDevice.GetValidDevices();
            foreach(var hid in hidInterfaces)
            {
                portList.Add(AEC.Comm.CommDevices.HIDDevice.GetUSBHIDPortName(hid.serialNumber));
            }
            string[] ports = portList.ToArray();

            this.cmbCOMPort.Items.AddRange(ports);

            if (ports.Length > 0)
            {
                if (!string.IsNullOrEmpty(this.lastUSBPort))
                {
                    bool isFindLast = false;
                    for (int i = 0; i < ports.Length; ++i)
                    {
                        if (this.lastUSBPort == ports[i])
                        {
                            this.cmbCOMPort.SelectedIndex = i;
                            isFindLast = true;
                            break;
                        }
                    }

                    if (!isFindLast)
                    {
                        this.cmbCOMPort.SelectedIndex = 0;
                    }
                }
                else
                {
                    this.cmbCOMPort.SelectedIndex = 0;
                }
            }

            this.btnRun.Enabled = ports.Length > 0;
        }

        private void InitComPortUI()
        {
            this.cmbCOMPort.Items.Clear();
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            this.cmbCOMPort.Items.AddRange(ports);
            if (ports.Length > 0)
            {
                if(!string.IsNullOrEmpty(this.lastComPort))
                {
                    bool isFindLast = false;
                    for(int i = 0; i < ports.Length; ++i)
                    {
                        if(this.lastComPort == ports[i])
                        {
                            this.cmbCOMPort.SelectedIndex = i;
                            isFindLast = true;
                            break;
                        }

                        if (!isFindLast)
                        {
                            this.cmbCOMPort.SelectedIndex = 0;
                        }
                    }
                }
                else
                {
                    this.cmbCOMPort.SelectedIndex = 0;
                }
            }

            this.btnRun.Enabled = ports.Length > 0;
        }

        private void OutputMsg(string format, params object[] arg)
        {
            string txt = string.Format(format, arg);
            this.txtOutput.AppendText(txt);
            this.txtOutput.SelectionStart = this.txtOutput.TextLength;
            this.txtOutput.SelectionLength = 0;
        }

        private void ClearOutput()
        {
            this.txtOutput.Clear();
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            this.NetIpOrPortChanged();
        }

        private void NetIpOrPortChanged()
        {
            System.Net.IPAddress ip;
            ushort port;
            if (System.Net.IPAddress.TryParse(this.txtIPAddress.Text, out ip) &&
                ushort.TryParse(this.txtPort.Text, out port))
            {
                this.btnRun.Enabled = true;
            }
            else
            {
                this.btnRun.Enabled = false;
            }
        }
    }
}
