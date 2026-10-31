namespace AEC.UI
{
    partial class EQResetAndByPassPanel
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
            this.btnEQReset = new AEC.UI.gtButton();
            this.chkEQPass = new AEC.UI.gtCheckBox();
            this.SuspendLayout();
            // 
            // btnEQReset
            // 
            this.btnEQReset.BackColor = System.Drawing.Color.Transparent;
            this.btnEQReset.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.btnEQReset.backHover = global::AEC.Properties.Resources.btn_hover;
            this.btnEQReset.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.btnEQReset.backPressed = global::AEC.Properties.Resources.btn_pressed;
            this.btnEQReset.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnEQReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.btnEQReset.isStretch = true;
            this.btnEQReset.Location = new System.Drawing.Point(4, 1);
            this.btnEQReset.Margin = new System.Windows.Forms.Padding(0);
            this.btnEQReset.Name = "btnEQReset";
            this.btnEQReset.Size = new System.Drawing.Size(80, 30);
            this.btnEQReset.TabIndex = 0;
            this.btnEQReset.Text = "EQ重置";
            this.btnEQReset.userGDIPaint = true;
            // 
            // chkEQPass
            // 
            this.chkEQPass.BackColor = System.Drawing.Color.Transparent;
            this.chkEQPass.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.chkEQPass.backHover = global::AEC.Properties.Resources.btn_hover;
            this.chkEQPass.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.chkEQPass.backPressed = global::AEC.Properties.Resources.btn_pressed;
            this.chkEQPass.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.chkEQPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.chkEQPass.isAutoCheck = true;
            this.chkEQPass.isChecked = false;
            this.chkEQPass.isShowCheckedBackWhenDsiabled = false;
            this.chkEQPass.isStretch = true;
            this.chkEQPass.Location = new System.Drawing.Point(84, 1);
            this.chkEQPass.Margin = new System.Windows.Forms.Padding(0);
            this.chkEQPass.Name = "chkEQPass";
            this.chkEQPass.Size = new System.Drawing.Size(80, 30);
            this.chkEQPass.TabIndex = 1;
            this.chkEQPass.Text = "EQ Pass";
            this.chkEQPass.userGDIPaint = true;
            // 
            // EQResetAndByPassPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.chkEQPass);
            this.Controls.Add(this.btnEQReset);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "EQResetAndByPassPanel";
            this.Size = new System.Drawing.Size(168, 32);
            this.ResumeLayout(false);

        }

        #endregion

        private gtButton btnEQReset;
        private gtCheckBox chkEQPass;
    }
}
