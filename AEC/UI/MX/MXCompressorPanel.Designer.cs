namespace AEC.UI.MX
{
    partial class MXCompressorPanel
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
            this.lblRatio = new AEC.UI.gtLabel();
            this.lblRelease = new AEC.UI.gtLabel();
            this.lblAttack = new AEC.UI.gtLabel();
            this.lblThreshold = new AEC.UI.gtLabel();
            this.lblMode = new AEC.UI.gtLabel();
            this.rdoSlow = new AEC.UI.gtRadioButton();
            this.rdoMedium = new AEC.UI.gtRadioButton();
            this.rdoFast = new AEC.UI.gtRadioButton();
            this.btnSwitchUnit = new AEC.UI.gtButton();
            this.sliderRatio = new AEC.UI.HSliderParam();
            this.sliderRelease = new AEC.UI.HSliderParam();
            this.sliderAttack = new AEC.UI.HSliderParam();
            this.sliderThreshold = new AEC.UI.HSliderParam();
            this.SuspendLayout();
            // 
            // lblRatio
            // 
            this.lblRatio.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblRatio.BackColor = System.Drawing.Color.Transparent;
            this.lblRatio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblRatio.Location = new System.Drawing.Point(32, 95);
            this.lblRatio.Name = "lblRatio";
            this.lblRatio.Size = new System.Drawing.Size(90, 23);
            this.lblRatio.TabIndex = 11;
            this.lblRatio.Text = "压限比例";
            // 
            // lblRelease
            // 
            this.lblRelease.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblRelease.BackColor = System.Drawing.Color.Transparent;
            this.lblRelease.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblRelease.Location = new System.Drawing.Point(32, 66);
            this.lblRelease.Name = "lblRelease";
            this.lblRelease.Size = new System.Drawing.Size(90, 23);
            this.lblRelease.TabIndex = 12;
            this.lblRelease.Text = "恢复时间";
            // 
            // lblAttack
            // 
            this.lblAttack.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblAttack.BackColor = System.Drawing.Color.Transparent;
            this.lblAttack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblAttack.Location = new System.Drawing.Point(32, 37);
            this.lblAttack.Name = "lblAttack";
            this.lblAttack.Size = new System.Drawing.Size(90, 23);
            this.lblAttack.TabIndex = 13;
            this.lblAttack.Text = "启动时间";
            // 
            // lblThreshold
            // 
            this.lblThreshold.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblThreshold.BackColor = System.Drawing.Color.Transparent;
            this.lblThreshold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblThreshold.Location = new System.Drawing.Point(32, 8);
            this.lblThreshold.Name = "lblThreshold";
            this.lblThreshold.Size = new System.Drawing.Size(90, 23);
            this.lblThreshold.TabIndex = 14;
            this.lblThreshold.Text = "启动电平";
            // 
            // lblMode
            // 
            this.lblMode.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMode.BackColor = System.Drawing.Color.Transparent;
            this.lblMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMode.Location = new System.Drawing.Point(32, 127);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(90, 23);
            this.lblMode.TabIndex = 11;
            this.lblMode.Text = "压限模式";
            // 
            // rdoSlow
            // 
            this.rdoSlow.backChecked = global::AEC.Properties.Resources.rdo_cm_pressed;
            this.rdoSlow.BackColor = System.Drawing.Color.Transparent;
            this.rdoSlow.backDisabled = global::AEC.Properties.Resources.rdo_cm_disabled;
            this.rdoSlow.backHover = global::AEC.Properties.Resources.rdo_cm_hover;
            this.rdoSlow.backNormal = global::AEC.Properties.Resources.rdo_cm_normal;
            this.rdoSlow.isAutoCheck = true;
            this.rdoSlow.isChecked = false;
            this.rdoSlow.isCheckInit = false;
            this.rdoSlow.isShowCheckedBackWhenDsiabled = true;
            this.rdoSlow.isStretch = false;
            this.rdoSlow.Location = new System.Drawing.Point(254, 125);
            this.rdoSlow.Name = "rdoSlow";
            this.rdoSlow.Size = new System.Drawing.Size(53, 27);
            this.rdoSlow.TabIndex = 19;
            this.rdoSlow.Text = "慢速";
            this.rdoSlow.userGDIPaint = false;
            // 
            // rdoMedium
            // 
            this.rdoMedium.backChecked = global::AEC.Properties.Resources.rdo_cm_pressed;
            this.rdoMedium.BackColor = System.Drawing.Color.Transparent;
            this.rdoMedium.backDisabled = global::AEC.Properties.Resources.rdo_cm_disabled;
            this.rdoMedium.backHover = global::AEC.Properties.Resources.rdo_cm_hover;
            this.rdoMedium.backNormal = global::AEC.Properties.Resources.rdo_cm_normal;
            this.rdoMedium.isAutoCheck = true;
            this.rdoMedium.isChecked = false;
            this.rdoMedium.isCheckInit = false;
            this.rdoMedium.isShowCheckedBackWhenDsiabled = true;
            this.rdoMedium.isStretch = false;
            this.rdoMedium.Location = new System.Drawing.Point(190, 125);
            this.rdoMedium.Name = "rdoMedium";
            this.rdoMedium.Size = new System.Drawing.Size(53, 27);
            this.rdoMedium.TabIndex = 19;
            this.rdoMedium.Text = "中速";
            this.rdoMedium.userGDIPaint = false;
            // 
            // rdoFast
            // 
            this.rdoFast.backChecked = global::AEC.Properties.Resources.rdo_cm_pressed;
            this.rdoFast.BackColor = System.Drawing.Color.Transparent;
            this.rdoFast.backDisabled = global::AEC.Properties.Resources.rdo_cm_disabled;
            this.rdoFast.backHover = global::AEC.Properties.Resources.rdo_cm_hover;
            this.rdoFast.backNormal = global::AEC.Properties.Resources.rdo_cm_normal;
            this.rdoFast.isAutoCheck = true;
            this.rdoFast.isChecked = false;
            this.rdoFast.isCheckInit = false;
            this.rdoFast.isShowCheckedBackWhenDsiabled = true;
            this.rdoFast.isStretch = false;
            this.rdoFast.Location = new System.Drawing.Point(126, 125);
            this.rdoFast.Name = "rdoFast";
            this.rdoFast.Size = new System.Drawing.Size(53, 27);
            this.rdoFast.TabIndex = 19;
            this.rdoFast.Text = "快速";
            this.rdoFast.userGDIPaint = false;
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
            this.btnSwitchUnit.Location = new System.Drawing.Point(12, 10);
            this.btnSwitchUnit.Name = "btnSwitchUnit";
            this.btnSwitchUnit.Size = new System.Drawing.Size(18, 18);
            this.btnSwitchUnit.TabIndex = 7;
            this.btnSwitchUnit.userGDIPaint = false;
            this.btnSwitchUnit.Click += new System.EventHandler(this.btnSwitchUnit_Click);
            // 
            // sliderRatio
            // 
            this.sliderRatio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderRatio.BackColor = System.Drawing.Color.Transparent;
            this.sliderRatio.backgroundDisabled = null;
            this.sliderRatio.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.sliderRatio.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderRatio.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderRatio.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderRatio.Location = new System.Drawing.Point(126, 95);
            this.sliderRatio.Margin = new System.Windows.Forms.Padding(0);
            this.sliderRatio.Name = "sliderRatio";
            this.sliderRatio.Size = new System.Drawing.Size(197, 23);
            this.sliderRatio.SliderWidth = 109;
            this.sliderRatio.TabIndex = 15;
            this.sliderRatio.thumbCenterY = 11;
            this.sliderRatio.ThumbEndX = 92;
            this.sliderRatio.upperLimit = ((short)(100));
            this.sliderRatio.value = ((short)(0));
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
            this.sliderRelease.Location = new System.Drawing.Point(126, 66);
            this.sliderRelease.Margin = new System.Windows.Forms.Padding(0);
            this.sliderRelease.Name = "sliderRelease";
            this.sliderRelease.Size = new System.Drawing.Size(197, 23);
            this.sliderRelease.SliderWidth = 109;
            this.sliderRelease.TabIndex = 16;
            this.sliderRelease.thumbCenterY = 11;
            this.sliderRelease.ThumbEndX = 92;
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
            this.sliderAttack.Location = new System.Drawing.Point(126, 37);
            this.sliderAttack.Margin = new System.Windows.Forms.Padding(0);
            this.sliderAttack.Name = "sliderAttack";
            this.sliderAttack.Size = new System.Drawing.Size(197, 23);
            this.sliderAttack.SliderWidth = 109;
            this.sliderAttack.TabIndex = 17;
            this.sliderAttack.thumbCenterY = 11;
            this.sliderAttack.ThumbEndX = 92;
            this.sliderAttack.upperLimit = ((short)(100));
            this.sliderAttack.value = ((short)(0));
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
            this.sliderThreshold.Location = new System.Drawing.Point(126, 8);
            this.sliderThreshold.Margin = new System.Windows.Forms.Padding(0);
            this.sliderThreshold.Name = "sliderThreshold";
            this.sliderThreshold.Size = new System.Drawing.Size(197, 23);
            this.sliderThreshold.SliderWidth = 109;
            this.sliderThreshold.TabIndex = 18;
            this.sliderThreshold.thumbCenterY = 11;
            this.sliderThreshold.ThumbEndX = 92;
            this.sliderThreshold.upperLimit = ((short)(100));
            this.sliderThreshold.value = ((short)(0));
            // 
            // MXCompressorPanel
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.rdoSlow);
            this.Controls.Add(this.rdoMedium);
            this.Controls.Add(this.rdoFast);
            this.Controls.Add(this.btnSwitchUnit);
            this.Controls.Add(this.sliderRatio);
            this.Controls.Add(this.sliderRelease);
            this.Controls.Add(this.sliderAttack);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.sliderThreshold);
            this.Controls.Add(this.lblRatio);
            this.Controls.Add(this.lblRelease);
            this.Controls.Add(this.lblAttack);
            this.Controls.Add(this.lblThreshold);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MXCompressorPanel";
            this.Size = new System.Drawing.Size(331, 160);
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
        private gtLabel lblMode;
        private gtButton btnSwitchUnit;
        private gtRadioButton rdoFast;
        private gtRadioButton rdoMedium;
        private gtRadioButton rdoSlow;
    }
}
