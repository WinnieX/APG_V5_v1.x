using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace AEC.UI
{
    public partial class QRCodePanel : UserControl
    {
        public event EventHandler<EventArgs> PanelClick;

        public QRCodePanel()
        {
            InitializeComponent();
            InitFont();
            this.MouseClick += Panel_MouseClick;
            this.pictureBox.MouseClick += Panel_MouseClick;
            this.lblScanQRCode.MouseClick += Panel_MouseClick;
        }

        public override string Text
        {
            get
            {
                return lblScanQRCode.Text;
            }
            set
            {
                lblScanQRCode.Text = value;
                //lblScanQRCode.Invalidate();
            }
        }

        private void InitFont()
        {
            this.lblScanQRCode.Font = FontHelper.CreateFont10();
        }

        public Image QRCodeImage
        {
            get
            {
                return this.pictureBox.BackgroundImage;
            }
            set
            {
                this.pictureBox.BackgroundImage = value;
            }
        }

        private void Panel_MouseClick(object sender, MouseEventArgs e)
        {
            PanelClickedHandler();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            PanelClickedHandler();
        }

        private void PanelClickedHandler()
        {
            if (PanelClick != null)
            {
                PanelClick(this, EventArgs.Empty);
            }
        }
    }
}
