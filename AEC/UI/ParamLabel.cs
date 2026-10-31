using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Forms.Layout;
using AEC.Core;
using AEC.Common;
using AEC.UI.Utility;

namespace AEC.UI
{
    public delegate string GetLabelDelegate(short index);
    //public delegate string GetEditTextDelegate(short index);
    public delegate short CalcIndexFromEditTextDelegate(string text);

    public partial class ParamLabel : UserControl
    {
        private const int btnWidth = 13;
        private const int btnHeight = 9;
        private static readonly Size btnSize = new Size(btnWidth, btnHeight);

        private short curr = 0;
        private short max = 0;
        private short _upperLimit = 0;
        private GetLabelDelegate getLabel = null;
        private CalcIndexFromEditTextDelegate calcIndexFromEditText = null;

        public event EventHandler<EventArgs> ValueChanged;

        private bool cancelEditing = true;

        private bool _isShowUpdownButton = true;

        protected Rectangle labelRect = new Rectangle();
        protected Image btnUpBackImage = null;
        //private Point btnUpLocation = new Point();
        protected Image btnDownBackImage = null;
        //private Point btnDownLocation = new Point();
        private Rectangle btnUpDestRect = new Rectangle();
        private Rectangle btnDownDestRect = new Rectangle();
        private bool _btnUpIsEnabled = true;
        private bool _btnDownIsEnabled = true;

        public bool canBeChanged = true;

        private Color disabledForeColor = Color.FromArgb(128, 128, 128);

        public Image normalBack { get; set; }
        public Image disabledBack { get; set; }

        private Size _upDownSize = new Size(13, 9);

        protected Image btnUpNormal = AEC.Properties.Resources.up_normal;
        protected Image btnUpHover = AEC.Properties.Resources.up_hover;
        protected Image btnUpPressed = AEC.Properties.Resources.up_pressed;
        protected Image btnUpDisabled = AEC.Properties.Resources.up_disabled;

        protected Image btnDownNormal = AEC.Properties.Resources.down_normal;
        protected Image btnDownHover = AEC.Properties.Resources.down_hover;
        protected Image btnDownPressed = AEC.Properties.Resources.down_pressed;
        protected Image btnDownDisabled = AEC.Properties.Resources.down_disabled;

        protected bool _userGDIPaint = true;
        public bool userGDIPaint
        {
            get
            {
                return _userGDIPaint;
            }
            set
            {
                _userGDIPaint = value;
            }
        }
        
        public short v
        {
            get
            {
                return this.curr;
            }
            set
            {
                value = Utils.ConstrainMax(value, this._upperLimit);
                if (value != this.curr)
                {
                    this.curr = value;
                    this.btnUpIsEnabled = this.curr < this._upperLimit;
                    this.btnDownIsEnabled = this.curr > 0;
                    this.UpdateLabel();
                    this.UpdateDisplay();
                    this.OnValueChanged();
                }
            }
        }

        public short initV
        {
            get
            {
                return this.curr;
            }
            set
            {
                value = Utils.ConstrainMax(value, this._upperLimit);
                this.curr = value;
                this.btnUpIsEnabled = this.curr < this._upperLimit;
                this.btnDownIsEnabled = this.curr > 0;
                this.UpdateLabel();
                this.UpdateDisplay();
                //if(value != this.curr)
                //{
                //    this.curr = value;
                //    this.btnUpIsEnabled = this.curr < this.max;
                //    this.btnDownIsEnabled = this.curr > 0;
                //    this.UpdateLabel();
                //    this.UpdateDisplay();
                //    //this.UpdateTextBox();
                //    //this.Invalidate();
                //    //this.Update();
                //}
            }
        }

        public short upperLimit
        {
            get
            {
                return this._upperLimit;
            }
            set
            {
                value = Utils.ConstrainMax(value, this.max);
                this._upperLimit = value;
                btnUpIsEnabled = this.curr < this._upperLimit;
                btnDownIsEnabled = this.curr > 0;
                if(this._upperLimit < this.curr)
                {
                    this.curr = this._upperLimit;
                    this.UpdateLabel();                    
                }
                this.Invalidate();
                this.Update();
            }
        }

