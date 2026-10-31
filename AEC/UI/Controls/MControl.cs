using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using AEC.UI.Utility;

namespace AEC.UI.Controls
{
    public partial class MControl : Control
    {
        protected Color _borderColor = Color.FromArgb(128, 128, 128);
        protected Color _fillColor = Color.FromArgb(48, 48, 48);
        protected int _radius = 12;
        protected float _borderWidth = 1;

        protected bool _showBorder = false;

        protected RoundStyle _roundStyle = RoundStyle.All;
        public RoundStyle RoundStyle
        {
            get
            {
                return _roundStyle;
            }
            set
            {
                if (_roundStyle != value)
                {
                    _roundStyle = value;
                    Invalidate();
                }
            }
        }

        public virtual bool ShowBorder
        {
            get
            {
                return _showBorder;
            }
            set
            {
                if (_showBorder != value)
                {
                    _showBorder = value;
                    Invalidate();
                }
            }
        }

        public virtual Color BorderColor
        {
            get
            {
                return this._borderColor;
            }
            set
            {
                this._borderColor = value;
            }
        }

        public virtual Color fillColor
        {
            get
            {
                return this._fillColor;
            }
            set
            {
                this._fillColor = value;
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual int Radius
        {
            get
            {
                return this._radius;
            }
            set
            {
                this._radius = value;
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public float BorderWidth
        {
            get
            {
                return this._borderWidth;
            }
            set
            {
                this._borderWidth = value;
            }
        }

        public MControl()
        {
            InitializeComponent();

            this.Init();
        }

        private void Init()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.StandardClick, true);
            this.SetStyle(ControlStyles.StandardDoubleClick, true);
            this.SetStyle(ControlStyles.UserMouse, true);
            this.SetStyle(ControlStyles.CacheText, true);
            this.SetStyle(ControlStyles.Selectable, true);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            this.PaintBackground(pe.Graphics);
            this.PaintForeground(pe.Graphics);
        }

        protected virtual void PaintBackground(Graphics g)
        {
            if(Visible)
            {
                GraphicsPath roundedRect = RoundRectangle.CreateRoundedRectPath(this, this._radius, this._roundStyle);

                GDIHelper.FillPath(g, this._fillColor, roundedRect);

                if (_showBorder)
                {
                    GDIHelper.DrawPath(g, this._borderColor, roundedRect, _borderWidth);
                }

                roundedRect.Dispose();
            }
        }

        //protected override void OnGotFocus(EventArgs e)
        //{
        //    base.OnGotFocus(e);
        //    Log.OutputLine("btn GotFocus focus");
        //}

        //protected override void OnLostFocus(EventArgs e)
        //{
        //    base.OnLostFocus(e);
        //    Log.OutputLine("btn LostFocus focus");
        //}

        protected virtual void PaintForeground(Graphics g)
        {
        }

        public void UpdateUI()
        {
            this.Invalidate();
            this.Update();
        }

        public void UpdateText(string text, bool isUpdateUI)
        {
            this.Text = text;
            if(isUpdateUI)
            {
                this.UpdateUI();
            }
        }
    }
}
