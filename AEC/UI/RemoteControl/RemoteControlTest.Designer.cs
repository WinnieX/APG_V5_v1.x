namespace AEC.UI.RemoteControl
{
    partial class RemoteControlTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteControlTest));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblVC = new AEC.UI.gtLabel();
            this.lblIdentificationCode = new AEC.UI.gtLabel();
            this.txtVC = new System.Windows.Forms.TextBox();
            this.txtIdCode = new System.Windows.Forms.TextBox();
            this.buttonSwapData = new System.Windows.Forms.Button();
            this.buttonQueryServer = new System.Windows.Forms.Button();
            this.btnConnectRemoteClient = new System.Windows.Forms.Button();
            this.gtLabel1 = new AEC.UI.gtLabel();
            this.btnLocalConnectToServer = new System.Windows.Forms.Button();
            this.txtLocalIdCode = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDisconnectRC = new System.Windows.Forms.Button();
            this.btnUpdateVCode = new System.Windows.Forms.Button();
            this.chkAllowed = new System.Windows.Forms.CheckBox();
            this.lblNativeVC = new AEC.UI.gtLabel();
            this.btnRemoteConnectToServer = new System.Windows.Forms.Button();
            this.lblNativeIdentificationCode = new AEC.UI.gtLabel();
            this.txtNativeVC = new System.Windows.Forms.TextBox();
            this.txtNativeIdCode = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblVC);
            this.groupBox1.Controls.Add(this.lblIdentificationCode);
            this.groupBox1.Controls.Add(this.txtVC);
            this.groupBox1.Controls.Add(this.txtIdCode);
            this.groupBox1.Controls.Add(this.buttonSwapData);
            this.groupBox1.Controls.Add(this.buttonQueryServer);
            this.groupBox1.Controls.Add(this.btnConnectRemoteClient);
            this.groupBox1.Controls.Add(this.gtLabel1);
            this.groupBox1.Controls.Add(this.btnLocalConnectToServer);
            this.groupBox1.Controls.Add(this.txtLocalIdCode);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 300);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "本地客户端";
            // 
            // lblVC
            // 
            this.lblVC.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblVC.BackColor = System.Drawing.Color.Transparent;
            this.lblVC.Location = new System.Drawing.Point(167, 82);
            this.lblVC.Name = "lblVC";
            this.lblVC.Size = new System.Drawing.Size(75, 23);
            this.lblVC.TabIndex = 29;
            this.lblVC.Text = "验证码";
            // 
            // lblIdentificationCode
            // 
            this.lblIdentificationCode.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblIdentificationCode.BackColor = System.Drawing.Color.Transparent;
            this.lblIdentificationCode.Location = new System.Drawing.Point(29, 82);
            this.lblIdentificationCode.Name = "lblIdentificationCode";
            this.lblIdentificationCode.Size = new System.Drawing.Size(75, 23);
            this.lblIdentificationCode.TabIndex = 29;
            this.lblIdentificationCode.Text = "识别码";
            // 
            // txtVC
            // 
            this.txtVC.BackColor = System.Drawing.Color.White;
            this.txtVC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVC.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.txtVC.ForeColor = System.Drawing.Color.Black;
            this.txtVC.Location = new System.Drawing.Point(167, 111);
            this.txtVC.MaxLength = 4;
            this.txtVC.Name = "txtVC";
            this.txtVC.Size = new System.Drawing.Size(96, 39);
            this.txtVC.TabIndex = 28;
            this.txtVC.Text = "7777";
            // 
            // txtIdCode
            // 
            this.txtIdCode.BackColor = System.Drawing.Color.White;
            this.txtIdCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIdCode.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.txtIdCode.ForeColor = System.Drawing.Color.Black;
            this.txtIdCode.Location = new System.Drawing.Point(29, 111);
            this.txtIdCode.MaxLength = 6;
            this.txtIdCode.Name = "txtIdCode";
            this.txtIdCode.Size = new System.Drawing.Size(128, 39);
            this.txtIdCode.TabIndex = 28;
            this.txtIdCode.Text = "901445";
            // 
            // buttonSwapData
            // 
            this.buttonSwapData.Enabled = false;
            this.buttonSwapData.Location = new System.Drawing.Point(29, 245);
            this.buttonSwapData.Name = "buttonSwapData";
            this.buttonSwapData.Size = new System.Drawing.Size(234, 32);
            this.buttonSwapData.TabIndex = 0;
            this.buttonSwapData.Text = "交换数据";
            this.buttonSwapData.UseVisualStyleBackColor = true;
            this.buttonSwapData.Click += new System.EventHandler(this.buttonSwapData_Click);
            // 
            // buttonQueryServer
            // 
            this.buttonQueryServer.Enabled = false;
            this.buttonQueryServer.Location = new System.Drawing.Point(29, 207);
            this.buttonQueryServer.Name = "buttonQueryServer";
            this.buttonQueryServer.Size = new System.Drawing.Size(234, 32);
            this.buttonQueryServer.TabIndex = 0;
            this.buttonQueryServer.Text = "查询服务端";
            this.buttonQueryServer.UseVisualStyleBackColor = true;
            this.buttonQueryServer.Click += new System.EventHandler(this.buttonQueryServer_Click);
            // 
            // btnConnectRemoteClient
            // 
            this.btnConnectRemoteClient.Enabled = false;
            this.btnConnectRemoteClient.Location = new System.Drawing.Point(29, 167);
            this.btnConnectRemoteClient.Name = "btnConnectRemoteClient";
            this.btnConnectRemoteClient.Size = new System.Drawing.Size(234, 32);
            this.btnConnectRemoteClient.TabIndex = 0;
            this.btnConnectRemoteClient.Text = "远程控制";
            this.btnConnectRemoteClient.UseVisualStyleBackColor = true;
            this.btnConnectRemoteClient.Click += new System.EventHandler(this.btnRemoteControl_Click);
            // 
            // gtLabel1
            // 
            this.gtLabel1.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.gtLabel1.BackColor = System.Drawing.Color.Transparent;
            this.gtLabel1.Location = new System.Drawing.Point(179, 6);
            this.gtLabel1.Name = "gtLabel1";
            this.gtLabel1.Size = new System.Drawing.Size(75, 23);
            this.gtLabel1.TabIndex = 29;
            this.gtLabel1.Text = "本机识别码";
            // 
            // btnLocalConnectToServer
            // 
            this.btnLocalConnectToServer.Location = new System.Drawing.Point(29, 33);
            this.btnLocalConnectToServer.Name = "btnLocalConnectToServer";
            this.btnLocalConnectToServer.Size = new System.Drawing.Size(117, 32);
            this.btnLocalConnectToServer.TabIndex = 0;
            this.btnLocalConnectToServer.Text = "连接服务器";
            this.btnLocalConnectToServer.UseVisualStyleBackColor = true;
            this.btnLocalConnectToServer.Click += new System.EventHandler(this.btnConnectDevice_Click);
            // 
            // txtLocalIdCode
            // 
            this.txtLocalIdCode.BackColor = System.Drawing.SystemColors.Control;
            this.txtLocalIdCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLocalIdCode.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocalIdCode.ForeColor = System.Drawing.Color.Black;
            this.txtLocalIdCode.Location = new System.Drawing.Point(179, 33);
            this.txtLocalIdCode.MaxLength = 6;
            this.txtLocalIdCode.Name = "txtLocalIdCode";
            this.txtLocalIdCode.Size = new System.Drawing.Size(84, 32);
            this.txtLocalIdCode.TabIndex = 28;
            this.txtLocalIdCode.Text = "------";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDisconnectRC);
            this.groupBox2.Controls.Add(this.btnUpdateVCode);
            this.groupBox2.Controls.Add(this.chkAllowed);
            this.groupBox2.Controls.Add(this.lblNativeVC);
            this.groupBox2.Controls.Add(this.btnRemoteConnectToServer);
            this.groupBox2.Controls.Add(this.lblNativeIdentificationCode);
            this.groupBox2.Controls.Add(this.txtNativeVC);
            this.groupBox2.Controls.Add(this.txtNativeIdCode);
            this.groupBox2.Location = new System.Drawing.Point(315, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(300, 300);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "远程客户端";
            // 
            // btnDisconnectRC
            // 
            this.btnDisconnectRC.Location = new System.Drawing.Point(25, 207);
            this.btnDisconnectRC.Name = "btnDisconnectRC";
            this.btnDisconnectRC.Size = new System.Drawing.Size(231, 31);
            this.btnDisconnectRC.TabIndex = 31;
            this.btnDisconnectRC.Text = "断开远程控制";
            this.btnDisconnectRC.UseVisualStyleBackColor = true;
            this.btnDisconnectRC.Click += new System.EventHandler(this.btnDisconnectRC_Click);
            // 
            // btnUpdateVCode
            // 
            this.btnUpdateVCode.Location = new System.Drawing.Point(143, 169);
            this.btnUpdateVCode.Name = "btnUpdateVCode";
            this.btnUpdateVCode.Size = new System.Drawing.Size(113, 30);
            this.btnUpdateVCode.TabIndex = 31;
            this.btnUpdateVCode.Text = "更新验证码";
            this.btnUpdateVCode.UseVisualStyleBackColor = true;
            this.btnUpdateVCode.Click += new System.EventHandler(this.btnUpdateVCode_Click);
            // 
            // chkAllowed
            // 
            this.chkAllowed.AutoSize = true;
            this.chkAllowed.Checked = true;
            this.chkAllowed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllowed.Location = new System.Drawing.Point(155, 41);
            this.chkAllowed.Name = "chkAllowed";
            this.chkAllowed.Size = new System.Drawing.Size(130, 19);
            this.chkAllowed.TabIndex = 30;
            this.chkAllowed.Text = "允许被被远程控制";
            this.chkAllowed.UseVisualStyleBackColor = true;
            this.chkAllowed.CheckedChanged += new System.EventHandler(this.chkAllowed_CheckedChanged);
            // 
            // lblNativeVC
            // 
            this.lblNativeVC.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblNativeVC.BackColor = System.Drawing.Color.Transparent;
            this.lblNativeVC.Location = new System.Drawing.Point(21, 145);
            this.lblNativeVC.Name = "lblNativeVC";
            this.lblNativeVC.Size = new System.Drawing.Size(75, 23);
            this.lblNativeVC.TabIndex = 29;
            this.lblNativeVC.Text = "本机验证码";
            // 
            // btnRemoteConnectToServer
            // 
            this.btnRemoteConnectToServer.Location = new System.Drawing.Point(21, 33);
            this.btnRemoteConnectToServer.Name = "btnRemoteConnectToServer";
            this.btnRemoteConnectToServer.Size = new System.Drawing.Size(117, 32);
            this.btnRemoteConnectToServer.TabIndex = 0;
            this.btnRemoteConnectToServer.Text = "连接服务器";
            this.btnRemoteConnectToServer.UseVisualStyleBackColor = true;
            this.btnRemoteConnectToServer.Click += new System.EventHandler(this.btnConnectDevice_Click);
            // 
            // lblNativeIdentificationCode
            // 
            this.lblNativeIdentificationCode.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblNativeIdentificationCode.BackColor = System.Drawing.Color.Transparent;
            this.lblNativeIdentificationCode.Location = new System.Drawing.Point(21, 82);
            this.lblNativeIdentificationCode.Name = "lblNativeIdentificationCode";
            this.lblNativeIdentificationCode.Size = new System.Drawing.Size(75, 23);
            this.lblNativeIdentificationCode.TabIndex = 29;
            this.lblNativeIdentificationCode.Text = "本机识别码";
            // 
            // txtNativeVC
            // 
            this.txtNativeVC.BackColor = System.Drawing.SystemColors.Control;
            this.txtNativeVC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNativeVC.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNativeVC.ForeColor = System.Drawing.Color.Black;
            this.txtNativeVC.Location = new System.Drawing.Point(21, 169);
            this.txtNativeVC.MaxLength = 4;
            this.txtNativeVC.Name = "txtNativeVC";
            this.txtNativeVC.Size = new System.Drawing.Size(84, 32);
            this.txtNativeVC.TabIndex = 28;
            this.txtNativeVC.Text = "----";
            // 
            // txtNativeIdCode
            // 
            this.txtNativeIdCode.BackColor = System.Drawing.SystemColors.Control;
            this.txtNativeIdCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNativeIdCode.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNativeIdCode.ForeColor = System.Drawing.Color.Black;
            this.txtNativeIdCode.Location = new System.Drawing.Point(21, 111);
            this.txtNativeIdCode.MaxLength = 6;
            this.txtNativeIdCode.Name = "txtNativeIdCode";
            this.txtNativeIdCode.Size = new System.Drawing.Size(84, 32);
            this.txtNativeIdCode.TabIndex = 28;
            this.txtNativeIdCode.Text = "------";
            // 
            // RemoteControlTest
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(624, 321);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RemoteControlTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "远程控制测试";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLocalConnectToServer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRemoteConnectToServer;
        private gtLabel lblVC;
        private gtLabel lblIdentificationCode;
        private System.Windows.Forms.TextBox txtVC;
        private System.Windows.Forms.TextBox txtIdCode;
        private System.Windows.Forms.Button btnConnectRemoteClient;
        private gtLabel lblNativeVC;
        private gtLabel lblNativeIdentificationCode;
        private System.Windows.Forms.TextBox txtNativeVC;
        private System.Windows.Forms.TextBox txtNativeIdCode;
        private System.Windows.Forms.Button btnUpdateVCode;
        private System.Windows.Forms.CheckBox chkAllowed;
        private System.Windows.Forms.Button btnDisconnectRC;
        private gtLabel gtLabel1;
        private System.Windows.Forms.TextBox txtLocalIdCode;
        private System.Windows.Forms.Button buttonQueryServer;
        private System.Windows.Forms.Button buttonSwapData;
    }
}