namespace AEC.UI.VKxPIIDCM
{
    partial class VKxPIIDCMBassExciterPanel
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
            this.btnResetBoostLevel = new AEC.UI.gtButton();
            this.btnResetFBGain = new AEC.UI.gtButton();
            this.btnResetXoverFreq = new AEC.UI.gtButton();
            this.lblExciterBoostLevel = new AEC.UI.gtLabel();
            this.sliderExciterBoostLevel = new AEC.UI.HSliderParam();
            this.lblExciterFBGain = new AEC.UI.gtLabel();
            this.sliderExciterFBGain = new AEC.UI.HSliderParam();
            this.lblExciterXoverFreq = new AEC.UI.gtLabel();
            this.sliderExciterXoverFreq = new AEC.UI.HSliderParam();
            this.gtLabel1 = new AEC.UI.gtLabel();
            this.chkBassExciterOn = new AEC.UI.Controls.MSwitch();
            this.SuspendLayout();
            // 
            // btnResetBoostLevel
            // 
            this.btnResetBoostLevel.BackColor = System.Drawing.Color.Transparent;
            this.btnResetBoostLevel.backDisabled = global::AEC.Properties.Resources.switch_unit_disabled;
            this.btnResetBoostLevel.backHover = global::AEC.Properties.Resources.switch_unit_hover;
            this.btnResetBoostLevel.backNormal = global::AEC.Properties.Resources.switch_unit_normal;
            this.btnResetBoostLevel.backPressed = global::AEC.Properties.Resources.switch_unit_pressed;
            this.btnResetBoostLevel.ForeColor = System.Drawing.Color.White;
            this.btnResetBoostLevel.isStretch = false;
            this.btnResetBoostLevel.Location = new System.Drawing.Point(4, 105);
            this.btnResetBoostLevel.Name = "btnResetBoostLevel";
            this.btnResetBoostLevel.Size = new System.Drawing.Size(18, 18);
            this.btnResetBoostLevel.TabIndex = 53;
            this.btnResetBoostLevel.userGDIPaint = false;
            this.btnResetBoostLevel.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnResetFBGain
            // 
            this.btnResetFBGain.BackColor = System.Drawing.Color.Transparent;
            this.btnResetFBGain.backDisabled = global::AEC.Properties.Resources.switch_unit_disabled;
            this.btnResetFBGain.backHover = global::AEC.Properties.Resources.switch_unit_hover;
            this.btnResetFBGain.backNormal = global::AEC.Properties.Resources.switch_unit_normal;
            this.btnResetFBGain.backPressed = global::AEC.Properties.Resources.switch_unit_pressed;
            this.btnResetFBGain.ForeColor = System.Drawing.Color.White;
            this.btnResetFBGain.isStretch = false;
            this.btnResetFBGain.Location = new System.Drawing.Point(4, 78);
            this.btnResetFBGain.Name = "btnResetFBGain";
            this.btnResetFBGain.Size = new System.Drawing.Size(18, 18);
            this.btnResetFBGain.TabIndex = 52;
            this.btnResetFBGain.userGDIPaint = false;
            this.btnResetFBGain.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnResetXoverFreq
            // 
            this.btnResetXoverFreq.BackColor = System.Drawing.Color.Transparent;
            this.btnResetXoverFreq.backDisabled = global::AEC.Properties.Resources.switch_unit_disabled;
            this.btnResetXoverFreq.backHover = global::AEC.Properties.Resources.switch_unit_hover;
            this.btnResetXoverFreq.backNormal = global::AEC.Properties.Resources.switch_unit_normal;
            this.btnResetXoverFreq.backPressed = global::AEC.Properties.Resources.switch_unit_pressed;
            this.btnResetXoverFreq.ForeColor = System.Drawing.Color.White;
            this.btnResetXoverFreq.isStretch = false;
            this.btnResetXoverFreq.Location = new System.Drawing.Point(4, 51);
            this.btnResetXoverFreq.Name = "btnResetXoverFreq";
            this.btnResetXoverFreq.Size = new System.Drawing.Size(18, 18);
            this.btnResetXoverFreq.TabIndex = 51;
            this.btnResetXoverFreq.userGDIPaint = false;
            this.btnResetXoverFreq.Click += new System.EventHandler(this.btn_Click);
            // 
            // lblExciterBoostLevel
            // 
            this.lblExciterBoostLevel.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblExciterBoostLevel.BackColor = System.Drawing.Color.Transparent;
            this.lblExciterBoostLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblExciterBoostLevel.Location = new System.Drawing.Point(8, 102);
            this.lblExciterBoostLevel.Name = "lblExciterBoostLevel";
            this.lblExciterBoostLevel.Size = new System.Drawing.Size(92, 23);
            this.lblExciterBoostLevel.TabIndex = 50;
            this.lblExciterBoostLevel.Text = "电平";
            // 
            // sliderExciterBoostLevel
            // 
            this.sliderExciterBoostLevel.BackColor = System.Drawing.Color.Transparent;
            this.sliderExciterBoostLevel.backgroundDisabled = null;
            this.sliderExciterBoostLevel.backgroundNormal = global::AEC.Properties.Resources.mic_slider_back;
            this.sliderExciterBoostLevel.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderExciterBoostLevel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderExciterBoostLevel.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderExciterBoostLevel.Location = new System.Drawing.Point(104, 102);
            this.sliderExciterBoostLevel.Margin = new System.Windows.Forms.Padding(0);
            this.sliderExciterBoostLevel.Name = "sliderExciterBoostLevel";
            this.sliderExciterBoostLevel.Size = new System.Drawing.Size(232, 23);
            this.sliderExciterBoostLevel.SliderWidth = 152;
            this.sliderExciterBoostLevel.TabIndex = 49;
            this.sliderExciterBoostLevel.thumbCenterY = 11;
            this.sliderExciterBoostLevel.ThumbEndX = 140;
            this.sliderExciterBoostLevel.upperLimit = ((short)(100));
            this.sliderExciterBoostLevel.value = ((short)(0));
            // 
            // lblExciterFBGain
            // 
            this.lblExciterFBGain.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblExciterFBGain.BackColor = System.Drawing.Color.Transparent;
            this.lblExciterFBGain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblExciterFBGain.Location = new System.Drawing.Point(8, 75);
            this.lblExciterFBGain.Name = "lblExciterFBGain";
            this.lblExciterFBGain.Size = new System.Drawing.Size(92, 23);
            this.lblExciterFBGain.TabIndex = 48;
            this.lblExciterFBGain.Text = "深度";
            // 
            // sliderExciterFBGain
            // 
            this.sliderExciterFBGain.BackColor = System.Drawing.Color.Transparent;
            this.sliderExciterFBGain.backgroundDisabled = null;
            this.sliderExciterFBGain.backgroundNormal = global::AEC.Properties.Resources.mic_slider_back;
            this.sliderExciterFBGain.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderExciterFBGain.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderExciterFBGain.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderExciterFBGain.Location = new System.Drawing.Point(104, 75);
            this.sliderExciterFBGain.Margin = new System.Windows.Forms.Padding(0);
            this.sliderExciterFBGain.Name = "sliderExciterFBGain";
            this.sliderExciterFBGain.Size = new System.Drawing.Size(232, 23);
            this.sliderExciterFBGain.SliderWidth = 152;
            this.sliderExciterFBGain.TabIndex = 47;
            this.sliderExciterFBGain.thumbCenterY = 11;
            this.sliderExciterFBGain.ThumbEndX = 140;
            this.sliderExciterFBGain.upperLimit = ((short)(100));
            this.sliderExciterFBGain.value = ((short)(0));
            // 
            // lblExciterXoverFreq
            // 
            this.lblExciterXoverFreq.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblExciterXoverFreq.BackColor = System.Drawing.Color.Transparent;
            this.lblExciterXoverFreq.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblExciterXoverFreq.Location = new System.Drawing.Point(8, 48);
            this.lblExciterXoverFreq.Name = "lblExciterXoverFreq";
            this.lblExciterXoverFreq.Size = new System.Drawing.Size(92, 23);
            this.lblExciterXoverFreq.TabIndex = 46;
            this.lblExciterXoverFreq.Text = "分频点";
            // 
            // sliderExciterXoverFreq
            // 
            this.sliderExciterXoverFreq.BackColor = System.Drawing.Color.Transparent;
            this.sliderExciterXoverFreq.backgroundDisabled = null;
            this.sliderExciterXoverFreq.backgroundNormal = global::AEC.Properties.Resources.mic_slider_back;
            this.sliderExciterXoverFreq.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderExciterXoverFreq.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderExciterXoverFreq.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderExciterXoverFreq.Location = new System.Drawing.Point(104, 48);
            this.sliderExciterXoverFreq.Margin = new System.Windows.Forms.Padding(0);
            this.sliderExciterXoverFreq.Name = "sliderExciterXoverFreq";
            this.sliderExciterXoverFreq.Size = new System.Drawing.Size(232, 23);
            this.sliderExciterXoverFreq.SliderWidth = 152;
            this.sliderExciterXoverFreq.TabIndex = 45;
            this.sliderExciterXoverFreq.thumbCenterY = 11;
            this.sliderExciterXoverFreq.ThumbEndX = 140;
            this.sliderExciterXoverFreq.upperLimit = ((short)(100));
            this.sliderExciterXoverFreq.value = ((short)(0));
            // 
            // gtLabel1
            // 
            this.gtLabel1.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.gtLabel1.BackColor = System.Drawing.Color.Transparent;
            this.gtLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gtLabel1.Location = new System.Drawing.Point(8, 60);
            this.gtLabel1.Name = "gtLabel1";
            this.gtLabel1.Size = new System.Drawing.Size(64, 25);
            this.gtLabel1.TabIndex = 41;
            // 
            // chkBassExciterOn
            // 
            this.chkBassExciterOn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkBassExciterOn.BorderWidth = 1F;
            this.chkBassExciterOn.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkBassExciterOn.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(74)))), ((int)(((byte)(52)))));
            this.chkBassExciterOn.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkBassExciterOn.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkBassExciterOn.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(78)))), ((int)(((byte)(15)))));
            this.chkBassExciterOn.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkBassExciterOn.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkBassExciterOn.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(69)))), ((int)(((byte)(14)))));
            this.chkBassExciterOn.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkBassExciterOn.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkBassExciterOn.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(60)))), ((int)(((byte)(11)))));
            this.chkBassExciterOn.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkBassExciterOn.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.chkBassExciterOn.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.chkBassExciterOn.isAutoCheck = true;
            this.chkBassExciterOn.IsChecked = false;
            this.chkBassExciterOn.IsCheckedInit = false;
            this.chkBassExciterOn.Location = new System.Drawing.Point(104, 16);
            this.chkBassExciterOn.Name = "chkBassExciterOn";
            this.chkBassExciterOn.Radius = 22;
            this.chkBassExciterOn.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.chkBassExciterOn.ShowBorder = false;
            this.chkBassExciterOn.ShowText = true;
            this.chkBassExciterOn.Size = new System.Drawing.Size(40, 22);
            this.chkBassExciterOn.SwitchWdith = 40;
            this.chkBassExciterOn.TabIndex = 39;
            this.chkBassExciterOn.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkBassExciterOn.UncheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkBassExciterOn.UncheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkBassExciterOn.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkBassExciterOn.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.chkBassExciterOn.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chkBassExciterOn.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkBassExciterOn.UncheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkBassExciterOn.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkBassExciterOn.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.chkBassExciterOn.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.chkBassExciterOn.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.chkBassExciterOn.CheckedChanged += new System.EventHandler(this.chkBassExciterOn_CheckedChanged);
            // 
            // VKxPIIBassExciterPanel
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.btnResetBoostLevel);
            this.Controls.Add(this.btnResetFBGain);
            this.Controls.Add(this.btnResetXoverFreq);
            this.Controls.Add(this.lblExciterBoostLevel);
            this.Controls.Add(this.sliderExciterBoostLevel);
            this.Controls.Add(this.lblExciterFBGain);
            this.Controls.Add(this.sliderExciterFBGain);
            this.Controls.Add(this.lblExciterXoverFreq);
            this.Controls.Add(this.sliderExciterXoverFreq);
            this.Controls.Add(this.gtLabel1);
            this.Controls.Add(this.chkBassExciterOn);
            this.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "VKxPIIBassExciterPanel";
            this.ShowBorder = false;
            this.ShowText = false;
            this.Size = new System.Drawing.Size(342, 136);
            this.ResumeLayout(false);

        }

        #endregion

        private gtLabel gtLabel1;
        private Controls.MSwitch chkBassExciterOn;
        private gtLabel lblExciterFBGain;
        private HSliderParam sliderExciterFBGain;
        private gtLabel lblExciterXoverFreq;
        private HSliderParam sliderExciterXoverFreq;
        private gtLabel lblExciterBoostLevel;
        private HSliderParam sliderExciterBoostLevel;
        private gtButton btnResetBoostLevel;
        private gtButton btnResetFBGain;
        private gtButton btnResetXoverFreq;

    }
}
