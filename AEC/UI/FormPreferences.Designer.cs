namespace AEC.UI
{
    partial class FormPreferences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPreferences));
            this.switchLastDeviceModel = new AEC.UI.Controls.MSwitch();
            this.cmbDeviceModelWhenStartup = new AEC.UI.gtComboBox();
            this.lblDeviceModelWhenStartup = new AEC.UI.gtLabel();
            this.title = new AEC.UI.Title();
            this.lblAutoConnection = new AEC.UI.gtLabel();
            this.switchAutoConnection = new AEC.UI.Controls.MSwitch();
            this.SuspendLayout();
            // 
            // switchLastDeviceModel
            // 
            this.switchLastDeviceModel.BackColor = System.Drawing.Color.Transparent;
            this.switchLastDeviceModel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchLastDeviceModel.BorderWidth = 1F;
            this.switchLastDeviceModel.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.switchLastDeviceModel.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.switchLastDeviceModel.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.switchLastDeviceModel.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.switchLastDeviceModel.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.switchLastDeviceModel.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.switchLastDeviceModel.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchLastDeviceModel.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.switchLastDeviceModel.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.switchLastDeviceModel.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.switchLastDeviceModel.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.switchLastDeviceModel.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchLastDeviceModel.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.switchLastDeviceModel.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.switchLastDeviceModel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.switchLastDeviceModel.isAutoCheck = true;
            this.switchLastDeviceModel.IsChecked = false;
            this.switchLastDeviceModel.IsCheckedInit = false;
            this.switchLastDeviceModel.Location = new System.Drawing.Point(194, 83);
            this.switchLastDeviceModel.Name = "switchLastDeviceModel";
            this.switchLastDeviceModel.Radius = 29;
            this.switchLastDeviceModel.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.switchLastDeviceModel.ShowBorder = false;
            this.switchLastDeviceModel.ShowText = true;
            this.switchLastDeviceModel.Size = new System.Drawing.Size(232, 29);
            this.switchLastDeviceModel.SwitchWdith = 48;
            this.switchLastDeviceModel.TabIndex = 12;
            this.switchLastDeviceModel.Text = "最后一次联机设备型号";
            this.switchLastDeviceModel.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.switchLastDeviceModel.UncheckedDisbaledFillColor = System.Drawing.Color.Gray;
            this.switchLastDeviceModel.UncheckedDisbaledTextColor = System.Drawing.Color.Silver;
            this.switchLastDeviceModel.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.switchLastDeviceModel.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.switchLastDeviceModel.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.switchLastDeviceModel.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchLastDeviceModel.UncheckedNormalFillColor = System.Drawing.Color.Silver;
            this.switchLastDeviceModel.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(240)))));
            this.switchLastDeviceModel.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.switchLastDeviceModel.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchLastDeviceModel.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.switchLastDeviceModel.CheckedChanged += new System.EventHandler(this.switch_CheckedChanged);
            // 
            // cmbDeviceModelWhenStartup
            // 
            this.cmbDeviceModelWhenStartup.ArrowColor = System.Drawing.Color.Gray;
            this.cmbDeviceModelWhenStartup.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbDeviceModelWhenStartup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeviceModelWhenStartup.Enabled = false;
            this.cmbDeviceModelWhenStartup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.cmbDeviceModelWhenStartup.FormattingEnabled = true;
            this.cmbDeviceModelWhenStartup.Location = new System.Drawing.Point(192, 128);
            this.cmbDeviceModelWhenStartup.Name = "cmbDeviceModelWhenStartup";
            this.cmbDeviceModelWhenStartup.Size = new System.Drawing.Size(194, 23);
            this.cmbDeviceModelWhenStartup.TabIndex = 11;
            this.cmbDeviceModelWhenStartup.SelectedIndexChanged += new System.EventHandler(this.cmbDeviceModelWhenStartup_SelectedIndexChanged);
            // 
            // lblDeviceModelWhenStartup
            // 
            this.lblDeviceModelWhenStartup.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblDeviceModelWhenStartup.BackColor = System.Drawing.Color.Transparent;
            this.lblDeviceModelWhenStartup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblDeviceModelWhenStartup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblDeviceModelWhenStartup.Location = new System.Drawing.Point(12, 86);
            this.lblDeviceModelWhenStartup.Name = "lblDeviceModelWhenStartup";
            this.lblDeviceModelWhenStartup.Size = new System.Drawing.Size(172, 23);
            this.lblDeviceModelWhenStartup.TabIndex = 10;
            this.lblDeviceModelWhenStartup.Text = "启动时设备型号";
            // 
            // title
            // 
            this.title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.title.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.isAllowDoubleClickMaximize = false;
            this.title.isShowCloseButton = true;
            this.title.isShowSystemBtns = false;
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Margin = new System.Windows.Forms.Padding(0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(568, 32);
            this.title.TabIndex = 5;
            // 
            // lblAutoConnection
            // 
            this.lblAutoConnection.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblAutoConnection.BackColor = System.Drawing.Color.Transparent;
            this.lblAutoConnection.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblAutoConnection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblAutoConnection.Location = new System.Drawing.Point(12, 51);
            this.lblAutoConnection.Name = "lblAutoConnection";
            this.lblAutoConnection.Size = new System.Drawing.Size(172, 23);
            this.lblAutoConnection.TabIndex = 13;
            this.lblAutoConnection.Text = "启动后自动联机";
            // 
            // switchAutoConnection
            // 
            this.switchAutoConnection.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchAutoConnection.BorderWidth = 1F;
            this.switchAutoConnection.CheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.switchAutoConnection.CheckedDisbaledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(162)))), ((int)(((byte)(78)))));
            this.switchAutoConnection.CheckedDisbaledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.switchAutoConnection.CheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.switchAutoConnection.CheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(213)))), ((int)(((byte)(106)))));
            this.switchAutoConnection.CheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.switchAutoConnection.CheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchAutoConnection.CheckedNormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(193)))), ((int)(((byte)(96)))));
            this.switchAutoConnection.CheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.switchAutoConnection.CheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.switchAutoConnection.CheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(175)))), ((int)(((byte)(87)))));
            this.switchAutoConnection.CheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchAutoConnection.DisplayOrder = AEC.UI.Controls.SwitchDisplayOrder.BoxText;
            this.switchAutoConnection.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.switchAutoConnection.isAutoCheck = true;
            this.switchAutoConnection.IsChecked = false;
            this.switchAutoConnection.IsCheckedInit = false;
            this.switchAutoConnection.Location = new System.Drawing.Point(194, 45);
            this.switchAutoConnection.Name = "switchAutoConnection";
            this.switchAutoConnection.Radius = 29;
            this.switchAutoConnection.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.switchAutoConnection.ShowBorder = false;
            this.switchAutoConnection.ShowText = true;
            this.switchAutoConnection.Size = new System.Drawing.Size(51, 29);
            this.switchAutoConnection.SwitchWdith = 48;
            this.switchAutoConnection.TabIndex = 23;
            this.switchAutoConnection.UncheckedDisbaledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.switchAutoConnection.UncheckedDisbaledFillColor = System.Drawing.Color.Gray;
            this.switchAutoConnection.UncheckedDisbaledTextColor = System.Drawing.Color.Silver;
            this.switchAutoConnection.UncheckedHoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.switchAutoConnection.UncheckedHoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.switchAutoConnection.UncheckedHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.switchAutoConnection.UncheckedNormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchAutoConnection.UncheckedNormalFillColor = System.Drawing.Color.Silver;
            this.switchAutoConnection.UncheckedNormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(240)))));
            this.switchAutoConnection.UncheckedPressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.switchAutoConnection.UncheckedPressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.switchAutoConnection.UncheckedPressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.switchAutoConnection.CheckedChanged += new System.EventHandler(this.switch_CheckedChanged);
            // 
            // FormPreferences
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(568, 310);
            this.Controls.Add(this.switchAutoConnection);
            this.Controls.Add(this.lblAutoConnection);
            this.Controls.Add(this.switchLastDeviceModel);
            this.Controls.Add(this.cmbDeviceModelWhenStartup);
            this.Controls.Add(this.lblDeviceModelWhenStartup);
            this.Controls.Add(this.title);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPreferences";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormPreferences";
            this.ResumeLayout(false);

        }

        #endregion

        private Title title;
        private gtComboBox cmbDeviceModelWhenStartup;
        private gtLabel lblDeviceModelWhenStartup;
        private Controls.MSwitch switchLastDeviceModel;
        private gtLabel lblAutoConnection;
        private Controls.MSwitch switchAutoConnection;
    }
}