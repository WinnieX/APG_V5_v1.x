using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.UI;
using AEC.Core;
using AEC.Common;
using AEC.Effectors.VX;
using AEC.Effectors.VX.Modules;
using System.Drawing.Drawing2D;
using AEC.UI.Forms;

namespace AEC.UITest
{
    public partial class FormTest : Form
    {
        public const string FreqUnit = "Hz";
        public const string Kilo = "k";

        public FormTest()
        {
            InitializeComponent();
            //this.hpfFreq.Init(MusicModule.maxHPFFreqIndex, 50, GetHpfFreqLabel, CalcFreqIndexFromText);
            //this.micFbe1.Init(2);
            //levelMusic.thumbCenterY = 20;
            //outputLevelPhase2.Binding(new Level(0, 0, 1, null, 500));
            //InitFont();
            //this.textBox1
            //this.gtComboBox1.SizeChanged += gtComboBox1_SizeChanged;
        }

        //void gtComboBox1_SizeChanged(object sender, EventArgs e)
        //{
        //    //throw new NotImplementedException();
        //}

        

        private void FormTest_Load(object sender, EventArgs e)
        {
            //this.paramLabel1.Text = "1000Hz";

            //this.paramLabel1.Init(120, 30, GetLabel, CalcIndexFromEditText);

            //this.hSlider1.maximum = 150;
            //this.hSlider1.v = 50;
        }

        private string GetLabel(short index)
        {
            return string.Format("{0:0.0} dB", index * 0.1f);
        }

        private string GetEditText(short index)
        {
            return string.Format("{0:0.0}", index * 0.1f);
        }

        private short CalcIndexFromEditText(string text)
        {
            float index;
            if (float.TryParse(text, out index))
            {
                if(index > 12.0)
                {
                    return 120;
                }
                else if(index < 0)
                {
                    return 0;
                }
                else
                {
                    return ((short)(index * 10));
                }
                
            }

            return -1;
        }

        private string GetHpfFreqLabel(short index)
        {
            return DataUtils.FilterIndex2String(index);
        }

        private short CalcFreqIndexFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, FreqUnit);
            double value;
            double multiple = 1.0;
            if (str.EndsWith(Kilo))
            {
                int index = text.IndexOf(Kilo);
                if (index > 0)
                {
                    str = text.Remove(index);
                    multiple = 1000.0;
                }
            }
            if (double.TryParse(str, out value))
            {
                short index = DataUtils.Freq2EQIndex(value * multiple);
                return index;
            }

