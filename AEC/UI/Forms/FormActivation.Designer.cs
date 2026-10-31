namespace AEC.UI.Forms
{
    partial class FormActivation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormActivation));
            this.lblMachineCode = new AEC.UI.Controls.MLabel();
            this.txtMachineCode = new System.Windows.Forms.TextBox();
            this.lblRequestCode = new AEC.UI.Controls.MLabel();
            this.txtRequestCode = new System.Windows.Forms.TextBox();
            this.lblActivationCode = new AEC.UI.Controls.MLabel();
            this.txtActivationCode = new System.Windows.Forms.TextBox();
            this.btnActivation = new AEC.UI.Controls.MButton();
            this.SuspendLayout();
            // 
            // lblMachineCode
            // 
            this.lblMachineCode.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblMachineCode.BackColor = System.Drawing.Color.Transparent;
            this.lblMachineCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblMachineCode.BorderWidth = 1F;
            this.lblMachineCode.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblMachineCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMachineCode.Location = new System.Drawing.Point(12, 63);
            this.lblMachineCode.Name = "lblMachineCode";
            this.lblMachineCode.Radius = 16;
            this.lblMachineCode.ShowBackground = false;
            this.lblMachineCode.ShowBorder = false;
            this.lblMachineCode.Size = new System.Drawing.Size(84, 23);
            this.lblMachineCode.TabIndex = 42;
            this.lblMachineCode.Text = "机器码";
            // 
            // txtMachineCode
            // 
            this.txtMachineCode.AllowDrop = true;
            this.txtMachineCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtMachineCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMachineCode.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtMachineCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtMachineCode.Location = new System.Drawing.Point(102, 59);
            this.txtMachineCode.Name = "txtMachineCode";
            this.txtMachineCode.ReadOnly = true;
            this.txtMachineCode.Size = new System.Drawing.Size(320, 27);
            this.txtMachineCode.TabIndex = 41;
            // 
            // lblRequestCode
            // 
            this.lblRequestCode.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRequestCode.BackColor = System.Drawing.Color.Transparent;
            this.lblRequestCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblRequestCode.BorderWidth = 1F;
            this.lblRequestCode.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblRequestCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblRequestCode.Location = new System.Drawing.Point(12, 104);
            this.lblRequestCode.Name = "lblRequestCode";
            this.lblRequestCode.Radius = 16;
            this.lblRequestCode.ShowBackground = false;
            this.lblRequestCode.ShowBorder = false;
            this.lblRequestCode.Size = new System.Drawing.Size(84, 23);
            this.lblRequestCode.TabIndex = 44;
            this.lblRequestCode.Text = "请求码";
            // 
            // txtRequestCode
            // 
            this.txtRequestCode.AllowDrop = true;
            this.txtRequestCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtRequestCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRequestCode.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtRequestCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtRequestCode.Location = new System.Drawing.Point(102, 102);
            this.txtRequestCode.Name = "txtRequestCode";
            this.txtRequestCode.ReadOnly = true;
            this.txtRequestCode.Size = new System.Drawing.Size(320, 27);
            this.txtRequestCode.TabIndex = 43;
            // 
            // lblActivationCode
            // 
            this.lblActivationCode.Alignment = System.Drawing.ContentAlignment.MiddleRight;
            this.lblActivationCode.BackColor = System.Drawing.Color.Transparent;
            this.lblActivationCode.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(100)))), ((int)(((byte)(189)))));
            this.lblActivationCode.BorderWidth = 1F;
            this.lblActivationCode.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblActivationCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblActivationCode.Location = new System.Drawing.Point(12, 146);
            this.lblActivationCode.Name = "lblActivationCode";
            this.lblActivationCode.Radius = 16;
            this.lblActivationCode.ShowBackground = false;
            this.lblActivationCode.ShowBorder = false;
            this.lblActivationCode.Size = new System.Drawing.Size(84, 23);
            this.lblActivationCode.TabIndex = 46;
            this.lblActivationCode.Text = "激活码";
            // 
            // txtActivationCode
            // 
            this.txtActivationCode.AllowDrop = true;
            this.txtActivationCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtActivationCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtActivationCode.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtActivationCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtActivationCode.Location = new System.Drawing.Point(102, 144);
            this.txtActivationCode.MaxLength = 32;
            this.txtActivationCode.Name = "txtActivationCode";
            this.txtActivationCode.Size = new System.Drawing.Size(320, 27);
            this.txtActivationCode.TabIndex = 45;
            this.txtActivationCode.TextChanged += new System.EventHandler(this.txtActivationCode_TextChanged);
            // 
            // btnActivation
            // 
            this.btnActivation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnActivation.BorderWidth = 1F;
            this.btnActivation.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnActivation.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnActivation.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnActivation.Enabled = false;
            this.btnActivation.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnActivation.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnActivation.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.btnActivation.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnActivation.Location = new System.Drawing.Point(193, 196);
            this.btnActivation.Name = "btnActivation";
            this.btnActivation.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnActivation.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnActivation.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnActivation.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.btnActivation.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btnActivation.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.btnActivation.Radius = 12;
            this.btnActivation.ShowBorder = true;
            this.btnActivation.ShowText = true;
            this.btnActivation.Size = new System.Drawing.Size(96, 27);
            this.btnActivation.TabIndex = 47;
            this.btnActivation.Text = "激活";
            this.btnActivation.Click += new System.EventHandler(this.btnActivation_Click);
            // 
            // FormActivation
            // 
            this.ClientSize = new System.Drawing.Size(480, 256);
            this.Controls.Add(this.btnActivation);
            this.Controls.Add(this.lblActivationCode);
            this.Controls.Add(this.txtActivationCode);
            this.Controls.Add(this.lblRequestCode);
            this.Controls.Add(this.txtRequestCode);
            this.Controls.Add(this.lblMachineCode);
            this.Controls.Add(this.txtMachineCode);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormActivation";
            this.Text = "激活";
            this.Controls.SetChildIndex(this.txtMachineCode, 0);
            this.Controls.SetChildIndex(this.lblMachineCode, 0);
            this.Controls.SetChildIndex(this.txtRequestCode, 0);
            this.Controls.SetChildIndex(this.lblRequestCode, 0);
            this.Controls.SetChildIndex(this.txtActivationCode, 0);
            this.Controls.SetChildIndex(this.lblActivationCode, 0);
            this.Controls.SetChildIndex(this.btnActivation, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MLabel lblMachineCode;
        private System.Windows.Forms.TextBox txtMachineCode;
        private Controls.MLabel lblRequestCode;
        private System.Windows.Forms.TextBox txtRequestCode;
        private Controls.MLabel lblActivationCode;
        private System.Windows.Forms.TextBox txtActivationCode;
        private Controls.MButton btnActivation;
    }
}
