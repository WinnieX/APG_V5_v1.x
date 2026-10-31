using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;
using AEC.Common;

namespace AEC.Effectors
{
    public class MAudioEffectorModel
    {
        private DeviceModel _currModel = DeviceModel.M_Audio;
        private DeviceModel _nextModel = DeviceModel.M_Audio;

        private event EventHandler modelChanged;

        public MAudioEffectorModel(DeviceModel deviceModel)
        {
            this._currModel = deviceModel;
            this._nextModel = this._currModel;
        }

        public DeviceModel currModel
        {
            get
            {
                if (this._currModel != DeviceModel.M_Audio)
                {
                    return _currModel;
                }
                else
                {
                    return ProductsConfig.MAudioCorrespondingModel;
                }
            }
            set
            {
                if(this._currModel != value)
                {
                    this._currModel = value;
                    
                    if (modelChanged != null)
                    {
                        modelChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public DeviceModel nextModel
        {
            get
            {
                return _nextModel;
            }
            set
            {
                if (this._nextModel != value)
                {
                    this._nextModel = value;
                }
            }
        }

        public string modelLabel
        {
            get
            {
                return GetModelLabel(_currModel);
            }
        }

        //public string nextModelType
        //{
        //    get
        //    {
        //        return EffectorModelToType(this._nextModel);
        //    }
        //}

        //获取型号序列化后类型
        //VK3、VK5序列化后为VKx
        public string currModelSeralizeType
        {
            get
            {
                if (ProductsConfig.ProductIsMengBrand(this.currModel))
                {
                    MEffectorFamily effectorFamily = ProductsConfig.EffectorModelToFamily(this.currModel);

                    if (effectorFamily == MEffectorFamily.X5)
                    {
                        return ProductsConfig.ProductFamily_X5;
                    }
                    else if (effectorFamily == MEffectorFamily.VKxS)
                    {
                        return ProductsConfig.ProductFamily_VKxS;
                    }
                    else if (effectorFamily == MEffectorFamily.VKxDM)
                    {
                        return ProductsConfig.ProductFamily_VKxDM;
                    }
                    else if (effectorFamily == MEffectorFamily.KX)
                    {
                        return ProductsConfig.ProductFamily_VKx;
                    }
                    else if (effectorFamily == MEffectorFamily.VKxSCM)
                    {
                        return ProductsConfig.ProductFamily_VKxSCM;
                    }
                    else if (effectorFamily == MEffectorFamily.VKxDCM)
                    {
                        return ProductsConfig.ProductFamily_VKxDCM;
                    }
                    else if (effectorFamily == MEffectorFamily.VKxPII)
                    {
                        return ProductsConfig.ProductFamily_VKxPII;
                    }
                    else if (effectorFamily == MEffectorFamily.VKxPIIDCM)
                    {
                        return ProductsConfig.ProductFamily_VKxPIIDCM;
                    }
                    else
                    {
                        return currModelType;
                    }
                }
                else if (ProductsConfig.ProductIsNeutral(this.currModel))
                {
                    return currModelType;
                }
                else
                {
                    string brand = ProductsConfig.GetOEMBrand(this._currModel);
                    if (currModelType.StartsWith(brand))
                    {
                        return currModelType;
                    }
                    else
                    {
                        return string.Format("{0} {1}", ProductsConfig.GetOEMBrand(this._currModel), currModelType);
                    }
                }
            }
        }

        public string currModelType
        {
            get
            {
                return EffectorModelToType(this._currModel);
            }
        }

        //获取当前型号的预设文件夹名
        public string[] currModelPresetMainFolder
        {
            get
            {

                List<string> folders = new List<string>();

                if (ProductsConfig.ProductIsMengBrand(this.currModel))
                {
                    MEffectorFamily effectorFamily = ProductsConfig.EffectorModelToFamily(this.currModel);

                    if (effectorFamily == MEffectorFamily.VKxPII)
                    {
                        folders.Add(ProductsConfig.ProductFamily_VKxPII);
                    }
                    else if (effectorFamily == MEffectorFamily.VKxPIIDCM)
                    {
                        folders.Add(ProductsConfig.ProductFamily_VKxPIIDCM);
                    }
                    else if (effectorFamily == MEffectorFamily.VKxSCM)
                    {
                        folders.Add(ProductsConfig.ProductFamily_VKxSCM);
                    }
                    else if (effectorFamily == MEffectorFamily.VKxDCM)
                    {
                        folders.Add(ProductsConfig.ProductFamily_VKxDCM);
                    }
                    else if(effectorFamily == MEffectorFamily.X5)
                    {
                        folders.Add(ProductsConfig.ProductFamily_X5);
                    }
                    else if(effectorFamily == MEffectorFamily.VKxS)
                    {
                        folders.Add(ProductsConfig.ProductFamily_VKxS);
                        folders.Add(ProductsConfig.ProductFamily_X5);
                    }
                    else if (effectorFamily == MEffectorFamily.VKxDM)
                    {
                        folders.Add(ProductsConfig.ProductFamily_VKxDM);
                    }
                    else if (effectorFamily == MEffectorFamily.KX)
                    {
                        folders.Add(ProductsConfig.ProductFamily_VKx);
                    }
                    else
                    {
                        folders.Add(currModelType);
                    }
                }
                else if(ProductsConfig.ProductIsNeutral(this.currModel))
                {
                    folders.Add(currModelType);
                }
                else
                {
                    string brand = ProductsConfig.GetOEMBrand(this._currModel);
                    if (currModelType.StartsWith(brand))
                    {
                        folders.Add(currModelType);
                    }
                    else
                    {
                        folders.Add(string.Format("{0} {1}", ProductsConfig.GetOEMBrand(this._currModel), currModelType));
                    }
                }

                return folders.ToArray();
            }
        }

        public static string GetModelLabel(DeviceModel model)
        {
            if (ProductsConfig.ProductIsMengBrand(model))
            {
                if (ProductsConfig.deviceModelMap.ContainsKey(model))
                {
                    return ProductsConfig.deviceModelMap[model];
                }
                else
                {
                    return ProductsConfig.Model_MAudio_Presentation;
                }
            }
            else
            {
                if(ProductsConfig.ProductIsNeutral(model))
                {
                    return ProductsConfig.GetOEMBrand(model);
                }
                else
                {
                    if (ProductsConfig.deviceModelMap.ContainsKey(model))
                    {
                        return ProductsConfig.deviceModelMap[model];
                    }
                    else
                    {
                        return ProductsConfig.Model_MAudio_Presentation;
                    }
                }
            }
        }

        public static string EffectorModelToType(DeviceModel model)
        {
            if (ProductsConfig.deviceModelMap.ContainsKey(model))
            {
                return ProductsConfig.deviceModelMap[model];
            }
            else
            {
                return ProductsConfig.deviceModelMap[ProductsConfig.MAudioCorrespondingModel];
            }
        }

        public void RegisterModelChangedHandler(EventHandler handler)
        {
            this.modelChanged += handler;
        }

        public void RemoveModelChangedHandler(EventHandler handler)
        {
            this.modelChanged -= handler;
        }

        //public static bool CheckModelIsSupported(string modelLabel)
        //{
        //    foreach (DeviceModel model in AppConfig.supportedDeviceModelList)
        //    {
        //        if (deviceModelMap.ContainsKey(model) && deviceModelMap[model] == modelLabel)
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}
    }
}
