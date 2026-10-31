using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AEC.Core;
using AEC.Comm;
using AEC.Comm.FirmwareUpdate;

namespace AEC.UI.Forms
{
    public partial class FormDspProgrammer : AEC.UI.MFormBase
    {
        private static readonly Color txtForeColor = Color.FromArgb(224, 224, 224);

        //private const string BIN_FILE_FILTER = "bin文件|*.bin";
        //private const string BIN_FILE_EXTENSION = ".bin";
        private const int Size4K = 0x1000;

        private BindingSource portsBindingSource = new BindingSource();

        private FirmwareUpdateService commService = new FirmwareUpdateService();

        //private readonly DspProgrammerDownloadParams downloadParams = new DspProgrammerDownloadParams();
        //private readonly DeviceID deviceId = new DeviceID();

        public bool workIsRunning
        {
            set
            {
                this.cmbPorts.Enabled = !value;
                this.linkRefresh.Enabled = !value;
                this.btnUpload.Enabled = !value && portIsValid;
                this.btnDownload.Enabled = !value && (portIsValid && crcFileIsExist);
            }
        }

        public bool portIsValid
        {
            get
            {
                return this.cmbPorts.SelectedValue != null;
            }
        }

        public bool _crcFileIsExist = false;

        public bool crcFileIsExist
        {
            get
            {
                return this._crcFileIsExist;
            }
            set
            {
                this._crcFileIsExist = value;

                this.btnDownload.Enabled = this._crcFileIsExist && portIsValid;
            }
        }

        public FormDspProgrammer()
        {
            InitializeComponent();
        }

        protected override void OnLoadHandler()
        {
            this.InitUI();
            this.RegisterEventHandler();
        }

        protected override void OnClosedHandler()
        {
            this.RemoveEventHandler();
        }

        private void RegisterEventHandler()
        {
            this.commService.eventBus.RegisterHandler(ServiceEventHandler);
        }

        private void RemoveEventHandler()
        {
            this.commService.eventBus.RemoveHandler(ServiceEventHandler);
        }


