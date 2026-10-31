using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Core
{
    public class XPFilterTypeConverter : XPFilterConverterBase
    {
        private static readonly short typeMaxIndex = (short)(Enum.GetNames(typeof(FilterType)).Length - 1);

        public readonly MShort type;
        public readonly Dictionary<int, FilterType> indexMapToType = new Dictionary<int, FilterType>();

        public XPFilterTypeConverter(FilterType immutableType) :
            base(typeMaxIndex, 0, 0, 0)
        {
            this.type = base.param;
            this.indexMapToType[0] = immutableType;
        }

        public XPFilterTypeConverter(
            FilterType[] allFilterTypeList,     //至少含有1个元素,不能含有重复元素,按顺序排列
            short initIndex_) :
            base(typeMaxIndex, 0, (short)(allFilterTypeList.Length - 1), Utils.ConstrainMax(initIndex_, (short)(allFilterTypeList.Length - 1)))
        {
            this.type = base.param;
            for(int i = 0; i < allFilterTypeList.Length; ++i)
            {
                indexMapToType[i] = allFilterTypeList[i];
            }
        }

        public FilterType filterType
        {
            get
            {
                return indexMapToType[base.paramIndex];
            }
        }

        public short filterTypeIndex
        {
            get
            {
                return base.paramIndex;
            }
            set
            {
                base.paramIndex = value;
            }
        }

        public short typeLowerLimitIndex
        {
            get
            {
                return base.paramLowerLimitIndex;
            }
        }

        public short typeUpperLimitIndex
        {
            get
            {
                return base.paramUpperLimitIndex;
            }
        }
    }

    //public class XPFilterImmutableTypeConverter : XPFilterTypeConverter
    //{
    //    public XPFilterImmutableTypeConverter(FilterType filterType):
    //        base((short)filterType, (short)filterType, (short)filterType)
    //    {

    //    }
    //}

    //public class FilterTypeButterworth12dBConverter : XPFilterImmutableTypeConverter
    //{
    //    public FilterTypeButterworth12dBConverter() :
    //        base(FilterType.Butterworth12dB)
    //    {

    //    }
    //}

    //public class XPFilterMultipleTypeConverter : XPFilterTypeConverter
    //{

    //}
}
