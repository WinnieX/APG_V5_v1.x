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

namespace AEC.UI
{
    public partial class FormAbout : AEC.UI.MFormBase
    {
        private const string webSite = "https://www.meng-audio.com/";
        private const string OEMProductName = "APG Sound Processor";

        public FormAbout()
        {
            InitializeComponent();            
            this.InitFont();            
        }

        protected override void UpdateDisplayLanguage()
        {
            this.Text = localization[Localization.formAbout];
        }

        protected override void OnLoadHandler()
        {
            base.OnLoadHandler();

            this.lblVersion.Text = "Version " + AEC.Tools.VersionHelper.GetVersion();
            this.linkLabel.Text = webSite;
            this.linkLabel.Visible = !AppConfig.isOEMVersion;
            if (AppConfig.isOEMVersion)
            {
                this.lblProductName.Text = OEMProductName;
            }

            string supportModelList = string.Format(localization[Localization.formAbout_SupportModels], System.Environment.NewLine + AppConfig.GetSupportDeviceModels());
            this.toolTipSupportModels.SetToolTip(this.lblVersion, supportModelList);
        }

        private void InitFont()
        {
            this.Font = FontHelper.CreateFont10();
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

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(webSite);
        }


        private int mouseRightButtonClickCount = 0;
        private const int MouseRightButtonClickThreshold = 7;
        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (++mouseRightButtonClickCount >= MouseRightButtonClickThreshold)
            {
                mouseRightButtonClickCount = 0;
                System.Diagnostics.Process.Start(webSite);
            }
        }
    }
}
