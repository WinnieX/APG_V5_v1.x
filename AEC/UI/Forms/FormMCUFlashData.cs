using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using AEC.Comm.FirmwareUpdate;
using AEC.Comm;

namespace AEC.UI.Forms
{
    public partial class FormMCUFlashData : AEC.UI.MFormBase
    {
        private const string XML_FILE_FILTER = "xml文件|*.xml";
        private const string XML_FILE_EXTENSION = ".xml";

        private const string VKxModel = "VKx";
        private const string VKxSModel = "VKxS";
        private const string X5ProModel = "X5Pro";
        private const string QxModel = "Qx";

        private BindingSource portsBindingSource = new BindingSource();
        private BindingSource baudRatesBindingSource = new BindingSource();
        private BindingSource dataEncodingBindingSource = new BindingSource();

        private FirmwareUpdateService commService = new FirmwareUpdateService();

        private const string Title = "MCU Flash Data";
        private ActivationCode.MLicenseType currLicenseType;

        private bool rs232BaudRatesVisible
        {
            set
            {
                this.lblBaudRate.Visible = value;
                this.cmbBaudRate.Visible = value;
            }
        }

        public FormMCUFlashData()
        {
            InitializeComponent();
        }

        protected override void OnLoadHandler()
        {
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
            //this.RegisterEventHandler();
        }

        private void ActivedHandler(ActivationCode.MLicenseType licenseType)
        {
            if (licenseType == ActivationCode.MLicenseType.Temporary)
            {
                ActivationCode.ClearLicense();
            }

            this.panelExport.Enabled = true;
            this.panelImport.Enabled = true;

            this.UpdateText(licenseType);
        }

        private void UnactivedHandler()
        {
            this.panelExport.Enabled = false;
            this.panelImport.Enabled = false;

            this.UpdateText(ActivationCode.MLicenseType.Unauthorized);
        }

        private void UpdateText(ActivationCode.MLicenseType licenseType)
        {
            base.Text = Title + " | " + ActivationCode.GetLicenseDesc(licenseType) + " | " + AEC.Tools.VersionHelper.GetVersion();
        }

        private void InitUI()
        {
            this.InitCmbBaudRate();
            this.InitCmbPorts();
            this.InitCmbTargetDevice();
            this.InitDataEncoding();
        }

        private void InitDataEncoding()
        {
            this.dataEncodingBindingSource.Clear();

            this.dataEncodingBindingSource.DataSource = ByteArrayEncoding.StringToEncodingMap;

            this.cmbDataEncoding.DataSource = this.dataEncodingBindingSource;

            if (ByteArrayEncoding.StringToEncodingMap.Count > 0)
            {
                this.cmbDataEncoding.DisplayMember = "Key";
                this.cmbDataEncoding.ValueMember = "Value";
            }

            this.cmbDataEncoding.SelectedValue = ByteArrayEncoding.Encoding.Base64;
        }

        private const int RS232BaudRate57600 = 57600;
        private const int RS232BaudRate115200 = 115200;

        private void InitCmbBaudRate()
        {
            Dictionary<string, int> baudRates = new Dictionary<string, int>();
            baudRates[RS232BaudRate57600.ToString()] = RS232BaudRate57600;
            baudRates[RS232BaudRate115200.ToString()] = RS232BaudRate115200;

            this.baudRatesBindingSource.Clear();

            this.baudRatesBindingSource.DataSource = baudRates;

            this.cmbBaudRate.DataSource = this.baudRatesBindingSource;

            if (baudRates.Count > 0)
            {
                this.cmbBaudRate.DisplayMember = "Key";
                this.cmbBaudRate.ValueMember = "Value";
            }

            this.cmbBaudRate.SelectedValue = RS232BaudRate115200;

            this.rs232BaudRatesVisible = false;
        }

        private void InitCmbPorts()
        {
            Dictionary<string, CommPort> commPorts = new Dictionary<string, CommPort>();

            commPorts["USB（ 自动）"] = null;

            //AEC.Comm.CommDevices.HID.HIDDevice.InterfaceDetails[] hidInterfaces = FUHIDDevice.GetValidDevices();
            //if (hidInterfaces.Length > 0)
            //{
            //    foreach (var hid in hidInterfaces)
            //    {
            //        HIDCommPort hidCommPort = new HIDCommPort(hid.devicePath, hid.index);
            //        commPorts[hidCommPort.displayName] = hidCommPort;
            //    }
            //}

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

        private void linkRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.InitCmbPorts();
        }

