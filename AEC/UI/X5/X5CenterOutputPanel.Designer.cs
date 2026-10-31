namespace AEC.UI.X5
{
    partial class X5CenterOutputPanel
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
            this.lblHpfSlope = new AEC.UI.gtLabel();
            this.cmbHpfType = new AEC.UI.gtComboBox();
            this.sliderHpfFreq = new AEC.UI.HSliderParam();
            this.chkMute = new AEC.UI.gtCheckBox();
            this.lblHpf = new AEC.UI.gtLabel();
            this.lblDelay = new AEC.UI.gtLabel();
            this.sliderDelay = new AEC.UI.HSliderParam();
            this.limiterPanel = new AEC.UI.X5.X5CompAndLimiterPanel();
            this.mixerPanel = new AEC.UI.X5.X5MixerPanel();
            this.eqResetAndByPassPanel = new AEC.UI.EQResetAndByPassPanel();
            this.workModePanel = new AEC.UI.WorkModePanel();
            this.eqGroupPanel = new AEC.UI.EQGroupPanel();
            this.channelPanel = new AEC.UI.gtPanel();
            this.btnSwitchUnit = new AEC.UI.gtButton();
            this.channelPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHpfSlope
            // 
            this.lblHpfSlope.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblHpfSlope.BackColor = System.Drawing.Color.Transparent;
            this.lblHpfSlope.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblHpfSlope.Location = new System.Drawing.Point(6, 42);
            this.lblHpfSlope.Name = "lblHpfSlope";
            this.lblHpfSlope.Size = new System.Drawing.Size(108, 23);
            this.lblHpfSlope.TabIndex = 34;
            this.lblHpfSlope.Text = "滤波器斜率";
            // 
            // cmbHpfType
            // 
            this.cmbHpfType.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbHpfType.BackColor = System.Drawing.SystemColors.Window;
            this.cmbHpfType.DropDownBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(186)))), ((int)(((byte)(255)))));
            this.cmbHpfType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHpfType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbHpfType.ForeColor = System.Drawing.Color.Black;
            this.cmbHpfType.FormattingEnabled = true;
            this.cmbHpfType.Items.AddRange(new object[] {
            "12 dB",
            "24 dB"});
            this.cmbHpfType.Location = new System.Drawing.Point(122, 42);
            this.cmbHpfType.Margin = new System.Windows.Forms.Padding(0);
            this.cmbHpfType.Name = "cmbHpfType";
            this.cmbHpfType.Size = new System.Drawing.Size(148, 23);
            this.cmbHpfType.TabIndex = 35;
            this.cmbHpfType.TabStop = false;
            // 
            // sliderHpfFreq
            // 
            this.sliderHpfFreq.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderHpfFreq.BackColor = System.Drawing.Color.Transparent;
            this.sliderHpfFreq.backgroundDisabled = global::AEC.Properties.Resources.output_slider_disabled;
            this.sliderHpfFreq.backgroundNormal = global::AEC.Properties.Resources.aux1_output_slider_back;
            this.sliderHpfFreq.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderHpfFreq.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderHpfFreq.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderHpfFreq.Location = new System.Drawing.Point(83, 12);
            this.sliderHpfFreq.Margin = new System.Windows.Forms.Padding(0);
            this.sliderHpfFreq.Name = "sliderHpfFreq";
            this.sliderHpfFreq.Size = new System.Drawing.Size(189, 23);
            this.sliderHpfFreq.SliderWidth = 104;
            this.sliderHpfFreq.TabIndex = 30;
            this.sliderHpfFreq.thumbCenterY = 11;
            this.sliderHpfFreq.ThumbEndX = 92;
            this.sliderHpfFreq.upperLimit = ((short)(100));
            this.sliderHpfFreq.value = ((short)(0));
            // 
            // chkMute
            // 
            this.chkMute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.chkMute.Location = new System.Drawing.Point(100, 140);
            this.chkMute.Margin = new System.Windows.Forms.Padding(0);
            this.chkMute.Name = "chkMute";
            this.chkMute.Size = new System.Drawing.Size(84, 28);
            this.chkMute.TabIndex = 32;
            this.chkMute.Text = "静音";
            this.chkMute.userGDIPaint = true;
            // 
            // lblHpf
            // 
            this.lblHpf.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblHpf.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHpf.BackColor = System.Drawing.Color.Transparent;
            this.lblHpf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblHpf.Location = new System.Drawing.Point(6, 12);
            this.lblHpf.Name = "lblHpf";
            this.lblHpf.Size = new System.Drawing.Size(76, 23);
            this.lblHpf.TabIndex = 28;
            this.lblHpf.Text = "高通";
            // 
            // lblDelay
            // 
            this.lblDelay.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblDelay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblDelay.Location = new System.Drawing.Point(6, 102);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(76, 23);
            this.lblDelay.TabIndex = 29;
            this.lblDelay.Text = "延时";
            this.lblDelay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.channelPanel_MouseUp);
            // 
            // sliderDelay
            // 
            this.sliderDelay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderDelay.BackColor = System.Drawing.Color.Transparent;
            this.sliderDelay.backgroundDisabled = null;
            this.sliderDelay.backgroundNormal = global::AEC.Properties.Resources.aux1_output_slider_back;
            this.sliderDelay.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderDelay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderDelay.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderDelay.Location = new System.Drawing.Point(83, 102);
            this.sliderDelay.Margin = new System.Windows.Forms.Padding(0);
            this.sliderDelay.Name = "sliderDelay";
            this.sliderDelay.Size = new System.Drawing.Size(189, 23);
            this.sliderDelay.SliderWidth = 104;
            this.sliderDelay.TabIndex = 31;
            this.sliderDelay.thumbCenterY = 11;
            this.sliderDelay.ThumbEndX = 92;
            this.sliderDelay.upperLimit = ((short)(100));
            this.sliderDelay.value = ((short)(0));
            // 
            // limiterPanel
            // 
            this.limiterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.limiterPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.limiterPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.limiterPanel.ForeColor = System.Drawing.Color.LightGray;
            this.limiterPanel.Location = new System.Drawing.Point(340, 152);
            this.limiterPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.limiterPanel.Name = "limiterPanel";
            this.limiterPanel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.limiterPanel.Radius = 18;
            this.limiterPanel.RectColor = System.Drawing.Color.Gray;
            this.limiterPanel.Size = new System.Drawing.Size(331, 120);
            this.limiterPanel.sliderback = global::AEC.Properties.Resources.aux1_output_slider_back;
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
            this.mixerPanel.backgroundNormal = global::AEC.Properties.Resources.aux1_output_slider_back;
            this.mixerPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.mixerPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.mixerPanel.ForeColor = System.Drawing.Color.LightGray;
            this.mixerPanel.Location = new System.Drawing.Point(340, 8);
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
            this.workModePanel.Location = new System.Drawing.Point(340, 275);
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
            this.eqGroupPanel.Size = new System.Drawing.Size(318, 264);
            this.eqGroupPanel.TabIndex = 1;
            // 
            // channelPanel
            // 
            this.channelPanel.Controls.Add(this.btnSwitchUnit);
            this.channelPanel.Controls.Add(this.sliderHpfFreq);
            this.channelPanel.Controls.Add(this.lblHpfSlope);
            this.channelPanel.Controls.Add(this.sliderDelay);
            this.channelPanel.Controls.Add(this.cmbHpfType);
            this.channelPanel.Controls.Add(this.lblDelay);
            this.channelPanel.Controls.Add(this.lblHpf);
            this.channelPanel.Controls.Add(this.chkMute);
            this.channelPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.channelPanel.ForeColor = System.Drawing.Color.LightGray;
            this.channelPanel.Location = new System.Drawing.Point(683, 8);
            this.channelPanel.Name = "channelPanel";
            this.channelPanel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.channelPanel.Radius = 18;
            this.channelPanel.RectColor = System.Drawing.Color.Black;
            this.channelPanel.ShowRect = true;
            this.channelPanel.Size = new System.Drawing.Size(284, 264);
            this.channelPanel.TabIndex = 36;
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
            this.btnSwitchUnit.Location = new System.Drawing.Point(12, 104);
            this.btnSwitchUnit.Name = "btnSwitchUnit";
            this.btnSwitchUnit.Size = new System.Drawing.Size(18, 18);
            this.btnSwitchUnit.TabIndex = 37;
            this.btnSwitchUnit.userGDIPaint = false;
            this.btnSwitchUnit.Click += new System.EventHandler(this.btnSwitchUnit_Click);
            // 
            // X5CenterOutputPanel
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
            this.Name = "X5CenterOutputPanel";
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
        protected HSliderParam sliderHpfFreq;
        private gtCheckBox chkMute;
        private gtLabel lblHpf;
        private gtLabel lblDelay;
        protected HSliderParam sliderDelay;
        private gtLabel lblHpfSlope;
        private gtComboBox cmbHpfType;
        private gtPanel channelPanel;
        private gtButton btnSwitchUnit;
    }
}
