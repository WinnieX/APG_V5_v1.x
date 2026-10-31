namespace AEC.UI
{
    partial class SystemPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelDataBackupMgt = new AEC.UI.DataBackupMgtPanel();
            this.panelDevicePresets = new AEC.UI.DeviceModePanel();
            this.panelSystemUser = new AEC.UI.gtPanel();
            this.buttonOK = new AEC.UI.Controls.MButton();
            this.rdoNormalAdmin = new AEC.UI.gtRadioButtonWithText();
            this.lblSystemPassowrd = new AEC.UI.gtLabel();
            this.txtSysPassowrd = new System.Windows.Forms.TextBox();
            this.rdoNormalUser = new AEC.UI.gtRadioButtonWithText();
            this.panelKeyLock = new AEC.UI.gtPanel();
            this.rdoDisableKeyLock = new AEC.UI.gtRadioButtonWithText();
            this.rdoEnableKeyLock = new AEC.UI.gtRadioButtonWithText();
            this.panelInfraredRemoteControlMgt = new AEC.UI.gtPanel();
            this.rdoDisableRemoteControl = new AEC.UI.gtRadioButtonWithText();
            this.rdoEnableRemoteControl = new AEC.UI.gtRadioButtonWithText();
            this.panelDeviceInfo = new AEC.UI.gtPanel();
            this.picLock = new System.Windows.Forms.PictureBox();
            this.lblDeviceID = new AEC.UI.gtLabel();
            this.txtDeviceID = new System.Windows.Forms.TextBox();
            this.changePasswordPanelSystem = new AEC.UI.ChangePasswordPanel();
            this.changePasswordPanelKeyboardLock = new AEC.UI.ChangePasswordPanel();
            this.volMgtPanel = new AEC.UI.VolMgtPanel();
            this.panelTimbreControl = new AEC.UI.gtPanel();
            this.rdoClassicsTimbre = new AEC.UI.gtRadioButtonWithText();
            this.rdoProTimbre = new AEC.UI.gtRadioButtonWithText();
            this.panelDeviceWorkMode = new AEC.UI.gtPanel();
            this.rdoX5Mode = new AEC.UI.gtRadioButtonWithText();
            this.rdoStandardMode = new AEC.UI.gtRadioButtonWithText();
            this.toolTipDeviceFirmware = new System.Windows.Forms.ToolTip(this.components);
            this.panelClarityControl = new AEC.UI.gtPanel();
            this.labelClarityTip = new System.Windows.Forms.Label();
            this.rdoUHDB = new AEC.UI.gtRadioButtonWithText();
            this.rdoUHDA = new AEC.UI.gtRadioButtonWithText();
            this.rdoHDB = new AEC.UI.gtRadioButtonWithText();
            this.rdoHDA = new AEC.UI.gtRadioButtonWithText();
            this.rdoSDB = new AEC.UI.gtRadioButtonWithText();
            this.rdoSDA = new AEC.UI.gtRadioButtonWithText();
            this.panelSystemUser.SuspendLayout();
            this.panelKeyLock.SuspendLayout();
            this.panelInfraredRemoteControlMgt.SuspendLayout();
            this.panelDeviceInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLock)).BeginInit();
            this.panelTimbreControl.SuspendLayout();
            this.panelDeviceWorkMode.SuspendLayout();
            this.panelClarityControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDataBackupMgt
            // 
            this.panelDataBackupMgt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.panelDataBackupMgt.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelDataBackupMgt.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelDataBackupMgt.ForeColor = System.Drawing.Color.LightGray;
            this.panelDataBackupMgt.Location = new System.Drawing.Point(540, 8);
            this.panelDataBackupMgt.Margin = new System.Windows.Forms.Padding(0);
            this.panelDataBackupMgt.Name = "panelDataBackupMgt";
            this.panelDataBackupMgt.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.panelDataBackupMgt.Radius = 18;
            this.panelDataBackupMgt.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.panelDataBackupMgt.ShowRect = true;
            this.panelDataBackupMgt.Size = new System.Drawing.Size(724, 308);
            this.panelDataBackupMgt.TabIndex = 5;
            this.panelDataBackupMgt.title = "数据备份管理";
            this.panelDataBackupMgt.titleHeight = 16;
            // 
            // panelDevicePresets
            // 
            this.panelDevicePresets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.panelDevicePresets.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelDevicePresets.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelDevicePresets.ForeColor = System.Drawing.Color.LightGray;
            this.panelDevicePresets.isLockSaveLastPreset = false;
            this.panelDevicePresets.Location = new System.Drawing.Point(540, 324);
            this.panelDevicePresets.Margin = new System.Windows.Forms.Padding(0);
            this.panelDevicePresets.Name = "panelDevicePresets";
            this.panelDevicePresets.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.panelDevicePresets.Radius = 18;
            this.panelDevicePresets.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.panelDevicePresets.ShowRect = true;
            this.panelDevicePresets.Size = new System.Drawing.Size(456, 318);
            this.panelDevicePresets.TabIndex = 4;
            this.panelDevicePresets.title = "设备模式管理";
            this.panelDevicePresets.titleHeight = 16;
            // 
            // panelSystemUser
            // 
            this.panelSystemUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.panelSystemUser.Controls.Add(this.buttonOK);
            this.panelSystemUser.Controls.Add(this.rdoNormalAdmin);
            this.panelSystemUser.Controls.Add(this.lblSystemPassowrd);
            this.panelSystemUser.Controls.Add(this.txtSysPassowrd);
            this.panelSystemUser.Controls.Add(this.rdoNormalUser);
            this.panelSystemUser.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelSystemUser.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelSystemUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelSystemUser.Location = new System.Drawing.Point(14, 488);
            this.panelSystemUser.Margin = new System.Windows.Forms.Padding(0);
            this.panelSystemUser.Name = "panelSystemUser";
            this.panelSystemUser.Padding = new System.Windows.Forms.Padding(0, 6, 2, 0);
            this.panelSystemUser.Radius = 18;
            this.panelSystemUser.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.panelSystemUser.ShowRect = true;
            this.panelSystemUser.Size = new System.Drawing.Size(254, 154);
            this.panelSystemUser.TabIndex = 3;
            this.panelSystemUser.title = "系统用户设置";
            this.panelSystemUser.titleHeight = 20;
            // 
            // buttonOK
            // 
            this.buttonOK.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.buttonOK.BorderWidth = 1F;
            this.buttonOK.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.buttonOK.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.buttonOK.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.buttonOK.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.buttonOK.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonOK.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(204)))), ((int)(((byte)(6)))));
            this.buttonOK.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonOK.Location = new System.Drawing.Point(179, 46);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.buttonOK.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(188)))), ((int)(((byte)(6)))));
            this.buttonOK.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonOK.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.buttonOK.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(153)))), ((int)(((byte)(5)))));
            this.buttonOK.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.buttonOK.Radius = 12;
            this.buttonOK.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.buttonOK.ShowBorder = false;
            this.buttonOK.ShowText = true;
            this.buttonOK.Size = new System.Drawing.Size(46, 23);
            this.buttonOK.TabIndex = 7;
            this.buttonOK.Text = "确定";
            this.buttonOK.Click += new System.EventHandler(this.button_Click);
            // 
            // rdoNormalAdmin
            // 
            this.rdoNormalAdmin.backChecked = global::AEC.Properties.Resources.rdo_checked;
            this.rdoNormalAdmin.BackColor = System.Drawing.Color.Transparent;
            this.rdoNormalAdmin.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoNormalAdmin.backHover = global::AEC.Properties.Resources.rdo_hover;
            this.rdoNormalAdmin.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoNormalAdmin.isAutoCheck = true;
            this.rdoNormalAdmin.isChecked = false;
            this.rdoNormalAdmin.isCheckInit = false;
            this.rdoNormalAdmin.isShowCheckedBackWhenDsiabled = true;
            this.rdoNormalAdmin.isStretch = false;
            this.rdoNormalAdmin.Location = new System.Drawing.Point(143, 94);
            this.rdoNormalAdmin.Name = "rdoNormalAdmin";
            this.rdoNormalAdmin.Size = new System.Drawing.Size(82, 30);
            this.rdoNormalAdmin.TabIndex = 0;
            this.rdoNormalAdmin.Text = "管理员";
            this.rdoNormalAdmin.userGDIPaint = true;
            this.rdoNormalAdmin.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // lblSystemPassowrd
            // 
            this.lblSystemPassowrd.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblSystemPassowrd.BackColor = System.Drawing.Color.Transparent;
            this.lblSystemPassowrd.Location = new System.Drawing.Point(2, 47);
            this.lblSystemPassowrd.Name = "lblSystemPassowrd";
            this.lblSystemPassowrd.Size = new System.Drawing.Size(98, 23);
            this.lblSystemPassowrd.TabIndex = 4;
            this.lblSystemPassowrd.Text = " 系统权限密码";
            // 
            // txtSysPassowrd
            // 
            this.txtSysPassowrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(240)))));
            this.txtSysPassowrd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSysPassowrd.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtSysPassowrd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.txtSysPassowrd.Location = new System.Drawing.Point(105, 44);
            this.txtSysPassowrd.MaxLength = 6;
            this.txtSysPassowrd.Name = "txtSysPassowrd";
            this.txtSysPassowrd.PasswordChar = '●';
            this.txtSysPassowrd.ShortcutsEnabled = false;
            this.txtSysPassowrd.Size = new System.Drawing.Size(67, 27);
            this.txtSysPassowrd.TabIndex = 3;
            this.txtSysPassowrd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSysPassowrd_KeyDown);
            // 
            // rdoNormalUser
            // 
            this.rdoNormalUser.backChecked = global::AEC.Properties.Resources.rdo_checked;
            this.rdoNormalUser.BackColor = System.Drawing.Color.Transparent;
            this.rdoNormalUser.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoNormalUser.backHover = global::AEC.Properties.Resources.rdo_hover;
            this.rdoNormalUser.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoNormalUser.isAutoCheck = true;
            this.rdoNormalUser.isChecked = false;
            this.rdoNormalUser.isCheckInit = false;
            this.rdoNormalUser.isShowCheckedBackWhenDsiabled = true;
            this.rdoNormalUser.isStretch = false;
            this.rdoNormalUser.Location = new System.Drawing.Point(33, 94);
            this.rdoNormalUser.Name = "rdoNormalUser";
            this.rdoNormalUser.Size = new System.Drawing.Size(87, 30);
            this.rdoNormalUser.TabIndex = 0;
            this.rdoNormalUser.Text = "普通用户";
            this.rdoNormalUser.userGDIPaint = true;
            this.rdoNormalUser.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // panelKeyLock
            // 
            this.panelKeyLock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.panelKeyLock.Controls.Add(this.rdoDisableKeyLock);
            this.panelKeyLock.Controls.Add(this.rdoEnableKeyLock);
            this.panelKeyLock.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelKeyLock.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelKeyLock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelKeyLock.Location = new System.Drawing.Point(14, 324);
            this.panelKeyLock.Margin = new System.Windows.Forms.Padding(0);
            this.panelKeyLock.Name = "panelKeyLock";
            this.panelKeyLock.Padding = new System.Windows.Forms.Padding(0, 6, 2, 0);
            this.panelKeyLock.Radius = 18;
            this.panelKeyLock.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.panelKeyLock.ShowRect = true;
            this.panelKeyLock.Size = new System.Drawing.Size(254, 154);
            this.panelKeyLock.TabIndex = 3;
            this.panelKeyLock.title = "自动锁键盘";
            this.panelKeyLock.titleHeight = 20;
            // 
            // rdoDisableKeyLock
            // 
            this.rdoDisableKeyLock.backChecked = global::AEC.Properties.Resources.rdo_checked;
            this.rdoDisableKeyLock.BackColor = System.Drawing.Color.Transparent;
            this.rdoDisableKeyLock.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoDisableKeyLock.backHover = global::AEC.Properties.Resources.rdo_hover;
            this.rdoDisableKeyLock.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoDisableKeyLock.isAutoCheck = true;
            this.rdoDisableKeyLock.isChecked = false;
            this.rdoDisableKeyLock.isCheckInit = false;
            this.rdoDisableKeyLock.isShowCheckedBackWhenDsiabled = true;
            this.rdoDisableKeyLock.isStretch = false;
            this.rdoDisableKeyLock.Location = new System.Drawing.Point(143, 86);
            this.rdoDisableKeyLock.Name = "rdoDisableKeyLock";
            this.rdoDisableKeyLock.Size = new System.Drawing.Size(80, 30);
            this.rdoDisableKeyLock.TabIndex = 0;
            this.rdoDisableKeyLock.Text = "禁用";
            this.rdoDisableKeyLock.userGDIPaint = true;
            this.rdoDisableKeyLock.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // rdoEnableKeyLock
            // 
            this.rdoEnableKeyLock.backChecked = global::AEC.Properties.Resources.rdo_checked;
            this.rdoEnableKeyLock.BackColor = System.Drawing.Color.Transparent;
            this.rdoEnableKeyLock.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoEnableKeyLock.backHover = global::AEC.Properties.Resources.rdo_hover;
            this.rdoEnableKeyLock.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoEnableKeyLock.isAutoCheck = true;
            this.rdoEnableKeyLock.isChecked = false;
            this.rdoEnableKeyLock.isCheckInit = false;
            this.rdoEnableKeyLock.isShowCheckedBackWhenDsiabled = true;
            this.rdoEnableKeyLock.isStretch = false;
            this.rdoEnableKeyLock.Location = new System.Drawing.Point(33, 86);
            this.rdoEnableKeyLock.Name = "rdoEnableKeyLock";
            this.rdoEnableKeyLock.Size = new System.Drawing.Size(80, 30);
            this.rdoEnableKeyLock.TabIndex = 0;
            this.rdoEnableKeyLock.Text = "开启";
            this.rdoEnableKeyLock.userGDIPaint = true;
            this.rdoEnableKeyLock.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // panelInfraredRemoteControlMgt
            // 
            this.panelInfraredRemoteControlMgt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.panelInfraredRemoteControlMgt.Controls.Add(this.rdoDisableRemoteControl);
            this.panelInfraredRemoteControlMgt.Controls.Add(this.rdoEnableRemoteControl);
            this.panelInfraredRemoteControlMgt.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelInfraredRemoteControlMgt.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelInfraredRemoteControlMgt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelInfraredRemoteControlMgt.Location = new System.Drawing.Point(1010, 324);
            this.panelInfraredRemoteControlMgt.Margin = new System.Windows.Forms.Padding(0);
            this.panelInfraredRemoteControlMgt.Name = "panelInfraredRemoteControlMgt";
            this.panelInfraredRemoteControlMgt.Padding = new System.Windows.Forms.Padding(0, 6, 2, 0);
            this.panelInfraredRemoteControlMgt.Radius = 18;
            this.panelInfraredRemoteControlMgt.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.panelInfraredRemoteControlMgt.ShowRect = true;
            this.panelInfraredRemoteControlMgt.Size = new System.Drawing.Size(254, 74);
            this.panelInfraredRemoteControlMgt.TabIndex = 2;
            this.panelInfraredRemoteControlMgt.title = "红外遥控管理";
            this.panelInfraredRemoteControlMgt.titleHeight = 20;
            // 
            // rdoDisableRemoteControl
            // 
            this.rdoDisableRemoteControl.backChecked = global::AEC.Properties.Resources.rdo_checked;
            this.rdoDisableRemoteControl.BackColor = System.Drawing.Color.Transparent;
            this.rdoDisableRemoteControl.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoDisableRemoteControl.backHover = global::AEC.Properties.Resources.rdo_hover;
            this.rdoDisableRemoteControl.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoDisableRemoteControl.isAutoCheck = true;
            this.rdoDisableRemoteControl.isChecked = false;
            this.rdoDisableRemoteControl.isCheckInit = false;
            this.rdoDisableRemoteControl.isShowCheckedBackWhenDsiabled = true;
            this.rdoDisableRemoteControl.isStretch = false;
            this.rdoDisableRemoteControl.Location = new System.Drawing.Point(137, 32);
            this.rdoDisableRemoteControl.Name = "rdoDisableRemoteControl";
            this.rdoDisableRemoteControl.Size = new System.Drawing.Size(80, 30);
            this.rdoDisableRemoteControl.TabIndex = 0;
            this.rdoDisableRemoteControl.Text = "禁用";
            this.rdoDisableRemoteControl.userGDIPaint = true;
            this.rdoDisableRemoteControl.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // rdoEnableRemoteControl
            // 
            this.rdoEnableRemoteControl.backChecked = global::AEC.Properties.Resources.rdo_checked;
            this.rdoEnableRemoteControl.BackColor = System.Drawing.Color.Transparent;
            this.rdoEnableRemoteControl.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoEnableRemoteControl.backHover = global::AEC.Properties.Resources.rdo_hover;
            this.rdoEnableRemoteControl.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoEnableRemoteControl.isAutoCheck = true;
            this.rdoEnableRemoteControl.isChecked = false;
            this.rdoEnableRemoteControl.isCheckInit = false;
            this.rdoEnableRemoteControl.isShowCheckedBackWhenDsiabled = true;
            this.rdoEnableRemoteControl.isStretch = false;
            this.rdoEnableRemoteControl.Location = new System.Drawing.Point(27, 32);
            this.rdoEnableRemoteControl.Name = "rdoEnableRemoteControl";
            this.rdoEnableRemoteControl.Size = new System.Drawing.Size(80, 30);
            this.rdoEnableRemoteControl.TabIndex = 0;
            this.rdoEnableRemoteControl.Text = "开启";
            this.rdoEnableRemoteControl.userGDIPaint = true;
            this.rdoEnableRemoteControl.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // panelDeviceInfo
            // 
            this.panelDeviceInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.panelDeviceInfo.Controls.Add(this.picLock);
            this.panelDeviceInfo.Controls.Add(this.lblDeviceID);
            this.panelDeviceInfo.Controls.Add(this.txtDeviceID);
            this.panelDeviceInfo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelDeviceInfo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelDeviceInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelDeviceInfo.Location = new System.Drawing.Point(1010, 568);
            this.panelDeviceInfo.Margin = new System.Windows.Forms.Padding(0);
            this.panelDeviceInfo.Name = "panelDeviceInfo";
            this.panelDeviceInfo.Padding = new System.Windows.Forms.Padding(0, 6, 2, 0);
            this.panelDeviceInfo.Radius = 18;
            this.panelDeviceInfo.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.panelDeviceInfo.ShowRect = true;
            this.panelDeviceInfo.Size = new System.Drawing.Size(254, 74);
            this.panelDeviceInfo.TabIndex = 2;
            this.panelDeviceInfo.title = "设备信息";
            this.panelDeviceInfo.titleHeight = 20;
            this.panelDeviceInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelDeviceInfo_MouseDown);
            this.panelDeviceInfo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelDeviceInfo_MouseUp);
            // 
            // picLock
            // 
            this.picLock.Image = global::AEC.Properties.Resources.system_usage_limit_lock;
            this.picLock.Location = new System.Drawing.Point(10, 10);
            this.picLock.Name = "picLock";
            this.picLock.Size = new System.Drawing.Size(24, 24);
            this.picLock.TabIndex = 9;
            this.picLock.TabStop = false;
            this.picLock.Visible = false;
            // 
            // lblDeviceID
            // 
            this.lblDeviceID.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblDeviceID.BackColor = System.Drawing.Color.Transparent;
            this.lblDeviceID.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblDeviceID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblDeviceID.Location = new System.Drawing.Point(28, 34);
            this.lblDeviceID.Name = "lblDeviceID";
            this.lblDeviceID.Size = new System.Drawing.Size(80, 23);
            this.lblDeviceID.TabIndex = 8;
            this.lblDeviceID.Text = "设备 ID：";
            this.lblDeviceID.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelDeviceInfo_MouseDown);
            // 
            // txtDeviceID
            // 
            this.txtDeviceID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtDeviceID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeviceID.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtDeviceID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtDeviceID.Location = new System.Drawing.Point(108, 32);
            this.txtDeviceID.Name = "txtDeviceID";
            this.txtDeviceID.ReadOnly = true;
            this.txtDeviceID.Size = new System.Drawing.Size(106, 27);
            this.txtDeviceID.TabIndex = 3;
            // 
            // changePasswordPanelSystem
            // 
            this.changePasswordPanelSystem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.changePasswordPanelSystem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.changePasswordPanelSystem.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.changePasswordPanelSystem.ForeColor = System.Drawing.Color.LightGray;
            this.changePasswordPanelSystem.Location = new System.Drawing.Point(282, 488);
            this.changePasswordPanelSystem.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.changePasswordPanelSystem.Name = "changePasswordPanelSystem";
            this.changePasswordPanelSystem.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.changePasswordPanelSystem.Radius = 18;
            this.changePasswordPanelSystem.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.changePasswordPanelSystem.ShowRect = true;
            this.changePasswordPanelSystem.Size = new System.Drawing.Size(244, 154);
            this.changePasswordPanelSystem.TabIndex = 1;
            this.changePasswordPanelSystem.title = "修改系统密码";
            this.changePasswordPanelSystem.titleHeight = 20;
            // 
            // changePasswordPanelKeyboardLock
            // 
            this.changePasswordPanelKeyboardLock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.changePasswordPanelKeyboardLock.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.changePasswordPanelKeyboardLock.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.changePasswordPanelKeyboardLock.ForeColor = System.Drawing.Color.LightGray;
            this.changePasswordPanelKeyboardLock.Location = new System.Drawing.Point(282, 324);
            this.changePasswordPanelKeyboardLock.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.changePasswordPanelKeyboardLock.Name = "changePasswordPanelKeyboardLock";
            this.changePasswordPanelKeyboardLock.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.changePasswordPanelKeyboardLock.Radius = 18;
            this.changePasswordPanelKeyboardLock.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.changePasswordPanelKeyboardLock.ShowRect = true;
            this.changePasswordPanelKeyboardLock.Size = new System.Drawing.Size(244, 154);
            this.changePasswordPanelKeyboardLock.TabIndex = 1;
            this.changePasswordPanelKeyboardLock.title = "修改键盘锁密码";
            this.changePasswordPanelKeyboardLock.titleHeight = 20;
            // 
            // volMgtPanel
            // 
            this.volMgtPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.volMgtPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.volMgtPanel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.volMgtPanel.ForeColor = System.Drawing.Color.LightGray;
            this.volMgtPanel.Location = new System.Drawing.Point(14, 8);
            this.volMgtPanel.Margin = new System.Windows.Forms.Padding(0);
            this.volMgtPanel.Name = "volMgtPanel";
            this.volMgtPanel.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.volMgtPanel.Radius = 18;
            this.volMgtPanel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.volMgtPanel.ShowRect = true;
            this.volMgtPanel.Size = new System.Drawing.Size(512, 308);
            this.volMgtPanel.TabIndex = 0;
            this.volMgtPanel.title = "音量控制";
            this.volMgtPanel.titleHeight = 16;
            // 
            // panelTimbreControl
            // 
            this.panelTimbreControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.panelTimbreControl.Controls.Add(this.rdoClassicsTimbre);
            this.panelTimbreControl.Controls.Add(this.rdoProTimbre);
            this.panelTimbreControl.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelTimbreControl.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelTimbreControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelTimbreControl.Location = new System.Drawing.Point(1010, 406);
            this.panelTimbreControl.Margin = new System.Windows.Forms.Padding(0);
            this.panelTimbreControl.Name = "panelTimbreControl";
            this.panelTimbreControl.Padding = new System.Windows.Forms.Padding(0, 6, 2, 0);
            this.panelTimbreControl.Radius = 18;
            this.panelTimbreControl.RectColor = System.Drawing.Color.Black;
            this.panelTimbreControl.ShowRect = true;
            this.panelTimbreControl.Size = new System.Drawing.Size(254, 74);
            this.panelTimbreControl.TabIndex = 3;
            this.panelTimbreControl.title = "音色调节";
            this.panelTimbreControl.titleHeight = 20;
            // 
            // rdoClassicsTimbre
            // 
            this.rdoClassicsTimbre.backChecked = global::AEC.Properties.Resources.rdo_checked;
            this.rdoClassicsTimbre.BackColor = System.Drawing.Color.Transparent;
            this.rdoClassicsTimbre.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoClassicsTimbre.backHover = global::AEC.Properties.Resources.rdo_hover;
            this.rdoClassicsTimbre.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoClassicsTimbre.isAutoCheck = true;
            this.rdoClassicsTimbre.isChecked = false;
            this.rdoClassicsTimbre.isCheckInit = false;
            this.rdoClassicsTimbre.isShowCheckedBackWhenDsiabled = true;
            this.rdoClassicsTimbre.isStretch = false;
            this.rdoClassicsTimbre.Location = new System.Drawing.Point(137, 32);
            this.rdoClassicsTimbre.Name = "rdoClassicsTimbre";
            this.rdoClassicsTimbre.Size = new System.Drawing.Size(108, 30);
            this.rdoClassicsTimbre.TabIndex = 0;
            this.rdoClassicsTimbre.Text = "经典";
            this.rdoClassicsTimbre.userGDIPaint = true;
            this.rdoClassicsTimbre.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // rdoProTimbre
            // 
            this.rdoProTimbre.backChecked = global::AEC.Properties.Resources.rdo_checked;
            this.rdoProTimbre.BackColor = System.Drawing.Color.Transparent;
            this.rdoProTimbre.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoProTimbre.backHover = global::AEC.Properties.Resources.rdo_hover;
            this.rdoProTimbre.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoProTimbre.isAutoCheck = true;
            this.rdoProTimbre.isChecked = false;
            this.rdoProTimbre.isCheckInit = false;
            this.rdoProTimbre.isShowCheckedBackWhenDsiabled = true;
            this.rdoProTimbre.isStretch = false;
            this.rdoProTimbre.Location = new System.Drawing.Point(27, 32);
            this.rdoProTimbre.Name = "rdoProTimbre";
            this.rdoProTimbre.Size = new System.Drawing.Size(108, 30);
            this.rdoProTimbre.TabIndex = 0;
            this.rdoProTimbre.Text = "专业";
            this.rdoProTimbre.userGDIPaint = true;
            this.rdoProTimbre.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // panelDeviceWorkMode
            // 
            this.panelDeviceWorkMode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.panelDeviceWorkMode.Controls.Add(this.rdoX5Mode);
            this.panelDeviceWorkMode.Controls.Add(this.rdoStandardMode);
            this.panelDeviceWorkMode.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelDeviceWorkMode.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelDeviceWorkMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelDeviceWorkMode.Location = new System.Drawing.Point(1010, 487);
            this.panelDeviceWorkMode.Margin = new System.Windows.Forms.Padding(0);
            this.panelDeviceWorkMode.Name = "panelDeviceWorkMode";
            this.panelDeviceWorkMode.Padding = new System.Windows.Forms.Padding(0, 6, 2, 0);
            this.panelDeviceWorkMode.Radius = 18;
            this.panelDeviceWorkMode.RectColor = System.Drawing.Color.Black;
            this.panelDeviceWorkMode.ShowRect = true;
            this.panelDeviceWorkMode.Size = new System.Drawing.Size(254, 74);
            this.panelDeviceWorkMode.TabIndex = 4;
            this.panelDeviceWorkMode.title = "设备模式";
            this.panelDeviceWorkMode.titleHeight = 20;
            // 
            // rdoX5Mode
            // 
            this.rdoX5Mode.backChecked = global::AEC.Properties.Resources.rdo_checked;
            this.rdoX5Mode.BackColor = System.Drawing.Color.Transparent;
            this.rdoX5Mode.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoX5Mode.backHover = global::AEC.Properties.Resources.rdo_hover;
            this.rdoX5Mode.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoX5Mode.isAutoCheck = true;
            this.rdoX5Mode.isChecked = false;
            this.rdoX5Mode.isCheckInit = false;
            this.rdoX5Mode.isShowCheckedBackWhenDsiabled = true;
            this.rdoX5Mode.isStretch = false;
            this.rdoX5Mode.Location = new System.Drawing.Point(137, 32);
            this.rdoX5Mode.Name = "rdoX5Mode";
            this.rdoX5Mode.Size = new System.Drawing.Size(108, 30);
            this.rdoX5Mode.TabIndex = 0;
            this.rdoX5Mode.Text = "X5模式";
            this.rdoX5Mode.userGDIPaint = true;
            this.rdoX5Mode.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // rdoStandardMode
            // 
            this.rdoStandardMode.backChecked = global::AEC.Properties.Resources.rdo_checked;
            this.rdoStandardMode.BackColor = System.Drawing.Color.Transparent;
            this.rdoStandardMode.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoStandardMode.backHover = global::AEC.Properties.Resources.rdo_hover;
            this.rdoStandardMode.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoStandardMode.isAutoCheck = true;
            this.rdoStandardMode.isChecked = false;
            this.rdoStandardMode.isCheckInit = false;
            this.rdoStandardMode.isShowCheckedBackWhenDsiabled = true;
            this.rdoStandardMode.isStretch = false;
            this.rdoStandardMode.Location = new System.Drawing.Point(27, 32);
            this.rdoStandardMode.Name = "rdoStandardMode";
            this.rdoStandardMode.Size = new System.Drawing.Size(108, 30);
            this.rdoStandardMode.TabIndex = 0;
            this.rdoStandardMode.Text = "标准模式";
            this.rdoStandardMode.userGDIPaint = true;
            this.rdoStandardMode.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // panelClarityControl
            // 
            this.panelClarityControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.panelClarityControl.Controls.Add(this.labelClarityTip);
            this.panelClarityControl.Controls.Add(this.rdoUHDB);
            this.panelClarityControl.Controls.Add(this.rdoUHDA);
            this.panelClarityControl.Controls.Add(this.rdoHDB);
            this.panelClarityControl.Controls.Add(this.rdoHDA);
            this.panelClarityControl.Controls.Add(this.rdoSDB);
            this.panelClarityControl.Controls.Add(this.rdoSDA);
            this.panelClarityControl.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelClarityControl.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelClarityControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelClarityControl.Location = new System.Drawing.Point(1010, 406);
            this.panelClarityControl.Margin = new System.Windows.Forms.Padding(0);
            this.panelClarityControl.Name = "panelClarityControl";
            this.panelClarityControl.Padding = new System.Windows.Forms.Padding(0, 6, 2, 0);
            this.panelClarityControl.Radius = 18;
            this.panelClarityControl.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.panelClarityControl.ShowRect = true;
            this.panelClarityControl.Size = new System.Drawing.Size(254, 155);
            this.panelClarityControl.TabIndex = 4;
            this.panelClarityControl.title = "清晰度";
            this.panelClarityControl.titleHeight = 20;
            this.panelClarityControl.Visible = false;
            // 
            // labelClarityTip
            // 
            this.labelClarityTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.labelClarityTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelClarityTip.Location = new System.Drawing.Point(6, 93);
            this.labelClarityTip.Name = "labelClarityTip";
            this.labelClarityTip.Size = new System.Drawing.Size(243, 60);
            this.labelClarityTip.TabIndex = 7;
            this.labelClarityTip.Text = "选择清晰度档位 A 时，其超高音频段要比选择清晰度档位 B 时更丰富。";
            // 
            // rdoUHDB
            // 
            this.rdoUHDB.backChecked = global::AEC.Properties.Resources.rdo_checked;
            this.rdoUHDB.BackColor = System.Drawing.Color.Transparent;
            this.rdoUHDB.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoUHDB.backHover = global::AEC.Properties.Resources.rdo_hover;
            this.rdoUHDB.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoUHDB.isAutoCheck = true;
            this.rdoUHDB.isChecked = false;
            this.rdoUHDB.isCheckInit = false;
            this.rdoUHDB.isShowCheckedBackWhenDsiabled = true;
            this.rdoUHDB.isStretch = false;
            this.rdoUHDB.Location = new System.Drawing.Point(162, 60);
            this.rdoUHDB.Name = "rdoUHDB";
            this.rdoUHDB.Size = new System.Drawing.Size(88, 30);
            this.rdoUHDB.TabIndex = 5;
            this.rdoUHDB.Text = "超高清B";
            this.rdoUHDB.userGDIPaint = true;
            this.rdoUHDB.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // rdoUHDA
            // 
            this.rdoUHDA.backChecked = global::AEC.Properties.Resources.rdo_checked;
            this.rdoUHDA.BackColor = System.Drawing.Color.Transparent;
            this.rdoUHDA.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoUHDA.backHover = global::AEC.Properties.Resources.rdo_hover;
            this.rdoUHDA.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoUHDA.isAutoCheck = true;
            this.rdoUHDA.isChecked = false;
            this.rdoUHDA.isCheckInit = false;
            this.rdoUHDA.isShowCheckedBackWhenDsiabled = true;
            this.rdoUHDA.isStretch = false;
            this.rdoUHDA.Location = new System.Drawing.Point(162, 24);
            this.rdoUHDA.Name = "rdoUHDA";
            this.rdoUHDA.Size = new System.Drawing.Size(88, 30);
            this.rdoUHDA.TabIndex = 6;
            this.rdoUHDA.Text = "超高清A";
            this.rdoUHDA.userGDIPaint = true;
            this.rdoUHDA.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // rdoHDB
            // 
            this.rdoHDB.backChecked = global::AEC.Properties.Resources.rdo_checked;
            this.rdoHDB.BackColor = System.Drawing.Color.Transparent;
            this.rdoHDB.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoHDB.backHover = global::AEC.Properties.Resources.rdo_hover;
            this.rdoHDB.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoHDB.isAutoCheck = true;
            this.rdoHDB.isChecked = false;
            this.rdoHDB.isCheckInit = false;
            this.rdoHDB.isShowCheckedBackWhenDsiabled = true;
            this.rdoHDB.isStretch = false;
            this.rdoHDB.Location = new System.Drawing.Point(85, 60);
            this.rdoHDB.Name = "rdoHDB";
            this.rdoHDB.Size = new System.Drawing.Size(72, 30);
            this.rdoHDB.TabIndex = 3;
            this.rdoHDB.Text = "高清B";
            this.rdoHDB.userGDIPaint = true;
            this.rdoHDB.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // rdoHDA
            // 
            this.rdoHDA.backChecked = global::AEC.Properties.Resources.rdo_checked;
            this.rdoHDA.BackColor = System.Drawing.Color.Transparent;
            this.rdoHDA.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoHDA.backHover = global::AEC.Properties.Resources.rdo_hover;
            this.rdoHDA.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoHDA.isAutoCheck = true;
            this.rdoHDA.isChecked = false;
            this.rdoHDA.isCheckInit = false;
            this.rdoHDA.isShowCheckedBackWhenDsiabled = true;
            this.rdoHDA.isStretch = false;
            this.rdoHDA.Location = new System.Drawing.Point(85, 24);
            this.rdoHDA.Name = "rdoHDA";
            this.rdoHDA.Size = new System.Drawing.Size(72, 30);
            this.rdoHDA.TabIndex = 4;
            this.rdoHDA.Text = "高清A";
            this.rdoHDA.userGDIPaint = true;
            this.rdoHDA.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // rdoSDB
            // 
            this.rdoSDB.backChecked = global::AEC.Properties.Resources.rdo_checked;
            this.rdoSDB.BackColor = System.Drawing.Color.Transparent;
            this.rdoSDB.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoSDB.backHover = global::AEC.Properties.Resources.rdo_hover;
            this.rdoSDB.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoSDB.isAutoCheck = true;
            this.rdoSDB.isChecked = false;
            this.rdoSDB.isCheckInit = false;
            this.rdoSDB.isShowCheckedBackWhenDsiabled = true;
            this.rdoSDB.isStretch = false;
            this.rdoSDB.Location = new System.Drawing.Point(8, 60);
            this.rdoSDB.Name = "rdoSDB";
            this.rdoSDB.Size = new System.Drawing.Size(72, 30);
            this.rdoSDB.TabIndex = 1;
            this.rdoSDB.Text = "标准B";
            this.rdoSDB.userGDIPaint = true;
            this.rdoSDB.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // rdoSDA
            // 
            this.rdoSDA.backChecked = global::AEC.Properties.Resources.rdo_checked;
            this.rdoSDA.BackColor = System.Drawing.Color.Transparent;
            this.rdoSDA.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoSDA.backHover = global::AEC.Properties.Resources.rdo_hover;
            this.rdoSDA.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoSDA.isAutoCheck = true;
            this.rdoSDA.isChecked = false;
            this.rdoSDA.isCheckInit = false;
            this.rdoSDA.isShowCheckedBackWhenDsiabled = true;
            this.rdoSDA.isStretch = false;
            this.rdoSDA.Location = new System.Drawing.Point(8, 24);
            this.rdoSDA.Name = "rdoSDA";
            this.rdoSDA.Size = new System.Drawing.Size(72, 30);
            this.rdoSDA.TabIndex = 2;
            this.rdoSDA.Text = "标准A";
            this.rdoSDA.userGDIPaint = true;
            this.rdoSDA.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // SystemPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelClarityControl);
            this.Controls.Add(this.panelDeviceWorkMode);
            this.Controls.Add(this.panelTimbreControl);
            this.Controls.Add(this.panelDataBackupMgt);
            this.Controls.Add(this.panelDevicePresets);
            this.Controls.Add(this.panelSystemUser);
            this.Controls.Add(this.panelKeyLock);
            this.Controls.Add(this.panelInfraredRemoteControlMgt);
            this.Controls.Add(this.panelDeviceInfo);
            this.Controls.Add(this.changePasswordPanelSystem);
            this.Controls.Add(this.changePasswordPanelKeyboardLock);
            this.Controls.Add(this.volMgtPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "SystemPanel";
            this.Size = new System.Drawing.Size(1276, 646);
            this.panelSystemUser.ResumeLayout(false);
            this.panelSystemUser.PerformLayout();
            this.panelKeyLock.ResumeLayout(false);
            this.panelInfraredRemoteControlMgt.ResumeLayout(false);
            this.panelDeviceInfo.ResumeLayout(false);
            this.panelDeviceInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLock)).EndInit();
            this.panelTimbreControl.ResumeLayout(false);
            this.panelDeviceWorkMode.ResumeLayout(false);
            this.panelClarityControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AEC.UI.VolMgtPanel volMgtPanel;
        private ChangePasswordPanel changePasswordPanelKeyboardLock;
        private ChangePasswordPanel changePasswordPanelSystem;
        private gtPanel panelDeviceInfo;
        private System.Windows.Forms.TextBox txtDeviceID;
        private gtPanel panelInfraredRemoteControlMgt;
        private gtPanel panelKeyLock;
        private gtRadioButtonWithText rdoEnableKeyLock;
        private gtRadioButtonWithText rdoDisableKeyLock;
        private gtPanel panelSystemUser;
        private gtRadioButtonWithText rdoNormalAdmin;
        private gtLabel lblSystemPassowrd;
        private System.Windows.Forms.TextBox txtSysPassowrd;
        private gtRadioButtonWithText rdoNormalUser;
        private gtRadioButtonWithText rdoDisableRemoteControl;
        private gtRadioButtonWithText rdoEnableRemoteControl;
        private AEC.UI.DeviceModePanel panelDevicePresets;
        private AEC.UI.DataBackupMgtPanel panelDataBackupMgt;
        private gtLabel lblDeviceID;
        private gtPanel panelTimbreControl;
        private gtRadioButtonWithText rdoClassicsTimbre;
        private gtRadioButtonWithText rdoProTimbre;
        private gtPanel panelDeviceWorkMode;
        private gtRadioButtonWithText rdoX5Mode;
        private gtRadioButtonWithText rdoStandardMode;
        private System.Windows.Forms.ToolTip toolTipDeviceFirmware;
        private Controls.MButton buttonOK;
        private System.Windows.Forms.PictureBox picLock;
        private gtPanel panelClarityControl;
        private gtRadioButtonWithText rdoUHDB;
        private gtRadioButtonWithText rdoUHDA;
        private gtRadioButtonWithText rdoHDB;
        private gtRadioButtonWithText rdoHDA;
        private gtRadioButtonWithText rdoSDB;
        private gtRadioButtonWithText rdoSDA;
        private System.Windows.Forms.Label labelClarityTip;
    }
}
