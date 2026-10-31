namespace AEC.UI.UsageLimitLocks
{
    partial class FormUsageLimitLockManagement
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUsageLimitLockManagement));
            this.lblDeviceName = new AEC.UI.Controls.MLabel();
            this.txtDeviceName = new AEC.UI.Controls.MEditableLabel();
            this.txtDeviceID = new System.Windows.Forms.TextBox();
            this.panelUsageLimitLock1 = new AEC.UI.UsageLimitLocks.UsageLimitLockPanel();
            this.panelUsageLimitLock2 = new AEC.UI.UsageLimitLocks.UsageLimitLockPanel();
            this.panelUsageLimitLock3 = new AEC.UI.UsageLimitLocks.UsageLimitLockPanel();
            this.panelUsageLimitLock4 = new AEC.UI.UsageLimitLocks.UsageLimitLockPanel();
            this.panelUsageLimitLock5 = new AEC.UI.UsageLimitLocks.UsageLimitLockPanel();
            this.lblDeviceID = new AEC.UI.Controls.MLabel();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClear = new System.Windows.Forms.ToolStripMenuItem();
            this.chkShowPassword = new AEC.UI.Controls.MImageCheckBox();
            this.lblRandKey = new AEC.UI.Controls.MLabel();
            this.txtRandKey = new System.Windows.Forms.TextBox();
            this.btnPasswordManagement = new AEC.UI.Controls.MButton();
            this.splitLine1 = new AEC.UI.SplitLine();
            this.btnLockManagement = new AEC.UI.Controls.MButton();
            this.btnSetting = new AEC.UI.Controls.MButton();
            this.txtTaskFilePath = new System.Windows.Forms.TextBox();
            this.lblTaskFile = new AEC.UI.Controls.MLabel();
            this.btnForgotPassword = new AEC.UI.Controls.MButton();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.Size = new System.Drawing.Size(1280, 32);
            // 
            // lblDeviceName
            // 
            this.lblDeviceName.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDeviceName.BackColor = System.Drawing.Color.Transparent;
            this.lblDeviceName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblDeviceName.BorderWidth = 1F;
            this.lblDeviceName.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblDeviceName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblDeviceName.Location = new System.Drawing.Point(889, 42);
            this.lblDeviceName.Name = "lblDeviceName";
            this.lblDeviceName.Radius = 16;
            this.lblDeviceName.ShowBackground = false;
            this.lblDeviceName.ShowBorder = false;
            this.lblDeviceName.Size = new System.Drawing.Size(75, 23);
            this.lblDeviceName.TabIndex = 1;
            this.lblDeviceName.Text = "设备名称：";
            // 
            // txtDeviceName
            // 
            this.txtDeviceName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtDeviceName.borderColor = System.Drawing.Color.Gray;
            this.txtDeviceName.canEditable = true;
            this.txtDeviceName.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtDeviceName.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtDeviceName.ForeColor = System.Drawing.Color.White;
            this.txtDeviceName.Location = new System.Drawing.Point(967, 40);
            this.txtDeviceName.MaxLength = 32;
            this.txtDeviceName.Name = "txtDeviceName";
            this.txtDeviceName.ShowBorder = true;
            this.txtDeviceName.Size = new System.Drawing.Size(301, 28);
            this.txtDeviceName.TabIndex = 2;
            this.txtDeviceName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDeviceName.TextChanged += new System.EventHandler(this.txtDeviceName_TextChanged);
            // 
            // txtDeviceID
            // 
            this.txtDeviceID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.txtDeviceID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeviceID.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtDeviceID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtDeviceID.Location = new System.Drawing.Point(802, 40);
            this.txtDeviceID.Name = "txtDeviceID";
            this.txtDeviceID.ReadOnly = true;
            this.txtDeviceID.Size = new System.Drawing.Size(80, 27);
            this.txtDeviceID.TabIndex = 9;
            // 
            // panelUsageLimitLock1
            // 
            this.panelUsageLimitLock1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.panelUsageLimitLock1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelUsageLimitLock1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelUsageLimitLock1.ForeColor = System.Drawing.Color.LightGray;
            this.panelUsageLimitLock1.Location = new System.Drawing.Point(12, 40);
            this.panelUsageLimitLock1.Name = "panelUsageLimitLock1";
            this.panelUsageLimitLock1.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelUsageLimitLock1.passwordVisible = false;
            this.panelUsageLimitLock1.Radius = 18;
            this.panelUsageLimitLock1.randKey = ((uint)(0u));
            this.panelUsageLimitLock1.RectColor = System.Drawing.Color.Gray;
            this.panelUsageLimitLock1.ShowRect = true;
            this.panelUsageLimitLock1.Size = new System.Drawing.Size(644, 124);
            this.panelUsageLimitLock1.TabIndex = 11;
            this.panelUsageLimitLock1.title = null;
            this.panelUsageLimitLock1.titleHeight = 20;
            // 
            // panelUsageLimitLock2
            // 
            this.panelUsageLimitLock2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.panelUsageLimitLock2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelUsageLimitLock2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelUsageLimitLock2.ForeColor = System.Drawing.Color.LightGray;
            this.panelUsageLimitLock2.Location = new System.Drawing.Point(12, 170);
            this.panelUsageLimitLock2.Name = "panelUsageLimitLock2";
            this.panelUsageLimitLock2.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelUsageLimitLock2.passwordVisible = false;
            this.panelUsageLimitLock2.Radius = 18;
            this.panelUsageLimitLock2.randKey = ((uint)(0u));
            this.panelUsageLimitLock2.RectColor = System.Drawing.Color.Gray;
            this.panelUsageLimitLock2.ShowRect = true;
            this.panelUsageLimitLock2.Size = new System.Drawing.Size(644, 124);
            this.panelUsageLimitLock2.TabIndex = 12;
            this.panelUsageLimitLock2.title = null;
            this.panelUsageLimitLock2.titleHeight = 20;
            // 
            // panelUsageLimitLock3
            // 
            this.panelUsageLimitLock3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.panelUsageLimitLock3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelUsageLimitLock3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelUsageLimitLock3.ForeColor = System.Drawing.Color.LightGray;
            this.panelUsageLimitLock3.Location = new System.Drawing.Point(12, 300);
            this.panelUsageLimitLock3.Name = "panelUsageLimitLock3";
            this.panelUsageLimitLock3.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelUsageLimitLock3.passwordVisible = false;
            this.panelUsageLimitLock3.Radius = 18;
            this.panelUsageLimitLock3.randKey = ((uint)(0u));
            this.panelUsageLimitLock3.RectColor = System.Drawing.Color.Gray;
            this.panelUsageLimitLock3.ShowRect = true;
            this.panelUsageLimitLock3.Size = new System.Drawing.Size(644, 124);
            this.panelUsageLimitLock3.TabIndex = 13;
            this.panelUsageLimitLock3.title = null;
            this.panelUsageLimitLock3.titleHeight = 20;
            // 
            // panelUsageLimitLock4
            // 
            this.panelUsageLimitLock4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.panelUsageLimitLock4.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelUsageLimitLock4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelUsageLimitLock4.ForeColor = System.Drawing.Color.LightGray;
            this.panelUsageLimitLock4.Location = new System.Drawing.Point(12, 430);
            this.panelUsageLimitLock4.Name = "panelUsageLimitLock4";
            this.panelUsageLimitLock4.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelUsageLimitLock4.passwordVisible = false;
            this.panelUsageLimitLock4.Radius = 18;
            this.panelUsageLimitLock4.randKey = ((uint)(0u));
            this.panelUsageLimitLock4.RectColor = System.Drawing.Color.Gray;
            this.panelUsageLimitLock4.ShowRect = true;
            this.panelUsageLimitLock4.Size = new System.Drawing.Size(644, 124);
            this.panelUsageLimitLock4.TabIndex = 14;
            this.panelUsageLimitLock4.title = null;
            this.panelUsageLimitLock4.titleHeight = 20;
            // 
            // panelUsageLimitLock5
            // 
            this.panelUsageLimitLock5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.panelUsageLimitLock5.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelUsageLimitLock5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelUsageLimitLock5.ForeColor = System.Drawing.Color.LightGray;
            this.panelUsageLimitLock5.Location = new System.Drawing.Point(12, 560);
            this.panelUsageLimitLock5.Name = "panelUsageLimitLock5";
            this.panelUsageLimitLock5.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelUsageLimitLock5.passwordVisible = false;
            this.panelUsageLimitLock5.Radius = 18;
            this.panelUsageLimitLock5.randKey = ((uint)(0u));
            this.panelUsageLimitLock5.RectColor = System.Drawing.Color.Gray;
            this.panelUsageLimitLock5.ShowRect = true;
            this.panelUsageLimitLock5.Size = new System.Drawing.Size(644, 124);
            this.panelUsageLimitLock5.TabIndex = 15;
            this.panelUsageLimitLock5.title = null;
            this.panelUsageLimitLock5.titleHeight = 20;
            // 
            // lblDeviceID
            // 
            this.lblDeviceID.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDeviceID.BackColor = System.Drawing.Color.Transparent;
            this.lblDeviceID.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblDeviceID.BorderWidth = 1F;
            this.lblDeviceID.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblDeviceID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblDeviceID.Location = new System.Drawing.Point(711, 42);
            this.lblDeviceID.Name = "lblDeviceID";
            this.lblDeviceID.Radius = 16;
            this.lblDeviceID.ShowBackground = false;
            this.lblDeviceID.ShowBorder = false;
            this.lblDeviceID.Size = new System.Drawing.Size(85, 23);
            this.lblDeviceID.TabIndex = 16;
            this.lblDeviceID.Text = "设备 ID：";
            // 
            // txtOutput
            // 
            this.txtOutput.AllowDrop = true;
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOutput.ContextMenuStrip = this.contextMenuStrip;
            this.txtOutput.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtOutput.Location = new System.Drawing.Point(664, 162);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.ShortcutsEnabled = false;
            this.txtOutput.Size = new System.Drawing.Size(604, 522);
            this.txtOutput.TabIndex = 42;
            this.txtOutput.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.txtOutput.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopy,
            this.tsmiClear});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(101, 48);
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Size = new System.Drawing.Size(100, 22);
            this.tsmiCopy.Text = "复制";
            this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // tsmiClear
            // 
            this.tsmiClear.Name = "tsmiClear";
            this.tsmiClear.Size = new System.Drawing.Size(100, 22);
            this.tsmiClear.Text = "清除";
            this.tsmiClear.Click += new System.EventHandler(this.tsmiClear_Click);
            // 
            // chkShowPassword
            // 
            this.chkShowPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowPassword.backCheckedDisabled = global::AEC.Properties.Resources.btn_visible_disabled;
            this.chkShowPassword.backCheckedHover = global::AEC.Properties.Resources.btn_visible_hover;
            this.chkShowPassword.backCheckedNormal = global::AEC.Properties.Resources.btn_visible_normal;
            this.chkShowPassword.backCheckedPressed = global::AEC.Properties.Resources.btn_visible_pressed;
            this.chkShowPassword.BackColor = System.Drawing.Color.Transparent;
            this.chkShowPassword.backUncheckedDisabled = global::AEC.Properties.Resources.btn_unvisible_disabled;
            this.chkShowPassword.backUncheckedHover = global::AEC.Properties.Resources.btn_unvisible_hover;
            this.chkShowPassword.backUncheckedNormal = global::AEC.Properties.Resources.btn_unvisible_normal;
            this.chkShowPassword.backUncheckedPressed = global::AEC.Properties.Resources.btn_unvisible_pressed;
            this.chkShowPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkShowPassword.BorderWidth = 1F;
            this.chkShowPassword.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkShowPassword.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.chkShowPassword.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkShowPassword.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkShowPassword.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.chkShowPassword.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkShowPassword.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkShowPassword.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.chkShowPassword.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkShowPassword.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkShowPassword.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.chkShowPassword.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkShowPassword.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkShowPassword.ImageRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.chkShowPassword.isAutoCheck = true;
            this.chkShowPassword.IsChecked = false;
            this.chkShowPassword.IsCheckedInit = false;
            this.chkShowPassword.Location = new System.Drawing.Point(664, 39);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Radius = 12;
            this.chkShowPassword.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkShowPassword.showBackFillColor = false;
            this.chkShowPassword.ShowBorder = false;
            this.chkShowPassword.ShowText = true;
            this.chkShowPassword.Size = new System.Drawing.Size(28, 28);
            this.chkShowPassword.TabIndex = 43;
            this.chkShowPassword.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkShowPassword.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkShowPassword.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkShowPassword.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkShowPassword.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkShowPassword.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkShowPassword.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkShowPassword.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.chkShowPassword.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkShowPassword.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkShowPassword.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.chkShowPassword.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);
            // 
            // lblRandKey
            // 
            this.lblRandKey.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRandKey.BackColor = System.Drawing.Color.Transparent;
            this.lblRandKey.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblRandKey.BorderWidth = 1F;
            this.lblRandKey.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblRandKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblRandKey.Location = new System.Drawing.Point(711, 75);
            this.lblRandKey.Name = "lblRandKey";
            this.lblRandKey.Radius = 16;
            this.lblRandKey.ShowBackground = false;
            this.lblRandKey.ShowBorder = false;
            this.lblRandKey.Size = new System.Drawing.Size(85, 23);
            this.lblRandKey.TabIndex = 44;
            this.lblRandKey.Text = "随机密钥：";
            // 
            // txtRandKey
            // 
            this.txtRandKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.txtRandKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRandKey.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtRandKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtRandKey.Location = new System.Drawing.Point(802, 73);
            this.txtRandKey.Name = "txtRandKey";
            this.txtRandKey.ReadOnly = true;
            this.txtRandKey.Size = new System.Drawing.Size(80, 27);
            this.txtRandKey.TabIndex = 45;
            // 
            // btnPasswordManagement
            // 
            this.btnPasswordManagement.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnPasswordManagement.BorderWidth = 1F;
            this.btnPasswordManagement.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnPasswordManagement.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnPasswordManagement.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnPasswordManagement.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnPasswordManagement.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnPasswordManagement.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnPasswordManagement.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnPasswordManagement.Location = new System.Drawing.Point(967, 75);
            this.btnPasswordManagement.Name = "btnPasswordManagement";
            this.btnPasswordManagement.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnPasswordManagement.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(120)))), ((int)(((byte)(253)))));
            this.btnPasswordManagement.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPasswordManagement.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.btnPasswordManagement.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnPasswordManagement.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnPasswordManagement.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnPasswordManagement.Radius = 12;
            this.btnPasswordManagement.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.btnPasswordManagement.ShowBorder = false;
            this.btnPasswordManagement.ShowText = true;
            this.btnPasswordManagement.Size = new System.Drawing.Size(96, 27);
            this.btnPasswordManagement.TabIndex = 61;
            this.btnPasswordManagement.Text = "密码管理";
            this.btnPasswordManagement.Click += new System.EventHandler(this.btn_Click);
            // 
            // splitLine1
            // 
            this.splitLine1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.splitLine1.ForeColor = System.Drawing.Color.Silver;
            this.splitLine1.Location = new System.Drawing.Point(664, 109);
            this.splitLine1.Margin = new System.Windows.Forms.Padding(0);
            this.splitLine1.Name = "splitLine1";
            this.splitLine1.Size = new System.Drawing.Size(604, 1);
            this.splitLine1.TabIndex = 62;
            this.splitLine1.Text = "splitLine1";
            // 
            // btnLockManagement
            // 
            this.btnLockManagement.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnLockManagement.BorderWidth = 1F;
            this.btnLockManagement.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnLockManagement.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnLockManagement.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnLockManagement.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnLockManagement.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnLockManagement.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnLockManagement.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnLockManagement.Location = new System.Drawing.Point(1069, 75);
            this.btnLockManagement.Name = "btnLockManagement";
            this.btnLockManagement.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnLockManagement.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(120)))), ((int)(((byte)(253)))));
            this.btnLockManagement.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLockManagement.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.btnLockManagement.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnLockManagement.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnLockManagement.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnLockManagement.Radius = 12;
            this.btnLockManagement.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.btnLockManagement.ShowBorder = false;
            this.btnLockManagement.ShowText = true;
            this.btnLockManagement.Size = new System.Drawing.Size(96, 27);
            this.btnLockManagement.TabIndex = 63;
            this.btnLockManagement.Text = "锁任务管理";
            this.btnLockManagement.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnSetting
            // 
            this.btnSetting.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnSetting.BorderWidth = 1F;
            this.btnSetting.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnSetting.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnSetting.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSetting.Enabled = false;
            this.btnSetting.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.btnSetting.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSetting.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnSetting.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSetting.Location = new System.Drawing.Point(1187, 122);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnSetting.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(120)))), ((int)(((byte)(253)))));
            this.btnSetting.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSetting.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnSetting.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnSetting.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnSetting.Radius = 12;
            this.btnSetting.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.btnSetting.ShowBorder = true;
            this.btnSetting.ShowText = true;
            this.btnSetting.Size = new System.Drawing.Size(81, 27);
            this.btnSetting.TabIndex = 65;
            this.btnSetting.Text = "设置";
            this.btnSetting.Click += new System.EventHandler(this.btn_Click);
            // 
            // txtTaskFilePath
            // 
            this.txtTaskFilePath.AllowDrop = true;
            this.txtTaskFilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtTaskFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTaskFilePath.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtTaskFilePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtTaskFilePath.Location = new System.Drawing.Point(750, 122);
            this.txtTaskFilePath.Name = "txtTaskFilePath";
            this.txtTaskFilePath.Size = new System.Drawing.Size(431, 27);
            this.txtTaskFilePath.TabIndex = 64;
            this.txtTaskFilePath.TextChanged += new System.EventHandler(this.txtTaskFilePath_TextChanged);
            this.txtTaskFilePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.txtTaskFilePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            this.txtTaskFilePath.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtTaskFilePath_MouseDoubleClick);
            // 
            // lblTaskFile
            // 
            this.lblTaskFile.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTaskFile.BackColor = System.Drawing.Color.Transparent;
            this.lblTaskFile.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblTaskFile.BorderWidth = 1F;
            this.lblTaskFile.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblTaskFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTaskFile.Location = new System.Drawing.Point(664, 126);
            this.lblTaskFile.Name = "lblTaskFile";
            this.lblTaskFile.Radius = 16;
            this.lblTaskFile.ShowBackground = false;
            this.lblTaskFile.ShowBorder = false;
            this.lblTaskFile.Size = new System.Drawing.Size(80, 23);
            this.lblTaskFile.TabIndex = 66;
            this.lblTaskFile.Text = "锁任务文件";
            // 
            // btnForgotPassword
            // 
            this.btnForgotPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnForgotPassword.BorderWidth = 1F;
            this.btnForgotPassword.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnForgotPassword.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnForgotPassword.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnForgotPassword.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnForgotPassword.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnForgotPassword.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnForgotPassword.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnForgotPassword.Location = new System.Drawing.Point(1172, 75);
            this.btnForgotPassword.Name = "btnForgotPassword";
            this.btnForgotPassword.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnForgotPassword.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(120)))), ((int)(((byte)(253)))));
            this.btnForgotPassword.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnForgotPassword.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.btnForgotPassword.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnForgotPassword.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnForgotPassword.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnForgotPassword.Radius = 12;
            this.btnForgotPassword.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.btnForgotPassword.ShowBorder = false;
            this.btnForgotPassword.ShowText = true;
            this.btnForgotPassword.Size = new System.Drawing.Size(96, 27);
            this.btnForgotPassword.TabIndex = 67;
            this.btnForgotPassword.Text = "忘记密码";
            this.btnForgotPassword.Click += new System.EventHandler(this.btn_Click);
            // 
            // FormUsageLimitLockManagement
            // 
            this.AllowDrop = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(1280, 692);
            this.Controls.Add(this.btnForgotPassword);
            this.Controls.Add(this.lblTaskFile);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.txtTaskFilePath);
            this.Controls.Add(this.btnLockManagement);
            this.Controls.Add(this.splitLine1);
            this.Controls.Add(this.btnPasswordManagement);
            this.Controls.Add(this.txtRandKey);
            this.Controls.Add(this.lblRandKey);
            this.Controls.Add(this.chkShowPassword);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.lblDeviceID);
            this.Controls.Add(this.panelUsageLimitLock5);
            this.Controls.Add(this.panelUsageLimitLock4);
            this.Controls.Add(this.panelUsageLimitLock3);
            this.Controls.Add(this.panelUsageLimitLock2);
            this.Controls.Add(this.panelUsageLimitLock1);
            this.Controls.Add(this.txtDeviceID);
            this.Controls.Add(this.txtDeviceName);
            this.Controls.Add(this.lblDeviceName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormUsageLimitLockManagement";
            this.ShowInTaskbar = false;
            this.Text = "使用限制锁管理";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            this.Controls.SetChildIndex(this.title, 0);
            this.Controls.SetChildIndex(this.lblDeviceName, 0);
            this.Controls.SetChildIndex(this.txtDeviceName, 0);
            this.Controls.SetChildIndex(this.txtDeviceID, 0);
            this.Controls.SetChildIndex(this.panelUsageLimitLock1, 0);
            this.Controls.SetChildIndex(this.panelUsageLimitLock2, 0);
            this.Controls.SetChildIndex(this.panelUsageLimitLock3, 0);
            this.Controls.SetChildIndex(this.panelUsageLimitLock4, 0);
            this.Controls.SetChildIndex(this.panelUsageLimitLock5, 0);
            this.Controls.SetChildIndex(this.lblDeviceID, 0);
            this.Controls.SetChildIndex(this.txtOutput, 0);
            this.Controls.SetChildIndex(this.chkShowPassword, 0);
            this.Controls.SetChildIndex(this.lblRandKey, 0);
            this.Controls.SetChildIndex(this.txtRandKey, 0);
            this.Controls.SetChildIndex(this.btnPasswordManagement, 0);
            this.Controls.SetChildIndex(this.splitLine1, 0);
            this.Controls.SetChildIndex(this.btnLockManagement, 0);
            this.Controls.SetChildIndex(this.txtTaskFilePath, 0);
            this.Controls.SetChildIndex(this.btnSetting, 0);
            this.Controls.SetChildIndex(this.lblTaskFile, 0);
            this.Controls.SetChildIndex(this.btnForgotPassword, 0);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MLabel lblDeviceName;
        private Controls.MEditableLabel txtDeviceName;
        private System.Windows.Forms.TextBox txtDeviceID;
        private UsageLimitLockPanel panelUsageLimitLock1;
        private UsageLimitLockPanel panelUsageLimitLock2;
        private UsageLimitLockPanel panelUsageLimitLock3;
        private UsageLimitLockPanel panelUsageLimitLock4;
        private UsageLimitLockPanel panelUsageLimitLock5;
        private Controls.MLabel lblDeviceID;
        private System.Windows.Forms.TextBox txtOutput;
        private Controls.MImageCheckBox chkShowPassword;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiClear;
        private Controls.MLabel lblRandKey;
        private System.Windows.Forms.TextBox txtRandKey;
        private Controls.MButton btnPasswordManagement;
        private SplitLine splitLine1;
        private Controls.MButton btnLockManagement;
        private Controls.MButton btnSetting;
        private System.Windows.Forms.TextBox txtTaskFilePath;
        private Controls.MLabel lblTaskFile;
        private Controls.MButton btnForgotPassword;
    }
}
