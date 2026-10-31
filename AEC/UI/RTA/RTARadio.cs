using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace AEC.UI.RTA
{
    public partial class RTARadio : Control
    {
        public enum BtnStatus
        {
            Normal = 0,
            MouseOver = 1,
            Pressed = 2,
            Disbaled = 3,
        }

        private static Color borderColor = Color.FromArgb(65, 157, 212);
        private static Color disabledColor = Color.FromArgb(204, 204, 204);
        private static Color normalColor = Color.FromArgb(225, 225, 225);
        private static Color overColor = Color.FromArgb(229, 241, 251);
        private static Color pressedColor = Color.FromArgb(204, 228, 247);
        private static Brush disabledBrush = new SolidBrush(disabledColor);
        private static Brush normalBrush = new SolidBrush(normalColor);
        private static Brush blackBrush = new SolidBrush(Color.Black);
        private static Pen borderPen = new Pen(borderColor);
        private static StringFormat sf = new StringFormat();
        private static Brush overBrush = new SolidBrush(Color.FromArgb(32, 255, 255, 255));
        private static Brush pressedBrush = new SolidBrush(Color.FromArgb(32, 64, 64, 64));

        private BtnStatus status = BtnStatus.Normal;
        private bool _isChecked;
        private bool _isSignal;

        private int _CornerRadius = 5;

        public RTARadio()
        {
            InitializeComponent();
            this.Init();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            PaintRdo(pe.Graphics);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.Cursor = Cursors.Hand;
            status = BtnStatus.MouseOver;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Cursor = Cursors.Arrow;
            status = BtnStatus.Normal;
            this.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.ClientRectangle.Contains(e.Location))
            {
                this.Cursor = Cursors.Hand;
                if (status != BtnStatus.Pressed)
                {
                    status = BtnStatus.MouseOver;
                }
            }
            else
            {
                this.Cursor = Cursors.Arrow;
                status = BtnStatus.Normal;
            }
            this.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            status = BtnStatus.Pressed;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (this.ClientRectangle.Contains(e.Location))
            {
                this.Cursor = Cursors.Hand;
                status = BtnStatus.MouseOver;
            }
            else
            {
                this.Cursor = Cursors.Arrow;
                status = BtnStatus.Normal;
            }
            this.Invalidate();
            this.Invalidate();
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
            this.BackColor = Color.Transparent;

            this.Size = new Size(24, 24);
        }

        public bool isChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                if (value != _isChecked)
                {
                    _isChecked = value;
                    this.Invalidate();
                    this.Update();
                }
            }
        }

        public bool isSignal
        {
            get
            {
                return _isSignal;
            }
            set
            {
                if (value != _isSignal)
                {
                    _isSignal = value;
                    this.Invalidate();
                    this.Update();
                }
            }
        }

        public Image nosignal_checked
        {
            get;
            set;
        }

        public Image nosignal_unchecked
        {
            get;
            set;
        }

        public Image signal_checked
        {
            get;
            set;
        }

        public Image signal_unchecked
        {
            get;
            set;
        }

        private void PaintRdo(Graphics g)
        {
            Rectangle client = this.ClientRectangle;
            client.Width -= 1;
            client.Height -= 1;

            if (this._isSignal)
            {
                if (this._isChecked)
                {
                    if (signal_checked != null)
                    {
                        g.DrawImage(this.signal_checked, client);
                    }
                    else
                    {
                        g.FillRectangle(normalBrush, client);
                        g.DrawRectangle(borderPen, client);
                    }
                }
                else
                {
                    if (signal_unchecked != null)
                    {
                        g.DrawImage(this.signal_unchecked, client);
                    }
                    else
                    {
                        g.FillRectangle(normalBrush, client);
                        g.DrawRectangle(borderPen, client);
                    }
                }
            }
            else
            {
                if (this._isChecked)
                {
                    if (nosignal_checked != null)
                    {
                        g.DrawImage(this.nosignal_checked, client);
                    }
                    else
                    {
                        g.FillRectangle(normalBrush, client);
                        g.DrawRectangle(borderPen, client);
                    }
                }
                else
                {
                    if (nosignal_unchecked != null)
                    {
                        g.DrawImage(this.nosignal_unchecked, client);
                    }
                    else
                    {
                        g.FillRectangle(normalBrush, client);
                        g.DrawRectangle(borderPen, client);
                    }
                }
            }

            if (status == BtnStatus.MouseOver)
            {
                FillRoundedRect(g, client, overBrush);
                //g.FillRectangle(overBrush, this.ClientRectangle);
            }
            else if (status == BtnStatus.Pressed)
            {
                FillRoundedRect(g, client, pressedBrush);
                //g.FillRectangle(pressedBrush, this.ClientRectangle);
            }
        }

        private void FillRoundedRect(Graphics g, Rectangle rect, Brush brush)
        {
            AEC.UI.Utility.RoundRectangle roundRect = new AEC.UI.Utility.RoundRectangle(rect, _CornerRadius);
            using (GraphicsPath path = roundRect.ToGraphicsBezierPath())
            {
                g.FillPath(brush, path);
            }
        }

        public int CornerRadius
        {
            get
            {
                return _CornerRadius;
            }
            set
            {
                _CornerRadius = value;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (!this.Checked)
            {
                this.Checked = true;
            }
            else
            {
                if (Actived != null)
                {
                    Actived(this, EventArgs.Empty);
                }
            }
        }

        private void UnCheckOtherRdoBox()
        {
            Control parent = this.Parent;
            if (parent != null && parent.HasChildren)
            {
                foreach (Control control in parent.Controls)
                {
                    if (control != this && control is RTARadio)
                    {
                        RTARadio rdo = control as RTARadio;
                        rdo._isChecked = false;
                        rdo.Invalidate();
                        rdo.Update();
                    }
                }
            }
        }

        public void Check()
        {
            if (this._isChecked)
            {
                return;
            }

            UnCheckOtherRdoBox();
            this._isChecked = true;
            this.Invalidate();
            this.Update();
        }

        public void Uncheck()
        {
            if (!this._isChecked)
            {
                return;
            }
            this._isChecked = false;
            this.Invalidate();
            this.Update();
        }

        public void UpdateCheck(bool check)
        {
            if (check != this._isChecked)
            {
                if (check)
                {
                    UnCheckOtherRdoBox();
                }
                this._isChecked = check;
                this.Invalidate();
                this.Update();
            }
        }

        public bool Checked
        {
            get
            {
                return this._isChecked;
            }
            set
            {
                if (value)
                {
                    UnCheckOtherRdoBox();
                }

                bool temp = this._isChecked;
                this._isChecked = value;
                this.Invalidate();
                this.Update();
                if (temp != this._isChecked && CheckedChanged != null)
                {
                    CheckedChanged(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler<EventArgs> CheckedChanged = null;
        public EventHandler Actived = null;
    }
}
