using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AEC.Core;
using AEC.Common;
using AEC.Misc;
using AEC.UI.Skin;
namespace AEC.UI.UsageLimitLocks
{
    public partial class UsageLimitLockPanel : AEC.UI.gtUserControl
    {
        private const string HideUsageCount = "●●●●";
        private const int MaxUsage = 3000;
        private static readonly int UsageMaxDigits = MaxUsage.ToString().Length;
        private SystemModuleBase system;
        private short lockIndex;

        public bool passwordVisible { get; set; }
        public uint randKey { get; set; }

        private bool isUnlock
        {
            set
            {
                this.chkState.Enabled = value;
                this.cmbUsage.Enabled = value;
                this.chkApplyCurrPassword.Enabled = value;
                this.ApplyCurrPassword(false);
                this.UpdateSettingBtnState(false);
                this.lblCurrState.Visible = value;
                this.chkCurrState.Visible = value;
                this.lblRemainderUsage.Visible = value;
                this.txtRemainderUsage.Visible = value;
            }
        }

        public UsageLimitLockPanel()
        {
            InitializeComponent();
        }

        public void InitTheme()
        {
            this.FillColor = SkinManager.CurrentSkin.PanelBackColor;
            this.BackColor = SkinManager.CurrentSkin.PanelBackColor;

            this.lblCurrPassword.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblCurrState.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblRemainderUsage.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblNewPassword.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblConfirmNewPassword.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblUseCurrPassword.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblState.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.lblUsage.ForeColor = SkinManager.CurrentSkin.TextForeColor;

            this.txtCurrPassword.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            this.txtCurrPassword.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;
            this.txtRemainderUsage.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            this.txtRemainderUsage.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;
            this.txtNewPassword.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            this.txtNewPassword.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;
            this.txtConfirmNewPassword.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            this.txtConfirmNewPassword.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;

        }

        public void Binding(SystemModuleBase system_, short index)
        {
            Debug.Assert(system_ != null);

            //this.mController = mController_;
            this.system = system_;
            this.lockIndex = index;

            this.Enabled = this.system.isSupportUsageLimitLock;

            this.isUnlock = false;

            this.lblIndex.Text = (this.lockIndex + 1).ToString();

            this.picLock.Visible = this.system.usageLimitLocks[lockIndex].isLocked;

            //this.RemoveEventBus();
            //this.mainEventBus = eventBus_;
            //this.controllerEvent = this.mController.controllerEvent;
            //this.RegisterEventBus();

            //this.InitUI();
        }

        public void UpdateUIFromData(SystemModuleBase system_)
        {
            Debug.Assert(this.system != null);

            if(this.system != system_)
            {
                this.system = system_;
                this.isUnlock = false;
            }
            else if(this.system.deviceId != system_.deviceId)
            {
                this.isUnlock = false;
            }

            this.Enabled = this.system.isSupportUsageLimitLock;

            this.picLock.Visible = this.system.usageLimitLocks[lockIndex].isLocked;
        }

        public void UpdateCurrData(UsageLimitLock usageLimitLock)
        {
            Debug.Assert(usageLimitLock != null && system != null && usageLimitLock.index < system.usageLimitLocks.Length);

            bool isUpdateCurrPassword = this.system.usageLimitLocks[lockIndex].systemPasswordStr == this.txtCurrPassword.Text;

            this.system.usageLimitLocks[usageLimitLock.index].CopyFrom(usageLimitLock);

            this.chkCurrState.IsCheckedInit = usageLimitLock.enable;
            this.txtRemainderUsage.Text = usageLimitLock.usageCount.ToString();

            if (isUpdateCurrPassword)
            {
                this.txtCurrPassword.Text = usageLimitLock.systemPasswordStr;
            }
            else
            {
                this.txtCurrPassword.ResetText();
            }

            this.picLock.Visible = this.system.usageLimitLocks[lockIndex].isLocked;
        }

        public void InitUI()
        {
            Debug.Assert(this.system != null);

            this.chkCurrState.IsCheckedInit = this.system.usageLimitLocks[this.lockIndex].enable;
            this.txtRemainderUsage.Text = this.system.usageLimitLocks[this.lockIndex].usageCount.ToString();
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            if(sender.Equals(this.txtCurrPassword))
            {
                this.txtCurrPasswordChangedHandler();
            }
            else if(sender.Equals(this.txtNewPassword))
            {
                this.txtNewPasswordChangedHandler(this.txtNewPassword);
            }
            else if(sender.Equals(this.txtConfirmNewPassword))
            {
                this.txtNewPasswordChangedHandler(this.txtConfirmNewPassword);
            }
        }

