namespace AEC.UI.VX
{
    partial class VXMonitorSingleChOutputPanel
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
            this.lblDelay = new AEC.UI.gtLabel();
            this.paramDelay = new AEC.UI.ParamLabel();
            this.chkMute = new AEC.UI.gtCheckBox();
            this.panelLimiter = new AEC.UI.gtPanel();
            this.panelCL = new AEC.UI.VX.VXCLPanel();
            this.panelDelay.SuspendLayout();
            this.panelLimiter.SuspendLayout();
            this.SuspendLayout();
            // 
            // ratioIndicator
            // 
            this.ratioIndicator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.ratioIndicator.Location = new System.Drawing.Point(185, 0);
            this.ratioIndicator.max = 10;
            this.ratioIndicator.min = 0;
            this.ratioIndicator.Name = "ratioIndicator";
            this.ratioIndicator.rightTick = global::AEC.Properties.Resources.limiter_ratio_tick;
            this.ratioIndicator.rightTickBackLocation = new System.Drawing.Point(22, 0);
            this.ratioIndicator.rightTickVisible = true;
            this.ratioIndicator.Size = new System.Drawing.Size(47, 88);
            this.ratioIndicator.TabIndex = 34;
            this.ratioIndicator.Text = "levelIndicator";
            // 
            // levelIndicator
            // 
            this.levelIndicator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.levelIndicator.Location = new System.Drawing.Point(185, 136);
            this.levelIndicator.max = 4;
            this.levelIndicator.min = -36;
            this.levelIndicator.Name = "levelIndicator";
            this.levelIndicator.rightTick = global::AEC.Properties.Resources.limiter_level_tick;
            this.levelIndicator.rightTickBackLocation = new System.Drawing.Point(22, 0);
            this.levelIndicator.rightTickVisible = true;
            this.levelIndicator.Size = new System.Drawing.Size(47, 330);
            this.levelIndicator.TabIndex = 33;
            this.levelIndicator.Text = "levelIndicator";
            // 
            // panelDelay
            // 
            this.panelDelay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDelay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.panelDelay.Controls.Add(this.lblDelay);
            this.panelDelay.Controls.Add(this.paramDelay);
            this.panelDelay.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.panelDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.panelDelay.Location = new System.Drawing.Point(6, 398);
            this.panelDelay.Name = "panelDelay";
            this.panelDelay.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelDelay.Radius = 18;
            this.panelDelay.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.panelDelay.ShowRect = false;
            this.panelDelay.Size = new System.Drawing.Size(173, 64);
            this.panelDelay.TabIndex = 32;
            this.panelDelay.title = null;
            this.panelDelay.titleHeight = 20;
            // 
            // lblDelay
            // 
            this.lblDelay.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblDelay.Location = new System.Drawing.Point(42, 4);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(90, 23);
            this.lblDelay.TabIndex = 1;
            this.lblDelay.Text = "延时";
            // 
            // paramDelay
            // 
            this.paramDelay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.paramDelay.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramDelay.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.paramDelay.initV = ((short)(0));
            this.paramDelay.isShowUpdownButton = true;
            this.paramDelay.Location = new System.Drawing.Point(42, 30);
            this.paramDelay.Margin = new System.Windows.Forms.Padding(0);
            this.paramDelay.Name = "paramDelay";
            this.paramDelay.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramDelay.Padding = new System.Windows.Forms.Padding(2);
            this.paramDelay.Size = new System.Drawing.Size(90, 25);
            this.paramDelay.TabIndex = 7;
            this.paramDelay.upperLimit = ((short)(0));
            this.paramDelay.userGDIPaint = true;
            this.paramDelay.v = ((short)(0));
            // 
            // chkMute
            // 
            this.chkMute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMute.BackColor = System.Drawing.Color.Transparent;
            this.chkMute.backDisabled = global::AEC.Properties.Resources.btn_mute_disabled;
            this.chkMute.backHover = global::AEC.Properties.Resources.btn_mute_hover;
            this.chkMute.backNormal = global::AEC.Properties.Resources.btn_mute_normal;
            this.chkMute.backPressed = global::AEC.Properties.Resources.btn_mute_pressed;
            this.chkMute.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.chkMute.isAutoCheck = true;
            this.chkMute.isChecked = false;
            this.chkMute.isShowCheckedBackWhenDsiabled = true;
            this.chkMute.isStretch = false;
            this.chkMute.Location = new System.Drawing.Point(44, 470);
            this.chkMute.Margin = new System.Windows.Forms.Padding(0);
            this.chkMute.Name = "chkMute";
            this.chkMute.Size = new System.Drawing.Size(97, 30);
            this.chkMute.TabIndex = 31;
            this.chkMute.Text = "静音";
            this.chkMute.userGDIPaint = true;
            // 
            // panelLimiter
            // 
            this.panelLimiter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLimiter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.panelLimiter.Controls.Add(this.panelCL);
            this.panelLimiter.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.panelLimiter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.panelLimiter.Location = new System.Drawing.Point(6, 0);
            this.panelLimiter.Name = "panelLimiter";
            this.panelLimiter.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelLimiter.Radius = 18;
            this.panelLimiter.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.panelLimiter.ShowRect = false;
            this.panelLimiter.Size = new System.Drawing.Size(173, 390);
            this.panelLimiter.TabIndex = 0;
            this.panelLimiter.title = null;
            this.panelLimiter.titleHeight = 20;
            // 
            // panelCL
            // 
            this.panelCL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.panelCL.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelCL.Location = new System.Drawing.Point(32, 0);
            this.panelCL.Name = "panelCL";
            this.panelCL.Size = new System.Drawing.Size(128, 390);
            this.panelCL.TabIndex = 0;
            // 
            // VXMonitorSingleChOutputPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.ratioIndicator);
            this.Controls.Add(this.levelIndicator);
            this.Controls.Add(this.panelDelay);
            this.Controls.Add(this.chkMute);
            this.Controls.Add(this.panelLimiter);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "VXMonitorSingleChOutputPanel";
            this.Size = new System.Drawing.Size(238, 506);
            this.panelDelay.ResumeLayout(false);
            this.panelLimiter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AEC.UI.gtPanel panelLimiter;
        private VXCLPanel panelCL;
        private gtLabel lblDelay;
        private ParamLabel paramDelay;
        private gtCheckBox chkMute;
        private gtPanel panelDelay;
        private LevelIndicator levelIndicator;
        private LevelIndicator ratioIndicator;
    }
}
