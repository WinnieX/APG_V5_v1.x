using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using AEC.UI.Skin;
using AEC.UI.Utility;

namespace AEC.UI
{
    [ToolboxBitmap(typeof(ComboBox))]
    public partial class gtComboBox : ComboBox
    {
        #region fileds

        /// <summary>
        /// 控件的状态
        /// </summary>
        //private EnumControlState _ControlState;

        private IntPtr _EditHandle = IntPtr.Zero;

        private int _Margin = 2;

        private bool _BeginPainting = false;

        private int _CornerRadius = 0;

        //private Color _BackColor = Color.Black;

        private Color _arrowColor = Color.Gray;
        private Color _dropDownBtnColor = Color.White;
        private Color _disabledColor = Color.DarkGray;

        #endregion

        #region Initializes
        public gtComboBox()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.UpdateStyles();
            this.Size = new Size(150, 20);
        }
        #endregion

        #region Properties

        //protected override bool ShowFocusCues
        //{
        //    get { return false; }
        //}

        internal Rectangle ButtonRect
        {
            get
            {
                return this.GetDropDownButtonRect();
            }
        }


        //[Browsable(true)]
        //public new Color BackColor
        //{
        //    get { return base.BackColor; }
        //}

        public Color ArrowColor
        {
            get
            {
                return this._arrowColor;
            }
            set
            {
                this._arrowColor = value;
            }
        }

        public Color DropDownBtnColor
        {
            get
            {
                return this._dropDownBtnColor;
            }
            set
            {
                this._dropDownBtnColor = value;
            }
        }

        private Color DisabledColor
        {
            get
            {
                return this._disabledColor;
            }
            set
            {
                this._disabledColor = value;
            }
        }

        [Browsable(false)]
        public new RightToLeft RightToLeft
        {
            get { return base.RightToLeft; }
        }

        internal Rectangle EditRect
        {
            get
            {
                if (this.DropDownStyle == ComboBoxStyle.DropDownList)
                {
                    Rectangle rect = new Rectangle(
                        this._Margin, this._Margin, Width - this.ButtonRect.Width - this._Margin * 2, Height - this._Margin * 2);
                    if (RightToLeft == RightToLeft.Yes)
                    {
                        rect.X += this.ButtonRect.Right;
                    }

                    return rect;
                }

                if (IsHandleCreated && this._EditHandle != IntPtr.Zero)
                {
                    RECT rcClient = new RECT();
                    Win32.GetWindowRect(this._EditHandle, ref rcClient);
                    return RectangleToClient(rcClient.Rect);
                }

                return Rectangle.Empty;
            }
        }

        #endregion

