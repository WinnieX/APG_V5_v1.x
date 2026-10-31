using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using AEC.UI.Utility;

namespace AEC.UI.Controls
{
    public partial class MContainerControl : UserControl
    {
        //protected static readonly mControl.Misc.Localization localization = mControl.Controllers.ControllerConfig.GetInstance().localization;

        private readonly List<DUIControlBase> duiControls = new List<DUIControlBase>();

        protected Color _borderColor = Color.FromArgb(47, 100, 189);
        protected Color _fillColor = Color.FromArgb(48, 48, 48);        
        protected int _radius = 16;
        protected float _borderWidth = 1;

        protected string _text = string.Empty;
        protected bool _showBorder = true;
        protected bool _showText = true;

        private ContentAlignment _textAlignment = ContentAlignment.TopCenter;

        public bool ShowText
        {
            get
            {
                return _showText;
            }
            set
            {
                if (_showText != value)
                {
                    _showText = value;
                    Invalidate();
                }
            }
        }

        public bool ShowBorder
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

        public Color BorderColor
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

        public virtual Color FillColor
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
        public int Radius
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

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (this._text != value)
                {
                    this._text = value;
                }
            }
        }

        [Localizable(true)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Padding TextPadding { get; set; }

        public ContentAlignment TextAlignment
        {
            get
            {
                return this._textAlignment;
            }
            set
            {
                this._textAlignment = value;
            }
        }

        public MContainerControl()
        {
            InitializeComponent();
            this.Init();
        }

        public void UpdateUI()
        {
            this.Invalidate();
            this.Update();
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

            this.DoubleBuffered = true;

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            //this.UpdateStyles();
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.FromArgb(224, 224, 224);
            this.TextPadding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.ShowBorder = true;
        }

        protected void UpdateRectangle(Rectangle rect)
        {
            this.Invalidate(rect);
            this.Update();
        }

        protected virtual void OnInitDuiControls()
        {

        }

        public void AddDuiControl(DUIControlBase control)
        {
            if(!duiControls.Contains(control))
            {
                duiControls.Add(control);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                this.OnInitDuiControls();
                this.OnLanguageChanged(false);
                this.OnLoadHandler();
            }
        }

        protected virtual void OnLoadHandler()
        {

        }

        public virtual void OnLanguageChanged(bool isUpdateUI = true)
        {
        }

        protected void UpdateDUIControlText(DUIControlBase control, string text)
        {
            Debug.Assert(control != null);

            control.Text = text;
            this.Invalidate(control.ClientRectangle);
        }

        //protected override void OnMouseClick(MouseEventArgs e)
        //{
        //    base.OnMouseClick(e);
        //    if(this.CanFocus && this.CanSelect)
        //    {
        //        this.Focus();
        //    }
        //    mControl.Log.OutputLine("can focus:{0}", this.Focused);
        //}

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            this.PaintControl(e.Graphics);
        }

        protected virtual void PaintControl(Graphics g)
        {
            PaintBackground(g);
        }

        protected virtual void PaintBackground(Graphics g)
        {
            if (this.Visible)
            {
                //this.UpdateRegion();

                GraphicsPath roundedRect = RoundRectangle.CreateRoundedRectPath(this, this._radius);

                GDIHelper.FillPath(g, this._fillColor, roundedRect);

                if (_showBorder)
                {
                    GDIHelper.DrawPath(g, this._borderColor, roundedRect, _borderWidth);
                }

                if(_showText && !string.IsNullOrEmpty(this.Text))
                {
                    Rectangle textRect = new Rectangle(new Point(this.TextPadding.Left, this.TextPadding.Top), new Size(this.Width - this.TextPadding.Left - this.TextPadding.Right, this.Height - this.TextPadding.Top - this.TextPadding.Bottom));
                    TextRenderHelper.RenderText(g, this.Text, this.Font, textRect, this.ForeColor, this._textAlignment);
                    //TextRenderer.DrawText(g, this.Text, this.Font, textRect, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.Top);
                }

                roundedRect.Dispose();

                if(this.duiControls != null)
                {
                    foreach(DUIControlBase duiControl in this.duiControls)
                    {
                        if(duiControl.Visible)
                        {
                            duiControl.OnPaint(g);
                        }
                    }
                }
            }
        }

        protected void UpdateRegion()
        {
            if (this._radius > 0)
            {
                GraphicsPath path = new GraphicsPath();
                Rectangle rect = new Rectangle(-1, -1, base.Width + 1, base.Height + 1);
                path = RoundRectangle.CreateRoundedRectPath(rect.Size, this._radius);
                base.Region = new Region(path);
            }
            else
            {
                GraphicsPath graphicsPath = new GraphicsPath();
                graphicsPath.AddRectangle(base.ClientRectangle);
                graphicsPath.CloseFigure();
                base.Region = new Region(graphicsPath);
            }
        }

        public void UpdateText(string text, bool isUpdateUI)
        {
            this.Text = text;
            if (isUpdateUI)
            {
                Rectangle textRect = new Rectangle(new Point(this.TextPadding.Left, this.TextPadding.Top), new Size(this.Width - this.TextPadding.Left - this.TextPadding.Right, this.Height - this.TextPadding.Top - this.TextPadding.Bottom));
                this.Invalidate(textRect);
            }
        }
    }
}
