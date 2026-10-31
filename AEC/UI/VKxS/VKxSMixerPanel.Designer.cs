namespace AEC.UI.VKxS
{
    partial class VKxSMixerPanel
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
            this.levelReverb = new AEC.UI.Common.X5OutputSignalMixerPanel();
            this.levelEcho = new AEC.UI.Common.X5OutputSignalMixerPanel();
            this.levelMic = new AEC.UI.Common.X5OutputSignalMixerPanel();
            this.levelMusic = new AEC.UI.Common.X5OutputSignalMixerPanel();
            this.lblReverbLevel = new AEC.UI.gtLabel();
            this.lblEchoLevel = new AEC.UI.gtLabel();
            this.lblMicLevel = new AEC.UI.gtLabel();
            this.lblMusicLevel = new AEC.UI.gtLabel();
            this.btnSwitchUnit = new AEC.UI.gtButton();
            this.SuspendLayout();
            // 
            // levelReverb
            // 
            this.levelReverb.BackColor = System.Drawing.Color.Transparent;
            this.levelReverb.backgroundDisabled = null;
            this.levelReverb.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.levelReverb.backLeftTop = new System.Drawing.Point(0, 6);
            this.levelReverb.FillColor = System.Drawing.Color.Transparent;
            this.levelReverb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.levelReverb.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.levelReverb.Location = new System.Drawing.Point(112, 102);
            this.levelReverb.Name = "levelReverb";
            this.levelReverb.Size = new System.Drawing.Size(210, 23);
            this.levelReverb.SliderWidth = 114;
            this.levelReverb.TabIndex = 25;
            this.levelReverb.thumbCenterY = 11;
            this.levelReverb.ThumbEndX = 92;
            // 
            // levelEcho
            // 
            this.levelEcho.BackColor = System.Drawing.Color.Transparent;
            this.levelEcho.backgroundDisabled = null;
            this.levelEcho.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.levelEcho.backLeftTop = new System.Drawing.Point(0, 6);
            this.levelEcho.FillColor = System.Drawing.Color.Transparent;
            this.levelEcho.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.levelEcho.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.levelEcho.Location = new System.Drawing.Point(112, 72);
            this.levelEcho.Name = "levelEcho";
            this.levelEcho.Size = new System.Drawing.Size(210, 23);
            this.levelEcho.SliderWidth = 114;
            this.levelEcho.TabIndex = 25;
            this.levelEcho.thumbCenterY = 11;
            this.levelEcho.ThumbEndX = 92;
            // 
            // levelMic
            // 
            this.levelMic.BackColor = System.Drawing.Color.Transparent;
            this.levelMic.backgroundDisabled = null;
            this.levelMic.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.levelMic.backLeftTop = new System.Drawing.Point(0, 6);
            this.levelMic.FillColor = System.Drawing.Color.Transparent;
            this.levelMic.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.levelMic.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.levelMic.Location = new System.Drawing.Point(112, 42);
            this.levelMic.Name = "levelMic";
            this.levelMic.Size = new System.Drawing.Size(210, 23);
            this.levelMic.SliderWidth = 114;
            this.levelMic.TabIndex = 25;
            this.levelMic.thumbCenterY = 11;
            this.levelMic.ThumbEndX = 92;
            // 
            // levelMusic
            // 
            this.levelMusic.BackColor = System.Drawing.Color.Transparent;
            this.levelMusic.backgroundDisabled = null;
            this.levelMusic.backgroundNormal = global::AEC.Properties.Resources.main_output_slider_back;
            this.levelMusic.backLeftTop = new System.Drawing.Point(0, 6);
            this.levelMusic.FillColor = System.Drawing.Color.Transparent;
            this.levelMusic.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.levelMusic.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.levelMusic.Location = new System.Drawing.Point(112, 12);
            this.levelMusic.Name = "levelMusic";
            this.levelMusic.Size = new System.Drawing.Size(210, 23);
            this.levelMusic.SliderWidth = 114;
            this.levelMusic.TabIndex = 25;
            this.levelMusic.thumbCenterY = 11;
            this.levelMusic.ThumbEndX = 92;
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
            this.lblReverbLevel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.VKxSMixerPanel_MouseUp);
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
            this.lblEchoLevel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.VKxSMixerPanel_MouseUp);
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
            this.lblMicLevel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.VKxSMixerPanel_MouseUp);
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
            this.lblMusicLevel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.VKxSMixerPanel_MouseUp);
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
            // VKxSMixerPanel
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnSwitchUnit);
            this.Controls.Add(this.levelReverb);
            this.Controls.Add(this.levelEcho);
            this.Controls.Add(this.levelMic);
            this.Controls.Add(this.levelMusic);
            this.Controls.Add(this.lblReverbLevel);
            this.Controls.Add(this.lblEchoLevel);
            this.Controls.Add(this.lblMicLevel);
            this.Controls.Add(this.lblMusicLevel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "VKxSMixerPanel";
            this.Size = new System.Drawing.Size(331, 136);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.VKxSMixerPanel_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private gtLabel lblReverbLevel;
        private gtLabel lblEchoLevel;
        private gtLabel lblMicLevel;
        private gtLabel lblMusicLevel;
        private Common.X5OutputSignalMixerPanel levelMusic;
        private Common.X5OutputSignalMixerPanel levelMic;
        private Common.X5OutputSignalMixerPanel levelEcho;
        private Common.X5OutputSignalMixerPanel levelReverb;
        private gtButton btnSwitchUnit;
    }
}
