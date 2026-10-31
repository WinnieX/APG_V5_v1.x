using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using AEC.Core;
using AEC.Common;
using AEC.Misc;
using AEC.UI.Skin;

namespace AEC.UI
{
    public partial class SystemPanel : PanelBase
    {
        private SystemModuleBase system;
        public static readonly Color themeColor = Color.FromArgb(72, 208, 7);

        private readonly Dictionary<short, gtRadioButtonWithText> indexToRdoMap;
        private readonly Dictionary<gtRadioButtonWithText, short> rdoToIndexMap;

        public bool suPasswordIsMatch
        {
            get
            {
                DateTime now = DateTime.Now;
                //AEC.Log.OutputLine("Super User Mode check 2...{0}, {1}, {2}, {3}", this.changePasswordPanelKeyboardLock.suPassword, now.ToString("MMdd"), this.changePasswordPanelSystem.suPassword, now.ToString("HHmmss"));
                return this.changePasswordPanelKeyboardLock.suPassword == now.ToString("MMdd") &&
                    this.changePasswordPanelSystem.suPassword == now.ToString("HHmmss");
            }
        }

        private bool _isLockParams = true;
        public bool isLockParams
        {
            get
            {
                return _isLockParams;
            }
            set
            {
                _isLockParams = value;
                panelDevicePresets.isLockSaveLastPreset = value;

                bool clarityVisible = this.mController.mEffectorFeatures.IsSupportControlClarity() && this.system.isSupportClarity;
                this.panelClarityControl.Visible = clarityVisible;
            }
        }

        public void ClearSuPassword()
        {
            this.changePasswordPanelKeyboardLock.ClearSuPassword();
            this.changePasswordPanelSystem.ClearSuPassword();
        }

        public SystemPanel()
            : base()
        {
            InitializeComponent();
            InitFont();
            this.VisibleChanged += SystemPanel_VisibleChanged;
            this.panelDevicePresets.cmbDeviceMode.DropDownBtnColor = themeColor;

            this.indexToRdoMap = new Dictionary<short, gtRadioButtonWithText>()
            {
                {0, rdoSDA},
                {1, rdoSDB},
                {2, rdoHDA},
                {3, rdoHDB},
                {4, rdoUHDA},
                {5, rdoUHDB},
            };

            this.rdoToIndexMap = new Dictionary<gtRadioButtonWithText, short>()
            {
                {rdoSDA, 0},
                {rdoSDB, 1},
                {rdoHDA, 2},
                {rdoHDB, 3},
                {rdoUHDA, 4},
                {rdoUHDB, 5},
            };
        }

        private void InitFont()
        {
            this.panelKeyLock.Font = FontHelper.CreateFont10();
            this.rdoEnableKeyLock.Font = FontHelper.CreateFont10();
            this.rdoDisableKeyLock.Font = FontHelper.CreateFont10();

            this.panelSystemUser.Font = FontHelper.CreateFont10();
            this.lblSystemPassowrd.Font = FontHelper.CreateFont10();
            this.buttonOK.Font = FontHelper.CreateFont10();
            this.rdoNormalUser.Font = FontHelper.CreateFont10();
            this.rdoNormalAdmin.Font = FontHelper.CreateFont10();

            this.changePasswordPanelKeyboardLock.Font = FontHelper.CreateFont10();
            this.changePasswordPanelSystem.Font = FontHelper.CreateFont10();

            this.panelInfraredRemoteControlMgt.Font = FontHelper.CreateFont10();
            this.rdoEnableRemoteControl.Font = FontHelper.CreateFont10();
            this.rdoDisableRemoteControl.Font = FontHelper.CreateFont10();

            this.panelTimbreControl.Font = FontHelper.CreateFont10();
            this.rdoProTimbre.Font = FontHelper.CreateFont10();
            this.rdoClassicsTimbre.Font = FontHelper.CreateFont10();

            //this.txtIPAddress.Font = FontHelper.CreateFont10();
            //this.txtPort.Font = FontHelper.CreateFont10();
            this.panelDeviceInfo.Font = FontHelper.CreateFont10();
            //this.txtDeviceID.Font = FontHelper.CreateFont10();

            this.lblDeviceID.Font = FontHelper.CreateFont10();
            //this.lblPrototype.Font = FontHelper.CreateFont10();
            //this.lblDevicePrototype.Font = FontHelper.CreateFont10();
            //this.lblFirmwareVersion.Font = FontHelper.CreateFont10();
            //this.lblDeviceFirmwareVersion.Font = FontHelper.CreateFont10();
            //this.lblFirmwareBuildNumber.Font = FontHelper.CreateFont10();
            //this.lblDeviceFirmwareBuildNumber.Font = FontHelper.CreateFont10();
        }

