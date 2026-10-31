using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using AEC.Core;
using AEC.Comm;
using AEC.Comm.FirmwareUpdate;
using AEC.Misc;

namespace AEC.UI.Forms
{
    public partial class FormFirmwareUpdate : AEC.UI.MFormBase
    {
        private const int KeyValidityPeriod = 6;    //小时

        private static readonly Color normalColor = Color.FromArgb(224, 224, 224);
        private static readonly Color failedColor = Color.Red;
        private static readonly Color successfulColor = Color.FromArgb(50, 205, 50);

        private BindingSource portsBindingSource = new BindingSource();
        private BindingSource baudRatesBindingSource = new BindingSource();

        private readonly FirmwareUpdateService commService = new FirmwareUpdateService();

        private FirmwarePackage firmwarePackage = null;

        private ActivationCode.MLicenseType currLicenseType = ActivationCode.MLicenseType.Unauthorized;

        private bool fileChangedTimer = false;
        private int fileChangedTimerCount = 0;
        private DateTime keyStartTimestamp = DateTime.Now;
        private Timer timer;

        public bool isWorking
        {
            set
            {
                this.btnUpdate.Enabled = !value && this.firmwarePackage != null;
                this.txtFirmwarePackageFilePath.Enabled = !value;
                this.txtFirmwarePackageInfo.Enabled = !value;
                this.cmbPorts.Enabled = !value;
                this.linkRefreshPort.Enabled = !value;
                this.cmbBaudRate.Enabled = !value;
                this.linkRefreshKey.Enabled = !value;
                this.txtKey.Enabled = !value;
            }
        }

        public FormFirmwareUpdate()
        {
            InitializeComponent();
        }

        protected override void UpdateDisplayLanguage()
        {
            this.lblPort.Text = localization[Localization.formFirmwareUpdate_Port];
            this.linkRefreshPort.Text = localization[Localization.formFirmwareUpdate_Refresh];
            this.lblBaudRate.Text = localization[Localization.formFirmwareUpdate_BaudRate];
            this.lblKey.Text = localization[Localization.formFirmwareUpdate_Key];
            this.linkRefreshKey.Text = localization[Localization.formFirmwareUpdate_Refresh];
            this.lblFilePath.Text = localization[Localization.formFirmwareUpdate_File];
            this.btnUpdate.Text = localization[Localization.formFirmwareUpdate_Update];
            this.lblFirmwarePackageInfo.Text = localization[Localization.formFirmwareUpdate_Info];
            this.lblProgress.Text = localization[Localization.formFirmwareUpdate_Progress];
        }

        private void txt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(sender.Equals(this.txtFirmwarePackageFilePath))
            {
                this.SelectFirmwarePackageFile();
            }
        }