        #region Override methods

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.DrawComboBox(e.Graphics);
        }

        //private int count = 0;
        //public Dictionary<int, int> msgMap = new Dictionary<int, int>();
        protected override void WndProc(ref Message m)
        {
            //AEC.Log.OutputLine("gtComboBox WndProc Msg:{0:X} [{1}]", m.Msg, ++count);
            //if (msgMap.ContainsKey(m.Msg))
            //{
            //    msgMap[m.Msg] += 1;
            //}
            //else
            //{
            //    msgMap[m.Msg] = 1;
            //}

            switch (m.Msg)
            {
                case (int)WindowMessages.WM_PAINT:
                    switch (this.DropDownStyle)
                    {
                        case ComboBoxStyle.DropDown:
                            if (!this._BeginPainting)
                            {
                                PAINTSTRUCT ps = new PAINTSTRUCT();
                                this._BeginPainting = true;
                                Win32.BeginPaint(m.HWnd, ref ps);
                                this.DrawComboBox(ref m);
                                Win32.EndPaint(m.HWnd, ref ps);
                                this._BeginPainting = false;
                                m.Result = Win32.TRUE;
                            }
                            else
                            {
                                base.WndProc(ref m);
                            }
                            break;
                        case ComboBoxStyle.DropDownList:
                            //if (!this._BeginPainting)
                            //{
                            //    PAINTSTRUCT ps = new PAINTSTRUCT();
                            //    this._BeginPainting = true;
                            //    Win32.BeginPaint(m.HWnd, ref ps);
                            //    this.DrawComboBox(ref m);
                            //    Win32.EndPaint(m.HWnd, ref ps);
                            //    this._BeginPainting = false;
                            //    m.Result = Win32.TRUE;
                            //}
                            //else
                            //{
                            //    base.WndProc(ref m);
                            //}
                            //Console.WriteLine("Cmb wmPaint...");
                            base.WndProc(ref m);
                            this.DrawComboBox(ref m);
                            break;
                        default:
                            base.WndProc(ref m);
                            break;
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        #endregion

        #region private methods

        #region RenderComboBox

        /// <summary>
        /// 绘制复选框和内容.
        /// </summary>
        /// User:Ryan  CreateTime:2011-07-29 15:44.
        private void DrawComboBox(ref Message msg)
        {
            using (Graphics g = Graphics.FromHwnd(msg.HWnd))
            {
                this.DrawComboBox(g);
            }
        }

        /// <summary>
        /// 绘制下拉框区域.
        /// </summary>
        /// <param name="g">The Graphics.</param>
        /// User:Ryan  CreateTime:2011-07-29 15:44.
        private void DrawComboBox(Graphics g)
        {
            GDIHelper.InitializeGraphics(g);
            Rectangle rect = new Rectangle(Point.Empty, this.Size);
            //Rectangle rect = new Rectangle(new Point(0, this.Height / 2), this.Size);
            rect.Width--;
            rect.Height--;
            //g.FillRectangle(new SolidBrush(Color.Black), rect);
            //return;
            
            ////背景
            RoundRectangle roundRect = new RoundRectangle(rect, new CornerRadius(this._CornerRadius));
            Color backColor = this.Enabled ? this.BackColor : DisabledColor;
            g.SetClip(this.EditRect, CombineMode.Exclude);
            GDIHelper.FillRectangle(g, roundRect, backColor);
            g.ResetClip();
            this.DrawButton(g);
            ////边框
            GDIHelper.DrawPathBorder(g, roundRect);
        }

        /// <summary>
        ///  绘制按钮
        /// </summary>
        /// <param name="g">The Graphics.</param>
        /// User:Ryan  CreateTime:2011-08-02 14:23.
        private void DrawButton(Graphics g)
        {
            Rectangle btnRect;
            EnumControlState btnState = this.GetComboBoxButtonPressed() ? EnumControlState.HeightLight : EnumControlState.Default;
            btnRect = new Rectangle(this.ButtonRect.X - 4, this.ButtonRect.Y - 1, this.ButtonRect.Width + 3 + this._Margin, this.ButtonRect.Height + 2);
            RoundRectangle btnRoundRect = new RoundRectangle(btnRect, new CornerRadius(0, this._CornerRadius, 0, this._CornerRadius));
            Blend blend = new Blend(3);
            blend.Positions = new float[] { 0f, 0.5f, 1f };
            blend.Factors = new float[] { 0f, 1f, 0f };
            //GDIHelper.FillRectangle(g, btnRoundRect, SkinManager.CurrentSkin.DefaultControlColor);
            GDIHelper.FillRectangle(g, btnRoundRect, this._dropDownBtnColor);
            Size btnSize = new Size(10, 7);
            ArrowDirection direction = ArrowDirection.Down;
            GDIHelper.DrawArrow(g, direction, btnRect, btnSize, 0f, ArrowColor);
            Color lineColor = SkinManager.CurrentSkin.BorderColor;
            GDIHelper.DrawGradientLine(g, lineColor, 90, btnRect.X, btnRect.Y, btnRect.X, btnRect.Bottom - 1);
        }

        #endregion

        #region GetBoxInfo

        private ComboBoxInfo GetComboBoxInfo()
        {
            ComboBoxInfo cbi = new ComboBoxInfo();
            cbi.cbSize = Marshal.SizeOf(cbi);
            Win32.GetComboBoxInfo(base.Handle, ref cbi);
            return cbi;
        }

        private bool GetComboBoxButtonPressed()
        {
            ComboBoxInfo cbi = this.GetComboBoxInfo();
            return cbi.stateButton == ComboBoxButtonState.STATE_SYSTEM_PRESSED;
        }

        private Rectangle GetDropDownButtonRect()
        {
            ComboBoxInfo cbi = this.GetComboBoxInfo();
            return cbi.rcButton.Rect;
        }

        #endregion

        #region ResetRegion

        private void ResetRegion()
        {
            if (this._CornerRadius > 0)
            {
                Rectangle rect = new Rectangle(Point.Empty, this.Size);
                RoundRectangle roundRect = new RoundRectangle(rect, new CornerRadius(this._CornerRadius));
                if (this.Region != null)
                {
                    this.Region.Dispose();
                }

                this.Region = new Region(roundRect.ToGraphicsBezierPath());
            }
        }
        #endregion

        #endregion

        #region 附加

        #region 获取值
        /// <summary>
        /// 获取值
        /// </summary>
        private object Value
        {
            get
            {
                ComboBoxItem item = this.SelectedItem as ComboBoxItem;

                if (item == null)
                {
                    return string.Empty;
                }

                return item.Value;
            }
        }
        #endregion



        #endregion
    }

    #region ComboBoxItem(绑定下拉框的自定义对象)

    /// <summary>
    /// 绑定下拉框的自定义对象
    /// </summary>
    /// User:Ryan  CreateTime:2012-9-14 20:48.
    public class ComboBoxItem
    {
        public ComboBoxItem(string text, string value)
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// 内容文本
        /// </summary>
        /// <value>The text.</value>
        /// User:Ryan  CreateTime:2012-9-14 20:49.
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// 值
        /// </summary>
        /// <value>The value.</value>
        /// User:Ryan  CreateTime:2012-9-14 20:49.
        public string Value
        {
            get;
            set;
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
    #endregion
}
