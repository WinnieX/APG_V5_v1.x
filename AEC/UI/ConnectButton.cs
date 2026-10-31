using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.Misc;

namespace AEC.UI
{
    public partial class ConnectButton : Control
    {
        public enum ButtonState
        {
            normal,
            hover,
            pressed,
        }

        private ButtonState state = ButtonState.normal;
        private bool _isStretch = false;

        private bool _isConnected = false;

        public bool isConnected
        {
            get
            {
                return _isConnected;
            }
            set
            {
                if (_isConnected != value)
                {
                    _isConnected = value;
                    this.UpdateDisplay();
                }
            }
        }

        public bool isStretch
        {
            get
            {
                return _isStretch;
            }
            set
            {
                _isStretch = value;
            }
        }

        public Image connectNormal { get; set; }
        public Image connectHover { get; set; }
        public Image connectPressed { get; set; }
        public Image connectDisabled { get; set; }
        public Image disconnectNormal { get; set; }
        public Image disconnectHover { get; set; }

        public ConnectButton()
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
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }

        #region 重写基类方法

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            state = ButtonState.hover;
            //AEC.Log.OutputLine("OnMouseEnter ButtonState.hover");
            UpdateDisplay();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if(!this._isConnected)
            {
                state = ButtonState.pressed;
            }
            //AEC.Log.OutputLine("OnMouseEnter ButtonState.hover");
            UpdateDisplay();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (this.ClientRectangle.Contains(e.Location))
            {
                state = ButtonState.hover;
                //AEC.Log.OutputLine("OnEnabledChanged ButtonState.hover");
            }
            else
            {
                state = ButtonState.normal;
                //AEC.Log.OutputLine("OnEnabledChanged ButtonState.normal");
            }
            //AEC.Log.OutputLine("OnMouseUp ButtonState.normal");
            UpdateDisplay();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            state = ButtonState.normal;
            //AEC.Log.OutputLine("OnMouseLeave ButtonState.normal");
            UpdateDisplay();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (this.Enabled)
            {
                if (this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
                {
                    state = ButtonState.hover;
                    //AEC.Log.OutputLine("OnEnabledChanged ButtonState.hover");
                }
                else
                {
                    state = ButtonState.normal;
                    //AEC.Log.OutputLine("OnEnabledChanged ButtonState.normal");
                }
                UpdateDisplay();
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            //base.OnPaint(pe);
            PaintBackground(pe.Graphics);
            PaintForeground(pe.Graphics);
        }
        #endregion

        private void PaintBackground(Graphics g)
        {
            Image back = null;
            if (this.Enabled)
            {
                if (_isConnected)
                {
                    if(state == ButtonState.normal)
                    {
                        back = disconnectNormal;
                    }
                    else
                    {
                        //AEC.Log.OutputLine("disconnectHover");
                        back = disconnectHover;
                    }
                }
                else
                {
                    if (state == ButtonState.normal)
                    {
                        back = connectNormal;
                    }
                    else if(state == ButtonState.hover)
                    {
                        back = connectHover;
                    }
                    else if(state == ButtonState.pressed)
                    {
                        back = connectPressed;
                    }
                }
            }
            else
            {
                back = connectDisabled;
            }

            if(back != null)
            {
                Rectangle srcRect = new Rectangle(new Point(), back.Size);
                Rectangle destRect = _isStretch ? this.ClientRectangle : srcRect;
                g.DrawImage(back, destRect, srcRect, GraphicsUnit.Pixel);

                //GDIHelper.QuickDrawImage(g, Point.Empty, back);
            }
        }

        private void PaintForeground(Graphics g)
        {
            if (this.Enabled)
            {
                TextRenderer.DrawText(g, this.Text, this.Font, this.ClientRectangle, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
            else
            {
                TextRenderer.DrawText(g, this.Text, this.Font, this.ClientRectangle, Color.LightGray, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private void UpdateDisplay()
        {
            this.Invalidate();
            this.Update();
        }

        public void UpdateEnabled(bool enabled)
        {
            this.Enabled = enabled;
            this.UpdateDisplay();
            //AEC.Log.OutputLine("btn 状态:{0}", enabled);
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            if (_isConnected)
            {
                this.Text = localization[Localization.buttonConnection_Disconnect];
            }
            else
            {
                this.Text = localization[Localization.buttonConnection_Connect];
            }

            this.Invalidate();
            this.Update();
        }
    }
}
