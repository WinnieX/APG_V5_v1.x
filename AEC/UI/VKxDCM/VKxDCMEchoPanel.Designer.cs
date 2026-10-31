namespace AEC.UI.VKxDCM
{
    partial class VKxDCMEchoPanel
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
            this.panelEcho = new AEC.UI.gtPanel();
            this.slideMicEcho = new AEC.UI.SignalMixerPanel();
            this.lblMicEcho = new AEC.UI.gtLabel();
            this.lblMicEchoDirect = new AEC.UI.gtLabel();
            this.sliderMicEchoDirect = new AEC.UI.SignalMixerPanel();
            this.lblEchoHpf = new AEC.UI.gtLabel();
            this.sliderEchoHpf = new AEC.UI.HSliderParam();
            this.lblEchoPreDelay = new AEC.UI.gtLabel();
            this.sliderEchoRChDelay = new AEC.UI.HSliderParam();
            this.lblEchoLpf = new AEC.UI.gtLabel();
            this.lblEchoRChDelay = new AEC.UI.gtLabel();
            this.sliderEchoPreDelay = new AEC.UI.HSliderParam();
            this.sliderEchoRChPreDelay = new AEC.UI.HSliderParam();
            this.sliderEchoLpf = new AEC.UI.HSliderParam();
            this.lblEchoRChPreDelay = new AEC.UI.gtLabel();
            this.lblEchoDelay = new AEC.UI.gtLabel();
            this.sliderEchoRepeat = new AEC.UI.HSliderParam();
            this.sliderEchoDelay = new AEC.UI.HSliderParam();
            this.lblEchoRepeat = new AEC.UI.gtLabel();
            this.eqResetAndByPassPanel = new AEC.UI.EQResetAndByPassPanel();
            this.eqGroupPanel = new AEC.UI.EQGroupPanel();
            this.panelEcho.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEcho
            // 
            this.panelEcho.BackColor = System.Drawing.Color.Transparent;
            this.panelEcho.Controls.Add(this.slideMicEcho);
            this.panelEcho.Controls.Add(this.lblMicEcho);
            this.panelEcho.Controls.Add(this.lblMicEchoDirect);
            this.panelEcho.Controls.Add(this.sliderMicEchoDirect);
            this.panelEcho.Controls.Add(this.lblEchoHpf);
            this.panelEcho.Controls.Add(this.sliderEchoHpf);
            this.panelEcho.Controls.Add(this.lblEchoPreDelay);
            this.panelEcho.Controls.Add(this.sliderEchoRChDelay);
            this.panelEcho.Controls.Add(this.lblEchoLpf);
            this.panelEcho.Controls.Add(this.lblEchoRChDelay);
            this.panelEcho.Controls.Add(this.sliderEchoPreDelay);
            this.panelEcho.Controls.Add(this.sliderEchoRChPreDelay);
            this.panelEcho.Controls.Add(this.sliderEchoLpf);
            this.panelEcho.Controls.Add(this.lblEchoRChPreDelay);
            this.panelEcho.Controls.Add(this.lblEchoDelay);
            this.panelEcho.Controls.Add(this.sliderEchoRepeat);
            this.panelEcho.Controls.Add(this.sliderEchoDelay);
            this.panelEcho.Controls.Add(this.lblEchoRepeat);
            this.panelEcho.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelEcho.ForeColor = System.Drawing.Color.LightGray;
            this.panelEcho.Location = new System.Drawing.Point(10, 8);
            this.panelEcho.Name = "panelEcho";
            this.panelEcho.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelEcho.Radius = 18;
            this.panelEcho.RectColor = System.Drawing.Color.Black;
            this.panelEcho.ShowRect = true;
            this.panelEcho.Size = new System.Drawing.Size(387, 298);
            this.panelEcho.TabIndex = 23;
            this.panelEcho.title = null;
            this.panelEcho.titleHeight = 20;
            // 
            // slideMicEcho
            // 
            this.slideMicEcho.BackColor = System.Drawing.Color.Transparent;
            this.slideMicEcho.backgroundDisabled = null;
            this.slideMicEcho.backgroundNormal = global::AEC.Properties.Resources.effect_slider_back;
            this.slideMicEcho.backLeftTop = new System.Drawing.Point(0, 4);
            this.slideMicEcho.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.slideMicEcho.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.slideMicEcho.Location = new System.Drawing.Point(148, 12);
            this.slideMicEcho.Name = "slideMicEcho";
            this.slideMicEcho.Size = new System.Drawing.Size(232, 23);
            this.slideMicEcho.SliderWidth = 142;
            this.slideMicEcho.TabIndex = 21;
            this.slideMicEcho.thumbCenterY = 11;
            this.slideMicEcho.ThumbEndX = 124;
            // 
            // lblMicEcho
            // 
            this.lblMicEcho.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicEcho.BackColor = System.Drawing.Color.Transparent;
            this.lblMicEcho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMicEcho.Location = new System.Drawing.Point(7, 12);
            this.lblMicEcho.Name = "lblMicEcho";
            this.lblMicEcho.Size = new System.Drawing.Size(137, 23);
            this.lblMicEcho.TabIndex = 19;
            this.lblMicEcho.Text = "MIC回声效果电平";
            // 
            // lblMicEchoDirect
            // 
            this.lblMicEchoDirect.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicEchoDirect.BackColor = System.Drawing.Color.Transparent;
            this.lblMicEchoDirect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMicEchoDirect.Location = new System.Drawing.Point(7, 42);
            this.lblMicEchoDirect.Name = "lblMicEchoDirect";
            this.lblMicEchoDirect.Size = new System.Drawing.Size(137, 23);
            this.lblMicEchoDirect.TabIndex = 19;
            this.lblMicEchoDirect.Text = "MIC回声直达电平";
            // 
            // sliderMicEchoDirect
            // 
            this.sliderMicEchoDirect.BackColor = System.Drawing.Color.Transparent;
            this.sliderMicEchoDirect.backgroundDisabled = null;
            this.sliderMicEchoDirect.backgroundNormal = global::AEC.Properties.Resources.effect_slider_back;
            this.sliderMicEchoDirect.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderMicEchoDirect.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderMicEchoDirect.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderMicEchoDirect.Location = new System.Drawing.Point(148, 42);
            this.sliderMicEchoDirect.Name = "sliderMicEchoDirect";
            this.sliderMicEchoDirect.Size = new System.Drawing.Size(232, 23);
            this.sliderMicEchoDirect.SliderWidth = 142;
            this.sliderMicEchoDirect.TabIndex = 21;
            this.sliderMicEchoDirect.thumbCenterY = 11;
            this.sliderMicEchoDirect.ThumbEndX = 124;
            // 
            // lblEchoHpf
            // 
            this.lblEchoHpf.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEchoHpf.BackColor = System.Drawing.Color.Transparent;
            this.lblEchoHpf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEchoHpf.Location = new System.Drawing.Point(7, 72);
            this.lblEchoHpf.Name = "lblEchoHpf";
            this.lblEchoHpf.Size = new System.Drawing.Size(137, 23);
            this.lblEchoHpf.TabIndex = 19;
            this.lblEchoHpf.Text = "回声高通";
            // 
            // sliderEchoHpf
            // 
            this.sliderEchoHpf.BackColor = System.Drawing.Color.Transparent;
            this.sliderEchoHpf.backgroundDisabled = null;
            this.sliderEchoHpf.backgroundNormal = global::AEC.Properties.Resources.effect_slider_back;
            this.sliderEchoHpf.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderEchoHpf.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderEchoHpf.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderEchoHpf.Location = new System.Drawing.Point(148, 72);
            this.sliderEchoHpf.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEchoHpf.Name = "sliderEchoHpf";
            this.sliderEchoHpf.Size = new System.Drawing.Size(232, 23);
            this.sliderEchoHpf.SliderWidth = 142;
            this.sliderEchoHpf.TabIndex = 20;
            this.sliderEchoHpf.thumbCenterY = 11;
            this.sliderEchoHpf.ThumbEndX = 124;
            this.sliderEchoHpf.upperLimit = ((short)(100));
            this.sliderEchoHpf.value = ((short)(0));
            // 
            // lblEchoPreDelay
            // 
            this.lblEchoPreDelay.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEchoPreDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblEchoPreDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEchoPreDelay.Location = new System.Drawing.Point(7, 132);
            this.lblEchoPreDelay.Name = "lblEchoPreDelay";
            this.lblEchoPreDelay.Size = new System.Drawing.Size(137, 23);
            this.lblEchoPreDelay.TabIndex = 19;
            this.lblEchoPreDelay.Text = "回声预延时";
            // 
            // sliderEchoRChDelay
            // 
            this.sliderEchoRChDelay.BackColor = System.Drawing.Color.Transparent;
            this.sliderEchoRChDelay.backgroundDisabled = null;
            this.sliderEchoRChDelay.backgroundNormal = global::AEC.Properties.Resources.effect_slider_back;
            this.sliderEchoRChDelay.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderEchoRChDelay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderEchoRChDelay.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderEchoRChDelay.Location = new System.Drawing.Point(148, 252);
            this.sliderEchoRChDelay.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEchoRChDelay.Name = "sliderEchoRChDelay";
            this.sliderEchoRChDelay.Size = new System.Drawing.Size(232, 23);
            this.sliderEchoRChDelay.SliderWidth = 142;
            this.sliderEchoRChDelay.TabIndex = 20;
            this.sliderEchoRChDelay.thumbCenterY = 11;
            this.sliderEchoRChDelay.ThumbEndX = 124;
            this.sliderEchoRChDelay.upperLimit = ((short)(100));
            this.sliderEchoRChDelay.value = ((short)(0));
            // 
            // lblEchoLpf
            // 
            this.lblEchoLpf.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEchoLpf.BackColor = System.Drawing.Color.Transparent;
            this.lblEchoLpf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEchoLpf.Location = new System.Drawing.Point(7, 102);
            this.lblEchoLpf.Name = "lblEchoLpf";
            this.lblEchoLpf.Size = new System.Drawing.Size(137, 23);
            this.lblEchoLpf.TabIndex = 19;
            this.lblEchoLpf.Text = "回声低通";
            // 
            // lblEchoRChDelay
            // 
            this.lblEchoRChDelay.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEchoRChDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblEchoRChDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEchoRChDelay.Location = new System.Drawing.Point(7, 252);
            this.lblEchoRChDelay.Name = "lblEchoRChDelay";
            this.lblEchoRChDelay.Size = new System.Drawing.Size(137, 23);
            this.lblEchoRChDelay.TabIndex = 19;
            this.lblEchoRChDelay.Text = "回声右通道延时";
            // 
            // sliderEchoPreDelay
            // 
            this.sliderEchoPreDelay.BackColor = System.Drawing.Color.Transparent;
            this.sliderEchoPreDelay.backgroundDisabled = null;
            this.sliderEchoPreDelay.backgroundNormal = global::AEC.Properties.Resources.effect_slider_back;
            this.sliderEchoPreDelay.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderEchoPreDelay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderEchoPreDelay.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderEchoPreDelay.Location = new System.Drawing.Point(148, 132);
            this.sliderEchoPreDelay.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEchoPreDelay.Name = "sliderEchoPreDelay";
            this.sliderEchoPreDelay.Size = new System.Drawing.Size(232, 23);
            this.sliderEchoPreDelay.SliderWidth = 142;
            this.sliderEchoPreDelay.TabIndex = 20;
            this.sliderEchoPreDelay.thumbCenterY = 11;
            this.sliderEchoPreDelay.ThumbEndX = 124;
            this.sliderEchoPreDelay.upperLimit = ((short)(100));
            this.sliderEchoPreDelay.value = ((short)(0));
            // 
            // sliderEchoRChPreDelay
            // 
            this.sliderEchoRChPreDelay.BackColor = System.Drawing.Color.Transparent;
            this.sliderEchoRChPreDelay.backgroundDisabled = null;
            this.sliderEchoRChPreDelay.backgroundNormal = global::AEC.Properties.Resources.effect_slider_back;
            this.sliderEchoRChPreDelay.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderEchoRChPreDelay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderEchoRChPreDelay.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderEchoRChPreDelay.Location = new System.Drawing.Point(148, 222);
            this.sliderEchoRChPreDelay.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEchoRChPreDelay.Name = "sliderEchoRChPreDelay";
            this.sliderEchoRChPreDelay.Size = new System.Drawing.Size(232, 23);
            this.sliderEchoRChPreDelay.SliderWidth = 142;
            this.sliderEchoRChPreDelay.TabIndex = 20;
            this.sliderEchoRChPreDelay.thumbCenterY = 11;
            this.sliderEchoRChPreDelay.ThumbEndX = 124;
            this.sliderEchoRChPreDelay.upperLimit = ((short)(100));
            this.sliderEchoRChPreDelay.value = ((short)(0));
            // 
            // sliderEchoLpf
            // 
            this.sliderEchoLpf.BackColor = System.Drawing.Color.Transparent;
            this.sliderEchoLpf.backgroundDisabled = null;
            this.sliderEchoLpf.backgroundNormal = global::AEC.Properties.Resources.effect_slider_back;
            this.sliderEchoLpf.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderEchoLpf.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderEchoLpf.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderEchoLpf.Location = new System.Drawing.Point(148, 102);
            this.sliderEchoLpf.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEchoLpf.Name = "sliderEchoLpf";
            this.sliderEchoLpf.Size = new System.Drawing.Size(232, 23);
            this.sliderEchoLpf.SliderWidth = 142;
            this.sliderEchoLpf.TabIndex = 20;
            this.sliderEchoLpf.thumbCenterY = 11;
            this.sliderEchoLpf.ThumbEndX = 124;
            this.sliderEchoLpf.upperLimit = ((short)(100));
            this.sliderEchoLpf.value = ((short)(0));
            // 
            // lblEchoRChPreDelay
            // 
            this.lblEchoRChPreDelay.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEchoRChPreDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblEchoRChPreDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEchoRChPreDelay.Location = new System.Drawing.Point(7, 222);
            this.lblEchoRChPreDelay.Name = "lblEchoRChPreDelay";
            this.lblEchoRChPreDelay.Size = new System.Drawing.Size(137, 23);
            this.lblEchoRChPreDelay.TabIndex = 19;
            this.lblEchoRChPreDelay.Text = "回声右通道预延时";
            // 
            // lblEchoDelay
            // 
            this.lblEchoDelay.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEchoDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblEchoDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEchoDelay.Location = new System.Drawing.Point(7, 162);
            this.lblEchoDelay.Name = "lblEchoDelay";
            this.lblEchoDelay.Size = new System.Drawing.Size(137, 23);
            this.lblEchoDelay.TabIndex = 19;
            this.lblEchoDelay.Text = "回声延时";
            // 
            // sliderEchoRepeat
            // 
            this.sliderEchoRepeat.BackColor = System.Drawing.Color.Transparent;
            this.sliderEchoRepeat.backgroundDisabled = null;
            this.sliderEchoRepeat.backgroundNormal = global::AEC.Properties.Resources.effect_slider_back;
            this.sliderEchoRepeat.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderEchoRepeat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderEchoRepeat.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderEchoRepeat.Location = new System.Drawing.Point(148, 192);
            this.sliderEchoRepeat.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEchoRepeat.Name = "sliderEchoRepeat";
            this.sliderEchoRepeat.Size = new System.Drawing.Size(232, 23);
            this.sliderEchoRepeat.SliderWidth = 142;
            this.sliderEchoRepeat.TabIndex = 20;
            this.sliderEchoRepeat.thumbCenterY = 11;
            this.sliderEchoRepeat.ThumbEndX = 124;
            this.sliderEchoRepeat.upperLimit = ((short)(100));
            this.sliderEchoRepeat.value = ((short)(0));
            // 
            // sliderEchoDelay
            // 
            this.sliderEchoDelay.BackColor = System.Drawing.Color.Transparent;
            this.sliderEchoDelay.backgroundDisabled = null;
            this.sliderEchoDelay.backgroundNormal = global::AEC.Properties.Resources.effect_slider_back;
            this.sliderEchoDelay.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderEchoDelay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderEchoDelay.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderEchoDelay.Location = new System.Drawing.Point(148, 162);
            this.sliderEchoDelay.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEchoDelay.Name = "sliderEchoDelay";
            this.sliderEchoDelay.Size = new System.Drawing.Size(232, 23);
            this.sliderEchoDelay.SliderWidth = 142;
            this.sliderEchoDelay.TabIndex = 20;
            this.sliderEchoDelay.thumbCenterY = 11;
            this.sliderEchoDelay.ThumbEndX = 124;
            this.sliderEchoDelay.upperLimit = ((short)(100));
            this.sliderEchoDelay.value = ((short)(0));
            // 
            // lblEchoRepeat
            // 
            this.lblEchoRepeat.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEchoRepeat.BackColor = System.Drawing.Color.Transparent;
            this.lblEchoRepeat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEchoRepeat.Location = new System.Drawing.Point(7, 192);
            this.lblEchoRepeat.Name = "lblEchoRepeat";
            this.lblEchoRepeat.Size = new System.Drawing.Size(137, 23);
            this.lblEchoRepeat.TabIndex = 19;
            this.lblEchoRepeat.Text = "回声重复比率";
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
            // VKxPIIEchoPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelEcho);
            this.Controls.Add(this.eqResetAndByPassPanel);
            this.Controls.Add(this.eqGroupPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "VKxPIIEchoPanel";
            this.Size = new System.Drawing.Size(1276, 312);
            this.panelEcho.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private EQGroupPanel eqGroupPanel;
        private gtLabel lblMicEcho;
        private gtLabel lblMicEchoDirect;
        private gtLabel lblEchoHpf;
        private HSliderParam sliderEchoHpf;
        private gtLabel lblEchoLpf;
        private HSliderParam sliderEchoLpf;
        private gtLabel lblEchoRepeat;
        private HSliderParam sliderEchoRepeat;
        private gtLabel lblEchoRChPreDelay;
        private HSliderParam sliderEchoRChPreDelay;
        private gtLabel lblEchoRChDelay;
        private HSliderParam sliderEchoRChDelay;
        private gtLabel lblEchoPreDelay;
        private HSliderParam sliderEchoPreDelay;
        private gtLabel lblEchoDelay;
        private HSliderParam sliderEchoDelay;
        private SignalMixerPanel slideMicEcho;
        private SignalMixerPanel sliderMicEchoDirect;
        private EQResetAndByPassPanel eqResetAndByPassPanel;
        private gtPanel panelEcho;
    }
}
