using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using AEC.UI.Utility;

namespace AEC.UI.Controls
{
    [ToolboxItem(true), DefaultEvent("TextChanged")]
    public partial class MEditableLabel : UserControl
    {
        private Color _borderColor = Color.FromArgb(55, 117, 207);
        private Color _fillColor = Color.FromArgb(48, 48, 48);
        private int _radius = 8;
        private string text = string.Empty;
        private bool cancelEditing = true;
        private HorizontalAlignment _textAlign = HorizontalAlignment.Left;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new event EventHandler TextChanged;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (this.text != value)
                {
                    this.text = value;
                    if(TextChanged != null)
                    {
                        this.TextChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        private int maxLength = 32;
        public int MaxLength
        {
            get
            {
                return this.maxLength;
            }
            set
            {
                if(this.maxLength != value)
                {
                    this.maxLength = value;
                    this.textBox.MaxLength = this.maxLength;
                    if(this.text.Length > this.maxLength)
                    {
                        this.text = this.text.Substring(0, this.maxLength);
                        Invalidate();
                    }
                }
            }
        }

        private bool _canEditable = false;
        public bool canEditable
        {
            get
            {
                return this._canEditable;
            }
            set
            {
                if (this._canEditable != value)
                {
                    this._canEditable = value;
                    if (!this._canEditable && this.textBox.Visible)
                    {
                        this.textBox.Visible = false;
                    }

                    Invalidate();
                }
            }
        }


        protected bool _showBorder = true;
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

        public new Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                this.textBox.Font = value;
            }
        }

        public Color borderColor
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

        public Color fillColor
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
        private int radius
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

        public HorizontalAlignment TextAlign
        {
            get
            {
                return this._textAlign;
            }
            set
            {
                this._textAlign = value;
                this.textBox.TextAlign = value;
            }
        }

        public MEditableLabel()
        {
            InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, false);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.CacheText, true);
            this.SetStyle(ControlStyles.StandardClick, true);
            this.SetStyle(ControlStyles.UserMouse, true);
            this.SetStyle(ControlStyles.StandardDoubleClick, true);
            this.DoubleBuffered = true;
            //this.UpdateStyles();

            this.textBox.LostFocus += textBox_LostFocus;
        }

        private void textBox_LostFocus(object sender, EventArgs e)
        {
            this.textBox.Visible = false;
            if (!this.cancelEditing)
            {
                this.Text = this.textBox.Text;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.textBox.Location = new Point(0, (this.Height - this.textBox.Height) / 2);
            this.textBox.Width = this.Width;
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            if(e.Button == System.Windows.Forms.MouseButtons.Left && this._canEditable)
            {
                this.textBox.Text = this.Text;
                //this.textBox.SelectionStart = this.textBox.TextLength;
                this.cancelEditing = true;
                this.textBox.Visible = true;
                this.textBox.Focus();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PaintLabel(e.Graphics);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if(!_canEditable)
            {
                return base.ProcessDialogKey(keyData);
            }

            if (this.textBox.Visible)
            {
                if (keyData == Keys.Escape)
                {
                    this.textBox.Visible = false;
                    this.Focus();
                }
                else if (keyData == Keys.Enter)
                {
                    this.textBox.Visible = false;
                    this.cancelEditing = false;
                    this.Focus();
                }

                return false;
            }
            else
            {
                return base.ProcessDialogKey(keyData);
            }
        }

        private TextFormatFlags horizontalAlignment
        {
            get
            {
                switch(this._textAlign)
                {
                    case HorizontalAlignment.Left:
                        return TextFormatFlags.Left;

                    case HorizontalAlignment.Center:
                        return TextFormatFlags.HorizontalCenter;

                    case HorizontalAlignment.Right:
                        return TextFormatFlags.Right;

                    default:
                        return TextFormatFlags.HorizontalCenter;
                }
            }
        }

        private void PaintLabel(Graphics g)
        {
            if (this.Visible)
            {
                //Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
                GraphicsPath roundedRect = RoundRectangle.CreateRoundedRectPath(this, this._radius);

                //GDIHelper.SetHighQuality(g);
                GDIHelper.FillPath(g, this._fillColor, roundedRect);

                if (this._showBorder && this._canEditable)
                {
                    GDIHelper.DrawPath(g, this._borderColor, roundedRect);
                }

                TextRenderer.DrawText(g, this.Text, this.Font, this.ClientRectangle, this.ForeColor, horizontalAlignment | TextFormatFlags.VerticalCenter);

                roundedRect.Dispose();
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                this.Focus();
                return;
            }
        }

        private const int WM_KEYDOWN = 0x0100;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == WM_KEYDOWN)
            {
                if (keyData == Keys.Escape && this.textBox.Visible)
                {
                    return false;
                }
            }

            //Log.OutputLine("ProcessCmdKey key data:{0}", keyData);

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void UpdateDisplay()
        {
            this.Invalidate(this.ClientRectangle);
            this.Update();
        }
    }
}
