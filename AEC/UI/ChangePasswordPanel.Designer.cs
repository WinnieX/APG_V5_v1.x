namespace AEC.UI
{
    partial class ChangePasswordPanel
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
            this.lblOldPassword = new AEC.UI.gtLabel();
            this.txtOldPassword = new System.Windows.Forms.TextBox();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmNewPassword = new AEC.UI.gtLabel();
            this.txtConfirmNewPassword = new System.Windows.Forms.TextBox();
            this.lblNewPassword = new AEC.UI.gtLabel();
            this.SuspendLayout();
            // 
            // lblOldPassword
            // 
            this.lblOldPassword.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblOldPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblOldPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblOldPassword.ForeColor = System.Drawing.Color.White;
            this.lblOldPassword.Location = new System.Drawing.Point(18, 43);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.Size = new System.Drawing.Size(120, 20);
            this.lblOldPassword.TabIndex = 2;
            this.lblOldPassword.Text = "旧密码";
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(240)))));
            this.txtOldPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOldPassword.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtOldPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.txtOldPassword.Location = new System.Drawing.Point(144, 39);
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.PasswordChar = '●';
            this.txtOldPassword.ShortcutsEnabled = false;
            this.txtOldPassword.Size = new System.Drawing.Size(64, 27);
            this.txtOldPassword.TabIndex = 0;
            this.txtOldPassword.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtOldPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            this.txtOldPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(240)))));
            this.txtNewPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewPassword.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtNewPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.txtNewPassword.Location = new System.Drawing.Point(144, 71);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '●';
            this.txtNewPassword.ShortcutsEnabled = false;
            this.txtNewPassword.Size = new System.Drawing.Size(64, 27);
            this.txtNewPassword.TabIndex = 1;
            this.txtNewPassword.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtNewPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            this.txtNewPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // lblConfirmNewPassword
            // 
            this.lblConfirmNewPassword.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblConfirmNewPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblConfirmNewPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblConfirmNewPassword.ForeColor = System.Drawing.Color.White;
            this.lblConfirmNewPassword.Location = new System.Drawing.Point(18, 107);
            this.lblConfirmNewPassword.Name = "lblConfirmNewPassword";
            this.lblConfirmNewPassword.Size = new System.Drawing.Size(120, 20);
            this.lblConfirmNewPassword.TabIndex = 2;
            this.lblConfirmNewPassword.Text = "确认密码";
            // 
            // txtConfirmNewPassword
            // 
            this.txtConfirmNewPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(240)))));
            this.txtConfirmNewPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfirmNewPassword.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtConfirmNewPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.txtConfirmNewPassword.Location = new System.Drawing.Point(144, 103);
            this.txtConfirmNewPassword.Name = "txtConfirmNewPassword";
            this.txtConfirmNewPassword.PasswordChar = '●';
            this.txtConfirmNewPassword.ShortcutsEnabled = false;
            this.txtConfirmNewPassword.Size = new System.Drawing.Size(64, 27);
            this.txtConfirmNewPassword.TabIndex = 2;
            this.txtConfirmNewPassword.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtConfirmNewPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            this.txtConfirmNewPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblNewPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblNewPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblNewPassword.ForeColor = System.Drawing.Color.White;
            this.lblNewPassword.Location = new System.Drawing.Point(18, 75);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(120, 20);
            this.lblNewPassword.TabIndex = 2;
            this.lblNewPassword.Text = "新密码";
            // 
            // ChangePasswordPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblOldPassword);
            this.Controls.Add(this.txtOldPassword);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.lblConfirmNewPassword);
            this.Controls.Add(this.txtConfirmNewPassword);
            this.Controls.Add(this.lblNewPassword);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "ChangePasswordPanel";
            this.Size = new System.Drawing.Size(244, 148);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private gtLabel lblOldPassword;
        private gtLabel lblNewPassword;
        private gtLabel lblConfirmNewPassword;
        private System.Windows.Forms.TextBox txtOldPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.TextBox txtConfirmNewPassword;
    }
}
