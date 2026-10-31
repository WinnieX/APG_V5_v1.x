using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Core
{
    class TestEQUtils
    {
        public static void TestEQFreq2String()
        {
            for (int i = 0; i < DataUtils.eqFreqList.Length; ++i)
            {
                Tools.LogHelper.WriteDebug(DataUtils.EQFreq2String(i));
            }
        }

        public static void TestFilterIndex2String()
        {
            for (int i = 0; i < DataUtils.filterFreqList.Length; ++i)
            {
                Tools.LogHelper.WriteDebug(DataUtils.FilterIndex2String(i));
            }
        }
    }
}
