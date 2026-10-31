namespace AEC.UI
{
    partial class FormSuperUserPermission
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSuperUserPermission));
            this.txtPermissionCode = new System.Windows.Forms.TextBox();
            this.txtRequestCode = new System.Windows.Forms.TextBox();
            this.lblAuthorizationCode = new AEC.UI.gtLabel();
            this.lblRequestCode = new AEC.UI.gtLabel();
            this.btnPermission = new AEC.UI.gtButton();
            this.btnClose = new AEC.UI.gtButton();
            this.title = new AEC.UI.Title();
            this.SuspendLayout();
            // 
            // txtPermissionCode
            // 
            this.txtPermissionCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtPermissionCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPermissionCode.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtPermissionCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtPermissionCode.Location = new System.Drawing.Point(134, 91);
            this.txtPermissionCode.MaxLength = 32;
            this.txtPermissionCode.Name = "txtPermissionCode";
            this.txtPermissionCode.Size = new System.Drawing.Size(320, 27);
            this.txtPermissionCode.TabIndex = 16;
            this.txtPermissionCode.TextChanged += new System.EventHandler(this.txtPermissionCode_TextChanged);
            // 
            // txtRequestCode
            // 
            this.txtRequestCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.txtRequestCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRequestCode.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtRequestCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtRequestCode.Location = new System.Drawing.Point(134, 58);
            this.txtRequestCode.MaxLength = 32;
            this.txtRequestCode.Name = "txtRequestCode";
            this.txtRequestCode.ReadOnly = true;
            this.txtRequestCode.Size = new System.Drawing.Size(320, 27);
            this.txtRequestCode.TabIndex = 16;
            // 
            // lblAuthorizationCode
            // 
            this.lblAuthorizationCode.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblAuthorizationCode.BackColor = System.Drawing.Color.Transparent;
            this.lblAuthorizationCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblAuthorizationCode.Location = new System.Drawing.Point(64, 95);
            this.lblAuthorizationCode.Name = "lblAuthorizationCode";
            this.lblAuthorizationCode.Size = new System.Drawing.Size(64, 23);
            this.lblAuthorizationCode.TabIndex = 17;
            this.lblAuthorizationCode.Text = "授权码";
            // 
            // lblRequestCode
            // 
            this.lblRequestCode.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblRequestCode.BackColor = System.Drawing.Color.Transparent;
            this.lblRequestCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblRequestCode.Location = new System.Drawing.Point(64, 62);
            this.lblRequestCode.Name = "lblRequestCode";
            this.lblRequestCode.Size = new System.Drawing.Size(64, 23);
            this.lblRequestCode.TabIndex = 17;
            this.lblRequestCode.Text = "请求码";
            // 
            // btnPermission
            // 
            this.btnPermission.BackColor = System.Drawing.Color.Transparent;
            this.btnPermission.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.btnPermission.backHover = global::AEC.Properties.Resources.btn_hover;
            this.btnPermission.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.btnPermission.backPressed = global::AEC.Properties.Resources.btn_pressed;
            this.btnPermission.ForeColor = System.Drawing.Color.White;
            this.btnPermission.isStretch = true;
            this.btnPermission.Location = new System.Drawing.Point(128, 142);
            this.btnPermission.Name = "btnPermission";
            this.btnPermission.Size = new System.Drawing.Size(118, 30);
            this.btnPermission.TabIndex = 14;
            this.btnPermission.Text = "请求授权";
            this.btnPermission.userGDIPaint = true;
            this.btnPermission.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.btnClose.backHover = global::AEC.Properties.Resources.btn_hover;
            this.btnClose.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.btnClose.backPressed = global::AEC.Properties.Resources.btn_pressed;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.isStretch = true;
            this.btnClose.Location = new System.Drawing.Point(266, 142);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(118, 30);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "关闭";
            this.btnClose.userGDIPaint = true;
            this.btnClose.Click += new System.EventHandler(this.btn_Click);
            // 
            // title
            // 
            this.title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.title.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.isAllowDoubleClickMaximize = false;
            this.title.isShowCloseButton = true;
            this.title.isShowSystemBtns = false;
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Margin = new System.Windows.Forms.Padding(0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(512, 32);
            this.title.TabIndex = 8;
            // 
            // FormSuperUserPermission
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(512, 192);
            this.Controls.Add(this.lblAuthorizationCode);
            this.Controls.Add(this.lblRequestCode);
            this.Controls.Add(this.txtPermissionCode);
            this.Controls.Add(this.txtRequestCode);
            this.Controls.Add(this.btnPermission);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.title);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSuperUserPermission";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormRequestSuperUserPermission";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Title title;
        private gtButton btnPermission;
        private gtButton btnClose;
        private System.Windows.Forms.TextBox txtPermissionCode;
        private gtLabel lblAuthorizationCode;
        private System.Windows.Forms.TextBox txtRequestCode;
        private gtLabel lblRequestCode;
    }
}