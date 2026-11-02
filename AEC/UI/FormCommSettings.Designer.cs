namespace AEC.UI
{
    partial class FormCommSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCommSettings));
            this.txtUDPPort = new System.Windows.Forms.TextBox();
            this.txtUDPIPAddress = new System.Windows.Forms.TextBox();
            this.txtTCPPort = new System.Windows.Forms.TextBox();
            this.txtTCPIPAddress = new System.Windows.Forms.TextBox();
            this.mSwitchSerialPortManualMode = new AEC.UI.Controls.MSwitch();
            this.lblTCP = new AEC.UI.gtLabel();
            this.mSwitchCommViaTCP = new AEC.UI.Controls.MSwitch();
            this.mSwitchCommViaUDP = new AEC.UI.Controls.MSwitch();
            this.mSwitchCommViaRS232 = new AEC.UI.Controls.MSwitch();
            this.mSwitchCommViaUSB = new AEC.UI.Controls.MSwitch();
            this.lblPort = new AEC.UI.gtLabel();
            this.lblIPAddress = new AEC.UI.gtLabel();
            this.splitLine3 = new AEC.UI.SplitLine();
            this.splitLine2 = new AEC.UI.SplitLine();
            this.splitLine1 = new AEC.UI.SplitLine();
            this.cmbCommRetransmission = new AEC.UI.gtComboBox();
            this.lblCommRetransmission = new AEC.UI.gtLabel();
            this.btnRestoreToDefault = new AEC.UI.gtButton();
            this.btnClose = new AEC.UI.gtButton();
            this.cmbDefaultComPort = new AEC.UI.gtComboBox();
            this.lblDefaultSerialPort = new AEC.UI.gtLabel();
            this.lblCommMethod = new AEC.UI.gtLabel();
            this.lblUDP = new AEC.UI.gtLabel();
            this.title = new AEC.UI.Title();
            this.linkHelp = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // txtUDPPort
            // 
            this.txtUDPPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.txtUDPPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUDPPort.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtUDPPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.txtUDPPort.Location = new System.Drawing.Point(297, 323);
            this.txtUDPPort.Name = "txtUDPPort";
            this.txtUDPPort.Size = new System.Drawing.Size(67, 27);
            this.txtUDPPort.TabIndex = 15;
            // 
            // txtUDPIPAddress
            // 
            this.txtUDPIPAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.txtUDPIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUDPIPAddress.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtUDPIPAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.txtUDPIPAddress.Location = new System.Drawing.Point(167, 323);
            this.txtUDPIPAddress.Name = "txtUDPIPAddress";
            this.txtUDPIPAddress.Size = new System.Drawing.Size(124, 27);
            this.txtUDPIPAddress.TabIndex = 16;
            // 
            // txtTCPPort
            // 
            this.txtTCPPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.txtTCPPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTCPPort.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtTCPPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.txtTCPPort.Location = new System.Drawing.Point(297, 360);
            this.txtTCPPort.Name = "txtTCPPort";
            this.txtTCPPort.Size = new System.Drawing.Size(67, 27);
            this.txtTCPPort.TabIndex = 27;
            // 
            // txtTCPIPAddress
            // 
            this.txtTCPIPAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.txtTCPIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTCPIPAddress.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtTCPIPAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.txtTCPIPAddress.Location = new System.Drawing.Point(167, 360);
            this.txtTCPIPAddress.Name = "txtTCPIPAddress";
            this.txtTCPIPAddress.Size = new System.Drawing.Size(124, 27);
            this.txtTCPIPAddress.TabIndex = 28;
            // 
            // mSwitchSerialPortManualMode
            // 
            this.mSwitchSerialPortManualMode.BackColor = System.Drawing.Color.Transparent;
            this.mSwitchSerialPortManualMode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchSerialPortManualMode.BorderWidth = 1F;
            this.mSwitchSerialPortManualMode.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.mSwitchSerialPortManualMode.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.mSwitchSerialPortManualMode.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.mSwitchSerialPortManualMode.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.mSwitchSerialPortManualMode.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.mSwitchSerialPortManualMode.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mSwitchSerialPortManualMode.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchSerialPortManualMode.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.mSwitchSerialPortManualMode.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mSwitchSerialPortManualMode.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.mSwitchSerialPortManualMode.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.mSwitchSerialPortManualMode.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchSerialPortManualMode.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.mSwitchSerialPortManualMode.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.mSwitchSerialPortManualMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.mSwitchSerialPortManualMode.isAutoCheck = true;
            this.mSwitchSerialPortManualMode.IsChecked = false;
            this.mSwitchSerialPortManualMode.IsCheckedInit = false;
            this.mSwitchSerialPortManualMode.Location = new System.Drawing.Point(167, 240);
            this.mSwitchSerialPortManualMode.Name = "mSwitchSerialPortManualMode";
            this.mSwitchSerialPortManualMode.Radius = 29;
            this.mSwitchSerialPortManualMode.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.mSwitchSerialPortManualMode.ShowBorder = false;
            this.mSwitchSerialPortManualMode.ShowText = true;
            this.mSwitchSerialPortManualMode.Size = new System.Drawing.Size(219, 29);
            this.mSwitchSerialPortManualMode.SwitchWdith = 48;
            this.mSwitchSerialPortManualMode.TabIndex = 29;
            this.mSwitchSerialPortManualMode.Text = "RS232 端口手动模式";
            this.mSwitchSerialPortManualMode.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.mSwitchSerialPortManualMode.UncheckedDisbaledFillColor = System.Drawing.Color.Gray;
            this.mSwitchSerialPortManualMode.UncheckedDisbaledTextColor = System.Drawing.Color.Silver;
            this.mSwitchSerialPortManualMode.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.mSwitchSerialPortManualMode.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mSwitchSerialPortManualMode.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mSwitchSerialPortManualMode.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchSerialPortManualMode.UncheckedNormalFillColor = System.Drawing.Color.Silver;
            this.mSwitchSerialPortManualMode.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.mSwitchSerialPortManualMode.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.mSwitchSerialPortManualMode.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchSerialPortManualMode.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mSwitchSerialPortManualMode.CheckedChanged += new System.EventHandler(this.switch_CheckedChanged);
            // 
            // lblTCP
            // 
            this.lblTCP.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblTCP.BackColor = System.Drawing.Color.Transparent;
            this.lblTCP.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblTCP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblTCP.Location = new System.Drawing.Point(9, 364);
            this.lblTCP.Name = "lblTCP";
            this.lblTCP.Size = new System.Drawing.Size(148, 23);
            this.lblTCP.TabIndex = 26;
            this.lblTCP.Text = "TCP";
            // 
            // mSwitchCommViaTCP
            // 
            this.mSwitchCommViaTCP.BackColor = System.Drawing.Color.Transparent;
            this.mSwitchCommViaTCP.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchCommViaTCP.BorderWidth = 1F;
            this.mSwitchCommViaTCP.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.mSwitchCommViaTCP.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.mSwitchCommViaTCP.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.mSwitchCommViaTCP.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.mSwitchCommViaTCP.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.mSwitchCommViaTCP.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mSwitchCommViaTCP.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchCommViaTCP.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.mSwitchCommViaTCP.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mSwitchCommViaTCP.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.mSwitchCommViaTCP.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.mSwitchCommViaTCP.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchCommViaTCP.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.mSwitchCommViaTCP.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.mSwitchCommViaTCP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.mSwitchCommViaTCP.isAutoCheck = false;
            this.mSwitchCommViaTCP.IsChecked = false;
            this.mSwitchCommViaTCP.IsCheckedInit = false;
            this.mSwitchCommViaTCP.Location = new System.Drawing.Point(275, 84);
            this.mSwitchCommViaTCP.Name = "mSwitchCommViaTCP";
            this.mSwitchCommViaTCP.Radius = 29;
            this.mSwitchCommViaTCP.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.mSwitchCommViaTCP.ShowBorder = false;
            this.mSwitchCommViaTCP.ShowText = true;
            this.mSwitchCommViaTCP.Size = new System.Drawing.Size(96, 29);
            this.mSwitchCommViaTCP.SwitchWdith = 48;
            this.mSwitchCommViaTCP.TabIndex = 25;
            this.mSwitchCommViaTCP.Text = "TCP";
            this.mSwitchCommViaTCP.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.mSwitchCommViaTCP.UncheckedDisbaledFillColor = System.Drawing.Color.Gray;
            this.mSwitchCommViaTCP.UncheckedDisbaledTextColor = System.Drawing.Color.Silver;
            this.mSwitchCommViaTCP.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.mSwitchCommViaTCP.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mSwitchCommViaTCP.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mSwitchCommViaTCP.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchCommViaTCP.UncheckedNormalFillColor = System.Drawing.Color.Silver;
            this.mSwitchCommViaTCP.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.mSwitchCommViaTCP.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.mSwitchCommViaTCP.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchCommViaTCP.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mSwitchCommViaTCP.CheckedChanged += new System.EventHandler(this.switch_CheckedChanged);
            this.mSwitchCommViaTCP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.switch_MouseClick);
            // 
            // mSwitchCommViaUDP
            // 
            this.mSwitchCommViaUDP.BackColor = System.Drawing.Color.Transparent;
            this.mSwitchCommViaUDP.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchCommViaUDP.BorderWidth = 1F;
            this.mSwitchCommViaUDP.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.mSwitchCommViaUDP.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.mSwitchCommViaUDP.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.mSwitchCommViaUDP.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.mSwitchCommViaUDP.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.mSwitchCommViaUDP.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mSwitchCommViaUDP.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchCommViaUDP.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.mSwitchCommViaUDP.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mSwitchCommViaUDP.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.mSwitchCommViaUDP.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.mSwitchCommViaUDP.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchCommViaUDP.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.mSwitchCommViaUDP.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.mSwitchCommViaUDP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.mSwitchCommViaUDP.isAutoCheck = false;
            this.mSwitchCommViaUDP.IsChecked = false;
            this.mSwitchCommViaUDP.IsCheckedInit = false;
            this.mSwitchCommViaUDP.Location = new System.Drawing.Point(167, 84);
            this.mSwitchCommViaUDP.Name = "mSwitchCommViaUDP";
            this.mSwitchCommViaUDP.Radius = 29;
            this.mSwitchCommViaUDP.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.mSwitchCommViaUDP.ShowBorder = false;
            this.mSwitchCommViaUDP.ShowText = true;
            this.mSwitchCommViaUDP.Size = new System.Drawing.Size(96, 29);
            this.mSwitchCommViaUDP.SwitchWdith = 48;
            this.mSwitchCommViaUDP.TabIndex = 24;
            this.mSwitchCommViaUDP.Text = "UDP";
            this.mSwitchCommViaUDP.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.mSwitchCommViaUDP.UncheckedDisbaledFillColor = System.Drawing.Color.Gray;
            this.mSwitchCommViaUDP.UncheckedDisbaledTextColor = System.Drawing.Color.Silver;
            this.mSwitchCommViaUDP.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.mSwitchCommViaUDP.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mSwitchCommViaUDP.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mSwitchCommViaUDP.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchCommViaUDP.UncheckedNormalFillColor = System.Drawing.Color.Silver;
            this.mSwitchCommViaUDP.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.mSwitchCommViaUDP.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.mSwitchCommViaUDP.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchCommViaUDP.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mSwitchCommViaUDP.CheckedChanged += new System.EventHandler(this.switch_CheckedChanged);
            this.mSwitchCommViaUDP.MouseClick += new System.Windows.Forms.MouseEventHandler(this.switch_MouseClick);
            // 
            // mSwitchCommViaRS232
            // 
            this.mSwitchCommViaRS232.BackColor = System.Drawing.Color.Transparent;
            this.mSwitchCommViaRS232.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchCommViaRS232.BorderWidth = 1F;
            this.mSwitchCommViaRS232.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.mSwitchCommViaRS232.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.mSwitchCommViaRS232.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.mSwitchCommViaRS232.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.mSwitchCommViaRS232.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.mSwitchCommViaRS232.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mSwitchCommViaRS232.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchCommViaRS232.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.mSwitchCommViaRS232.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mSwitchCommViaRS232.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.mSwitchCommViaRS232.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.mSwitchCommViaRS232.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchCommViaRS232.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.mSwitchCommViaRS232.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.mSwitchCommViaRS232.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.mSwitchCommViaRS232.isAutoCheck = false;
            this.mSwitchCommViaRS232.IsChecked = false;
            this.mSwitchCommViaRS232.IsCheckedInit = false;
            this.mSwitchCommViaRS232.Location = new System.Drawing.Point(275, 43);
            this.mSwitchCommViaRS232.Name = "mSwitchCommViaRS232";
            this.mSwitchCommViaRS232.Radius = 29;
            this.mSwitchCommViaRS232.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.mSwitchCommViaRS232.ShowBorder = false;
            this.mSwitchCommViaRS232.ShowText = true;
            this.mSwitchCommViaRS232.Size = new System.Drawing.Size(96, 29);
            this.mSwitchCommViaRS232.SwitchWdith = 48;
            this.mSwitchCommViaRS232.TabIndex = 23;
            this.mSwitchCommViaRS232.Text = "RS232";
            this.mSwitchCommViaRS232.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.mSwitchCommViaRS232.UncheckedDisbaledFillColor = System.Drawing.Color.Gray;
            this.mSwitchCommViaRS232.UncheckedDisbaledTextColor = System.Drawing.Color.Silver;
            this.mSwitchCommViaRS232.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.mSwitchCommViaRS232.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mSwitchCommViaRS232.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mSwitchCommViaRS232.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchCommViaRS232.UncheckedNormalFillColor = System.Drawing.Color.Silver;
            this.mSwitchCommViaRS232.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.mSwitchCommViaRS232.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.mSwitchCommViaRS232.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchCommViaRS232.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mSwitchCommViaRS232.CheckedChanged += new System.EventHandler(this.switch_CheckedChanged);
            this.mSwitchCommViaRS232.MouseClick += new System.Windows.Forms.MouseEventHandler(this.switch_MouseClick);
            // 
            // mSwitchCommViaUSB
            // 
            this.mSwitchCommViaUSB.BackColor = System.Drawing.Color.Transparent;
            this.mSwitchCommViaUSB.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchCommViaUSB.BorderWidth = 1F;
            this.mSwitchCommViaUSB.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.mSwitchCommViaUSB.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.mSwitchCommViaUSB.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.mSwitchCommViaUSB.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.mSwitchCommViaUSB.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.mSwitchCommViaUSB.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mSwitchCommViaUSB.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchCommViaUSB.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.mSwitchCommViaUSB.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mSwitchCommViaUSB.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.mSwitchCommViaUSB.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.mSwitchCommViaUSB.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchCommViaUSB.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.mSwitchCommViaUSB.Enabled = false;
            this.mSwitchCommViaUSB.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.mSwitchCommViaUSB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.mSwitchCommViaUSB.isAutoCheck = false;
            this.mSwitchCommViaUSB.IsChecked = false;
            this.mSwitchCommViaUSB.IsCheckedInit = false;
            this.mSwitchCommViaUSB.Location = new System.Drawing.Point(167, 43);
            this.mSwitchCommViaUSB.Name = "mSwitchCommViaUSB";
            this.mSwitchCommViaUSB.Radius = 29;
            this.mSwitchCommViaUSB.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.mSwitchCommViaUSB.ShowBorder = false;
            this.mSwitchCommViaUSB.ShowText = true;
            this.mSwitchCommViaUSB.Size = new System.Drawing.Size(96, 29);
            this.mSwitchCommViaUSB.SwitchWdith = 48;
            this.mSwitchCommViaUSB.TabIndex = 22;
            this.mSwitchCommViaUSB.Text = "USB";
            this.mSwitchCommViaUSB.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.mSwitchCommViaUSB.UncheckedDisbaledFillColor = System.Drawing.Color.Gray;
            this.mSwitchCommViaUSB.UncheckedDisbaledTextColor = System.Drawing.Color.Silver;
            this.mSwitchCommViaUSB.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.mSwitchCommViaUSB.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mSwitchCommViaUSB.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mSwitchCommViaUSB.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchCommViaUSB.UncheckedNormalFillColor = System.Drawing.Color.Silver;
            this.mSwitchCommViaUSB.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.mSwitchCommViaUSB.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.mSwitchCommViaUSB.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.mSwitchCommViaUSB.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mSwitchCommViaUSB.CheckedChanged += new System.EventHandler(this.switch_CheckedChanged);
            this.mSwitchCommViaUSB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.switch_MouseClick);
            // 
            // lblPort
            // 
            this.lblPort.alignment = AEC.UI.gtLabel.AlignMent.Left;
            this.lblPort.BackColor = System.Drawing.Color.Transparent;
            this.lblPort.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblPort.Location = new System.Drawing.Point(297, 297);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(34, 23);
            this.lblPort.TabIndex = 20;
            this.lblPort.Text = "端口";
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.alignment = AEC.UI.gtLabel.AlignMent.Left;
            this.lblIPAddress.BackColor = System.Drawing.Color.Transparent;
            this.lblIPAddress.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblIPAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblIPAddress.Location = new System.Drawing.Point(167, 297);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(83, 23);
            this.lblIPAddress.TabIndex = 21;
            this.lblIPAddress.Text = "IP地址";
            // 
            // splitLine3
            // 
            this.splitLine3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.splitLine3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.splitLine3.Location = new System.Drawing.Point(167, 282);
            this.splitLine3.Margin = new System.Windows.Forms.Padding(0);
            this.splitLine3.Name = "splitLine3";
            this.splitLine3.Size = new System.Drawing.Size(219, 1);
            this.splitLine3.TabIndex = 19;
            this.splitLine3.Text = "splitLine3";
            // 
            // splitLine2
            // 
            this.splitLine2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.splitLine2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.splitLine2.Location = new System.Drawing.Point(167, 185);
            this.splitLine2.Margin = new System.Windows.Forms.Padding(0);
            this.splitLine2.Name = "splitLine2";
            this.splitLine2.Size = new System.Drawing.Size(219, 1);
            this.splitLine2.TabIndex = 18;
            this.splitLine2.Text = "splitLine2";
            // 
            // splitLine1
            // 
            this.splitLine1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.splitLine1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.splitLine1.Location = new System.Drawing.Point(167, 129);
            this.splitLine1.Margin = new System.Windows.Forms.Padding(0);
            this.splitLine1.Name = "splitLine1";
            this.splitLine1.Size = new System.Drawing.Size(219, 1);
            this.splitLine1.TabIndex = 17;
            this.splitLine1.Text = "splitLine1";
            // 
            // cmbCommRetransmission
            // 
            this.cmbCommRetransmission.ArrowColor = System.Drawing.Color.Gray;
            this.cmbCommRetransmission.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbCommRetransmission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCommRetransmission.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.cmbCommRetransmission.FormattingEnabled = true;
            this.cmbCommRetransmission.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.cmbCommRetransmission.Location = new System.Drawing.Point(167, 145);
            this.cmbCommRetransmission.Name = "cmbCommRetransmission";
            this.cmbCommRetransmission.Size = new System.Drawing.Size(219, 23);
            this.cmbCommRetransmission.TabIndex = 14;
            // 
            // lblCommRetransmission
            // 
            this.lblCommRetransmission.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblCommRetransmission.BackColor = System.Drawing.Color.Transparent;
            this.lblCommRetransmission.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblCommRetransmission.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblCommRetransmission.Location = new System.Drawing.Point(9, 145);
            this.lblCommRetransmission.Name = "lblCommRetransmission";
            this.lblCommRetransmission.Size = new System.Drawing.Size(148, 23);
            this.lblCommRetransmission.TabIndex = 13;
            this.lblCommRetransmission.Text = "通讯重传次数";
            // 
            // btnRestoreToDefault
            // 
            this.btnRestoreToDefault.BackColor = System.Drawing.Color.Transparent;
            this.btnRestoreToDefault.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.btnRestoreToDefault.backHover = global::AEC.Properties.Resources.btn_hover;
            this.btnRestoreToDefault.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.btnRestoreToDefault.backPressed = global::AEC.Properties.Resources.btn_pressed;
            this.btnRestoreToDefault.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnRestoreToDefault.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.btnRestoreToDefault.isStretch = true;
            this.btnRestoreToDefault.Location = new System.Drawing.Point(112, 406);
            this.btnRestoreToDefault.Name = "btnRestoreToDefault";
            this.btnRestoreToDefault.Size = new System.Drawing.Size(118, 30);
            this.btnRestoreToDefault.TabIndex = 10;
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
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.btnClose.isStretch = true;
            this.btnClose.Location = new System.Drawing.Point(250, 406);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(118, 30);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "关闭";
            this.btnClose.userGDIPaint = true;
            this.btnClose.Click += new System.EventHandler(this.btn_Click);
            // 
            // cmbDefaultComPort
            // 
            this.cmbDefaultComPort.ArrowColor = System.Drawing.Color.Gray;
            this.cmbDefaultComPort.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbDefaultComPort.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.cmbDefaultComPort.FormattingEnabled = true;
            this.cmbDefaultComPort.Location = new System.Drawing.Point(167, 202);
            this.cmbDefaultComPort.Name = "cmbDefaultComPort";
            this.cmbDefaultComPort.Size = new System.Drawing.Size(219, 23);
            this.cmbDefaultComPort.TabIndex = 9;
            // 
            // lblDefaultSerialPort
            // 
            this.lblDefaultSerialPort.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblDefaultSerialPort.BackColor = System.Drawing.Color.Transparent;
            this.lblDefaultSerialPort.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblDefaultSerialPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblDefaultSerialPort.Location = new System.Drawing.Point(9, 202);
            this.lblDefaultSerialPort.Name = "lblDefaultSerialPort";
            this.lblDefaultSerialPort.Size = new System.Drawing.Size(148, 23);
            this.lblDefaultSerialPort.TabIndex = 6;
            this.lblDefaultSerialPort.Text = "串口通讯默认端口号";
            // 
            // lblCommMethod
            // 
            this.lblCommMethod.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblCommMethod.BackColor = System.Drawing.Color.Transparent;
            this.lblCommMethod.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblCommMethod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblCommMethod.Location = new System.Drawing.Point(9, 49);
            this.lblCommMethod.Name = "lblCommMethod";
            this.lblCommMethod.Size = new System.Drawing.Size(148, 23);
            this.lblCommMethod.TabIndex = 6;
            this.lblCommMethod.Text = "通讯方式";
            // 
            // lblUDP
            // 
            this.lblUDP.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblUDP.BackColor = System.Drawing.Color.Transparent;
            this.lblUDP.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblUDP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblUDP.Location = new System.Drawing.Point(9, 323);
            this.lblUDP.Name = "lblUDP";
            this.lblUDP.Size = new System.Drawing.Size(148, 23);
            this.lblUDP.TabIndex = 6;
            this.lblUDP.Text = "UDP";
            // 
            // title
            // 
            this.title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.title.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.isAllowDoubleClickMaximize = false;
            this.title.isShowCloseButton = true;
            this.title.isShowSystemBtns = false;
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Margin = new System.Windows.Forms.Padding(0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(480, 32);
            this.title.TabIndex = 4;
            // 
            // linkHelp
            // 
            this.linkHelp.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.linkHelp.AutoSize = true;
            this.linkHelp.BackColor = System.Drawing.Color.Transparent;
            this.linkHelp.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkHelp.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.linkHelp.Location = new System.Drawing.Point(353, 301);
            this.linkHelp.Name = "linkHelp";
            this.linkHelp.Size = new System.Drawing.Size(33, 15);
            this.linkHelp.TabIndex = 61;
            this.linkHelp.TabStop = true;
            this.linkHelp.Text = "帮助";
            this.linkHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkHelp_LinkClicked);
            // 
            // FormCommSettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(480, 472);
            this.Controls.Add(this.linkHelp);
            this.Controls.Add(this.mSwitchSerialPortManualMode);
            this.Controls.Add(this.txtTCPPort);
            this.Controls.Add(this.txtTCPIPAddress);
            this.Controls.Add(this.lblTCP);
            this.Controls.Add(this.mSwitchCommViaTCP);
            this.Controls.Add(this.mSwitchCommViaUDP);
            this.Controls.Add(this.mSwitchCommViaRS232);
            this.Controls.Add(this.mSwitchCommViaUSB);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblIPAddress);
            this.Controls.Add(this.splitLine3);
            this.Controls.Add(this.splitLine2);
            this.Controls.Add(this.splitLine1);
            this.Controls.Add(this.txtUDPPort);
            this.Controls.Add(this.txtUDPIPAddress);
            this.Controls.Add(this.cmbCommRetransmission);
            this.Controls.Add(this.lblCommRetransmission);
            this.Controls.Add(this.btnRestoreToDefault);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cmbDefaultComPort);
            this.Controls.Add(this.lblDefaultSerialPort);
            this.Controls.Add(this.lblCommMethod);
            this.Controls.Add(this.lblUDP);
            this.Controls.Add(this.title);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormCommSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "联机设置";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormConnectionSetting_FormClosed);
            this.Load += new System.EventHandler(this.FormConnectionSetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Title title;
        private gtLabel lblCommMethod;
        private gtLabel lblDefaultSerialPort;
        private gtComboBox cmbDefaultComPort;
        private gtButton btnRestoreToDefault;
        private gtButton btnClose;
        private gtComboBox cmbCommRetransmission;
        private gtLabel lblCommRetransmission;
        private gtLabel lblUDP;
        private System.Windows.Forms.TextBox txtUDPPort;
        private System.Windows.Forms.TextBox txtUDPIPAddress;
        private SplitLine splitLine1;
        private SplitLine splitLine2;
        private SplitLine splitLine3;
        private gtLabel lblPort;
        private gtLabel lblIPAddress;
        private Controls.MSwitch mSwitchCommViaUSB;
        private Controls.MSwitch mSwitchCommViaRS232;
        private Controls.MSwitch mSwitchCommViaTCP;
        private Controls.MSwitch mSwitchCommViaUDP;
        private gtLabel lblTCP;
        private System.Windows.Forms.TextBox txtTCPPort;
        private System.Windows.Forms.TextBox txtTCPIPAddress;
        private Controls.MSwitch mSwitchSerialPortManualMode;
        private System.Windows.Forms.LinkLabel linkHelp;
    }
}