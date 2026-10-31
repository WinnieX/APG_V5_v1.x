using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace AEC.Comm.FirmwareUpdate
{
    public class DSPFirmwarePackage
    {
        public DSPFirmwareInfo dspFirmwareInfo {get; private set; }
        public byte[] dspFirmwareData { get; private set; }

        public DSPFirmwarePackage(DSPFirmwareInfo dspFirmwareInfo_, byte[] dspFirmwareData_)
        {
            Debug.Assert(dspFirmwareInfo_ != null && dspFirmwareData_ != null);

            this.dspFirmwareInfo = dspFirmwareInfo_;
            this.dspFirmwareData = dspFirmwareData_;
        }
    }

    public class DSPFirmwareInfoSeralizer
    {
        public const string DEF_MDF_FILE_FILTER = "mdf File|*.mdf";
        public const string MDF_FILE_EXTENSION = ".mdf";

        private const string VersionAttr = "version";
        private const string IsTpmEncryptedAttr = "is_tpm_encrypted";
        private const string LastModifiedTimeStampAttr = "lastModifiedTimeStamp";
        private const string FileNameAttr = "file_name";
        private const string CommentsAttr = "comments";
        private const string HashAttr = "hash";

        private const string RootNode = "mdf";

        public static void SeralizeToFile(DSPFirmwarePackage fp, string path)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlDeclaration xmldecl;
            xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmldoc.AppendChild(xmldecl);

            XmlElement rootEle = xmldoc.CreateElement(RootNode);
            rootEle.SetAttribute(VersionAttr, Utils.Uint32ToHexString(fp.dspFirmwareInfo.version));
            rootEle.SetAttribute(IsTpmEncryptedAttr, Utils.BoolToString(fp.dspFirmwareInfo.isTpmEncrypted));
            rootEle.SetAttribute(LastModifiedTimeStampAttr, Utils.Uint32ToHexString(fp.dspFirmwareInfo.lastModifiedTimeStamp));
            rootEle.SetAttribute(FileNameAttr, fp.dspFirmwareInfo.fileName);
            rootEle.SetAttribute(CommentsAttr, fp.dspFirmwareInfo.comments);
            rootEle.SetAttribute(HashAttr, Utils.ByteArrayToHexStr(Tools.HashHelper.SHA256(fp.dspFirmwareData), string.Empty));

            rootEle.InnerText = ByteArrayEncoding.Encode(fp.dspFirmwareData, ByteArrayEncoding.Encoding.Base64);

            xmldoc.AppendChild(rootEle);

            using(System.IO.StreamWriter sw = new System.IO.StreamWriter(path, false, new UTF8Encoding(false)))
            {
                xmldoc.Save(sw);
            }
        }

        public static DSPFirmwarePackage DeseralizeFromFile(string path, out string errorMsg)
        {
            errorMsg = string.Empty;

            using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite))
            {
                return DeseralizeFromStream(fs, out errorMsg);
            }
        }

        public static DSPFirmwarePackage DeseralizeFromStream(Stream stream, out string errorMsg)
        {
            errorMsg = string.Empty;

            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(stream);

                if (xmldoc.DocumentElement != null &&
                xmldoc.DocumentElement.LocalName == RootNode)
                {
                    XmlElement rootNode = xmldoc.DocumentElement;

                    byte[] data = ByteArrayEncoding.Decode(rootNode.InnerText, ByteArrayEncoding.Encoding.Base64);

                    DSPFirmwarePackage dsp_fw_package = null;
                    using(MemoryStream ms = new MemoryStream(data))
                    {
                        dsp_fw_package = CrcImageHelper.ParseFromFile(ms);
                    }

                    if(dsp_fw_package == null)
                    {
                        errorMsg = "dsp firmware data error";
                        return null;
                    }

                    uint version;
                    if (!Utils.ParseHexStringToUint32(rootNode.GetAttribute(VersionAttr), out version))
                    {
                        errorMsg = "version data error";
                        return null;
                    }

                    bool isTpmEncripted;
                    if (!Utils.StringToBool(rootNode.GetAttribute(IsTpmEncryptedAttr), out isTpmEncripted))
                    {
                        errorMsg = "isTpmEncripted data error";
                        return null;
                    }

                    uint lastModifiedTimeStamp;
                    if (!Utils.ParseHexStringToUint32(rootNode.GetAttribute(LastModifiedTimeStampAttr), out lastModifiedTimeStamp))
                    {
                        errorMsg = "lastModifiedTimeStamp data error";
                        return null;
                    }

                    if (Utils.ByteArrayToHexStr(Tools.HashHelper.SHA256(dsp_fw_package.dspFirmwareData), string.Empty) != rootNode.GetAttribute(HashAttr))
                    {
                        errorMsg = "check data hash error";
                        return null;
                    }

                    dsp_fw_package.dspFirmwareInfo.version = version;
                    dsp_fw_package.dspFirmwareInfo.isTpmEncrypted = isTpmEncripted;
                    dsp_fw_package.dspFirmwareInfo.lastModifiedTimeStamp = lastModifiedTimeStamp;
                    dsp_fw_package.dspFirmwareInfo.fileName = rootNode.GetAttribute(FileNameAttr);
                    dsp_fw_package.dspFirmwareInfo.comments = rootNode.GetAttribute(CommentsAttr);

                    return dsp_fw_package;
                }                
            }
            catch (Exception ex)
            {
                errorMsg = string.Format("DeseralizeFromStream()...Exception:{0}", ex.Message);
                System.Diagnostics.Debug.Print(ex.Message);
            }

            return null;
        }

        //private static XmlElement CreateFirmwareBinNode(XmlDocument xmldoc, FirmwareBin firmwareBin, byte[] key, string name)
        //{
        //    XmlElement ele = xmldoc.CreateElement(FirmwareBinNode);
        //    ele.SetAttribute(NameAttr, name);

        //    if (firmwareBin != null)
        //    {
        //        ele.SetAttribute(EnabledAttr, Utils.BoolToString(firmwareBin.isDownloadBin));
        //        if (!string.IsNullOrEmpty(firmwareBin.originalName))
        //        {
        //            ele.SetAttribute(OriginalNameAttr, firmwareBin.originalName);
        //        }

        //        if (firmwareBin.binLength > 0 && firmwareBin.binData != null)
        //        {
        //            ele.SetAttribute(BinLengthAttr, Utils.Uint32ToHexString(firmwareBin.binLength));
        //            ele.SetAttribute(BinOffsetAddressAttr, Utils.Uint32ToHexString(firmwareBin.offsetAddress));
        //            ele.SetAttribute(BinTimestampAttr, Utils.Int64ToHexString(firmwareBin.binTimestamp.ToBinary()));
        //            ele.SetAttribute(BinTimeAttr, Tools.DateTimeHelper.DateTime2String(firmwareBin.binTimestamp));

        //            byte[] binHash = AEC.Tools.HashHelper.MD5(firmwareBin.binData);
        //            ele.SetAttribute(BinHashAttr, Utils.ByteArrayToHexStr(binHash, string.Empty));

        //            byte[] data = firmwareBin.binData;

        //            if (key != null)
        //            {
        //                data = AEC.Tools.EncryptHelper.AESEncrypt(firmwareBin.binData, key, binHash);
        //            }

        //            ele.InnerText = ByteArrayEncoding.Encode(data, ByteArrayEncoding.Encoding.Base64);
        //        }
        //    }

        //    return ele;
        //}

        //private static XmlElement CreateMCUFlashDataBlockNode(XmlDocument xmldoc, DataBlock mcuFlashBlock, bool enable, string name)
        //{
        //    XmlElement ele = xmldoc.CreateElement(MCUFlashDataBlockNode);
        //    ele.SetAttribute(NameAttr, name);
        //    ele.SetAttribute(EnabledAttr, Utils.BoolToString(enable));

        //    if (mcuFlashBlock != null)
        //    {
        //        ele.AppendChild(DataBlockSeralizer.CreateMCUFlashDataBlockNode(xmldoc, ByteArrayEncoding.Encoding.Base64, mcuFlashBlock));
        //    }

        //    return ele;
        //}

        //private static XmlElement CreateMCUFlashDataBlocksNode(XmlDocument xmldoc, DataBlock[] mcuFlashBlocks, bool enable, string name)
        //{
        //    XmlElement ele = xmldoc.CreateElement(MCUFlashDataBlocksNode);
        //    ele.SetAttribute(NameAttr, name);
        //    ele.SetAttribute(EnabledAttr, Utils.BoolToString(enable));

        //    if (mcuFlashBlocks != null)
        //    {
        //        for (int i = 0; i < mcuFlashBlocks.Length; ++i)
        //        {
        //            ele.AppendChild(DataBlockSeralizer.CreateMCUFlashDataBlockNode(xmldoc, ByteArrayEncoding.Encoding.Hex, mcuFlashBlocks[i]));
        //        }
        //    }

        //    return ele;
        //}

        //public static FirmwarePackage DeseralizeFromFile(string path, byte[] key, out string errorMsg)
        //{
        //    errorMsg = string.Empty;

        //    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        //    {
        //        return DeseralizeFromStream(fs, key, out errorMsg);
        //    }
        //}

        //public static FirmwarePackage DeseralizeFromStream(Stream stream, byte[] key, out string errorMsg)
        //{
        //    errorMsg = string.Empty;

        //    try
        //    {
        //        FirmwarePackage fp = new FirmwarePackage();

        //        XmlDocument xmldoc = new XmlDocument();
        //        xmldoc.Load(stream);

        //        if (xmldoc.DocumentElement != null &&
        //        xmldoc.DocumentElement.LocalName == RootNode)
        //        {
        //            XmlElement rootNode = xmldoc.DocumentElement;

        //            fp.targetModel = rootNode.GetAttribute(TargetModelAttr);
        //            fp.descirption = rootNode.GetAttribute(DescriptonAttr);

        //            uint mcuAppActivateAddress;
        //            if (!Utils.ParseHexStringToUint32(rootNode.GetAttribute(MCUAppActivateAddress), out mcuAppActivateAddress))
        //            {
        //                errorMsg = "mpf node mcu_app_activate_address attribute data error";
        //                return null;
        //            }
        //            fp.mcuAppActivateAddress = mcuAppActivateAddress;

        //            bool encription;
        //            if (Utils.StringToBool(rootNode.GetAttribute(EncriptionAttr), out encription))
        //            {
        //                if (encription)
        //                {
        //                    string key_hash = rootNode.GetAttribute(KeyHashAttr);

        //                    if (string.IsNullOrEmpty(key_hash))
        //                    {
        //                        errorMsg = "Enable ecriyption, key Hash cannot be null!";
        //                        return null;
        //                    }

        //                    //Log.OutputLine("key = {0}", Utils.ByteArrayToHexStr(key));
        //                    //Log.OutputLine("key_hash = {0}", Utils.ByteArrayToHexStr(Tools.HashHelper.SHA256(key), string.Empty));

        //                    if (key == null)
        //                    {
        //                        errorMsg = "The data has been encrypted, please enter the key";
        //                        return null;
        //                    }

        //                    if (key_hash != Utils.ByteArrayToHexStr(Tools.HashHelper.SHA256(key), string.Empty))
        //                    {
        //                        errorMsg = "The key hash does not match!";
        //                        return null;
        //                    }
        //                }
        //                else
        //                {
        //                    if (key != null)
        //                    {
        //                        errorMsg = "The firmware packet data is not encrypted, use a null key!";
        //                        return null;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                errorMsg = "Encryption attribute data error!";
        //                return null;
        //            }

        //            XmlNodeList firmwareBinNodes = rootNode.SelectNodes(FirmwareBinNode);
        //            foreach (XmlNode firmwareBinNode in firmwareBinNodes)
        //            {
        //                XmlElement ele = firmwareBinNode as XmlElement;
        //                string name = ele.GetAttribute(NameAttr);

        //                if (name == FirmwareDspBinName)
        //                {
        //                    fp.dspBin = ParseFirmwareBinNode(ele, key, out errorMsg);
        //                }
        //                else if (name == FirmwareMcuBinName)
        //                {
        //                    fp.mcuBin = ParseFirmwareBinNode(ele, key, out errorMsg);
        //                }
        //            }

        //            //if (fp.dspBin == null)
        //            //{
        //            //    errorMsg = "dspBIn 不能为空";
        //            //    return null;
        //            //}

        //            //if (fp.mcuBin == null)
        //            //{
        //            //    errorMsg = "dspBIn 不能为空";
        //            //    return null;
        //            //}

        //            XmlNodeList mcuFlashDataBlockNodes = rootNode.SelectNodes(MCUFlashDataBlockNode);
        //            foreach (XmlNode dataBlockNode in mcuFlashDataBlockNodes)
        //            {
        //                XmlElement ele = dataBlockNode as XmlElement;
        //                string name = ele.GetAttribute(NameAttr);

        //                bool enabled = false;
        //                if (!Utils.StringToBool(ele.GetAttribute(EnabledAttr), out enabled))
        //                {
        //                    errorMsg = "mcu_flash_data_block node enabled attribute data error!";
        //                    continue;
        //                }

        //                if (name == MCUAllData)
        //                {
        //                    fp.isUpdateMCUAllData = enabled;
        //                    fp.mcuAllDataBlock = ParseMCUFlashDataBlockNode(ele, out errorMsg);
        //                }
        //                else if (name == MCUCustomPresetData)
        //                {
        //                    fp.isUpdateCustomPresetData = enabled;
        //                    fp.presetDataBlock = ParseMCUFlashDataBlockNode(ele, out errorMsg);
        //                }
        //            }

        //            //if (fp.isUpdateMCUAllData && fp.mcuAllDataBlock == null)
        //            //{
        //            //    errorMsg = "使能 isUpdateMCUAllData 时 mcuAllDataBlcok 不能为空";
        //            //    return null;
        //            //}

        //            //if (fp.isUpdateCustomPresetData && fp.presetDataBlock == null)
        //            //{
        //            //    errorMsg = "使能 isUpdateCustomPresetData 时 presetDataBlock 不能为空";
        //            //    return null;
        //            //}

        //            {
        //                XmlElement mcuFlashDataBlocksEle = rootNode.SelectSingleNode(MCUFlashDataBlocksNode) as XmlElement;
        //                string name = mcuFlashDataBlocksEle.GetAttribute(NameAttr);
        //                if (name == MCUCustomDeviceInfo)
        //                {
        //                    bool enabled = false;
        //                    if (!Utils.StringToBool(mcuFlashDataBlocksEle.GetAttribute(EnabledAttr), out enabled))
        //                    {
        //                        errorMsg = "mcu_flash_data_blocks node enabled attribute data error!";
        //                        return null;
        //                    }

        //                    fp.isUpdateCustomDeviceInfo = enabled;
        //                    fp.customDeviceInfoDataBlocks = ParseMCUFlashDataBlocksNode(mcuFlashDataBlocksEle, out errorMsg);
        //                }

        //                //if (fp.isUpdateCustomDeviceInfo && fp.customDeviceInfoDataBlocks == null)
        //                //{
        //                //    errorMsg = "使能 isUpdateCustomDeviceInfo 时 customDeviceInfoDataBlocks 不能为空";
        //                //    return null;
        //                //}
        //            }
        //        }

        //        if (!fp.CheckIsOK(out errorMsg))
        //        {
        //            return null;
        //        }

        //        return fp;
        //    }
        //    catch (Exception ex)
        //    {
        //        errorMsg = string.Format("DeseralizeFromStream()...Exception:{0}", ex.Message);
        //        System.Diagnostics.Debug.Print(ex.Message);
        //    }

        //    return null;
        //}

        //private static FirmwareBin ParseFirmwareBinNode(XmlElement firmwareBinNode, byte[] key, out string errorMsg)
        //{
        //    System.Diagnostics.Debug.Assert(firmwareBinNode != null);

        //    errorMsg = string.Empty;

        //    FirmwareBin firmwareBin = new FirmwareBin();

        //    bool enabled;
        //    if (!Utils.StringToBool(firmwareBinNode.GetAttribute(EnabledAttr), out enabled))
        //    {
        //        errorMsg = "firmware_bin node enabled attribute data error";
        //        return null;
        //    }

        //    firmwareBin.isDownloadBin = enabled;

        //    firmwareBin.originalName = firmwareBinNode.GetAttribute(OriginalNameAttr);

        //    if (enabled)
        //    {
        //        uint binLength;
        //        if (!Utils.ParseHexStringToUint32(firmwareBinNode.GetAttribute(BinLengthAttr), out binLength))
        //        {
        //            errorMsg = "firmware_bin node bin_length attribute data error";
        //            return null;
        //        }
        //        firmwareBin.binLength = binLength;

        //        uint offsetAddress;
        //        if (!Utils.ParseHexStringToUint32(firmwareBinNode.GetAttribute(BinOffsetAddressAttr), out offsetAddress))
        //        {
        //            errorMsg = "firmware_bin node bin_offset_address attribute data error";
        //            return null;
        //        }
        //        firmwareBin.offsetAddress = offsetAddress;

        //        long timestamp;
        //        if (Utils.ParseHexStringToInt64(firmwareBinNode.GetAttribute(BinTimestampAttr), out timestamp))
        //        {
        //            firmwareBin.binTimestamp = DateTime.FromBinary(timestamp);
        //        }

        //        byte[] binHash = Utils.HexStringTorBytes(firmwareBinNode.GetAttribute(BinHashAttr), out errorMsg);
        //        if (binHash == null)
        //        {
        //            errorMsg = "firmware_bin node bin_hash attribute data error";
        //            return null;
        //        }

        //        if (binHash.Length != KeyLength)
        //        {
        //            errorMsg = "firmware_bin node bin_length attribute data error";
        //            return null;
        //        }

        //        byte[] data = null;

        //        try
        //        {
        //            data = ByteArrayEncoding.Decode((firmwareBinNode as XmlElement).InnerText, ByteArrayEncoding.Encoding.Base64);
        //            if (key != null)
        //            {
        //                data = AEC.Tools.EncryptHelper.AESDecrypt(data, key, binHash);
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            errorMsg = "firmware_bin node data error!";
        //            return null;
        //        }

        //        System.Diagnostics.Debug.Assert(data != null);

        //        if (data.Length < (int)firmwareBin.binLength)
        //        {
        //            errorMsg = "firmware_bin node data length does not match!";
        //            return null;
        //        }

        //        if (data.Length > (int)firmwareBin.binLength)
        //        {
        //            Array.Resize(ref data, (int)firmwareBin.binLength);
        //        }

        //        if (!Utils.CompareByteArray(AEC.Tools.HashHelper.MD5(data), binHash))
        //        {
        //            errorMsg = "The hash verification of the firmware_bin node data fails";
        //            return null;
        //        }

        //        firmwareBin.binData = data;
        //    }

        //    return firmwareBin;
        //}

        //private static DataBlock ParseMCUFlashDataBlockNode(XmlElement mcuFlashDataBlockNode, out string errorMsg)
        //{
        //    System.Diagnostics.Debug.Assert(mcuFlashDataBlockNode != null);

        //    errorMsg = string.Empty;

        //    try
        //    {
        //        XmlElement dataBlockEle = mcuFlashDataBlockNode.SelectSingleNode(DataBlockSeralizer.DataBlockNodeName) as XmlElement;
        //        if (dataBlockEle != null)
        //        {
        //            ByteArrayEncoding.Encoding encoding = ByteArrayEncoding.Encoding.Base64;
        //            return DataBlockSeralizer.ParseDataBlockNode(dataBlockEle, out encoding);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Tools.LogHelper.WriteDebug("ParseMCUFlashDataBlockNode()...Exception:{0}", e.Message);
        //    }

        //    return null;
        //}

        //private static DataBlock[] ParseMCUFlashDataBlocksNode(XmlElement mcuFlashDataBlocksNode, out string errorMsg)
        //{
        //    System.Diagnostics.Debug.Assert(mcuFlashDataBlocksNode != null);

        //    errorMsg = string.Empty;

        //    try
        //    {
        //        List<DataBlock> list = new List<DataBlock>();
        //        XmlNodeList dataBlockNodes = mcuFlashDataBlocksNode.SelectNodes(DataBlockSeralizer.DataBlockNodeName);
        //        foreach (XmlNode dataBlockNode in dataBlockNodes)
        //        {
        //            XmlElement ele = dataBlockNode as XmlElement;

        //            ByteArrayEncoding.Encoding encoding = ByteArrayEncoding.Encoding.Base64;
        //            list.Add(DataBlockSeralizer.ParseDataBlockNode(ele, out encoding));
        //        }

        //        return list.ToArray();
        //    }
        //    catch (Exception e)
        //    {
        //        Tools.LogHelper.WriteDebug("ParseMCUFlashDataBlocksNode()...Exception:{0}", e.Message);
        //    }

        //    return null;
        //}
    }
}
