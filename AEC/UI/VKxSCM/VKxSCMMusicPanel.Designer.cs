namespace AEC.UI.VKxSCM
{
    partial class VKxSCMMusicPanel
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panelBassExciter = new AEC.UI.gtPanel();
            this.btnResetBoostLevel = new AEC.UI.gtButton();
            this.btnResetFBGain = new AEC.UI.gtButton();
            this.btnResetXoverFreq = new AEC.UI.gtButton();
            this.lblSuperBassExciterFBGain = new AEC.UI.gtLabel();
            this.lblBassExciterBoostLevel = new AEC.UI.gtLabel();
            this.sliderBassExciterBoostLevel = new AEC.UI.HSliderParam();
            this.gtLabel1 = new AEC.UI.gtLabel();
            this.sliderBassExciterFBGain = new AEC.UI.HSliderParam();
            this.chkBassExciterBypass = new AEC.UI.Controls.MSwitch();
            this.lblBassExciterXoverFreq = new AEC.UI.gtLabel();
            this.sliderBassExciterXoverFreq = new AEC.UI.HSliderParam();
            this.panelHPF = new AEC.UI.gtPanel();
            this.cmbHpfType = new AEC.UI.gtComboBox();
            this.lblHpfType = new AEC.UI.gtLabel();
            this.hpfFreqSlider = new AEC.UI.HSliderParam();
            this.workModePanel = new AEC.UI.WorkModePanel();
            this.eqResetAndByPassPanel = new AEC.UI.EQResetAndByPassPanel();
            this.eqGroupPanel = new AEC.UI.EQGroupPanel();
            this.panelMusicInput = new AEC.UI.gtPanel();
            this.lblMusicInputPort = new AEC.UI.gtLabel();
            this.cmbMusicInputPort = new AEC.UI.gtComboBox();
            this.panelBassExciter.SuspendLayout();
            this.panelHPF.SuspendLayout();
            this.panelMusicInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBassExciter
            // 
            this.panelBassExciter.Controls.Add(this.btnResetBoostLevel);
            this.panelBassExciter.Controls.Add(this.btnResetFBGain);
            this.panelBassExciter.Controls.Add(this.btnResetXoverFreq);
            this.panelBassExciter.Controls.Add(this.lblSuperBassExciterFBGain);
            this.panelBassExciter.Controls.Add(this.lblBassExciterBoostLevel);
            this.panelBassExciter.Controls.Add(this.sliderBassExciterBoostLevel);
            this.panelBassExciter.Controls.Add(this.gtLabel1);
            this.panelBassExciter.Controls.Add(this.sliderBassExciterFBGain);
            this.panelBassExciter.Controls.Add(this.chkBassExciterBypass);
            this.panelBassExciter.Controls.Add(this.lblBassExciterXoverFreq);
            this.panelBassExciter.Controls.Add(this.sliderBassExciterXoverFreq);
            this.panelBassExciter.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.panelBassExciter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.panelBassExciter.Location = new System.Drawing.Point(10, 85);
            this.panelBassExciter.Name = "panelBassExciter";
            this.panelBassExciter.Padding = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.panelBassExciter.Radius = 18;
            this.panelBassExciter.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.panelBassExciter.ShowRect = true;
            this.panelBassExciter.Size = new System.Drawing.Size(370, 114);
            this.panelBassExciter.TabIndex = 7;
            this.panelBassExciter.title = "电子管低音";
            this.panelBassExciter.titleHeight = 20;
            // 
            // btnResetBoostLevel
            // 
            this.btnResetBoostLevel.BackColor = System.Drawing.Color.Transparent;
            this.btnResetBoostLevel.backDisabled = global::AEC.Properties.Resources.switch_unit_disabled;
            this.btnResetBoostLevel.backHover = global::AEC.Properties.Resources.switch_unit_hover;
            this.btnResetBoostLevel.backNormal = global::AEC.Properties.Resources.switch_unit_normal;
            this.btnResetBoostLevel.backPressed = global::AEC.Properties.Resources.switch_unit_pressed;
            this.btnResetBoostLevel.ForeColor = System.Drawing.Color.White;
            this.btnResetBoostLevel.isStretch = false;
            this.btnResetBoostLevel.Location = new System.Drawing.Point(8, 87);
            this.btnResetBoostLevel.Name = "btnResetBoostLevel";
            this.btnResetBoostLevel.Size = new System.Drawing.Size(18, 18);
            this.btnResetBoostLevel.TabIndex = 39;
            this.btnResetBoostLevel.userGDIPaint = false;
            this.btnResetBoostLevel.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnResetFBGain
            // 
            this.btnResetFBGain.BackColor = System.Drawing.Color.Transparent;
            this.btnResetFBGain.backDisabled = global::AEC.Properties.Resources.switch_unit_disabled;
            this.btnResetFBGain.backHover = global::AEC.Properties.Resources.switch_unit_hover;
            this.btnResetFBGain.backNormal = global::AEC.Properties.Resources.switch_unit_normal;
            this.btnResetFBGain.backPressed = global::AEC.Properties.Resources.switch_unit_pressed;
            this.btnResetFBGain.ForeColor = System.Drawing.Color.White;
            this.btnResetFBGain.isStretch = false;
            this.btnResetFBGain.Location = new System.Drawing.Point(8, 62);
            this.btnResetFBGain.Name = "btnResetFBGain";
            this.btnResetFBGain.Size = new System.Drawing.Size(18, 18);
            this.btnResetFBGain.TabIndex = 38;
            this.btnResetFBGain.userGDIPaint = false;
            this.btnResetFBGain.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnResetXoverFreq
            // 
            this.btnResetXoverFreq.BackColor = System.Drawing.Color.Transparent;
            this.btnResetXoverFreq.backDisabled = global::AEC.Properties.Resources.switch_unit_disabled;
            this.btnResetXoverFreq.backHover = global::AEC.Properties.Resources.switch_unit_hover;
            this.btnResetXoverFreq.backNormal = global::AEC.Properties.Resources.switch_unit_normal;
            this.btnResetXoverFreq.backPressed = global::AEC.Properties.Resources.switch_unit_pressed;
            this.btnResetXoverFreq.ForeColor = System.Drawing.Color.White;
            this.btnResetXoverFreq.isStretch = false;
            this.btnResetXoverFreq.Location = new System.Drawing.Point(8, 35);
            this.btnResetXoverFreq.Name = "btnResetXoverFreq";
            this.btnResetXoverFreq.Size = new System.Drawing.Size(18, 18);
            this.btnResetXoverFreq.TabIndex = 37;
            this.btnResetXoverFreq.userGDIPaint = false;
            this.btnResetXoverFreq.Click += new System.EventHandler(this.btn_Click);
            // 
            // lblSuperBassExciterFBGain
            // 
            this.lblSuperBassExciterFBGain.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblSuperBassExciterFBGain.BackColor = System.Drawing.Color.Transparent;
            this.lblSuperBassExciterFBGain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblSuperBassExciterFBGain.Location = new System.Drawing.Point(8, 59);
            this.lblSuperBassExciterFBGain.Name = "lblSuperBassExciterFBGain";
            this.lblSuperBassExciterFBGain.Size = new System.Drawing.Size(84, 25);
            this.lblSuperBassExciterFBGain.TabIndex = 36;
            this.lblSuperBassExciterFBGain.Text = "深度";
            // 
            // lblBassExciterBoostLevel
            // 
            this.lblBassExciterBoostLevel.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblBassExciterBoostLevel.BackColor = System.Drawing.Color.Transparent;
            this.lblBassExciterBoostLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblBassExciterBoostLevel.Location = new System.Drawing.Point(8, 84);
            this.lblBassExciterBoostLevel.Name = "lblBassExciterBoostLevel";
            this.lblBassExciterBoostLevel.Size = new System.Drawing.Size(84, 25);
            this.lblBassExciterBoostLevel.TabIndex = 33;
            this.lblBassExciterBoostLevel.Text = "电平";
            // 
            // sliderBassExciterBoostLevel
            // 
            this.sliderBassExciterBoostLevel.BackColor = System.Drawing.Color.Transparent;
            this.sliderBassExciterBoostLevel.backgroundDisabled = global::AEC.Properties.Resources.music_slider_back_disabled;
            this.sliderBassExciterBoostLevel.backgroundNormal = global::AEC.Properties.Resources.music_slider_back;
            this.sliderBassExciterBoostLevel.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderBassExciterBoostLevel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderBassExciterBoostLevel.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderBassExciterBoostLevel.Location = new System.Drawing.Point(96, 84);
            this.sliderBassExciterBoostLevel.Margin = new System.Windows.Forms.Padding(0);
            this.sliderBassExciterBoostLevel.Name = "sliderBassExciterBoostLevel";
            this.sliderBassExciterBoostLevel.Size = new System.Drawing.Size(268, 23);
            this.sliderBassExciterBoostLevel.SliderWidth = 188;
            this.sliderBassExciterBoostLevel.TabIndex = 32;
            this.sliderBassExciterBoostLevel.thumbCenterY = 11;
            this.sliderBassExciterBoostLevel.ThumbEndX = 176;
            this.sliderBassExciterBoostLevel.upperLimit = ((short)(100));
            this.sliderBassExciterBoostLevel.value = ((short)(0));
            // 
            // gtLabel1
            // 
            this.gtLabel1.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.gtLabel1.BackColor = System.Drawing.Color.Transparent;
            this.gtLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gtLabel1.Location = new System.Drawing.Point(8, 58);
            this.gtLabel1.Name = "gtLabel1";
            this.gtLabel1.Size = new System.Drawing.Size(64, 25);
            this.gtLabel1.TabIndex = 31;
            // 
            // sliderBassExciterFBGain
            // 
            this.sliderBassExciterFBGain.BackColor = System.Drawing.Color.Transparent;
            this.sliderBassExciterFBGain.backgroundDisabled = global::AEC.Properties.Resources.music_slider_back_disabled;
            this.sliderBassExciterFBGain.backgroundNormal = global::AEC.Properties.Resources.music_slider_back;
            this.sliderBassExciterFBGain.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderBassExciterFBGain.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderBassExciterFBGain.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderBassExciterFBGain.Location = new System.Drawing.Point(96, 58);
            this.sliderBassExciterFBGain.Margin = new System.Windows.Forms.Padding(0);
            this.sliderBassExciterFBGain.Name = "sliderBassExciterFBGain";
            this.sliderBassExciterFBGain.Size = new System.Drawing.Size(268, 23);
            this.sliderBassExciterFBGain.SliderWidth = 188;
            this.sliderBassExciterFBGain.TabIndex = 30;
            this.sliderBassExciterFBGain.thumbCenterY = 11;
            this.sliderBassExciterFBGain.ThumbEndX = 176;
            this.sliderBassExciterFBGain.upperLimit = ((short)(100));
            this.sliderBassExciterFBGain.value = ((short)(0));
            // 
            // chkBassExciterBypass
            // 
            this.chkBassExciterBypass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkBassExciterBypass.BorderWidth = 1F;
            this.chkBassExciterBypass.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkBassExciterBypass.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.chkBassExciterBypass.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkBassExciterBypass.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkBassExciterBypass.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(255)))), ((int)(((byte)(3)))));
            this.chkBassExciterBypass.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkBassExciterBypass.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkBassExciterBypass.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(208)))), ((int)(((byte)(7)))));
            this.chkBassExciterBypass.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkBassExciterBypass.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkBassExciterBypass.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(184)))), ((int)(((byte)(6)))));
            this.chkBassExciterBypass.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkBassExciterBypass.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkBassExciterBypass.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkBassExciterBypass.isAutoCheck = true;
            this.chkBassExciterBypass.IsChecked = false;
            this.chkBassExciterBypass.IsCheckedInit = false;
            this.chkBassExciterBypass.Location = new System.Drawing.Point(96, 8);
            this.chkBassExciterBypass.Name = "chkBassExciterBypass";
            this.chkBassExciterBypass.Radius = 22;
            this.chkBassExciterBypass.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkBassExciterBypass.ShowBorder = false;
            this.chkBassExciterBypass.ShowText = true;
            this.chkBassExciterBypass.Size = new System.Drawing.Size(40, 22);
            this.chkBassExciterBypass.SwitchWdith = 40;
            this.chkBassExciterBypass.TabIndex = 29;
            this.chkBassExciterBypass.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkBassExciterBypass.UncheckedDisbaledFillColor = System.Drawing.Color.Gray;
            this.chkBassExciterBypass.UncheckedDisbaledTextColor = System.Drawing.Color.Silver;
            this.chkBassExciterBypass.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkBassExciterBypass.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkBassExciterBypass.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkBassExciterBypass.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkBassExciterBypass.UncheckedNormalFillColor = System.Drawing.Color.Silver;
            this.chkBassExciterBypass.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(240)))));
            this.chkBassExciterBypass.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkBassExciterBypass.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkBassExciterBypass.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkBassExciterBypass.CheckedChanged += new System.EventHandler(this.chkBassExciterBypass_CheckedChanged);
            // 
            // lblBassExciterXoverFreq
            // 
            this.lblBassExciterXoverFreq.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblBassExciterXoverFreq.BackColor = System.Drawing.Color.Transparent;
            this.lblBassExciterXoverFreq.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblBassExciterXoverFreq.Location = new System.Drawing.Point(8, 32);
            this.lblBassExciterXoverFreq.Name = "lblBassExciterXoverFreq";
            this.lblBassExciterXoverFreq.Size = new System.Drawing.Size(84, 25);
            this.lblBassExciterXoverFreq.TabIndex = 28;
            this.lblBassExciterXoverFreq.Text = "分频点";
            // 
            // sliderBassExciterXoverFreq
            // 
            this.sliderBassExciterXoverFreq.BackColor = System.Drawing.Color.Transparent;
            this.sliderBassExciterXoverFreq.backgroundDisabled = global::AEC.Properties.Resources.music_slider_back_disabled;
            this.sliderBassExciterXoverFreq.backgroundNormal = global::AEC.Properties.Resources.music_slider_back;
            this.sliderBassExciterXoverFreq.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderBassExciterXoverFreq.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderBassExciterXoverFreq.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderBassExciterXoverFreq.Location = new System.Drawing.Point(96, 32);
            this.sliderBassExciterXoverFreq.Margin = new System.Windows.Forms.Padding(0);
            this.sliderBassExciterXoverFreq.Name = "sliderBassExciterXoverFreq";
            this.sliderBassExciterXoverFreq.Size = new System.Drawing.Size(268, 23);
            this.sliderBassExciterXoverFreq.SliderWidth = 188;
            this.sliderBassExciterXoverFreq.TabIndex = 2;
            this.sliderBassExciterXoverFreq.thumbCenterY = 11;
            this.sliderBassExciterXoverFreq.ThumbEndX = 176;
            this.sliderBassExciterXoverFreq.upperLimit = ((short)(100));
            this.sliderBassExciterXoverFreq.value = ((short)(0));
            // 
            // panelHPF
            // 
            this.panelHPF.Controls.Add(this.cmbHpfType);
            this.panelHPF.Controls.Add(this.lblHpfType);
            this.panelHPF.Controls.Add(this.hpfFreqSlider);
            this.panelHPF.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.panelHPF.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.panelHPF.Location = new System.Drawing.Point(10, 203);
            this.panelHPF.Name = "panelHPF";
            this.panelHPF.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelHPF.Radius = 18;
            this.panelHPF.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.panelHPF.ShowRect = true;
            this.panelHPF.Size = new System.Drawing.Size(370, 103);
            this.panelHPF.TabIndex = 6;
            this.panelHPF.title = "音乐至主输出高通";
            this.panelHPF.titleHeight = 20;
            // 
            // cmbHpfType
            // 
            this.cmbHpfType.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbHpfType.BackColor = System.Drawing.SystemColors.Window;
            this.cmbHpfType.DropDownBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(208)))), ((int)(((byte)(7)))));
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
            this.cmbHpfType.Location = new System.Drawing.Point(120, 64);
            this.cmbHpfType.Margin = new System.Windows.Forms.Padding(0);
            this.cmbHpfType.Name = "cmbHpfType";
            this.cmbHpfType.Size = new System.Drawing.Size(146, 23);
            this.cmbHpfType.TabIndex = 30;
            this.cmbHpfType.TabStop = false;
            // 
            // lblHpfType
            // 
            this.lblHpfType.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblHpfType.BackColor = System.Drawing.Color.Transparent;
            this.lblHpfType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblHpfType.Location = new System.Drawing.Point(37, 64);
            this.lblHpfType.Name = "lblHpfType";
            this.lblHpfType.Size = new System.Drawing.Size(80, 23);
            this.lblHpfType.TabIndex = 31;
            this.lblHpfType.Text = "滤波器类型";
            // 
            // hpfFreqSlider
            // 
            this.hpfFreqSlider.BackColor = System.Drawing.Color.Transparent;
            this.hpfFreqSlider.backgroundDisabled = global::AEC.Properties.Resources.music_slider_back_disabled;
            this.hpfFreqSlider.backgroundNormal = global::AEC.Properties.Resources.music_slider_back;
            this.hpfFreqSlider.backLeftTop = new System.Drawing.Point(0, 6);
            this.hpfFreqSlider.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.hpfFreqSlider.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.hpfFreqSlider.Location = new System.Drawing.Point(48, 32);
            this.hpfFreqSlider.Margin = new System.Windows.Forms.Padding(0);
            this.hpfFreqSlider.Name = "hpfFreqSlider";
            this.hpfFreqSlider.Size = new System.Drawing.Size(268, 23);
            this.hpfFreqSlider.SliderWidth = 188;
            this.hpfFreqSlider.TabIndex = 2;
            this.hpfFreqSlider.thumbCenterY = 11;
            this.hpfFreqSlider.ThumbEndX = 176;
            this.hpfFreqSlider.upperLimit = ((short)(100));
            this.hpfFreqSlider.value = ((short)(0));
            // 
            // workModePanel
            // 
            this.workModePanel.BackColor = System.Drawing.Color.Transparent;
            this.workModePanel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.workModePanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.workModePanel.Location = new System.Drawing.Point(595, 275);
            this.workModePanel.Margin = new System.Windows.Forms.Padding(0);
            this.workModePanel.Name = "workModePanel";
            this.workModePanel.Size = new System.Drawing.Size(336, 32);
            this.workModePanel.TabIndex = 4;
            // 
            // eqResetAndByPassPanel
            // 
            this.eqResetAndByPassPanel.BackColor = System.Drawing.Color.Transparent;
            this.eqResetAndByPassPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.eqResetAndByPassPanel.isPass = false;
            this.eqResetAndByPassPanel.Location = new System.Drawing.Point(390, 275);
            this.eqResetAndByPassPanel.Name = "eqResetAndByPassPanel";
            this.eqResetAndByPassPanel.Size = new System.Drawing.Size(168, 33);
            this.eqResetAndByPassPanel.TabIndex = 3;
            // 
            // eqGroupPanel
            // 
            this.eqGroupPanel.BackColor = System.Drawing.Color.Transparent;
            this.eqGroupPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.eqGroupPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.eqGroupPanel.Location = new System.Drawing.Point(390, 8);
            this.eqGroupPanel.Name = "eqGroupPanel";
            this.eqGroupPanel.Size = new System.Drawing.Size(638, 258);
            this.eqGroupPanel.TabIndex = 0;
            // 
            // panelMusicInput
            // 
            this.panelMusicInput.Controls.Add(this.lblMusicInputPort);
            this.panelMusicInput.Controls.Add(this.cmbMusicInputPort);
            this.panelMusicInput.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.panelMusicInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.panelMusicInput.Location = new System.Drawing.Point(10, 8);
            this.panelMusicInput.Name = "panelMusicInput";
            this.panelMusicInput.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelMusicInput.Radius = 18;
            this.panelMusicInput.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.panelMusicInput.ShowRect = true;
            this.panelMusicInput.Size = new System.Drawing.Size(370, 72);
            this.panelMusicInput.TabIndex = 5;
            this.panelMusicInput.title = "音乐输入";
            this.panelMusicInput.titleHeight = 20;
            // 
            // lblMusicInputPort
            // 
            this.lblMusicInputPort.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMusicInputPort.BackColor = System.Drawing.Color.Transparent;
            this.lblMusicInputPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblMusicInputPort.Location = new System.Drawing.Point(49, 30);
            this.lblMusicInputPort.Name = "lblMusicInputPort";
            this.lblMusicInputPort.Size = new System.Drawing.Size(128, 23);
            this.lblMusicInputPort.TabIndex = 26;
            this.lblMusicInputPort.Text = "音乐输入端口";
            // 
            // cmbMusicInputPort
            // 
            this.cmbMusicInputPort.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbMusicInputPort.BackColor = System.Drawing.SystemColors.Window;
            this.cmbMusicInputPort.DropDownBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(208)))), ((int)(((byte)(7)))));
            this.cmbMusicInputPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMusicInputPort.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbMusicInputPort.ForeColor = System.Drawing.Color.Black;
            this.cmbMusicInputPort.FormattingEnabled = true;
            this.cmbMusicInputPort.Location = new System.Drawing.Point(183, 30);
            this.cmbMusicInputPort.Margin = new System.Windows.Forms.Padding(0);
            this.cmbMusicInputPort.Name = "cmbMusicInputPort";
            this.cmbMusicInputPort.Size = new System.Drawing.Size(92, 23);
            this.cmbMusicInputPort.TabIndex = 27;
            this.cmbMusicInputPort.TabStop = false;
            // 
            // VKxSCMMusicPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.Controls.Add(this.panelBassExciter);
            this.Controls.Add(this.panelHPF);
            this.Controls.Add(this.workModePanel);
            this.Controls.Add(this.eqResetAndByPassPanel);
            this.Controls.Add(this.eqGroupPanel);
            this.Controls.Add(this.panelMusicInput);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "VKxSCMMusicPanel";
            this.Size = new System.Drawing.Size(1276, 312);
            this.panelBassExciter.ResumeLayout(false);
            this.panelHPF.ResumeLayout(false);
            this.panelMusicInput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private EQGroupPanel eqGroupPanel;
        private HSliderParam hpfFreqSlider;
        private EQResetAndByPassPanel eqResetAndByPassPanel;
        private WorkModePanel workModePanel;
        private gtComboBox cmbMusicInputPort;
        private gtLabel lblMusicInputPort;
        private gtPanel panelMusicInput;
        private gtPanel panelHPF;
        private gtPanel panelBassExciter;
        private HSliderParam sliderBassExciterXoverFreq;
        private Controls.MSwitch chkBassExciterBypass;
        private gtLabel lblBassExciterXoverFreq;
        private gtLabel lblBassExciterBoostLevel;
        private HSliderParam sliderBassExciterBoostLevel;
        private gtLabel gtLabel1;
        private HSliderParam sliderBassExciterFBGain;
        private gtLabel lblSuperBassExciterFBGain;
        private gtComboBox cmbHpfType;
        private gtLabel lblHpfType;
        private gtButton btnResetBoostLevel;
        private gtButton btnResetFBGain;
        private gtButton btnResetXoverFreq;
    }
}
