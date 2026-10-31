namespace AEC.UI
{
    partial class FormComm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormComm));
            this.panelCommProgress = new System.Windows.Forms.Panel();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.panelDeviceNotActivated = new System.Windows.Forms.Panel();
            this.panelDeviceIsLocked = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnClose = new AEC.UI.gtButton();
            this.btnDisconnect = new AEC.UI.gtButton();
            this.lblPassword = new AEC.UI.gtLabel();
            this.lblDeviceIsLocked = new AEC.UI.gtLabel();
            this.btnConfirm = new AEC.UI.gtButton();
            this.lblDeviceNotActivated = new AEC.UI.gtLabel();
            this.vScrollbar = new AEC.UI.VScrollbar();
            this.circleProgress = new AEC.UI.CircleProgress();
            this.title = new AEC.UI.Title();
            this.panelCommProgress.SuspendLayout();
            this.panelDeviceNotActivated.SuspendLayout();
            this.panelDeviceIsLocked.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCommProgress
            // 
            this.panelCommProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.panelCommProgress.Controls.Add(this.vScrollbar);
            this.panelCommProgress.Controls.Add(this.richTextBoxLog);
            this.panelCommProgress.Controls.Add(this.circleProgress);
            this.panelCommProgress.Location = new System.Drawing.Point(3, 32);
            this.panelCommProgress.Margin = new System.Windows.Forms.Padding(0);
            this.panelCommProgress.Name = "panelCommProgress";
            this.panelCommProgress.Size = new System.Drawing.Size(634, 180);
            this.panelCommProgress.TabIndex = 2;
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.richTextBoxLog.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.richTextBoxLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.richTextBoxLog.HideSelection = false;
            this.richTextBoxLog.Location = new System.Drawing.Point(179, 22);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.Size = new System.Drawing.Size(431, 138);
            this.richTextBoxLog.TabIndex = 3;
            this.richTextBoxLog.Text = "";
            // 
            // panelDeviceNotActivated
            // 
            this.panelDeviceNotActivated.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.panelDeviceNotActivated.Controls.Add(this.lblDeviceNotActivated);
            this.panelDeviceNotActivated.Location = new System.Drawing.Point(3, 32);
            this.panelDeviceNotActivated.Name = "panelDeviceNotActivated";
            this.panelDeviceNotActivated.Size = new System.Drawing.Size(634, 180);
            this.panelDeviceNotActivated.TabIndex = 4;
            // 
            // panelDeviceIsLocked
            // 
            this.panelDeviceIsLocked.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.panelDeviceIsLocked.Controls.Add(this.lblPassword);
            this.panelDeviceIsLocked.Controls.Add(this.txtPassword);
            this.panelDeviceIsLocked.Controls.Add(this.lblDeviceIsLocked);
            this.panelDeviceIsLocked.Controls.Add(this.btnConfirm);
            this.panelDeviceIsLocked.Location = new System.Drawing.Point(3, 32);
            this.panelDeviceIsLocked.Name = "panelDeviceIsLocked";
            this.panelDeviceIsLocked.Size = new System.Drawing.Size(634, 180);
            this.panelDeviceIsLocked.TabIndex = 4;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.txtPassword.Location = new System.Drawing.Point(239, 103);
            this.txtPassword.MaxLength = 4;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(109, 27);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.panelBottom.Controls.Add(this.btnClose);
            this.panelBottom.Controls.Add(this.btnDisconnect);
            this.panelBottom.Location = new System.Drawing.Point(3, 212);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(0);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(635, 41);
            this.panelBottom.TabIndex = 5;
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
            this.btnClose.Location = new System.Drawing.Point(536, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关闭";
            this.btnClose.userGDIPaint = true;
            this.btnClose.Click += new System.EventHandler(this.btn_click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.BackColor = System.Drawing.Color.Transparent;
            this.btnDisconnect.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.btnDisconnect.backHover = global::AEC.Properties.Resources.btn_hover;
            this.btnDisconnect.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.btnDisconnect.backPressed = global::AEC.Properties.Resources.btn_pressed;
            this.btnDisconnect.ForeColor = System.Drawing.Color.White;
            this.btnDisconnect.isStretch = true;
            this.btnDisconnect.Location = new System.Drawing.Point(523, 0);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(87, 30);
            this.btnDisconnect.TabIndex = 1;
            this.btnDisconnect.Text = "断开连接";
            this.btnDisconnect.userGDIPaint = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btn_click);
            // 
            // lblPassword
            // 
            this.lblPassword.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblPassword.Location = new System.Drawing.Point(156, 106);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(75, 20);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "密码";
            // 
            // lblDeviceIsLocked
            // 
            this.lblDeviceIsLocked.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblDeviceIsLocked.BackColor = System.Drawing.Color.Transparent;
            this.lblDeviceIsLocked.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblDeviceIsLocked.ForeColor = System.Drawing.Color.Red;
            this.lblDeviceIsLocked.Location = new System.Drawing.Point(50, 62);
            this.lblDeviceIsLocked.Name = "lblDeviceIsLocked";
            this.lblDeviceIsLocked.Size = new System.Drawing.Size(534, 23);
            this.lblDeviceIsLocked.TabIndex = 1;
            this.lblDeviceIsLocked.Text = "设备已被锁定，请输入4位密码解锁!";
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirm.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.btnConfirm.backHover = global::AEC.Properties.Resources.btn_hover;
            this.btnConfirm.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.btnConfirm.backPressed = global::AEC.Properties.Resources.btn_pressed;
            this.btnConfirm.Enabled = false;
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.isStretch = true;
            this.btnConfirm.Location = new System.Drawing.Point(354, 101);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 30);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.userGDIPaint = true;
            this.btnConfirm.Click += new System.EventHandler(this.btn_click);
            // 
            // lblDeviceNotActivated
            // 
            this.lblDeviceNotActivated.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblDeviceNotActivated.BackColor = System.Drawing.Color.Transparent;
            this.lblDeviceNotActivated.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblDeviceNotActivated.ForeColor = System.Drawing.Color.Red;
            this.lblDeviceNotActivated.Location = new System.Drawing.Point(50, 93);
            this.lblDeviceNotActivated.Name = "lblDeviceNotActivated";
            this.lblDeviceNotActivated.Size = new System.Drawing.Size(534, 23);
            this.lblDeviceNotActivated.TabIndex = 1;
            this.lblDeviceNotActivated.Text = "设备未激活，请先激活后再使用!";
            // 
            // vScrollbar
            // 
            this.vScrollbar.BtnHeight = 18;
            this.vScrollbar.ConerRadius = 2;
            this.vScrollbar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.vScrollbar.IsRadius = true;
            this.vScrollbar.IsShowRect = false;
            this.vScrollbar.LargeChange = 10;
            this.vScrollbar.Location = new System.Drawing.Point(590, 22);
            this.vScrollbar.Maximum = 100;
            this.vScrollbar.Minimum = 0;
            this.vScrollbar.Name = "vScrollbar";
            this.vScrollbar.RectColor = System.Drawing.Color.Silver;
            this.vScrollbar.RectWidth = 1;
            this.vScrollbar.Size = new System.Drawing.Size(18, 138);
            this.vScrollbar.SmallChange = 1;
            this.vScrollbar.TabIndex = 4;
            this.vScrollbar.Text = "vScrollbar1";
            this.vScrollbar.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(177)))), ((int)(((byte)(178)))));
            this.vScrollbar.Value = 0;
            this.vScrollbar.Visible = false;
            this.vScrollbar.wheelStep = 1;
            // 
            // circleProgress
            // 
            this.circleProgress.BackColor = System.Drawing.Color.Transparent;
            this.circleProgress.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.circleProgress.ForeColor = System.Drawing.Color.LightGray;
            this.circleProgress.Location = new System.Drawing.Point(27, 28);
            this.circleProgress.Name = "circleProgress";
            this.circleProgress.Size = new System.Drawing.Size(128, 128);
            this.circleProgress.TabIndex = 2;
            this.circleProgress.Text = "circleProgress1";
            // 
            // title
            // 
            this.title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.isAllowDoubleClickMaximize = false;
            this.title.isShowCloseButton = false;
            this.title.isShowSystemBtns = false;
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Margin = new System.Windows.Forms.Padding(0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(640, 32);
            this.title.TabIndex = 0;
            // 
            // FormComm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(640, 256);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelDeviceIsLocked);
            this.Controls.Add(this.panelDeviceNotActivated);
            this.Controls.Add(this.panelCommProgress);
            this.Controls.Add(this.title);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormComm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormComm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormComm_FormClosed);
            this.Load += new System.EventHandler(this.FormComm_Load);
            this.panelCommProgress.ResumeLayout(false);
            this.panelDeviceNotActivated.ResumeLayout(false);
            this.panelDeviceIsLocked.ResumeLayout(false);
            this.panelDeviceIsLocked.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Title title;
        private gtButton btnClose;
        private System.Windows.Forms.Panel panelCommProgress;
        private CircleProgress circleProgress;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.Panel panelDeviceNotActivated;
        private gtLabel lblDeviceNotActivated;
        private gtButton btnDisconnect;
        private System.Windows.Forms.Panel panelDeviceIsLocked;
        private gtLabel lblDeviceIsLocked;
        private gtButton btnConfirm;
        private gtLabel lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Panel panelBottom;
        private VScrollbar vScrollbar;

    }
}