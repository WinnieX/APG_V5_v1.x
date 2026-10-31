namespace AEC.UI
{
    partial class FormModeOperation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormModeOperation));
            this.deviceModePanel = new AEC.UI.DeviceModePanel();
            this.volMgtPanel = new AEC.UI.VolMgtPanel();
            this.title = new AEC.UI.Title();
            this.SuspendLayout();
            // 
            // deviceModePanel
            // 
            this.deviceModePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.deviceModePanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.deviceModePanel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.deviceModePanel.ForeColor = System.Drawing.Color.LightGray;
            this.deviceModePanel.Location = new System.Drawing.Point(532, 49);
            this.deviceModePanel.Margin = new System.Windows.Forms.Padding(0);
            this.deviceModePanel.Name = "deviceModePanel";
            this.deviceModePanel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.deviceModePanel.Radius = 18;
            this.deviceModePanel.RectColor = System.Drawing.Color.Gray;
            this.deviceModePanel.Size = new System.Drawing.Size(476, 294);
            this.deviceModePanel.TabIndex = 4;
            this.deviceModePanel.title = "数据备份管理";
            this.deviceModePanel.titleHeight = 20;
            // 
            // volMgtPanel
            // 
            this.volMgtPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.volMgtPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.volMgtPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.volMgtPanel.ForeColor = System.Drawing.Color.LightGray;
            this.volMgtPanel.Location = new System.Drawing.Point(16, 49);
            this.volMgtPanel.Name = "volMgtPanel";
            this.volMgtPanel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.volMgtPanel.Radius = 18;
            this.volMgtPanel.RectColor = System.Drawing.Color.Gray;
            this.volMgtPanel.Size = new System.Drawing.Size(500, 294);
            this.volMgtPanel.TabIndex = 3;
            this.volMgtPanel.title = "音量控制";
            this.volMgtPanel.titleHeight = 20;
            // 
            // title
            // 
            this.title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.title.Font = new System.Drawing.Font("Microsoft YaHei UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.isAllowDoubleClickMaximize = false;
            this.title.isShowCloseButton = true;
            this.title.isShowSystemBtns = false;
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Margin = new System.Windows.Forms.Padding(0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(1024, 32);
            this.title.TabIndex = 2;
            // 
            // FormModeOperation
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1024, 360);
            this.Controls.Add(this.deviceModePanel);
            this.Controls.Add(this.volMgtPanel);
            this.Controls.Add(this.title);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormModeOperation";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备模式";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormModeOperation_FormClosed);
            this.Load += new System.EventHandler(this.FormModeOperation_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Title title;
        private VolMgtPanel volMgtPanel;
        private DeviceModePanel deviceModePanel;

    }
}