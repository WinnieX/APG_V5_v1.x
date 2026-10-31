using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Comm.FirmwareUpdate
{
    public class ByteArrayEncoding
    {
        public enum Encoding
        {
            Base64,
            Hex
        }

        public static readonly Dictionary<Encoding, string> encodingToStringMap = new Dictionary<Encoding, string>()
        {
            {Encoding.Base64, "Base64"},
            {Encoding.Hex, "Hex"},
        };

        public static readonly Dictionary<string, Encoding> StringToEncodingMap = new Dictionary<string, Encoding>()
        {
            {"Base64", Encoding.Base64},
            {"Hex", Encoding.Hex},
        };

        public static string EncodingToString(Encoding encoding)
        {
            return ByteArrayEncoding.encodingToStringMap[encoding];
        }

        public static string Encode(byte[] data, Encoding encoding)
        {
            if(encoding == Encoding.Base64)
            {
                return Convert.ToBase64String(data, Base64FormattingOptions.None);
            }
            else if(encoding == Encoding.Hex)
            {
                return Utils.ByteArrayToHexStr(data, string.Empty);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
   
        public static byte[] Decode(string text, Encoding encoding)
        {
            if (encoding == Encoding.Base64)
            {
                return Convert.FromBase64String(text);
            }
            else if (encoding == Encoding.Hex)
            {
                return Utils.HexStringTorByteArray(text);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
