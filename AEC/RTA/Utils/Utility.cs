using System;
using System.Collections.Generic;
using System.Text;
using AEC.RTA.Core;

namespace AEC.RTA.Utils
{
    public class Utility
    {
        public static void CopyData2ArrayEnd(double[] source, double[] dest)
        {
            double[] temp = new double[dest.Length];

            Buffer.BlockCopy(dest, source.Length * sizeof(double), temp, 0, (dest.Length - source.Length) * sizeof(double));
            Buffer.BlockCopy(source, 0, temp, (dest.Length - source.Length) * sizeof(double), source.Length * sizeof(double));
            Buffer.BlockCopy(temp, 0, dest, 0, dest.Length * sizeof(double));
        }

        public static double[] CutData(double[] source, int maxLength)
        {
            if (source.Length > maxLength)
            {
                double[] dest = new double[maxLength];
                //Array.Copy(source, source.Length - maxLength, dest, 0, maxLength);
                int offset = (source.Length - maxLength) * sizeof(double);
                int bytesCount = maxLength * sizeof(double);
                Buffer.BlockCopy(source, offset, dest, 0, bytesCount);
                return dest;
            }

            return source;
        }

        public static double[] ExtractMonoFromStereo(double[] stereoData, AudioChannel channel)
        {
            int length = stereoData.Length / 2;
            double[] monoData = new double[length];
            int ch = (int)channel;
            for (int i = 0; i < length; ++i)
            {
                monoData[i] = stereoData[i * 2 + ch];
            }

            return monoData;
        }

        public static System.Drawing.Rectangle[] CopyRectArray(System.Drawing.Rectangle[] source)
        {
            if (source != null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bf.Serialize(ms, source);
                ms.Seek(0, 0);
                System.Drawing.Rectangle[] dest = (System.Drawing.Rectangle[])bf.Deserialize(ms);
                ms.Close();
                return dest;
            }

            return null;
        }

        public static List<int> CopyList(List<int> source)
        {
            System.Diagnostics.Debug.Assert(source != null);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            bf.Serialize(ms, source);
            ms.Seek(0, 0);
            List<int> dest = (List<int>)bf.Deserialize(ms);
            ms.Close();
            return dest;
        }

        public static double[] CopyArray(double[] source)
        {
            System.Diagnostics.Debug.Assert(source != null);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            bf.Serialize(ms, source);
            ms.Seek(0, 0);
            double[] dest = (double[])bf.Deserialize(ms);
            ms.Close();
            return dest;
        }
    }
}
