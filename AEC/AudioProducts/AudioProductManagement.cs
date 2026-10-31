using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.IO;

namespace AEC.AudioProducts
{
    public enum MicWiringMode
    {
        Unkonwn,
        Wireless,
        Wired,
    }

    public class AudioProductManagement
    {
        public enum LSBrand
        {
            JBL,
            Martin,     //玛田
        }

        public static readonly Dictionary<LSBrand, string> lsBrandMap = new Dictionary<LSBrand, string>()
        {
            {LSBrand.JBL, "JBL"},
            {LSBrand.Martin, "Martin"},
        };

        public enum PABrand
        {
            Crown,      //皇冠
            Yamaha,     //雅马哈
        }

        public static readonly Dictionary<PABrand, string> paBrandMap = new Dictionary<PABrand, string>()
        {
            {PABrand.Crown, "Crown"},
            {PABrand.Yamaha, "Yamaha"},
        };

        public enum micBrand
        {
            BBS,
            Shure,      //舒尔
        }

        public static readonly Dictionary<micBrand, string> micBrandMap = new Dictionary<micBrand, string>()
        {
            {micBrand.BBS, "BBS"},
            {micBrand.Shure, "Shure"},
        };
        
        private static AudioProductManagement _instance = new AudioProductManagement();
        public static AudioProductManagement GetInstance()
        {
            return _instance;
        }

        private AudioProductManagement()
        {
            InitBuildInAudioProducts();
            InitCustomAudioProducts();
            InitBrandSet();
        }

        private Loudspeaker[] buildInLS = null;
        private PowerAmplifier[] buildInPA = null;
        private Microphone[] buildInMic = null;

        private readonly Dictionary<string, object> lsBrandSet = new Dictionary<string, object>();
        private readonly Dictionary<string, object> paBrandSet = new Dictionary<string, object>();
        private readonly Dictionary<string, object> micBrandSet = new Dictionary<string, object>();

        private Dictionary<Loudspeaker, Loudspeaker> customLSSet = new Dictionary<Loudspeaker, Loudspeaker>();
        private Dictionary<PowerAmplifier, PowerAmplifier> customPASet = new Dictionary<PowerAmplifier, PowerAmplifier>();
        private Dictionary<Microphone, Microphone> customMicSet = new Dictionary<Microphone, Microphone>();

        private void InitBuildInAudioProducts()
        {
            buildInLS = new Loudspeaker[]
            {
                new Loudspeaker(lsBrandMap[LSBrand.JBL], "KP051", 400, 10, 8),
                new Loudspeaker(lsBrandMap[LSBrand.JBL], "KP052", 500, 12, 8),
                new Loudspeaker(lsBrandMap[LSBrand.JBL], "KP055", 600, 15, 8),
                new Loudspeaker(lsBrandMap[LSBrand.JBL], "KP4012", 350, 10, 8),
                new Loudspeaker(lsBrandMap[LSBrand.JBL], "Ki512", 500, 12, 8),
                new Loudspeaker(lsBrandMap[LSBrand.Martin], "FX10", 250, 10, 8),
                new Loudspeaker(lsBrandMap[LSBrand.Martin], "FX12", 300, 12, 8),
                new Loudspeaker(lsBrandMap[LSBrand.Martin], "FX15", 400, 15, 8),
            };

            buildInPA = new PowerAmplifier[]
            {
                new PowerAmplifier(paBrandMap[PABrand.Crown], "T3", 450, 8, 0.775f),
                new PowerAmplifier(paBrandMap[PABrand.Crown], "T5", 750, 8, 0.775f),
                new PowerAmplifier(paBrandMap[PABrand.Crown], "T7", 1000, 8, 0.775f),
                new PowerAmplifier(paBrandMap[PABrand.Crown], "T10", 1350, 8, 0.775f),
                new PowerAmplifier(paBrandMap[PABrand.Yamaha], "RX-V385", 675, 6, 0.775f),
            };

            buildInMic = new Microphone[]
            {
                new Microphone(micBrandMap[micBrand.BBS], "U898", 15),
                new Microphone(micBrandMap[micBrand.BBS], "U998", 15),
                new Microphone(micBrandMap[micBrand.BBS], "U-1880S", 15),
                new Microphone(micBrandMap[micBrand.Shure], "SM58"),
                new Microphone(micBrandMap[micBrand.Shure], "BETA 58A"),
            };
        }

