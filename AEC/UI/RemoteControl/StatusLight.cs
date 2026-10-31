using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.UI.Utility;

namespace AEC.UI.RemoteControl
{
    public partial class StatusLight : Control
    {
        public enum Status
        {
            Unconnected = 0,
            Connecting,
            Connected,
        }

        private static readonly Color greenColor = Color.FromArgb(83, 209, 21);
        private static readonly Color grayColor = Color.FromArgb(153, 153, 153);
        //private static readonly Color grayColor = Color.FromArgb(255, 0, 0);

        private Status _status = Status.Unconnected;
        private int tickCount = 0;

        private Timer timer = new Timer();

        //private int radius = 8;

        public Status status
        {
            get
            {
                return this._status;
            }
            set
            {
                if(this._status != value)
                {
                    this._status = value;

                    if(this._status == Status.Unconnected)
                    {
                        this.ForeColor = grayColor;
                        this.timer.Stop();
                        this.Invalidate();
                    }
                    else if(this._status == Status.Connected)
                    {
                        this.ForeColor = greenColor;
                        this.timer.Stop();
                        this.Invalidate();
                    }
                    else
                    {
                        this.timer.Start();
                    }
                }
            }
        }

        public StatusLight()
        {
            InitializeComponent();

            this.Init();

            this.Size = new Size(24, 24);
            this.timer = new Timer();
            this.timer.Interval = 500;
            this.timer.Tick += timer_Tick;
        }

        private void Init()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.SetStyle(ControlStyles.StandardDoubleClick, false);

            this.BackColor = Color.Transparent;
            this.ForeColor = grayColor;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.ForeColor = ++tickCount % 2 == 0 ? grayColor : greenColor;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            //base.OnPaint(pe);
            GDIHelper.InitializeGraphics(pe.Graphics);
            using(Brush foreBrush = new SolidBrush(this.ForeColor))
            {
                Rectangle rect = new Rectangle(Point.Empty, new Size(this.Width - 1, this.Height - 1));
                pe.Graphics.FillEllipse(foreBrush, rect);
            }
        }
    }
}