        private void txtCurrPasswordChangedHandler()
        {
            string password = UsageLimitLockHelper.FilterDigitChar(txtCurrPassword.Text);
            this.btnOK.Enabled = password.Length == UsageLimitLock.PasswordLength;
            if(password != this.txtCurrPassword.Text)
            {
                this.txtCurrPassword.TextChanged -= txt_TextChanged;
                this.txtCurrPassword.Text = password;
                this.txtCurrPassword.TextChanged += txt_TextChanged;
            }
        }

        private void txtNewPasswordChangedHandler(TextBox textBox)
        {
            string password = UsageLimitLockHelper.FilterDigitChar(textBox.Text);
            this.btnOK.Enabled = password.Length == UsageLimitLock.PasswordLength;
            if (password != textBox.Text)
            {
                textBox.TextChanged -= txt_TextChanged;
                textBox.Text = password;
                textBox.TextChanged += txt_TextChanged;
            }

            this.UpdateSettingBtnState(true);
        }

        private void UpdateSettingBtnState(bool outputError)
        {
            if(!this.chkApplyCurrPassword.IsChecked)
            {
                if(this.txtNewPassword.TextLength != UsageLimitLock.PasswordLength)
                {
                    this.btnSetting.Enabled = false;
                    if (this.txtNewPassword.Focused || this.txtConfirmNewPassword.Focused || !this.cmbUsage.Focused)
                    {
                        this.txtNewPassword.Focus();
                    }

                    this.OutputErrorInfo(outputError, localization[Localization.usageLimitLock_NewPasswordLengthLessThan6]);
                    return;
                }

                if(this.txtConfirmNewPassword.TextLength != UsageLimitLock.PasswordLength)
                {
                    this.btnSetting.Enabled = false;
                    if (this.txtNewPassword.Focused || this.txtConfirmNewPassword.Focused || !this.cmbUsage.Focused)
                    {
                        this.txtConfirmNewPassword.Focus();
                    }
                    this.OutputErrorInfo(outputError, localization[Localization.usageLimitLock_ConfirmNewPasswordLengthLessThan6]);
                    return;
                }

                if(this.txtNewPassword.Text != this.txtConfirmNewPassword.Text)
                {
                    this.btnSetting.Enabled = false;
                    if (this.txtNewPassword.Focused || this.txtConfirmNewPassword.Focused || !this.cmbUsage.Focused)
                    {
                        this.txtNewPassword.Focus();
                    }
                    this.OutputErrorInfo(outputError, localization[Localization.usageLimitLock_PasswordEnteredInconsistent]);
                    return;
                }
            }
            else
            {
                this.FocusCmbusage();
            }

            ushort usage = 0;
            if(!ushort.TryParse(this.cmbUsage.Text, out usage))
            {
                this.btnSetting.Enabled = false;
                this.FocusCmbusage();
                this.OutputErrorInfo(outputError, localization[Localization.usageLimitLock_UsageCountEnteredIncorrectly]);
                return;
            }
            else
            {
                this.FocusCmbusage();
            }

            if (usage > MaxUsage)
            {
                this.btnSetting.Enabled = false;
                this.cmbUsage.Text = MaxUsage.ToString();
                this.cmbUsage.SelectionStart = this.cmbUsage.Text.Length;
                //Log.OutputLine("cmbUsage获取输入焦点");
            }

            this.btnSetting.Enabled = true;
            this.UpdateErrorTip(string.Empty);
        }

        private void FocusCmbusage()
        {
            if (!this.cmbUsage.Focused)
            {
                this.cmbUsage.Focus();
                this.cmbUsage.SelectionStart = this.cmbUsage.Text.Length;
                //Log.OutputLine("cmbUsage获取输入焦点");
            } 
        }

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            if(sender.Equals(this.chkApplyCurrPassword))
            {
                this.ApplyCurrPassword(true);
            }
        }

