using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using AEC.Core;
using AEC.Core.DSP;

namespace AEC.Common
{
    public class EQGraphic
    {
        private readonly ModuleBase module;

        private const float sweepAngle = 90;
        private const float halfSweepAngle = sweepAngle / 2;
        private const float bottomArcAngle = 90 - halfSweepAngle;
        private const float topArcAngle = 270 - halfSweepAngle;
        private const float leftArcAngle = 180 - halfSweepAngle;
        private const float rightArcAngle = 0 - halfSweepAngle;

        private const int pointAlpha = 0x40;

        public static readonly Color deepGray = Color.FromArgb(0xff, 48, 48, 48);
        public static readonly Color darkGray = Color.FromArgb(0xff, 64, 64, 64);
        public static readonly Color gray = Color.FromArgb(0xff, 96, 96, 96);
        public static readonly Color lightGray = Color.FromArgb(0xff, 128, 128, 128);
        public static readonly Color extremeGray = Color.FromArgb(0xff, 168, 168, 168);

        public static readonly Color BassExciterFillColor = Color.FromArgb(191, 54, 12);
        public static readonly Color ExciterLowerBoundaryColor = Color.FromArgb(244, 67, 54);
        public static readonly Color ExciterUpperBoundaryColor = Color.FromArgb(179, 136, 255);
        public static readonly Color Exciter1FillColor = Color.FromArgb(139, 195, 74);
        public static readonly Color Exciter2FillColor = Color.FromArgb(64, 196, 255);

        public static readonly Color[] colorListEq5 =
        {
            Color.FromArgb(255, 82, 82),
            Color.FromArgb(255, 171, 64),
            Color.FromArgb(178, 255, 89),
            Color.FromArgb(24, 255, 255),
            Color.FromArgb(224, 64, 251),
        };

        public static readonly Color[] colorListEq7 =
        {
            Color.FromArgb(255, 64, 129),
            Color.FromArgb(255, 82, 82),
            Color.FromArgb(255, 171, 64),
            Color.FromArgb(178, 255, 89),
            Color.FromArgb(24, 255, 255),
            Color.FromArgb(83, 109, 254),
            Color.FromArgb(224, 64, 251),
        };

        public static readonly Color[] colorListEq15 = 
        {
            Color.FromArgb(255, 64, 129),
            Color.FromArgb(255, 82, 82),
            Color.FromArgb(255, 110, 64),
            Color.FromArgb(255, 171, 64),
            Color.FromArgb(255, 215, 64),

            Color.FromArgb(255, 255, 0),
            Color.FromArgb(238, 255, 65),
            Color.FromArgb(178, 255, 89),
            Color.FromArgb(105, 240, 174),
            Color.FromArgb(100, 255, 218),

            Color.FromArgb(24, 255, 255),
            Color.FromArgb(64, 196, 255),
            Color.FromArgb(224, 64, 251),
            Color.FromArgb(83, 109, 254),
            Color.FromArgb(124, 77, 255),
            
        };

        public static readonly Dictionary<int, Color[]> eqBandCountToColorListMap = new Dictionary<int, Color[]>()
        {
            {colorListEq5.Length, colorListEq5},
            {colorListEq7.Length, colorListEq7},
            {colorListEq15.Length, colorListEq15},
        };

        public static readonly Pen deepGrayPen = new Pen(deepGray, 1.0f);
        public static readonly Pen darkGrayPen = new Pen(darkGray, 1.0f);
        public static readonly Pen grayPen = new Pen(gray, 1.0f);
        public static readonly Pen lightGrayPen = new Pen(lightGray, 1.0f);
        public static readonly Pen extremeGrayPen = new Pen(extremeGray, 1.0f);

        public static readonly Pen effectCurvePen = new Pen(Color.FromArgb(244, 179, 179), 1.6f);
        //public static readonly Pen thumbnailEffectCurvePen = new Pen(Color.FromArgb(244, 179, 179), 1.6f);

        public static readonly Pen outerPen = new Pen(Color.White, 2);

        public static readonly Brush whiteBrush = new SolidBrush(Color.White);

        public static readonly Brush eqPointBrush = new SolidBrush(Color.White);
        public static readonly Brush normalEqPointBrush = new SolidBrush(Color.FromArgb(0xD0, 192, 192, 192));
        public static readonly Brush activeEqPointBrush = new SolidBrush(Color.FromArgb(0xC0, 3, 168, 243));

        public static readonly Brush activeLabelBrush = new SolidBrush(Color.Yellow);
        //public static readonly Font activeLabelFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
        //public static readonly Font labelFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);

        public static readonly SolidBrush centerbrush = new SolidBrush(Color.White);

        public static readonly Font labelFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);

        public static readonly float normalEQRadius = 10f;
        public static readonly float activeEQRadius = 10f;

        public static readonly float normalFilterRadius = 11f;
        public static readonly float activeFilterRadius = 11f;

        public static readonly float activeExpandRadius = 1.5f;
        public static readonly int activeMaxRadius = 16;

