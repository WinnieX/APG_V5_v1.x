namespace AEC.UI
{
    partial class UserControl1
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
            this.panelSystemVolume = new AEC.UI.PanelWithTitle();
            this.SuspendLayout();
            // 
            // panelSystemVolume
            // 
            this.panelSystemVolume.BackColor = System.Drawing.Color.Black;
            this.panelSystemVolume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSystemVolume.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelSystemVolume.ForeColor = System.Drawing.Color.White;
            this.panelSystemVolume.Location = new System.Drawing.Point(0, 0);
            this.panelSystemVolume.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelSystemVolume.Name = "panelSystemVolume";
            this.panelSystemVolume.Padding = new System.Windows.Forms.Padding(0, 12, 2, 0);
            this.panelSystemVolume.Size = new System.Drawing.Size(880, 543);
            this.panelSystemVolume.TabIndex = 17;
            this.panelSystemVolume.title = "音量控制";
            this.panelSystemVolume.titleHeight = 16;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelSystemVolume);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(880, 543);
            this.ResumeLayout(false);

        }

        #endregion

        private PanelWithTitle panelSystemVolume;
    }
}
