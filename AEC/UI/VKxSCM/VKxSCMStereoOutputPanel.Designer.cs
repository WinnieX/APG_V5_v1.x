namespace AEC.UI.VKxSCM
{
    partial class VKxSCMStereoOutputPanel
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
            this.sliderLeftDelay = new AEC.UI.HSliderParam();
            this.lblLeftDelay = new AEC.UI.gtLabel();
            this.lblRightDelay = new AEC.UI.gtLabel();
            this.sliderRightDelay = new AEC.UI.HSliderParam();
            this.chkLeftMute = new AEC.UI.gtCheckBox();
            this.chkRightMute = new AEC.UI.gtCheckBox();
            this.panelDelayAndMute = new AEC.UI.gtPanel();
            this.btnSwitchUnit = new AEC.UI.gtButton();
            this.chkRightPhase = new AEC.UI.gtCheckBox();
            this.chkLeftPhase = new AEC.UI.gtCheckBox();
            this.panelDelayAndMute.SuspendLayout();
            this.SuspendLayout();
            // 
            // mixerPanel
            // 
            this.mixerPanel.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            // 
            // sliderLeftDelay
            // 
            this.sliderLeftDelay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderLeftDelay.BackColor = System.Drawing.Color.Transparent;
            this.sliderLeftDelay.backgroundDisabled = null;
            this.sliderLeftDelay.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.sliderLeftDelay.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderLeftDelay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderLeftDelay.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderLeftDelay.Location = new System.Drawing.Point(79, 16);
            this.sliderLeftDelay.Margin = new System.Windows.Forms.Padding(0);
            this.sliderLeftDelay.Name = "sliderLeftDelay";
            this.sliderLeftDelay.Size = new System.Drawing.Size(189, 23);
            this.sliderLeftDelay.SliderWidth = 104;
            this.sliderLeftDelay.TabIndex = 26;
            this.sliderLeftDelay.thumbCenterY = 11;
            this.sliderLeftDelay.ThumbEndX = 92;
            this.sliderLeftDelay.upperLimit = ((short)(100));
            this.sliderLeftDelay.value = ((short)(0));
            this.sliderLeftDelay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelDelayAndMute_MouseUp);
            // 
            // lblLeftDelay
            // 
            this.lblLeftDelay.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblLeftDelay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLeftDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblLeftDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblLeftDelay.Location = new System.Drawing.Point(1, 16);
            this.lblLeftDelay.Name = "lblLeftDelay";
            this.lblLeftDelay.Size = new System.Drawing.Size(76, 23);
            this.lblLeftDelay.TabIndex = 25;
            this.lblLeftDelay.Text = "左通道延时";
            this.lblLeftDelay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelDelayAndMute_MouseUp);
            // 
            // lblRightDelay
            // 
            this.lblRightDelay.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblRightDelay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRightDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblRightDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblRightDelay.Location = new System.Drawing.Point(1, 45);
            this.lblRightDelay.Name = "lblRightDelay";
            this.lblRightDelay.Size = new System.Drawing.Size(76, 23);
            this.lblRightDelay.TabIndex = 25;
            this.lblRightDelay.Text = "右通道延时";
            this.lblRightDelay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelDelayAndMute_MouseUp);
            // 
            // sliderRightDelay
            // 
            this.sliderRightDelay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderRightDelay.BackColor = System.Drawing.Color.Transparent;
            this.sliderRightDelay.backgroundDisabled = null;
            this.sliderRightDelay.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.sliderRightDelay.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderRightDelay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderRightDelay.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderRightDelay.Location = new System.Drawing.Point(79, 45);
            this.sliderRightDelay.Margin = new System.Windows.Forms.Padding(0);
            this.sliderRightDelay.Name = "sliderRightDelay";
            this.sliderRightDelay.Size = new System.Drawing.Size(189, 23);
            this.sliderRightDelay.SliderWidth = 104;
            this.sliderRightDelay.TabIndex = 26;
            this.sliderRightDelay.thumbCenterY = 11;
            this.sliderRightDelay.ThumbEndX = 92;
            this.sliderRightDelay.upperLimit = ((short)(100));
            this.sliderRightDelay.value = ((short)(0));
            this.sliderRightDelay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelDelayAndMute_MouseUp);
            // 
            // chkLeftMute
            // 
            this.chkLeftMute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLeftMute.BackColor = System.Drawing.Color.Transparent;
            this.chkLeftMute.backDisabled = global::AEC.Properties.Resources.btn_mute_disabled;
            this.chkLeftMute.backHover = global::AEC.Properties.Resources.btn_mute_hover;
            this.chkLeftMute.backNormal = global::AEC.Properties.Resources.btn_mute_normal;
            this.chkLeftMute.backPressed = global::AEC.Properties.Resources.btn_mute_pressed;
            this.chkLeftMute.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.chkLeftMute.isAutoCheck = true;
            this.chkLeftMute.isChecked = false;
            this.chkLeftMute.isShowCheckedBackWhenDsiabled = true;
            this.chkLeftMute.isStretch = false;
            this.chkLeftMute.Location = new System.Drawing.Point(37, 114);
            this.chkLeftMute.Margin = new System.Windows.Forms.Padding(0);
            this.chkLeftMute.Name = "chkLeftMute";
            this.chkLeftMute.Size = new System.Drawing.Size(96, 30);
            this.chkLeftMute.TabIndex = 27;
            this.chkLeftMute.Text = "左通道静音";
            this.chkLeftMute.userGDIPaint = true;
            // 
            // chkRightMute
            // 
            this.chkRightMute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRightMute.BackColor = System.Drawing.Color.Transparent;
            this.chkRightMute.backDisabled = global::AEC.Properties.Resources.btn_mute_disabled;
            this.chkRightMute.backHover = global::AEC.Properties.Resources.btn_mute_hover;
            this.chkRightMute.backNormal = global::AEC.Properties.Resources.btn_mute_normal;
            this.chkRightMute.backPressed = global::AEC.Properties.Resources.btn_mute_pressed;
            this.chkRightMute.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.chkRightMute.isAutoCheck = true;
            this.chkRightMute.isChecked = false;
            this.chkRightMute.isShowCheckedBackWhenDsiabled = true;
            this.chkRightMute.isStretch = true;
            this.chkRightMute.Location = new System.Drawing.Point(143, 114);
            this.chkRightMute.Margin = new System.Windows.Forms.Padding(0);
            this.chkRightMute.Name = "chkRightMute";
            this.chkRightMute.Size = new System.Drawing.Size(96, 30);
            this.chkRightMute.TabIndex = 27;
            this.chkRightMute.Text = "右通道静音";
            this.chkRightMute.userGDIPaint = true;
            // 
            // panelDelayAndMute
            // 
            this.panelDelayAndMute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.panelDelayAndMute.Controls.Add(this.btnSwitchUnit);
            this.panelDelayAndMute.Controls.Add(this.chkRightPhase);
            this.panelDelayAndMute.Controls.Add(this.chkLeftPhase);
            this.panelDelayAndMute.Controls.Add(this.sliderLeftDelay);
            this.panelDelayAndMute.Controls.Add(this.chkRightMute);
            this.panelDelayAndMute.Controls.Add(this.lblLeftDelay);
            this.panelDelayAndMute.Controls.Add(this.chkLeftMute);
            this.panelDelayAndMute.Controls.Add(this.lblRightDelay);
            this.panelDelayAndMute.Controls.Add(this.sliderRightDelay);
            this.panelDelayAndMute.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.panelDelayAndMute.ForeColor = System.Drawing.Color.LightGray;
            this.panelDelayAndMute.Location = new System.Drawing.Point(346, 152);
            this.panelDelayAndMute.Name = "panelDelayAndMute";
            this.panelDelayAndMute.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelDelayAndMute.Radius = 0;
            this.panelDelayAndMute.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.panelDelayAndMute.ShowRect = false;
            this.panelDelayAndMute.Size = new System.Drawing.Size(274, 148);
            this.panelDelayAndMute.TabIndex = 28;
            this.panelDelayAndMute.title = null;
            this.panelDelayAndMute.titleHeight = 20;
            this.panelDelayAndMute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelDelayAndMute_MouseUp);
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
            this.btnSwitchUnit.Location = new System.Drawing.Point(8, 71);
            this.btnSwitchUnit.Name = "btnSwitchUnit";
            this.btnSwitchUnit.Size = new System.Drawing.Size(18, 18);
            this.btnSwitchUnit.TabIndex = 31;
            this.btnSwitchUnit.userGDIPaint = false;
            this.btnSwitchUnit.Click += new System.EventHandler(this.btnSwitchUnit_Click);
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
            this.chkRightPhase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.chkRightPhase.isAutoCheck = true;
            this.chkRightPhase.isChecked = false;
            this.chkRightPhase.isShowCheckedBackWhenDsiabled = true;
            this.chkRightPhase.isStretch = true;
            this.chkRightPhase.Location = new System.Drawing.Point(143, 80);
            this.chkRightPhase.Margin = new System.Windows.Forms.Padding(0);
            this.chkRightPhase.Name = "chkRightPhase";
            this.chkRightPhase.Size = new System.Drawing.Size(96, 30);
            this.chkRightPhase.TabIndex = 28;
            this.chkRightPhase.Text = "右通道相位 -";
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
            this.chkLeftPhase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.chkLeftPhase.isAutoCheck = true;
            this.chkLeftPhase.isChecked = false;
            this.chkLeftPhase.isShowCheckedBackWhenDsiabled = true;
            this.chkLeftPhase.isStretch = true;
            this.chkLeftPhase.Location = new System.Drawing.Point(37, 80);
            this.chkLeftPhase.Margin = new System.Windows.Forms.Padding(0);
            this.chkLeftPhase.Name = "chkLeftPhase";
            this.chkLeftPhase.Size = new System.Drawing.Size(96, 30);
            this.chkLeftPhase.TabIndex = 29;
            this.chkLeftPhase.Text = "左通道相位 +";
            this.chkLeftPhase.userGDIPaint = true;
            // 
            // VKxSCMStereoOutputPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelDelayAndMute);
            this.Name = "VKxSCMStereoOutputPanel";
            this.Controls.SetChildIndex(this.mixerPanel, 0);
            this.Controls.SetChildIndex(this.limiterPanel, 0);
            this.Controls.SetChildIndex(this.panelDelayAndMute, 0);
            this.panelDelayAndMute.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected HSliderParam sliderLeftDelay;
        private gtLabel lblLeftDelay;
        private gtLabel lblRightDelay;
        protected HSliderParam sliderRightDelay;
        private gtCheckBox chkLeftMute;
        private gtCheckBox chkRightMute;
        private AEC.UI.gtPanel panelDelayAndMute;
        private gtCheckBox chkRightPhase;
        private gtCheckBox chkLeftPhase;
        private gtButton btnSwitchUnit;

    }
}
