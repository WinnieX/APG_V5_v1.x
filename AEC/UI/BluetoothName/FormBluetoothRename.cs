using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.Core;
using AEC.Comm;
using AEC.Comm.FirmwareUpdate;
using AEC.Misc;

namespace AEC.UI.BluetoothName
{
    public partial class FormBluetoothRename : AEC.UI.MFormBase
    {
        private static readonly Color normalColor = Color.FromArgb(224, 224, 224);
        private static readonly Color failedColor = Color.Red;
        private static readonly Color successfulColor = Color.FromArgb(50, 205, 50);

        private BindingSource portsBindingSource = new BindingSource();
        private BindingSource baudRatesBindingSource = new BindingSource();

        private readonly FirmwareUpdateService commService = new FirmwareUpdateService();

        public FormBluetoothRename()
        {
            InitializeComponent();
        }

        protected override void OnLoadHandler()
        {
            base.OnLoadHandler();
            this.InitUI();
            this.RegisterEventHandler();
        }

        protected override void OnClosedHandler()
        {
            base.OnClosedHandler();
            this.RemoveEventHandler();
        }

        private void RegisterEventHandler()
        {
            this.commService.eventBus.RegisterHandler(EventBusHandler);
        }

        private void RemoveEventHandler()
        {
            this.commService.eventBus.RemoveHandler(EventBusHandler);
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
                case FirmwareUpdateOutputArgs.FirmwareUpdateOutput:
                    {
                        FirmwareUpdateOutputArgs args = e as FirmwareUpdateOutputArgs;
                        this.OutputInfo(args);
                    }
                    break;

                case FirmwareUpdateCompletedArgs.FirmwareUpdateCompleted:
                    {
                        FirmwareUpdateCompletedArgs args = e as FirmwareUpdateCompletedArgs;
                        this.isWorking = false;
                        this.txtOutput.ForeColor = args.isSuccess ? successfulColor : failedColor;
                        //this.lblInfo.UpdateUI();
                    }
                    break;

                default:
                    break;
            }
        }

        private void OutputInfo(FirmwareUpdateOutputArgs args)
        {
            string msg = args.info;
            bool isError = args.isError;

            if(this.txtOutput.TextLength == 0 || args.lineControl == FirmwareUpdateOutputArgs.LineControl.None)
            {
                this.txtOutput.AppendText(msg);
            }
            else if(args.lineControl == FirmwareUpdateOutputArgs.LineControl.NewLine)
            {
                msg = Environment.NewLine + msg;
                this.txtOutput.AppendText(msg);
            }
            else if (args.lineControl == FirmwareUpdateOutputArgs.LineControl.Return)
            {
                StringBuilder sb = new StringBuilder();
                string[] lines = this.txtOutput.Lines;

                for (int i = 0; i < lines.Length - 1; ++i)
                {
                    sb.AppendLine(lines[i]);
                }

                sb.Append(msg);
                this.txtOutput.Clear();
                this.txtOutput.AppendText(sb.ToString());
            }
        }

        private void InitUI()
        {
            this.InitCmbPorts();
        }

        private const int RS232BaudRateDefault = AEC.Comm.CommDevices.SerialPort.defaultBaudRate;
        private const int RS232BaudRateSecondary = AEC.Comm.CommDevices.SerialPort.secondaryBaudRate;

