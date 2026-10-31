using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AEC.UI
{
    public partial class Title : UserControl
    {
        private Form form = null;
        private bool _isShowSystemButton = true;
        private bool _isShowCloseButton = true;
        public bool _isAllowDoubleClickMaximize = false;

        public Title()
        {
            InitializeComponent();
            InitFont();
        }

        private void InitFont()
        {
            this.Font = FontHelper.CreateFont10();
        }

        public bool isShowSystemBtns
        {
            get
            {
                return this._isShowSystemButton;
            }
            set
            {
                this._isShowSystemButton = value;
                this.btnMenu.Visible = this._isShowSystemButton;
                this.btnMin.Visible = this._isShowSystemButton;
                this.btnMax.Visible = this._isShowSystemButton;
                this.btnClose.Visible = this._isShowCloseButton | this._isShowSystemButton;
            }
        }

        public bool isShowCloseButton
        {
            get
            {
                return this._isShowCloseButton;
            }
            set
            {
                this._isShowCloseButton = value;
                if (this._isShowCloseButton)
                {
                    this.btnClose.Visible = true;
                }
                else
                {
                    this.btnClose.Visible = this._isShowSystemButton;
                }
            }
        }
        
        public bool isAllowDoubleClickMaximize
        {
            get
            {
                return _isAllowDoubleClickMaximize;
            }
            set
            {
                this._isAllowDoubleClickMaximize = value;
            }
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    this.PaintBackground(e.Graphics);
        //}

        //private void PaintBackground(Graphics g)
        //{
        //    Point bottomLeft = new Point(2, this.Height - 2);
        //    Point bottomRight = new Point(this.Width - 2, this.Height - 2);
        //    GDIHelper.InitializeGraphics(g);
        //    using(Pen whiteLine = new Pen(Color.FromArgb(196, 196, 196), 1.5f))
        //    {
        //        whiteLine.DashCap = System.Drawing.Drawing2D.DashCap.Round;
        //        whiteLine.StartCap = System.Drawing.Drawing2D.LineCap.Round;
        //        whiteLine.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        //        g.DrawLine(whiteLine, bottomLeft, bottomRight);
        //    }

        //    //if(this.Text.Length > 0)
        //    //{
        //    //    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        //    //    PointF centerLeft = new PointF(2, this.Height / 2.0f);
        //    //    StringFormat sf = new StringFormat();
        //    //    sf.Alignment = StringAlignment.Near;
        //    //    sf.LineAlignment = StringAlignment.Center;
        //    //    using (Brush brush = new SolidBrush(Color.FromArgb(0xff, 0xff, 0xff)))
        //    //    {
        //    //        g.DrawString(this.Text, this.Font, brush, centerLeft, sf);
        //    //    }
        //    //}
        //}

        [Browsable(true)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                this.Invalidate();
                this.Update();
            }
        }

        public void BindingForm(Form form_)
        {
            this.form = form_;
        }

        private void Title_Load(object sender, EventArgs e)
        {
            this.MouseDown += Title_MouseDown;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.PaintTitle(e.Graphics);
        }

        private void PaintTitle(Graphics g)
        {
            Rectangle rect = new Rectangle(new Point(6, 0), new Size(this.Width, this.Height));
            TextRenderer.DrawText(g, Text, this.Font, rect, this.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }

        void Title_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.form != null)
            {
                Win32.ReleaseCapture();
                Win32API.PostMessage(this.form.Handle, Win32.WM_SYSCOMMAND, Win32.SC_MOVE + Win32.HTCAPTION, 0);
            }

            if (this._isAllowDoubleClickMaximize && e.Clicks == 2)
            {
                Maximize();
            }
        }

        private void Minimize()
        {
            if (this.form == null)
            {
                return;
            }

            this.form.WindowState = FormWindowState.Minimized;
        }

        private void Maximize()
        {
            if (this.form == null)
            {
                return;
            }

            if (this.form.WindowState == FormWindowState.Normal)
            {
                //Console.WriteLine(Screen.FromControl(this).DeviceName);
                Screen CurrentScreen = Screen.FromControl(this);
                this.form.MaximumSize = CurrentScreen.WorkingArea.Size;

                this.form.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.form.WindowState = FormWindowState.Normal;
            }
        }

        private void Close()
        {
            if (this.form == null)
            {
                return;
            }

            this.form.Close();
        }

        //[DllImport("user32.dll")]
        //public static extern bool ReleaseCapture();
        //[DllImport("user32.dll")]
        //public static extern int PostMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        //public const int WM_SYSCOMMAND = 0x0112;
        //public const int SC_MOVE = 0xF010;
        //public const int HTCAPTION = 0x0002;

        private void btn_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.btnClose))
            {
                Close();
            }
            else if (sender.Equals(this.btnMax))
            {
                Maximize();
            }
            else if (sender.Equals(this.btnMin))
            {
                Minimize();
            }
            else if(sender.Equals(this.btnMenu))
            {

            }
        }
    }
}
