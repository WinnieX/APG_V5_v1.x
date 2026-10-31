namespace AEC.UI.RemoteControl
{
    partial class NetworkConnectionStatus
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
            this.linkLabelReconnect = new System.Windows.Forms.LinkLabel();
            this.btnSettings = new AEC.UI.gtButton();
            this.lblStatus = new AEC.UI.gtLabel();
            this.statusLight = new AEC.UI.RemoteControl.StatusLight();
            this.SuspendLayout();
            // 
            // linkLabelReconnect
            // 
            this.linkLabelReconnect.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(136)))), ((int)(((byte)(209)))));
            this.linkLabelReconnect.AutoSize = true;
            this.linkLabelReconnect.DisabledLinkColor = System.Drawing.Color.Black;
            this.linkLabelReconnect.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.linkLabelReconnect.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabelReconnect.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(169)))), ((int)(((byte)(244)))));
            this.linkLabelReconnect.Location = new System.Drawing.Point(45, 43);
            this.linkLabelReconnect.Name = "linkLabelReconnect";
            this.linkLabelReconnect.Size = new System.Drawing.Size(72, 15);
            this.linkLabelReconnect.TabIndex = 10;
            this.linkLabelReconnect.TabStop = true;
            this.linkLabelReconnect.Text = "重连服务器";
            this.linkLabelReconnect.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelReconnect_LinkClicked);
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnSettings.backDisabled = global::AEC.Properties.Resources.rc_setting_normal;
            this.btnSettings.backHover = global::AEC.Properties.Resources.rc_setting_normal;
            this.btnSettings.backNormal = global::AEC.Properties.Resources.rc_setting_normal;
            this.btnSettings.backPressed = global::AEC.Properties.Resources.rc_setting_normal;
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.isStretch = false;
            this.btnSettings.Location = new System.Drawing.Point(293, 27);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(24, 24);
            this.btnSettings.TabIndex = 11;
            this.btnSettings.userGDIPaint = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.alignment = AEC.UI.gtLabel.AlignMent.Left;
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.lblStatus.Location = new System.Drawing.Point(46, 17);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(244, 23);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "未连接到服务器!";
            // 
            // statusLight
            // 
            this.statusLight.BackColor = System.Drawing.Color.Transparent;
            this.statusLight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.statusLight.Location = new System.Drawing.Point(16, 16);
            this.statusLight.Name = "statusLight";
            this.statusLight.Size = new System.Drawing.Size(24, 24);
            this.statusLight.status = AEC.UI.RemoteControl.StatusLight.Status.Unconnected;
            this.statusLight.TabIndex = 0;
            this.statusLight.Text = "statusLight1";
            // 
            // NetworkConnectionStatus
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.linkLabelReconnect);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.statusLight);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "NetworkConnectionStatus";
            this.Size = new System.Drawing.Size(320, 64);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusLight statusLight;
        private gtLabel lblStatus;
        private System.Windows.Forms.LinkLabel linkLabelReconnect;
        private gtButton btnSettings;
    }
}
