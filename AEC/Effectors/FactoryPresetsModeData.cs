using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Effectors
{
    public class FactoryPresetsModeData
    {
        public static readonly Dictionary<string, byte[]> vkxdmFactoryModeList = new Dictionary<string, byte[]>
        {
            {"Echo+Reverb-1", global::AEC.Properties.Resources.Factory_VKxDM_Echo_Reverb_1},
            {"Echo+Reverb-2", global::AEC.Properties.Resources.Factory_VKxDM_Echo_Reverb_2},
            {"Echo+Reverb-3", global::AEC.Properties.Resources.Factory_VKxDM_Echo_Reverb_3},
            {"Echo+Reverb-4", global::AEC.Properties.Resources.Factory_VKxDM_Echo_Reverb_4},
            {"Echo+Reverb-5", global::AEC.Properties.Resources.Factory_VKxDM_Echo_Reverb_5},
            {"Echo-1", global::AEC.Properties.Resources.Factory_VKxDM_Echo_1},
            {"Echo-MT", global::AEC.Properties.Resources.Factory_VKxDM_Echo_MT},
            {"Reverb-1", global::AEC.Properties.Resources.Factory_VKxDM_Reverb_1},
            {"Reverb-2", global::AEC.Properties.Resources.Factory_VKxDM_Reverb_2},
        };

        public static readonly Dictionary<string, byte[]> x5FactoryModeList = new Dictionary<string, byte[]>
        {
            {"Factory Preset", global::AEC.Properties.Resources.Factory_X5_Factory_Preset},
        };

        public static readonly Dictionary<string, byte[]> vkxSFactoryModeList = new Dictionary<string, byte[]>
        {
            {"Echo+Reverb-1", global::AEC.Properties.Resources.Factory_VKxS_Echo_Reverb_1},
            {"Echo+Reverb-2", global::AEC.Properties.Resources.Factory_VKxS_Echo_Reverb_2},
            {"Echo+Reverb-3", global::AEC.Properties.Resources.Factory_VKxS_Echo_Reverb_3},
            {"Echo+Reverb-4", global::AEC.Properties.Resources.Factory_VKxS_Echo_Reverb_4},
            {"Echo+Reverb-5", global::AEC.Properties.Resources.Factory_VKxS_Echo_Reverb_5},
            {"Echo-1", global::AEC.Properties.Resources.Factory_VKxS_Echo_1},
            {"Echo-MT", global::AEC.Properties.Resources.Factory_VKxS_Echo_MT},
            {"Reverb-1", global::AEC.Properties.Resources.Factory_VKxS_Reverb_1},
            {"Reverb-2", global::AEC.Properties.Resources.Factory_VKxS_Reverb_2},

            {"X5 Factory Preset", global::AEC.Properties.Resources.Factory_X5_Factory_Preset},
        };

        public static readonly Dictionary<string, byte[]> vkxFactoryModeList = new Dictionary<string, byte[]>
        {
            {"Echo+Reverb-1", global::AEC.Properties.Resources.Factory_VKx_Echo_Reverb_1},
            {"Echo+Reverb-2", global::AEC.Properties.Resources.Factory_VKx_Echo_Reverb_2},
            {"Echo+Reverb-3", global::AEC.Properties.Resources.Factory_VKx_Echo_Reverb_3},
            {"Echo+Reverb-4", global::AEC.Properties.Resources.Factory_VKx_Echo_Reverb_4},
            {"Echo+Reverb-5", global::AEC.Properties.Resources.Factory_VKx_Echo_Reverb_5},
            {"Echo-1", global::AEC.Properties.Resources.Factory_VKx_Echo_1},
            {"Echo-MT", global::AEC.Properties.Resources.Factory_VKx_Echo_MT},
            {"Reverb-1", global::AEC.Properties.Resources.Factory_VKx_Reverb_1},
            {"Reverb-2", global::AEC.Properties.Resources.Factory_VKx_Reverb_2},
        };

        public static readonly Dictionary<string, byte[]> vhiFactoryModeList = new Dictionary<string, byte[]>
        {
            {"LG-Echo+Rev 1", global::AEC.Properties.Resources.Factory_VHi_LG_Echo_Rev_1},
            {"LG-Echo+Rev 2", global::AEC.Properties.Resources.Factory_VHi_LG_Echo_Rev_2},
            {"LG-Echo+Rev 3", global::AEC.Properties.Resources.Factory_VHi_LG_Echo_Rev_3},
            {"PT-Echo+Rev 4", global::AEC.Properties.Resources.Factory_VHi_PT_Echo_Rev_4},
            {"PT-Echo", global::AEC.Properties.Resources.Factory_VHi_PT_Echo},
            {"PT-Reverb", global::AEC.Properties.Resources.Factory_VHi_PT_Reverb},
            {"Meng-Echo", global::AEC.Properties.Resources.Factory_VHi_Meng_Echo},
        };

        public static readonly Dictionary<string, byte[]> v0FactoryModeList = new Dictionary<string, byte[]>
        {

        };

        public static readonly Dictionary<string, byte[]> v5FactoryModeList = new Dictionary<string, byte[]>
        {
            {"LG-Echo+Rev 1", global::AEC.Properties.Resources.Factory_V5_LG_Echo_Rev_1},
            {"LG-Echo+Rev 2", global::AEC.Properties.Resources.Factory_V5_LG_Echo_Rev_2},
            {"LG-Echo+Rev 3", global::AEC.Properties.Resources.Factory_V5_LG_Echo_Rev_3},
            {"PT-Echo+Rev 4", global::AEC.Properties.Resources.Factory_V5_PT_Echo_Rev_4},
            {"PT-Echo", global::AEC.Properties.Resources.Factory_V5_PT_Echo},
            {"PT-Reverb", global::AEC.Properties.Resources.Factory_V5_PT_Reverb},
            {"Meng-Echo", global::AEC.Properties.Resources.Factory_V5_Meng_Echo},
        };
        
        public static readonly Dictionary<string, byte[]> e300FactoryModeList = new Dictionary<string, byte[]>
        {
            {"LG-Echo+Rev 1", global::AEC.Properties.Resources.Factory_E300_LG_Echo_Rev_1},
            {"LG-Echo+Rev 2", global::AEC.Properties.Resources.Factory_E300_LG_Echo_Rev_2},
            {"LG-Echo+Rev 3", global::AEC.Properties.Resources.Factory_E300_LG_Echo_Rev_3},
            {"PT-Echo+Rev 4", global::AEC.Properties.Resources.Factory_E300_PT_Echo_Rev_4},
            {"PT-Echo", global::AEC.Properties.Resources.Factory_E300_PT_Echo},
            {"PT-Reverb", global::AEC.Properties.Resources.Factory_E300_PT_Reverb},
            {"Meng-Echo", global::AEC.Properties.Resources.Factory_E300_Meng_Echo},
        };

        public static readonly Dictionary<string, byte[]> m5ProFactoryModeList = new Dictionary<string, byte[]>
        {
            {"Meng-Echo", global::AEC.Properties.Resources.Factory_M5Pro_Meng_Echo},
        };

        public static readonly Dictionary<string, byte[]> a100FactoryModeList = new Dictionary<string, byte[]>
        {
            {"Meng-Echo", global::AEC.Properties.Resources.Factory_A100_Meng_Echo},
        };
    }
}
