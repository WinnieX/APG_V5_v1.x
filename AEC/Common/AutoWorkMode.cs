using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;

namespace AEC.Common
{
    public class AutoWorkMode
    {
        public readonly ShortParam wm = null;
        public readonly ShortParam ws = null;


        public short workModeInit
        {
            set
            {
                this.wm.initV = value;
            }
        }

        public short workSelectInit
        {
            set
            {
                this.ws.initV = value;
            }
        }

        public WorkMode workMode
        {
            get
            {
                return DataConverter.Short2WorkMode(this.wm.v);
            }
            set
            {
                this.wm.v = (short)value;
            }
        }

        public WorkSelect workSelect
        {
            get
            {
                return DataConverter.Short2WorkSelect(this.ws.v);
            }
            set
            {
                this.ws.v = (short)value;
            }
        }

        public bool isManual
        {
            get
            {
                return this.wm.v == (short)WorkMode.Manual;
            }
        }

        public bool isSing
        {
            get
            {
                return this.ws.v == (short)WorkSelect.Sing;
            }
        }

        public AutoWorkMode(byte moduleIndex_, byte address, EventHandler<DataParamChangedArgs> dataChangedHandler = null)
        {
            this.wm = new ShortParam(moduleIndex_, (byte)(address + 0), (short)WorkMode.Manual, dataChangedHandler);
            this.ws = new ShortParam(moduleIndex_, (byte)(address + 1), (short)WorkSelect.Disco, dataChangedHandler);

            this.workModeInit = 1;
            this.workSelectInit = 0;
        }

        public void Reset()
        {
            this.workModeInit = (short)WorkMode.Manual;
            this.workSelectInit = (short)WorkSelect.Sing;
        }

        public void CopyFrom(AutoWorkMode other)
        {
            this.workModeInit = (short)other.workMode;
            this.workSelectInit = (short)other.workSelect;
        }
    }
}