        protected override void InitTheme()
        {
            this.volMgtPanel.InitTheme();

            this.panelKeyLock.FillColor = SkinManager.CurrentSkin.PanelBackColor;
            this.panelKeyLock.ForeColor = SkinManager.CurrentSkin.PanelForeColor;

            this.rdoEnableKeyLock.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.rdoEnableKeyLock.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.rdoDisableKeyLock.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.rdoDisableKeyLock.ForeColor = SkinManager.CurrentSkin.TextForeColor;

            this.panelSystemUser.FillColor = SkinManager.CurrentSkin.PanelBackColor;
            this.panelSystemUser.ForeColor = SkinManager.CurrentSkin.PanelForeColor;
            this.lblSystemPassowrd.ForeColor = SkinManager.CurrentSkin.TextForeColor;

            this.txtSysPassowrd.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            this.txtSysPassowrd.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;

            this.rdoNormalUser.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.rdoNormalUser.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.rdoNormalAdmin.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.rdoNormalAdmin.ForeColor = SkinManager.CurrentSkin.TextForeColor;

            this.buttonOK.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.buttonOK.ForeColor = SkinManager.CurrentSkin.TextForeColor;

            this.panelInfraredRemoteControlMgt.FillColor = SkinManager.CurrentSkin.PanelBackColor;
            this.panelInfraredRemoteControlMgt.ForeColor = SkinManager.CurrentSkin.PanelForeColor;
            this.rdoEnableRemoteControl.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.rdoEnableRemoteControl.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.rdoDisableRemoteControl.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.rdoDisableRemoteControl.ForeColor = SkinManager.CurrentSkin.TextForeColor;

            this.panelClarityControl.FillColor = SkinManager.CurrentSkin.PanelBackColor;
            this.panelClarityControl.ForeColor = SkinManager.CurrentSkin.PanelForeColor;
            this.rdoSDA.ForeColor = SkinManager.CurrentSkin.PanelForeColor;
            this.rdoSDB.ForeColor = SkinManager.CurrentSkin.PanelForeColor;
            this.rdoHDA.ForeColor = SkinManager.CurrentSkin.PanelForeColor;
            this.rdoHDB.ForeColor = SkinManager.CurrentSkin.PanelForeColor;
            this.rdoUHDA.ForeColor = SkinManager.CurrentSkin.PanelForeColor;
            this.rdoUHDB.ForeColor = SkinManager.CurrentSkin.PanelForeColor;
			
            this.panelTimbreControl.FillColor = SkinManager.CurrentSkin.PanelBackColor;
            this.panelTimbreControl.ForeColor = SkinManager.CurrentSkin.PanelForeColor;
			this.rdoProTimbre.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.rdoProTimbre.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.rdoClassicsTimbre.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.rdoClassicsTimbre.ForeColor = SkinManager.CurrentSkin.TextForeColor;
			
            this.panelDeviceWorkMode.FillColor = SkinManager.CurrentSkin.PanelBackColor;
            this.panelDeviceWorkMode.ForeColor = SkinManager.CurrentSkin.PanelForeColor;
			this.rdoStandardMode.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.rdoStandardMode.ForeColor = SkinManager.CurrentSkin.TextForeColor;
            this.rdoX5Mode.BackColor = SkinManager.CurrentSkin.PanelBackColor;
            this.rdoX5Mode.ForeColor = SkinManager.CurrentSkin.TextForeColor;
			
            this.panelDeviceInfo.FillColor = SkinManager.CurrentSkin.PanelBackColor;
            this.panelDeviceInfo.BackColor = SkinManager.CurrentSkin.PanelBackColor;
			this.panelDeviceInfo.ForeColor = SkinManager.CurrentSkin.TextForeColor;

            this.lblDeviceID.ForeColor = SkinManager.CurrentSkin.TextForeColor;	
            this.txtDeviceID.BackColor = SkinManager.CurrentSkin.TextBoxBackColor;
            this.txtDeviceID.ForeColor = SkinManager.CurrentSkin.TextBoxForeColor;

            this.changePasswordPanelKeyboardLock.InitTheme();
            this.changePasswordPanelSystem.InitTheme();

            this.panelDataBackupMgt.InitTheme();
            this.panelDevicePresets.InitTheme();
        }

