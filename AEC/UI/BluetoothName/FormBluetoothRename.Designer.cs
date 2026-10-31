namespace AEC.UI.BluetoothName
{
    partial class FormBluetoothRename
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
            this.lblBluetoothName = new AEC.UI.Controls.MLabel();
            this.txtBluetoothName = new System.Windows.Forms.TextBox();
            this.btnRename = new AEC.UI.Controls.MButton();
            this.linkRefreshPort = new System.Windows.Forms.LinkLabel();
            this.cmbPorts = new AEC.UI.gtComboBox();
            this.lblPort = new AEC.UI.Controls.MLabel();
            this.lblOutput = new AEC.UI.Controls.MLabel();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.lblFirmwareVersion = new AEC.UI.Controls.MLabel();
            this.txtFirmwareVersion = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblBluetoothName
            // 
            this.lblBluetoothName.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBluetoothName.BackColor = System.Drawing.Color.Transparent;
            this.lblBluetoothName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblBluetoothName.BorderWidth = 1F;
            this.lblBluetoothName.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblBluetoothName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblBluetoothName.Location = new System.Drawing.Point(14, 116);
            this.lblBluetoothName.Name = "lblBluetoothName";
            this.lblBluetoothName.Radius = 16;
            this.lblBluetoothName.ShowBackground = false;
            this.lblBluetoothName.ShowBorder = false;
            this.lblBluetoothName.Size = new System.Drawing.Size(96, 23);
            this.lblBluetoothName.TabIndex = 39;
            this.lblBluetoothName.Text = "蓝牙名称";
            // 
            // txtBluetoothName
            // 
            this.txtBluetoothName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtBluetoothName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBluetoothName.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtBluetoothName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtBluetoothName.Location = new System.Drawing.Point(114, 114);
            this.txtBluetoothName.MaxLength = 32;
            this.txtBluetoothName.Name = "txtBluetoothName";
            this.txtBluetoothName.Size = new System.Drawing.Size(231, 27);
            this.txtBluetoothName.TabIndex = 38;
            // 
            // btnRename
            // 
            this.btnRename.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnRename.BorderWidth = 1F;
            this.btnRename.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnRename.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnRename.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnRename.Enabled = false;
            this.btnRename.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnRename.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnRename.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnRename.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnRename.Location = new System.Drawing.Point(351, 114);
            this.btnRename.Name = "btnRename";
            this.btnRename.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnRename.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(120)))), ((int)(((byte)(253)))));
            this.btnRename.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnRename.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.btnRename.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnRename.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnRename.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnRename.Radius = 12;
            this.btnRename.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.btnRename.ShowBorder = false;
            this.btnRename.ShowText = true;
            this.btnRename.Size = new System.Drawing.Size(80, 27);
            this.btnRename.TabIndex = 69;
            this.btnRename.Text = "重命名";
            this.btnRename.Click += new System.EventHandler(this.btn_Click);
            // 
            // linkRefreshPort
            // 
            this.linkRefreshPort.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.linkRefreshPort.AutoSize = true;
            this.linkRefreshPort.BackColor = System.Drawing.Color.Transparent;
            this.linkRefreshPort.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkRefreshPort.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.linkRefreshPort.Location = new System.Drawing.Point(235, 51);
            this.linkRefreshPort.Name = "linkRefreshPort";
            this.linkRefreshPort.Size = new System.Drawing.Size(33, 15);
            this.linkRefreshPort.TabIndex = 73;
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
            this.cmbPorts.Location = new System.Drawing.Point(114, 48);
            this.cmbPorts.Name = "cmbPorts";
            this.cmbPorts.Size = new System.Drawing.Size(115, 23);
            this.cmbPorts.TabIndex = 70;
            // 
            // lblPort
            // 
            this.lblPort.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPort.BackColor = System.Drawing.Color.Transparent;
            this.lblPort.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblPort.BorderWidth = 1F;
            this.lblPort.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblPort.Location = new System.Drawing.Point(14, 48);
            this.lblPort.Name = "lblPort";
            this.lblPort.Radius = 16;
            this.lblPort.ShowBackground = false;
            this.lblPort.ShowBorder = false;
            this.lblPort.Size = new System.Drawing.Size(96, 23);
            this.lblPort.TabIndex = 72;
            this.lblPort.Text = "端口";
            // 
            // lblOutput
            // 
            this.lblOutput.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblOutput.BackColor = System.Drawing.Color.Transparent;
            this.lblOutput.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblOutput.BorderWidth = 1F;
            this.lblOutput.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblOutput.Location = new System.Drawing.Point(14, 150);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Radius = 16;
            this.lblOutput.ShowBackground = false;
            this.lblOutput.ShowBorder = false;
            this.lblOutput.Size = new System.Drawing.Size(96, 23);
            this.lblOutput.TabIndex = 76;
            this.lblOutput.Text = "输出";
            // 
            // txtOutput
            // 
            this.txtOutput.AllowDrop = true;
            this.txtOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOutput.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtOutput.Location = new System.Drawing.Point(114, 149);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(504, 186);
            this.txtOutput.TabIndex = 75;
            // 
            // lblFirmwareVersion
            // 
            this.lblFirmwareVersion.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFirmwareVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblFirmwareVersion.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblFirmwareVersion.BorderWidth = 1F;
            this.lblFirmwareVersion.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblFirmwareVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblFirmwareVersion.Location = new System.Drawing.Point(14, 81);
            this.lblFirmwareVersion.Name = "lblFirmwareVersion";
            this.lblFirmwareVersion.Radius = 16;
            this.lblFirmwareVersion.ShowBackground = false;
            this.lblFirmwareVersion.ShowBorder = false;
            this.lblFirmwareVersion.Size = new System.Drawing.Size(96, 23);
            this.lblFirmwareVersion.TabIndex = 79;
            this.lblFirmwareVersion.Text = "固件版本";
            // 
            // txtFirmwareVersion
            // 
            this.txtFirmwareVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtFirmwareVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFirmwareVersion.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtFirmwareVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtFirmwareVersion.Location = new System.Drawing.Point(114, 79);
            this.txtFirmwareVersion.MaxLength = 256;
            this.txtFirmwareVersion.Name = "txtFirmwareVersion";
            this.txtFirmwareVersion.Size = new System.Drawing.Size(504, 27);
            this.txtFirmwareVersion.TabIndex = 78;
            this.txtFirmwareVersion.TextChanged += new System.EventHandler(this.txtFirmwareVersion_TextChanged);
            // 
            // FormBluetoothRename
            // 
            this.ClientSize = new System.Drawing.Size(640, 360);
            this.Controls.Add(this.lblFirmwareVersion);
            this.Controls.Add(this.txtFirmwareVersion);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.linkRefreshPort);
            this.Controls.Add(this.cmbPorts);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.lblBluetoothName);
            this.Controls.Add(this.txtBluetoothName);
            this.Name = "FormBluetoothRename";
            this.Text = "蓝牙重命名";
            this.Controls.SetChildIndex(this.txtBluetoothName, 0);
            this.Controls.SetChildIndex(this.lblBluetoothName, 0);
            this.Controls.SetChildIndex(this.btnRename, 0);
            this.Controls.SetChildIndex(this.lblPort, 0);
            this.Controls.SetChildIndex(this.cmbPorts, 0);
            this.Controls.SetChildIndex(this.linkRefreshPort, 0);
            this.Controls.SetChildIndex(this.txtOutput, 0);
            this.Controls.SetChildIndex(this.lblOutput, 0);
            this.Controls.SetChildIndex(this.txtFirmwareVersion, 0);
            this.Controls.SetChildIndex(this.lblFirmwareVersion, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MLabel lblBluetoothName;
        private System.Windows.Forms.TextBox txtBluetoothName;
        private Controls.MButton btnRename;
        private System.Windows.Forms.LinkLabel linkRefreshPort;
        private gtComboBox cmbPorts;
        private Controls.MLabel lblPort;
        private Controls.MLabel lblOutput;
        private System.Windows.Forms.TextBox txtOutput;
        private Controls.MLabel lblFirmwareVersion;
        private System.Windows.Forms.TextBox txtFirmwareVersion;
    }
}
