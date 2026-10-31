namespace AEC.UI.KX
{
    partial class KXMonitorModulePanelBase
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
            this.eqThumbnail = new AEC.UI.Controls.EQThumbnail();
            this.SuspendLayout();
            // 
            // eqThumbnail
            // 
            this.eqThumbnail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.eqThumbnail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.eqThumbnail.BorderWidth = 1F;
            this.eqThumbnail.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.eqThumbnail.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.eqThumbnail.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.eqThumbnail.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.eqThumbnail.ForeColor = System.Drawing.Color.LightGray;
            this.eqThumbnail.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.eqThumbnail.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.eqThumbnail.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.eqThumbnail.isShowBackEQLabel = false;
            this.eqThumbnail.Location = new System.Drawing.Point(11, 48);
            this.eqThumbnail.Name = "eqThumbnail";
            this.eqThumbnail.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.eqThumbnail.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.eqThumbnail.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.eqThumbnail.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.eqThumbnail.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.eqThumbnail.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.eqThumbnail.Radius = 12;
            this.eqThumbnail.ShowBorder = false;
            this.eqThumbnail.ShowText = true;
            this.eqThumbnail.Size = new System.Drawing.Size(86, 42);
            this.eqThumbnail.TabIndex = 3;
            this.eqThumbnail.Text = "eqThumbnail1";
            this.eqThumbnail.MouseClick += new System.Windows.Forms.MouseEventHandler(this.eqThumbnail_MouseClick);
            // 
            // KXMonitorModulePanelBase
            // 
            this.BorderColor = System.Drawing.Color.Black;
            this.BorderWidth = 2F;
            this.Controls.Add(this.eqThumbnail);
            this.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Name = "KXMonitorModulePanelBase";
            this.Size = new System.Drawing.Size(108, 612);
            this.ResumeLayout(false);

        }

        #endregion

        protected Controls.EQThumbnail eqThumbnail;

    }
}
