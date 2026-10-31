namespace AEC.UI.Forms
{
    partial class FormMCUFlashData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMCUFlashData));
            this.btnExport = new AEC.UI.Controls.MButton();
            this.txtExportDataLength = new System.Windows.Forms.TextBox();
            this.lblExportDataLength = new AEC.UI.Controls.MLabel();
            this.txtExportEndAddress = new System.Windows.Forms.TextBox();
            this.lblExportEndAddress = new AEC.UI.Controls.MLabel();
            this.txtExportStartAddress = new System.Windows.Forms.TextBox();
            this.lblExportStartAddress = new AEC.UI.Controls.MLabel();
            this.lblTargetDevice = new AEC.UI.Controls.MLabel();
            this.cmbTargetDevice = new AEC.UI.gtComboBox();
            this.panelExport = new AEC.UI.Controls.MPanel();
            this.lblFillData = new AEC.UI.Controls.MLabel();
            this.txtFillData = new System.Windows.Forms.TextBox();
            this.btnGenerate = new AEC.UI.Controls.MButton();
            this.cmbDataEncoding = new AEC.UI.gtComboBox();
            this.lblDataEncoding = new AEC.UI.Controls.MLabel();
            this.panelImport = new AEC.UI.Controls.MPanel();
            this.txtImportEndAddress = new System.Windows.Forms.TextBox();
            this.lblImportEndAddress = new AEC.UI.Controls.MLabel();
            this.txtImportDataLength = new System.Windows.Forms.TextBox();
            this.lblImportDataLength = new AEC.UI.Controls.MLabel();
            this.lblImportStartAddress = new AEC.UI.Controls.MLabel();
            this.btnImport = new AEC.UI.Controls.MButton();
            this.txtImportStartAddress = new System.Windows.Forms.TextBox();
            this.txtImportFilePath = new System.Windows.Forms.TextBox();
            this.lblImportDataFile = new AEC.UI.Controls.MLabel();
            this.cmbBaudRate = new AEC.UI.gtComboBox();
            this.lblBaudRate = new AEC.UI.Controls.MLabel();
            this.linkRefresh = new System.Windows.Forms.LinkLabel();
            this.cmbPorts = new AEC.UI.gtComboBox();
            this.lblPort = new AEC.UI.Controls.MLabel();
            this.panelExport.SuspendLayout();
            this.panelImport.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnExport.BorderWidth = 1F;
            this.btnExport.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnExport.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnExport.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnExport.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnExport.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnExport.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnExport.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnExport.Location = new System.Drawing.Point(525, 122);
            this.btnExport.Name = "btnExport";
            this.btnExport.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnExport.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnExport.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExport.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnExport.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnExport.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnExport.Radius = 12;
            this.btnExport.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.btnExport.ShowBorder = true;
            this.btnExport.ShowText = true;
            this.btnExport.Size = new System.Drawing.Size(64, 27);
            this.btnExport.TabIndex = 54;
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btn_Click);
            // 
            // txtExportDataLength
            // 
            this.txtExportDataLength.AllowDrop = true;
            this.txtExportDataLength.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtExportDataLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExportDataLength.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtExportDataLength.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtExportDataLength.Location = new System.Drawing.Point(96, 89);
            this.txtExportDataLength.Name = "txtExportDataLength";
            this.txtExportDataLength.Size = new System.Drawing.Size(115, 27);
            this.txtExportDataLength.TabIndex = 53;
            // 
            // lblExportDataLength
            // 
            this.lblExportDataLength.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblExportDataLength.BackColor = System.Drawing.Color.Transparent;
            this.lblExportDataLength.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblExportDataLength.BorderWidth = 1F;
            this.lblExportDataLength.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblExportDataLength.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblExportDataLength.Location = new System.Drawing.Point(19, 93);
            this.lblExportDataLength.Name = "lblExportDataLength";
            this.lblExportDataLength.Radius = 16;
            this.lblExportDataLength.ShowBackground = false;
            this.lblExportDataLength.ShowBorder = false;
            this.lblExportDataLength.Size = new System.Drawing.Size(64, 23);
            this.lblExportDataLength.TabIndex = 55;
            this.lblExportDataLength.Text = "数据长度";
            // 
            // txtExportEndAddress
            // 
            this.txtExportEndAddress.AllowDrop = true;
            this.txtExportEndAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtExportEndAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExportEndAddress.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtExportEndAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtExportEndAddress.Location = new System.Drawing.Point(96, 122);
            this.txtExportEndAddress.Name = "txtExportEndAddress";
            this.txtExportEndAddress.Size = new System.Drawing.Size(115, 27);
            this.txtExportEndAddress.TabIndex = 51;
            // 
            // lblExportEndAddress
            // 
            this.lblExportEndAddress.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblExportEndAddress.BackColor = System.Drawing.Color.Transparent;
            this.lblExportEndAddress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblExportEndAddress.BorderWidth = 1F;
            this.lblExportEndAddress.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblExportEndAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblExportEndAddress.Location = new System.Drawing.Point(19, 126);
            this.lblExportEndAddress.Name = "lblExportEndAddress";
            this.lblExportEndAddress.Radius = 16;
            this.lblExportEndAddress.ShowBackground = false;
            this.lblExportEndAddress.ShowBorder = false;
            this.lblExportEndAddress.Size = new System.Drawing.Size(64, 23);
            this.lblExportEndAddress.TabIndex = 52;
            this.lblExportEndAddress.Text = "结束地址";
            // 
            // txtExportStartAddress
            // 
            this.txtExportStartAddress.AllowDrop = true;
            this.txtExportStartAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtExportStartAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExportStartAddress.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtExportStartAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtExportStartAddress.Location = new System.Drawing.Point(96, 56);
            this.txtExportStartAddress.Name = "txtExportStartAddress";
            this.txtExportStartAddress.Size = new System.Drawing.Size(115, 27);
            this.txtExportStartAddress.TabIndex = 49;
            // 
            // lblExportStartAddress
            // 
            this.lblExportStartAddress.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblExportStartAddress.BackColor = System.Drawing.Color.Transparent;
            this.lblExportStartAddress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblExportStartAddress.BorderWidth = 1F;
            this.lblExportStartAddress.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblExportStartAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblExportStartAddress.Location = new System.Drawing.Point(19, 60);
            this.lblExportStartAddress.Name = "lblExportStartAddress";
            this.lblExportStartAddress.Radius = 16;
            this.lblExportStartAddress.ShowBackground = false;
            this.lblExportStartAddress.ShowBorder = false;
            this.lblExportStartAddress.Size = new System.Drawing.Size(64, 23);
            this.lblExportStartAddress.TabIndex = 50;
            this.lblExportStartAddress.Text = "起始地址";
            // 
            // lblTargetDevice
            // 
            this.lblTargetDevice.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTargetDevice.BackColor = System.Drawing.Color.Transparent;
            this.lblTargetDevice.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblTargetDevice.BorderWidth = 1F;
            this.lblTargetDevice.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblTargetDevice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTargetDevice.Location = new System.Drawing.Point(19, 24);
            this.lblTargetDevice.Name = "lblTargetDevice";
            this.lblTargetDevice.Radius = 16;
            this.lblTargetDevice.ShowBackground = false;
            this.lblTargetDevice.ShowBorder = false;
            this.lblTargetDevice.Size = new System.Drawing.Size(64, 23);
            this.lblTargetDevice.TabIndex = 47;
            this.lblTargetDevice.Text = "目标设备";
            // 
            // cmbTargetDevice
            // 
            this.cmbTargetDevice.ArrowColor = System.Drawing.Color.Gray;
            this.cmbTargetDevice.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbTargetDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTargetDevice.FormattingEnabled = true;
            this.cmbTargetDevice.Location = new System.Drawing.Point(96, 24);
            this.cmbTargetDevice.Name = "cmbTargetDevice";
            this.cmbTargetDevice.Size = new System.Drawing.Size(115, 23);
            this.cmbTargetDevice.TabIndex = 48;
            this.cmbTargetDevice.SelectedIndexChanged += new System.EventHandler(this.cmbTargetDevice_SelectedIndexChanged);
            // 
            // panelExport
            // 
            this.panelExport.BackColor = System.Drawing.Color.Transparent;
            this.panelExport.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.panelExport.Controls.Add(this.lblFillData);
            this.panelExport.Controls.Add(this.txtFillData);
            this.panelExport.Controls.Add(this.btnGenerate);
            this.panelExport.Controls.Add(this.cmbDataEncoding);
            this.panelExport.Controls.Add(this.lblDataEncoding);
            this.panelExport.Controls.Add(this.cmbTargetDevice);
            this.panelExport.Controls.Add(this.btnExport);
            this.panelExport.Controls.Add(this.lblTargetDevice);
            this.panelExport.Controls.Add(this.txtExportDataLength);
            this.panelExport.Controls.Add(this.lblExportStartAddress);
            this.panelExport.Controls.Add(this.lblExportDataLength);
            this.panelExport.Controls.Add(this.txtExportStartAddress);
            this.panelExport.Controls.Add(this.txtExportEndAddress);
            this.panelExport.Controls.Add(this.lblExportEndAddress);
            this.panelExport.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panelExport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelExport.Location = new System.Drawing.Point(16, 78);
            this.panelExport.Name = "panelExport";
            this.panelExport.Padding = new System.Windows.Forms.Padding(3);
            this.panelExport.Radius = 16;
            this.panelExport.ShowBorder = false;
            this.panelExport.Size = new System.Drawing.Size(608, 158);
            this.panelExport.TabIndex = 56;
            this.panelExport.Text = "导出";
            // 
            // lblFillData
            // 
            this.lblFillData.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFillData.BackColor = System.Drawing.Color.Transparent;
            this.lblFillData.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblFillData.BorderWidth = 1F;
            this.lblFillData.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblFillData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblFillData.Location = new System.Drawing.Point(454, 24);
            this.lblFillData.Name = "lblFillData";
            this.lblFillData.Radius = 16;
            this.lblFillData.ShowBackground = false;
            this.lblFillData.ShowBorder = false;
            this.lblFillData.Size = new System.Drawing.Size(65, 23);
            this.lblFillData.TabIndex = 60;
            this.lblFillData.Text = "填充数据";
            // 
            // txtFillData
            // 
            this.txtFillData.AllowDrop = true;
            this.txtFillData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtFillData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFillData.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtFillData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtFillData.Location = new System.Drawing.Point(525, 23);
            this.txtFillData.Name = "txtFillData";
            this.txtFillData.Size = new System.Drawing.Size(64, 27);
            this.txtFillData.TabIndex = 59;
            this.txtFillData.Text = "0xFF";
            // 
            // btnGenerate
            // 
            this.btnGenerate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnGenerate.BorderWidth = 1F;
            this.btnGenerate.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnGenerate.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnGenerate.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnGenerate.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnGenerate.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnGenerate.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnGenerate.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnGenerate.Location = new System.Drawing.Point(525, 56);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnGenerate.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnGenerate.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnGenerate.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnGenerate.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnGenerate.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnGenerate.Radius = 12;
            this.btnGenerate.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.btnGenerate.ShowBorder = true;
            this.btnGenerate.ShowText = true;
            this.btnGenerate.Size = new System.Drawing.Size(64, 27);
            this.btnGenerate.TabIndex = 58;
            this.btnGenerate.Text = "生成";
            this.btnGenerate.Click += new System.EventHandler(this.btn_Click);
            // 
            // cmbDataEncoding
            // 
            this.cmbDataEncoding.ArrowColor = System.Drawing.Color.Gray;
            this.cmbDataEncoding.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbDataEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataEncoding.FormattingEnabled = true;
            this.cmbDataEncoding.Location = new System.Drawing.Point(312, 24);
            this.cmbDataEncoding.Name = "cmbDataEncoding";
            this.cmbDataEncoding.Size = new System.Drawing.Size(115, 23);
            this.cmbDataEncoding.TabIndex = 57;
            // 
            // lblDataEncoding
            // 
            this.lblDataEncoding.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDataEncoding.BackColor = System.Drawing.Color.Transparent;
            this.lblDataEncoding.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblDataEncoding.BorderWidth = 1F;
            this.lblDataEncoding.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblDataEncoding.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblDataEncoding.Location = new System.Drawing.Point(243, 24);
            this.lblDataEncoding.Name = "lblDataEncoding";
            this.lblDataEncoding.Radius = 16;
            this.lblDataEncoding.ShowBackground = false;
            this.lblDataEncoding.ShowBorder = false;
            this.lblDataEncoding.Size = new System.Drawing.Size(65, 23);
            this.lblDataEncoding.TabIndex = 56;
            this.lblDataEncoding.Text = "编码方式";
            // 
            // panelImport
            // 
            this.panelImport.AllowDrop = true;
            this.panelImport.BackColor = System.Drawing.Color.Transparent;
            this.panelImport.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.panelImport.Controls.Add(this.txtImportEndAddress);
            this.panelImport.Controls.Add(this.lblImportEndAddress);
            this.panelImport.Controls.Add(this.txtImportDataLength);
            this.panelImport.Controls.Add(this.lblImportDataLength);
            this.panelImport.Controls.Add(this.lblImportStartAddress);
            this.panelImport.Controls.Add(this.btnImport);
            this.panelImport.Controls.Add(this.txtImportStartAddress);
            this.panelImport.Controls.Add(this.txtImportFilePath);
            this.panelImport.Controls.Add(this.lblImportDataFile);
            this.panelImport.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panelImport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelImport.Location = new System.Drawing.Point(16, 246);
            this.panelImport.Name = "panelImport";
            this.panelImport.Padding = new System.Windows.Forms.Padding(3);
            this.panelImport.Radius = 16;
            this.panelImport.ShowBorder = false;
            this.panelImport.Size = new System.Drawing.Size(608, 96);
            this.panelImport.TabIndex = 57;
            this.panelImport.Text = "导入";
            this.panelImport.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.panelImport.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            // 
            // txtImportEndAddress
            // 
            this.txtImportEndAddress.AllowDrop = true;
            this.txtImportEndAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtImportEndAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtImportEndAddress.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtImportEndAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtImportEndAddress.Location = new System.Drawing.Point(478, 60);
            this.txtImportEndAddress.Name = "txtImportEndAddress";
            this.txtImportEndAddress.ReadOnly = true;
            this.txtImportEndAddress.Size = new System.Drawing.Size(115, 27);
            this.txtImportEndAddress.TabIndex = 58;
            this.txtImportEndAddress.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.txtImportEndAddress.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            // 
            // lblImportEndAddress
            // 
            this.lblImportEndAddress.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblImportEndAddress.BackColor = System.Drawing.Color.Transparent;
            this.lblImportEndAddress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblImportEndAddress.BorderWidth = 1F;
            this.lblImportEndAddress.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblImportEndAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblImportEndAddress.Location = new System.Drawing.Point(407, 64);
            this.lblImportEndAddress.Name = "lblImportEndAddress";
            this.lblImportEndAddress.Radius = 16;
            this.lblImportEndAddress.ShowBackground = false;
            this.lblImportEndAddress.ShowBorder = false;
            this.lblImportEndAddress.Size = new System.Drawing.Size(65, 23);
            this.lblImportEndAddress.TabIndex = 59;
            this.lblImportEndAddress.Text = "结束地址";
            // 
            // txtImportDataLength
            // 
            this.txtImportDataLength.AllowDrop = true;
            this.txtImportDataLength.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtImportDataLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtImportDataLength.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtImportDataLength.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtImportDataLength.Location = new System.Drawing.Point(291, 60);
            this.txtImportDataLength.Name = "txtImportDataLength";
            this.txtImportDataLength.ReadOnly = true;
            this.txtImportDataLength.Size = new System.Drawing.Size(101, 27);
            this.txtImportDataLength.TabIndex = 58;
            this.txtImportDataLength.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.txtImportDataLength.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            // 
            // lblImportDataLength
            // 
            this.lblImportDataLength.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblImportDataLength.BackColor = System.Drawing.Color.Transparent;
            this.lblImportDataLength.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblImportDataLength.BorderWidth = 1F;
            this.lblImportDataLength.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblImportDataLength.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblImportDataLength.Location = new System.Drawing.Point(220, 64);
            this.lblImportDataLength.Name = "lblImportDataLength";
            this.lblImportDataLength.Radius = 16;
            this.lblImportDataLength.ShowBackground = false;
            this.lblImportDataLength.ShowBorder = false;
            this.lblImportDataLength.Size = new System.Drawing.Size(65, 23);
            this.lblImportDataLength.TabIndex = 59;
            this.lblImportDataLength.Text = "数据长度";
            // 
            // lblImportStartAddress
            // 
            this.lblImportStartAddress.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblImportStartAddress.BackColor = System.Drawing.Color.Transparent;
            this.lblImportStartAddress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblImportStartAddress.BorderWidth = 1F;
            this.lblImportStartAddress.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblImportStartAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblImportStartAddress.Location = new System.Drawing.Point(19, 64);
            this.lblImportStartAddress.Name = "lblImportStartAddress";
            this.lblImportStartAddress.Radius = 16;
            this.lblImportStartAddress.ShowBackground = false;
            this.lblImportStartAddress.ShowBorder = false;
            this.lblImportStartAddress.Size = new System.Drawing.Size(64, 23);
            this.lblImportStartAddress.TabIndex = 59;
            this.lblImportStartAddress.Text = "起始地址";
            // 
            // btnImport
            // 
            this.btnImport.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnImport.BorderWidth = 1F;
            this.btnImport.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnImport.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnImport.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnImport.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnImport.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnImport.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnImport.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnImport.Location = new System.Drawing.Point(525, 24);
            this.btnImport.Name = "btnImport";
            this.btnImport.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnImport.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnImport.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnImport.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnImport.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnImport.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnImport.Radius = 12;
            this.btnImport.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.btnImport.ShowBorder = true;
            this.btnImport.ShowText = true;
            this.btnImport.Size = new System.Drawing.Size(64, 27);
            this.btnImport.TabIndex = 48;
            this.btnImport.Text = "导入";
            this.btnImport.Click += new System.EventHandler(this.btn_Click);
            // 
            // txtImportStartAddress
            // 
            this.txtImportStartAddress.AllowDrop = true;
            this.txtImportStartAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtImportStartAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtImportStartAddress.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtImportStartAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtImportStartAddress.Location = new System.Drawing.Point(96, 60);
            this.txtImportStartAddress.Name = "txtImportStartAddress";
            this.txtImportStartAddress.ReadOnly = true;
            this.txtImportStartAddress.Size = new System.Drawing.Size(115, 27);
            this.txtImportStartAddress.TabIndex = 58;
            this.txtImportStartAddress.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.txtImportStartAddress.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            // 
            // txtImportFilePath
            // 
            this.txtImportFilePath.AllowDrop = true;
            this.txtImportFilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtImportFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtImportFilePath.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtImportFilePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtImportFilePath.Location = new System.Drawing.Point(96, 24);
            this.txtImportFilePath.Name = "txtImportFilePath";
            this.txtImportFilePath.Size = new System.Drawing.Size(414, 27);
            this.txtImportFilePath.TabIndex = 46;
            this.txtImportFilePath.TextChanged += new System.EventHandler(this.txtImportFilePath_TextChanged);
            this.txtImportFilePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.txtImportFilePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            this.txtImportFilePath.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txt_MouseDoubleClick);
            // 
            // lblImportDataFile
            // 
            this.lblImportDataFile.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblImportDataFile.BackColor = System.Drawing.Color.Transparent;
            this.lblImportDataFile.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblImportDataFile.BorderWidth = 1F;
            this.lblImportDataFile.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblImportDataFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblImportDataFile.Location = new System.Drawing.Point(19, 27);
            this.lblImportDataFile.Name = "lblImportDataFile";
            this.lblImportDataFile.Radius = 16;
            this.lblImportDataFile.ShowBackground = false;
            this.lblImportDataFile.ShowBorder = false;
            this.lblImportDataFile.Size = new System.Drawing.Size(64, 23);
            this.lblImportDataFile.TabIndex = 47;
            this.lblImportDataFile.Text = "数据文件";
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.ArrowColor = System.Drawing.Color.Gray;
            this.cmbBaudRate.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Location = new System.Drawing.Point(328, 44);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(115, 23);
            this.cmbBaudRate.TabIndex = 62;
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblBaudRate.BackColor = System.Drawing.Color.Transparent;
            this.lblBaudRate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblBaudRate.BorderWidth = 1F;
            this.lblBaudRate.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblBaudRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblBaudRate.Location = new System.Drawing.Point(277, 44);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Radius = 16;
            this.lblBaudRate.ShowBackground = false;
            this.lblBaudRate.ShowBorder = false;
            this.lblBaudRate.Size = new System.Drawing.Size(47, 23);
            this.lblBaudRate.TabIndex = 61;
            this.lblBaudRate.Text = "波特率";
            // 
            // linkRefresh
            // 
            this.linkRefresh.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.linkRefresh.AutoSize = true;
            this.linkRefresh.BackColor = System.Drawing.Color.Transparent;
            this.linkRefresh.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkRefresh.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.linkRefresh.Location = new System.Drawing.Point(233, 47);
            this.linkRefresh.Name = "linkRefresh";
            this.linkRefresh.Size = new System.Drawing.Size(33, 15);
            this.linkRefresh.TabIndex = 60;
            this.linkRefresh.TabStop = true;
            this.linkRefresh.Text = "刷新";
            this.linkRefresh.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRefresh_LinkClicked);
            // 
            // cmbPorts
            // 
            this.cmbPorts.ArrowColor = System.Drawing.Color.Gray;
            this.cmbPorts.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPorts.FormattingEnabled = true;
            this.cmbPorts.Location = new System.Drawing.Point(112, 44);
            this.cmbPorts.Name = "cmbPorts";
            this.cmbPorts.Size = new System.Drawing.Size(115, 23);
            this.cmbPorts.TabIndex = 59;
            this.cmbPorts.SelectedValueChanged += new System.EventHandler(this.cmbPorts_SelectedValueChanged);
            // 
            // lblPort
            // 
            this.lblPort.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPort.BackColor = System.Drawing.Color.Transparent;
            this.lblPort.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblPort.BorderWidth = 1F;
            this.lblPort.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblPort.Location = new System.Drawing.Point(36, 44);
            this.lblPort.Name = "lblPort";
            this.lblPort.Radius = 16;
            this.lblPort.ShowBackground = false;
            this.lblPort.ShowBorder = false;
            this.lblPort.Size = new System.Drawing.Size(64, 23);
            this.lblPort.TabIndex = 58;
            this.lblPort.Text = "端口";
            // 
            // FormMCUFlashData
            // 
            this.ClientSize = new System.Drawing.Size(640, 360);
            this.Controls.Add(this.cmbBaudRate);
            this.Controls.Add(this.lblBaudRate);
            this.Controls.Add(this.linkRefresh);
            this.Controls.Add(this.cmbPorts);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.panelImport);
            this.Controls.Add(this.panelExport);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormMCUFlashData";
            this.Text = "MCU Flash Data";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.Controls.SetChildIndex(this.panelExport, 0);
            this.Controls.SetChildIndex(this.panelImport, 0);
            this.Controls.SetChildIndex(this.lblPort, 0);
            this.Controls.SetChildIndex(this.cmbPorts, 0);
            this.Controls.SetChildIndex(this.linkRefresh, 0);
            this.Controls.SetChildIndex(this.lblBaudRate, 0);
            this.Controls.SetChildIndex(this.cmbBaudRate, 0);
            this.panelExport.ResumeLayout(false);
            this.panelExport.PerformLayout();
            this.panelImport.ResumeLayout(false);
            this.panelImport.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MButton btnExport;
        private System.Windows.Forms.TextBox txtExportDataLength;
        private Controls.MLabel lblExportDataLength;
        private System.Windows.Forms.TextBox txtExportEndAddress;
        private Controls.MLabel lblExportEndAddress;
        private System.Windows.Forms.TextBox txtExportStartAddress;
        private Controls.MLabel lblExportStartAddress;
        private Controls.MLabel lblTargetDevice;
        private gtComboBox cmbTargetDevice;
        private Controls.MPanel panelExport;
        private Controls.MPanel panelImport;
        private Controls.MButton btnImport;
        private System.Windows.Forms.TextBox txtImportFilePath;
        private Controls.MLabel lblImportDataFile;
        private gtComboBox cmbBaudRate;
        private Controls.MLabel lblBaudRate;
        private System.Windows.Forms.LinkLabel linkRefresh;
        private gtComboBox cmbPorts;
        private Controls.MLabel lblPort;
        private gtComboBox cmbDataEncoding;
        private Controls.MLabel lblDataEncoding;
        private System.Windows.Forms.TextBox txtImportEndAddress;
        private Controls.MLabel lblImportEndAddress;
        private System.Windows.Forms.TextBox txtImportDataLength;
        private Controls.MLabel lblImportDataLength;
        private Controls.MLabel lblImportStartAddress;
        private System.Windows.Forms.TextBox txtImportStartAddress;
        private System.Windows.Forms.TextBox txtFillData;
        private Controls.MButton btnGenerate;
        private Controls.MLabel lblFillData;


    }
}
