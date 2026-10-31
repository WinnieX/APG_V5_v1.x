using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.UI.Skin;
using AEC.UI.Utility;
using AEC.Misc;
using AEC.Effectors;

namespace AEC.UI
{
    public partial class FormPreferences : Form
    {
        public FormPreferences()
        {
            InitializeComponent();
            this.title.BindingForm(this);
            this.InitFont();
        }

        private void InitFont()
        {
            this.Font = FontHelper.CreateFont10();
            this.title.Font = FontHelper.CreateFont10();
            this.lblAutoConnection.Font = FontHelper.CreateFont10();
            this.lblDeviceModelWhenStartup.Font = FontHelper.CreateFont10();
            this.switchLastDeviceModel.Font = FontHelper.CreateFont10();
            this.cmbDeviceModelWhenStartup.Font = FontHelper.CreateFont10();
        }

        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.title.Text = value;
            }
        }

        private void InitUI()
        {
            this.switchAutoConnection.IsCheckedInit = Config.GetInstance().isAutoConnection;

            List<string> modelList = AppConfig.GetSupportedDeviceModelList();

            if(modelList.Count > 1)
            {
                this.switchLastDeviceModel.IsCheckedInit = Config.GetInstance().useLastConnectedModelWhenStarts;

                this.cmbDeviceModelWhenStartup.Items.AddRange(modelList.ToArray());

                this.cmbDeviceModelWhenStartup.Enabled = !Config.GetInstance().useLastConnectedModelWhenStarts;

                this.cmbDeviceModelWhenStartup.SelectedIndexChanged -= cmbDeviceModelWhenStartup_SelectedIndexChanged;

                if (modelList.Contains(Config.GetInstance().defaultDevcieModel))
                {
                    this.cmbDeviceModelWhenStartup.Text = Config.GetInstance().defaultDevcieModel;
                }
                else
                {
                    this.cmbDeviceModelWhenStartup.SelectedIndex = 0;
                }

                this.cmbDeviceModelWhenStartup.SelectedIndexChanged += cmbDeviceModelWhenStartup_SelectedIndexChanged;
            }
            else
            {
                this.lblDeviceModelWhenStartup.Visible = false;
                this.switchLastDeviceModel.Visible = false;
                this.cmbDeviceModelWhenStartup.Visible = false;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.InitUI();

            this.UpdateDisplayLanguage();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            GDIHelper.FillRectangle(e.Graphics, this.ClientRectangle, SkinManager.CurrentSkin.proModeBackgroundColor);

            int borderWidth = 1;
            using (Pen border = new Pen(Color.FromArgb(96, 96, 96), borderWidth))
            {
                Point[] points = new Point[]
                {
                    new Point(borderWidth - 1, borderWidth - 1),
                    new Point(borderWidth - 1, this.Height - (borderWidth)),
                    new Point(this.Width - (borderWidth), this.Height - (borderWidth)),
                    new Point(this.Width - (borderWidth), borderWidth - 1),
                };
                e.Graphics.DrawLines(border, points);
            }
        }

        private Localization localization = Config.GetInstance().localization;
        private void UpdateDisplayLanguage()
        {
            this.Text = localization[Localization.formPreferences];
            this.lblAutoConnection.Text = localization[Localization.formCommSettings_AutoConnection];
            this.lblDeviceModelWhenStartup.Text = localization[Localization.formPreferences_DeviceModelWhenStarts];
            this.switchLastDeviceModel.Text = localization[Localization.formPreferences_UseLastConnectedDeviceModel];
            //this.lblAutoConnection.Text = localization[Localization.formCommSettings_AutoConnection];
            //this.lblCommMethod.Text = localization[Localization.formCommSettings_CommMethod];
            //this.lblCommRetransmission.Text = localization[Localization.formCommSettings_CommRetransmission];

            //this.lblDefaultSerialPort.Text = localization[Localization.formCommSettings_DefaultSerialPort];

            //this.lblSerialPortMode.Text = localization[Localization.formCommSettings_SerialPortMode];

            //this.rdoAuto.Text = localization[Localization.formCommSettings_AutoMode];
            //this.rdoManual.Text = localization[Localization.formCommSettings_ManualMode];

            //this.lblIPAddress.Text = localization[Localization.formCommSettings_IPAddress];
            //this.lblPort.Text = localization[Localization.formCommSettings_Port];

            //this.btnRestoreToDefault.Text = localization[Localization.formCommSettings_RestoreToDefault];
            //this.btnClose.Text = localization[Localization.formCommSettings_Close];
        }

        private void switch_CheckedChanged(object sender, EventArgs e)
        {
            if(sender.Equals(switchAutoConnection))
            {
                Config.GetInstance().isAutoConnection = this.switchAutoConnection.IsChecked;
            }
            else if(sender.Equals(switchLastDeviceModel))
            {
                Config.GetInstance().useLastConnectedModelWhenStarts = this.switchLastDeviceModel.IsChecked;
                this.cmbDeviceModelWhenStartup.Enabled = !Config.GetInstance().useLastConnectedModelWhenStarts;
            }
        }

        private void cmbDeviceModelWhenStartup_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.GetInstance().defaultDevcieModel = this.cmbDeviceModelWhenStartup.Text;
        }
    }
}
