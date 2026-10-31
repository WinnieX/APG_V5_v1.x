namespace AEC.UI.VKxPII
{
    partial class VKxPIIMixerPanel
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
            this.mixerReverb = new AEC.UI.Common.X5OutputSignalMixerPanel();
            this.mixerEcho = new AEC.UI.Common.X5OutputSignalMixerPanel();
            this.mixerMic = new AEC.UI.Common.X5OutputSignalMixerPanel();
            this.mixerMusic = new AEC.UI.Common.X5OutputSignalMixerPanel();
            this.lblReverbLevel = new AEC.UI.gtLabel();
            this.lblEchoLevel = new AEC.UI.gtLabel();
            this.lblMicLevel = new AEC.UI.gtLabel();
            this.lblMusicLevel = new AEC.UI.gtLabel();
            this.btnSwitchUnit = new AEC.UI.gtButton();
            this.SuspendLayout();
            // 
            // mixerReverb
            // 
            this.mixerReverb.BackColor = System.Drawing.Color.Transparent;
            this.mixerReverb.backgroundDisabled = null;
            this.mixerReverb.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.mixerReverb.backLeftTop = new System.Drawing.Point(0, 6);
            this.mixerReverb.FillColor = System.Drawing.Color.Transparent;
            this.mixerReverb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.mixerReverb.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.mixerReverb.Location = new System.Drawing.Point(112, 102);
            this.mixerReverb.Name = "mixerReverb";
            this.mixerReverb.Size = new System.Drawing.Size(210, 23);
            this.mixerReverb.SliderWidth = 114;
            this.mixerReverb.TabIndex = 25;
            this.mixerReverb.thumbCenterY = 11;
            this.mixerReverb.ThumbEndX = 92;
            // 
            // mixerEcho
            // 
            this.mixerEcho.BackColor = System.Drawing.Color.Transparent;
            this.mixerEcho.backgroundDisabled = null;
            this.mixerEcho.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.mixerEcho.backLeftTop = new System.Drawing.Point(0, 6);
            this.mixerEcho.FillColor = System.Drawing.Color.Transparent;
            this.mixerEcho.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.mixerEcho.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.mixerEcho.Location = new System.Drawing.Point(112, 72);
            this.mixerEcho.Name = "mixerEcho";
            this.mixerEcho.Size = new System.Drawing.Size(210, 23);
            this.mixerEcho.SliderWidth = 114;
            this.mixerEcho.TabIndex = 25;
            this.mixerEcho.thumbCenterY = 11;
            this.mixerEcho.ThumbEndX = 92;
            // 
            // mixerMic
            // 
            this.mixerMic.BackColor = System.Drawing.Color.Transparent;
            this.mixerMic.backgroundDisabled = null;
            this.mixerMic.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.mixerMic.backLeftTop = new System.Drawing.Point(0, 6);
            this.mixerMic.FillColor = System.Drawing.Color.Transparent;
            this.mixerMic.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.mixerMic.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.mixerMic.Location = new System.Drawing.Point(112, 42);
            this.mixerMic.Name = "mixerMic";
            this.mixerMic.Size = new System.Drawing.Size(210, 23);
            this.mixerMic.SliderWidth = 114;
            this.mixerMic.TabIndex = 25;
            this.mixerMic.thumbCenterY = 11;
            this.mixerMic.ThumbEndX = 92;
            // 
            // mixerMusic
            // 
            this.mixerMusic.BackColor = System.Drawing.Color.Transparent;
            this.mixerMusic.backgroundDisabled = null;
            this.mixerMusic.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.mixerMusic.backLeftTop = new System.Drawing.Point(0, 6);
            this.mixerMusic.FillColor = System.Drawing.Color.Transparent;
            this.mixerMusic.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.mixerMusic.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.mixerMusic.Location = new System.Drawing.Point(112, 12);
            this.mixerMusic.Name = "mixerMusic";
            this.mixerMusic.Size = new System.Drawing.Size(210, 23);
            this.mixerMusic.SliderWidth = 114;
            this.mixerMusic.TabIndex = 25;
            this.mixerMusic.thumbCenterY = 11;
            this.mixerMusic.ThumbEndX = 92;
            // 
            // lblReverbLevel
            // 
            this.lblReverbLevel.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblReverbLevel.BackColor = System.Drawing.Color.Transparent;
            this.lblReverbLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblReverbLevel.Location = new System.Drawing.Point(20, 102);
            this.lblReverbLevel.Margin = new System.Windows.Forms.Padding(0);
            this.lblReverbLevel.Name = "lblReverbLevel";
            this.lblReverbLevel.Size = new System.Drawing.Size(88, 23);
            this.lblReverbLevel.TabIndex = 19;
            this.lblReverbLevel.Text = "混响电平";
            this.lblReverbLevel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.KXMixerPanel_MouseUp);
            // 
            // lblEchoLevel
            // 
            this.lblEchoLevel.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEchoLevel.BackColor = System.Drawing.Color.Transparent;
            this.lblEchoLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEchoLevel.Location = new System.Drawing.Point(20, 72);
            this.lblEchoLevel.Margin = new System.Windows.Forms.Padding(0);
            this.lblEchoLevel.Name = "lblEchoLevel";
            this.lblEchoLevel.Size = new System.Drawing.Size(88, 23);
            this.lblEchoLevel.TabIndex = 20;
            this.lblEchoLevel.Text = "回声电平";
            this.lblEchoLevel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.KXMixerPanel_MouseUp);
            // 
            // lblMicLevel
            // 
            this.lblMicLevel.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicLevel.BackColor = System.Drawing.Color.Transparent;
            this.lblMicLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMicLevel.Location = new System.Drawing.Point(20, 42);
            this.lblMicLevel.Margin = new System.Windows.Forms.Padding(0);
            this.lblMicLevel.Name = "lblMicLevel";
            this.lblMicLevel.Size = new System.Drawing.Size(88, 23);
            this.lblMicLevel.TabIndex = 21;
            this.lblMicLevel.Text = "麦克风电平";
            this.lblMicLevel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.KXMixerPanel_MouseUp);
            // 
            // lblMusicLevel
            // 
            this.lblMusicLevel.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMusicLevel.BackColor = System.Drawing.Color.Transparent;
            this.lblMusicLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMusicLevel.Location = new System.Drawing.Point(20, 12);
            this.lblMusicLevel.Margin = new System.Windows.Forms.Padding(0);
            this.lblMusicLevel.Name = "lblMusicLevel";
            this.lblMusicLevel.Size = new System.Drawing.Size(88, 23);
            this.lblMusicLevel.TabIndex = 22;
            this.lblMusicLevel.Text = "音乐电平";
            this.lblMusicLevel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.KXMixerPanel_MouseUp);
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
            this.btnSwitchUnit.Location = new System.Drawing.Point(12, 14);
            this.btnSwitchUnit.Name = "btnSwitchUnit";
            this.btnSwitchUnit.Size = new System.Drawing.Size(18, 18);
            this.btnSwitchUnit.TabIndex = 26;
            this.btnSwitchUnit.userGDIPaint = false;
            this.btnSwitchUnit.Click += new System.EventHandler(this.btnSwitchUnit_Click);
            // 
            // KXMixerPanel
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnSwitchUnit);
            this.Controls.Add(this.mixerReverb);
            this.Controls.Add(this.mixerEcho);
            this.Controls.Add(this.mixerMic);
            this.Controls.Add(this.mixerMusic);
            this.Controls.Add(this.lblReverbLevel);
            this.Controls.Add(this.lblEchoLevel);
            this.Controls.Add(this.lblMicLevel);
            this.Controls.Add(this.lblMusicLevel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "KXMixerPanel";
            this.Size = new System.Drawing.Size(331, 136);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.KXMixerPanel_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private gtLabel lblReverbLevel;
        private gtLabel lblEchoLevel;
        private gtLabel lblMicLevel;
        private gtLabel lblMusicLevel;
        private Common.X5OutputSignalMixerPanel mixerMusic;
        private Common.X5OutputSignalMixerPanel mixerMic;
        private Common.X5OutputSignalMixerPanel mixerEcho;
        private Common.X5OutputSignalMixerPanel mixerReverb;
        private gtButton btnSwitchUnit;
    }
}
