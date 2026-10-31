using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AEC.Core;

namespace AEC.UI
{
    public partial class LevelIndicator : Control
    {
        private const int DefMax = 0;
        private const int DefMin = -36;

        public enum Direction
        {
            Upward,
            Downward,
        }

        private bool _leftTickVisible = false;
        private bool _rightTickVisible = true;

        private Direction _direction = Direction.Upward;
        private int _max = DefMax;
        private int _min = DefMin;
        private int _ledLightHeight = 9;

        private Point _leftTickBackLocation = new Point(0, 0);
        private Point _rightTickBackLocation = new Point(45, 0);
        private Point _ledBackLocation = new Point(25, 0);

        private bool _isOn = false;
        private int _level = DefMin;

        public bool isOn
        {
            get
            {
                return _isOn;
            }
            set
            {
                if(this._isOn != value)
                {
                    _isOn = value;
                    if(!_isOn)
                    {
                        _level = _min;
                    }

                    this.Invalidate();
                }
            }
        }

        public int level
        {
            get
            {
                return this._level;
            }
            set
            {
                value = Utils.ConstrainValue(value, _min, _max);
                if(value != this._level)
                {
                    this._level = value;
                    if(_isOn)
                    {
                        if(this.ledOff != null)
                        {
                            Rectangle rect = new Rectangle(_ledBackLocation, this.ledOff.Size);
                            this.Invalidate(rect);
                        }
                        else
                        {
                            this.Invalidate();
                        }
                        
                        this.Update();
                    }
                }
            }
        }

        public int min
        {
            get
            {
                return this._min;
            }
            set
            {
                this._min = value;
            }
        }

        public int max
        {
            get
            {
                return this._max;
            }
            set
            {
                this._max = value;
            }
        }

        public bool leftTickVisible
        {
            get
            {
                return _leftTickVisible;
            }
            set
            {
                _leftTickVisible = value;
            }
        }

        public bool rightTickVisible
        {
            get
            {
                return _rightTickVisible;
            }
            set
            {
                _rightTickVisible = value;
            }
        }

        public int ledLightHeight
        {
            get
            {
                return _ledLightHeight;
            }
            set
            {
                _ledLightHeight = value;
            }
        }

        public Image leftTick
        {
            get;
            set;
        }

        public Image rightTick
        {
            get;
            set;
        }

        public Image ledOn
        {
            get;
            set;
        }

        public Image ledOff
        {
            get;
            set;
        }

        public Point leftTickBackLocation
        {
            get
            {
                return _leftTickBackLocation;
            }
            set
            {
                _leftTickBackLocation = value;
            }
        }

        public Point rightTickBackLocation
        {
            get
            {
                return _rightTickBackLocation;
            }
            set
            {
                _rightTickBackLocation = value;
            }
        }

        public Point ledBackLocation
        {
            get
            {
                return _ledBackLocation;
            }
            set
            {
                _ledBackLocation = value;
            }
        }

        public Direction direction
        {
            get
            {
                return _direction;
            }
            set
            {
                _direction = value;
            }
        }

        public LevelIndicator()
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
            //this.BackColor = Color.Transparent;
            this.Size = new Size(45, 360);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            PaintLevel(pe.Graphics);
        }

        protected virtual void PaintLevel(Graphics g)
        {
            //GDIHelper.InitializeGraphics(g);
            PaintTick(g);
            PaintLight(g);
        }

        private void PaintTick(Graphics g)
        {
            if(_leftTickVisible && leftTick != null)
            {
                //GDIHelper.QuickDrawImage(g, _leftTickBackLocation, leftTick);
                g.DrawImage(leftTick, _leftTickBackLocation);
            }

            if (_rightTickVisible && rightTick != null)
            {
                //GDIHelper.QuickDrawImage(g, _rightTickBackLocation, rightTick);
                g.DrawImage(rightTick, _rightTickBackLocation);
            }
        }

        private void PaintLight(Graphics g)
        {
            if(!this._isOn || this._level <= _min)
            {
                if(ledOff != null)
                {
                    //GDIHelper.QuickDrawImage(g, _ledBackLocation, ledOff);
                    g.DrawImage(ledOff, _ledBackLocation);
                }
            }
            else
            {
                _level = Utils.ConstrainValue(this._level, _min, _max);

                if(_direction == Direction.Upward)
                {
                    if(ledOff != null && _level < this._max)
                    {
                        //背景
                        int offHeight = (this._max - _level) * _ledLightHeight;
                        Rectangle backSrc = new Rectangle(new Point(), new Size(ledOff.Width, offHeight));
                        Rectangle backDest = new Rectangle(_ledBackLocation, backSrc.Size);
                        g.DrawImage(ledOff, backDest, backSrc, GraphicsUnit.Pixel);
                    }

                    if(ledOn != null && _level > this._min)
                    {
                        int yOffset = (this._max - _level) * _ledLightHeight;
                        int onHeight = (this._level - this._min) * _ledLightHeight;
                        Rectangle foreSrc = new Rectangle(new Point(0, yOffset), new Size(ledOn.Width, onHeight));
                        Rectangle foreDest = new Rectangle(new Point(_ledBackLocation.X, _ledBackLocation.Y + yOffset), foreSrc.Size);
                        g.DrawImage(ledOn, foreDest, foreSrc, GraphicsUnit.Pixel);
                    }
                }
                else
                {
                    if (ledOn != null && _level > this._min)
                    {
                        //前景
                        int onHeight = (_level - this._min) * _ledLightHeight;
                        Rectangle foreSrc = new Rectangle(new Point(), new Size(ledOn.Width, onHeight));
                        Rectangle foreDest = new Rectangle(_ledBackLocation, foreSrc.Size);
                        g.DrawImage(ledOn, foreDest, foreSrc, GraphicsUnit.Pixel);
                    }

                    if (ledOff != null && _level < this._max)
                    {
                        //背景
                        int yOffset = (_level - this._min) * _ledLightHeight;
                        int offHeight = (this._max - this._level) * _ledLightHeight;
                        Rectangle foreSrc = new Rectangle(new Point(0, yOffset), new Size(ledOff.Width, offHeight));
                        Rectangle foreDest = new Rectangle(new Point(_ledBackLocation.X, _ledBackLocation.Y + yOffset), foreSrc.Size);
                        g.DrawImage(ledOff, foreDest, foreSrc, GraphicsUnit.Pixel);
                    }
                }
            }
        }

        public void UpdateLimit(short min_, short max_)
        {
            System.Diagnostics.Debug.Assert(min_ != max_);
            this._min = min_;
            this._max = max_;
            this._level = min_;
        }
    }
}
