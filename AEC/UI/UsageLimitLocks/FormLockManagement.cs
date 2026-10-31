using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AEC.Common;
using AEC.Misc;
using AEC.UI.Skin;

namespace AEC.UI.UsageLimitLocks
{
    public partial class FormLockManagement : AEC.UI.MFormBase
    {
        private readonly UI.Controls.MLabel[] lblLockX;
        private readonly UI.Controls.MSwitch[] chkIsUpdateGroup;
        private readonly UI.Controls.MSwitch[] chkIsEnableGroup;

        private string currTaskName { get; set; }

        public FormLockManagement()
        {
            InitializeComponent();

            this.lblLockX = new Controls.MLabel[]
            {
                this.lblLock1,
                this.lblLock2,
                this.lblLock3,
                this.lblLock4,
                this.lblLock5,
            };

            this.chkIsUpdateGroup = new Controls.MSwitch[]
            {
                this.chkUpdateLock1,
                this.chkUpdateLock2,
                this.chkUpdateLock3,
                this.chkUpdateLock4,
                this.chkUpdateLock5,
            };

            this.chkIsEnableGroup = new Controls.MSwitch[]
            {
                this.chkEnableLock1,
                this.chkEnableLock2,
                this.chkEnableLock3,
                this.chkEnableLock4,
                this.chkEnableLock5,
            };

            this.currTaskName = string.Empty;

            this.InitTheme();
        }

        private void InitTheme()
        {
            this.BackColor = SkinManager.CurrentSkin.PanelBackColor;

            this.lblLock1.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblLock2.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblLock3.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblLock4.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblLock5.ForeColor = SkinManager.CurrentSkin.TextForeColor;

            this.lblIsUpdate.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblOldPassword.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblIsEnable.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblUsage.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblNewPassword.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            //this.lblDeviceName.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            //this.lblRandKey.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            //this.lblTaskFile.ForeColor = SkinManager.CurrentSkin.TextForeColor;

            //this.txtDeviceID.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            //this.txtDeviceID.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;
            //this.txtDeviceName.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            //this.txtDeviceName.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;
            //this.txtRandKey.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            //this.txtRandKey.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;
            //this.txtTaskFilePath.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            //this.txtTaskFilePath.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;

            //this.txtOutput.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            //this.txtOutput.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;
        }