        //public static readonly float lableXOffset = 0;
        public static readonly float lableYOffset = 1;
        public static readonly float activeLableYOffset = 12;

        public static readonly string hpfLabel = "HP";
        public static readonly string lpfLabel = "LP";

        public const int inactiveCurveColorAlpha = 0x80;

        public static readonly StringFormat pointLabelSf = new StringFormat();
        public static readonly StringFormat activeLabelBelowSf = new StringFormat();
        public static readonly StringFormat activeLabelAboveSf = new StringFormat();

        //private Bitmap cachedBackground = null;
        public static readonly Color hpfColor = Color.FromArgb(0xF8, 0xBB, 0xD0);
        public static readonly Color lpfColor = Color.FromArgb(0xB3, 0x88, 0xFF);

        private EQGraphicPoint[] _allPointList;
        private EQPoint[] _eqPointList = null;
        private HPFPoint _hpfPoint = null;
        private LPFPoint _lpfPoint = null;
        private PointF[] effectCurve;
        private PointF[] effectThumbnailCurve;

        private readonly Color[] colorList = null;

        static EQGraphic()
        {
            pointLabelSf.LineAlignment = StringAlignment.Center;
            pointLabelSf.Alignment = StringAlignment.Center;

            activeLabelBelowSf.LineAlignment = StringAlignment.Near;
            activeLabelBelowSf.Alignment = StringAlignment.Center;

            activeLabelAboveSf.LineAlignment = StringAlignment.Far;
            activeLabelAboveSf.Alignment = StringAlignment.Center;

            if(AEC.Tools.OSVersionHelper.isWinXP())
            {
                //lableXOffset = 0;
                lableYOffset = 0;
                labelFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            }
        }

        //public void Dispose()
        //{
        //    if(cachedBackground != null)
        //    {
        //        cachedBackground.Dispose();
        //        cachedBackground = null;
        //    }
        //}
        
        private EQGraphicHelper eqGraphicHelper
        {
            get
            {
                return EQGraphicHelper.GetCurrInstance();
            }
        }

        public EQGraphicPoint[] allPointList
        {
            get
            {
                return _allPointList;
            }
        }

        public EQGraphicPoint[] eqPoints
        {
            get
            {
                return _eqPointList;
            }
        }

        public EQGraphicPoint hpfPoint
        {
            get
            {
                return _hpfPoint;
            }
        }

        public EQGraphicPoint lpfPoint
        {
            get
            {
                return _lpfPoint;
            }
        }

        public EQGraphic(ModuleBase module_)
        {
            System.Diagnostics.Debug.Assert(module_ != null);
            this.module = module_;

            if (this.module.eqGroup != null && eqBandCountToColorListMap.ContainsKey(this.module.eqGroup.eqBandCount))
            {
                this.colorList = eqBandCountToColorListMap[this.module.eqGroup.eqBandCount];
            }
            else
            {
                this.colorList = colorListEq15;
            }
        }

        public void InitNormal()
        {
            if(eqGraphicHelper == null)
            {
                return;
            }
            //AEC.Test.PrintTest.PointNow("EQGraphic Init()开始");
            List<EQGraphicPoint> allPoints = new List<EQGraphicPoint>();

            if(this.module.eqGroup != null)
            {
                if (this._eqPointList == null || this._eqPointList.Length != this.module.eqGroup.eqBandCount)
                {
                    _eqPointList = new EQPoint[this.module.eqGroup.eqBandCount];
                }
                for (int i = 0; i < this.module.eqGroup.eqBandCount; ++i)
                {
                    _eqPointList[i] = new EQPoint(this.module.eqGroup.eqList[i], eqGraphicHelper.GetEQLocation(this.module.eqGroup.eqList[i]));
                }
                allPoints.AddRange(_eqPointList);
            }
            else
            {
                this._eqPointList = null;
            }

            if(module.hpf != null)
            {
                _hpfPoint = new HPFPoint(this.module.hpf, eqGraphicHelper.GetFilterLocation(module.hpf));
                allPoints.Add(_hpfPoint);
            }

            if(module.lpf != null)
            {
                _lpfPoint = new LPFPoint(module.lpf, eqGraphicHelper.GetFilterLocation(module.lpf));
                allPoints.Add(_lpfPoint);
            }

            _allPointList = allPoints.ToArray();
            //AEC.Test.PrintTest.PointNow("EQGraphic Init() 1");

            if(module.hasFreqRespCurve)
            {
                InitFreqRespCurve();
            }
            //AEC.Test.PrintTest.PointNow("EQGraphic Init() 2");
        }

        public void InitSecondaryNormal()
        {
            if (eqGraphicHelper == null)
            {
                return;
            }

            if (module.hasFreqRespCurve)
            {
                InitFreqRespCurve();
            }
        }

        public void InitThumbnail()
        {
            InitFreqRespThumbnailCurve();
        }

        private static uint time = 0;

        public static void ResetTime()
        {
            time = 0;
        }

        public static void TimePassing()
        {
            time++;
        }

