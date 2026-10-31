namespace AEC.UI.KX
{
    partial class KXMonitorStereoOutputControlPanel
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
            this.panelCL = new AEC.UI.KX.KXCLPanel();
            this.panelDelay = new AEC.UI.gtPanel();
            this.lblLeftDelay = new AEC.UI.gtLabel();
            this.lblRightDelay = new AEC.UI.gtLabel();
            this.paramLeftChDelay = new AEC.UI.ParamLabel();
            this.paramRightChDelay = new AEC.UI.ParamLabel();
            this.chkRightPhase = new AEC.UI.gtCheckBox();
            this.chkLeftPhase = new AEC.UI.gtCheckBox();
            this.chkRightMute = new AEC.UI.gtCheckBox();
            this.chkLeftMute = new AEC.UI.gtCheckBox();
            this.panelDelay.SuspendLayout();
            this.SuspendLayout();
            // 
            // eqThumbnail
            // 
            this.eqThumbnail.Location = new System.Drawing.Point(82, 48);
            // 
            // panelCL
            // 
            this.panelCL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelCL.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.panelCL.BorderWidth = 1F;
            this.panelCL.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelCL.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelCL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelCL.Location = new System.Drawing.Point(68, 100);
            this.panelCL.Name = "panelCL";
            this.panelCL.Radius = 16;
            this.panelCL.ShowBorder = false;
            this.panelCL.ShowText = true;
            this.panelCL.Size = new System.Drawing.Size(126, 386);
            this.panelCL.TabIndex = 0;
            this.panelCL.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.panelCL.TextPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            // 
            // panelDelay
            // 
            this.panelDelay.Controls.Add(this.lblLeftDelay);
            this.panelDelay.Controls.Add(this.lblRightDelay);
            this.panelDelay.Controls.Add(this.paramLeftChDelay);
            this.panelDelay.Controls.Add(this.paramRightChDelay);
            this.panelDelay.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelDelay.ForeColor = System.Drawing.Color.LightGray;
            this.panelDelay.Location = new System.Drawing.Point(6, 492);
            this.panelDelay.Name = "panelDelay";
            this.panelDelay.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelDelay.Radius = 18;
            this.panelDelay.RectColor = System.Drawing.Color.Black;
            this.panelDelay.ShowRect = false;
            this.panelDelay.Size = new System.Drawing.Size(188, 64);
            this.panelDelay.TabIndex = 30;
            this.panelDelay.title = null;
            this.panelDelay.titleHeight = 20;
            this.panelDelay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelDelay_MouseUp);
            // 
            // lblLeftDelay
            // 
            this.lblLeftDelay.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblLeftDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblLeftDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblLeftDelay.Location = new System.Drawing.Point(4, 4);
            this.lblLeftDelay.Name = "lblLeftDelay";
            this.lblLeftDelay.Size = new System.Drawing.Size(88, 23);
            this.lblLeftDelay.TabIndex = 1;
            this.lblLeftDelay.Text = "左通道延时";
            this.lblLeftDelay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelDelay_MouseUp);
            // 
            // lblRightDelay
            // 
            this.lblRightDelay.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblRightDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblRightDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblRightDelay.Location = new System.Drawing.Point(96, 4);
            this.lblRightDelay.Name = "lblRightDelay";
            this.lblRightDelay.Size = new System.Drawing.Size(88, 23);
            this.lblRightDelay.TabIndex = 1;
            this.lblRightDelay.Text = "右通道延时";
            this.lblRightDelay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelDelay_MouseUp);
            // 
            // paramLeftChDelay
            // 
            this.paramLeftChDelay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.paramLeftChDelay.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramLeftChDelay.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramLeftChDelay.ForeColor = System.Drawing.Color.White;
            this.paramLeftChDelay.initV = ((short)(0));
            this.paramLeftChDelay.isShowUpdownButton = true;
            this.paramLeftChDelay.Location = new System.Drawing.Point(4, 30);
            this.paramLeftChDelay.Margin = new System.Windows.Forms.Padding(0);
            this.paramLeftChDelay.Name = "paramLeftChDelay";
            this.paramLeftChDelay.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramLeftChDelay.Padding = new System.Windows.Forms.Padding(2);
            this.paramLeftChDelay.Size = new System.Drawing.Size(88, 25);
            this.paramLeftChDelay.TabIndex = 7;
            this.paramLeftChDelay.upperLimit = ((short)(0));
            this.paramLeftChDelay.userGDIPaint = false;
            this.paramLeftChDelay.v = ((short)(0));
            this.paramLeftChDelay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelDelay_MouseUp);
            // 
            // paramRightChDelay
            // 
            this.paramRightChDelay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.paramRightChDelay.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramRightChDelay.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramRightChDelay.ForeColor = System.Drawing.Color.White;
            this.paramRightChDelay.initV = ((short)(0));
            this.paramRightChDelay.isShowUpdownButton = true;
            this.paramRightChDelay.Location = new System.Drawing.Point(96, 30);
            this.paramRightChDelay.Margin = new System.Windows.Forms.Padding(0);
            this.paramRightChDelay.Name = "paramRightChDelay";
            this.paramRightChDelay.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramRightChDelay.Padding = new System.Windows.Forms.Padding(2);
            this.paramRightChDelay.Size = new System.Drawing.Size(88, 25);
            this.paramRightChDelay.TabIndex = 7;
            this.paramRightChDelay.upperLimit = ((short)(0));
            this.paramRightChDelay.userGDIPaint = false;
            this.paramRightChDelay.v = ((short)(0));
            this.paramRightChDelay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelDelay_MouseUp);
            // 
            // chkRightPhase
            // 
            this.chkRightPhase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRightPhase.BackColor = System.Drawing.Color.Transparent;
            this.chkRightPhase.backDisabled = global::AEC.Properties.Resources.btn_mute_disabled;
            this.chkRightPhase.backHover = global::AEC.Properties.Resources.btn_mute_hover;
            this.chkRightPhase.backNormal = global::AEC.Properties.Resources.btn_mute_normal;
            this.chkRightPhase.backPressed = global::AEC.Properties.Resources.auto_btn_pressed;
            this.chkRightPhase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkRightPhase.isAutoCheck = true;
            this.chkRightPhase.isChecked = false;
            this.chkRightPhase.isShowCheckedBackWhenDsiabled = true;
            this.chkRightPhase.isStretch = true;
            this.chkRightPhase.Location = new System.Drawing.Point(102, 560);
            this.chkRightPhase.Margin = new System.Windows.Forms.Padding(0);
            this.chkRightPhase.Name = "chkRightPhase";
            this.chkRightPhase.Size = new System.Drawing.Size(92, 30);
            this.chkRightPhase.TabIndex = 31;
            this.chkRightPhase.Text = "右通道相位 +";
            this.chkRightPhase.userGDIPaint = true;
            // 
            // chkLeftPhase
            // 
            this.chkLeftPhase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLeftPhase.BackColor = System.Drawing.Color.Transparent;
            this.chkLeftPhase.backDisabled = global::AEC.Properties.Resources.btn_mute_disabled;
            this.chkLeftPhase.backHover = global::AEC.Properties.Resources.btn_mute_hover;
            this.chkLeftPhase.backNormal = global::AEC.Properties.Resources.btn_mute_normal;
            this.chkLeftPhase.backPressed = global::AEC.Properties.Resources.auto_btn_pressed;
            this.chkLeftPhase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkLeftPhase.isAutoCheck = true;
            this.chkLeftPhase.isChecked = false;
            this.chkLeftPhase.isShowCheckedBackWhenDsiabled = true;
            this.chkLeftPhase.isStretch = true;
            this.chkLeftPhase.Location = new System.Drawing.Point(6, 560);
            this.chkLeftPhase.Margin = new System.Windows.Forms.Padding(0);
            this.chkLeftPhase.Name = "chkLeftPhase";
            this.chkLeftPhase.Size = new System.Drawing.Size(92, 30);
            this.chkLeftPhase.TabIndex = 30;
            this.chkLeftPhase.Text = "左通道相位 +";
            this.chkLeftPhase.userGDIPaint = true;
            // 
            // chkRightMute
            // 
            this.chkRightMute.BackColor = System.Drawing.Color.Transparent;
            this.chkRightMute.backDisabled = global::AEC.Properties.Resources.btn_mute_disabled;
            this.chkRightMute.backHover = global::AEC.Properties.Resources.btn_mute_hover;
            this.chkRightMute.backNormal = global::AEC.Properties.Resources.btn_mute_normal;
            this.chkRightMute.backPressed = global::AEC.Properties.Resources.btn_mute_pressed;
            this.chkRightMute.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkRightMute.isAutoCheck = true;
            this.chkRightMute.isChecked = false;
            this.chkRightMute.isShowCheckedBackWhenDsiabled = true;
            this.chkRightMute.isStretch = true;
            this.chkRightMute.Location = new System.Drawing.Point(102, 592);
            this.chkRightMute.Margin = new System.Windows.Forms.Padding(0);
            this.chkRightMute.Name = "chkRightMute";
            this.chkRightMute.Size = new System.Drawing.Size(92, 30);
            this.chkRightMute.TabIndex = 28;
            this.chkRightMute.Text = "右通道静音";
            this.chkRightMute.userGDIPaint = true;
            // 
            // chkLeftMute
            // 
            this.chkLeftMute.BackColor = System.Drawing.Color.Transparent;
            this.chkLeftMute.backDisabled = global::AEC.Properties.Resources.btn_mute_disabled;
            this.chkLeftMute.backHover = global::AEC.Properties.Resources.btn_mute_hover;
            this.chkLeftMute.backNormal = global::AEC.Properties.Resources.btn_mute_normal;
            this.chkLeftMute.backPressed = global::AEC.Properties.Resources.btn_mute_pressed;
            this.chkLeftMute.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkLeftMute.isAutoCheck = true;
            this.chkLeftMute.isChecked = false;
            this.chkLeftMute.isShowCheckedBackWhenDsiabled = true;
            this.chkLeftMute.isStretch = true;
            this.chkLeftMute.Location = new System.Drawing.Point(6, 592);
            this.chkLeftMute.Margin = new System.Windows.Forms.Padding(0);
            this.chkLeftMute.Name = "chkLeftMute";
            this.chkLeftMute.Size = new System.Drawing.Size(92, 30);
            this.chkLeftMute.TabIndex = 29;
            this.chkLeftMute.Text = "左通道静音";
            this.chkLeftMute.userGDIPaint = true;
            // 
            // KXMonitorStereoOutputControlPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.chkRightPhase);
            this.Controls.Add(this.panelCL);
            this.Controls.Add(this.chkLeftPhase);
            this.Controls.Add(this.panelDelay);
            this.Controls.Add(this.chkRightMute);
            this.Controls.Add(this.chkLeftMute);
            this.Name = "KXMonitorStereoOutputControlPanel";
            this.ShowBorder = false;
            this.Size = new System.Drawing.Size(260, 636);
            this.Controls.SetChildIndex(this.eqThumbnail, 0);
            this.Controls.SetChildIndex(this.chkLeftMute, 0);
            this.Controls.SetChildIndex(this.chkRightMute, 0);
            this.Controls.SetChildIndex(this.panelDelay, 0);
            this.Controls.SetChildIndex(this.chkLeftPhase, 0);
            this.Controls.SetChildIndex(this.panelCL, 0);
            this.Controls.SetChildIndex(this.chkRightPhase, 0);
            this.panelDelay.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KXCLPanel panelCL;
        private gtLabel lblLeftDelay;
        private gtLabel lblRightDelay;
        private ParamLabel paramLeftChDelay;
        private ParamLabel paramRightChDelay;
        private gtCheckBox chkRightMute;
        private gtCheckBox chkLeftMute;
        private gtPanel panelDelay;
        private gtCheckBox chkLeftPhase;
        private gtCheckBox chkRightPhase;
    }
}
