namespace AEC.UI.Forms
{
    partial class FormExportDspFirmwarePackage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExportDspFirmwarePackage));
            this.txtBinFilePath = new System.Windows.Forms.TextBox();
            this.mLabel1 = new AEC.UI.Controls.MLabel();
            this.panelDspFirmwareInfo = new AEC.UI.PanelDspFirmwareInfo();
            this.btnExport = new AEC.UI.Controls.MButton();
            this.SuspendLayout();
            // 
            // txtBinFilePath
            // 
            this.txtBinFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBinFilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtBinFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBinFilePath.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtBinFilePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtBinFilePath.Location = new System.Drawing.Point(136, 43);
            this.txtBinFilePath.Name = "txtBinFilePath";
            this.txtBinFilePath.ReadOnly = true;
            this.txtBinFilePath.Size = new System.Drawing.Size(506, 27);
            this.txtBinFilePath.TabIndex = 4;
            this.txtBinFilePath.TextChanged += new System.EventHandler(this.txtBinFilePath_TextChanged);
            this.txtBinFilePath.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtBinFilePath_MouseDoubleClick);
            // 
            // mLabel1
            // 
            this.mLabel1.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.mLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mLabel1.BackColor = System.Drawing.Color.Transparent;
            this.mLabel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.mLabel1.BorderWidth = 1F;
            this.mLabel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.mLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mLabel1.Location = new System.Drawing.Point(16, 45);
            this.mLabel1.Name = "mLabel1";
            this.mLabel1.Radius = 16;
            this.mLabel1.ShowBackground = false;
            this.mLabel1.ShowBorder = false;
            this.mLabel1.Size = new System.Drawing.Size(114, 23);
            this.mLabel1.TabIndex = 5;
            this.mLabel1.Text = "DSP bin 镜像文件";
            // 
            // panelDspFirmwareInfo
            // 
            this.panelDspFirmwareInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panelDspFirmwareInfo.BackColor = System.Drawing.Color.Transparent;
            this.panelDspFirmwareInfo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.panelDspFirmwareInfo.BorderWidth = 1F;
            this.panelDspFirmwareInfo.dspFirmwareInfo = null;
            this.panelDspFirmwareInfo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.panelDspFirmwareInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelDspFirmwareInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelDspFirmwareInfo.isEditable = true;
            this.panelDspFirmwareInfo.Location = new System.Drawing.Point(16, 82);
            this.panelDspFirmwareInfo.Name = "panelDspFirmwareInfo";
            this.panelDspFirmwareInfo.Radius = 16;
            this.panelDspFirmwareInfo.ShowBorder = false;
            this.panelDspFirmwareInfo.ShowText = true;
            this.panelDspFirmwareInfo.Size = new System.Drawing.Size(736, 248);
            this.panelDspFirmwareInfo.TabIndex = 6;
            this.panelDspFirmwareInfo.Text = "DSP固件信息";
            this.panelDspFirmwareInfo.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.panelDspFirmwareInfo.TextPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnExport.BorderWidth = 1F;
            this.btnExport.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnExport.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnExport.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnExport.Enabled = false;
            this.btnExport.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnExport.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnExport.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnExport.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnExport.Location = new System.Drawing.Point(656, 43);
            this.btnExport.Name = "btnExport";
            this.btnExport.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnExport.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnExport.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExport.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnExport.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnExport.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnExport.Radius = 12;
            this.btnExport.ShowBorder = true;
            this.btnExport.ShowText = true;
            this.btnExport.Size = new System.Drawing.Size(96, 27);
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "导出";
            this.btnExport.Click += new System.EventHandler(this.btn_Click);
            // 
            // FormExportDspFirmwarePackage
            // 
            this.AllowDrop = true;
            this.ClientSize = new System.Drawing.Size(768, 344);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.panelDspFirmwareInfo);
            this.Controls.Add(this.mLabel1);
            this.Controls.Add(this.txtBinFilePath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormExportDspFirmwarePackage";
            this.Text = "M-DSP固件导出";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDspFirmware_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDspFirmware_DragEnter);
            this.Controls.SetChildIndex(this.txtBinFilePath, 0);
            this.Controls.SetChildIndex(this.mLabel1, 0);
            this.Controls.SetChildIndex(this.panelDspFirmwareInfo, 0);
            this.Controls.SetChildIndex(this.btnExport, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBinFilePath;
        private Controls.MLabel mLabel1;
        private PanelDspFirmwareInfo panelDspFirmwareInfo;
        private Controls.MButton btnExport;
    }
}