            return -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(this.hpfFreq.SliderWidth);
            //this.hpfFreq.SliderWidth = 150;
            //Console.WriteLine(this.hpfFreq.SliderWidth);
        }

        private void FormTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            Tools.LogHelper.WriteInfo("FormTest_FormClosing");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormTest_FormClosed(object sender, FormClosedEventArgs e)
        {
            Tools.LogHelper.WriteInfo("FormTest_FormClosed");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Color color = Color.Blue;
            Color shadow = Color.FromArgb(255, 16, 16, 16);

            for (int i = 0; i < 8; i++)
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(80 - i * 10, shadow)))
                {
                    g.FillEllipse(brush, panel1.ClientRectangle.X + i * 2,
                                         panel1.ClientRectangle.Y + i, 60, 60);
                }
            using (SolidBrush brush = new SolidBrush(color))
                g.FillEllipse(brush, panel1.ClientRectangle.X, panel1.ClientRectangle.Y, 60, 60);

            // move to the right to use the same coordinates again for the drawn shape
            g.TranslateTransform(80, 0);

            for (int i = 0; i < 8; i++)
                using (Pen pen = new Pen(Color.FromArgb(80 - i * 10, shadow), 2.5f))
                {
                    g.DrawEllipse(pen, panel1.ClientRectangle.X + i * 1.25f,
                                       panel1.ClientRectangle.Y + i, 60, 60);
                }
            using (Pen pen = new Pen(color))
                g.DrawEllipse(pen, panel1.ClientRectangle.X, panel1.ClientRectangle.Y, 60, 60);
        }


        void drawShadow(Graphics G, Color c, GraphicsPath GP, int d)
        {
            Color[] colors = getColorVector(c, this.BackColor, d).ToArray();
            for (int i = 0; i < d; i++)
            {
                G.TranslateTransform(1f, 0.75f);                // <== shadow vector!
                using (Pen pen = new Pen(colors[i], 1.75f))  // <== pen width (*)
                    G.DrawPath(pen, GP);
            }
            G.ResetTransform();
        }


        List<Color> getColorVector(Color fc, Color bc, int depth)
        {
            List<Color> cv = new List<Color>();
            float dRed = 1f * (bc.R - fc.R) / depth;
            float dGreen = 1f * (bc.G - fc.G) / depth;
            float dBlue = 1f * (bc.B - fc.B) / depth;
            for (int d = 1; d <= depth; d++)
                cv.Add(Color.FromArgb(255, (int)(fc.R + dRed * d),
                  (int)(fc.G + dGreen * d), (int)(fc.B + dBlue * d)));
            return cv;
        }

        GraphicsPath getRectPath(Rectangle R)
        {
            byte[] fm = new byte[3];
            for (int b = 0; b < 3; b++) fm[b] = 1;
            List<Point> points = new List<Point>();
            points.Add(new Point(R.Left, R.Bottom));
            points.Add(new Point(R.Right, R.Bottom));
            points.Add(new Point(R.Right, R.Top));
            return new GraphicsPath(points.ToArray(), fm);
        }

        private void button0_Click(object sender, EventArgs e)
        {
            using (Graphics G = this.CreateGraphics())
                drawShadow(G, Color.Black, getRectPath(new Rectangle(111, 111, 222, 222)), 17);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            ColorBlend colorBlend = new ColorBlend(3);
            colorBlend.Colors = new Color[] { Color.FromArgb(2, 17, 30), Color.FromArgb(0, 80, 117), Color.FromArgb(2, 17, 30) };
            colorBlend.Positions = new float[] { 0, 0.5f, 1.0f };
            LinearGradientBrush lgb = new LinearGradientBrush(this.panel1.ClientRectangle, this.BackColor, this.BackColor, LinearGradientMode.Vertical);
            lgb.InterpolationColors = colorBlend;
            lgb.GammaCorrection = true;

            if (lgb != null)
            {
                e.Graphics.FillRectangle(lgb, this.panel1.ClientRectangle);
            }
        }

        private const float minLevel = -30;
        private const float maxLevel = 14;
        private const float range = maxLevel - minLevel;
        private readonly Color darkGreen = Color.FromArgb(0, 128, 0);
        private readonly Color lightGreen = Color.FromArgb(0, 192, 0);
        private readonly Color yellow = Color.FromArgb(255, 255, 0);
        private readonly Color orange = Color.FromArgb(255, 165, 0);
        private readonly Color red = Color.FromArgb(255, 0, 0);

        private void FormTest_Paint(object sender, PaintEventArgs e)
        {
            //ColorBlend colorBlend = new ColorBlend(3);

            //colorBlend.Colors = new Color[] { 
            //    Color.FromArgb(0, 128, 0), 
            //    Color.FromArgb(0, 192, 0), 
            //    Color.FromArgb(255, 255, 0),
            //    Color.FromArgb(255, 165, 0),
            //    Color.FromArgb(255, 0, 0),
            //};

            //float[] t = new float[]{
            //    minLevel, -6, 0, 8, maxLevel
            //};

            //colorBlend.Positions = new float[t.Length];
            //for(int i = 0; i < t.Length; ++i)
            //{
            //    colorBlend.Positions[i] = (t[i] - minLevel) / range;
            //}

            //LinearGradientBrush lgb = new LinearGradientBrush(this.ClientRectangle, this.BackColor, this.BackColor, LinearGradientMode.Vertical);
            //lgb.InterpolationColors = colorBlend;
            //lgb.GammaCorrection = true;

            //if (lgb != null)
            //{
            //    e.Graphics.FillRectangle(lgb, this.ClientRectangle);
            //}
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            ColorBlend colorBlend = new ColorBlend(7);

            colorBlend.Colors = new Color[] {
                darkGreen,
                darkGreen,
                lightGreen, 
                yellow,
                orange,
                red,
                red,
            };

            float[] t = new float[]{
                minLevel, -12, 0, 4, 7, 10, maxLevel
            };

            colorBlend.Positions = new float[t.Length];
            for (int i = 0; i < t.Length; ++i)
            {
                colorBlend.Positions[i] = (t[i] - minLevel) / range;
            }

            LinearGradientBrush lgb = new LinearGradientBrush(this.panel1.ClientRectangle, this.BackColor, this.BackColor, LinearGradientMode.Horizontal);
            lgb.InterpolationColors = colorBlend;
            lgb.GammaCorrection = true;

            e.Graphics.FillRectangle(lgb, this.panel1.ClientRectangle);

            float[] tick = new float[]
            {
                -12,
                -6,
                0,
                2,
                4,
                6,
                8,
            };
            for (int i = 0; i < tick.Length; ++i)
            {
                float x = (tick[i] - minLevel) / range * this.panel1.Width;
                PointF st = new PointF(x, 0);
                PointF ed = new PointF(x, this.panel1.Height);
                //e.Graphics.DrawLine(new Pen(Color.Black), st, ed);
            }
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {
            ColorBlend colorBlend = new ColorBlend(7);

            colorBlend.Colors = new Color[] {
                darkGreen,
                darkGreen,
                lightGreen, 
                yellow,
                orange,
                red,
                red,
            };

            float[] t = new float[]{
                minLevel, -12, -2, 2, 4, 8, maxLevel
            };

            colorBlend.Positions = new float[t.Length];
            for (int i = 0; i < t.Length; ++i)
            {
                colorBlend.Positions[i] = (t[i] - minLevel) / range;
            }

            LinearGradientBrush lgb = new LinearGradientBrush(this.panel2.ClientRectangle, this.BackColor, this.BackColor, LinearGradientMode.Horizontal);
            lgb.InterpolationColors = colorBlend;
            lgb.GammaCorrection = true;

            e.Graphics.FillRectangle(lgb, this.panel2.ClientRectangle);

            float[] tick = new float[]
            {
                -12,
                -6,
                0,
                2,
                4,
                6,
                8,
            };
            for (int i = 0; i < tick.Length; ++i)
            {
                float x = (tick[i] - minLevel) / range * this.panel2.Width;
                PointF st = new PointF(x, 0);
                PointF ed = new PointF(x, this.panel2.Height);
                //e.Graphics.DrawLine(new Pen(Color.Black), st, ed);
            }
        }

        private Form formUsageLimitLock = null;

        private void ShowFromUsageLimitLock()
        {
            if (this.formUsageLimitLock == null || this.formUsageLimitLock.IsDisposed)
            {
                this.formUsageLimitLock = new UI.UsageLimitLocks.FormUsageLimitLockManagement();
                //this.formUsageLimitLock.Binding(this.mController, this.system, this._eventBus);
                this.formUsageLimitLock.Owner = this;
            }
            else
            {
                this.formUsageLimitLock.Activate();
            }

            this.formUsageLimitLock.Show();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.ShowFromUsageLimitLock();
        }
    }
}