        private void InitCmbPorts()
        {
            Dictionary<string, CommPort> commPorts = new Dictionary<string, CommPort>();

            AEC.Comm.CommDevices.HID.HIDDevice.InterfaceDetails[] hidInterfaces = FUHIDDevice.GetValidDevices();
            if (hidInterfaces.Length > 0)
            {
                foreach (var hid in hidInterfaces)
                {
                    HIDCommPort hidCommPort = new HIDCommPort(hid.devicePath, hid.serialNumber);
                    commPorts[hidCommPort.displayName] = hidCommPort;
                }
            }

            string[] serialPorts = System.IO.Ports.SerialPort.GetPortNames();
            if (serialPorts.Length > 0)
            {
                foreach (var port in serialPorts)
                {
                    RS232CommPort rs232CommPort = new RS232CommPort(port);
                    commPorts[rs232CommPort.displayName] = rs232CommPort;
                }
            }

            this.portsBindingSource.Clear();
            this.portsBindingSource.DataSource = commPorts;

            this.cmbPorts.DataSource = this.portsBindingSource;

            if (commPorts.Count > 0)
            {
                this.cmbPorts.DisplayMember = "Key";
                this.cmbPorts.ValueMember = "Value";
            }

            this.txtFirmwareVersion.Enabled = commPorts.Count > 0;
            this.txtBluetoothName.Enabled = commPorts.Count > 0;
            this.btnRename.Enabled = commPorts.Count > 0 && this.txtFirmwareVersion.TextLength > 0;
            //this.btnUpload.Enabled = portIsValid;
            //this.btnDownload.Enabled = portIsValid && crcFileIsExist;
        }

        private void link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (sender.Equals(this.linkRefreshPort))
            {
                this.InitCmbPorts();
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if(sender.Equals(this.btnRename))
            {
                this.BluetoothRename();
            }
        }

        private void BluetoothRename()
        {
            this.ClearOutput();
            string firmwareText = this.txtFirmwareVersion.Text.Trim();
            if(this.txtFirmwareVersion.Text != firmwareText)
            {
                this.txtFirmwareVersion.Text = firmwareText;
                if(string.IsNullOrEmpty(firmwareText))
                {
                    return;
                }
            }

            string btNameText = Tools.StringHelper.GetMaxSizeUtf8String(this.txtBluetoothName.Text.Trim(), BTDeviceInfo.BT_NAME_MAX_SIZE);
            if (this.txtBluetoothName.Text != btNameText)
            {
                this.txtBluetoothName.Text = btNameText;
            }

            CommPort commPort = (CommPort)(this.cmbPorts.SelectedValue);

            if (commPort != null && commPort.type == CommPortType.RS232)
            {
                (commPort as RS232CommPort).baudRate = AEC.Comm.CommDevices.SerialPort.defaultBaudRate;
            }

            BluetoothRename btRename = new Comm.FirmwareUpdate.BluetoothRename()
            {
                btName = btNameText,
                mcuFirmwareSet = firmwareText,
            };

            BluetoothRenameTask firmwareUpdateTask = new BluetoothRenameTask(commPort, btRename);

            FirmwareUpdateWrokerArgs firmwareUpgradeArgs = new FirmwareUpdateWrokerArgs(FirmwareUpdateProcess.BluetoothRename, firmwareUpdateTask);

            if (this.commService.StartWork(firmwareUpgradeArgs))
            {
                this.isWorking = true;
            }
        }

        private void ClearOutput()
        {
            Log.Clear();
            this.txtOutput.Clear();
            this.txtOutput.ForeColor = normalColor;
        }

        public bool isWorking
        {
            set
            {
                this.txtBluetoothName.Enabled = !value;
                this.btnRename.Enabled = !value;
                this.cmbPorts.Enabled = !value;
                this.linkRefreshPort.Enabled = !value;
            }
        }

        private void txtFirmwareVersion_TextChanged(object sender, EventArgs e)
        {
            this.btnRename.Enabled = this.txtFirmwareVersion.TextLength > 0;
        }

        protected override void UpdateDisplayLanguage()
        {
            this.Text = localization[Localization.formProMode_BluetoothRename];
            this.lblPort.Text = localization[Localization.formFirmwareUpdate_Port];
            this.linkRefreshPort.Text = localization[Localization.formFirmwareUpdate_Refresh];
            this.lblFirmwareVersion.Text = localization[Localization.formFirmwareUpdate_FirmwareVersion];
            this.lblBluetoothName.Text = localization[Localization.formFirmwareUpdate_BluetoothName];
            this.btnRename.Text = localization[Localization.formFirmwareUpdate_Rename];
            this.lblOutput.Text = localization[Localization.formFirmwareUpdate_Output];
        }
    }
}