        private void InitCmbTargetDevice()
        {
            string[] items = new string[] { VKxModel, VKxSModel, X5ProModel, QxModel};
            this.cmbTargetDevice.Items.Clear();
            this.cmbTargetDevice.Items.AddRange(items);

            this.cmbTargetDevice.SelectedIndex = 0;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if(sender.Equals(this.btnGenerate))
            {
                this.GenerateData();
            }
            else if (sender.Equals(this.btnExport))
            {
                this.ExportFlashData();
            }
            else if (sender.Equals(this.btnImport))
            {
                this.ImportFlashData();
            }
        }

        private ExportDataProperties GetExportDataProperties(bool checkAlign4k = true)
        {
            uint startAddress = 0;
            uint length = 0;
            uint endAddress = 0;
            if (!Utils.ParseHexStringToUint32(this.txtExportStartAddress.Text, out startAddress))
            {
                Tools.LogHelper.WriteInfo("起始地址输入有误！");
                return null;
            }

            if (Utils.ParseHexStringToUint32(this.txtExportDataLength.Text, out length))
            {
                endAddress = startAddress + length;

                if (startAddress >= endAddress)
                {
                    Tools.LogHelper.WriteInfo("数据长度输入有误，结束地址应大与起始地址");
                    return null;
                }

                this.txtExportEndAddress.Text = Utils.Uint32ToHexString(endAddress);
            }
            else if (Utils.ParseHexStringToUint32(this.txtExportEndAddress.Text, out endAddress))
            {
                if (startAddress >= endAddress)
                {
                    Tools.LogHelper.WriteInfo("结束数据输入有误，结束地址应大与起始地址");
                    return null;
                }

                length = endAddress - startAddress;
                this.txtExportDataLength.Text = Utils.Uint32ToHexString(length);
            }

            if (checkAlign4k)
            {
                if (startAddress % (uint)FirmwareUpdateService.Size1K != 0)
                {
                    Tools.LogHelper.WriteInfo("数据输入有误，起始地址没有1k对齐");
                    return null;
                }

                if (length % (uint)FirmwareUpdateService.Size1K != 0)
                {
                    Tools.LogHelper.WriteInfo("数据输入有误，数据长度没有1k对齐");
                    return null;
                }

                if (endAddress % (uint)FirmwareUpdateService.Size1K != 0)
                {
                    Tools.LogHelper.WriteInfo("数据输入有误，结束地址没有1k对齐");
                    return null;
                }
            }

            return new ExportDataProperties()
            {
                startAddress = startAddress,
                dataSize = length,
                encoding = (ByteArrayEncoding.Encoding)this.cmbDataEncoding.SelectedValue,
            };
        }

