using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Effectors;

namespace AEC
{
    public static class ProductsConfig
    {
        public const string AppName = "M-Audio AEC";        
        public const string OrigianlManufacturer = "M-Audio";
        public const string OEM = "APG";
        public const string ProductName = "Sound Processor";

        public const string DefaultBrand = "Meng";

        public const string DefaultNeutralPresentation = "AEC"; //默认中性产品名

        public const string Model_MAudio_Presentation = "M-Audio";

        public const string Model_K320_Prototype_Presentation = "VK3S";
        public const string Model_K220_Prototype_Presentation = "VK3S";

        public const string Model_VK5S_Presentation = "VK5S";
        public const string Model_VK3S_Presentation = "VK3Pro";
        public const string Model_VK3I_Presentation = "VK3I";
        public const string Model_VK3W_Presentation = "VK3W";
        public const string Model_VK3SDW_Presentation = "VK3WS";

        public const string Model_VK5_Presentation = "VK5";
        public const string Model_A300_Presentation = "A300";
        public const string Model_VK3_Presentation = "VK3";

        public const string Model_VK5D_Presentation = "VK5D";
        public const string Model_VK8_Presentation = "VK8";
        public const string Model_VK3D_Presentation = "X-6";
        public const string Model_VK3BT_Presentation = "VK3BT";

        public const string Model_VK8S_Presentation = "VK8S";
        public const string Model_VK6S_Presentation = "VK6S";
        public const string Model_VK6WS_Presentation = "VK6WS";
        public const string Model_VK6GS_Presentation = "VK6";
        public const string Model_Q2_Presentation = "Q2";
        public const string Model_Q3_Presentation = "Q3";

        public const string Model_VK8Pro_Presentation = "VK8Pro";
        public const string Model_VK6Pro_Presentation = "VK6Pro";

        public const string Model_VK8SPro_Presentation = "VK8SPro";
        public const string Model_VK6SPro_Presentation = "VK6SPro";

        public const string Model_VHi_Presentation = "VHi";
        public const string Model_V0_Presentation = "V0";
        public const string Model_V5_Presentation = "V5";
        public const string Model_E300_Presentation = "E300";
        public const string Model_M5Pro_Presentation = "M5Pro";
        public const string Model_A100_Presentation = "A100";

        public const string Model_K220_OEM_LINGJIE_AEC_Presentation = "LINGJIE AEC";

        public const string ProductFamily_X5 = "X5";
        public const string ProductFamily_X5Pro = "X5Pro";
        public const string ProductFamily_X5Plus = "X5";
        public const string ProductFamily_VKxS = "VKxS";
        public const string ProductFamily_VKx = "VKx";
        public const string ProductFamily_VKxSCM = "VKxSCM";
        public const string ProductFamily_VKxDCM = "VKxDCM";
        public const string ProductFamily_VKxPII = "VKxPII";
        public const string ProductFamily_VKxPIIDCM = "VKxPIIDCM";

        public const string ProductFamily_VKxDM = "VKxDM";

        public enum DeviceCommIdentity : uint
        {
            Model_VK5S = 0x00,
            Model_VK3S = 0x10,
            Model_X5 = 0x20,
            Model_X5W = 0x21,
            Model_X5Pro = 0x22,
            Model_X5OPBT = 0x23,
            Model_VK3I = 0x30,
            Model_VK3W = 0x40,
            Model_VK3W_WithoutBT = 0x41,

            Model_VK3DM = 0x50,

            Model_VK5 = 0x60,
            Model_A300 = 0x61,
            Model_VK3 = 0x70,

            Model_VK5D = 0x80,
            Model_VK8 = 0x81,
            Model_VK3D = 0x92,
            Model_VK3BT = 0x91,

            Model_VK7S = 0xa0,
            Model_VK6S = 0xb0,
            //Model_VK6WS = 0xb1, //0xb1代号留给菱杰7580专用
            Model_VK6GS = 0xb2,
            Model_Q3 = 0xb3,
            Model_Q2 = 0xb4,
            Model_VK6WS = 0xb5,

