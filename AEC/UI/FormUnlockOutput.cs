using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.Misc;

namespace AEC.UI
{
    public partial class FormUnlockOutput : AEC.UI.MFormBase
    {
        private const string DefaultUnlockOutputPassword = "666666";

        public FormUnlockOutput()
        {
            InitializeComponent();
            this.InitFont();
        }

        private void InitFont()
        {
            this.Font = FontHelper.CreateFont10();
            base.title.Font = FontHelper.CreateFont10();
            this.lblPassword.Font = FontHelper.CreateFont10();
            //this.txtPassword.Font = FontHelper.CreateFont10();
            this.btnUnlock.Font = FontHelper.CreateFont10();
        }

        public new bool ShowDialog()
        {
            base.ShowDialog();

            return CheckPasswordIsOk();
        }


        private bool CheckPasswordIsOk()
        {
            string inputPassword = this.txtPassword.Text.Trim();
            return inputPassword == DefaultUnlockOutputPassword ||
                inputPassword == DateTime.Now.ToString("yyyyMMdd");
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            this.UnlockOutput();
        }

        private void UnlockOutput()
        {
            if (CheckPasswordIsOk())
            {
                this.Close();
            }
            else
            {
                this.txtPassword.Clear();
                gtMessageBox.ShowMessage(
                    this,
                    localization[Localization.FormUnlockOutput_PasswordIncorrect],
                    localization[Localization.FormUnlockOutput], MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            this.btnUnlock.Enabled = this.txtPassword.TextLength >= 4;
        }

        private void FormUnlockOutput_Load(object sender, EventArgs e)
        {
            this.Text = localization[Localization.FormUnlockOutput];
            this.lblPassword.Text = localization[Localization.FormUnlockOutput_Password];
            this.btnUnlock.Text = localization[Localization.FormUnlockOutput_Unlock];

            this.txtPassword.Focus();
            this.ActiveControl = this.txtPassword;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.UnlockOutput();
            }
        }
    }
}
