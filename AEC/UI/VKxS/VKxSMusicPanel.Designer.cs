namespace AEC.UI.VKxS
{
    partial class VKxSMusicPanel
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
            this.cmbHpfType = new AEC.UI.gtComboBox();
            this.sliderHpfFreq = new AEC.UI.HSliderParam();
            this.panelMusicInput = new AEC.UI.gtPanel();
            this.cmbMusicInputGain = new AEC.UI.gtComboBox();
            this.lblMusicInputPort = new AEC.UI.gtLabel();
            this.lblMusicInputGain = new AEC.UI.gtLabel();
            this.cmbMusicInputPort = new AEC.UI.gtComboBox();
            this.eqResetAndByPassPanel = new AEC.UI.EQResetAndByPassPanel();
            this.eqGroupPanel = new AEC.UI.EQGroupPanel();
            this.panelHPF.SuspendLayout();
            this.panelMusicInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHPF
            // 
            this.panelHPF.Controls.Add(this.lblHPFSlope);
            this.panelHPF.Controls.Add(this.cmbHpfType);
            this.panelHPF.Controls.Add(this.sliderHpfFreq);
            this.panelHPF.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelHPF.ForeColor = System.Drawing.Color.LightGray;
            this.panelHPF.Location = new System.Drawing.Point(10, 176);
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
            this.lblHPFSlope.Location = new System.Drawing.Point(39, 82);
            this.lblHPFSlope.Name = "lblHPFSlope";
            this.lblHPFSlope.Size = new System.Drawing.Size(128, 25);
            this.lblHPFSlope.TabIndex = 30;
            this.lblHPFSlope.Text = "滤波器斜率";
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
            this.cmbHpfType.Location = new System.Drawing.Point(173, 82);
            this.cmbHpfType.Margin = new System.Windows.Forms.Padding(0);
            this.cmbHpfType.Name = "cmbHpfType";
            this.cmbHpfType.Size = new System.Drawing.Size(92, 23);
            this.cmbHpfType.TabIndex = 31;
            this.cmbHpfType.TabStop = false;
            // 
            // sliderHpfFreq
            // 
            this.sliderHpfFreq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.sliderHpfFreq.backgroundDisabled = global::AEC.Properties.Resources.music_slider_back_disabled;
            this.sliderHpfFreq.backgroundNormal = global::AEC.Properties.Resources.music_slider_back;
            this.sliderHpfFreq.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderHpfFreq.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderHpfFreq.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderHpfFreq.Location = new System.Drawing.Point(43, 41);
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
            // panelMusicInput
            // 
            this.panelMusicInput.BackColor = System.Drawing.Color.Transparent;
            this.panelMusicInput.Controls.Add(this.cmbMusicInputGain);
            this.panelMusicInput.Controls.Add(this.lblMusicInputPort);
            this.panelMusicInput.Controls.Add(this.lblMusicInputGain);
            this.panelMusicInput.Controls.Add(this.cmbMusicInputPort);
            this.panelMusicInput.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelMusicInput.ForeColor = System.Drawing.Color.LightGray;
            this.panelMusicInput.Location = new System.Drawing.Point(10, 8);
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
            // cmbMusicInputGain
            // 
            this.cmbMusicInputGain.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbMusicInputGain.DropDownBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbMusicInputGain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMusicInputGain.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbMusicInputGain.ForeColor = System.Drawing.Color.Black;
            this.cmbMusicInputGain.FormattingEnabled = true;
            this.cmbMusicInputGain.Items.AddRange(new object[] {
            "-6 dB",
            "-3 dB"});
            this.cmbMusicInputGain.Location = new System.Drawing.Point(173, 102);
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
            this.lblMusicInputPort.Location = new System.Drawing.Point(39, 58);
            this.lblMusicInputPort.Name = "lblMusicInputPort";
            this.lblMusicInputPort.Size = new System.Drawing.Size(128, 23);
            this.lblMusicInputPort.TabIndex = 26;
            this.lblMusicInputPort.Text = "音乐输入端口";
            // 
            // lblMusicInputGain
            // 
            this.lblMusicInputGain.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMusicInputGain.BackColor = System.Drawing.Color.Transparent;
            this.lblMusicInputGain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMusicInputGain.Location = new System.Drawing.Point(18, 102);
            this.lblMusicInputGain.Name = "lblMusicInputGain";
            this.lblMusicInputGain.Size = new System.Drawing.Size(149, 23);
            this.lblMusicInputGain.TabIndex = 26;
            this.lblMusicInputGain.Text = "音乐输入增益";
            this.lblMusicInputGain.Visible = false;
            // 
            // cmbMusicInputPort
            // 
            this.cmbMusicInputPort.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbMusicInputPort.BackColor = System.Drawing.SystemColors.Window;
            this.cmbMusicInputPort.DropDownBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbMusicInputPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMusicInputPort.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbMusicInputPort.ForeColor = System.Drawing.Color.Black;
            this.cmbMusicInputPort.FormattingEnabled = true;
            this.cmbMusicInputPort.Location = new System.Drawing.Point(173, 58);
            this.cmbMusicInputPort.Margin = new System.Windows.Forms.Padding(0);
            this.cmbMusicInputPort.Name = "cmbMusicInputPort";
            this.cmbMusicInputPort.Size = new System.Drawing.Size(92, 23);
            this.cmbMusicInputPort.TabIndex = 27;
            this.cmbMusicInputPort.TabStop = false;
            // 
            // eqResetAndByPassPanel
            // 
            this.eqResetAndByPassPanel.BackColor = System.Drawing.Color.Transparent;
            this.eqResetAndByPassPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.eqResetAndByPassPanel.isPass = false;
            this.eqResetAndByPassPanel.Location = new System.Drawing.Point(370, 275);
            this.eqResetAndByPassPanel.Name = "eqResetAndByPassPanel";
            this.eqResetAndByPassPanel.Size = new System.Drawing.Size(168, 33);
            this.eqResetAndByPassPanel.TabIndex = 3;
            // 
            // eqGroupPanel
            // 
            this.eqGroupPanel.BackColor = System.Drawing.Color.Transparent;
            this.eqGroupPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.eqGroupPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.eqGroupPanel.Location = new System.Drawing.Point(370, 8);
            this.eqGroupPanel.Name = "eqGroupPanel";
            this.eqGroupPanel.Size = new System.Drawing.Size(638, 258);
            this.eqGroupPanel.TabIndex = 0;
            // 
            // VKxSMusicPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelHPF);
            this.Controls.Add(this.panelMusicInput);
            this.Controls.Add(this.eqResetAndByPassPanel);
            this.Controls.Add(this.eqGroupPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "VKxSMusicPanel";
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
        private gtComboBox cmbMusicInputPort;
        private gtLabel lblMusicInputPort;
        private gtPanel panelMusicInput;
        private gtPanel panelHPF;
        private gtLabel lblHPFSlope;
        private gtComboBox cmbHpfType;
    }
}