        private static float GetActiveRadius(float normalRadius)
        {
            return normalRadius + (1.0f + (float)Math.Sin((time - 2) * Math.PI / 4.0f)) * activeExpandRadius;
        }

        private void InitFreqRespCurve()
        {
            if (this.effectCurve == null || this.effectCurve.Length != FreqResp.keyFreqListLength)
            {
                this.effectCurve = new PointF[FreqResp.keyFreqListLength];
            }

            for (int i = 0; i < this.effectCurve.Length; ++i)
            {
                this.effectCurve[i] = new PointF(eqGraphicHelper.freqrespFreq2XTable[i], eqGraphicHelper.Gain2PanelY(module.freqResp.amps[i]));
            }
        }

        private void InitFreqRespThumbnailCurve()
        {
            if (this.effectThumbnailCurve == null)
            {
                effectThumbnailCurve = EQGraphicHelper.InitThumbnailCurve();
            }
            
            EQGraphicHelper.UpdateRespFreqThumbnailCurve(this.effectThumbnailCurve, module.freqResp.amps);
        }

        public void UpdateEQ(EQ eq)
        {
            Debug.Assert(_eqPointList != null && eq != null && eq.index < _eqPointList.Length);

            _eqPointList[eq.index].center = eqGraphicHelper.GetEQLocation(eq);
            UpdateFreqRespCurve();
        }

        public void UpdateXPF(XPFilterType xpFilterType)
        {
            if (xpFilterType == XPFilterType.HighPass)
            {
                if (_hpfPoint == null || !_hpfPoint.visible)
                {
                    return;
                }

                _hpfPoint.center = eqGraphicHelper.GetFilterLocation(module.hpf);
            }
            else
            {
                if (_lpfPoint == null || !_lpfPoint.visible)
                {
                    return;
                }

                _lpfPoint.center = eqGraphicHelper.GetFilterLocation(module.lpf);
            }
            UpdateFreqRespCurve();
        }

        public void UpdateEQGroup()
        {
            Debug.Assert(_eqPointList != null && this.module.eqGroup.eqBandCount == _eqPointList.Length);

            for (int i = 0; i < this.module.eqGroup.eqBandCount; ++i)
            {
                _eqPointList[i].center = eqGraphicHelper.GetEQLocation(this.module.eqGroup.eqList[i]);
            }
            UpdateFreqRespCurve();
        }

        //public void ResetCachedBackground()
        //{
        //    if (this.cachedBackground != null)
        //    {
        //        this.cachedBackground.Dispose();
        //        this.cachedBackground = null;
        //    }
        //}

        //public void UpdateEQGroupAndFilter()
        //{
        //    for (int i = 0; i < this.module.eqBandCount; ++i)
        //    {
        //        _eqPointList[i].center = eqGraphicHelper.GetEQLocation(this.module.eqGroup.eqList[i]);
        //    }
        //    if (module.hpf != null)
        //    {
        //        _hpfPoint.center = eqGraphicHelper.GetFilterLocation(module.hpf);
        //    }
        //    if (module.lpf != null)
        //    {
        //        _lpfPoint.center = eqGraphicHelper.GetFilterLocation(module.lpf);
        //    }
        //    UpdateRespFreqCurve();
        //}
        public void UpdateNormalRespFreqCurve()
        {
            if (this.effectCurve != null)
            {
                for (int i = 0; i < effectCurve.Length; ++i)
                {
                    effectCurve[i].Y = eqGraphicHelper.Gain2PanelY(module.freqResp.amps[i]);
                }
            }
        }

        public void UpdateThumbnailCurve()
        {
            if (this.effectThumbnailCurve != null)
            {
                EQGraphicHelper.UpdateRespFreqThumbnailCurve(this.effectThumbnailCurve, module.freqResp.amps);
            }
        }


        public void UpdateFreqRespCurve()
        {
            this.UpdateNormalRespFreqCurve();
            this.UpdateThumbnailCurve();
        }

        public void PaintNormal(Graphics graphics)
        {
            //AEC.Log.OutputLine("EQ Graphic Paint");
            if (eqGraphicHelper == null)
            {
                return;
            }
            
            //this.PaintBackground(graphics);
            eqGraphicHelper.PaintBackground(graphics);
            this.PaintForeground(graphics);
        }

        public void PaintNormalBackground(Graphics graphics)
        {
            if (eqGraphicHelper == null)
            {
                return;
            }

            eqGraphicHelper.PaintBackground(graphics);
        }

        public void PaintNormalFreqRespInactiveCurve(Graphics graphics)
        {
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            effectCurvePen.Color = Color.FromArgb(inactiveCurveColorAlpha, module.themeColor);
            graphics.DrawCurve(effectCurvePen, effectCurve, 0);
        }

        public void PaintSecondaryNormalFreqResp(Graphics graphics, bool isActive)
        {
            effectCurvePen.Color = isActive ? module.themeColor : Color.FromArgb(inactiveCurveColorAlpha, module.themeColor);
            graphics.DrawCurve(effectCurvePen, effectCurve, 0);
        }

