using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using AEC.RTA;

namespace AEC.Core
{
    class EQGraphicHelper
    {
        private static Dictionary<Size, EQGraphicHelper> graphicHelperMap = new Dictionary<Size,EQGraphicHelper>();
        
        private static Size currSize = Size.Empty;
        public static EQGraphicHelper GetCurrInstance()
        {
            if (!currSize.IsEmpty && graphicHelperMap.ContainsKey(currSize))
            {
                return graphicHelperMap[currSize];
            }

            return null;
        }

        public static EQGraphicHelper GetInstance(Size size)
        {
            Debug.Assert(!size.IsEmpty);
            if(graphicHelperMap.ContainsKey(size))
            {
                currSize = size;
                return graphicHelperMap[size];
            }
            else
            {
                currSize = size;
                graphicHelperMap[size] = new EQGraphicHelper(size);
                return graphicHelperMap[size];
            }
        }

        private const int PaddingLeft = 38;
        private const int PaddingRight = 48;
        private const int PaddingTop = 38;
        private const int PaddingBottom = 20;

        private const double freqLowerLimit = 10.0;
        private const double freqUpperLimit = 23600.0;

        private const int gainSpace = 3;
        
        private const int spMinAmp = -150;
        private const double spAmpRange = -spMinAmp;
        private const int spAmpSpace = 25;
        private const int tfMaxAmpResp = 18;
        private const int tfMinAmpResp = -18;
        private const double tfAmpRespRange = tfMaxAmpResp - tfMinAmpResp;
        private const int tfAmpRespSpace = 6;

        private const int tfMaxPhaseResp = 180;
        private const int tfMinPhaseResp = -180;
        private const double tfPhaseRespRange = tfMaxPhaseResp - tfMinPhaseResp;
        private const int tfPhaseRespSpace = 60;

        private static readonly int gainLowerLimit = (int)DataUtils.minEQGain;
        private static readonly int gainUpperLimit = (int)DataUtils.maxEQGain;
        private static readonly double gainRange = gainUpperLimit - gainLowerLimit;

        private static readonly double log10FreqLowerLimit = Math.Log10(freqLowerLimit);
        private static readonly double freqRange = Math.Log10(freqUpperLimit) - Math.Log10(freqLowerLimit);

        //缩略图
        public static readonly int ThumbnailGainLowerLimit = -24;
        public static readonly int ThumbnailGainUpperLimit = 12;
        public static readonly float ThumbnailGainRange = ThumbnailGainUpperLimit - ThumbnailGainLowerLimit;
        private const int ThumbnailInterval = 1;
        public const int ThumbnailWidth = 86;
        public const int ThumbnailHeight = 42;
        private const int ThumbnailPadding = 3;
        private const int ThumbnailEqWidth = ThumbnailWidth - 2 * ThumbnailPadding;       
        private const int ThumbnailEqHeight = ThumbnailHeight - 2 * ThumbnailPadding;
        private static readonly Rectangle ThumbnailEqRect = new Rectangle(ThumbnailPadding, ThumbnailPadding, ThumbnailEqWidth, ThumbnailEqHeight);
        public static readonly Rectangle ThumbnailClipRect = new Rectangle(1, 1, ThumbnailWidth - 2, ThumbnailHeight - 2);
        private static readonly double yPixelToGainScale = ThumbnailEqHeight / gainRange;
        private static readonly double xPixelToFreqScale = ThumbnailEqWidth / freqRange;

        //private static readonly Interval<float>[] ThumbnailPixel;
        private static readonly Interval<int>[] ThumbnailFreqIndex;
        //private const int ThumbnailCurvePointNum = ThumbnailEqWidth;
        private static readonly float ThumbnailZerodBLineY;

        public static readonly float zerodBPosition;

