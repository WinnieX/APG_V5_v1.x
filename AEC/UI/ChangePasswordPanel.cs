using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AEC.Core;
using AEC.Misc;
using AEC.UI.Skin;

namespace AEC.UI
{
    public partial class ChangePasswordPanel : gtUserControl
    {
        private event EventHandler<EventBusArgs> PasswordChanged;
        private string oldPassword;
        public string changePasswordTips;

        public ChangePasswordPanel()
        {
            InitializeComponent();
            InitFont();
        }

        private void InitFont()
        {
            this.Font = FontHelper.CreateFont10();
            this.lblOldPassword.Font = FontHelper.CreateFont10();
            this.lblNewPassword.Font = FontHelper.CreateFont10();
            this.lblConfirmNewPassword.Font = FontHelper.CreateFont10();
        }

        public void InitTheme()
        {
            this.FillColor = SkinManager.CurrentSkin.PanelBackColor;
            this.ForeColor = SkinManager.CurrentSkin.PanelForeColor;

            this.lblOldPassword.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblNewPassword.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblConfirmNewPassword.ForeColor = SkinManager.CurrentSkin.TextForeColor;

            this.txtOldPassword.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            this.txtOldPassword.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;
            this.txtNewPassword.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            this.txtNewPassword.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;
            this.txtConfirmNewPassword.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            this.txtConfirmNewPassword.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;
        }

        public void Init(string oldPassword_, EventHandler<EventBusArgs> PasswordChanged_)
        {
            this.PasswordChanged = PasswordChanged_;
            this.oldPassword = oldPassword_;
            this.txtOldPassword.MaxLength = this.oldPassword.Length;
            this.txtNewPassword.MaxLength = this.oldPassword.Length;
            this.txtConfirmNewPassword.MaxLength = this.oldPassword.Length;
            this.txtNewPassword.Enabled = false;
            this.txtConfirmNewPassword.Enabled = false;
        }

        public void UpdateUIFromData(string oldPassword_)
        {
            this.oldPassword = oldPassword_;
            this.ResetPanel();
        }

        public void ResetPanel()
        {
            this.txtOldPassword.Clear();
            this.txtNewPassword.Clear();
            this.txtConfirmNewPassword.Clear();
            this.txtNewPassword.Enabled = false;
            this.txtConfirmNewPassword.Enabled = false;
        }

        public void ClearSuPassword()
        {
            this.txtOldPassword.Clear();
        }

        public string suPassword
        {
            get
            {
                return this.txtOldPassword.Text;
            }
        }

        public int PassowrdLength
        {
            get
            {
                if(this.oldPassword != null)
                {
                    return this.oldPassword.Length;
                }
                else
                {
                    return 0;
                }
            }
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            if(sender.Equals(this.txtOldPassword))
            {
                string password = this.txtOldPassword.Text;
                if (password == this.oldPassword)
                {
                    this.txtNewPassword.Enabled = true;
                    this.txtConfirmNewPassword.Enabled = true;
                    this.txtNewPassword.Focus();
                }
                else
                {
                    this.txtNewPassword.Clear();
                    this.txtConfirmNewPassword.Clear();
                    this.txtNewPassword.Enabled = false;
                    this.txtConfirmNewPassword.Enabled = false;
                }
            }
            else if(sender.Equals(this.txtNewPassword))
            {
                if (this.txtConfirmNewPassword.Text.Length == PassowrdLength)
                {
                    if (this.txtNewPassword.Text.Length == PassowrdLength)
                    {
                        ModifyKeyLockPassword();
                    }
                }
                else if (this.txtNewPassword.Text.Length == PassowrdLength)
                {
                    this.txtConfirmNewPassword.Focus();
                }
            }
            else if(sender.Equals(this.txtConfirmNewPassword))
            {
                if (this.txtNewPassword.Text.Length == PassowrdLength)
                {
                    if (this.txtConfirmNewPassword.Text.Length == PassowrdLength)
                    {
                        ModifyKeyLockPassword();
                    }
                }
                else if (this.txtConfirmNewPassword.Text.Length == PassowrdLength)
                {
                    this.txtConfirmNewPassword.Focus();
                }
            }
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            if (sender.Equals(this.txtOldPassword))
            {
                this.txtNewPassword.Clear();
                this.txtConfirmNewPassword.Clear();
                this.txtNewPassword.Enabled = false;
                this.txtConfirmNewPassword.Enabled = false;
            }
            else if (sender.Equals(this.txtNewPassword))
            {

            }
            else if (sender.Equals(this.txtConfirmNewPassword))
            {

            }
        }
        private void ModifyKeyLockPassword()
        {
            if(this.txtNewPassword.TextLength!= PassowrdLength ||
                this.txtConfirmNewPassword.TextLength != PassowrdLength ||
                this.txtNewPassword.Text != this.txtConfirmNewPassword.Text)
            {
                ShowTip(localization[Localization.panelSystem_Passowrd_InputError], localization[Localization.panelSystem_Passowrd_InvalidInput]);
                return;
            }

            if (DialogResult.OK != gtMessageBox.ShowMessage(this.FindForm(), string.Format(changePasswordTips, this.title), "AEC", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                return;
            }

            string newPassowrd = this.txtNewPassword.Text;

            this.txtOldPassword.Clear();
            this.txtNewPassword.Clear();
            this.txtConfirmNewPassword.Clear();
            this.txtOldPassword.Focus();
            this.txtNewPassword.Enabled = false;
            this.txtConfirmNewPassword.Enabled = false;

            if (this.PasswordChanged != null)
            {
                this.PasswordChanged(this, new PasswordUpdatedArgs(newPassowrd));
            }
        }

        private void ShowTip(string title, string text, int time_in_ms = 1500)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.ToolTipTitle = title;
            toolTip.Show(text, this.txtConfirmNewPassword, time_in_ms);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.txtOldPassword.Focused ||
                this.txtNewPassword.Focused ||
                this.txtConfirmNewPassword.Focused)
            {
                return false;
            }
            else
            {
                return base.ProcessDialogKey(keyData);
            }
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.lblOldPassword.Text = localization[Localization.panelSystem_Passowrd_OldPassowrd];
            this.lblNewPassword.Text = localization[Localization.panelSystem_Passowrd_NewPassowrd];
            this.lblConfirmNewPassword.Text = localization[Localization.panelSystem_Passowrd_ConfirmPassowrd];
            
            this.Invalidate();
        }
    }
}