        public bool isShowUpdownButton
        {
            get
            {
                return this._isShowUpdownButton;
            }
            set
            {
                this._isShowUpdownButton = value;
                this.UpdateControl();
            }
        }

        private bool btnUpIsEnabled
        {
            get
            {
                return _btnUpIsEnabled;
            }
            set
            {
                if (_btnUpIsEnabled != value)
                {
                    _btnUpIsEnabled = value;
                    if (value)
                    {
                        this.btnUpBackImage = btnUpNormal;
                    }
                    else
                    {
                        this.btnUpBackImage = btnUpDisabled;
                    }
                }
                
            }
        }
        private bool btnDownIsEnabled
        {
            get
            {
                return _btnDownIsEnabled;
            }
            set
            {
                if (_btnDownIsEnabled != value)
                {
                    _btnDownIsEnabled = value;
                    if (value)
                    {
                        this.btnDownBackImage = btnDownNormal;
                    }
                    else
                    {
                        this.btnDownBackImage = btnDownDisabled;
                    }
                }
            }
        }

        private Size upDownSize
        {
            get
            {
                return _upDownSize;
            }
            set
            {
                _upDownSize = value;
            }
        }

        public ParamLabel()
        {
            InitializeComponent();
            this.Init();
        }

        private void Init()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            //Console.WriteLine("ResizeRedraw:{0}", this.ResizeRedraw);

            btnUpBackImage = btnUpNormal;
            btnDownBackImage = btnDownNormal;

            this.labelRect =  this.ClientRectangle;
            this.textBox.Width = this.Width;
            this.textBox.LostFocus += textBox_LostFocus;
            this.MouseWheel += ParamLabel_MouseWheel;
            this.SizeChanged += ParamLabel_SizeChanged;

            this.EnabledChanged += ParamLabel_EnabledChanged;
            this.UpdateControl();
        }

        private void InitFont()
        {
            this.Font = FontHelper.CreateLabelFont10();
            this.textBox.Font = FontHelper.CreateLabelFont10();
        }

        private void ParamLabel_EnabledChanged(object sender, EventArgs e)
        {
            if(this.Enabled)
            {
                btnUpIsEnabled = this.curr < this._upperLimit;
                btnDownIsEnabled = this.curr > 0;
            }
            else
            {
                btnUpIsEnabled = false;
                btnDownIsEnabled = false;
            }

            this.UpdateDisplay();
        }

        private void ParamLabel_SizeChanged(object sender, EventArgs e)
        {
            this.UpdateControl();
        }

        private void UpdateControl()
        {
            if(_isShowUpdownButton)
            {
                labelRect = new Rectangle(new Point(this.Padding.Left, 0), new Size(this.Width - this.Padding.Horizontal - btnWidth, this.Height));
            }
            else
            {
                labelRect = new Rectangle(new Point(this.Padding.Left, 0), new Size(this.Width - this.Padding.Horizontal, this.Height));
            }
            Point btnUpLocation = new Point(this.Width - this.Padding.Right - btnWidth,  (this.Height / 2 - upDownSize.Height - 1));
            Point btnDownLocation = new Point(this.Width - this.Padding.Right - btnWidth, this.Height / 2 + 2);
            this.btnUpDestRect = new Rectangle(btnUpLocation, btnSize);
            this.btnDownDestRect = new Rectangle(btnDownLocation, btnSize);

            this.textBox.Location = new Point(this.Padding.Left, 0);
            this.textBox.Width = this.Width - this.Padding.Horizontal;

            btnUpIsEnabled = this.curr < this._upperLimit;
            btnDownIsEnabled = this.curr > 0;
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

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox_LostFocus(object sender, EventArgs e)
        {
            this.textBox.Visible = false;
            if (calcIndexFromEditText != null && !this.cancelEditing)
            {
                short index = calcIndexFromEditText(this.textBox.Text);
                index = Utils.ConstrainMax(index, this._upperLimit);
                if(index != this.v)
                {
                    this.v = index;
                }
            }
        }

        private void ParamLabel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (getLabel == null || !canBeChanged)
            {
                return;
            }

            HandledMouseEventArgs h = e as HandledMouseEventArgs;
            if (h != null)
            {
                h.Handled = true;
            }

            if(e.Delta > 0)
            {
                Increase();
            }else
            {
                Decrease();
            }
        }