        private void UpdateText()
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(this.currTaskName))
            {
                sb.AppendFormat("{0} - ", this.currTaskName);
            }
            sb.Append(localization[Localization.usageLimitLock_LockTaskManagement]);
            this.Text = sb.ToString();
        }

        private void link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (sender.Equals(this.linkClearOldPassword))
            {
                this.panelOldPasswordGroup.Clear();
            }
            else if (sender.Equals(this.linkImportOldPassword))
            {
                this.ImportPassword(this.panelOldPasswordGroup);
            }
            else if(sender.Equals(this.linkClearNewPassword))
            {
                this.panelNewPasswordGroup.Clear();
            }
            else if (sender.Equals(this.linkImportNewPassword))
            {
                this.ImportPassword(this.panelNewPasswordGroup);
            }
        }

        private void ImportPassword(ULLPasswordGroupPanel panelPG)
        {
            Debug.Assert(panelPG != null);

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ULLPasswordGroupSeralizer.DEF_MLP_FILE_FILTER;
            ofd.RestoreDirectory = true;

            try
            {
                ofd.InitialDirectory = ULLPasswordGroupSeralizer.InitialDirectory;
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    string errorMsg = string.Empty;
                    ULLPasswordGroup pg = ULLPasswordGroupSeralizer.DeseralizeFromFile(ofd.FileName, out errorMsg);
                    if (pg != null)
                    {
                        panelPG.passwords = pg.passwords;
                    }
                }
            }
            catch (Exception e)
            {
                gtMessageBox.ShowMessage(this, e.Message, "Error");
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if(sender.Equals(this.btnImport))
            {
                this.ImportLockTask();
            }
            else if(sender.Equals(this.btnExport))
            {
                this.ExportLockTask();
            }
        }

        private void ImportLockTask()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ULLTaskSeralizer.DEF_MLT_FILE_FILTER;
            ofd.RestoreDirectory = true;

            try
            {
                ofd.InitialDirectory = ULLTaskSeralizer.InitialDirectory;
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    string errorMsg = string.Empty;
                    ULLTask task = ULLTaskSeralizer.DeseralizeFromFile(ofd.FileName, out errorMsg);
                    if (task != null)
                    {
                        this.panelOldPasswordGroup.passwords = task.oldPasswords;
                        this.panelUsageCountGroup.usageCounts = task.usageCounts;
                        this.panelNewPasswordGroup.passwords = task.newPasswords;

                        for (int i = 0; i < task.ullOperations.Length; ++i)
                        {
                            this.chkIsUpdateGroup[i].IsChecked = task.ullOperations[i].isUpdate;
                            this.chkIsEnableGroup[i].IsChecked = task.ullOperations[i].enable;
                        }

                        this.currTaskName = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                        UpdateText();
                    }
                }
            }
            catch (Exception e)
            {
                gtMessageBox.ShowMessage(this, e.Message, "Error");
            }
        }

        private void ExportLockTask()
        {
            string[] oldPasswords = this.panelOldPasswordGroup.passwords;
            ushort[] usageCounts = this.panelUsageCountGroup.usageCounts;
            string[] newPasswords = this.panelNewPasswordGroup.passwords;

            for(int i = 0; i < this.chkIsUpdateGroup.Length; ++i)
            {
                if(this.chkIsUpdateGroup[i].IsChecked)
                {
                    if(oldPasswords[i].Length != UsageLimitLock.PasswordLength ||
                        newPasswords[i].Length != UsageLimitLock.PasswordLength)
                    {
                        return;
                    }
                }
            }

            ULLTask task = new ULLTask();
            for(int i = 0; i < task.ullOperations.Length; ++i)
            {
                task.ullOperations[i].isUpdate = this.chkIsUpdateGroup[i].IsChecked;
                task.ullOperations[i].oldPassword = oldPasswords[i];

                task.ullOperations[i].enable = this.chkIsEnableGroup[i].IsChecked;
                task.ullOperations[i].usageCount = usageCounts[i];
                task.ullOperations[i].newPassword = newPasswords[i];
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = ULLTaskSeralizer.DEF_MLT_FILE_FILTER;
            sfd.RestoreDirectory = true;

            try
            {
                sfd.InitialDirectory = ULLTaskSeralizer.InitialDirectory;
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    ULLTaskSeralizer.SeralizeToFile(task, sfd.FileName);
                    Tools.FileHelper.OpenFileLocation(sfd.FileName);

                    this.currTaskName = System.IO.Path.GetFileNameWithoutExtension(sfd.FileName);
                    UpdateText();
                }
            }
            catch (Exception e)
            {
                gtMessageBox.ShowMessage(this, e.Message, "Error");
            }
        }

        protected override void UpdateDisplayLanguage()
        {
            this.UpdateText();

            this.lblIsUpdate.UpdateText(localization[Localization.usageLimitLock_IsUpdate], true);
            this.lblOldPassword.UpdateText(localization[Localization.usageLimitLock_OldPassword], true);
            this.lblIsEnable.UpdateText(localization[Localization.usageLimitLock_IsEnable], true);
            this.lblUsage.UpdateText(localization[Localization.usageLimitLock_UsageCount], true);
            this.lblNewPassword.UpdateText(localization[Localization.usageLimitLock_NewPassword], true);

            this.linkClearOldPassword.Text = localization[Localization.usageLimitLock_Clear];
            this.linkImportOldPassword.Text = localization[Localization.usageLimitLock_Import];
            this.linkClearNewPassword.Text = localization[Localization.usageLimitLock_Clear];
            this.linkImportNewPassword.Text = localization[Localization.usageLimitLock_Import];
            
            this.btnImport.UpdateText(localization[Localization.usageLimitLock_Import], true);
            this.btnExport.UpdateText(localization[Localization.usageLimitLock_Export], true);

            for (int i = 0; i < this.lblLockX.Length; ++i)
            {
                this.lblLockX[i].UpdateText(string.Format(localization[Localization.usageLimitLock_LockX], i + 1), true);
            }
        }
    }
}
