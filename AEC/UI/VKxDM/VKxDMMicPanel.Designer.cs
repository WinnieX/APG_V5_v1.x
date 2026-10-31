namespace AEC.UI.VKxDM
{
    partial class VKxDMMicPanel
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
            this.compressorPanel = new AEC.UI.VKxDM.VKxDMCompressorPanel();
            this.panelXPF = new AEC.UI.gtPanel();
            this.lblEffectLevel = new AEC.UI.gtLabel();
            this.sliderEffectLevel = new AEC.UI.HSliderParam();
            this.lblMicLevel = new AEC.UI.gtLabel();
            this.sliderMicLevel = new AEC.UI.HSliderParam();
            this.lblMicHpfFreq = new AEC.UI.gtLabel();
            this.hpfFreqSlider = new AEC.UI.HSliderParam();
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
            this.panelXPF.Controls.Add(this.lblEffectLevel);
            this.panelXPF.Controls.Add(this.sliderEffectLevel);
            this.panelXPF.Controls.Add(this.lblMicLevel);
            this.panelXPF.Controls.Add(this.sliderMicLevel);
            this.panelXPF.Controls.Add(this.lblMicHpfFreq);
            this.panelXPF.Controls.Add(this.hpfFreqSlider);
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
            // lblEffectLevel
            // 
            this.lblEffectLevel.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEffectLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblEffectLevel.BackColor = System.Drawing.Color.Transparent;
            this.lblEffectLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEffectLevel.Location = new System.Drawing.Point(20, 38);
            this.lblEffectLevel.Name = "lblEffectLevel";
            this.lblEffectLevel.Size = new System.Drawing.Size(80, 23);
            this.lblEffectLevel.TabIndex = 31;
            this.lblEffectLevel.Text = "效果电平";
            // 
            // sliderEffectLevel
            // 
            this.sliderEffectLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sliderEffectLevel.BackColor = System.Drawing.Color.Transparent;
            this.sliderEffectLevel.backgroundDisabled = null;
            this.sliderEffectLevel.backgroundNormal = global::AEC.Properties.Resources.mic_slider_back;
            this.sliderEffectLevel.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderEffectLevel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderEffectLevel.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderEffectLevel.Location = new System.Drawing.Point(104, 38);
            this.sliderEffectLevel.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEffectLevel.Name = "sliderEffectLevel";
            this.sliderEffectLevel.Size = new System.Drawing.Size(232, 23);
            this.sliderEffectLevel.SliderWidth = 152;
            this.sliderEffectLevel.TabIndex = 30;
            this.sliderEffectLevel.thumbCenterY = 11;
            this.sliderEffectLevel.ThumbEndX = 140;
            this.sliderEffectLevel.upperLimit = ((short)(100));
            this.sliderEffectLevel.value = ((short)(0));
            // 
            // lblMicLevel
            // 
            this.lblMicLevel.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMicLevel.BackColor = System.Drawing.Color.Transparent;
            this.lblMicLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMicLevel.Location = new System.Drawing.Point(20, 9);
            this.lblMicLevel.Name = "lblMicLevel";
            this.lblMicLevel.Size = new System.Drawing.Size(80, 23);
            this.lblMicLevel.TabIndex = 29;
            this.lblMicLevel.Text = "麦克风电平";
            // 
            // sliderMicLevel
            // 
            this.sliderMicLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sliderMicLevel.BackColor = System.Drawing.Color.Transparent;
            this.sliderMicLevel.backgroundDisabled = null;
            this.sliderMicLevel.backgroundNormal = global::AEC.Properties.Resources.mic_slider_back;
            this.sliderMicLevel.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderMicLevel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderMicLevel.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderMicLevel.Location = new System.Drawing.Point(104, 9);
            this.sliderMicLevel.Margin = new System.Windows.Forms.Padding(0);
            this.sliderMicLevel.Name = "sliderMicLevel";
            this.sliderMicLevel.Size = new System.Drawing.Size(232, 23);
            this.sliderMicLevel.SliderWidth = 152;
            this.sliderMicLevel.TabIndex = 28;
            this.sliderMicLevel.thumbCenterY = 11;
            this.sliderMicLevel.ThumbEndX = 140;
            this.sliderMicLevel.upperLimit = ((short)(100));
            this.sliderMicLevel.value = ((short)(0));
            // 
            // lblMicHpfFreq
            // 
            this.lblMicHpfFreq.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicHpfFreq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMicHpfFreq.BackColor = System.Drawing.Color.Transparent;
            this.lblMicHpfFreq.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMicHpfFreq.Location = new System.Drawing.Point(20, 67);
            this.lblMicHpfFreq.Name = "lblMicHpfFreq";
            this.lblMicHpfFreq.Size = new System.Drawing.Size(80, 23);
            this.lblMicHpfFreq.TabIndex = 27;
            this.lblMicHpfFreq.Text = "麦克风高通";
            // 
            // hpfFreqSlider
            // 
            this.hpfFreqSlider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hpfFreqSlider.BackColor = System.Drawing.Color.Transparent;
            this.hpfFreqSlider.backgroundDisabled = null;
            this.hpfFreqSlider.backgroundNormal = global::AEC.Properties.Resources.mic_slider_back;
            this.hpfFreqSlider.backLeftTop = new System.Drawing.Point(0, 6);
            this.hpfFreqSlider.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.hpfFreqSlider.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.hpfFreqSlider.Location = new System.Drawing.Point(104, 67);
            this.hpfFreqSlider.Margin = new System.Windows.Forms.Padding(0);
            this.hpfFreqSlider.Name = "hpfFreqSlider";
            this.hpfFreqSlider.Size = new System.Drawing.Size(232, 23);
            this.hpfFreqSlider.SliderWidth = 152;
            this.hpfFreqSlider.TabIndex = 3;
            this.hpfFreqSlider.thumbCenterY = 11;
            this.hpfFreqSlider.ThumbEndX = 140;
            this.hpfFreqSlider.upperLimit = ((short)(100));
            this.hpfFreqSlider.value = ((short)(0));
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
            // VKxDMMicPanel
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
            this.Name = "VKxDMMicPanel";
            this.Size = new System.Drawing.Size(1276, 312);
            this.panelXPF.ResumeLayout(false);
            this.panelFbe.ResumeLayout(false);
            this.panelNoiseGate.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private EQGroupPanel eqGroupPanel;
        private HSliderParam hpfFreqSlider;
        private MicParam micFbe;
        private EQResetAndByPassPanel eqResetAndByPassPanel;
        private gtPanel panelFbe;
        private gtPanel panelXPF;
        private VKxDMCompressorPanel compressorPanel;
        private gtLabel lblMicHpfFreq;
        private gtPanel panelNoiseGate;
        private MicParam micNoiseGate;
        private gtLabel lblEffectLevel;
        private HSliderParam sliderEffectLevel;
        private gtLabel lblMicLevel;
        private HSliderParam sliderMicLevel;
    }
}
