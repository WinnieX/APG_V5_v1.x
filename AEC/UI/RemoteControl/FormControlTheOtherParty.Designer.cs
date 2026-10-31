namespace AEC.UI.RemoteControl
{
    partial class FormControlTheOtherParty
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormControlTheOtherParty));
            this.title = new AEC.UI.Title();
            this.txtRemoteIdCode = new System.Windows.Forms.TextBox();
            this.lblRemoteVCode = new AEC.UI.gtLabel();
            this.lblRemoteId = new AEC.UI.gtLabel();
            this.txtRemoteVCode = new System.Windows.Forms.TextBox();
            this.splitLine1 = new AEC.UI.SplitLine();
            this.networkConnectionStatus = new AEC.UI.RemoteControl.NetworkConnectionStatus();
            this.chkRCConnect = new AEC.UI.gtCheckBox();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.title.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.isAllowDoubleClickMaximize = false;
            this.title.isShowCloseButton = true;
            this.title.isShowSystemBtns = false;
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Margin = new System.Windows.Forms.Padding(0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(360, 32);
            this.title.TabIndex = 6;
            // 
            // txtRemoteIdCode
            // 
            this.txtRemoteIdCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtRemoteIdCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemoteIdCode.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtRemoteIdCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtRemoteIdCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtRemoteIdCode.Location = new System.Drawing.Point(160, 126);
            this.txtRemoteIdCode.MaxLength = 6;
            this.txtRemoteIdCode.Name = "txtRemoteIdCode";
            this.txtRemoteIdCode.Size = new System.Drawing.Size(128, 34);
            this.txtRemoteIdCode.TabIndex = 0;
            // 
            // lblRemoteVCode
            // 
            this.lblRemoteVCode.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblRemoteVCode.BackColor = System.Drawing.Color.Transparent;
            this.lblRemoteVCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.lblRemoteVCode.Location = new System.Drawing.Point(8, 177);
            this.lblRemoteVCode.Name = "lblRemoteVCode";
            this.lblRemoteVCode.Size = new System.Drawing.Size(148, 23);
            this.lblRemoteVCode.TabIndex = 36;
            this.lblRemoteVCode.Text = "对方验证码";
            // 
            // lblRemoteId
            // 
            this.lblRemoteId.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblRemoteId.BackColor = System.Drawing.Color.Transparent;
            this.lblRemoteId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.lblRemoteId.Location = new System.Drawing.Point(28, 135);
            this.lblRemoteId.Name = "lblRemoteId";
            this.lblRemoteId.Size = new System.Drawing.Size(128, 23);
            this.lblRemoteId.TabIndex = 37;
            this.lblRemoteId.Text = "对方识别码";
            // 
            // txtRemoteVCode
            // 
            this.txtRemoteVCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtRemoteVCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRemoteVCode.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtRemoteVCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtRemoteVCode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtRemoteVCode.Location = new System.Drawing.Point(160, 171);
            this.txtRemoteVCode.MaxLength = 4;
            this.txtRemoteVCode.Name = "txtRemoteVCode";
            this.txtRemoteVCode.Size = new System.Drawing.Size(128, 34);
            this.txtRemoteVCode.TabIndex = 1;
            // 
            // splitLine1
            // 
            this.splitLine1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.splitLine1.Location = new System.Drawing.Point(20, 107);
            this.splitLine1.Margin = new System.Windows.Forms.Padding(0);
            this.splitLine1.Name = "splitLine1";
            this.splitLine1.Size = new System.Drawing.Size(320, 1);
            this.splitLine1.TabIndex = 40;
            this.splitLine1.Text = "splitLine1";
            // 
            // networkConnectionStatus
            // 
            this.networkConnectionStatus.BackColor = System.Drawing.Color.Transparent;
            this.networkConnectionStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.networkConnectionStatus.Location = new System.Drawing.Point(20, 39);
            this.networkConnectionStatus.Name = "networkConnectionStatus";
            this.networkConnectionStatus.Size = new System.Drawing.Size(320, 64);
            this.networkConnectionStatus.TabIndex = 39;
            // 
            // chkRCConnect
            // 
            this.chkRCConnect.BackColor = System.Drawing.Color.Transparent;
            this.chkRCConnect.backDisabled = global::AEC.Properties.Resources.rta_graph_disabled;
            this.chkRCConnect.backHover = global::AEC.Properties.Resources.rta_graph_hover;
            this.chkRCConnect.backNormal = global::AEC.Properties.Resources.rta_graph_normal;
            this.chkRCConnect.backPressed = global::AEC.Properties.Resources.rta_graph_checked;
            this.chkRCConnect.ForeColor = System.Drawing.Color.White;
            this.chkRCConnect.isAutoCheck = false;
            this.chkRCConnect.isChecked = false;
            this.chkRCConnect.isShowCheckedBackWhenDsiabled = true;
            this.chkRCConnect.isStretch = false;
            this.chkRCConnect.Location = new System.Drawing.Point(108, 241);
            this.chkRCConnect.Name = "chkRCConnect";
            this.chkRCConnect.Size = new System.Drawing.Size(148, 32);
            this.chkRCConnect.TabIndex = 41;
            this.chkRCConnect.Text = "断开远程连接";
            this.chkRCConnect.userGDIPaint = false;
            this.chkRCConnect.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chkDisconnect_MouseClick);
            // 
            // FormControlTheOtherParty
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(360, 320);
            this.Controls.Add(this.chkRCConnect);
            this.Controls.Add(this.splitLine1);
            this.Controls.Add(this.networkConnectionStatus);
            this.Controls.Add(this.lblRemoteVCode);
            this.Controls.Add(this.lblRemoteId);
            this.Controls.Add(this.txtRemoteVCode);
            this.Controls.Add(this.txtRemoteIdCode);
            this.Controls.Add(this.title);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormControlTheOtherParty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "控制远程设备";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormControlTheOtherParty_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormControlTheOtherParty_FormClosed);
            this.Load += new System.EventHandler(this.FormControlTheOtherParty_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Title title;
        private System.Windows.Forms.TextBox txtRemoteIdCode;
        private gtLabel lblRemoteVCode;
        private gtLabel lblRemoteId;
        private System.Windows.Forms.TextBox txtRemoteVCode;
        private SplitLine splitLine1;
        private NetworkConnectionStatus networkConnectionStatus;
        private gtCheckBox chkRCConnect;
    }
}