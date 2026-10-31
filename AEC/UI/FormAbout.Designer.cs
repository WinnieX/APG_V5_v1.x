namespace AEC.UI
{
    partial class FormAbout
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.lblProductName = new AEC.UI.gtLabel();
            this.lblVersion = new AEC.UI.gtLabel();
            this.lblCopyRight = new AEC.UI.gtLabel();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.toolTipSupportModels = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.Size = new System.Drawing.Size(360, 32);
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox.BackgroundImage = global::AEC.Properties.Resources.icon_64;
            this.pictureBox.Location = new System.Drawing.Point(32, 64);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(64, 64);
            this.pictureBox.TabIndex = 4;
            this.pictureBox.TabStop = false;
            this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseClick);
            // 
            // lblProductName
            // 
            this.lblProductName.alignment = AEC.UI.gtLabel.AlignMent.Left;
            this.lblProductName.BackColor = System.Drawing.Color.Transparent;
            this.lblProductName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblProductName.Location = new System.Drawing.Point(128, 50);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(192, 23);
            this.lblProductName.TabIndex = 5;
            this.lblProductName.Text = "M-Audio AEC";
            // 
            // lblVersion
            // 
            this.lblVersion.alignment = AEC.UI.gtLabel.AlignMent.Left;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblVersion.Location = new System.Drawing.Point(128, 75);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(160, 23);
            this.lblVersion.TabIndex = 5;
            this.lblVersion.Text = "Version 1.0.0";
            // 
            // lblCopyRight
            // 
            this.lblCopyRight.alignment = AEC.UI.gtLabel.AlignMent.Left;
            this.lblCopyRight.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblCopyRight.Location = new System.Drawing.Point(128, 100);
            this.lblCopyRight.Name = "lblCopyRight";
            this.lblCopyRight.Size = new System.Drawing.Size(160, 23);
            this.lblCopyRight.TabIndex = 5;
            this.lblCopyRight.Text = "Copyright © 2026";
            // 
            // linkLabel
            // 
            this.linkLabel.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(136)))), ((int)(((byte)(209)))));
            this.linkLabel.AutoSize = true;
            this.linkLabel.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(169)))), ((int)(((byte)(244)))));
            this.linkLabel.Location = new System.Drawing.Point(128, 125);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(175, 15);
            this.linkLabel.TabIndex = 6;
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "https://www.meng-audio.com/";
            this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // FormAbout
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(360, 160);
            this.Controls.Add(this.linkLabel);
            this.Controls.Add(this.lblCopyRight);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.pictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAbout";
            this.Text = "关于";
            this.Controls.SetChildIndex(this.title, 0);
            this.Controls.SetChildIndex(this.pictureBox, 0);
            this.Controls.SetChildIndex(this.lblProductName, 0);
            this.Controls.SetChildIndex(this.lblVersion, 0);
            this.Controls.SetChildIndex(this.lblCopyRight, 0);
            this.Controls.SetChildIndex(this.linkLabel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private gtLabel lblProductName;
        private gtLabel lblVersion;
        private gtLabel lblCopyRight;
        private System.Windows.Forms.LinkLabel linkLabel;
        private System.Windows.Forms.ToolTip toolTipSupportModels;
    }
}