        public void PaintThumbnail(Graphics graphics)
        {
            if (eqGraphicHelper == null)
            {
                return;
            }

            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            effectCurvePen.Color = module.themeColor;

            graphics.SetClip(EQGraphicHelper.ThumbnailClipRect);
            graphics.DrawLines(effectCurvePen, effectThumbnailCurve);
            graphics.ResetClip();
        }

        //private void PaintBackground(Graphics graphics)
        //{
        //    Debug.Assert(eqGraphicHelper != null);

        //    //eqGraphicHelper.PaintBackground(graphics);

        //    //if (this.cachedBackground != null)
        //    //{
        //    //    AEC.UI.GDIHelper.QuickDrawImage(graphics, Point.Empty, this.cachedBackground);
        //    //    //graphics.DrawImage(this.background, new Point());
        //    //}
        //    //else
        //    //{
        //    //    this.cachedBackground = new Bitmap(eqGraphicHelper.clientSize.Width, eqGraphicHelper.clientSize.Height);
        //    //    Graphics gBitmap = Graphics.FromImage(this.cachedBackground);

        //    //    gBitmap.Clear(Color.Black);

        //    //    foreach (LineF line in eqGraphicHelper.hLine1s)
        //    //    {
        //    //        gBitmap.DrawLine(grayPen, line.st, line.ed);
        //    //    }
        //    //    foreach (LineF line in eqGraphicHelper.hLine2s)
        //    //    {
        //    //        gBitmap.DrawLine(darkGrayPen, line.st, line.ed);
        //    //    }
        //    //    foreach (LineF line in eqGraphicHelper.vLine1s)
        //    //    {
        //    //        gBitmap.DrawLine(grayPen, line.st, line.ed);
        //    //    }
        //    //    foreach (LineF line in eqGraphicHelper.vLine2s)
        //    //    {
        //    //        gBitmap.DrawLine(darkGrayPen, line.st, line.ed);
        //    //    }

        //    //    //gBitmap.FillRectangle(new SolidBrush(deepGray), eqGraphicHelper.eqRect);

        //    //    gBitmap.DrawLine(lightGrayPen, eqGraphicHelper.zerodBLine.st, eqGraphicHelper.zerodBLine.ed);
        //    //    gBitmap.DrawLines(lightGrayPen, eqGraphicHelper.axis);

        //    //    foreach (EQGraphicHelper.Label label in eqGraphicHelper.freqLabels)
        //    //    {
        //    //        label.Paint(gBitmap);
        //    //    }

        //    //    foreach (EQGraphicHelper.Label label in eqGraphicHelper.gainLabels)
        //    //    {
        //    //        label.Paint(gBitmap);
        //    //    }

        //    //    AEC.UI.GDIHelper.QuickDrawImage(graphics, Point.Empty, this.cachedBackground);
        //    //    //graphics.DrawImage(this.background, new Point());
        //    //}

        //    graphics.Clear(Color.Black);

        //    foreach (LineF line in eqGraphicHelper.hLine1s)
        //    {
        //        graphics.DrawLine(grayPen, line.st, line.ed);
        //    }
        //    foreach (LineF line in eqGraphicHelper.hLine2s)
        //    {
        //        graphics.DrawLine(darkGrayPen, line.st, line.ed);
        //    }
        //    foreach (LineF line in eqGraphicHelper.vLine1s)
        //    {
        //        graphics.DrawLine(grayPen, line.st, line.ed);
        //    }
        //    foreach (LineF line in eqGraphicHelper.vLine2s)
        //    {
        //        graphics.DrawLine(darkGrayPen, line.st, line.ed);
        //    }

        //    //graphics.FillRectangle(new SolidBrush(deepGray), eqGraphicHelper.eqRect);

        //    graphics.DrawLine(lightGrayPen, eqGraphicHelper.zerodBLine.st, eqGraphicHelper.zerodBLine.ed);
        //    graphics.DrawLines(lightGrayPen, eqGraphicHelper.axis);

        //    foreach (EQGraphicHelper.Label label in eqGraphicHelper.freqLabels)
        //    {
        //        label.Paint(graphics);
        //    }

        //    foreach (EQGraphicHelper.Label label in eqGraphicHelper.gainLabels)
        //    {
        //        label.Paint(graphics);
        //    }
        //}

        public void PaintForeground(Graphics graphics)
        {
            Debug.Assert(this.module != null && eqGraphicHelper != null);

            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (this.module.secondaryModule == null || this.module.secondaryModule.eqGraphic == null)
            {
                this.PaintModuleForegroundGraphic(graphics, true);
            }
            else
            {
                if (this.module.isSecondaryModuleActivated)
                {
                    this.PaintModuleForegroundGraphic(graphics, false);
                    this.module.secondaryModule.eqGraphic.PaintModuleForegroundGraphic(graphics, true);
                    //PaintSecondaryModuleGraphic(graphics, true);
                }
                else
                {
                    this.module.secondaryModule.eqGraphic.PaintModuleForegroundGraphic(graphics, false);
                    this.PaintModuleForegroundGraphic(graphics, true);
                }
            }
        }

