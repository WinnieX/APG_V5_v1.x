namespace AEC.UI.KX
{
    partial class KXMusicPanel
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
            this.panelHPF = new AEC.UI.gtPanel();
            this.hpfFreqSlider = new AEC.UI.HSliderParam();
            this.panelMusicInput = new AEC.UI.gtPanel();
            this.cmbMusicInputGain = new AEC.UI.gtComboBox();
            this.lblMusicInputPort = new AEC.UI.gtLabel();
            this.lblMusicInputGain = new AEC.UI.gtLabel();
            this.cmbMusicInputPort = new AEC.UI.gtComboBox();
            this.workModePanel = new AEC.UI.WorkModePanel();
            this.eqResetAndByPassPanel = new AEC.UI.EQResetAndByPassPanel();
            this.eqGroupPanel = new AEC.UI.EQGroupPanel();
            this.panelHPF.SuspendLayout();
            this.panelMusicInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHPF
            // 
            this.panelHPF.Controls.Add(this.hpfFreqSlider);
            this.panelHPF.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelHPF.ForeColor = System.Drawing.Color.LightGray;
            this.panelHPF.Location = new System.Drawing.Point(10, 176);
            this.panelHPF.Name = "panelHPF";
            this.panelHPF.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelHPF.Radius = 18;
            this.panelHPF.RectColor = System.Drawing.Color.Black;
            this.panelHPF.ShowRect = true;
            this.panelHPF.Size = new System.Drawing.Size(350, 130);
            this.panelHPF.TabIndex = 6;
            this.panelHPF.title = "音乐高通";
            this.panelHPF.titleHeight = 20;
            // 
            // hpfFreqSlider
            // 
            this.hpfFreqSlider.BackColor = System.Drawing.Color.Transparent;
            this.hpfFreqSlider.backgroundDisabled = global::AEC.Properties.Resources.music_slider_back_disabled;
            this.hpfFreqSlider.backgroundNormal = global::AEC.Properties.Resources.music_slider_back;
            this.hpfFreqSlider.backLeftTop = new System.Drawing.Point(0, 6);
            this.hpfFreqSlider.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.hpfFreqSlider.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.hpfFreqSlider.Location = new System.Drawing.Point(43, 59);
            this.hpfFreqSlider.Margin = new System.Windows.Forms.Padding(0);
            this.hpfFreqSlider.Name = "hpfFreqSlider";
            this.hpfFreqSlider.Size = new System.Drawing.Size(268, 23);
            this.hpfFreqSlider.SliderWidth = 188;
            this.hpfFreqSlider.TabIndex = 2;
            this.hpfFreqSlider.thumbCenterY = 11;
            this.hpfFreqSlider.ThumbEndX = 176;
            this.hpfFreqSlider.upperLimit = ((short)(100));
            this.hpfFreqSlider.value = ((short)(0));
            // 
            // panelMusicInput
            // 
            this.panelMusicInput.Controls.Add(this.cmbMusicInputGain);
            this.panelMusicInput.Controls.Add(this.lblMusicInputPort);
            this.panelMusicInput.Controls.Add(this.lblMusicInputGain);
            this.panelMusicInput.Controls.Add(this.cmbMusicInputPort);
            this.panelMusicInput.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelMusicInput.ForeColor = System.Drawing.Color.LightGray;
            this.panelMusicInput.Location = new System.Drawing.Point(10, 8);
            this.panelMusicInput.Name = "panelMusicInput";
            this.panelMusicInput.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelMusicInput.Radius = 18;
            this.panelMusicInput.RectColor = System.Drawing.Color.Black;
            this.panelMusicInput.ShowRect = true;
            this.panelMusicInput.Size = new System.Drawing.Size(350, 158);
            this.panelMusicInput.TabIndex = 5;
            this.panelMusicInput.title = "音乐输入";
            this.panelMusicInput.titleHeight = 20;
            // 
            // cmbMusicInputGain
            // 
            this.cmbMusicInputGain.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbMusicInputGain.DropDownBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbMusicInputGain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMusicInputGain.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbMusicInputGain.ForeColor = System.Drawing.Color.Black;
            this.cmbMusicInputGain.FormattingEnabled = true;
            this.cmbMusicInputGain.Items.AddRange(new object[] {
            "-6 dB",
            "-3 dB"});
            this.cmbMusicInputGain.Location = new System.Drawing.Point(173, 96);
            this.cmbMusicInputGain.Margin = new System.Windows.Forms.Padding(0);
            this.cmbMusicInputGain.Name = "cmbMusicInputGain";
            this.cmbMusicInputGain.Size = new System.Drawing.Size(92, 23);
            this.cmbMusicInputGain.TabIndex = 27;
            this.cmbMusicInputGain.TabStop = false;
            // 
            // lblMusicInputPort
            // 
            this.lblMusicInputPort.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMusicInputPort.BackColor = System.Drawing.Color.Transparent;
            this.lblMusicInputPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMusicInputPort.Location = new System.Drawing.Point(39, 56);
            this.lblMusicInputPort.Name = "lblMusicInputPort";
            this.lblMusicInputPort.Size = new System.Drawing.Size(128, 23);
            this.lblMusicInputPort.TabIndex = 26;
            this.lblMusicInputPort.Text = "音乐输入端口";
            // 
            // lblMusicInputGain
            // 
            this.lblMusicInputGain.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMusicInputGain.BackColor = System.Drawing.Color.Transparent;
            this.lblMusicInputGain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMusicInputGain.Location = new System.Drawing.Point(18, 96);
            this.lblMusicInputGain.Name = "lblMusicInputGain";
            this.lblMusicInputGain.Size = new System.Drawing.Size(149, 23);
            this.lblMusicInputGain.TabIndex = 26;
            this.lblMusicInputGain.Text = "音乐输入增益";
            this.lblMusicInputGain.Visible = false;
            // 
            // cmbMusicInputPort
            // 
            this.cmbMusicInputPort.ArrowColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbMusicInputPort.BackColor = System.Drawing.SystemColors.Window;
            this.cmbMusicInputPort.DropDownBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbMusicInputPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMusicInputPort.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbMusicInputPort.ForeColor = System.Drawing.Color.Black;
            this.cmbMusicInputPort.FormattingEnabled = true;
            this.cmbMusicInputPort.Location = new System.Drawing.Point(173, 56);
            this.cmbMusicInputPort.Margin = new System.Windows.Forms.Padding(0);
            this.cmbMusicInputPort.Name = "cmbMusicInputPort";
            this.cmbMusicInputPort.Size = new System.Drawing.Size(92, 23);
            this.cmbMusicInputPort.TabIndex = 27;
            this.cmbMusicInputPort.TabStop = false;
            // 
            // workModePanel
            // 
            this.workModePanel.BackColor = System.Drawing.Color.Transparent;
            this.workModePanel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.workModePanel.ForeColor = System.Drawing.Color.LightGray;
            this.workModePanel.Location = new System.Drawing.Point(575, 275);
            this.workModePanel.Margin = new System.Windows.Forms.Padding(0);
            this.workModePanel.Name = "workModePanel";
            this.workModePanel.Size = new System.Drawing.Size(336, 32);
            this.workModePanel.TabIndex = 4;
            // 
            // eqResetAndByPassPanel
            // 
            this.eqResetAndByPassPanel.BackColor = System.Drawing.Color.Transparent;
            this.eqResetAndByPassPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.eqResetAndByPassPanel.isPass = false;
            this.eqResetAndByPassPanel.Location = new System.Drawing.Point(370, 275);
            this.eqResetAndByPassPanel.Name = "eqResetAndByPassPanel";
            this.eqResetAndByPassPanel.Size = new System.Drawing.Size(168, 33);
            this.eqResetAndByPassPanel.TabIndex = 3;
            // 
            // eqGroupPanel
            // 
            this.eqGroupPanel.BackColor = System.Drawing.Color.Transparent;
            this.eqGroupPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.eqGroupPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.eqGroupPanel.Location = new System.Drawing.Point(370, 8);
            this.eqGroupPanel.Name = "eqGroupPanel";
            this.eqGroupPanel.Size = new System.Drawing.Size(638, 258);
            this.eqGroupPanel.TabIndex = 0;
            // 
            // KXMusicPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelHPF);
            this.Controls.Add(this.panelMusicInput);
            this.Controls.Add(this.workModePanel);
            this.Controls.Add(this.eqResetAndByPassPanel);
            this.Controls.Add(this.eqGroupPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "KXMusicPanel";
            this.Size = new System.Drawing.Size(1276, 312);
            this.panelHPF.ResumeLayout(false);
            this.panelMusicInput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private EQGroupPanel eqGroupPanel;
        private HSliderParam hpfFreqSlider;
        private EQResetAndByPassPanel eqResetAndByPassPanel;
        private WorkModePanel workModePanel;
        private gtComboBox cmbMusicInputGain;
        private gtLabel lblMusicInputGain;
        private gtComboBox cmbMusicInputPort;
        private gtLabel lblMusicInputPort;
        private gtPanel panelMusicInput;
        private gtPanel panelHPF;
    }
}