        private void InitBrandSet()
        {
            //初始化内置音频系统设备品牌
            foreach(Loudspeaker ls in buildInLS)
            {
                if(!lsBrandSet.ContainsKey(ls.brand))
                {
                    
                    lsBrandSet[ls.brand] = null;
                }
            }

            foreach (PowerAmplifier pa in buildInPA)
            {
                if (!paBrandSet.ContainsKey(pa.brand))
                {

                    paBrandSet[pa.brand] = null;
                }
            }

            foreach (Microphone mic in buildInMic)
            {
                if (!micBrandSet.ContainsKey(mic.brand))
                {
                    micBrandSet[mic.brand] = null;
                }
            }

            //初始化内置音频系统设备品牌
            foreach (Loudspeaker ls in customLSSet.Keys)
            {
                if(!lsBrandSet.ContainsKey(ls.brand))
                {
                    
                    lsBrandSet[ls.brand] = null;
                }
            }

            foreach (PowerAmplifier pa in customPASet.Keys)
            {
                if (!paBrandSet.ContainsKey(pa.brand))
                {

                    paBrandSet[pa.brand] = null;
                }
            }

            foreach (Microphone mic in customMicSet.Keys)
            {
                if (!micBrandSet.ContainsKey(mic.brand))
                {

                    micBrandSet[mic.brand] = null;
                }
            }

            //Console.WriteLine("lsBrandSet:");
            //foreach (string brand in lsBrandSet.Keys)
            //{
            //    Console.WriteLine(brand);
            //}
            //Console.WriteLine("paBrandSet:");
            //foreach (string brand in paBrandSet.Keys)
            //{
            //    Console.WriteLine(brand);
            //}
            //Console.WriteLine("micBrandSet:");
            //foreach (string brand in micBrandSet.Keys)
            //{
            //    Console.WriteLine(brand);
            //}
        }

        public string[] LSBrands
        {
            get
            {
                return new List<string>(this.lsBrandSet.Keys).ToArray();
            }
        }

        public static string GetPowerLabel(short power)
        {
            return string.Format("{0} W", power);
        }

        public string[] LSPowers
        {
            get
            {
                List<short> powerList = new List<short>();
                foreach (Loudspeaker ls in this.buildInLS)
                {
                    if(!powerList.Contains(ls.power))
                    {
                        powerList.Add(ls.power);
                    }
                }
                foreach (Loudspeaker ls in this.customLSSet.Keys)
                {
                    if (!powerList.Contains(ls.power))
                    {
                        powerList.Add(ls.power);
                    }
                }
                powerList.Sort();

                List<string> list = new List<string>();
                foreach(short power in powerList)
                {
                    list.Add(GetPowerLabel(power));
                }

                return list.ToArray();
            }
        }

        public string[] PAPowers
        {
            get
            {
                List<short> powerList = new List<short>();
                foreach (PowerAmplifier pa in this.buildInPA)
                {
                    if (!powerList.Contains(pa.power))
                    {
                        powerList.Add(pa.power);
                    }
                }
                foreach (PowerAmplifier pa in this.customPASet.Keys)
                {
                    if (!powerList.Contains(pa.power))
                    {
                        powerList.Add(pa.power);
                    }
                }
                powerList.Sort();

                List<string> list = new List<string>();
                foreach (short power in powerList)
                {
                    list.Add(GetPowerLabel(power));
                }

                return list.ToArray();
            }
        }

        //public static string[] GetPowerList()

        public string[] PABrands
        {
            get
            {
                return new List<string>(this.paBrandSet.Keys).ToArray();
            }
        }

        public string[] MicBrands
        {
            get
            {
                return new List<string>(this.micBrandSet.Keys).ToArray();
            }
        }

        public string[] GetLSModels(string brand)
        {
            List<string> models = new List<string>();
            foreach(Loudspeaker ls in this.buildInLS)
            {
                if(string.Equals(ls.brand, brand, StringComparison.CurrentCultureIgnoreCase))
                {
                    models.Add(ls.model);
                }
            }
            foreach (Loudspeaker ls in this.customLSSet.Keys)
            {
                if (string.Equals(ls.brand, brand, StringComparison.CurrentCultureIgnoreCase))
                {
                    models.Add(ls.model);
                }
            }

            return models.ToArray();
        }

