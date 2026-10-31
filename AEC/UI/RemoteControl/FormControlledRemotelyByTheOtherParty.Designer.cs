namespace AEC.UI.RemoteControl
{
    partial class FormControlledRemotelyByTheOtherParty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormControlledRemotelyByTheOtherParty));
            this.title = new AEC.UI.Title();
            this.lblLocalId = new AEC.UI.gtLabel();
            this.txtLocalIdCode = new System.Windows.Forms.TextBox();
            this.lblLocalVCode = new AEC.UI.gtLabel();
            this.txtLocalVCode = new System.Windows.Forms.TextBox();
            this.networkConnectionStatus = new AEC.UI.RemoteControl.NetworkConnectionStatus();
            this.splitLine1 = new AEC.UI.SplitLine();
            this.chkAllowToBeControlled = new AEC.UI.gtCheckBox();
            this.chkDisconnect = new AEC.UI.gtCheckBox();
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
            this.title.TabIndex = 7;
            // 
            // lblLocalId
            // 
            this.lblLocalId.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblLocalId.BackColor = System.Drawing.Color.Transparent;
            this.lblLocalId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.lblLocalId.Location = new System.Drawing.Point(29, 116);
            this.lblLocalId.Name = "lblLocalId";
            this.lblLocalId.Size = new System.Drawing.Size(128, 23);
            this.lblLocalId.TabIndex = 8;
            this.lblLocalId.Text = "本机识别码";
            // 
            // txtLocalIdCode
            // 
            this.txtLocalIdCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtLocalIdCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLocalIdCode.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtLocalIdCode.ForeColor = System.Drawing.Color.White;
            this.txtLocalIdCode.Location = new System.Drawing.Point(170, 109);
            this.txtLocalIdCode.MaxLength = 6;
            this.txtLocalIdCode.Name = "txtLocalIdCode";
            this.txtLocalIdCode.ReadOnly = true;
            this.txtLocalIdCode.Size = new System.Drawing.Size(128, 32);
            this.txtLocalIdCode.TabIndex = 29;
            this.txtLocalIdCode.Text = "------";
            // 
            // lblLocalVCode
            // 
            this.lblLocalVCode.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblLocalVCode.BackColor = System.Drawing.Color.Transparent;
            this.lblLocalVCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.lblLocalVCode.Location = new System.Drawing.Point(29, 153);
            this.lblLocalVCode.Name = "lblLocalVCode";
            this.lblLocalVCode.Size = new System.Drawing.Size(128, 23);
            this.lblLocalVCode.TabIndex = 8;
            this.lblLocalVCode.Text = "本机验证码";
            // 
            // txtLocalVCode
            // 
            this.txtLocalVCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.txtLocalVCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLocalVCode.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtLocalVCode.ForeColor = System.Drawing.Color.White;
            this.txtLocalVCode.Location = new System.Drawing.Point(170, 148);
            this.txtLocalVCode.MaxLength = 4;
            this.txtLocalVCode.Name = "txtLocalVCode";
            this.txtLocalVCode.ReadOnly = true;
            this.txtLocalVCode.Size = new System.Drawing.Size(128, 27);
            this.txtLocalVCode.TabIndex = 29;
            this.txtLocalVCode.Text = "----";
            // 
            // networkConnectionStatus
            // 
            this.networkConnectionStatus.BackColor = System.Drawing.Color.Transparent;
            this.networkConnectionStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.networkConnectionStatus.Location = new System.Drawing.Point(20, 36);
            this.networkConnectionStatus.Name = "networkConnectionStatus";
            this.networkConnectionStatus.Size = new System.Drawing.Size(320, 64);
            this.networkConnectionStatus.TabIndex = 32;
            // 
            // splitLine1
            // 
            this.splitLine1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.splitLine1.Location = new System.Drawing.Point(20, 104);
            this.splitLine1.Margin = new System.Windows.Forms.Padding(0);
            this.splitLine1.Name = "splitLine1";
            this.splitLine1.Size = new System.Drawing.Size(320, 1);
            this.splitLine1.TabIndex = 33;
            this.splitLine1.Text = "splitLine1";
            // 
            // chkAllowToBeControlled
            // 
            this.chkAllowToBeControlled.BackColor = System.Drawing.Color.Transparent;
            this.chkAllowToBeControlled.backDisabled = global::AEC.Properties.Resources.rta_graph_disabled;
            this.chkAllowToBeControlled.backHover = global::AEC.Properties.Resources.rta_graph_hover;
            this.chkAllowToBeControlled.backNormal = global::AEC.Properties.Resources.rta_graph_normal;
            this.chkAllowToBeControlled.backPressed = global::AEC.Properties.Resources.rta_graph_checked;
            this.chkAllowToBeControlled.ForeColor = System.Drawing.Color.White;
            this.chkAllowToBeControlled.isAutoCheck = true;
            this.chkAllowToBeControlled.isChecked = false;
            this.chkAllowToBeControlled.isShowCheckedBackWhenDsiabled = true;
            this.chkAllowToBeControlled.isStretch = true;
            this.chkAllowToBeControlled.Location = new System.Drawing.Point(98, 251);
            this.chkAllowToBeControlled.Name = "chkAllowToBeControlled";
            this.chkAllowToBeControlled.Size = new System.Drawing.Size(164, 32);
            this.chkAllowToBeControlled.TabIndex = 1;
            this.chkAllowToBeControlled.Text = "允许被远程控制";
            this.chkAllowToBeControlled.userGDIPaint = false;
            this.chkAllowToBeControlled.CheckedChanged += new System.EventHandler<System.EventArgs>(this.chkAllowToBeControlled_CheckedChanged);
            // 
            // chkDisconnect
            // 
            this.chkDisconnect.BackColor = System.Drawing.Color.Transparent;
            this.chkDisconnect.backDisabled = global::AEC.Properties.Resources.rta_graph_disabled;
            this.chkDisconnect.backHover = global::AEC.Properties.Resources.rta_graph_hover;
            this.chkDisconnect.backNormal = global::AEC.Properties.Resources.rta_graph_normal;
            this.chkDisconnect.backPressed = global::AEC.Properties.Resources.rta_graph_checked;
            this.chkDisconnect.ForeColor = System.Drawing.Color.White;
            this.chkDisconnect.isAutoCheck = false;
            this.chkDisconnect.isChecked = false;
            this.chkDisconnect.isShowCheckedBackWhenDsiabled = true;
            this.chkDisconnect.isStretch = false;
            this.chkDisconnect.Location = new System.Drawing.Point(106, 198);
            this.chkDisconnect.Name = "chkDisconnect";
            this.chkDisconnect.Size = new System.Drawing.Size(148, 32);
            this.chkDisconnect.TabIndex = 34;
            this.chkDisconnect.Text = "断开远程连接";
            this.chkDisconnect.userGDIPaint = false;
            this.chkDisconnect.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chkDisconnect_MouseClick);
            // 
            // FormControlledRemotelyByTheOtherParty
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(360, 320);
            this.Controls.Add(this.chkDisconnect);
            this.Controls.Add(this.chkAllowToBeControlled);
            this.Controls.Add(this.splitLine1);
            this.Controls.Add(this.networkConnectionStatus);
            this.Controls.Add(this.txtLocalVCode);
            this.Controls.Add(this.txtLocalIdCode);
            this.Controls.Add(this.lblLocalVCode);
            this.Controls.Add(this.lblLocalId);
            this.Controls.Add(this.title);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormControlledRemotelyByTheOtherParty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "请求对方远程控制本机";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormControlledRemotelyByTheOtherParty_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormControlledRemotelyByTheOtherParty_FormClosed);
            this.Load += new System.EventHandler(this.FormControlledRemotelyByTheOtherParty_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Title title;
        private gtLabel lblLocalId;
        private System.Windows.Forms.TextBox txtLocalIdCode;
        private gtLabel lblLocalVCode;
        private System.Windows.Forms.TextBox txtLocalVCode;
        private NetworkConnectionStatus networkConnectionStatus;
        private SplitLine splitLine1;
        private gtCheckBox chkAllowToBeControlled;
        private gtCheckBox chkDisconnect;
    }
}