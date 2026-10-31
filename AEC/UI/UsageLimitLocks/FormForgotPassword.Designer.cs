namespace AEC.UI.UsageLimitLocks
{
    partial class FormForgotPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForgotPassword));
            this.txtForgotPassword = new System.Windows.Forms.TextBox();
            this.btnCopy = new AEC.UI.Controls.MButton();
            this.SuspendLayout();
            // 
            // txtForgotPassword
            // 
            this.txtForgotPassword.AllowDrop = true;
            this.txtForgotPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtForgotPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtForgotPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtForgotPassword.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtForgotPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtForgotPassword.Location = new System.Drawing.Point(16, 48);
            this.txtForgotPassword.Multiline = true;
            this.txtForgotPassword.Name = "txtForgotPassword";
            this.txtForgotPassword.ReadOnly = true;
            this.txtForgotPassword.Size = new System.Drawing.Size(352, 96);
            this.txtForgotPassword.TabIndex = 43;
            // 
            // btnCopy
            // 
            this.btnCopy.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnCopy.BorderWidth = 1F;
            this.btnCopy.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCopy.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnCopy.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnCopy.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnCopy.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCopy.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnCopy.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCopy.Location = new System.Drawing.Point(152, 160);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnCopy.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(120)))), ((int)(((byte)(253)))));
            this.btnCopy.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCopy.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.btnCopy.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnCopy.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnCopy.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnCopy.Radius = 12;
            this.btnCopy.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.btnCopy.ShowBorder = false;
            this.btnCopy.ShowText = true;
            this.btnCopy.Size = new System.Drawing.Size(80, 27);
            this.btnCopy.TabIndex = 68;
            this.btnCopy.Text = "复制";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // FormForgotPassword
            // 
            this.ClientSize = new System.Drawing.Size(384, 208);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.txtForgotPassword);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormForgotPassword";
            this.Text = "忘记密码";
            this.Controls.SetChildIndex(this.txtForgotPassword, 0);
            this.Controls.SetChildIndex(this.btnCopy, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtForgotPassword;
        private Controls.MButton btnCopy;
    }
}