        private void SystemPanel_VisibleChanged(object sender, EventArgs e)
        {
            //AEC.Log.OutputLine("system panel visible:{0}", this.Visible);
            if(!this.Visible)
            {
                this.panelDevicePresets.UpdateUIFromData();
                this.changePasswordPanelKeyboardLock.ResetPanel();
                this.changePasswordPanelSystem.ResetPanel();
                this.txtSysPassowrd.Clear();
            }
        }

        public override void Close()
        {
            this.panelDevicePresets.Close();
            this.panelDataBackupMgt.Close();
        }

        //public override void BindingController(MController mController_, EventBus eventBus_)
        //{
        //    base.BindingController(mController_, eventBus_);
        //}

        public void BindingSystemModel(SystemModuleBase system_)
        {
            Debug.Assert(base.mController != null && system_ != null);

            this.system = system_;
            this.volMgtPanel.BindingSystemModule(this.system, this._eventBus);
            this.changePasswordPanelKeyboardLock.Init(this.system.keyboardLockPassword, PasswordChanged);
            this.changePasswordPanelSystem.Init(this.system.systemPassword, PasswordChanged);
            this.panelDevicePresets.Binding(base.mController, this.system, this._eventBus);
            this.panelDataBackupMgt.Binding(base.mController, this._eventBus);
            this.InitUI();
        }

        public override void UpdateUIFromData()
        {
            this.volMgtPanel.UpdateUIFromData();
            this.changePasswordPanelKeyboardLock.UpdateUIFromData(this.system.keyboardLockPassword);
            this.changePasswordPanelSystem.UpdateUIFromData(this.system.systemPassword);
            this.panelDevicePresets.UpdateUIFromData();
            this.panelDataBackupMgt.UpdateUIFromData();
            this.InitUI();

            this.UpdateFormUsageLimitLockManagement();
        }

        public void OnEffectorModelChanged()
        {
            this.panelDataBackupMgt.UpdateUIFromData();
        }

        private void InitUI()
        {
            if (this.system.isLockKeyboard.value)
            {
                rdoEnableKeyLock.isCheckInit = true;
            }
            else
            {
                rdoDisableKeyLock.isCheckInit = true;
            }

            if (this.system.currUser == User.Normal)
            {
                rdoNormalUser.isCheckInit = true;
                rdoNormalAdmin.Enabled = false;
            }
            else
            {
                rdoNormalAdmin.isCheckInit = true;
                rdoNormalAdmin.Enabled = true;
            }

            if (this.system.isEnableRemote.value)
            {
                rdoEnableRemoteControl.isCheckInit = true;
            }
            else
            {
                rdoDisableRemoteControl.isCheckInit = true;
            }

            //bool timbreVisible = this.mController.currEffectorFamily == Effectors.MEffectorFamily.VKxS;
            bool timbreVisible = this.mController.mEffectorFeatures.IsSupportSwitchTimbre();
            this.panelTimbreControl.Visible = timbreVisible;
            if (timbreVisible)
            {
                switch(this.system.timbreControl.v)
                {
                    case 0:
                        this.rdoProTimbre.isCheckInit = true;
                        break;

                    case 1:
                        this.rdoClassicsTimbre.isCheckInit = true;
                        break;
                }
            }

            bool workModeVisible = this.mController.mEffectorFeatures.IsSupportSwitchWorkMode();
            this.panelDeviceWorkMode.Visible = workModeVisible;

            if (workModeVisible)
            {
                this.panelDeviceWorkMode.Enabled = this.system.currUser == User.Admin;

                if (this.system.deviceWorkMode.v == 0)
                {
                    this.rdoStandardMode.isCheckInit = true;
                }
                else
                {
                    this.rdoX5Mode.isCheckInit = true;
                }
            }

            bool clarityVisible = this.mController.mEffectorFeatures.IsSupportControlClarity() && this.system.isSupportClarity;
            this.panelClarityControl.Visible = clarityVisible;

            if (indexToRdoMap.ContainsKey(this.system.clarity.v))
            {
                indexToRdoMap[this.system.clarity.v].isCheckInit = true;
            }
            else
            {
                foreach (var index in indexToRdoMap.Keys)
                {
                    indexToRdoMap[index].isCheckInit = false;
                }
            }

            this.txtDeviceID.Text = this.system.deviceIdCode;

            //this.panelFirmware.Visible = base.mController.isSupportLoadDeviceFirmwareVersion;

            //if (base.mController.isSupportLoadDeviceFirmwareVersion)
            //{
            //    this.lblDevicePrototype.UpdateText(this.system.devicePrototypeModel, true);
            //    this.lblDeviceFirmwareVersion.UpdateText(this.system.firmwareVersion, true);
            //    this.lblDeviceFirmwareBuildNumber.UpdateText(this.system.firmwareBuildNumber, true);
            //}

            this.UpdateDeviceFirmware();
            
            this.panelDeviceInfo.Invalidate();
        }

