namespace AEC.UI
{
    partial class HSliderParam
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
            this.paramLabel = new AEC.UI.ParamLabel();
            this.hSlider = new AEC.UI.HSlider();
            this.SuspendLayout();
            // 
            // paramLabel
            // 
            this.paramLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.paramLabel.BackColor = System.Drawing.Color.Transparent;
            this.paramLabel.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramLabel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.paramLabel.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.paramLabel.initV = ((short)(0));
            this.paramLabel.isShowUpdownButton = true;
            this.paramLabel.Location = new System.Drawing.Point(188, 0);
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
            this.hSlider.Size = new System.Drawing.Size(188, 23);
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
            // HSliderParam
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.paramLabel);
            this.Controls.Add(this.hSlider);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "HSliderParam";
            this.Size = new System.Drawing.Size(258, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private HSlider hSlider;
        private ParamLabel paramLabel;
    }
}