        static EQGraphicHelper()
        {
            Interval<float>[] ThumbnailPixel = new Interval<float>[ThumbnailEqWidth / ThumbnailInterval];
            for (int i = 0; i < ThumbnailPixel.Length; ++i)
            {
                ThumbnailPixel[i].start = ThumbnailInterval * i;
                ThumbnailPixel[i].end = ThumbnailPixel[i].start + ThumbnailInterval;
            }

            ThumbnailFreqIndex = new Interval<int>[ThumbnailPixel.Length];
            for (int i = 0; i < ThumbnailFreqIndex.Length; ++i)
            {
                float startFreq = ThumbnailPanelX2Freq(ThumbnailPixel[i].start);
                float endFreq = ThumbnailPanelX2Freq(ThumbnailPixel[i].end);

                if(i == 0)
                {
                    ThumbnailFreqIndex[i].start = Freq2RespKeyFreqIndex(startFreq);
                }
                else
                {
                    ThumbnailFreqIndex[i].start = ThumbnailFreqIndex[i - 1].end + 1;
                }
                ThumbnailFreqIndex[i].end = Freq2RespKeyFreqIndex(endFreq);
                int startIndex = ThumbnailFreqIndex[i].start;
                int endIndex = ThumbnailFreqIndex[i].end;
            }
            ThumbnailZerodBLineY = Gain2ThumbnailPanelY(0);

            zerodBPosition = ThumbnailZerodBLineY / ThumbnailHeight;
        }

        public static PointF[] InitThumbnailCurve()
        {
            PointF[] curve = new PointF[ThumbnailFreqIndex.Length * 2];
            for(int i = 0; i < curve.Length; ++i)
            {
                curve[i] = new PointF(i / 2.0f + ThumbnailPadding, ThumbnailZerodBLineY);
            }

            return curve;
        }

        public static void UpdateRespFreqThumbnailCurve(PointF[] curve, double[] amps)
        {
            Debug.Assert(curve != null && amps != null && curve.Length == (ThumbnailFreqIndex.Length * 2) && amps.Length == DSP.FreqResp.keyFreqList.Length);
            for(int i = 0; i < ThumbnailFreqIndex.Length; ++i)
            {
                int startIndex = ThumbnailFreqIndex[i].start;
                int endIndex = ThumbnailFreqIndex[i].end;
                int minIndex = startIndex;
                int maxIndex = startIndex;
                double min = amps[startIndex];
                double max = amps[startIndex];
                for(int j = startIndex + 1; j <= endIndex; ++j)
                {
                    if(amps[j] < min)
                    {
                        min = amps[j];
                        minIndex = j;
                    }
                    else if(amps[j] > max)
                    {
                        max = amps[j];
                        maxIndex = j;
                    }
                }

                if(minIndex == maxIndex)
                {
                    curve[ThumbnailInterval * i * 2].Y = Gain2ThumbnailPanelY(amps[startIndex]);
                    curve[ThumbnailInterval * i * 2 + 1].Y = curve[ThumbnailInterval * i * 2].Y;
                }
                else if(minIndex < maxIndex)
                {
                    curve[ThumbnailInterval * i * 2].Y = Gain2ThumbnailPanelY(min);
                    curve[ThumbnailInterval * i * 2 + 1].Y = Gain2ThumbnailPanelY(max);
                }
                else
                {
                    curve[ThumbnailInterval * i * 2].Y = Gain2ThumbnailPanelY(max);
                    curve[ThumbnailInterval * i * 2 + 1].Y = Gain2ThumbnailPanelY(min);
                }
            }
        }

        private static readonly double[] freqList1 = new double[]
        {
            100.0, 1000.0, 10000.0,
        };

        private static readonly double[] freqList2 = new double[]
        {
            20.0, 30.0, 40.0, 50.0, 60.0, 70.0, 80.0, 90.0,
            200.0, 300.0, 400.0, 500.0, 600.0, 700.0, 800.0, 900.0,
            2000.0, 3000.0, 4000.0, 5000.0, 6000.0, 7000.0, 8000.0, 9000.0,
            20000.0,
        };

        private static readonly int[] labelFreq = new int[]
        {
            20, 50,
            100, 200, 500,
            1000, 2000, 5000,
            10000, 20000,
        };

        private static readonly int[] labelGain = new int[]
        {
            -24, -18, -12, -6, 0, 6, 12,
        };

        private static readonly string eqBypass = "Bypassed";

        private static AudioAnalysis audioAnalysic = AudioAnalysis.GetInstance();

        private readonly Font axisLabelFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
        private readonly Font bypassLabelFont = new System.Drawing.Font("Microsoft Sans Serif", 64F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);

        private readonly Color axisLabelColor = Color.White;
        private readonly Color bypassLabelColor = Color.FromArgb(109, 221, 23);

        public readonly Size clientSize;
        private readonly int eqRectWidth;
        private readonly int eqRectHeight;
        public readonly Rectangle eqRect;

        private readonly float zerodBLineY;

