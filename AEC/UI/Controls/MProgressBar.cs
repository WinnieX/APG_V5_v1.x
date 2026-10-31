using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AEC.UI.Utility;

namespace AEC.UI.Controls
{
    public class MProgressBar : MControl
    {
        public bool isShowProgress { get; set; }

        private float _progressRatio = 0;
        private Color _startColor = Color.FromArgb(21, 101, 192);
        private Color _endColor = Color.FromArgb(100, 181, 246);

        public Color startColor
        {
            get
            {
                return _startColor;
            }
            set
            {
                this._startColor = value;
            }
        }

        public Color endColor
        {
            get
            {
                return _endColor;
            }
            set
            {
                this._endColor = value;
            }
        }

        public float progressRatio
        {
            get
            {
                return this._progressRatio;
            }
            set
            {
                value = AEC.Core.Utils.ConstrainValue(value, 0.0f, 1.0f);
                this._progressRatio = value;
                this.Invalidate();
                this.Update();
            }
        }

        public MProgressBar()
        {
            this.progressRatio = 0;
        }

        protected override void PaintForeground(Graphics g)
        {
            if (this.Visible)
            {
                this.PaintProgress(g);
                this.PaintProgressText(g);
            }
        }

        private void PaintProgress(Graphics g)
        {
            if (this._progressRatio == 0)
            {
                return;
            }

            using (LinearGradientBrush lgBrush = new LinearGradientBrush(this.ClientRectangle, startColor, endColor, LinearGradientMode.Horizontal))
            {
                lgBrush.SetSigmaBellShape(0.5f);

                ColorBlend cb = new ColorBlend()
                {
                    Colors = new Color[]
                    {
                        startColor,
                        endColor,
                    },
                    Positions = new float[]
                    {
                        0,
                        1.0f,
                    },
                };

                lgBrush.InterpolationColors = cb;

                int progressWidth = (int)(_progressRatio * this.Width);
                Rectangle rect = new Rectangle(Point.Empty, new Size(progressWidth, this.Height));

                g.SetClip(rect);

                UI.Utility.GDIHelper.SetHighQuality(g);

                using(GraphicsPath roundedRect = RoundRectangle.CreateRoundedRectPath(this, this._radius))
                {
                    g.FillPath(lgBrush, roundedRect);
                }

                UI.Utility.GDIHelper.SetDefaultQuality(g);

                g.ResetClip();
            }
        }

        private void PaintProgressText(Graphics g)
        {
            if (isShowProgress)
            {
                string progressRatioText = string.Format("{0}%", (this._progressRatio * 100).ToString("F1"));
                if (!string.IsNullOrEmpty(progressRatioText))
                {
                    TextRenderer.DrawText(g, progressRatioText, this.Font, this.ClientRectangle, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
            }
        }
    }
}