        private void UpdateDeviceFirmware()
        {
            if(string.IsNullOrEmpty(this.system.devicePrototypeModel))
            {
                this.toolTipDeviceFirmware.SetToolTip(this.panelDeviceInfo, string.Empty);
            }
            else
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendFormat("{0}:{1}", localization[Localization.panelSystem_DevicePrototype], this.system.devicePrototypeModel);
                sb.AppendLine(string.Empty);
                sb.AppendFormat("{0}:{1}", localization[Localization.panelSystem_DeviceFirmwareVersion], this.system.firmwareVersion);
                sb.AppendLine(string.Empty);
                sb.AppendFormat("{0}:{1}", localization[Localization.panelSystem_DeviceFirmwareBuildNumber], this.system.firmwareBuildNumber);

                this.toolTipDeviceFirmware.SetToolTip(this.panelDeviceInfo, sb.ToString());
            }
        }

        private void PasswordChanged(object sender, EventBusArgs e)
        {
            if (e.type == EventType.PasswordUpdated)
            {
                string newPassword = (e as PasswordUpdatedArgs).passowrd;
                
                if(sender.Equals(this.changePasswordPanelKeyboardLock))
                {
                    system.keyboardLockPassword = newPassword;
                    this.changePasswordPanelKeyboardLock.UpdateUIFromData(this.system.keyboardLockPassword);
                }
                else if (sender.Equals(this.changePasswordPanelSystem))
                {
                    system.systemPassword = newPassword;
                    this.changePasswordPanelSystem.UpdateUIFromData(this.system.systemPassword);
                }
            }
        }

        private void rdo_CheckedChanged(object sender, EventArgs e)
        {
            gtRadioButtonWithText rdo = sender as gtRadioButtonWithText;
            if(!rdo.isChecked)
            {
                return;
            }

            if(sender.Equals(this.rdoEnableKeyLock))
            {
                this.system.isLockKeyboard.value = true;
            }
            else if(sender.Equals(this.rdoDisableKeyLock))
            {
                this.system.isLockKeyboard.value = false;
            }
            else if (sender.Equals(this.rdoNormalUser))
            {
                this.system.currUser = User.Normal;
                //base.effector.isLockUserOperation = true;
                this.rdoNormalAdmin.Enabled = false;
                this.panelDeviceWorkMode.Enabled = false;
                this._eventBus.NotifyObeserver(new SystemUserChangedArgs());
            }
            else if (sender.Equals(this.rdoNormalAdmin))
            {
                this.system.currUser = User.Admin;
                //base.effector.isLockUserOperation = false;
                this.rdoNormalAdmin.Enabled = false;
                this.panelDeviceWorkMode.Enabled = true;
                this._eventBus.NotifyObeserver(new SystemUserChangedArgs());
            }
            else if (sender.Equals(this.rdoEnableRemoteControl))
            {
                this.system.isEnableRemote.value = true;
            }
            else if (sender.Equals(this.rdoDisableRemoteControl))
            {
                this.system.isEnableRemote.value = false;
            }
            else if (sender.Equals(this.rdoProTimbre))
            {
                this.system.timbreControl.v = 0;
            }
            else if (sender.Equals(this.rdoClassicsTimbre))
            {
                this.system.timbreControl.v = 1;
            }
            else if(sender.Equals(this.rdoStandardMode))
            {
                this.SwitchDeviceWorkMode(0, true, false);
            }
            else if(sender.Equals(this.rdoX5Mode))
            {
                this.SwitchDeviceWorkMode(1, true, false);
            }
            else if(rdoToIndexMap.ContainsKey(rdo))
            {
                this.system.clarity.v = rdoToIndexMap[rdo];
            }
        }

