using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using AEC.Core.DSP;

namespace AEC.Core
{
    public abstract class XPFilterBase : MFilterBase
    {
        public delegate string GetTypeLabel(XPFilterBase filterBase);

        private const short X5_Bessel24dB_QIndex = 12;
        private const short X5_Butterworth24dB_QIndex = 13;
        private const short X5_LinkwitzRiley24dB_QIndex = 10;

        public static readonly short defSIndex = DataUtils.defSIndex;
        private static readonly Dictionary<FilterType, string> filterTypeMapString = new Dictionary<FilterType, string>()
        {
            {FilterType.FirstOrder,             "1st-6"},
            {FilterType.Bessel12dB,             "BE-12"},
            {FilterType.Butterworth12dB,        "BW-12"},
            {FilterType.LinkwitzRiley12dB,      "LR-12"},
            {FilterType.Bessel18dB,             "BE-18"},
            {FilterType.Butterworth18dB,        "BW-18"},
            {FilterType.Bessel24dB,             "BE-24"},
            {FilterType.Butterworth24dB,        "BW-24"},
            {FilterType.LinkwitzRiley24dB,      "LR-24"},
            {FilterType.Bessel36dB,             "BE-36"},
            {FilterType.Butterworth36dB,        "BW-36"},
            {FilterType.LinkwitzRiley36dB,      "LR-36"},
            {FilterType.Bessel48dB,             "BE-48"},
            {FilterType.Butterworth48dB,        "BW-48"},
            {FilterType.LinkwitzRiley48dB,      "LR-48"},
            {FilterType.X5_Bessel24dB,          "X5 BE-24"},
            {FilterType.X5_Butterworth24dB,     "X5 BW-24"},
            {FilterType.X5_LinkwitzRiley24dB,   "X5 LR-24"},
            {FilterType.X5_User12dB,            "User-12"},
            {FilterType.X5_User24dB,            "User-24"},
        };

        public readonly XPFilterType pFilterType;

        private readonly XPFilterFreqConverter freqConverter;
        private readonly XPFilterTypeConverter typeConverter;
        private readonly XPFilterQConverter qConverter;

        public bool hasQ
        {
            get
            {
                return this.qConverter != null;
            }
        }

        public ShortParam freqParam
        {
            get;
            private set;
        }
        public ShortParam typeParam
        {
            get;
            private set;
        }
        public ShortParam qParam
        {
            get;
            private set;
        }

        public GetTypeLabel getTypeLabel
        {
            get;
            private set;
        }

        //public bool showTypeAsSlope
        //{
        //    get;
        //    set;
        //}

        public bool typeIsFixed
        {
            get;
            set;
        }

        public XPFilterBase(
            XPFilterType filterType,
            XPFilterFreqConverter freqConverter_,
            XPFilterTypeConverter typeConverter_,
            XPFilterQConverter qConverter_ = null)
        {
            Debug.Assert(freqConverter_ != null && typeConverter_ != null);

            this.pFilterType = filterType;
            this.freqConverter = freqConverter_;
            this.typeConverter = typeConverter_;
            this.qConverter = qConverter_;

            this.typeIsFixed = false;
        }

        public void BindingParams(ShortParam freqParam_, ShortParam typeParam_ = null, ShortParam qParam_ = null)
        {
            Debug.Assert(freqParam_ != null);

            this.freqParam = freqParam_;
            this.typeParam = typeParam_;
            this.qParam = qParam_;

            this.InitFreqParam();
            this.InitTypeParam();
            this.InitQParam();
        }

        //public void SynchronizeAllParams()
        //{
        //    this.SynchronizeFreqParam();
        //    this.SynchronizeTypeParam();
        //    this.SynchronizeQParam();
        //}

        //public void SynchronizeFreqParam()
        //{
        //    if(this.freqParam != null)
        //    {
        //        this.freqParam.initV = this.freqIndex;
        //    }
        //}

        //public void SynchronizeTypeParam()
        //{
        //    if(this.typeParam != null)
        //    {
        //        this.typeParam.initV = this.typeIndex;
        //    }
        //}

