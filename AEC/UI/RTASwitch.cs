using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AEC.UI
{
    class RTASwitch:gtButton
    {
        private bool _isOn = false;

        private Image _offNormal = null;
        private Image _offHover = null;
        private Image _offPressed = null;
        private Image _onNormal = null;
        private Image _onHover = null;
        private Image _onPressed = null;

        public Image offNormal
        {
            get
            {
                return _offNormal;
            }
            set
            {
                _offNormal = value;
                if(!_isOn)
                {
                    base.backNormal = _offNormal;
                }
            }
        }
        
        public Image offHover
        {
            get
            {
                return _offHover;
            }
            set
            {
                _offHover = value;
                if (!_isOn)
                {
                    base.backHover = _offHover;
                }
            }
        }

        public Image offPressed
        {
            get
            {
                return _offPressed;
            }
            set
            {
                _offPressed = value;
                if (!_isOn)
                {
                    base.backPressed = _offPressed;
                }
            }
        }

        public Image onNormal
        {
            get
            {
                return _onNormal;
            }
            set
            {
                _onNormal = value;
                if (_isOn)
                {
                    base.backNormal = _onNormal;
                }
            }
        }

        public Image onHover
        {
            get
            {
                return _onHover;
            }
            set
            {
                _onHover = value;
                if (_isOn)
                {
                    base.backHover = _onHover;
                }
            }
        }

        public Image onPressed
        {
            get
            {
                return _onPressed;
            }
            set
            {
                _onPressed = value;
                if (_isOn)
                {
                    base.backPressed = _onPressed;
                }
            }
        }

        public bool isOn
        {
            get
            {
                return _isOn;
            }
            set
            {
                if (_isOn != value)
                {
                    _isOn = value;
                    if (_isOn)
                    {
                        this.backNormal = onNormal;
                        this.backHover = onHover;
                        this.backPressed = onPressed;
                    }
                    else
                    {
                        this.backNormal = offNormal;
                        this.backHover = offHover;
                        this.backPressed = offPressed;
                    }

                    this.Invalidate();
                }
            }
        }

        //public bool 
        //{
        //    this.Invalidate();
        //}

        public RTASwitch()
        {
            userGDIPaint = false;
            //this.backDisabled = global::AEC.Properties.Resources.rta_switch_disabled;
            //this.backNormal = global::AEC.Properties.Resources.rta_switch_closed_normal;
            //this.backHover = global::AEC.Properties.Resources.rta_switch_closed_hover;
            //this.backPressed = global::AEC.Properties.Resources.rta_switch_closed_pressed;
        }
    }
}