        private void SwitchDeviceWorkMode(short index, bool retainsData, bool outputMute)
        {
            SwitchWorkMode switchWorkMode = new SwitchWorkMode(index, retainsData, outputMute);
            this._eventBus.NotifyObeserver(new SwitchDeviceWorkModeArgs(switchWorkMode));
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.buttonOK))
            {
                this.VerifySystemPassword();
            }
        }

        private void txtSysPassowrd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;

            this.VerifySystemPassword();
        }

        private void VerifySystemPassword()
        {
            if (this.txtSysPassowrd.Text == this.system.systemPassword)
            {
                this.rdoNormalAdmin.Enabled = true;
                this.txtSysPassowrd.Clear();
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.txtSysPassowrd.Focused)
            {
                return false;
            }
            else
            {
                return base.ProcessDialogKey(keyData);
            }
        }

        public override void UpdateDisplayLanguage()
        {
            this.volMgtPanel.UpdateDisplayLanguage();

            this.panelKeyLock.title = localization[Localization.panelSystem_AutoLockKeyboard];
            this.rdoEnableKeyLock.Text = localization[Localization.panelSystem_Enabled];
            this.rdoDisableKeyLock.Text = localization[Localization.panelSystem_Disabled];
            this.panelKeyLock.Invalidate();

            this.panelSystemUser.title = localization[Localization.panelSystem_SystemUser];
            this.lblSystemPassowrd.Text = localization[Localization.panelSystem_SystemPassword];
            this.buttonOK.Text = localization[Localization.panelSystem_OK];
            this.rdoNormalUser.Text = localization[Localization.panelSystem_NormalUser];
            this.rdoNormalAdmin.Text = localization[Localization.panelSystem_Admin];
            this.panelSystemUser.Invalidate();

            this.changePasswordPanelKeyboardLock.title = localization[Localization.panelSystem_Passowrd_ChangeKeyboardLockPassowrd];
            this.changePasswordPanelKeyboardLock.changePasswordTips = localization[Localization.panelSystem_Passowrd_AskForChangeKeyboardPassword];
            this.changePasswordPanelKeyboardLock.UpdateDisplayLanguage();

            this.changePasswordPanelSystem.title = localization[Localization.panelSystem_Passowrd_ChangeSystemUserPassowrd];
            this.changePasswordPanelSystem.changePasswordTips = localization[Localization.panelSystem_Passowrd_AskForChangeSystemPassword];
            this.changePasswordPanelSystem.UpdateDisplayLanguage();

            this.panelDataBackupMgt.UpdateDisplayLanguage();
            this.panelDevicePresets.UpdateDisplayLanguage();

            this.panelInfraredRemoteControlMgt.title = localization[Localization.panelSystem_InfraredRemoteControl];
            this.rdoEnableRemoteControl.Text = localization[Localization.panelSystem_Enabled];
            this.rdoDisableRemoteControl.Text = localization[Localization.panelSystem_Disabled];
            this.panelInfraredRemoteControlMgt.Invalidate();

            this.panelTimbreControl.title = localization[Localization.panelSystem_TimbreSelect];
            this.rdoProTimbre.Text = localization[Localization.panelSystem_TimbrePro];
            this.rdoClassicsTimbre.Text = localization[Localization.panelSystem_TimbreClassics];
            this.panelTimbreControl.Invalidate();

            this.panelDeviceWorkMode.title = localization[Localization.panelSystem_DeviceWorkMode];
            this.rdoStandardMode.Text = localization[Localization.panelSystem_DeviceWorkMode_Standard];
            this.rdoX5Mode.Text = localization[Localization.panelSystem_DeviceWorkMode_X5];
            this.panelDeviceWorkMode.Invalidate();
            this.panelDeviceWorkMode.Update();

            this.panelClarityControl.title = localization[Localization.panelSystem_Clarity];
            this.rdoSDA.Text = localization[Localization.panelSystem_Clarity_SDA];
            this.rdoSDB.Text = localization[Localization.panelSystem_Clarity_SDB];
            this.rdoHDA.Text = localization[Localization.panelSystem_Clarity_HDA];
            this.rdoHDB.Text = localization[Localization.panelSystem_Clarity_HDB];
            this.rdoUHDA.Text = localization[Localization.panelSystem_Clarity_UHDA];
            this.rdoUHDB.Text = localization[Localization.panelSystem_Clarity_UHDB];
            this.labelClarityTip.Text = localization[Localization.panelSystem_Clarity_Note];
            this.panelClarityControl.Invalidate();
            this.panelDeviceWorkMode.Update();

            this.panelDeviceInfo.title = localization[Localization.panelSystem_DeviceInfo];
            this.lblDeviceID.Text = localization[Localization.panelSystem_DeviceID];

            this.UpdateDeviceFirmware();
            
            this.panelDeviceInfo.Invalidate();

            this.Invalidate();
        }

        private bool panelDeviceInfoMouseIsDown = false;
        private int deviceInfoClickCount = 0;
        private const int MouseLeftButtonClickThreshold = 7;
        private UsageLimitLocks.FormUsageLimitLockManagement formUsageLimitLock = null;

        public void ShowFromUsageLimitLock()
        {
            if(this.formUsageLimitLock == null || this.formUsageLimitLock.IsDisposed)
            {
                this.formUsageLimitLock = new UsageLimitLocks.FormUsageLimitLockManagement();
                this.formUsageLimitLock.Binding(this.mController, this.system, this._eventBus);
                this.formUsageLimitLock.Owner = this.ParentForm;
            }
            else
            {
                this.formUsageLimitLock.Activate();
            }
            
            this.formUsageLimitLock.Show();
        }

        private void UpdateFormUsageLimitLockManagement()
        {
            if (this.formUsageLimitLock != null && !this.formUsageLimitLock.IsDisposed)
            {
                this.formUsageLimitLock.UpdateUIFromData(this.system);
            }
        }

        private void panelDeviceInfo_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (mController.isConnected && system.isSupportUsageLimitLock)
                {
                    this.panelDeviceInfo.FillColor = Color.FromArgb(224, 224, 224);
                    this.panelDeviceInfoMouseIsDown = true;
                }

                deviceInfoClickCount++;
                if (deviceInfoClickCount >= MouseLeftButtonClickThreshold)
                {
                    deviceInfoClickCount = 0;
                    this.ShowFromUsageLimitLock();
                }
                //else
                //{
                //    if (this.formUsageLimitLock != null && !this.formUsageLimitLock.IsDisposed)
                //    {
                //        this.formUsageLimitLock.Activate();
                //    }
                //}
            }
            else if(e.Button == System.Windows.Forms.MouseButtons.Right && (Control.ModifierKeys & Keys.Control) != 0)
            {
                if (mController.isConnected && system.isSupportUsageLimitLock)
                {
                    this.picLock.Visible = true;
                    //Log.OutputLine("show lock");
                }

                if((Control.ModifierKeys & Keys.Alt) != 0)
                {
                    deviceInfoClickCount = 0;
                    this.ShowFromUsageLimitLock();
                }
            }
        }

        private void panelDeviceInfo_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if(panelDeviceInfoMouseIsDown)
                {
                    panelDeviceInfoMouseIsDown = false;
                    this.panelDeviceInfo.FillColor = SkinManager.CurrentSkin.PanelBackColor; 
                }
            }
            else if(e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.picLock.Visible = false;
            }
        }

#if COMM_TEST
        public void RandLoadDataToDevice()
        {
            this.panelDataBackupMgt.RandLoadDataToDevice();
        }

        public void RandSaveMode()
        {
            this.panelDevicePresets.RandSaveMode();
        }

        public void RandCallMode()
        {
            this.panelDevicePresets.RandCallMode();
        }
#endif
    }
}