        public string[] GetPAModels(string brand)
        {
            List<string> models = new List<string>();
            foreach (PowerAmplifier pa in this.buildInPA)
            {
                if (string.Equals(pa.brand, brand, StringComparison.CurrentCultureIgnoreCase))
                {
                    models.Add(pa.model);
                }
            }
            foreach (PowerAmplifier pa in this.customPASet.Keys)
            {
                if (string.Equals(pa.brand, brand, StringComparison.CurrentCultureIgnoreCase))
                {
                    models.Add(pa.model);
                }
            }

            return models.ToArray();
        }

        public string[] GetMicModels(string brand)
        {
            List<string> models = new List<string>();
            foreach (Microphone mic in this.buildInMic)
            {
                if (string.Equals(mic.brand, brand, StringComparison.CurrentCultureIgnoreCase))
                {
                    models.Add(mic.model);
                }
            }
            foreach (Microphone mic in this.customMicSet.Keys)
            {
                if (string.Equals(mic.brand, brand, StringComparison.CurrentCultureIgnoreCase))
                {
                    models.Add(mic.model);
                }
            }

            return models.ToArray();
        }

        public Loudspeaker GetLoudSpeaker(string brand, string model)
        {
            foreach(Loudspeaker ls in this.buildInLS)
            {
                if(string.Equals(brand, ls.brand, StringComparison.CurrentCultureIgnoreCase) &&
                   string.Equals(model, ls.model, StringComparison.CurrentCultureIgnoreCase))
                {
                    return ls;
                }
            }

            foreach (Loudspeaker ls in this.customLSSet.Keys)
            {
                if (string.Equals(brand, ls.brand, StringComparison.CurrentCultureIgnoreCase) &&
                   string.Equals(model, ls.model, StringComparison.CurrentCultureIgnoreCase))
                {
                    return ls;
                }
            }

            return null;
        }

        public PowerAmplifier GetPowerAmplifier(string brand, string model)
        {
            foreach (PowerAmplifier pa in this.buildInPA)
            {
                if (string.Equals(brand, pa.brand, StringComparison.CurrentCultureIgnoreCase) &&
                   string.Equals(model, pa.model, StringComparison.CurrentCultureIgnoreCase))
                {
                    return pa;
                }
            }

            foreach (PowerAmplifier pa in this.customPASet.Keys)
            {
                if (string.Equals(brand, pa.brand, StringComparison.CurrentCultureIgnoreCase) &&
                   string.Equals(model, pa.model, StringComparison.CurrentCultureIgnoreCase))
                {
                    return pa;
                }
            }

            return null;
        }

        public Microphone GetMicrophone(string brand, string model)
        {
            foreach (Microphone mic in this.buildInMic)
            {
                if (string.Equals(brand, mic.brand, StringComparison.CurrentCultureIgnoreCase) &&
                   string.Equals(model, mic.model, StringComparison.CurrentCultureIgnoreCase))
                {
                    return mic;
                }
            }

            foreach (Microphone mic in this.customMicSet.Keys)
            {
                if (string.Equals(brand, mic.brand, StringComparison.CurrentCultureIgnoreCase) &&
                   string.Equals(model, mic.model, StringComparison.CurrentCultureIgnoreCase))
                {
                    return mic;
                }
            }

            return null;
        }

        public static MicWiringMode GetMicWiringMode(string brand)
        {
            brand = brand.ToLower().Trim();
            switch (brand)
            {
                case "bbs":
                    return MicWiringMode.Wireless;

                case "shure":
                    return MicWiringMode.Wired;

                default:
                    return MicWiringMode.Unkonwn;
            }
        }

        private static string externalProductsFilePath
        {
            get
            {
                string currDir = System.AppDomain.CurrentDomain.BaseDirectory;
                return currDir + ExternalProductsFileName;
            }
        }

