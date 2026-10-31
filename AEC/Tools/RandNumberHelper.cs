using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AEC.Tools
{
    public class RandNumberHelper
    {
        public static byte[] GetRandNumbers(int size)
        {
            byte[] randBumberBuffer = new byte[size];

            new Random((int)DateTime.Now.Ticks).NextBytes(randBumberBuffer);

            try
            {
                var rngCsp = new System.Security.Cryptography.RNGCryptoServiceProvider();
                rngCsp.GetBytes(randBumberBuffer);
            }
            catch (Exception)
            {

            }

            return randBumberBuffer;
        }

        public static void FillRandomNumber(byte[] buffer)
        {
            Debug.Assert(buffer != null);

            new Random((int)DateTime.Now.Ticks).NextBytes(buffer);

            try
            {
                var rngCsp = new System.Security.Cryptography.RNGCryptoServiceProvider();
                rngCsp.GetBytes(buffer);
            }
            catch (Exception)
            {

            }
        }
    }
}