        public void UpdateLabel()
        {
            if (getLabel == null)
            {
                return;
            }

            this.Text = getLabel(curr);
            if (this.textBox.Visible)
            {
                this.textBox.Text = getLabel(this.curr);
            }
        }

        public virtual void UpdateLabelDisplay()
        {
            this.UpdateLabel();
            this.UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            if(_userGDIPaint)
            {
                Graphics graphics = this.CreateGraphics();
                BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
                BufferedGraphics buffer = currentContext.Allocate(graphics, this.ClientRectangle);
                Graphics g = buffer.Graphics;

                using (Brush backBrush = new SolidBrush(this.BackColor))
                {
                    g.FillRectangle(backBrush, this.ClientRectangle);
                }

                this.PaintBackground(g);
                //this.RenderGraphic(g);

                buffer.Render(graphics);

                g.Dispose();
                buffer.Dispose();
                currentContext.Dispose();
                graphics.Dispose();
            }
            else
            {
                this.Invalidate();
                this.Update();
            }
        }

        public void Init(short max_, short curr_, GetLabelDelegate getLabel_, CalcIndexFromEditTextDelegate calcIndexFromEditText_ = null)
        {
            Debug.Assert(
                max_ >= 0 &&
                curr_ >= 0 &&
                curr_ <= max_ &&
                getLabel_ != null);

            this.curr = curr_;
            this.max = max_;
            this._upperLimit = this.max;
            this.getLabel = getLabel_;
            //this.getEditText = getEditText_;
            this.calcIndexFromEditText = calcIndexFromEditText_;
            this.UpdateLabel();
            this.btnUpIsEnabled = this.curr < this._upperLimit;
            this.btnDownIsEnabled = this.curr > 0;
            this.UpdateDisplay();
        }

        public void ReInit(GetLabelDelegate getLabel_, CalcIndexFromEditTextDelegate calcIndexFromEditText_ = null)
        {
            this.getLabel = getLabel_;
            this.calcIndexFromEditText = calcIndexFromEditText_;
        }

        public void Reset()
        {
            this.curr = 0;
            this.max = 0;
            this._upperLimit = 0;
            this.btnUpIsEnabled = false;
            this.btnDownIsEnabled = false;
            this.Text = string.Empty;
            if (this.textBox.Visible)
            {
                this.textBox.Text = string.Empty;
            }
            this.UpdateDisplay();
        }

        private void Increase()
        {
            if (this.curr < this._upperLimit)
            {
                this.curr++;
                this.btnUpIsEnabled = this.curr < this._upperLimit;
                this.btnDownIsEnabled = this.curr > 0;
                this.UpdateLabel();
                this.UpdateDisplay();
                this.OnValueChanged();
            }
        }

        private void Decrease()
        {
            if (this.curr > 0)
            {
                this.curr--;
                this.btnUpIsEnabled = this.curr < this._upperLimit;
                this.btnDownIsEnabled = this.curr > 0;
                this.UpdateLabel();
                this.UpdateDisplay();
                this.OnValueChanged();
            }
        }

