namespace AEC.UI.VKxDM
{
    partial class VKxDMLimiterPanel
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
            this.btnSwitchUnit = new AEC.UI.gtButton();
            this.sliderClipRelease = new AEC.UI.HSliderParam();
            this.sliderClipThreshold = new AEC.UI.HSliderParam();
            this.sliderRelease = new AEC.UI.HSliderParam();
            this.sliderAttack = new AEC.UI.HSliderParam();
            this.lblClipRelease = new AEC.UI.gtLabel();
            this.sliderThreshold = new AEC.UI.HSliderParam();
            this.lblClipThreshold = new AEC.UI.gtLabel();
            this.lblRelease = new AEC.UI.gtLabel();
            this.lblAttack = new AEC.UI.gtLabel();
            this.lblThreshold = new AEC.UI.gtLabel();
            this.SuspendLayout();
            // 
            // btnSwitchUnit
            // 
            this.btnSwitchUnit.BackColor = System.Drawing.Color.Transparent;
            this.btnSwitchUnit.backDisabled = global::AEC.Properties.Resources.switch_unit_disabled;
            this.btnSwitchUnit.backHover = global::AEC.Properties.Resources.switch_unit_hover;
            this.btnSwitchUnit.backNormal = global::AEC.Properties.Resources.switch_unit_normal;
            this.btnSwitchUnit.backPressed = global::AEC.Properties.Resources.switch_unit_pressed;
            this.btnSwitchUnit.ForeColor = System.Drawing.Color.White;
            this.btnSwitchUnit.isStretch = false;
            this.btnSwitchUnit.Location = new System.Drawing.Point(12, 12);
            this.btnSwitchUnit.Name = "btnSwitchUnit";
            this.btnSwitchUnit.Size = new System.Drawing.Size(18, 18);
            this.btnSwitchUnit.TabIndex = 7;
            this.btnSwitchUnit.userGDIPaint = false;
            this.btnSwitchUnit.Click += new System.EventHandler(this.btnSwitchUnit_Click);
            // 
            // sliderClipRelease
            // 
            this.sliderClipRelease.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderClipRelease.BackColor = System.Drawing.Color.Transparent;
            this.sliderClipRelease.backgroundDisabled = null;
            this.sliderClipRelease.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.sliderClipRelease.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderClipRelease.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderClipRelease.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderClipRelease.Location = new System.Drawing.Point(112, 122);
            this.sliderClipRelease.Margin = new System.Windows.Forms.Padding(0);
            this.sliderClipRelease.Name = "sliderClipRelease";
            this.sliderClipRelease.Size = new System.Drawing.Size(210, 23);
            this.sliderClipRelease.SliderWidth = 122;
            this.sliderClipRelease.TabIndex = 15;
            this.sliderClipRelease.thumbCenterY = 11;
            this.sliderClipRelease.ThumbEndX = 140;
            this.sliderClipRelease.upperLimit = ((short)(100));
            this.sliderClipRelease.value = ((short)(0));
            // 
            // sliderClipThreshold
            // 
            this.sliderClipThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderClipThreshold.BackColor = System.Drawing.Color.Transparent;
            this.sliderClipThreshold.backgroundDisabled = null;
            this.sliderClipThreshold.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.sliderClipThreshold.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderClipThreshold.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderClipThreshold.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderClipThreshold.Location = new System.Drawing.Point(112, 94);
            this.sliderClipThreshold.Margin = new System.Windows.Forms.Padding(0);
            this.sliderClipThreshold.Name = "sliderClipThreshold";
            this.sliderClipThreshold.Size = new System.Drawing.Size(210, 23);
            this.sliderClipThreshold.SliderWidth = 122;
            this.sliderClipThreshold.TabIndex = 15;
            this.sliderClipThreshold.thumbCenterY = 11;
            this.sliderClipThreshold.ThumbEndX = 140;
            this.sliderClipThreshold.upperLimit = ((short)(100));
            this.sliderClipThreshold.value = ((short)(0));
            // 
            // sliderRelease
            // 
            this.sliderRelease.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderRelease.BackColor = System.Drawing.Color.Transparent;
            this.sliderRelease.backgroundDisabled = null;
            this.sliderRelease.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.sliderRelease.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderRelease.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderRelease.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderRelease.Location = new System.Drawing.Point(112, 66);
            this.sliderRelease.Margin = new System.Windows.Forms.Padding(0);
            this.sliderRelease.Name = "sliderRelease";
            this.sliderRelease.Size = new System.Drawing.Size(210, 23);
            this.sliderRelease.SliderWidth = 122;
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
            this.sliderAttack.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.sliderAttack.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderAttack.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderAttack.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderAttack.Location = new System.Drawing.Point(112, 38);
            this.sliderAttack.Margin = new System.Windows.Forms.Padding(0);
            this.sliderAttack.Name = "sliderAttack";
            this.sliderAttack.Size = new System.Drawing.Size(210, 23);
            this.sliderAttack.SliderWidth = 122;
            this.sliderAttack.TabIndex = 17;
            this.sliderAttack.thumbCenterY = 11;
            this.sliderAttack.ThumbEndX = 140;
            this.sliderAttack.upperLimit = ((short)(100));
            this.sliderAttack.value = ((short)(0));
            // 
            // lblClipRelease
            // 
            this.lblClipRelease.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblClipRelease.BackColor = System.Drawing.Color.Transparent;
            this.lblClipRelease.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblClipRelease.Location = new System.Drawing.Point(20, 122);
            this.lblClipRelease.Name = "lblClipRelease";
            this.lblClipRelease.Size = new System.Drawing.Size(90, 23);
            this.lblClipRelease.TabIndex = 11;
            this.lblClipRelease.Text = "削波恢复时间";
            this.lblClipRelease.MouseUp += new System.Windows.Forms.MouseEventHandler(this.KXLimiterPanel_MouseUp);
            // 
            // sliderThreshold
            // 
            this.sliderThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderThreshold.BackColor = System.Drawing.Color.Transparent;
            this.sliderThreshold.backgroundDisabled = null;
            this.sliderThreshold.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.sliderThreshold.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderThreshold.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderThreshold.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderThreshold.Location = new System.Drawing.Point(112, 10);
            this.sliderThreshold.Margin = new System.Windows.Forms.Padding(0);
            this.sliderThreshold.Name = "sliderThreshold";
            this.sliderThreshold.Size = new System.Drawing.Size(210, 23);
            this.sliderThreshold.SliderWidth = 123;
            this.sliderThreshold.TabIndex = 18;
            this.sliderThreshold.thumbCenterY = 11;
            this.sliderThreshold.ThumbEndX = 140;
            this.sliderThreshold.upperLimit = ((short)(100));
            this.sliderThreshold.value = ((short)(0));
            // 
            // lblClipThreshold
            // 
            this.lblClipThreshold.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblClipThreshold.BackColor = System.Drawing.Color.Transparent;
            this.lblClipThreshold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblClipThreshold.Location = new System.Drawing.Point(20, 94);
            this.lblClipThreshold.Name = "lblClipThreshold";
            this.lblClipThreshold.Size = new System.Drawing.Size(90, 23);
            this.lblClipThreshold.TabIndex = 11;
            this.lblClipThreshold.Text = "削波启动电平";
            this.lblClipThreshold.MouseUp += new System.Windows.Forms.MouseEventHandler(this.KXLimiterPanel_MouseUp);
            // 
            // lblRelease
            // 
            this.lblRelease.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblRelease.BackColor = System.Drawing.Color.Transparent;
            this.lblRelease.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblRelease.Location = new System.Drawing.Point(20, 66);
            this.lblRelease.Name = "lblRelease";
            this.lblRelease.Size = new System.Drawing.Size(90, 23);
            this.lblRelease.TabIndex = 12;
            this.lblRelease.Text = "恢复时间";
            this.lblRelease.MouseUp += new System.Windows.Forms.MouseEventHandler(this.KXLimiterPanel_MouseUp);
            // 
            // lblAttack
            // 
            this.lblAttack.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblAttack.BackColor = System.Drawing.Color.Transparent;
            this.lblAttack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblAttack.Location = new System.Drawing.Point(20, 38);
            this.lblAttack.Name = "lblAttack";
            this.lblAttack.Size = new System.Drawing.Size(90, 23);
            this.lblAttack.TabIndex = 13;
            this.lblAttack.Text = "启动时间";
            this.lblAttack.MouseUp += new System.Windows.Forms.MouseEventHandler(this.KXLimiterPanel_MouseUp);
            // 
            // lblThreshold
            // 
            this.lblThreshold.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblThreshold.BackColor = System.Drawing.Color.Transparent;
            this.lblThreshold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblThreshold.Location = new System.Drawing.Point(20, 12);
            this.lblThreshold.Name = "lblThreshold";
            this.lblThreshold.Size = new System.Drawing.Size(90, 23);
            this.lblThreshold.TabIndex = 14;
            this.lblThreshold.Text = "启动电平";
            this.lblThreshold.MouseUp += new System.Windows.Forms.MouseEventHandler(this.KXLimiterPanel_MouseUp);
            // 
            // VKxSLimiterPanel
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.btnSwitchUnit);
            this.Controls.Add(this.sliderClipRelease);
            this.Controls.Add(this.sliderClipThreshold);
            this.Controls.Add(this.sliderRelease);
            this.Controls.Add(this.sliderAttack);
            this.Controls.Add(this.lblClipRelease);
            this.Controls.Add(this.sliderThreshold);
            this.Controls.Add(this.lblClipThreshold);
            this.Controls.Add(this.lblRelease);
            this.Controls.Add(this.lblAttack);
            this.Controls.Add(this.lblThreshold);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "VKxSLimiterPanel";
            this.Size = new System.Drawing.Size(331, 154);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.KXLimiterPanel_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private gtLabel lblClipThreshold;
        private gtLabel lblRelease;
        private gtLabel lblAttack;
        private gtLabel lblThreshold;
        private HSliderParam sliderClipThreshold;
        private HSliderParam sliderRelease;
        private HSliderParam sliderAttack;
        private HSliderParam sliderThreshold;
        private gtLabel lblClipRelease;
        private HSliderParam sliderClipRelease;
        private gtButton btnSwitchUnit;
    }
}