        public void InitCustomAudioProducts()
        {
            if (!File.Exists(externalProductsFilePath))
            {
                return;
            }

            try
            {
                FileStream fs = new FileStream(externalProductsFilePath, FileMode.Open, FileAccess.Read);
                LoadFromStream(fs);
                fs.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("加载文件发生异常!", "AEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AEC.Tools.LogHelper.WriteDebug("AudioProductManagement.LoadCustomAudioProducts()... Exception: ", ex);
            }

            //Console.WriteLine("用户音箱:");
            //foreach (Loudspeaker ls in this.customLSSet.Keys)
            //{
            //    Console.WriteLine("Loudspeaker:{0}", ls.label);
            //}
            //Console.WriteLine("用户功放:");
            //foreach (PowerAmplifier pa in this.customPASet.Keys)
            //{
            //    Console.WriteLine("PowerAmplifier:{0}", pa.label);
            //}
            //Console.WriteLine("用户麦克风:");
            //foreach (Microphone mic in this.customMicSet.Keys)
            //{
            //    Console.WriteLine("Microphone:{0}", mic.label);
            //}
        }

        private void LoadFromStream(Stream stream)
        {
            Debug.Assert(stream != null);

            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(stream);

                if (xmldoc.DocumentElement != null &&
                    xmldoc.DocumentElement.LocalName == rootNodeName)
                {
                    XmlElement rootNode = xmldoc.DocumentElement;

                    XmlNodeList lsNodeList = rootNode.SelectNodes(string.Format("{0}/{1}", lsListNode, lsNode));
                    ParseLoudspeakerList(lsNodeList);

                    XmlNodeList paNodeList = rootNode.SelectNodes(string.Format("{0}/{1}", paListNode, paNode));
                    ParsePowerAmplifierList(paNodeList);
                    
                    XmlNodeList micNodeList = rootNode.SelectNodes(string.Format("{0}/{1}", micListNode, micNode));
                    ParseMicrophoneList(micNodeList);
                }
            }
            catch (Exception ex)
            {
                Tools.LogHelper.WriteDebug("LoadFromStream()...Exception:", ex);
            }
        }
        

        private void ParseLoudspeakerList(XmlNodeList lsNodeList)
        {
            if(lsNodeList == null || lsNodeList.Count == 0)
            {
                return;
            }

            //List<Loudspeaker> list = new List<Loudspeaker>();

            foreach(XmlElement node in lsNodeList)
            {
                string brand = node.GetAttribute(brandAttr).Trim();
                string model = node.GetAttribute(modelAttr).Trim();
                string powerA = node.GetAttribute(powerAttr);
                string sizeA = node.GetAttribute(sizeAttr);
                string impedanceA = node.GetAttribute(impedanceAttr);

                if (!string.IsNullOrEmpty(brand) || !string.IsNullOrEmpty(model))
                {
                    short power = 0;
                    short size = 0;
                    short impedance = 0;
                    if (!short.TryParse(powerA, out power) || !Loudspeaker.PowerIsValid(power))
                    {
                        break;
                    }
                    if (!short.TryParse(sizeA, out size) || !Loudspeaker.SizeIsValid(size))
                    {
                        break;
                    }
                    if (!short.TryParse(impedanceA, out impedance) || !Loudspeaker.ImpedanceIsValid(impedance))
                    {
                        break;
                    }
                    
                    var ls = new Loudspeaker(brand, model, power, size, impedance);
                    //如果新增型号与内置型号相同，则需要将新增型号修正
                    foreach (Loudspeaker bLS in this.buildInLS)
                    {
                        if (string.Equals(ls.brand, bLS.brand, StringComparison.CurrentCultureIgnoreCase))
                        {
                            ls.brand = bLS.brand;
                            break;
                        }
                    }

                    foreach (Loudspeaker cLS in this.customLSSet.Keys)
                    {
                        if (string.Equals(ls.brand, cLS.brand, StringComparison.CurrentCultureIgnoreCase))
                        {
                            ls.brand = cLS.brand;
                            break;
                        }
                    }

                    //检测是否在BuildIn中
                    bool isRepeat = false;
                    foreach(Loudspeaker loudspeaker in this.buildInLS)
                    {
                        if (loudspeaker.Equals(ls))
                        {
                            isRepeat = true;
                            break;
                        }
                    }

                    if(isRepeat)
                    {
                        break;
                    }

                    if (customLSSet.ContainsKey(ls))
                    {
                        customLSSet.Remove(ls);
                    }

                    customLSSet[ls] = ls;
                }
            }
        }

