namespace AEC.UI.KX
{
    partial class KXMonitorMonoOutputControlPanel
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
            this.lblDelay = new AEC.UI.gtLabel();
            this.paramDelay = new AEC.UI.ParamLabel();
            this.chkMute = new AEC.UI.gtCheckBox();
            this.chkPhase = new AEC.UI.gtCheckBox();
            this.panelDelay.SuspendLayout();
            this.SuspendLayout();
            // 
            // eqThumbnail
            // 
            this.eqThumbnail.Location = new System.Drawing.Point(37, 48);
            // 
            // panelCL
            // 
            this.panelCL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelCL.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.panelCL.BorderWidth = 1F;
            this.panelCL.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelCL.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelCL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelCL.Location = new System.Drawing.Point(6, 100);
            this.panelCL.Name = "panelCL";
            this.panelCL.Radius = 16;
            this.panelCL.ShowBorder = false;
            this.panelCL.ShowText = true;
            this.panelCL.Size = new System.Drawing.Size(96, 386);
            this.panelCL.TabIndex = 0;
            this.panelCL.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.panelCL.TextPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            // 
            // panelDelay
            // 
            this.panelDelay.Controls.Add(this.lblDelay);
            this.panelDelay.Controls.Add(this.paramDelay);
            this.panelDelay.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelDelay.ForeColor = System.Drawing.Color.LightGray;
            this.panelDelay.Location = new System.Drawing.Point(6, 492);
            this.panelDelay.Name = "panelDelay";
            this.panelDelay.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelDelay.Radius = 18;
            this.panelDelay.RectColor = System.Drawing.Color.Black;
            this.panelDelay.ShowRect = false;
            this.panelDelay.Size = new System.Drawing.Size(96, 64);
            this.panelDelay.TabIndex = 32;
            this.panelDelay.title = null;
            this.panelDelay.titleHeight = 20;
            this.panelDelay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelDelay_MouseUp);
            // 
            // lblDelay
            // 
            this.lblDelay.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblDelay.Location = new System.Drawing.Point(4, 4);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(88, 23);
            this.lblDelay.TabIndex = 1;
            this.lblDelay.Text = "延时";
            this.lblDelay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelDelay_MouseUp);
            // 
            // paramDelay
            // 
            this.paramDelay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.paramDelay.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramDelay.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramDelay.ForeColor = System.Drawing.Color.White;
            this.paramDelay.initV = ((short)(0));
            this.paramDelay.isShowUpdownButton = true;
            this.paramDelay.Location = new System.Drawing.Point(4, 30);
            this.paramDelay.Margin = new System.Windows.Forms.Padding(0);
            this.paramDelay.Name = "paramDelay";
            this.paramDelay.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramDelay.Padding = new System.Windows.Forms.Padding(2);
            this.paramDelay.Size = new System.Drawing.Size(88, 25);
            this.paramDelay.TabIndex = 7;
            this.paramDelay.upperLimit = ((short)(0));
            this.paramDelay.userGDIPaint = false;
            this.paramDelay.v = ((short)(0));
            this.paramDelay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelDelay_MouseUp);
            // 
            // chkMute
            // 
            this.chkMute.BackColor = System.Drawing.Color.Transparent;
            this.chkMute.backDisabled = global::AEC.Properties.Resources.btn_mute_disabled;
            this.chkMute.backHover = global::AEC.Properties.Resources.btn_mute_hover;
            this.chkMute.backNormal = global::AEC.Properties.Resources.btn_mute_normal;
            this.chkMute.backPressed = global::AEC.Properties.Resources.btn_mute_pressed;
            this.chkMute.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkMute.isAutoCheck = true;
            this.chkMute.isChecked = false;
            this.chkMute.isShowCheckedBackWhenDsiabled = true;
            this.chkMute.isStretch = true;
            this.chkMute.Location = new System.Drawing.Point(6, 592);
            this.chkMute.Margin = new System.Windows.Forms.Padding(0);
            this.chkMute.Name = "chkMute";
            this.chkMute.Size = new System.Drawing.Size(96, 30);
            this.chkMute.TabIndex = 31;
            this.chkMute.Text = "静音";
            this.chkMute.userGDIPaint = true;
            // 
            // chkPhase
            // 
            this.chkPhase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkPhase.BackColor = System.Drawing.Color.Transparent;
            this.chkPhase.backDisabled = global::AEC.Properties.Resources.btn_mute_disabled;
            this.chkPhase.backHover = global::AEC.Properties.Resources.btn_mute_hover;
            this.chkPhase.backNormal = global::AEC.Properties.Resources.btn_mute_normal;
            this.chkPhase.backPressed = global::AEC.Properties.Resources.auto_btn_pressed;
            this.chkPhase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkPhase.isAutoCheck = true;
            this.chkPhase.isChecked = false;
            this.chkPhase.isShowCheckedBackWhenDsiabled = true;
            this.chkPhase.isStretch = true;
            this.chkPhase.Location = new System.Drawing.Point(6, 560);
            this.chkPhase.Margin = new System.Windows.Forms.Padding(0);
            this.chkPhase.Name = "chkPhase";
            this.chkPhase.Size = new System.Drawing.Size(96, 30);
            this.chkPhase.TabIndex = 33;
            this.chkPhase.Text = "相位 +";
            this.chkPhase.userGDIPaint = true;
            // 
            // KXMonitorMonoOutputControlPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.chkPhase);
            this.Controls.Add(this.panelCL);
            this.Controls.Add(this.panelDelay);
            this.Controls.Add(this.chkMute);
            this.Name = "KXMonitorMonoOutputControlPanel";
            this.Size = new System.Drawing.Size(170, 636);
            this.Controls.SetChildIndex(this.chkMute, 0);
            this.Controls.SetChildIndex(this.panelDelay, 0);
            this.Controls.SetChildIndex(this.panelCL, 0);
            this.Controls.SetChildIndex(this.eqThumbnail, 0);
            this.Controls.SetChildIndex(this.chkPhase, 0);
            this.panelDelay.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private KXCLPanel panelCL;
        private gtLabel lblDelay;
        private ParamLabel paramDelay;
        private gtCheckBox chkMute;
        private gtPanel panelDelay;
        private gtCheckBox chkPhase;
    }
}
