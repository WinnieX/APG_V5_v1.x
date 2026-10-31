namespace AEC.UI
{
    partial class Title
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
            this.btnClose = new AEC.UI.gtButton();
            this.btnMax = new AEC.UI.gtButton();
            this.btnMin = new AEC.UI.gtButton();
            this.btnMenu = new AEC.UI.gtButton();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.backDisabled = global::AEC.Properties.Resources.close_normal;
            this.btnClose.backHover = global::AEC.Properties.Resources.close_hover;
            this.btnClose.backNormal = global::AEC.Properties.Resources.close_normal;
            this.btnClose.backPressed = global::AEC.Properties.Resources.close_pressed;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.isStretch = false;
            this.btnClose.Location = new System.Drawing.Point(1234, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(44, 30);
            this.btnClose.TabIndex = 7;
            this.btnClose.userGDIPaint = false;
            this.btnClose.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnMax
            // 
            this.btnMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMax.BackColor = System.Drawing.Color.Transparent;
            this.btnMax.backDisabled = global::AEC.Properties.Resources.max_normal;
            this.btnMax.backHover = global::AEC.Properties.Resources.max_hover;
            this.btnMax.backNormal = global::AEC.Properties.Resources.max_normal;
            this.btnMax.backPressed = global::AEC.Properties.Resources.max_pressed;
            this.btnMax.ForeColor = System.Drawing.Color.White;
            this.btnMax.isStretch = false;
            this.btnMax.Location = new System.Drawing.Point(1189, 1);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(44, 31);
            this.btnMax.TabIndex = 8;
            this.btnMax.userGDIPaint = false;
            this.btnMax.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnMin
            // 
            this.btnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMin.BackColor = System.Drawing.Color.Transparent;
            this.btnMin.backDisabled = global::AEC.Properties.Resources.min_normal;
            this.btnMin.backHover = global::AEC.Properties.Resources.min_hover;
            this.btnMin.backNormal = global::AEC.Properties.Resources.min_normal;
            this.btnMin.backPressed = global::AEC.Properties.Resources.min_pressed;
            this.btnMin.ForeColor = System.Drawing.Color.White;
            this.btnMin.isStretch = false;
            this.btnMin.Location = new System.Drawing.Point(1144, 1);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(44, 31);
            this.btnMin.TabIndex = 9;
            this.btnMin.userGDIPaint = false;
            this.btnMin.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnMenu
            // 
            this.btnMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMenu.BackColor = System.Drawing.Color.Transparent;
            this.btnMenu.backDisabled = global::AEC.Properties.Resources.menu_normal;
            this.btnMenu.backHover = global::AEC.Properties.Resources.menu_hover;
            this.btnMenu.backNormal = global::AEC.Properties.Resources.menu_normal;
            this.btnMenu.backPressed = global::AEC.Properties.Resources.menu_pressed;
            this.btnMenu.ForeColor = System.Drawing.Color.White;
            this.btnMenu.isStretch = false;
            this.btnMenu.Location = new System.Drawing.Point(1099, 1);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(44, 31);
            this.btnMenu.TabIndex = 10;
            this.btnMenu.userGDIPaint = false;
            this.btnMenu.Click += new System.EventHandler(this.btn_Click);
            // 
            // Title
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Controls.Add(this.btnMax);
            this.Controls.Add(this.btnMin);
            this.Controls.Add(this.btnMenu);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "Title";
            this.Size = new System.Drawing.Size(1280, 32);
            this.Load += new System.EventHandler(this.Title_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private gtButton btnClose;
        private gtButton btnMax;
        private gtButton btnMin;
        public gtButton btnMenu;

    }
}
