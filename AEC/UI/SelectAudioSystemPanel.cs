using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AEC.Core;
using AEC.AudioProducts;
using AEC.Common;
using AEC.Misc;

namespace AEC.UI
{
    public partial class SelectAudioSystemPanel : gtUserControl
    {
        private const short maxVolume = 100;
        private short currVolume = 0;

        private const short maxLSPowerIndex = 38;
        private const short defaultLSPowerIndex = 8;
        private const short LSPowerStep = 50;
        private const short minLSPower = 100;

        private const short maxLSSizeIndex = 15;
        private const short defaultLSSizeIndex = 7;
        private const short minLSSize = 5;

        private const short maxImpedanceIndex = 99;
        private const short defaultImpedanceIndex = 7;
        private const short minImpedance = 1;

        private const short maxPAPowerIndex = 78;
        private const short defaultPAPowerIndex = 18;
        private const short PAPowerStep = 50;
        private const short minPAPower = 100;

        private const string PowerUnit = "W";
        private const string ImpedanceUnit = "ohms";
        private const string SizeUnit = "”";
        private const string SensitivityUnit = "V";

        private AudioProductManagement apm = AudioProductManagement.GetInstance();

        public SelectAudioSystemPanel()
        {
            InitializeComponent();

            InitCmb();
            InitParam();

            rdoWireless.isCheckInit = true;

            //this.title = "系统选择";
        }

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000; // 用双缓冲绘制窗口的所有子控件
        //        return cp;
        //    }
        //}


        //获取音箱功率标签
        private string GetLSPowerLabel(short index)
        {
            return string.Format("{0} {1}", (index * LSPowerStep + minLSPower), PowerUnit);
        }

        //获取音箱尺寸标签
        private string GetLSSizeLabel(short index)
        {
            return string.Format("{0} {1}", (index + minLSSize), SizeUnit);
        }

        //获取音箱阻抗标签
        private string GetLSImpedanceLabel(short index)
        {
            return string.Format("{0} {1}", (index + minImpedance), ImpedanceUnit);
        }


        //获取功放功率标签
        private string GetPAPowerLabel(short index)
        {
            return string.Format("{0} {1}", (index * PAPowerStep + minPAPower), PowerUnit);
        }

        //获取功放阻抗标签
        private string GetImpedanceLabel(short index)
        {
            return string.Format("{0} {1}", (index + maxImpedanceIndex), ImpedanceUnit);
        }

        //从功率标签计算音箱功率
        private short CalcLSPowerIndexFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, PowerUnit);

            short value;
            if (short.TryParse(str, out value))
            {
                return (short)Math.Round((float)(value - minLSPower) / LSPowerStep);
            }

