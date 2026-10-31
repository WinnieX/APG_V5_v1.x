namespace AEC.UI.VX
{
    partial class VXMonitorDualChOutputPanel
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
            this.ratioIndicator = new AEC.UI.LevelIndicator();
            this.levelIndicator = new AEC.UI.LevelIndicator();
            this.panelDelay = new AEC.UI.gtPanel();
            this.lblLeftDelay = new AEC.UI.gtLabel();
            this.lblRightDelay = new AEC.UI.gtLabel();
            this.paramLeftChDelay = new AEC.UI.ParamLabel();
            this.paramRightChDelay = new AEC.UI.ParamLabel();
            this.chkRightMute = new AEC.UI.gtCheckBox();
            this.chkLeftMute = new AEC.UI.gtCheckBox();
            this.panelLimiter = new AEC.UI.gtPanel();
            this.panelCL = new AEC.UI.VX.VXCLPanel();
            this.panelDelay.SuspendLayout();
            this.panelLimiter.SuspendLayout();
            this.SuspendLayout();
            // 
            // ratioIndicator
            // 
            this.ratioIndicator.direction = AEC.UI.LevelIndicator.Direction.Downward;
            this.ratioIndicator.isOn = false;
            this.ratioIndicator.ledBackLocation = new System.Drawing.Point(0, 4);
            this.ratioIndicator.ledLightHeight = 8;
            this.ratioIndicator.ledOff = global::AEC.Properties.Resources.limiter_ratio_light_off;
            this.ratioIndicator.ledOn = global::AEC.Properties.Resources.limiter_ratio_light_on;
            this.ratioIndicator.leftTick = null;
            this.ratioIndicator.leftTickBackLocation = new System.Drawing.Point(0, 0);
            this.ratioIndicator.leftTickVisible = false;
            this.ratioIndicator.level = 0;
            this.ratioIndicator.Location = new System.Drawing.Point(204, 0);
            this.ratioIndicator.max = 10;
            this.ratioIndicator.min = 0;
            this.ratioIndicator.Name = "ratioIndicator";
            this.ratioIndicator.rightTick = global::AEC.Properties.Resources.limiter_ratio_tick;
            this.ratioIndicator.rightTickBackLocation = new System.Drawing.Point(22, 0);
            this.ratioIndicator.rightTickVisible = true;
            this.ratioIndicator.Size = new System.Drawing.Size(47, 88);
            this.ratioIndicator.TabIndex = 31;
            this.ratioIndicator.Text = "levelIndicator";
            // 
            // levelIndicator
            // 
            this.levelIndicator.direction = AEC.UI.LevelIndicator.Direction.Upward;
            this.levelIndicator.isOn = false;
            this.levelIndicator.ledBackLocation = new System.Drawing.Point(0, 4);
            this.levelIndicator.ledLightHeight = 8;
            this.levelIndicator.ledOff = global::AEC.Properties.Resources.limiter_level_light_off;
            this.levelIndicator.ledOn = global::AEC.Properties.Resources.limiter_level_light_on;
            this.levelIndicator.leftTick = null;
            this.levelIndicator.leftTickBackLocation = new System.Drawing.Point(0, 0);
            this.levelIndicator.leftTickVisible = false;
            this.levelIndicator.level = -36;
            this.levelIndicator.Location = new System.Drawing.Point(204, 136);
            this.levelIndicator.max = 4;
            this.levelIndicator.min = -36;
            this.levelIndicator.Name = "levelIndicator";
            this.levelIndicator.rightTick = global::AEC.Properties.Resources.limiter_level_tick;
            this.levelIndicator.rightTickBackLocation = new System.Drawing.Point(22, 0);
            this.levelIndicator.rightTickVisible = true;
            this.levelIndicator.Size = new System.Drawing.Size(47, 327);
            this.levelIndicator.TabIndex = 31;
            this.levelIndicator.Text = "levelIndicator";
            // 
            // panelDelay
            // 
            this.panelDelay.Controls.Add(this.lblLeftDelay);
            this.panelDelay.Controls.Add(this.lblRightDelay);
            this.panelDelay.Controls.Add(this.paramLeftChDelay);
            this.panelDelay.Controls.Add(this.paramRightChDelay);
            this.panelDelay.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelDelay.ForeColor = System.Drawing.Color.LightGray;
            this.panelDelay.Location = new System.Drawing.Point(6, 398);
            this.panelDelay.Name = "panelDelay";
            this.panelDelay.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelDelay.Radius = 18;
            this.panelDelay.RectColor = System.Drawing.Color.Black;
            this.panelDelay.ShowRect = false;
            this.panelDelay.Size = new System.Drawing.Size(192, 64);
            this.panelDelay.TabIndex = 30;
            this.panelDelay.title = null;
            this.panelDelay.titleHeight = 20;
            // 
            // lblLeftDelay
            // 
            this.lblLeftDelay.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblLeftDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblLeftDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblLeftDelay.Location = new System.Drawing.Point(10, 4);
            this.lblLeftDelay.Name = "lblLeftDelay";
            this.lblLeftDelay.Size = new System.Drawing.Size(88, 23);
            this.lblLeftDelay.TabIndex = 1;
            this.lblLeftDelay.Text = "左通道延时";
            // 
            // lblRightDelay
            // 
            this.lblRightDelay.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblRightDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblRightDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblRightDelay.Location = new System.Drawing.Point(101, 4);
            this.lblRightDelay.Name = "lblRightDelay";
            this.lblRightDelay.Size = new System.Drawing.Size(88, 23);
            this.lblRightDelay.TabIndex = 1;
            this.lblRightDelay.Text = "右通道延时";
            // 
            // paramLeftChDelay
            // 
            this.paramLeftChDelay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.paramLeftChDelay.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramLeftChDelay.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramLeftChDelay.ForeColor = System.Drawing.Color.White;
            this.paramLeftChDelay.initV = ((short)(0));
            this.paramLeftChDelay.isShowUpdownButton = true;
            this.paramLeftChDelay.Location = new System.Drawing.Point(15, 30);
            this.paramLeftChDelay.Margin = new System.Windows.Forms.Padding(0);
            this.paramLeftChDelay.Name = "paramLeftChDelay";
            this.paramLeftChDelay.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramLeftChDelay.Padding = new System.Windows.Forms.Padding(2);
            this.paramLeftChDelay.Size = new System.Drawing.Size(78, 25);
            this.paramLeftChDelay.TabIndex = 7;
            this.paramLeftChDelay.upperLimit = ((short)(0));
            this.paramLeftChDelay.userGDIPaint = true;
            this.paramLeftChDelay.v = ((short)(0));
            // 
            // paramRightChDelay
            // 
            this.paramRightChDelay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.paramRightChDelay.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramRightChDelay.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramRightChDelay.ForeColor = System.Drawing.Color.White;
            this.paramRightChDelay.initV = ((short)(0));
            this.paramRightChDelay.isShowUpdownButton = true;
            this.paramRightChDelay.Location = new System.Drawing.Point(108, 30);
            this.paramRightChDelay.Margin = new System.Windows.Forms.Padding(0);
            this.paramRightChDelay.Name = "paramRightChDelay";
            this.paramRightChDelay.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramRightChDelay.Padding = new System.Windows.Forms.Padding(2);
            this.paramRightChDelay.Size = new System.Drawing.Size(78, 25);
            this.paramRightChDelay.TabIndex = 7;
            this.paramRightChDelay.upperLimit = ((short)(0));
            this.paramRightChDelay.userGDIPaint = true;
            this.paramRightChDelay.v = ((short)(0));
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
            this.chkRightMute.Location = new System.Drawing.Point(106, 470);
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
            this.chkLeftMute.Location = new System.Drawing.Point(6, 470);
            this.chkLeftMute.Margin = new System.Windows.Forms.Padding(0);
            this.chkLeftMute.Name = "chkLeftMute";
            this.chkLeftMute.Size = new System.Drawing.Size(92, 30);
            this.chkLeftMute.TabIndex = 29;
            this.chkLeftMute.Text = "左通道静音";
            this.chkLeftMute.userGDIPaint = true;
            // 
            // panelLimiter
            // 
            this.panelLimiter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelLimiter.Controls.Add(this.panelCL);
            this.panelLimiter.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelLimiter.ForeColor = System.Drawing.Color.LightGray;
            this.panelLimiter.Location = new System.Drawing.Point(6, 0);
            this.panelLimiter.Name = "panelLimiter";
            this.panelLimiter.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelLimiter.Radius = 18;
            this.panelLimiter.RectColor = System.Drawing.Color.Black;
            this.panelLimiter.ShowRect = false;
            this.panelLimiter.Size = new System.Drawing.Size(192, 390);
            this.panelLimiter.TabIndex = 0;
            this.panelLimiter.title = null;
            this.panelLimiter.titleHeight = 20;
            // 
            // panelCL
            // 
            this.panelCL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelCL.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelCL.Location = new System.Drawing.Point(42, 0);
            this.panelCL.Name = "panelCL";
            this.panelCL.Size = new System.Drawing.Size(128, 390);
            this.panelCL.TabIndex = 0;
            // 
            // MonitorDualChOutputPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.ratioIndicator);
            this.Controls.Add(this.levelIndicator);
            this.Controls.Add(this.panelDelay);
            this.Controls.Add(this.chkRightMute);
            this.Controls.Add(this.chkLeftMute);
            this.Controls.Add(this.panelLimiter);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "MonitorDualChOutputPanel";
            this.Size = new System.Drawing.Size(258, 506);
            this.panelDelay.ResumeLayout(false);
            this.panelLimiter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AEC.UI.gtPanel panelLimiter;
        private VXCLPanel panelCL;
        private gtLabel lblLeftDelay;
        private gtLabel lblRightDelay;
        private ParamLabel paramLeftChDelay;
        private ParamLabel paramRightChDelay;
        private gtCheckBox chkRightMute;
        private gtCheckBox chkLeftMute;
        private gtPanel panelDelay;
        private LevelIndicator levelIndicator;
        private LevelIndicator ratioIndicator;
    }
}
