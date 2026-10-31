namespace AEC.UI
{
    partial class DEQAdjuster
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
            this.panelMode = new AEC.UI.gtPanel();
            this.rdoSlow = new AEC.UI.gtRadioButton();
            this.rdoFast = new AEC.UI.gtRadioButton();
            this.rdoMedium = new AEC.UI.gtRadioButton();
            this.sliderThreshold = new AEC.UI.HSliderParam();
            this.btn14V = new AEC.UI.gtButton();
            this.lblAmpSensitivity = new AEC.UI.gtLabel();
            this.btn1V = new AEC.UI.gtButton();
            this.lblThreshold = new AEC.UI.gtLabel();
            this.btn0775V = new AEC.UI.gtButton();
            this.lblRelease = new AEC.UI.gtLabel();
            this.sliderRatio = new AEC.UI.HSliderParam();
            this.lblAttack = new AEC.UI.gtLabel();
            this.sliderAttack = new AEC.UI.HSliderParam();
            this.lblRatio = new AEC.UI.gtLabel();
            this.sliderRelease = new AEC.UI.HSliderParam();
            this.lblDM = new AEC.UI.gtLabel();
            this.rdoAbove = new AEC.UI.gtRadioButton();
            this.rdoBelow = new AEC.UI.gtRadioButton();
            this.rdoOff = new AEC.UI.gtRadioButton();
            this.chkEQPass = new AEC.UI.gtCheckBox();
            this.labelQ = new AEC.UI.gtLabel();
            this.labelGain = new AEC.UI.gtLabel();
            this.lblCM = new AEC.UI.gtLabel();
            this.labelFilterType = new AEC.UI.gtLabel();
            this.labelFreq = new AEC.UI.gtLabel();
            this.eqAdjuster = new AEC.UI.EQAdjuster();
            this.panelMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMode
            // 
            this.panelMode.BackColor = System.Drawing.Color.Transparent;
            this.panelMode.Controls.Add(this.rdoSlow);
            this.panelMode.Controls.Add(this.rdoFast);
            this.panelMode.Controls.Add(this.rdoMedium);
            this.panelMode.Controls.Add(this.sliderThreshold);
            this.panelMode.Controls.Add(this.btn14V);
            this.panelMode.Controls.Add(this.lblAmpSensitivity);
            this.panelMode.Controls.Add(this.btn1V);
            this.panelMode.Controls.Add(this.lblThreshold);
            this.panelMode.Controls.Add(this.btn0775V);
            this.panelMode.Controls.Add(this.lblRelease);
            this.panelMode.Controls.Add(this.sliderRatio);
            this.panelMode.Controls.Add(this.lblAttack);
            this.panelMode.Controls.Add(this.sliderAttack);
            this.panelMode.Controls.Add(this.lblRatio);
            this.panelMode.Controls.Add(this.sliderRelease);
            this.panelMode.Controls.Add(this.lblDM);
            this.panelMode.FillColor = System.Drawing.Color.Transparent;
            this.panelMode.ForeColor = System.Drawing.Color.LightGray;
            this.panelMode.Location = new System.Drawing.Point(142, 0);
            this.panelMode.Name = "panelMode";
            this.panelMode.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelMode.Radius = 0;
            this.panelMode.RectColor = System.Drawing.Color.Black;
            this.panelMode.ShowRect = false;
            this.panelMode.Size = new System.Drawing.Size(228, 280);
            this.panelMode.TabIndex = 50;
            this.panelMode.title = null;
            this.panelMode.titleHeight = 20;
            // 
            // rdoSlow
            // 
            this.rdoSlow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoSlow.backChecked = global::AEC.Properties.Resources.rdo_cm_pressed;
            this.rdoSlow.BackColor = System.Drawing.Color.Transparent;
            this.rdoSlow.backDisabled = global::AEC.Properties.Resources.rdo_cm_disabled;
            this.rdoSlow.backHover = global::AEC.Properties.Resources.rdo_cm_hover;
            this.rdoSlow.backNormal = global::AEC.Properties.Resources.rdo_cm_normal;
            this.rdoSlow.ForeColor = System.Drawing.Color.LightGray;
            this.rdoSlow.isAutoCheck = true;
            this.rdoSlow.isChecked = false;
            this.rdoSlow.isCheckInit = false;
            this.rdoSlow.isShowCheckedBackWhenDsiabled = true;
            this.rdoSlow.isStretch = false;
            this.rdoSlow.Location = new System.Drawing.Point(156, 253);
            this.rdoSlow.Name = "rdoSlow";
            this.rdoSlow.Size = new System.Drawing.Size(53, 27);
            this.rdoSlow.TabIndex = 43;
            this.rdoSlow.Text = "慢速";
            this.rdoSlow.userGDIPaint = false;
            this.rdoSlow.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // rdoFast
            // 
            this.rdoFast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoFast.backChecked = global::AEC.Properties.Resources.rdo_cm_pressed;
            this.rdoFast.BackColor = System.Drawing.Color.Transparent;
            this.rdoFast.backDisabled = global::AEC.Properties.Resources.rdo_cm_disabled;
            this.rdoFast.backHover = global::AEC.Properties.Resources.rdo_cm_hover;
            this.rdoFast.backNormal = global::AEC.Properties.Resources.rdo_cm_normal;
            this.rdoFast.ForeColor = System.Drawing.Color.LightGray;
            this.rdoFast.isAutoCheck = true;
            this.rdoFast.isChecked = false;
            this.rdoFast.isCheckInit = false;
            this.rdoFast.isShowCheckedBackWhenDsiabled = true;
            this.rdoFast.isStretch = false;
            this.rdoFast.Location = new System.Drawing.Point(2, 253);
            this.rdoFast.Name = "rdoFast";
            this.rdoFast.Size = new System.Drawing.Size(53, 27);
            this.rdoFast.TabIndex = 45;
            this.rdoFast.Text = "快速";
            this.rdoFast.userGDIPaint = false;
            this.rdoFast.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // rdoMedium
            // 
            this.rdoMedium.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rdoMedium.backChecked = global::AEC.Properties.Resources.rdo_cm_pressed;
            this.rdoMedium.BackColor = System.Drawing.Color.Transparent;
            this.rdoMedium.backDisabled = global::AEC.Properties.Resources.rdo_cm_disabled;
            this.rdoMedium.backHover = global::AEC.Properties.Resources.rdo_cm_hover;
            this.rdoMedium.backNormal = global::AEC.Properties.Resources.rdo_cm_normal;
            this.rdoMedium.ForeColor = System.Drawing.Color.LightGray;
            this.rdoMedium.isAutoCheck = true;
            this.rdoMedium.isChecked = false;
            this.rdoMedium.isCheckInit = false;
            this.rdoMedium.isShowCheckedBackWhenDsiabled = true;
            this.rdoMedium.isStretch = false;
            this.rdoMedium.Location = new System.Drawing.Point(79, 253);
            this.rdoMedium.Name = "rdoMedium";
            this.rdoMedium.Size = new System.Drawing.Size(53, 27);
            this.rdoMedium.TabIndex = 44;
            this.rdoMedium.Text = "中速";
            this.rdoMedium.userGDIPaint = false;
            this.rdoMedium.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // sliderThreshold
            // 
            this.sliderThreshold.BackColor = System.Drawing.Color.Transparent;
            this.sliderThreshold.backgroundDisabled = global::AEC.Properties.Resources.deq_slider_back_disabled;
            this.sliderThreshold.backgroundNormal = global::AEC.Properties.Resources.music_deq_slider_back_normal;
            this.sliderThreshold.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderThreshold.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderThreshold.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderThreshold.Location = new System.Drawing.Point(2, 53);
            this.sliderThreshold.Margin = new System.Windows.Forms.Padding(0);
            this.sliderThreshold.Name = "sliderThreshold";
            this.sliderThreshold.Size = new System.Drawing.Size(224, 23);
            this.sliderThreshold.SliderWidth = 144;
            this.sliderThreshold.TabIndex = 42;
            this.sliderThreshold.thumbCenterY = 11;
            this.sliderThreshold.ThumbEndX = 128;
            this.sliderThreshold.upperLimit = ((short)(100));
            this.sliderThreshold.value = ((short)(0));
            // 
            // btn14V
            // 
            this.btn14V.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btn14V.backDisabled = global::AEC.Properties.Resources.btn_amp_sen_disabled;
            this.btn14V.backHover = global::AEC.Properties.Resources.btn_amp_sen_hover;
            this.btn14V.backNormal = global::AEC.Properties.Resources.btn_amp_sen_normal;
            this.btn14V.backPressed = global::AEC.Properties.Resources.btn_amp_sen_pressed;
            this.btn14V.ForeColor = System.Drawing.Color.White;
            this.btn14V.isStretch = false;
            this.btn14V.Location = new System.Drawing.Point(180, 2);
            this.btn14V.Name = "btn14V";
            this.btn14V.Size = new System.Drawing.Size(44, 23);
            this.btn14V.TabIndex = 46;
            this.btn14V.Text = "1.4V";
            this.btn14V.userGDIPaint = false;
            this.btn14V.Click += new System.EventHandler(this.btn_Click);
            // 
            // lblAmpSensitivity
            // 
            this.lblAmpSensitivity.alignment = AEC.UI.gtLabel.AlignMent.Left;
            this.lblAmpSensitivity.BackColor = System.Drawing.Color.Transparent;
            this.lblAmpSensitivity.ForeColor = System.Drawing.Color.LightGray;
            this.lblAmpSensitivity.Location = new System.Drawing.Point(0, 2);
            this.lblAmpSensitivity.Name = "lblAmpSensitivity";
            this.lblAmpSensitivity.Size = new System.Drawing.Size(74, 23);
            this.lblAmpSensitivity.TabIndex = 40;
            this.lblAmpSensitivity.Text = "功放灵敏度";
            // 
            // btn1V
            // 
            this.btn1V.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btn1V.backDisabled = global::AEC.Properties.Resources.btn_amp_sen_disabled;
            this.btn1V.backHover = global::AEC.Properties.Resources.btn_amp_sen_hover;
            this.btn1V.backNormal = global::AEC.Properties.Resources.btn_amp_sen_normal;
            this.btn1V.backPressed = global::AEC.Properties.Resources.btn_amp_sen_pressed;
            this.btn1V.ForeColor = System.Drawing.Color.White;
            this.btn1V.isStretch = false;
            this.btn1V.Location = new System.Drawing.Point(130, 2);
            this.btn1V.Name = "btn1V";
            this.btn1V.Size = new System.Drawing.Size(44, 23);
            this.btn1V.TabIndex = 46;
            this.btn1V.Text = "1V";
            this.btn1V.userGDIPaint = false;
            this.btn1V.Click += new System.EventHandler(this.btn_Click);
            // 
            // lblThreshold
            // 
            this.lblThreshold.alignment = AEC.UI.gtLabel.AlignMent.Left;
            this.lblThreshold.BackColor = System.Drawing.Color.Transparent;
            this.lblThreshold.ForeColor = System.Drawing.Color.LightGray;
            this.lblThreshold.Location = new System.Drawing.Point(0, 28);
            this.lblThreshold.Name = "lblThreshold";
            this.lblThreshold.Size = new System.Drawing.Size(115, 23);
            this.lblThreshold.TabIndex = 40;
            this.lblThreshold.Text = "启动电平";
            // 
            // btn0775V
            // 
            this.btn0775V.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btn0775V.backDisabled = global::AEC.Properties.Resources.btn_amp_sen_disabled;
            this.btn0775V.backHover = global::AEC.Properties.Resources.btn_amp_sen_hover;
            this.btn0775V.backNormal = global::AEC.Properties.Resources.btn_amp_sen_normal;
            this.btn0775V.backPressed = global::AEC.Properties.Resources.btn_amp_sen_pressed;
            this.btn0775V.ForeColor = System.Drawing.Color.White;
            this.btn0775V.isStretch = false;
            this.btn0775V.Location = new System.Drawing.Point(80, 2);
            this.btn0775V.Name = "btn0775V";
            this.btn0775V.Size = new System.Drawing.Size(44, 23);
            this.btn0775V.TabIndex = 46;
            this.btn0775V.Text = "0.775V";
            this.btn0775V.userGDIPaint = false;
            this.btn0775V.Click += new System.EventHandler(this.btn_Click);
            // 
            // lblRelease
            // 
            this.lblRelease.alignment = AEC.UI.gtLabel.AlignMent.Left;
            this.lblRelease.BackColor = System.Drawing.Color.Transparent;
            this.lblRelease.ForeColor = System.Drawing.Color.LightGray;
            this.lblRelease.Location = new System.Drawing.Point(0, 128);
            this.lblRelease.Name = "lblRelease";
            this.lblRelease.Size = new System.Drawing.Size(115, 23);
            this.lblRelease.TabIndex = 40;
            this.lblRelease.Text = "恢复时间";
            // 
            // sliderRatio
            // 
            this.sliderRatio.BackColor = System.Drawing.Color.Transparent;
            this.sliderRatio.backgroundDisabled = global::AEC.Properties.Resources.deq_slider_back_disabled;
            this.sliderRatio.backgroundNormal = global::AEC.Properties.Resources.music_deq_slider_back_normal;
            this.sliderRatio.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderRatio.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderRatio.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderRatio.Location = new System.Drawing.Point(2, 203);
            this.sliderRatio.Margin = new System.Windows.Forms.Padding(0);
            this.sliderRatio.Name = "sliderRatio";
            this.sliderRatio.Size = new System.Drawing.Size(224, 23);
            this.sliderRatio.SliderWidth = 144;
            this.sliderRatio.TabIndex = 42;
            this.sliderRatio.thumbCenterY = 11;
            this.sliderRatio.ThumbEndX = 128;
            this.sliderRatio.upperLimit = ((short)(100));
            this.sliderRatio.value = ((short)(0));
            // 
            // lblAttack
            // 
            this.lblAttack.alignment = AEC.UI.gtLabel.AlignMent.Left;
            this.lblAttack.BackColor = System.Drawing.Color.Transparent;
            this.lblAttack.ForeColor = System.Drawing.Color.LightGray;
            this.lblAttack.Location = new System.Drawing.Point(0, 78);
            this.lblAttack.Name = "lblAttack";
            this.lblAttack.Size = new System.Drawing.Size(115, 23);
            this.lblAttack.TabIndex = 40;
            this.lblAttack.Text = "启动时间";
            // 
            // sliderAttack
            // 
            this.sliderAttack.BackColor = System.Drawing.Color.Transparent;
            this.sliderAttack.backgroundDisabled = global::AEC.Properties.Resources.deq_slider_back_disabled;
            this.sliderAttack.backgroundNormal = global::AEC.Properties.Resources.music_deq_slider_back_normal;
            this.sliderAttack.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderAttack.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderAttack.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderAttack.Location = new System.Drawing.Point(2, 103);
            this.sliderAttack.Margin = new System.Windows.Forms.Padding(0);
            this.sliderAttack.Name = "sliderAttack";
            this.sliderAttack.Size = new System.Drawing.Size(224, 23);
            this.sliderAttack.SliderWidth = 144;
            this.sliderAttack.TabIndex = 42;
            this.sliderAttack.thumbCenterY = 11;
            this.sliderAttack.ThumbEndX = 128;
            this.sliderAttack.upperLimit = ((short)(100));
            this.sliderAttack.value = ((short)(0));
            // 
            // lblRatio
            // 
            this.lblRatio.alignment = AEC.UI.gtLabel.AlignMent.Left;
            this.lblRatio.BackColor = System.Drawing.Color.Transparent;
            this.lblRatio.ForeColor = System.Drawing.Color.LightGray;
            this.lblRatio.Location = new System.Drawing.Point(0, 178);
            this.lblRatio.Name = "lblRatio";
            this.lblRatio.Size = new System.Drawing.Size(115, 23);
            this.lblRatio.TabIndex = 40;
            this.lblRatio.Text = "动态比例";
            // 
            // sliderRelease
            // 
            this.sliderRelease.BackColor = System.Drawing.Color.Transparent;
            this.sliderRelease.backgroundDisabled = global::AEC.Properties.Resources.deq_slider_back_disabled;
            this.sliderRelease.backgroundNormal = global::AEC.Properties.Resources.music_deq_slider_back_normal;
            this.sliderRelease.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderRelease.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderRelease.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderRelease.Location = new System.Drawing.Point(2, 153);
            this.sliderRelease.Margin = new System.Windows.Forms.Padding(0);
            this.sliderRelease.Name = "sliderRelease";
            this.sliderRelease.Size = new System.Drawing.Size(224, 23);
            this.sliderRelease.SliderWidth = 144;
            this.sliderRelease.TabIndex = 42;
            this.sliderRelease.thumbCenterY = 11;
            this.sliderRelease.ThumbEndX = 128;
            this.sliderRelease.upperLimit = ((short)(100));
            this.sliderRelease.value = ((short)(0));
            // 
            // lblDM
            // 
            this.lblDM.alignment = AEC.UI.gtLabel.AlignMent.Left;
            this.lblDM.BackColor = System.Drawing.Color.Transparent;
            this.lblDM.ForeColor = System.Drawing.Color.LightGray;
            this.lblDM.Location = new System.Drawing.Point(0, 228);
            this.lblDM.Name = "lblDM";
            this.lblDM.Size = new System.Drawing.Size(115, 23);
            this.lblDM.TabIndex = 40;
            this.lblDM.Text = "动态模式";
            // 
            // rdoAbove
            // 
            this.rdoAbove.backChecked = global::AEC.Properties.Resources.rdo_dm_above_pressed;
            this.rdoAbove.BackColor = System.Drawing.Color.Transparent;
            this.rdoAbove.backDisabled = global::AEC.Properties.Resources.rdo_dm_above_normal;
            this.rdoAbove.backHover = global::AEC.Properties.Resources.rdo_dm_above_hover;
            this.rdoAbove.backNormal = global::AEC.Properties.Resources.rdo_dm_above_normal;
            this.rdoAbove.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.rdoAbove.ForeColor = System.Drawing.Color.White;
            this.rdoAbove.isAutoCheck = true;
            this.rdoAbove.isChecked = false;
            this.rdoAbove.isCheckInit = false;
            this.rdoAbove.isShowCheckedBackWhenDsiabled = true;
            this.rdoAbove.isStretch = false;
            this.rdoAbove.Location = new System.Drawing.Point(44, 68);
            this.rdoAbove.Name = "rdoAbove";
            this.rdoAbove.Size = new System.Drawing.Size(35, 12);
            this.rdoAbove.TabIndex = 49;
            this.rdoAbove.userGDIPaint = false;
            this.rdoAbove.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // rdoBelow
            // 
            this.rdoBelow.backChecked = global::AEC.Properties.Resources.rdo_dm_below_pressed;
            this.rdoBelow.BackColor = System.Drawing.Color.Transparent;
            this.rdoBelow.backDisabled = global::AEC.Properties.Resources.rdo_dm_below_normal;
            this.rdoBelow.backHover = global::AEC.Properties.Resources.rdo_dm_below_hover;
            this.rdoBelow.backNormal = global::AEC.Properties.Resources.rdo_dm_below_normal;
            this.rdoBelow.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.rdoBelow.ForeColor = System.Drawing.Color.White;
            this.rdoBelow.isAutoCheck = true;
            this.rdoBelow.isChecked = false;
            this.rdoBelow.isCheckInit = false;
            this.rdoBelow.isShowCheckedBackWhenDsiabled = true;
            this.rdoBelow.isStretch = false;
            this.rdoBelow.Location = new System.Drawing.Point(44, 48);
            this.rdoBelow.Name = "rdoBelow";
            this.rdoBelow.Size = new System.Drawing.Size(35, 12);
            this.rdoBelow.TabIndex = 48;
            this.rdoBelow.userGDIPaint = false;
            this.rdoBelow.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // rdoOff
            // 
            this.rdoOff.backChecked = global::AEC.Properties.Resources.rdo_dm_off_pressed;
            this.rdoOff.BackColor = System.Drawing.Color.Transparent;
            this.rdoOff.backDisabled = global::AEC.Properties.Resources.rdo_dm_off_normal;
            this.rdoOff.backHover = global::AEC.Properties.Resources.rdo_dm_off_hover;
            this.rdoOff.backNormal = global::AEC.Properties.Resources.rdo_dm_off_normal;
            this.rdoOff.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.rdoOff.ForeColor = System.Drawing.Color.White;
            this.rdoOff.isAutoCheck = true;
            this.rdoOff.isChecked = false;
            this.rdoOff.isCheckInit = false;
            this.rdoOff.isShowCheckedBackWhenDsiabled = true;
            this.rdoOff.isStretch = false;
            this.rdoOff.Location = new System.Drawing.Point(44, 27);
            this.rdoOff.Name = "rdoOff";
            this.rdoOff.Size = new System.Drawing.Size(35, 12);
            this.rdoOff.TabIndex = 47;
            this.rdoOff.userGDIPaint = false;
            this.rdoOff.CheckedChanged += new System.EventHandler<System.EventArgs>(this.rdo_CheckedChanged);
            // 
            // chkEQPass
            // 
            this.chkEQPass.BackColor = System.Drawing.Color.Transparent;
            this.chkEQPass.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.chkEQPass.backHover = global::AEC.Properties.Resources.btn_hover;
            this.chkEQPass.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.chkEQPass.backPressed = global::AEC.Properties.Resources.btn_pressed;
            this.chkEQPass.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.chkEQPass.ForeColor = System.Drawing.Color.LightGray;
            this.chkEQPass.isAutoCheck = true;
            this.chkEQPass.isChecked = false;
            this.chkEQPass.isShowCheckedBackWhenDsiabled = false;
            this.chkEQPass.isStretch = true;
            this.chkEQPass.Location = new System.Drawing.Point(46, 252);
            this.chkEQPass.Margin = new System.Windows.Forms.Padding(0);
            this.chkEQPass.Name = "chkEQPass";
            this.chkEQPass.Size = new System.Drawing.Size(80, 30);
            this.chkEQPass.TabIndex = 41;
            this.chkEQPass.Text = "EQ Pass";
            this.chkEQPass.userGDIPaint = true;
            this.chkEQPass.CheckedChanged += new System.EventHandler<System.EventArgs>(this.chkEQPass_CheckedChanged);
            // 
            // labelQ
            // 
            this.labelQ.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.labelQ.BackColor = System.Drawing.Color.Transparent;
            this.labelQ.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelQ.Location = new System.Drawing.Point(2, 226);
            this.labelQ.Name = "labelQ";
            this.labelQ.Size = new System.Drawing.Size(48, 23);
            this.labelQ.TabIndex = 36;
            this.labelQ.Text = "Q值:";
            // 
            // labelGain
            // 
            this.labelGain.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.labelGain.BackColor = System.Drawing.Color.Transparent;
            this.labelGain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelGain.Location = new System.Drawing.Point(2, 172);
            this.labelGain.Name = "labelGain";
            this.labelGain.Size = new System.Drawing.Size(48, 23);
            this.labelGain.TabIndex = 37;
            this.labelGain.Text = "增益:";
            // 
            // lblCM
            // 
            this.lblCM.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblCM.BackColor = System.Drawing.Color.Transparent;
            this.lblCM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCM.Location = new System.Drawing.Point(2, 44);
            this.lblCM.Name = "lblCM";
            this.lblCM.Size = new System.Drawing.Size(36, 23);
            this.lblCM.TabIndex = 38;
            this.lblCM.Text = "模式";
            // 
            // labelFilterType
            // 
            this.labelFilterType.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.labelFilterType.BackColor = System.Drawing.Color.Transparent;
            this.labelFilterType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelFilterType.Location = new System.Drawing.Point(2, 118);
            this.labelFilterType.Name = "labelFilterType";
            this.labelFilterType.Size = new System.Drawing.Size(48, 23);
            this.labelFilterType.TabIndex = 38;
            this.labelFilterType.Text = "类型";
            // 
            // labelFreq
            // 
            this.labelFreq.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.labelFreq.BackColor = System.Drawing.Color.Transparent;
            this.labelFreq.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelFreq.Location = new System.Drawing.Point(2, 199);
            this.labelFreq.Name = "labelFreq";
            this.labelFreq.Size = new System.Drawing.Size(48, 23);
            this.labelFreq.TabIndex = 39;
            this.labelFreq.Text = "频率:";
            // 
            // eqAdjuster
            // 
            this.eqAdjuster.activeColor = System.Drawing.Color.Empty;
            this.eqAdjuster.BackColor = System.Drawing.Color.Transparent;
            this.eqAdjuster.canBeChanged = true;
            this.eqAdjuster.eqIndexBackgroundActive = null;
            this.eqAdjuster.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.eqAdjuster.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.eqAdjuster.isActive = false;
            this.eqAdjuster.isActiveInit = false;
            this.eqAdjuster.isShowIndex = false;
            this.eqAdjuster.Location = new System.Drawing.Point(54, 41);
            this.eqAdjuster.Margin = new System.Windows.Forms.Padding(0);
            this.eqAdjuster.Name = "eqAdjuster";
            this.eqAdjuster.Size = new System.Drawing.Size(80, 209);
            this.eqAdjuster.TabIndex = 0;
            // 
            // DEQAdjuster
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelMode);
            this.Controls.Add(this.rdoAbove);
            this.Controls.Add(this.rdoBelow);
            this.Controls.Add(this.rdoOff);
            this.Controls.Add(this.chkEQPass);
            this.Controls.Add(this.labelQ);
            this.Controls.Add(this.labelGain);
            this.Controls.Add(this.lblCM);
            this.Controls.Add(this.labelFilterType);
            this.Controls.Add(this.labelFreq);
            this.Controls.Add(this.eqAdjuster);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DEQAdjuster";
            this.Size = new System.Drawing.Size(380, 288);
            this.panelMode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private EQAdjuster eqAdjuster;
        private gtLabel labelQ;
        private gtLabel labelGain;
        private gtLabel labelFilterType;
        private gtLabel labelFreq;
        private gtLabel lblAmpSensitivity;
        private gtCheckBox chkEQPass;
        private HSliderParam sliderThreshold;
        private gtLabel lblThreshold;
        private gtLabel lblAttack;
        private HSliderParam sliderAttack;
        private gtLabel lblRelease;
        private gtLabel lblRatio;
        private HSliderParam sliderRelease;
        private HSliderParam sliderRatio;
        private gtLabel lblDM;
        private gtRadioButton rdoSlow;
        private gtRadioButton rdoMedium;
        private gtRadioButton rdoFast;
        private gtButton btn0775V;
        private gtButton btn1V;
        private gtButton btn14V;
        private gtRadioButton rdoAbove;
        private gtRadioButton rdoBelow;
        private gtRadioButton rdoOff;
        private gtLabel lblCM;
        private gtPanel panelMode;
    }
}