        private void ParsePowerAmplifierList(XmlNodeList paNodeList)
        {
            if (paNodeList == null || paNodeList.Count == 0)
            {
                return;
            }

            foreach (XmlElement node in paNodeList)
            {
                string brand = node.GetAttribute(brandAttr).Trim();
                string model = node.GetAttribute(modelAttr).Trim();
                string sensitivityA = node.GetAttribute(sensitivityAttr);
                string powerA = node.GetAttribute(powerAttr);
                string impedanceA = node.GetAttribute(impedanceAttr);

                if (!string.IsNullOrEmpty(brand) || !string.IsNullOrEmpty(model))
                {
                    float sensitivity = 0.0f;
                    short power = 0;
                    short impedance = 0;
                    if (!float.TryParse(sensitivityA, out sensitivity) || !PowerAmplifier.SensitivityIsValid(sensitivity))
                    {
                        break;
                    }
                    if (!short.TryParse(powerA, out power) || !PowerAmplifier.PowerIsValid(power))
                    {
                        break;
                    }
                    if (!short.TryParse(impedanceA, out impedance) || !PowerAmplifier.ImpedanceIsValid(impedance))
                    {
                        break;
                    }

                    var pa = new PowerAmplifier(brand, model, power, impedance, sensitivity);

                    //如果新增型号与内置型号相同，则需要将新增型号修正
                    foreach (PowerAmplifier bPA in this.buildInPA)
                    {
                        if (string.Equals(pa.brand, bPA.brand, StringComparison.CurrentCultureIgnoreCase))
                        {
                            pa.brand = bPA.brand;
                            break;
                        }
                    }
                    foreach (PowerAmplifier cPA in this.customPASet.Keys)
                    {
                        if (string.Equals(pa.brand, cPA.brand, StringComparison.CurrentCultureIgnoreCase))
                        {
                            pa.brand = cPA.brand;
                            break;
                        }
                    }

                    bool isRepeat = false;
                    foreach (PowerAmplifier powerAmp in this.buildInPA)
                    {
                        if (powerAmp.Equals(pa))
                        {
                            isRepeat = true;
                            break;
                        }
                    }

                    if (isRepeat)
                    {
                        break;
                    }

                    if(customPASet.ContainsKey(pa))
                    {
                        customPASet.Remove(pa);
                    }

                    customPASet[pa] = pa;
                }
            }
        }

        private void ParseMicrophoneList(XmlNodeList micNodeList)
        {
            if (micNodeList == null || micNodeList.Count == 0)
            {
                return;
            }

            foreach (XmlElement node in micNodeList)
            {
                string brand = node.GetAttribute(brandAttr);
                string model = node.GetAttribute(modelAttr);
                string recommendedVolumeA = node.GetAttribute(recommendedVolumeAttr);

                if (!string.IsNullOrEmpty(brand) || !string.IsNullOrEmpty(model))
                {
                    short recommendedVolume = 0;

                    if (!short.TryParse(recommendedVolumeA, out recommendedVolume) || !Microphone.RecommendedVolumeIsValid(recommendedVolume))
                    {
                        break;
                    }

                    var mic = new Microphone(brand, model, recommendedVolume);

                    foreach (Microphone bMic in this.buildInMic)
                    {
                        if (string.Equals(mic.brand, bMic.brand, StringComparison.CurrentCultureIgnoreCase))
                        {
                            mic.brand = bMic.brand;
                            break;
                        }
                    }
                    foreach (Microphone cMic in this.customMicSet.Keys)
                    {
                        if (string.Equals(mic.brand, cMic.brand, StringComparison.CurrentCultureIgnoreCase))
                        {
                            mic.brand = cMic.brand;
                            break;
                        }
                    }

                    bool isRepeat = false;
                    foreach (Microphone microphone in this.buildInMic)
                    {
                        if (microphone.Equals(mic))
                        {
                            isRepeat = true;
                            break;
                        }
                    }

                    if (isRepeat)
                    {
                        break;
                    }

                    if(customMicSet.ContainsKey(mic))
                    {
                        customMicSet.Remove(mic);
                    }

                    customMicSet[mic] = mic;
                }
            }
        }