        private void PaintModuleForegroundGraphic(Graphics graphics, ModuleBase module, bool isActive)
        {
            effectCurvePen.Color = isActive ? module.themeColor : Color.FromArgb(inactiveCurveColorAlpha, module.themeColor);
            graphics.DrawCurve(effectCurvePen, effectCurve, 0);

            if (isActive)
            {
                PaintAllPoint(graphics);

                if (module.eqGroup.isBypass)
                {
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    eqGraphicHelper.PaintEQPass(graphics);
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
                    graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
                }

                PaintActiveLabel(graphics);
            }
        }

        private void PaintModuleForegroundGraphic(Graphics graphics, bool isActive)
        {
            this.PaintModuleExciters(graphics);

            if (!this.module.hasFreqRespCurve)
            {
                return;
            }

            effectCurvePen.Color = isActive ? module.themeColor : Color.FromArgb(inactiveCurveColorAlpha, module.themeColor);
            graphics.DrawCurve(effectCurvePen, effectCurve, 0);

            if (isActive)
            {
                PaintAllPoint(graphics);

                PaintEQBypass(graphics);

                PaintActiveLabel(graphics);
            }
        }

        private void PaintModuleExciters(Graphics graphics)
        {
            if(module == null || !module.isShowExciter)
            {
                return;
            }

            if(module.getBassExciter != null)
            {
                float right = eqGraphicHelper.Freq2PanelX(module.getBassExciter.freqValue);
                float left = eqGraphicHelper.eqRect.Left;
                
                PointF topLeft = new PointF(left, eqGraphicHelper.eqRect.Top);
                PointF bottomLeft = new PointF(left, eqGraphicHelper.eqRect.Bottom);
                PointF topRight = new PointF(right, eqGraphicHelper.eqRect.Top);
                PointF bottomRight = new PointF(right, eqGraphicHelper.eqRect.Bottom);

                Color fillColor = Color.FromArgb(module.getBassExciter.on.value ? 0x40 : 0x20, BassExciterFillColor);

                if (module.getBassExciter.on.value)
                {
                    using (Brush fillBrush = new SolidBrush(fillColor))
                    {
                        graphics.FillRectangle(fillBrush, new RectangleF(topLeft, new SizeF(right - left, eqGraphicHelper.eqRect.Height)));
                    }
                }

                using (Pen rightPen = new Pen(Color.FromArgb(module.getBassExciter.on.value ? 0x80 : 0x40, ExciterUpperBoundaryColor), 2.0f))
                {
                    graphics.DrawLine(rightPen, topRight, bottomRight);
                }

                if(module.activeItem == ActiveItem.BassExciter)
                {
                    graphics.DrawString(module.getBassExciter.label, labelFont, activeLabelBrush, right, eqGraphicHelper.eqRect.Top + lableYOffset, activeLabelBelowSf);
                }
            }

            if (module.activeItem == ActiveItem.Exciter1)
            {
                if (module.getExciter2 != null)
                {
                    PaintExciter(graphics, module.getExciter2, Exciter2FillColor, module.activeItem == ActiveItem.Exciter2);
                }

                if (module.getExciter1 != null)
                {
                    PaintExciter(graphics, module.getExciter1, Exciter1FillColor, module.activeItem == ActiveItem.Exciter1);
                }
            }
            else
            {
                if (module.getExciter1 != null)
                {
                    PaintExciter(graphics, module.getExciter1, Exciter1FillColor, module.activeItem == ActiveItem.Exciter1);
                }

                if (module.getExciter2 != null)
                {
                    PaintExciter(graphics, module.getExciter2, Exciter2FillColor, module.activeItem == ActiveItem.Exciter2);
                }
            }
        }

        private void PaintExciter(Graphics graphics, Exciter exciter, Color color, bool isActive)
        {
            Debug.Assert(exciter != null);

            float x = eqGraphicHelper.Freq2PanelX(exciter.centerfreqValue);
            float left = eqGraphicHelper.Freq2PanelX(exciter.freqBandLowerLimit);
            float right = eqGraphicHelper.Freq2PanelX(exciter.freqBandUpperLimit);

            PointF topLeft = new PointF(left, eqGraphicHelper.eqRect.Top);
            PointF bottomLeft = new PointF(left, eqGraphicHelper.eqRect.Bottom);
            PointF topRight = new PointF(right, eqGraphicHelper.eqRect.Top);
            PointF bottomRight = new PointF(right, eqGraphicHelper.eqRect.Bottom);

            Color fillColor = Color.FromArgb(exciter.on.value ? 0x80 : 0x30, color);
            
            using (Brush fillBrush = new SolidBrush(fillColor))
            {
                graphics.FillRectangle(fillBrush, new RectangleF(topLeft, new SizeF(right - left, eqGraphicHelper.eqRect.Height)));
            }

            using (Pen leftPen = new Pen(Color.FromArgb(exciter.on.value ? 0x80 : 0x40, ExciterLowerBoundaryColor), 2.0f))
            {
                graphics.DrawLine(leftPen, topLeft, bottomLeft);
            }
            using (Pen rightPen = new Pen(Color.FromArgb(exciter.on.value ? 0x80 : 0x40, ExciterUpperBoundaryColor), 2.0f))
            {
                graphics.DrawLine(rightPen, topRight, bottomRight);
            }

            if(isActive)
            {
                graphics.DrawString(exciter.label, labelFont, activeLabelBrush, x, eqGraphicHelper.getZerodBLineY, pointLabelSf);
            }

            //if (module.activeItem == ActiveItem.BassExciter)
            //{
            //    graphics.DrawString(module.getBassExciter.freqLabel, labelFont, activeLabelBrush, x, eqGraphicHelper.eqRect.Top - lableYOffset, activeLabelBelowSf);
            //    top.Y += 18;
            //}

            //if (module.getBassExciter.on.value)
            //{
                
            //}
            //else
            //{
            //    using (Pen pen = new Pen(freqLineColor, 2.0f))
            //    {
            //        pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            //        graphics.DrawLine(pen, top, bottom);
            //    }
            //}
        }