        private void GenerateData()
        {
            ExportDataProperties exportDataProperties = GetExportDataProperties(false);
            if (exportDataProperties == null)
            {
                return;
            }

            byte fill = 0xff;
            string error = string.Empty;
            if (!Utils.ParseStringToByte(this.txtFillData.Text, out fill, out error))
            {
                Tools.LogHelper.WriteInfo("填充数据输入有误:{0}", error);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = XML_FILE_FILTER;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            byte[] data = Utils.CreateByteArray(exportDataProperties.dataSize, fill);
            DataBlock dataBlock = new DataBlock(exportDataProperties.startAddress, data);

            string path = sfd.FileName;
            DataBlockSeralizer.SeralizeToFile(dataBlock, exportDataProperties.encoding, path);
            Tools.FileHelper.OpenFileLocation(path);
        }

        private void ExportFlashData()
        {
            CommPort commPort = (CommPort)(this.cmbPorts.SelectedValue);

            if (commPort != null && commPort.type == CommPortType.RS232)
            {
                (commPort as RS232CommPort).baudRate = (int)this.cmbBaudRate.SelectedValue;
            }

            ExportDataProperties exportDataProperties = GetExportDataProperties();
            if(exportDataProperties == null)
            {
                return;
            }

            MCUFlashDataLoadTask task = new MCUFlashDataLoadTask(exportDataProperties)
            {
                commPort = commPort,                
                DataLoaded = MCUFlashDataLoaded,
            };

            FirmwareUpdateWrokerArgs firmwareUpgradeArgs = new FirmwareUpdateWrokerArgs(FirmwareUpdateProcess.MCUFlashDataLoad, task);

            //byte[] data = Utils.CreateByteArray(length);
            ////data[0] = 0;
            //MCUFlashDataBlock dataBlock = new MCUFlashDataBlock(startAddress, data);

            //MCUFlashDataSeralizer.SeralizeToFile(dataBlock, task.encoding, "20240712.xml");

            //MCUFlashDataBlock dataBlcok2 = MCUFlashDataSeralizer.DeseralizeFromFile("20240712.xml");

            //MCUFlashDataSeralizer.SeralizeToFile(dataBlcok2, task.encoding, "20240712-2.xml");
            //return;
            
            if (this.commService.StartWork(firmwareUpgradeArgs))
            {
                //this.workIsRunning = true;
            }
        }

        private void MCUFlashDataLoaded(object sender, EventArgs e)
        {
            syncContext.Post(MCUFlashDataLoadedHandler, sender);            
        }

        private void MCUFlashDataLoadedHandler(object state)
        {
            MCUFlashDataLoadTask task = state as MCUFlashDataLoadTask;
            if (task == null || task.dataBlcok == null)
            {
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = XML_FILE_FILTER;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                string path = sfd.FileName;
                DataBlockSeralizer.SeralizeToFile(task.dataBlcok, task.encoding, sfd.FileName);
                Tools.FileHelper.OpenFileLocation(sfd.FileName);
            }
        }

        private void ImportFlashData()
        {
            CommPort commPort = (CommPort)(this.cmbPorts.SelectedValue);

            if (commPort != null && commPort.type == CommPortType.RS232)
            {
                (commPort as RS232CommPort).baudRate = (int)this.cmbBaudRate.SelectedValue;
            }

            string filePath = this.txtImportFilePath.Text.Trim();
            if (!System.IO.File.Exists(filePath))
            {
                this.ClearImportDataInfo();
                //Tools.LogHelper.WriteInfo("文件 {0} 不存在！", filePath);
                return;
            }

            DataBlock dataBlcok = DataBlockSeralizer.DeseralizeFromFile(filePath);
            if (dataBlcok == null)
            {
                this.ClearImportDataInfo();
                return;
            }

            MCUFlashDataSaveTask task = new MCUFlashDataSaveTask()
            {
                commPort = commPort,
                dataBlcok = dataBlcok,
            };

            FirmwareUpdateWrokerArgs firmwareUpgradeArgs = new FirmwareUpdateWrokerArgs(FirmwareUpdateProcess.MCUFlashDataSave, task);

            //byte[] data = Utils.CreateByteArray(length);
            ////data[0] = 0;
            //MCUFlashDataBlock dataBlock = new MCUFlashDataBlock(startAddress, data);

            //MCUFlashDataSeralizer.SeralizeToFile(dataBlock, task.encoding, "20240712.xml");

            //MCUFlashDataBlock dataBlcok2 = MCUFlashDataSeralizer.DeseralizeFromFile("20240712.xml");

            //MCUFlashDataSeralizer.SeralizeToFile(dataBlcok2, task.encoding, "20240712-2.xml");
            //return;

            if (this.commService.StartWork(firmwareUpgradeArgs))
            {
                //this.workIsRunning = true;
            }
        }

        private void cmbTargetDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.cmbTargetDevice.Text == VKxModel)
            {
                this.txtExportStartAddress.Text = Utils.Uint32ToHexString(VKxMCUFlashData.PRESET_DATA_START_ADDR);
                this.txtExportDataLength.Text = Utils.Uint32ToHexString(VKxMCUFlashData.PRESET_DATA_LENGTH);
                this.txtExportEndAddress.Text = Utils.Uint32ToHexString(VKxMCUFlashData.PRESET_DATA_END_ADDR);
            }
            else if (this.cmbTargetDevice.Text == VKxSModel)
            {
                this.txtExportStartAddress.Text = Utils.Uint32ToHexString(VKxSMCUFlashData.PRESET_DATA_START_ADDR);
                this.txtExportDataLength.Text = Utils.Uint32ToHexString(VKxSMCUFlashData.PRESET_DATA_LENGTH);
                this.txtExportEndAddress.Text = Utils.Uint32ToHexString(VKxSMCUFlashData.PRESET_DATA_END_ADDR);
            }
            else if (this.cmbTargetDevice.Text == X5ProModel)
            {
                this.txtExportStartAddress.Text = Utils.Uint32ToHexString(X5ProMCUFlashData.PRESET_DATA_START_ADDR);
                this.txtExportDataLength.Text = Utils.Uint32ToHexString(X5ProMCUFlashData.PRESET_DATA_LENGTH);
                this.txtExportEndAddress.Text = Utils.Uint32ToHexString(X5ProMCUFlashData.PRESET_DATA_END_ADDR);
            }
            else if (this.cmbTargetDevice.Text == QxModel)
            {
                this.txtExportStartAddress.Text = Utils.Uint32ToHexString(QxMCUFlashData.PRESET_DATA_START_ADDR);
                this.txtExportDataLength.Text = Utils.Uint32ToHexString(QxMCUFlashData.PRESET_DATA_LENGTH);
                this.txtExportEndAddress.Text = Utils.Uint32ToHexString(QxMCUFlashData.PRESET_DATA_END_ADDR);
            }
        }

