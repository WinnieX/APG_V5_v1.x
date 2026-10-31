namespace AEC.UI.X5
{
    partial class X5MainOutputPanel
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
            this.channelPanel = new AEC.UI.gtPanel();
            this.btnSwitchUnit = new AEC.UI.gtButton();
            this.chkLeftMute = new AEC.UI.gtCheckBox();
            this.sliderLeftDelay = new AEC.UI.HSliderParam();
            this.lblLeftDelay = new AEC.UI.gtLabel();
            this.chkRightMute = new AEC.UI.gtCheckBox();
            this.lblRightDelay = new AEC.UI.gtLabel();
            this.sliderRightDelay = new AEC.UI.HSliderParam();
            this.limiterPanel = new AEC.UI.X5.X5CompAndLimiterPanel();
            this.mixerPanel = new AEC.UI.X5.X5MixerPanel();
            this.eqResetAndByPassPanel = new AEC.UI.EQResetAndByPassPanel();
            this.workModePanel = new AEC.UI.WorkModePanel();
            this.eqGroupPanel = new AEC.UI.EQGroupPanel();
            this.channelPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // channelPanel
            // 
            this.channelPanel.Controls.Add(this.btnSwitchUnit);
            this.channelPanel.Controls.Add(this.chkLeftMute);
            this.channelPanel.Controls.Add(this.sliderLeftDelay);
            this.channelPanel.Controls.Add(this.lblLeftDelay);
            this.channelPanel.Controls.Add(this.chkRightMute);
            this.channelPanel.Controls.Add(this.lblRightDelay);
            this.channelPanel.Controls.Add(this.sliderRightDelay);
            this.channelPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.channelPanel.ForeColor = System.Drawing.Color.LightGray;
            this.channelPanel.Location = new System.Drawing.Point(843, 8);
            this.channelPanel.Name = "channelPanel";
            this.channelPanel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.channelPanel.Radius = 18;
            this.channelPanel.RectColor = System.Drawing.Color.Black;
            this.channelPanel.ShowRect = true;
            this.channelPanel.Size = new System.Drawing.Size(284, 264);
            this.channelPanel.TabIndex = 34;
            this.channelPanel.title = null;
            this.channelPanel.titleHeight = 20;
            this.channelPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.channelPanel_MouseUp);
            // 
            // btnSwitchUnit
            // 
            this.btnSwitchUnit.BackColor = System.Drawing.Color.Transparent;
            this.btnSwitchUnit.backDisabled = global::AEC.Properties.Resources.switch_unit_disabled;
            this.btnSwitchUnit.backHover = global::AEC.Properties.Resources.switch_unit_hover;
            this.btnSwitchUnit.backNormal = global::AEC.Properties.Resources.switch_unit_normal;
            this.btnSwitchUnit.backPressed = global::AEC.Properties.Resources.switch_unit_pressed;
            this.btnSwitchUnit.ForeColor = System.Drawing.Color.White;
            this.btnSwitchUnit.isStretch = false;
            this.btnSwitchUnit.Location = new System.Drawing.Point(12, 72);
            this.btnSwitchUnit.Name = "btnSwitchUnit";
            this.btnSwitchUnit.Size = new System.Drawing.Size(18, 18);
            this.btnSwitchUnit.TabIndex = 34;
            this.btnSwitchUnit.userGDIPaint = false;
            this.btnSwitchUnit.Click += new System.EventHandler(this.btnSwitchUnit_Click);
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
            this.chkLeftMute.isStretch = false;
            this.chkLeftMute.Location = new System.Drawing.Point(41, 87);
            this.chkLeftMute.Margin = new System.Windows.Forms.Padding(0);
            this.chkLeftMute.Name = "chkLeftMute";
            this.chkLeftMute.Size = new System.Drawing.Size(96, 28);
            this.chkLeftMute.TabIndex = 33;
            this.chkLeftMute.Text = "左通道静音";
            this.chkLeftMute.userGDIPaint = true;
            // 
            // sliderLeftDelay
            // 
            this.sliderLeftDelay.BackColor = System.Drawing.Color.Transparent;
            this.sliderLeftDelay.backgroundDisabled = null;
            this.sliderLeftDelay.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.sliderLeftDelay.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderLeftDelay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderLeftDelay.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderLeftDelay.Location = new System.Drawing.Point(83, 12);
            this.sliderLeftDelay.Margin = new System.Windows.Forms.Padding(0);
            this.sliderLeftDelay.Name = "sliderLeftDelay";
            this.sliderLeftDelay.Size = new System.Drawing.Size(189, 23);
            this.sliderLeftDelay.SliderWidth = 104;
            this.sliderLeftDelay.TabIndex = 30;
            this.sliderLeftDelay.thumbCenterY = 11;
            this.sliderLeftDelay.ThumbEndX = 92;
            this.sliderLeftDelay.upperLimit = ((short)(100));
            this.sliderLeftDelay.value = ((short)(0));
            // 
            // lblLeftDelay
            // 
            this.lblLeftDelay.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblLeftDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblLeftDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblLeftDelay.Location = new System.Drawing.Point(6, 12);
            this.lblLeftDelay.Name = "lblLeftDelay";
            this.lblLeftDelay.Size = new System.Drawing.Size(76, 23);
            this.lblLeftDelay.TabIndex = 28;
            this.lblLeftDelay.Text = "左通道延时";
            this.lblLeftDelay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.channelPanel_MouseUp);
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
            this.chkRightMute.Location = new System.Drawing.Point(147, 87);
            this.chkRightMute.Margin = new System.Windows.Forms.Padding(0);
            this.chkRightMute.Name = "chkRightMute";
            this.chkRightMute.Size = new System.Drawing.Size(96, 28);
            this.chkRightMute.TabIndex = 32;
            this.chkRightMute.Text = "右通道静音";
            this.chkRightMute.userGDIPaint = true;
            // 
            // lblRightDelay
            // 
            this.lblRightDelay.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblRightDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblRightDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblRightDelay.Location = new System.Drawing.Point(6, 44);
            this.lblRightDelay.Name = "lblRightDelay";
            this.lblRightDelay.Size = new System.Drawing.Size(76, 23);
            this.lblRightDelay.TabIndex = 29;
            this.lblRightDelay.Text = "右通道延时";
            this.lblRightDelay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.channelPanel_MouseUp);
            // 
            // sliderRightDelay
            // 
            this.sliderRightDelay.BackColor = System.Drawing.Color.Transparent;
            this.sliderRightDelay.backgroundDisabled = null;
            this.sliderRightDelay.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.sliderRightDelay.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderRightDelay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderRightDelay.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderRightDelay.Location = new System.Drawing.Point(83, 44);
            this.sliderRightDelay.Margin = new System.Windows.Forms.Padding(0);
            this.sliderRightDelay.Name = "sliderRightDelay";
            this.sliderRightDelay.Size = new System.Drawing.Size(189, 23);
            this.sliderRightDelay.SliderWidth = 104;
            this.sliderRightDelay.TabIndex = 31;
            this.sliderRightDelay.thumbCenterY = 11;
            this.sliderRightDelay.ThumbEndX = 92;
            this.sliderRightDelay.upperLimit = ((short)(100));
            this.sliderRightDelay.value = ((short)(0));
            // 
            // limiterPanel
            // 
            this.limiterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.limiterPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.limiterPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.limiterPanel.ForeColor = System.Drawing.Color.LightGray;
            this.limiterPanel.Location = new System.Drawing.Point(500, 152);
            this.limiterPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.limiterPanel.Name = "limiterPanel";
            this.limiterPanel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.limiterPanel.Radius = 18;
            this.limiterPanel.RectColor = System.Drawing.Color.Gray;
            this.limiterPanel.Size = new System.Drawing.Size(331, 120);
            this.limiterPanel.sliderback = global::AEC.Properties.Resources.main_output_slider_back;
            this.limiterPanel.SliderParamWidth = 210;
            this.limiterPanel.sliderWidth = 110;
            this.limiterPanel.sliderX = 112;
            this.limiterPanel.TabIndex = 8;
            this.limiterPanel.ThumbEndX = 94;
            this.limiterPanel.title = null;
            this.limiterPanel.titleHeight = 20;
            // 
            // mixerPanel
            // 
            this.mixerPanel.BackColor = System.Drawing.Color.Transparent;
            this.mixerPanel.backgroundDisabled = global::AEC.Properties.Resources.output_slider_disabled;
            this.mixerPanel.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.mixerPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.mixerPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.mixerPanel.ForeColor = System.Drawing.Color.LightGray;
            this.mixerPanel.Location = new System.Drawing.Point(500, 8);
            this.mixerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.mixerPanel.Name = "mixerPanel";
            this.mixerPanel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.mixerPanel.Radius = 18;
            this.mixerPanel.RectColor = System.Drawing.Color.Gray;
            this.mixerPanel.Size = new System.Drawing.Size(331, 136);
            this.mixerPanel.TabIndex = 7;
            this.mixerPanel.title = null;
            this.mixerPanel.titleHeight = 20;
            // 
            // eqResetAndByPassPanel
            // 
            this.eqResetAndByPassPanel.BackColor = System.Drawing.Color.Transparent;
            this.eqResetAndByPassPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.eqResetAndByPassPanel.isPass = false;
            this.eqResetAndByPassPanel.Location = new System.Drawing.Point(10, 275);
            this.eqResetAndByPassPanel.Name = "eqResetAndByPassPanel";
            this.eqResetAndByPassPanel.Size = new System.Drawing.Size(168, 33);
            this.eqResetAndByPassPanel.TabIndex = 6;
            // 
            // workModePanel
            // 
            this.workModePanel.BackColor = System.Drawing.Color.Transparent;
            this.workModePanel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.workModePanel.ForeColor = System.Drawing.Color.LightGray;
            this.workModePanel.Location = new System.Drawing.Point(500, 275);
            this.workModePanel.Margin = new System.Windows.Forms.Padding(0);
            this.workModePanel.Name = "workModePanel";
            this.workModePanel.Size = new System.Drawing.Size(336, 32);
            this.workModePanel.TabIndex = 5;
            // 
            // eqGroupPanel
            // 
            this.eqGroupPanel.BackColor = System.Drawing.Color.Transparent;
            this.eqGroupPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.eqGroupPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.eqGroupPanel.Location = new System.Drawing.Point(10, 8);
            this.eqGroupPanel.Name = "eqGroupPanel";
            this.eqGroupPanel.Size = new System.Drawing.Size(478, 264);
            this.eqGroupPanel.TabIndex = 1;
            // 
            // X5MainOutputPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.channelPanel);
            this.Controls.Add(this.limiterPanel);
            this.Controls.Add(this.mixerPanel);
            this.Controls.Add(this.eqResetAndByPassPanel);
            this.Controls.Add(this.workModePanel);
            this.Controls.Add(this.eqGroupPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "X5MainOutputPanel";
            this.Size = new System.Drawing.Size(1276, 312);
            this.channelPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private EQGroupPanel eqGroupPanel;
        private WorkModePanel workModePanel;
        private EQResetAndByPassPanel eqResetAndByPassPanel;
        private X5MixerPanel mixerPanel;
        private X5CompAndLimiterPanel limiterPanel;
        protected HSliderParam sliderLeftDelay;
        private gtCheckBox chkRightMute;
        private gtLabel lblLeftDelay;
        private gtCheckBox chkLeftMute;
        private gtLabel lblRightDelay;
        protected HSliderParam sliderRightDelay;
        private gtPanel channelPanel;
        private gtButton btnSwitchUnit;
    }
}