        private void InitUI()
        {
            this.InitCmbPorts();

            this.txtWriteKeyHash.Text = Comm.Utils.ByteArrayToHexStr(FirmwareUpdateService.keyHash, string.Empty);

            //this.txtDspFwPackageFilePath.Text = @"E:\雷萌\飞傲\Projects\K17\DSP\20241016\飞傲 K17 DSP.mdf";
            //this.cmbPorts.SelectedIndex = 1;
            //this.switchOnlyDownloadFirmwareInfo.IsCheckedInit = true;
        }

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
                    rs232CommPort.baudRate = FirmwareUpdateService.DSPDownloaderToUpcBaudRate;
                    commPorts[rs232CommPort.displayName] = rs232CommPort;
                }
            }

            portsBindingSource.Clear();
            portsBindingSource.DataSource = commPorts;

            this.cmbPorts.DataSource = portsBindingSource;

            if (commPorts.Count > 0)
            {
                this.cmbPorts.DisplayMember = "Key";
                this.cmbPorts.ValueMember = "Value";
            }

            this.btnUpload.Enabled = portIsValid;
            this.btnDownload.Enabled = portIsValid && crcFileIsExist;
        }

        private void linkRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.InitCmbPorts();
        }

        private void txtBinFilePath_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = DSPFirmwareInfoSeralizer.DEF_MDF_FILE_FILTER;
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.txtDspFwPackageFilePath.Text = ofd.FileName;
                    this.txtDspFwPackageFilePath.SelectionStart = this.txtDspFwPackageFilePath.TextLength;
                }
            }
            else
            {
                this.DspFwPackageFilePathChangedHandler();
            }
        }

        private void FormDspIapProgrammerDownload_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                System.IO.FileInfo fi = new System.IO.FileInfo(path);
                if ((fi.Attributes & FileAttributes.Directory) == FileAttributes.Directory || fi.Extension != DSPFirmwareInfoSeralizer.MDF_FILE_EXTENSION)
                {
                    e.Effect = DragDropEffects.None;
                }
                else
                {
                    e.Effect = DragDropEffects.All;   
                }
            }                                                           
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void FormDspIapProgrammerDownload_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            //if()
            //System.IO.FileInfo fi = new System.IO.FileInfo(path);
            //if(fi.Attributes & FileAttributes.SparseFile)
            this.txtDspFwPackageFilePath.Text = path;
            this.txtDspFwPackageFilePath.SelectionStart = this.txtDspFwPackageFilePath.TextLength;
        }

        private void txtDspFwPackageFilePath_TextChanged(object sender, EventArgs e)
        {
            this.DspFwPackageFilePathChangedHandler();
        }

        private void DspFwPackageFilePathChangedHandler()
        {
            this._crcFileIsExist = false;
            string filePath = this.txtDspFwPackageFilePath.Text.Trim();

            if (!System.IO.File.Exists(filePath))
            {
                this.btnDownload.Enabled = false;
                return;
            }
            string errorMsg = string.Empty;
            DSPFirmwarePackage dsp_fw_package = DSPFirmwareInfoSeralizer.DeseralizeFromFile(filePath, out errorMsg);
            if (dsp_fw_package == null)
            {
                this.btnDownload.Enabled = false;
                gtMessageBox.ShowMessage(this, errorMsg, this.Text);
                return;
            }
            this._crcFileIsExist = dsp_fw_package != null;
            this.panelDspProgrammerDownloadParamsDownload.dspFirmwareInfo = dsp_fw_package.dspFirmwareInfo;

            this.btnDownload.Enabled = portIsValid && crcFileIsExist;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if(sender.Equals(this.btnUpload))
            {
                this.Upload();
            }
            else if(sender.Equals(this.btnDownload))
            {
                this.Download();
            }
        }

        private void Upload()
        {
            this.ClearErrorMsg();

            CommPort commPort = (CommPort)(this.cmbPorts.SelectedValue);
            
            if (commPort == null)
            {
                this.OutputErrorMsgLine("端口不能为空！");
                return;
            }

            DspProgrammerParamsUploadTask dspProgrammerParamsUploadTask = new DspProgrammerParamsUploadTask()
            {
                commPort = commPort,
                //downloadParams = downloadParams,
                //deviceId = deviceId,
            };

            FirmwareUpdateWrokerArgs dspProgrammerArgs = new FirmwareUpdateWrokerArgs(FirmwareUpdateProcess.DSPProgrammerParamsUpload, dspProgrammerParamsUploadTask);

            if (this.commService.StartWork(dspProgrammerArgs))
            {
                this.workIsRunning = true;
            }
        }

        private void Download()
        {
            this.ClearErrorMsg();

            CommPort commPort = (CommPort)(this.cmbPorts.SelectedValue);
            
            if (commPort == null)
            {
                this.OutputErrorMsgLine("端口不能为空！");
                return;
            }

            string dspFwFilePath = this.txtDspFwPackageFilePath.Text.Trim();
            if (!System.IO.File.Exists(dspFwFilePath))
            {
                this.OutputErrorMsgLine("DSP Firmware 文件 {0} 不存在！", dspFwFilePath);
                return;
            }

            string errorMsg = string.Empty;
            DSPFirmwarePackage dspFwPackage = DSPFirmwareInfoSeralizer.DeseralizeFromFile(dspFwFilePath, out errorMsg);
            if (dspFwPackage == null)
            {
                this.OutputErrorMsgLine("DSP 固件有误 或 数据输入有误: {0}", errorMsg);
                return;
            }

            DspProgrammerDownloadTask dspDownloadTask = new DspProgrammerDownloadTask()
            {
                commPort = commPort,
                dspFwPackage = dspFwPackage,
                isOnlyDownloadParams = this.switchOnlyDownloadFirmwareInfo.IsChecked,
                isDownloadCompleteBin = this.switchDownloadCompleteBin.IsChecked,                
            };

            FirmwareUpdateWrokerArgs firmwareUpgradeArgs = new FirmwareUpdateWrokerArgs(FirmwareUpdateProcess.DSPProgrammerDownload, dspDownloadTask);

            if(this.commService.StartWork(firmwareUpgradeArgs))
            {
                this.workIsRunning = true;
            }
        }

        public static DateTime GetFileModifiedTimeStamp(string fileName)
        {
            try
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(fileName);
                if (fi.Exists)
                {
                    return fi.LastWriteTime;
                }
            }
            catch(Exception)
            {

            }

            return DateTime.MinValue;
        }

        private DspProgrammerDownloadParams GetDownloadParams()
        {
            DspProgrammerDownloadParams downloadParams = new DspProgrammerDownloadParams();

            //uint data = 0;
            //if (!panelDspProgrammerDownloadParamsDownload.GetVersion(ref data))
            //{
            //    return null;
            //}
            //downloadParams.version = data;

            //if (!panelDspProgrammerDownloadParamsDownload.GetRetries(ref data))
            //{
            //    return null;
            //}
            //downloadParams.retries = data;

            //if (!panelDspProgrammerDownloadParamsDownload.GetDSPPowerResetDuration(ref data))
            //{
            //    return null;
            //}
            //downloadParams.dspPowerResetDuration = data;

            //if (!panelDspProgrammerDownloadParamsDownload.GetDSPBaudRate(ref data))
            //{
            //    return null;
            //}
            //downloadParams.dspBaudRate = data;

            //downloadParams.remarks = panelDspProgrammerDownloadParamsDownload.GetRemarks();

            return downloadParams;
        }




        private void ServiceEventHandler(object sender, MEventBusArgs e)
        {
            base.syncContext.Post(ProcessServiceEvent, e);
        }

        private void ProcessServiceEvent(object state)
        {
            MEventBusArgs e = state as MEventBusArgs;
            switch (e.type)
            {
                case FirmwareUpdateOutputArgs.FirmwareUpdateOutput:
                    this.FirmwareUpgradeOutputHandler(e as FirmwareUpdateOutputArgs);
                    break;

                case FirmwareUpdateCompletedArgs.FirmwareUpdateCompleted:
                    this.FirmwareUpgradeCompletedHandler(e as FirmwareUpdateCompletedArgs);
                    break;

                case FirmwareInfoLoadedArgs.FirmwareInfoLoaded:
                    this.FirmwareInfoLoadedHandler(e as FirmwareInfoLoadedArgs);
                    break;

                case DownloaderDeviceIdLoadedArgs.DownloaderDeviceIdLoaded:
                    this.DownloaderDeviceIdLoadedHandler(e as DownloaderDeviceIdLoadedArgs);
                    break;

                case DSPKeyHashLoadedArgs.DSPKeyHashLoaded:
                    this.DSPKeyHashLoadedHandler(e as DSPKeyHashLoadedArgs);
                    break;

                default:
                    break;
            }
        }

        private void FirmwareInfoLoadedHandler(FirmwareInfoLoadedArgs arg)
        {
            this.panelDspProgrammerDownloadParamsUpload.dspFirmwareInfo = arg.dspFirmwareInfo;
        }

        private void DownloaderDeviceIdLoadedHandler(DownloaderDeviceIdLoadedArgs arg)
        {
            this.txtDeviceId.Text = arg.deviceId.ToString();
        }

        private void DSPKeyHashLoadedHandler(DSPKeyHashLoadedArgs arg)
        {
            this.txtReadKeyHash.Text = Comm.Utils.ByteArrayToHexStr(arg.keyHash, string.Empty);
        }

        private void FirmwareUpgradeOutputHandler(FirmwareUpdateOutputArgs arg)
        {
            if(arg.isError)
            {
                if(arg.lineControl != FirmwareUpdateOutputArgs.LineControl.None)
                {
                    this.OutputErrorMsgLine(arg.info);
                }
                else
                {
                    this.OutputErrorMsg(arg.info);
                }
            }
            else
            {
                if (arg.lineControl != FirmwareUpdateOutputArgs.LineControl.None)
                {
                    this.OutputMsgLine(arg.info);
                }
                else
                {
                    this.OutputMsg(arg.info);
                }
            }
        }

        private void FirmwareUpgradeCompletedHandler(FirmwareUpdateCompletedArgs arg)
        {
            this.workIsRunning = false;
            //if (arg.process == FirmwareUpdateProcess.DSPProgrammerParamsUpload && arg.isSuccess)
            //{
            //    this.UpdateLoadedParamsUI();
            //    this.UpdateDeviceId();
            //}
        }

        //private void UpdateDeviceId()
        //{
        //    this.txtDeviceId.Text = this.deviceId.ToString();
        //}

        private void OutputMsg(string format, params object[] arg)
        {
            this.txtOutput.AppendText(string.Format(format, arg));
        }

        private void OutputMsgLine(string format, params object[] arg)
        {
            if(this.txtOutput.TextLength != 0)
            {
                this.txtOutput.AppendText(System.Environment.NewLine);
            }
            this.txtOutput.AppendText(string.Format(format, arg));            
        }

        private void OutputErrorMsg(string format, params object[] arg)
        {
            this.txtOutput.AppendText(string.Format(format, arg));
            this.txtOutput.ForeColor = Color.Red;
        }

        private void OutputErrorMsgLine(string format, params object[] arg)
        {
            if (this.txtOutput.TextLength != 0)
            {
                this.txtOutput.AppendText(System.Environment.NewLine);
            }
            this.txtOutput.AppendText(string.Format(format, arg));
            this.txtOutput.ForeColor = Color.Red;
        }

        private void ClearErrorMsg()
        {
            this.txtOutput.Clear();
            this.txtOutput.ForeColor = txtForeColor;
        }

        private FormExportDspFirmwarePackage formExportDspFirmwarePackage = null;

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F7)
            {
                if(this.formExportDspFirmwarePackage == null || this.formExportDspFirmwarePackage.IsDisposed)
                {
                    this.formExportDspFirmwarePackage = new FormExportDspFirmwarePackage();
                }

                this.formExportDspFirmwarePackage.Show(this);
            }
        }
    }
}