        private void cmbPorts_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                //this.rs232PortVisible = serialPorts.Length > 0;
                CommPort commPort = (this.cmbPorts.SelectedValue) as CommPort;
                this.rs232BaudRatesVisible = commPort != null && commPort.type == CommPortType.RS232;
            }
            catch (Exception ex)
            {
                this.rs232BaudRatesVisible = false;
                Console.WriteLine(string.Format("cmbPorts_SelectedValueChanged()...Exception:{0}", ex.Message));
            }
        }

        private void txt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (sender.Equals(this.txtImportFilePath))
            {
                this.ExportFile();
            }
        }
        
        private void ExportFile()
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Filter = XML_FILE_FILTER;
            //if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    this.txtImportFilePath.Text = ofd.FileName;
            //}
            string filePath = this.txtImportFilePath.Text.Trim();
            if (!System.IO.File.Exists(filePath))
            {
                Tools.LogHelper.WriteInfo("文件 {0} 不存在！", filePath);
                return;
            }

            ByteArrayEncoding.Encoding encoding;
            DataBlock dataBlcok = DataBlockSeralizer.DeseralizeFromFile(filePath, out encoding);
            if (dataBlcok == null)
            {
                Tools.LogHelper.WriteInfo("文件 {0} 数据无效！", filePath);
                return;
            }

            if (encoding == ByteArrayEncoding.Encoding.Base64)
            {
                encoding = ByteArrayEncoding.Encoding.Hex;
            }
            else if (encoding == ByteArrayEncoding.Encoding.Hex)
            {
                encoding = ByteArrayEncoding.Encoding.Base64;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = XML_FILE_FILTER;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                DataBlockSeralizer.SeralizeToFile(dataBlcok, encoding, sfd.FileName);
                Tools.FileHelper.OpenFileLocation(sfd.FileName);
            }
        }

        private void txt_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                System.IO.FileInfo fi = new System.IO.FileInfo(path);
                if ((fi.Attributes & FileAttributes.Directory) == FileAttributes.Directory || fi.Extension != XML_FILE_EXTENSION)
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
            //TextBox textBox = (TextBox)sender;
            //if (textBox == null)
            //{
            //    return;
            //}

            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            //if()
            //System.IO.FileInfo fi = new System.IO.FileInfo(path);
            //if(fi.Attributes & FileAttributes.SparseFile)
            this.txtImportFilePath.Text = path;
        }

        private void txtImportFilePath_TextChanged(object sender, EventArgs e)
        {
            string filePath = this.txtImportFilePath.Text.Trim();
            if (!System.IO.File.Exists(filePath))
            {
                this.ImportFileIsInvalid = true;
                //Tools.LogHelper.WriteInfo("文件 {0} 不存在！", filePath);
                return;
            }

            DataBlock dataBlcok = DataBlockSeralizer.DeseralizeFromFile(filePath);
            if (dataBlcok == null)
            {
                this.ImportFileIsInvalid = true;
                return;
            }

            this.ImportFileIsInvalid = false;
            this.txtImportStartAddress.Text = Utils.Uint32ToHexString(dataBlcok.address);
            this.txtImportDataLength.Text = Utils.Uint32ToHexString(dataBlcok.size);
            this.txtImportEndAddress.Text = Utils.Uint32ToHexString(dataBlcok.endAddress);
        }

        private bool ImportFileIsInvalid
        {
            set
            {
                this.btnImport.Enabled = !value;
                if(value)
                {
                    ClearImportDataInfo();
                }
            }
        }

        private void ClearImportDataInfo()
        {
            this.txtImportStartAddress.Clear();
            this.txtImportDataLength.Clear();
            this.txtImportEndAddress.Clear();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.F5)
                {
                    if (this.currLicenseType != ActivationCode.MLicenseType.Unauthorized)
                    {
                        if (MessageBox.Show("是否注销许可?", Title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            this.currLicenseType = ActivationCode.MLicenseType.Unauthorized;
                            ActivationCode.ClearLicense();
                            this.UnactivedHandler();
                        }
                    }
                    else
                    {
                        FormActivation form = new FormActivation();
                        string activationCode;
                        ActivationCode.MLicenseType licenseType = form.ShowDialog(out activationCode);
                        if (licenseType != ActivationCode.MLicenseType.Unauthorized)
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
            }
        }
    }
}
