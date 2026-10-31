namespace AEC.UI.MX
{
    partial class MXOutputPanel
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
            this.rdoLinkage = new AEC.UI.gtRadioButton();
            this.rdoRightCh = new AEC.UI.gtRadioButton();
            this.rdoLeftCh = new AEC.UI.gtRadioButton();
            this.outputPanel.SuspendLayout();
            this.panelDelayAndMute.SuspendLayout();
            this.SuspendLayout();
            // 
            // mixerPanel
            // 
            this.mixerPanel.Size = new System.Drawing.Size(331, 130);
            // 
            // outputPanel
            // 
            this.outputPanel.Controls.Add(this.panelDelayAndMute);
            this.outputPanel.Controls.SetChildIndex(this.sliderHpfQ, 0);
            this.outputPanel.Controls.SetChildIndex(this.sliderLpfFreq, 0);
            this.outputPanel.Controls.SetChildIndex(this.sliderLpfQ, 0);
            this.outputPanel.Controls.SetChildIndex(this.panelDelayAndMute, 0);
            this.outputPanel.Controls.SetChildIndex(this.cmbHpfType, 0);
            this.outputPanel.Controls.SetChildIndex(this.sliderHpfFreq, 0);
            this.outputPanel.Controls.SetChildIndex(this.cmbLpfType, 0);
            // 
            // sliderLeftDelay
            // 
            this.sliderLeftDelay.BackColor = System.Drawing.Color.Transparent;
            this.sliderLeftDelay.backgroundDisabled = null;
            this.sliderLeftDelay.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.sliderLeftDelay.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderLeftDelay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderLeftDelay.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderLeftDelay.Location = new System.Drawing.Point(87, 0);
            this.sliderLeftDelay.Margin = new System.Windows.Forms.Padding(0);
            this.sliderLeftDelay.Name = "sliderLeftDelay";
            this.sliderLeftDelay.Size = new System.Drawing.Size(181, 23);
            this.sliderLeftDelay.SliderWidth = 104;
            this.sliderLeftDelay.TabIndex = 26;
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
            this.lblLeftDelay.Location = new System.Drawing.Point(0, 0);
            this.lblLeftDelay.Name = "lblLeftDelay";
            this.lblLeftDelay.Size = new System.Drawing.Size(84, 23);
            this.lblLeftDelay.TabIndex = 25;
            this.lblLeftDelay.Text = "左通道延时";
            // 
            // lblRightDelay
            // 
            this.lblRightDelay.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblRightDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblRightDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblRightDelay.Location = new System.Drawing.Point(0, 26);
            this.lblRightDelay.Name = "lblRightDelay";
            this.lblRightDelay.Size = new System.Drawing.Size(84, 23);
            this.lblRightDelay.TabIndex = 25;
            this.lblRightDelay.Text = "右通道延时";
            // 
            // sliderRightDelay
            // 
            this.sliderRightDelay.BackColor = System.Drawing.Color.Transparent;
            this.sliderRightDelay.backgroundDisabled = null;
            this.sliderRightDelay.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.sliderRightDelay.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderRightDelay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderRightDelay.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderRightDelay.Location = new System.Drawing.Point(87, 26);
            this.sliderRightDelay.Margin = new System.Windows.Forms.Padding(0);
            this.sliderRightDelay.Name = "sliderRightDelay";
            this.sliderRightDelay.Size = new System.Drawing.Size(181, 23);
            this.sliderRightDelay.SliderWidth = 104;
            this.sliderRightDelay.TabIndex = 26;
            this.sliderRightDelay.thumbCenterY = 11;
            this.sliderRightDelay.ThumbEndX = 92;
            this.sliderRightDelay.upperLimit = ((short)(100));
            this.sliderRightDelay.value = ((short)(0));
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
            this.chkLeftMute.Location = new System.Drawing.Point(37, 52);
            this.chkLeftMute.Margin = new System.Windows.Forms.Padding(0);
            this.chkLeftMute.Name = "chkLeftMute";
            this.chkLeftMute.Size = new System.Drawing.Size(96, 30);
            this.chkLeftMute.TabIndex = 27;
            this.chkLeftMute.Text = "左通道静音";
            this.chkLeftMute.userGDIPaint = true;
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
            this.chkRightMute.Location = new System.Drawing.Point(143, 52);
            this.chkRightMute.Margin = new System.Windows.Forms.Padding(0);
            this.chkRightMute.Name = "chkRightMute";
            this.chkRightMute.Size = new System.Drawing.Size(96, 30);
            this.chkRightMute.TabIndex = 27;
            this.chkRightMute.Text = "右通道静音";
            this.chkRightMute.userGDIPaint = true;
            // 
            // panelDelayAndMute
            // 
            this.panelDelayAndMute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelDelayAndMute.Controls.Add(this.sliderLeftDelay);
            this.panelDelayAndMute.Controls.Add(this.chkRightMute);
            this.panelDelayAndMute.Controls.Add(this.lblLeftDelay);
            this.panelDelayAndMute.Controls.Add(this.chkLeftMute);
            this.panelDelayAndMute.Controls.Add(this.lblRightDelay);
            this.panelDelayAndMute.Controls.Add(this.sliderRightDelay);
            this.panelDelayAndMute.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelDelayAndMute.ForeColor = System.Drawing.Color.LightGray;
            this.panelDelayAndMute.Location = new System.Drawing.Point(5, 172);
            this.panelDelayAndMute.Name = "panelDelayAndMute";
            this.panelDelayAndMute.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelDelayAndMute.Radius = 0;
            this.panelDelayAndMute.RectColor = System.Drawing.Color.Black;
            this.panelDelayAndMute.ShowRect = false;
            this.panelDelayAndMute.Size = new System.Drawing.Size(274, 85);
            this.panelDelayAndMute.TabIndex = 28;
            this.panelDelayAndMute.title = null;
            this.panelDelayAndMute.titleHeight = 20;
            // 
            // rdoLinkage
            // 
            this.rdoLinkage.backChecked = global::AEC.Properties.Resources.rdo_main_output_eqcm_linkage_pressed;
            this.rdoLinkage.BackColor = System.Drawing.Color.Transparent;
            this.rdoLinkage.backDisabled = global::AEC.Properties.Resources.rdo_output_eqcm_normal;
            this.rdoLinkage.backHover = global::AEC.Properties.Resources.rdo_main_output_eqcm_linkage_hover;
            this.rdoLinkage.backNormal = global::AEC.Properties.Resources.rdo_output_eqcm_normal;
            this.rdoLinkage.ForeColor = System.Drawing.Color.LightGray;
            this.rdoLinkage.isAutoCheck = true;
            this.rdoLinkage.isChecked = false;
            this.rdoLinkage.isCheckInit = false;
            this.rdoLinkage.isShowCheckedBackWhenDsiabled = true;
            this.rdoLinkage.isStretch = false;
            this.rdoLinkage.Location = new System.Drawing.Point(522, 276);
            this.rdoLinkage.Name = "rdoLinkage";
            this.rdoLinkage.Size = new System.Drawing.Size(53, 27);
            this.rdoLinkage.TabIndex = 29;
            this.rdoLinkage.Text = "联调";
            this.rdoLinkage.userGDIPaint = false;
            // 
            // rdoRightCh
            // 
            this.rdoRightCh.backChecked = global::AEC.Properties.Resources.rdo_output_eqcm_right_pressed;
            this.rdoRightCh.BackColor = System.Drawing.Color.Transparent;
            this.rdoRightCh.backDisabled = global::AEC.Properties.Resources.rdo_output_eqcm_normal;
            this.rdoRightCh.backHover = global::AEC.Properties.Resources.rdo_output_eqcm_right_hover;
            this.rdoRightCh.backNormal = global::AEC.Properties.Resources.rdo_output_eqcm_normal;
            this.rdoRightCh.ForeColor = System.Drawing.Color.LightGray;
            this.rdoRightCh.isAutoCheck = true;
            this.rdoRightCh.isChecked = false;
            this.rdoRightCh.isCheckInit = false;
            this.rdoRightCh.isShowCheckedBackWhenDsiabled = true;
            this.rdoRightCh.isStretch = false;
            this.rdoRightCh.Location = new System.Drawing.Point(457, 276);
            this.rdoRightCh.Name = "rdoRightCh";
            this.rdoRightCh.Size = new System.Drawing.Size(53, 27);
            this.rdoRightCh.TabIndex = 30;
            this.rdoRightCh.Text = "右";
            this.rdoRightCh.userGDIPaint = false;
            // 
            // rdoLeftCh
            // 
            this.rdoLeftCh.backChecked = global::AEC.Properties.Resources.rdo_output_eqcm_left_pressed;
            this.rdoLeftCh.BackColor = System.Drawing.Color.Transparent;
            this.rdoLeftCh.backDisabled = global::AEC.Properties.Resources.rdo_output_eqcm_normal;
            this.rdoLeftCh.backHover = global::AEC.Properties.Resources.rdo_output_eqcm_left_hover;
            this.rdoLeftCh.backNormal = global::AEC.Properties.Resources.rdo_output_eqcm_normal;
            this.rdoLeftCh.ForeColor = System.Drawing.Color.LightGray;
            this.rdoLeftCh.isAutoCheck = true;
            this.rdoLeftCh.isChecked = false;
            this.rdoLeftCh.isCheckInit = false;
            this.rdoLeftCh.isShowCheckedBackWhenDsiabled = true;
            this.rdoLeftCh.isStretch = false;
            this.rdoLeftCh.Location = new System.Drawing.Point(392, 276);
            this.rdoLeftCh.Name = "rdoLeftCh";
            this.rdoLeftCh.Size = new System.Drawing.Size(53, 27);
            this.rdoLeftCh.TabIndex = 31;
            this.rdoLeftCh.Text = "左";
            this.rdoLeftCh.userGDIPaint = false;
            // 
            // MXOutputPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.rdoLinkage);
            this.Controls.Add(this.rdoRightCh);
            this.Controls.Add(this.rdoLeftCh);
            this.Name = "MXOutputPanel";
            this.Controls.SetChildIndex(this.workModePanel, 0);
            this.Controls.SetChildIndex(this.eqResetAndByPassPanel, 0);
            this.Controls.SetChildIndex(this.outputPanel, 0);
            this.Controls.SetChildIndex(this.mixerPanel, 0);
            this.Controls.SetChildIndex(this.compressorPanel, 0);
            this.Controls.SetChildIndex(this.rdoLeftCh, 0);
            this.Controls.SetChildIndex(this.rdoRightCh, 0);
            this.Controls.SetChildIndex(this.rdoLinkage, 0);
            this.outputPanel.ResumeLayout(false);
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
        private gtRadioButton rdoRightCh;
        private gtRadioButton rdoLeftCh;
        private gtRadioButton rdoLinkage;

    }
}