            return -1;
        }

        //从尺寸标签计算音箱尺寸
        private short CalcLSSizeIndexFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, SizeUnit);

            short value;
            if (short.TryParse(str, out value))
            {
                return (short)Math.Round((float)(value - minLSSize));
            }

            return -1;
        }

        //从尺寸标签计算音箱阻抗
        private short CalcLSImpedanceIndexFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, ImpedanceUnit);

            short value;
            if (short.TryParse(str, out value))
            {
                return (short)Math.Round((float)(value - minImpedance));
            }

            return -1;
        }

        //从功率标签计算音箱功率
        private short CalcPAPowerIndexFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, PowerUnit);

            short value;
            if (short.TryParse(str, out value))
            {
                return (short)Math.Round((float)(value - minPAPower) / PAPowerStep);
            }

            return -1;
        }

        private short CalcVolumeIndexFromText(string text)
        {
            short value;
            if (short.TryParse(text, out value))
            {
                return Utils.ConstrainMax(value, maxVolume);
            }

            return -1;
        }

        private void InitCmb()
        {
            //this.cmbLSBrand.Items.Clear();
            this.cmbLSBrand.Items.AddRange(apm.LSBrands);

            //this.cmbPABrand.Items.Clear();
            this.cmbPABrand.Items.AddRange(apm.PABrands);

            //this.cmbMicBrand.Items.Clear();
            this.cmbMicBrand.Items.AddRange(apm.MicBrands);

            this.RegisterCmbTextChanged();

            //Console.WriteLine("Init Cmb...");
            this.cmbLSBrand.Visible = true;
            this.cmbLSModel.Visible = true;
            this.cmbPABrand.Visible = true;
            this.cmbPAModel.Visible = true;
            this.cmbSensitivity.Visible = true;
            this.cmbMicBrand.Visible = true;
            this.cmbMicModel.Visible = true;
        }

        private void RegisterCmbTextChanged()
        {
            this.cmbLSBrand.TextChanged += cmb_SelectedIndexChanged;
            this.cmbLSModel.TextChanged += cmb_SelectedIndexChanged;
            this.cmbPABrand.TextChanged += cmb_SelectedIndexChanged;
            this.cmbPAModel.TextChanged += cmb_SelectedIndexChanged;
            this.cmbMicBrand.TextChanged += cmb_SelectedIndexChanged;
            this.cmbMicModel.TextChanged += cmb_SelectedIndexChanged;
        }

        private void RemoveCmbTextChanged()
        {
            this.cmbLSBrand.TextChanged -= cmb_SelectedIndexChanged;
            this.cmbLSModel.TextChanged -= cmb_SelectedIndexChanged;
            this.cmbPABrand.TextChanged -= cmb_SelectedIndexChanged;
            this.cmbPAModel.TextChanged -= cmb_SelectedIndexChanged;
            this.cmbMicBrand.TextChanged -= cmb_SelectedIndexChanged;
            this.cmbMicModel.TextChanged -= cmb_SelectedIndexChanged;
        }

        private void InitParam()
        {
            this.paramLSPower.Init(maxLSPowerIndex, defaultLSPowerIndex, GetLSPowerLabel, CalcLSPowerIndexFromText);
            this.paramLSSize.Init(maxLSSizeIndex, defaultLSSizeIndex, GetLSSizeLabel, CalcLSSizeIndexFromText);
            this.paramLSImpedance.Init(maxImpedanceIndex, defaultImpedanceIndex, GetLSImpedanceLabel, CalcLSImpedanceIndexFromText);

            this.paramPAPower.Init(maxPAPowerIndex, defaultPAPowerIndex, GetPAPowerLabel, CalcPAPowerIndexFromText);
            this.paramPAImpedance.Init(maxImpedanceIndex, defaultImpedanceIndex, GetLSImpedanceLabel, CalcLSImpedanceIndexFromText);

            this.paramLabelRecommendedVolume.Init(
                maxVolume,
                currVolume,
                delegate(short index) { return index.ToString(); },
                CalcVolumeIndexFromText
                );
        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender.Equals(this.cmbLSBrand))
            {
                this.cmbLSModel.Items.Clear();
                string lsBrand = this.cmbLSBrand.Text.Trim();
                if (!string.IsNullOrEmpty(lsBrand))
                {
                    this.cmbLSModel.Items.AddRange(apm.GetLSModels(lsBrand));
                }
                this.cmbLSModel.Text = string.Empty;
                //UpdateLSSpecs();
            }
            else if(sender.Equals(this.cmbLSModel))
            {
                UpdateLSSpecs();
            }
            else if (sender.Equals(this.cmbPABrand))
            {
                this.cmbPAModel.Items.Clear();
                string paBrand = this.cmbPABrand.Text.Trim();
                if (!string.IsNullOrEmpty(paBrand))
                {
                    this.cmbPAModel.Items.AddRange(apm.GetPAModels(paBrand));
                }
                this.cmbPAModel.Text = string.Empty;
                //UpdatePASpecs();
            }
            else if(sender.Equals(this.cmbPAModel))
            {
                UpdatePASpecs();
            }
            else if (sender.Equals(this.cmbMicBrand))
            {
                this.cmbMicModel.Items.Clear();
                string micBrand = this.cmbMicBrand.Text.Trim();
                if (!string.IsNullOrEmpty(micBrand))
                {
                    this.cmbMicModel.Items.AddRange(apm.GetMicModels(micBrand));
                }
                this.cmbMicModel.Text = string.Empty;
            }
            else if(sender.Equals(this.cmbMicModel))
            {
                UpdateMicSpecs();
            }
        }

        private void UpdateLSSpecs()
        {
            String brand = this.cmbLSBrand.Text.Trim();
            String model = this.cmbLSModel.Text.Trim();

            if (!string.IsNullOrEmpty(brand) &&
                !string.IsNullOrEmpty(model))
            {
                Loudspeaker currLS = apm.GetLoudSpeaker(brand, model);

                if (currLS != null)
                {
                    short lsPowerIndex = (short)((currLS.power - minLSPower) / LSPowerStep);
                    short lsSizeIndex = (short)((currLS.size - minLSSize));
                    short lsImpedanceIndex = (short)(currLS.impedance - minImpedance);

                    this.paramLSPower.initV = lsPowerIndex;
                    this.paramLSSize.initV = lsSizeIndex;
                    this.paramLSImpedance.initV = lsImpedanceIndex;

                    bool lsIsBI = apm.LoudspeakerIsBuildIn(currLS);

                    this.paramLSPower.Enabled = !lsIsBI;
                    this.paramLSSize.Enabled = !lsIsBI;
                    this.paramLSImpedance.Enabled = !lsIsBI;

                    return;
                }
            }
            
            this.paramLSPower.Enabled = true;
            this.paramLSSize.Enabled = true;
            this.paramLSImpedance.Enabled = true;
        }

        private void UpdatePASpecs()
        {
            String brand = this.cmbPABrand.Text.Trim();
            String model = this.cmbPAModel.Text.Trim();

            if (!string.IsNullOrEmpty(brand) &&
                !string.IsNullOrEmpty(model))
            {
                PowerAmplifier currPA = apm.GetPowerAmplifier(brand, model);

                if (currPA != null)
                {
                    short lsPowerIndex = (short)((currPA.power - minPAPower) / PAPowerStep);
                    short lsImpedanceIndex = (short)(currPA.impedance - minImpedance);

                    this.paramPAPower.initV = lsPowerIndex;
                    this.paramPAImpedance.initV = lsImpedanceIndex;

                    bool lsIsBI = apm.PowerAmplifierIsBuildIn(currPA);

                    this.paramPAPower.Enabled = !lsIsBI;
                    this.paramPAImpedance.Enabled = !lsIsBI;

                    return;
                }
            }

            this.paramPAPower.Enabled = true;
            this.paramPAImpedance.Enabled = true;
        }

        private void UpdateMicSpecs()
        {
            String brand = this.cmbMicBrand.Text.Trim();
            String model = this.cmbMicModel.Text.Trim();

            if (!string.IsNullOrEmpty(brand) &&
                !string.IsNullOrEmpty(model))
            {
                Microphone currMic = apm.GetMicrophone(brand, model);

                if (currMic != null)
                {
                    micIsWired = currMic.recommendedVolume == 0;
                    if(currMic.recommendedVolume == 0)
                    {
                        this.rdoWired.isChecked = true;
                    }
                    else
                    {
                        this.rdoWireless.isChecked = true;
                        this.paramLabelRecommendedVolume.initV = currMic.recommendedVolume;
                    }

                    return;
                }
            }
            else if(!string.IsNullOrEmpty(brand))
            {
                var wireingMode = AEC.AudioProducts.AudioProductManagement.GetMicWiringMode(brand);
                //Log.OutputLine("接线方式:{0}", wireingMode);
                if (wireingMode == MicWiringMode.Wired)
                {
                    this.rdoWired.isChecked = true;
                }
                else if (wireingMode == MicWiringMode.Wireless)
                {
                    this.rdoWireless.isChecked = true;
                }
            }
        }

        public void UpdateCmb(Loudspeaker ls, PowerAmplifier pa, Microphone mic)
        {
            RemoveCmbTextChanged();

            if(this.cmbLSBrand.Text != ls.brand)
            {
                this.cmbLSBrand.Text = ls.brand;
            }
            if (this.cmbPABrand.Text != pa.brand)
            {
                this.cmbPABrand.Text = pa.brand;
            }
            if (this.cmbMicBrand.Text != mic.brand)
            {
                this.cmbMicBrand.Text = mic.brand;
            }

            if (this.cmbLSModel.Text != ls.model)
            {
                this.cmbLSModel.Text = ls.model;
            }
            if (this.cmbPAModel.Text != pa.model)
            {
                this.cmbPAModel.Text = pa.model;
            }
            if (this.cmbMicModel.Text != mic.model)
            {
                this.cmbMicModel.Text = mic.model;
            }

            this.cmbLSBrand.Items.Clear();
            this.cmbLSBrand.Items.AddRange(apm.LSBrands);

            this.cmbPABrand.Items.Clear();
            this.cmbPABrand.Items.AddRange(apm.PABrands);

            this.cmbMicBrand.Items.Clear();
            this.cmbMicBrand.Items.AddRange(apm.MicBrands);

            this.cmbLSModel.Items.Clear();
            string[] lsModels = apm.GetLSModels(ls.brand);
            if(lsModels.Length > 0)
            {
                this.cmbLSModel.Items.AddRange(lsModels);
            }

            this.cmbPAModel.Items.Clear();
            string[] paModels = apm.GetPAModels(pa.brand);
            if (paModels.Length > 0)
            {
                this.cmbPAModel.Items.AddRange(paModels);
            }

            this.cmbMicModel.Items.Clear();
            string[] micModels = apm.GetMicModels(mic.brand);
            if (micModels.Length > 0)
            {
                this.cmbMicModel.Items.AddRange(micModels);
            }

            RegisterCmbTextChanged();
        }

        public Loudspeaker CreateLoudspeaker()
        {
            if(string.IsNullOrEmpty(this.cmbLSBrand.Text))
            {
                gtMessageBox.ShowMessage(this.FindForm(), localization[Localization.formExportPresetModeData_LoudspeakerBrand] + localization[Localization.formExportPresetModeData_CannotBeEmpty]);
                return null;
            }
            else if (string.IsNullOrEmpty(this.cmbLSModel.Text))
            {
                gtMessageBox.ShowMessage(this.FindForm(), localization[Localization.formExportPresetModeData_LoudspeakerModel] + localization[Localization.formExportPresetModeData_CannotBeEmpty]);
                return null;
            }

            short power = (short)(this.paramLSPower.v * LSPowerStep + minLSPower);
            short size = (short)(this.paramLSSize.v + minLSSize);
            short impedance = (short)(this.paramLSImpedance.v + minImpedance);

            return new Loudspeaker(this.cmbLSBrand.Text, this.cmbLSModel.Text, power, size, impedance);
        }

        public PowerAmplifier CreatePowerAmplifier()
        {
            if (string.IsNullOrEmpty(this.cmbPABrand.Text))
            {
                gtMessageBox.ShowMessage(this.FindForm(), localization[Localization.formExportPresetModeData_PowerAmplifierBrand] + localization[Localization.formExportPresetModeData_CannotBeEmpty]);
                return null;
            }
            else if (string.IsNullOrEmpty(this.cmbPAModel.Text))
            {
                gtMessageBox.ShowMessage(this.FindForm(), localization[Localization.formExportPresetModeData_PowerAmplifierModel] + localization[Localization.formExportPresetModeData_CannotBeEmpty]);
                return null;
            }
            else if (string.IsNullOrEmpty(this.cmbSensitivity.Text))
            {
                gtMessageBox.ShowMessage(this.FindForm(), localization[Localization.formExportPresetModeData_PowerAmplifierSensitivity] + localization[Localization.formExportPresetModeData_CannotBeEmpty]);
                return null;
            }

            float sensitivity = CalcPASensitivityFromText(this.cmbSensitivity.Text);
            if(sensitivity <= 0.0f)
            {
                gtMessageBox.ShowMessage(this.FindForm(), localization[Localization.formExportPresetModeData_PowerAmplifierSensitivityInputError]);
                return null;
            }

            short power = (short)(this.paramPAPower.v * PAPowerStep + minPAPower);
            short impedance = (short)(this.paramPAImpedance.v + minImpedance);

            return new PowerAmplifier(this.cmbPABrand.Text, this.cmbPAModel.Text, power, impedance, sensitivity);
        }

        public Microphone CreateMicrophone()
        {
            if (string.IsNullOrEmpty(this.cmbMicBrand.Text))
            {
                gtMessageBox.ShowMessage(this.FindForm(), localization[Localization.formExportPresetModeData_MicrophoneBrand] + localization[Localization.formExportPresetModeData_CannotBeEmpty]);
                return null;
            }
            else if (string.IsNullOrEmpty(this.cmbMicModel.Text))
            {
                gtMessageBox.ShowMessage(this.FindForm(), localization[Localization.formExportPresetModeData_MicrophoneModel] + localization[Localization.formExportPresetModeData_CannotBeEmpty]);
                return null;
            }

            short recommendedVolume = 0;
            if(this.rdoWireless.isChecked)
            {
                if(this.paramLabelRecommendedVolume.v == 0)
                {
                    gtMessageBox.ShowMessage(this.FindForm(), localization[Localization.formExportPresetModeData_WirelessMicrophoneRecommendedVolumeCannotBeZero]);
                    return null;
                }

                recommendedVolume = this.paramLabelRecommendedVolume.v;
            }

            return new Microphone(this.cmbMicBrand.Text, this.cmbMicModel.Text, recommendedVolume);
        }

        private float CalcPASensitivityFromText(string text)
        {
            string str = Utils.RemoveStringSuffix(text, SensitivityUnit);

            float value;
            if (float.TryParse(str, out value))
            {
                return value;
            }

            return 0.0f;
        }

        private void rdo_CheckedChanged(object sender, EventArgs e)
        {
            gtRadioButton rdo = sender as gtRadioButton;
            if(!rdo.isChecked)
            {
                return;
            }

            if(rdo.Equals(this.rdoWired))
            {
                micIsWired = true;
            }
            else if(rdo.Equals(this.rdoWireless))
            {
                micIsWired = false;
            }
        }

        private bool micIsWired
        {
            set
            {
                lblRecommendedVolume.Visible = !value;
                paramLabelRecommendedVolume.Visible = !value;
            }
        }

        private Localization localization = Config.GetInstance().localization;
        public void UpdateDisplayLanguage()
        {
            this.title = localization[Localization.panelSystemSelection];
            this.lblSL.Text = localization[Localization.panelSystemSelection_Loudspeaker];
            this.lblPA.Text = localization[Localization.panelSystemSelection_PowerAmplifier];
            this.lblMic.Text = localization[Localization.panelSystemSelection_Microphone];
            this.lblLSBrand.Text = localization[Localization.panelSystemSelection_Brand];
            this.lblLSModel.Text = localization[Localization.panelSystemSelection_Model];
            this.lblLSPower.Text = localization[Localization.panelSystemSelection_Power];
            this.lblLSSize.Text = localization[Localization.panelSystemSelection_Size];
            this.lblLSImpedance.Text = localization[Localization.panelSystemSelection_Impedance];

            this.lblPABrand.Text = localization[Localization.panelSystemSelection_Brand];
            this.lblPAModel.Text = localization[Localization.panelSystemSelection_Model];
            this.lblSensitivity.Text = localization[Localization.panelSystemSelection_Sensitivity];
            this.lblPAPower.Text = localization[Localization.panelSystemSelection_Power];
            this.lblPAImpedance.Text = localization[Localization.panelSystemSelection_Impedance];

            this.lblMicBrand.Text = localization[Localization.panelSystemSelection_Brand];
            this.lblMicModel.Text = localization[Localization.panelSystemSelection_Model];
            this.rdoWired.Text = localization[Localization.panelSystemSelection_Wired];
            this.rdoWireless.Text = localization[Localization.panelSystemSelection_Wireless];
            this.lblRecommendedVolume.Text = localization[Localization.panelSystemSelection_RecommendedVolume];

            //this.Invalidate();
        }
    }
}
