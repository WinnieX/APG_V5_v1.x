using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.RemoteControl.Protocol
{
    class StringBufferHelper
    {
        //字符串以utf-8编码成字节数组
        public static byte[] StringEncodeToBytes(string str)
        {
            return System.Text.Encoding.UTF8.GetBytes(str);
        }

        //字节数组以utf-8解码成字符串
        public static string BytesDncodeToString(byte[] buffer)
        {
            return System.Text.Encoding.UTF8.GetString(buffer);
        }

        public static string BytesDncodeToString(byte[] buffer, int index, int count)
        {
            return System.Text.Encoding.UTF8.GetString(buffer, index, count);
        }
    }
}
