using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using AEC.UI.Utility;

namespace AEC.UI.Controls
{
    public class MButton : MButtonBase
    {
        protected Color _normalBorderColor = Color.FromArgb(168, 168, 168);
        protected Color _normalFillColor = Color.FromArgb(32, 32, 32);
        protected Color _normalTextColor = Color.FromArgb(224, 224, 224);

        protected Color _hoverBorderColor = Color.FromArgb(192, 192, 192);
        protected Color _hoverFillColor = Color.FromArgb(0, 145, 234);
        protected Color _hoverTextColor = Color.FromArgb(255, 255, 255);

        protected Color _pressedBorderColor = Color.FromArgb(96, 96, 96);
        protected Color _pressedFillColor = Color.FromArgb(0, 96, 168);
        protected Color _pressedTextColor = Color.FromArgb(168, 168, 168);

        protected Color _disabledBorderColor = Color.FromArgb(80, 80, 80);
        protected Color _disabledFillColor = Color.FromArgb(48, 48, 48);
        protected Color _disabledTextColor = Color.FromArgb(128, 128, 128);


        //private Color _normalBorderColor = Color.FromArgb(168, 168, 168);
        //private Color _normalFillColor = Color.FromArgb(32, 32, 32);
        //private Color _normalTextColor = Color.FromArgb(224, 224, 224);

        //private Color _hoverBorderColor = Color.FromArgb(192, 192, 192);
        //private Color _hoverFillColor = Color.FromArgb(64, 64, 64);
        //private Color _hoverTextColor = Color.FromArgb(0, 176, 255);

        //private Color _pressedBorderColor = Color.FromArgb(96, 96, 96);
        //private Color _pressedFillColor = Color.FromArgb(16, 16, 16);
        //private Color _pressedTextColor = Color.FromArgb(2, 136, 209);

        //private Color _disbaledBorderColor = Color.FromArgb(80, 80, 80);
        //private Color _disbaledFillColor = Color.FromArgb(48, 48, 48);
        //private Color _disbaledTextColor = Color.FromArgb(128, 128, 128);

        public Color NormalBorderColor
        {
            get { return _normalBorderColor; }
            set { _normalBorderColor = value; }
        }

        public Color NormalFillColor
        {
            get { return _normalFillColor; }
            set { _normalFillColor = value; }
        }

        public Color NormalTextColor
        {
            get { return _normalTextColor; }
            set { _normalTextColor = value; }
        }

        public Color HoverBorderColor
        {
            get { return _hoverBorderColor; }
            set { _hoverBorderColor = value; }
        }

        public Color HoverFillColor
        {
            get { return _hoverFillColor; }
            set { _hoverFillColor = value; }
        }

        public Color HoverTextColor
        {
            get { return _hoverTextColor; }
            set { _hoverTextColor = value; }
        }

        public Color PressedBorderColor
        {
            get { return _pressedBorderColor; }
            set { _pressedBorderColor = value; }
        }

        public Color PressedFillColor 
        { 
            get{return _pressedFillColor;}
            set{ _pressedFillColor = value;}
        }

        public Color PressedTextColor
        {
            get { return _pressedTextColor; }
            set { _pressedTextColor = value; }
        }

        public Color DisabledBorderColor
        {
            get { return _disabledBorderColor; }
            set { _disabledBorderColor = value; }
        }

        public Color DisabledFillColor
        {
            get { return _disabledFillColor; }
            set { _disabledFillColor = value; }
        }

        public Color DisabledTextColor
        {
            get { return _disabledTextColor; }
            set { _disabledTextColor = value; }
        }

        public MButton()
        {

        }

        public override Color BorderColor
        {
            get
            {
                if(!this.Enabled)
                {
                    return _disabledBorderColor;
                }

                switch(base.state)
                {
                    case MButtonState.Normal:
                        return _normalBorderColor;

                    case MButtonState.Hover:
                        return _hoverBorderColor;

                    case MButtonState.Pressed:
                        return _pressedBorderColor;
                }

                return _normalBorderColor;
            }
        }

        public override Color FillColor
        {
            get
            {
                if (!this.Enabled)
                {
                    return _disabledFillColor;
                }

                switch (base.state)
                {
                    case MButtonState.Normal:
                        return _normalFillColor;

                    case MButtonState.Hover:
                        return _hoverFillColor;

                    case MButtonState.Pressed:
                        return _pressedFillColor;
                }

                return _normalFillColor;
            }
        }

        public override Color TextColor
        {
            get
            {
                if (!this.Enabled)
                {
                    return _disabledTextColor;
                }

                switch (base.state)
                {
                    case MButtonState.Normal:
                        return _normalTextColor;

                    case MButtonState.Hover:
                        return _hoverTextColor;

                    case MButtonState.Pressed:
                        return _pressedTextColor;
                }

                return _normalTextColor;
            }
        }
    }
}
