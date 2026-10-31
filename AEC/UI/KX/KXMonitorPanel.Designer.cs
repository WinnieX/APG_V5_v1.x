namespace AEC.UI.KX
{
    partial class KXMonitorPanel
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
            this.panelMonitorSurOutput = new AEC.UI.KX.KXMonitorStereoOutputControlPanel();
            this.panelMonitorAux2Output = new AEC.UI.KX.KXMonitorMonoOutputControlPanel();
            this.panelMonitorAux1Output = new AEC.UI.KX.KXMonitorMonoOutputControlPanel();
            this.panelMonitorMainOutput = new AEC.UI.KX.KXMonitorStereoOutputControlPanel();
            this.panelMonitorMusicInput = new AEC.UI.KX.KXMonitorMusicPanel();
            this.panelMonitorMicInput = new AEC.UI.KX.KXMonitorMicPanel();
            this.SuspendLayout();
            // 
            // panelMonitorSurOutput
            // 
            this.panelMonitorSurOutput.BackColor = System.Drawing.Color.Transparent;
            this.panelMonitorSurOutput.BorderColor = System.Drawing.Color.Black;
            this.panelMonitorSurOutput.BorderWidth = 2F;
            this.panelMonitorSurOutput.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelMonitorSurOutput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelMonitorSurOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelMonitorSurOutput.Location = new System.Drawing.Point(960, 8);
            this.panelMonitorSurOutput.Margin = new System.Windows.Forms.Padding(4);
            this.panelMonitorSurOutput.Name = "panelMonitorSurOutput";
            this.panelMonitorSurOutput.Radius = 16;
            this.panelMonitorSurOutput.ShowBorder = true;
            this.panelMonitorSurOutput.ShowText = true;
            this.panelMonitorSurOutput.Size = new System.Drawing.Size(260, 636);
            this.panelMonitorSurOutput.TabIndex = 2;
            this.panelMonitorSurOutput.Text = "后置输出";
            this.panelMonitorSurOutput.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.panelMonitorSurOutput.TextPadding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelMonitorSurOutput.EQThumbnailMouseClick += new System.EventHandler<System.EventArgs>(this.panel_EQThumbnailMouseClick);
            // 
            // panelMonitorAux2Output
            // 
            this.panelMonitorAux2Output.BackColor = System.Drawing.Color.Transparent;
            this.panelMonitorAux2Output.BorderColor = System.Drawing.Color.Black;
            this.panelMonitorAux2Output.BorderWidth = 2F;
            this.panelMonitorAux2Output.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelMonitorAux2Output.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelMonitorAux2Output.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelMonitorAux2Output.Location = new System.Drawing.Point(782, 8);
            this.panelMonitorAux2Output.Margin = new System.Windows.Forms.Padding(4);
            this.panelMonitorAux2Output.Name = "panelMonitorAux2Output";
            this.panelMonitorAux2Output.Radius = 16;
            this.panelMonitorAux2Output.ShowBorder = true;
            this.panelMonitorAux2Output.ShowText = true;
            this.panelMonitorAux2Output.Size = new System.Drawing.Size(170, 636);
            this.panelMonitorAux2Output.TabIndex = 2;
            this.panelMonitorAux2Output.Text = "辅助输出2";
            this.panelMonitorAux2Output.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.panelMonitorAux2Output.TextPadding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelMonitorAux2Output.EQThumbnailMouseClick += new System.EventHandler<System.EventArgs>(this.panel_EQThumbnailMouseClick);
            // 
            // panelMonitorAux1Output
            // 
            this.panelMonitorAux1Output.BackColor = System.Drawing.Color.Transparent;
            this.panelMonitorAux1Output.BorderColor = System.Drawing.Color.Black;
            this.panelMonitorAux1Output.BorderWidth = 2F;
            this.panelMonitorAux1Output.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelMonitorAux1Output.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelMonitorAux1Output.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelMonitorAux1Output.Location = new System.Drawing.Point(604, 8);
            this.panelMonitorAux1Output.Margin = new System.Windows.Forms.Padding(4);
            this.panelMonitorAux1Output.Name = "panelMonitorAux1Output";
            this.panelMonitorAux1Output.Radius = 16;
            this.panelMonitorAux1Output.ShowBorder = true;
            this.panelMonitorAux1Output.ShowText = true;
            this.panelMonitorAux1Output.Size = new System.Drawing.Size(170, 636);
            this.panelMonitorAux1Output.TabIndex = 2;
            this.panelMonitorAux1Output.Text = "辅助输出1";
            this.panelMonitorAux1Output.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.panelMonitorAux1Output.TextPadding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelMonitorAux1Output.EQThumbnailMouseClick += new System.EventHandler<System.EventArgs>(this.panel_EQThumbnailMouseClick);
            // 
            // panelMonitorMainOutput
            // 
            this.panelMonitorMainOutput.BackColor = System.Drawing.Color.Transparent;
            this.panelMonitorMainOutput.BorderColor = System.Drawing.Color.Black;
            this.panelMonitorMainOutput.BorderWidth = 2F;
            this.panelMonitorMainOutput.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelMonitorMainOutput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelMonitorMainOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelMonitorMainOutput.Location = new System.Drawing.Point(336, 8);
            this.panelMonitorMainOutput.Margin = new System.Windows.Forms.Padding(4);
            this.panelMonitorMainOutput.Name = "panelMonitorMainOutput";
            this.panelMonitorMainOutput.Radius = 16;
            this.panelMonitorMainOutput.ShowBorder = true;
            this.panelMonitorMainOutput.ShowText = true;
            this.panelMonitorMainOutput.Size = new System.Drawing.Size(260, 636);
            this.panelMonitorMainOutput.TabIndex = 2;
            this.panelMonitorMainOutput.Text = "主输出";
            this.panelMonitorMainOutput.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.panelMonitorMainOutput.TextPadding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelMonitorMainOutput.EQThumbnailMouseClick += new System.EventHandler<System.EventArgs>(this.panel_EQThumbnailMouseClick);
            // 
            // panelMonitorMusicInput
            // 
            this.panelMonitorMusicInput.BackColor = System.Drawing.Color.Transparent;
            this.panelMonitorMusicInput.BorderColor = System.Drawing.Color.Black;
            this.panelMonitorMusicInput.BorderWidth = 2F;
            this.panelMonitorMusicInput.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelMonitorMusicInput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelMonitorMusicInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelMonitorMusicInput.Location = new System.Drawing.Point(56, 8);
            this.panelMonitorMusicInput.Margin = new System.Windows.Forms.Padding(4);
            this.panelMonitorMusicInput.Name = "panelMonitorMusicInput";
            this.panelMonitorMusicInput.Radius = 16;
            this.panelMonitorMusicInput.ShowBorder = true;
            this.panelMonitorMusicInput.ShowText = true;
            this.panelMonitorMusicInput.Size = new System.Drawing.Size(128, 636);
            this.panelMonitorMusicInput.TabIndex = 6;
            this.panelMonitorMusicInput.Text = "音乐";
            this.panelMonitorMusicInput.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.panelMonitorMusicInput.TextPadding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelMonitorMusicInput.EQThumbnailMouseClick += new System.EventHandler<System.EventArgs>(this.panel_EQThumbnailMouseClick);
            // 
            // panelMonitorMicInput
            // 
            this.panelMonitorMicInput.BackColor = System.Drawing.Color.Transparent;
            this.panelMonitorMicInput.BorderColor = System.Drawing.Color.Black;
            this.panelMonitorMicInput.BorderWidth = 2F;
            this.panelMonitorMicInput.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelMonitorMicInput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelMonitorMicInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelMonitorMicInput.Location = new System.Drawing.Point(192, 8);
            this.panelMonitorMicInput.Margin = new System.Windows.Forms.Padding(4);
            this.panelMonitorMicInput.Name = "panelMonitorMicInput";
            this.panelMonitorMicInput.Radius = 16;
            this.panelMonitorMicInput.ShowBorder = true;
            this.panelMonitorMicInput.ShowText = true;
            this.panelMonitorMicInput.Size = new System.Drawing.Size(128, 636);
            this.panelMonitorMicInput.TabIndex = 7;
            this.panelMonitorMicInput.Text = "麦克风";
            this.panelMonitorMicInput.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.panelMonitorMicInput.TextPadding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelMonitorMicInput.EQThumbnailMouseClick += new System.EventHandler<System.EventArgs>(this.panel_EQThumbnailMouseClick);
            // 
            // KXMonitorPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelMonitorAux2Output);
            this.Controls.Add(this.panelMonitorAux1Output);
            this.Controls.Add(this.panelMonitorSurOutput);
            this.Controls.Add(this.panelMonitorMainOutput);
            this.Controls.Add(this.panelMonitorMicInput);
            this.Controls.Add(this.panelMonitorMusicInput);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "KXMonitorPanel";
            this.Size = new System.Drawing.Size(1276, 648);
            this.ResumeLayout(false);

        }

        #endregion

        private KXMonitorStereoOutputControlPanel panelMonitorMainOutput;
        private KXMonitorMonoOutputControlPanel panelMonitorAux1Output;
        private KXMonitorStereoOutputControlPanel panelMonitorSurOutput;
        private KXMonitorMonoOutputControlPanel panelMonitorAux2Output;
        private KXMonitorMusicPanel panelMonitorMusicInput;
        private KXMonitorMicPanel panelMonitorMicInput;
    }
}