        //public void SynchronizeQParam()
        //{
        //    if(this.qParam != null)
        //    {
        //        this.qParam.initV = this.qIndex;
        //    }
        //}

        private void InitFreqParam()
        {
            if (this.freqParam != null)
            {
                this.freqParam.initV = this.freqIndex;
            }
        }

        private void UpdateFreqParam()
        {
            if (this.freqParam != null)
            {
                this.freqParam.v = this.freqIndex;
            }
        }

        private void InitTypeParam()
        {
            if (this.typeParam != null)
            {
                this.typeParam.initV = this.typeIndex;
            }
        }

        private void UpdateTypeParam()
        {
            if (this.typeParam != null)
            {
                this.typeParam.v = this.typeIndex;
            }
        }

        private void InitQParam()
        {
            if (this.qParam != null)
            {
                this.qParam.initV = this.qIndex;
            }
        }

        private void UpdateQParam()
        {
            if (this.qParam != null)
            {
                this.qParam.v = this.qIndex;
            }
        }


        public void UpdateFreqIndexFormParam()
        {
            if (this.freqParam != null)
            {
                this.freqConverter.index = this.freqParam.v;
            }
        }

        public void UpdateTypeIndexFromParam()
        {
            if (this.typeParam != null)
            {
                this.typeConverter.index = this.typeParam.v;
            }
        }

        public void UpdateQIndexFromParam()
        {
            if (this.qParam != null)
            {
                this.qConverter.index = this.qParam.v;
            }
        }


        public override bool freqRespIsZero
        {
            get
            {
                return !this.visible || this.filterFreqIndex == 0 || this.filterFreqIndex == DataUtils.maxFilterFreqIndex;
            }
        }

        public bool freqCanBeIncreased
        {
            get
            {
                return this.filterFreqIndex < this.freqUpperLimitIndex;
            }
        }

        public bool freqCanBeDecreased
        {
            get
            {
                return this.filterFreqIndex > this.freqLowerLimitIndex;
            }
        }

        

        public bool typeCanChanged
        {
            get
            {
                return !this.typeIsFixed && !this.typeConverter.isImmutable;
            }
        }

        public bool qCanBeChanged
        {
            get
            {
                if ((this.type == FilterType.X5_User12dB || this.type == FilterType.X5_User24dB) && this.qConverter != null && !this.qConverter.isImmutable)
                {
                    return true;
                }

                return false;
            }
        }

        public bool qCanBeIncreased
        {
            get
            {
                if (qCanBeChanged)
                {
                    return this.qConverter.index < this.qConverter.max;
                }

                return false;
            }
        }

        public bool qCanBeDecreased
        {
            get
            {
                if (qCanBeChanged)
                {
                    return this.qConverter.index > 0;
                }

                return false;
            }
        }

        //实际的频率索引
        public short freqIndex
        {
            get
            {
                return freqConverter.freq.v;
            }
            set
            {
                this.freqConverter.freq.v = value;
                this.InitFreqParam();
            }
        }

        public short freqMaxIndex
        {
            get
            {
                return this.freqConverter.max;
            }
        }

        //实际的频率索引最大值
        public short freqUpperLimitIndex
        {
            get
            {
                return freqConverter.freqUpperLimitIndex;
            }
        }

        //实际的频率索引最小值
        public short freqLowerLimitIndex
        {
            get
            {
                return freqConverter.freqLowerLimitIndex;
            }
        }

        //参与计算的频率索引
        public short filterFreqIndex
        {
            get
            {
                return this.freqConverter.filterFreqIndex;
            }
            set
            {
                this.freqConverter.filterFreqIndex = value;

                this.UpdateFreqParam();
            }
        }

        //public short typeIndexInit
        //{
        //    get
        //    {
        //        return this.type
        //    }
        //}

        public short typeIndexParam
        {
            get
            {
                return typeIndex;
            }
            set
            {
                this.typeConverter.type.v = value;
                this.UpdateTypeParam();
            }
        }

        //实际的类型索引
        public short typeIndex
        {
            get
            {
                return this.typeConverter.type.v;
            }
            set
            {
                this.typeConverter.type.v = value;
                this.InitTypeParam();
            }
        }

