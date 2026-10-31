namespace AEC.UI
{
    partial class PanelDspFirmwareInfo
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
            this.txtFlashSize = new System.Windows.Forms.TextBox();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.txtTimeStamp = new System.Windows.Forms.TextBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.txtExeSize = new System.Windows.Forms.TextBox();
            this.txtExeCrc = new System.Windows.Forms.TextBox();
            this.txtWavAddress = new System.Windows.Forms.TextBox();
            this.txtWavCrc = new System.Windows.Forms.TextBox();
            this.txtWavSize = new System.Windows.Forms.TextBox();
            this.txtEnvCrc = new System.Windows.Forms.TextBox();
            this.txtEnvSize = new System.Windows.Forms.TextBox();
            this.txtEnvAddress = new System.Windows.Forms.TextBox();
            this.txtTpmAddress = new System.Windows.Forms.TextBox();
            this.lblTpmAddress = new AEC.UI.Controls.MLabel();
            this.lblEnvCrc = new AEC.UI.Controls.MLabel();
            this.lblEnvSize = new AEC.UI.Controls.MLabel();
            this.lblEnvAddress = new AEC.UI.Controls.MLabel();
            this.lblWavCrc = new AEC.UI.Controls.MLabel();
            this.lblWavSize = new AEC.UI.Controls.MLabel();
            this.lblWavAddress = new AEC.UI.Controls.MLabel();
            this.lblExeCrc = new AEC.UI.Controls.MLabel();
            this.lblExeSize = new AEC.UI.Controls.MLabel();
            this.lblFlashSize = new AEC.UI.Controls.MLabel();
            this.lblComments = new AEC.UI.Controls.MLabel();
            this.lblTimeStamp = new AEC.UI.Controls.MLabel();
            this.lblVersion = new AEC.UI.Controls.MLabel();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.lblFileName = new AEC.UI.Controls.MLabel();
            this.switchTPM = new AEC.UI.Controls.MSwitch();
            this.switchEfuse = new AEC.UI.Controls.MSwitch();
            this.SuspendLayout();
            // 
            // txtFlashSize
            // 
            this.txtFlashSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtFlashSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFlashSize.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtFlashSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtFlashSize.Location = new System.Drawing.Point(120, 24);
            this.txtFlashSize.Name = "txtFlashSize";
            this.txtFlashSize.ReadOnly = true;
            this.txtFlashSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFlashSize.Size = new System.Drawing.Size(96, 27);
            this.txtFlashSize.TabIndex = 17;
            // 
            // txtComments
            // 
            this.txtComments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtComments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtComments.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtComments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtComments.Location = new System.Drawing.Point(120, 214);
            this.txtComments.MaxLength = 64;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(587, 27);
            this.txtComments.TabIndex = 15;
            // 
            // txtTimeStamp
            // 
            this.txtTimeStamp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtTimeStamp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTimeStamp.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtTimeStamp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtTimeStamp.Location = new System.Drawing.Point(370, 150);
            this.txtTimeStamp.Name = "txtTimeStamp";
            this.txtTimeStamp.ReadOnly = true;
            this.txtTimeStamp.Size = new System.Drawing.Size(337, 27);
            this.txtTimeStamp.TabIndex = 7;
            // 
            // txtVersion
            // 
            this.txtVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVersion.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtVersion.Location = new System.Drawing.Point(120, 152);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(96, 27);
            this.txtVersion.TabIndex = 5;
            // 
            // txtExeSize
            // 
            this.txtExeSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtExeSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExeSize.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtExeSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtExeSize.Location = new System.Drawing.Point(370, 24);
            this.txtExeSize.Name = "txtExeSize";
            this.txtExeSize.ReadOnly = true;
            this.txtExeSize.Size = new System.Drawing.Size(96, 27);
            this.txtExeSize.TabIndex = 19;
            // 
            // txtExeCrc
            // 
            this.txtExeCrc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtExeCrc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExeCrc.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtExeCrc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtExeCrc.Location = new System.Drawing.Point(611, 24);
            this.txtExeCrc.Name = "txtExeCrc";
            this.txtExeCrc.ReadOnly = true;
            this.txtExeCrc.Size = new System.Drawing.Size(96, 27);
            this.txtExeCrc.TabIndex = 23;
            // 
            // txtWavAddress
            // 
            this.txtWavAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtWavAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWavAddress.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtWavAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtWavAddress.Location = new System.Drawing.Point(120, 55);
            this.txtWavAddress.Name = "txtWavAddress";
            this.txtWavAddress.ReadOnly = true;
            this.txtWavAddress.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtWavAddress.Size = new System.Drawing.Size(96, 27);
            this.txtWavAddress.TabIndex = 25;
            // 
            // txtWavCrc
            // 
            this.txtWavCrc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtWavCrc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWavCrc.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtWavCrc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtWavCrc.Location = new System.Drawing.Point(611, 55);
            this.txtWavCrc.Name = "txtWavCrc";
            this.txtWavCrc.ReadOnly = true;
            this.txtWavCrc.Size = new System.Drawing.Size(96, 27);
            this.txtWavCrc.TabIndex = 29;
            // 
            // txtWavSize
            // 
            this.txtWavSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtWavSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWavSize.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtWavSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtWavSize.Location = new System.Drawing.Point(370, 55);
            this.txtWavSize.Name = "txtWavSize";
            this.txtWavSize.ReadOnly = true;
            this.txtWavSize.Size = new System.Drawing.Size(96, 27);
            this.txtWavSize.TabIndex = 27;
            // 
            // txtEnvCrc
            // 
            this.txtEnvCrc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtEnvCrc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEnvCrc.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtEnvCrc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtEnvCrc.Location = new System.Drawing.Point(611, 86);
            this.txtEnvCrc.Name = "txtEnvCrc";
            this.txtEnvCrc.ReadOnly = true;
            this.txtEnvCrc.Size = new System.Drawing.Size(96, 27);
            this.txtEnvCrc.TabIndex = 35;
            // 
            // txtEnvSize
            // 
            this.txtEnvSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtEnvSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEnvSize.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtEnvSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtEnvSize.Location = new System.Drawing.Point(370, 86);
            this.txtEnvSize.Name = "txtEnvSize";
            this.txtEnvSize.ReadOnly = true;
            this.txtEnvSize.Size = new System.Drawing.Size(96, 27);
            this.txtEnvSize.TabIndex = 33;
            // 
            // txtEnvAddress
            // 
            this.txtEnvAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtEnvAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEnvAddress.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtEnvAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtEnvAddress.Location = new System.Drawing.Point(120, 86);
            this.txtEnvAddress.Name = "txtEnvAddress";
            this.txtEnvAddress.ReadOnly = true;
            this.txtEnvAddress.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEnvAddress.Size = new System.Drawing.Size(96, 27);
            this.txtEnvAddress.TabIndex = 31;
            // 
            // txtTpmAddress
            // 
            this.txtTpmAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtTpmAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTpmAddress.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtTpmAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtTpmAddress.Location = new System.Drawing.Point(120, 119);
            this.txtTpmAddress.Name = "txtTpmAddress";
            this.txtTpmAddress.ReadOnly = true;
            this.txtTpmAddress.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTpmAddress.Size = new System.Drawing.Size(96, 27);
            this.txtTpmAddress.TabIndex = 37;
            // 
            // lblTpmAddress
            // 
            this.lblTpmAddress.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTpmAddress.BackColor = System.Drawing.Color.Transparent;
            this.lblTpmAddress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblTpmAddress.BorderWidth = 1F;
            this.lblTpmAddress.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblTpmAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTpmAddress.Location = new System.Drawing.Point(19, 121);
            this.lblTpmAddress.Name = "lblTpmAddress";
            this.lblTpmAddress.Radius = 16;
            this.lblTpmAddress.ShowBackground = false;
            this.lblTpmAddress.ShowBorder = false;
            this.lblTpmAddress.Size = new System.Drawing.Size(96, 23);
            this.lblTpmAddress.TabIndex = 36;
            this.lblTpmAddress.Text = "TPM段地址";
            // 
            // lblEnvCrc
            // 
            this.lblEnvCrc.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEnvCrc.BackColor = System.Drawing.Color.Transparent;
            this.lblEnvCrc.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblEnvCrc.BorderWidth = 1F;
            this.lblEnvCrc.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblEnvCrc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEnvCrc.Location = new System.Drawing.Point(497, 88);
            this.lblEnvCrc.Name = "lblEnvCrc";
            this.lblEnvCrc.Radius = 16;
            this.lblEnvCrc.ShowBackground = false;
            this.lblEnvCrc.ShowBorder = false;
            this.lblEnvCrc.Size = new System.Drawing.Size(108, 23);
            this.lblEnvCrc.TabIndex = 34;
            this.lblEnvCrc.Text = "Env段CRC";
            // 
            // lblEnvSize
            // 
            this.lblEnvSize.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEnvSize.BackColor = System.Drawing.Color.Transparent;
            this.lblEnvSize.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblEnvSize.BorderWidth = 1F;
            this.lblEnvSize.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblEnvSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEnvSize.Location = new System.Drawing.Point(256, 88);
            this.lblEnvSize.Name = "lblEnvSize";
            this.lblEnvSize.Radius = 16;
            this.lblEnvSize.ShowBackground = false;
            this.lblEnvSize.ShowBorder = false;
            this.lblEnvSize.Size = new System.Drawing.Size(108, 23);
            this.lblEnvSize.TabIndex = 32;
            this.lblEnvSize.Text = "Env段大小";
            // 
            // lblEnvAddress
            // 
            this.lblEnvAddress.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEnvAddress.BackColor = System.Drawing.Color.Transparent;
            this.lblEnvAddress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblEnvAddress.BorderWidth = 1F;
            this.lblEnvAddress.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblEnvAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEnvAddress.Location = new System.Drawing.Point(19, 88);
            this.lblEnvAddress.Name = "lblEnvAddress";
            this.lblEnvAddress.Radius = 16;
            this.lblEnvAddress.ShowBackground = false;
            this.lblEnvAddress.ShowBorder = false;
            this.lblEnvAddress.Size = new System.Drawing.Size(96, 23);
            this.lblEnvAddress.TabIndex = 30;
            this.lblEnvAddress.Text = "Env段地址";
            // 
            // lblWavCrc
            // 
            this.lblWavCrc.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblWavCrc.BackColor = System.Drawing.Color.Transparent;
            this.lblWavCrc.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblWavCrc.BorderWidth = 1F;
            this.lblWavCrc.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblWavCrc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblWavCrc.Location = new System.Drawing.Point(497, 57);
            this.lblWavCrc.Name = "lblWavCrc";
            this.lblWavCrc.Radius = 16;
            this.lblWavCrc.ShowBackground = false;
            this.lblWavCrc.ShowBorder = false;
            this.lblWavCrc.Size = new System.Drawing.Size(108, 23);
            this.lblWavCrc.TabIndex = 28;
            this.lblWavCrc.Text = "Wav段CRC";
            // 
            // lblWavSize
            // 
            this.lblWavSize.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblWavSize.BackColor = System.Drawing.Color.Transparent;
            this.lblWavSize.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblWavSize.BorderWidth = 1F;
            this.lblWavSize.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblWavSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblWavSize.Location = new System.Drawing.Point(256, 57);
            this.lblWavSize.Name = "lblWavSize";
            this.lblWavSize.Radius = 16;
            this.lblWavSize.ShowBackground = false;
            this.lblWavSize.ShowBorder = false;
            this.lblWavSize.Size = new System.Drawing.Size(108, 23);
            this.lblWavSize.TabIndex = 26;
            this.lblWavSize.Text = "Wav段大小";
            // 
            // lblWavAddress
            // 
            this.lblWavAddress.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblWavAddress.BackColor = System.Drawing.Color.Transparent;
            this.lblWavAddress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblWavAddress.BorderWidth = 1F;
            this.lblWavAddress.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblWavAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblWavAddress.Location = new System.Drawing.Point(19, 57);
            this.lblWavAddress.Name = "lblWavAddress";
            this.lblWavAddress.Radius = 16;
            this.lblWavAddress.ShowBackground = false;
            this.lblWavAddress.ShowBorder = false;
            this.lblWavAddress.Size = new System.Drawing.Size(96, 23);
            this.lblWavAddress.TabIndex = 24;
            this.lblWavAddress.Text = "Wav段地址";
            // 
            // lblExeCrc
            // 
            this.lblExeCrc.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblExeCrc.BackColor = System.Drawing.Color.Transparent;
            this.lblExeCrc.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblExeCrc.BorderWidth = 1F;
            this.lblExeCrc.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblExeCrc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblExeCrc.Location = new System.Drawing.Point(497, 26);
            this.lblExeCrc.Name = "lblExeCrc";
            this.lblExeCrc.Radius = 16;
            this.lblExeCrc.ShowBackground = false;
            this.lblExeCrc.ShowBorder = false;
            this.lblExeCrc.Size = new System.Drawing.Size(108, 23);
            this.lblExeCrc.TabIndex = 22;
            this.lblExeCrc.Text = "可执行程序段CRC";
            // 
            // lblExeSize
            // 
            this.lblExeSize.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblExeSize.BackColor = System.Drawing.Color.Transparent;
            this.lblExeSize.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblExeSize.BorderWidth = 1F;
            this.lblExeSize.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblExeSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblExeSize.Location = new System.Drawing.Point(256, 26);
            this.lblExeSize.Name = "lblExeSize";
            this.lblExeSize.Radius = 16;
            this.lblExeSize.ShowBackground = false;
            this.lblExeSize.ShowBorder = false;
            this.lblExeSize.Size = new System.Drawing.Size(108, 23);
            this.lblExeSize.TabIndex = 18;
            this.lblExeSize.Text = "可执行程序段大小";
            // 
            // lblFlashSize
            // 
            this.lblFlashSize.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFlashSize.BackColor = System.Drawing.Color.Transparent;
            this.lblFlashSize.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblFlashSize.BorderWidth = 1F;
            this.lblFlashSize.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblFlashSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblFlashSize.Location = new System.Drawing.Point(19, 26);
            this.lblFlashSize.Name = "lblFlashSize";
            this.lblFlashSize.Radius = 16;
            this.lblFlashSize.ShowBackground = false;
            this.lblFlashSize.ShowBorder = false;
            this.lblFlashSize.Size = new System.Drawing.Size(96, 23);
            this.lblFlashSize.TabIndex = 16;
            this.lblFlashSize.Text = "Flash 大小";
            // 
            // lblComments
            // 
            this.lblComments.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblComments.BackColor = System.Drawing.Color.Transparent;
            this.lblComments.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblComments.BorderWidth = 1F;
            this.lblComments.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblComments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblComments.Location = new System.Drawing.Point(19, 216);
            this.lblComments.Name = "lblComments";
            this.lblComments.Radius = 16;
            this.lblComments.ShowBackground = false;
            this.lblComments.ShowBorder = false;
            this.lblComments.Size = new System.Drawing.Size(96, 23);
            this.lblComments.TabIndex = 14;
            this.lblComments.Text = "说明";
            // 
            // lblTimeStamp
            // 
            this.lblTimeStamp.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTimeStamp.BackColor = System.Drawing.Color.Transparent;
            this.lblTimeStamp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblTimeStamp.BorderWidth = 1F;
            this.lblTimeStamp.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblTimeStamp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTimeStamp.Location = new System.Drawing.Point(265, 152);
            this.lblTimeStamp.Name = "lblTimeStamp";
            this.lblTimeStamp.Radius = 16;
            this.lblTimeStamp.ShowBackground = false;
            this.lblTimeStamp.ShowBorder = false;
            this.lblTimeStamp.Size = new System.Drawing.Size(99, 23);
            this.lblTimeStamp.TabIndex = 6;
            this.lblTimeStamp.Text = "文件修改时间戳";
            // 
            // lblVersion
            // 
            this.lblVersion.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblVersion.BorderWidth = 1F;
            this.lblVersion.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblVersion.Location = new System.Drawing.Point(19, 154);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Radius = 16;
            this.lblVersion.ShowBackground = false;
            this.lblVersion.ShowBorder = false;
            this.lblVersion.Size = new System.Drawing.Size(96, 23);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "版本";
            // 
            // txtFileName
            // 
            this.txtFileName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFileName.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtFileName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtFileName.Location = new System.Drawing.Point(120, 183);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(587, 27);
            this.txtFileName.TabIndex = 39;
            // 
            // lblFileName
            // 
            this.lblFileName.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFileName.BackColor = System.Drawing.Color.Transparent;
            this.lblFileName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblFileName.BorderWidth = 1F;
            this.lblFileName.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblFileName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblFileName.Location = new System.Drawing.Point(15, 185);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Radius = 16;
            this.lblFileName.ShowBackground = false;
            this.lblFileName.ShowBorder = false;
            this.lblFileName.Size = new System.Drawing.Size(99, 23);
            this.lblFileName.TabIndex = 38;
            this.lblFileName.Text = "文件名";
            // 
            // switchTPM
            // 
            this.switchTPM.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchTPM.BorderWidth = 1F;
            this.switchTPM.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.switchTPM.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.switchTPM.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.switchTPM.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.switchTPM.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.switchTPM.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.switchTPM.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchTPM.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.switchTPM.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.switchTPM.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.switchTPM.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.switchTPM.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchTPM.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.TextBox;
            this.switchTPM.Enabled = false;
            this.switchTPM.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.switchTPM.isAutoCheck = true;
            this.switchTPM.IsChecked = false;
            this.switchTPM.IsCheckedInit = false;
            this.switchTPM.Location = new System.Drawing.Point(307, 118);
            this.switchTPM.Name = "switchTPM";
            this.switchTPM.Radius = 29;
            this.switchTPM.ShowBorder = false;
            this.switchTPM.ShowText = true;
            this.switchTPM.Size = new System.Drawing.Size(112, 29);
            this.switchTPM.TabIndex = 40;
            this.switchTPM.Text = "TPM加密";
            this.switchTPM.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.switchTPM.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.switchTPM.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.switchTPM.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.switchTPM.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.switchTPM.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.switchTPM.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchTPM.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.switchTPM.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.switchTPM.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.switchTPM.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.switchTPM.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            // 
            // switchEfuse
            // 
            this.switchEfuse.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchEfuse.BorderWidth = 1F;
            this.switchEfuse.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.switchEfuse.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.switchEfuse.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.switchEfuse.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.switchEfuse.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.switchEfuse.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.switchEfuse.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchEfuse.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.switchEfuse.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.switchEfuse.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.switchEfuse.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.switchEfuse.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchEfuse.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.TextBox;
            this.switchEfuse.Enabled = false;
            this.switchEfuse.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.switchEfuse.isAutoCheck = true;
            this.switchEfuse.IsChecked = false;
            this.switchEfuse.IsCheckedInit = false;
            this.switchEfuse.Location = new System.Drawing.Point(548, 118);
            this.switchEfuse.Name = "switchEfuse";
            this.switchEfuse.Radius = 29;
            this.switchEfuse.ShowBorder = false;
            this.switchEfuse.ShowText = true;
            this.switchEfuse.Size = new System.Drawing.Size(112, 29);
            this.switchEfuse.TabIndex = 41;
            this.switchEfuse.Text = "Efuse加密";
            this.switchEfuse.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.switchEfuse.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.switchEfuse.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.switchEfuse.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.switchEfuse.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.switchEfuse.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.switchEfuse.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchEfuse.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.switchEfuse.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.switchEfuse.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.switchEfuse.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.switchEfuse.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            // 
            // PanelDspProgrammerDownloadParams
            // 
            this.Controls.Add(this.switchEfuse);
            this.Controls.Add(this.switchTPM);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.txtTpmAddress);
            this.Controls.Add(this.lblTpmAddress);
            this.Controls.Add(this.txtEnvCrc);
            this.Controls.Add(this.lblEnvCrc);
            this.Controls.Add(this.txtEnvSize);
            this.Controls.Add(this.lblEnvSize);
            this.Controls.Add(this.txtEnvAddress);
            this.Controls.Add(this.lblEnvAddress);
            this.Controls.Add(this.txtWavCrc);
            this.Controls.Add(this.lblWavCrc);
            this.Controls.Add(this.txtWavSize);
            this.Controls.Add(this.lblWavSize);
            this.Controls.Add(this.txtWavAddress);
            this.Controls.Add(this.lblWavAddress);
            this.Controls.Add(this.txtExeCrc);
            this.Controls.Add(this.lblExeCrc);
            this.Controls.Add(this.txtExeSize);
            this.Controls.Add(this.lblExeSize);
            this.Controls.Add(this.txtFlashSize);
            this.Controls.Add(this.lblFlashSize);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this.lblComments);
            this.Controls.Add(this.txtTimeStamp);
            this.Controls.Add(this.lblTimeStamp);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.lblVersion);
            this.Name = "PanelDspProgrammerDownloadParams";
            this.ShowBorder = false;
            this.Size = new System.Drawing.Size(736, 248);
            this.Text = "DSP固件信息";
            this.TextPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MLabel lblVersion;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.TextBox txtTimeStamp;
        private Controls.MLabel lblTimeStamp;
        private System.Windows.Forms.TextBox txtComments;
        private Controls.MLabel lblComments;
        private System.Windows.Forms.TextBox txtFlashSize;
        private Controls.MLabel lblFlashSize;
        private Controls.MLabel lblExeSize;
        private System.Windows.Forms.TextBox txtExeSize;
        private Controls.MLabel lblExeCrc;
        private System.Windows.Forms.TextBox txtExeCrc;
        private System.Windows.Forms.TextBox txtWavAddress;
        private Controls.MLabel lblWavAddress;
        private System.Windows.Forms.TextBox txtWavCrc;
        private Controls.MLabel lblWavCrc;
        private System.Windows.Forms.TextBox txtWavSize;
        private Controls.MLabel lblWavSize;
        private System.Windows.Forms.TextBox txtEnvCrc;
        private Controls.MLabel lblEnvCrc;
        private System.Windows.Forms.TextBox txtEnvSize;
        private Controls.MLabel lblEnvSize;
        private System.Windows.Forms.TextBox txtEnvAddress;
        private Controls.MLabel lblEnvAddress;
        private System.Windows.Forms.TextBox txtTpmAddress;
        private Controls.MLabel lblTpmAddress;
        private System.Windows.Forms.TextBox txtFileName;
        private Controls.MLabel lblFileName;
        private Controls.MSwitch switchTPM;
        private Controls.MSwitch switchEfuse;
    }
}
