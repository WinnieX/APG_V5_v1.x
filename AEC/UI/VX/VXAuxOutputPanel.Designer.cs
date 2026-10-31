namespace AEC.UI.VX
{
    partial class VXAuxOutputPanel
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
            this.panelDelayAndMute.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkMute
            // 
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
            this.chkMute.Location = new System.Drawing.Point(89, 53);
            this.chkMute.Margin = new System.Windows.Forms.Padding(0);
            this.chkMute.Name = "chkMute";
            this.chkMute.Size = new System.Drawing.Size(96, 30);
            this.chkMute.TabIndex = 30;
            this.chkMute.Text = "静音";
            this.chkMute.userGDIPaint = true;
            // 
            // sliderDelay
            // 
            this.sliderDelay.BackColor = System.Drawing.Color.Transparent;
            this.sliderDelay.backgroundDisabled = null;
            this.sliderDelay.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.sliderDelay.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderDelay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderDelay.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderDelay.Location = new System.Drawing.Point(82, 15);
            this.sliderDelay.Margin = new System.Windows.Forms.Padding(0);
            this.sliderDelay.Name = "sliderDelay";
            this.sliderDelay.Size = new System.Drawing.Size(181, 23);
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
            this.lblDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblDelay.Location = new System.Drawing.Point(0, 15);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(78, 23);
            this.lblDelay.TabIndex = 28;
            this.lblDelay.Text = "延时";
            // 
            // panelDelayAndMute
            // 
            this.panelDelayAndMute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelDelayAndMute.Controls.Add(this.sliderDelay);
            this.panelDelayAndMute.Controls.Add(this.chkMute);
            this.panelDelayAndMute.Controls.Add(this.lblDelay);
            this.panelDelayAndMute.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelDelayAndMute.ForeColor = System.Drawing.Color.LightGray;
            this.panelDelayAndMute.Location = new System.Drawing.Point(346, 200);
            this.panelDelayAndMute.Name = "panelDelayAndMute";
            this.panelDelayAndMute.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelDelayAndMute.Radius = 0;
            this.panelDelayAndMute.RectColor = System.Drawing.Color.Black;
            this.panelDelayAndMute.ShowRect = false;
            this.panelDelayAndMute.Size = new System.Drawing.Size(274, 96);
            this.panelDelayAndMute.TabIndex = 31;
            this.panelDelayAndMute.title = null;
            this.panelDelayAndMute.titleHeight = 20;
            // 
            // VXAuxOutputPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panelDelayAndMute);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "VXAuxOutputPanel";
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


    }
}