        public short typeMaxIndex
        {
            get
            {
                return this.typeConverter.max;
            }
        }

        public FilterType type
        {
            get
            {
                return this.typeConverter.filterType;
            }
            //set
            //{
            //    if (type != value)
            //    {
            //        this._type = value;
            //    }
            //}
        }

        public short qIndexParam
        {
            get
            {
                return this.qIndex;
            }
            set
            {
                if (this.qConverter != null)
                {
                    this.qConverter.qFactor.v = value;
                    this.UpdateQParam();
                }
            }
        }

        //Q值频率索引
        public short qIndex
        {
            get
            {
                if(this.qConverter != null)
                {
                    return this.qConverter.qFactor.v;
                }

                return 0;
            }
            set
            {
                if (this.qConverter != null)
                {
                    this.qConverter.qFactor.v = value;
                    this.InitQParam();
                }
            }
        }

        public short qMaxIndex
        {
            get
            {
                if(this.qConverter != null)
                {
                    return this.qConverter.max;
                }

                return 0;
            }
        }

        public short filterQIndex
        {
            get
            {
                if (this.qConverter != null)
                {
                    return qConverter.filterQFactorIndex;
                }
                else
                {
                    return defSIndex;
                }
            }
            set
            {
                if (this.qConverter != null)
                {
                    this.qConverter.filterQFactorIndex = value;
                }

                this.UpdateQParam();
            }
        }

        //实际的频率索引最大值
        public short qUpperLimitIndex
        {
            get
            {
                return qConverter.qFactorUpperLimitIndex;
            }
        }

        //实际的频率索引最小值
        public short qLowerLimitIndex
        {
            get
            {
                return qConverter.qFactorLowerLimitIndex;
            }
        }

        public double freq
        {
            get
            {
                return DataUtils.filterFreqList[this.filterFreqIndex];
            }
        }

        public bool isClassicalFilter
        {
            get
            {
                return (this.type == FilterType.X5_Bessel24dB ||
                    this.type == FilterType.X5_Butterworth24dB ||
                    this.type == FilterType.X5_LinkwitzRiley24dB ||
                    this.type == FilterType.X5_User12dB ||
                    this.type == FilterType.X5_User24dB);
            }
        }

        private short dspQIndex
        {
            get
            {
                switch(type)
                {
                    case FilterType.X5_Bessel24dB:
                        return X5_Bessel24dB_QIndex;

                    case FilterType.X5_Butterworth24dB:
                        return X5_Butterworth24dB_QIndex;

                    case FilterType.X5_LinkwitzRiley24dB:
                        return X5_LinkwitzRiley24dB_QIndex;

                    default:
                        return this.filterQIndex;
                }
            }
            
        }

        public double q
        {
            get
            {

                return DataUtils.filterQList[dspQIndex];
            }
        }

        public string label
        {
            get
            {
                if (this.freqRespIsZero)
                {
                    return DataUtils.Off;
                }
                else
                {
                    if (!this.typeCanChanged)
                    {
                        return DataUtils.FilterIndex2String(this.filterFreqIndex);
                    }
                    else
                    {
                        if (this.type == FilterType.X5_User12dB || this.type == FilterType.X5_User24dB)
                        {
                            return String.Format("{0}{1}{2}{3}Q:{4}",
                                DataUtils.FilterIndex2String(this.filterFreqIndex),
                                System.Environment.NewLine,
                                filterTypeMapString[this.type],
                                System.Environment.NewLine,
                                DataUtils.Q2String(this.q));
                        }
                        else
                        {
                            return String.Format("{0}{1}{2}",
                                DataUtils.FilterIndex2String(this.filterFreqIndex),
                                System.Environment.NewLine,
                                getTypeLabel != null ? getTypeLabel(this) : filterTypeMapString[this.type]);
                                //showTypeAsSlope ? GetSlopeLabel(this.type) : filterTypeMapString[this.type]);
                        }
                    }
                }
            }
        }

