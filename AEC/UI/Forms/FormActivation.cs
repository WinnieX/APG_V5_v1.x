using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.Misc;

namespace AEC.UI.Forms
{
    public partial class FormActivation : AEC.UI.MFormBase
    {
        private ActivationCode.MLicenseType currLicenseType = ActivationCode.MLicenseType.Unauthorized;

        public FormActivation()
        {
            InitializeComponent();
        }

        protected override void UpdateDisplayLanguage()
        {
            this.Text = localization[Localization.formActivation];
            this.lblMachineCode.Text = localization[Localization.formActivation_MachineCode];
            this.lblRequestCode.Text = localization[Localization.formActivation_RequestCode];
            this.lblActivationCode.Text = localization[Localization.formActivation_ActivationCode];
            this.btnActivation.Text = localization[Localization.formActivation];
        }

        protected override void OnLoadHandler()
        {
            base.OnLoadHandler();
            this.txtMachineCode.Text = Tools.MachineIDHelper.GetMachineCode();
            this.txtRequestCode.Text = AEC.ActivationCode.GetRequestCode();
        }

        private void btnActivation_Click(object sender, EventArgs e)
        {
            ActivationCode.MLicenseType licneseType = ActivationCode.CheckActivationCode(this.txtActivationCode.Text, this.txtRequestCode.Text);
            if(licneseType != ActivationCode.MLicenseType.Unauthorized)
            {
                this.currLicenseType = licneseType;
                this.Close();
                return;
            }
            else
            {
                gtMessageBox.ShowMessage(this, "激活码错误");
                this.txtActivationCode.Clear();
            }
        }

        public ActivationCode.MLicenseType ShowDialog(out string activationCode)
        {
            base.ShowDialog();
            activationCode = this.txtActivationCode.Text;
            return this.currLicenseType;
        }

        private void txtActivationCode_TextChanged(object sender, EventArgs e)
        {
            this.btnActivation.Enabled = this.txtActivationCode.TextLength == this.txtActivationCode.MaxLength;
        }
    }
}