        private void SelectFirmwarePackageFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = FirmwarePackageSeralizer.DEF_MFP_FILE_FILTER;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtFirmwarePackageFilePath.Text = ofd.FileName;
                this.txtFirmwarePackageFilePath.SelectionStart = this.txtFirmwarePackageFilePath.TextLength;
            }
        }

        protected override void OnClosingHandler(CancelEventArgs e)
        {
            if(this.commService.isBusy)
            {
                gtMessageBox.ShowMessage(this, localization[Localization.systemIsBusyAndTryAgain]);
                e.Cancel = true;
            }
        }
        
        protected override void OnLoadHandler()
        {
            base.OnLoadHandler();

            currLicenseType = ActivationCode.CheckLicense();
            if (currLicenseType != ActivationCode.MLicenseType.Unauthorized)
            {
                this.ActivedHandler(currLicenseType);
            }
            else
            {
                this.UnactivedHandler();
            }

            this.InitUI();
            this.RegisterEventHandler();
            this.InitTimer();
        }

        
        private void InitTimer()
        {
            this.timer = new Timer();
            this.timer.Interval = 0x100;
            this.timer.Tick += timer_Tick;
            this.timer.Start();
        }

        private void DeInitTimer()
        {
            if (this.timer != null)
            {
                this.timer.Stop();
                this.timer.Tick -= timer_Tick;
                this.timer.Dispose();
                this.timer = null;
            }
        }

        private void ActivedHandler(ActivationCode.MLicenseType licenseType)
        {
            if (licenseType == ActivationCode.MLicenseType.Temporary)
            {
                ActivationCode.ClearLicense();
            }

            base.Text = localization[Localization.formFirmwareUpdate] + " | " + ActivationCode.GetLicenseDesc(licenseType) + " | " + AEC.Tools.VersionHelper.GetVersion();
            this.txtKey.ReadOnly = false;
        }

        private void UnactivedHandler()
        {
            base.Text = localization[Localization.formFirmwareUpdate] + " | " + AEC.Tools.VersionHelper.GetVersion();
            this.txtKey.ReadOnly = true;
        }

        protected override void OnClosedHandler()
        {
            this.StopFileWatcher();
            this.DeInitTimer();
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
                //case CommOutputMessageArgs.CommOutputMessage:
                //    {
                //        CommOutputMessageArgs args = e as CommOutputMessageArgs;
                //        if (args != null)
                //        {
                //            this.OutputMsg(args.message);
                //        }
                //    }
                //    break;

                //case CommWrokCompletedArgs.CommWrokCompleted:
                //    {
                //        CommWrokCompletedArgs args = e as CommWrokCompletedArgs;
                //        this.UpdateResultUI(args.result);
                //        this.btnRun.Enabled = true;
                //    }
                //    break;

                case FirmwareUpdateOutputArgs.FirmwareUpdateOutput:
                    {
                        FirmwareUpdateOutputArgs args = e as FirmwareUpdateOutputArgs;
                        this.OutputInfo(args);
                    }
                    break;

                case FirmwareUpdateProgressArgs.FirmwareUpdateProgress:
                    {
                        FirmwareUpdateProgressArgs args = e as FirmwareUpdateProgressArgs;
                        this.UpdateProgress(args.progressRatio, args.isShowProgress);
                    }
                    break;

                case FirmwareUpdateCompletedArgs.FirmwareUpdateCompleted:
                    {
                        FirmwareUpdateCompletedArgs args = e as FirmwareUpdateCompletedArgs;
                        this.isWorking = false;
                        this.lblInfo.ForeColor = args.isSuccess ? successfulColor : failedColor;
                        this.lblInfo.UpdateUI();
                        if(args.isSuccess)
                        {
                            this.UpdateProgress(1.0f, true);
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        private void OutputInfo(FirmwareUpdateOutputArgs args)
        {
            if (args.lineControl != FirmwareUpdateOutputArgs.LineControl.None)
            {
                this.lblInfo.UpdateText(args.info, true);
            }
            else
            {
                this.lblInfo.UpdateText(this.lblInfo.Text + args.info, true);
            }
        }

        private void UpdateProgress(float progressRatio, bool showProgress)
        {
            this.mProgressBar.isShowProgress = showProgress;
            this.mProgressBar.progressRatio = progressRatio;
            //this.lblProgress.Visible = showProgress;
            //this.mProgressBar.Visible = showProgress;
        }


        private void InitUI()
        {
            this.InitCmbBaudRate();
            this.InitCmbPorts();

            if (this.currLicenseType != ActivationCode.MLicenseType.Unlimited)
            {
                this.InitRandKey();
            }
            //this.txtFirmwarePackageFilePath.Text = @"D:\雷萌工具\固件更新\固件包\VK3Pro\VK3Pro固件包-不加密 - 副本.mfp";
            //this.txtFirmwarePackageFilePath.Text = @"D:\雷萌工具\固件更新\固件包\VK3Pro\VK3Pro固件包-不加密.mfp";
        }

        private void InitDownloadParams()
        {
            //this.panelDspProgrammerDownloadParamsDownload.version = 0x01000001;
            //this.panelDspProgrammerDownloadParamsDownload.SetDefaultParams();
        }

        private bool rs232BaudRatesVisible
        {
            set
            {
                this.lblBaudRate.Visible = value;
                this.cmbBaudRate.Visible = value;
            }
        }

        private const int RS232BaudRateDefault = AEC.Comm.CommDevices.SerialPort.defaultBaudRate;
        private const int RS232BaudRateSecondary = AEC.Comm.CommDevices.SerialPort.secondaryBaudRate;

        private void InitCmbBaudRate()
        {
            Dictionary<string, int> baudRates = new Dictionary<string, int>();
            baudRates[localization[Localization.formFirmwareUpdate_Auto]] = 0;
            baudRates[RS232BaudRateDefault.ToString()] = RS232BaudRateDefault;
            baudRates[RS232BaudRateSecondary.ToString()] = RS232BaudRateSecondary;

            this.baudRatesBindingSource.Clear();

            this.baudRatesBindingSource.DataSource = baudRates;

            this.cmbBaudRate.DataSource = this.baudRatesBindingSource;

            if (baudRates.Count > 0)
            {
                this.cmbBaudRate.DisplayMember = "Key";
                this.cmbBaudRate.ValueMember = "Value";
            }

            this.cmbBaudRate.SelectedValue = 0;

            this.rs232BaudRatesVisible = false;
        }

        private void InitCmbPorts()
        {
            Dictionary<string, CommPort> commPorts = new Dictionary<string, CommPort>();

            string usbPortName = string.Format("USB （{0}）", localization[Localization.formFirmwareUpdate_Auto]);
            commPorts[usbPortName] = null;

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

            //this.btnUpload.Enabled = portIsValid;
            //this.btnDownload.Enabled = portIsValid && crcFileIsExist;
        }

        private void InitRandKey()
        {
            byte[] randNumber = AEC.Tools.RandNumberHelper.GetRandNumbers(0x10);
            byte[] hash = AEC.Tools.HashHelper.MD5(randNumber);
            this.txtKey.Text = Comm.Utils.ByteArrayToHexStr(hash, string.Empty);

            this.keyStartTimestamp = DateTime.Now;

            this.txtFirmwarePackageFilePath.Clear();
            this.UpdateFirmwarePackage(null, null);
        }

        private void link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(sender.Equals(this.linkRefreshPort))
            {
                this.InitCmbPorts();
            }
            else if(sender.Equals(this.linkRefreshKey))
            {
                this.InitRandKey();
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.btnUpdate))
            {
                this.Download();
            }            
        }

        private void Download()
        {
            //Log.Clear();

            CommPort commPort = (CommPort)(this.cmbPorts.SelectedValue);

            if(commPort != null && commPort.type == CommPortType.RS232)
            {
                (commPort as RS232CommPort).baudRate = (int)this.cmbBaudRate.SelectedValue;
            }

            if(this.firmwarePackage == null)
            {
                return;
            }
            
            //if (commPort == null)
            //{
            //    Log.OutputLine("端口不能为空！");
            //    return;
            //}

            //string dspBinPath = this.txtDSPBinFilePath.Text.Trim();
            //string mcuBinPath = this.txtMCUBinFilePath.Text.Trim();

            //if(chkDSP.IsChecked)
            //{
            //    if (!File.Exists(dspBinPath))
            //    {
            //        Log.OutputLine("DSP bin 文件不存在");
            //        return;
            //    }
            //}
            
            //if(chkMCU.IsChecked)
            //{
            //    if (!File.Exists(mcuBinPath))
            //    {
            //        Log.OutputLine("MCU bin 文件不存在");
            //        return;
            //    }
            //}

            //DSPProgrammingTask dspPgmTask = new DSPProgrammingTask()
            //{
            //    dspCrcImageFilePath = dspBinPath,
            //    isDownload = chkDSP.IsChecked,
            //};

            //MCUProgrammingTask mcuPgmTask = new MCUProgrammingTask()
            //{
            //    mcuBinFilePath = mcuBinPath,
            //    mcuAppVectorOffset = 0x08006000,
            //    isDownload = chkMCU.IsChecked,
            //    restorePreset = chkRestorePreset.IsChecked,
            //};

            FirmwareUpdateTask firmwareUpdateTask = new FirmwareUpdateTask(commPort, this.firmwarePackage);

            FirmwareUpdateWrokerArgs firmwareUpgradeArgs = new FirmwareUpdateWrokerArgs(FirmwareUpdateProcess.FirmwareUpdate, firmwareUpdateTask);

            if (this.commService.StartWork(firmwareUpgradeArgs))
            {
                this.isWorking = true;
                this.lblInfo.ForeColor = normalColor;
                this.lblInfo.UpdateText(string.Empty, true);

                this.UpdateProgress(0, false);
            }
        }

        private void txt_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                System.IO.FileInfo fi = new System.IO.FileInfo(path);
                if ((fi.Attributes & FileAttributes.Directory) == FileAttributes.Directory || fi.Extension != FirmwarePackageSeralizer.MFP_FILE_EXTENSION)
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

        private void txt_DragDrop(object sender, DragEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if(textBox == null)
            {
                return;
            }

            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            //if()
            //System.IO.FileInfo fi = new System.IO.FileInfo(path);
            //if(fi.Attributes & FileAttributes.SparseFile)
            this.txtFirmwarePackageFilePath.Text = path;
            this.txtFirmwarePackageFilePath.SelectionStart = this.txtFirmwarePackageFilePath.TextLength;
        }

        private void cmbPorts_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                //this.rs232PortVisible = serialPorts.Length > 0;
                CommPort commPort = (this.cmbPorts.SelectedValue) as CommPort;
                this.rs232BaudRatesVisible = commPort != null && commPort.type == CommPortType.RS232;
            }
            catch(Exception ex)
            {
                this.rs232BaudRatesVisible = false;
                //Console.WriteLine(string.Format("cmbPorts_SelectedValueChanged()...Exception:{0}", ex.Message));
            }
        }

        public void UpdateFirmwarePackage(FirmwarePackage fp, byte[] hash)
        {
            if(fp != null && fp.CheckIsOk())
            {
                this.firmwarePackage = fp;

                StringBuilder sb = new StringBuilder();
                if(hash != null)
                {
                    Tools.StringHelper.StringBuilderAppendLine(sb, localization[Localization.formFirmwareUpdate_FileHash], Comm.Utils.ByteArrayToHexStr(hash, string.Empty));
                    sb.Append(string.Empty);
                }

                this.firmwarePackage.AppendToSB(sb);

                this.txtFirmwarePackageInfo.Text = sb.ToString();

                this.btnUpdate.Enabled = true;
            }
            else
            {
                this.txtFirmwarePackageInfo.Clear();
                this.btnUpdate.Enabled = false;
            }

            this.lblInfo.ForeColor = normalColor;
            this.lblInfo.UpdateText(string.Empty, true);
        }

        private System.IO.FileSystemWatcher fileWatcher = null;
        private string watchFilePath = string.Empty;
        
        private void StopFileWatcher()
        {
            if (this.fileWatcher != null && this.fileWatcher.EnableRaisingEvents)
            {
                this.fileWatcher.Created -= fileWatcher_EventHandler;
                this.fileWatcher.Changed -= fileWatcher_EventHandler;
                this.fileWatcher.Deleted -= fileWatcher_EventHandler;
                this.fileWatcher.EnableRaisingEvents = false;
                this.fileWatcher.Dispose();
            }
        }

        private void txtFirmwarePackageFilePath_TextChanged(object sender, EventArgs e)
        {
            string filePath = this.txtFirmwarePackageFilePath.Text.Trim();

            if (this.fileWatcher != null && watchFilePath != filePath)
            {
                this.watchFilePath = string.Empty;
                this.fileWatcher.Created -= fileWatcher_EventHandler;
                this.fileWatcher.Changed -= fileWatcher_EventHandler;
                this.fileWatcher.Deleted -= fileWatcher_EventHandler;
                this.fileWatcher.EnableRaisingEvents = false;
                this.fileWatcher.Dispose();
                this.fileWatcher = null;
            }

            if (this.FileChangedHandler(filePath) && this.fileWatcher == null)
            {
                this.watchFilePath = filePath;
                this.fileWatcher = new FileSystemWatcher(Path.GetDirectoryName(filePath), Path.GetFileName(filePath));
                this.fileWatcher.IncludeSubdirectories = false;
                this.fileWatcher.Created += fileWatcher_EventHandler;
                this.fileWatcher.Changed += fileWatcher_EventHandler;
                this.fileWatcher.Deleted += fileWatcher_EventHandler;
                this.fileWatcher.EnableRaisingEvents = true;
            }
        }

        private bool FileChangedHandler(string filePath)
        {
            if (!File.Exists(filePath) || Path.GetExtension(filePath) != FirmwarePackageSeralizer.MFP_FILE_EXTENSION)
            {
                this.UpdateFirmwarePackage(null, null);
                return false;
            }

            string errorMsg;
            byte[] key = null;
            if (!string.IsNullOrEmpty(this.txtKey.Text.Trim()))
            {
                key = Comm.Utils.HexStringToBytes(this.txtKey.Text.Trim(), out errorMsg);
                if (key == null || (key.Length != 0 && key.Length != FirmwarePackageSeralizer.KeyLength))
                {
                    this.txtFirmwarePackageInfo.Text = localization[Localization.formFirmwareUpdate_KeyDataError];
                    return true;
                }
            }

            FirmwarePackage fp = FirmwarePackageSeralizer.DeseralizeFromFile(filePath, key, out errorMsg);
            if (fp == null)
            {
                this.UpdateFirmwarePackage(null, null);
                this.txtFirmwarePackageInfo.Text = errorMsg;
                return true;
            }

            byte[] hash = Tools.HashHelper.GetFileMD5(filePath);
            if (hash == null)
            {
                this.UpdateFirmwarePackage(null, null);
                this.txtFirmwarePackageInfo.Text = localization[Localization.formFirmwareUpdate_GetFileHashError];
                return true;
            }

            this.UpdateFirmwarePackage(fp, hash);

            return true;
        }

        private void fileWatcher_EventHandler(object sender, FileSystemEventArgs e)
        {
            //Log.OutputLine("{0} : {1}", e.FullPath, e.ChangeType.ToString());
            //if (this.fileChangedTimer != null)
            //{
            //    this.fileChangedTimer.Stop();
            //    this.fileChangedTimer.Tick -= fileChangedTimer_Tick;
            //    this.fileChangedTimer.Dispose();
            //    this.fileChangedTimer = null;
            //}
            if(fileChangedTimer)
            {
                fileChangedTimerCount = 0;
            }
            else
            {
                fileChangedTimer = true;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (fileChangedTimer)
            {
                if (fileChangedTimerCount++ >= 1)
                {
                    fileChangedTimerCount = 0;
                    fileChangedTimer = false;
                    this.FileChangedHandler(this.txtFirmwarePackageFilePath.Text.Trim());
                }
            }

            //double period = (DateTime.Now - keyStartTimestamp).TotalMinutes;
            //Log.OutputLine("时间:{0}", period * 60);
            //if ((DateTime.Now - keyStartTimestamp).TotalMinutes > KeyValidityPeriod)
            if (this.currLicenseType != ActivationCode.MLicenseType.Unlimited)
            {
                double period = (DateTime.Now - keyStartTimestamp).TotalHours;
                if (period > KeyValidityPeriod || period < 0)
                {
                    this.InitRandKey();
                }
            }
        }

        

        //private void FileChangedHandler(object state)
        //{
        //    FileSystemEventArgs e = state as FileSystemEventArgs;

        //    Log.OutputLine("{0} : {1}", e.FullPath, e.ChangeType.ToString());

        //    if (e == null)
        //    {
        //        return;
        //    }

        //    if (e.ChangeType == WatcherChangeTypes.Deleted)
        //    {
        //        this.UpdateFirmwarePackage(null, null);
        //    }
        //    else
        //    {
        //        this.FileChangedHandler(e.FullPath);
        //    }
        //}

        private Form fromEncryptFirmwarePackage = null;
        private Form formExportFirmwarePackage = null;
        private Form formMCUFlashData = null;

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.F5)
                {
                    if(this.currLicenseType != ActivationCode.MLicenseType.Unauthorized)
                    {
                        if (MessageBox.Show(localization[Localization.formActivation_WhetherToCancelTheLicense], localization[Localization.formFirmwareUpdate], MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            this.currLicenseType = ActivationCode.MLicenseType.Unauthorized;
                            ActivationCode.ClearLicense();
                            this.UnactivedHandler();
                            this.InitRandKey();
                        }
                    }
                    else
                    {
                        FormActivation form = new FormActivation();
                        string activationCode;
                        ActivationCode.MLicenseType licenseType = form.ShowDialog(out activationCode);
                        if(licenseType != ActivationCode.MLicenseType.Unauthorized)
                        {
                            this.currLicenseType = licenseType;
                            if (this.currLicenseType != ActivationCode.MLicenseType.Temporary)
                            {
                                ActivationCode.SaveActivationCode(activationCode);
                            }
                            this.ActivedHandler(licenseType);
                        }
                    }
                }
                else if (AppConfig.isSupportFirmwareUpdateAuxiliaryFunctions)
                {
                    if (this.currLicenseType == ActivationCode.MLicenseType.Unauthorized)
                    {
                        return;
                    }

                    if (e.KeyCode == Keys.F6)
                    {
                        if (this.fromEncryptFirmwarePackage == null || this.fromEncryptFirmwarePackage.IsDisposed)
                        {
                            this.fromEncryptFirmwarePackage = new FormEncryptFirmwarePackage();
                        }

                        this.fromEncryptFirmwarePackage.Show();
                    }
                    else if (e.KeyCode == Keys.F7)
                    {
                        if (this.formExportFirmwarePackage == null || this.formExportFirmwarePackage.IsDisposed)
                        {
                            this.formExportFirmwarePackage = new FormExportFirmwarePackage();
                        }

                        this.formExportFirmwarePackage.Show();
                    }
                    else if (e.KeyCode == Keys.F8)
                    {
                        if (this.formMCUFlashData == null || this.formMCUFlashData.IsDisposed)
                        {
                            this.formMCUFlashData = new FormMCUFlashData();
                        }

                        this.formMCUFlashData.Show();
                    }
                }
            }
        }
    }
}
