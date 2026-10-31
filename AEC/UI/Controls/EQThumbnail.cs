using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using AEC.Core;
using AEC.Common;
using AEC.UI.Utility;

namespace AEC.UI.Controls
{
    public partial class EQThumbnail : MButton
    {
        private const string eq = "EQ";

        private static readonly Size DefSize = new Size(86, 42);
        
        private ModuleBase module;

        private Color normalBackColor1 = Color.FromArgb(48, 48, 48);
        private Color normalBackColor2 = Color.FromArgb(64, 64, 64);

        private Color hoverBackColor1 = Color.FromArgb(72, 72, 72);
        private Color hoverBackColor2 = Color.FromArgb(88, 88, 88);

        private Color pressedBackColor1 = Color.FromArgb(36, 36, 36);
        private Color pressedBackColor2 = Color.FromArgb(56, 56, 56);

        private float zerodBPosition = 0.35f;

        public bool isShowBackEQLabel { get; set; }

        public EQThumbnail()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            this.Size = DefSize;
            this.NormalBorderColor = Color.FromArgb(168, 168, 168);
            this.HoverBorderColor = Color.FromArgb(192, 192, 192);
            this.PressedBorderColor = Color.FromArgb(128, 128, 128);
        }

        public void BindingModule(ModuleBase module_)
        {
            this.module = module_;
            this.module.InitEQThumbnailGraphic();
        }

        protected override void PaintBackground(Graphics g)
        {
            GraphicsPath roundedRect = RoundRectangle.CreateRoundedRectPath(this, this._radius);

            //switch()
            Color color1 = normalBackColor1;
            Color color2 = normalBackColor2;
            switch (this.state)
            {
                case MButtonState.Hover:
                    color1 = hoverBackColor1;
                    color2 = hoverBackColor2;
                    break;

                case MButtonState.Pressed:
                    color1 = pressedBackColor1;
                    color2 = pressedBackColor2;
                    break;
            }

            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, color1, color2, LinearGradientMode.Vertical))
            {
                brush.SetSigmaBellShape(0.5f);

                ColorBlend cb = new ColorBlend(3)
                {
                    Colors = new Color[]
	                {
		                color1,
		                color2,
		                color1,
	                },
                    Positions = new float[]
                    {
                        0, 
                        zerodBPosition, 
                        1.0f,
                    },
                };

                brush.InterpolationColors = cb;

                //g.SetClip(roundedRect);

                g.FillPath(brush, roundedRect);

                //g.ResetClip();
            }

            GDIHelper.DrawPath(g, this.BorderColor, roundedRect, _borderWidth);

            if (isShowBackEQLabel)
            {
                //GDIHelper.SetHighQuality(g);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                using(Brush eqBrush = new SolidBrush(Color.FromArgb(0x40, this.ForeColor)))
                {
                    Rectangle textRect = this.ClientRectangle;
                    textRect.Y -= 2;
                    g.DrawString(eq, this.Font, eqBrush, textRect, new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
                //GDIHelper.SetHighQuality(g);
                //TextRenderer.DrawText(g, eq, this.Font, this.ClientRectangle, Color.FromArgb(0x20, this.ForeColor), TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
            }

            roundedRect.Dispose();
        }

        protected override void PaintForeground(Graphics g)
        {
            if (this.module != null && this.module.eqGraphic != null)
            {
                this.module.eqGraphic.PaintThumbnail(g);
            }
        }
    }
}
