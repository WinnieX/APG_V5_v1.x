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
    public partial class VScrollbar : Control
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

        private int lastValue = 0;
        private int _wheelStep = 10;
        public int wheelStep
        {
            get
            {
                return _wheelStep;
            }
            set
            {
                _wheelStep = value;
            }
        }
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
        protected int moThumbTop = 0;
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
        private int btnHeight = 18;
        /// <summary>
        /// The m int thumb minimum height
        /// </summary>
        private int m_intThumbMinHeight = 15;

        /// <summary>
        /// Gets or sets the height of the BTN.
        /// </summary>
        /// <value>The height of the BTN.</value>
        public int BtnHeight
        {
            get { return btnHeight; }
            set { btnHeight = value; }
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
            get { return moMaximum; }
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

                int nTrackHeight = (this.Height - btnHeight * 2);
                float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
                int nThumbHeight = (int)fThumbHeight;

                if (nThumbHeight > nTrackHeight)
                {
                    nThumbHeight = nTrackHeight;
                    fThumbHeight = nTrackHeight;
                }
                if (nThumbHeight < m_intThumbMinHeight)
                {
                    nThumbHeight = m_intThumbMinHeight;
                    fThumbHeight = m_intThumbMinHeight;
                }

                //figure out value
                int nPixelRange = nTrackHeight - nThumbHeight;
                int nRealRange = (Maximum - Minimum) - LargeChange;
                float fPerc = 0.0f;
                if (nRealRange != 0)
                {
                    fPerc = (float)moValue / (float)nRealRange;

                }

                float fTop = fPerc * nPixelRange;
                moThumbTop = (int)fTop;

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

        public VScrollbar()
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
            this.Size = new System.Drawing.Size(18, 150);

            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CustomScrollbar_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CustomScrollbar_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CustomScrollbar_MouseUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.CustomScrollbar_MouseWheel);
        }

        /// <summary>
        /// Handles the MouseDown event of the CustomScrollbar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void CustomScrollbar_MouseDown(object sender, MouseEventArgs e)
        {
            Point ptPoint = this.PointToClient(Cursor.Position);
            int nTrackHeight = (this.Height - btnHeight * 2);
            float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
            int nThumbHeight = (int)fThumbHeight;

            if (nThumbHeight > nTrackHeight)
            {
                nThumbHeight = nTrackHeight;
                fThumbHeight = nTrackHeight;
            }
            if (nThumbHeight < m_intThumbMinHeight)
            {
                nThumbHeight = m_intThumbMinHeight;
                fThumbHeight = m_intThumbMinHeight;
            }

            int nTop = moThumbTop;
            nTop += btnHeight;


            Rectangle thumbrect = new Rectangle(new Point(1, nTop), new Size(this.Width - 2, nThumbHeight));
            if (thumbrect.Contains(ptPoint))
            {

                //hit the thumb
                nClickPoint = (ptPoint.Y - nTop);
                //MessageBox.Show(Convert.ToString((ptPoint.Y - nTop)));
                this.moThumbMouseDown = true;
            }
            else
            {
                Rectangle uparrowrect = new Rectangle(new Point(1, 0), new Size(this.Width, btnHeight));
                if (uparrowrect.Contains(ptPoint))
                {

                    int nRealRange = (Maximum - Minimum) - LargeChange;
                    int nPixelRange = (nTrackHeight - nThumbHeight);
                    if (nRealRange > 0)
                    {
                        if (nPixelRange > 0)
                        {
                            //if ((moThumbTop - SmallChange) < 0)
                            //    moThumbTop = 0;
                            //else
                            //    moThumbTop -= SmallChange;

                            ////figure out value
                            //float fPerc = (float)moThumbTop / (float)nPixelRange;
                            //float fValue = fPerc * (Maximum - LargeChange);

                            //moValue = (int)Math.Round(fValue, MidpointRounding.AwayFromZero);

                            //if (lastValue != moValue)
                            //{
                            //    lastValue = moValue;
                            //    ValueChangedHandler();
                            //}

                            //Invalidate();

                            this.moValue -= SmallChange;
                            if (moValue < 0)
                            {
                                this.moValue = 0;
                            }
                            if (lastValue != moValue)
                            {
                                lastValue = moValue;
                                ValueChangedHandler();

                                //int nTrackHeight = (this.Height - btnHeight * 2);
                                //float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
                                //int nThumbHeight = (int)fThumbHeight;

                                if (nThumbHeight > nTrackHeight)
                                {
                                    nThumbHeight = nTrackHeight;
                                    fThumbHeight = nTrackHeight;
                                }
                                if (nThumbHeight < m_intThumbMinHeight)
                                {
                                    nThumbHeight = m_intThumbMinHeight;
                                    fThumbHeight = m_intThumbMinHeight;
                                }

                                //figure out value
                                //int nPixelRange = nTrackHeight - nThumbHeight;
                                //int nRealRange = (Maximum - Minimum) - LargeChange;
                                float fPerc = 0.0f;
                                if (nRealRange != 0)
                                {
                                    fPerc = (float)moValue / (float)nRealRange;
                                }

                                float fTop = fPerc * nPixelRange;
                                moThumbTop = (int)fTop;
                            }
                            Invalidate();
                        }
                    }
                }
                else
                {
                    Rectangle downarrowrect = new Rectangle(new Point(1, btnHeight + nTrackHeight), new Size(this.Width, btnHeight));
                    if (downarrowrect.Contains(ptPoint))
                    {
                        int nRealRange = (Maximum - Minimum) - LargeChange;
                        int nPixelRange = (nTrackHeight - nThumbHeight);
                        if (nRealRange > 0)
                        {
                            if (nPixelRange > 0)
                            {
                                //if ((moThumbTop + SmallChange) > nPixelRange)
                                //    moThumbTop = nPixelRange;
                                //else
                                //    moThumbTop += SmallChange;

                                ////figure out value
                                //float fPerc = (float)moThumbTop / (float)nPixelRange;
                                //float fValue = fPerc * (Maximum - LargeChange);

                                //moValue = (int)Math.Round(fValue, MidpointRounding.AwayFromZero);

                                //if (lastValue != moValue)
                                //{
                                //    lastValue = moValue;
                                //    ValueChangedHandler();
                                //}

                                //Invalidate();

                                this.moValue += SmallChange;
                                if (this.moValue + moLargeChange > this.moMaximum)
                                {
                                    this.moValue = this.moMaximum - moLargeChange;
                                }
                                if (lastValue != moValue)
                                {
                                    lastValue = moValue;
                                    ValueChangedHandler();

                                    //int nTrackHeight = (this.Height - btnHeight * 2);
                                    //float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
                                    //int nThumbHeight = (int)fThumbHeight;

                                    if (nThumbHeight > nTrackHeight)
                                    {
                                        nThumbHeight = nTrackHeight;
                                        fThumbHeight = nTrackHeight;
                                    }
                                    if (nThumbHeight < m_intThumbMinHeight)
                                    {
                                        nThumbHeight = m_intThumbMinHeight;
                                        fThumbHeight = m_intThumbMinHeight;
                                    }

                                    //figure out value
                                    //int nPixelRange = nTrackHeight - nThumbHeight;
                                    //int nRealRange = (Maximum - Minimum) - LargeChange;
                                    float fPerc = 0.0f;
                                    if (nRealRange != 0)
                                    {
                                        fPerc = (float)moValue / (float)nRealRange;

                                    }

                                    float fTop = fPerc * nPixelRange;
                                    moThumbTop = (int)fTop;
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
        /// <param name="y">The y.</param>
        private void MoveThumb(int y)
        {
            int nRealRange = Maximum - Minimum;
            int nTrackHeight = (this.Height - btnHeight * 2);
            float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
            int nThumbHeight = (int)fThumbHeight;

            if (nThumbHeight > nTrackHeight)
            {
                nThumbHeight = nTrackHeight;
                fThumbHeight = nTrackHeight;
            }
            if (nThumbHeight < m_intThumbMinHeight)
            {
                nThumbHeight = m_intThumbMinHeight;
                fThumbHeight = m_intThumbMinHeight;
            }

            int nSpot = nClickPoint;

            int nPixelRange = (nTrackHeight - nThumbHeight);
            if (moThumbMouseDown && nRealRange > 0)
            {
                if (nPixelRange > 0)
                {
                    int nNewThumbTop = y - (btnHeight + nSpot);

                    if (nNewThumbTop < 0)
                    {
                        moThumbTop = nNewThumbTop = 0;
                    }
                    else if (nNewThumbTop > nPixelRange)
                    {
                        moThumbTop = nNewThumbTop = nPixelRange;
                    }
                    else
                    {
                        moThumbTop = y - (btnHeight + nSpot);
                    }

                    float fPerc = (float)moThumbTop / (float)nPixelRange;
                    float fValue = fPerc * (Maximum - LargeChange);
                    //Log.OutputLine("fValue = {0}", fValue);
                    moValue = (int)Math.Round(fValue, MidpointRounding.AwayFromZero);
                    //Log.OutputLine("moValue = {0}", moValue);
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
                MoveThumb(e.Y);
            }

            if (lastValue != moValue)
            {
                lastValue = moValue;
                ValueChangedHandler();
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the CustomScrollbar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void CustomScrollbar_MouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs h = e as HandledMouseEventArgs;
            if (h != null)
            {
                h.Handled = true;
            }

            if (e.Delta > 0)
            {
                if (this.moValue > 0)
                {
                    this.moValue -= wheelStep;
                    if(moValue < 0)
                    {
                        this.moValue = 0;
                    }
                    if (lastValue != moValue)
                    {
                        lastValue = moValue;
                        ValueChangedHandler();


                        int nTrackHeight = (this.Height - btnHeight * 2);
                        float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
                        int nThumbHeight = (int)fThumbHeight;

                        if (nThumbHeight > nTrackHeight)
                        {
                            nThumbHeight = nTrackHeight;
                            fThumbHeight = nTrackHeight;
                        }
                        if (nThumbHeight < m_intThumbMinHeight)
                        {
                            nThumbHeight = m_intThumbMinHeight;
                            fThumbHeight = m_intThumbMinHeight;
                        }

                        //figure out value
                        int nPixelRange = nTrackHeight - nThumbHeight;
                        int nRealRange = (Maximum - Minimum) - LargeChange;
                        float fPerc = 0.0f;
                        if (nRealRange != 0)
                        {
                            fPerc = (float)moValue / (float)nRealRange;
                        }

                        float fTop = fPerc * nPixelRange;
                        moThumbTop = (int)fTop;
                    }
                    Invalidate();
                }                
            }
            else
            {
                if (this.moValue + moLargeChange < this.moMaximum)
                {
                    this.moValue += wheelStep;
                    if(this.moValue + moLargeChange > this.moMaximum)
                    {
                        this.moValue = this.moMaximum - moLargeChange;
                    }
                    if (lastValue != moValue)
                    {
                        lastValue = moValue;
                        ValueChangedHandler();

                        int nTrackHeight = (this.Height - btnHeight * 2);
                        float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
                        int nThumbHeight = (int)fThumbHeight;

                        if (nThumbHeight > nTrackHeight)
                        {
                            nThumbHeight = nTrackHeight;
                            fThumbHeight = nTrackHeight;
                        }
                        if (nThumbHeight < m_intThumbMinHeight)
                        {
                            nThumbHeight = m_intThumbMinHeight;
                            fThumbHeight = m_intThumbMinHeight;
                        }

                        //figure out value
                        int nPixelRange = nTrackHeight - nThumbHeight;
                        int nRealRange = (Maximum - Minimum) - LargeChange;
                        float fPerc = 0.0f;
                        if (nRealRange != 0)
                        {
                            fPerc = (float)moValue / (float)nRealRange;

                        }

                        float fTop = fPerc * nPixelRange;
                        moThumbTop = (int)fTop;
                    }
                    Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if (!Visible)
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
            int nTrackHeight = (this.Height - btnHeight * 2);
            float fThumbHeight = ((float)LargeChange / (float)Maximum) * nTrackHeight;
            int nThumbHeight = (int)fThumbHeight;

            if (nThumbHeight > nTrackHeight)
            {
                nThumbHeight = nTrackHeight;
                fThumbHeight = nTrackHeight;
            }
            if (nThumbHeight < m_intThumbMinHeight)
            {
                nThumbHeight = m_intThumbMinHeight;
                fThumbHeight = m_intThumbMinHeight;
            }
            int nTop = moThumbTop;
            nTop += btnHeight;

            using (Brush brush = new SolidBrush(thumbColor))
            {
                Rectangle thumbRect = new Rectangle(1, nTop, this.Width - 3, nThumbHeight);
                GraphicsPath path = GDIHelper.CreateRoundedRectanglePath(thumbRect, this.ConerRadius);
                g.FillPath(brush, path);
                path.Dispose();
            }

            GDIHelper.PaintTriangle(g, new SolidBrush(thumbColor), new Point(this.Width / 2, btnHeight - Math.Min(5, this.Width / 2)), Math.Min(5, this.Width / 2), GraphDirection.Upward);
            GDIHelper.PaintTriangle(g, new SolidBrush(thumbColor), new Point(this.Width / 2, this.Height - (btnHeight - Math.Min(5, this.Width / 2))), Math.Min(5, this.Width / 2), GraphDirection.Downward);
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

            using (Brush brush = new SolidBrush(this._fillColor))
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
