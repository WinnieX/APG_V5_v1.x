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
    public partial class AudioInputLevel : Control
    {
        private const int minLevel = -96;
        private const int maxLevel = 0;

        //private static readonly int[] ticks = { minLevel, -72, -60, -48, -36, -24, -12, -6, maxLevel };
        private static readonly int[] ticks = { minLevel, -48, -24, -12, -6, -3, maxLevel };
        private static readonly Color borderColor = Color.FromArgb(96, 96, 96);
        private static readonly Color tickLineColor = Color.FromArgb(32, 32, 64);
       
        private const int levelLeftPadding = 6;
        private const int labelWidth = 72;
        private const int levelHeight = 24;
        private const int labelLeftPadding = 4;
        private const int tickLineHeight = 4;

        private static Font labelFont = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
        private static Font tickLabelFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);

        private double _level = minLevel;

        private static readonly Pen borderPen = new Pen(borderColor);
        private static readonly Pen tickLinePen = new Pen(tickLineColor);
        private static readonly Brush tickLabelBrush = new SolidBrush(tickLineColor);

        private bool _isShowLevel = false;
        private bool _isShowTick = true;

        private int levelGrade = 0;
        private int levelWidth = 0;

        private PointF[] tickLines = null;

        private static readonly StringFormat sf;

        static AudioInputLevel()
        {
            sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Near;
        }

        public double level
        {
            set
            {
                _level = Math.Min(Math.Max(value, minLevel), (double)maxLevel);

                this.levelGrade = CalcLevelGrade(_level);
                this.levelWidth = CalcLevelWidth(_level);

                this.Invalidate();
                this.Update();
            }
        }

        public bool isShowLevel
        {
            get
            {
                return _isShowLevel;
            }
            set
            {
                if(_isShowLevel != value)
                {
                    _isShowLevel = value;
                    this.Invalidate();
                    this.Update();
                }
            }
        }

        public bool isShowTick
        {
            get
            {
                return _isShowTick;
            }
            set
            {
                if (_isShowTick != value)
                {
                    _isShowTick = value;
                }
            }
        }

        public void Reset()
        {
            _level = minLevel;
            _isShowLevel = false;
            this.Invalidate();
            this.Update();
        }

        public AudioInputLevel()
        {
            InitializeComponent();
            Init();
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

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ResizeClient();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Render(pe.Graphics);
        }

        private void Render(Graphics g)
        {
            if (_isShowLevel)
            {
                PaintLevel(g);
                PaintLevelValue(g);
            }

            PaintBorder(g);

            if (_isShowTick)
            {
                DrawTicks(g);
                //PaintLevelValue(g);
            }
        }

        private void DrawTicks(Graphics g)
        {
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            for (int i = 0; i < tickLines.Length; ++i)
            {
                PointF st = new PointF();
                st.X = tickLines[i].X;
                st.Y = tickLines[i].Y;
                PointF ed = new PointF();
                ed.X = st.X;
                ed.Y = st.Y + tickLineHeight;

                g.DrawLine(tickLinePen, st, ed);

                //st.X = tickLine[i].X;
                //st.Y = rightChLevelRect.Top;
                //ed.X = st.X;
                //ed.Y = rightChLevelRect.Bottom;
                //g.DrawLine(blackPen, st, ed);
                //111
                //Size size = Size.Ceiling(g.MeasureString(tickText[i], tickFont));
                //PointF textPos = new PointF();
                //textPos.X = st.X - size.Width / 2;
                //textPos.Y = tickLine[i].Y - size.Height / 2;
                string label = i > 0 ? ticks[i].ToString() : "dB";
                g.DrawString(label, tickLabelFont, tickLabelBrush, ed, sf);
                //tickText
            }
        }

        private void PaintBorder(Graphics g)
        {
            g.DrawRectangle(borderPen, levelRect);
        }

        private void PaintLevel(Graphics g)
        {
            if (levelWidth > 0 && linearGradientBrush != null)
            {
                Rectangle rect = levelRect;
                rect.Width = levelWidth;
                g.FillRectangle(linearGradientBrush, rect);
            }
        }

        private void PaintLevelValue(Graphics g)
        {
            Rectangle labelRect = new Rectangle(this.Width - labelWidth + labelLeftPadding, 0, labelWidth - labelLeftPadding, levelHeight);
            string label = string.Format("{0:0.0} dB", _level);
            TextRenderer.DrawText(g, label, labelFont, labelRect, this.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }

        private Rectangle levelRect = new Rectangle();

        private void ResizeClient()
        {
            levelRect.X = levelLeftPadding;
            levelRect.Y = 1;
            levelRect.Width = this.Width - levelLeftPadding - labelWidth;
            levelRect.Height = levelHeight - 1;

            if (tickLines == null || tickLines.Length != ticks.Length)
            {
                tickLines = new PointF[ticks.Length];
            }
            for (int i = 0; i < tickLines.Length; ++i )
            {
                tickLines[i].X = CalcLevelWidth(ticks[i]) + levelLeftPadding;
                tickLines[i].Y = levelRect.Bottom;
            }

            InitColorBlend();

            linearGradientBrush = new LinearGradientBrush(levelRect, Color.FromArgb(55, 239, 70), Color.FromArgb(245, 70, 70), LinearGradientMode.Horizontal);
            linearGradientBrush.InterpolationColors = colorBlend;
            linearGradientBrush.GammaCorrection = true;
        }

        private const int colorBlendCount = 7;
        private ColorBlend colorBlend = null;
        private LinearGradientBrush linearGradientBrush = null;

        private void InitColorBlend()
        {
            if (this.colorBlend == null)
            {
                this.colorBlend = new ColorBlend(colorBlendCount);
                colorBlend.Colors = new Color[colorBlendCount] {
                    Color.FromArgb(55, 140, 70),        //-96dB
                    Color.FromArgb(55, 167, 70),        //-48dB
                    Color.FromArgb(55, 222, 70),        //-24dB
                    Color.FromArgb(170, 239, 70),       //-12dB
                    Color.FromArgb(245, 208, 70),       //-6dB
                    Color.FromArgb(245, 139, 70),        //-3dB
                    Color.FromArgb(245, 70, 70)         //0dB
                };
                colorBlend.Positions = new float[colorBlendCount] { 0f, 1.0f / 6, 2.0f / 6, 3.0f / 6, 4.0f / 6, 5.0f / 6, 1.0f };
            }
        }

        private int CalcLevelWidth(double level)
        {
            if (level <= minLevel)
            {
                return 0;
            }
            else if (level >= maxLevel)
            {
                return levelRect.Width;
            }

            int grade = CalcLevelGrade(level);
            double tempLevel = level - ticks[grade];
            double sectionWidth = (double)this.levelRect.Width / (ticks.Length - 1);

            double gradeWidth = sectionWidth * grade;
            double tempWidth = tempLevel / (ticks[grade + 1] - ticks[grade]) * sectionWidth;
            //Console.WriteLine("width = {0}", tempWidth);
            return (int)(gradeWidth + tempWidth);
        }

        private int CalcLevelGrade(double level)
        {
            if (level <= minLevel)
            {
                return 0;
            }
            else if (level >= maxLevel)
            {
                return ticks.Length - 1;
            }

            int grade = 0;

            for (; grade < ticks.Length - 1; ++grade)
            {
                if (level >= ticks[grade] && level < ticks[grade + 1])
                {
                    break;
                }
            }

            return grade;
        }
    }
}
