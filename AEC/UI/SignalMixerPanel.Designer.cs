namespace AEC.UI
{
    partial class SignalMixerPanel
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
            this.btnPhase = new AEC.UI.gtButton();
            this.paramLabel = new AEC.UI.ParamLabel();
            this.hSlider = new AEC.UI.HSlider();
            this.SuspendLayout();
            // 
            // btnPhase
            // 
            this.btnPhase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPhase.BackColor = System.Drawing.Color.Transparent;
            this.btnPhase.backDisabled = global::AEC.Properties.Resources.phase_positive_disabled;
            this.btnPhase.backHover = global::AEC.Properties.Resources.phase_positive_hover;
            this.btnPhase.backNormal = global::AEC.Properties.Resources.phase_positive_normal;
            this.btnPhase.backPressed = global::AEC.Properties.Resources.phase_positive_pressed;
            this.btnPhase.ForeColor = System.Drawing.Color.White;
            this.btnPhase.isStretch = false;
            this.btnPhase.Location = new System.Drawing.Point(188, 2);
            this.btnPhase.Margin = new System.Windows.Forms.Padding(0);
            this.btnPhase.Name = "btnPhase";
            this.btnPhase.Size = new System.Drawing.Size(19, 19);
            this.btnPhase.TabIndex = 2;
            this.btnPhase.userGDIPaint = true;
            // 
            // paramLabel
            // 
            this.paramLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.paramLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.paramLabel.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.paramLabel.initV = ((short)(0));
            this.paramLabel.isShowUpdownButton = true;
            this.paramLabel.Location = new System.Drawing.Point(208, 0);
            this.paramLabel.Margin = new System.Windows.Forms.Padding(0);
            this.paramLabel.Name = "paramLabel";
            this.paramLabel.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramLabel.Padding = new System.Windows.Forms.Padding(2);
            this.paramLabel.Size = new System.Drawing.Size(70, 23);
            this.paramLabel.TabIndex = 1;
            this.paramLabel.upperLimit = ((short)(0));
            this.paramLabel.userGDIPaint = false;
            this.paramLabel.v = ((short)(0));
            // 
            // hSlider
            // 
            this.hSlider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hSlider.BackColor = System.Drawing.Color.Transparent;
            this.hSlider.backgroundDisabled = null;
            this.hSlider.backgroundNormal = null;
            this.hSlider.backLeftTop = new System.Drawing.Point(0, 6);
            this.hSlider.Location = new System.Drawing.Point(0, 0);
            this.hSlider.Margin = new System.Windows.Forms.Padding(0);
            this.hSlider.maximum = ((short)(100));
            this.hSlider.Name = "hSlider";
            this.hSlider.Size = new System.Drawing.Size(186, 23);
            this.hSlider.TabIndex = 0;
            this.hSlider.Text = "hSlider";
            this.hSlider.thumbCenterY = 11;
            this.hSlider.thumbDisabled = global::AEC.Properties.Resources.HSliderThumbDisabled;
            this.hSlider.ThumbEndX = 176;
            this.hSlider.thumbHeight = 18;
            this.hSlider.thumbHover = global::AEC.Properties.Resources.HSliderThumbHover;
            this.hSlider.thumbNormal = global::AEC.Properties.Resources.HSliderThumbNormal;
            this.hSlider.thumbPressed = global::AEC.Properties.Resources.HSliderThumbPressed;
            this.hSlider.thumbStartX = 11;
            this.hSlider.thumbWidth = 22;
            this.hSlider.upperLimit = ((short)(100));
            this.hSlider.userGDIPaint = true;
            this.hSlider.v = ((short)(0));
            // 
            // SignalMixerPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnPhase);
            this.Controls.Add(this.paramLabel);
            this.Controls.Add(this.hSlider);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Name = "SignalMixerPanel";
            this.Size = new System.Drawing.Size(278, 23);
            this.ResumeLayout(false);

        }

        #endregion

        protected ParamLabel paramLabel;
        private HSlider hSlider;
        private gtButton btnPhase;
    }
}