        private void PaintEQBypass(Graphics graphics)
        {
            if (module.eqGroup != null && module.eqGroup.isBypass)
            {
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                eqGraphicHelper.PaintEQPass(graphics);
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            }
        }

        private void PaintAllPoint(Graphics graphics)
        {
            switch(module.activeItem)
            {
                case ActiveItem.EQ:
                case ActiveItem.BassExciter:
                case ActiveItem.Exciter1:
                case ActiveItem.Exciter2:
                    if(_hpfPoint != null && _hpfPoint.visible)
                    {
                        PaintFilterPoint(graphics, XPFilterType.HighPass);
                    }
                    if (_lpfPoint != null && _lpfPoint.visible)
                    {
                        PaintFilterPoint(graphics, XPFilterType.LowPass);
                    }

                    if (_eqPointList != null)
                    {
                        for (int i = 0; i < _eqPointList.Length; ++i)
                        {
                            if (i != module.eqGroup.currEQIndex)
                            {
                                PaintEQPoint(graphics, i);
                            }
                        }
                        PaintEQPoint(graphics, module.eqGroup.currEQIndex);
                    }
                    break;

                case ActiveItem.HPF:
                    if (_eqPointList != null)
                    {
                        for (int i = 0; i < _eqPointList.Length; ++i)
                        {
                            PaintEQPoint(graphics, i);
                        }
                    }
                    if (_lpfPoint != null && _lpfPoint.visible)
                    {
                        PaintFilterPoint(graphics, XPFilterType.LowPass);
                    }
                    if (_hpfPoint != null && _hpfPoint.visible)
                    {
                        PaintFilterPoint(graphics, XPFilterType.HighPass);
                    }
                    break;

                case ActiveItem.LPF:
                    if (_eqPointList != null)
                    {
                        for (int i = 0; i < _eqPointList.Length; ++i)
                        {
                            PaintEQPoint(graphics, i);
                        }
                    }
                    if (_hpfPoint != null && _hpfPoint.visible)
                    {
                        PaintFilterPoint(graphics, XPFilterType.HighPass);
                    }
                    if (_lpfPoint != null && _lpfPoint.visible)
                    {
                        PaintFilterPoint(graphics, XPFilterType.LowPass);
                    }
                    break;
            }
        }

        private void PaintEQPoint(Graphics graphics, int index)
        {
            bool isActive = module.activeItem == ActiveItem.EQ && index == module.eqGroup.currEQIndex;
            PointF center = _eqPointList[index].center;
            float outerRadius = isActive ? GetActiveRadius(normalEQRadius) : normalEQRadius;
            float centerRadius = outerRadius - 2;
            Color color = this.colorList[index % this.colorList.Length];
            RectangleF outerRect = new RectangleF(center.X - outerRadius, center.Y - outerRadius, 2 * outerRadius, 2 * outerRadius);

            outerPen.Color = Color.FromArgb(pointAlpha, color);
            graphics.DrawEllipse(outerPen, outerRect);
            outerPen.Color = color;
            graphics.DrawArc(outerPen, outerRect, bottomArcAngle, sweepAngle);
            graphics.DrawArc(outerPen, outerRect, topArcAngle, sweepAngle);

            RectangleF centerRect = new RectangleF(center.X - centerRadius, center.Y - centerRadius, 2 * centerRadius, 2 * centerRadius);
            centerbrush.Color = Color.FromArgb(pointAlpha * 2, Color.Black);
            graphics.FillEllipse(centerbrush, centerRect);
            if (!isActive)
            {
                centerbrush.Color = Color.FromArgb(pointAlpha * 3 / 4, color);
                graphics.FillEllipse(centerbrush, centerRect);
            }

            //centerbrush.Color = isActive ? color : Color.White;
            centerbrush.Color = color;
            graphics.DrawString((index + 1).ToString(), labelFont, centerbrush, center.X, center.Y + lableYOffset, pointLabelSf);
            //bool isActive = module.activeItem == ActiveItem.EQ && index == module.eqGroup.currEQIndex;
            //EQGraphicPoint point = _eqPointList[index];

            //Debug.Assert(point != null);

            //if(isActive)
            //{
            //    graphics.FillEllipse(activeEqPointBrush, point.center.X - activeEQRadius, point.center.Y - activeEQRadius, activeEQRadius * 2, activeEQRadius * 2);
            //    //graphics.DrawString((index + 1).ToString(), labelFont, whiteBrush, point.center.X, point.center.Y + 1, pointLabelSf);                  
            //}
            //else
            //{
            //    graphics.FillEllipse(normalEqPointBrush, point.center.X - normalEQRadius, point.center.Y - normalEQRadius, normalEQRadius * 2, normalEQRadius * 2);
            //}
            //graphics.DrawString((index + 1).ToString(), labelFont, whiteBrush, point.center.X, point.center.Y + lableYOffset, pointLabelSf);  
        }

