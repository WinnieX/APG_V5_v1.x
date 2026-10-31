namespace AEC.UI
{
    partial class FormProMode
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProMode));
            this.panelToolBar = new System.Windows.Forms.TableLayoutPanel();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDeviceModels = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRTAOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCommSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPreferences = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmizh_CN = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmien_US = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOnlineHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBluetoothRename = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFirmwareUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiActivateDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenLogFileLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConnect = new AEC.UI.ConnectButton();
            this.rtaSwitch = new AEC.UI.RTASwitch();
            this.systemControlPanel = new AEC.UI.SystemParamControlPanel();
            this.title = new AEC.UI.Title();
            this.eqGraphicPanel = new AEC.UI.EQGraphicPanel();
            this.panelToolBar.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelToolBar
            // 
            this.panelToolBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolBar.BackColor = System.Drawing.Color.Silver;
            this.panelToolBar.ColumnCount = 2;
            this.panelToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 551F));
            this.panelToolBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelToolBar.Controls.Add(this.systemControlPanel, 1, 0);
            this.panelToolBar.Location = new System.Drawing.Point(0, 32);
            this.panelToolBar.Name = "panelToolBar";
            this.panelToolBar.RowCount = 1;
            this.panelToolBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelToolBar.Size = new System.Drawing.Size(1280, 35);
            this.panelToolBar.TabIndex = 6;
            this.panelToolBar.Paint += new System.Windows.Forms.PaintEventHandler(this.panelToolBar_Paint);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDeviceModels,
            this.tsmiRTAOptions,
            this.tsmiCommSettings,
            this.tsmiPreferences,
            this.tsmiLanguage,
            this.tsmiHelp});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(169, 136);
            // 
            // tsmiDeviceModels
            // 
            this.tsmiDeviceModels.Name = "tsmiDeviceModels";
            this.tsmiDeviceModels.Size = new System.Drawing.Size(168, 22);
            this.tsmiDeviceModels.Text = "设备型号";
            // 
            // tsmiRTAOptions
            // 
            this.tsmiRTAOptions.Name = "tsmiRTAOptions";
            this.tsmiRTAOptions.Size = new System.Drawing.Size(168, 22);
            this.tsmiRTAOptions.Text = "RTA 选项";
            this.tsmiRTAOptions.Click += new System.EventHandler(this.tsmi_Click);
            // 
            // tsmiCommSettings
            // 
            this.tsmiCommSettings.Name = "tsmiCommSettings";
            this.tsmiCommSettings.Size = new System.Drawing.Size(168, 22);
            this.tsmiCommSettings.Text = "通讯设置";
            this.tsmiCommSettings.Click += new System.EventHandler(this.tsmi_Click);
            // 
            // tsmiPreferences
            // 
            this.tsmiPreferences.Name = "tsmiPreferences";
            this.tsmiPreferences.Size = new System.Drawing.Size(168, 22);
            this.tsmiPreferences.Text = "首选项";
            this.tsmiPreferences.Click += new System.EventHandler(this.tsmi_Click);
            // 
            // tsmiLanguage
            // 
            this.tsmiLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmizh_CN,
            this.tsmien_US});
            this.tsmiLanguage.Name = "tsmiLanguage";
            this.tsmiLanguage.Size = new System.Drawing.Size(168, 22);
            this.tsmiLanguage.Text = "语言/Languages";
            // 
            // tsmizh_CN
            // 
            this.tsmizh_CN.Checked = true;
            this.tsmizh_CN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmizh_CN.Name = "tsmizh_CN";
            this.tsmizh_CN.Size = new System.Drawing.Size(117, 22);
            this.tsmizh_CN.Text = "中文";
            this.tsmizh_CN.Click += new System.EventHandler(this.tsmi_Click);
            // 
            // tsmien_US
            // 
            this.tsmien_US.Name = "tsmien_US";
            this.tsmien_US.Size = new System.Drawing.Size(117, 22);
            this.tsmien_US.Text = "English";
            this.tsmien_US.Click += new System.EventHandler(this.tsmi_Click);
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOnlineHelp,
            this.tsmiBluetoothRename,
            this.tsmiFirmwareUpdate,
            this.tsmiActivateDevice,
            this.tsmiOpenLogFileLocation,
            this.tsmiAbout});
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(168, 22);
            this.tsmiHelp.Text = "帮助";
            // 
            // tsmiOnlineHelp
            // 
            this.tsmiOnlineHelp.Name = "tsmiOnlineHelp";
            this.tsmiOnlineHelp.Size = new System.Drawing.Size(172, 22);
            this.tsmiOnlineHelp.Text = "联机帮助";
            this.tsmiOnlineHelp.Click += new System.EventHandler(this.tsmi_Click);
            // 
            // tsmiBluetoothRename
            // 
            this.tsmiBluetoothRename.Name = "tsmiBluetoothRename";
            this.tsmiBluetoothRename.Size = new System.Drawing.Size(172, 22);
            this.tsmiBluetoothRename.Text = "蓝牙重命名";
            this.tsmiBluetoothRename.Click += new System.EventHandler(this.tsmi_Click);
            // 
            // tsmiFirmwareUpdate
            // 
            this.tsmiFirmwareUpdate.Name = "tsmiFirmwareUpdate";
            this.tsmiFirmwareUpdate.Size = new System.Drawing.Size(172, 22);
            this.tsmiFirmwareUpdate.Text = "固件升级";
            this.tsmiFirmwareUpdate.Click += new System.EventHandler(this.tsmi_Click);
            // 
            // tsmiActivateDevice
            // 
            this.tsmiActivateDevice.Name = "tsmiActivateDevice";
            this.tsmiActivateDevice.Size = new System.Drawing.Size(172, 22);
            this.tsmiActivateDevice.Text = "激活设备";
            this.tsmiActivateDevice.Click += new System.EventHandler(this.tsmi_Click);
            // 
            // tsmiOpenLogFileLocation
            // 
            this.tsmiOpenLogFileLocation.Name = "tsmiOpenLogFileLocation";
            this.tsmiOpenLogFileLocation.Size = new System.Drawing.Size(172, 22);
            this.tsmiOpenLogFileLocation.Text = "打开日志文件目录";
            this.tsmiOpenLogFileLocation.Click += new System.EventHandler(this.tsmi_Click);
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(172, 22);
            this.tsmiAbout.Text = "关于";
            this.tsmiAbout.Click += new System.EventHandler(this.tsmi_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.BackColor = System.Drawing.Color.Transparent;
            this.btnConnect.connectDisabled = global::AEC.Properties.Resources.connect_disabled;
            this.btnConnect.connectHover = global::AEC.Properties.Resources.connect_hover;
            this.btnConnect.connectNormal = global::AEC.Properties.Resources.connect_normal;
            this.btnConnect.connectPressed = global::AEC.Properties.Resources.connect_pressed;
            this.btnConnect.disconnectHover = global::AEC.Properties.Resources.connected_hover;
            this.btnConnect.disconnectNormal = global::AEC.Properties.Resources.connected_normal;
            this.btnConnect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnConnect.isConnected = false;
            this.btnConnect.isStretch = false;
            this.btnConnect.Location = new System.Drawing.Point(1032, 2);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(0);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(59, 29);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "联机";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // rtaSwitch
            // 
            this.rtaSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rtaSwitch.BackColor = System.Drawing.Color.Transparent;
            this.rtaSwitch.backDisabled = global::AEC.Properties.Resources.rta_switch1_disabled;
            this.rtaSwitch.backHover = global::AEC.Properties.Resources.rta_switch1_off_hover;
            this.rtaSwitch.backNormal = global::AEC.Properties.Resources.rta_switch1_off_normal;
            this.rtaSwitch.backPressed = global::AEC.Properties.Resources.rta_switch1_off_pressed;
            this.rtaSwitch.ForeColor = System.Drawing.Color.White;
            this.rtaSwitch.isOn = false;
            this.rtaSwitch.isStretch = false;
            this.rtaSwitch.Location = new System.Drawing.Point(1188, 338);
            this.rtaSwitch.Name = "rtaSwitch";
            this.rtaSwitch.offHover = global::AEC.Properties.Resources.rta_switch1_off_hover;
            this.rtaSwitch.offNormal = global::AEC.Properties.Resources.rta_switch1_off_normal;
            this.rtaSwitch.offPressed = global::AEC.Properties.Resources.rta_switch1_off_pressed;
            this.rtaSwitch.onHover = global::AEC.Properties.Resources.rta_switch1_on_hover;
            this.rtaSwitch.onNormal = global::AEC.Properties.Resources.rta_switch1_on_normal;
            this.rtaSwitch.onPressed = global::AEC.Properties.Resources.rta_switch1_on_pressed;
            this.rtaSwitch.Size = new System.Drawing.Size(86, 38);
            this.rtaSwitch.TabIndex = 32;
            this.rtaSwitch.userGDIPaint = false;
            this.rtaSwitch.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chkRTA_MouseClick);
            // 
            // systemControlPanel
            // 
            this.systemControlPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.systemControlPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.systemControlPanel.BorderWidth = 1F;
            this.systemControlPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.systemControlPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.systemControlPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.systemControlPanel.Location = new System.Drawing.Point(551, 0);
            this.systemControlPanel.Margin = new System.Windows.Forms.Padding(0);
            this.systemControlPanel.Name = "systemControlPanel";
            this.systemControlPanel.Radius = 0;
            this.systemControlPanel.ShowBorder = false;
            this.systemControlPanel.ShowText = false;
            this.systemControlPanel.Size = new System.Drawing.Size(729, 32);
            this.systemControlPanel.TabIndex = 7;
            this.systemControlPanel.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.systemControlPanel.TextPadding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            // 
            // title
            // 
            this.title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.isAllowDoubleClickMaximize = true;
            this.title.isShowCloseButton = true;
            this.title.isShowSystemBtns = true;
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(1280, 32);
            this.title.TabIndex = 3;
            // 
            // eqGraphicPanel
            // 
            this.eqGraphicPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eqGraphicPanel.BackColor = System.Drawing.Color.Black;
            this.eqGraphicPanel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.eqGraphicPanel.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.eqGraphicPanel.Location = new System.Drawing.Point(2, 378);
            this.eqGraphicPanel.Margin = new System.Windows.Forms.Padding(0);
            this.eqGraphicPanel.Name = "eqGraphicPanel";
            this.eqGraphicPanel.Size = new System.Drawing.Size(1276, 340);
            this.eqGraphicPanel.TabIndex = 0;
            this.eqGraphicPanel.Text = "eqGraphicPanel";
            // 
            // FormProMode
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.rtaSwitch);
            this.Controls.Add(this.panelToolBar);
            this.Controls.Add(this.title);
            this.Controls.Add(this.eqGraphicPanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormProMode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "V5 AEC 专业模式 v1.0.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormProMode_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormProMode_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormProMode_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormProMode_KeyDown);
            this.panelToolBar.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private EQGraphicPanel eqGraphicPanel;
        private Title title;
        private System.Windows.Forms.TableLayoutPanel panelToolBar;
        private AEC.UI.ConnectButton btnConnect;
        private AEC.UI.SystemParamControlPanel systemControlPanel;
        private RTASwitch rtaSwitch;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiCommSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmiRTAOptions;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenLogFileLocation;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.ToolStripMenuItem tsmiLanguage;
        private System.Windows.Forms.ToolStripMenuItem tsmizh_CN;
        private System.Windows.Forms.ToolStripMenuItem tsmien_US;
        private System.Windows.Forms.ToolStripMenuItem tsmiPreferences;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeviceModels;
        private System.Windows.Forms.ToolStripMenuItem tsmiOnlineHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiFirmwareUpdate;
        private System.Windows.Forms.ToolStripMenuItem tsmiActivateDevice;
        private System.Windows.Forms.ToolStripMenuItem tsmiBluetoothRename;
    }
}