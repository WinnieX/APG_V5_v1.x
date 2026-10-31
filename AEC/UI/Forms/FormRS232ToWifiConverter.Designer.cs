namespace AEC.UI.Forms
{
    partial class FormRS232ToWifiConverter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRS232ToWifiConverter));
            this.lblDeviceIP = new AEC.UI.Controls.MLabel();
            this.lblDeviceInfo = new AEC.UI.Controls.MLabel();
            this.txtDeviceBasicInfo = new System.Windows.Forms.TextBox();
            this.btnUpdateConfig = new AEC.UI.Controls.MButton();
            this.linkRefreshPort = new System.Windows.Forms.LinkLabel();
            this.lblBautRate = new AEC.UI.Controls.MLabel();
            this.linkLabelTips = new System.Windows.Forms.LinkLabel();
            this.cmbDeviceIP = new AEC.UI.gtComboBox();
            this.cmbBaudRate = new AEC.UI.gtComboBox();
            this.SuspendLayout();
            // 
            // lblDeviceIP
            // 
            this.lblDeviceIP.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDeviceIP.BackColor = System.Drawing.Color.Transparent;
            this.lblDeviceIP.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblDeviceIP.BorderWidth = 1F;
            this.lblDeviceIP.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblDeviceIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblDeviceIP.Location = new System.Drawing.Point(28, 48);
            this.lblDeviceIP.Name = "lblDeviceIP";
            this.lblDeviceIP.Radius = 16;
            this.lblDeviceIP.ShowBackground = false;
            this.lblDeviceIP.ShowBorder = false;
            this.lblDeviceIP.Size = new System.Drawing.Size(64, 23);
            this.lblDeviceIP.TabIndex = 17;
            this.lblDeviceIP.Text = "设备IP";
            // 
            // lblDeviceInfo
            // 
            this.lblDeviceInfo.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDeviceInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblDeviceInfo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblDeviceInfo.BorderWidth = 1F;
            this.lblDeviceInfo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblDeviceInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblDeviceInfo.Location = new System.Drawing.Point(28, 84);
            this.lblDeviceInfo.Name = "lblDeviceInfo";
            this.lblDeviceInfo.Radius = 16;
            this.lblDeviceInfo.ShowBackground = false;
            this.lblDeviceInfo.ShowBorder = false;
            this.lblDeviceInfo.Size = new System.Drawing.Size(64, 23);
            this.lblDeviceInfo.TabIndex = 20;
            this.lblDeviceInfo.Text = "基本信息";
            // 
            // txtDeviceBasicInfo
            // 
            this.txtDeviceBasicInfo.AllowDrop = true;
            this.txtDeviceBasicInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.txtDeviceBasicInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeviceBasicInfo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtDeviceBasicInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtDeviceBasicInfo.Location = new System.Drawing.Point(96, 84);
            this.txtDeviceBasicInfo.Multiline = true;
            this.txtDeviceBasicInfo.Name = "txtDeviceBasicInfo";
            this.txtDeviceBasicInfo.ReadOnly = true;
            this.txtDeviceBasicInfo.Size = new System.Drawing.Size(481, 185);
            this.txtDeviceBasicInfo.TabIndex = 40;
            // 
            // btnUpdateConfig
            // 
            this.btnUpdateConfig.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnUpdateConfig.BorderWidth = 1F;
            this.btnUpdateConfig.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnUpdateConfig.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnUpdateConfig.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnUpdateConfig.Enabled = false;
            this.btnUpdateConfig.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnUpdateConfig.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnUpdateConfig.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnUpdateConfig.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnUpdateConfig.Location = new System.Drawing.Point(243, 307);
            this.btnUpdateConfig.Name = "btnUpdateConfig";
            this.btnUpdateConfig.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnUpdateConfig.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnUpdateConfig.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnUpdateConfig.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnUpdateConfig.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnUpdateConfig.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnUpdateConfig.Radius = 12;
            this.btnUpdateConfig.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.btnUpdateConfig.ShowBorder = true;
            this.btnUpdateConfig.ShowText = true;
            this.btnUpdateConfig.Size = new System.Drawing.Size(64, 23);
            this.btnUpdateConfig.TabIndex = 54;
            this.btnUpdateConfig.Text = "更新";
            this.btnUpdateConfig.Click += new System.EventHandler(this.btnUpdateConfig_Click);
            // 
            // linkRefreshPort
            // 
            this.linkRefreshPort.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.linkRefreshPort.AutoSize = true;
            this.linkRefreshPort.BackColor = System.Drawing.Color.Transparent;
            this.linkRefreshPort.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkRefreshPort.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.linkRefreshPort.Location = new System.Drawing.Point(228, 52);
            this.linkRefreshPort.Name = "linkRefreshPort";
            this.linkRefreshPort.Size = new System.Drawing.Size(33, 15);
            this.linkRefreshPort.TabIndex = 60;
            this.linkRefreshPort.TabStop = true;
            this.linkRefreshPort.Text = "刷新";
            this.linkRefreshPort.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRefreshPort_LinkClicked);
            // 
            // lblBautRate
            // 
            this.lblBautRate.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBautRate.BackColor = System.Drawing.Color.Transparent;
            this.lblBautRate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblBautRate.BorderWidth = 1F;
            this.lblBautRate.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblBautRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblBautRate.Location = new System.Drawing.Point(28, 307);
            this.lblBautRate.Name = "lblBautRate";
            this.lblBautRate.Radius = 16;
            this.lblBautRate.ShowBackground = false;
            this.lblBautRate.ShowBorder = false;
            this.lblBautRate.Size = new System.Drawing.Size(64, 23);
            this.lblBautRate.TabIndex = 61;
            this.lblBautRate.Text = "波特率";
            this.lblBautRate.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lblBautRate_MouseDoubleClick);
            // 
            // linkLabelTips
            // 
            this.linkLabelTips.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.linkLabelTips.AutoSize = true;
            this.linkLabelTips.BackColor = System.Drawing.Color.Transparent;
            this.linkLabelTips.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.linkLabelTips.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelTips.LinkColor = System.Drawing.Color.Red;
            this.linkLabelTips.Location = new System.Drawing.Point(93, 276);
            this.linkLabelTips.Name = "linkLabelTips";
            this.linkLabelTips.Size = new System.Drawing.Size(0, 17);
            this.linkLabelTips.TabIndex = 63;
            this.linkLabelTips.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelTips_LinkClicked);
            // 
            // cmbDeviceIP
            // 
            this.cmbDeviceIP.ArrowColor = System.Drawing.Color.Gray;
            this.cmbDeviceIP.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbDeviceIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeviceIP.FormattingEnabled = true;
            this.cmbDeviceIP.Location = new System.Drawing.Point(96, 48);
            this.cmbDeviceIP.Name = "cmbDeviceIP";
            this.cmbDeviceIP.Size = new System.Drawing.Size(128, 23);
            this.cmbDeviceIP.TabIndex = 64;
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.ArrowColor = System.Drawing.Color.Gray;
            this.cmbBaudRate.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBaudRate.Enabled = false;
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Location = new System.Drawing.Point(96, 307);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(128, 23);
            this.cmbBaudRate.TabIndex = 65;
            // 
            // FormRS232ToWifiConverter
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(640, 360);
            this.Controls.Add(this.cmbBaudRate);
            this.Controls.Add(this.cmbDeviceIP);
            this.Controls.Add(this.linkLabelTips);
            this.Controls.Add(this.btnUpdateConfig);
            this.Controls.Add(this.lblBautRate);
            this.Controls.Add(this.linkRefreshPort);
            this.Controls.Add(this.txtDeviceBasicInfo);
            this.Controls.Add(this.lblDeviceInfo);
            this.Controls.Add(this.lblDeviceIP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormRS232ToWifiConverter";
            this.Text = "RS232转WiFi";
            this.Controls.SetChildIndex(this.title, 0);
            this.Controls.SetChildIndex(this.lblDeviceIP, 0);
            this.Controls.SetChildIndex(this.lblDeviceInfo, 0);
            this.Controls.SetChildIndex(this.txtDeviceBasicInfo, 0);
            this.Controls.SetChildIndex(this.linkRefreshPort, 0);
            this.Controls.SetChildIndex(this.lblBautRate, 0);
            this.Controls.SetChildIndex(this.btnUpdateConfig, 0);
            this.Controls.SetChildIndex(this.linkLabelTips, 0);
            this.Controls.SetChildIndex(this.cmbDeviceIP, 0);
            this.Controls.SetChildIndex(this.cmbBaudRate, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MLabel lblDeviceIP;
        private Controls.MLabel lblDeviceInfo;
        private System.Windows.Forms.TextBox txtDeviceBasicInfo;
        private Controls.MButton btnUpdateConfig;
        private System.Windows.Forms.LinkLabel linkRefreshPort;
        private Controls.MLabel lblBautRate;
        private System.Windows.Forms.LinkLabel linkLabelTips;
        private gtComboBox cmbDeviceIP;
        private gtComboBox cmbBaudRate;
    }
}
