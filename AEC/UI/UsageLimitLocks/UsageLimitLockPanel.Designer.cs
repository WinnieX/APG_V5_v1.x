namespace AEC.UI.UsageLimitLocks
{
    partial class UsageLimitLockPanel
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblIndex = new AEC.UI.Controls.MLabel();
            this.picLock = new System.Windows.Forms.PictureBox();
            this.lblError = new AEC.UI.Controls.MLabel();
            this.lblUseCurrPassword = new AEC.UI.Controls.MLabel();
            this.chkApplyCurrPassword = new AEC.UI.Controls.MSwitch();
            this.btnSetting = new AEC.UI.Controls.MButton();
            this.btnOK = new AEC.UI.Controls.MButton();
            this.cmbUsage = new AEC.UI.gtComboBox();
            this.lblUsage = new AEC.UI.Controls.MLabel();
            this.lblConfirmNewPassword = new AEC.UI.Controls.MLabel();
            this.txtConfirmNewPassword = new System.Windows.Forms.TextBox();
            this.lblNewPassword = new AEC.UI.Controls.MLabel();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.lblState = new AEC.UI.Controls.MLabel();
            this.chkState = new AEC.UI.Controls.MSwitch();
            this.splitLine1 = new AEC.UI.SplitLine();
            this.txtRemainderUsage = new System.Windows.Forms.TextBox();
            this.lblRemainderUsage = new AEC.UI.Controls.MLabel();
            this.lblCurrState = new AEC.UI.Controls.MLabel();
            this.lblCurrPassword = new AEC.UI.Controls.MLabel();
            this.chkCurrState = new AEC.UI.Controls.MSwitch();
            this.txtCurrPassword = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLock)).BeginInit();
            this.SuspendLayout();
            // 
            // lblIndex
            // 
            this.lblIndex.Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblIndex.BackColor = System.Drawing.Color.Transparent;
            this.lblIndex.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblIndex.BorderWidth = 1F;
            this.lblIndex.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblIndex.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblIndex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(120)))), ((int)(((byte)(253)))));
            this.lblIndex.Location = new System.Drawing.Point(8, 29);
            this.lblIndex.Name = "lblIndex";
            this.lblIndex.Radius = 16;
            this.lblIndex.ShowBackground = false;
            this.lblIndex.ShowBorder = false;
            this.lblIndex.Size = new System.Drawing.Size(36, 56);
            this.lblIndex.TabIndex = 0;
            this.lblIndex.Text = "X";
            // 
            // picLock
            // 
            this.picLock.Image = global::AEC.Properties.Resources.usage_limit_lock_enable;
            this.picLock.Location = new System.Drawing.Point(231, 72);
            this.picLock.Name = "picLock";
            this.picLock.Size = new System.Drawing.Size(24, 24);
            this.picLock.TabIndex = 55;
            this.picLock.TabStop = false;
            this.picLock.Visible = false;
            // 
            // lblError
            // 
            this.lblError.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblError.BackColor = System.Drawing.Color.Transparent;
            this.lblError.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblError.BorderWidth = 1F;
            this.lblError.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(289, 97);
            this.lblError.Name = "lblError";
            this.lblError.Radius = 16;
            this.lblError.ShowBackground = false;
            this.lblError.ShowBorder = false;
            this.lblError.Size = new System.Drawing.Size(350, 23);
            this.lblError.TabIndex = 54;
            this.lblError.Text = "……";
            this.lblError.Visible = false;
            // 
            // lblUseCurrPassword
            // 
            this.lblUseCurrPassword.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblUseCurrPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblUseCurrPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblUseCurrPassword.BorderWidth = 1F;
            this.lblUseCurrPassword.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblUseCurrPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblUseCurrPassword.Location = new System.Drawing.Point(278, 73);
            this.lblUseCurrPassword.Name = "lblUseCurrPassword";
            this.lblUseCurrPassword.Radius = 16;
            this.lblUseCurrPassword.ShowBackground = false;
            this.lblUseCurrPassword.ShowBorder = false;
            this.lblUseCurrPassword.Size = new System.Drawing.Size(124, 23);
            this.lblUseCurrPassword.TabIndex = 53;
            this.lblUseCurrPassword.Text = "使用当前密码：";
            // 
            // chkApplyCurrPassword
            // 
            this.chkApplyCurrPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkApplyCurrPassword.BorderWidth = 1F;
            this.chkApplyCurrPassword.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkApplyCurrPassword.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.chkApplyCurrPassword.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkApplyCurrPassword.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkApplyCurrPassword.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.chkApplyCurrPassword.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkApplyCurrPassword.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkApplyCurrPassword.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.chkApplyCurrPassword.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkApplyCurrPassword.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkApplyCurrPassword.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.chkApplyCurrPassword.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkApplyCurrPassword.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkApplyCurrPassword.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkApplyCurrPassword.isAutoCheck = true;
            this.chkApplyCurrPassword.IsChecked = false;
            this.chkApplyCurrPassword.IsCheckedInit = false;
            this.chkApplyCurrPassword.Location = new System.Drawing.Point(402, 73);
            this.chkApplyCurrPassword.Name = "chkApplyCurrPassword";
            this.chkApplyCurrPassword.Radius = 22;
            this.chkApplyCurrPassword.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkApplyCurrPassword.ShowBorder = false;
            this.chkApplyCurrPassword.ShowText = true;
            this.chkApplyCurrPassword.Size = new System.Drawing.Size(40, 22);
            this.chkApplyCurrPassword.SwitchWdith = 40;
            this.chkApplyCurrPassword.TabIndex = 52;
            this.chkApplyCurrPassword.UncheckedDisbaledBorderColor = System.Drawing.Color.Gray;
            this.chkApplyCurrPassword.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.chkApplyCurrPassword.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkApplyCurrPassword.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkApplyCurrPassword.UncheckedHoverFillColor = System.Drawing.Color.Silver;
            this.chkApplyCurrPassword.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkApplyCurrPassword.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkApplyCurrPassword.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkApplyCurrPassword.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkApplyCurrPassword.UncheckedPressedBorderColor = System.Drawing.Color.Gray;
            this.chkApplyCurrPassword.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.chkApplyCurrPassword.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.chkApplyCurrPassword.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // btnSetting
            // 
            this.btnSetting.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnSetting.BorderWidth = 1F;
            this.btnSetting.DisabledBorderColor = System.Drawing.Color.Gray;
            this.btnSetting.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnSetting.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSetting.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnSetting.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSetting.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnSetting.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSetting.Location = new System.Drawing.Point(551, 73);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnSetting.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(120)))), ((int)(((byte)(253)))));
            this.btnSetting.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSetting.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.btnSetting.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnSetting.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnSetting.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnSetting.Radius = 12;
            this.btnSetting.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.btnSetting.ShowBorder = false;
            this.btnSetting.ShowText = true;
            this.btnSetting.Size = new System.Drawing.Size(64, 23);
            this.btnSetting.TabIndex = 51;
            this.btnSetting.Text = "设置";
            this.btnSetting.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnOK
            // 
            this.btnOK.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnOK.BorderWidth = 1F;
            this.btnOK.DisabledBorderColor = System.Drawing.Color.Gray;
            this.btnOK.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnOK.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnOK.Enabled = false;
            this.btnOK.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnOK.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnOK.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnOK.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnOK.Location = new System.Drawing.Point(220, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnOK.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(120)))), ((int)(((byte)(253)))));
            this.btnOK.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOK.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.btnOK.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnOK.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnOK.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnOK.Radius = 12;
            this.btnOK.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.btnOK.ShowBorder = false;
            this.btnOK.ShowText = true;
            this.btnOK.Size = new System.Drawing.Size(46, 23);
            this.btnOK.TabIndex = 50;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btn_Click);
            // 
            // cmbUsage
            // 
            this.cmbUsage.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbUsage.BackColor = System.Drawing.SystemColors.Window;
            this.cmbUsage.DropDownBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(120)))), ((int)(((byte)(253)))));
            this.cmbUsage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbUsage.ForeColor = System.Drawing.Color.Black;
            this.cmbUsage.FormattingEnabled = true;
            this.cmbUsage.Items.AddRange(new object[] {
            "0",
            "10",
            "20",
            "30",
            "40",
            "50",
            "60",
            "80",
            "100",
            "120",
            "150",
            "180",
            "200",
            "240",
            "300",
            "500",
            "600",
            "900",
            "1000"});
            this.cmbUsage.Location = new System.Drawing.Point(551, 42);
            this.cmbUsage.Margin = new System.Windows.Forms.Padding(0);
            this.cmbUsage.Name = "cmbUsage";
            this.cmbUsage.Size = new System.Drawing.Size(80, 23);
            this.cmbUsage.TabIndex = 49;
            this.cmbUsage.TabStop = false;
            this.cmbUsage.TextChanged += new System.EventHandler(this.cmbUsage_TextChanged);
            // 
            // lblUsage
            // 
            this.lblUsage.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblUsage.BackColor = System.Drawing.Color.Transparent;
            this.lblUsage.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblUsage.BorderWidth = 1F;
            this.lblUsage.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblUsage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblUsage.Location = new System.Drawing.Point(476, 42);
            this.lblUsage.Name = "lblUsage";
            this.lblUsage.Radius = 16;
            this.lblUsage.ShowBackground = false;
            this.lblUsage.ShowBorder = false;
            this.lblUsage.Size = new System.Drawing.Size(75, 23);
            this.lblUsage.TabIndex = 48;
            this.lblUsage.Text = "使用次数：";
            // 
            // lblConfirmNewPassword
            // 
            this.lblConfirmNewPassword.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblConfirmNewPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblConfirmNewPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblConfirmNewPassword.BorderWidth = 1F;
            this.lblConfirmNewPassword.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblConfirmNewPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblConfirmNewPassword.Location = new System.Drawing.Point(278, 42);
            this.lblConfirmNewPassword.Name = "lblConfirmNewPassword";
            this.lblConfirmNewPassword.Radius = 16;
            this.lblConfirmNewPassword.ShowBackground = false;
            this.lblConfirmNewPassword.ShowBorder = false;
            this.lblConfirmNewPassword.Size = new System.Drawing.Size(124, 23);
            this.lblConfirmNewPassword.TabIndex = 47;
            this.lblConfirmNewPassword.Text = "确认新密码：";
            // 
            // txtConfirmNewPassword
            // 
            this.txtConfirmNewPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtConfirmNewPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfirmNewPassword.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtConfirmNewPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtConfirmNewPassword.Location = new System.Drawing.Point(402, 40);
            this.txtConfirmNewPassword.MaxLength = 6;
            this.txtConfirmNewPassword.Name = "txtConfirmNewPassword";
            this.txtConfirmNewPassword.PasswordChar = '●';
            this.txtConfirmNewPassword.ShortcutsEnabled = false;
            this.txtConfirmNewPassword.Size = new System.Drawing.Size(64, 27);
            this.txtConfirmNewPassword.TabIndex = 46;
            this.txtConfirmNewPassword.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtConfirmNewPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            this.txtConfirmNewPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblNewPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblNewPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblNewPassword.BorderWidth = 1F;
            this.lblNewPassword.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblNewPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblNewPassword.Location = new System.Drawing.Point(278, 12);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Radius = 16;
            this.lblNewPassword.ShowBackground = false;
            this.lblNewPassword.ShowBorder = false;
            this.lblNewPassword.Size = new System.Drawing.Size(124, 23);
            this.lblNewPassword.TabIndex = 45;
            this.lblNewPassword.Text = "新密码：";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtNewPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewPassword.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtNewPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtNewPassword.Location = new System.Drawing.Point(402, 10);
            this.txtNewPassword.MaxLength = 6;
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '●';
            this.txtNewPassword.ShortcutsEnabled = false;
            this.txtNewPassword.Size = new System.Drawing.Size(64, 27);
            this.txtNewPassword.TabIndex = 44;
            this.txtNewPassword.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtNewPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            this.txtNewPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // lblState
            // 
            this.lblState.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblState.BackColor = System.Drawing.Color.Transparent;
            this.lblState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblState.BorderWidth = 1F;
            this.lblState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblState.Location = new System.Drawing.Point(476, 12);
            this.lblState.Name = "lblState";
            this.lblState.Radius = 16;
            this.lblState.ShowBackground = false;
            this.lblState.ShowBorder = false;
            this.lblState.Size = new System.Drawing.Size(75, 23);
            this.lblState.TabIndex = 43;
            this.lblState.Text = "状态：";
            // 
            // chkState
            // 
            this.chkState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkState.BorderWidth = 1F;
            this.chkState.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkState.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.chkState.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkState.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkState.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.chkState.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkState.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkState.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.chkState.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkState.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkState.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.chkState.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkState.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkState.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkState.isAutoCheck = true;
            this.chkState.IsChecked = false;
            this.chkState.IsCheckedInit = false;
            this.chkState.Location = new System.Drawing.Point(551, 12);
            this.chkState.Name = "chkState";
            this.chkState.Radius = 22;
            this.chkState.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkState.ShowBorder = false;
            this.chkState.ShowText = true;
            this.chkState.Size = new System.Drawing.Size(40, 22);
            this.chkState.SwitchWdith = 40;
            this.chkState.TabIndex = 42;
            this.chkState.UncheckedDisbaledBorderColor = System.Drawing.Color.Gray;
            this.chkState.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.chkState.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkState.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkState.UncheckedHoverFillColor = System.Drawing.Color.Silver;
            this.chkState.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkState.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkState.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkState.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkState.UncheckedPressedBorderColor = System.Drawing.Color.Gray;
            this.chkState.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.chkState.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            // 
            // splitLine1
            // 
            this.splitLine1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.splitLine1.ForeColor = System.Drawing.Color.Silver;
            this.splitLine1.Location = new System.Drawing.Point(274, 8);
            this.splitLine1.Margin = new System.Windows.Forms.Padding(0);
            this.splitLine1.Name = "splitLine1";
            this.splitLine1.Size = new System.Drawing.Size(1, 108);
            this.splitLine1.TabIndex = 41;
            this.splitLine1.Text = "splitLine1";
            // 
            // txtRemainderUsage
            // 
            this.txtRemainderUsage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtRemainderUsage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRemainderUsage.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtRemainderUsage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.txtRemainderUsage.Location = new System.Drawing.Point(150, 73);
            this.txtRemainderUsage.MaxLength = 4;
            this.txtRemainderUsage.Name = "txtRemainderUsage";
            this.txtRemainderUsage.ReadOnly = true;
            this.txtRemainderUsage.ShortcutsEnabled = false;
            this.txtRemainderUsage.Size = new System.Drawing.Size(64, 20);
            this.txtRemainderUsage.TabIndex = 40;
            this.txtRemainderUsage.Visible = false;
            // 
            // lblRemainderUsage
            // 
            this.lblRemainderUsage.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRemainderUsage.BackColor = System.Drawing.Color.Transparent;
            this.lblRemainderUsage.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblRemainderUsage.BorderWidth = 1F;
            this.lblRemainderUsage.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblRemainderUsage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblRemainderUsage.Location = new System.Drawing.Point(42, 72);
            this.lblRemainderUsage.Name = "lblRemainderUsage";
            this.lblRemainderUsage.Radius = 16;
            this.lblRemainderUsage.ShowBackground = false;
            this.lblRemainderUsage.ShowBorder = false;
            this.lblRemainderUsage.Size = new System.Drawing.Size(108, 23);
            this.lblRemainderUsage.TabIndex = 39;
            this.lblRemainderUsage.Text = "剩余使用次数：";
            this.lblRemainderUsage.Visible = false;
            // 
            // lblCurrState
            // 
            this.lblCurrState.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCurrState.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblCurrState.BorderWidth = 1F;
            this.lblCurrState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblCurrState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCurrState.Location = new System.Drawing.Point(42, 42);
            this.lblCurrState.Name = "lblCurrState";
            this.lblCurrState.Radius = 16;
            this.lblCurrState.ShowBackground = false;
            this.lblCurrState.ShowBorder = false;
            this.lblCurrState.Size = new System.Drawing.Size(108, 23);
            this.lblCurrState.TabIndex = 38;
            this.lblCurrState.Text = "当前状态：";
            this.lblCurrState.Visible = false;
            // 
            // lblCurrPassword
            // 
            this.lblCurrPassword.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblCurrPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblCurrPassword.BorderWidth = 1F;
            this.lblCurrPassword.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblCurrPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCurrPassword.Location = new System.Drawing.Point(42, 12);
            this.lblCurrPassword.Name = "lblCurrPassword";
            this.lblCurrPassword.Radius = 16;
            this.lblCurrPassword.ShowBackground = false;
            this.lblCurrPassword.ShowBorder = false;
            this.lblCurrPassword.Size = new System.Drawing.Size(108, 23);
            this.lblCurrPassword.TabIndex = 37;
            this.lblCurrPassword.Text = "当前密码：";
            // 
            // chkCurrState
            // 
            this.chkCurrState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkCurrState.BorderWidth = 1F;
            this.chkCurrState.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkCurrState.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.chkCurrState.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkCurrState.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkCurrState.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.chkCurrState.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkCurrState.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkCurrState.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.chkCurrState.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkCurrState.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkCurrState.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.chkCurrState.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkCurrState.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkCurrState.Enabled = false;
            this.chkCurrState.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkCurrState.isAutoCheck = true;
            this.chkCurrState.IsChecked = false;
            this.chkCurrState.IsCheckedInit = false;
            this.chkCurrState.Location = new System.Drawing.Point(150, 42);
            this.chkCurrState.Name = "chkCurrState";
            this.chkCurrState.Radius = 22;
            this.chkCurrState.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkCurrState.ShowBorder = false;
            this.chkCurrState.ShowText = true;
            this.chkCurrState.Size = new System.Drawing.Size(40, 22);
            this.chkCurrState.SwitchWdith = 40;
            this.chkCurrState.TabIndex = 30;
            this.chkCurrState.UncheckedDisbaledBorderColor = System.Drawing.Color.Gray;
            this.chkCurrState.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.chkCurrState.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkCurrState.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkCurrState.UncheckedHoverFillColor = System.Drawing.Color.Silver;
            this.chkCurrState.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkCurrState.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkCurrState.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkCurrState.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkCurrState.UncheckedPressedBorderColor = System.Drawing.Color.Gray;
            this.chkCurrState.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.chkCurrState.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.chkCurrState.Visible = false;
            // 
            // txtCurrPassword
            // 
            this.txtCurrPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtCurrPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrPassword.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtCurrPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtCurrPassword.Location = new System.Drawing.Point(150, 10);
            this.txtCurrPassword.MaxLength = 6;
            this.txtCurrPassword.Name = "txtCurrPassword";
            this.txtCurrPassword.PasswordChar = '●';
            this.txtCurrPassword.ShortcutsEnabled = false;
            this.txtCurrPassword.Size = new System.Drawing.Size(64, 27);
            this.txtCurrPassword.TabIndex = 11;
            this.txtCurrPassword.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtCurrPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            this.txtCurrPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // UsageLimitLockPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.Controls.Add(this.lblIndex);
            this.Controls.Add(this.picLock);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.lblUseCurrPassword);
            this.Controls.Add(this.chkApplyCurrPassword);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cmbUsage);
            this.Controls.Add(this.lblUsage);
            this.Controls.Add(this.lblConfirmNewPassword);
            this.Controls.Add(this.txtConfirmNewPassword);
            this.Controls.Add(this.lblNewPassword);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.chkState);
            this.Controls.Add(this.splitLine1);
            this.Controls.Add(this.txtRemainderUsage);
            this.Controls.Add(this.lblRemainderUsage);
            this.Controls.Add(this.lblCurrState);
            this.Controls.Add(this.lblCurrPassword);
            this.Controls.Add(this.chkCurrState);
            this.Controls.Add(this.txtCurrPassword);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "UsageLimitLockPanel";
            this.Size = new System.Drawing.Size(644, 124);
            ((System.ComponentModel.ISupportInitialize)(this.picLock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MLabel lblIndex;
        private System.Windows.Forms.TextBox txtCurrPassword;
        private Controls.MSwitch chkCurrState;
        private Controls.MLabel lblCurrPassword;
        private Controls.MLabel lblCurrState;
        private Controls.MLabel lblRemainderUsage;
        private System.Windows.Forms.TextBox txtRemainderUsage;
        private SplitLine splitLine1;
        private Controls.MLabel lblState;
        private Controls.MSwitch chkState;
        private Controls.MLabel lblNewPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private Controls.MLabel lblConfirmNewPassword;
        private System.Windows.Forms.TextBox txtConfirmNewPassword;
        private Controls.MLabel lblUsage;
        private gtComboBox cmbUsage;
        private Controls.MButton btnOK;
        private Controls.MButton btnSetting;
        private Controls.MSwitch chkApplyCurrPassword;
        private Controls.MLabel lblUseCurrPassword;
        private Controls.MLabel lblError;
        private System.Windows.Forms.PictureBox picLock;
    }
}
