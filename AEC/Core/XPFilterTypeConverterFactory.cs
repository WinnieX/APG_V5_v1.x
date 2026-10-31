using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Core
{
    public class XPFilterTypeConverterFactory
    {
        public static XPFilterTypeConverter CreateImmutableFilterTypeConverter(FilterType immutableType)
        {
            return new XPFilterTypeConverter(immutableType);
        }

        public static XPFilterTypeConverter Create2ndOrderFilterTypeConverter()
        {
            FilterType[] filterType2ndOrder = new FilterType[]
            {
                FilterType.FirstOrder,
                FilterType.Bessel12dB,
                FilterType.Butterworth12dB,
                FilterType.LinkwitzRiley12dB,
            };
            
            return new XPFilterTypeConverter(filterType2ndOrder, 2);
        }

        public static XPFilterTypeConverter CreateFilterTypeConverter(FilterType[] allFilterTypeList, FilterType defaultFilterType)
        {
            List<FilterType> typeList = new List<FilterType>(allFilterTypeList);
            short initIndex = Utils.ConstrainMax((short)(typeList.IndexOf(defaultFilterType)), (short)(typeList.Count - 1));
            return new XPFilterTypeConverter(allFilterTypeList, initIndex);
        }

        public static XPFilterTypeConverter CreateClassicalFilterTypeConverter()
        {
            FilterType[] allFilterTypeList = new FilterType[]
            {
                FilterType.X5_Bessel24dB,
                FilterType.X5_Butterworth24dB,
                FilterType.X5_LinkwitzRiley24dB,
                FilterType.X5_User12dB,
                FilterType.X5_User24dB,
            };

            return CreateFilterTypeConverter(allFilterTypeList, allFilterTypeList[0]);
        }

        public static XPFilterTypeConverter CreateVKxSFilterTypeConverter()
        {
            FilterType[] allFilterTypeList = new FilterType[]
            {
                FilterType.FirstOrder,
                FilterType.Bessel12dB,
                FilterType.Butterworth12dB,
                FilterType.LinkwitzRiley12dB,
                FilterType.Bessel18dB,
                FilterType.Butterworth18dB,
                FilterType.Bessel24dB,
                FilterType.Butterworth24dB,
                FilterType.LinkwitzRiley24dB,
                FilterType.X5_Bessel24dB,
                FilterType.X5_Butterworth24dB,
                FilterType.X5_LinkwitzRiley24dB,
                FilterType.X5_User12dB,
                FilterType.X5_User24dB,
            };

            return CreateFilterTypeConverter(allFilterTypeList, allFilterTypeList[8]);
        }

        public static XPFilterTypeConverter CreateX5FilterTypeConverter()
        {
            FilterType[] allFilterTypeList = new FilterType[]
            {
                FilterType.Butterworth12dB,
                FilterType.LinkwitzRiley24dB,
            };

            return CreateFilterTypeConverter(allFilterTypeList, allFilterTypeList[0]);
        }

        public static XPFilterTypeConverter Create4thOrderFilterTypeConverter()
        {
            FilterType[] allFilterTypeList = new FilterType[]
            {
                FilterType.FirstOrder,
                FilterType.Bessel12dB,
                FilterType.Butterworth12dB,
                FilterType.LinkwitzRiley12dB,
                FilterType.Bessel18dB,
                FilterType.Butterworth18dB,
                FilterType.Bessel24dB,
                FilterType.Butterworth24dB,
                FilterType.LinkwitzRiley24dB,
            };

            return CreateFilterTypeConverter(allFilterTypeList, FilterType.LinkwitzRiley24dB);
        }

        public static XPFilterTypeConverter CreateProfessionalFilterTypeConverter()
        {
            FilterType[] allFilterTypeList = new FilterType[]
            {
                FilterType.FirstOrder,
                FilterType.Bessel12dB,
                FilterType.Butterworth12dB,
                FilterType.LinkwitzRiley12dB,
                FilterType.Bessel18dB,
                FilterType.Butterworth18dB,
                FilterType.Bessel24dB,
                FilterType.Butterworth24dB,
                FilterType.LinkwitzRiley24dB,
                FilterType.Bessel36dB,
                FilterType.Butterworth36dB,
                FilterType.LinkwitzRiley36dB,
                FilterType.Bessel48dB,
                FilterType.Butterworth48dB,
                FilterType.LinkwitzRiley48dB,
            };

            return CreateFilterTypeConverter(allFilterTypeList, FilterType.LinkwitzRiley24dB);
        }
    }
}