        private PointF[] axis;  //四跟轴的点
        private LineF zerodBLine;
        private LineF[] hLine1s;
        private LineF[] hLine2s;
        private LineF[] vLine1s;
        private LineF[] vLine2s;
        private float[] eqFreq2XTable;
        private float[] filter2XTable;
        public float[] freqrespFreq2XTable;
        private float[] eqGain2YTable;
        private List<Label> freqLabels;  //频率轴标签
        private List<Label> gainLabels;  //增益轴标签
        private Label eqPassLabel;       //EQPass标签
        private List<Label> spAmpLabels;   //频谱标签
        private List<Label> tfFreqRespLabels;    //频率响应Y轴标签

        private EQGraphicHelper(Size clientSize_)
        {
            Debug.Assert(!clientSize_.IsEmpty);
            this.clientSize = clientSize_;
            this.eqRectWidth = clientSize.Width - PaddingLeft - PaddingRight;
            this.eqRectHeight = clientSize.Height - PaddingTop - PaddingBottom;
            
            this.eqRect = new Rectangle(PaddingLeft, PaddingTop, eqRectWidth, eqRectHeight);

            this.zerodBLineY = Gain2PanelY(0.0);

            this.InitAxis();
            this.InitGridLine();
            this.InitTable();
            this.InitAxis();
            this.InitLabel();
        }

        private void InitAxis()
        {
            this.axis = new PointF[]
            {
                new PointF(this.eqRect.Left, this.eqRect.Top),
                new PointF(this.eqRect.Right, this.eqRect.Top),
                new PointF(this.eqRect.Right, this.eqRect.Bottom),
                new PointF(this.eqRect.Left, this.eqRect.Bottom),
                new PointF(this.eqRect.Left, this.eqRect.Top),
            };
        }

        private void InitGridLine()
        {
            //水平线
            zerodBLine = new LineF(
                new PointF(this.eqRect.Left, (float)zerodBLineY),
                new PointF(this.eqRect.Right, (float)zerodBLineY)
            );
            List<LineF> hLine1List = new List<LineF>();
            List<LineF> hLine2List = new List<LineF>();
            int hLineCount = (gainUpperLimit - gainLowerLimit) / gainSpace;
            for(int i = 1; i < hLineCount; ++i)
            {
                int gain = gainLowerLimit + i * gainSpace;
                double y = Gain2PanelY(gain);
                LineF line = new LineF(
                    new PointF(this.eqRect.Left, (float)y),
                    new PointF(this.eqRect.Right, (float)y)
                );
                if (gain != 0){
                    if (i % 2 == 0)
                    {
                        hLine1List.Add(line);
                    }
                    else
                    {
                        hLine2List.Add(line);
                    }
                }
            }
            hLine1s = hLine1List.ToArray();
            hLine2s = hLine2List.ToArray();

            //垂直线
            List<LineF> vLine1List = new List<LineF>();
            List<LineF> vLine2List = new List<LineF>();
            foreach(double freq in freqList1)
            {
                double x = Freq2PanelX(freq);
                vLine1List.Add(
                    new LineF(
                        new PointF((float)x, this.eqRect.Top),
                        new PointF((float)x, this.eqRect.Bottom)
                    )
               );
            }
            foreach (double freq in freqList2)
            {
                double x = Freq2PanelX(freq);
                vLine2List.Add(
                    new LineF(
                        new PointF((float)x, this.eqRect.Top),
                        new PointF((float)x, this.eqRect.Bottom)
                    )
               );
            }
            vLine1s = vLine1List.ToArray();
            vLine2s = vLine2List.ToArray();
        }

        private void InitTable()
        {
            eqFreq2XTable = new float[DataUtils.eqFreqList.Length];
            for(int i = 0; i < eqFreq2XTable.Length; ++i)
            {
                eqFreq2XTable[i] = Freq2PanelX(DataUtils.eqFreqList[i]);
            }

            filter2XTable = new float[DataUtils.filterFreqList.Length];
            for (int i = 0; i < filter2XTable.Length; ++i)
            {
                filter2XTable[i] = Freq2PanelX(DataUtils.filterFreqList[i]);
            }

            freqrespFreq2XTable = new float[DSP.FreqResp.keyFreqList.Length];
            for (int i = 0; i < freqrespFreq2XTable.Length; ++i)
            {
                freqrespFreq2XTable[i] = Freq2PanelX(DSP.FreqResp.keyFreqList[i]);
            }

            eqGain2YTable = new float[DataUtils.eqGainList.Length];
            for (int i = 0; i < eqGain2YTable.Length; ++i)
            {
                eqGain2YTable[i] = Gain2PanelY(DataUtils.eqGainList[i]);
            }
        }

