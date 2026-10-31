using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using AEC.Misc;

namespace AEC.Comm.FirmwareUpdate
{
    public class FirmwarePackage
    {
        public const string DefBluetoothName = "MDSP_";

        private static Localization localization = Config.GetInstance().localization;

        public string targetModel { get; set; }
        public string descirption { get; set; }

        public string mcuModel { get; set; }

        public bool isForcedUpdateBTInfo { get; set; }
        public bool isRetainBTDeviceInfo { get; set; }
        public bool isEnableRandomBTAddress { get; set; }
        public string BTName { get; set; }

        public FirmwareBin dspBin { get; set; }
        public FirmwareBin mcuBin { get; set; }

        //public uint mcuAppAddress { get; set; }
        public uint mcuAppActivateAddress { get; set; }

        public bool isUpdateMCUAllData { get; set; }
        public DataBlock mcuAllDataBlock { get; set; }

        public bool isUpdateCustomDeviceInfo { get; set; }
        public DataBlock[] customDeviceInfoDataBlocks { get; set; }

        public bool isUpdateCustomPresetData { get; set; }
        public DataBlock presetDataBlock { get; set; }

        public FirmwarePackage()
        {
            this.mcuModel = string.Empty;
            this.BTName = DefBluetoothName;
        }

        public DataBlock GetMergeDataBlock()
        {
            try
            {
                List<DataBlock> list = new List<DataBlock>();
                if (isUpdateMCUAllData)
                {
                    System.Diagnostics.Debug.Assert(this.mcuAllDataBlock != null);

                    DataBlock all = this.mcuAllDataBlock.Clone();

                    if (isUpdateCustomDeviceInfo)
                    {
                        System.Diagnostics.Debug.Assert(this.customDeviceInfoDataBlocks != null);
                        foreach(var dataBlock in this.customDeviceInfoDataBlocks)
                        {
                            System.Diagnostics.Debug.Assert(dataBlock != null);

                            all.Update(dataBlock);
                        }
                    }

                    if(isUpdateCustomPresetData)
                    {
                        System.Diagnostics.Debug.Assert(this.presetDataBlock != null);

                        all.Update(presetDataBlock);
                    }

                    return all;
                }
                else
                {
                    if(isUpdateCustomPresetData)
                    {
                        return presetDataBlock.Clone();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch(Exception e)
            {
                Tools.LogHelper.WriteInfo("GetMergeDataBlock()...Exception:{0}", e.Message);
            }

            return null;
        }

        public bool CheckMcuAllDataBlockIsOK(out string errMsg)
        {
            errMsg = string.Empty;

            if (mcuAllDataBlock == null)
            {
                errMsg = "MCU All data cannot be null";
                return false;
            }

            return true;
        }

        public bool CheckCustomDeviceInfoDataBlocksIsOk(out string errMsg)
        {
            errMsg = string.Empty;

            if (customDeviceInfoDataBlocks == null)
            {
                errMsg = "Custom Device Info cannot be null";
                return false;
            }

            foreach (var dataBlock in customDeviceInfoDataBlocks)
            {
                if (dataBlock == null)
                {
                    errMsg = "Custom Device Info data is incorrect";
                    return false;
                }
            }

            return true;
        }

        public bool CheckPresetDataBlockIsOK(out string errMsg)
        {
            errMsg = string.Empty;

            if (presetDataBlock == null)
            {
                errMsg = "Custom Preset Data cannot be null";
                return false;
            }

            return true;
        }

        //public bool isEncrypted { get; set; }
        //public string keyHash { get; set; }

        public bool CheckIsOk()
        {
            string errMsg;
            return CheckIsOK(out errMsg);
        }

        public bool CheckIsOK(out string errMsg)
        {
            errMsg = string.Empty;

            if ((isForcedUpdateBTInfo || isEnableRandomBTAddress) && string.IsNullOrEmpty(BTName))
            {
                errMsg = "Bluetooth Name is empty";
                return false;
            }

            if(dspBin == null || !dspBin.isOk)
            {
                errMsg = "dspBin is NULL or incorrect";
                return false;
            }

            if (mcuBin == null || !mcuBin.isOk)
            {
                errMsg = "mcuBin is NULL or incorrect";
                return false;
            }
            
            if (isUpdateMCUAllData)
            {
                if (!CheckMcuAllDataBlockIsOK(out errMsg))
                {
                    return false;
                }
            }

            if (isUpdateCustomDeviceInfo)
            {
                if (!CheckCustomDeviceInfoDataBlocksIsOk(out errMsg))
                {
                    return false;
                }
            }

            if (isUpdateCustomPresetData)
            {
                if (!CheckPresetDataBlockIsOK(out errMsg))
                {
                    return false;
                }
            }

            if (!isUpdateMCUAllData && isUpdateCustomDeviceInfo)
            {
                errMsg = "Enable UpdateCustomDeviceInfo must enable UpdateMCUAllData";
                return false;
            }

            //if(isEncrypted)
            //{
            //    if (string.IsNullOrEmpty(targetModel))
            //    {
            //        errMsg = "密钥hash不能为空";
            //        return false;
            //    }
            //}

            return true;
        }

        public void AppendBaseInfoToSB(StringBuilder sb)
        {
            if (!string.IsNullOrEmpty(this.targetModel))
            {
                Tools.StringHelper.StringBuilderAppendLine(sb, "Target Model: {0}", this.targetModel);
            }

            if (!string.IsNullOrEmpty(this.descirption))
            {
                Tools.StringHelper.StringBuilderAppendLine(sb, "Description: {0}", this.descirption);
            }

            if(!string.IsNullOrEmpty(this.mcuModel))
            {
                Tools.StringHelper.StringBuilderAppendLine(sb, "MCU Model: {0}", this.mcuModel);
            }

            Tools.StringHelper.StringBuilderAppendLine(sb, "Update Bluetooth Info: {0}", BoolToEnableString(this.isForcedUpdateBTInfo));
            Tools.StringHelper.StringBuilderAppendLine(sb, "Retain Bluetooth Info: {0}", BoolToEnableString(this.isRetainBTDeviceInfo));
            Tools.StringHelper.StringBuilderAppendLine(sb, "Random Bluetooth Address: {0}", BoolToEnableString(this.isEnableRandomBTAddress));

            if (!string.IsNullOrEmpty(this.BTName))
            {
                Tools.StringHelper.StringBuilderAppendLine(sb, "Bluetooth Name: {0}", this.BTName);
            }

            sb.AppendLine(string.Empty);
        }

        public void AppendToSB(StringBuilder sb)
        {
            if(!this.CheckIsOk())
            {
                sb.Append(localization[Localization.firmwareUpdate_DataIsInvalid]);
                return;
            }

            this.AppendBaseInfoToSB(sb);

            if(this.dspBin != null)
            {
                Tools.StringHelper.StringBuilderAppendLine(sb, this.dspBin.ToString("DSP"));
                sb.AppendLine(string.Empty);
            }

            if (this.mcuBin != null)
            {
                Tools.StringHelper.StringBuilderAppendLine(sb, this.mcuBin.ToString("MCU APP"));
                sb.AppendLine(string.Empty);
            }

            Tools.StringHelper.StringBuilderAppendLine(sb, localization[Localization.firmwareUpdate_WhetherToUpdateFlashAllData], Utils.BoolToYesOrNo(this.isUpdateMCUAllData));
            if (this.mcuAllDataBlock != null)
            {
                Tools.StringHelper.StringBuilderAppendLine(sb, localization[Localization.firmwareUpdate_FlashAllData], Utils.Uint32ToHexString(this.mcuAllDataBlock.address), Utils.Uint32ToHexString(this.mcuAllDataBlock.size));
                sb.AppendLine(string.Empty);
            }

            Tools.StringHelper.StringBuilderAppendLine(sb, localization[Localization.firmwareUpdate_WhetherToUpdateCustomDeviceInfo], Utils.BoolToYesOrNo(this.isUpdateCustomDeviceInfo));
            if (this.customDeviceInfoDataBlocks != null)
            {
                for (int i = 0; i < this.customDeviceInfoDataBlocks.Length; ++i )
                {
                    if (this.customDeviceInfoDataBlocks[i] != null)
                    {
                        Tools.StringHelper.StringBuilderAppendLine(sb, localization[Localization.firmwareUpdate_CustomDeviceInfo], i + 1, Utils.Uint32ToHexString(this.customDeviceInfoDataBlocks[i].address), Utils.Uint32ToHexString(this.customDeviceInfoDataBlocks[i].size));
                    }
                }

                sb.AppendLine(string.Empty);
            }

            Tools.StringHelper.StringBuilderAppendLine(sb, localization[Localization.firmwareUpdate_WhetherToUpdatePresetData], Utils.BoolToYesOrNo(this.isUpdateCustomPresetData));
            if (this.presetDataBlock != null)
            {
                Tools.StringHelper.StringBuilderAppendLine(sb, localization[Localization.firmwareUpdate_PresetData], Utils.Uint32ToHexString(this.presetDataBlock.address), Utils.Uint32ToHexString(this.presetDataBlock.size));
            }
        }

        private static string BoolToEnableString(bool enable)
        {
            return enable ? "Enabled" : "Disabled";
        }
    }

    public class FirmwarePackageSeralizer
    {
        public const int KeyLength = 0x10;

        public const string DEF_MFP_FILE_FILTER = "mfp File|*.mfp";
        public const string MFP_FILE_EXTENSION = ".mfp";

        private const string TargetModelAttr = "target_model";
        private const string MCUModelAttr = "mcu_model";
        private const string MCUModelHashAttr = "mcu_model_hash";
        private const string MCUAppActivateAddress = "mcu_app_activate_address";
        private const string DescriptonAttr = "description";
        private const string IsForcedUpdateBTInfo = "forced_update_bt_info";
        private const string IsRetainBTInfo = "retain_bt_info";
        private const string IsEnableRandomBTAddress = "enable_random_bt_address";
        private const string BTName = "bt_name";
        private const string EncriptionAttr = "encription";
        private const string KeyHashAttr = "key_hash";
        
        private const string RootNode = "mfp";

        private const string FirmwareBinNode = "firmware_bin";
        private const string FirmwareDspBinName = "dsp_bin";
        private const string FirmwareMcuBinName = "mcu_bin";

        private const string NameAttr = "name";
        private const string EnabledAttr = "enabled";
        private const string OriginalNameAttr = "original_name";
        private const string BinLengthAttr = "bin_length";
        private const string BinOffsetAddressAttr = "bin_offset_address";
        private const string BinTimestampAttr = "bin_timestamp";
        private const string BinTimeAttr = "bin_time";
        private const string BinHashAttr = "bin_hash";

        private const string MCUFlashDataBlockNode = "mcu_flash_data_block";
        private const string MCUFlashDataBlocksNode = "mcu_flash_data_blocks";

        private const string MCUAllData = "mcu_all_data";
        private const string MCUCustomDeviceInfo = "mcu_custom_device_info_data";
        private const string MCUCustomPresetData = "mcu_custom_preset_data";

        public static void SeralizeToFile(FirmwarePackage fp, byte[] key, string path)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlDeclaration xmldecl;
            xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmldoc.AppendChild(xmldecl);

            XmlElement rootEle = xmldoc.CreateElement(RootNode);
            rootEle.SetAttribute(TargetModelAttr, fp.targetModel);
            rootEle.SetAttribute(DescriptonAttr, fp.descirption);

            if(!string.IsNullOrEmpty(fp.mcuModel))
            {
                rootEle.SetAttribute(MCUModelAttr, fp.mcuModel);
                rootEle.SetAttribute(MCUModelHashAttr, Tools.HashHelper.GetStringMD5(fp.mcuModel));
            }

            rootEle.SetAttribute(IsForcedUpdateBTInfo, Utils.BoolToString(fp.isForcedUpdateBTInfo));
            rootEle.SetAttribute(IsRetainBTInfo, Utils.BoolToString(fp.isRetainBTDeviceInfo));
            rootEle.SetAttribute(IsEnableRandomBTAddress, Utils.BoolToString(fp.isEnableRandomBTAddress));
            rootEle.SetAttribute(BTName, fp.BTName);
            rootEle.SetAttribute(MCUAppActivateAddress, Utils.Uint32ToHexString(fp.mcuAppActivateAddress));

            rootEle.SetAttribute(EncriptionAttr, Utils.BoolToString(key != null));
            if(key != null)
            {
                if (key.Length != KeyLength)
                {
                    throw new Exception("Key length incorrect!");
                }

                //Log.OutputLine("key = {0}", Utils.ByteArrayToHexStr(key));
                //Log.OutputLine("key_hash = {0}", Utils.ByteArrayToHexStr(Tools.HashHelper.SHA256(key)));

                rootEle.SetAttribute(KeyHashAttr, Utils.ByteArrayToHexStr(Tools.HashHelper.SHA256(key), string.Empty));
            }
            
            xmldoc.AppendChild(rootEle);

            rootEle.AppendChild(CreateFirmwareBinNode(xmldoc, fp.dspBin, key, FirmwareDspBinName));
            rootEle.AppendChild(CreateFirmwareBinNode(xmldoc, fp.mcuBin, key, FirmwareMcuBinName));
            rootEle.AppendChild(CreateMCUFlashDataBlockNode(xmldoc, fp.mcuAllDataBlock, fp.isUpdateMCUAllData, MCUAllData));
            rootEle.AppendChild(CreateMCUFlashDataBlocksNode(xmldoc, fp.customDeviceInfoDataBlocks, fp.isUpdateCustomDeviceInfo, MCUCustomDeviceInfo));
            rootEle.AppendChild(CreateMCUFlashDataBlockNode(xmldoc, fp.presetDataBlock, fp.isUpdateCustomPresetData, MCUCustomPresetData));
            //rootEle.AppendChild(CreateDataNode(xmldoc, mucFlashData.systemCommonData));
            //rootEle.AppendChild(CreateDataNode(xmldoc, mucFlashData.deviceCustomData));
            //rootEle.AppendChild(CreateDataNode(xmldoc, mucFlashData.deviceInfoData));

            System.IO.StreamWriter sw = new System.IO.StreamWriter(path, false, new UTF8Encoding(false));
            xmldoc.Save(sw);
            sw.Close();
        }

        private static XmlElement CreateFirmwareBinNode(XmlDocument xmldoc, FirmwareBin firmwareBin, byte[] key, string name)
        {
            XmlElement ele = xmldoc.CreateElement(FirmwareBinNode);
            ele.SetAttribute(NameAttr, name);

            if(firmwareBin != null)
            {
                ele.SetAttribute(EnabledAttr, Utils.BoolToString(firmwareBin.isDownloadBin));
                if (!string.IsNullOrEmpty(firmwareBin.originalName))
                {
                    ele.SetAttribute(OriginalNameAttr, firmwareBin.originalName);
                }

                if (firmwareBin.binLength > 0 && firmwareBin.binData != null)
                {
                    ele.SetAttribute(BinLengthAttr, Utils.Uint32ToHexString(firmwareBin.binLength));
                    ele.SetAttribute(BinOffsetAddressAttr, Utils.Uint32ToHexString(firmwareBin.offsetAddress));
                    ele.SetAttribute(BinTimestampAttr, Utils.Int64ToHexString(firmwareBin.binTimestamp.ToBinary()));
                    ele.SetAttribute(BinTimeAttr, Tools.DateTimeHelper.DateTime2String(firmwareBin.binTimestamp));

                    byte[] binHash = AEC.Tools.HashHelper.MD5(firmwareBin.binData);
                    ele.SetAttribute(BinHashAttr, Utils.ByteArrayToHexStr(binHash, string.Empty));

                    byte[] data = firmwareBin.binData;

                    if (key != null)
                    {
                        data = AEC.Tools.EncryptHelper.AESEncrypt(firmwareBin.binData, key, binHash);
                    }

                    ele.InnerText = ByteArrayEncoding.Encode(data, ByteArrayEncoding.Encoding.Base64);
                }                
            }

            return ele;
        }

        private static XmlElement CreateMCUFlashDataBlockNode(XmlDocument xmldoc, DataBlock mcuFlashBlock, bool enable, string name)
        {
            XmlElement ele = xmldoc.CreateElement(MCUFlashDataBlockNode);
            ele.SetAttribute(NameAttr, name);
            ele.SetAttribute(EnabledAttr, Utils.BoolToString(enable));

            if (mcuFlashBlock != null)
            {
                ele.AppendChild(DataBlockSeralizer.CreateMCUFlashDataBlockNode(xmldoc, ByteArrayEncoding.Encoding.Base64, mcuFlashBlock));
            }

            return ele;
        }

        private static XmlElement CreateMCUFlashDataBlocksNode(XmlDocument xmldoc, DataBlock[] mcuFlashBlocks, bool enable, string name)
        {
            XmlElement ele = xmldoc.CreateElement(MCUFlashDataBlocksNode);
            ele.SetAttribute(NameAttr, name);
            ele.SetAttribute(EnabledAttr, Utils.BoolToString(enable));

            if(mcuFlashBlocks != null)
            {
                for (int i = 0; i < mcuFlashBlocks.Length; ++i)
                {
                    ele.AppendChild(DataBlockSeralizer.CreateMCUFlashDataBlockNode(xmldoc, ByteArrayEncoding.Encoding.Hex, mcuFlashBlocks[i]));
                }
            }

            return ele;
        }

        public static FirmwarePackage DeseralizeFromFile(string path, byte[] key, out string errorMsg)
        {
            errorMsg = string.Empty;

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return DeseralizeFromStream(fs, key, out errorMsg);
            }
        }

        public static FirmwarePackage DeseralizeFromStream(Stream stream, byte[] key, out string errorMsg)
        {
            errorMsg = string.Empty;

            try
            {
                FirmwarePackage fp = new FirmwarePackage();

                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(stream);

                if (xmldoc.DocumentElement != null &&
                xmldoc.DocumentElement.LocalName == RootNode)
                {
                    XmlElement rootNode = xmldoc.DocumentElement;

                    fp.targetModel = rootNode.GetAttribute(TargetModelAttr);
                    fp.descirption = rootNode.GetAttribute(DescriptonAttr);

                    fp.mcuModel = rootNode.GetAttribute(MCUModelAttr);
                    if(!string.IsNullOrEmpty(fp.mcuModel))
                    {
                        string mcuModelHash = rootNode.GetAttribute(MCUModelHashAttr);
                        if(Tools.HashHelper.GetStringMD5(fp.mcuModel) != mcuModelHash)
                        {
                            errorMsg = "mfp node mcu_model_hash attribute data does not match the mcu_model !";
                            return null;
                        }
                    }

                    fp.BTName = rootNode.GetAttribute(BTName);

                    bool bt_info_enabled = false;
                    if (Utils.StringToBool(rootNode.GetAttribute(IsForcedUpdateBTInfo), out bt_info_enabled))
                    {
                        fp.isForcedUpdateBTInfo = bt_info_enabled;
                    }

                    if (Utils.StringToBool(rootNode.GetAttribute(IsRetainBTInfo), out bt_info_enabled))
                    {
                        fp.isRetainBTDeviceInfo = bt_info_enabled;
                    }

                    if (Utils.StringToBool(rootNode.GetAttribute(IsEnableRandomBTAddress), out bt_info_enabled))
                    {
                        fp.isEnableRandomBTAddress = bt_info_enabled;
                    }

                    uint mcuAppActivateAddress;
                    if (!Utils.ParseHexStringToUint32(rootNode.GetAttribute(MCUAppActivateAddress), out mcuAppActivateAddress))
                    {
                        errorMsg = "mfp node mcu_app_activate_address attribute data error";
                        return null;
                    }
                    fp.mcuAppActivateAddress = mcuAppActivateAddress;

                    bool encription;
                    if (Utils.StringToBool(rootNode.GetAttribute(EncriptionAttr), out encription))
                    {
                        if(encription)
                        {
                            string key_hash = rootNode.GetAttribute(KeyHashAttr);

                            if (string.IsNullOrEmpty(key_hash))
                            {
                                errorMsg = "Enable ecriyption, key Hash cannot be null!";
                                return null;
                            }

                            //Log.OutputLine("key = {0}", Utils.ByteArrayToHexStr(key));
                            //Log.OutputLine("key_hash = {0}", Utils.ByteArrayToHexStr(Tools.HashHelper.SHA256(key), string.Empty));

                            if(key == null)
                            {
                                errorMsg = "The data has been encrypted, please enter the key";
                                return null;
                            }

                            if (key_hash != Utils.ByteArrayToHexStr(Tools.HashHelper.SHA256(key), string.Empty))
                            {
                                errorMsg = "The key hash does not match!";
                                return null;
                            }
                        }
                        else
                        {
                            if(key != null)
                            {
                                errorMsg = "The firmware packet data is not encrypted, use a null key!";
                                return null;
                            }
                        }
                    }
                    else
                    {
                        errorMsg = "Encryption attribute data error!";
                        return null;
                    }

                    XmlNodeList firmwareBinNodes = rootNode.SelectNodes(FirmwareBinNode);
                    foreach(XmlNode firmwareBinNode in firmwareBinNodes)
                    {
                        XmlElement ele = firmwareBinNode as XmlElement;
                        string name = ele.GetAttribute(NameAttr);

                        if(name == FirmwareDspBinName)
                        {
                            fp.dspBin = ParseFirmwareBinNode(ele, key, out errorMsg);
                        }
                        else if(name == FirmwareMcuBinName)
                        {
                            fp.mcuBin = ParseFirmwareBinNode(ele, key, out errorMsg);
                        }
                    }

                    //if (fp.dspBin == null)
                    //{
                    //    errorMsg = "dspBIn 不能为空";
                    //    return null;
                    //}

                    //if (fp.mcuBin == null)
                    //{
                    //    errorMsg = "dspBIn 不能为空";
                    //    return null;
                    //}

                    XmlNodeList mcuFlashDataBlockNodes = rootNode.SelectNodes(MCUFlashDataBlockNode);
                    foreach (XmlNode dataBlockNode in mcuFlashDataBlockNodes)
                    {
                        XmlElement ele = dataBlockNode as XmlElement;
                        string name = ele.GetAttribute(NameAttr);

                        bool enabled = false;
                        if (!Utils.StringToBool(ele.GetAttribute(EnabledAttr), out enabled))
                        {
                            errorMsg = "mcu_flash_data_block node enabled attribute data error!";
                            continue;
                        }

                        if (name == MCUAllData)
                        {
                            fp.isUpdateMCUAllData = enabled;
                            fp.mcuAllDataBlock = ParseMCUFlashDataBlockNode(ele, out errorMsg);
                        }
                        else if (name == MCUCustomPresetData)
                        {
                            fp.isUpdateCustomPresetData = enabled;
                            fp.presetDataBlock = ParseMCUFlashDataBlockNode(ele, out errorMsg);
                        }
                    }

                    //if (fp.isUpdateMCUAllData && fp.mcuAllDataBlock == null)
                    //{
                    //    errorMsg = "使能 isUpdateMCUAllData 时 mcuAllDataBlcok 不能为空";
                    //    return null;
                    //}

                    //if (fp.isUpdateCustomPresetData && fp.presetDataBlock == null)
                    //{
                    //    errorMsg = "使能 isUpdateCustomPresetData 时 presetDataBlock 不能为空";
                    //    return null;
                    //}

                    {
                        XmlElement mcuFlashDataBlocksEle = rootNode.SelectSingleNode(MCUFlashDataBlocksNode) as XmlElement;
                        string name = mcuFlashDataBlocksEle.GetAttribute(NameAttr);
                        if(name == MCUCustomDeviceInfo)
                        {
                            bool enabled = false;
                            if (!Utils.StringToBool(mcuFlashDataBlocksEle.GetAttribute(EnabledAttr), out enabled))
                            {
                                errorMsg = "mcu_flash_data_blocks node enabled attribute data error!";
                                return null;
                            }

                            fp.isUpdateCustomDeviceInfo = enabled;
                            fp.customDeviceInfoDataBlocks = ParseMCUFlashDataBlocksNode(mcuFlashDataBlocksEle, out errorMsg);
                        }

                        //if (fp.isUpdateCustomDeviceInfo && fp.customDeviceInfoDataBlocks == null)
                        //{
                        //    errorMsg = "使能 isUpdateCustomDeviceInfo 时 customDeviceInfoDataBlocks 不能为空";
                        //    return null;
                        //}
                    }
                }

                if (!fp.CheckIsOK(out errorMsg))
                {
                    return null;
                }

                return fp;
            }
            catch (Exception ex)
            {
                errorMsg = string.Format("DeseralizeFromStream()...Exception:{0}", ex.Message);
                System.Diagnostics.Debug.Print(ex.Message);
            }

            return null;
        }

        private static FirmwareBin ParseFirmwareBinNode(XmlElement firmwareBinNode, byte[] key, out string errorMsg)
        {
            System.Diagnostics.Debug.Assert(firmwareBinNode != null);

            errorMsg = string.Empty;

            FirmwareBin firmwareBin = new FirmwareBin();

            bool enabled;
            if(!Utils.StringToBool(firmwareBinNode.GetAttribute(EnabledAttr), out enabled))
            {
                errorMsg = "firmware_bin node enabled attribute data error";
                return null;
            }

            firmwareBin.isDownloadBin = enabled;

            firmwareBin.originalName = firmwareBinNode.GetAttribute(OriginalNameAttr);
            
            if(enabled)
            {
                uint binLength;
                if(!Utils.ParseHexStringToUint32(firmwareBinNode.GetAttribute(BinLengthAttr), out binLength))
                {
                    errorMsg = "firmware_bin node bin_length attribute data error";
                    return null;
                }
                firmwareBin.binLength =  binLength;

                uint offsetAddress;
                if (!Utils.ParseHexStringToUint32(firmwareBinNode.GetAttribute(BinOffsetAddressAttr), out offsetAddress))
                {
                    errorMsg = "firmware_bin node bin_offset_address attribute data error";
                    return null;
                }
                firmwareBin.offsetAddress = offsetAddress;

                long timestamp;
                if(Utils.ParseHexStringToInt64(firmwareBinNode.GetAttribute(BinTimestampAttr), out timestamp))
                {
                    firmwareBin.binTimestamp = DateTime.FromBinary(timestamp);
                }

                byte[] binHash = Utils.HexStringToBytes(firmwareBinNode.GetAttribute(BinHashAttr), out errorMsg);
                if(binHash == null)
                {
                    errorMsg = "firmware_bin node bin_hash attribute data error";
                    return null;
                }

                if (binHash.Length != KeyLength)
                {
                    errorMsg = "firmware_bin node bin_length attribute data error";
                    return null;
                }

                byte[] data = null;

                try
                {
                    data = ByteArrayEncoding.Decode((firmwareBinNode as XmlElement).InnerText, ByteArrayEncoding.Encoding.Base64);
                    if(key != null)
                    {
                        data = AEC.Tools.EncryptHelper.AESDecrypt(data, key, binHash);
                    }
                }
                catch(Exception )
                {
                    errorMsg = "firmware_bin node data error!";
                    return null;
                }

                System.Diagnostics.Debug.Assert(data != null);

                if (data.Length < (int)firmwareBin.binLength)
                {
                    errorMsg = "firmware_bin node data length does not match!";
                    return null;
                }

                if(data.Length > (int)firmwareBin.binLength)
                {
                    Array.Resize(ref data, (int)firmwareBin.binLength);
                }

                if (!Utils.CompareByteArray(AEC.Tools.HashHelper.MD5(data), binHash))
                {
                    errorMsg = "The hash verification of the firmware_bin node data fails";
                    return null;
                }

                firmwareBin.binData = data;
            }

            return firmwareBin;
        }

        private static DataBlock ParseMCUFlashDataBlockNode(XmlElement mcuFlashDataBlockNode, out string errorMsg)
        {
            System.Diagnostics.Debug.Assert(mcuFlashDataBlockNode != null);

            errorMsg = string.Empty;

            try
            {
                XmlElement dataBlockEle = mcuFlashDataBlockNode.SelectSingleNode(DataBlockSeralizer.DataBlockNodeName) as XmlElement;
                if(dataBlockEle != null)
                {
                    ByteArrayEncoding.Encoding encoding = ByteArrayEncoding.Encoding.Base64;
                    return DataBlockSeralizer.ParseDataBlockNode(dataBlockEle, out encoding);
                }
            }
            catch(Exception e)
            {
                Tools.LogHelper.WriteDebug("ParseMCUFlashDataBlockNode()...Exception:{0}", e.Message);
            }

            return null;
        }

        private static DataBlock[] ParseMCUFlashDataBlocksNode(XmlElement mcuFlashDataBlocksNode, out string errorMsg)
        {
            System.Diagnostics.Debug.Assert(mcuFlashDataBlocksNode != null);

            errorMsg = string.Empty;

            try
            {
                List<DataBlock> list = new List<DataBlock>();
                XmlNodeList dataBlockNodes = mcuFlashDataBlocksNode.SelectNodes(DataBlockSeralizer.DataBlockNodeName);
                foreach (XmlNode dataBlockNode in dataBlockNodes)
                {
                    XmlElement ele = dataBlockNode as XmlElement;

                    ByteArrayEncoding.Encoding encoding = ByteArrayEncoding.Encoding.Base64;
                    list.Add(DataBlockSeralizer.ParseDataBlockNode(ele, out encoding));
                }

                return list.ToArray();
            }
            catch (Exception e)
            {
                Tools.LogHelper.WriteDebug("ParseMCUFlashDataBlocksNode()...Exception:{0}", e.Message);
            }

            return null;
        }
    }
}
