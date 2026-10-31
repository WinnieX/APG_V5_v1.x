using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AEC.UI
{
    public class NumberKeysHelper
    {
        private const int timeDiffThreshold_in_ms = 1024;

        private short last = 0;
        private DateTime lastKeytimestamp = DateTime.Now;

        public static Dictionary<Keys, short> keyNumberMap = new Dictionary<Keys, short>()
        {
            {Keys.D0, 0},
            {Keys.D1, 1},
            {Keys.D2, 2},
            {Keys.D3, 3},
            {Keys.D4, 4},
            {Keys.D5, 5},
            {Keys.D6, 6},
            {Keys.D7, 7},
            {Keys.D8, 8},
            {Keys.D9, 9},
            {Keys.NumPad0, 0},
            {Keys.NumPad1, 1},
            {Keys.NumPad2, 2},
            {Keys.NumPad3, 3},
            {Keys.NumPad4, 4},
            {Keys.NumPad5, 5},
            {Keys.NumPad6, 6},
            {Keys.NumPad7, 7},
            {Keys.NumPad8, 8},
            {Keys.NumPad9, 9},
        };
        
        public static bool IsNumber(Keys keyData)
        {
            return keyNumberMap.ContainsKey(keyData);
        }

        public List<short> GetIndex(Keys keyData)
        {
            if(!IsNumber(keyData))
            {
                return null;
            }

            double timeDiff = (DateTime.Now - lastKeytimestamp).TotalMilliseconds;
            lastKeytimestamp = DateTime.Now;
            //AEC.Log.OutputLine("timeDiff {0}", timeDiff);
            // > timeThreshold_in_ms
            if(timeDiff > timeDiffThreshold_in_ms)
            {
                last = keyNumberMap[keyData];
                if(last > 0)
                {
                    //AEC.Log.OutputLine("last {0}", last);
                    return new List<short>() { last };
                }
                else
                {
                    return null;
                }
            }
            else
            {
                short curr = keyNumberMap[keyData];

                if(last == 0)
                {
                    last = curr;
                    if(curr == 0)
                    {
                        return new List<short>(curr);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    short total = (short)(last * 10 + curr);
                    last = curr;
                    if (curr == 0)
                    {
                        return new List<short>() {total};
                    }
                    else
                    {
                        return new List<short>() {total, curr};
                    }
                }
            }
        }
    }
}
