using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using AEC.UI.Utility;

namespace AEC.UI
{
    [DefaultEvent("Scroll")]
    public partial class HScrollbar : Control
    {
        /// <summary>
        /// The is radius
        /// </summary>
        private bool _isRadius = false;

        /// <summary>
        /// The corner radius
        /// </summary>
        private int _cornerRadius = 24;


        /// <summary>
        /// The is show rect
        /// </summary>
        private bool _isShowRect = false;

        /// <summary>
        /// The rect color
        /// </summary>
        private Color _rectColor = Color.FromArgb(80, 80, 80);

        /// <summary>
        /// The rect width
        /// </summary>
        private int _rectWidth = 1;

        /// <summary>
        /// The fill color
        /// </summary>
        private Color _fillColor = Color.FromArgb(64, 64, 64);
        /// <summary>
        /// 是否圆角
        /// </summary>
        /// <value><c>true</c> if this instance is radius; otherwise, <c>false</c>.</value>
        [Description("是否圆角"), Category("自定义")]
        public virtual bool IsRadius
        {
            get
            {
                return this._isRadius;
            }
            set
            {
                this._isRadius = value;
                Refresh();
            }
        }
        /// <summary>
        /// 圆角角度
        /// </summary>
        /// <value>The coner radius.</value>
        [Description("圆角角度"), Category("自定义")]
        public virtual int ConerRadius
        {
            get
            {
                return this._cornerRadius;
            }
            set
            {
                this._cornerRadius = Math.Max(value, 1);
                Refresh();
            }
        }

        /// <summary>
        /// 是否显示边框
        /// </summary>
        /// <value><c>true</c> if this instance is show rect; otherwise, <c>false</c>.</value>
        [Description("是否显示边框"), Category("自定义")]
        public virtual bool IsShowRect
        {
            get
            {
                return this._isShowRect;
            }
            set
            {
                this._isShowRect = value;
                Refresh();
            }
        }
        /// <summary>
        /// 边框颜色
        /// </summary>
        /// <value>The color of the rect.</value>
        [Description("边框颜色"), Category("自定义")]
        public virtual Color RectColor
        {
            get
            {
                return this._rectColor;
            }
            set
            {
                this._rectColor = value;
                this.Refresh();
            }
        }
        /// <summary>
        /// 边框宽度
        /// </summary>
        /// <value>The width of the rect.</value>
        [Description("边框宽度"), Category("自定义")]
        public virtual int RectWidth
        {
            get
            {
                return this._rectWidth;
            }
            set
            {
                this._rectWidth = value;
                Refresh();
            }
        }
        /// <summary>
        /// 当使用边框时填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        /// <value>The color of the fill.</value>
        [Description("当使用边框时填充颜色，当值为背景色或透明色或空值则不填充"), Category("自定义")]
        public virtual Color FillColor
        {
            get
            {
                return this._fillColor;
            }
            set
            {
                this._fillColor = value;
                Refresh();
            }
        }

        #region 属性    English:attribute
        private int lastValue = 0;
        /// <summary>
        /// The mo large change
        /// </summary>
        protected int moLargeChange = 10;
        /// <summary>
        /// The mo small change
        /// </summary>
        protected int moSmallChange = 1;
        /// <summary>
        /// The mo minimum
        /// </summary>
        protected int moMinimum = 0;
        /// <summary>
        /// The mo maximum
        /// </summary>
        protected int moMaximum = 100;
        /// <summary>
        /// The mo value
        /// </summary>
        protected int moValue = 0;
        /// <summary>
        /// The n click point
        /// </summary>
        private int nClickPoint;
        /// <summary>
        /// The mo thumb top
        /// </summary>
        protected int moThumbLeft = 0;
        /// <summary>
        /// The mo automatic size
        /// </summary>
        protected bool moAutoSize = false;
        /// <summary>
        /// The mo thumb down
        /// </summary>
        private bool moThumbMouseDown = false;
        /// <summary>
        /// The mo thumb dragging
        /// </summary>
        private bool moThumbMouseDragging = false;
        /// <summary>
        /// Occurs when [scroll].
        /// </summary>
        public event EventHandler Scroll = null;
        /// <summary>
        /// Occurs when [value changed].
        /// </summary>
        public event EventHandler ValueChanged = null;

