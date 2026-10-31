namespace AEC.UI.Forms
{
    partial class FormExportFirmwarePackage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExportFirmwarePackage));
            this.cmbTargetDevice = new AEC.UI.gtComboBox();
            this.lblTargetDevice = new AEC.UI.Controls.MLabel();
            this.chkUpdateMCUAllData = new AEC.UI.Controls.MSwitch();
            this.chkMCU = new AEC.UI.Controls.MSwitch();
            this.chkDSP = new AEC.UI.Controls.MSwitch();
            this.txtMCUBinFilePath = new System.Windows.Forms.TextBox();
            this.txtDSPBinFilePath = new System.Windows.Forms.TextBox();
            this.btnExport = new AEC.UI.Controls.MButton();
            this.chkUpdateCustomDeviceInfo = new AEC.UI.Controls.MSwitch();
            this.txtCustomDeviceInfo = new System.Windows.Forms.TextBox();
            this.chkUpdatePresetData = new AEC.UI.Controls.MSwitch();
            this.txtPresetData = new System.Windows.Forms.TextBox();
            this.chkEncryptedExport = new AEC.UI.Controls.MSwitch();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.lblKey = new AEC.UI.Controls.MLabel();
            this.mLabel1 = new AEC.UI.Controls.MLabel();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtMCUAllData = new System.Windows.Forms.TextBox();
            this.lblName = new AEC.UI.Controls.MLabel();
            this.cmbCustomDeviceInfoTask = new AEC.UI.gtComboBox();
            this.chkWriteCustomModelCode = new AEC.UI.Controls.MSwitch();
            this.txtCustomDeviceModelCode = new System.Windows.Forms.TextBox();
            this.chkWriteCustomDeviceInfo = new AEC.UI.Controls.MSwitch();
            this.chkEnableCustomDeviceInfo = new AEC.UI.Controls.MSwitch();
            this.txtCustomManufacturer = new System.Windows.Forms.TextBox();
            this.lblCustomManufacturer = new AEC.UI.Controls.MLabel();
            this.lblCustomDescription = new AEC.UI.Controls.MLabel();
            this.lblCustomModel = new AEC.UI.Controls.MLabel();
            this.lblCustomVersion = new AEC.UI.Controls.MLabel();
            this.txtCustomModel = new System.Windows.Forms.TextBox();
            this.txtCustomDescription = new System.Windows.Forms.TextBox();
            this.txtCustomVersion = new System.Windows.Forms.TextBox();
            this.txtX5CustomDeviceModelCode = new System.Windows.Forms.TextBox();
            this.lblActivateAddress = new AEC.UI.Controls.MLabel();
            this.lblMCUAppAddress = new AEC.UI.Controls.MLabel();
            this.txtMCUAppAddress = new System.Windows.Forms.TextBox();
            this.txtActivateAddress = new System.Windows.Forms.TextBox();
            this.chkForcedUpdateBTInfo = new AEC.UI.Controls.MSwitch();
            this.chkEnableRandomBTAddress = new AEC.UI.Controls.MSwitch();
            this.txtBluetoothName = new System.Windows.Forms.TextBox();
            this.lblBluetoothName = new AEC.UI.Controls.MLabel();
            this.chkRetainBTDeviceInfo = new AEC.UI.Controls.MSwitch();
            this.cmbTargetMCUModel = new AEC.UI.gtComboBox();
            this.lblTargetMCUModel = new AEC.UI.Controls.MLabel();
            this.SuspendLayout();
            // 
            // cmbTargetDevice
            // 
            this.cmbTargetDevice.ArrowColor = System.Drawing.Color.Gray;
            this.cmbTargetDevice.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbTargetDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTargetDevice.FormattingEnabled = true;
            this.cmbTargetDevice.Location = new System.Drawing.Point(128, 49);
            this.cmbTargetDevice.Name = "cmbTargetDevice";
            this.cmbTargetDevice.Size = new System.Drawing.Size(87, 23);
            this.cmbTargetDevice.TabIndex = 16;
            this.cmbTargetDevice.SelectedValueChanged += new System.EventHandler(this.cmbTargetDevice_SelectedValueChanged);
            // 
            // lblTargetDevice
            // 
            this.lblTargetDevice.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTargetDevice.BackColor = System.Drawing.Color.Transparent;
            this.lblTargetDevice.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblTargetDevice.BorderWidth = 1F;
            this.lblTargetDevice.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblTargetDevice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTargetDevice.Location = new System.Drawing.Point(48, 49);
            this.lblTargetDevice.Name = "lblTargetDevice";
            this.lblTargetDevice.Radius = 16;
            this.lblTargetDevice.ShowBackground = false;
            this.lblTargetDevice.ShowBorder = false;
            this.lblTargetDevice.Size = new System.Drawing.Size(65, 23);
            this.lblTargetDevice.TabIndex = 15;
            this.lblTargetDevice.Text = "目标设备";
            // 
            // chkUpdateMCUAllData
            // 
            this.chkUpdateMCUAllData.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdateMCUAllData.BorderWidth = 1F;
            this.chkUpdateMCUAllData.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkUpdateMCUAllData.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.chkUpdateMCUAllData.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkUpdateMCUAllData.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkUpdateMCUAllData.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.chkUpdateMCUAllData.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkUpdateMCUAllData.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdateMCUAllData.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.chkUpdateMCUAllData.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkUpdateMCUAllData.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkUpdateMCUAllData.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.chkUpdateMCUAllData.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdateMCUAllData.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkUpdateMCUAllData.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkUpdateMCUAllData.isAutoCheck = true;
            this.chkUpdateMCUAllData.IsChecked = true;
            this.chkUpdateMCUAllData.IsCheckedInit = true;
            this.chkUpdateMCUAllData.Location = new System.Drawing.Point(48, 187);
            this.chkUpdateMCUAllData.Name = "chkUpdateMCUAllData";
            this.chkUpdateMCUAllData.Radius = 29;
            this.chkUpdateMCUAllData.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkUpdateMCUAllData.ShowBorder = false;
            this.chkUpdateMCUAllData.ShowText = true;
            this.chkUpdateMCUAllData.Size = new System.Drawing.Size(172, 29);
            this.chkUpdateMCUAllData.SwitchWdith = 48;
            this.chkUpdateMCUAllData.TabIndex = 30;
            this.chkUpdateMCUAllData.Text = "更新MCU Flash数据";
            this.chkUpdateMCUAllData.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkUpdateMCUAllData.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkUpdateMCUAllData.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkUpdateMCUAllData.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkUpdateMCUAllData.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkUpdateMCUAllData.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkUpdateMCUAllData.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdateMCUAllData.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkUpdateMCUAllData.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkUpdateMCUAllData.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkUpdateMCUAllData.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkUpdateMCUAllData.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdateMCUAllData.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkMCU
            // 
            this.chkMCU.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkMCU.BorderWidth = 1F;
            this.chkMCU.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkMCU.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.chkMCU.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkMCU.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkMCU.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.chkMCU.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkMCU.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkMCU.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.chkMCU.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkMCU.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkMCU.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.chkMCU.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkMCU.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkMCU.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkMCU.isAutoCheck = true;
            this.chkMCU.IsChecked = true;
            this.chkMCU.IsCheckedInit = true;
            this.chkMCU.Location = new System.Drawing.Point(48, 152);
            this.chkMCU.Name = "chkMCU";
            this.chkMCU.Radius = 29;
            this.chkMCU.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkMCU.ShowBorder = false;
            this.chkMCU.ShowText = true;
            this.chkMCU.Size = new System.Drawing.Size(172, 29);
            this.chkMCU.SwitchWdith = 48;
            this.chkMCU.TabIndex = 29;
            this.chkMCU.Text = "MCU bin 镜像文件";
            this.chkMCU.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkMCU.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkMCU.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkMCU.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkMCU.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkMCU.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkMCU.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkMCU.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkMCU.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkMCU.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkMCU.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkMCU.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            // 
            // chkDSP
            // 
            this.chkDSP.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkDSP.BorderWidth = 1F;
            this.chkDSP.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkDSP.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.chkDSP.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkDSP.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkDSP.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.chkDSP.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkDSP.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkDSP.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.chkDSP.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkDSP.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkDSP.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.chkDSP.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkDSP.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkDSP.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkDSP.isAutoCheck = true;
            this.chkDSP.IsChecked = true;
            this.chkDSP.IsCheckedInit = true;
            this.chkDSP.Location = new System.Drawing.Point(48, 82);
            this.chkDSP.Name = "chkDSP";
            this.chkDSP.Radius = 29;
            this.chkDSP.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkDSP.ShowBorder = false;
            this.chkDSP.ShowText = true;
            this.chkDSP.Size = new System.Drawing.Size(172, 29);
            this.chkDSP.SwitchWdith = 48;
            this.chkDSP.TabIndex = 28;
            this.chkDSP.Text = "DSP bin 镜像文件";
            this.chkDSP.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkDSP.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkDSP.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkDSP.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkDSP.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkDSP.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkDSP.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkDSP.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkDSP.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkDSP.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkDSP.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkDSP.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            // 
            // txtMCUBinFilePath
            // 
            this.txtMCUBinFilePath.AllowDrop = true;
            this.txtMCUBinFilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtMCUBinFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMCUBinFilePath.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtMCUBinFilePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtMCUBinFilePath.Location = new System.Drawing.Point(226, 154);
            this.txtMCUBinFilePath.Name = "txtMCUBinFilePath";
            this.txtMCUBinFilePath.Size = new System.Drawing.Size(527, 27);
            this.txtMCUBinFilePath.TabIndex = 27;
            this.txtMCUBinFilePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.txtMCUBinFilePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            this.txtMCUBinFilePath.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txt_MouseDoubleClick);
            // 
            // txtDSPBinFilePath
            // 
            this.txtDSPBinFilePath.AllowDrop = true;
            this.txtDSPBinFilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtDSPBinFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDSPBinFilePath.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtDSPBinFilePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtDSPBinFilePath.Location = new System.Drawing.Point(226, 83);
            this.txtDSPBinFilePath.Name = "txtDSPBinFilePath";
            this.txtDSPBinFilePath.Size = new System.Drawing.Size(686, 27);
            this.txtDSPBinFilePath.TabIndex = 26;
            this.txtDSPBinFilePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.txtDSPBinFilePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            this.txtDSPBinFilePath.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txt_MouseDoubleClick);
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
            this.btnExport.Location = new System.Drawing.Point(848, 45);
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
            this.btnExport.TabIndex = 31;
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btn_Click);
            // 
            // chkUpdateCustomDeviceInfo
            // 
            this.chkUpdateCustomDeviceInfo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdateCustomDeviceInfo.BorderWidth = 1F;
            this.chkUpdateCustomDeviceInfo.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkUpdateCustomDeviceInfo.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.chkUpdateCustomDeviceInfo.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkUpdateCustomDeviceInfo.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkUpdateCustomDeviceInfo.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.chkUpdateCustomDeviceInfo.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkUpdateCustomDeviceInfo.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdateCustomDeviceInfo.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.chkUpdateCustomDeviceInfo.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkUpdateCustomDeviceInfo.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkUpdateCustomDeviceInfo.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.chkUpdateCustomDeviceInfo.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdateCustomDeviceInfo.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkUpdateCustomDeviceInfo.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkUpdateCustomDeviceInfo.isAutoCheck = true;
            this.chkUpdateCustomDeviceInfo.IsChecked = true;
            this.chkUpdateCustomDeviceInfo.IsCheckedInit = true;
            this.chkUpdateCustomDeviceInfo.Location = new System.Drawing.Point(48, 222);
            this.chkUpdateCustomDeviceInfo.Name = "chkUpdateCustomDeviceInfo";
            this.chkUpdateCustomDeviceInfo.Radius = 29;
            this.chkUpdateCustomDeviceInfo.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkUpdateCustomDeviceInfo.ShowBorder = false;
            this.chkUpdateCustomDeviceInfo.ShowText = true;
            this.chkUpdateCustomDeviceInfo.Size = new System.Drawing.Size(172, 29);
            this.chkUpdateCustomDeviceInfo.SwitchWdith = 48;
            this.chkUpdateCustomDeviceInfo.TabIndex = 33;
            this.chkUpdateCustomDeviceInfo.Text = "更新自定义设备信息";
            this.chkUpdateCustomDeviceInfo.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkUpdateCustomDeviceInfo.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkUpdateCustomDeviceInfo.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkUpdateCustomDeviceInfo.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkUpdateCustomDeviceInfo.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkUpdateCustomDeviceInfo.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkUpdateCustomDeviceInfo.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdateCustomDeviceInfo.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkUpdateCustomDeviceInfo.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkUpdateCustomDeviceInfo.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkUpdateCustomDeviceInfo.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkUpdateCustomDeviceInfo.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdateCustomDeviceInfo.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // txtCustomDeviceInfo
            // 
            this.txtCustomDeviceInfo.AllowDrop = true;
            this.txtCustomDeviceInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtCustomDeviceInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomDeviceInfo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtCustomDeviceInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtCustomDeviceInfo.Location = new System.Drawing.Point(226, 224);
            this.txtCustomDeviceInfo.Name = "txtCustomDeviceInfo";
            this.txtCustomDeviceInfo.Size = new System.Drawing.Size(527, 27);
            this.txtCustomDeviceInfo.TabIndex = 32;
            this.txtCustomDeviceInfo.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtCustomDeviceInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.txtCustomDeviceInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            this.txtCustomDeviceInfo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txt_MouseDoubleClick);
            // 
            // chkUpdatePresetData
            // 
            this.chkUpdatePresetData.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdatePresetData.BorderWidth = 1F;
            this.chkUpdatePresetData.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkUpdatePresetData.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.chkUpdatePresetData.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkUpdatePresetData.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkUpdatePresetData.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.chkUpdatePresetData.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkUpdatePresetData.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdatePresetData.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.chkUpdatePresetData.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkUpdatePresetData.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkUpdatePresetData.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.chkUpdatePresetData.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdatePresetData.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkUpdatePresetData.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkUpdatePresetData.isAutoCheck = true;
            this.chkUpdatePresetData.IsChecked = true;
            this.chkUpdatePresetData.IsCheckedInit = true;
            this.chkUpdatePresetData.Location = new System.Drawing.Point(48, 368);
            this.chkUpdatePresetData.Name = "chkUpdatePresetData";
            this.chkUpdatePresetData.Radius = 29;
            this.chkUpdatePresetData.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkUpdatePresetData.ShowBorder = false;
            this.chkUpdatePresetData.ShowText = true;
            this.chkUpdatePresetData.Size = new System.Drawing.Size(172, 29);
            this.chkUpdatePresetData.SwitchWdith = 48;
            this.chkUpdatePresetData.TabIndex = 35;
            this.chkUpdatePresetData.Text = "更新自定义预设数据";
            this.chkUpdatePresetData.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkUpdatePresetData.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkUpdatePresetData.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkUpdatePresetData.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkUpdatePresetData.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkUpdatePresetData.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkUpdatePresetData.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkUpdatePresetData.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkUpdatePresetData.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkUpdatePresetData.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkUpdatePresetData.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkUpdatePresetData.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            // 
            // txtPresetData
            // 
            this.txtPresetData.AllowDrop = true;
            this.txtPresetData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtPresetData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPresetData.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtPresetData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtPresetData.Location = new System.Drawing.Point(226, 370);
            this.txtPresetData.Name = "txtPresetData";
            this.txtPresetData.Size = new System.Drawing.Size(686, 27);
            this.txtPresetData.TabIndex = 34;
            this.txtPresetData.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.txtPresetData.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            this.txtPresetData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txt_MouseDoubleClick);
            // 
            // chkEncryptedExport
            // 
            this.chkEncryptedExport.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkEncryptedExport.BorderWidth = 1F;
            this.chkEncryptedExport.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkEncryptedExport.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.chkEncryptedExport.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkEncryptedExport.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkEncryptedExport.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.chkEncryptedExport.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkEncryptedExport.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkEncryptedExport.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.chkEncryptedExport.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkEncryptedExport.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkEncryptedExport.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.chkEncryptedExport.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkEncryptedExport.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkEncryptedExport.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkEncryptedExport.isAutoCheck = true;
            this.chkEncryptedExport.IsChecked = true;
            this.chkEncryptedExport.IsCheckedInit = true;
            this.chkEncryptedExport.Location = new System.Drawing.Point(403, 46);
            this.chkEncryptedExport.Name = "chkEncryptedExport";
            this.chkEncryptedExport.Radius = 29;
            this.chkEncryptedExport.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkEncryptedExport.ShowBorder = false;
            this.chkEncryptedExport.ShowText = true;
            this.chkEncryptedExport.Size = new System.Drawing.Size(108, 29);
            this.chkEncryptedExport.SwitchWdith = 48;
            this.chkEncryptedExport.TabIndex = 36;
            this.chkEncryptedExport.Text = "加密导出";
            this.chkEncryptedExport.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkEncryptedExport.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkEncryptedExport.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkEncryptedExport.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkEncryptedExport.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkEncryptedExport.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkEncryptedExport.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkEncryptedExport.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkEncryptedExport.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkEncryptedExport.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkEncryptedExport.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkEncryptedExport.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            // 
            // txtKey
            // 
            this.txtKey.AllowDrop = true;
            this.txtKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKey.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtKey.Location = new System.Drawing.Point(552, 46);
            this.txtKey.MaxLength = 32;
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(288, 27);
            this.txtKey.TabIndex = 37;
            // 
            // lblKey
            // 
            this.lblKey.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblKey.BackColor = System.Drawing.Color.Transparent;
            this.lblKey.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblKey.BorderWidth = 1F;
            this.lblKey.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblKey.Location = new System.Drawing.Point(516, 49);
            this.lblKey.Name = "lblKey";
            this.lblKey.Radius = 16;
            this.lblKey.ShowBackground = false;
            this.lblKey.ShowBorder = false;
            this.lblKey.Size = new System.Drawing.Size(32, 23);
            this.lblKey.TabIndex = 38;
            this.lblKey.Text = "密钥";
            // 
            // mLabel1
            // 
            this.mLabel1.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.mLabel1.BackColor = System.Drawing.Color.Transparent;
            this.mLabel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.mLabel1.BorderWidth = 1F;
            this.mLabel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.mLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mLabel1.Location = new System.Drawing.Point(48, 403);
            this.mLabel1.Name = "mLabel1";
            this.mLabel1.Radius = 16;
            this.mLabel1.ShowBackground = false;
            this.mLabel1.ShowBorder = false;
            this.mLabel1.Size = new System.Drawing.Size(167, 23);
            this.mLabel1.TabIndex = 40;
            this.mLabel1.Text = "描述";
            // 
            // txtDescription
            // 
            this.txtDescription.AllowDrop = true;
            this.txtDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtDescription.Location = new System.Drawing.Point(226, 403);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(686, 85);
            this.txtDescription.TabIndex = 39;
            // 
            // txtMCUAllData
            // 
            this.txtMCUAllData.AllowDrop = true;
            this.txtMCUAllData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtMCUAllData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMCUAllData.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtMCUAllData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtMCUAllData.Location = new System.Drawing.Point(226, 190);
            this.txtMCUAllData.Name = "txtMCUAllData";
            this.txtMCUAllData.Size = new System.Drawing.Size(527, 27);
            this.txtMCUAllData.TabIndex = 41;
            this.txtMCUAllData.DragDrop += new System.Windows.Forms.DragEventHandler(this.txt_DragDrop);
            this.txtMCUAllData.DragEnter += new System.Windows.Forms.DragEventHandler(this.txt_DragEnter);
            this.txtMCUAllData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txt_MouseDoubleClick);
            // 
            // lblName
            // 
            this.lblName.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblName.BorderWidth = 1F;
            this.lblName.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblName.Location = new System.Drawing.Point(759, 227);
            this.lblName.Name = "lblName";
            this.lblName.Radius = 16;
            this.lblName.ShowBackground = false;
            this.lblName.ShowBorder = false;
            this.lblName.Size = new System.Drawing.Size(32, 23);
            this.lblName.TabIndex = 42;
            this.lblName.Text = "名称";
            // 
            // cmbCustomDeviceInfoTask
            // 
            this.cmbCustomDeviceInfoTask.ArrowColor = System.Drawing.Color.Gray;
            this.cmbCustomDeviceInfoTask.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbCustomDeviceInfoTask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomDeviceInfoTask.Enabled = false;
            this.cmbCustomDeviceInfoTask.FormattingEnabled = true;
            this.cmbCustomDeviceInfoTask.Items.AddRange(new object[] {
            "VKx",
            "VKxS"});
            this.cmbCustomDeviceInfoTask.Location = new System.Drawing.Point(797, 227);
            this.cmbCustomDeviceInfoTask.Name = "cmbCustomDeviceInfoTask";
            this.cmbCustomDeviceInfoTask.Size = new System.Drawing.Size(115, 23);
            this.cmbCustomDeviceInfoTask.TabIndex = 43;
            this.cmbCustomDeviceInfoTask.SelectedValueChanged += new System.EventHandler(this.cmbCustomDeviceInfoTask_SelectedValueChanged);
            // 
            // chkWriteCustomModelCode
            // 
            this.chkWriteCustomModelCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkWriteCustomModelCode.BorderWidth = 1F;
            this.chkWriteCustomModelCode.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkWriteCustomModelCode.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.chkWriteCustomModelCode.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkWriteCustomModelCode.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkWriteCustomModelCode.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.chkWriteCustomModelCode.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkWriteCustomModelCode.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkWriteCustomModelCode.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.chkWriteCustomModelCode.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkWriteCustomModelCode.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkWriteCustomModelCode.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.chkWriteCustomModelCode.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkWriteCustomModelCode.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkWriteCustomModelCode.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkWriteCustomModelCode.isAutoCheck = true;
            this.chkWriteCustomModelCode.IsChecked = true;
            this.chkWriteCustomModelCode.IsCheckedInit = true;
            this.chkWriteCustomModelCode.Location = new System.Drawing.Point(227, 262);
            this.chkWriteCustomModelCode.Name = "chkWriteCustomModelCode";
            this.chkWriteCustomModelCode.Radius = 29;
            this.chkWriteCustomModelCode.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkWriteCustomModelCode.ShowBorder = false;
            this.chkWriteCustomModelCode.ShowText = true;
            this.chkWriteCustomModelCode.Size = new System.Drawing.Size(172, 29);
            this.chkWriteCustomModelCode.SwitchWdith = 48;
            this.chkWriteCustomModelCode.TabIndex = 44;
            this.chkWriteCustomModelCode.Text = "更新自定义型号代码";
            this.chkWriteCustomModelCode.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkWriteCustomModelCode.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkWriteCustomModelCode.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkWriteCustomModelCode.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkWriteCustomModelCode.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkWriteCustomModelCode.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkWriteCustomModelCode.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkWriteCustomModelCode.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkWriteCustomModelCode.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkWriteCustomModelCode.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkWriteCustomModelCode.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkWriteCustomModelCode.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            // 
            // txtCustomDeviceModelCode
            // 
            this.txtCustomDeviceModelCode.AllowDrop = true;
            this.txtCustomDeviceModelCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtCustomDeviceModelCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomDeviceModelCode.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtCustomDeviceModelCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtCustomDeviceModelCode.Location = new System.Drawing.Point(405, 262);
            this.txtCustomDeviceModelCode.Name = "txtCustomDeviceModelCode";
            this.txtCustomDeviceModelCode.Size = new System.Drawing.Size(60, 27);
            this.txtCustomDeviceModelCode.TabIndex = 45;
            // 
            // chkWriteCustomDeviceInfo
            // 
            this.chkWriteCustomDeviceInfo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkWriteCustomDeviceInfo.BorderWidth = 1F;
            this.chkWriteCustomDeviceInfo.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkWriteCustomDeviceInfo.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.chkWriteCustomDeviceInfo.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkWriteCustomDeviceInfo.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkWriteCustomDeviceInfo.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.chkWriteCustomDeviceInfo.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkWriteCustomDeviceInfo.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkWriteCustomDeviceInfo.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.chkWriteCustomDeviceInfo.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkWriteCustomDeviceInfo.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkWriteCustomDeviceInfo.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.chkWriteCustomDeviceInfo.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkWriteCustomDeviceInfo.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkWriteCustomDeviceInfo.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkWriteCustomDeviceInfo.isAutoCheck = true;
            this.chkWriteCustomDeviceInfo.IsChecked = true;
            this.chkWriteCustomDeviceInfo.IsCheckedInit = true;
            this.chkWriteCustomDeviceInfo.Location = new System.Drawing.Point(552, 262);
            this.chkWriteCustomDeviceInfo.Name = "chkWriteCustomDeviceInfo";
            this.chkWriteCustomDeviceInfo.Radius = 29;
            this.chkWriteCustomDeviceInfo.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkWriteCustomDeviceInfo.ShowBorder = false;
            this.chkWriteCustomDeviceInfo.ShowText = true;
            this.chkWriteCustomDeviceInfo.Size = new System.Drawing.Size(172, 29);
            this.chkWriteCustomDeviceInfo.SwitchWdith = 48;
            this.chkWriteCustomDeviceInfo.TabIndex = 46;
            this.chkWriteCustomDeviceInfo.Text = "更新自定义设备信息";
            this.chkWriteCustomDeviceInfo.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkWriteCustomDeviceInfo.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkWriteCustomDeviceInfo.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkWriteCustomDeviceInfo.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkWriteCustomDeviceInfo.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkWriteCustomDeviceInfo.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkWriteCustomDeviceInfo.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkWriteCustomDeviceInfo.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkWriteCustomDeviceInfo.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkWriteCustomDeviceInfo.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkWriteCustomDeviceInfo.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkWriteCustomDeviceInfo.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            // 
            // chkEnableCustomDeviceInfo
            // 
            this.chkEnableCustomDeviceInfo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkEnableCustomDeviceInfo.BorderWidth = 1F;
            this.chkEnableCustomDeviceInfo.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkEnableCustomDeviceInfo.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.chkEnableCustomDeviceInfo.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkEnableCustomDeviceInfo.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkEnableCustomDeviceInfo.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.chkEnableCustomDeviceInfo.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkEnableCustomDeviceInfo.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkEnableCustomDeviceInfo.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.chkEnableCustomDeviceInfo.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkEnableCustomDeviceInfo.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkEnableCustomDeviceInfo.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.chkEnableCustomDeviceInfo.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkEnableCustomDeviceInfo.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkEnableCustomDeviceInfo.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkEnableCustomDeviceInfo.isAutoCheck = true;
            this.chkEnableCustomDeviceInfo.IsChecked = true;
            this.chkEnableCustomDeviceInfo.IsCheckedInit = true;
            this.chkEnableCustomDeviceInfo.Location = new System.Drawing.Point(740, 262);
            this.chkEnableCustomDeviceInfo.Name = "chkEnableCustomDeviceInfo";
            this.chkEnableCustomDeviceInfo.Radius = 29;
            this.chkEnableCustomDeviceInfo.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkEnableCustomDeviceInfo.ShowBorder = false;
            this.chkEnableCustomDeviceInfo.ShowText = true;
            this.chkEnableCustomDeviceInfo.Size = new System.Drawing.Size(172, 29);
            this.chkEnableCustomDeviceInfo.SwitchWdith = 48;
            this.chkEnableCustomDeviceInfo.TabIndex = 47;
            this.chkEnableCustomDeviceInfo.Text = "使能自定义设备信息";
            this.chkEnableCustomDeviceInfo.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkEnableCustomDeviceInfo.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkEnableCustomDeviceInfo.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkEnableCustomDeviceInfo.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkEnableCustomDeviceInfo.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkEnableCustomDeviceInfo.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkEnableCustomDeviceInfo.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkEnableCustomDeviceInfo.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkEnableCustomDeviceInfo.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkEnableCustomDeviceInfo.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkEnableCustomDeviceInfo.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkEnableCustomDeviceInfo.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            // 
            // txtCustomManufacturer
            // 
            this.txtCustomManufacturer.AllowDrop = true;
            this.txtCustomManufacturer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtCustomManufacturer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomManufacturer.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtCustomManufacturer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtCustomManufacturer.Location = new System.Drawing.Point(309, 299);
            this.txtCustomManufacturer.MaxLength = 20;
            this.txtCustomManufacturer.Name = "txtCustomManufacturer";
            this.txtCustomManufacturer.Size = new System.Drawing.Size(240, 27);
            this.txtCustomManufacturer.TabIndex = 48;
            // 
            // lblCustomManufacturer
            // 
            this.lblCustomManufacturer.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCustomManufacturer.BackColor = System.Drawing.Color.Transparent;
            this.lblCustomManufacturer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblCustomManufacturer.BorderWidth = 1F;
            this.lblCustomManufacturer.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblCustomManufacturer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCustomManufacturer.Location = new System.Drawing.Point(226, 303);
            this.lblCustomManufacturer.Name = "lblCustomManufacturer";
            this.lblCustomManufacturer.Radius = 16;
            this.lblCustomManufacturer.ShowBackground = false;
            this.lblCustomManufacturer.ShowBorder = false;
            this.lblCustomManufacturer.Size = new System.Drawing.Size(80, 23);
            this.lblCustomManufacturer.TabIndex = 49;
            this.lblCustomManufacturer.Text = "自定义厂商";
            // 
            // lblCustomDescription
            // 
            this.lblCustomDescription.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCustomDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblCustomDescription.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblCustomDescription.BorderWidth = 1F;
            this.lblCustomDescription.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblCustomDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCustomDescription.Location = new System.Drawing.Point(586, 339);
            this.lblCustomDescription.Name = "lblCustomDescription";
            this.lblCustomDescription.Radius = 16;
            this.lblCustomDescription.ShowBackground = false;
            this.lblCustomDescription.ShowBorder = false;
            this.lblCustomDescription.Size = new System.Drawing.Size(80, 23);
            this.lblCustomDescription.TabIndex = 50;
            this.lblCustomDescription.Text = "自定义描述";
            // 
            // lblCustomModel
            // 
            this.lblCustomModel.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCustomModel.BackColor = System.Drawing.Color.Transparent;
            this.lblCustomModel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblCustomModel.BorderWidth = 1F;
            this.lblCustomModel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblCustomModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCustomModel.Location = new System.Drawing.Point(586, 303);
            this.lblCustomModel.Name = "lblCustomModel";
            this.lblCustomModel.Radius = 16;
            this.lblCustomModel.ShowBackground = false;
            this.lblCustomModel.ShowBorder = false;
            this.lblCustomModel.Size = new System.Drawing.Size(80, 23);
            this.lblCustomModel.TabIndex = 51;
            this.lblCustomModel.Text = "自定义型号";
            // 
            // lblCustomVersion
            // 
            this.lblCustomVersion.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCustomVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblCustomVersion.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblCustomVersion.BorderWidth = 1F;
            this.lblCustomVersion.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblCustomVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCustomVersion.Location = new System.Drawing.Point(226, 339);
            this.lblCustomVersion.Name = "lblCustomVersion";
            this.lblCustomVersion.Radius = 16;
            this.lblCustomVersion.ShowBackground = false;
            this.lblCustomVersion.ShowBorder = false;
            this.lblCustomVersion.Size = new System.Drawing.Size(80, 23);
            this.lblCustomVersion.TabIndex = 52;
            this.lblCustomVersion.Text = "自定义版本";
            // 
            // txtCustomModel
            // 
            this.txtCustomModel.AllowDrop = true;
            this.txtCustomModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtCustomModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomModel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtCustomModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtCustomModel.Location = new System.Drawing.Point(672, 299);
            this.txtCustomModel.MaxLength = 20;
            this.txtCustomModel.Name = "txtCustomModel";
            this.txtCustomModel.Size = new System.Drawing.Size(240, 27);
            this.txtCustomModel.TabIndex = 53;
            // 
            // txtCustomDescription
            // 
            this.txtCustomDescription.AllowDrop = true;
            this.txtCustomDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtCustomDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomDescription.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtCustomDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtCustomDescription.Location = new System.Drawing.Point(672, 335);
            this.txtCustomDescription.MaxLength = 20;
            this.txtCustomDescription.Name = "txtCustomDescription";
            this.txtCustomDescription.Size = new System.Drawing.Size(240, 27);
            this.txtCustomDescription.TabIndex = 55;
            // 
            // txtCustomVersion
            // 
            this.txtCustomVersion.AllowDrop = true;
            this.txtCustomVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtCustomVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomVersion.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtCustomVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtCustomVersion.Location = new System.Drawing.Point(309, 335);
            this.txtCustomVersion.MaxLength = 20;
            this.txtCustomVersion.Name = "txtCustomVersion";
            this.txtCustomVersion.Size = new System.Drawing.Size(240, 27);
            this.txtCustomVersion.TabIndex = 54;
            // 
            // txtX5CustomDeviceModelCode
            // 
            this.txtX5CustomDeviceModelCode.AllowDrop = true;
            this.txtX5CustomDeviceModelCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtX5CustomDeviceModelCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtX5CustomDeviceModelCode.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtX5CustomDeviceModelCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtX5CustomDeviceModelCode.Location = new System.Drawing.Point(471, 262);
            this.txtX5CustomDeviceModelCode.Name = "txtX5CustomDeviceModelCode";
            this.txtX5CustomDeviceModelCode.Size = new System.Drawing.Size(60, 27);
            this.txtX5CustomDeviceModelCode.TabIndex = 56;
            // 
            // lblActivateAddress
            // 
            this.lblActivateAddress.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblActivateAddress.BackColor = System.Drawing.Color.Transparent;
            this.lblActivateAddress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblActivateAddress.BorderWidth = 1F;
            this.lblActivateAddress.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblActivateAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblActivateAddress.Location = new System.Drawing.Point(759, 193);
            this.lblActivateAddress.Name = "lblActivateAddress";
            this.lblActivateAddress.Radius = 16;
            this.lblActivateAddress.ShowBackground = false;
            this.lblActivateAddress.ShowBorder = false;
            this.lblActivateAddress.Size = new System.Drawing.Size(56, 23);
            this.lblActivateAddress.TabIndex = 57;
            this.lblActivateAddress.Text = "激活地址";
            // 
            // lblMCUAppAddress
            // 
            this.lblMCUAppAddress.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblMCUAppAddress.BackColor = System.Drawing.Color.Transparent;
            this.lblMCUAppAddress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblMCUAppAddress.BorderWidth = 1F;
            this.lblMCUAppAddress.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblMCUAppAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMCUAppAddress.Location = new System.Drawing.Point(759, 158);
            this.lblMCUAppAddress.Name = "lblMCUAppAddress";
            this.lblMCUAppAddress.Radius = 16;
            this.lblMCUAppAddress.ShowBackground = false;
            this.lblMCUAppAddress.ShowBorder = false;
            this.lblMCUAppAddress.Size = new System.Drawing.Size(56, 23);
            this.lblMCUAppAddress.TabIndex = 58;
            this.lblMCUAppAddress.Text = "App地址";
            // 
            // txtMCUAppAddress
            // 
            this.txtMCUAppAddress.AllowDrop = true;
            this.txtMCUAppAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtMCUAppAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMCUAppAddress.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtMCUAppAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtMCUAppAddress.Location = new System.Drawing.Point(821, 154);
            this.txtMCUAppAddress.Name = "txtMCUAppAddress";
            this.txtMCUAppAddress.Size = new System.Drawing.Size(91, 27);
            this.txtMCUAppAddress.TabIndex = 59;
            this.txtMCUAppAddress.Text = "0x08006000";
            // 
            // txtActivateAddress
            // 
            this.txtActivateAddress.AllowDrop = true;
            this.txtActivateAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtActivateAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtActivateAddress.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtActivateAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtActivateAddress.Location = new System.Drawing.Point(821, 190);
            this.txtActivateAddress.Name = "txtActivateAddress";
            this.txtActivateAddress.Size = new System.Drawing.Size(91, 27);
            this.txtActivateAddress.TabIndex = 60;
            // 
            // chkForcedUpdateBTInfo
            // 
            this.chkForcedUpdateBTInfo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkForcedUpdateBTInfo.BorderWidth = 1F;
            this.chkForcedUpdateBTInfo.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkForcedUpdateBTInfo.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.chkForcedUpdateBTInfo.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkForcedUpdateBTInfo.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkForcedUpdateBTInfo.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.chkForcedUpdateBTInfo.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkForcedUpdateBTInfo.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkForcedUpdateBTInfo.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.chkForcedUpdateBTInfo.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkForcedUpdateBTInfo.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkForcedUpdateBTInfo.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.chkForcedUpdateBTInfo.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkForcedUpdateBTInfo.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkForcedUpdateBTInfo.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkForcedUpdateBTInfo.isAutoCheck = true;
            this.chkForcedUpdateBTInfo.IsChecked = false;
            this.chkForcedUpdateBTInfo.IsCheckedInit = false;
            this.chkForcedUpdateBTInfo.Location = new System.Drawing.Point(227, 116);
            this.chkForcedUpdateBTInfo.Name = "chkForcedUpdateBTInfo";
            this.chkForcedUpdateBTInfo.Radius = 29;
            this.chkForcedUpdateBTInfo.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkForcedUpdateBTInfo.ShowBorder = false;
            this.chkForcedUpdateBTInfo.ShowText = true;
            this.chkForcedUpdateBTInfo.Size = new System.Drawing.Size(172, 29);
            this.chkForcedUpdateBTInfo.SwitchWdith = 48;
            this.chkForcedUpdateBTInfo.TabIndex = 61;
            this.chkForcedUpdateBTInfo.Text = "强制更新蓝牙设备信息";
            this.chkForcedUpdateBTInfo.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkForcedUpdateBTInfo.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkForcedUpdateBTInfo.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkForcedUpdateBTInfo.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkForcedUpdateBTInfo.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkForcedUpdateBTInfo.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkForcedUpdateBTInfo.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkForcedUpdateBTInfo.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkForcedUpdateBTInfo.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkForcedUpdateBTInfo.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkForcedUpdateBTInfo.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkForcedUpdateBTInfo.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            // 
            // chkEnableRandomBTAddress
            // 
            this.chkEnableRandomBTAddress.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkEnableRandomBTAddress.BorderWidth = 1F;
            this.chkEnableRandomBTAddress.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkEnableRandomBTAddress.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.chkEnableRandomBTAddress.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkEnableRandomBTAddress.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkEnableRandomBTAddress.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.chkEnableRandomBTAddress.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkEnableRandomBTAddress.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkEnableRandomBTAddress.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.chkEnableRandomBTAddress.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkEnableRandomBTAddress.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkEnableRandomBTAddress.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.chkEnableRandomBTAddress.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkEnableRandomBTAddress.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkEnableRandomBTAddress.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkEnableRandomBTAddress.isAutoCheck = true;
            this.chkEnableRandomBTAddress.IsChecked = false;
            this.chkEnableRandomBTAddress.IsCheckedInit = false;
            this.chkEnableRandomBTAddress.Location = new System.Drawing.Point(605, 116);
            this.chkEnableRandomBTAddress.Name = "chkEnableRandomBTAddress";
            this.chkEnableRandomBTAddress.Radius = 29;
            this.chkEnableRandomBTAddress.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkEnableRandomBTAddress.ShowBorder = false;
            this.chkEnableRandomBTAddress.ShowText = true;
            this.chkEnableRandomBTAddress.Size = new System.Drawing.Size(148, 29);
            this.chkEnableRandomBTAddress.SwitchWdith = 48;
            this.chkEnableRandomBTAddress.TabIndex = 62;
            this.chkEnableRandomBTAddress.Text = "蓝牙随机地址";
            this.chkEnableRandomBTAddress.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkEnableRandomBTAddress.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkEnableRandomBTAddress.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkEnableRandomBTAddress.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkEnableRandomBTAddress.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkEnableRandomBTAddress.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkEnableRandomBTAddress.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkEnableRandomBTAddress.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkEnableRandomBTAddress.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkEnableRandomBTAddress.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkEnableRandomBTAddress.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkEnableRandomBTAddress.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            // 
            // txtBluetoothName
            // 
            this.txtBluetoothName.AllowDrop = true;
            this.txtBluetoothName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtBluetoothName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBluetoothName.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtBluetoothName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtBluetoothName.Location = new System.Drawing.Point(821, 117);
            this.txtBluetoothName.Name = "txtBluetoothName";
            this.txtBluetoothName.Size = new System.Drawing.Size(91, 27);
            this.txtBluetoothName.TabIndex = 63;
            // 
            // lblBluetoothName
            // 
            this.lblBluetoothName.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBluetoothName.BackColor = System.Drawing.Color.Transparent;
            this.lblBluetoothName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblBluetoothName.BorderWidth = 1F;
            this.lblBluetoothName.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblBluetoothName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblBluetoothName.Location = new System.Drawing.Point(759, 119);
            this.lblBluetoothName.Name = "lblBluetoothName";
            this.lblBluetoothName.Radius = 16;
            this.lblBluetoothName.ShowBackground = false;
            this.lblBluetoothName.ShowBorder = false;
            this.lblBluetoothName.Size = new System.Drawing.Size(56, 23);
            this.lblBluetoothName.TabIndex = 64;
            this.lblBluetoothName.Text = "蓝牙名称";
            // 
            // chkRetainBTDeviceInfo
            // 
            this.chkRetainBTDeviceInfo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkRetainBTDeviceInfo.BorderWidth = 1F;
            this.chkRetainBTDeviceInfo.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkRetainBTDeviceInfo.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.chkRetainBTDeviceInfo.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkRetainBTDeviceInfo.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkRetainBTDeviceInfo.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.chkRetainBTDeviceInfo.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkRetainBTDeviceInfo.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkRetainBTDeviceInfo.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.chkRetainBTDeviceInfo.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkRetainBTDeviceInfo.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkRetainBTDeviceInfo.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.chkRetainBTDeviceInfo.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkRetainBTDeviceInfo.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkRetainBTDeviceInfo.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkRetainBTDeviceInfo.isAutoCheck = true;
            this.chkRetainBTDeviceInfo.IsChecked = false;
            this.chkRetainBTDeviceInfo.IsCheckedInit = false;
            this.chkRetainBTDeviceInfo.Location = new System.Drawing.Point(417, 116);
            this.chkRetainBTDeviceInfo.Name = "chkRetainBTDeviceInfo";
            this.chkRetainBTDeviceInfo.Radius = 29;
            this.chkRetainBTDeviceInfo.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkRetainBTDeviceInfo.ShowBorder = false;
            this.chkRetainBTDeviceInfo.ShowText = true;
            this.chkRetainBTDeviceInfo.Size = new System.Drawing.Size(172, 29);
            this.chkRetainBTDeviceInfo.SwitchWdith = 48;
            this.chkRetainBTDeviceInfo.TabIndex = 65;
            this.chkRetainBTDeviceInfo.Text = "保留蓝牙设备信息";
            this.chkRetainBTDeviceInfo.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkRetainBTDeviceInfo.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkRetainBTDeviceInfo.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkRetainBTDeviceInfo.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkRetainBTDeviceInfo.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkRetainBTDeviceInfo.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkRetainBTDeviceInfo.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkRetainBTDeviceInfo.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkRetainBTDeviceInfo.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkRetainBTDeviceInfo.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkRetainBTDeviceInfo.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkRetainBTDeviceInfo.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            // 
            // cmbTargetMCUModel
            // 
            this.cmbTargetMCUModel.ArrowColor = System.Drawing.Color.Gray;
            this.cmbTargetMCUModel.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbTargetMCUModel.FormattingEnabled = true;
            this.cmbTargetMCUModel.Items.AddRange(new object[] {
            "AT32F415CBT7",
            "MH22D3RMU6"});
            this.cmbTargetMCUModel.Location = new System.Drawing.Point(286, 49);
            this.cmbTargetMCUModel.Name = "cmbTargetMCUModel";
            this.cmbTargetMCUModel.Size = new System.Drawing.Size(108, 23);
            this.cmbTargetMCUModel.TabIndex = 67;
            // 
            // lblTargetMCUModel
            // 
            this.lblTargetMCUModel.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTargetMCUModel.BackColor = System.Drawing.Color.Transparent;
            this.lblTargetMCUModel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblTargetMCUModel.BorderWidth = 1F;
            this.lblTargetMCUModel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblTargetMCUModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTargetMCUModel.Location = new System.Drawing.Point(226, 50);
            this.lblTargetMCUModel.Name = "lblTargetMCUModel";
            this.lblTargetMCUModel.Radius = 16;
            this.lblTargetMCUModel.ShowBackground = false;
            this.lblTargetMCUModel.ShowBorder = false;
            this.lblTargetMCUModel.Size = new System.Drawing.Size(65, 23);
            this.lblTargetMCUModel.TabIndex = 66;
            this.lblTargetMCUModel.Text = "MCU型号";
            // 
            // FormExportFirmwarePackage
            // 
            this.ClientSize = new System.Drawing.Size(960, 512);
            this.Controls.Add(this.cmbTargetMCUModel);
            this.Controls.Add(this.lblTargetMCUModel);
            this.Controls.Add(this.chkRetainBTDeviceInfo);
            this.Controls.Add(this.lblBluetoothName);
            this.Controls.Add(this.txtBluetoothName);
            this.Controls.Add(this.chkEnableRandomBTAddress);
            this.Controls.Add(this.chkForcedUpdateBTInfo);
            this.Controls.Add(this.txtActivateAddress);
            this.Controls.Add(this.txtMCUAppAddress);
            this.Controls.Add(this.lblMCUAppAddress);
            this.Controls.Add(this.lblActivateAddress);
            this.Controls.Add(this.txtX5CustomDeviceModelCode);
            this.Controls.Add(this.txtCustomDescription);
            this.Controls.Add(this.txtCustomVersion);
            this.Controls.Add(this.txtCustomModel);
            this.Controls.Add(this.lblCustomVersion);
            this.Controls.Add(this.lblCustomModel);
            this.Controls.Add(this.lblCustomDescription);
            this.Controls.Add(this.lblCustomManufacturer);
            this.Controls.Add(this.txtCustomManufacturer);
            this.Controls.Add(this.chkEnableCustomDeviceInfo);
            this.Controls.Add(this.chkWriteCustomDeviceInfo);
            this.Controls.Add(this.txtCustomDeviceModelCode);
            this.Controls.Add(this.chkWriteCustomModelCode);
            this.Controls.Add(this.cmbCustomDeviceInfoTask);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtMCUAllData);
            this.Controls.Add(this.mLabel1);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblKey);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.chkEncryptedExport);
            this.Controls.Add(this.chkUpdatePresetData);
            this.Controls.Add(this.txtPresetData);
            this.Controls.Add(this.chkUpdateCustomDeviceInfo);
            this.Controls.Add(this.txtCustomDeviceInfo);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.chkUpdateMCUAllData);
            this.Controls.Add(this.chkMCU);
            this.Controls.Add(this.chkDSP);
            this.Controls.Add(this.txtMCUBinFilePath);
            this.Controls.Add(this.txtDSPBinFilePath);
            this.Controls.Add(this.cmbTargetDevice);
            this.Controls.Add(this.lblTargetDevice);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormExportFirmwarePackage";
            this.Text = "导出固件升级包";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.Controls.SetChildIndex(this.lblTargetDevice, 0);
            this.Controls.SetChildIndex(this.cmbTargetDevice, 0);
            this.Controls.SetChildIndex(this.txtDSPBinFilePath, 0);
            this.Controls.SetChildIndex(this.txtMCUBinFilePath, 0);
            this.Controls.SetChildIndex(this.chkDSP, 0);
            this.Controls.SetChildIndex(this.chkMCU, 0);
            this.Controls.SetChildIndex(this.chkUpdateMCUAllData, 0);
            this.Controls.SetChildIndex(this.btnExport, 0);
            this.Controls.SetChildIndex(this.txtCustomDeviceInfo, 0);
            this.Controls.SetChildIndex(this.chkUpdateCustomDeviceInfo, 0);
            this.Controls.SetChildIndex(this.txtPresetData, 0);
            this.Controls.SetChildIndex(this.chkUpdatePresetData, 0);
            this.Controls.SetChildIndex(this.chkEncryptedExport, 0);
            this.Controls.SetChildIndex(this.txtKey, 0);
            this.Controls.SetChildIndex(this.lblKey, 0);
            this.Controls.SetChildIndex(this.txtDescription, 0);
            this.Controls.SetChildIndex(this.mLabel1, 0);
            this.Controls.SetChildIndex(this.txtMCUAllData, 0);
            this.Controls.SetChildIndex(this.lblName, 0);
            this.Controls.SetChildIndex(this.cmbCustomDeviceInfoTask, 0);
            this.Controls.SetChildIndex(this.chkWriteCustomModelCode, 0);
            this.Controls.SetChildIndex(this.txtCustomDeviceModelCode, 0);
            this.Controls.SetChildIndex(this.chkWriteCustomDeviceInfo, 0);
            this.Controls.SetChildIndex(this.chkEnableCustomDeviceInfo, 0);
            this.Controls.SetChildIndex(this.txtCustomManufacturer, 0);
            this.Controls.SetChildIndex(this.lblCustomManufacturer, 0);
            this.Controls.SetChildIndex(this.lblCustomDescription, 0);
            this.Controls.SetChildIndex(this.lblCustomModel, 0);
            this.Controls.SetChildIndex(this.lblCustomVersion, 0);
            this.Controls.SetChildIndex(this.txtCustomModel, 0);
            this.Controls.SetChildIndex(this.txtCustomVersion, 0);
            this.Controls.SetChildIndex(this.txtCustomDescription, 0);
            this.Controls.SetChildIndex(this.txtX5CustomDeviceModelCode, 0);
            this.Controls.SetChildIndex(this.lblActivateAddress, 0);
            this.Controls.SetChildIndex(this.lblMCUAppAddress, 0);
            this.Controls.SetChildIndex(this.txtMCUAppAddress, 0);
            this.Controls.SetChildIndex(this.txtActivateAddress, 0);
            this.Controls.SetChildIndex(this.chkForcedUpdateBTInfo, 0);
            this.Controls.SetChildIndex(this.chkEnableRandomBTAddress, 0);
            this.Controls.SetChildIndex(this.txtBluetoothName, 0);
            this.Controls.SetChildIndex(this.lblBluetoothName, 0);
            this.Controls.SetChildIndex(this.chkRetainBTDeviceInfo, 0);
            this.Controls.SetChildIndex(this.lblTargetMCUModel, 0);
            this.Controls.SetChildIndex(this.cmbTargetMCUModel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private gtComboBox cmbTargetDevice;
        private Controls.MLabel lblTargetDevice;
        private Controls.MSwitch chkUpdateMCUAllData;
        private Controls.MSwitch chkMCU;
        private Controls.MSwitch chkDSP;
        private System.Windows.Forms.TextBox txtMCUBinFilePath;
        private System.Windows.Forms.TextBox txtDSPBinFilePath;
        private Controls.MButton btnExport;
        private Controls.MSwitch chkUpdateCustomDeviceInfo;
        private System.Windows.Forms.TextBox txtCustomDeviceInfo;
        private Controls.MSwitch chkUpdatePresetData;
        private System.Windows.Forms.TextBox txtPresetData;
        private Controls.MSwitch chkEncryptedExport;
        private System.Windows.Forms.TextBox txtKey;
        private Controls.MLabel lblKey;
        private Controls.MLabel mLabel1;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtMCUAllData;
        private Controls.MLabel lblName;
        private gtComboBox cmbCustomDeviceInfoTask;
        private Controls.MSwitch chkWriteCustomModelCode;
        private System.Windows.Forms.TextBox txtCustomDeviceModelCode;
        private Controls.MSwitch chkWriteCustomDeviceInfo;
        private Controls.MSwitch chkEnableCustomDeviceInfo;
        private System.Windows.Forms.TextBox txtCustomManufacturer;
        private Controls.MLabel lblCustomManufacturer;
        private Controls.MLabel lblCustomDescription;
        private Controls.MLabel lblCustomModel;
        private Controls.MLabel lblCustomVersion;
        private System.Windows.Forms.TextBox txtCustomModel;
        private System.Windows.Forms.TextBox txtCustomDescription;
        private System.Windows.Forms.TextBox txtCustomVersion;
        private System.Windows.Forms.TextBox txtX5CustomDeviceModelCode;
        private Controls.MLabel lblActivateAddress;
        private Controls.MLabel lblMCUAppAddress;
        private System.Windows.Forms.TextBox txtMCUAppAddress;
        private System.Windows.Forms.TextBox txtActivateAddress;
        private Controls.MSwitch chkForcedUpdateBTInfo;
        private Controls.MSwitch chkEnableRandomBTAddress;
        private System.Windows.Forms.TextBox txtBluetoothName;
        private Controls.MLabel lblBluetoothName;
        private Controls.MSwitch chkRetainBTDeviceInfo;
        private gtComboBox cmbTargetMCUModel;
        private Controls.MLabel lblTargetMCUModel;
    }
}
