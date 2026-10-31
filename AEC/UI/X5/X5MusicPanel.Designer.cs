namespace AEC.UI.X5
{
    partial class X5MusicPanel
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
            this.panelHPF = new AEC.UI.gtPanel();
            this.lblHPFSlope = new AEC.UI.gtLabel();
            this.sliderHpfFreq = new AEC.UI.HSliderParam();
            this.cmbHpfType = new AEC.UI.gtComboBox();
            this.panelMusicInput = new AEC.UI.gtPanel();
            this.rdoMusicInputSPDIF = new AEC.UI.gtRadioButtonWithText();
            this.rdoMusicInputBT = new AEC.UI.gtRadioButtonWithText();
            this.rdoMusicInputPort2 = new AEC.UI.gtRadioButtonWithText();
            this.rdoMusicInputPort1 = new AEC.UI.gtRadioButtonWithText();
            this.cmbMusicInputGain = new AEC.UI.gtComboBox();
            this.lblMusicInputPort = new AEC.UI.gtLabel();
            this.lblMusicInputGain = new AEC.UI.gtLabel();
            this.eqResetAndByPassPanel = new AEC.UI.EQResetAndByPassPanel();
            this.eqGroupPanel = new AEC.UI.EQGroupPanel();
            this.panelHPF.SuspendLayout();
            this.panelMusicInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHPF
            // 
            this.panelHPF.Controls.Add(this.lblHPFSlope);
            this.panelHPF.Controls.Add(this.sliderHpfFreq);
            this.panelHPF.Controls.Add(this.cmbHpfType);
            this.panelHPF.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelHPF.ForeColor = System.Drawing.Color.LightGray;
            this.panelHPF.Location = new System.Drawing.Point(660, 176);
            this.panelHPF.Name = "panelHPF";
            this.panelHPF.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelHPF.Radius = 18;
            this.panelHPF.RectColor = System.Drawing.Color.Black;
            this.panelHPF.ShowRect = true;
            this.panelHPF.Size = new System.Drawing.Size(350, 130);
            this.panelHPF.TabIndex = 6;
            this.panelHPF.title = "音乐至主输出高通";
            this.panelHPF.titleHeight = 20;
            // 
            // lblHPFSlope
            // 
            this.lblHPFSlope.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblHPFSlope.BackColor = System.Drawing.Color.Transparent;
            this.lblHPFSlope.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblHPFSlope.Location = new System.Drawing.Point(39, 84);
            this.lblHPFSlope.Name = "lblHPFSlope";
            this.lblHPFSlope.Size = new System.Drawing.Size(128, 23);
            this.lblHPFSlope.TabIndex = 28;
            this.lblHPFSlope.Text = "滤波器斜率";
            // 
            // sliderHpfFreq
            // 
            this.sliderHpfFreq.BackColor = System.Drawing.Color.Transparent;
            this.sliderHpfFreq.backgroundDisabled = global::AEC.Properties.Resources.music_slider_back_disabled;
            this.sliderHpfFreq.backgroundNormal = global::AEC.Properties.Resources.music_slider_back;
            this.sliderHpfFreq.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderHpfFreq.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderHpfFreq.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderHpfFreq.Location = new System.Drawing.Point(43, 42);
            this.sliderHpfFreq.Margin = new System.Windows.Forms.Padding(0);
            this.sliderHpfFreq.Name = "sliderHpfFreq";
            this.sliderHpfFreq.Size = new System.Drawing.Size(268, 23);
            this.sliderHpfFreq.SliderWidth = 188;
            this.sliderHpfFreq.TabIndex = 2;
            this.sliderHpfFreq.thumbCenterY = 11;
            this.sliderHpfFreq.ThumbEndX = 176;
            this.sliderHpfFreq.upperLimit = ((short)(100));
            this.sliderHpfFreq.value = ((short)(0));
            // 
            // cmbHpfType
            // 
            this.cmbHpfType.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbHpfType.BackColor = System.Drawing.SystemColors.Window;
            this.cmbHpfType.DropDownBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbHpfType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHpfType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbHpfType.ForeColor = System.Drawing.Color.Black;
            this.cmbHpfType.FormattingEnabled = true;
            this.cmbHpfType.Items.AddRange(new object[] {
            "12 dB",
            "24 dB"});
            this.cmbHpfType.Location = new System.Drawing.Point(173, 84);
            this.cmbHpfType.Margin = new System.Windows.Forms.Padding(0);
            this.cmbHpfType.Name = "cmbHpfType";
            this.cmbHpfType.Size = new System.Drawing.Size(92, 23);
            this.cmbHpfType.TabIndex = 29;
            this.cmbHpfType.TabStop = false;
            // 
            // panelMusicInput
            // 
            this.panelMusicInput.Controls.Add(this.rdoMusicInputSPDIF);
            this.panelMusicInput.Controls.Add(this.rdoMusicInputBT);
            this.panelMusicInput.Controls.Add(this.rdoMusicInputPort2);
            this.panelMusicInput.Controls.Add(this.rdoMusicInputPort1);
            this.panelMusicInput.Controls.Add(this.cmbMusicInputGain);
            this.panelMusicInput.Controls.Add(this.lblMusicInputPort);
            this.panelMusicInput.Controls.Add(this.lblMusicInputGain);
            this.panelMusicInput.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelMusicInput.ForeColor = System.Drawing.Color.LightGray;
            this.panelMusicInput.Location = new System.Drawing.Point(660, 8);
            this.panelMusicInput.Name = "panelMusicInput";
            this.panelMusicInput.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelMusicInput.Radius = 18;
            this.panelMusicInput.RectColor = System.Drawing.Color.Black;
            this.panelMusicInput.ShowRect = true;
            this.panelMusicInput.Size = new System.Drawing.Size(350, 158);
            this.panelMusicInput.TabIndex = 5;
            this.panelMusicInput.title = "音乐输入";
            this.panelMusicInput.titleHeight = 20;
            // 
            // rdoMusicInputSPDIF
            // 
            this.rdoMusicInputSPDIF.backChecked = global::AEC.Properties.Resources.music_rdo_checked;
            this.rdoMusicInputSPDIF.BackColor = System.Drawing.Color.Transparent;
            this.rdoMusicInputSPDIF.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoMusicInputSPDIF.backHover = global::AEC.Properties.Resources.music_rdo_hover;
            this.rdoMusicInputSPDIF.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoMusicInputSPDIF.isAutoCheck = true;
            this.rdoMusicInputSPDIF.isChecked = false;
            this.rdoMusicInputSPDIF.isCheckInit = false;
            this.rdoMusicInputSPDIF.isShowCheckedBackWhenDsiabled = true;
            this.rdoMusicInputSPDIF.isStretch = false;
            this.rdoMusicInputSPDIF.Location = new System.Drawing.Point(236, 77);
            this.rdoMusicInputSPDIF.Name = "rdoMusicInputSPDIF";
            this.rdoMusicInputSPDIF.Size = new System.Drawing.Size(96, 30);
            this.rdoMusicInputSPDIF.TabIndex = 32;
            this.rdoMusicInputSPDIF.Text = "SPDIF";
            this.rdoMusicInputSPDIF.userGDIPaint = true;
            // 
            // rdoMusicInputBT
            // 
            this.rdoMusicInputBT.backChecked = global::AEC.Properties.Resources.music_rdo_checked;
            this.rdoMusicInputBT.BackColor = System.Drawing.Color.Transparent;
            this.rdoMusicInputBT.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoMusicInputBT.backHover = global::AEC.Properties.Resources.music_rdo_hover;
            this.rdoMusicInputBT.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoMusicInputBT.isAutoCheck = true;
            this.rdoMusicInputBT.isChecked = false;
            this.rdoMusicInputBT.isCheckInit = false;
            this.rdoMusicInputBT.isShowCheckedBackWhenDsiabled = true;
            this.rdoMusicInputBT.isStretch = false;
            this.rdoMusicInputBT.Location = new System.Drawing.Point(165, 77);
            this.rdoMusicInputBT.Name = "rdoMusicInputBT";
            this.rdoMusicInputBT.Size = new System.Drawing.Size(64, 30);
            this.rdoMusicInputBT.TabIndex = 31;
            this.rdoMusicInputBT.Text = "BT";
            this.rdoMusicInputBT.userGDIPaint = true;
            // 
            // rdoMusicInputPort2
            // 
            this.rdoMusicInputPort2.backChecked = global::AEC.Properties.Resources.music_rdo_checked;
            this.rdoMusicInputPort2.BackColor = System.Drawing.Color.Transparent;
            this.rdoMusicInputPort2.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoMusicInputPort2.backHover = global::AEC.Properties.Resources.music_rdo_hover;
            this.rdoMusicInputPort2.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoMusicInputPort2.isAutoCheck = true;
            this.rdoMusicInputPort2.isChecked = false;
            this.rdoMusicInputPort2.isCheckInit = false;
            this.rdoMusicInputPort2.isShowCheckedBackWhenDsiabled = true;
            this.rdoMusicInputPort2.isStretch = false;
            this.rdoMusicInputPort2.Location = new System.Drawing.Point(236, 43);
            this.rdoMusicInputPort2.Name = "rdoMusicInputPort2";
            this.rdoMusicInputPort2.Size = new System.Drawing.Size(96, 30);
            this.rdoMusicInputPort2.TabIndex = 30;
            this.rdoMusicInputPort2.Text = "2";
            this.rdoMusicInputPort2.userGDIPaint = true;
            // 
            // rdoMusicInputPort1
            // 
            this.rdoMusicInputPort1.backChecked = global::AEC.Properties.Resources.music_rdo_checked;
            this.rdoMusicInputPort1.BackColor = System.Drawing.Color.Transparent;
            this.rdoMusicInputPort1.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoMusicInputPort1.backHover = global::AEC.Properties.Resources.music_rdo_hover;
            this.rdoMusicInputPort1.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoMusicInputPort1.isAutoCheck = true;
            this.rdoMusicInputPort1.isChecked = false;
            this.rdoMusicInputPort1.isCheckInit = false;
            this.rdoMusicInputPort1.isShowCheckedBackWhenDsiabled = true;
            this.rdoMusicInputPort1.isStretch = false;
            this.rdoMusicInputPort1.Location = new System.Drawing.Point(165, 43);
            this.rdoMusicInputPort1.Name = "rdoMusicInputPort1";
            this.rdoMusicInputPort1.Size = new System.Drawing.Size(64, 30);
            this.rdoMusicInputPort1.TabIndex = 29;
            this.rdoMusicInputPort1.Text = "1";
            this.rdoMusicInputPort1.userGDIPaint = true;
            // 
            // cmbMusicInputGain
            // 
            this.cmbMusicInputGain.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbMusicInputGain.DropDownBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbMusicInputGain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMusicInputGain.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbMusicInputGain.ForeColor = System.Drawing.Color.Black;
            this.cmbMusicInputGain.FormattingEnabled = true;
            this.cmbMusicInputGain.Items.AddRange(new object[] {
            "0 dB",
            "3 dB",
            "6 dB"});
            this.cmbMusicInputGain.Location = new System.Drawing.Point(165, 115);
            this.cmbMusicInputGain.Margin = new System.Windows.Forms.Padding(0);
            this.cmbMusicInputGain.Name = "cmbMusicInputGain";
            this.cmbMusicInputGain.Size = new System.Drawing.Size(92, 23);
            this.cmbMusicInputGain.TabIndex = 27;
            this.cmbMusicInputGain.TabStop = false;
            // 
            // lblMusicInputPort
            // 
            this.lblMusicInputPort.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMusicInputPort.BackColor = System.Drawing.Color.Transparent;
            this.lblMusicInputPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMusicInputPort.Location = new System.Drawing.Point(31, 48);
            this.lblMusicInputPort.Name = "lblMusicInputPort";
            this.lblMusicInputPort.Size = new System.Drawing.Size(128, 25);
            this.lblMusicInputPort.TabIndex = 26;
            this.lblMusicInputPort.Text = "音乐输入端口";
            // 
            // lblMusicInputGain
            // 
            this.lblMusicInputGain.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMusicInputGain.BackColor = System.Drawing.Color.Transparent;
            this.lblMusicInputGain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMusicInputGain.Location = new System.Drawing.Point(10, 115);
            this.lblMusicInputGain.Name = "lblMusicInputGain";
            this.lblMusicInputGain.Size = new System.Drawing.Size(149, 23);
            this.lblMusicInputGain.TabIndex = 26;
            this.lblMusicInputGain.Text = "音乐输入增益";
            // 
            // eqResetAndByPassPanel
            // 
            this.eqResetAndByPassPanel.BackColor = System.Drawing.Color.Transparent;
            this.eqResetAndByPassPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.eqResetAndByPassPanel.isPass = false;
            this.eqResetAndByPassPanel.Location = new System.Drawing.Point(10, 275);
            this.eqResetAndByPassPanel.Name = "eqResetAndByPassPanel";
            this.eqResetAndByPassPanel.Size = new System.Drawing.Size(168, 33);
            this.eqResetAndByPassPanel.TabIndex = 3;
            // 
            // eqGroupPanel
            // 
            this.eqGroupPanel.BackColor = System.Drawing.Color.Transparent;
            this.eqGroupPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.eqGroupPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.eqGroupPanel.Location = new System.Drawing.Point(10, 8);
            this.eqGroupPanel.Name = "eqGroupPanel";
            this.eqGroupPanel.Size = new System.Drawing.Size(638, 258);
            this.eqGroupPanel.TabIndex = 0;
            // 
            // X5MusicPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelHPF);
            this.Controls.Add(this.panelMusicInput);
            this.Controls.Add(this.eqResetAndByPassPanel);
            this.Controls.Add(this.eqGroupPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "X5MusicPanel";
            this.Size = new System.Drawing.Size(1276, 312);
            this.panelHPF.ResumeLayout(false);
            this.panelMusicInput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private EQGroupPanel eqGroupPanel;
        private HSliderParam sliderHpfFreq;
        private EQResetAndByPassPanel eqResetAndByPassPanel;
        private gtComboBox cmbMusicInputGain;
        private gtLabel lblMusicInputGain;
        private gtLabel lblMusicInputPort;
        private gtPanel panelMusicInput;
        private gtPanel panelHPF;
        private gtLabel lblHPFSlope;
        private gtComboBox cmbHpfType;
        private gtRadioButtonWithText rdoMusicInputPort2;
        private gtRadioButtonWithText rdoMusicInputPort1;
        private gtRadioButtonWithText rdoMusicInputSPDIF;
        private gtRadioButtonWithText rdoMusicInputBT;
    }
}
