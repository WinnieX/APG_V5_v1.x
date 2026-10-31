using System;
using System.Collections.Generic;
using System.Text;
using AEC.Misc;

namespace AEC.Comm.FirmwareUpdate
{
    public class FirmwareBin
    {
        private static Localization localization = Config.GetInstance().localization;

        public bool isDownloadBin { get; set; }
        public string originalName { get; set; }
        public uint binLength { get; set; }
        public DateTime binTimestamp { get; set; }   
        //public byte[] binHash { get; set; }
        public byte[] binData { get; set; }

        public uint offsetAddress { get; set; }

        public bool isOk
        {
            get
            {
                if(isDownloadBin)
                {
                    return binIsOk;
                }

                return true;
            }
        }

        public bool binIsOk
        {
            get
            {
                return binLength > 0 && binData != null && binLength == (uint)binData.Length;
            }
        }

        public string ToString(string name, bool isContainDownloadFlag = true)
        {
            StringBuilder sb = new StringBuilder();

            Tools.StringHelper.StringBuilderAppendLine(sb, localization[Localization.firmwareUpdate_FirmwareType], name);
            if (isContainDownloadFlag)
            {
                Tools.StringHelper.StringBuilderAppendLine(sb, localization[Localization.firmwareUpdate_WhetherToUpdate], Utils.BoolToYesOrNo(this.isDownloadBin));
            }            
            if(this.binLength > 0)
            {
                Tools.StringHelper.StringBuilderAppendLine(sb, localization[Localization.firmwareUpdate_DataLength], Utils.Uint32ToHexString(this.binLength));
            }
            if (this.binTimestamp.ToBinary() != 0)
            {
                Tools.StringHelper.StringBuilderAppendLine(sb, localization[Localization.firmwareUpdate_UpdateTime], Tools.DateTimeHelper.DateTime2String(this.binTimestamp));
            }
            if(this.binData != null)
            {
                Tools.StringHelper.StringBuilderAppendLine(sb, localization[Localization.firmwareUpdate_DataHash], Utils.ByteArrayToHexStr(AEC.Tools.HashHelper.MD5(this.binData), string.Empty));
            }

            return sb.ToString();
        }
    }

    //public class DSPFirmwareBin : FirmwareBin
    //{

    //}

    //public class MCUFirmwareBin : FirmwareBin
    //{

    //}
}
