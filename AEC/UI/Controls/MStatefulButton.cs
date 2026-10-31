using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using AEC.UI.Utility;

namespace AEC.UI.Controls
{
    [ToolboxItem(true), DefaultEvent("CheckedChanged")]
    public class MStatefulButton : MButtonBase
    {
        protected bool _isChecked = false;

        public event EventHandler CheckedChanged;

        protected bool _isAutoCheck = true;

        public bool isAutoCheck
        {
            get
            {
                return _isAutoCheck;
            }
            set
            {
                _isAutoCheck = value;
            }
        }

        #region 未选中状态
        private Color _uncheckedNormalBorderColor = Color.FromArgb(168, 168, 168);
        public Color UncheckedNormalBorderColor
        {
            get { return _uncheckedNormalBorderColor; }
            set { _uncheckedNormalBorderColor = value; }
        }

        private Color _uncheckedNormalFillColor = Color.FromArgb(32, 32, 32);
        public Color UncheckedNormalFillColor
        {
            get { return _uncheckedNormalFillColor; }
            set { _uncheckedNormalFillColor = value; }
        }

        private Color _uncheckedNormalTextColor = Color.FromArgb(224, 224, 224);
        public Color UncheckedNormalTextColor
        {
            get { return _uncheckedNormalTextColor; }
            set { _uncheckedNormalTextColor = value; }
        }

        private Color _uncheckedHoverBorderColor = Color.FromArgb(192, 192, 192);
        public Color UncheckedHoverBorderColor
        {
            get { return _uncheckedHoverBorderColor; }
            set { _uncheckedHoverBorderColor = value; }
        }

        private Color _uncheckedHoverFillColor = Color.FromArgb(64, 64, 64);
        public Color UncheckedHoverFillColor
        {
            get { return _uncheckedHoverFillColor; }
            set { _uncheckedHoverFillColor = value; }
        }

        private Color _uncheckedHoverTextColor = Color.FromArgb(255, 255, 255);
        public Color UncheckedHoverTextColor
        {
            get { return _uncheckedHoverTextColor; }
            set { _uncheckedHoverTextColor = value; }
        }

        private Color _uncheckedPressedBorderColor = Color.FromArgb(96, 96, 96);
        public Color UncheckedPressedBorderColor
        {
            get { return _uncheckedPressedBorderColor; }
            set { _uncheckedPressedBorderColor = value; }
        }

        private Color _uncheckedPressedFillColor = Color.FromArgb(16, 16, 16);
        public Color UncheckedPressedFillColor
        {
            get { return _uncheckedPressedFillColor; }
            set { _uncheckedPressedFillColor = value; }
        }

        private Color _uncheckedPressedTextColor = Color.FromArgb(168, 168, 168);
        public Color UncheckedPressedTextColor
        {
            get { return _uncheckedPressedTextColor; }
            set { _uncheckedPressedTextColor = value; }
        }

        private Color _uncheckedDisbaledBorderColor = Color.FromArgb(80, 80, 80);
        public Color UncheckedDisbaledBorderColor
        {
            get { return _uncheckedDisbaledBorderColor; }
            set { _uncheckedDisbaledBorderColor = value; }
        }

        private Color _uncheckedDisbaledFillColor = Color.FromArgb(48, 48, 48);
        public Color UncheckedDisbaledFillColor
        {
            get { return _uncheckedDisbaledFillColor; }
            set { _uncheckedDisbaledFillColor = value; }
        }

        private Color _uncheckedDisbaledTextColor = Color.FromArgb(128, 128, 128);
        public Color UncheckedDisbaledTextColor
        {
            get { return _uncheckedDisbaledTextColor; }
            set { _uncheckedDisbaledTextColor = value; }
        }
        #endregion

        #region 选中状态
        private Color _checkedNormalBorderColor = Color.FromArgb(168, 168, 168);
        public Color CheckedNormalBorderColor
        {
            get { return _checkedNormalBorderColor; }
            set { _checkedNormalBorderColor = value; }
        }

        private Color _checkedNormalFillColor = Color.FromArgb(2, 119, 189);
        public Color CheckedNormalFillColor
        {
            get { return _checkedNormalFillColor; }
            set { _checkedNormalFillColor = value; }
        }

        private Color _checkedNormalTextColor = Color.FromArgb(224, 224, 224);
        public Color CheckedNormalTextColor
        {
            get { return _checkedNormalTextColor; }
            set { _checkedNormalTextColor = value; }
        }

        private Color _checkedHoverBorderColor = Color.FromArgb(192, 192, 192);
        public Color CheckedHoverBorderColor
        {
            get { return _checkedHoverBorderColor; }
            set { _checkedHoverBorderColor = value; }
        }