        private void InitLabel()
        {
            freqLabels = new List<Label>();
            foreach(int freq in labelFreq)
            {
                freqLabels.Add(new Label(
                    DataUtils.Freq2String(freq),
                    axisLabelFont,
                    Color.White,
                    new PointF(Freq2PanelX(freq), eqRect.Bottom),
                    Alignment.BottomCenter
                    ));
            }

            gainLabels = new List<Label>();
            foreach(int gain in labelGain)
            {
                gainLabels.Add(new Label(
                    DataUtils.Gain2String(gain),
                    axisLabelFont,
                    Color.White,
                    new PointF(eqRect.Left, Gain2PanelY(gain)),
                    Alignment.CenterLeft
                    ));
            }

            eqPassLabel = new Label(
                eqBypass,
                bypassLabelFont,
                bypassLabelColor,
                new PointF((this.eqRect.Left + this.eqRect.Right) / 2, (this.eqRect.Top + this.eqRect.Bottom) / 2),
                Alignment.Center
                );

            spAmpLabels = new List<Label>();
            for (int amp = 0; amp >= spMinAmp; amp -= spAmpSpace)
            {
                spAmpLabels.Add(new Label(
                    DataUtils.Gain2String(amp),
                    axisLabelFont,
                    Color.FromArgb(0, 192, 0),
                    new PointF(eqRect.Right, SpAmp2PanelY(amp)),
                    Alignment.CenterRight
                    ));
            }

            tfFreqRespLabels = new List<Label>();
            for (int amp = tfMaxAmpResp; amp >= tfMinAmpResp; amp -= tfAmpRespSpace)
            {
                tfFreqRespLabels.Add(new Label(
                    DataUtils.Gain2String(amp),
                    axisLabelFont,
                    Color.FromArgb(192, 192, 0),
                    new PointF(eqRect.Right, TfAmpResp2PanelY(amp)),
                    Alignment.TopRight
                    ));
            }

            for (int phase = tfMaxPhaseResp; phase >= tfMinPhaseResp; phase -= tfPhaseRespSpace)
            {
                tfFreqRespLabels.Add(new Label(
                    string.Format("{0}°", phase),
                    axisLabelFont,
                    Color.FromArgb(192, 0, 192),
                    new PointF(eqRect.Right, TfPhaseResp2PanelY(phase)),
                    Alignment.BottomRight
                    ));
            }
        }

        public static readonly Color darkGray = Color.FromArgb(0xff, 64, 64, 64);
        public static readonly Color gray = Color.FromArgb(0xff, 96, 96, 96);
        public static readonly Color lightGray = Color.FromArgb(0xff, 128, 128, 128);

        public static readonly Pen darkGrayPen = new Pen(darkGray, 1.0f);
        public static readonly Pen grayPen = new Pen(gray, 1.0f);
        public static readonly Pen lightGrayPen = new Pen(lightGray, 1.0f); 

