using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;
using AEC.Common;
using AEC.Comm.VKxDCMComm;

namespace AEC.Effectors.VKxDCM.Modules
{
    public class MicExciterModule: ModuleBase     
    {
        private readonly MicModule micModule;
        public readonly ShortParam exciter1Select;        //激励器1功能选择，选择0为低频激励器
        public readonly BassExciter bassExciter;        //选择1为低音激励器
        public readonly Exciter exciter1;               //频段激励器1
        public readonly Exciter exciter2;               //频段激励器2

        public MicExciterModule(EventBus effectorEvent_, MicModule micModule_) :
            base(VKxDCMCommProtocol.micAModuleIndex, effectorEvent_)
        {
            //_themeColor = Color.FromArgb(168, 68, 0);
            base._themeColor = Color.FromArgb(230, 81, 0);
            this.micModule = micModule_;
            this.exciter1Select = this.micModule.exciter1Select;
            this.bassExciter = this.micModule.bassExciter;
            this.exciter1 = this.micModule.exciter1;
            this.exciter2 = this.micModule.exciter2;
        }

        public override bool hasFreqRespCurve
        {
            get
            {
                return false;
            }
        }

        public bool isSelectExciter
        {
            get
            {
                return this.exciter1Select.v != 0;
            }
            set
            {
                this.exciter1Select.v = AEC.Core.DataConverter.Bool2Short(value);
                if(value)
                {
                    this.ExciterChanged(ActiveItem.Exciter1);
                }
                else
                {
                    this.ExciterChanged(ActiveItem.BassExciter);
                }
            }
        }

        public override BassExciter getBassExciter
        {
            get
            {
                if (isSelectExciter)
                {
                    return null;
                }

                return this.bassExciter;
            }
        }

        public override Exciter getExciter1
        {
            get
            {
                if (isSelectExciter)
                {
                    return this.exciter1;
                }

                return null;
            }
        }

        public override Exciter getExciter2
        {
            get
            {
                return this.exciter2;
            }
        }
    }
}
