namespace AEC.UI.VX
{
    partial class VXMicPanel
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
            this.panelHPF = new AEC.UI.gtPanel();
            this.hpfFreqSlier = new AEC.UI.HSliderParam();
            this.panelFbe = new AEC.UI.gtPanel();
            this.micFbe = new AEC.UI.MicParam();
            this.eqResetAndByPassPanel = new AEC.UI.EQResetAndByPassPanel();
            this.eqGroupPanel = new AEC.UI.EQGroupPanel();
            this.compressorPanel = new AEC.UI.VX.VXCompressorPanel();
            this.panelHPF.SuspendLayout();
            this.panelFbe.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHPF
            // 
            this.panelHPF.Controls.Add(this.hpfFreqSlier);
            this.panelHPF.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelHPF.ForeColor = System.Drawing.Color.LightGray;
            this.panelHPF.Location = new System.Drawing.Point(10, 86);
            this.panelHPF.Name = "panelHPF";
            this.panelHPF.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelHPF.Radius = 18;
            this.panelHPF.RectColor = System.Drawing.Color.Black;
            this.panelHPF.ShowRect = true;
            this.panelHPF.Size = new System.Drawing.Size(350, 72);
            this.panelHPF.TabIndex = 25;
            this.panelHPF.title = "麦克风高通";
            this.panelHPF.titleHeight = 20;
            // 
            // hpfFreqSlier
            // 
            this.hpfFreqSlier.BackColor = System.Drawing.Color.Transparent;
            this.hpfFreqSlier.backgroundDisabled = null;
            this.hpfFreqSlier.backgroundNormal = global::AEC.Properties.Resources.mic_slider_back;
            this.hpfFreqSlier.backLeftTop = new System.Drawing.Point(0, 6);
            this.hpfFreqSlier.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.hpfFreqSlier.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.hpfFreqSlier.Location = new System.Drawing.Point(67, 34);
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
            this.panelFbe.Location = new System.Drawing.Point(10, 8);
            this.panelFbe.Name = "panelFbe";
            this.panelFbe.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelFbe.Radius = 18;
            this.panelFbe.RectColor = System.Drawing.Color.Black;
            this.panelFbe.ShowRect = true;
            this.panelFbe.Size = new System.Drawing.Size(350, 72);
            this.panelFbe.TabIndex = 24;
            this.panelFbe.title = "麦克风移频";
            this.panelFbe.titleHeight = 20;
            // 
            // micFbe
            // 
            this.micFbe.BackColor = System.Drawing.Color.Transparent;
            this.micFbe.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.micFbe.Location = new System.Drawing.Point(136, 34);
            this.micFbe.Margin = new System.Windows.Forms.Padding(0);
            this.micFbe.Name = "micFbe";
            this.micFbe.Size = new System.Drawing.Size(81, 25);
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
            // compressorPanel
            // 
            this.compressorPanel.BackColor = System.Drawing.Color.Transparent;
            this.compressorPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.compressorPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.compressorPanel.ForeColor = System.Drawing.Color.LightGray;
            this.compressorPanel.Location = new System.Drawing.Point(10, 164);
            this.compressorPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.compressorPanel.Name = "compressorPanel";
            this.compressorPanel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.compressorPanel.Radius = 18;
            this.compressorPanel.RectColor = System.Drawing.Color.Gray;
            this.compressorPanel.Size = new System.Drawing.Size(350, 142);
            this.compressorPanel.sliderback = global::AEC.Properties.Resources.mic_slider_back;
            this.compressorPanel.SliderParamWidth = 234;
            this.compressorPanel.sliderWidth = 158;
            this.compressorPanel.TabIndex = 26;
            this.compressorPanel.ThumbEndX = 140;
            this.compressorPanel.title = null;
            this.compressorPanel.titleHeight = 20;
            // 
            // VXMicPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.compressorPanel);
            this.Controls.Add(this.panelHPF);
            this.Controls.Add(this.panelFbe);
            this.Controls.Add(this.eqResetAndByPassPanel);
            this.Controls.Add(this.eqGroupPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "VXMicPanel";
            this.Size = new System.Drawing.Size(1276, 312);
            this.panelHPF.ResumeLayout(false);
            this.panelFbe.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private EQGroupPanel eqGroupPanel;
        private HSliderParam hpfFreqSlier;
        private MicParam micFbe;
        private EQResetAndByPassPanel eqResetAndByPassPanel;
        private gtPanel panelFbe;
        private gtPanel panelHPF;
        private VXCompressorPanel compressorPanel;
    }
}
