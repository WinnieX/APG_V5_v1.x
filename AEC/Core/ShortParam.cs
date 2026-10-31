using System;
using System.Collections.Generic;
using System.Text;

#if COMM_TEST
#endif

namespace AEC.Core
{
    public class ShortParam
    {
        public readonly byte moduleIndex = 0;
        public readonly byte addr = 0;
        private short _max;
        protected short _value = 0;

        private event EventHandler<DataParamChangedArgs> dataChanged;

        public ShortParam(byte moduleIndex_, byte address, short max, EventHandler<DataParamChangedArgs> dataChangedHandler = null)
        {
            this.moduleIndex = moduleIndex_;
            this.addr = address;
            this._max = max;
            this.dataChanged += dataChangedHandler;
        }

        public ShortParam(byte moduleIndex_, byte address, short max, short value)
        {
            this.moduleIndex = moduleIndex_;
            this.addr = address;
            this._max = max;
            this._value = value;
        }

        public ShortParam(short max)
        {
            this._max = max;
        }

        public short initV
        {
            set
            {
                //this._value = Utils.ConstrainMax(value, this._max);
                if (value >= 0 && value <= this._max)
                {
                    this._value = value;
                }
                else
                {
                    this._value = 0;
                }
            }
        }

        public short v
        {
            get
            {
                return this._value;
            }
            set
            {
                if (value != this._value)
                {
                    //this._value = Utils.ConstrainMax(value, this._max);
                    if (value >= 0 && value <= this._max)
                    {
                        this._value = value;
                    }
                    else
                    {
                        this._value = 0;
                    }

                    this.UpdateParam();
                }
            }
        }
        
        public void UpdateParam()
        {
            if (this.dataChanged != null)
            {
                this.dataChanged(this, new DataParamChangedArgs(this));
            }
        }

        public short max
        {
            get
            {
                return this._max;
            }
            set
            {
                if(this._max >= 0)
                {
                    if(this._value >= value)
                    {
                        this._value = 0;
                    }

                    this._max = value;
                }
            }
        }

        public void AddListener(EventHandler<DataParamChangedArgs> handler)
        {
            dataChanged += handler;
        }

        public void RemoveListener(EventHandler<DataParamChangedArgs> handler)
        {
            dataChanged -= handler;
        }

#if COMM_TEST
        public readonly static Random random = new Random();

        public void RandomTest()
        {
            short newValue = (short)random.Next(this._max + 1);
            if (this.v != newValue)
            {
                this.v = newValue;
            }
            else
            {
                if (this.v == 0)
                {
                    this.v = 1;
                }
                else
                {
                    this.v = 0;
                }
            }
        }

        public void RandomTest(short max)
        {
            max = Utils.ConstrainMax(max, this._max);
            if(max == 0)
            {
                this.v = 0;
                return;
            }

            short newValue = (short)random.Next(max + 1);
            if (this.v != newValue)
            {
                this.v = newValue;
            }
            else
            {
                if (this.v == 0)
                {
                    this.v = 1;
                }
                else
                {
                    this.v = 0;
                }
            }
        }
#endif
    }
}
