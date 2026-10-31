namespace AEC.UI.VKxSCM
{
    partial class VKxSCMOutputBasePanel
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
            this.mixerPanel = new AEC.UI.VKxSCM.VKxSCMMixerPanel();
            this.limiterPanel = new AEC.UI.VKxSCM.VKxSCMLimiterPanel();
            this.eqResetAndByPassPanel = new AEC.UI.EQResetAndByPassPanel();
            this.workModePanel = new AEC.UI.WorkModePanel();
            this.eqGroupPanel = new AEC.UI.EQGroupPanel();
            this.gtPanel1 = new AEC.UI.gtPanel();
            this.cmbLpfType = new AEC.UI.gtComboBox();
            this.sliderHpfFreq = new AEC.UI.HSliderParam();
            this.cmbHpfType = new AEC.UI.gtComboBox();
            this.labelHpfFreq = new AEC.UI.gtLabel();
            this.labelHpfMode = new AEC.UI.gtLabel();
            this.sliderLpfFreq = new AEC.UI.HSliderParam();
            this.labelLpfFreq = new AEC.UI.gtLabel();
            this.labelLpfMode = new AEC.UI.gtLabel();
            this.gtPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mixerPanel
            // 
            this.mixerPanel.BackColor = System.Drawing.Color.Transparent;
            this.mixerPanel.backgroundDisabled = global::AEC.Properties.Resources.output_slider_disabled;
            this.mixerPanel.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.mixerPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.mixerPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.mixerPanel.ForeColor = System.Drawing.Color.LightGray;
            this.mixerPanel.Location = new System.Drawing.Point(5, 8);
            this.mixerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.mixerPanel.Name = "mixerPanel";
            this.mixerPanel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.mixerPanel.Radius = 18;
            this.mixerPanel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.mixerPanel.ShowRect = true;
            this.mixerPanel.Size = new System.Drawing.Size(331, 136);
            this.mixerPanel.TabIndex = 10;
            this.mixerPanel.title = null;
            this.mixerPanel.titleHeight = 20;
            // 
            // limiterPanel
            // 
            this.limiterPanel.BackColor = System.Drawing.Color.Transparent;
            this.limiterPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.limiterPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.limiterPanel.ForeColor = System.Drawing.Color.LightGray;
            this.limiterPanel.Location = new System.Drawing.Point(5, 152);
            this.limiterPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.limiterPanel.Name = "limiterPanel";
            this.limiterPanel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.limiterPanel.Radius = 18;
            this.limiterPanel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.limiterPanel.ShowRect = true;
            this.limiterPanel.Size = new System.Drawing.Size(331, 154);
            this.limiterPanel.sliderbackgroundDisabled = global::AEC.Properties.Resources.output_slider_disabled;
            this.limiterPanel.sliderNormalBack = global::AEC.Properties.Resources.main_output_slider_back;
            this.limiterPanel.SliderParamWidth = 197;
            this.limiterPanel.sliderWidth = 109;
            this.limiterPanel.TabIndex = 4;
            this.limiterPanel.ThumbEndX = 92;
            this.limiterPanel.title = null;
            this.limiterPanel.titleHeight = 20;
            // 
            // eqResetAndByPassPanel
            // 
            this.eqResetAndByPassPanel.BackColor = System.Drawing.Color.Transparent;
            this.eqResetAndByPassPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.eqResetAndByPassPanel.isPass = false;
            this.eqResetAndByPassPanel.Location = new System.Drawing.Point(632, 275);
            this.eqResetAndByPassPanel.Name = "eqResetAndByPassPanel";
            this.eqResetAndByPassPanel.Size = new System.Drawing.Size(168, 33);
            this.eqResetAndByPassPanel.TabIndex = 6;
            // 
            // workModePanel
            // 
            this.workModePanel.BackColor = System.Drawing.Color.Transparent;
            this.workModePanel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.workModePanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.workModePanel.Location = new System.Drawing.Point(823, 275);
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
            this.eqGroupPanel.Location = new System.Drawing.Point(632, 8);
            this.eqGroupPanel.Name = "eqGroupPanel";
            this.eqGroupPanel.Size = new System.Drawing.Size(638, 264);
            this.eqGroupPanel.TabIndex = 1;
            // 
            // gtPanel1
            // 
            this.gtPanel1.Controls.Add(this.cmbLpfType);
            this.gtPanel1.Controls.Add(this.sliderHpfFreq);
            this.gtPanel1.Controls.Add(this.cmbHpfType);
            this.gtPanel1.Controls.Add(this.labelHpfFreq);
            this.gtPanel1.Controls.Add(this.labelHpfMode);
            this.gtPanel1.Controls.Add(this.sliderLpfFreq);
            this.gtPanel1.Controls.Add(this.labelLpfFreq);
            this.gtPanel1.Controls.Add(this.labelLpfMode);
            this.gtPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.gtPanel1.ForeColor = System.Drawing.Color.LightGray;
            this.gtPanel1.Location = new System.Drawing.Point(342, 8);
            this.gtPanel1.Name = "gtPanel1";
            this.gtPanel1.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.gtPanel1.Radius = 18;
            this.gtPanel1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.gtPanel1.ShowRect = true;
            this.gtPanel1.Size = new System.Drawing.Size(284, 298);
            this.gtPanel1.TabIndex = 9;
            this.gtPanel1.title = null;
            this.gtPanel1.titleHeight = 20;
            // 
            // cmbLpfType
            // 
            this.cmbLpfType.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbLpfType.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbLpfType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLpfType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbLpfType.ForeColor = System.Drawing.Color.Black;
            this.cmbLpfType.FormattingEnabled = true;
            this.cmbLpfType.Items.AddRange(new object[] {
            "6dB First Order",
            "12dB Bessel",
            "12dB Butterworth",
            "12dB Linkwitz-Riley",
            "18dB Bessel",
            "18dB Butterworth",
            "24dB Bessel",
            "24dB Butterworth",
            "24dB Linkwitz-Riley"});
            this.cmbLpfType.Location = new System.Drawing.Point(124, 112);
            this.cmbLpfType.Name = "cmbLpfType";
            this.cmbLpfType.Size = new System.Drawing.Size(148, 23);
            this.cmbLpfType.TabIndex = 25;
            this.cmbLpfType.TabStop = false;
            // 
            // sliderHpfFreq
            // 
            this.sliderHpfFreq.BackColor = System.Drawing.Color.Transparent;
            this.sliderHpfFreq.backgroundDisabled = global::AEC.Properties.Resources.output_slider_disabled;
            this.sliderHpfFreq.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.sliderHpfFreq.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderHpfFreq.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderHpfFreq.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderHpfFreq.Location = new System.Drawing.Point(74, 8);
            this.sliderHpfFreq.Margin = new System.Windows.Forms.Padding(0);
            this.sliderHpfFreq.Name = "sliderHpfFreq";
            this.sliderHpfFreq.Size = new System.Drawing.Size(198, 23);
            this.sliderHpfFreq.SliderWidth = 108;
            this.sliderHpfFreq.TabIndex = 24;
            this.sliderHpfFreq.thumbCenterY = 11;
            this.sliderHpfFreq.ThumbEndX = 92;
            this.sliderHpfFreq.upperLimit = ((short)(100));
            this.sliderHpfFreq.value = ((short)(0));
            // 
            // cmbHpfType
            // 
            this.cmbHpfType.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbHpfType.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbHpfType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHpfType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbHpfType.ForeColor = System.Drawing.Color.Black;
            this.cmbHpfType.FormattingEnabled = true;
            this.cmbHpfType.Items.AddRange(new object[] {
            "6dB First Order",
            "12dB Bessel",
            "12dB Butterworth",
            "12dB Linkwitz-Riley",
            "18dB Bessel",
            "18dB Butterworth",
            "24dB Bessel",
            "24dB Butterworth",
            "24dB Linkwitz-Riley"});
            this.cmbHpfType.Location = new System.Drawing.Point(124, 42);
            this.cmbHpfType.Margin = new System.Windows.Forms.Padding(0);
            this.cmbHpfType.Name = "cmbHpfType";
            this.cmbHpfType.Size = new System.Drawing.Size(148, 23);
            this.cmbHpfType.TabIndex = 25;
            this.cmbHpfType.TabStop = false;
            // 
            // labelHpfFreq
            // 
            this.labelHpfFreq.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.labelHpfFreq.BackColor = System.Drawing.Color.Transparent;
            this.labelHpfFreq.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.labelHpfFreq.Location = new System.Drawing.Point(34, 8);
            this.labelHpfFreq.Name = "labelHpfFreq";
            this.labelHpfFreq.Size = new System.Drawing.Size(37, 23);
            this.labelHpfFreq.TabIndex = 22;
            this.labelHpfFreq.Text = "高通";
            // 
            // labelHpfMode
            // 
            this.labelHpfMode.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.labelHpfMode.BackColor = System.Drawing.Color.Transparent;
            this.labelHpfMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.labelHpfMode.Location = new System.Drawing.Point(8, 42);
            this.labelHpfMode.Name = "labelHpfMode";
            this.labelHpfMode.Size = new System.Drawing.Size(108, 23);
            this.labelHpfMode.TabIndex = 22;
            this.labelHpfMode.Text = "高通滤波器模式";
            // 
            // sliderLpfFreq
            // 
            this.sliderLpfFreq.BackColor = System.Drawing.Color.Transparent;
            this.sliderLpfFreq.backgroundDisabled = global::AEC.Properties.Resources.output_slider_disabled;
            this.sliderLpfFreq.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.sliderLpfFreq.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderLpfFreq.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderLpfFreq.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderLpfFreq.Location = new System.Drawing.Point(74, 78);
            this.sliderLpfFreq.Margin = new System.Windows.Forms.Padding(0);
            this.sliderLpfFreq.Name = "sliderLpfFreq";
            this.sliderLpfFreq.Size = new System.Drawing.Size(198, 23);
            this.sliderLpfFreq.SliderWidth = 108;
            this.sliderLpfFreq.TabIndex = 23;
            this.sliderLpfFreq.thumbCenterY = 11;
            this.sliderLpfFreq.ThumbEndX = 92;
            this.sliderLpfFreq.upperLimit = ((short)(100));
            this.sliderLpfFreq.value = ((short)(0));
            // 
            // labelLpfFreq
            // 
            this.labelLpfFreq.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.labelLpfFreq.BackColor = System.Drawing.Color.Transparent;
            this.labelLpfFreq.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.labelLpfFreq.Location = new System.Drawing.Point(34, 78);
            this.labelLpfFreq.Name = "labelLpfFreq";
            this.labelLpfFreq.Size = new System.Drawing.Size(37, 23);
            this.labelLpfFreq.TabIndex = 21;
            this.labelLpfFreq.Text = "低通";
            // 
            // labelLpfMode
            // 
            this.labelLpfMode.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.labelLpfMode.BackColor = System.Drawing.Color.Transparent;
            this.labelLpfMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.labelLpfMode.Location = new System.Drawing.Point(8, 112);
            this.labelLpfMode.Name = "labelLpfMode";
            this.labelLpfMode.Size = new System.Drawing.Size(108, 23);
            this.labelLpfMode.TabIndex = 22;
            this.labelLpfMode.Text = "低通滤波器模式";
            // 
            // VKxSCMOutputBasePanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.mixerPanel);
            this.Controls.Add(this.limiterPanel);
            this.Controls.Add(this.eqResetAndByPassPanel);
            this.Controls.Add(this.workModePanel);
            this.Controls.Add(this.eqGroupPanel);
            this.Controls.Add(this.gtPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "VKxSCMOutputBasePanel";
            this.Size = new System.Drawing.Size(1276, 312);
            this.gtPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private EQGroupPanel eqGroupPanel;
        private gtLabel labelLpfFreq;
        private gtLabel labelHpfFreq;
        protected HSliderParam sliderLpfFreq;
        protected HSliderParam sliderHpfFreq;
        protected VKxSCMLimiterPanel limiterPanel;
        private gtLabel labelLpfMode;
        private gtLabel labelHpfMode;
        private WorkModePanel workModePanel;
        private EQResetAndByPassPanel eqResetAndByPassPanel;
        protected gtComboBox cmbLpfType;
        protected gtComboBox cmbHpfType;
        private gtPanel gtPanel1;
        protected VKxSCMMixerPanel mixerPanel;
    }
}
