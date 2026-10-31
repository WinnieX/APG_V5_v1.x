using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AEC.Comm.FirmwareUpdate;
using AEC.Comm;

namespace AEC.UI.Forms
{
    public partial class FormEncryptFirmwarePackage : AEC.UI.MFormBase
    {
        private const string Title = "加密固件包";
        private ActivationCode.MLicenseType currLicenseType;

        public FormEncryptFirmwarePackage()
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
        }

        private void ActivedHandler(ActivationCode.MLicenseType licenseType)
        {
            if (licenseType == ActivationCode.MLicenseType.Temporary)
            {
                ActivationCode.ClearLicense();
            }

            this.txtKey.Enabled = true;
            this.txtKey.ReadOnly = false;

            this.UpdateText(licenseType);
        }

        private void UnactivedHandler()
        {
            this.txtKey.Enabled = false;
            this.txtKey.ReadOnly = true;

            this.UpdateText(ActivationCode.MLicenseType.Unauthorized);
        }

        private void UpdateText(ActivationCode.MLicenseType licenseType)
        {
            base.Text = Title + " | " + ActivationCode.GetLicenseDesc(licenseType) + " | " + AEC.Tools.VersionHelper.GetVersion();
        }

        private FirmwarePackage firmwarePackage;
        private bool keyIsOk = false;

        private void txtKey_TextChanged(object sender, EventArgs e)
        {
            //this.btnEncryptFP.Enabled = this.txtKey.TextLength == this.txtKey.MaxLength;
            this.keyIsOk = this.txtKey.TextLength == this.txtKey.MaxLength;
            this.UpdateEncryptFPButtonState();
        }

        private void txt_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            this.txtFirmwarePackageFilePath.Text = path;
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