        public void PaintBackground(Graphics graphics)
        {
            graphics.Clear(Color.Black);

            foreach (LineF line in this.hLine1s)
            {
                graphics.DrawLine(grayPen, line.st, line.ed);
            }
            foreach (LineF line in this.hLine2s)
            {
                graphics.DrawLine(darkGrayPen, line.st, line.ed);
            }
            foreach (LineF line in this.vLine1s)
            {
                graphics.DrawLine(grayPen, line.st, line.ed);
            }
            foreach (LineF line in this.vLine2s)
            {
                graphics.DrawLine(darkGrayPen, line.st, line.ed);
            }

            //gBitmap.FillRectangle(new SolidBrush(deepGray), eqGraphicHelper.eqRect);

            graphics.DrawLine(lightGrayPen, this.zerodBLine.st, this.zerodBLine.ed);
            graphics.DrawLines(lightGrayPen, this.axis);

            foreach (EQGraphicHelper.Label label in this.freqLabels)
            {
                //label.Paint(graphics);
                label.Paint(graphics);
            }

            foreach (EQGraphicHelper.Label label in this.gainLabels)
            {
                label.Paint(graphics);
            }

            this.PaintRTA(graphics);

            

            //foreach (EQGraphicHelper.Label label in this.tfPhaseRespLabels)
            //{
            //    label.Paint(graphics);
            //}

            
            

            //if(this.cachedBackground != null)
            //{
            //    AEC.UI.GDIHelper.QuickDrawImage(graphics, Point.Empty, this.cachedBackground);
            //}
            //else
            //{
            //    this.cachedBackground = new Bitmap(this.clientSize.Width, this.clientSize.Height);
            //    Graphics gBitmap = Graphics.FromImage(this.cachedBackground);

            //    gBitmap.Clear(Color.Black);

            //    foreach (LineF line in this.hLine1s)
            //    {
            //        gBitmap.DrawLine(grayPen, line.st, line.ed);
            //    }
            //    foreach (LineF line in this.hLine2s)
            //    {
            //        gBitmap.DrawLine(darkGrayPen, line.st, line.ed);
            //    }
            //    foreach (LineF line in this.vLine1s)
            //    {
            //        gBitmap.DrawLine(grayPen, line.st, line.ed);
            //    }
            //    foreach (LineF line in this.vLine2s)
            //    {
            //        gBitmap.DrawLine(darkGrayPen, line.st, line.ed);
            //    }

            //    //gBitmap.FillRectangle(new SolidBrush(deepGray), eqGraphicHelper.eqRect);

            //    gBitmap.DrawLine(lightGrayPen, this.zerodBLine.st, this.zerodBLine.ed);
            //    gBitmap.DrawLines(lightGrayPen, this.axis);

            //    foreach (EQGraphicHelper.Label label in this.freqLabels)
            //    {
            //        label.Paint(gBitmap);
            //    }

            //    foreach (EQGraphicHelper.Label label in this.gainLabels)
            //    {
            //        label.Paint(gBitmap);
            //    }

            //    AEC.UI.GDIHelper.QuickDrawImage(graphics, Point.Empty, this.cachedBackground);
            //}
        }

        public void PaintEQPass(Graphics graphics)
        {
            eqPassLabel.Paint(graphics);
        }

        private void PaintRTA(Graphics graphics)
        {
            if (!audioAnalysic.displayIsOn)
            {
                return;
            }

            if (audioAnalysic.rtaMode == RTA.Core.RTAMode.Spectrum)
            {
                foreach (EQGraphicHelper.Label label in this.spAmpLabels)
                {
                    label.Paint(graphics);
                }
            }
            else
            {
                foreach (EQGraphicHelper.Label label in this.tfFreqRespLabels)
                {
                    label.Paint(graphics);
                }
            }

            audioAnalysic.PaintRTAMap(graphics);
        }

        public Rectangle topRect
        {
            get
            {
                return new Rectangle(Point.Empty, new Size(this.clientSize.Width, PaddingTop));
            }
        }

        
        public float getZerodBLineY
        {
            get
            {
                return this.zerodBLineY;
            }
        }

        public PointF GetEQLocation(EQ eq)
        {
            return new PointF(eqFreq2XTable[eq.freqIndex], eqGain2YTable[eq.gainIndex]);
        }

        public PointF GetFilterLocation(XPFilterBase filter)
        {
            return new PointF(filter2XTable[filter.filterFreqIndex], zerodBLineY);
        }

        //根据增益计算y坐标
        public float Gain2PanelY(double gain)
        {
            return (float)((gainUpperLimit - gain) * (eqRectHeight / gainRange) + PaddingTop);
        }

        //根据频率计算x坐标
        public float Freq2PanelX(double freq)
        {
            return (float)((Math.Log10(freq) - log10FreqLowerLimit) * (eqRectWidth / freqRange) + PaddingLeft);
        }

        //根据x坐标计算频率
        public float PanelX2Freq(double x)
        {
            return (float)Math.Pow(10, (x - PaddingLeft) / (eqRect.Width / freqRange) + log10FreqLowerLimit);
        }

        //根据频谱幅度dB计算y坐标
        public float SpAmp2PanelY(double amp)
        {
            return (float)((0 - amp) * (eqRectHeight / spAmpRange) + PaddingTop);
        }

        //根据幅频响应dB计算y坐标
        public float TfAmpResp2PanelY(double amp)
        {
            return (float)((tfMaxAmpResp - amp) * (eqRectHeight / tfAmpRespRange) + PaddingTop);
        }

        //根据相频响应角度计算y坐标
        public float TfPhaseResp2PanelY(double phase)
        {
            return (float)((tfMaxPhaseResp - phase) * (eqRectHeight / tfPhaseRespRange) + PaddingTop);
        }

        //根据增益计算缩略图y坐标
        public static float Gain2ThumbnailPanelY(double gain)
        {
            return (float)((gainUpperLimit - gain) * yPixelToGainScale + ThumbnailPadding);
        }

