namespace AEC.UI
{
    partial class FormUnlockOutput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUnlockOutput));
            this.btnUnlock = new AEC.UI.Controls.MButton();
            this.lblPassword = new AEC.UI.Controls.MLabel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.Size = new System.Drawing.Size(320, 32);
            // 
            // btnUnlock
            // 
            this.btnUnlock.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnUnlock.BorderWidth = 1F;
            this.btnUnlock.DisabledBorderColor = System.Drawing.Color.Gray;
            this.btnUnlock.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnUnlock.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnUnlock.Enabled = false;
            this.btnUnlock.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnUnlock.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnUnlock.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnUnlock.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnUnlock.Location = new System.Drawing.Point(112, 128);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnUnlock.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnUnlock.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnUnlock.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnUnlock.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnUnlock.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.btnUnlock.Radius = 12;
            this.btnUnlock.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.btnUnlock.ShowBorder = true;
            this.btnUnlock.ShowText = true;
            this.btnUnlock.Size = new System.Drawing.Size(96, 27);
            this.btnUnlock.TabIndex = 10;
            this.btnUnlock.Text = "解锁";
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // lblPassword
            // 
            this.lblPassword.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblPassword.BorderWidth = 1F;
            this.lblPassword.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblPassword.Location = new System.Drawing.Point(16, 73);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Radius = 16;
            this.lblPassword.ShowBackground = false;
            this.lblPassword.ShowBorder = false;
            this.lblPassword.Size = new System.Drawing.Size(64, 23);
            this.lblPassword.TabIndex = 9;
            this.lblPassword.Text = "密码";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Location = new System.Drawing.Point(86, 70);
            this.txtPassword.MaxLength = 8;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(168, 27);
            this.txtPassword.TabIndex = 8;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // FormUnlockOutput
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(320, 194);
            this.Controls.Add(this.btnUnlock);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormUnlockOutput";
            this.Text = "解锁输出";
            this.Load += new System.EventHandler(this.FormUnlockOutput_Load);
            this.Controls.SetChildIndex(this.title, 0);
            this.Controls.SetChildIndex(this.txtPassword, 0);
            this.Controls.SetChildIndex(this.lblPassword, 0);
            this.Controls.SetChildIndex(this.btnUnlock, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MButton btnUnlock;
        private Controls.MLabel lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
    }
}
