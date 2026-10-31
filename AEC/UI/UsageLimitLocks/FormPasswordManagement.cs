using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.Common;
using AEC.Misc;
using AEC.UI.Skin;

namespace AEC.UI.UsageLimitLocks
{
    public partial class FormPasswordManagement : AEC.UI.MFormBase
    {
        private string currName { get; set; }

        private readonly UI.Controls.MLabel[] lblLockXPassword;

        public FormPasswordManagement()
        {
            InitializeComponent();

            this.currName = string.Empty;

            this.lblLockXPassword = new Controls.MLabel[]
            {
                this.lblLock1Password,
                this.lblLock2Password,
                this.lblLock3Password,
                this.lblLock4Password,
                this.lblLock5Password,
            };

            this.InitTheme();
        }

        private void InitTheme()
        {
            this.BackColor = SkinManager.CurrentSkin.PanelBackColor;

            this.lblLock1Password.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblLock2Password.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblLock3Password.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblLock4Password.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblLock5Password.ForeColor = SkinManager.CurrentSkin.TextForeColor;
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
            if(!string.IsNullOrEmpty(this.currName))
            {
                sb.AppendFormat("{0} - ", this.currName);
            }
            sb.Append(localization[Localization.usageLimitLock_PasswordManagement]);
            this.Text = sb.ToString();
        }

        private void link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(sender.Equals(this.linkRandom))
            {
                this.panelPasswordGroup.Random();
            }
            else if(sender.Equals(this.linkClear))
            {
                this.panelPasswordGroup.Clear();
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if(sender.Equals(this.btnImport))
            {
                this.ImportPassword();
            }
            else if(sender.Equals(this.btnExport))
            {
                this.ExportPassword();
            }
        }

        private void ExportPassword()
        {
            ULLPasswordGroup pg = new ULLPasswordGroup(this.panelPasswordGroup.passwords);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = ULLPasswordGroupSeralizer.DEF_MLP_FILE_FILTER;            
            sfd.RestoreDirectory = true;

            try
            {
                sfd.InitialDirectory = ULLPasswordGroupSeralizer.InitialDirectory;
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    ULLPasswordGroupSeralizer.SeralizeToFile(pg, sfd.FileName);
                    this.currName = System.IO.Path.GetFileNameWithoutExtension(sfd.FileName);
                    UpdateText();
                    Tools.FileHelper.OpenFileLocation(sfd.FileName);
                }
            }
            catch (Exception e)
            {
                gtMessageBox.ShowMessage(this, e.Message, "Error");
            }
        }

        private void ImportPassword()
        {
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
                    if(pg != null)
                    {
                        this.panelPasswordGroup.passwords = pg.passwords;

                        this.currName = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                        UpdateText();
                    }
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

            this.linkClear.Text = localization[Localization.usageLimitLock_Clear];
            this.linkRandom.Text = localization[Localization.usageLimitLock_Random];
            this.btnImport.UpdateText(localization[Localization.usageLimitLock_Import], true);
            this.btnExport.UpdateText(localization[Localization.usageLimitLock_Export], true);

            for (int i = 0; i < this.lblLockXPassword.Length; ++i )
            {
                this.lblLockXPassword[i].UpdateText(string.Format(localization[Localization.usageLimitLock_LockXPassword], i + 1), true);
            }
        }
    }
}
