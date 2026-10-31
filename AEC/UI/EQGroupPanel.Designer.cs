namespace AEC.UI
{
    partial class EQGroupPanel
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
            this.panelEQ = new AEC.UI.gtPanel();
            this.hScrollbar = new AEC.UI.HScrollbar();
            this.labelQ = new AEC.UI.gtLabel();
            this.panelEQGroup = new AEC.UI.gtFlowLayoutPanel();
            this.labelGain = new AEC.UI.gtLabel();
            this.labelTitle = new AEC.UI.gtLabel();
            this.labelFilterType = new AEC.UI.gtLabel();
            this.labelFreq = new AEC.UI.gtLabel();
            this.panelEQ.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEQ
            // 
            this.panelEQ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEQ.Controls.Add(this.hScrollbar);
            this.panelEQ.Controls.Add(this.labelQ);
            this.panelEQ.Controls.Add(this.panelEQGroup);
            this.panelEQ.Controls.Add(this.labelGain);
            this.panelEQ.Controls.Add(this.labelTitle);
            this.panelEQ.Controls.Add(this.labelFilterType);
            this.panelEQ.Controls.Add(this.labelFreq);
            this.panelEQ.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.panelEQ.ForeColor = System.Drawing.Color.LightGray;
            this.panelEQ.Location = new System.Drawing.Point(0, 0);
            this.panelEQ.Name = "panelEQ";
            this.panelEQ.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelEQ.Radius = 18;
            this.panelEQ.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.panelEQ.ShowRect = true;
            this.panelEQ.Size = new System.Drawing.Size(878, 264);
            this.panelEQ.TabIndex = 36;
            this.panelEQ.title = null;
            this.panelEQ.titleHeight = 20;
            // 
            // hScrollbar
            // 
            this.hScrollbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollbar.BtnWidth = 18;
            this.hScrollbar.ConerRadius = 7;
            this.hScrollbar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.hScrollbar.IsRadius = true;
            this.hScrollbar.IsShowRect = true;
            this.hScrollbar.LargeChange = 800;
            this.hScrollbar.Location = new System.Drawing.Point(62, 239);
            this.hScrollbar.Maximum = 1200;
            this.hScrollbar.Minimum = 0;
            this.hScrollbar.Name = "hScrollbar";
            this.hScrollbar.RectColor = System.Drawing.Color.Silver;
            this.hScrollbar.RectWidth = 1;
            this.hScrollbar.Size = new System.Drawing.Size(800, 18);
            this.hScrollbar.SmallChange = 51;
            this.hScrollbar.TabIndex = 34;
            this.hScrollbar.Text = "hScrollbar";
            this.hScrollbar.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(177)))), ((int)(((byte)(178)))));
            this.hScrollbar.Value = 0;
            this.hScrollbar.Visible = false;
            this.hScrollbar.Scroll += new System.EventHandler(this.hScrollbar_Scroll);
            // 
            // labelQ
            // 
            this.labelQ.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.labelQ.BackColor = System.Drawing.Color.Transparent;
            this.labelQ.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.labelQ.Location = new System.Drawing.Point(8, 212);
            this.labelQ.Name = "labelQ";
            this.labelQ.Size = new System.Drawing.Size(48, 23);
            this.labelQ.TabIndex = 35;
            this.labelQ.Text = "Q值:";
            // 
            // panelEQGroup
            // 
            this.panelEQGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEQGroup.AutoScroll = true;
            this.panelEQGroup.BackColor = System.Drawing.Color.Transparent;
            this.panelEQGroup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelEQGroup.Location = new System.Drawing.Point(62, 25);
            this.panelEQGroup.Margin = new System.Windows.Forms.Padding(0);
            this.panelEQGroup.Name = "panelEQGroup";
            this.panelEQGroup.Size = new System.Drawing.Size(800, 232);
            this.panelEQGroup.TabIndex = 1;
            this.panelEQGroup.WrapContents = false;
            // 
            // labelGain
            // 
            this.labelGain.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.labelGain.BackColor = System.Drawing.Color.Transparent;
            this.labelGain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.labelGain.Location = new System.Drawing.Point(8, 158);
            this.labelGain.Name = "labelGain";
            this.labelGain.Size = new System.Drawing.Size(48, 23);
            this.labelGain.TabIndex = 35;
            this.labelGain.Text = "增益:";
            // 
            // labelTitle
            // 
            this.labelTitle.alignment = AEC.UI.gtLabel.AlignMent.Left;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.labelTitle.Location = new System.Drawing.Point(12, 6);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(128, 19);
            this.labelTitle.TabIndex = 35;
            this.labelTitle.Text = "X段EQ";
            // 
            // labelFilterType
            // 
            this.labelFilterType.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.labelFilterType.BackColor = System.Drawing.Color.Transparent;
            this.labelFilterType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.labelFilterType.Location = new System.Drawing.Point(8, 104);
            this.labelFilterType.Name = "labelFilterType";
            this.labelFilterType.Size = new System.Drawing.Size(48, 23);
            this.labelFilterType.TabIndex = 35;
            this.labelFilterType.Text = "类型";
            // 
            // labelFreq
            // 
            this.labelFreq.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.labelFreq.BackColor = System.Drawing.Color.Transparent;
            this.labelFreq.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.labelFreq.Location = new System.Drawing.Point(8, 185);
            this.labelFreq.Name = "labelFreq";
            this.labelFreq.Size = new System.Drawing.Size(48, 23);
            this.labelFreq.TabIndex = 35;
            this.labelFreq.Text = "频率:";
            // 
            // EQGroupPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.panelEQ);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "EQGroupPanel";
            this.Size = new System.Drawing.Size(878, 264);
            this.Load += new System.EventHandler(this.EQGroupPanel_Load);
            this.panelEQ.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private gtFlowLayoutPanel panelEQGroup;
        private HScrollbar hScrollbar;
        private gtLabel labelGain;
        private gtLabel labelFreq;
        private gtLabel labelQ;
        private gtLabel labelFilterType;
        private gtPanel panelEQ;
        private gtLabel labelTitle;
    }
}