        /// <summary>
        /// The BTN height
        /// </summary>
        private int btnWidth = 18;
        /// <summary>
        /// The m int thumb minimum height
        /// </summary>
        private int m_intThumbMinWidth = 15;

        /// <summary>
        /// Gets or sets the height of the BTN.
        /// </summary>
        /// <value>The height of the BTN.</value>
        public int BtnWidth
        {
            get { return btnWidth; }
            set { btnWidth = value; }
        }
        /// <summary>
        /// Gets or sets the large change.
        /// </summary>
        /// <value>The large change.</value>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("自定义"), Description("LargeChange")]
        public int LargeChange
        {
            get { return moLargeChange; }
            set
            {
                moLargeChange = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the small change.
        /// </summary>
        /// <value>The small change.</value>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("自定义"), Description("SmallChange")]
        public int SmallChange
        {
            get { return moSmallChange; }
            set
            {
                moSmallChange = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("自定义"), Description("Minimum")]
        public int Minimum
        {
            get { return moMinimum; }
            set
            {
                moMinimum = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("自定义"), Description("Maximum")]
        public int Maximum
        {
            get 
            { 
                return moMaximum;
            }
            set
            {
                moMaximum = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("自定义"), Description("Value")]
        public int Value
        {
            get { return moValue; }
            set
            {
                moValue = value;

                int nTrackWidth = (this.Width - btnWidth * 2);
                float fThumbWidth = ((float)LargeChange / (float)Maximum) * nTrackWidth;
                int nThumbWidth = (int)fThumbWidth;

                if (nThumbWidth > nTrackWidth)
                {
                    nThumbWidth = nTrackWidth;
                    fThumbWidth = nTrackWidth;
                }
                if (nThumbWidth < m_intThumbMinWidth)
                {
                    nThumbWidth = m_intThumbMinWidth;
                    fThumbWidth = m_intThumbMinWidth;
                }

                //figure out value
                int nPixelRange = nTrackWidth - nThumbWidth;
                int nRealRange = (Maximum - Minimum) - LargeChange;
                float fPerc = 0.0f;
                if (nRealRange != 0)
                {
                    fPerc = (float)moValue / (float)nRealRange;

                }

                float fLeft = fPerc * nPixelRange;
                moThumbLeft = (int)fLeft;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic size].
        /// </summary>
        /// <value><c>true</c> if [automatic size]; otherwise, <c>false</c>.</value>
        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                base.AutoSize = value;
                if (base.AutoSize)
                {
                    this.Width = 15;
                }
            }
        }

        /// <summary>
        /// The thumb color
        /// </summary>
        private Color thumbColor = Color.FromArgb(128, 128, 128);

        /// <summary>
        /// Gets or sets the color of the thumb.
        /// </summary>
        /// <value>The color of the thumb.</value>
        public Color ThumbColor
        {
            get { return thumbColor; }
            set { thumbColor = value; }
        }
        #endregion

        public HScrollbar()
        {
            InitializeComponent();

            ConerRadius = 2;
            FillColor = Color.FromArgb(239, 239, 239);
            IsShowRect = false;
            IsRadius = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.Size = new System.Drawing.Size(150, 18);

            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CustomScrollbar_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CustomScrollbar_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CustomScrollbar_MouseUp);
        }

        #region 鼠标事件    English:Mouse event
        /// <summary>
        /// Handles the MouseDown event of the CustomScrollbar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void CustomScrollbar_MouseDown(object sender, MouseEventArgs e)
        {
            Point ptPoint = this.PointToClient(Cursor.Position);
            int nTrackWidth = (this.Width - btnWidth * 2);
            float fThumbWidth = ((float)LargeChange / (float)Maximum) * nTrackWidth;
            int nThumbWidth = (int)fThumbWidth;

            if (nThumbWidth > nTrackWidth)
            {
                nThumbWidth = nTrackWidth;
                fThumbWidth = nTrackWidth;
            }
            if (nThumbWidth < m_intThumbMinWidth)
            {
                nThumbWidth = m_intThumbMinWidth;
                fThumbWidth = m_intThumbMinWidth;
            }

            int nLeft = moThumbLeft;
            nLeft += btnWidth;


            Rectangle thumbrect = new Rectangle(new Point(nLeft, 1), new Size(nThumbWidth, this.Height - 2));
            //滑块
            if (thumbrect.Contains(ptPoint))
            {
                //hit the thumb
                nClickPoint = (ptPoint.X - nLeft);
                this.moThumbMouseDown = true;
            }
            else
            {
                //左按钮
                Rectangle leftarrowrect = new Rectangle(new Point(0, 1), new Size(btnWidth, this.Height));
                if (leftarrowrect.Contains(ptPoint))
                {
                    int nRealRange = (Maximum - Minimum) - LargeChange;
                    int nPixelRange = (nTrackWidth - nThumbWidth);
                    if (nRealRange > 0)
                    {
                        if (nPixelRange > 0)
                        {
                            if ((moThumbLeft - SmallChange) < 0)
                                moThumbLeft = 0;
                            else
                                moThumbLeft -= SmallChange;

                            //figure out value
                            float fPerc = (float)moThumbLeft / (float)nPixelRange;
                            float fValue = fPerc * (Maximum - LargeChange);

                            moValue = (int)fValue;

                            if (lastValue != moValue)
                            {
                                lastValue = moValue;
                                ValueChangedHandler();
                            }

                            Invalidate();
                        }
                    }
                }
                else
                {
                    Rectangle rightarrowrect = new Rectangle(new Point(btnWidth + nTrackWidth, 1), new Size(btnWidth, this.Height));
                    if (rightarrowrect.Contains(ptPoint))
                    {
                        int nRealRange = (Maximum - Minimum) - LargeChange;
                        int nPixelRange = (nTrackWidth - nThumbWidth);
                        if (nRealRange > 0)
                        {
                            if (nPixelRange > 0)
                            {
                                if ((moThumbLeft + SmallChange) > nPixelRange)
                                    moThumbLeft = nPixelRange;
                                else
                                    moThumbLeft += SmallChange;

                                //figure out value
                                float fPerc = (float)moThumbLeft / (float)nPixelRange;
                                float fValue = fPerc * (Maximum - LargeChange);

                                moValue = (int)fValue;

                                if (lastValue != moValue)
                                {
                                    lastValue = moValue;
                                    ValueChangedHandler();
                                }

                                Invalidate();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the MouseUp event of the CustomScrollbar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void CustomScrollbar_MouseUp(object sender, MouseEventArgs e)
        {
            this.moThumbMouseDown = false;
            this.moThumbMouseDragging = false;
        }

        /// <summary>
        /// Moves the thumb.
        /// </summary>
        /// <param name="x">The y.</param>
        private void MoveThumb(int x)
        {
            int nRealRange = Maximum - Minimum;
            int nTrackWidth = (this.Width - btnWidth * 2);
            float fThumbWidth = ((float)LargeChange / (float)Maximum) * nTrackWidth;
            int nThumbWidth = (int)fThumbWidth;

            if (nThumbWidth > nTrackWidth)
            {
                nThumbWidth = nTrackWidth;
                fThumbWidth = nTrackWidth;
            }
            if (nThumbWidth < m_intThumbMinWidth)
            {
                nThumbWidth = m_intThumbMinWidth;
                fThumbWidth = m_intThumbMinWidth;
            }

            int nSpot = nClickPoint;

            int nPixelRange = (nTrackWidth - nThumbWidth);
            if (moThumbMouseDown && nRealRange > 0)
            {
                if (nPixelRange > 0)
                {
                    int nNewThumbLeft = x - (btnWidth + nSpot);

                    if (nNewThumbLeft < 0)
                    {
                        moThumbLeft = nNewThumbLeft = 0;
                    }
                    else if (nNewThumbLeft > nPixelRange)
                    {
                        moThumbLeft = nNewThumbLeft = nPixelRange;
                    }
                    else
                    {
                        moThumbLeft = x - (btnWidth + nSpot);
                    }


                    float fPerc = (float)moThumbLeft / (float)nPixelRange;
                    float fValue = fPerc * (Maximum - LargeChange);
                    moValue = (int)fValue;

                    //Application.DoEvents();

                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the CustomScrollbar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void CustomScrollbar_MouseMove(object sender, MouseEventArgs e)
        {
            if (!moThumbMouseDown)
                return;

            if (moThumbMouseDown == true)
            {
                this.moThumbMouseDragging = true;
            }

            if (this.moThumbMouseDragging)
            {
                MoveThumb(e.X);
            }

            if (lastValue != moValue)
            {
                lastValue = moValue;
                ValueChangedHandler();
            }
        }
        #endregion

        protected override void OnPaint(PaintEventArgs pe)
        {
            //base.OnPaint(pe);
            if(!Visible)
            {
                return;
            }

            PaintScrollbar(pe.Graphics);
        }

        private void PaintScrollbar(Graphics g)
        {
            GDIHelper.InitializeGraphics(g);
            PaintBackground(g);
            //draw thumb
            int nTrackWidth = (this.Width - btnWidth * 2);
            float fThumbWidth = ((float)LargeChange / (float)Maximum) * nTrackWidth;
            int nThumbWidth = (int)fThumbWidth;

            if (nThumbWidth > nTrackWidth)
            {
                nThumbWidth = nTrackWidth;
                fThumbWidth = nTrackWidth;
            }
            if (nThumbWidth < m_intThumbMinWidth)
            {
                nThumbWidth = m_intThumbMinWidth;
                fThumbWidth = m_intThumbMinWidth;
            }
            int nLeft = moThumbLeft;
            nLeft += btnWidth;

            using (Brush brush = new SolidBrush(thumbColor))
            {
                Rectangle thumbRect = new Rectangle(nLeft, 1, nThumbWidth, this.Height - 3);
                GraphicsPath path = GDIHelper.CreateRoundedRectanglePath(thumbRect, this.ConerRadius);
                g.FillPath(brush, path);
                path.Dispose();
            }

            GDIHelper.PaintTriangle(g, new SolidBrush(thumbColor), new Point(btnWidth - Math.Min(5, this.Height / 2), this.Height / 2), Math.Min(5, this.Height / 2), GraphDirection.Leftward);
            GDIHelper.PaintTriangle(g, new SolidBrush(thumbColor), new Point(this.Width - (btnWidth - Math.Min(5, this.Height / 2)), this.Height / 2), Math.Min(5, this.Height / 2), GraphDirection.Rightward);
        }

        private void PaintBackground(Graphics g)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            Rectangle clientRectangle = base.ClientRectangle;
            graphicsPath.AddArc(0, 0, _cornerRadius, _cornerRadius, 180f, 90f);
            graphicsPath.AddArc(clientRectangle.Width - _cornerRadius - 1, 0, _cornerRadius, _cornerRadius, 270f, 90f);
            graphicsPath.AddArc(clientRectangle.Width - _cornerRadius - 1, clientRectangle.Height - _cornerRadius - 1, _cornerRadius, _cornerRadius, 0f, 90f);
            graphicsPath.AddArc(0, clientRectangle.Height - _cornerRadius - 1, _cornerRadius, _cornerRadius, 90f, 90f);
            graphicsPath.CloseFigure();
            
            using(Brush brush = new SolidBrush(this._fillColor))
            {
                g.FillPath(brush, graphicsPath);
            }

            if (this._isShowRect)
            {
                using (Pen pen = new Pen(this._rectColor, (float)this._rectWidth))
                {
                    g.DrawPath(pen, graphicsPath);
                }
            }

            graphicsPath.Dispose();
        }

        
        private void ValueChangedHandler()
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, new EventArgs());
            }

            if (Scroll != null)
            {
                Scroll(this, new EventArgs());
            }
        }
    }
}
