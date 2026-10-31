namespace AEC.UI.VKxPIIDCM
{
    partial class VKxPIIDCMReverbPanel
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
            this.gtPanel1 = new AEC.UI.gtPanel();
            this.sliderMicReverb = new AEC.UI.SignalMixerPanel();
            this.lblMicReverb = new AEC.UI.gtLabel();
            this.lblMicReverbDirect = new AEC.UI.gtLabel();
            this.lblReverbMicHpf = new AEC.UI.gtLabel();
            this.sliderMicReverbDirect = new AEC.UI.SignalMixerPanel();
            this.sliderReverbMicHpf = new AEC.UI.HSliderParam();
            this.lblReverbMicLpf = new AEC.UI.gtLabel();
            this.sliderReverbMicLpf = new AEC.UI.HSliderParam();
            this.lblReverbPreDelay = new AEC.UI.gtLabel();
            this.sliderReverbPreDelay = new AEC.UI.HSliderParam();
            this.lblReverbDelay = new AEC.UI.gtLabel();
            this.sliderReverbTime = new AEC.UI.HSliderParam();
            this.eqResetAndByPassPanel = new AEC.UI.EQResetAndByPassPanel();
            this.eqGroupPanel = new AEC.UI.EQGroupPanel();
            this.gtPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gtPanel1
            // 
            this.gtPanel1.Controls.Add(this.sliderMicReverb);
            this.gtPanel1.Controls.Add(this.lblMicReverb);
            this.gtPanel1.Controls.Add(this.lblMicReverbDirect);
            this.gtPanel1.Controls.Add(this.lblReverbMicHpf);
            this.gtPanel1.Controls.Add(this.sliderMicReverbDirect);
            this.gtPanel1.Controls.Add(this.sliderReverbMicHpf);
            this.gtPanel1.Controls.Add(this.lblReverbMicLpf);
            this.gtPanel1.Controls.Add(this.sliderReverbMicLpf);
            this.gtPanel1.Controls.Add(this.lblReverbPreDelay);
            this.gtPanel1.Controls.Add(this.sliderReverbPreDelay);
            this.gtPanel1.Controls.Add(this.lblReverbDelay);
            this.gtPanel1.Controls.Add(this.sliderReverbTime);
            this.gtPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.gtPanel1.ForeColor = System.Drawing.Color.LightGray;
            this.gtPanel1.Location = new System.Drawing.Point(10, 8);
            this.gtPanel1.Name = "gtPanel1";
            this.gtPanel1.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.gtPanel1.Radius = 18;
            this.gtPanel1.RectColor = System.Drawing.Color.Black;
            this.gtPanel1.ShowRect = true;
            this.gtPanel1.Size = new System.Drawing.Size(387, 298);
            this.gtPanel1.TabIndex = 24;
            this.gtPanel1.title = null;
            this.gtPanel1.titleHeight = 20;
            // 
            // sliderMicReverb
            // 
            this.sliderMicReverb.BackColor = System.Drawing.Color.Transparent;
            this.sliderMicReverb.backgroundDisabled = null;
            this.sliderMicReverb.backgroundNormal = global::AEC.Properties.Resources.reverb_slider_back;
            this.sliderMicReverb.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderMicReverb.FillColor = System.Drawing.Color.Transparent;
            this.sliderMicReverb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderMicReverb.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderMicReverb.Location = new System.Drawing.Point(148, 12);
            this.sliderMicReverb.Name = "sliderMicReverb";
            this.sliderMicReverb.Size = new System.Drawing.Size(232, 23);
            this.sliderMicReverb.SliderWidth = 142;
            this.sliderMicReverb.TabIndex = 21;
            this.sliderMicReverb.thumbCenterY = 11;
            this.sliderMicReverb.ThumbEndX = 124;
            // 
            // lblMicReverb
            // 
            this.lblMicReverb.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicReverb.BackColor = System.Drawing.Color.Transparent;
            this.lblMicReverb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMicReverb.Location = new System.Drawing.Point(7, 12);
            this.lblMicReverb.Name = "lblMicReverb";
            this.lblMicReverb.Size = new System.Drawing.Size(137, 23);
            this.lblMicReverb.TabIndex = 19;
            this.lblMicReverb.Text = "MIC混响效果电平";
            // 
            // lblMicReverbDirect
            // 
            this.lblMicReverbDirect.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicReverbDirect.BackColor = System.Drawing.Color.Transparent;
            this.lblMicReverbDirect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMicReverbDirect.Location = new System.Drawing.Point(7, 42);
            this.lblMicReverbDirect.Name = "lblMicReverbDirect";
            this.lblMicReverbDirect.Size = new System.Drawing.Size(137, 23);
            this.lblMicReverbDirect.TabIndex = 19;
            this.lblMicReverbDirect.Text = "MIC混响直达电平";
            // 
            // lblReverbMicHpf
            // 
            this.lblReverbMicHpf.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblReverbMicHpf.BackColor = System.Drawing.Color.Transparent;
            this.lblReverbMicHpf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblReverbMicHpf.Location = new System.Drawing.Point(7, 72);
            this.lblReverbMicHpf.Name = "lblReverbMicHpf";
            this.lblReverbMicHpf.Size = new System.Drawing.Size(137, 23);
            this.lblReverbMicHpf.TabIndex = 19;
            this.lblReverbMicHpf.Text = "混响麦克风高通";
            // 
            // sliderMicReverbDirect
            // 
            this.sliderMicReverbDirect.BackColor = System.Drawing.Color.Transparent;
            this.sliderMicReverbDirect.backgroundDisabled = null;
            this.sliderMicReverbDirect.backgroundNormal = global::AEC.Properties.Resources.reverb_slider_back;
            this.sliderMicReverbDirect.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderMicReverbDirect.FillColor = System.Drawing.Color.Transparent;
            this.sliderMicReverbDirect.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderMicReverbDirect.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderMicReverbDirect.Location = new System.Drawing.Point(148, 42);
            this.sliderMicReverbDirect.Name = "sliderMicReverbDirect";
            this.sliderMicReverbDirect.Size = new System.Drawing.Size(232, 23);
            this.sliderMicReverbDirect.SliderWidth = 142;
            this.sliderMicReverbDirect.TabIndex = 21;
            this.sliderMicReverbDirect.thumbCenterY = 11;
            this.sliderMicReverbDirect.ThumbEndX = 124;
            // 
            // sliderReverbMicHpf
            // 
            this.sliderReverbMicHpf.BackColor = System.Drawing.Color.Transparent;
            this.sliderReverbMicHpf.backgroundDisabled = null;
            this.sliderReverbMicHpf.backgroundNormal = global::AEC.Properties.Resources.reverb_slider_back;
            this.sliderReverbMicHpf.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderReverbMicHpf.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderReverbMicHpf.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderReverbMicHpf.Location = new System.Drawing.Point(148, 72);
            this.sliderReverbMicHpf.Margin = new System.Windows.Forms.Padding(0);
            this.sliderReverbMicHpf.Name = "sliderReverbMicHpf";
            this.sliderReverbMicHpf.Size = new System.Drawing.Size(232, 23);
            this.sliderReverbMicHpf.SliderWidth = 142;
            this.sliderReverbMicHpf.TabIndex = 20;
            this.sliderReverbMicHpf.thumbCenterY = 11;
            this.sliderReverbMicHpf.ThumbEndX = 124;
            this.sliderReverbMicHpf.upperLimit = ((short)(100));
            this.sliderReverbMicHpf.value = ((short)(0));
            // 
            // lblReverbMicLpf
            // 
            this.lblReverbMicLpf.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblReverbMicLpf.BackColor = System.Drawing.Color.Transparent;
            this.lblReverbMicLpf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblReverbMicLpf.Location = new System.Drawing.Point(7, 102);
            this.lblReverbMicLpf.Name = "lblReverbMicLpf";
            this.lblReverbMicLpf.Size = new System.Drawing.Size(137, 23);
            this.lblReverbMicLpf.TabIndex = 19;
            this.lblReverbMicLpf.Text = "混响麦克风低通";
            // 
            // sliderReverbMicLpf
            // 
            this.sliderReverbMicLpf.BackColor = System.Drawing.Color.Transparent;
            this.sliderReverbMicLpf.backgroundDisabled = null;
            this.sliderReverbMicLpf.backgroundNormal = global::AEC.Properties.Resources.reverb_slider_back;
            this.sliderReverbMicLpf.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderReverbMicLpf.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderReverbMicLpf.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderReverbMicLpf.Location = new System.Drawing.Point(148, 102);
            this.sliderReverbMicLpf.Margin = new System.Windows.Forms.Padding(0);
            this.sliderReverbMicLpf.Name = "sliderReverbMicLpf";
            this.sliderReverbMicLpf.Size = new System.Drawing.Size(232, 23);
            this.sliderReverbMicLpf.SliderWidth = 142;
            this.sliderReverbMicLpf.TabIndex = 20;
            this.sliderReverbMicLpf.thumbCenterY = 11;
            this.sliderReverbMicLpf.ThumbEndX = 124;
            this.sliderReverbMicLpf.upperLimit = ((short)(100));
            this.sliderReverbMicLpf.value = ((short)(0));
            // 
            // lblReverbPreDelay
            // 
            this.lblReverbPreDelay.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblReverbPreDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblReverbPreDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblReverbPreDelay.Location = new System.Drawing.Point(7, 132);
            this.lblReverbPreDelay.Name = "lblReverbPreDelay";
            this.lblReverbPreDelay.Size = new System.Drawing.Size(137, 23);
            this.lblReverbPreDelay.TabIndex = 19;
            this.lblReverbPreDelay.Text = "混响预延时";
            // 
            // sliderReverbPreDelay
            // 
            this.sliderReverbPreDelay.BackColor = System.Drawing.Color.Transparent;
            this.sliderReverbPreDelay.backgroundDisabled = null;
            this.sliderReverbPreDelay.backgroundNormal = global::AEC.Properties.Resources.reverb_slider_back;
            this.sliderReverbPreDelay.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderReverbPreDelay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderReverbPreDelay.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderReverbPreDelay.Location = new System.Drawing.Point(148, 132);
            this.sliderReverbPreDelay.Margin = new System.Windows.Forms.Padding(0);
            this.sliderReverbPreDelay.Name = "sliderReverbPreDelay";
            this.sliderReverbPreDelay.Size = new System.Drawing.Size(232, 23);
            this.sliderReverbPreDelay.SliderWidth = 142;
            this.sliderReverbPreDelay.TabIndex = 20;
            this.sliderReverbPreDelay.thumbCenterY = 11;
            this.sliderReverbPreDelay.ThumbEndX = 124;
            this.sliderReverbPreDelay.upperLimit = ((short)(100));
            this.sliderReverbPreDelay.value = ((short)(0));
            // 
            // lblReverbDelay
            // 
            this.lblReverbDelay.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblReverbDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblReverbDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblReverbDelay.Location = new System.Drawing.Point(7, 162);
            this.lblReverbDelay.Name = "lblReverbDelay";
            this.lblReverbDelay.Size = new System.Drawing.Size(137, 23);
            this.lblReverbDelay.TabIndex = 19;
            this.lblReverbDelay.Text = "混响时间";
            // 
            // sliderReverbTime
            // 
            this.sliderReverbTime.BackColor = System.Drawing.Color.Transparent;
            this.sliderReverbTime.backgroundDisabled = null;
            this.sliderReverbTime.backgroundNormal = global::AEC.Properties.Resources.reverb_slider_back;
            this.sliderReverbTime.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderReverbTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderReverbTime.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderReverbTime.Location = new System.Drawing.Point(148, 162);
            this.sliderReverbTime.Margin = new System.Windows.Forms.Padding(0);
            this.sliderReverbTime.Name = "sliderReverbTime";
            this.sliderReverbTime.Size = new System.Drawing.Size(232, 23);
            this.sliderReverbTime.SliderWidth = 142;
            this.sliderReverbTime.TabIndex = 20;
            this.sliderReverbTime.thumbCenterY = 11;
            this.sliderReverbTime.ThumbEndX = 124;
            this.sliderReverbTime.upperLimit = ((short)(100));
            this.sliderReverbTime.value = ((short)(0));
            // 
            // eqResetAndByPassPanel
            // 
            this.eqResetAndByPassPanel.BackColor = System.Drawing.Color.Transparent;
            this.eqResetAndByPassPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.eqResetAndByPassPanel.isPass = false;
            this.eqResetAndByPassPanel.Location = new System.Drawing.Point(403, 275);
            this.eqResetAndByPassPanel.Name = "eqResetAndByPassPanel";
            this.eqResetAndByPassPanel.Size = new System.Drawing.Size(168, 33);
            this.eqResetAndByPassPanel.TabIndex = 22;
            // 
            // eqGroupPanel
            // 
            this.eqGroupPanel.BackColor = System.Drawing.Color.Transparent;
            this.eqGroupPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.eqGroupPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.eqGroupPanel.Location = new System.Drawing.Point(403, 8);
            this.eqGroupPanel.Name = "eqGroupPanel";
            this.eqGroupPanel.Size = new System.Drawing.Size(478, 258);
            this.eqGroupPanel.TabIndex = 2;
            // 
            // VKxPIIReverbPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.gtPanel1);
            this.Controls.Add(this.eqResetAndByPassPanel);
            this.Controls.Add(this.eqGroupPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "VKxPIIReverbPanel";
            this.Size = new System.Drawing.Size(1276, 312);
            this.gtPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private EQGroupPanel eqGroupPanel;
        private gtLabel lblReverbPreDelay;
        private HSliderParam sliderReverbPreDelay;
        private gtLabel lblReverbDelay;
        private HSliderParam sliderReverbTime;
        private gtLabel lblMicReverb;
        private gtLabel lblMicReverbDirect;
        private gtLabel lblReverbMicHpf;
        private HSliderParam sliderReverbMicHpf;
        private gtLabel lblReverbMicLpf;
        private HSliderParam sliderReverbMicLpf;
        private SignalMixerPanel sliderMicReverb;
        private SignalMixerPanel sliderMicReverbDirect;
        private EQResetAndByPassPanel eqResetAndByPassPanel;
        private gtPanel gtPanel1;
    }
}
