namespace AEC.UI.VKxS
{
    partial class VKxSCompressorPanel
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
            this.sliderRatio = new AEC.UI.HSliderParam();
            this.sliderRelease = new AEC.UI.HSliderParam();
            this.sliderAttack = new AEC.UI.HSliderParam();
            this.sliderThreshold = new AEC.UI.HSliderParam();
            this.lblRatio = new AEC.UI.gtLabel();
            this.lblRelease = new AEC.UI.gtLabel();
            this.lblAttack = new AEC.UI.gtLabel();
            this.lblThreshold = new AEC.UI.gtLabel();
            this.SuspendLayout();
            // 
            // sliderRatio
            // 
            this.sliderRatio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderRatio.BackColor = System.Drawing.Color.Transparent;
            this.sliderRatio.backgroundDisabled = null;
            this.sliderRatio.backgroundNormal = global::AEC.Properties.Resources.mic_slider_back;
            this.sliderRatio.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderRatio.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderRatio.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderRatio.Location = new System.Drawing.Point(104, 90);
            this.sliderRatio.Margin = new System.Windows.Forms.Padding(0);
            this.sliderRatio.Name = "sliderRatio";
            this.sliderRatio.Size = new System.Drawing.Size(233, 23);
            this.sliderRatio.SliderWidth = 157;
            this.sliderRatio.TabIndex = 15;
            this.sliderRatio.thumbCenterY = 11;
            this.sliderRatio.ThumbEndX = 140;
            this.sliderRatio.upperLimit = ((short)(100));
            this.sliderRatio.value = ((short)(0));
            // 
            // sliderRelease
            // 
            this.sliderRelease.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderRelease.BackColor = System.Drawing.Color.Transparent;
            this.sliderRelease.backgroundDisabled = null;
            this.sliderRelease.backgroundNormal = global::AEC.Properties.Resources.mic_slider_back;
            this.sliderRelease.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderRelease.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderRelease.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderRelease.Location = new System.Drawing.Point(104, 63);
            this.sliderRelease.Margin = new System.Windows.Forms.Padding(0);
            this.sliderRelease.Name = "sliderRelease";
            this.sliderRelease.Size = new System.Drawing.Size(233, 23);
            this.sliderRelease.SliderWidth = 157;
            this.sliderRelease.TabIndex = 16;
            this.sliderRelease.thumbCenterY = 11;
            this.sliderRelease.ThumbEndX = 140;
            this.sliderRelease.upperLimit = ((short)(100));
            this.sliderRelease.value = ((short)(0));
            // 
            // sliderAttack
            // 
            this.sliderAttack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderAttack.BackColor = System.Drawing.Color.Transparent;
            this.sliderAttack.backgroundDisabled = null;
            this.sliderAttack.backgroundNormal = global::AEC.Properties.Resources.mic_slider_back;
            this.sliderAttack.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderAttack.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderAttack.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderAttack.Location = new System.Drawing.Point(104, 36);
            this.sliderAttack.Margin = new System.Windows.Forms.Padding(0);
            this.sliderAttack.Name = "sliderAttack";
            this.sliderAttack.Size = new System.Drawing.Size(233, 23);
            this.sliderAttack.SliderWidth = 157;
            this.sliderAttack.TabIndex = 17;
            this.sliderAttack.thumbCenterY = 11;
            this.sliderAttack.ThumbEndX = 140;
            this.sliderAttack.upperLimit = ((short)(100));
            this.sliderAttack.value = ((short)(0));
            // 
            // sliderThreshold
            // 
            this.sliderThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderThreshold.BackColor = System.Drawing.Color.Transparent;
            this.sliderThreshold.backgroundDisabled = null;
            this.sliderThreshold.backgroundNormal = global::AEC.Properties.Resources.mic_slider_back;
            this.sliderThreshold.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderThreshold.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderThreshold.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderThreshold.Location = new System.Drawing.Point(104, 9);
            this.sliderThreshold.Margin = new System.Windows.Forms.Padding(0);
            this.sliderThreshold.Name = "sliderThreshold";
            this.sliderThreshold.Size = new System.Drawing.Size(233, 23);
            this.sliderThreshold.SliderWidth = 157;
            this.sliderThreshold.TabIndex = 18;
            this.sliderThreshold.thumbCenterY = 11;
            this.sliderThreshold.ThumbEndX = 140;
            this.sliderThreshold.upperLimit = ((short)(100));
            this.sliderThreshold.value = ((short)(0));
            // 
            // lblRatio
            // 
            this.lblRatio.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblRatio.BackColor = System.Drawing.Color.Transparent;
            this.lblRatio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblRatio.Location = new System.Drawing.Point(10, 90);
            this.lblRatio.Name = "lblRatio";
            this.lblRatio.Size = new System.Drawing.Size(90, 23);
            this.lblRatio.TabIndex = 11;
            this.lblRatio.Text = "压缩比例";
            // 
            // lblRelease
            // 
            this.lblRelease.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblRelease.BackColor = System.Drawing.Color.Transparent;
            this.lblRelease.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblRelease.Location = new System.Drawing.Point(10, 63);
            this.lblRelease.Name = "lblRelease";
            this.lblRelease.Size = new System.Drawing.Size(90, 23);
            this.lblRelease.TabIndex = 12;
            this.lblRelease.Text = "压缩恢复时间";
            // 
            // lblAttack
            // 
            this.lblAttack.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblAttack.BackColor = System.Drawing.Color.Transparent;
            this.lblAttack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblAttack.Location = new System.Drawing.Point(10, 36);
            this.lblAttack.Name = "lblAttack";
            this.lblAttack.Size = new System.Drawing.Size(90, 23);
            this.lblAttack.TabIndex = 13;
            this.lblAttack.Text = "压缩启动时间";
            // 
            // lblThreshold
            // 
            this.lblThreshold.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblThreshold.BackColor = System.Drawing.Color.Transparent;
            this.lblThreshold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblThreshold.Location = new System.Drawing.Point(10, 9);
            this.lblThreshold.Name = "lblThreshold";
            this.lblThreshold.Size = new System.Drawing.Size(90, 23);
            this.lblThreshold.TabIndex = 14;
            this.lblThreshold.Text = "压缩启动电平";
            // 
            // VKxSCompressorPanel
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.sliderRatio);
            this.Controls.Add(this.sliderRelease);
            this.Controls.Add(this.sliderAttack);
            this.Controls.Add(this.sliderThreshold);
            this.Controls.Add(this.lblRatio);
            this.Controls.Add(this.lblRelease);
            this.Controls.Add(this.lblAttack);
            this.Controls.Add(this.lblThreshold);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "VKxSCompressorPanel";
            this.Size = new System.Drawing.Size(350, 122);
            this.ResumeLayout(false);

        }

        #endregion

        private gtLabel lblRatio;
        private gtLabel lblRelease;
        private gtLabel lblAttack;
        private gtLabel lblThreshold;
        private HSliderParam sliderRatio;
        private HSliderParam sliderRelease;
        private HSliderParam sliderAttack;
        private HSliderParam sliderThreshold;
    }
}
