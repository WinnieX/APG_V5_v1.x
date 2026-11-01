using System;
using System.Collections.Generic;
using System.Text;
using AEC.Effectors;

namespace AEC
{
    public class AppConfig
    {
        public const bool isShowManufacturer = true;
        public const bool isShowMainForm = false;
        public const bool isEncryptCommunication = false;
        public const bool isOutputDebugLog = true;
        public const bool isLoadCustomDeviceInfo = true;
        public const bool isLogOutputToConsole = true;
        public const bool isSupportSwitchDeviceModel = true;
        public const bool isSupportOnlineHelp = true;
        public const bool isSupportBluetoothRename = true;
        public const bool isSupportFirmwareUpdate = true;
        public const bool isSupportActivateDevice = true;
        public const bool isSupportFirmwareUpdateAuxiliaryFunctions = true;
        public const bool isOEMVersion = true;

        public static readonly List<DeviceModel> supportedDeviceModelList = new List<DeviceModel>()
        {
            //DeviceModel.VK5S,
            //DeviceModel.VK3S,
            //DeviceModel.X5,
            //DeviceModel.X5W,
            //DeviceModel.X5Pro,
            //DeviceModel.X5OPBT,
            //DeviceModel.VK3I,
            //DeviceModel.VK3W,
            //DeviceModel.VK3W_WithoutBT,
            //DeviceModel.VK3WDM,

            //DeviceModel.K320,
            //DeviceModel.A300,
            //DeviceModel.K220,

            //DeviceModel.VK8,
            //DeviceModel.VK5D,

//            DeviceModel.VK3D,
            //DeviceModel.VK3BT,

            //DeviceModel.VK8S,
            //DeviceModel.VK6S,
            //DeviceModel.VK6GS,
            //DeviceModel.Q3,
            //DeviceModel.Q2,
            //DeviceModel.VK6WS,

            //DeviceModel.VK8Pro,
            //DeviceModel.VK6Pro,

            //DeviceModel.VK8SPro,
            //DeviceModel.VK6SPro,
            
//            DeviceModel.K320,
//            DeviceModel.A300,
//            DeviceModel.K220,
//            DeviceModel.K320Lite,
//            DeviceModel.K220Lite,
//#if DEBUG_VERSION
//            DeviceModel.K320_OEM_MYART_ES5Pro,
//            DeviceModel.K220_OEM_MYART_ES5Ultra,
//            DeviceModel.K220_OEM_LINGJIE_AEC,
//#endif
//            DeviceModel.VHi,
            DeviceModel.V5,
//            DeviceModel.E300,
//            DeviceModel.M5Pro,
//            DeviceModel.A100,
        };

        public static string GetManufacturer()
        {
            if(isOEMVersion)
            {
                return ProductsConfig.OEM;
            }
            else
            {
                return ProductsConfig.OrigianlManufacturer;
            }
        }

        public static bool DeviceModelIsSupported(DeviceModel deviceModel)
        {
            return supportedDeviceModelList.Contains(deviceModel);
        }

        public static bool IsSupportOverOneDeviceModel()
        {
            return supportedDeviceModelList.Count > 1;
        }

        public static bool IsShowSwitchDeviceModelMenuItem()
        {
            return isSupportSwitchDeviceModel && IsSupportOverOneDeviceModel();
        }

        public static List<string> GetSupportedDeviceModelList()
        {
            List<string> list = new List<string>();
            foreach (DeviceModel model in AppConfig.supportedDeviceModelList)
            {
                if (ProductsConfig.deviceModelMap.ContainsKey(model))
                {
                    if (!list.Contains(ProductsConfig.deviceModelMap[model]))
                    {
                        list.Add(ProductsConfig.deviceModelMap[model]);
                    }
                }
            }

            return list;
        }

        public static string GetSupportDeviceModels()
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < supportedDeviceModelList.Count; ++i)// DeviceModel model in supportedDeviceModelList)
            {
                uint deviceCommIdentity;

                DeviceModel model = supportedDeviceModelList[i];

                if(ProductsConfig.deviceModelMap.ContainsKey(model) && ProductsConfig.GetDeviceCommIdentity(model, out deviceCommIdentity))
                {
                    sb.AppendFormat("{0} ( {1} )", ProductsConfig.deviceModelMap[model], Core.Utils.Uint32ToShortHexStr(deviceCommIdentity));

                    if(i < supportedDeviceModelList.Count - 1)
                    {
                        sb.AppendLine(string.Empty);
                    }
                }
            }

            return sb.ToString();
        }
    }
}