        private void OnValueChanged()
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, EventArgs.Empty);
            }
        }

        private void ParamLabel_MouseDown(object sender, MouseEventArgs e)
        {
            //this.MouseDown(this, e);
            this.OnMouseDown(e);
        }

        #region 重写基类方法
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            //base.OnMouseDoubleClick(e);
            if(e.Button != System.Windows.Forms.MouseButtons.Left)
            {
                return;
            }
            this.OnMouseDoubleClick(e.Location);
            if(this.labelRect.Contains(e.Location))
            {
                base.OnMouseDoubleClick(e);
            }
        }

        //protected override void OnClick(EventArgs e)
        //{
        //    base.OnClick(e);
        //    AEC.Log.OutputLine("OnClick");
        //}

        protected override void OnMouseDown(MouseEventArgs e)
        {
            //base.OnClick(e);
            base.OnMouseDown(e);

            if(!_isShowUpdownButton || !canBeChanged)
            {
                return;
            }

            if(_btnUpIsEnabled)
            {
                if(btnUpDestRect.Contains(e.Location))
                {
                    this.btnUpBackImage = btnUpPressed;
                    this.Increase();
                }
            }
            if(_btnDownIsEnabled)
            {
                if (btnDownDestRect.Contains(e.Location))
                {
                    btnDownBackImage = btnDownPressed;
                    this.Decrease();
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!canBeChanged)
            {
                return;
            }

            if(this._isShowUpdownButton)
            {
                if (_btnUpIsEnabled)
                {
                    if (btnUpDestRect.Contains(e.Location))
                    {
                        btnUpBackImage = btnUpHover;
                        this.Invalidate(this.btnUpDestRect);
                        this.Update();
                    }
                    else
                    {
                        btnUpBackImage = btnUpNormal;
                        this.Invalidate(this.btnUpDestRect);
                        this.Update();
                    }
                }

                if (_btnDownIsEnabled)
                {
                    if (btnDownDestRect.Contains(e.Location))
                    {
                        btnDownBackImage = btnDownHover;
                        this.Invalidate(this.btnDownDestRect);
                        this.Update();
                    }
                    else
                    {
                        btnDownBackImage = btnDownNormal;
                        this.Invalidate(this.btnDownDestRect);
                        this.Update();
                    }
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!canBeChanged)
            {
                return;
            }

            if (this._isShowUpdownButton)
            {
                if (_btnUpIsEnabled)
                {
                    btnUpBackImage = btnUpNormal;
                    this.Invalidate(this.btnUpDestRect);
                    this.Update();
                }

                if (_btnDownIsEnabled)
                {
                    btnDownBackImage = btnDownNormal;
                    this.Invalidate(this.btnDownDestRect);
                    this.Update();
                }
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

        protected override bool ProcessDialogKey(Keys keyData)
        {
            //AEC.Log.OutputLine("******");
            if (!canBeChanged)
            {
                return base.ProcessDialogKey(keyData);
            }
            if(this.textBox.Visible)
            {
                if (keyData == Keys.Escape)
                {
                    this.textBox.Visible = false;
                    this.Focus();
                }
                else if(keyData == Keys.Enter)
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

            //if (keyData == Keys.Escape)
            //{
            //    this.textBox.Visible = false;
            //    this.Focus();
            //    return false;
            //}
            //else if(keyData == Keys.Enter)
            //{
            //    this.textBox.Visible = false;
            //    this.cancelEditing = false;
            //    this.Focus();
            //    return false;
            //}
            //AEC.Log.OutputLine("ParamLabel ProcessDialogKey:{0}", keyData);
            ////AEC.Log.OutputLine("EQAdjuster ProcessDialogKey");
            //return base.ProcessDialogKey(keyData);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.PaintBackground(e.Graphics);
        }
        #endregion

        protected virtual void OnMouseDoubleClick(Point location)
        {
            if (this.calcIndexFromEditText != null && labelRect.Contains(location) && canBeChanged)
            {
                this.textBox.Visible = true;
                this.textBox.Text = this.Text;
                this.cancelEditing = true;
                this.textBox.Focus();
            }
        }

        protected virtual void PaintBackground(Graphics g)
        {
            //GDIHelper.InitializeGraphics(g);
            //if (this.label.Text.Length > 0)
            //{
            //    //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            //    PointF centerLeft = new PointF(2, this.Height / 2.0f);
            //    StringFormat sf = new StringFormat();
            //    sf.Alignment = StringAlignment.Center;
            //    sf.LineAlignment = StringAlignment.Center;
            //    RectangleF client = Rectangle.FromLTRB(this.Padding.Left, this.Padding.Top, this.Width - Padding.Right, this.Height - Padding.Bottom);
            //    using (Brush brush = new SolidBrush(this.ForeColor))
            //    {
            //        g.DrawString(this.label.Text, this.Font, brush, client, sf);
            //    }
            //    sf.Dispose();
            //}
            //Rectangle rect = ClientRectangle;
            //g.FillRectangle(new SolidBrush(Color.White), ClientRectangle);
            //g.FillRectangle(new SolidBrush(Color.Green), labelRect);

            //labelRect.Height = 30;
            //Image backImage = this.Enabled ? AEC.Properties.Resources.param_label_normal : AEC.Properties.Resources.param_label_disabled;
            Image backImage = this.Enabled ? normalBack : disabledBack;
            if(backImage != null)
            {
                if(_userGDIPaint)
                {
                    GDIHelper.QuickDrawImage(g, labelRect, backImage);
                }
                else
                {
                    Rectangle srcDest = new Rectangle(new Point(), backImage.Size);
                    g.DrawImage(backImage, labelRect, srcDest, GraphicsUnit.Pixel);
                }
            }

            if (this.Text.Length > 0)
            {
                Color foreColor = this.Enabled ? this.ForeColor : disabledForeColor;
                TextRenderer.DrawText(g, this.Text, this.Font, labelRect, foreColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }

            if (isShowUpdownButton)
            {
                if(_userGDIPaint)
                {
                    if (btnUpBackImage != null)
                    {
                        GDIHelper.QuickDrawImage(g, btnUpDestRect.Location, btnUpBackImage);
                    }
                    if (btnDownBackImage != null)
                    {
                        GDIHelper.QuickDrawImage(g, btnDownDestRect.Location, btnDownBackImage);
                    }
                }
                else
                {
                    Rectangle srcRect = new Rectangle(new Point(), btnSize);
                    g.DrawImage(btnUpBackImage, btnUpDestRect, srcRect, GraphicsUnit.Pixel);
                    g.DrawImage(btnDownBackImage, btnDownDestRect, srcRect, GraphicsUnit.Pixel);
                }
            }
        }

        //public static bool PointInRect(Point pt, Rectangle rect)
        //{
        //    if (pt.X >= rect.Left && pt.X <= rect.Right &&
        //        pt.Y >= rect.Top && pt.Y <= rect.Bottom)
        //    {
        //        return true;
        //    }

        //    return false;
        //}
    }

    public class VolumeLabel:ParamLabel
    {
        private ShortParam volumeParam;
        private BoolParam muteParam;

        public Image muteBack { get; set; }

        public event EventHandler<EventArgs> MuteChanged;

        public VolumeLabel():
            base()
        {
            base.isShowUpdownButton = false;
        }

        public void Init(ShortParam volumeParam_, BoolParam muteParam_)
        {
            Debug.Assert(volumeParam_ != null);
            this.volumeParam = volumeParam_;
            this.muteParam = muteParam_;

            this.canBeChanged = this.muteParam != null ? !this.muteParam.value : true;

            base.Init(volumeParam_.max, volumeParam_.v, ParamGetLabelHelper.GetVolumeLabel);
        }

        public void UpdateUIFromData()
        {
            this.initV = this.volumeParam.v;
        }

        //public void UpdateMuteState()
        //{
        //    base.canBeChanged = !this.muteParam.value;
        //    this.UpdateDisplay();
        //}

        protected override void OnMouseDoubleClick(Point location)
        {
            if(this.muteParam == null)
            {
                return;
            }

            if (this.labelRect.Contains(location))
            {
                this.muteParam.value = !this.muteParam.value;
                base.canBeChanged = !this.muteParam.value;
                this.UpdateDisplay();
                if (this.MuteChanged != null)
                {
                    this.MuteChanged(this, EventArgs.Empty);
                }
            }
        }

        protected override void PaintBackground(Graphics g)
        {
            GDIHelper.InitializeGraphics(g);

            Image backImage = (this.muteParam != null && this.muteParam.value) ? muteBack : normalBack;
            if(backImage != null)
            {
                Rectangle srcDest = new Rectangle(new Point(), backImage.Size);
                g.DrawImage(backImage, labelRect, srcDest, GraphicsUnit.Pixel);
            }

            if (this.Text.Length > 0)
            {
                TextRenderer.DrawText(g, this.Text, this.Font, labelRect, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }
    }
}
