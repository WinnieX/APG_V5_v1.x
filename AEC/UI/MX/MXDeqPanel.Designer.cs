namespace AEC.UI.MX
{
    partial class MXDeqPanel
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
            this.panelDEQ = new AEC.UI.gtPanel();
            this.panelDEQGroup = new AEC.UI.gtFlowLayoutPanel();
            this.btnM1 = new AEC.UI.gtButton();
            this.btnReset = new AEC.UI.gtButton();
            this.panelDEQ.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDEQ
            // 
            this.panelDEQ.Controls.Add(this.panelDEQGroup);
            this.panelDEQ.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelDEQ.ForeColor = System.Drawing.Color.LightGray;
            this.panelDEQ.Location = new System.Drawing.Point(14, 8);
            this.panelDEQ.Name = "panelDEQ";
            this.panelDEQ.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelDEQ.Radius = 18;
            this.panelDEQ.RectColor = System.Drawing.Color.Black;
            this.panelDEQ.ShowRect = true;
            this.panelDEQ.Size = new System.Drawing.Size(1164, 298);
            this.panelDEQ.TabIndex = 6;
            this.panelDEQ.title = "";
            this.panelDEQ.titleHeight = 20;
            // 
            // panelDEQGroup
            // 
            this.panelDEQGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDEQGroup.AutoScroll = true;
            this.panelDEQGroup.BackColor = System.Drawing.Color.Transparent;
            this.panelDEQGroup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelDEQGroup.Location = new System.Drawing.Point(10, 5);
            this.panelDEQGroup.Margin = new System.Windows.Forms.Padding(0);
            this.panelDEQGroup.Name = "panelDEQGroup";
            this.panelDEQGroup.Size = new System.Drawing.Size(1142, 288);
            this.panelDEQGroup.TabIndex = 2;
            this.panelDEQGroup.WrapContents = false;
            // 
            // btnM1
            // 
            this.btnM1.BackColor = System.Drawing.Color.Transparent;
            this.btnM1.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.btnM1.backHover = global::AEC.Properties.Resources.btn_hover;
            this.btnM1.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.btnM1.backPressed = global::AEC.Properties.Resources.btn_pressed;
            this.btnM1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnM1.ForeColor = System.Drawing.Color.LightGray;
            this.btnM1.isStretch = true;
            this.btnM1.Location = new System.Drawing.Point(1187, 49);
            this.btnM1.Margin = new System.Windows.Forms.Padding(0);
            this.btnM1.Name = "btnM1";
            this.btnM1.Size = new System.Drawing.Size(80, 30);
            this.btnM1.TabIndex = 7;
            this.btnM1.Text = "M1";
            this.btnM1.userGDIPaint = true;
            this.btnM1.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Transparent;
            this.btnReset.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.btnReset.backHover = global::AEC.Properties.Resources.btn_hover;
            this.btnReset.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.btnReset.backPressed = global::AEC.Properties.Resources.btn_pressed;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnReset.ForeColor = System.Drawing.Color.LightGray;
            this.btnReset.isStretch = true;
            this.btnReset.Location = new System.Drawing.Point(1187, 8);
            this.btnReset.Margin = new System.Windows.Forms.Padding(0);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(80, 30);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "Reset";
            this.btnReset.userGDIPaint = true;
            this.btnReset.Click += new System.EventHandler(this.btn_Click);
            // 
            // MXDeqPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnM1);
            this.Controls.Add(this.panelDEQ);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "MXDeqPanel";
            this.Size = new System.Drawing.Size(1276, 312);
            this.panelDEQ.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private gtPanel panelDEQ;
        protected gtButton btnM1;
        private gtFlowLayoutPanel panelDEQGroup;
        protected gtButton btnReset;

    }
}
