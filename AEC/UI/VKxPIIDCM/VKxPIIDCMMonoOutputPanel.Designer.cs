namespace AEC.UI.VKxPIIDCM
{
    partial class VKxPIIDCMMonoOutputPanel
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
            this.chkMute = new AEC.UI.gtCheckBox();
            this.sliderDelay = new AEC.UI.HSliderParam();
            this.lblDelay = new AEC.UI.gtLabel();
            this.panelDelayAndMute = new AEC.UI.gtPanel();
            this.btnSwitchUnit = new AEC.UI.gtButton();
            this.chkPhase = new AEC.UI.gtCheckBox();
            this.panelDelayAndMute.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkMute
            // 
            this.chkMute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMute.BackColor = System.Drawing.Color.Transparent;
            this.chkMute.backDisabled = global::AEC.Properties.Resources.btn_mute_disabled;
            this.chkMute.backHover = global::AEC.Properties.Resources.btn_mute_hover;
            this.chkMute.backNormal = global::AEC.Properties.Resources.btn_mute_normal;
            this.chkMute.backPressed = global::AEC.Properties.Resources.btn_mute_pressed;
            this.chkMute.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkMute.isAutoCheck = true;
            this.chkMute.isChecked = false;
            this.chkMute.isShowCheckedBackWhenDsiabled = true;
            this.chkMute.isStretch = false;
            this.chkMute.Location = new System.Drawing.Point(89, 77);
            this.chkMute.Margin = new System.Windows.Forms.Padding(0);
            this.chkMute.Name = "chkMute";
            this.chkMute.Size = new System.Drawing.Size(96, 30);
            this.chkMute.TabIndex = 30;
            this.chkMute.Text = "静音";
            this.chkMute.userGDIPaint = true;
            // 
            // sliderDelay
            // 
            this.sliderDelay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sliderDelay.BackColor = System.Drawing.Color.Transparent;
            this.sliderDelay.backgroundDisabled = null;
            this.sliderDelay.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.sliderDelay.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderDelay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderDelay.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderDelay.Location = new System.Drawing.Point(79, 8);
            this.sliderDelay.Margin = new System.Windows.Forms.Padding(0);
            this.sliderDelay.Name = "sliderDelay";
            this.sliderDelay.Size = new System.Drawing.Size(189, 23);
            this.sliderDelay.SliderWidth = 104;
            this.sliderDelay.TabIndex = 29;
            this.sliderDelay.thumbCenterY = 11;
            this.sliderDelay.ThumbEndX = 92;
            this.sliderDelay.upperLimit = ((short)(100));
            this.sliderDelay.value = ((short)(0));
            // 
            // lblDelay
            // 
            this.lblDelay.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblDelay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblDelay.Location = new System.Drawing.Point(1, 8);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(76, 23);
            this.lblDelay.TabIndex = 28;
            this.lblDelay.Text = "延时";
            this.lblDelay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelDelayAndMute_MouseUp);
            // 
            // panelDelayAndMute
            // 
            this.panelDelayAndMute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelDelayAndMute.Controls.Add(this.btnSwitchUnit);
            this.panelDelayAndMute.Controls.Add(this.chkPhase);
            this.panelDelayAndMute.Controls.Add(this.sliderDelay);
            this.panelDelayAndMute.Controls.Add(this.chkMute);
            this.panelDelayAndMute.Controls.Add(this.lblDelay);
            this.panelDelayAndMute.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelDelayAndMute.ForeColor = System.Drawing.Color.LightGray;
            this.panelDelayAndMute.Location = new System.Drawing.Point(346, 178);
            this.panelDelayAndMute.Name = "panelDelayAndMute";
            this.panelDelayAndMute.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelDelayAndMute.Radius = 0;
            this.panelDelayAndMute.RectColor = System.Drawing.Color.Black;
            this.panelDelayAndMute.ShowRect = false;
            this.panelDelayAndMute.Size = new System.Drawing.Size(274, 124);
            this.panelDelayAndMute.TabIndex = 31;
            this.panelDelayAndMute.title = null;
            this.panelDelayAndMute.titleHeight = 20;
            this.panelDelayAndMute.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelDelayAndMute_MouseUp);
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
            this.btnSwitchUnit.TabIndex = 32;
            this.btnSwitchUnit.userGDIPaint = false;
            this.btnSwitchUnit.Click += new System.EventHandler(this.btnSwitchUnit_Click);
            // 
            // chkPhase
            // 
            this.chkPhase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkPhase.BackColor = System.Drawing.Color.Transparent;
            this.chkPhase.backDisabled = global::AEC.Properties.Resources.btn_mute_disabled;
            this.chkPhase.backHover = global::AEC.Properties.Resources.btn_mute_hover;
            this.chkPhase.backNormal = global::AEC.Properties.Resources.btn_mute_normal;
            this.chkPhase.backPressed = global::AEC.Properties.Resources.auto_btn_pressed;
            this.chkPhase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chkPhase.isAutoCheck = true;
            this.chkPhase.isChecked = false;
            this.chkPhase.isShowCheckedBackWhenDsiabled = true;
            this.chkPhase.isStretch = true;
            this.chkPhase.Location = new System.Drawing.Point(89, 41);
            this.chkPhase.Margin = new System.Windows.Forms.Padding(0);
            this.chkPhase.Name = "chkPhase";
            this.chkPhase.Size = new System.Drawing.Size(96, 30);
            this.chkPhase.TabIndex = 31;
            this.chkPhase.Text = "相位 +";
            this.chkPhase.userGDIPaint = true;
            // 
            // VKxPIIMonoOutputPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panelDelayAndMute);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "VKxPIIMonoOutputPanel";
            this.Controls.SetChildIndex(this.mixerPanel, 0);
            this.Controls.SetChildIndex(this.limiterPanel, 0);
            this.Controls.SetChildIndex(this.panelDelayAndMute, 0);
            this.panelDelayAndMute.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private gtCheckBox chkMute;
        protected HSliderParam sliderDelay;
        private gtLabel lblDelay;
        private AEC.UI.gtPanel panelDelayAndMute;
        private gtCheckBox chkPhase;
        private gtButton btnSwitchUnit;


    }
}
