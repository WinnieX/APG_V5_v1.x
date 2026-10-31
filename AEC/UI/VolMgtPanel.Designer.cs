namespace AEC.UI
{
    partial class VolMgtPanel
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
            this.btnQuickSettings = new AEC.UI.Controls.MButton();
            this.lblMusicStartVol = new AEC.UI.gtLabel();
            this.lblEffectStartVol = new AEC.UI.gtLabel();
            this.lblEffectLimitVol = new AEC.UI.gtLabel();
            this.sliderMicLimitVol = new AEC.UI.HSliderParam();
            this.lblMicLimitVol = new AEC.UI.gtLabel();
            this.sliderEffectStartVol = new AEC.UI.HSliderParam();
            this.sliderMusicLimitVol = new AEC.UI.HSliderParam();
            this.lblMicStartVol = new AEC.UI.gtLabel();
            this.sliderMicStartVol = new AEC.UI.HSliderParam();
            this.sliderEffectLimitVol = new AEC.UI.HSliderParam();
            this.sliderMusicStartVol = new AEC.UI.HSliderParam();
            this.lblMusicLimitVol = new AEC.UI.gtLabel();
            this.SuspendLayout();
            // 
            // btnQuickSettings
            // 
            this.btnQuickSettings.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnQuickSettings.BorderWidth = 1F;
            this.btnQuickSettings.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnQuickSettings.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnQuickSettings.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnQuickSettings.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.btnQuickSettings.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnQuickSettings.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(204)))), ((int)(((byte)(6)))));
            this.btnQuickSettings.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnQuickSettings.Location = new System.Drawing.Point(387, 249);
            this.btnQuickSettings.Name = "btnQuickSettings";
            this.btnQuickSettings.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnQuickSettings.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(188)))), ((int)(((byte)(6)))));
            this.btnQuickSettings.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnQuickSettings.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnQuickSettings.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(153)))), ((int)(((byte)(5)))));
            this.btnQuickSettings.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnQuickSettings.Radius = 12;
            this.btnQuickSettings.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.btnQuickSettings.ShowBorder = true;
            this.btnQuickSettings.ShowText = true;
            this.btnQuickSettings.Size = new System.Drawing.Size(96, 28);
            this.btnQuickSettings.TabIndex = 43;
            this.btnQuickSettings.Text = "快捷设置";
            this.btnQuickSettings.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // lblMusicStartVol
            // 
            this.lblMusicStartVol.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMusicStartVol.BackColor = System.Drawing.Color.Transparent;
            this.lblMusicStartVol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMusicStartVol.Location = new System.Drawing.Point(10, 33);
            this.lblMusicStartVol.Name = "lblMusicStartVol";
            this.lblMusicStartVol.Size = new System.Drawing.Size(136, 23);
            this.lblMusicStartVol.TabIndex = 41;
            this.lblMusicStartVol.Text = "音乐启动音量";
            // 
            // lblEffectStartVol
            // 
            this.lblEffectStartVol.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEffectStartVol.BackColor = System.Drawing.Color.Transparent;
            this.lblEffectStartVol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEffectStartVol.Location = new System.Drawing.Point(10, 177);
            this.lblEffectStartVol.Name = "lblEffectStartVol";
            this.lblEffectStartVol.Size = new System.Drawing.Size(136, 23);
            this.lblEffectStartVol.TabIndex = 37;
            this.lblEffectStartVol.Text = "效果启动音量";
            // 
            // lblEffectLimitVol
            // 
            this.lblEffectLimitVol.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEffectLimitVol.BackColor = System.Drawing.Color.Transparent;
            this.lblEffectLimitVol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEffectLimitVol.Location = new System.Drawing.Point(10, 213);
            this.lblEffectLimitVol.Name = "lblEffectLimitVol";
            this.lblEffectLimitVol.Size = new System.Drawing.Size(136, 23);
            this.lblEffectLimitVol.TabIndex = 36;
            this.lblEffectLimitVol.Text = "效果最大音量";
            // 
            // sliderMicLimitVol
            // 
            this.sliderMicLimitVol.BackColor = System.Drawing.Color.Transparent;
            this.sliderMicLimitVol.backgroundDisabled = null;
            this.sliderMicLimitVol.backgroundNormal = global::AEC.Properties.Resources.system_slider_back;
            this.sliderMicLimitVol.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderMicLimitVol.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderMicLimitVol.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderMicLimitVol.Location = new System.Drawing.Point(155, 141);
            this.sliderMicLimitVol.Margin = new System.Windows.Forms.Padding(0);
            this.sliderMicLimitVol.Name = "sliderMicLimitVol";
            this.sliderMicLimitVol.Size = new System.Drawing.Size(334, 23);
            this.sliderMicLimitVol.SliderWidth = 264;
            this.sliderMicLimitVol.TabIndex = 31;
            this.sliderMicLimitVol.thumbCenterY = 11;
            this.sliderMicLimitVol.ThumbEndX = 248;
            this.sliderMicLimitVol.upperLimit = ((short)(100));
            this.sliderMicLimitVol.value = ((short)(0));
            // 
            // lblMicLimitVol
            // 
            this.lblMicLimitVol.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicLimitVol.BackColor = System.Drawing.Color.Transparent;
            this.lblMicLimitVol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMicLimitVol.Location = new System.Drawing.Point(10, 141);
            this.lblMicLimitVol.Name = "lblMicLimitVol";
            this.lblMicLimitVol.Size = new System.Drawing.Size(136, 23);
            this.lblMicLimitVol.TabIndex = 38;
            this.lblMicLimitVol.Text = "话筒最大音量";
            // 
            // sliderEffectStartVol
            // 
            this.sliderEffectStartVol.BackColor = System.Drawing.Color.Transparent;
            this.sliderEffectStartVol.backgroundDisabled = null;
            this.sliderEffectStartVol.backgroundNormal = global::AEC.Properties.Resources.system_slider_back;
            this.sliderEffectStartVol.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderEffectStartVol.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderEffectStartVol.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderEffectStartVol.Location = new System.Drawing.Point(155, 177);
            this.sliderEffectStartVol.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEffectStartVol.Name = "sliderEffectStartVol";
            this.sliderEffectStartVol.Size = new System.Drawing.Size(334, 23);
            this.sliderEffectStartVol.SliderWidth = 264;
            this.sliderEffectStartVol.TabIndex = 33;
            this.sliderEffectStartVol.thumbCenterY = 11;
            this.sliderEffectStartVol.ThumbEndX = 248;
            this.sliderEffectStartVol.upperLimit = ((short)(100));
            this.sliderEffectStartVol.value = ((short)(0));
            // 
            // sliderMusicLimitVol
            // 
            this.sliderMusicLimitVol.BackColor = System.Drawing.Color.Transparent;
            this.sliderMusicLimitVol.backgroundDisabled = null;
            this.sliderMusicLimitVol.backgroundNormal = global::AEC.Properties.Resources.system_slider_back;
            this.sliderMusicLimitVol.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderMusicLimitVol.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderMusicLimitVol.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderMusicLimitVol.Location = new System.Drawing.Point(155, 69);
            this.sliderMusicLimitVol.Margin = new System.Windows.Forms.Padding(0);
            this.sliderMusicLimitVol.Name = "sliderMusicLimitVol";
            this.sliderMusicLimitVol.Size = new System.Drawing.Size(334, 23);
            this.sliderMusicLimitVol.SliderWidth = 264;
            this.sliderMusicLimitVol.TabIndex = 30;
            this.sliderMusicLimitVol.thumbCenterY = 11;
            this.sliderMusicLimitVol.ThumbEndX = 248;
            this.sliderMusicLimitVol.upperLimit = ((short)(100));
            this.sliderMusicLimitVol.value = ((short)(0));
            // 
            // lblMicStartVol
            // 
            this.lblMicStartVol.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicStartVol.BackColor = System.Drawing.Color.Transparent;
            this.lblMicStartVol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMicStartVol.Location = new System.Drawing.Point(10, 105);
            this.lblMicStartVol.Name = "lblMicStartVol";
            this.lblMicStartVol.Size = new System.Drawing.Size(136, 23);
            this.lblMicStartVol.TabIndex = 39;
            this.lblMicStartVol.Text = "话筒启动音量";
            // 
            // sliderMicStartVol
            // 
            this.sliderMicStartVol.BackColor = System.Drawing.Color.Transparent;
            this.sliderMicStartVol.backgroundDisabled = null;
            this.sliderMicStartVol.backgroundNormal = global::AEC.Properties.Resources.system_slider_back;
            this.sliderMicStartVol.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderMicStartVol.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderMicStartVol.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderMicStartVol.Location = new System.Drawing.Point(155, 105);
            this.sliderMicStartVol.Margin = new System.Windows.Forms.Padding(0);
            this.sliderMicStartVol.Name = "sliderMicStartVol";
            this.sliderMicStartVol.Size = new System.Drawing.Size(334, 23);
            this.sliderMicStartVol.SliderWidth = 264;
            this.sliderMicStartVol.TabIndex = 34;
            this.sliderMicStartVol.thumbCenterY = 11;
            this.sliderMicStartVol.ThumbEndX = 248;
            this.sliderMicStartVol.upperLimit = ((short)(100));
            this.sliderMicStartVol.value = ((short)(0));
            // 
            // sliderEffectLimitVol
            // 
            this.sliderEffectLimitVol.BackColor = System.Drawing.Color.Transparent;
            this.sliderEffectLimitVol.backgroundDisabled = null;
            this.sliderEffectLimitVol.backgroundNormal = global::AEC.Properties.Resources.system_slider_back;
            this.sliderEffectLimitVol.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderEffectLimitVol.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderEffectLimitVol.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderEffectLimitVol.Location = new System.Drawing.Point(155, 213);
            this.sliderEffectLimitVol.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEffectLimitVol.Name = "sliderEffectLimitVol";
            this.sliderEffectLimitVol.Size = new System.Drawing.Size(334, 23);
            this.sliderEffectLimitVol.SliderWidth = 264;
            this.sliderEffectLimitVol.TabIndex = 32;
            this.sliderEffectLimitVol.thumbCenterY = 11;
            this.sliderEffectLimitVol.ThumbEndX = 248;
            this.sliderEffectLimitVol.upperLimit = ((short)(100));
            this.sliderEffectLimitVol.value = ((short)(0));
            // 
            // sliderMusicStartVol
            // 
            this.sliderMusicStartVol.BackColor = System.Drawing.Color.Transparent;
            this.sliderMusicStartVol.backgroundDisabled = null;
            this.sliderMusicStartVol.backgroundNormal = global::AEC.Properties.Resources.system_slider_back;
            this.sliderMusicStartVol.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderMusicStartVol.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderMusicStartVol.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderMusicStartVol.Location = new System.Drawing.Point(155, 33);
            this.sliderMusicStartVol.Margin = new System.Windows.Forms.Padding(0);
            this.sliderMusicStartVol.Name = "sliderMusicStartVol";
            this.sliderMusicStartVol.Size = new System.Drawing.Size(334, 23);
            this.sliderMusicStartVol.SliderWidth = 264;
            this.sliderMusicStartVol.TabIndex = 35;
            this.sliderMusicStartVol.thumbCenterY = 11;
            this.sliderMusicStartVol.ThumbEndX = 248;
            this.sliderMusicStartVol.upperLimit = ((short)(100));
            this.sliderMusicStartVol.value = ((short)(0));
            // 
            // lblMusicLimitVol
            // 
            this.lblMusicLimitVol.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMusicLimitVol.BackColor = System.Drawing.Color.Transparent;
            this.lblMusicLimitVol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMusicLimitVol.Location = new System.Drawing.Point(10, 69);
            this.lblMusicLimitVol.Name = "lblMusicLimitVol";
            this.lblMusicLimitVol.Size = new System.Drawing.Size(136, 23);
            this.lblMusicLimitVol.TabIndex = 40;
            this.lblMusicLimitVol.Text = "音乐最大音量";
            // 
            // VolMgtPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.Controls.Add(this.btnQuickSettings);
            this.Controls.Add(this.lblMusicStartVol);
            this.Controls.Add(this.lblEffectStartVol);
            this.Controls.Add(this.lblEffectLimitVol);
            this.Controls.Add(this.sliderMicLimitVol);
            this.Controls.Add(this.lblMicLimitVol);
            this.Controls.Add(this.sliderEffectStartVol);
            this.Controls.Add(this.sliderMusicLimitVol);
            this.Controls.Add(this.lblMicStartVol);
            this.Controls.Add(this.sliderMicStartVol);
            this.Controls.Add(this.sliderEffectLimitVol);
            this.Controls.Add(this.sliderMusicStartVol);
            this.Controls.Add(this.lblMusicLimitVol);
            this.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "VolMgtPanel";
            this.Size = new System.Drawing.Size(512, 294);
            this.ResumeLayout(false);

        }

        #endregion

        private gtLabel lblEffectLimitVol;
        private gtLabel lblEffectStartVol;
        private gtLabel lblMicLimitVol;
        private gtLabel lblMicStartVol;
        private gtLabel lblMusicLimitVol;
        private gtLabel lblMusicStartVol;
        private HSliderParam sliderMusicStartVol;
        private HSliderParam sliderMusicLimitVol;
        private HSliderParam sliderMicStartVol;
        private HSliderParam sliderMicLimitVol;
        private HSliderParam sliderEffectStartVol;
        private HSliderParam sliderEffectLimitVol;
        private Controls.MButton btnQuickSettings;
    }
}
