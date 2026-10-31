namespace AEC.UI.Forms
{
    partial class FormActivateDevice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormActivateDevice));
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnRun = new AEC.UI.Controls.MButton();
            this.cmbCommInterface = new AEC.UI.gtComboBox();
            this.lblCommInterface = new AEC.UI.Controls.MLabel();
            this.cmbCOMPort = new AEC.UI.gtComboBox();
            this.lblInterfaceProperty1 = new AEC.UI.Controls.MLabel();
            this.lblInterfaceProperty2 = new AEC.UI.Controls.MLabel();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.panelOptions = new AEC.UI.Controls.MPanel();
            this.lblResult = new AEC.UI.Controls.MLabel();
            this.panelOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtOutput
            // 
            this.txtOutput.AllowDrop = true;
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOutput.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtOutput.Location = new System.Drawing.Point(264, 48);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(684, 296);
            this.txtOutput.TabIndex = 41;
            // 
            // btnRun
            // 
            this.btnRun.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnRun.BorderWidth = 1F;
            this.btnRun.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnRun.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnRun.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnRun.Enabled = false;
            this.btnRun.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnRun.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnRun.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnRun.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnRun.Location = new System.Drawing.Point(20, 48);
            this.btnRun.Name = "btnRun";
            this.btnRun.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnRun.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnRun.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnRun.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnRun.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnRun.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnRun.Radius = 12;
            this.btnRun.ShowBorder = true;
            this.btnRun.ShowText = true;
            this.btnRun.Size = new System.Drawing.Size(224, 29);
            this.btnRun.TabIndex = 55;
            this.btnRun.Text = "激活设备";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // cmbCommInterface
            // 
            this.cmbCommInterface.ArrowColor = System.Drawing.Color.Gray;
            this.cmbCommInterface.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbCommInterface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCommInterface.FormattingEnabled = true;
            this.cmbCommInterface.Location = new System.Drawing.Point(104, 14);
            this.cmbCommInterface.Name = "cmbCommInterface";
            this.cmbCommInterface.Size = new System.Drawing.Size(128, 23);
            this.cmbCommInterface.TabIndex = 57;
            this.cmbCommInterface.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            // 
            // lblCommInterface
            // 
            this.lblCommInterface.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCommInterface.BackColor = System.Drawing.Color.Transparent;
            this.lblCommInterface.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblCommInterface.BorderWidth = 1F;
            this.lblCommInterface.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblCommInterface.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCommInterface.Location = new System.Drawing.Point(8, 14);
            this.lblCommInterface.Name = "lblCommInterface";
            this.lblCommInterface.Radius = 16;
            this.lblCommInterface.ShowBackground = false;
            this.lblCommInterface.ShowBorder = false;
            this.lblCommInterface.Size = new System.Drawing.Size(96, 23);
            this.lblCommInterface.TabIndex = 56;
            this.lblCommInterface.Text = "通讯接口";
            // 
            // cmbCOMPort
            // 
            this.cmbCOMPort.ArrowColor = System.Drawing.Color.Gray;
            this.cmbCOMPort.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbCOMPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCOMPort.FormattingEnabled = true;
            this.cmbCOMPort.Location = new System.Drawing.Point(104, 51);
            this.cmbCOMPort.Name = "cmbCOMPort";
            this.cmbCOMPort.Size = new System.Drawing.Size(128, 23);
            this.cmbCOMPort.TabIndex = 59;
            this.cmbCOMPort.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            // 
            // lblInterfaceProperty1
            // 
            this.lblInterfaceProperty1.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInterfaceProperty1.BackColor = System.Drawing.Color.Transparent;
            this.lblInterfaceProperty1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblInterfaceProperty1.BorderWidth = 1F;
            this.lblInterfaceProperty1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblInterfaceProperty1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblInterfaceProperty1.Location = new System.Drawing.Point(8, 52);
            this.lblInterfaceProperty1.Name = "lblInterfaceProperty1";
            this.lblInterfaceProperty1.Radius = 16;
            this.lblInterfaceProperty1.ShowBackground = false;
            this.lblInterfaceProperty1.ShowBorder = false;
            this.lblInterfaceProperty1.Size = new System.Drawing.Size(96, 23);
            this.lblInterfaceProperty1.TabIndex = 58;
            this.lblInterfaceProperty1.Text = "端口";
            this.lblInterfaceProperty1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lblCOMPort_MouseDoubleClick);
            // 
            // lblInterfaceProperty2
            // 
            this.lblInterfaceProperty2.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInterfaceProperty2.BackColor = System.Drawing.Color.Transparent;
            this.lblInterfaceProperty2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblInterfaceProperty2.BorderWidth = 1F;
            this.lblInterfaceProperty2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblInterfaceProperty2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblInterfaceProperty2.Location = new System.Drawing.Point(8, 90);
            this.lblInterfaceProperty2.Name = "lblInterfaceProperty2";
            this.lblInterfaceProperty2.Radius = 16;
            this.lblInterfaceProperty2.ShowBackground = false;
            this.lblInterfaceProperty2.ShowBorder = false;
            this.lblInterfaceProperty2.Size = new System.Drawing.Size(96, 23);
            this.lblInterfaceProperty2.TabIndex = 60;
            this.lblInterfaceProperty2.Text = "波特率";
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.txtIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIPAddress.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtIPAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtIPAddress.Location = new System.Drawing.Point(104, 49);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(128, 27);
            this.txtIPAddress.TabIndex = 62;
            this.txtIPAddress.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // txtPort
            // 
            this.txtPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.txtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPort.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtPort.Location = new System.Drawing.Point(104, 88);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(128, 27);
            this.txtPort.TabIndex = 63;
            this.txtPort.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // panelOptions
            // 
            this.panelOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelOptions.BackColor = System.Drawing.Color.Transparent;
            this.panelOptions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.panelOptions.Controls.Add(this.txtPort);
            this.panelOptions.Controls.Add(this.lblCommInterface);
            this.panelOptions.Controls.Add(this.txtIPAddress);
            this.panelOptions.Controls.Add(this.cmbCommInterface);
            this.panelOptions.Controls.Add(this.lblInterfaceProperty1);
            this.panelOptions.Controls.Add(this.lblInterfaceProperty2);
            this.panelOptions.Controls.Add(this.cmbCOMPort);
            this.panelOptions.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.panelOptions.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelOptions.Location = new System.Drawing.Point(12, 86);
            this.panelOptions.Name = "panelOptions";
            this.panelOptions.Radius = 16;
            this.panelOptions.ShowBorder = true;
            this.panelOptions.Size = new System.Drawing.Size(240, 128);
            this.panelOptions.TabIndex = 64;
            // 
            // lblResult
            // 
            this.lblResult.Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblResult.BackColor = System.Drawing.Color.Transparent;
            this.lblResult.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblResult.BorderWidth = 1F;
            this.lblResult.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblResult.Font = new System.Drawing.Font("Segoe UI", 96F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblResult.Location = new System.Drawing.Point(12, 220);
            this.lblResult.Name = "lblResult";
            this.lblResult.Radius = 16;
            this.lblResult.ShowBackground = false;
            this.lblResult.ShowBorder = false;
            this.lblResult.Size = new System.Drawing.Size(240, 124);
            this.lblResult.TabIndex = 64;
            this.lblResult.Text = "...";
            // 
            // FormActivateDevice
            // 
            this.ClientSize = new System.Drawing.Size(960, 360);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.panelOptions);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.txtOutput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormActivateDevice";
            this.Text = "激活设备";
            this.Controls.SetChildIndex(this.txtOutput, 0);
            this.Controls.SetChildIndex(this.btnRun, 0);
            this.Controls.SetChildIndex(this.panelOptions, 0);
            this.Controls.SetChildIndex(this.lblResult, 0);
            this.panelOptions.ResumeLayout(false);
            this.panelOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOutput;
        private Controls.MButton btnRun;
        private gtComboBox cmbCommInterface;
        private Controls.MLabel lblCommInterface;
        private gtComboBox cmbCOMPort;
        private Controls.MLabel lblInterfaceProperty1;
        private Controls.MLabel lblInterfaceProperty2;
        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.TextBox txtPort;
        private Controls.MPanel panelOptions;
        private Controls.MLabel lblResult;
    }
}