        private void txt_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (sender.Equals(this.txtFirmwarePackageFilePath))
            {
                this.SelectFirmwarePackageFile();
            }
        }

        private void txtFirmwarePackageFilePath_TextChanged(object sender, EventArgs e)
        {
            string filePath = this.txtFirmwarePackageFilePath.Text.Trim();
            if (!File.Exists(filePath) || Path.GetExtension(filePath) != FirmwarePackageSeralizer.MFP_FILE_EXTENSION)
            {
                this.UpdateFirmwarePackageUI(null, "固件包文件不存在或者文件扩展名错误");
                return;
            }

            string errorMsg;
            FirmwarePackage fp = FirmwarePackageSeralizer.DeseralizeFromFile(filePath, null, out errorMsg);
            if (fp == null)
            {
                this.UpdateFirmwarePackageUI(null, "固件包文件数据错误或已经被加密，请选择未加密的固件包");
                return;
            }

            byte[] hash = Tools.HashHelper.GetFileMD5(filePath);
            if (hash == null)
            {
                this.UpdateFirmwarePackageUI(null, "获取文件Hash错误！");
                return;
            }

            this.UpdateFirmwarePackageUI(fp, Utils.ByteArrayToHexStr(hash, string.Empty));
        }

        public void UpdateFirmwarePackageUI(FirmwarePackage fp, string fpInfo)
        {
            if (fp != null && fp.CheckIsOk())
            {
                StringBuilder sb = new StringBuilder();
                if (!string.IsNullOrEmpty(fpInfo))
                {
                    Tools.StringHelper.StringBuilderAppendLine(sb, "文件 Hash: {0}", fpInfo);
                    sb.Append(string.Empty);
                }

                fp.AppendBaseInfoToSB(sb);

                this.txtFirmwarePackageInfo.Text = sb.ToString();

                string errMsg = string.Empty;

                this.chkDSPFirmware.Enabled = fp.dspBin.binIsOk;
                this.chkMCUFirmware.Enabled = fp.mcuBin.binIsOk;
                this.chkUpdateMCUAllData.Enabled = fp.CheckMcuAllDataBlockIsOK(out errMsg);
                this.chkUpdateCustomDeviceInfo.Enabled = fp.CheckCustomDeviceInfoDataBlocksIsOk(out errMsg);
                this.chkUpdatePresetData.Enabled = fp.CheckPresetDataBlockIsOK(out errMsg);

                this.txtDSPFirmwareBinInfo.Enabled = fp.dspBin.binIsOk;
                this.txtMCUFirmwareBinInfo.Enabled = fp.mcuBin.binIsOk;                
                this.txtMCUAllData.Enabled = fp.CheckMcuAllDataBlockIsOK(out errMsg);
                this.txtCustomDeviceInfo.Enabled = fp.CheckCustomDeviceInfoDataBlocksIsOk(out errMsg);
                this.txtCustomPresetData.Enabled = fp.CheckPresetDataBlockIsOK(out errMsg);

                this.chkDSPFirmware.IsChecked = fp.dspBin.isDownloadBin;
                this.chkMCUFirmware.IsChecked = fp.mcuBin.isDownloadBin;
                this.chkUpdateMCUAllData.IsChecked = fp.isUpdateMCUAllData;
                this.chkUpdateCustomDeviceInfo.IsChecked = fp.isUpdateCustomDeviceInfo;
                this.chkUpdatePresetData.IsChecked = fp.isUpdateCustomPresetData;

                if (fp.dspBin.binIsOk)
                {
                    this.txtDSPFirmwareBinInfo.Text = fp.dspBin.ToString("DSP", false);
                }
                else
                {
                    this.txtDSPFirmwareBinInfo.Clear();
                }

                if (fp.mcuBin.binIsOk)
                {
                    this.txtMCUFirmwareBinInfo.Text = fp.mcuBin.ToString("MCU_APP", false);
                }
                else
                {
                    this.txtMCUFirmwareBinInfo.Clear();
                }

                if (fp.CheckMcuAllDataBlockIsOK(out errMsg))
                {
                    this.txtMCUAllData.Text = string.Format("地址: {0}, 长度：{1}", Utils.Uint32ToHexString(fp.mcuAllDataBlock.address), Utils.Uint32ToHexString(fp.mcuAllDataBlock.size));
                }                

                if (fp.CheckCustomDeviceInfoDataBlocksIsOk(out errMsg))
                {
                    StringBuilder temp = new StringBuilder();
                    for (int i = 0; i < fp.customDeviceInfoDataBlocks.Length; ++i)
                    {
                        if (fp.customDeviceInfoDataBlocks[i] != null)
                        {
                            Tools.StringHelper.StringBuilderAppendLine(temp, "数据[{0}], 地址: {1}, 长度：{2}", i + 1, Utils.Uint32ToHexString(fp.customDeviceInfoDataBlocks[i].address), Utils.Uint32ToHexString(fp.customDeviceInfoDataBlocks[i].size));
                        }
                    }

                    this.txtCustomDeviceInfo.Text = temp.ToString();
                }                

                if (fp.CheckPresetDataBlockIsOK(out errMsg))
                {
                    this.txtCustomPresetData.Text = string.Format("地址: {0}, 长度：{1}", Utils.Uint32ToHexString(fp.presetDataBlock.address), Utils.Uint32ToHexString(fp.presetDataBlock.size));
                }
                
                this.firmwarePackage = fp;
                this.panelFPInfo.Enabled = true;
            }
            else
            {
                this.txtFirmwarePackageInfo.Text = fpInfo;

                this.txtDSPFirmwareBinInfo.Clear();
                this.txtMCUFirmwareBinInfo.Clear();
                this.txtMCUAllData.Clear();
                this.txtCustomDeviceInfo.Clear();
                this.txtCustomPresetData.Clear();

                this.chkDSPFirmware.IsChecked = false;
                this.chkMCUFirmware.IsChecked = false;
                this.chkUpdateMCUAllData.IsChecked = false;
                this.chkUpdateCustomDeviceInfo.IsChecked = false;
                this.chkUpdatePresetData.IsChecked = false;

                this.firmwarePackage = null;
                this.panelFPInfo.Enabled = false;
            }

            
            this.UpdateEncryptFPButtonState();
        }

        public void UpdateEncryptFPButtonState()
        {
            this.btnEncryptFP.Enabled = this.firmwarePackage != null && keyIsOk;
        }

        private void SelectFirmwarePackageFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = FirmwarePackageSeralizer.DEF_MFP_FILE_FILTER;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtFirmwarePackageFilePath.Text = ofd.FileName;
            }
        }

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            if (sender.Equals(this.chkUpdateMCUAllData))
            {
                if (!this.chkUpdateMCUAllData.IsChecked)
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

        private void btnEncryptFP_Click(object sender, EventArgs e)
        {
            if(this.firmwarePackage == null || !this.firmwarePackage.CheckIsOk())
            {
                gtMessageBox.ShowMessage(this, "固件包数据错误");
                return;
            }

            string errorMsg;
            byte[] key = Utils.HexStringToBytes(this.txtKey.Text.Trim(), out errorMsg);
            if (key == null || key.Length != FirmwarePackageSeralizer.KeyLength)
            {
                gtMessageBox.ShowMessage(this, "密钥数据为空或有误");
                return;
            }
            
            this.firmwarePackage.dspBin.isDownloadBin = this.chkDSPFirmware.IsChecked;
            this.firmwarePackage.mcuBin.isDownloadBin = this.chkMCUFirmware.IsChecked;
            this.firmwarePackage.isUpdateMCUAllData = this.chkUpdateMCUAllData.IsChecked;
            this.firmwarePackage.isUpdateCustomDeviceInfo = this.chkUpdateCustomDeviceInfo.IsChecked;
            this.firmwarePackage.isUpdateCustomPresetData = this.chkUpdatePresetData.IsChecked;

            this.SaveFirmwarePackage(this.firmwarePackage, key);
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
