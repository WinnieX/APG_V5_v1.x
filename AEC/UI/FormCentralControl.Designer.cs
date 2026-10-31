namespace AEC.UI
{
    partial class FormCentralControl
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCentralControl));
            this.cmbPorts = new AEC.UI.gtComboBox();
            this.lblPort = new AEC.UI.Controls.MLabel();
            this.btnStart = new AEC.UI.Controls.MButton();
            this.btnStop = new AEC.UI.Controls.MButton();
            this.linkRefresh = new System.Windows.Forms.LinkLabel();
            this.lblMusicStartVol = new AEC.UI.gtLabel();
            this.lblEffectStartVol = new AEC.UI.gtLabel();
            this.sliderEffectVol = new AEC.UI.HSliderParam();
            this.lblMicStartVol = new AEC.UI.gtLabel();
            this.sliderMicVol = new AEC.UI.HSliderParam();
            this.sliderMusicVol = new AEC.UI.HSliderParam();
            this.cmbPresets = new AEC.UI.gtComboBox();
            this.lblPreset = new AEC.UI.Controls.MLabel();
            this.cmbMusicInputPort = new AEC.UI.gtComboBox();
            this.lblMusicInputPort = new AEC.UI.Controls.MLabel();
            this.SuspendLayout();
            // 
            // cmbPorts
            // 
            this.cmbPorts.ArrowColor = System.Drawing.Color.Gray;
            this.cmbPorts.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPorts.FormattingEnabled = true;
            this.cmbPorts.Location = new System.Drawing.Point(174, 68);
            this.cmbPorts.Name = "cmbPorts";
            this.cmbPorts.Size = new System.Drawing.Size(122, 23);
            this.cmbPorts.TabIndex = 5;
            // 
            // lblPort
            // 
            this.lblPort.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPort.BackColor = System.Drawing.Color.Transparent;
            this.lblPort.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblPort.BorderWidth = 1F;
            this.lblPort.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblPort.Location = new System.Drawing.Point(54, 68);
            this.lblPort.Name = "lblPort";
            this.lblPort.Radius = 16;
            this.lblPort.ShowBackground = false;
            this.lblPort.ShowBorder = false;
            this.lblPort.Size = new System.Drawing.Size(114, 23);
            this.lblPort.TabIndex = 4;
            this.lblPort.Text = "端口";
            // 
            // btnStart
            // 
            this.btnStart.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnStart.BorderWidth = 1F;
            this.btnStart.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnStart.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnStart.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnStart.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnStart.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnStart.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnStart.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnStart.Location = new System.Drawing.Point(363, 64);
            this.btnStart.Name = "btnStart";
            this.btnStart.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnStart.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnStart.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStart.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnStart.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnStart.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnStart.Radius = 12;
            this.btnStart.ShowBorder = true;
            this.btnStart.ShowText = true;
            this.btnStart.Size = new System.Drawing.Size(96, 27);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "打开";
            this.btnStart.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnStop
            // 
            this.btnStop.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnStop.BorderWidth = 1F;
            this.btnStop.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnStop.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnStop.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnStop.Enabled = false;
            this.btnStop.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnStop.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnStop.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnStop.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnStop.Location = new System.Drawing.Point(476, 64);
            this.btnStop.Name = "btnStop";
            this.btnStop.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnStop.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnStop.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStop.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnStop.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnStop.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnStop.Radius = 12;
            this.btnStop.ShowBorder = true;
            this.btnStop.ShowText = true;
            this.btnStop.Size = new System.Drawing.Size(96, 27);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "关闭";
            this.btnStop.Click += new System.EventHandler(this.btn_Click);
            // 
            // linkRefresh
            // 
            this.linkRefresh.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.linkRefresh.AutoSize = true;
            this.linkRefresh.BackColor = System.Drawing.Color.Transparent;
            this.linkRefresh.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkRefresh.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.linkRefresh.Location = new System.Drawing.Point(311, 71);
            this.linkRefresh.Name = "linkRefresh";
            this.linkRefresh.Size = new System.Drawing.Size(33, 15);
            this.linkRefresh.TabIndex = 13;
            this.linkRefresh.TabStop = true;
            this.linkRefresh.Text = "刷新";
            this.linkRefresh.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRefresh_LinkClicked);
            // 
            // lblMusicStartVol
            // 
            this.lblMusicStartVol.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMusicStartVol.BackColor = System.Drawing.Color.Transparent;
            this.lblMusicStartVol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMusicStartVol.Location = new System.Drawing.Point(32, 132);
            this.lblMusicStartVol.Name = "lblMusicStartVol";
            this.lblMusicStartVol.Size = new System.Drawing.Size(136, 23);
            this.lblMusicStartVol.TabIndex = 47;
            this.lblMusicStartVol.Text = "音乐音量";
            // 
            // lblEffectStartVol
            // 
            this.lblEffectStartVol.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEffectStartVol.BackColor = System.Drawing.Color.Transparent;
            this.lblEffectStartVol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEffectStartVol.Location = new System.Drawing.Point(32, 218);
            this.lblEffectStartVol.Name = "lblEffectStartVol";
            this.lblEffectStartVol.Size = new System.Drawing.Size(136, 23);
            this.lblEffectStartVol.TabIndex = 45;
            this.lblEffectStartVol.Text = "效果音量";
            // 
            // sliderEffectVol
            // 
            this.sliderEffectVol.BackColor = System.Drawing.Color.Transparent;
            this.sliderEffectVol.backgroundDisabled = null;
            this.sliderEffectVol.backgroundNormal = global::AEC.Properties.Resources.system_slider_back;
            this.sliderEffectVol.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderEffectVol.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderEffectVol.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderEffectVol.Location = new System.Drawing.Point(174, 218);
            this.sliderEffectVol.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEffectVol.Name = "sliderEffectVol";
            this.sliderEffectVol.Size = new System.Drawing.Size(334, 23);
            this.sliderEffectVol.SliderWidth = 264;
            this.sliderEffectVol.TabIndex = 42;
            this.sliderEffectVol.thumbCenterY = 11;
            this.sliderEffectVol.ThumbEndX = 248;
            this.sliderEffectVol.upperLimit = ((short)(100));
            this.sliderEffectVol.value = ((short)(0));
            // 
            // lblMicStartVol
            // 
            this.lblMicStartVol.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicStartVol.BackColor = System.Drawing.Color.Transparent;
            this.lblMicStartVol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMicStartVol.Location = new System.Drawing.Point(32, 175);
            this.lblMicStartVol.Name = "lblMicStartVol";
            this.lblMicStartVol.Size = new System.Drawing.Size(136, 23);
            this.lblMicStartVol.TabIndex = 46;
            this.lblMicStartVol.Text = "话筒音量";
            // 
            // sliderMicVol
            // 
            this.sliderMicVol.BackColor = System.Drawing.Color.Transparent;
            this.sliderMicVol.backgroundDisabled = null;
            this.sliderMicVol.backgroundNormal = global::AEC.Properties.Resources.system_slider_back;
            this.sliderMicVol.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderMicVol.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderMicVol.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderMicVol.Location = new System.Drawing.Point(174, 175);
            this.sliderMicVol.Margin = new System.Windows.Forms.Padding(0);
            this.sliderMicVol.Name = "sliderMicVol";
            this.sliderMicVol.Size = new System.Drawing.Size(334, 23);
            this.sliderMicVol.SliderWidth = 264;
            this.sliderMicVol.TabIndex = 43;
            this.sliderMicVol.thumbCenterY = 11;
            this.sliderMicVol.ThumbEndX = 248;
            this.sliderMicVol.upperLimit = ((short)(100));
            this.sliderMicVol.value = ((short)(0));
            // 
            // sliderMusicVol
            // 
            this.sliderMusicVol.BackColor = System.Drawing.Color.Transparent;
            this.sliderMusicVol.backgroundDisabled = null;
            this.sliderMusicVol.backgroundNormal = global::AEC.Properties.Resources.system_slider_back;
            this.sliderMusicVol.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderMusicVol.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderMusicVol.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderMusicVol.Location = new System.Drawing.Point(174, 132);
            this.sliderMusicVol.Margin = new System.Windows.Forms.Padding(0);
            this.sliderMusicVol.Name = "sliderMusicVol";
            this.sliderMusicVol.Size = new System.Drawing.Size(334, 23);
            this.sliderMusicVol.SliderWidth = 264;
            this.sliderMusicVol.TabIndex = 44;
            this.sliderMusicVol.thumbCenterY = 11;
            this.sliderMusicVol.ThumbEndX = 248;
            this.sliderMusicVol.upperLimit = ((short)(100));
            this.sliderMusicVol.value = ((short)(0));
            // 
            // cmbPresets
            // 
            this.cmbPresets.ArrowColor = System.Drawing.Color.Gray;
            this.cmbPresets.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPresets.FormattingEnabled = true;
            this.cmbPresets.Location = new System.Drawing.Point(174, 266);
            this.cmbPresets.Name = "cmbPresets";
            this.cmbPresets.Size = new System.Drawing.Size(122, 23);
            this.cmbPresets.TabIndex = 49;
            this.cmbPresets.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            // 
            // lblPreset
            // 
            this.lblPreset.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPreset.BackColor = System.Drawing.Color.Transparent;
            this.lblPreset.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblPreset.BorderWidth = 1F;
            this.lblPreset.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblPreset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblPreset.Location = new System.Drawing.Point(54, 266);
            this.lblPreset.Name = "lblPreset";
            this.lblPreset.Radius = 16;
            this.lblPreset.ShowBackground = false;
            this.lblPreset.ShowBorder = false;
            this.lblPreset.Size = new System.Drawing.Size(114, 23);
            this.lblPreset.TabIndex = 48;
            this.lblPreset.Text = "预设模式";
            // 
            // cmbMusicInputPort
            // 
            this.cmbMusicInputPort.ArrowColor = System.Drawing.Color.Gray;
            this.cmbMusicInputPort.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbMusicInputPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMusicInputPort.FormattingEnabled = true;
            this.cmbMusicInputPort.Location = new System.Drawing.Point(174, 308);
            this.cmbMusicInputPort.Name = "cmbMusicInputPort";
            this.cmbMusicInputPort.Size = new System.Drawing.Size(122, 23);
            this.cmbMusicInputPort.TabIndex = 51;
            this.cmbMusicInputPort.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            // 
            // lblMusicInputPort
            // 
            this.lblMusicInputPort.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblMusicInputPort.BackColor = System.Drawing.Color.Transparent;
            this.lblMusicInputPort.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblMusicInputPort.BorderWidth = 1F;
            this.lblMusicInputPort.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblMusicInputPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMusicInputPort.Location = new System.Drawing.Point(54, 308);
            this.lblMusicInputPort.Name = "lblMusicInputPort";
            this.lblMusicInputPort.Radius = 16;
            this.lblMusicInputPort.ShowBackground = false;
            this.lblMusicInputPort.ShowBorder = false;
            this.lblMusicInputPort.Size = new System.Drawing.Size(114, 23);
            this.lblMusicInputPort.TabIndex = 50;
            this.lblMusicInputPort.Text = "音乐输入端口";
            // 
            // FormCentralControl
            // 
            this.ClientSize = new System.Drawing.Size(640, 360);
            this.Controls.Add(this.cmbMusicInputPort);
            this.Controls.Add(this.lblMusicInputPort);
            this.Controls.Add(this.cmbPresets);
            this.Controls.Add(this.lblPreset);
            this.Controls.Add(this.lblMusicStartVol);
            this.Controls.Add(this.lblEffectStartVol);
            this.Controls.Add(this.sliderEffectVol);
            this.Controls.Add(this.lblMicStartVol);
            this.Controls.Add(this.sliderMicVol);
            this.Controls.Add(this.sliderMusicVol);
            this.Controls.Add(this.linkRefresh);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.cmbPorts);
            this.Controls.Add(this.lblPort);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormCentralControl";
            this.Text = "中控测试";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormCentralControl_FormClosed);
            this.Load += new System.EventHandler(this.FormCentralControl_Load);
            this.Controls.SetChildIndex(this.lblPort, 0);
            this.Controls.SetChildIndex(this.cmbPorts, 0);
            this.Controls.SetChildIndex(this.btnStart, 0);
            this.Controls.SetChildIndex(this.btnStop, 0);
            this.Controls.SetChildIndex(this.linkRefresh, 0);
            this.Controls.SetChildIndex(this.sliderMusicVol, 0);
            this.Controls.SetChildIndex(this.sliderMicVol, 0);
            this.Controls.SetChildIndex(this.lblMicStartVol, 0);
            this.Controls.SetChildIndex(this.sliderEffectVol, 0);
            this.Controls.SetChildIndex(this.lblEffectStartVol, 0);
            this.Controls.SetChildIndex(this.lblMusicStartVol, 0);
            this.Controls.SetChildIndex(this.lblPreset, 0);
            this.Controls.SetChildIndex(this.cmbPresets, 0);
            this.Controls.SetChildIndex(this.lblMusicInputPort, 0);
            this.Controls.SetChildIndex(this.cmbMusicInputPort, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private gtComboBox cmbPorts;
        private Controls.MLabel lblPort;
        private Controls.MButton btnStart;
        private Controls.MButton btnStop;
        private System.Windows.Forms.LinkLabel linkRefresh;
        private gtLabel lblMusicStartVol;
        private gtLabel lblEffectStartVol;
        private HSliderParam sliderEffectVol;
        private gtLabel lblMicStartVol;
        private HSliderParam sliderMicVol;
        private HSliderParam sliderMusicVol;
        private gtComboBox cmbPresets;
        private Controls.MLabel lblPreset;
        private gtComboBox cmbMusicInputPort;
        private Controls.MLabel lblMusicInputPort;
    }
}
