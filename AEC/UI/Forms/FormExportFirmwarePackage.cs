using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using AEC.Comm.FirmwareUpdate;
using AEC.Comm;

namespace AEC.UI.Forms
{
    public partial class FormExportFirmwarePackage : AEC.UI.MFormBase
    {
        private const string BIN_FILE_FILTER = "bin文件|*.bin";
        private const string BIN_FILE_EXTENSION = ".bin";
        private const string XML_FILE_FILTER = "xml文件|*.xml";
        private const string XML_FILE_EXTENSION = ".xml";

        private const string VKxModel = "VKx";
        private const string VKxSModel = "VKxS";
        private const string X5ProModel = "X5Pro";
        private const string QxModel = "Qx";

        private const uint DefaultMCUAppAddress = 0x08006000;
        private const uint VKxActivateAddress = VKxMCUFlashData.DEVICE_INFO_DATA_START_ADDR + 8;
        private const uint VKxSActivateAddress = VKxSMCUFlashData.DEVICE_INFO_DATA_START_ADDR + 8;
        private const uint X5ProActivateAddress = X5ProMCUFlashData.DEVICE_INFO_DATA_START_ADDR + 8;

        private const uint QxMCUAppAddress = 0x08010000;
        private const uint QxActivateAddress = QxMCUFlashData.DEVICE_INFO_DATA_START_ADDR + 8;

        private const string Title = "导出固件包";
        private ActivationCode.MLicenseType currLicenseType;

        public FormExportFirmwarePackage()
        {
            InitializeComponent();
        }

        protected override void OnLoadHandler()
        {
            base.Text = "导出固件升级包 | " + AEC.Tools.VersionHelper.GetVersion();
            this.InitUI();
            //this.RegisterEventHandler();
        }

        private void InitUI()
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

            this.InitBluetoothName();
            this.InitCmbTargetDevice();
            this.InitMCUAppAddress(DefaultMCUAppAddress);
            this.CustomDeviceInfoFileChanged();
        }

        private void ActivedHandler(ActivationCode.MLicenseType licenseType)
        {
            if (licenseType == ActivationCode.MLicenseType.Temporary)
            {
                ActivationCode.ClearLicense();
            }

            this.btnExport.Enabled = true;

            this.UpdateText(licenseType);
        }

        private void UnactivedHandler()
        {
            this.btnExport.Enabled = false;

            this.UpdateText(ActivationCode.MLicenseType.Unauthorized);
        }

        private void UpdateText(ActivationCode.MLicenseType licenseType)
        {
            base.Text = Title + " - " + ActivationCode.GetLicenseDesc(licenseType) + " - " + AEC.Tools.VersionHelper.GetVersion();
        }

        private void InitBluetoothName()
        {
            this.txtBluetoothName.Text = FirmwarePackage.DefBluetoothName;
        }

        private void InitMCUAppAddress(uint appAddress)
        {
            this.txtMCUAppAddress.Text = Utils.Uint32ToHexString(appAddress);
        }

        private void InitCmbTargetDevice()
        {
            string[] items = new string[] { VKxModel, VKxSModel, X5ProModel, QxModel};
            this.cmbTargetDevice.Items.Clear();
            this.cmbTargetDevice.Items.AddRange(items);

            this.cmbTargetDevice.SelectedIndex = 0;
        }

