namespace AEC.UI.Forms
{
    partial class FormFirmwareUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFirmwareUpdate));
            this.linkRefreshPort = new System.Windows.Forms.LinkLabel();
            this.cmbPorts = new AEC.UI.gtComboBox();
            this.lblPort = new AEC.UI.Controls.MLabel();
            this.txtFirmwarePackageFilePath = new System.Windows.Forms.TextBox();
            this.txtFirmwarePackageInfo = new System.Windows.Forms.TextBox();
            this.btnUpdate = new AEC.UI.Controls.MButton();
            this.cmbBaudRate = new AEC.UI.gtComboBox();
            this.lblBaudRate = new AEC.UI.Controls.MLabel();
            this.lblKey = new AEC.UI.Controls.MLabel();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.linkRefreshKey = new System.Windows.Forms.LinkLabel();
            this.lblFilePath = new AEC.UI.Controls.MLabel();
            this.lblFirmwarePackageInfo = new AEC.UI.Controls.MLabel();
            this.lblProgress = new AEC.UI.Controls.MLabel();
            this.mProgressBar = new AEC.UI.Controls.MProgressBar();
            this.lblInfo = new AEC.UI.Controls.MLabel();
            this.SuspendLayout();
            // 
            // linkRefreshPort
            // 
            this.linkRefreshPort.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.linkRefreshPort.AutoSize = true;
            this.linkRefreshPort.BackColor = System.Drawing.Color.Transparent;
            this.linkRefreshPort.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkRefreshPort.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.linkRefreshPort.Location = new System.Drawing.Point(209, 51);
            this.linkRefreshPort.Name = "linkRefreshPort";
            this.linkRefreshPort.Size = new System.Drawing.Size(33, 15);
            this.linkRefreshPort.TabIndex = 16;
            this.linkRefreshPort.TabStop = true;
            this.linkRefreshPort.Text = "刷新";
            this.linkRefreshPort.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_LinkClicked);
            // 
            // cmbPorts
            // 
            this.cmbPorts.ArrowColor = System.Drawing.Color.Gray;
            this.cmbPorts.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPorts.FormattingEnabled = true;
            this.cmbPorts.Location = new System.Drawing.Point(88, 48);
            this.cmbPorts.Name = "cmbPorts";
            this.cmbPorts.Size = new System.Drawing.Size(115, 23);
            this.cmbPorts.TabIndex = 2;
            this.cmbPorts.SelectedValueChanged += new System.EventHandler(this.cmbPorts_SelectedValueChanged);
            // 
            // lblPort
            // 
            this.lblPort.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPort.BackColor = System.Drawing.Color.Transparent;
            this.lblPort.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblPort.BorderWidth = 1F;
            this.lblPort.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblPort.Location = new System.Drawing.Point(20, 48);
            this.lblPort.Name = "lblPort";
            this.lblPort.Radius = 16;
            this.lblPort.ShowBackground = false;
            this.lblPort.ShowBorder = false;
            this.lblPort.Size = new System.Drawing.Size(64, 23);
            this.lblPort.TabIndex = 13;
            this.lblPort.Text = "端口";
            // 
            // txtFirmwarePackageFilePath
            // 
            this.txtFirmwarePackageFilePath.AllowDrop = true;
            this.txtFirmwarePackageFilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtFirmwarePackageFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFirmwarePackageFilePath.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtFirmwarePackageFilePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtFirmwarePackageFilePath.Location = new System.Drawing.Point(88, 116);
            this.txtFirmwarePackageFilePath.Name = "txtFirmwarePackageFilePath";
            this.txtFirmwarePackageFilePath.Size = new System.Drawing.Size(390, 27);
            this.txtFirmwarePackageFilePath.TabIndex = 0;
            this.txtFirmwarePackageFilePath.TextChanged += new System.EventHandler(this.txtFirmwarePackageFilePath_TextChanged);
            this.txtFirmwarePackageFilePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.txtFirmwarePackageFilePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            this.txtFirmwarePackageFilePath.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txt_MouseDoubleClick);
            // 
            // txtFirmwarePackageInfo
            // 
            this.txtFirmwarePackageInfo.AllowDrop = true;
            this.txtFirmwarePackageInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtFirmwarePackageInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFirmwarePackageInfo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtFirmwarePackageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtFirmwarePackageInfo.Location = new System.Drawing.Point(88, 153);
            this.txtFirmwarePackageInfo.Multiline = true;
            this.txtFirmwarePackageInfo.Name = "txtFirmwarePackageInfo";
            this.txtFirmwarePackageInfo.ReadOnly = true;
            this.txtFirmwarePackageInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFirmwarePackageInfo.Size = new System.Drawing.Size(504, 164);
            this.txtFirmwarePackageInfo.TabIndex = 19;
            this.txtFirmwarePackageInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.txtFirmwarePackageInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnUpdate.BorderWidth = 1F;
            this.btnUpdate.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnUpdate.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnUpdate.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnUpdate.Enabled = false;
            this.btnUpdate.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnUpdate.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnUpdate.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnUpdate.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnUpdate.Location = new System.Drawing.Point(496, 114);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnUpdate.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(120)))), ((int)(((byte)(253)))));
            this.btnUpdate.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnUpdate.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnUpdate.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnUpdate.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnUpdate.Radius = 12;
            this.btnUpdate.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.btnUpdate.ShowBorder = true;
            this.btnUpdate.ShowText = true;
            this.btnUpdate.Size = new System.Drawing.Size(96, 27);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "更新";
            this.btnUpdate.Click += new System.EventHandler(this.btn_Click);
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.ArrowColor = System.Drawing.Color.Gray;
            this.cmbBaudRate.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Location = new System.Drawing.Point(323, 48);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(115, 23);
            this.cmbBaudRate.TabIndex = 3;
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBaudRate.BackColor = System.Drawing.Color.Transparent;
            this.lblBaudRate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblBaudRate.BorderWidth = 1F;
            this.lblBaudRate.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblBaudRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblBaudRate.Location = new System.Drawing.Point(255, 48);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Radius = 16;
            this.lblBaudRate.ShowBackground = false;
            this.lblBaudRate.ShowBorder = false;
            this.lblBaudRate.Size = new System.Drawing.Size(64, 23);
            this.lblBaudRate.TabIndex = 26;
            this.lblBaudRate.Text = "波特率";
            // 
            // lblKey
            // 
            this.lblKey.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblKey.BackColor = System.Drawing.Color.Transparent;
            this.lblKey.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblKey.BorderWidth = 1F;
            this.lblKey.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblKey.Location = new System.Drawing.Point(20, 83);
            this.lblKey.Name = "lblKey";
            this.lblKey.Radius = 16;
            this.lblKey.ShowBackground = false;
            this.lblKey.ShowBorder = false;
            this.lblKey.Size = new System.Drawing.Size(64, 23);
            this.lblKey.TabIndex = 40;
            this.lblKey.Text = "密钥";
            // 
            // txtKey
            // 
            this.txtKey.AllowDrop = true;
            this.txtKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKey.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtKey.Location = new System.Drawing.Point(88, 81);
            this.txtKey.Name = "txtKey";
            this.txtKey.ReadOnly = true;
            this.txtKey.Size = new System.Drawing.Size(320, 27);
            this.txtKey.TabIndex = 39;
            // 
            // linkRefreshKey
            // 
            this.linkRefreshKey.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.linkRefreshKey.AutoSize = true;
            this.linkRefreshKey.BackColor = System.Drawing.Color.Transparent;
            this.linkRefreshKey.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkRefreshKey.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.linkRefreshKey.Location = new System.Drawing.Point(414, 87);
            this.linkRefreshKey.Name = "linkRefreshKey";
            this.linkRefreshKey.Size = new System.Drawing.Size(33, 15);
            this.linkRefreshKey.TabIndex = 41;
            this.linkRefreshKey.TabStop = true;
            this.linkRefreshKey.Text = "刷新";
            this.linkRefreshKey.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_LinkClicked);
            // 
            // lblFilePath
            // 
            this.lblFilePath.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFilePath.BackColor = System.Drawing.Color.Transparent;
            this.lblFilePath.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblFilePath.BorderWidth = 1F;
            this.lblFilePath.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblFilePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblFilePath.Location = new System.Drawing.Point(20, 118);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Radius = 16;
            this.lblFilePath.ShowBackground = false;
            this.lblFilePath.ShowBorder = false;
            this.lblFilePath.Size = new System.Drawing.Size(64, 23);
            this.lblFilePath.TabIndex = 42;
            this.lblFilePath.Text = "文件";
            // 
            // lblFirmwarePackageInfo
            // 
            this.lblFirmwarePackageInfo.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFirmwarePackageInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblFirmwarePackageInfo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblFirmwarePackageInfo.BorderWidth = 1F;
            this.lblFirmwarePackageInfo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblFirmwarePackageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblFirmwarePackageInfo.Location = new System.Drawing.Point(20, 154);
            this.lblFirmwarePackageInfo.Name = "lblFirmwarePackageInfo";
            this.lblFirmwarePackageInfo.Radius = 16;
            this.lblFirmwarePackageInfo.ShowBackground = false;
            this.lblFirmwarePackageInfo.ShowBorder = false;
            this.lblFirmwarePackageInfo.Size = new System.Drawing.Size(64, 23);
            this.lblFirmwarePackageInfo.TabIndex = 43;
            this.lblFirmwarePackageInfo.Text = "信息";
            // 
            // lblProgress
            // 
            this.lblProgress.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblProgress.BackColor = System.Drawing.Color.Transparent;
            this.lblProgress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblProgress.BorderWidth = 1F;
            this.lblProgress.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblProgress.Location = new System.Drawing.Point(20, 354);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Radius = 16;
            this.lblProgress.ShowBackground = false;
            this.lblProgress.ShowBorder = false;
            this.lblProgress.Size = new System.Drawing.Size(64, 23);
            this.lblProgress.TabIndex = 44;
            this.lblProgress.Text = "进度";
            // 
            // mProgressBar
            // 
            this.mProgressBar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.mProgressBar.BorderWidth = 1F;
            this.mProgressBar.endColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(181)))), ((int)(((byte)(246)))));
            this.mProgressBar.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mProgressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mProgressBar.isShowProgress = false;
            this.mProgressBar.Location = new System.Drawing.Point(88, 354);
            this.mProgressBar.Name = "mProgressBar";
            this.mProgressBar.progressRatio = 0F;
            this.mProgressBar.Radius = 12;
            this.mProgressBar.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.mProgressBar.ShowBorder = false;
            this.mProgressBar.Size = new System.Drawing.Size(504, 23);
            this.mProgressBar.startColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.mProgressBar.TabIndex = 45;
            // 
            // lblInfo
            // 
            this.lblInfo.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblInfo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblInfo.BorderWidth = 1F;
            this.lblInfo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblInfo.Location = new System.Drawing.Point(88, 329);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Radius = 16;
            this.lblInfo.ShowBackground = false;
            this.lblInfo.ShowBorder = false;
            this.lblInfo.Size = new System.Drawing.Size(504, 23);
            this.lblInfo.TabIndex = 46;
            this.lblInfo.Text = "...";
            // 
            // FormFirmwareUpdate
            // 
            this.ClientSize = new System.Drawing.Size(640, 400);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.mProgressBar);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.lblFirmwarePackageInfo);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.linkRefreshKey);
            this.Controls.Add(this.lblKey);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.cmbBaudRate);
            this.Controls.Add(this.lblBaudRate);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtFirmwarePackageInfo);
            this.Controls.Add(this.txtFirmwarePackageFilePath);
            this.Controls.Add(this.linkRefreshPort);
            this.Controls.Add(this.cmbPorts);
            this.Controls.Add(this.lblPort);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormFirmwareUpdate";
            this.Text = "固件更新";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.Controls.SetChildIndex(this.lblPort, 0);
            this.Controls.SetChildIndex(this.cmbPorts, 0);
            this.Controls.SetChildIndex(this.linkRefreshPort, 0);
            this.Controls.SetChildIndex(this.txtFirmwarePackageFilePath, 0);
            this.Controls.SetChildIndex(this.txtFirmwarePackageInfo, 0);
            this.Controls.SetChildIndex(this.btnUpdate, 0);
            this.Controls.SetChildIndex(this.lblBaudRate, 0);
            this.Controls.SetChildIndex(this.cmbBaudRate, 0);
            this.Controls.SetChildIndex(this.txtKey, 0);
            this.Controls.SetChildIndex(this.lblKey, 0);
            this.Controls.SetChildIndex(this.linkRefreshKey, 0);
            this.Controls.SetChildIndex(this.lblFilePath, 0);
            this.Controls.SetChildIndex(this.lblFirmwarePackageInfo, 0);
            this.Controls.SetChildIndex(this.lblProgress, 0);
            this.Controls.SetChildIndex(this.mProgressBar, 0);
            this.Controls.SetChildIndex(this.lblInfo, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkRefreshPort;
        private gtComboBox cmbPorts;
        private Controls.MLabel lblPort;
        private System.Windows.Forms.TextBox txtFirmwarePackageFilePath;
        private System.Windows.Forms.TextBox txtFirmwarePackageInfo;
        private Controls.MButton btnUpdate;
        private gtComboBox cmbBaudRate;
        private Controls.MLabel lblBaudRate;
        private Controls.MLabel lblKey;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.LinkLabel linkRefreshKey;
        private Controls.MLabel lblFilePath;
        private Controls.MLabel lblFirmwarePackageInfo;
        private Controls.MLabel lblProgress;
        private Controls.MProgressBar mProgressBar;
        private Controls.MLabel lblInfo;
    }
}
