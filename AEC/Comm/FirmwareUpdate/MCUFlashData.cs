using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace AEC.Comm.FirmwareUpdate
{
    public class VKxMCUFlashData
    {
        public const int FLASH_END_ADDR = 0x08020000;
        public const uint DEVICE_INFO_DATA_START_ADDR = 0x0801F800;
        public const uint DEVICE_CUSTOM_MODULE_START_ADDR = 0x0801F000;
        public const uint SYSTEM_COMMON_DATA_START_ADDR = 0x0801E800;
        public const uint PRESET_DATA_START_ADDR = 0x0801A000;

        public const uint PRESET_DATA_END_ADDR = SYSTEM_COMMON_DATA_START_ADDR;
        public const uint PRESET_DATA_LENGTH = SYSTEM_COMMON_DATA_START_ADDR - PRESET_DATA_START_ADDR;

        public readonly DataBlock presetsData = new DataBlock(PRESET_DATA_START_ADDR, Utils.CreateByteArray(SYSTEM_COMMON_DATA_START_ADDR - PRESET_DATA_START_ADDR));
        public readonly DataBlock systemCommonData = new DataBlock(SYSTEM_COMMON_DATA_START_ADDR, Utils.CreateByteArray(DEVICE_CUSTOM_MODULE_START_ADDR - SYSTEM_COMMON_DATA_START_ADDR));
        public readonly DataBlock deviceCustomData = new DataBlock(DEVICE_CUSTOM_MODULE_START_ADDR, Utils.CreateByteArray(DEVICE_INFO_DATA_START_ADDR - DEVICE_CUSTOM_MODULE_START_ADDR));
        public readonly DataBlock deviceInfoData = new DataBlock(DEVICE_INFO_DATA_START_ADDR, Utils.CreateByteArray(FLASH_END_ADDR - DEVICE_INFO_DATA_START_ADDR));

        //public void SeralizeToFile(string path, )
    }

    public class VKxMCUFlashDataSeralizer
    {
        private const string ModelAttr = "model";
        private const string Model = "VKx";
        private const string RootNode = "mcu_flash_data";
        private const string DataNode = "data";
        //private const string AddressAttr = "address";

        private const string StartAddressAttr = "start_address";
        private const string SizeAttr = "size";
        private const string EndAddressAttr = "end_address";
        private const string EncodingAttr = "encoding";

        public static void SeralizeToFile(VKxMCUFlashData mucFlashData, ByteArrayEncoding.Encoding encoding, string path)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlDeclaration xmldecl;
                xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmldoc.AppendChild(xmldecl);

                //加入根节点 lmAEC
                XmlElement rootEle = xmldoc.CreateElement(RootNode);
                rootEle.SetAttribute(ModelAttr, Model);

                xmldoc.AppendChild(rootEle);

                rootEle.AppendChild(CreateDataNode(xmldoc, mucFlashData.presetsData, encoding));
                rootEle.AppendChild(CreateDataNode(xmldoc, mucFlashData.systemCommonData, encoding));
                rootEle.AppendChild(CreateDataNode(xmldoc, mucFlashData.deviceCustomData, encoding));
                rootEle.AppendChild(CreateDataNode(xmldoc, mucFlashData.deviceInfoData, encoding));

                xmldoc.Save(path);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }

        private static XmlElement CreateDataNode(XmlDocument xmldoc, DataBlock dataBlock, ByteArrayEncoding.Encoding encoding)
        {
            XmlElement ele = xmldoc.CreateElement(DataNode);
            ele.SetAttribute(StartAddressAttr, Utils.Uint32ToHexString(dataBlock.address));
            ele.SetAttribute(SizeAttr, Utils.Uint32ToHexString(dataBlock.size));
            ele.SetAttribute(EndAddressAttr, Utils.Uint32ToHexString(dataBlock.endAddress));
            ele.SetAttribute(EncodingAttr, ByteArrayEncoding.EncodingToString(encoding));
            ele.InnerText = ByteArrayEncoding.Encode(dataBlock.data, encoding);

            return ele;
        }

        public static VKxMCUFlashData DeseralizeFromBuffer(byte[] data)
        {
            return null;
        }
    }

    public class VKxSMCUFlashData
    {
        public const int FLASH_END_ADDR = 0x08020000;
        public const uint DEVICE_INFO_DATA_START_ADDR = 0x0801FC00;
        public const uint SYSTEM_COMMON_DATA_START_ADDR = 0x0801F800;
        public const uint VKS_PRESET_DATA_START_ADDR = 0x0801B000;
        public const uint X5_PRESET_DATA_START_ADDR = 0x08017000;

        public const uint PRESET_DATA_START_ADDR = X5_PRESET_DATA_START_ADDR;
        public const uint PRESET_DATA_END_ADDR = SYSTEM_COMMON_DATA_START_ADDR;
        public const uint PRESET_DATA_LENGTH = PRESET_DATA_END_ADDR - PRESET_DATA_START_ADDR;

        public readonly DataBlock x5_resetsData = new DataBlock(X5_PRESET_DATA_START_ADDR, Utils.CreateByteArray(VKS_PRESET_DATA_START_ADDR - X5_PRESET_DATA_START_ADDR));
        public readonly DataBlock presetsData = new DataBlock(VKS_PRESET_DATA_START_ADDR, Utils.CreateByteArray(SYSTEM_COMMON_DATA_START_ADDR - VKS_PRESET_DATA_START_ADDR));
        public readonly DataBlock systemCommonData = new DataBlock(SYSTEM_COMMON_DATA_START_ADDR, Utils.CreateByteArray(DEVICE_INFO_DATA_START_ADDR - SYSTEM_COMMON_DATA_START_ADDR));
        public readonly DataBlock deviceInfoData = new DataBlock(DEVICE_INFO_DATA_START_ADDR, Utils.CreateByteArray(FLASH_END_ADDR - DEVICE_INFO_DATA_START_ADDR));

        //public void SeralizeToFile(string path, )
    }

    public class VKxSMCUFlashDataSeralizer
    {
        private const string ModelAttr = "model";
        private const string Model = "VKxS";
        private const string RootNode = "mcu_flash_data";
        private const string DataNode = "data";

        private const string StartAddressAttr = "start_address";
        private const string SizeAttr = "size";
        private const string EndAddressAttr = "end_address";
        private const string EncodingAttr = "encoding";

        public static void SeralizeToFile(VKxSMCUFlashData mucFlashData, ByteArrayEncoding.Encoding encoding, string path)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlDeclaration xmldecl;
                xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmldoc.AppendChild(xmldecl);

                //加入根节点 lmAEC
                XmlElement rootEle = xmldoc.CreateElement(RootNode);
                rootEle.SetAttribute(ModelAttr, Model);

                xmldoc.AppendChild(rootEle);

                rootEle.AppendChild(CreateDataNode(xmldoc, mucFlashData.x5_resetsData, encoding));
                rootEle.AppendChild(CreateDataNode(xmldoc, mucFlashData.presetsData, encoding));
                rootEle.AppendChild(CreateDataNode(xmldoc, mucFlashData.systemCommonData, encoding));
                rootEle.AppendChild(CreateDataNode(xmldoc, mucFlashData.deviceInfoData, encoding));

                xmldoc.Save(path);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }

        private static XmlElement CreateDataNode(XmlDocument xmldoc, DataBlock dataBlock, ByteArrayEncoding.Encoding encoding)
        {
            XmlElement ele = xmldoc.CreateElement(DataNode);

            ele.SetAttribute(StartAddressAttr, Utils.Uint32ToHexString(dataBlock.address));
            ele.SetAttribute(SizeAttr, Utils.Uint32ToHexString(dataBlock.size));
            ele.SetAttribute(EndAddressAttr, Utils.Uint32ToHexString(dataBlock.endAddress));
            ele.SetAttribute(EncodingAttr, ByteArrayEncoding.EncodingToString(encoding));
            ele.InnerText = ByteArrayEncoding.Encode(dataBlock.data, encoding);

            return ele;
        }

        public static VKxSMCUFlashData DeseralizeFromFile(string path)
        {
            return null;
        }
    }

    public class X5ProMCUFlashData
    {
        public const int FLASH_END_ADDR = 0x08020000;
        public const uint DEVICE_INFO_DATA_START_ADDR = 0x0801FC00;
        public const uint SYSTEM_COMMON_DATA_START_ADDR = 0x0801F800;
        public const uint PRESET_DATA_START_ADDR = 0x0801B800;

        public const uint PRESET_DATA_END_ADDR = SYSTEM_COMMON_DATA_START_ADDR;
        public const uint PRESET_DATA_LENGTH = SYSTEM_COMMON_DATA_START_ADDR - PRESET_DATA_START_ADDR;

        public readonly DataBlock presetsData = new DataBlock(PRESET_DATA_START_ADDR, Utils.CreateByteArray(SYSTEM_COMMON_DATA_START_ADDR - PRESET_DATA_START_ADDR));
        public readonly DataBlock systemCommonData = new DataBlock(SYSTEM_COMMON_DATA_START_ADDR, Utils.CreateByteArray(DEVICE_INFO_DATA_START_ADDR - SYSTEM_COMMON_DATA_START_ADDR));
        public readonly DataBlock deviceInfoData = new DataBlock(DEVICE_INFO_DATA_START_ADDR, Utils.CreateByteArray(FLASH_END_ADDR - DEVICE_INFO_DATA_START_ADDR));

        //public void SeralizeToFile(string path, )
    }

    public class X5ProMCUFlashDataSeralizer
    {
        private const string ModelAttr = "model";
        private const string Model = "X5Pro";
        private const string RootNode = "mcu_flash_data";
        private const string DataNode = "data";

        private const string StartAddressAttr = "start_address";
        private const string SizeAttr = "size";
        private const string EndAddressAttr = "end_address";
        private const string EncodingAttr = "encoding";

        public static void SeralizeToFile(X5ProMCUFlashData mucFlashData, ByteArrayEncoding.Encoding encoding, string path)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlDeclaration xmldecl;
                xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmldoc.AppendChild(xmldecl);

                //加入根节点 lmAEC
                XmlElement rootEle = xmldoc.CreateElement(RootNode);
                rootEle.SetAttribute(ModelAttr, Model);

                xmldoc.AppendChild(rootEle);

                rootEle.AppendChild(CreateDataNode(xmldoc, mucFlashData.presetsData, encoding));
                rootEle.AppendChild(CreateDataNode(xmldoc, mucFlashData.systemCommonData, encoding));
                rootEle.AppendChild(CreateDataNode(xmldoc, mucFlashData.deviceInfoData, encoding));

                xmldoc.Save(path);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }

        private static XmlElement CreateDataNode(XmlDocument xmldoc, DataBlock dataBlock, ByteArrayEncoding.Encoding encoding)
        {
            XmlElement ele = xmldoc.CreateElement(DataNode);
            ele.SetAttribute(StartAddressAttr, Utils.Uint32ToHexString(dataBlock.address));
            ele.SetAttribute(SizeAttr, Utils.Uint32ToHexString(dataBlock.size));
            ele.SetAttribute(EndAddressAttr, Utils.Uint32ToHexString(dataBlock.endAddress));
            ele.SetAttribute(EncodingAttr, ByteArrayEncoding.EncodingToString(encoding));
            ele.InnerText = ByteArrayEncoding.Encode(dataBlock.data, encoding);

            return ele;
        }

        public static X5ProMCUFlashData DeseralizeFromBuffer(byte[] data)
        {
            return null;
        }
    }

    public class QxMCUFlashData
    {
        public const int FLASH_END_ADDR = 0x08100000;
        public const uint DEVICE_INFO_DATA_START_ADDR = 0x080FF000;
        public const uint DEVICE_CUSTOM_MODULE_START_ADDR = 0x080FE000;
        public const uint SYSTEM_COMMON_DATA_START_ADDR = 0x080FD000;
        public const uint PRESET_DATA_START_ADDR = 0x080F4000;

        public const uint PRESET_DATA_END_ADDR = SYSTEM_COMMON_DATA_START_ADDR;
        public const uint PRESET_DATA_LENGTH = SYSTEM_COMMON_DATA_START_ADDR - PRESET_DATA_START_ADDR;

        public readonly DataBlock presetsData = new DataBlock(PRESET_DATA_START_ADDR, Utils.CreateByteArray(SYSTEM_COMMON_DATA_START_ADDR - PRESET_DATA_START_ADDR));
        public readonly DataBlock systemCommonData = new DataBlock(SYSTEM_COMMON_DATA_START_ADDR, Utils.CreateByteArray(DEVICE_CUSTOM_MODULE_START_ADDR - SYSTEM_COMMON_DATA_START_ADDR));
        public readonly DataBlock deviceCustomData = new DataBlock(DEVICE_CUSTOM_MODULE_START_ADDR, Utils.CreateByteArray(DEVICE_INFO_DATA_START_ADDR - DEVICE_CUSTOM_MODULE_START_ADDR));
        public readonly DataBlock deviceInfoData = new DataBlock(DEVICE_INFO_DATA_START_ADDR, Utils.CreateByteArray(FLASH_END_ADDR - DEVICE_INFO_DATA_START_ADDR));

        //public void SeralizeToFile(string path, )
    }

    public class QxMCUFlashDataSeralizer
    {
        private const string ModelAttr = "model";
        private const string Model = "Qx";
        private const string RootNode = "mcu_flash_data";
        private const string DataNode = "data";
        //private const string AddressAttr = "address";

        private const string StartAddressAttr = "start_address";
        private const string SizeAttr = "size";
        private const string EndAddressAttr = "end_address";
        private const string EncodingAttr = "encoding";

        public static void SeralizeToFile(QxMCUFlashData mucFlashData, ByteArrayEncoding.Encoding encoding, string path)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlDeclaration xmldecl;
                xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmldoc.AppendChild(xmldecl);

                //加入根节点 lmAEC
                XmlElement rootEle = xmldoc.CreateElement(RootNode);
                rootEle.SetAttribute(ModelAttr, Model);

                xmldoc.AppendChild(rootEle);

                rootEle.AppendChild(CreateDataNode(xmldoc, mucFlashData.presetsData, encoding));
                rootEle.AppendChild(CreateDataNode(xmldoc, mucFlashData.systemCommonData, encoding));
                rootEle.AppendChild(CreateDataNode(xmldoc, mucFlashData.deviceCustomData, encoding));
                rootEle.AppendChild(CreateDataNode(xmldoc, mucFlashData.deviceInfoData, encoding));

                xmldoc.Save(path);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }

        private static XmlElement CreateDataNode(XmlDocument xmldoc, DataBlock dataBlock, ByteArrayEncoding.Encoding encoding)
        {
            XmlElement ele = xmldoc.CreateElement(DataNode);
            ele.SetAttribute(StartAddressAttr, Utils.Uint32ToHexString(dataBlock.address));
            ele.SetAttribute(SizeAttr, Utils.Uint32ToHexString(dataBlock.size));
            ele.SetAttribute(EndAddressAttr, Utils.Uint32ToHexString(dataBlock.endAddress));
            ele.SetAttribute(EncodingAttr, ByteArrayEncoding.EncodingToString(encoding));
            ele.InnerText = ByteArrayEncoding.Encode(dataBlock.data, encoding);

            return ele;
        }

        public static QxMCUFlashData DeseralizeFromBuffer(byte[] data)
        {
            return null;
        }
    }
}