        public void FreqIncrease()
        {
            if(this.filterFreqIndex < this.freqUpperLimitIndex)
            {
                this.freqConverter.Increase();
                this.UpdateFreqParam();
            }
        }

        public void FreqDecrease()
        {
            if(this.filterFreqIndex > this.freqLowerLimitIndex)
            {
                this.freqConverter.Decrease();
                this.UpdateFreqParam();
            }
        }

        public void QIncrease()
        {
            if (this.qConverter!= null && !this.qConverter.isImmutable)
            {
                this.qConverter.Increase(false);
                this.UpdateQParam();
            }
        }

        public void QDecrease()
        {
            if (this.qConverter != null && !this.qConverter.isImmutable)
            {
                this.qConverter.Decrease(false);
                this.UpdateQParam();
            }
        }

        public void Change2NextType()
        {
            if (!this.typeConverter.isImmutable)
            {
                this.typeConverter.Increase(true);
                this.UpdateTypeParam();
            }
        }

        public void Change2PrevType()
        {
            if (!this.typeConverter.isImmutable)
            {
                this.typeConverter.Decrease(true);
                this.UpdateTypeParam();
            }
        }

        public void UpdateFreqResp()
        {
            DSPHelper.UpdateXPFilterFreqResp(this);
        }

        public int GetFreqIndex(int index)
        {
            return Utils.ConstrainMax(index + this.freqConverter.freqLowerLimitIndex, this.freqConverter.freqUpperLimitIndex);
        }

        public void CopyFrom(XPFilterBase other)
        {
            Debug.Assert(this.pFilterType == other.pFilterType);

            this.freqConverter.filterFreqIndex = other.freqConverter.filterFreqIndex;
            this.InitFreqParam();
            if(!this.typeConverter.isImmutable)
            {
                this.typeIndex = other.typeIndex;
            }
            if(this.qConverter != null && other.qConverter != null && !this.qConverter.isImmutable)
            {
                this.qIndex = other.qIndex;
            }
        }

        public void Reset()
        {
            this.freqConverter.Reset();
            this.InitFreqParam();
            if (!this.typeConverter.isImmutable)
            {
                this.typeConverter.Reset();
                this.InitTypeParam();
            }
            if (this.qConverter != null)
            {
                this.qConverter.Reset();
                this.InitQParam();
            }

            this.UpdateFreqResp();
        }

        public short ConstrainFreqIndex(short fIndex)
        {
            return Utils.ConstrainValue(fIndex, this.freqConverter.freqLowerLimitIndex, this.freqConverter.freqUpperLimitIndex);
        }

        public void SetGetTypeAsOrderLabel()
        {
            this.getTypeLabel = GetTypeAsOrderLabel;
        }

        public void SetGetTypeAsSlopeIndexLabel()
        {
            this.getTypeLabel = GetTypeAsSlopeIndexLabel;
        }

        private static string GetTypeAsOrderLabel(XPFilterBase filterBase)
        {
            int order = 1;
            switch (filterBase.type)
            {
                case FilterType.FirstOrder:
                    break;

                case FilterType.Bessel12dB:
                case FilterType.Butterworth12dB:
                case FilterType.LinkwitzRiley12dB:
                case FilterType.X5_User12dB:
                    order = 2;
                    break;

                case FilterType.Bessel18dB:
                case FilterType.Butterworth18dB:
                    order = 3;
                    break;

                case FilterType.Bessel24dB:
                case FilterType.Butterworth24dB:
                case FilterType.LinkwitzRiley24dB:
                case FilterType.X5_User24dB:
                    order = 4;
                    break;

                case FilterType.Bessel36dB:
                case FilterType.Butterworth36dB:
                case FilterType.LinkwitzRiley36dB:
                    order = 6;
                    break;

                case FilterType.Bessel48dB:
                case FilterType.Butterworth48dB:
                case FilterType.LinkwitzRiley48dB:
                    order = 8;
                    break;
            }
            return string.Format("{0} dB", order * 6);
        }

        private static string GetTypeAsSlopeIndexLabel(XPFilterBase filterBase)
        {
            return string.Format("Slope: {0}", filterBase.typeIndex + 1);
        }
    }
}
