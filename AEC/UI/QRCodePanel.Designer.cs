namespace AEC.UI
{
    partial class QRCodePanel
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.lblScanQRCode = new AEC.UI.gtLabel();
            this.btnClose = new AEC.UI.gtButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackgroundImage = global::AEC.Properties.Resources.QRCodeVideoTutorial;
            this.pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox.Location = new System.Drawing.Point(45, 51);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(256, 256);
            this.pictureBox.TabIndex = 32;
            this.pictureBox.TabStop = false;
            // 
            // lblScanQRCode
            // 
            this.lblScanQRCode.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblScanQRCode.BackColor = System.Drawing.Color.Transparent;
            this.lblScanQRCode.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblScanQRCode.ForeColor = System.Drawing.Color.White;
            this.lblScanQRCode.Location = new System.Drawing.Point(45, 310);
            this.lblScanQRCode.Name = "lblScanQRCode";
            this.lblScanQRCode.Size = new System.Drawing.Size(256, 26);
            this.lblScanQRCode.TabIndex = 33;
            this.lblScanQRCode.Text = "扫一扫打开在线视频教程";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.backDisabled = global::AEC.Properties.Resources.btn_close_disabled;
            this.btnClose.backHover = global::AEC.Properties.Resources.btn_close_hover;
            this.btnClose.backNormal = global::AEC.Properties.Resources.btn_close_normal;
            this.btnClose.backPressed = global::AEC.Properties.Resources.btn_close_pressed;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.isStretch = false;
            this.btnClose.Location = new System.Drawing.Point(16, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 24);
            this.btnClose.TabIndex = 34;
            this.btnClose.userGDIPaint = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // QRCodePanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DimGray;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblScanQRCode);
            this.Controls.Add(this.pictureBox);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "QRCodePanel";
            this.Size = new System.Drawing.Size(346, 365);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private gtLabel lblScanQRCode;
        private System.Windows.Forms.PictureBox pictureBox;
        private gtButton btnClose;
    }
}
