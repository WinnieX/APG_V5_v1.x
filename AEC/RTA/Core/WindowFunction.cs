using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RTA.Core
{
    public enum WindowFunction
    {
        HannWindow = 0,       //汉宁窗
        HammingWindow,        //海明窗
        RectangleWindow,      //矩形窗
        GaussianWindow,       //高斯窗
        BlackmanWindow,       //布莱克曼窗
        BlackmanHarrisWindow, //布莱克曼-哈里斯窗
        FlatTopWindow,        //平顶窗
    }
}
