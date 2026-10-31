using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using AEC.Core;

namespace AEC.Test
{
    class DictTest
    {
        public static void TestDict()
        {
            Dictionary<Size, EQ> dictTest = new Dictionary<Size, EQ>();
            dictTest[new Size(2, 3)] = EQ.Create(0);
            dictTest[new Size(3, 3)] = EQ.Create(1);
            dictTest[new Size(2, 3)] = EQ.Create(2);
            //AEC.Log.OutputLine(dictTest);
        }
    }
}
