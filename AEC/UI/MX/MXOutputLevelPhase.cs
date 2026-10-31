using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;

namespace AEC.UI.MX
{
    public class MXOutputLevelPhase : LevelPhase
    {
        protected override string GetLevelLabel(short index)
        {
            return string.Format("{0} %", index * 2);
        }

        protected override short CalcLevelIndexFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, Precent);
            short value;
            if (short.TryParse(str, out value))
            {
                return (short)(value / 2);
            }

            return -1;
        }
    }
}