        private void txt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (sender.Equals(this.txtDSPBinFilePath))
            {
                SelectFile(this.txtDSPBinFilePath, BIN_FILE_FILTER);
            }
            else if (sender.Equals(this.txtMCUBinFilePath))
            {
                SelectFile(this.txtMCUBinFilePath, BIN_FILE_FILTER);
            }
            else if (sender.Equals(this.txtMCUAllData))
            {
                SelectFile(this.txtMCUAllData, XML_FILE_FILTER);
            }
            else if(sender.Equals(this.txtCustomDeviceInfo))
            {
                SelectFile(this.txtCustomDeviceInfo, XML_FILE_FILTER);
            }
            else if(sender.Equals(this.txtPresetData))
            {
                SelectFile(this.txtPresetData, XML_FILE_FILTER);
            }
        }

        private void SelectFile(TextBox textBox, string filter)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = filter;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox.Text = ofd.FileName;
            }
        }

        private void txt_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                System.IO.FileInfo fi = new System.IO.FileInfo(path);
                if ((fi.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    e.Effect = DragDropEffects.None;
                }
                else if ((sender.Equals(this.txtMCUBinFilePath) || sender.Equals(this.txtDSPBinFilePath)) && fi.Extension == BIN_FILE_EXTENSION)
                {
                    e.Effect = DragDropEffects.All;
                }
                else if ((sender.Equals(this.txtMCUAllData) || (sender.Equals(this.txtCustomDeviceInfo) || sender.Equals(this.txtPresetData)) && fi.Extension == XML_FILE_EXTENSION))
                {
                    e.Effect = DragDropEffects.All;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
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
            if (textBox == null)
            {
                return;
            }

            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            //if()
            //System.IO.FileInfo fi = new System.IO.FileInfo(path);
            //if(fi.Attributes & FileAttributes.SparseFile)
            textBox.Text = path;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.btnExport))
            {
                this.ExportFirmwarePackage();
            }
        }

        private void ExportFirmwarePackage()
        {
            string errorMsg = string.Empty;
            FirmwarePackage fp = GenerateFirmwarePackage(out errorMsg);
            if(fp == null)
            {
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    gtMessageBox.ShowMessage(this, errorMsg, "导出固件包错误");
                }
                
                return;
            }

            byte[] key;
            if (!GetEncriptionKey(out key, out errorMsg))
            {
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    gtMessageBox.ShowMessage(this, errorMsg, "导出固件包错误");
                }

                return;
            }

            this.SaveFirmwarePackage(fp, key);
            //FirmwarePackage fp = FirmwarePackageSeralizer.DeseralizeFromFile(@"D:\Users\wuwan\Desktop\MFP\VK3.mfp", key, out errorMsg);
            //FirmwarePackage fp = FirmwarePackageSeralizer.DeseralizeFromFile(@"D:\Users\wuwan\Desktop\MFP\VK3_不加密.mfp", key, out errorMsg);
            //return;
            
        }

        private void SaveFirmwarePackage(FirmwarePackage firmwarePackage, byte[] key)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = FirmwarePackageSeralizer.DEF_MFP_FILE_FILTER;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    string filePath = sfd.FileName;
                    if (key != null)
                    {
                        string hash = Utils.ByteArrayToHexStr(key, string.Empty);
                        hash = hash.Substring(hash.Length - 8);
                        filePath = string.Format(@"{0}{1}{2}_{3}{4}",
                            Path.GetDirectoryName(sfd.FileName),
                            Path.DirectorySeparatorChar,
                            Path.GetFileNameWithoutExtension(sfd.FileName),
                            hash,
                            Path.GetExtension(sfd.FileName));
                        //Log.OutputLine("path = {0}", filePath);
                    }
                    FirmwarePackageSeralizer.SeralizeToFile(firmwarePackage, key, filePath);
                    Tools.LogHelper.WriteInfo("导出文件 Hash: {0}", Utils.ByteArrayToHexStr(Tools.HashHelper.GetFileMD5(filePath), string.Empty));
                    Tools.FileHelper.OpenFileLocation(filePath);
                }
                catch (Exception e)
                {
                    gtMessageBox.ShowMessage(this, e.Message, "导出固件包错误");
                }
            }
        }

        private bool GetEncriptionKey(out byte[] key, out string errorMsg)
        {
            errorMsg = string.Empty;

            if (this.chkEncryptedExport.IsChecked)
            {
                key = Utils.HexStringToBytes(this.txtKey.Text, out errorMsg);
                if (key == null || key.Length != FirmwarePackageSeralizer.KeyLength)
                {
                    errorMsg = "密钥数据为空或有误";
                    return false;
                }
            }
            else
            {
                key = null;
            }

            return true;
        }

        private FirmwarePackage GenerateFirmwarePackage(out string errorMsg)
        {
            errorMsg = string.Empty;

            FirmwarePackage firmwarePackage = new FirmwarePackage();

            firmwarePackage.targetModel = this.cmbTargetDevice.Text;
            firmwarePackage.descirption = this.txtDescription.Text.Trim();

            firmwarePackage.mcuModel = this.cmbTargetMCUModel.Text.Trim();

            firmwarePackage.isForcedUpdateBTInfo = this.chkForcedUpdateBTInfo.IsChecked;
            firmwarePackage.isRetainBTDeviceInfo = this.chkRetainBTDeviceInfo.IsChecked;
            firmwarePackage.isEnableRandomBTAddress = this.chkEnableRandomBTAddress.IsChecked;
            firmwarePackage.BTName = this.txtBluetoothName.Text.Trim();

            uint mcuAppActivateAddress = 0;
            if (!Utils.ParseHexStringToUint32(this.txtActivateAddress.Text.Trim(), out mcuAppActivateAddress))
            {
                errorMsg = "MCU App Address 数据为空或有误";
                return null;
            }
            firmwarePackage.mcuAppActivateAddress = mcuAppActivateAddress;

            firmwarePackage.dspBin = InitBin(FirmwareUpdateService.FirmwareType.DSP, this.chkDSP.IsChecked, this.txtDSPBinFilePath.Text.Trim(), out errorMsg);
            if (firmwarePackage.dspBin == null)
            {
                return null;
            }

            uint mcuAppAddress = 0;
            if (!Utils.ParseHexStringToUint32(this.txtMCUAppAddress.Text.Trim(), out mcuAppAddress))
            {
                errorMsg = "MCU App Address 数据为空或有误";
                return null;
            }

            firmwarePackage.mcuBin = InitBin(FirmwareUpdateService.FirmwareType.MCU_APP, this.chkMCU.IsChecked, this.txtMCUBinFilePath.Text.Trim(), out errorMsg);
            if (firmwarePackage.mcuBin == null)
            {
                return null;
            }
            firmwarePackage.mcuBin.offsetAddress = mcuAppAddress;

            firmwarePackage.mcuAllDataBlock = InitFlashDataBlock(this.txtMCUAllData.Text.Trim());
            firmwarePackage.isUpdateMCUAllData = this.chkUpdateMCUAllData.IsChecked;

            if (this.chkUpdateMCUAllData.IsChecked && firmwarePackage.mcuAllDataBlock == null)
            {
                errorMsg = "MCU Flash 数据为空或有误";
                Tools.LogHelper.WriteInfo("MCU Flash 数据为空或有误");
                return null;
            }

            firmwarePackage.presetDataBlock = InitFlashDataBlock(this.txtPresetData.Text.Trim());
            firmwarePackage.isUpdateCustomPresetData = this.chkUpdatePresetData.IsChecked;

            if (this.chkUpdatePresetData.IsChecked && firmwarePackage.presetDataBlock == null)
            {
                errorMsg = "MCU 预设数据为空或有误";
                Tools.LogHelper.WriteInfo("MCU 预设数据为空或有误");
                return null;
            }

            if (chkUpdateCustomDeviceInfo.IsChecked && !this.chkUpdateMCUAllData.IsChecked)
            {
                errorMsg = "开启自定义设备信息必须要开启更新MCU Flash数据";
                Tools.LogHelper.WriteInfo("开启自定义设备信息必须要开启更新MCU Flash数据");
                return null;
            }

            firmwarePackage.customDeviceInfoDataBlocks = InitCustomDeviceBlocks(this.chkUpdateCustomDeviceInfo.IsChecked);
            firmwarePackage.isUpdateCustomDeviceInfo = this.chkUpdateCustomDeviceInfo.IsChecked;
            if (firmwarePackage.customDeviceInfoDataBlocks == null)
            {
                errorMsg = "MCU 自定义设备信息错误";
                Tools.LogHelper.WriteInfo("MCU 自定义设备信息错误");
                return null;
            }

            

            return firmwarePackage;
        }

        private const int CustomDeviceInfoItemSize = 21;

        private DataBlock[] InitCustomDeviceBlocks(bool isRequired)
        {
            CustomDeviceInfo cdi = GetCustomDeviceInfoFromUI();
            if(cdi == null)
            {
                if (isRequired)
                {
                    return null;
                }
                else
                {
                    return new DataBlock[0];
                }
            }

            byte[] customDeviceModel = BitConverter.GetBytes(cdi.deviceModelCode);            
            byte[] enableCustomDeviceInfo = BitConverter.GetBytes((uint)(cdi.enableCustomDeviceInfo ? 1 : 0));

            byte[] customDeviceInfoBuffer = Utils.CreateByteArray(CustomDeviceInfoItemSize * 4, 0);

            if (!FillCustomDeviceInfoItemToBuffer(cdi.customManufacturer, customDeviceInfoBuffer, 0) ||
                !FillCustomDeviceInfoItemToBuffer(cdi.customModel, customDeviceInfoBuffer, 1) ||
                !FillCustomDeviceInfoItemToBuffer(cdi.customVersion, customDeviceInfoBuffer, 2) ||
                !FillCustomDeviceInfoItemToBuffer(cdi.customDescription, customDeviceInfoBuffer, 3))
            {
                Tools.LogHelper.WriteInfo("FillCustomDeviceInfoItemToBuffer()...异常");
                return null;
            }

            List<DataBlock> dataBlocks = new List<DataBlock>();

            if(this.cmbTargetDevice.Text == VKxModel)
            {
                if(this.chkWriteCustomModelCode.IsChecked)
                {
                    dataBlocks.Add(new DataBlock(VKxMCUFlashData.DEVICE_INFO_DATA_START_ADDR + 8 + 16, customDeviceModel));
                }

                if (this.chkWriteCustomDeviceInfo.IsChecked)
                {
                    dataBlocks.Add(new DataBlock(VKxMCUFlashData.DEVICE_INFO_DATA_START_ADDR + 8 + 16 + 4, enableCustomDeviceInfo));
                    dataBlocks.Add(new DataBlock(VKxMCUFlashData.DEVICE_CUSTOM_MODULE_START_ADDR, customDeviceInfoBuffer));
                }
            }
            else if(this.cmbTargetDevice.Text == VKxSModel)
            {
                byte[] x5_customDeviceModel = BitConverter.GetBytes(cdi.x5_deviceModelCode);

                byte[] deviceModel = Utils.ConcatByteArray(customDeviceModel, x5_customDeviceModel);

                if (this.chkWriteCustomModelCode.IsChecked)
                {
                    dataBlocks.Add(new DataBlock(VKxSMCUFlashData.DEVICE_INFO_DATA_START_ADDR + 8 + 16, deviceModel));
                }

                if (this.chkWriteCustomDeviceInfo.IsChecked)
                {
                    byte[] deviceInfo = Utils.ConcatByteArray(enableCustomDeviceInfo, customDeviceInfoBuffer);
                    dataBlocks.Add(new DataBlock(VKxSMCUFlashData.DEVICE_INFO_DATA_START_ADDR + 8 + 16 + 8, deviceInfo));
                }
            }

            return dataBlocks.ToArray();
        }

        public static bool FillCustomDeviceInfoItemToBuffer(string item, byte[] buffer, int index)
        {
            try
            {
                byte[] itemBuffer = CustomDeviceInfoItemToBuffer(item);
                Array.Copy(itemBuffer, 0, buffer, index * CustomDeviceInfoItemSize, itemBuffer.Length);

                return true;
            }
            catch(Exception )
            {
                return false;
            }
        }

        public static byte[] CustomDeviceInfoItemToBuffer(string item)
        {
            byte[] data = ASCIIEncoding.ASCII.GetBytes(item);
            if(data.Length > CustomDeviceInfoItemSize - 1)
            {
                Array.Resize(ref data, CustomDeviceInfoItemSize - 1);
            }

            return data;
        }

        private CustomDeviceInfo GetCustomDeviceInfoFromUI()
        {
            try
            {
                CustomDeviceInfo cdi = new CustomDeviceInfo()
                {
                    isWriteDeviceModelCode = this.chkWriteCustomModelCode.IsChecked,
                    isWriteCustomDeviceInfo = this.chkWriteCustomDeviceInfo.IsChecked,
                    enableCustomDeviceInfo = this.chkEnableCustomDeviceInfo.IsChecked,
                    customManufacturer = this.txtCustomManufacturer.Text,
                    customModel = this.txtCustomModel.Text,
                    customVersion = this.txtCustomVersion.Text,
                    customDescription = this.txtCustomDescription.Text,
                };
                uint modelCode;
                if (Utils.ParseHexStringToUint32(this.txtCustomDeviceModelCode.Text.Trim(), out modelCode))
                {
                    cdi.deviceModelCode = modelCode;
                }
                else
                {
                    Tools.LogHelper.WriteInfo("ParseHexStringToUint32()...错误:自定义型号代码数据有误");

                    return null;
                }

                if(this.cmbTargetDevice.Text == VKxSModel)
                {
                    if (Utils.ParseHexStringToUint32(this.txtX5CustomDeviceModelCode.Text.Trim(), out modelCode))
                    {
                        cdi.x5_deviceModelCode = modelCode;
                    }
                    else
                    {
                        Tools.LogHelper.WriteInfo("ParseHexStringToUint32()...错误:X5自定义型号代码数据有误");

                        return null;
                    }
                }

                return cdi;
            }
            catch(Exception e)
            {
                Tools.LogHelper.WriteInfo("GetCustomDeviceInfoFromUI()...异常:{0}", e.Message);
            }

            return null;
        }

        private DataBlock InitFlashDataBlock(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    return DataBlockSeralizer.DeseralizeFromFile(filePath);
                }
            }
            catch(Exception e)
            {
                Tools.LogHelper.WriteInfo("InitFlashDataBlock()...文件{0}, 异常:{1}", filePath, e.Message);
            }

            return null;
        }

        private FirmwareBin InitBin(FirmwareUpdateService.FirmwareType type, bool isEnabled, string path, out string errorMsg)
        {
            errorMsg = string.Empty;

            string firmwareName = type == FirmwareUpdateService.FirmwareType.MCU_APP ? "MCU" : "DSP"; 

            FirmwareBin firmwareBin = new FirmwareBin();

            firmwareBin.isDownloadBin = isEnabled;

            if (firmwareBin.isDownloadBin)
            {
                if (!File.Exists(path))
                {
                    errorMsg = string.Format("{0} bin 文件不存在！", firmwareName);

                    return null;
                }
            }
            else if (!File.Exists(path))
            {
                return firmwareBin;
            }

            try
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);
                firmwareBin.originalName = Path.GetFileName(path);
                firmwareBin.binLength = (uint)fileInfo.Length;
                firmwareBin.binTimestamp = fileInfo.LastWriteTime;

                using (var fileStream = File.Open(path, FileMode.Open))
                {
                    int length = (int)fileStream.Length;

                    firmwareBin.binData = new byte[length];
                    fileStream.Read(firmwareBin.binData, 0, (int)fileStream.Length);
                }
            }
            catch (Exception e)
            {
                errorMsg = string.Format("读取 bin 文件 {0} 异常:{1}", path, e.Message);
                //Console.WriteLine("GetMcuBinFileBuffer()...异常：{0}", e.Message);
                return null;
            }

            //System.Diagnostics.Debug.Assert(firmwareBin.binData != null);

            return firmwareBin;
        }

        //private bool InitDSPBin(FirmwarePackage firmwarePackage, byte[] key, out string errorMsg)
        //{
        //    errorMsg = string.Empty;

        //    FirmwareBin firmwareBin = new FirmwareBin();

        //    firmwareBin.isDownloadBin = this.chkDSP.IsChecked;
        //    string path = this.txtDSPBinFilePath.Text.Trim();

        //    if(firmwareBin.isDownloadBin)
        //    {
        //        if(!File.Exists(path))
        //        {
        //            errorMsg = "DSP bin 文件不存在！";

        //            return false;
        //        }
        //    }

        //    byte[] buffer = null;

        //    try
        //    {
        //        System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);

        //        using (var fileStream = File.Open(path, FileMode.Open))
        //        {
        //            int length = (int)fileStream.Length;
                    
        //            buffer = new byte[length];
        //            fileStream.Read(buffer, 0, (int)fileStream.Length);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Read Bin File...异常：{0}", e.Message);
        //        return false;
        //    }

        //    firmwareBin.binBufferHash = Utils.ByteArrayToHexStr(AEC.Tools.HashHelper.SHA256(buffer), string.Empty);

        //    return true;
        //}

        //private byte[] GetKey(string keyStr, out string errorMsg)
        //{
        //    errorMsg = string.Empty;

        //    try
        //    {
        //        return Utils.HexStringTorByteArray(keyStr);
        //    }
        //    catch(Exception e)
        //    {
        //        errorMsg = string.Format("hex string 转 字节数组有误:{0}", e.Message);
        //        return null;
        //    }
        //}

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            if(sender.Equals(this.chkUpdateMCUAllData))
            {
                if(!this.chkUpdateMCUAllData.IsChecked)
                {
                    this.chkUpdateCustomDeviceInfo.IsCheckedInit = false;
                }
            }
            else if (sender.Equals(this.chkUpdateCustomDeviceInfo))
            {
                if (this.chkUpdateCustomDeviceInfo.IsChecked)
                {
                    this.chkUpdateMCUAllData.IsCheckedInit = true;
                }
            }
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            if (sender.Equals(this.txtCustomDeviceInfo))
            {
                this.CustomDeviceInfoFileChanged();
            }
        }

        private void CustomDeviceInfoFileChanged()
        {
            string path = this.txtCustomDeviceInfo.Text.Trim();
            if(!File.Exists(path))
            {
                CustomDeviceInfoFileIsInvalid = true;
                return;
            }

            string defName = string.Empty;
            Dictionary<string, CustomDeviceInfo> customMap = ParseCustomDeviceInfoFile(path, out defName);
            if(customMap == null || customMap.Count == 0 || !customMap.ContainsKey(defName))
            {
                Tools.LogHelper.WriteInfo("数据错误");
                return;
            }

            InitCmbCustomDeviceInfoTask(customMap, defName);

            this.cmbCustomDeviceInfoTask.SelectedItem = defName;

            CustomDeviceInfoFileIsInvalid = false;
        }

        private BindingSource customDeviceInfoTaskBindingSource = new BindingSource();

        private void InitCmbCustomDeviceInfoTask(Dictionary<string, CustomDeviceInfo> customMap, string defTask)
        {
            this.customDeviceInfoTaskBindingSource.Clear();

            this.customDeviceInfoTaskBindingSource.DataSource = customMap;

            this.cmbCustomDeviceInfoTask.DataSource = this.customDeviceInfoTaskBindingSource;

            if (customMap.Count > 0)
            {
                this.cmbCustomDeviceInfoTask.DisplayMember = "Key";
                this.cmbCustomDeviceInfoTask.ValueMember = "Value";
            }
        }

        private bool CustomDeviceInfoFileIsInvalid
        {
            set
            {
                this.cmbCustomDeviceInfoTask.Enabled = !value;
                if (value)
                {
                    this.customDeviceInfoTaskBindingSource.Clear();
                }
            }
        }

        internal static Dictionary<string, CustomDeviceInfo> ParseCustomDeviceInfoFile(string filePath, out string defName)
        {
            defName = string.Empty;

            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(filePath);

                if (xmldoc.DocumentElement != null &&
                xmldoc.DocumentElement.LocalName == "task_group")
                {
                    XmlNode rootNode = xmldoc.DocumentElement as XmlNode;

                    XmlNode defTaskNode = rootNode.SelectSingleNode("default_task");

                    defName = defTaskNode.InnerText;

                    XmlNodeList nodeList = rootNode.SelectNodes("task");

                    Dictionary<string, CustomDeviceInfo> customDeviceInfoMap = new Dictionary<string, CustomDeviceInfo>();

                    foreach (XmlNode node in nodeList)
                    {
                        string taskName = (node as XmlElement).GetAttribute("name");

                        if(string.IsNullOrEmpty(taskName))
                        {
                            continue;
                        }

                        CustomDeviceInfo cdi = new CustomDeviceInfo();

                        foreach(XmlNode item in node.SelectNodes("item"))
                        {
                            string name = (item as XmlElement).GetAttribute("name");

                            if (string.IsNullOrEmpty(taskName))
                            {
                                continue;
                            }

                            //Log.OutputLine("name = {0}", name);

                            switch(name)
                            {
                                case "is_write_custom_device_model_code":
                                    cdi.isWriteDeviceModelCode = StringToBool(item.InnerText);
                                    break;

                                case "custom_device_model_code":
                                    cdi.deviceModelCode = StringToUint32(item.InnerText);
                                    break;

                                case "x5_custom_device_model_code":
                                    cdi.x5_deviceModelCode = StringToUint32(item.InnerText);
                                    break;

                                case "is_write_custom_device_info":
                                    cdi.isWriteCustomDeviceInfo = StringToBool(item.InnerText);
                                    break;

                                case "custom_device_info":
                                    foreach (XmlNode cdiItem in item.SelectNodes("item"))
                                    {
                                        string cdiItemName = (cdiItem as XmlElement).GetAttribute("name");

                                        if (string.IsNullOrEmpty(cdiItemName))
                                        {
                                            continue;
                                        }

                                        switch (cdiItemName)
                                        {
                                            case "enable_custom_device_model":
                                                cdi.enableCustomDeviceInfo = StringToBool(cdiItem.InnerText);
                                                break;

                                            case "custom_manufacturer":
                                                cdi.customManufacturer = cdiItem.InnerText;
                                                break;

                                            case "custom_device_model":
                                                cdi.customModel = cdiItem.InnerText;
                                                break;

                                            case "custom_device_cersion":
                                                cdi.customVersion = cdiItem.InnerText;
                                                break;

                                            case "custom_device_description":
                                                cdi.customDescription = cdiItem.InnerText;
                                                break;

                                            default:
                                                break;  
                                        }
                                    }
                                    break;

                                default:
                                    break;                                    
                            }
                        }

                        customDeviceInfoMap[taskName] = cdi;
                    }

                    return customDeviceInfoMap;
                }

            }
            catch (Exception ex)
            {
                Tools.LogHelper.WriteInfo("ParseCustomDeviceInfoFile()...文件{0}, 异常:{1}", filePath, ex.Message);
                System.Diagnostics.Debug.Print(ex.Message);
            }

            return null;
        }

        public static bool StringToBool(string s)
        {
            return s == "1" ? true : false;
        }

        public static uint StringToUint32(string s)
        {
            try
            {
                return Convert.ToUInt32(s);
            }
            catch(Exception )
            {
                return 0;
            }
        }

        internal class CustomDeviceInfo
        {
            public bool isWriteDeviceModelCode { get; set; }
            public uint deviceModelCode { get; set; }
            public uint x5_deviceModelCode { get; set; }
            public bool isWriteCustomDeviceInfo { get; set; }
            public bool enableCustomDeviceInfo { get; set; }
            public string customManufacturer { get; set; }
            public string customModel { get; set; }
            public string customVersion { get; set; }
            public string customDescription { get; set; }
        }

        private void cmbCustomDeviceInfoTask_SelectedValueChanged(object sender, EventArgs e)
        {
            CustomDeviceInfo cdi = this.cmbCustomDeviceInfoTask.SelectedValue as CustomDeviceInfo;

            if(cdi == null)
            {
                return;
            }

            this.chkWriteCustomModelCode.IsChecked = cdi.isWriteDeviceModelCode;
            this.txtCustomDeviceModelCode.Text = Utils.Uint32ToHexString(cdi.deviceModelCode);
            this.txtCustomDeviceModelCode.SelectionStart = this.txtCustomDeviceModelCode.TextLength;
            this.txtX5CustomDeviceModelCode.Text = Utils.Uint32ToHexString(cdi.x5_deviceModelCode);
            this.txtX5CustomDeviceModelCode.SelectionStart = this.txtX5CustomDeviceModelCode.TextLength;
            this.chkWriteCustomDeviceInfo.IsChecked = cdi.isWriteCustomDeviceInfo;
            this.chkEnableCustomDeviceInfo.IsChecked = cdi.enableCustomDeviceInfo;
            this.txtCustomManufacturer.Text = cdi.customManufacturer;
            this.txtCustomModel.Text = cdi.customModel;
            this.txtCustomVersion.Text = cdi.customVersion;
            this.txtCustomDescription.Text = cdi.customDescription;
        }

        private void cmbTargetDevice_SelectedValueChanged(object sender, EventArgs e)
        {
            this.txtX5CustomDeviceModelCode.Visible = this.cmbTargetDevice.Text == VKxSModel;
            if(this.cmbTargetDevice.Text == VKxModel)
            {
                this.InitMCUAppAddress(DefaultMCUAppAddress);
                this.txtActivateAddress.Text = Utils.Uint32ToHexString(VKxActivateAddress);
            }
            else if (this.cmbTargetDevice.Text == VKxSModel)
            {
                this.InitMCUAppAddress(DefaultMCUAppAddress);
                this.txtActivateAddress.Text = Utils.Uint32ToHexString(VKxSActivateAddress);
            }
            else if (this.cmbTargetDevice.Text == X5ProModel)
            {
                this.InitMCUAppAddress(DefaultMCUAppAddress);
                this.txtActivateAddress.Text = Utils.Uint32ToHexString(X5ProActivateAddress);
            }
            else if(this.cmbTargetDevice.Text == QxModel)
            {
                this.InitMCUAppAddress(QxMCUAppAddress);
                this.txtActivateAddress.Text = Utils.Uint32ToHexString(QxActivateAddress);
            }
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