        private void PaintFilterPoint(Graphics graphics, XPFilterType filterType)
        {
            //bool isActive = false;
            //if(filterType == XPFilterType.HighPass)
            //{
            //    isActive = module.activeItem == ActiveItem.HPF;
            //}
            //else if(filterType == XPFilterType.LowPass)
            //{
            //    isActive = module.activeItem == ActiveItem.LPF;
            //}

            //PointF center = filterType == XPFilterType.HighPass ? _hpfPoint.center : _lpfPoint.center;
            //String label = filterType == XPFilterType.HighPass ? hpfLabel : lpfLabel;
            //if(isActive)
            //{
            //    graphics.FillEllipse(activeEqPointBrush, center.X - activeFilterRadius, center.Y - activeFilterRadius, activeFilterRadius * 2, activeFilterRadius * 2);
            //    //graphics.DrawString(label, labelFont, whiteBrush, center.X + 1, center.Y + lableYOffset, pointLabelSf);
            //}
            //else
            //{
            //    graphics.FillEllipse(normalEqPointBrush, center.X - normalFilterRadius, center.Y - normalFilterRadius, normalFilterRadius * 2, normalFilterRadius * 2);
            //}
            //graphics.DrawString(label, labelFont, whiteBrush, center.X, center.Y + lableYOffset, pointLabelSf);
            bool isActive = false;
            if (filterType == XPFilterType.HighPass)
            {
                isActive = module.activeItem == ActiveItem.HPF;
            }
            else if (filterType == XPFilterType.LowPass)
            {
                isActive = module.activeItem == ActiveItem.LPF;
            }

            PointF center = filterType == XPFilterType.HighPass ? _hpfPoint.center : _lpfPoint.center;
            String label = filterType == XPFilterType.HighPass ? hpfLabel : lpfLabel;
            float outerRadius = isActive ? GetActiveRadius(normalFilterRadius) : normalFilterRadius;
            float centerRadius = outerRadius - 2;
            Color color = filterType == XPFilterType.HighPass ? hpfColor : lpfColor;
            RectangleF outerRect = new RectangleF(center.X - outerRadius, center.Y - outerRadius, 2 * outerRadius, 2 * outerRadius);

            outerPen.Color = Color.FromArgb(pointAlpha, color);
            graphics.DrawEllipse(outerPen, outerRect);
            outerPen.Color = color;
            graphics.DrawArc(outerPen, outerRect, leftArcAngle, sweepAngle);
            graphics.DrawArc(outerPen, outerRect, rightArcAngle, sweepAngle);

            RectangleF centerRect = new RectangleF(center.X - centerRadius, center.Y - centerRadius, 2 * centerRadius, 2 * centerRadius);
            centerbrush.Color = Color.FromArgb(pointAlpha * 2, Color.Black);
            graphics.FillEllipse(centerbrush, centerRect);
            if (!isActive)
            {
                centerbrush.Color = Color.FromArgb(pointAlpha, color);
                graphics.FillEllipse(centerbrush, centerRect);
            }

            //centerbrush.Color = isActive ? color : Color.White;
            centerbrush.Color = color;
            graphics.DrawString(label, labelFont, centerbrush, center.X, center.Y + lableYOffset, pointLabelSf);
        }

        //private void PaintEQPoint(Graphics graphics)
        //{
        //    for (int i = 0; i < _eqPointList.Length; ++i)
        //    {
        //        EQGraphicPoint point = _eqPointList[i];
        //        if(i != module.currEQIndex && module.activeItem == ActiveItem.EQ)
        //        {
        //            graphics.FillEllipse(normalEqPointBrush, point.center.X - normalEQRadius, point.center.Y - normalEQRadius, normalEQRadius * 2, normalEQRadius * 2);
        //            //graphics.DrawString((i + 1).ToString(), labelFont, whiteBrush, point.center.X, point.center.Y + lableYOffset, pointLabelSf);                    
        //        }
        //        else
        //        {
        //            graphics.FillEllipse(activeEqPointBrush, point.center.X - activeEQRadius, point.center.Y - activeEQRadius, activeEQRadius * 2, activeEQRadius * 2);
        //        }
        //        graphics.DrawString((i + 1).ToString(), labelFont, whiteBrush, point.center.X, point.center.Y + lableYOffset, pointLabelSf);
        //    }
        //}

