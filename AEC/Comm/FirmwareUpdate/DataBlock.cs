using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace AEC.Comm.FirmwareUpdate
{
    public class DataBlock
    {
        public readonly uint address;
        public readonly byte[] data;

        public uint size
        {
            get
            {
                return (uint)this.data.Length;
            }
        }

        public uint endAddress
        {
            get
            {
                return this.address + this.size;
            }
        }

        public DataBlock(uint address_, byte[] data_)
        {
            System.Diagnostics.Debug.Assert(data_ != null && data_.Length > 0);

            this.address = address_;
            this.data = data_;
        }

        public DataBlock Clone()
        {
            byte[] temp = new byte[this.size];
            Array.Copy(this.data, temp, temp.Length);

            return new DataBlock(this.address, temp);
        }

        public bool IsSuperset(DataBlock other)
        {
            System.Diagnostics.Debug.Assert(other != null);

            return this.address <= other.address && this.endAddress >= other.address;
        }

        public bool IsSubset(DataBlock other)
        {
            System.Diagnostics.Debug.Assert(other != null);

            return this.address >= other.address && this.endAddress <= other.address;
        }

        public void Update(DataBlock other)
        {
            System.Diagnostics.Debug.Assert(other != null);

            if(this.IsSuperset(other))
            {
                uint destIndex = other.address - this.address;
                uint length = Math.Min(other.endAddress, this.endAddress) - other.address;
                Array.Copy(other.data, 0, this.data, destIndex, length);
            }
        }
    }

    public class DataBlockSeralizer
    {
        public const string DataBlockNodeName = "data_block";
        private const string DataNode = "data";
        private const string StartAddressAttr = "start_address";
        private const string SizeAttr = "size";
        private const string EndAddressAttr = "end_address";
        private const string EncodingAttr = "encoding";

        public static void SeralizeToFile(DataBlock mucFlashData, ByteArrayEncoding.Encoding encoding, string path)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlDeclaration xmldecl;
                xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmldoc.AppendChild(xmldecl);

                xmldoc.AppendChild(CreateMCUFlashDataBlockNode(xmldoc, encoding, mucFlashData));

                xmldoc.Save(path);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }

        public static XmlElement CreateMCUFlashDataBlockNode(XmlDocument xmldoc, ByteArrayEncoding.Encoding encoding, DataBlock dataBlock)
        {
            XmlElement ele = xmldoc.CreateElement(DataBlockNodeName);
            ele.SetAttribute(StartAddressAttr, Utils.Uint32ToHexString(dataBlock.address));
            ele.SetAttribute(SizeAttr, Utils.Uint32ToHexString(dataBlock.size));
            ele.SetAttribute(EndAddressAttr, Utils.Uint32ToHexString(dataBlock.endAddress));
            ele.SetAttribute(EncodingAttr, ByteArrayEncoding.EncodingToString(encoding));
            ele.InnerText = ByteArrayEncoding.Encode(dataBlock.data, encoding);

            return ele;
        }

        public static DataBlock DeseralizeFromFile(string path, out ByteArrayEncoding.Encoding encoding)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return DeseralizeFromStream(fs, out encoding);
            }
        }

        public static DataBlock DeseralizeFromFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                ByteArrayEncoding.Encoding encoding;
                return DeseralizeFromStream(fs, out encoding);
            }
        }

        public static DataBlock DeseralizeFromStream(Stream stream, out ByteArrayEncoding.Encoding encoding)
        {
            encoding = ByteArrayEncoding.Encoding.Base64;

            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(stream);

                if (xmldoc.DocumentElement != null &&
                xmldoc.DocumentElement.LocalName == DataBlockNodeName)
                {
                    XmlElement rootNode = xmldoc.DocumentElement;

                    return ParseDataBlockNode(rootNode, out encoding);
                }
               
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }

            return null;
        }

        public static DataBlock ParseDataBlockNode(XmlElement dataBlockNode, out ByteArrayEncoding.Encoding encoding)
        {
            encoding = ByteArrayEncoding.Encoding.Base64;

            uint startAddress = 0;
            uint size = 0;
            uint end_address = 0;

            if (!Utils.ParseHexStringToUint32(dataBlockNode.GetAttribute(StartAddressAttr), out startAddress) ||
                !Utils.ParseHexStringToUint32(dataBlockNode.GetAttribute(SizeAttr), out size) ||
                !Utils.ParseHexStringToUint32(dataBlockNode.GetAttribute(EndAddressAttr), out end_address) ||
                startAddress + size != end_address)
            {
                Tools.LogHelper.WriteDebug("数据错误");
                return null;
            }

            string encodingAttr = dataBlockNode.GetAttribute(EncodingAttr);
            if (!ByteArrayEncoding.StringToEncodingMap.ContainsKey(encodingAttr))
            {
                Tools.LogHelper.WriteDebug("编码方式数据错误");
                return null;
            }

            encoding = ByteArrayEncoding.StringToEncodingMap[encodingAttr];

            byte[] data = ByteArrayEncoding.Decode(dataBlockNode.InnerText, encoding);
            if (data.Length != size)
            {
                Tools.LogHelper.WriteDebug("数据实际长度与Size属性不相符");
                return null;
            }

            return new DataBlock(startAddress, data);
        }
    }
}
