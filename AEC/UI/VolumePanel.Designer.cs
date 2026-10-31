namespace AEC.UI
{
    partial class VolumePanel
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
            this.lblTag = new AEC.UI.gtLabel();
            this.volumeLabel = new AEC.UI.VolumeLabel();
            this.hSlider = new AEC.UI.VolumeSlider();
            this.SuspendLayout();
            // 
            // lblTag
            // 
            this.lblTag.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblTag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTag.BackColor = System.Drawing.Color.Transparent;
            this.lblTag.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(81)))), ((int)(((byte)(0)))));
            this.lblTag.Location = new System.Drawing.Point(0, 3);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(44, 23);
            this.lblTag.TabIndex = 3;
            this.lblTag.Text = "音乐";
            // 
            // volumeLabel
            // 
            this.volumeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.volumeLabel.BackColor = System.Drawing.Color.Black;
            this.volumeLabel.disabledBack = null;
            this.volumeLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.volumeLabel.ForeColor = System.Drawing.Color.Black;
            this.volumeLabel.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.volumeLabel.initV = ((short)(0));
            this.volumeLabel.isShowUpdownButton = false;
            this.volumeLabel.Location = new System.Drawing.Point(145, 2);
            this.volumeLabel.Margin = new System.Windows.Forms.Padding(0);
            this.volumeLabel.muteBack = global::AEC.Properties.Resources.volume_label_mute_back;
            this.volumeLabel.Name = "volumeLabel";
            this.volumeLabel.normalBack = global::AEC.Properties.Resources.volume_label_normal_back;
            this.volumeLabel.Padding = new System.Windows.Forms.Padding(2);
            this.volumeLabel.Size = new System.Drawing.Size(32, 25);
            this.volumeLabel.TabIndex = 2;
            this.volumeLabel.upperLimit = ((short)(0));
            this.volumeLabel.userGDIPaint = false;
            this.volumeLabel.v = ((short)(0));
            // 
            // hSlider
            // 
            this.hSlider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hSlider.BackColor = System.Drawing.Color.Transparent;
            this.hSlider.backgroundDisabled = null;
            this.hSlider.backgroundNormal = global::AEC.Properties.Resources.volume_slider_back;
            this.hSlider.backLeftTop = new System.Drawing.Point(0, 2);
            this.hSlider.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.hSlider.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.hSlider.levelLocation = new System.Drawing.Point(4, 5);
            this.hSlider.Location = new System.Drawing.Point(44, 3);
            this.hSlider.Margin = new System.Windows.Forms.Padding(0);
            this.hSlider.maximum = ((short)(100));
            this.hSlider.Name = "hSlider";
            this.hSlider.Size = new System.Drawing.Size(100, 23);
            this.hSlider.TabIndex = 1;
            this.hSlider.Text = "hSlider";
            this.hSlider.thumbCenterY = 10;
            this.hSlider.thumbDisabled = global::AEC.Properties.Resources.volume_slider_thumb_disabled;
            this.hSlider.ThumbEndX = 90;
            this.hSlider.thumbHeight = 18;
            this.hSlider.thumbHover = global::AEC.Properties.Resources.volume_slider_thumb_hover;
            this.hSlider.thumbNormal = global::AEC.Properties.Resources.volume_slider_thumb_normal;
            this.hSlider.thumbPressed = global::AEC.Properties.Resources.volume_slider_thumb_pressed;
            this.hSlider.thumbStartX = 13;
            this.hSlider.thumbWidth = 22;
            this.hSlider.upperLimit = ((short)(100));
            this.hSlider.userGDIPaint = false;
            this.hSlider.v = ((short)(0));
            // 
            // VolumePanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.volumeLabel);
            this.Controls.Add(this.hSlider);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Name = "VolumePanel";
            this.Size = new System.Drawing.Size(178, 25);
            this.ResumeLayout(false);

        }

        #endregion

        private VolumeSlider hSlider;
        private VolumeLabel volumeLabel;
        private gtLabel lblTag;

    }
}
