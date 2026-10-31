using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.Common;
using AEC.Core;
using AEC.Misc;
using AEC.UI.Skin;

namespace AEC.UI.UsageLimitLocks
{
    public partial class FormForgotPassword : AEC.UI.MFormBase
    {
        public FormForgotPassword()
        {
            InitializeComponent();

            this.InitTheme();
        }

        private void InitTheme()
        {
            this.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.txtForgotPassword.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            this.txtForgotPassword.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;

            //this.lblDeviceID.ForeColor = SkinManager.CurrentSkin.TextForeColor;
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

        public void Init(SystemModuleBase system_, string randKey)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Device ID : {0}", system_.deviceIdCode);
            sb.AppendLine();
            sb.AppendFormat("Device Name : {0}", system_.deviceName);
            sb.AppendLine();
            sb.AppendFormat("Rand Key : {0}", randKey);
            sb.AppendLine();
            sb.AppendFormat("TimeStamp : {0}", Tools.DateTimeHelper.DateTime2String(DateTime.Now));

            this.txtForgotPassword.Text = sb.ToString();

            
        }

        protected override void OnLoadHandler()
        {
            base.OnLoadHandler();

            this.txtForgotPassword.Focus();
            this.txtForgotPassword.SelectionStart = 0;
            this.txtForgotPassword.SelectionLength = this.txtForgotPassword.TextLength;
            
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            this.txtForgotPassword.SelectionStart = 0;
            this.txtForgotPassword.SelectionLength = this.txtForgotPassword.TextLength;
            this.txtForgotPassword.Copy();
            this.txtForgotPassword.Focus();
        }

        protected override void UpdateDisplayLanguage()
        {
            this.Text = localization[Localization.usageLimitLock_ForgotPassword];
            this.btnCopy.UpdateText(localization[Localization.usageLimitLock_Copy], true);
            
        }
    }
}
