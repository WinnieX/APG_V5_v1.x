namespace AEC.UI
{
    partial class SelectAudioSystemPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitLine2 = new AEC.UI.SplitLine();
            this.splitLine1 = new AEC.UI.SplitLine();
            this.paramLSImpedance = new AEC.UI.ParamLabel();
            this.paramPAImpedance = new AEC.UI.ParamLabel();
            this.paramLSSize = new AEC.UI.ParamLabel();
            this.paramPAPower = new AEC.UI.ParamLabel();
            this.paramLSPower = new AEC.UI.ParamLabel();
            this.paramLabelRecommendedVolume = new AEC.UI.ParamLabel();
            this.rdoWireless = new AEC.UI.gtRadioButtonWithText();
            this.rdoWired = new AEC.UI.gtRadioButtonWithText();
            this.cmbPAModel = new AEC.UI.gtComboBox();
            this.cmbPABrand = new AEC.UI.gtComboBox();
            this.cmbMicModel = new AEC.UI.gtComboBox();
            this.cmbLSModel = new AEC.UI.gtComboBox();
            this.cmbMicBrand = new AEC.UI.gtComboBox();
            this.lblLSImpedance = new AEC.UI.gtLabel();
            this.lblPAImpedance = new AEC.UI.gtLabel();
            this.cmbLSBrand = new AEC.UI.gtComboBox();
            this.lblLSSize = new AEC.UI.gtLabel();
            this.lblPAPower = new AEC.UI.gtLabel();
            this.cmbSensitivity = new AEC.UI.gtComboBox();
            this.lblLSPower = new AEC.UI.gtLabel();
            this.lblSensitivity = new AEC.UI.gtLabel();
            this.lblRecommendedVolume = new AEC.UI.gtLabel();
            this.lblMicModel = new AEC.UI.gtLabel();
            this.lblPAModel = new AEC.UI.gtLabel();
            this.lblLSModel = new AEC.UI.gtLabel();
            this.lblPABrand = new AEC.UI.gtLabel();
            this.lblMicBrand = new AEC.UI.gtLabel();
            this.lblPA = new AEC.UI.gtLabel();
            this.lblMic = new AEC.UI.gtLabel();
            this.lblLSBrand = new AEC.UI.gtLabel();
            this.lblSL = new AEC.UI.gtLabel();
            this.SuspendLayout();
            // 
            // splitLine2
            // 
            this.splitLine2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.splitLine2.Location = new System.Drawing.Point(127, 220);
            this.splitLine2.Name = "splitLine2";
            this.splitLine2.Size = new System.Drawing.Size(500, 1);
            this.splitLine2.TabIndex = 60;
            this.splitLine2.Text = "splitLine1";
            // 
            // splitLine1
            // 
            this.splitLine1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.splitLine1.Location = new System.Drawing.Point(126, 128);
            this.splitLine1.Name = "splitLine1";
            this.splitLine1.Size = new System.Drawing.Size(500, 1);
            this.splitLine1.TabIndex = 60;
            this.splitLine1.Text = "splitLine1";
            // 
            // paramLSImpedance
            // 
            this.paramLSImpedance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.paramLSImpedance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.paramLSImpedance.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramLSImpedance.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramLSImpedance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.paramLSImpedance.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.paramLSImpedance.initV = ((short)(0));
            this.paramLSImpedance.isShowUpdownButton = true;
            this.paramLSImpedance.Location = new System.Drawing.Point(545, 90);
            this.paramLSImpedance.Margin = new System.Windows.Forms.Padding(0);
            this.paramLSImpedance.Name = "paramLSImpedance";
            this.paramLSImpedance.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramLSImpedance.Padding = new System.Windows.Forms.Padding(2);
            this.paramLSImpedance.Size = new System.Drawing.Size(87, 23);
            this.paramLSImpedance.TabIndex = 59;
            this.paramLSImpedance.upperLimit = ((short)(0));
            this.paramLSImpedance.userGDIPaint = true;
            this.paramLSImpedance.v = ((short)(0));
            // 
            // paramPAImpedance
            // 
            this.paramPAImpedance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.paramPAImpedance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.paramPAImpedance.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramPAImpedance.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramPAImpedance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.paramPAImpedance.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.paramPAImpedance.initV = ((short)(0));
            this.paramPAImpedance.isShowUpdownButton = true;
            this.paramPAImpedance.Location = new System.Drawing.Point(350, 182);
            this.paramPAImpedance.Margin = new System.Windows.Forms.Padding(0);
            this.paramPAImpedance.Name = "paramPAImpedance";
            this.paramPAImpedance.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramPAImpedance.Padding = new System.Windows.Forms.Padding(2);
            this.paramPAImpedance.Size = new System.Drawing.Size(87, 23);
            this.paramPAImpedance.TabIndex = 59;
            this.paramPAImpedance.upperLimit = ((short)(0));
            this.paramPAImpedance.userGDIPaint = true;
            this.paramPAImpedance.v = ((short)(0));
            // 
            // paramLSSize
            // 
            this.paramLSSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.paramLSSize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.paramLSSize.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramLSSize.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramLSSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.paramLSSize.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.paramLSSize.initV = ((short)(0));
            this.paramLSSize.isShowUpdownButton = true;
            this.paramLSSize.Location = new System.Drawing.Point(350, 90);
            this.paramLSSize.Margin = new System.Windows.Forms.Padding(0);
            this.paramLSSize.Name = "paramLSSize";
            this.paramLSSize.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramLSSize.Padding = new System.Windows.Forms.Padding(2);
            this.paramLSSize.Size = new System.Drawing.Size(87, 23);
            this.paramLSSize.TabIndex = 59;
            this.paramLSSize.upperLimit = ((short)(0));
            this.paramLSSize.userGDIPaint = true;
            this.paramLSSize.v = ((short)(0));
            // 
            // paramPAPower
            // 
            this.paramPAPower.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.paramPAPower.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.paramPAPower.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramPAPower.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramPAPower.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.paramPAPower.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.paramPAPower.initV = ((short)(0));
            this.paramPAPower.isShowUpdownButton = true;
            this.paramPAPower.Location = new System.Drawing.Point(178, 182);
            this.paramPAPower.Margin = new System.Windows.Forms.Padding(0);
            this.paramPAPower.Name = "paramPAPower";
            this.paramPAPower.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramPAPower.Padding = new System.Windows.Forms.Padding(2);
            this.paramPAPower.Size = new System.Drawing.Size(87, 23);
            this.paramPAPower.TabIndex = 59;
            this.paramPAPower.upperLimit = ((short)(0));
            this.paramPAPower.userGDIPaint = true;
            this.paramPAPower.v = ((short)(0));
            // 
            // paramLSPower
            // 
            this.paramLSPower.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.paramLSPower.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.paramLSPower.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramLSPower.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramLSPower.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.paramLSPower.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.paramLSPower.initV = ((short)(0));
            this.paramLSPower.isShowUpdownButton = true;
            this.paramLSPower.Location = new System.Drawing.Point(178, 90);
            this.paramLSPower.Margin = new System.Windows.Forms.Padding(0);
            this.paramLSPower.Name = "paramLSPower";
            this.paramLSPower.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramLSPower.Padding = new System.Windows.Forms.Padding(2);
            this.paramLSPower.Size = new System.Drawing.Size(87, 23);
            this.paramLSPower.TabIndex = 59;
            this.paramLSPower.upperLimit = ((short)(0));
            this.paramLSPower.userGDIPaint = true;
            this.paramLSPower.v = ((short)(0));
            // 
            // paramLabelRecommendedVolume
            // 
            this.paramLabelRecommendedVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.paramLabelRecommendedVolume.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.paramLabelRecommendedVolume.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramLabelRecommendedVolume.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramLabelRecommendedVolume.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.paramLabelRecommendedVolume.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.paramLabelRecommendedVolume.initV = ((short)(0));
            this.paramLabelRecommendedVolume.isShowUpdownButton = true;
            this.paramLabelRecommendedVolume.Location = new System.Drawing.Point(435, 277);
            this.paramLabelRecommendedVolume.Margin = new System.Windows.Forms.Padding(0);
            this.paramLabelRecommendedVolume.Name = "paramLabelRecommendedVolume";
            this.paramLabelRecommendedVolume.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramLabelRecommendedVolume.Padding = new System.Windows.Forms.Padding(2);
            this.paramLabelRecommendedVolume.Size = new System.Drawing.Size(70, 23);
            this.paramLabelRecommendedVolume.TabIndex = 59;
            this.paramLabelRecommendedVolume.upperLimit = ((short)(0));
            this.paramLabelRecommendedVolume.userGDIPaint = true;
            this.paramLabelRecommendedVolume.v = ((short)(0));
            // 
            // rdoWireless
            // 
            this.rdoWireless.backChecked = global::AEC.Properties.Resources.rdo_checked;
            this.rdoWireless.BackColor = System.Drawing.Color.Transparent;
            this.rdoWireless.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoWireless.backHover = global::AEC.Properties.Resources.rdo_hover;
            this.rdoWireless.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoWireless.ForeColor = System.Drawing.Color.LightGray;
            this.rdoWireless.isAutoCheck = true;
            this.rdoWireless.isChecked = false;
            this.rdoWireless.isCheckInit = false;
            this.rdoWireless.isShowCheckedBackWhenDsiabled = true;
            this.rdoWireless.isStretch = false;
            this.rdoWireless.Location = new System.Drawing.Point(216, 273);
            this.rdoWireless.Name = "rdoWireless";
            this.rdoWireless.Size = new System.Drawing.Size(76, 30);
            this.rdoWireless.TabIndex = 57;
            this.rdoWireless.Text = "无线";
            this.rdoWireless.userGDIPaint = true;
            this.rdoWireless.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // rdoWired
            // 
            this.rdoWired.backChecked = global::AEC.Properties.Resources.rdo_checked;
            this.rdoWired.BackColor = System.Drawing.Color.Transparent;
            this.rdoWired.backDisabled = global::AEC.Properties.Resources.rdo_disabled;
            this.rdoWired.backHover = global::AEC.Properties.Resources.rdo_hover;
            this.rdoWired.backNormal = global::AEC.Properties.Resources.rdo_normal;
            this.rdoWired.ForeColor = System.Drawing.Color.LightGray;
            this.rdoWired.isAutoCheck = true;
            this.rdoWired.isChecked = false;
            this.rdoWired.isCheckInit = false;
            this.rdoWired.isShowCheckedBackWhenDsiabled = true;
            this.rdoWired.isStretch = false;
            this.rdoWired.Location = new System.Drawing.Point(134, 273);
            this.rdoWired.Name = "rdoWired";
            this.rdoWired.Size = new System.Drawing.Size(67, 30);
            this.rdoWired.TabIndex = 58;
            this.rdoWired.Text = "有线";
            this.rdoWired.userGDIPaint = true;
            this.rdoWired.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // cmbPAModel
            // 
            this.cmbPAModel.ArrowColor = System.Drawing.Color.Gray;
            this.cmbPAModel.BackColor = System.Drawing.Color.Silver;
            this.cmbPAModel.DropDownBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.cmbPAModel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbPAModel.FormattingEnabled = true;
            this.cmbPAModel.Location = new System.Drawing.Point(350, 140);
            this.cmbPAModel.Margin = new System.Windows.Forms.Padding(0);
            this.cmbPAModel.Name = "cmbPAModel";
            this.cmbPAModel.Size = new System.Drawing.Size(108, 27);
            this.cmbPAModel.TabIndex = 54;
            this.cmbPAModel.Visible = false;
            // 
            // cmbPABrand
            // 
            this.cmbPABrand.ArrowColor = System.Drawing.Color.Gray;
            this.cmbPABrand.BackColor = System.Drawing.Color.Silver;
            this.cmbPABrand.DropDownBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.cmbPABrand.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbPABrand.FormattingEnabled = true;
            this.cmbPABrand.Location = new System.Drawing.Point(178, 140);
            this.cmbPABrand.Margin = new System.Windows.Forms.Padding(0);
            this.cmbPABrand.Name = "cmbPABrand";
            this.cmbPABrand.Size = new System.Drawing.Size(108, 27);
            this.cmbPABrand.TabIndex = 53;
            this.cmbPABrand.Visible = false;
            // 
            // cmbMicModel
            // 
            this.cmbMicModel.ArrowColor = System.Drawing.Color.Gray;
            this.cmbMicModel.BackColor = System.Drawing.Color.Silver;
            this.cmbMicModel.DropDownBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.cmbMicModel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbMicModel.FormattingEnabled = true;
            this.cmbMicModel.Location = new System.Drawing.Point(350, 234);
            this.cmbMicModel.Margin = new System.Windows.Forms.Padding(0);
            this.cmbMicModel.Name = "cmbMicModel";
            this.cmbMicModel.Size = new System.Drawing.Size(108, 27);
            this.cmbMicModel.TabIndex = 52;
            this.cmbMicModel.Visible = false;
            // 
            // cmbLSModel
            // 
            this.cmbLSModel.ArrowColor = System.Drawing.Color.Gray;
            this.cmbLSModel.BackColor = System.Drawing.Color.Silver;
            this.cmbLSModel.DropDownBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.cmbLSModel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbLSModel.FormattingEnabled = true;
            this.cmbLSModel.Location = new System.Drawing.Point(350, 47);
            this.cmbLSModel.Margin = new System.Windows.Forms.Padding(0);
            this.cmbLSModel.Name = "cmbLSModel";
            this.cmbLSModel.Size = new System.Drawing.Size(108, 27);
            this.cmbLSModel.TabIndex = 51;
            this.cmbLSModel.Visible = false;
            // 
            // cmbMicBrand
            // 
            this.cmbMicBrand.ArrowColor = System.Drawing.Color.Gray;
            this.cmbMicBrand.BackColor = System.Drawing.Color.Silver;
            this.cmbMicBrand.DropDownBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.cmbMicBrand.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbMicBrand.FormattingEnabled = true;
            this.cmbMicBrand.Location = new System.Drawing.Point(178, 234);
            this.cmbMicBrand.Margin = new System.Windows.Forms.Padding(0);
            this.cmbMicBrand.Name = "cmbMicBrand";
            this.cmbMicBrand.Size = new System.Drawing.Size(108, 27);
            this.cmbMicBrand.TabIndex = 50;
            this.cmbMicBrand.Visible = false;
            // 
            // lblLSImpedance
            // 
            this.lblLSImpedance.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblLSImpedance.BackColor = System.Drawing.Color.Transparent;
            this.lblLSImpedance.ForeColor = System.Drawing.Color.LightGray;
            this.lblLSImpedance.Location = new System.Drawing.Point(473, 90);
            this.lblLSImpedance.Name = "lblLSImpedance";
            this.lblLSImpedance.Size = new System.Drawing.Size(64, 23);
            this.lblLSImpedance.TabIndex = 46;
            this.lblLSImpedance.Text = "阻抗";
            // 
            // lblPAImpedance
            // 
            this.lblPAImpedance.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblPAImpedance.BackColor = System.Drawing.Color.Transparent;
            this.lblPAImpedance.ForeColor = System.Drawing.Color.LightGray;
            this.lblPAImpedance.Location = new System.Drawing.Point(278, 182);
            this.lblPAImpedance.Name = "lblPAImpedance";
            this.lblPAImpedance.Size = new System.Drawing.Size(64, 23);
            this.lblPAImpedance.TabIndex = 46;
            this.lblPAImpedance.Text = "阻抗";
            // 
            // cmbLSBrand
            // 
            this.cmbLSBrand.ArrowColor = System.Drawing.Color.Gray;
            this.cmbLSBrand.BackColor = System.Drawing.Color.Silver;
            this.cmbLSBrand.DropDownBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.cmbLSBrand.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbLSBrand.ForeColor = System.Drawing.Color.Black;
            this.cmbLSBrand.FormattingEnabled = true;
            this.cmbLSBrand.Location = new System.Drawing.Point(178, 47);
            this.cmbLSBrand.Margin = new System.Windows.Forms.Padding(0);
            this.cmbLSBrand.Name = "cmbLSBrand";
            this.cmbLSBrand.Size = new System.Drawing.Size(108, 27);
            this.cmbLSBrand.TabIndex = 49;
            this.cmbLSBrand.Visible = false;
            // 
            // lblLSSize
            // 
            this.lblLSSize.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblLSSize.BackColor = System.Drawing.Color.Transparent;
            this.lblLSSize.ForeColor = System.Drawing.Color.LightGray;
            this.lblLSSize.Location = new System.Drawing.Point(298, 90);
            this.lblLSSize.Name = "lblLSSize";
            this.lblLSSize.Size = new System.Drawing.Size(44, 23);
            this.lblLSSize.TabIndex = 46;
            this.lblLSSize.Text = "尺寸";
            // 
            // lblPAPower
            // 
            this.lblPAPower.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblPAPower.BackColor = System.Drawing.Color.Transparent;
            this.lblPAPower.ForeColor = System.Drawing.Color.LightGray;
            this.lblPAPower.Location = new System.Drawing.Point(126, 182);
            this.lblPAPower.Name = "lblPAPower";
            this.lblPAPower.Size = new System.Drawing.Size(44, 23);
            this.lblPAPower.TabIndex = 46;
            this.lblPAPower.Text = "功率";
            // 
            // cmbSensitivity
            // 
            this.cmbSensitivity.ArrowColor = System.Drawing.Color.Gray;
            this.cmbSensitivity.BackColor = System.Drawing.Color.Silver;
            this.cmbSensitivity.DropDownBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.cmbSensitivity.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbSensitivity.FormattingEnabled = true;
            this.cmbSensitivity.Items.AddRange(new object[] {
            "0.775 V",
            "1 V",
            "1.4 V"});
            this.cmbSensitivity.Location = new System.Drawing.Point(545, 140);
            this.cmbSensitivity.Margin = new System.Windows.Forms.Padding(0);
            this.cmbSensitivity.Name = "cmbSensitivity";
            this.cmbSensitivity.Size = new System.Drawing.Size(87, 27);
            this.cmbSensitivity.TabIndex = 55;
            this.cmbSensitivity.Visible = false;
            // 
            // lblLSPower
            // 
            this.lblLSPower.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblLSPower.BackColor = System.Drawing.Color.Transparent;
            this.lblLSPower.ForeColor = System.Drawing.Color.LightGray;
            this.lblLSPower.Location = new System.Drawing.Point(126, 90);
            this.lblLSPower.Name = "lblLSPower";
            this.lblLSPower.Size = new System.Drawing.Size(44, 23);
            this.lblLSPower.TabIndex = 46;
            this.lblLSPower.Text = "功率";
            // 
            // lblSensitivity
            // 
            this.lblSensitivity.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblSensitivity.BackColor = System.Drawing.Color.Transparent;
            this.lblSensitivity.ForeColor = System.Drawing.Color.LightGray;
            this.lblSensitivity.Location = new System.Drawing.Point(470, 144);
            this.lblSensitivity.Name = "lblSensitivity";
            this.lblSensitivity.Size = new System.Drawing.Size(67, 23);
            this.lblSensitivity.TabIndex = 38;
            this.lblSensitivity.Text = "灵敏度";
            // 
            // lblRecommendedVolume
            // 
            this.lblRecommendedVolume.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblRecommendedVolume.BackColor = System.Drawing.Color.Transparent;
            this.lblRecommendedVolume.ForeColor = System.Drawing.Color.LightGray;
            this.lblRecommendedVolume.Location = new System.Drawing.Point(305, 277);
            this.lblRecommendedVolume.Name = "lblRecommendedVolume";
            this.lblRecommendedVolume.Size = new System.Drawing.Size(128, 23);
            this.lblRecommendedVolume.TabIndex = 46;
            this.lblRecommendedVolume.Text = "建议音量";
            // 
            // lblMicModel
            // 
            this.lblMicModel.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicModel.BackColor = System.Drawing.Color.Transparent;
            this.lblMicModel.ForeColor = System.Drawing.Color.LightGray;
            this.lblMicModel.Location = new System.Drawing.Point(298, 238);
            this.lblMicModel.Name = "lblMicModel";
            this.lblMicModel.Size = new System.Drawing.Size(44, 23);
            this.lblMicModel.TabIndex = 45;
            this.lblMicModel.Text = "型号";
            // 
            // lblPAModel
            // 
            this.lblPAModel.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblPAModel.BackColor = System.Drawing.Color.Transparent;
            this.lblPAModel.ForeColor = System.Drawing.Color.LightGray;
            this.lblPAModel.Location = new System.Drawing.Point(298, 144);
            this.lblPAModel.Name = "lblPAModel";
            this.lblPAModel.Size = new System.Drawing.Size(44, 23);
            this.lblPAModel.TabIndex = 44;
            this.lblPAModel.Text = "型号";
            // 
            // lblLSModel
            // 
            this.lblLSModel.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblLSModel.BackColor = System.Drawing.Color.Transparent;
            this.lblLSModel.ForeColor = System.Drawing.Color.LightGray;
            this.lblLSModel.Location = new System.Drawing.Point(298, 51);
            this.lblLSModel.Name = "lblLSModel";
            this.lblLSModel.Size = new System.Drawing.Size(44, 23);
            this.lblLSModel.TabIndex = 43;
            this.lblLSModel.Text = "型号";
            // 
            // lblPABrand
            // 
            this.lblPABrand.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblPABrand.BackColor = System.Drawing.Color.Transparent;
            this.lblPABrand.ForeColor = System.Drawing.Color.LightGray;
            this.lblPABrand.Location = new System.Drawing.Point(126, 144);
            this.lblPABrand.Name = "lblPABrand";
            this.lblPABrand.Size = new System.Drawing.Size(44, 23);
            this.lblPABrand.TabIndex = 42;
            this.lblPABrand.Text = "品牌";
            // 
            // lblMicBrand
            // 
            this.lblMicBrand.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicBrand.BackColor = System.Drawing.Color.Transparent;
            this.lblMicBrand.ForeColor = System.Drawing.Color.LightGray;
            this.lblMicBrand.Location = new System.Drawing.Point(126, 238);
            this.lblMicBrand.Name = "lblMicBrand";
            this.lblMicBrand.Size = new System.Drawing.Size(44, 23);
            this.lblMicBrand.TabIndex = 41;
            this.lblMicBrand.Text = "品牌";
            // 
            // lblPA
            // 
            this.lblPA.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblPA.BackColor = System.Drawing.Color.Transparent;
            this.lblPA.ForeColor = System.Drawing.Color.LightGray;
            this.lblPA.Location = new System.Drawing.Point(14, 163);
            this.lblPA.Name = "lblPA";
            this.lblPA.Size = new System.Drawing.Size(96, 23);
            this.lblPA.TabIndex = 40;
            this.lblPA.Text = "功放";
            // 
            // lblMic
            // 
            this.lblMic.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMic.BackColor = System.Drawing.Color.Transparent;
            this.lblMic.ForeColor = System.Drawing.Color.LightGray;
            this.lblMic.Location = new System.Drawing.Point(14, 257);
            this.lblMic.Name = "lblMic";
            this.lblMic.Size = new System.Drawing.Size(96, 23);
            this.lblMic.TabIndex = 39;
            this.lblMic.Text = "麦克风";
            // 
            // lblLSBrand
            // 
            this.lblLSBrand.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblLSBrand.BackColor = System.Drawing.Color.Transparent;
            this.lblLSBrand.ForeColor = System.Drawing.Color.LightGray;
            this.lblLSBrand.Location = new System.Drawing.Point(126, 51);
            this.lblLSBrand.Name = "lblLSBrand";
            this.lblLSBrand.Size = new System.Drawing.Size(44, 23);
            this.lblLSBrand.TabIndex = 47;
            this.lblLSBrand.Text = "品牌";
            // 
            // lblSL
            // 
            this.lblSL.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblSL.BackColor = System.Drawing.Color.Transparent;
            this.lblSL.ForeColor = System.Drawing.Color.LightGray;
            this.lblSL.Location = new System.Drawing.Point(14, 71);
            this.lblSL.Name = "lblSL";
            this.lblSL.Size = new System.Drawing.Size(96, 23);
            this.lblSL.TabIndex = 48;
            this.lblSL.Text = "音箱";
            // 
            // SelectAudioSystemPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.splitLine2);
            this.Controls.Add(this.splitLine1);
            this.Controls.Add(this.paramLSImpedance);
            this.Controls.Add(this.paramPAImpedance);
            this.Controls.Add(this.paramLSSize);
            this.Controls.Add(this.paramPAPower);
            this.Controls.Add(this.paramLSPower);
            this.Controls.Add(this.paramLabelRecommendedVolume);
            this.Controls.Add(this.rdoWireless);
            this.Controls.Add(this.rdoWired);
            this.Controls.Add(this.cmbPAModel);
            this.Controls.Add(this.cmbPABrand);
            this.Controls.Add(this.cmbMicModel);
            this.Controls.Add(this.cmbLSModel);
            this.Controls.Add(this.cmbMicBrand);
            this.Controls.Add(this.lblLSImpedance);
            this.Controls.Add(this.lblPAImpedance);
            this.Controls.Add(this.cmbLSBrand);
            this.Controls.Add(this.lblLSSize);
            this.Controls.Add(this.lblPAPower);
            this.Controls.Add(this.cmbSensitivity);
            this.Controls.Add(this.lblLSPower);
            this.Controls.Add(this.lblSensitivity);
            this.Controls.Add(this.lblRecommendedVolume);
            this.Controls.Add(this.lblMicModel);
            this.Controls.Add(this.lblPAModel);
            this.Controls.Add(this.lblLSModel);
            this.Controls.Add(this.lblPABrand);
            this.Controls.Add(this.lblMicBrand);
            this.Controls.Add(this.lblPA);
            this.Controls.Add(this.lblMic);
            this.Controls.Add(this.lblLSBrand);
            this.Controls.Add(this.lblSL);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "SelectAudioSystemPanel";
            this.Size = new System.Drawing.Size(700, 320);
            this.ResumeLayout(false);

        }

        #endregion

        private gtRadioButtonWithText rdoWireless;
        private gtRadioButtonWithText rdoWired;
        private gtComboBox cmbPAModel;
        private gtComboBox cmbPABrand;
        private gtComboBox cmbMicModel;
        private gtComboBox cmbLSModel;
        private gtComboBox cmbMicBrand;
        private gtComboBox cmbLSBrand;
        private gtComboBox cmbSensitivity;
        private gtLabel lblSensitivity;
        private gtLabel lblRecommendedVolume;
        private gtLabel lblMicModel;
        private gtLabel lblPAModel;
        private gtLabel lblLSModel;
        private gtLabel lblPABrand;
        private gtLabel lblMicBrand;
        private gtLabel lblPA;
        private gtLabel lblMic;
        private gtLabel lblLSBrand;
        private gtLabel lblSL;
        private ParamLabel paramLabelRecommendedVolume;
        private gtLabel lblLSPower;
        private ParamLabel paramLSPower;
        private gtLabel lblLSSize;
        private ParamLabel paramLSSize;
        private gtLabel lblLSImpedance;
        private ParamLabel paramLSImpedance;
        private SplitLine splitLine1;
        private SplitLine splitLine2;
        private gtLabel lblPAPower;
        private gtLabel lblPAImpedance;
        private ParamLabel paramPAPower;
        private ParamLabel paramPAImpedance;

    }
}
