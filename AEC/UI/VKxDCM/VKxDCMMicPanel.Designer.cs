namespace AEC.UI.VKxDCM
{
    partial class VKxDCMMicPanel
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
            this.chkMicABLink = new AEC.UI.Controls.MImageCheckBox();
            this.compressorPanel = new AEC.UI.VKxDCM.VKxDCMCompressorPanel();
            this.panelMic = new AEC.UI.gtPanel();
            this.lblMicLevel = new AEC.UI.gtLabel();
            this.sliderMicLevel = new AEC.UI.HSliderParam();
            this.lblMicFBE = new AEC.UI.gtLabel();
            this.lblMicNoiseGate = new AEC.UI.gtLabel();
            this.micFbe = new AEC.UI.MicParam();
            this.micNoiseGate = new AEC.UI.MicParam();
            this.cmbLpfType = new AEC.UI.gtComboBox();
            this.lblMicLpfType = new AEC.UI.gtLabel();
            this.lblMicLpfFreq = new AEC.UI.gtLabel();
            this.lpfFreqSlier = new AEC.UI.HSliderParam();
            this.lblMicHpfFreq = new AEC.UI.gtLabel();
            this.hpfFreqSlier = new AEC.UI.HSliderParam();
            this.eqResetAndByPassPanel = new AEC.UI.EQResetAndByPassPanel();
            this.eqGroupPanel = new AEC.UI.EQGroupPanel();
            this.panelMic.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkMicABLink
            // 
            this.chkMicABLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMicABLink.backCheckedDisabled = global::AEC.Properties.Resources.mic_ab_linked_disabled;
            this.chkMicABLink.backCheckedHover = global::AEC.Properties.Resources.mic_ab_linked_hover;
            this.chkMicABLink.backCheckedNormal = global::AEC.Properties.Resources.mic_ab_linked_normal;
            this.chkMicABLink.backCheckedPressed = global::AEC.Properties.Resources.mic_ab_linked_pressed;
            this.chkMicABLink.BackColor = System.Drawing.Color.Transparent;
            this.chkMicABLink.backUncheckedDisabled = global::AEC.Properties.Resources.mic_ab_unlinked_disabled;
            this.chkMicABLink.backUncheckedHover = global::AEC.Properties.Resources.mic_ab_unlinked_hover;
            this.chkMicABLink.backUncheckedNormal = global::AEC.Properties.Resources.mic_ab_unlinked_normal;
            this.chkMicABLink.backUncheckedPressed = global::AEC.Properties.Resources.mic_ab_unlinked_pressed;
            this.chkMicABLink.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkMicABLink.BorderWidth = 1F;
            this.chkMicABLink.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkMicABLink.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.chkMicABLink.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkMicABLink.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkMicABLink.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.chkMicABLink.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkMicABLink.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkMicABLink.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(119)))), ((int)(((byte)(189)))));
            this.chkMicABLink.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkMicABLink.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkMicABLink.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.chkMicABLink.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkMicABLink.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkMicABLink.ImageRectangle = new System.Drawing.Rectangle(16, 0, 32, 28);
            this.chkMicABLink.isAutoCheck = false;
            this.chkMicABLink.IsChecked = false;
            this.chkMicABLink.IsCheckedInit = false;
            this.chkMicABLink.Location = new System.Drawing.Point(553, 277);
            this.chkMicABLink.Name = "chkMicABLink";
            this.chkMicABLink.Padding = new System.Windows.Forms.Padding(36, 0, 0, 0);
            this.chkMicABLink.Radius = 12;
            this.chkMicABLink.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkMicABLink.showBackFillColor = true;
            this.chkMicABLink.ShowBorder = false;
            this.chkMicABLink.ShowText = true;
            this.chkMicABLink.Size = new System.Drawing.Size(168, 28);
            this.chkMicABLink.TabIndex = 44;
            this.chkMicABLink.Text = "MIC A+B 联调";
            this.chkMicABLink.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkMicABLink.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkMicABLink.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkMicABLink.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkMicABLink.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkMicABLink.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkMicABLink.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkMicABLink.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            this.chkMicABLink.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkMicABLink.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkMicABLink.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkMicABLink.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkMicABLink.CheckedChanged += new System.EventHandler(this.chkMicABLink_CheckedChanged);
            this.chkMicABLink.Click += new System.EventHandler(this.chkMicABLink_Click);
            // 
            // compressorPanel
            // 
            this.compressorPanel.BackColor = System.Drawing.Color.Transparent;
            this.compressorPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.compressorPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.compressorPanel.ForeColor = System.Drawing.Color.LightGray;
            this.compressorPanel.Location = new System.Drawing.Point(10, 178);
            this.compressorPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.compressorPanel.Name = "compressorPanel";
            this.compressorPanel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.compressorPanel.Radius = 18;
            this.compressorPanel.RectColor = System.Drawing.Color.Gray;
            this.compressorPanel.ShowRect = true;
            this.compressorPanel.Size = new System.Drawing.Size(350, 128);
            this.compressorPanel.sliderback = global::AEC.Properties.Resources.mic_slider_back;
            this.compressorPanel.SliderParamWidth = 234;
            this.compressorPanel.sliderWidth = 158;
            this.compressorPanel.TabIndex = 26;
            this.compressorPanel.ThumbEndX = 140;
            this.compressorPanel.title = null;
            this.compressorPanel.titleHeight = 20;
            // 
            // panelMic
            // 
            this.panelMic.Controls.Add(this.lblMicLevel);
            this.panelMic.Controls.Add(this.sliderMicLevel);
            this.panelMic.Controls.Add(this.lblMicFBE);
            this.panelMic.Controls.Add(this.lblMicNoiseGate);
            this.panelMic.Controls.Add(this.micFbe);
            this.panelMic.Controls.Add(this.micNoiseGate);
            this.panelMic.Controls.Add(this.cmbLpfType);
            this.panelMic.Controls.Add(this.lblMicLpfType);
            this.panelMic.Controls.Add(this.lblMicLpfFreq);
            this.panelMic.Controls.Add(this.lpfFreqSlier);
            this.panelMic.Controls.Add(this.lblMicHpfFreq);
            this.panelMic.Controls.Add(this.hpfFreqSlier);
            this.panelMic.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelMic.ForeColor = System.Drawing.Color.LightGray;
            this.panelMic.Location = new System.Drawing.Point(10, 8);
            this.panelMic.Name = "panelMic";
            this.panelMic.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelMic.Radius = 18;
            this.panelMic.RectColor = System.Drawing.Color.Black;
            this.panelMic.ShowRect = true;
            this.panelMic.Size = new System.Drawing.Size(350, 164);
            this.panelMic.TabIndex = 25;
            this.panelMic.title = "";
            this.panelMic.titleHeight = 20;
            // 
            // lblMicLevel
            // 
            this.lblMicLevel.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicLevel.BackColor = System.Drawing.Color.Transparent;
            this.lblMicLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMicLevel.Location = new System.Drawing.Point(19, 43);
            this.lblMicLevel.Name = "lblMicLevel";
            this.lblMicLevel.Size = new System.Drawing.Size(80, 23);
            this.lblMicLevel.TabIndex = 35;
            this.lblMicLevel.Text = "麦克风电平";
            // 
            // sliderMicLevel
            // 
            this.sliderMicLevel.BackColor = System.Drawing.Color.Transparent;
            this.sliderMicLevel.backgroundDisabled = null;
            this.sliderMicLevel.backgroundNormal = global::AEC.Properties.Resources.mic_slider_back;
            this.sliderMicLevel.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderMicLevel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderMicLevel.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderMicLevel.Location = new System.Drawing.Point(103, 43);
            this.sliderMicLevel.Margin = new System.Windows.Forms.Padding(0);
            this.sliderMicLevel.Name = "sliderMicLevel";
            this.sliderMicLevel.Size = new System.Drawing.Size(232, 23);
            this.sliderMicLevel.SliderWidth = 152;
            this.sliderMicLevel.TabIndex = 34;
            this.sliderMicLevel.thumbCenterY = 11;
            this.sliderMicLevel.ThumbEndX = 140;
            this.sliderMicLevel.upperLimit = ((short)(100));
            this.sliderMicLevel.value = ((short)(0));
            // 
            // lblMicFBE
            // 
            this.lblMicFBE.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicFBE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMicFBE.BackColor = System.Drawing.Color.Transparent;
            this.lblMicFBE.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMicFBE.Location = new System.Drawing.Point(203, 13);
            this.lblMicFBE.Name = "lblMicFBE";
            this.lblMicFBE.Size = new System.Drawing.Size(36, 23);
            this.lblMicFBE.TabIndex = 33;
            this.lblMicFBE.Text = "移频";
            // 
            // lblMicNoiseGate
            // 
            this.lblMicNoiseGate.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicNoiseGate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMicNoiseGate.BackColor = System.Drawing.Color.Transparent;
            this.lblMicNoiseGate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMicNoiseGate.Location = new System.Drawing.Point(21, 13);
            this.lblMicNoiseGate.Name = "lblMicNoiseGate";
            this.lblMicNoiseGate.Size = new System.Drawing.Size(80, 23);
            this.lblMicNoiseGate.TabIndex = 32;
            this.lblMicNoiseGate.Text = "噪声门";
            // 
            // micFbe
            // 
            this.micFbe.BackColor = System.Drawing.Color.Transparent;
            this.micFbe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.micFbe.Location = new System.Drawing.Point(241, 12);
            this.micFbe.Margin = new System.Windows.Forms.Padding(0);
            this.micFbe.Name = "micFbe";
            this.micFbe.Size = new System.Drawing.Size(95, 25);
            this.micFbe.TabIndex = 31;
            // 
            // micNoiseGate
            // 
            this.micNoiseGate.BackColor = System.Drawing.Color.Transparent;
            this.micNoiseGate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.micNoiseGate.Location = new System.Drawing.Point(104, 12);
            this.micNoiseGate.Margin = new System.Windows.Forms.Padding(0);
            this.micNoiseGate.Name = "micNoiseGate";
            this.micNoiseGate.Size = new System.Drawing.Size(95, 25);
            this.micNoiseGate.TabIndex = 30;
            // 
            // cmbLpfType
            // 
            this.cmbLpfType.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbLpfType.BackColor = System.Drawing.SystemColors.Window;
            this.cmbLpfType.DropDownBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(81)))), ((int)(((byte)(0)))));
            this.cmbLpfType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLpfType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbLpfType.ForeColor = System.Drawing.Color.Black;
            this.cmbLpfType.FormattingEnabled = true;
            this.cmbLpfType.Items.AddRange(new object[] {
            "6dB First Order",
            "12dB Bessel",
            "12dB Butterworth",
            "12dB Linkwitz-Riley"});
            this.cmbLpfType.Location = new System.Drawing.Point(104, 130);
            this.cmbLpfType.Margin = new System.Windows.Forms.Padding(0);
            this.cmbLpfType.Name = "cmbLpfType";
            this.cmbLpfType.Size = new System.Drawing.Size(146, 23);
            this.cmbLpfType.TabIndex = 29;
            this.cmbLpfType.TabStop = false;
            // 
            // lblMicLpfType
            // 
            this.lblMicLpfType.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicLpfType.BackColor = System.Drawing.Color.Transparent;
            this.lblMicLpfType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMicLpfType.Location = new System.Drawing.Point(20, 130);
            this.lblMicLpfType.Name = "lblMicLpfType";
            this.lblMicLpfType.Size = new System.Drawing.Size(80, 23);
            this.lblMicLpfType.TabIndex = 29;
            this.lblMicLpfType.Text = "低通类型";
            // 
            // lblMicLpfFreq
            // 
            this.lblMicLpfFreq.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicLpfFreq.BackColor = System.Drawing.Color.Transparent;
            this.lblMicLpfFreq.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMicLpfFreq.Location = new System.Drawing.Point(20, 101);
            this.lblMicLpfFreq.Name = "lblMicLpfFreq";
            this.lblMicLpfFreq.Size = new System.Drawing.Size(80, 23);
            this.lblMicLpfFreq.TabIndex = 29;
            this.lblMicLpfFreq.Text = "麦克风低通";
            // 
            // lpfFreqSlier
            // 
            this.lpfFreqSlier.BackColor = System.Drawing.Color.Transparent;
            this.lpfFreqSlier.backgroundDisabled = null;
            this.lpfFreqSlier.backgroundNormal = global::AEC.Properties.Resources.mic_slider_back;
            this.lpfFreqSlier.backLeftTop = new System.Drawing.Point(0, 6);
            this.lpfFreqSlier.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lpfFreqSlier.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.lpfFreqSlier.Location = new System.Drawing.Point(104, 101);
            this.lpfFreqSlier.Margin = new System.Windows.Forms.Padding(0);
            this.lpfFreqSlier.Name = "lpfFreqSlier";
            this.lpfFreqSlier.Size = new System.Drawing.Size(232, 23);
            this.lpfFreqSlier.SliderWidth = 152;
            this.lpfFreqSlier.TabIndex = 28;
            this.lpfFreqSlier.thumbCenterY = 11;
            this.lpfFreqSlier.ThumbEndX = 140;
            this.lpfFreqSlier.upperLimit = ((short)(100));
            this.lpfFreqSlier.value = ((short)(0));
            // 
            // lblMicHpfFreq
            // 
            this.lblMicHpfFreq.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicHpfFreq.BackColor = System.Drawing.Color.Transparent;
            this.lblMicHpfFreq.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMicHpfFreq.Location = new System.Drawing.Point(20, 72);
            this.lblMicHpfFreq.Name = "lblMicHpfFreq";
            this.lblMicHpfFreq.Size = new System.Drawing.Size(80, 23);
            this.lblMicHpfFreq.TabIndex = 27;
            this.lblMicHpfFreq.Text = "麦克风高通";
            // 
            // hpfFreqSlier
            // 
            this.hpfFreqSlier.BackColor = System.Drawing.Color.Transparent;
            this.hpfFreqSlier.backgroundDisabled = null;
            this.hpfFreqSlier.backgroundNormal = global::AEC.Properties.Resources.mic_slider_back;
            this.hpfFreqSlier.backLeftTop = new System.Drawing.Point(0, 6);
            this.hpfFreqSlier.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.hpfFreqSlier.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.hpfFreqSlier.Location = new System.Drawing.Point(104, 72);
            this.hpfFreqSlier.Margin = new System.Windows.Forms.Padding(0);
            this.hpfFreqSlier.Name = "hpfFreqSlier";
            this.hpfFreqSlier.Size = new System.Drawing.Size(232, 23);
            this.hpfFreqSlier.SliderWidth = 152;
            this.hpfFreqSlier.TabIndex = 3;
            this.hpfFreqSlier.thumbCenterY = 11;
            this.hpfFreqSlier.ThumbEndX = 140;
            this.hpfFreqSlier.upperLimit = ((short)(100));
            this.hpfFreqSlier.value = ((short)(0));
            // 
            // eqResetAndByPassPanel
            // 
            this.eqResetAndByPassPanel.BackColor = System.Drawing.Color.Transparent;
            this.eqResetAndByPassPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.eqResetAndByPassPanel.isPass = false;
            this.eqResetAndByPassPanel.Location = new System.Drawing.Point(370, 275);
            this.eqResetAndByPassPanel.Name = "eqResetAndByPassPanel";
            this.eqResetAndByPassPanel.Size = new System.Drawing.Size(168, 33);
            this.eqResetAndByPassPanel.TabIndex = 23;
            // 
            // eqGroupPanel
            // 
            this.eqGroupPanel.BackColor = System.Drawing.Color.Transparent;
            this.eqGroupPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.eqGroupPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.eqGroupPanel.Location = new System.Drawing.Point(370, 8);
            this.eqGroupPanel.Name = "eqGroupPanel";
            this.eqGroupPanel.Size = new System.Drawing.Size(878, 264);
            this.eqGroupPanel.TabIndex = 1;
            // 
            // VKxDCMMicPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.chkMicABLink);
            this.Controls.Add(this.compressorPanel);
            this.Controls.Add(this.panelMic);
            this.Controls.Add(this.eqResetAndByPassPanel);
            this.Controls.Add(this.eqGroupPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "VKxDCMMicPanel";
            this.Size = new System.Drawing.Size(1276, 312);
            this.panelMic.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private EQGroupPanel eqGroupPanel;
        private HSliderParam hpfFreqSlier;
        private EQResetAndByPassPanel eqResetAndByPassPanel;
        private gtPanel panelMic;
        private VKxDCMCompressorPanel compressorPanel;
        private gtLabel lblMicLpfFreq;
        private HSliderParam lpfFreqSlier;
        private gtLabel lblMicHpfFreq;
        private gtLabel lblMicLpfType;
        private gtComboBox cmbLpfType;
        private MicParam micFbe;
        private MicParam micNoiseGate;
        private gtLabel lblMicNoiseGate;
        private gtLabel lblMicFBE;
        private gtLabel lblMicLevel;
        private HSliderParam sliderMicLevel;
        private Controls.MImageCheckBox chkMicABLink;
    }
}
