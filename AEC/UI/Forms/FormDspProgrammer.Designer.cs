namespace AEC.UI.Forms
{
    partial class FormDspProgrammer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDspProgrammer));
            this.lblPort = new AEC.UI.Controls.MLabel();
            this.cmbPorts = new AEC.UI.gtComboBox();
            this.txtDspFwPackageFilePath = new System.Windows.Forms.TextBox();
            this.lblDspFirmwarePackage = new AEC.UI.Controls.MLabel();
            this.panelDspProgrammerDownloadParamsDownload = new AEC.UI.PanelDspFirmwareInfo();
            this.btnDownload = new AEC.UI.Controls.MButton();
            this.panelDspProgrammerDownloadParamsUpload = new AEC.UI.PanelDspFirmwareInfo();
            this.btnUpload = new AEC.UI.Controls.MButton();
            this.lblOutput = new AEC.UI.Controls.MLabel();
            this.linkRefresh = new System.Windows.Forms.LinkLabel();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.switchOnlyDownloadFirmwareInfo = new AEC.UI.Controls.MSwitch();
            this.lblDeviceid = new AEC.UI.Controls.MLabel();
            this.txtDeviceId = new System.Windows.Forms.TextBox();
            this.switchDownloadCompleteBin = new AEC.UI.Controls.MSwitch();
            this.lblWriteKeyHash = new AEC.UI.Controls.MLabel();
            this.txtWriteKeyHash = new System.Windows.Forms.TextBox();
            this.lblReadKeyHash = new AEC.UI.Controls.MLabel();
            this.txtReadKeyHash = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblPort
            // 
            this.lblPort.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPort.BackColor = System.Drawing.Color.Transparent;
            this.lblPort.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblPort.BorderWidth = 1F;
            this.lblPort.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblPort.Location = new System.Drawing.Point(16, 42);
            this.lblPort.Name = "lblPort";
            this.lblPort.Radius = 16;
            this.lblPort.ShowBackground = false;
            this.lblPort.ShowBorder = false;
            this.lblPort.Size = new System.Drawing.Size(114, 23);
            this.lblPort.TabIndex = 1;
            this.lblPort.Text = "端口";
            // 
            // cmbPorts
            // 
            this.cmbPorts.ArrowColor = System.Drawing.Color.Gray;
            this.cmbPorts.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPorts.FormattingEnabled = true;
            this.cmbPorts.Location = new System.Drawing.Point(136, 42);
            this.cmbPorts.Name = "cmbPorts";
            this.cmbPorts.Size = new System.Drawing.Size(461, 23);
            this.cmbPorts.TabIndex = 3;
            // 
            // txtDspFwPackageFilePath
            // 
            this.txtDspFwPackageFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDspFwPackageFilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtDspFwPackageFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDspFwPackageFilePath.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtDspFwPackageFilePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtDspFwPackageFilePath.Location = new System.Drawing.Point(136, 402);
            this.txtDspFwPackageFilePath.Name = "txtDspFwPackageFilePath";
            this.txtDspFwPackageFilePath.ReadOnly = true;
            this.txtDspFwPackageFilePath.Size = new System.Drawing.Size(506, 27);
            this.txtDspFwPackageFilePath.TabIndex = 4;
            this.txtDspFwPackageFilePath.TextChanged += new System.EventHandler(this.txtDspFwPackageFilePath_TextChanged);
            this.txtDspFwPackageFilePath.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtBinFilePath_MouseDoubleClick);
            // 
            // lblDspFirmwarePackage
            // 
            this.lblDspFirmwarePackage.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDspFirmwarePackage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDspFirmwarePackage.BackColor = System.Drawing.Color.Transparent;
            this.lblDspFirmwarePackage.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblDspFirmwarePackage.BorderWidth = 1F;
            this.lblDspFirmwarePackage.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblDspFirmwarePackage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblDspFirmwarePackage.Location = new System.Drawing.Point(16, 404);
            this.lblDspFirmwarePackage.Name = "lblDspFirmwarePackage";
            this.lblDspFirmwarePackage.Radius = 16;
            this.lblDspFirmwarePackage.ShowBackground = false;
            this.lblDspFirmwarePackage.ShowBorder = false;
            this.lblDspFirmwarePackage.Size = new System.Drawing.Size(114, 23);
            this.lblDspFirmwarePackage.TabIndex = 5;
            this.lblDspFirmwarePackage.Text = "DSP固件包";
            // 
            // panelDspProgrammerDownloadParamsDownload
            // 
            this.panelDspProgrammerDownloadParamsDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panelDspProgrammerDownloadParamsDownload.BackColor = System.Drawing.Color.Transparent;
            this.panelDspProgrammerDownloadParamsDownload.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.panelDspProgrammerDownloadParamsDownload.BorderWidth = 1F;
            this.panelDspProgrammerDownloadParamsDownload.dspFirmwareInfo = null;
            this.panelDspProgrammerDownloadParamsDownload.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panelDspProgrammerDownloadParamsDownload.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelDspProgrammerDownloadParamsDownload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelDspProgrammerDownloadParamsDownload.isEditable = false;
            this.panelDspProgrammerDownloadParamsDownload.Location = new System.Drawing.Point(16, 506);
            this.panelDspProgrammerDownloadParamsDownload.Name = "panelDspProgrammerDownloadParamsDownload";
            this.panelDspProgrammerDownloadParamsDownload.Radius = 16;
            this.panelDspProgrammerDownloadParamsDownload.ShowBorder = false;
            this.panelDspProgrammerDownloadParamsDownload.ShowText = true;
            this.panelDspProgrammerDownloadParamsDownload.Size = new System.Drawing.Size(736, 248);
            this.panelDspProgrammerDownloadParamsDownload.TabIndex = 6;
            this.panelDspProgrammerDownloadParamsDownload.Text = "DSP固件信息";
            this.panelDspProgrammerDownloadParamsDownload.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.panelDspProgrammerDownloadParamsDownload.TextPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnDownload.BorderWidth = 1F;
            this.btnDownload.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnDownload.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnDownload.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnDownload.Enabled = false;
            this.btnDownload.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnDownload.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDownload.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnDownload.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnDownload.Location = new System.Drawing.Point(656, 402);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnDownload.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnDownload.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDownload.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnDownload.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnDownload.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnDownload.Radius = 12;
            this.btnDownload.ShowBorder = true;
            this.btnDownload.ShowText = true;
            this.btnDownload.Size = new System.Drawing.Size(96, 27);
            this.btnDownload.TabIndex = 7;
            this.btnDownload.Text = "下载";
            this.btnDownload.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelDspProgrammerDownloadParamsUpload
            // 
            this.panelDspProgrammerDownloadParamsUpload.BackColor = System.Drawing.Color.Transparent;
            this.panelDspProgrammerDownloadParamsUpload.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.panelDspProgrammerDownloadParamsUpload.BorderWidth = 1F;
            this.panelDspProgrammerDownloadParamsUpload.dspFirmwareInfo = null;
            this.panelDspProgrammerDownloadParamsUpload.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panelDspProgrammerDownloadParamsUpload.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelDspProgrammerDownloadParamsUpload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelDspProgrammerDownloadParamsUpload.isEditable = false;
            this.panelDspProgrammerDownloadParamsUpload.Location = new System.Drawing.Point(16, 144);
            this.panelDspProgrammerDownloadParamsUpload.Name = "panelDspProgrammerDownloadParamsUpload";
            this.panelDspProgrammerDownloadParamsUpload.Radius = 16;
            this.panelDspProgrammerDownloadParamsUpload.ShowBorder = false;
            this.panelDspProgrammerDownloadParamsUpload.ShowText = true;
            this.panelDspProgrammerDownloadParamsUpload.Size = new System.Drawing.Size(736, 248);
            this.panelDspProgrammerDownloadParamsUpload.TabIndex = 8;
            this.panelDspProgrammerDownloadParamsUpload.Text = "DSP固件信息";
            this.panelDspProgrammerDownloadParamsUpload.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.panelDspProgrammerDownloadParamsUpload.TextPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpload.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnUpload.BorderWidth = 1F;
            this.btnUpload.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnUpload.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnUpload.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnUpload.Enabled = false;
            this.btnUpload.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnUpload.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnUpload.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnUpload.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnUpload.Location = new System.Drawing.Point(656, 39);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnUpload.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnUpload.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnUpload.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnUpload.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnUpload.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnUpload.Radius = 12;
            this.btnUpload.ShowBorder = true;
            this.btnUpload.ShowText = true;
            this.btnUpload.Size = new System.Drawing.Size(96, 27);
            this.btnUpload.TabIndex = 9;
            this.btnUpload.Text = "加载";
            this.btnUpload.Click += new System.EventHandler(this.btn_Click);
            // 
            // lblOutput
            // 
            this.lblOutput.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOutput.BackColor = System.Drawing.Color.Transparent;
            this.lblOutput.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblOutput.BorderWidth = 1F;
            this.lblOutput.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblOutput.Location = new System.Drawing.Point(773, 41);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Radius = 16;
            this.lblOutput.ShowBackground = false;
            this.lblOutput.ShowBorder = false;
            this.lblOutput.Size = new System.Drawing.Size(48, 23);
            this.lblOutput.TabIndex = 10;
            this.lblOutput.Text = "输出";
            // 
            // linkRefresh
            // 
            this.linkRefresh.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.linkRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkRefresh.AutoSize = true;
            this.linkRefresh.BackColor = System.Drawing.Color.Transparent;
            this.linkRefresh.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkRefresh.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.linkRefresh.Location = new System.Drawing.Point(609, 45);
            this.linkRefresh.Name = "linkRefresh";
            this.linkRefresh.Size = new System.Drawing.Size(33, 15);
            this.linkRefresh.TabIndex = 12;
            this.linkRefresh.TabStop = true;
            this.linkRefresh.Text = "刷新";
            this.linkRefresh.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRefresh_LinkClicked);
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtOutput.ForeColor = System.Drawing.Color.LightGray;
            this.txtOutput.HideSelection = false;
            this.txtOutput.Location = new System.Drawing.Point(773, 72);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(495, 682);
            this.txtOutput.TabIndex = 3;
            this.txtOutput.Text = "";
            // 
            // switchOnlyDownloadFirmwareInfo
            // 
            this.switchOnlyDownloadFirmwareInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.switchOnlyDownloadFirmwareInfo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchOnlyDownloadFirmwareInfo.BorderWidth = 1F;
            this.switchOnlyDownloadFirmwareInfo.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.switchOnlyDownloadFirmwareInfo.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.switchOnlyDownloadFirmwareInfo.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.switchOnlyDownloadFirmwareInfo.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.switchOnlyDownloadFirmwareInfo.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.switchOnlyDownloadFirmwareInfo.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.switchOnlyDownloadFirmwareInfo.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchOnlyDownloadFirmwareInfo.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.switchOnlyDownloadFirmwareInfo.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.switchOnlyDownloadFirmwareInfo.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.switchOnlyDownloadFirmwareInfo.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.switchOnlyDownloadFirmwareInfo.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchOnlyDownloadFirmwareInfo.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.switchOnlyDownloadFirmwareInfo.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.switchOnlyDownloadFirmwareInfo.isAutoCheck = true;
            this.switchOnlyDownloadFirmwareInfo.IsChecked = false;
            this.switchOnlyDownloadFirmwareInfo.IsCheckedInit = false;
            this.switchOnlyDownloadFirmwareInfo.Location = new System.Drawing.Point(136, 435);
            this.switchOnlyDownloadFirmwareInfo.Name = "switchOnlyDownloadFirmwareInfo";
            this.switchOnlyDownloadFirmwareInfo.Radius = 29;
            this.switchOnlyDownloadFirmwareInfo.ShowBorder = false;
            this.switchOnlyDownloadFirmwareInfo.ShowText = true;
            this.switchOnlyDownloadFirmwareInfo.Size = new System.Drawing.Size(162, 29);
            this.switchOnlyDownloadFirmwareInfo.TabIndex = 13;
            this.switchOnlyDownloadFirmwareInfo.Text = "仅下载固件信息";
            this.switchOnlyDownloadFirmwareInfo.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.switchOnlyDownloadFirmwareInfo.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.switchOnlyDownloadFirmwareInfo.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.switchOnlyDownloadFirmwareInfo.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.switchOnlyDownloadFirmwareInfo.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.switchOnlyDownloadFirmwareInfo.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.switchOnlyDownloadFirmwareInfo.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchOnlyDownloadFirmwareInfo.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.switchOnlyDownloadFirmwareInfo.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.switchOnlyDownloadFirmwareInfo.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.switchOnlyDownloadFirmwareInfo.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.switchOnlyDownloadFirmwareInfo.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            // 
            // lblDeviceid
            // 
            this.lblDeviceid.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDeviceid.BackColor = System.Drawing.Color.Transparent;
            this.lblDeviceid.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblDeviceid.BorderWidth = 1F;
            this.lblDeviceid.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblDeviceid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblDeviceid.Location = new System.Drawing.Point(16, 74);
            this.lblDeviceid.Name = "lblDeviceid";
            this.lblDeviceid.Radius = 16;
            this.lblDeviceid.ShowBackground = false;
            this.lblDeviceid.ShowBorder = false;
            this.lblDeviceid.Size = new System.Drawing.Size(114, 23);
            this.lblDeviceid.TabIndex = 15;
            this.lblDeviceid.Text = "设备ID";
            // 
            // txtDeviceId
            // 
            this.txtDeviceId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtDeviceId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeviceId.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtDeviceId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtDeviceId.Location = new System.Drawing.Point(136, 72);
            this.txtDeviceId.Name = "txtDeviceId";
            this.txtDeviceId.ReadOnly = true;
            this.txtDeviceId.Size = new System.Drawing.Size(586, 27);
            this.txtDeviceId.TabIndex = 14;
            // 
            // switchDownloadCompleteBin
            // 
            this.switchDownloadCompleteBin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.switchDownloadCompleteBin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchDownloadCompleteBin.BorderWidth = 1F;
            this.switchDownloadCompleteBin.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.switchDownloadCompleteBin.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.switchDownloadCompleteBin.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.switchDownloadCompleteBin.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.switchDownloadCompleteBin.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.switchDownloadCompleteBin.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.switchDownloadCompleteBin.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchDownloadCompleteBin.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.switchDownloadCompleteBin.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.switchDownloadCompleteBin.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.switchDownloadCompleteBin.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.switchDownloadCompleteBin.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchDownloadCompleteBin.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.switchDownloadCompleteBin.Enabled = false;
            this.switchDownloadCompleteBin.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.switchDownloadCompleteBin.isAutoCheck = true;
            this.switchDownloadCompleteBin.IsChecked = true;
            this.switchDownloadCompleteBin.IsCheckedInit = true;
            this.switchDownloadCompleteBin.Location = new System.Drawing.Point(304, 435);
            this.switchDownloadCompleteBin.Name = "switchDownloadCompleteBin";
            this.switchDownloadCompleteBin.Radius = 29;
            this.switchDownloadCompleteBin.ShowBorder = false;
            this.switchDownloadCompleteBin.ShowText = true;
            this.switchDownloadCompleteBin.Size = new System.Drawing.Size(160, 29);
            this.switchDownloadCompleteBin.TabIndex = 16;
            this.switchDownloadCompleteBin.Text = "下载完整bin数据";
            this.switchDownloadCompleteBin.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.switchDownloadCompleteBin.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.switchDownloadCompleteBin.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.switchDownloadCompleteBin.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.switchDownloadCompleteBin.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.switchDownloadCompleteBin.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.switchDownloadCompleteBin.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchDownloadCompleteBin.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.switchDownloadCompleteBin.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.switchDownloadCompleteBin.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.switchDownloadCompleteBin.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.switchDownloadCompleteBin.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            // 
            // lblWriteKeyHash
            // 
            this.lblWriteKeyHash.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblWriteKeyHash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblWriteKeyHash.BackColor = System.Drawing.Color.Transparent;
            this.lblWriteKeyHash.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblWriteKeyHash.BorderWidth = 1F;
            this.lblWriteKeyHash.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblWriteKeyHash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblWriteKeyHash.Location = new System.Drawing.Point(16, 472);
            this.lblWriteKeyHash.Name = "lblWriteKeyHash";
            this.lblWriteKeyHash.Radius = 16;
            this.lblWriteKeyHash.ShowBackground = false;
            this.lblWriteKeyHash.ShowBorder = false;
            this.lblWriteKeyHash.Size = new System.Drawing.Size(114, 23);
            this.lblWriteKeyHash.TabIndex = 18;
            this.lblWriteKeyHash.Text = "密钥Hash";
            // 
            // txtWriteKeyHash
            // 
            this.txtWriteKeyHash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtWriteKeyHash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtWriteKeyHash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWriteKeyHash.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtWriteKeyHash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtWriteKeyHash.Location = new System.Drawing.Point(136, 470);
            this.txtWriteKeyHash.Name = "txtWriteKeyHash";
            this.txtWriteKeyHash.ReadOnly = true;
            this.txtWriteKeyHash.Size = new System.Drawing.Size(586, 27);
            this.txtWriteKeyHash.TabIndex = 17;
            // 
            // lblReadKeyHash
            // 
            this.lblReadKeyHash.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblReadKeyHash.BackColor = System.Drawing.Color.Transparent;
            this.lblReadKeyHash.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblReadKeyHash.BorderWidth = 1F;
            this.lblReadKeyHash.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblReadKeyHash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblReadKeyHash.Location = new System.Drawing.Point(16, 110);
            this.lblReadKeyHash.Name = "lblReadKeyHash";
            this.lblReadKeyHash.Radius = 16;
            this.lblReadKeyHash.ShowBackground = false;
            this.lblReadKeyHash.ShowBorder = false;
            this.lblReadKeyHash.Size = new System.Drawing.Size(114, 23);
            this.lblReadKeyHash.TabIndex = 20;
            this.lblReadKeyHash.Text = "密钥Hash";
            // 
            // txtReadKeyHash
            // 
            this.txtReadKeyHash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtReadKeyHash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReadKeyHash.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtReadKeyHash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtReadKeyHash.Location = new System.Drawing.Point(136, 108);
            this.txtReadKeyHash.Name = "txtReadKeyHash";
            this.txtReadKeyHash.ReadOnly = true;
            this.txtReadKeyHash.Size = new System.Drawing.Size(586, 27);
            this.txtReadKeyHash.TabIndex = 19;
            // 
            // FormDspProgrammer
            // 
            this.AllowDrop = true;
            this.ClientSize = new System.Drawing.Size(1280, 768);
            this.Controls.Add(this.lblReadKeyHash);
            this.Controls.Add(this.txtReadKeyHash);
            this.Controls.Add(this.lblWriteKeyHash);
            this.Controls.Add(this.txtWriteKeyHash);
            this.Controls.Add(this.switchDownloadCompleteBin);
            this.Controls.Add(this.lblDeviceid);
            this.Controls.Add(this.txtDeviceId);
            this.Controls.Add(this.switchOnlyDownloadFirmwareInfo);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.linkRefresh);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.panelDspProgrammerDownloadParamsUpload);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.panelDspProgrammerDownloadParamsDownload);
            this.Controls.Add(this.lblDspFirmwarePackage);
            this.Controls.Add(this.txtDspFwPackageFilePath);
            this.Controls.Add(this.cmbPorts);
            this.Controls.Add(this.lblPort);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormDspProgrammer";
            this.Text = "M-DSP编程器";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDspIapProgrammerDownload_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDspIapProgrammerDownload_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.Controls.SetChildIndex(this.lblPort, 0);
            this.Controls.SetChildIndex(this.cmbPorts, 0);
            this.Controls.SetChildIndex(this.txtDspFwPackageFilePath, 0);
            this.Controls.SetChildIndex(this.lblDspFirmwarePackage, 0);
            this.Controls.SetChildIndex(this.panelDspProgrammerDownloadParamsDownload, 0);
            this.Controls.SetChildIndex(this.btnDownload, 0);
            this.Controls.SetChildIndex(this.panelDspProgrammerDownloadParamsUpload, 0);
            this.Controls.SetChildIndex(this.btnUpload, 0);
            this.Controls.SetChildIndex(this.lblOutput, 0);
            this.Controls.SetChildIndex(this.linkRefresh, 0);
            this.Controls.SetChildIndex(this.txtOutput, 0);
            this.Controls.SetChildIndex(this.switchOnlyDownloadFirmwareInfo, 0);
            this.Controls.SetChildIndex(this.txtDeviceId, 0);
            this.Controls.SetChildIndex(this.lblDeviceid, 0);
            this.Controls.SetChildIndex(this.switchDownloadCompleteBin, 0);
            this.Controls.SetChildIndex(this.txtWriteKeyHash, 0);
            this.Controls.SetChildIndex(this.lblWriteKeyHash, 0);
            this.Controls.SetChildIndex(this.txtReadKeyHash, 0);
            this.Controls.SetChildIndex(this.lblReadKeyHash, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MLabel lblPort;
        private gtComboBox cmbPorts;
        private System.Windows.Forms.TextBox txtDspFwPackageFilePath;
        private Controls.MLabel lblDspFirmwarePackage;
        private PanelDspFirmwareInfo panelDspProgrammerDownloadParamsDownload;
        private Controls.MButton btnDownload;
        private PanelDspFirmwareInfo panelDspProgrammerDownloadParamsUpload;
        private Controls.MButton btnUpload;
        private Controls.MLabel lblOutput;
        private System.Windows.Forms.LinkLabel linkRefresh;
        private System.Windows.Forms.RichTextBox txtOutput;
        private Controls.MSwitch switchOnlyDownloadFirmwareInfo;
        private Controls.MLabel lblDeviceid;
        private System.Windows.Forms.TextBox txtDeviceId;
        private Controls.MSwitch switchDownloadCompleteBin;
        private Controls.MLabel lblWriteKeyHash;
        private System.Windows.Forms.TextBox txtWriteKeyHash;
        private Controls.MLabel lblReadKeyHash;
        private System.Windows.Forms.TextBox txtReadKeyHash;
    }
}