        //根据缩略图x坐标(不计内边距)计算频率
        public static float ThumbnailPanelX2Freq(double x)
        {
            return (float)Math.Pow(10, x / xPixelToFreqScale + log10FreqLowerLimit);
        }

        public short PanelY2NearestGain(float y)
        {
            double gain = gainUpperLimit - (y - eqRect.Top) / (eqRect.Height / gainRange);
            short gainIndex = (short)Math.Round(((gain - gainLowerLimit) * 5.0), MidpointRounding.AwayFromZero);
            return Utils.ConstrainMax(gainIndex, DataUtils.maxGainIndex);
        }

        public short PanelX2NearestEQFreq(float x)
        {
            double freq = Math.Pow(10, (x - eqRect.Left) / (eqRect.Width / freqRange) + log10FreqLowerLimit);
            return DataUtils.FindNearestIndexFromOrderedList(freq, DataUtils.eqFreqList);
        }

        public short PanelX2NearestFilterFreq(float x)
        {
            double freq = Math.Pow(10, (x - eqRect.Left) / (eqRect.Width / freqRange) + log10FreqLowerLimit);
            return DataUtils.FindNearestIndexFromOrderedList(freq, DataUtils.filterFreqList);
        }

        //通过频率计算频率响应对应频点索引
        public static short Freq2RespKeyFreqIndex(float freq)
        {
            return DataUtils.FindIndexFromOrderedList(freq, DSP.FreqResp.keyFreqList);
        }

        public enum Alignment
        {
            TopRight,
            CenterLeft,
            Center,
            CenterRight,
            BottomRight,
            BottomCenter,
        }

        public class Label
        {
            public readonly string text;
            public readonly Font font;
            private Brush brush;
            public readonly PointF point;
            private readonly StringFormat sf;
            //System.Windows.Forms.TextFormatFlags tff;

            public Label(string text_, Font font_, Color color_, PointF point_, Alignment alignment_)
            {
                this.text = text_;
                this.font = font_;
                this.brush = new System.Drawing.SolidBrush(color_);
                this.point = point_;
                this.sf = new StringFormat();
                switch (alignment_)
                {
                    case Alignment.TopRight:
                        this.sf.Alignment = StringAlignment.Near;
                        this.sf.LineAlignment = StringAlignment.Far;
                        //tff = System.Windows.Forms.TextFormatFlags.Right | System.Windows.Forms.TextFormatFlags.VerticalCenter;
                        break;

                    case Alignment.Center:
                        this.sf.Alignment = StringAlignment.Center;
                        this.sf.LineAlignment = StringAlignment.Center;
                        //tff = System.Windows.Forms.TextFormatFlags.HorizontalCenter | System.Windows.Forms.TextFormatFlags.VerticalCenter;
                        break;

                    case Alignment.CenterLeft:
                        this.sf.Alignment = StringAlignment.Far;
                        this.sf.LineAlignment = StringAlignment.Center;
                        //tff = System.Windows.Forms.TextFormatFlags.Left | System.Windows.Forms.TextFormatFlags.VerticalCenter;
                        break;

                    case Alignment.CenterRight:
                        this.sf.Alignment = StringAlignment.Near;
                        this.sf.LineAlignment = StringAlignment.Center;
                        //tff = System.Windows.Forms.TextFormatFlags.VerticalCenter;
                        break;

                    case Alignment.BottomCenter:
                        this.sf.Alignment = StringAlignment.Center;
                        this.sf.LineAlignment = StringAlignment.Near;
                        //tff = System.Windows.Forms.TextFormatFlags.HorizontalCenter | System.Windows.Forms.TextFormatFlags.Bottom;
                        //tff = System.Windows.Forms.TextFormatFlags.Top;
                        break;

                    default:
                        break;
                }
            }

            public void Paint(Graphics g)
            {
                g.DrawString(
                    this.text,
                    this.font,
                    this.brush,
                    this.point.X,
                    this.point.Y,
                    this.sf);
            }

            //public void GDIPaint(Graphics g)
            //{
            //    Point pt = Point.Ceiling(this.point);
            //    System.Windows.Forms.TextRenderer.DrawText(g, this.text, this.font, pt, Color.White, tff);
            //}
        }
    }

    public class LineF
    {
        public readonly PointF st;
        public readonly PointF ed;

        public LineF(PointF st_, PointF ed_)
        {
            this.st = st_;
            this.ed = ed_;
        }
    }

    public struct Interval<T>
    {
        public T start;
        public T end;
    }
}