        //private void PaintFilterPoint(Graphics graphics)
        //{
        //    if(_hpfPoint != null)
        //    {
        //        if(module.activeItem != ActiveItem.HPF)
        //        {
        //            graphics.FillEllipse(normalEqPointBrush, _hpfPoint.center.X - normalEQRadius, _hpfPoint.center.Y - normalEQRadius, normalEQRadius * 2, normalEQRadius * 2);
        //            graphics.DrawString(hpfLabel, labelFont, whiteBrush, _hpfPoint.center.X, _hpfPoint.center.Y + 1, pointLabelSf);
        //        }
        //        else
        //        {
        //            graphics.FillEllipse(activeEqPointBrush, _hpfPoint.center.X - activeEQRadius, _hpfPoint.center.Y - activeEQRadius, activeEQRadius * 2, activeEQRadius * 2);
        //            graphics.DrawString(hpfLabel, labelFont, whiteBrush, _hpfPoint.center.X, _hpfPoint.center.Y + 1, pointLabelSf);
        //        }
        //    }
        //    else if(_lpfPoint != null)
        //    {
        //        if (module.activeItem != ActiveItem.LPF)
        //        {
        //            graphics.FillEllipse(normalEqPointBrush, _lpfPoint.center.X - normalEQRadius, _lpfPoint.center.Y - normalEQRadius, normalEQRadius * 2, normalEQRadius * 2);
        //            graphics.DrawString(hpfLabel, labelFont, whiteBrush, _lpfPoint.center.X, _lpfPoint.center.Y + 1, pointLabelSf);
        //        }
        //        else
        //        {
        //            graphics.FillEllipse(activeEqPointBrush, _lpfPoint.center.X - activeEQRadius, _lpfPoint.center.Y - activeEQRadius, activeEQRadius * 2, activeEQRadius * 2);
        //            graphics.DrawString(hpfLabel, labelFont, whiteBrush, _lpfPoint.center.X, _lpfPoint.center.Y + 1, pointLabelSf);
        //        }
        //    }
        //}

        private void PaintActiveLabel(Graphics graphics)
        {
            switch (module.activeItem)
            {
                case ActiveItem.EQ:
                    this.PaintEQLabel(graphics);
                    break;

                case ActiveItem.HPF:
                    this.PaintHPFLabel(graphics);
                    break;

                case ActiveItem.LPF:
                    this.PaintLPFLabel(graphics);
                    break;
            }
        }

        private void PaintEQLabel(Graphics graphics)
        {
            Debug.Assert(module.eqGroup != null && this._eqPointList != null);

            graphics.DrawString(
                    module.eqGroup.currEQ.label,
                    labelFont,
                    activeLabelBrush,
                    GetActiveEQLabelLocation(this._eqPointList, module.eqGroup.currEQ),
                    GetActiveEQLabelSf(module.eqGroup.currEQ));
        }

        private void PaintHPFLabel(Graphics graphics)
        {
            System.Diagnostics.Debug.Assert(_hpfPoint != null);
            PointF location = new PointF(this._hpfPoint.center.X, this._hpfPoint.center.Y - activeLableYOffset);
            graphics.DrawString(
                    module.hpf.label,
                    labelFont,
                    activeLabelBrush,
                    location.X,
                    location.Y,
                    activeLabelAboveSf);
        }

        private void PaintLPFLabel(Graphics graphics)
        {
            System.Diagnostics.Debug.Assert(_lpfPoint != null);
            PointF location = new PointF(this._lpfPoint.center.X, this._lpfPoint.center.Y - activeLableYOffset);
            //location.Y += activeLableYOffset;
            graphics.DrawString(
                    module.lpf.label,
                    labelFont,
                    activeLabelBrush,
                    location,
                    activeLabelAboveSf);
        }

        public static PointF GetActiveEQLabelLocation(EQPoint[] eqPoints, EQ activeEQ)
        {
            PointF center = eqPoints[activeEQ.index].center;
            if(activeEQ.gainIndex < EQ.defGainIndex)
            {
                return new PointF(center.X, center.Y - activeLableYOffset);
            }
            else
            {
                 return new PointF(center.X, center.Y + activeLableYOffset);
            }
        }

        public static StringFormat GetActiveEQLabelSf(EQ activeEQ)
        {
            if (activeEQ.gainIndex < EQ.defGainIndex)
            {
                return activeLabelAboveSf;
            }
            else
            {
                return activeLabelBelowSf;
            }
        }

        public static bool CheckPointIsSelected(PointF center, Point cursor)
        {
            return EQGraphic.CalcDistance(center, cursor) <= EQGraphic.activeEQRadius;
        }

        public static float CalcDistance(PointF pt1, Point pt2)
        {
            PointF offset = new PointF(pt1.X - pt2.X, pt1.Y - pt2.Y);
            return (float)Math.Sqrt(offset.X * offset.X + offset.Y * offset.Y);
        }
    }
}