        //此三种类型设备中，检测是否需要更新数据
        public bool UpdateAudioSystemProduct(Loudspeaker ls, PowerAmplifier pa, Microphone mic)
        {
            System.Diagnostics.Debug.Assert(ls != null && pa != null && mic != null);

            bool isNeedToUpdate = false;

            isNeedToUpdate = (!LoudspeakerIsBuildIn(ls) && UpdateCustomLoudspeaker(ls)) | isNeedToUpdate;
            isNeedToUpdate = (!PowerAmplifierIsBuildIn(pa) && UpdateCustomPowerAmplifier(pa)) | isNeedToUpdate;
            isNeedToUpdate = (!MicrophoneIsBuildIn(mic) && UpdateCustomMicrophone(mic)) | isNeedToUpdate;
            
            if(isNeedToUpdate)
            {
                ExportCustomAudioProductToFile();
            }

            return isNeedToUpdate;
        }

        public bool LoudspeakerIsBuildIn(Loudspeaker ls)
        {
            //如果新增型号与内置型号相同，则需要将新增型号修正
            foreach (Loudspeaker biLS in this.buildInLS)
            {
                if (string.Equals(ls.brand, biLS.brand, StringComparison.CurrentCultureIgnoreCase))
                {
                    ls.brand = biLS.brand;
                    break;
                }
            }

            foreach(Loudspeaker biLS in this.buildInLS)
            {
                if (biLS.Equals(ls))
                {
                    ls.brand = biLS.brand;
                    ls.model = biLS.model;

                    return true;
                }
            }

            return false;
        }