            Model_VK7Pro = 0xc0,
            Model_VK6Pro = 0xd0,

            Model_VK7SPro = 0xe0,
            Model_VK6SPro = 0xf0,

            Model_VHi = 0x101,
            Model_V0 = 0x102,
            Model_V5 = 0x103,
            Model_M5Pro = 0x110,
            Model_A100 = 0x111,
            Model_E300 = 0x120,
        }

        private static readonly List<DeviceModel> MengBrandModels = new List<DeviceModel>
        {
            DeviceModel.X5,
            DeviceModel.X5W,
            DeviceModel.X5Pro,
            DeviceModel.X5OPBT,
            DeviceModel.VK5S,
            
            DeviceModel.VK3S,
            DeviceModel.VK3I,
            DeviceModel.VK3W,
            DeviceModel.VK3W_WithoutBT,
            DeviceModel.VK3WDM,

            DeviceModel.K320,
            DeviceModel.A300,
            DeviceModel.K220,

            DeviceModel.VK3D,
            DeviceModel.VK3BT,
            DeviceModel.VK8,
            DeviceModel.VK5D,

            DeviceModel.VK6S,
            DeviceModel.VK6WS,
            DeviceModel.VK6GS,
            DeviceModel.VK8S,
            DeviceModel.Q2,
            DeviceModel.Q3,

            DeviceModel.VK8Pro,
            DeviceModel.VK6Pro,

            DeviceModel.VK8SPro,
            DeviceModel.VK6SPro,

            DeviceModel.VHi,
            DeviceModel.V0,
            DeviceModel.V5,
            DeviceModel.E300,
            DeviceModel.M5Pro,
            DeviceModel.A100,
            DeviceModel.M_Audio,
        };

