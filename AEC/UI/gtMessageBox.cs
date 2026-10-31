using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.Misc;
using AEC.UI.Skin;
using AEC.UI.Utility;

namespace AEC.UI
{
    public partial class gtMessageBox : Form
    {
        //private DialogResult dr = DialogResult.Cancel;
        private Localization localization = Config.GetInstance().localization;
        //private string message = string.Empty;
        // string caption = "AEC";
        //private MessageBoxButtons msgBoxButtons = MessageBoxButtons.OKCancel;
        //private MessageBoxIcon msgBoxIcon = MessageBoxIcon.Information;

        public gtMessageBox(MessageBoxButtons msgBoxButtons, MessageBoxIcon msgBoxIcon)
        {
            InitializeComponent();
            this.title.BindingForm(this);

            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            InitFont();
            InitTheme();
            UpdateDisplayLanguage();

            if(msgBoxButtons == MessageBoxButtons.OK)
            {
                this.buttonOK.Location = this.buttonCancel.Location;
                this.buttonCancel.Visible = false;
            }

            Image image = null;
            switch(msgBoxIcon)
            {
                case MessageBoxIcon.Error:
                    image = global::AEC.Properties.Resources.error;
                    break;

                case MessageBoxIcon.Warning:
                    image = global::AEC.Properties.Resources.warning;
                    break;

                case MessageBoxIcon.Question:
                    image = global::AEC.Properties.Resources.question;
                    break;

                default:
                    image = global::AEC.Properties.Resources.information;
                    break;
            }

            this.pictureBox.BackgroundImage = image;
        }

        private void InitFont()
        {
            this.Font = FontHelper.CreateFont10();
            this.title.Font = FontHelper.CreateFont10();
        }

        public void InitTheme()
        {
            this.labelMessage.ForeColor = SkinManager.CurrentSkin.TextForeColor;
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

        private const int DefaultLabelHeigth = 64;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if(this.labelMessage.PreferredHeight > DefaultLabelHeigth)
            {
                this.Height += this.labelMessage.PreferredHeight - DefaultLabelHeigth;
            }
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

        private void UpdateDisplayLanguage()
        {
            this.buttonOK.Text = localization[Localization.ok];
            this.buttonCancel.Text = localization[Localization.cancel];
        }

        //public static DialogResult ShowMessage(string text, string caption = "AEC", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information)
        //{
        //    gtMessageBox messageBox = new gtMessageBox(buttons, icon);
        //    messageBox.Text = caption;
        //    messageBox.labelMessage.Text = text;

        //    return messageBox.ShowDialog();
        //}

        public static DialogResult ShowMessage(Form owner, string text, string caption = "AEC", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            gtMessageBox messageBox = new gtMessageBox(buttons, icon);
            messageBox.Owner = owner;
            messageBox.Text = caption;
            messageBox.labelMessage.Text = text;

            return messageBox.ShowDialog();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.SelectOKHandler();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.SelectCancelHandler();
        }

        private void SelectOKHandler()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void SelectCancelHandler()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void gtMessageBox_Load(object sender, EventArgs e)
        {
            if(this.buttonCancel.Visible)
            {
                this.buttonCancel.Focus();
                this.ActiveControl = this.buttonCancel;
            }
            else if(this.buttonOK.Visible)
            {
                this.buttonOK.Focus();
                this.ActiveControl = this.buttonOK;
            }
        }

        private void buttonOK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                this.SelectOKHandler();
            }
        }

        private void buttonCancel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                this.SelectCancelHandler();
            }
        }
    }
}