        private void ApplyCurrPassword(bool outputError)
        {
            this.txtNewPassword.Enabled = !this.chkApplyCurrPassword.IsChecked && this.chkApplyCurrPassword.Enabled;
            this.txtConfirmNewPassword.Enabled = !this.chkApplyCurrPassword.IsChecked && this.chkApplyCurrPassword.Enabled;

            if(this.chkApplyCurrPassword.IsChecked)
            {
                this.txtNewPassword.Clear();
                this.txtConfirmNewPassword.Clear();
            }

            UpdateSettingBtnState(outputError);
        }


        

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            if (sender.Equals(this.txtCurrPassword))
            {
                string password = this.txtCurrPassword.Text;
                if (this.txtCurrPassword.TextLength == UsageLimitLock.PasswordLength)
                {
                    this.ConfirmCurrPassword();
                }
            }
            else if (sender.Equals(this.txtNewPassword))
            {
                if (this.txtConfirmNewPassword.Text.Length == UsageLimitLock.PasswordLength)
                {
                    if (this.txtNewPassword.Text.Length == UsageLimitLock.PasswordLength)
                    {
                        this.btnSetting.Focus();
                    }
                }
                else if (this.txtNewPassword.Text.Length == UsageLimitLock.PasswordLength)
                {
                    this.txtConfirmNewPassword.Focus();
                }
            }
            else if (sender.Equals(this.txtConfirmNewPassword))
            {
                if (this.txtNewPassword.Text.Length == UsageLimitLock.PasswordLength)
                {
                    if (this.txtConfirmNewPassword.Text.Length == UsageLimitLock.PasswordLength)
                    {
                        this.btnSetting.Focus();
                    }
                }
                else if (this.txtConfirmNewPassword.Text.Length == UsageLimitLock.PasswordLength)
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

        private void btn_Click(object sender, EventArgs e)
        {
            if(sender.Equals(this.btnOK))
            {
                this.ConfirmCurrPassword();
            }
            else if(sender.Equals(this.btnSetting))
            {
                this.SetUsageLimitLock();
            }
        }

        private void SetUsageLimitLock()
        {
            string errorMsg = string.Empty;
            UsageLimitLock usageLimitLock = GetUsageLimitLockFromUI(out errorMsg);
            if(usageLimitLock == null)
            {
                this.OutputErrorInfo(true, errorMsg);
                return;
            }

            this.system.UpdateUsageLimitLock(usageLimitLock);
        }

        private void ConfirmCurrPassword()
        {
            if (!VerifyInputPassword())
            {
                this.txtCurrPassword.Clear();
                return;
            }

            this.isUnlock = true;
        }

        private bool VerifyInputPassword()
        {
            List<string> passwords = this.system.GetUsageLimitLockPasswords((uint)lockIndex, randKey);
            return passwords.Contains(this.txtCurrPassword.Text);
            //if(this.txtCurrPassword.Text == this.system.usageLimitLocks[lockIndex].systemPasswordStr)   //当前密码
            //{
            //    return true;
            //}
            //else
            //{
            //    DateTime now = DateTime.UtcNow;
            //    uint timeStamp_0 = Tools.TimeStampHelper.GetTimeStampWithoutSecond(now);
            //    uint timeStamp_n1 = Tools.TimeStampHelper.GetTimeStampWithoutSecond(now - new TimeSpan(0, 1, 0));
            //    uint timeStamp_p1 = Tools.TimeStampHelper.GetTimeStampWithoutSecond(now + new TimeSpan(0, 1, 0));

            //    uint index = (uint)this.lockIndex;

            //    //Log.OutputLine("***********Lock {0} ************", index + 1);
            //    //Log.OutputLine("timeStamp = {0}, password = {1}", Utils.Uint32ToShortHexStr(timeStamp_0), UsageLimitLockHelper.GetSuperPassword(this.system.deviceId, index, timeStamp_0));
            //    //Log.OutputLine("timeStamp = {0}, password = {1}", Utils.Uint32ToShortHexStr(timeStamp_n1), UsageLimitLockHelper.GetSuperPassword(this.system.deviceId, index, timeStamp_n1));
            //    //Log.OutputLine("timeStamp = {0}, password = {1}", Utils.Uint32ToShortHexStr(timeStamp_p1), UsageLimitLockHelper.GetSuperPassword(this.system.deviceId, index, timeStamp_p1));
            //    //Log.OutputLine("randKey = {0}, password = {1}", Utils.Uint32ToShortHexStr(randKey), UsageLimitLockHelper.GetSuperPassword(this.system.deviceId, index, randKey));
            //    //Log.OutputLine("#######################");

            //    if (this.txtCurrPassword.Text == UsageLimitLockHelper.GetSuperPassword(this.system.deviceId, index, timeStamp_0) ||
            //        this.txtCurrPassword.Text == UsageLimitLockHelper.GetSuperPassword(this.system.deviceId, index, timeStamp_n1) ||
            //        this.txtCurrPassword.Text == UsageLimitLockHelper.GetSuperPassword(this.system.deviceId, index, timeStamp_p1) ||
            //        this.txtCurrPassword.Text == UsageLimitLockHelper.GetSuperPassword(this.system.deviceId, index, randKey))
            //    {
            //        return true;
            //    }
            //}
            

            //return false;
        }

        public UsageLimitLock GetUsageLimitLockFromUI(out string errorMsg)
        {
            errorMsg = string.Empty;

            UsageLimitLock usageLimitLock = new UsageLimitLock(this.lockIndex);
            usageLimitLock.enable = this.chkState.IsChecked;
            usageLimitLock.usageCount = 0;
            usageLimitLock.passwordVisible = this.passwordVisible;

            ushort usageCount = 0;
            if (!ushort.TryParse(this.cmbUsage.Text, out usageCount))
            {
                this.btnSetting.Enabled = false;
                this.FocusCmbusage();
                errorMsg = localization[Localization.usageLimitLock_UsageCountEnteredIncorrectly];
                return null;
            }
            else
            {
                this.FocusCmbusage();
            }

            usageLimitLock.usageCount = usageCount;

            if (usageLimitLock.usageCount > MaxUsage)
            {
                this.btnSetting.Enabled = false;
                this.FocusCmbusage();
                errorMsg = string.Format(localization[Localization.usageLimitLock_UsageCountEnteredExceedsMaximum], usageCount, MaxUsage);
                return null;
            }

            if(this.chkApplyCurrPassword.IsChecked)
            {
                usageLimitLock.systemPasswordStr = this.system.usageLimitLocks[lockIndex].systemPasswordStr;

                return usageLimitLock;
            }
            else
            {
                if(this.txtNewPassword.TextLength != UsageLimitLock.PasswordLength)
                {
                    errorMsg = localization[Localization.usageLimitLock_NewPasswordLengthLessThan6];
                }
                else if(this.txtConfirmNewPassword.TextLength != UsageLimitLock.PasswordLength)
                {
                    errorMsg = localization[Localization.usageLimitLock_ConfirmNewPasswordLengthLessThan6];
                }
                else if(this.txtNewPassword.Text != this.txtConfirmNewPassword.Text)
                {
                    errorMsg = localization[Localization.usageLimitLock_PasswordEnteredInconsistent];
                }
                else if(!Tools.StringHelper.StringIsDigit(this.txtNewPassword.Text))
                {
                    errorMsg = localization[Localization.usageLimitLock_PasswordsMustBeNumeric];
                }
                else
                {
                    usageLimitLock.systemPasswordStr = this.txtNewPassword.Text;

                    return usageLimitLock;
                }

                return null;
            }
        }

        private void cmbUsage_TextChanged(object sender, EventArgs e)
        {
            string password = UsageLimitLockHelper.FilterDigitChar(this.cmbUsage.Text);
            if(this.cmbUsage.Text != password)
            {
                this.cmbUsage.Text = password;
                return;
            }

            if (this.cmbUsage.Text.Length > UsageMaxDigits)
            {
                this.cmbUsage.Text = this.cmbUsage.Text.Substring(0, UsageMaxDigits);
                this.cmbUsage.SelectionStart = this.cmbUsage.Text.Length;
            }
            else
            {
                UpdateSettingBtnState(true);
            }
        }

        private void OutputErrorInfo(bool outputError, string format, params object[] arg)
        {
            if(outputError)
            {
                UpdateErrorTip(string.Format(format, arg));
            }
        }

        private void UpdateErrorTip(string error)
        {
            if(string.IsNullOrEmpty(error))
            {
                this.lblError.Visible = false;
            }
            else
            {
                this.lblError.UpdateText(error, true);
                this.lblError.Visible = true;
            }
        }

        private static Localization localization = Config.GetInstance().localization;

        public void UpdateDisplayLanguage()
        {
            this.lblCurrState.UpdateText(localization[Localization.usageLimitLock_CurrentState], true);
            this.lblRemainderUsage.UpdateText(localization[Localization.usageLimitLock_RemainderUsage], true);
            this.lblCurrPassword.UpdateText(localization[Localization.usageLimitLock_CurrentPassword], true);
            this.btnOK.UpdateText(localization[Localization.usageLimitLock_OK], true);
            this.lblNewPassword.UpdateText(localization[Localization.usageLimitLock_NewPassword], true);
            this.lblConfirmNewPassword.UpdateText(localization[Localization.usageLimitLock_ConfirmNewPassword], true);
            this.lblUseCurrPassword.UpdateText(localization[Localization.usageLimitLock_UseCurrentPassword], true);
            this.lblState.UpdateText(localization[Localization.usageLimitLock_State], true);
            this.lblUsage.UpdateText(localization[Localization.usageLimitLock_UsageCount], true);
            this.btnSetting.UpdateText(localization[Localization.usageLimitLock_Setting], true);
        }
    }
}
