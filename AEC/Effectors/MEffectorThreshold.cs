using System;
using System.Collections.Generic;
using System.Text;
using AEC.Core;

namespace AEC.Effectors
{
    public class MEffectorThreshold
    {
        public const short X5ThresholdBase = -20;
        public const short K320ThresholdBase = -37;
        public const short K220ThresholdBase = -37;
        public const short VHiThresholdBase = -30;
        public const short MXThresholdBase = -33;
        public const short E300ThresholdBase = -36;
        public const short DefaultThresholdBase = -34;

        public const short KXThresholdMax = 51;
        public const short VXThresholdMax = 51;
        public const short MXThresholdMax = 50;
        public const short X5ThresholdMax = 34;

        private AEC.Misc.Config config = AEC.Misc.Config.GetInstance();

        public readonly MAudioEffectorModel effectorModel;

        private ThresholdHelper _thresholdHelper = null;

        private static readonly Dictionary<DeviceModel, Threshold> model2ThresholdBaseMap = new Dictionary<DeviceModel, Threshold>()
        {
            {DeviceModel.M_Audio, new Threshold(K320ThresholdBase, VXThresholdMax)},

            {DeviceModel.X5, new Threshold(X5ThresholdBase, X5ThresholdMax)},
            {DeviceModel.X5W, new Threshold(X5ThresholdBase, X5ThresholdMax)},
            {DeviceModel.X5Pro, new Threshold(X5ThresholdBase, X5ThresholdMax)},
            {DeviceModel.X5OPBT, new Threshold(X5ThresholdBase, X5ThresholdMax)},
            {DeviceModel.VK5S, new Threshold(K320ThresholdBase, KXThresholdMax)},            
            {DeviceModel.VK3S, new Threshold(K220ThresholdBase, KXThresholdMax)},            
            {DeviceModel.VK3I, new Threshold(K220ThresholdBase, KXThresholdMax)},            
            {DeviceModel.VK3W, new Threshold(K220ThresholdBase, KXThresholdMax)},    
            {DeviceModel.VK3W_WithoutBT, new Threshold(K220ThresholdBase, KXThresholdMax)},    
        
            {DeviceModel.VK3WDM, new Threshold(K220ThresholdBase, KXThresholdMax)},  
  
            {DeviceModel.K320, new Threshold(K320ThresholdBase, KXThresholdMax)},
            {DeviceModel.A300, new Threshold(K320ThresholdBase, KXThresholdMax)},
            {DeviceModel.K220, new Threshold(K320ThresholdBase, KXThresholdMax)},

            {DeviceModel.VK3D, new Threshold(K320ThresholdBase, KXThresholdMax)},
            {DeviceModel.VK3BT, new Threshold(K320ThresholdBase, KXThresholdMax)},
            {DeviceModel.VK8, new Threshold(K320ThresholdBase, KXThresholdMax)},
            {DeviceModel.VK5D, new Threshold(K320ThresholdBase, KXThresholdMax)},

            {DeviceModel.VK6S, new Threshold(K320ThresholdBase, KXThresholdMax)},
            {DeviceModel.VK6WS, new Threshold(K320ThresholdBase, KXThresholdMax)},
            {DeviceModel.VK6GS, new Threshold(K320ThresholdBase, KXThresholdMax)},
            {DeviceModel.VK8S, new Threshold(K320ThresholdBase, KXThresholdMax)},
            {DeviceModel.Q3, new Threshold(K320ThresholdBase, KXThresholdMax)},
            {DeviceModel.Q2, new Threshold(K320ThresholdBase, KXThresholdMax)},

            {DeviceModel.VK6Pro, new Threshold(K320ThresholdBase, KXThresholdMax)},
            {DeviceModel.VK8Pro, new Threshold(K320ThresholdBase, KXThresholdMax)},

            {DeviceModel.VK6SPro, new Threshold(K320ThresholdBase, KXThresholdMax)},
            {DeviceModel.VK8SPro, new Threshold(K320ThresholdBase, KXThresholdMax)},

            {DeviceModel.VHi, new Threshold(VHiThresholdBase, VXThresholdMax)},
            {DeviceModel.V0, new Threshold(DefaultThresholdBase, VXThresholdMax)},
            {DeviceModel.V5, new Threshold(DefaultThresholdBase, VXThresholdMax)},
            {DeviceModel.E300, new Threshold(DefaultThresholdBase, VXThresholdMax)},
            {DeviceModel.M5Pro, new Threshold(MXThresholdBase, MXThresholdMax)},
            {DeviceModel.A100, new Threshold(MXThresholdBase, MXThresholdMax)},
        };

        private ThresholdHelper thresholdHelper
        {
            get
            {
                if (this._thresholdHelper == null)
                {
                    this._thresholdHelper = CreateThresholdHelper(this.effectorModel.currModel);
                }

                return this._thresholdHelper;
            }
        }

        public AEC.UI.GetLabelDelegate GetThresholdLabelIndB
        {
            get
            {
                return thresholdHelper.GetThresholdLabelIndB;
            }
        }

        public AEC.UI.GetLabelDelegate GetThresholdLabelIndBu
        {
            get
            {
                return thresholdHelper.GetThresholdLabelIndBu;
            }
        }

        public AEC.UI.CalcIndexFromEditTextDelegate CalcThresholdIndexFromTextIndB
        {
            get
            {
                return thresholdHelper.CalcThresholdIndexFromTextIndB;
            }
        }

        public AEC.UI.CalcIndexFromEditTextDelegate CalcThresholdIndexFromTextIndBu
        {
            get
            {
                return thresholdHelper.CalcThresholdIndexFromTextIndBu;
            }
        }

        public AEC.UI.GetLabelDelegate GetThresholdLabel
        {
            get
            {
                if (config.thresholdUnit == Misc.ThresholdUnit.IndB)
                {
                    return thresholdHelper.GetThresholdLabelIndBu;
                }
                else
                {
                    return thresholdHelper.GetThresholdLabelInV;
                }
            }
        }

        public AEC.UI.CalcIndexFromEditTextDelegate CalcThresholdIndexFromText
        {
            get
            {
                if (config.thresholdUnit == Misc.ThresholdUnit.IndB)
                {
                    return thresholdHelper.CalcThresholdIndexFromTextIndBu;
                }
                else
                {
                    return thresholdHelper.CalcThresholdIndexFromTextInV;
                }
            }
        }
                
        public int maxLevel
        {
            get
            {
                DeviceModel model = this.effectorModel.currModel;                              
                Threshold threshold = model2ThresholdBaseMap[model];
                return threshold.thresholdBase + threshold.thresholdMax;
            }
        }

        public MEffectorThreshold(MAudioEffectorModel effectorModel_)
        {
            this.effectorModel = effectorModel_;            
            this.effectorModel.RegisterModelChangedHandler(EffectorModelChanged);
            this._thresholdHelper = CreateThresholdHelper(this.effectorModel.currModel);
        }

        private void EffectorModelChanged(object sender, EventArgs e)
        {
            this._thresholdHelper = CreateThresholdHelper(this.effectorModel.currModel);
        }

        public static ThresholdHelper CreateThresholdHelper(DeviceModel model)
        {
            if(model == DeviceModel.M_Audio)
            {
                model = ProductsConfig.MAudioCorrespondingModel;
            }
            Threshold threshold = model2ThresholdBaseMap[model];
            return new ThresholdHelper(threshold.thresholdBase, threshold.thresholdMax);
        }
    }
}
