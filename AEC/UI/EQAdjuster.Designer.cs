namespace AEC.UI
{
    partial class EQAdjuster
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
            this.buttonHS = new AEC.UI.gtRadioButton();
            this.buttonLS = new AEC.UI.gtRadioButton();
            this.buttonP = new AEC.UI.gtRadioButton();
            this.paramLabelQ = new AEC.UI.ParamLabel();
            this.paramLabelFreq = new AEC.UI.ParamLabel();
            this.paramLabelGain = new AEC.UI.ParamLabel();
            this.gainSlider = new AEC.UI.GainSlider();
            this.SuspendLayout();
            // 
            // buttonHS
            // 
            this.buttonHS.backChecked = global::AEC.Properties.Resources.HighShelfChecked;
            this.buttonHS.BackColor = System.Drawing.Color.Transparent;
            this.buttonHS.backDisabled = global::AEC.Properties.Resources.HighShelfDisabled;
            this.buttonHS.backHover = global::AEC.Properties.Resources.HighShelfHover;
            this.buttonHS.backNormal = global::AEC.Properties.Resources.HighShelfUnchecked;
            this.buttonHS.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.buttonHS.ForeColor = System.Drawing.Color.White;
            this.buttonHS.isAutoCheck = true;
            this.buttonHS.isChecked = false;
            this.buttonHS.isCheckInit = false;
            this.buttonHS.isShowCheckedBackWhenDsiabled = true;
            this.buttonHS.isStretch = false;
            this.buttonHS.Location = new System.Drawing.Point(4, 103);
            this.buttonHS.Name = "buttonHS";
            this.buttonHS.Size = new System.Drawing.Size(23, 13);
            this.buttonHS.TabIndex = 6;
            this.buttonHS.userGDIPaint = true;
            this.buttonHS.CheckedChanged += new System.EventHandler<System.EventArgs>(this.btn_CheckedChanged);
            // 
            // buttonLS
            // 
            this.buttonLS.backChecked = global::AEC.Properties.Resources.LowShelfChecked;
            this.buttonLS.BackColor = System.Drawing.Color.Transparent;
            this.buttonLS.backDisabled = global::AEC.Properties.Resources.LowShelfDisabled;
            this.buttonLS.backHover = global::AEC.Properties.Resources.LowShelfHover;
            this.buttonLS.backNormal = global::AEC.Properties.Resources.LowShelfUnchecked;
            this.buttonLS.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.buttonLS.ForeColor = System.Drawing.Color.White;
            this.buttonLS.isAutoCheck = true;
            this.buttonLS.isChecked = false;
            this.buttonLS.isCheckInit = false;
            this.buttonLS.isShowCheckedBackWhenDsiabled = true;
            this.buttonLS.isStretch = false;
            this.buttonLS.Location = new System.Drawing.Point(4, 82);
            this.buttonLS.Name = "buttonLS";
            this.buttonLS.Size = new System.Drawing.Size(23, 13);
            this.buttonLS.TabIndex = 5;
            this.buttonLS.userGDIPaint = true;
            this.buttonLS.CheckedChanged += new System.EventHandler<System.EventArgs>(this.btn_CheckedChanged);
            // 
            // buttonP
            // 
            this.buttonP.backChecked = global::AEC.Properties.Resources.PeakingChecked;
            this.buttonP.BackColor = System.Drawing.Color.Transparent;
            this.buttonP.backDisabled = global::AEC.Properties.Resources.PeakingDisabled;
            this.buttonP.backHover = global::AEC.Properties.Resources.PeakingHover;
            this.buttonP.backNormal = global::AEC.Properties.Resources.PeakingUnchecked;
            this.buttonP.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.buttonP.ForeColor = System.Drawing.Color.White;
            this.buttonP.isAutoCheck = true;
            this.buttonP.isChecked = false;
            this.buttonP.isCheckInit = false;
            this.buttonP.isShowCheckedBackWhenDsiabled = true;
            this.buttonP.isStretch = false;
            this.buttonP.Location = new System.Drawing.Point(4, 61);
            this.buttonP.Name = "buttonP";
            this.buttonP.Size = new System.Drawing.Size(23, 13);
            this.buttonP.TabIndex = 4;
            this.buttonP.userGDIPaint = true;
            this.buttonP.CheckedChanged += new System.EventHandler<System.EventArgs>(this.btn_CheckedChanged);
            // 
            // paramLabelQ
            // 
            this.paramLabelQ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paramLabelQ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.paramLabelQ.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramLabelQ.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramLabelQ.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.paramLabelQ.initV = ((short)(0));
            this.paramLabelQ.isShowUpdownButton = true;
            this.paramLabelQ.Location = new System.Drawing.Point(0, 185);
            this.paramLabelQ.Margin = new System.Windows.Forms.Padding(0);
            this.paramLabelQ.Name = "paramLabelQ";
            this.paramLabelQ.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramLabelQ.Padding = new System.Windows.Forms.Padding(2);
            this.paramLabelQ.Size = new System.Drawing.Size(80, 23);
            this.paramLabelQ.TabIndex = 2;
            this.paramLabelQ.upperLimit = ((short)(0));
            this.paramLabelQ.userGDIPaint = false;
            this.paramLabelQ.v = ((short)(0));
            // 
            // paramLabelFreq
            // 
            this.paramLabelFreq.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paramLabelFreq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.paramLabelFreq.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramLabelFreq.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramLabelFreq.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.paramLabelFreq.initV = ((short)(0));
            this.paramLabelFreq.isShowUpdownButton = true;
            this.paramLabelFreq.Location = new System.Drawing.Point(0, 158);
            this.paramLabelFreq.Margin = new System.Windows.Forms.Padding(0);
            this.paramLabelFreq.Name = "paramLabelFreq";
            this.paramLabelFreq.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramLabelFreq.Padding = new System.Windows.Forms.Padding(2);
            this.paramLabelFreq.Size = new System.Drawing.Size(80, 23);
            this.paramLabelFreq.TabIndex = 1;
            this.paramLabelFreq.upperLimit = ((short)(0));
            this.paramLabelFreq.userGDIPaint = false;
            this.paramLabelFreq.v = ((short)(0));
            // 
            // paramLabelGain
            // 
            this.paramLabelGain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paramLabelGain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.paramLabelGain.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramLabelGain.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramLabelGain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.paramLabelGain.initV = ((short)(0));
            this.paramLabelGain.isShowUpdownButton = true;
            this.paramLabelGain.Location = new System.Drawing.Point(0, 131);
            this.paramLabelGain.Margin = new System.Windows.Forms.Padding(0);
            this.paramLabelGain.Name = "paramLabelGain";
            this.paramLabelGain.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramLabelGain.Padding = new System.Windows.Forms.Padding(2);
            this.paramLabelGain.Size = new System.Drawing.Size(80, 23);
            this.paramLabelGain.TabIndex = 0;
            this.paramLabelGain.upperLimit = ((short)(0));
            this.paramLabelGain.userGDIPaint = false;
            this.paramLabelGain.v = ((short)(0));
            // 
            // gainSlider
            // 
            this.gainSlider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.gainSlider.background = global::AEC.Properties.Resources.EQ_gain_slider_background;
            this.gainSlider.backLeftTop = new System.Drawing.Point(0, 5);
            this.gainSlider.Location = new System.Drawing.Point(36, 0);
            this.gainSlider.maximum = ((short)(180));
            this.gainSlider.Name = "gainSlider";
            this.gainSlider.Size = new System.Drawing.Size(45, 128);
            this.gainSlider.TabIndex = 3;
            this.gainSlider.Text = "gainSlider";
            this.gainSlider.thubmCenterX = 12;
            this.gainSlider.thumbDisabled = global::AEC.Properties.Resources.EQ_gain_slider_thumb_disabled;
            this.gainSlider.ThumbEndY = 118;
            this.gainSlider.thumbHeight = 21;
            this.gainSlider.thumbHover = global::AEC.Properties.Resources.EQ_gain_slider_thumb_hover;
            this.gainSlider.thumbNormal = global::AEC.Properties.Resources.EQ_gain_slider_thumb_normal;
            this.gainSlider.thumbPressed = global::AEC.Properties.Resources.EQ_gain_slider_thumb_pressed;
            this.gainSlider.thumbStartY = 10;
            this.gainSlider.thumbWidth = 13;
            this.gainSlider.v = ((short)(0));
            // 
            // EQAdjuster
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.Controls.Add(this.buttonHS);
            this.Controls.Add(this.buttonLS);
            this.Controls.Add(this.buttonP);
            this.Controls.Add(this.paramLabelQ);
            this.Controls.Add(this.paramLabelFreq);
            this.Controls.Add(this.paramLabelGain);
            this.Controls.Add(this.gainSlider);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "EQAdjuster";
            this.Size = new System.Drawing.Size(80, 209);
            this.ResumeLayout(false);

        }

        #endregion

        private GainSlider gainSlider;
        private ParamLabel paramLabelGain;
        private ParamLabel paramLabelFreq;
        private ParamLabel paramLabelQ;
        private gtRadioButton buttonP;
        private gtRadioButton buttonLS;
        private gtRadioButton buttonHS;
    }
}
