namespace AEC.UI.VKxPII
{
    partial class VKxPIIMicPanel
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
            this.compressorPanel = new AEC.UI.VKxPII.VKxPIICompressorPanel();
            this.panelXPF = new AEC.UI.gtPanel();
            this.cmbLpfType = new AEC.UI.gtComboBox();
            this.lblMicLpfType = new AEC.UI.gtLabel();
            this.lblMicLpfFreq = new AEC.UI.gtLabel();
            this.lpfFreqSlier = new AEC.UI.HSliderParam();
            this.lblMicHpfFreq = new AEC.UI.gtLabel();
            this.hpfFreqSlier = new AEC.UI.HSliderParam();
            this.panelFbe = new AEC.UI.gtPanel();
            this.micFbe = new AEC.UI.MicParam();
            this.eqResetAndByPassPanel = new AEC.UI.EQResetAndByPassPanel();
            this.eqGroupPanel = new AEC.UI.EQGroupPanel();
            this.panelNoiseGate = new AEC.UI.gtPanel();
            this.micNoiseGate = new AEC.UI.MicParam();
            this.panelXPF.SuspendLayout();
            this.panelFbe.SuspendLayout();
            this.panelNoiseGate.SuspendLayout();
            this.SuspendLayout();
            // 
            // compressorPanel
            // 
            this.compressorPanel.BackColor = System.Drawing.Color.Transparent;
            this.compressorPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.compressorPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.compressorPanel.ForeColor = System.Drawing.Color.LightGray;
            this.compressorPanel.Location = new System.Drawing.Point(10, 184);
            this.compressorPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.compressorPanel.Name = "compressorPanel";
            this.compressorPanel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.compressorPanel.Radius = 18;
            this.compressorPanel.RectColor = System.Drawing.Color.Gray;
            this.compressorPanel.Size = new System.Drawing.Size(350, 122);
            this.compressorPanel.sliderback = global::AEC.Properties.Resources.mic_slider_back;
            this.compressorPanel.SliderParamWidth = 234;
            this.compressorPanel.sliderWidth = 158;
            this.compressorPanel.TabIndex = 26;
            this.compressorPanel.ThumbEndX = 140;
            this.compressorPanel.title = null;
            this.compressorPanel.titleHeight = 20;
            // 
            // panelXPF
            // 
            this.panelXPF.Controls.Add(this.cmbLpfType);
            this.panelXPF.Controls.Add(this.lblMicLpfType);
            this.panelXPF.Controls.Add(this.lblMicLpfFreq);
            this.panelXPF.Controls.Add(this.lpfFreqSlier);
            this.panelXPF.Controls.Add(this.lblMicHpfFreq);
            this.panelXPF.Controls.Add(this.hpfFreqSlier);
            this.panelXPF.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelXPF.ForeColor = System.Drawing.Color.LightGray;
            this.panelXPF.Location = new System.Drawing.Point(10, 78);
            this.panelXPF.Name = "panelXPF";
            this.panelXPF.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelXPF.Radius = 18;
            this.panelXPF.RectColor = System.Drawing.Color.Black;
            this.panelXPF.ShowRect = true;
            this.panelXPF.Size = new System.Drawing.Size(350, 100);
            this.panelXPF.TabIndex = 25;
            this.panelXPF.title = "";
            this.panelXPF.titleHeight = 20;
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
            this.cmbLpfType.Location = new System.Drawing.Point(103, 64);
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
            this.lblMicLpfType.Location = new System.Drawing.Point(20, 64);
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
            this.lblMicLpfFreq.Location = new System.Drawing.Point(20, 37);
            this.lblMicLpfFreq.Name = "lblMicLpfFreq";
            this.lblMicLpfFreq.Size = new System.Drawing.Size(80, 25);
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
            this.lpfFreqSlier.Location = new System.Drawing.Point(104, 36);
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
            this.lblMicHpfFreq.Location = new System.Drawing.Point(20, 8);
            this.lblMicHpfFreq.Name = "lblMicHpfFreq";
            this.lblMicHpfFreq.Size = new System.Drawing.Size(80, 25);
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
            this.hpfFreqSlier.Location = new System.Drawing.Point(104, 9);
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
            // panelFbe
            // 
            this.panelFbe.BackColor = System.Drawing.Color.Transparent;
            this.panelFbe.Controls.Add(this.micFbe);
            this.panelFbe.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelFbe.ForeColor = System.Drawing.Color.LightGray;
            this.panelFbe.Location = new System.Drawing.Point(192, 8);
            this.panelFbe.Name = "panelFbe";
            this.panelFbe.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.panelFbe.Radius = 18;
            this.panelFbe.RectColor = System.Drawing.Color.Black;
            this.panelFbe.ShowRect = true;
            this.panelFbe.Size = new System.Drawing.Size(168, 64);
            this.panelFbe.TabIndex = 24;
            this.panelFbe.title = "麦克风移频";
            this.panelFbe.titleHeight = 20;
            // 
            // micFbe
            // 
            this.micFbe.BackColor = System.Drawing.Color.Transparent;
            this.micFbe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.micFbe.Location = new System.Drawing.Point(36, 28);
            this.micFbe.Margin = new System.Windows.Forms.Padding(0);
            this.micFbe.Name = "micFbe";
            this.micFbe.Size = new System.Drawing.Size(96, 25);
            this.micFbe.TabIndex = 4;
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
            // panelNoiseGate
            // 
            this.panelNoiseGate.BackColor = System.Drawing.Color.Transparent;
            this.panelNoiseGate.Controls.Add(this.micNoiseGate);
            this.panelNoiseGate.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelNoiseGate.ForeColor = System.Drawing.Color.LightGray;
            this.panelNoiseGate.Location = new System.Drawing.Point(10, 8);
            this.panelNoiseGate.Name = "panelNoiseGate";
            this.panelNoiseGate.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.panelNoiseGate.Radius = 18;
            this.panelNoiseGate.RectColor = System.Drawing.Color.Black;
            this.panelNoiseGate.ShowRect = true;
            this.panelNoiseGate.Size = new System.Drawing.Size(168, 64);
            this.panelNoiseGate.TabIndex = 29;
            this.panelNoiseGate.title = "麦克风噪声门";
            this.panelNoiseGate.titleHeight = 20;
            // 
            // micNoiseGate
            // 
            this.micNoiseGate.BackColor = System.Drawing.Color.Transparent;
            this.micNoiseGate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.micNoiseGate.Location = new System.Drawing.Point(36, 28);
            this.micNoiseGate.Margin = new System.Windows.Forms.Padding(0);
            this.micNoiseGate.Name = "micNoiseGate";
            this.micNoiseGate.Size = new System.Drawing.Size(96, 25);
            this.micNoiseGate.TabIndex = 5;
            // 
            // VKxPIIMicPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelNoiseGate);
            this.Controls.Add(this.compressorPanel);
            this.Controls.Add(this.panelXPF);
            this.Controls.Add(this.panelFbe);
            this.Controls.Add(this.eqResetAndByPassPanel);
            this.Controls.Add(this.eqGroupPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "VKxPIIMicPanel";
            this.Size = new System.Drawing.Size(1276, 312);
            this.panelXPF.ResumeLayout(false);
            this.panelFbe.ResumeLayout(false);
            this.panelNoiseGate.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private EQGroupPanel eqGroupPanel;
        private HSliderParam hpfFreqSlier;
        private MicParam micFbe;
        private EQResetAndByPassPanel eqResetAndByPassPanel;
        private gtPanel panelFbe;
        private gtPanel panelXPF;
        private VKxPIICompressorPanel compressorPanel;
        private gtLabel lblMicLpfFreq;
        private HSliderParam lpfFreqSlier;
        private gtLabel lblMicHpfFreq;
        private gtLabel lblMicLpfType;
        private gtComboBox cmbLpfType;
        private gtPanel panelNoiseGate;
        private MicParam micNoiseGate;
    }
}
