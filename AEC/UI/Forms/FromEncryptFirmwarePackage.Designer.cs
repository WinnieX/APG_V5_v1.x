namespace AEC.UI.Forms
{
    partial class FormEncryptFirmwarePackage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEncryptFirmwarePackage));
            this.lblFilePath = new AEC.UI.Controls.MLabel();
            this.btnEncryptFP = new AEC.UI.Controls.MButton();
            this.txtFirmwarePackageInfo = new System.Windows.Forms.TextBox();
            this.txtFirmwarePackageFilePath = new System.Windows.Forms.TextBox();
            this.chkUpdatePresetData = new AEC.UI.Controls.MSwitch();
            this.chkUpdateCustomDeviceInfo = new AEC.UI.Controls.MSwitch();
            this.chkUpdateMCUAllData = new AEC.UI.Controls.MSwitch();
            this.chkMCUFirmware = new AEC.UI.Controls.MSwitch();
            this.chkDSPFirmware = new AEC.UI.Controls.MSwitch();
            this.txtDSPFirmwareBinInfo = new System.Windows.Forms.TextBox();
            this.txtMCUFirmwareBinInfo = new System.Windows.Forms.TextBox();
            this.txtCustomPresetData = new System.Windows.Forms.TextBox();
            this.txtMCUAllData = new System.Windows.Forms.TextBox();
            this.txtCustomDeviceInfo = new System.Windows.Forms.TextBox();
            this.panelFPInfo = new AEC.UI.Controls.MPanel();
            this.lblKey = new AEC.UI.Controls.MLabel();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.panelFPInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFilePath
            // 
            this.lblFilePath.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFilePath.BackColor = System.Drawing.Color.Transparent;
            this.lblFilePath.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblFilePath.BorderWidth = 1F;
            this.lblFilePath.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblFilePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblFilePath.Location = new System.Drawing.Point(32, 57);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Radius = 16;
            this.lblFilePath.ShowBackground = false;
            this.lblFilePath.ShowBorder = false;
            this.lblFilePath.Size = new System.Drawing.Size(36, 23);
            this.lblFilePath.TabIndex = 47;
            this.lblFilePath.Text = "文件";
            // 
            // btnEncryptFP
            // 
            this.btnEncryptFP.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnEncryptFP.BorderWidth = 1F;
            this.btnEncryptFP.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnEncryptFP.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnEncryptFP.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnEncryptFP.Enabled = false;
            this.btnEncryptFP.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnEncryptFP.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnEncryptFP.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnEncryptFP.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnEncryptFP.Location = new System.Drawing.Point(1152, 55);
            this.btnEncryptFP.Name = "btnEncryptFP";
            this.btnEncryptFP.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnEncryptFP.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnEncryptFP.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnEncryptFP.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnEncryptFP.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnEncryptFP.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnEncryptFP.Radius = 12;
            this.btnEncryptFP.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.btnEncryptFP.ShowBorder = true;
            this.btnEncryptFP.ShowText = true;
            this.btnEncryptFP.Size = new System.Drawing.Size(96, 27);
            this.btnEncryptFP.TabIndex = 46;
            this.btnEncryptFP.Text = "加密并导出";
            this.btnEncryptFP.Click += new System.EventHandler(this.btnEncryptFP_Click);
            // 
            // txtFirmwarePackageInfo
            // 
            this.txtFirmwarePackageInfo.AllowDrop = true;
            this.txtFirmwarePackageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFirmwarePackageInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtFirmwarePackageInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFirmwarePackageInfo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtFirmwarePackageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtFirmwarePackageInfo.Location = new System.Drawing.Point(24, 24);
            this.txtFirmwarePackageInfo.Multiline = true;
            this.txtFirmwarePackageInfo.Name = "txtFirmwarePackageInfo";
            this.txtFirmwarePackageInfo.ReadOnly = true;
            this.txtFirmwarePackageInfo.Size = new System.Drawing.Size(1168, 136);
            this.txtFirmwarePackageInfo.TabIndex = 45;
            this.txtFirmwarePackageInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.txtFirmwarePackageInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            // 
            // txtFirmwarePackageFilePath
            // 
            this.txtFirmwarePackageFilePath.AllowDrop = true;
            this.txtFirmwarePackageFilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtFirmwarePackageFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFirmwarePackageFilePath.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtFirmwarePackageFilePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtFirmwarePackageFilePath.Location = new System.Drawing.Point(74, 55);
            this.txtFirmwarePackageFilePath.Name = "txtFirmwarePackageFilePath";
            this.txtFirmwarePackageFilePath.Size = new System.Drawing.Size(689, 27);
            this.txtFirmwarePackageFilePath.TabIndex = 44;
            this.txtFirmwarePackageFilePath.TextChanged += new System.EventHandler(this.txtFirmwarePackageFilePath_TextChanged);
            this.txtFirmwarePackageFilePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.txtFirmwarePackageFilePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            this.txtFirmwarePackageFilePath.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txt_MouseDoubleClick);
            // 
            // chkUpdatePresetData
            // 
            this.chkUpdatePresetData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkUpdatePresetData.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdatePresetData.BorderWidth = 1F;
            this.chkUpdatePresetData.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkUpdatePresetData.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.chkUpdatePresetData.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkUpdatePresetData.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkUpdatePresetData.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.chkUpdatePresetData.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkUpdatePresetData.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdatePresetData.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.chkUpdatePresetData.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkUpdatePresetData.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkUpdatePresetData.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.chkUpdatePresetData.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdatePresetData.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkUpdatePresetData.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkUpdatePresetData.isAutoCheck = true;
            this.chkUpdatePresetData.IsChecked = false;
            this.chkUpdatePresetData.IsCheckedInit = false;
            this.chkUpdatePresetData.Location = new System.Drawing.Point(808, 309);
            this.chkUpdatePresetData.Name = "chkUpdatePresetData";
            this.chkUpdatePresetData.Radius = 29;
            this.chkUpdatePresetData.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkUpdatePresetData.ShowBorder = false;
            this.chkUpdatePresetData.ShowText = true;
            this.chkUpdatePresetData.Size = new System.Drawing.Size(172, 29);
            this.chkUpdatePresetData.SwitchWdith = 48;
            this.chkUpdatePresetData.TabIndex = 54;
            this.chkUpdatePresetData.Text = "更新自定义预设数据";
            this.chkUpdatePresetData.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkUpdatePresetData.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkUpdatePresetData.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkUpdatePresetData.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkUpdatePresetData.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkUpdatePresetData.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkUpdatePresetData.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdatePresetData.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkUpdatePresetData.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkUpdatePresetData.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkUpdatePresetData.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkUpdatePresetData.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            // 
            // chkUpdateCustomDeviceInfo
            // 
            this.chkUpdateCustomDeviceInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkUpdateCustomDeviceInfo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdateCustomDeviceInfo.BorderWidth = 1F;
            this.chkUpdateCustomDeviceInfo.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkUpdateCustomDeviceInfo.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.chkUpdateCustomDeviceInfo.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkUpdateCustomDeviceInfo.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkUpdateCustomDeviceInfo.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.chkUpdateCustomDeviceInfo.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkUpdateCustomDeviceInfo.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdateCustomDeviceInfo.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.chkUpdateCustomDeviceInfo.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkUpdateCustomDeviceInfo.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkUpdateCustomDeviceInfo.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.chkUpdateCustomDeviceInfo.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdateCustomDeviceInfo.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkUpdateCustomDeviceInfo.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkUpdateCustomDeviceInfo.isAutoCheck = true;
            this.chkUpdateCustomDeviceInfo.IsChecked = false;
            this.chkUpdateCustomDeviceInfo.IsCheckedInit = false;
            this.chkUpdateCustomDeviceInfo.Location = new System.Drawing.Point(416, 309);
            this.chkUpdateCustomDeviceInfo.Name = "chkUpdateCustomDeviceInfo";
            this.chkUpdateCustomDeviceInfo.Radius = 29;
            this.chkUpdateCustomDeviceInfo.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkUpdateCustomDeviceInfo.ShowBorder = false;
            this.chkUpdateCustomDeviceInfo.ShowText = true;
            this.chkUpdateCustomDeviceInfo.Size = new System.Drawing.Size(172, 29);
            this.chkUpdateCustomDeviceInfo.SwitchWdith = 48;
            this.chkUpdateCustomDeviceInfo.TabIndex = 53;
            this.chkUpdateCustomDeviceInfo.Text = "更新自定义设备信息";
            this.chkUpdateCustomDeviceInfo.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkUpdateCustomDeviceInfo.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkUpdateCustomDeviceInfo.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkUpdateCustomDeviceInfo.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkUpdateCustomDeviceInfo.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkUpdateCustomDeviceInfo.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkUpdateCustomDeviceInfo.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdateCustomDeviceInfo.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkUpdateCustomDeviceInfo.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkUpdateCustomDeviceInfo.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkUpdateCustomDeviceInfo.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkUpdateCustomDeviceInfo.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdateCustomDeviceInfo.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkUpdateMCUAllData
            // 
            this.chkUpdateMCUAllData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkUpdateMCUAllData.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdateMCUAllData.BorderWidth = 1F;
            this.chkUpdateMCUAllData.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkUpdateMCUAllData.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.chkUpdateMCUAllData.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkUpdateMCUAllData.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkUpdateMCUAllData.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.chkUpdateMCUAllData.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkUpdateMCUAllData.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdateMCUAllData.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.chkUpdateMCUAllData.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkUpdateMCUAllData.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkUpdateMCUAllData.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.chkUpdateMCUAllData.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdateMCUAllData.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkUpdateMCUAllData.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkUpdateMCUAllData.isAutoCheck = true;
            this.chkUpdateMCUAllData.IsChecked = false;
            this.chkUpdateMCUAllData.IsCheckedInit = false;
            this.chkUpdateMCUAllData.Location = new System.Drawing.Point(24, 309);
            this.chkUpdateMCUAllData.Name = "chkUpdateMCUAllData";
            this.chkUpdateMCUAllData.Radius = 29;
            this.chkUpdateMCUAllData.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkUpdateMCUAllData.ShowBorder = false;
            this.chkUpdateMCUAllData.ShowText = true;
            this.chkUpdateMCUAllData.Size = new System.Drawing.Size(172, 29);
            this.chkUpdateMCUAllData.SwitchWdith = 48;
            this.chkUpdateMCUAllData.TabIndex = 52;
            this.chkUpdateMCUAllData.Text = "更新MCU Flash数据";
            this.chkUpdateMCUAllData.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkUpdateMCUAllData.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkUpdateMCUAllData.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkUpdateMCUAllData.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkUpdateMCUAllData.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkUpdateMCUAllData.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkUpdateMCUAllData.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdateMCUAllData.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkUpdateMCUAllData.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkUpdateMCUAllData.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkUpdateMCUAllData.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkUpdateMCUAllData.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdateMCUAllData.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkMCUFirmware
            // 
            this.chkMCUFirmware.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkMCUFirmware.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkMCUFirmware.BorderWidth = 1F;
            this.chkMCUFirmware.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkMCUFirmware.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.chkMCUFirmware.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkMCUFirmware.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkMCUFirmware.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.chkMCUFirmware.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkMCUFirmware.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkMCUFirmware.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.chkMCUFirmware.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkMCUFirmware.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkMCUFirmware.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.chkMCUFirmware.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkMCUFirmware.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkMCUFirmware.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkMCUFirmware.isAutoCheck = true;
            this.chkMCUFirmware.IsChecked = false;
            this.chkMCUFirmware.IsCheckedInit = false;
            this.chkMCUFirmware.Location = new System.Drawing.Point(612, 168);
            this.chkMCUFirmware.Name = "chkMCUFirmware";
            this.chkMCUFirmware.Radius = 29;
            this.chkMCUFirmware.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkMCUFirmware.ShowBorder = false;
            this.chkMCUFirmware.ShowText = true;
            this.chkMCUFirmware.Size = new System.Drawing.Size(172, 29);
            this.chkMCUFirmware.SwitchWdith = 48;
            this.chkMCUFirmware.TabIndex = 51;
            this.chkMCUFirmware.Text = "MCU bin 镜像文件";
            this.chkMCUFirmware.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkMCUFirmware.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkMCUFirmware.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkMCUFirmware.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkMCUFirmware.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkMCUFirmware.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkMCUFirmware.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkMCUFirmware.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkMCUFirmware.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkMCUFirmware.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkMCUFirmware.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkMCUFirmware.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            // 
            // chkDSPFirmware
            // 
            this.chkDSPFirmware.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkDSPFirmware.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkDSPFirmware.BorderWidth = 1F;
            this.chkDSPFirmware.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkDSPFirmware.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.chkDSPFirmware.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkDSPFirmware.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkDSPFirmware.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.chkDSPFirmware.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkDSPFirmware.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkDSPFirmware.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.chkDSPFirmware.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkDSPFirmware.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkDSPFirmware.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.chkDSPFirmware.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkDSPFirmware.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkDSPFirmware.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkDSPFirmware.isAutoCheck = true;
            this.chkDSPFirmware.IsChecked = false;
            this.chkDSPFirmware.IsCheckedInit = false;
            this.chkDSPFirmware.Location = new System.Drawing.Point(24, 168);
            this.chkDSPFirmware.Name = "chkDSPFirmware";
            this.chkDSPFirmware.Radius = 29;
            this.chkDSPFirmware.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkDSPFirmware.ShowBorder = false;
            this.chkDSPFirmware.ShowText = true;
            this.chkDSPFirmware.Size = new System.Drawing.Size(172, 29);
            this.chkDSPFirmware.SwitchWdith = 48;
            this.chkDSPFirmware.TabIndex = 50;
            this.chkDSPFirmware.Text = "DSP bin 镜像文件";
            this.chkDSPFirmware.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkDSPFirmware.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkDSPFirmware.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkDSPFirmware.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkDSPFirmware.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkDSPFirmware.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkDSPFirmware.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkDSPFirmware.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkDSPFirmware.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkDSPFirmware.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkDSPFirmware.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkDSPFirmware.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            // 
            // txtDSPFirmwareBinInfo
            // 
            this.txtDSPFirmwareBinInfo.AllowDrop = true;
            this.txtDSPFirmwareBinInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDSPFirmwareBinInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtDSPFirmwareBinInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDSPFirmwareBinInfo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtDSPFirmwareBinInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtDSPFirmwareBinInfo.Location = new System.Drawing.Point(24, 205);
            this.txtDSPFirmwareBinInfo.Multiline = true;
            this.txtDSPFirmwareBinInfo.Name = "txtDSPFirmwareBinInfo";
            this.txtDSPFirmwareBinInfo.ReadOnly = true;
            this.txtDSPFirmwareBinInfo.Size = new System.Drawing.Size(580, 96);
            this.txtDSPFirmwareBinInfo.TabIndex = 55;
            this.txtDSPFirmwareBinInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.txtDSPFirmwareBinInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            // 
            // txtMCUFirmwareBinInfo
            // 
            this.txtMCUFirmwareBinInfo.AllowDrop = true;
            this.txtMCUFirmwareBinInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMCUFirmwareBinInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtMCUFirmwareBinInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMCUFirmwareBinInfo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtMCUFirmwareBinInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtMCUFirmwareBinInfo.Location = new System.Drawing.Point(612, 205);
            this.txtMCUFirmwareBinInfo.Multiline = true;
            this.txtMCUFirmwareBinInfo.Name = "txtMCUFirmwareBinInfo";
            this.txtMCUFirmwareBinInfo.ReadOnly = true;
            this.txtMCUFirmwareBinInfo.Size = new System.Drawing.Size(580, 96);
            this.txtMCUFirmwareBinInfo.TabIndex = 56;
            this.txtMCUFirmwareBinInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.txtMCUFirmwareBinInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            // 
            // txtCustomPresetData
            // 
            this.txtCustomPresetData.AllowDrop = true;
            this.txtCustomPresetData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCustomPresetData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtCustomPresetData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomPresetData.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtCustomPresetData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtCustomPresetData.Location = new System.Drawing.Point(808, 346);
            this.txtCustomPresetData.Multiline = true;
            this.txtCustomPresetData.Name = "txtCustomPresetData";
            this.txtCustomPresetData.ReadOnly = true;
            this.txtCustomPresetData.Size = new System.Drawing.Size(384, 96);
            this.txtCustomPresetData.TabIndex = 58;
            this.txtCustomPresetData.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.txtCustomPresetData.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            // 
            // txtMCUAllData
            // 
            this.txtMCUAllData.AllowDrop = true;
            this.txtMCUAllData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMCUAllData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtMCUAllData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMCUAllData.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtMCUAllData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtMCUAllData.Location = new System.Drawing.Point(24, 346);
            this.txtMCUAllData.Multiline = true;
            this.txtMCUAllData.Name = "txtMCUAllData";
            this.txtMCUAllData.ReadOnly = true;
            this.txtMCUAllData.Size = new System.Drawing.Size(384, 96);
            this.txtMCUAllData.TabIndex = 57;
            this.txtMCUAllData.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.txtMCUAllData.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            // 
            // txtCustomDeviceInfo
            // 
            this.txtCustomDeviceInfo.AllowDrop = true;
            this.txtCustomDeviceInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCustomDeviceInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtCustomDeviceInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomDeviceInfo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtCustomDeviceInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtCustomDeviceInfo.Location = new System.Drawing.Point(416, 346);
            this.txtCustomDeviceInfo.Multiline = true;
            this.txtCustomDeviceInfo.Name = "txtCustomDeviceInfo";
            this.txtCustomDeviceInfo.ReadOnly = true;
            this.txtCustomDeviceInfo.Size = new System.Drawing.Size(384, 96);
            this.txtCustomDeviceInfo.TabIndex = 59;
            this.txtCustomDeviceInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.txtCustomDeviceInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            // 
            // panelFPInfo
            // 
            this.panelFPInfo.AllowDrop = true;
            this.panelFPInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelFPInfo.BackColor = System.Drawing.Color.Transparent;
            this.panelFPInfo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.panelFPInfo.Controls.Add(this.txtFirmwarePackageInfo);
            this.panelFPInfo.Controls.Add(this.txtCustomDeviceInfo);
            this.panelFPInfo.Controls.Add(this.chkDSPFirmware);
            this.panelFPInfo.Controls.Add(this.txtCustomPresetData);
            this.panelFPInfo.Controls.Add(this.chkMCUFirmware);
            this.panelFPInfo.Controls.Add(this.txtMCUAllData);
            this.panelFPInfo.Controls.Add(this.chkUpdateMCUAllData);
            this.panelFPInfo.Controls.Add(this.txtMCUFirmwareBinInfo);
            this.panelFPInfo.Controls.Add(this.chkUpdateCustomDeviceInfo);
            this.panelFPInfo.Controls.Add(this.txtDSPFirmwareBinInfo);
            this.panelFPInfo.Controls.Add(this.chkUpdatePresetData);
            this.panelFPInfo.Enabled = false;
            this.panelFPInfo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panelFPInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelFPInfo.Location = new System.Drawing.Point(32, 94);
            this.panelFPInfo.Name = "panelFPInfo";
            this.panelFPInfo.Padding = new System.Windows.Forms.Padding(3);
            this.panelFPInfo.Radius = 16;
            this.panelFPInfo.ShowBorder = false;
            this.panelFPInfo.Size = new System.Drawing.Size(1216, 458);
            this.panelFPInfo.TabIndex = 60;
            this.panelFPInfo.Text = "固件信息";
            this.panelFPInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.panelFPInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            // 
            // lblKey
            // 
            this.lblKey.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblKey.BackColor = System.Drawing.Color.Transparent;
            this.lblKey.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblKey.BorderWidth = 1F;
            this.lblKey.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblKey.Location = new System.Drawing.Point(770, 59);
            this.lblKey.Name = "lblKey";
            this.lblKey.Radius = 16;
            this.lblKey.ShowBackground = false;
            this.lblKey.ShowBorder = false;
            this.lblKey.Size = new System.Drawing.Size(36, 23);
            this.lblKey.TabIndex = 62;
            this.lblKey.Text = "密钥";
            // 
            // txtKey
            // 
            this.txtKey.AllowDrop = true;
            this.txtKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKey.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtKey.Location = new System.Drawing.Point(812, 55);
            this.txtKey.MaxLength = 32;
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(320, 27);
            this.txtKey.TabIndex = 61;
            this.txtKey.TextChanged += new System.EventHandler(this.txtKey_TextChanged);
            // 
            // FormEncryptFirmwarePackage
            // 
            this.AllowDrop = true;
            this.ClientSize = new System.Drawing.Size(1280, 576);
            this.Controls.Add(this.lblKey);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.panelFPInfo);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.btnEncryptFP);
            this.Controls.Add(this.txtFirmwarePackageFilePath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormEncryptFirmwarePackage";
            this.Text = "加密固件包";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.Controls.SetChildIndex(this.txtFirmwarePackageFilePath, 0);
            this.Controls.SetChildIndex(this.btnEncryptFP, 0);
            this.Controls.SetChildIndex(this.lblFilePath, 0);
            this.Controls.SetChildIndex(this.panelFPInfo, 0);
            this.Controls.SetChildIndex(this.txtKey, 0);
            this.Controls.SetChildIndex(this.lblKey, 0);
            this.panelFPInfo.ResumeLayout(false);
            this.panelFPInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MLabel lblFilePath;
        private Controls.MButton btnEncryptFP;
        private System.Windows.Forms.TextBox txtFirmwarePackageInfo;
        private System.Windows.Forms.TextBox txtFirmwarePackageFilePath;
        private Controls.MSwitch chkUpdatePresetData;
        private Controls.MSwitch chkUpdateCustomDeviceInfo;
        private Controls.MSwitch chkUpdateMCUAllData;
        private Controls.MSwitch chkMCUFirmware;
        private Controls.MSwitch chkDSPFirmware;
        private System.Windows.Forms.TextBox txtDSPFirmwareBinInfo;
        private System.Windows.Forms.TextBox txtMCUFirmwareBinInfo;
        private System.Windows.Forms.TextBox txtCustomPresetData;
        private System.Windows.Forms.TextBox txtMCUAllData;
        private System.Windows.Forms.TextBox txtCustomDeviceInfo;
        private Controls.MPanel panelFPInfo;
        private Controls.MLabel lblKey;
        private System.Windows.Forms.TextBox txtKey;
    }
}