        private Color _checkedHoverFillColor = Color.FromArgb(0, 145, 234);
        public Color CheckedHoverFillColor
        {
            get { return _checkedHoverFillColor; }
            set { _checkedHoverFillColor = value; }
        }

        private Color _checkedHoverTextColor = Color.FromArgb(255, 255, 255);
        public Color CheckedHoverTextColor
        {
            get { return _checkedHoverTextColor; }
            set { _checkedHoverTextColor = value; }
        }

        private Color _checkedPressedBorderColor = Color.FromArgb(96, 96, 96);
        public Color CheckedPressedBorderColor
        {
            get { return _checkedPressedBorderColor; }
            set { _checkedPressedBorderColor = value; }
        }

        private Color _checkedPressedFillColor = Color.FromArgb(0, 96, 168);
        public Color CheckedPressedFillColor
        {
            get { return _checkedPressedFillColor; }
            set { _checkedPressedFillColor = value; }
        }

        private Color _checkedPressedTextColor = Color.FromArgb(168, 168, 168);
        public Color CheckedPressedTextColor
        {
            get { return _checkedPressedTextColor; }
            set { _checkedPressedTextColor = value; }
        }

        private Color _checkedDisbaledBorderColor = Color.FromArgb(80, 80, 80);
        public Color CheckedDisbaledBorderColor
        {
            get { return _checkedDisbaledBorderColor; }
            set { _checkedDisbaledBorderColor = value; }
        }

        private Color _checkedDisbaledFillColor = Color.FromArgb(1, 87, 155);
        public Color CheckedDisbaledFillColor
        {
            get { return _checkedDisbaledFillColor; }
            set { _checkedDisbaledFillColor = value; }
        }

        private Color _checkedDisbaledTextColor = Color.FromArgb(128, 128, 128);
        public Color CheckedDisbaledTextColor
        {
            get { return _checkedDisbaledTextColor; }
            set { _checkedDisbaledTextColor = value; }
        }
        #endregion

        public virtual bool IsCheckedInit
        {
            get
            {
                return _isChecked;
            }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    base.UpdateUI();
                }
            }
        }

        public virtual bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    base.UpdateUI();
                    this.CheckedChangedHandler();
                }
            }
        }

        public MStatefulButton()
        {

        }

        public override Color BorderColor
        {
            get
            {
                if (!this.Enabled)
                {
                    return this._isChecked ? _checkedDisbaledBorderColor : _uncheckedDisbaledBorderColor;
                }

                switch (base.state)
                {
                    case MButtonState.Normal:
                        return this._isChecked ? _checkedNormalBorderColor : _uncheckedNormalBorderColor;

                    case MButtonState.Hover:
                        return this._isChecked ? _checkedHoverBorderColor : _uncheckedHoverBorderColor;

                    case MButtonState.Pressed:
                        return this._isChecked ? _checkedPressedBorderColor : _uncheckedPressedBorderColor;
                }

                return this._isChecked ? _checkedNormalBorderColor : _uncheckedNormalBorderColor;
            }
        }

        public override Color FillColor
        {
            get
            {
                if (!this.Enabled)
                {
                    return this._isChecked ? _checkedDisbaledFillColor : _uncheckedDisbaledFillColor;
                }

                switch (base.state)
                {
                    case MButtonState.Normal:
                        return this._isChecked ? _checkedNormalFillColor : _uncheckedNormalFillColor;

                    case MButtonState.Hover:
                        return this._isChecked ? _checkedHoverFillColor : _uncheckedHoverFillColor;

                    case MButtonState.Pressed:
                        return this._isChecked ? _checkedPressedFillColor : _uncheckedPressedFillColor;
                }

                return this._isChecked ? _checkedNormalFillColor : _uncheckedNormalFillColor;
            }
        }

        public override Color TextColor
        {
            get
            {
                if (!this.Enabled)
                {
                    return this._isChecked ? _checkedDisbaledTextColor : _uncheckedDisbaledTextColor;
                }

                switch (base.state)
                {
                    case MButtonState.Normal:
                        return this._isChecked ? _checkedNormalTextColor : _uncheckedNormalTextColor;

                    case MButtonState.Hover:
                        return this._isChecked ? _checkedHoverTextColor : _uncheckedHoverTextColor;

                    case MButtonState.Pressed:
                        return this._isChecked ? _checkedPressedTextColor : _uncheckedPressedTextColor;
                }

                return this._isChecked ? _checkedNormalTextColor : _uncheckedNormalTextColor;
            }
        }

        protected void CheckedChangedHandler()
        {
            if (this.CheckedChanged != null)
            {
                this.CheckedChanged(this, EventArgs.Empty);
            }
        }
    }
}