        public static readonly Dictionary<uint, DeviceModel> effectorModelMap = new Dictionary<uint, DeviceModel>()
        {
            
            {(uint)ProductsConfig.DeviceCommIdentity.Model_VHi, DeviceModel.VHi},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_V0, DeviceModel.V0},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_V5, DeviceModel.V5},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_M5Pro, DeviceModel.M5Pro},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_A100, DeviceModel.A100},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_E300, DeviceModel.E300},
            
            {(uint)ProductsConfig.DeviceCommIdentity.Model_VK5S, DeviceModel.VK5S},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_VK3S, DeviceModel.VK3S},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_X5, DeviceModel.X5},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_X5W, DeviceModel.X5W},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_X5Pro, DeviceModel.X5Pro},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_X5OPBT, DeviceModel.X5OPBT},

            {(uint)ProductsConfig.DeviceCommIdentity.Model_VK3I, DeviceModel.VK3I},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_VK3W, DeviceModel.VK3W},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_VK3W_WithoutBT, DeviceModel.VK3W_WithoutBT},

            {(uint)ProductsConfig.DeviceCommIdentity.Model_VK3DM, DeviceModel.VK3WDM},

            {(uint)ProductsConfig.DeviceCommIdentity.Model_VK5, DeviceModel.K320},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_A300, DeviceModel.A300},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_VK3, DeviceModel.K220},

            {(uint)ProductsConfig.DeviceCommIdentity.Model_VK5D, DeviceModel.VK5D},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_VK8, DeviceModel.VK8},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_VK3D, DeviceModel.VK3D},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_VK3BT, DeviceModel.VK3BT},

            {(uint)ProductsConfig.DeviceCommIdentity.Model_VK7S, DeviceModel.VK8S},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_VK6S, DeviceModel.VK6S},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_VK6WS, DeviceModel.VK6WS},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_VK6GS, DeviceModel.VK6GS},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_Q3, DeviceModel.Q3},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_Q2, DeviceModel.Q2},

            {(uint)ProductsConfig.DeviceCommIdentity.Model_VK7Pro, DeviceModel.VK8Pro},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_VK6Pro, DeviceModel.VK6Pro},

            {(uint)ProductsConfig.DeviceCommIdentity.Model_VK7SPro, DeviceModel.VK8SPro},
            {(uint)ProductsConfig.DeviceCommIdentity.Model_VK6SPro, DeviceModel.VK6SPro},
        };

        public static readonly Dictionary<DeviceModel, string> deviceModelMap = new Dictionary<DeviceModel, string>()
        {
            {DeviceModel.X5, ProductsConfig.ProductFamily_X5},
            {DeviceModel.X5W, ProductsConfig.ProductFamily_X5},
            {DeviceModel.X5Pro, ProductsConfig.ProductFamily_X5Pro},
            {DeviceModel.X5OPBT, ProductsConfig.ProductFamily_X5Plus},
            {DeviceModel.VK5S, ProductsConfig.Model_VK5S_Presentation},                      
            {DeviceModel.VK3S, ProductsConfig.Model_VK3S_Presentation},
            {DeviceModel.VK3I, ProductsConfig.Model_VK3I_Presentation},
            {DeviceModel.VK3W, ProductsConfig.Model_VK3W_Presentation},
            {DeviceModel.VK3W_WithoutBT, ProductsConfig.Model_VK3W_Presentation},
            {DeviceModel.VK3WDM, ProductsConfig.Model_VK3SDW_Presentation},

            {DeviceModel.K320, ProductsConfig.Model_VK5_Presentation},
            {DeviceModel.A300, ProductsConfig.Model_A300_Presentation},            
            {DeviceModel.K220, ProductsConfig.Model_VK3_Presentation},

            {DeviceModel.VK5D, ProductsConfig.Model_VK5D_Presentation},
            {DeviceModel.VK8, ProductsConfig.Model_VK8_Presentation},
            {DeviceModel.VK3D, ProductsConfig.Model_VK3D_Presentation},
            {DeviceModel.VK3BT, ProductsConfig.Model_VK3BT_Presentation},

            {DeviceModel.VK8S, ProductsConfig.Model_VK8S_Presentation},
            {DeviceModel.VK6S, ProductsConfig.Model_VK6S_Presentation},
            {DeviceModel.VK6WS, ProductsConfig.Model_VK6WS_Presentation},
            {DeviceModel.VK6GS, ProductsConfig.Model_VK6GS_Presentation},
            {DeviceModel.Q2, ProductsConfig.Model_Q2_Presentation},
            {DeviceModel.Q3, ProductsConfig.Model_Q3_Presentation},

            {DeviceModel.VK8Pro, ProductsConfig.Model_VK8Pro_Presentation},
            {DeviceModel.VK6Pro, ProductsConfig.Model_VK6Pro_Presentation},

            {DeviceModel.VK8SPro, ProductsConfig.Model_VK8SPro_Presentation},
            {DeviceModel.VK6SPro, ProductsConfig.Model_VK6SPro_Presentation},

            {DeviceModel.VHi, ProductsConfig.Model_VHi_Presentation},
            {DeviceModel.V0, ProductsConfig.Model_V0_Presentation},
            {DeviceModel.V5, ProductsConfig.Model_V5_Presentation},
            {DeviceModel.E300, ProductsConfig.Model_E300_Presentation},
            {DeviceModel.M5Pro, ProductsConfig.Model_M5Pro_Presentation},
            {DeviceModel.A100, ProductsConfig.Model_A100_Presentation},
        };

        public static bool GetDeviceCommIdentity(DeviceModel deviceModel, out uint deviceCommIdentity)
        {
            deviceCommIdentity = uint.MaxValue;

            foreach(uint commIdentity in effectorModelMap.Keys)
            {
                if(effectorModelMap[commIdentity] == deviceModel)
                {
                    deviceCommIdentity = commIdentity;

                    return true;
                }
            }

            return false;
        }

        public static readonly DeviceModel MAudioCorrespondingModel = DeviceModel.VK5S;   //默认的产品对应的型号

        public static readonly List<DeviceModel> NeutralProductsList = new List<DeviceModel>()
        {
            
        };

        public static string GetDeviceModelText(DeviceModel deviceModel)
        {
            if(deviceModelMap.ContainsKey(deviceModel))
            {
                return deviceModelMap[deviceModel];
            }

            return string.Empty;
        }

        public static bool GetDeviceModelFromText(string deviceModel, ref DeviceModel model)
        {
            foreach (DeviceModel dm in deviceModelMap.Keys)
            {
                if (deviceModelMap[dm] == deviceModel)
                {
                    model = dm;
                    return true;
                }
            }

            return false;
        }

        public static bool ProductIsMengBrand(DeviceModel deviceModel)
        {
            return ProductsConfig.MengBrandModels.Contains(deviceModel);
        }

        public static bool ProductIsNeutral(DeviceModel deviceModel)
        {
            return ProductsConfig.NeutralProductsList.Contains(deviceModel);
        }

        public static string GetOEMBrand(DeviceModel deviceModel)
        {
            switch (deviceModel)
            {
                default:
                    return ProductsConfig.DefaultBrand;
            }
        }

        static ProductsConfig()
        {
            if(!AppConfig.supportedDeviceModelList.Contains(MAudioCorrespondingModel))
            {
                MAudioCorrespondingModel = AppConfig.supportedDeviceModelList[0];
            }
        }

        public static DeviceModel GetDeviceModelFromModelLabel(string modelLabel)
        {
            DeviceModel deviceModel = DeviceModel.M_Audio;

            if (!string.IsNullOrEmpty(modelLabel))
            {
                foreach (DeviceModel model in AppConfig.supportedDeviceModelList)
                {
                    if (ProductsConfig.deviceModelMap.ContainsKey(model) && ProductsConfig.deviceModelMap[model] == modelLabel)
                    {
                        deviceModel = model;
                        break;
                    }
                }
            }

            if (!AppConfig.supportedDeviceModelList.Contains(deviceModel))
            {
                deviceModel = AppConfig.supportedDeviceModelList[0];
            }

            return deviceModel;
        }

        public static MEffectorFamily EffectorModelToFamily(DeviceModel model)
        {
            if (model == DeviceModel.M_Audio)
            {
                model = ProductsConfig.MAudioCorrespondingModel;
            }

            switch (model)
            {
                case DeviceModel.X5:
                case DeviceModel.X5W:
                case DeviceModel.X5Pro:
                case DeviceModel.X5OPBT:
                    return MEffectorFamily.X5;

                case DeviceModel.K320:
                case DeviceModel.A300:
                case DeviceModel.K220:
                    return MEffectorFamily.KX;

                case DeviceModel.VK3D:
                case DeviceModel.VK3BT:
                case DeviceModel.VK8:
                case DeviceModel.VK5D:
                    return MEffectorFamily.VKxSCM;

                case DeviceModel.VK6S:
                case DeviceModel.VK6WS:
                case DeviceModel.VK6GS:
                case DeviceModel.VK8S:
                case DeviceModel.Q2:
                case DeviceModel.Q3:
                    return MEffectorFamily.VKxDCM;

                case DeviceModel.VK8Pro:
                case DeviceModel.VK6Pro:
                    return MEffectorFamily.VKxPII;

                case DeviceModel.VK8SPro:
                case DeviceModel.VK6SPro:
                    return MEffectorFamily.VKxPIIDCM;

                case DeviceModel.VK5S:
                case DeviceModel.VK3S:
                case DeviceModel.VK3I:
                case DeviceModel.VK3W:
                case DeviceModel.VK3W_WithoutBT:
                    return MEffectorFamily.VKxS;

                case DeviceModel.VK3WDM:
                    return MEffectorFamily.VKxDM;

                case DeviceModel.VHi:
                case DeviceModel.V0:
                case DeviceModel.V5:
                case DeviceModel.E300:
                    return MEffectorFamily.VX;

                case DeviceModel.M5Pro:
                case DeviceModel.A100:
                    return MEffectorFamily.MX;

                default:
                    throw new Exception("MController.EffectorModelToFamily 参数错误!");
            }
        }
    }
}
