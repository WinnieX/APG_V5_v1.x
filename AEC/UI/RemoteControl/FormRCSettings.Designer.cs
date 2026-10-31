namespace AEC.UI.RemoteControl
{
    partial class FormRCSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRCSettings));
            this.title = new AEC.UI.Title();
            this.lblPort = new AEC.UI.gtLabel();
            this.lblIPAddress = new AEC.UI.gtLabel();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.btnRestoreToDefault = new AEC.UI.gtButton();
            this.btnClose = new AEC.UI.gtButton();
            this.lblIPError = new AEC.UI.gtLabel();
            this.lblPortError = new AEC.UI.gtLabel();
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
            this.title.Size = new System.Drawing.Size(384, 32);
            this.title.TabIndex = 7;
            // 
            // lblPort
            // 
            this.lblPort.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblPort.BackColor = System.Drawing.Color.Transparent;
            this.lblPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblPort.Location = new System.Drawing.Point(48, 113);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(83, 23);
            this.lblPort.TabIndex = 10;
            this.lblPort.Text = "端口";
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblIPAddress.BackColor = System.Drawing.Color.Transparent;
            this.lblIPAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblIPAddress.Location = new System.Drawing.Point(12, 56);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(119, 23);
            this.lblIPAddress.TabIndex = 11;
            this.lblIPAddress.Text = "服务器IP地址";
            // 
            // txtPort
            // 
            this.txtPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPort.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtPort.Location = new System.Drawing.Point(137, 113);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(67, 27);
            this.txtPort.TabIndex = 8;
            this.txtPort.Text = "8889";
            this.txtPort.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIPAddress.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtIPAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtIPAddress.Location = new System.Drawing.Point(137, 52);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(124, 27);
            this.txtIPAddress.TabIndex = 9;
            this.txtIPAddress.Text = "192.168.8.1";
            this.txtIPAddress.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // btnRestoreToDefault
            // 
            this.btnRestoreToDefault.BackColor = System.Drawing.Color.Transparent;
            this.btnRestoreToDefault.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.btnRestoreToDefault.backHover = global::AEC.Properties.Resources.btn_hover;
            this.btnRestoreToDefault.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.btnRestoreToDefault.backPressed = global::AEC.Properties.Resources.btn_pressed;
            this.btnRestoreToDefault.ForeColor = System.Drawing.Color.White;
            this.btnRestoreToDefault.isStretch = true;
            this.btnRestoreToDefault.Location = new System.Drawing.Point(64, 184);
            this.btnRestoreToDefault.Name = "btnRestoreToDefault";
            this.btnRestoreToDefault.Size = new System.Drawing.Size(118, 30);
            this.btnRestoreToDefault.TabIndex = 12;
            this.btnRestoreToDefault.Text = "恢复默认";
            this.btnRestoreToDefault.userGDIPaint = true;
            this.btnRestoreToDefault.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.btnClose.backHover = global::AEC.Properties.Resources.btn_hover;
            this.btnClose.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.btnClose.backPressed = global::AEC.Properties.Resources.btn_pressed;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.isStretch = true;
            this.btnClose.Location = new System.Drawing.Point(202, 184);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(118, 30);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "关闭";
            this.btnClose.userGDIPaint = true;
            this.btnClose.Click += new System.EventHandler(this.btn_Click);
            // 
            // lblIPError
            // 
            this.lblIPError.alignment = AEC.UI.gtLabel.AlignMent.Left;
            this.lblIPError.BackColor = System.Drawing.Color.Transparent;
            this.lblIPError.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblIPError.ForeColor = System.Drawing.Color.Red;
            this.lblIPError.Location = new System.Drawing.Point(137, 85);
            this.lblIPError.Name = "lblIPError";
            this.lblIPError.Size = new System.Drawing.Size(203, 23);
            this.lblIPError.TabIndex = 11;
            this.lblIPError.Text = "请输入正确的IP地址！";
            this.lblIPError.Visible = false;
            // 
            // lblPortError
            // 
            this.lblPortError.alignment = AEC.UI.gtLabel.AlignMent.Left;
            this.lblPortError.BackColor = System.Drawing.Color.Transparent;
            this.lblPortError.ForeColor = System.Drawing.Color.Red;
            this.lblPortError.Location = new System.Drawing.Point(137, 141);
            this.lblPortError.Name = "lblPortError";
            this.lblPortError.Size = new System.Drawing.Size(203, 23);
            this.lblPortError.TabIndex = 11;
            this.lblPortError.Text = "请输入正确的端口号！";
            this.lblPortError.Visible = false;
            // 
            // FormRCSettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(384, 256);
            this.Controls.Add(this.btnRestoreToDefault);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblPortError);
            this.Controls.Add(this.lblIPError);
            this.Controls.Add(this.lblIPAddress);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIPAddress);
            this.Controls.Add(this.title);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormRCSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormRCSettings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormRCSettings_FormClosed);
            this.Load += new System.EventHandler(this.FormRCSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Title title;
        private gtLabel lblPort;
        private gtLabel lblIPAddress;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIPAddress;
        private gtButton btnRestoreToDefault;
        private gtButton btnClose;
        private gtLabel lblIPError;
        private gtLabel lblPortError;
    }
}