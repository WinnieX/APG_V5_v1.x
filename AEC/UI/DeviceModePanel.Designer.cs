namespace AEC.UI
{
    partial class DeviceModePanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceModePanel));
            this.txtModeName = new System.Windows.Forms.TextBox();
            this.cmbDeviceMode = new AEC.UI.gtComboBox();
            this.lblCurrDeviceMode = new AEC.UI.gtLabel();
            this.lblDeviceMode = new AEC.UI.gtLabel();
            this.lblDeviceName = new AEC.UI.gtLabel();
            this.btnLock = new AEC.UI.gtButton();
            this.btnSaveMode = new AEC.UI.gtButton();
            this.btnCallMode = new AEC.UI.gtButton();
            this.SuspendLayout();
            // 
            // txtModeName
            // 
            this.txtModeName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtModeName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtModeName.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtModeName.ForeColor = System.Drawing.Color.White;
            this.txtModeName.Location = new System.Drawing.Point(151, 126);
            this.txtModeName.MaxLength = 20;
            this.txtModeName.Name = "txtModeName";
            this.txtModeName.Size = new System.Drawing.Size(171, 27);
            this.txtModeName.TabIndex = 27;
            this.txtModeName.TextChanged += new System.EventHandler(this.txtModeName_TextChanged);
            this.txtModeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtModeName_KeyDown);
            // 
            // cmbDeviceMode
            // 
            this.cmbDeviceMode.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbDeviceMode.DropDownBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(208)))), ((int)(((byte)(7)))));
            this.cmbDeviceMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeviceMode.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbDeviceMode.ForeColor = System.Drawing.Color.Black;
            this.cmbDeviceMode.FormattingEnabled = true;
            this.cmbDeviceMode.Location = new System.Drawing.Point(151, 91);
            this.cmbDeviceMode.Margin = new System.Windows.Forms.Padding(0);
            this.cmbDeviceMode.Name = "cmbDeviceMode";
            this.cmbDeviceMode.Size = new System.Drawing.Size(171, 27);
            this.cmbDeviceMode.TabIndex = 26;
            // 
            // lblCurrDeviceMode
            // 
            this.lblCurrDeviceMode.alignment = AEC.UI.gtLabel.AlignMent.Left;
            this.lblCurrDeviceMode.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrDeviceMode.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCurrDeviceMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCurrDeviceMode.Location = new System.Drawing.Point(335, 94);
            this.lblCurrDeviceMode.Name = "lblCurrDeviceMode";
            this.lblCurrDeviceMode.Size = new System.Drawing.Size(90, 20);
            this.lblCurrDeviceMode.TabIndex = 3;
            this.lblCurrDeviceMode.Text = "当前模式";
            // 
            // lblDeviceMode
            // 
            this.lblDeviceMode.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblDeviceMode.BackColor = System.Drawing.Color.Transparent;
            this.lblDeviceMode.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblDeviceMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblDeviceMode.Location = new System.Drawing.Point(45, 94);
            this.lblDeviceMode.Name = "lblDeviceMode";
            this.lblDeviceMode.Size = new System.Drawing.Size(95, 20);
            this.lblDeviceMode.TabIndex = 3;
            this.lblDeviceMode.Text = "设备模式";
            // 
            // lblDeviceName
            // 
            this.lblDeviceName.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblDeviceName.BackColor = System.Drawing.Color.Transparent;
            this.lblDeviceName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblDeviceName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblDeviceName.Location = new System.Drawing.Point(45, 129);
            this.lblDeviceName.Name = "lblDeviceName";
            this.lblDeviceName.Size = new System.Drawing.Size(95, 20);
            this.lblDeviceName.TabIndex = 4;
            this.lblDeviceName.Text = "模式名称";
            // 
            // btnLock
            // 
            this.btnLock.BackColor = System.Drawing.Color.Transparent;
            this.btnLock.backDisabled = global::AEC.Properties.Resources.mode_unlock_disabled;
            this.btnLock.backHover = ((System.Drawing.Image)(resources.GetObject("btnLock.backHover")));
            this.btnLock.backNormal = ((System.Drawing.Image)(resources.GetObject("btnLock.backNormal")));
            this.btnLock.backPressed = ((System.Drawing.Image)(resources.GetObject("btnLock.backPressed")));
            this.btnLock.ForeColor = System.Drawing.Color.White;
            this.btnLock.isStretch = false;
            this.btnLock.Location = new System.Drawing.Point(424, 88);
            this.btnLock.Margin = new System.Windows.Forms.Padding(0);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(32, 32);
            this.btnLock.TabIndex = 30;
            this.btnLock.userGDIPaint = true;
            this.btnLock.Visible = false;
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // btnSaveMode
            // 
            this.btnSaveMode.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveMode.backDisabled = global::AEC.Properties.Resources.save_device_mode_disabled;
            this.btnSaveMode.backHover = global::AEC.Properties.Resources.save_device_mode_hover;
            this.btnSaveMode.backNormal = global::AEC.Properties.Resources.save_device_mode_normal;
            this.btnSaveMode.backPressed = global::AEC.Properties.Resources.save_device_mode_pressed;
            this.btnSaveMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSaveMode.isStretch = false;
            this.btnSaveMode.Location = new System.Drawing.Point(125, 178);
            this.btnSaveMode.Margin = new System.Windows.Forms.Padding(0);
            this.btnSaveMode.Name = "btnSaveMode";
            this.btnSaveMode.Size = new System.Drawing.Size(103, 32);
            this.btnSaveMode.TabIndex = 28;
            this.btnSaveMode.Text = "保存设备模式";
            this.btnSaveMode.userGDIPaint = true;
            this.btnSaveMode.Click += new System.EventHandler(this.button_Click);
            // 
            // btnCallMode
            // 
            this.btnCallMode.BackColor = System.Drawing.Color.Transparent;
            this.btnCallMode.backDisabled = global::AEC.Properties.Resources.call_device_mode_disabled;
            this.btnCallMode.backHover = global::AEC.Properties.Resources.call_device_mode_hover;
            this.btnCallMode.backNormal = global::AEC.Properties.Resources.call_device_mode_normal;
            this.btnCallMode.backPressed = global::AEC.Properties.Resources.call_device_mode_pressed;
            this.btnCallMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCallMode.isStretch = false;
            this.btnCallMode.Location = new System.Drawing.Point(248, 178);
            this.btnCallMode.Margin = new System.Windows.Forms.Padding(0);
            this.btnCallMode.Name = "btnCallMode";
            this.btnCallMode.Size = new System.Drawing.Size(103, 32);
            this.btnCallMode.TabIndex = 28;
            this.btnCallMode.Text = "调用设备模式";
            this.btnCallMode.userGDIPaint = true;
            this.btnCallMode.Click += new System.EventHandler(this.button_Click);
            // 
            // DeviceModePanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.Controls.Add(this.btnLock);
            this.Controls.Add(this.btnSaveMode);
            this.Controls.Add(this.lblDeviceMode);
            this.Controls.Add(this.btnCallMode);
            this.Controls.Add(this.lblDeviceName);
            this.Controls.Add(this.txtModeName);
            this.Controls.Add(this.lblCurrDeviceMode);
            this.Controls.Add(this.cmbDeviceMode);
            this.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DeviceModePanel";
            this.Size = new System.Drawing.Size(476, 294);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private gtLabel lblDeviceMode;
        private gtLabel lblDeviceName;
        private System.Windows.Forms.TextBox txtModeName;
        private gtButton btnSaveMode;
        private gtButton btnCallMode;
        private gtLabel lblCurrDeviceMode;
        public gtComboBox cmbDeviceMode;
        private gtButton btnLock;
    }
}
