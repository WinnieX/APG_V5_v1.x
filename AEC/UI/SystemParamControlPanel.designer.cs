namespace AEC.UI
{
    partial class SystemParamControlPanel
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
            this.volumePanelEffect = new AEC.UI.VolumePanel();
            this.volumePanelMic = new AEC.UI.VolumePanel();
            this.volumePanelMusic = new AEC.UI.VolumePanel();
            this.timbrePnael = new AEC.UI.TimbrePnael();
            this.SuspendLayout();
            // 
            // volumePanelEffect
            // 
            this.volumePanelEffect.BackColor = System.Drawing.Color.Transparent;
            this.volumePanelEffect.canBeChanged = true;
            this.volumePanelEffect.FillColor = System.Drawing.Color.Transparent;
            this.volumePanelEffect.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.volumePanelEffect.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.volumePanelEffect.Location = new System.Drawing.Point(358, 4);
            this.volumePanelEffect.Name = "volumePanelEffect";
            this.volumePanelEffect.Size = new System.Drawing.Size(178, 25);
            this.volumePanelEffect.TabIndex = 2;
            this.volumePanelEffect.tag = "效果";
            this.volumePanelEffect.upperLimit = ((short)(100));
            // 
            // volumePanelMic
            // 
            this.volumePanelMic.BackColor = System.Drawing.Color.Transparent;
            this.volumePanelMic.canBeChanged = true;
            this.volumePanelMic.FillColor = System.Drawing.Color.Transparent;
            this.volumePanelMic.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.volumePanelMic.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.volumePanelMic.Location = new System.Drawing.Point(179, 4);
            this.volumePanelMic.Name = "volumePanelMic";
            this.volumePanelMic.Size = new System.Drawing.Size(178, 25);
            this.volumePanelMic.TabIndex = 2;
            this.volumePanelMic.tag = "话筒";
            this.volumePanelMic.upperLimit = ((short)(100));
            // 
            // volumePanelMusic
            // 
            this.volumePanelMusic.BackColor = System.Drawing.Color.Transparent;
            this.volumePanelMusic.canBeChanged = true;
            this.volumePanelMusic.FillColor = System.Drawing.Color.Transparent;
            this.volumePanelMusic.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.volumePanelMusic.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.volumePanelMusic.Location = new System.Drawing.Point(0, 4);
            this.volumePanelMusic.Name = "volumePanelMusic";
            this.volumePanelMusic.Size = new System.Drawing.Size(178, 25);
            this.volumePanelMusic.TabIndex = 2;
            this.volumePanelMusic.tag = "音乐";
            this.volumePanelMusic.upperLimit = ((short)(100));
            // 
            // timbrePnael
            // 
            this.timbrePnael.BackColor = System.Drawing.Color.Transparent;
            this.timbrePnael.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.timbrePnael.BorderWidth = 1F;
            this.timbrePnael.canBeChanged = true;
            this.timbrePnael.FillColor = System.Drawing.Color.Transparent;
            this.timbrePnael.Font = new System.Drawing.Font("迷你简准圆", 13.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.timbrePnael.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(195)))), ((int)(((byte)(74)))));
            this.timbrePnael.Location = new System.Drawing.Point(537, 4);
            this.timbrePnael.Name = "timbrePnael";
            this.timbrePnael.Radius = 0;
            this.timbrePnael.ShowBorder = false;
            this.timbrePnael.ShowText = true;
            this.timbrePnael.Size = new System.Drawing.Size(192, 25);
            this.timbrePnael.TabIndex = 3;
            this.timbrePnael.Text = "音色";
            this.timbrePnael.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.timbrePnael.TextPadding = new System.Windows.Forms.Padding(0, 2, 146, -2);
            this.timbrePnael.upperLimit = ((short)(100));
            this.timbrePnael.Visible = false;
            // 
            // SystemParamControlPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.timbrePnael);
            this.Controls.Add(this.volumePanelEffect);
            this.Controls.Add(this.volumePanelMic);
            this.Controls.Add(this.volumePanelMusic);
            this.FillColor = System.Drawing.Color.Silver;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SystemParamControlPanel";
            this.Radius = 0;
            this.ShowBorder = false;
            this.ShowText = false;
            this.Size = new System.Drawing.Size(729, 32);
            this.ResumeLayout(false);

        }

        #endregion

        private VolumePanel volumePanelMusic;
        private VolumePanel volumePanelMic;
        private VolumePanel volumePanelEffect;
        private TimbrePnael timbrePnael;
    }
}
