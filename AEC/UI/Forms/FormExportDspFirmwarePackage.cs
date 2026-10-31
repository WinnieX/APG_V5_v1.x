using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AEC.Comm;
using AEC.Comm.FirmwareUpdate;

namespace AEC.UI.Forms
{
    public partial class FormExportDspFirmwarePackage : AEC.UI.MFormBase
    {
        private static readonly Color txtForeColor = Color.FromArgb(224, 224, 224);

        private const string BIN_FILE_FILTER = "bin文件|*.bin";
        private const string BIN_FILE_EXTENSION = ".bin";

        public FormExportDspFirmwarePackage()
        {
            InitializeComponent();
        }

        protected override void OnLoadHandler()
        {
            this.InitUI();
        }

        private void InitUI()
        {
        }

        private void txtBinFilePath_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = BIN_FILE_FILTER;
            if(ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtBinFilePath.Text = ofd.FileName;
                this.txtBinFilePath.SelectionStart = this.txtBinFilePath.TextLength;
            }
        }

        private void FormDspFirmware_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                System.IO.FileInfo fi = new System.IO.FileInfo(path);
                if ((fi.Attributes & FileAttributes.Directory) == FileAttributes.Directory || fi.Extension != BIN_FILE_EXTENSION)
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

        private void FormDspFirmware_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            //if()
            //System.IO.FileInfo fi = new System.IO.FileInfo(path);
            //if(fi.Attributes & FileAttributes.SparseFile)
            this.txtBinFilePath.Text = path;
            this.txtBinFilePath.SelectionStart = this.txtBinFilePath.TextLength;
        }

        private void txtBinFilePath_TextChanged(object sender, EventArgs e)
        {
            string filePath = this.txtBinFilePath.Text.Trim();

            if(!System.IO.File.Exists(filePath))
            {
                this.btnExport.Enabled = false;
                return;
            }

            DSPFirmwarePackage dsp_fw_package = CrcImageHelper.ParseFromFile(filePath);
            this.panelDspFirmwareInfo.dspFirmwareInfo = dsp_fw_package.dspFirmwareInfo;

            this.btnExport.Enabled = dsp_fw_package != null;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if(sender.Equals(this.btnExport))
            {
                this.ExportFile();
            }
        }

        private void ExportFile()
        {
            DSPFirmwarePackage dsp_fw_package = CrcImageHelper.ParseFromFile(this.txtBinFilePath.Text.Trim());
            if(dsp_fw_package == null)
            {
                return;
            }

            string errorMsg = string.Empty;
            DSPFirmwareInfo dspFirmwareInfo = this.panelDspFirmwareInfo.GetDSPFirmwareInfo(out errorMsg);
            if (dspFirmwareInfo == null)
            {
                errorMsg = string.Format("DSP 固件有误 或 数据输入有误: {0}", errorMsg);
                gtMessageBox.ShowMessage(this, errorMsg, this.Text);
                return;
            }
            else if(dspFirmwareInfo.version == 0)
            {
                gtMessageBox.ShowMessage(this, "固件版本号不能为0", this.Text);
                return;
            }

            dsp_fw_package.dspFirmwareInfo.SyncData(dspFirmwareInfo);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = DSPFirmwareInfoSeralizer.DEF_MDF_FILE_FILTER;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    DSPFirmwareInfoSeralizer.SeralizeToFile(dsp_fw_package, sfd.FileName);
                    Tools.LogHelper.WriteInfo("导出文件 Hash: {0}", Utils.ByteArrayToHexStr(Tools.HashHelper.GetFileMD5(sfd.FileName), string.Empty));
                    Tools.FileHelper.OpenFileLocation(sfd.FileName);
                }
                catch(Exception e)
                {
                    gtMessageBox.ShowMessage(this, e.Message, "导出DSP固件包错误");
                }
            }
        }
    }
}