        private bool UpdateCustomLoudspeaker(Loudspeaker ls)
        {
            bool needToUpdate = false;

            //如果之前的品牌型号相同，则应将之前的品牌型号修正
            foreach (Loudspeaker cLS in this.customLSSet.Keys)
            {
                if (string.Equals(cLS.brand, ls.brand, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (lsBrandSet.ContainsKey(cLS.brand))
                    {
                        lsBrandSet.Remove(cLS.brand);
                    }

                    cLS.brand = ls.brand;
                    lsBrandSet[cLS.brand] = null;

                    needToUpdate = true;
                }
            }

            if (this.customLSSet.ContainsKey(ls))
            {
                Loudspeaker cLS = this.customLSSet[ls];

                if (cLS.model != ls.model)
                {
                    cLS.model = ls.model;
                    needToUpdate = true;
                }

                if (cLS.size != ls.size ||
                    cLS.power != ls.power ||
                    cLS.impedance != ls.impedance)
                {
                    cLS.size = ls.size;
                    cLS.power = ls.power;
                    cLS.impedance = ls.impedance;

                    needToUpdate = true;
                }

                return needToUpdate;
            }
            else
            {
                this.customLSSet[ls] = ls;
                lsBrandSet[ls.brand] = null;
                return true;
            }
        }

        public bool PowerAmplifierIsBuildIn(PowerAmplifier pa)
        {
            //如果新增型号与内置型号相同，则需要将新增型号修正
            foreach (PowerAmplifier bPA in this.buildInPA)
            {
                if (string.Equals(pa.brand, bPA.brand, StringComparison.CurrentCultureIgnoreCase))
                {
                    pa.brand = bPA.brand;
                    break;
                }
            }

            foreach (PowerAmplifier bPA in this.buildInPA)
            {
                if (bPA.Equals(pa))
                {
                    pa.brand = bPA.brand;
                    pa.model = bPA.model;

                    return true;
                }
            }

            return false;
        }

        private bool UpdateCustomPowerAmplifier(PowerAmplifier pa)
        {
            bool needToUpdate = false;

            //如果之前的品牌型号相同，则应将之前的品牌型号修正
            foreach (PowerAmplifier cPA in this.customPASet.Keys)
            {
                if (string.Equals(cPA.brand, pa.brand, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (paBrandSet.ContainsKey(cPA.brand))
                    {
                        paBrandSet.Remove(cPA.brand);
                    }

                    cPA.brand = pa.brand;
                    paBrandSet[cPA.brand] = null;

                    needToUpdate = true;
                }
            }

            if (this.customPASet.ContainsKey(pa))
            {
                PowerAmplifier cPA = this.customPASet[pa];

                if (cPA.model != pa.model)
                {
                    cPA.model = pa.model;
                    needToUpdate = true;
                }

                if (cPA.sensitivity != pa.sensitivity ||
                    cPA.power != pa.power ||
                    cPA.impedance != pa.impedance)
                {
                    cPA.sensitivity = pa.sensitivity;
                    cPA.power = pa.power;
                    cPA.impedance = pa.impedance;

                    needToUpdate = true;
                }

                return needToUpdate;
            }
            else
            {
                this.customPASet[pa] = pa;
                paBrandSet[pa.brand] = null;
                return true;
            }
        }

        public bool MicrophoneIsBuildIn(Microphone mic)
        {
            //如果新增型号与内置型号相同，则需要将新增型号修正
            foreach (Microphone bMic in this.buildInMic)
            {
                if (string.Equals(mic.brand, bMic.brand, StringComparison.CurrentCultureIgnoreCase))
                {
                    mic.brand = bMic.brand;
                    break;
                }
            }

            foreach (Microphone biMic in this.buildInMic)
            {
                if (biMic.Equals(mic))
                {
                    biMic.brand = mic.brand;
                    biMic.model = mic.model;

                    return true;
                }
            }

            return false;
        }

        private bool UpdateCustomMicrophone(Microphone mic)
        {
            bool needToUpdate = false;

            foreach (Microphone cMic in this.customMicSet.Keys)
            {
                if (string.Equals(mic.brand, cMic.brand, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (micBrandSet.ContainsKey(cMic.brand))
                    {
                        micBrandSet.Remove(cMic.brand);
                    }

                    cMic.brand = mic.brand;
                    micBrandSet[cMic.brand] = null;

                    break;
                }
            }

            if (this.customMicSet.ContainsKey(mic))
            {
                Microphone cMic = this.customMicSet[mic];

                if (cMic.model != mic.model)
                {
                    cMic.model = mic.model;
                    needToUpdate = true;
                }

                if (cMic.recommendedVolume != mic.recommendedVolume)
                {
                    cMic.recommendedVolume = mic.recommendedVolume;
                    needToUpdate = true;
                }

                return needToUpdate;
            }
            else
            {
                this.customMicSet[mic] = mic;
                micBrandSet[mic.brand] = null;
                return true;
            }
        }

        private void ExportCustomAudioProductToFile()
        {
            XmlDocument xmldoc = CreateAudioProductsXmlDoc();

            try
            {
                xmldoc.Save(externalProductsFilePath);
            }
            catch(Exception ex)
            {
                AEC.Tools.LogHelper.WriteDebug("AudioProductManagement.ExportAllAudioToFile() Exception: ", ex);
                //System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

        private XmlDocument CreateAudioProductsXmlDoc()
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlDeclaration xmldecl;
            xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmldoc.AppendChild(xmldecl);

            //加入根节点 lmAEC
            XmlElement rootEle = xmldoc.CreateElement(rootNodeName);
            xmldoc.AppendChild(rootEle);

            rootEle.AppendChild(CreateLoudspeakerNode(xmldoc, new List<Loudspeaker>(this.customLSSet.Keys)));
            rootEle.AppendChild(CreatePowerAmplifierNode(xmldoc, new List<PowerAmplifier>(this.customPASet.Keys)));
            rootEle.AppendChild(CreateMicrophoneNode(xmldoc, new List<Microphone>(this.customMicSet.Keys)));

            return xmldoc;
        }

        private XmlElement CreateLoudspeakerNode(XmlDocument xmldoc, List<Loudspeaker> loudspeakerList)
        {
            System.Diagnostics.Debug.Assert(loudspeakerList != null);

            XmlElement loudspeaker = xmldoc.CreateElement(lsListNode);

            if (loudspeakerList.Count > 0)
            {
                foreach (Loudspeaker ls in loudspeakerList)
                {
                    XmlElement ele = xmldoc.CreateElement(lsNode);

                    ele.SetAttribute(brandAttr, ls.brand);
                    ele.SetAttribute(modelAttr, ls.model);
                    ele.SetAttribute(powerAttr, ls.power.ToString());
                    ele.SetAttribute(sizeAttr, ls.size.ToString());
                    ele.SetAttribute(impedanceAttr, ls.impedance.ToString());

                    loudspeaker.AppendChild(ele);
                }
            }
            
            return loudspeaker;
        }

        private XmlElement CreatePowerAmplifierNode(XmlDocument xmldoc, List<PowerAmplifier> powerAmplifierList)
        {
            System.Diagnostics.Debug.Assert(powerAmplifierList != null);

            XmlElement powerAmplifierNode = xmldoc.CreateElement(paListNode);

            if (powerAmplifierList.Count > 0)
            {
                foreach (PowerAmplifier pa in powerAmplifierList)
                {
                    XmlElement ele = xmldoc.CreateElement(paNode);

                    ele.SetAttribute(brandAttr, pa.brand);
                    ele.SetAttribute(modelAttr, pa.model);

                    ele.SetAttribute(sensitivityAttr, pa.sensitivity.ToString());
                    ele.SetAttribute(powerAttr, pa.power.ToString());
                    ele.SetAttribute(impedanceAttr, pa.impedance.ToString());

                    powerAmplifierNode.AppendChild(ele);
                }
            }

            return powerAmplifierNode;
        }

        private XmlElement CreateMicrophoneNode(XmlDocument xmldoc, List<Microphone> microphoneList)
        {
            System.Diagnostics.Debug.Assert(microphoneList != null);

            XmlElement microphone = xmldoc.CreateElement(micListNode);

            if (microphoneList.Count > 0)
            {
                foreach (Microphone mic in microphoneList)
                {
                    XmlElement ele = xmldoc.CreateElement(micNode);

                    ele.SetAttribute(brandAttr, mic.brand);
                    ele.SetAttribute(modelAttr, mic.model);

                    ele.SetAttribute(recommendedVolumeAttr, mic.recommendedVolume.ToString());

                    microphone.AppendChild(ele);
                }
            }

            return microphone;
        }

        private XmlElement CreateSpecNode(XmlDocument xmldoc, string name, float value)
        {
            return CreateSpecNode(xmldoc, name, value.ToString());
        }

        private XmlElement CreateSpecNode(XmlDocument xmldoc, string name, int value)
        {
            return CreateSpecNode(xmldoc, name, value.ToString());
        }

        private XmlElement CreateSpecNode(XmlDocument xmldoc, string name, string value)
        {
            XmlElement ele = xmldoc.CreateElement(specNode);
            ele.SetAttribute(nameAttr, name);
            ele.InnerText = value;
            return ele;
        }

        private AudicProductBase[] GetAudicProducts(AudicProductBase[] allAudicProducts, string brand)
        {
            List<AudicProductBase> products = new List<AudicProductBase>();

            foreach (AudicProductBase ap in allAudicProducts)
            {
                if (ap.brand == brand)
                {
                    products.Add(ap);
                }
            }

            return products.ToArray();
        }

        private Loudspeaker[] GetLoudspeakers(Loudspeaker[] allLoudspeaker, string brand)
        {
            List<Loudspeaker> loudspeakers = new List<Loudspeaker>();

            foreach (Loudspeaker ls in allLoudspeaker)
            {
                if (ls.brand == brand)
                {
                    loudspeakers.Add(ls);
                }
            }

            return loudspeakers.ToArray();
        }

        private PowerAmplifier[] GetPowerAmplifiers(PowerAmplifier[] allPowerAmplifier, string brand)
        {
            List<PowerAmplifier> powerAmplifiers = new List<PowerAmplifier>();

            foreach (PowerAmplifier pa in allPowerAmplifier)
            {
                if (pa.brand == brand)
                {
                    powerAmplifiers.Add(pa);
                }
            }

            return powerAmplifiers.ToArray();
        }

        private const string rootNodeName = "CustomAudioProducts";
        
        private const string typeAttr = "type";
        private const string nameAttr = "name";
       
        private const string lsListNode = "loudspeakerList";
        private const string lsNode = "loudspeaker";
        private const string paListNode = "powerAmplifierList";
        private const string paNode = "powerAmplifier";
        private const string micListNode = "microphoneList";
        private const string micNode = "microphone";

        private const string brandAttr = "brand";
        private const string modelAttr = "model";

        private const string specNode = "spec";
        
        private const string powerAttr = "power";
        private const string sizeAttr = "size";
        private const string impedanceAttr = "impedance";
        private const string sensitivityAttr = "sensitivity";
        private const string recommendedVolumeAttr = "recommendedVolume";

        private const string ExternalProductsFileName = "CustomAudioProducts.xml";
    }
}
