namespace AEC.UI
{
    partial class MicParam
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
            this.btnRight = new AEC.UI.gtButton();
            this.btnLeft = new AEC.UI.gtButton();
            this.SuspendLayout();
            // 
            // paramLabel
            // 
            this.paramLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paramLabel.BackColor = System.Drawing.Color.Transparent;
            this.paramLabel.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.paramLabel.initV = ((short)(0));
            this.paramLabel.isShowUpdownButton = false;
            this.paramLabel.Location = new System.Drawing.Point(14, 0);
            this.paramLabel.Margin = new System.Windows.Forms.Padding(0);
            this.paramLabel.Name = "paramLabel";
            this.paramLabel.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramLabel.Padding = new System.Windows.Forms.Padding(2);
            this.paramLabel.Size = new System.Drawing.Size(53, 25);
            this.paramLabel.TabIndex = 1;
            this.paramLabel.upperLimit = ((short)(0));
            this.paramLabel.userGDIPaint = false;
            this.paramLabel.v = ((short)(0));
            // 
            // btnRight
            // 
            this.btnRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRight.BackColor = System.Drawing.Color.Transparent;
            this.btnRight.backDisabled = global::AEC.Properties.Resources.fbe_right_disabled;
            this.btnRight.backHover = global::AEC.Properties.Resources.fbe_right_hover;
            this.btnRight.backNormal = global::AEC.Properties.Resources.fbe_right_normal;
            this.btnRight.backPressed = global::AEC.Properties.Resources.fbe_right_pressed;
            this.btnRight.ForeColor = System.Drawing.Color.White;
            this.btnRight.isStretch = false;
            this.btnRight.Location = new System.Drawing.Point(67, 2);
            this.btnRight.Margin = new System.Windows.Forms.Padding(0);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(14, 21);
            this.btnRight.TabIndex = 0;
            this.btnRight.userGDIPaint = true;
            // 
            // btnLeft
            // 
            this.btnLeft.BackColor = System.Drawing.Color.Transparent;
            this.btnLeft.backDisabled = global::AEC.Properties.Resources.fbe_left_disabled;
            this.btnLeft.backHover = global::AEC.Properties.Resources.fbe_left_hover;
            this.btnLeft.backNormal = global::AEC.Properties.Resources.fbe_left_normal;
            this.btnLeft.backPressed = global::AEC.Properties.Resources.fbe_left_pressed;
            this.btnLeft.ForeColor = System.Drawing.Color.White;
            this.btnLeft.isStretch = false;
            this.btnLeft.Location = new System.Drawing.Point(0, 2);
            this.btnLeft.Margin = new System.Windows.Forms.Padding(0);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(14, 21);
            this.btnLeft.TabIndex = 0;
            this.btnLeft.userGDIPaint = true;
            // 
            // MicParam
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.paramLabel);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnLeft);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MicParam";
            this.Size = new System.Drawing.Size(81, 25);
            this.ResumeLayout(false);

        }

        #endregion

        private gtButton btnLeft;
        private ParamLabel paramLabel;
        private gtButton btnRight;
    }
}
