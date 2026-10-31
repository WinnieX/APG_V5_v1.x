namespace AEC.UI.KX
{
    partial class KXEffectPanel
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
            this.gtPanel1 = new AEC.UI.gtPanel();
            this.lblReverbMicHpfSlope = new AEC.UI.gtLabel();
            this.lblReverbMicLpfSlope = new AEC.UI.gtLabel();
            this.lblMicReverb = new AEC.UI.gtLabel();
            this.lblMicReverbDirect = new AEC.UI.gtLabel();
            this.lblReverbMicHpf = new AEC.UI.gtLabel();
            this.lblReverbMicLpf = new AEC.UI.gtLabel();
            this.lblReverbPreDelay = new AEC.UI.gtLabel();
            this.lblReverbDelay = new AEC.UI.gtLabel();
            this.lblReverbBassDamp = new AEC.UI.gtLabel();
            this.lblReverbTrebleDamp = new AEC.UI.gtLabel();
            this.panelEcho = new AEC.UI.gtPanel();
            this.lblEchoHpfSlope = new AEC.UI.gtLabel();
            this.lblEchoLpfSlope = new AEC.UI.gtLabel();
            this.lblMicEcho = new AEC.UI.gtLabel();
            this.lblMicEchoDirect = new AEC.UI.gtLabel();
            this.lblEchoHpf = new AEC.UI.gtLabel();
            this.lblEchoPreDelay = new AEC.UI.gtLabel();
            this.lblEchoLpf = new AEC.UI.gtLabel();
            this.lblEchoRChDelay = new AEC.UI.gtLabel();
            this.lblEchoRChPreDelay = new AEC.UI.gtLabel();
            this.lblEchoDelay = new AEC.UI.gtLabel();
            this.lblEchoRepeat = new AEC.UI.gtLabel();
            this.eqResetAndByPassPanel = new AEC.UI.EQResetAndByPassPanel();
            this.eqGroupPanel = new AEC.UI.EQGroupPanel();
            this.sliderReverbMicHpfType = new AEC.UI.HSliderParam();
            this.sliderReverbMicLpfType = new AEC.UI.HSliderParam();
            this.sliderMicReverb = new AEC.UI.LevelPhase();
            this.sliderMicReverbDirect = new AEC.UI.LevelPhase();
            this.sliderReverbMicHpf = new AEC.UI.HSliderParam();
            this.sliderReverbMicLpf = new AEC.UI.HSliderParam();
            this.sliderReverbTrebleDamp = new AEC.UI.HSliderParam();
            this.sliderReverbPreDelay = new AEC.UI.HSliderParam();
            this.sliderReverbBassDamp = new AEC.UI.HSliderParam();
            this.sliderReverbDelay = new AEC.UI.HSliderParam();
            this.sliderEchoHpfType = new AEC.UI.HSliderParam();
            this.sliderEchoLpfType = new AEC.UI.HSliderParam();
            this.slideMicEcho = new AEC.UI.LevelPhase();
            this.sliderMicEchoDirect = new AEC.UI.LevelPhase();
            this.sliderEchoHpf = new AEC.UI.HSliderParam();
            this.sliderEchoRChDelay = new AEC.UI.HSliderParam();
            this.sliderEchoPreDelay = new AEC.UI.HSliderParam();
            this.sliderEchoRChPreDelay = new AEC.UI.HSliderParam();
            this.sliderEchoLpf = new AEC.UI.HSliderParam();
            this.sliderEchoRepeat = new AEC.UI.HSliderParam();
            this.sliderEchoDelay = new AEC.UI.HSliderParam();
            this.gtPanel1.SuspendLayout();
            this.panelEcho.SuspendLayout();
            this.SuspendLayout();
            // 
            // gtPanel1
            // 
            this.gtPanel1.Controls.Add(this.lblReverbMicHpfSlope);
            this.gtPanel1.Controls.Add(this.sliderReverbMicHpfType);
            this.gtPanel1.Controls.Add(this.lblReverbMicLpfSlope);
            this.gtPanel1.Controls.Add(this.sliderReverbMicLpfType);
            this.gtPanel1.Controls.Add(this.sliderMicReverb);
            this.gtPanel1.Controls.Add(this.lblMicReverb);
            this.gtPanel1.Controls.Add(this.lblMicReverbDirect);
            this.gtPanel1.Controls.Add(this.lblReverbMicHpf);
            this.gtPanel1.Controls.Add(this.sliderMicReverbDirect);
            this.gtPanel1.Controls.Add(this.sliderReverbMicHpf);
            this.gtPanel1.Controls.Add(this.lblReverbMicLpf);
            this.gtPanel1.Controls.Add(this.sliderReverbMicLpf);
            this.gtPanel1.Controls.Add(this.sliderReverbTrebleDamp);
            this.gtPanel1.Controls.Add(this.lblReverbPreDelay);
            this.gtPanel1.Controls.Add(this.sliderReverbPreDelay);
            this.gtPanel1.Controls.Add(this.sliderReverbBassDamp);
            this.gtPanel1.Controls.Add(this.lblReverbDelay);
            this.gtPanel1.Controls.Add(this.lblReverbBassDamp);
            this.gtPanel1.Controls.Add(this.sliderReverbDelay);
            this.gtPanel1.Controls.Add(this.lblReverbTrebleDamp);
            this.gtPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.gtPanel1.ForeColor = System.Drawing.Color.LightGray;
            this.gtPanel1.Location = new System.Drawing.Point(399, 8);
            this.gtPanel1.Name = "gtPanel1";
            this.gtPanel1.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.gtPanel1.Radius = 18;
            this.gtPanel1.RectColor = System.Drawing.Color.Black;
            this.gtPanel1.ShowRect = true;
            this.gtPanel1.Size = new System.Drawing.Size(387, 298);
            this.gtPanel1.TabIndex = 24;
            this.gtPanel1.title = null;
            this.gtPanel1.titleHeight = 20;
            // 
            // lblReverbMicHpfSlope
            // 
            this.lblReverbMicHpfSlope.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblReverbMicHpfSlope.BackColor = System.Drawing.Color.Transparent;
            this.lblReverbMicHpfSlope.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblReverbMicHpfSlope.Location = new System.Drawing.Point(6, 90);
            this.lblReverbMicHpfSlope.Name = "lblReverbMicHpfSlope";
            this.lblReverbMicHpfSlope.Size = new System.Drawing.Size(137, 23);
            this.lblReverbMicHpfSlope.TabIndex = 24;
            this.lblReverbMicHpfSlope.Text = "混响高通斜率";
            // 
            // lblReverbMicLpfSlope
            // 
            this.lblReverbMicLpfSlope.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblReverbMicLpfSlope.BackColor = System.Drawing.Color.Transparent;
            this.lblReverbMicLpfSlope.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblReverbMicLpfSlope.Location = new System.Drawing.Point(7, 146);
            this.lblReverbMicLpfSlope.Name = "lblReverbMicLpfSlope";
            this.lblReverbMicLpfSlope.Size = new System.Drawing.Size(137, 23);
            this.lblReverbMicLpfSlope.TabIndex = 22;
            this.lblReverbMicLpfSlope.Text = "混响麦克风低通斜率";
            // 
            // lblMicReverb
            // 
            this.lblMicReverb.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicReverb.BackColor = System.Drawing.Color.Transparent;
            this.lblMicReverb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMicReverb.Location = new System.Drawing.Point(7, 6);
            this.lblMicReverb.Name = "lblMicReverb";
            this.lblMicReverb.Size = new System.Drawing.Size(137, 23);
            this.lblMicReverb.TabIndex = 19;
            this.lblMicReverb.Text = "MIC混响效果电平";
            // 
            // lblMicReverbDirect
            // 
            this.lblMicReverbDirect.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicReverbDirect.BackColor = System.Drawing.Color.Transparent;
            this.lblMicReverbDirect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMicReverbDirect.Location = new System.Drawing.Point(7, 34);
            this.lblMicReverbDirect.Name = "lblMicReverbDirect";
            this.lblMicReverbDirect.Size = new System.Drawing.Size(137, 23);
            this.lblMicReverbDirect.TabIndex = 19;
            this.lblMicReverbDirect.Text = "MIC混响直达电平";
            // 
            // lblReverbMicHpf
            // 
            this.lblReverbMicHpf.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblReverbMicHpf.BackColor = System.Drawing.Color.Transparent;
            this.lblReverbMicHpf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblReverbMicHpf.Location = new System.Drawing.Point(7, 62);
            this.lblReverbMicHpf.Name = "lblReverbMicHpf";
            this.lblReverbMicHpf.Size = new System.Drawing.Size(137, 23);
            this.lblReverbMicHpf.TabIndex = 19;
            this.lblReverbMicHpf.Text = "混响麦克风高通";
            // 
            // lblReverbMicLpf
            // 
            this.lblReverbMicLpf.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblReverbMicLpf.BackColor = System.Drawing.Color.Transparent;
            this.lblReverbMicLpf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblReverbMicLpf.Location = new System.Drawing.Point(7, 118);
            this.lblReverbMicLpf.Name = "lblReverbMicLpf";
            this.lblReverbMicLpf.Size = new System.Drawing.Size(137, 23);
            this.lblReverbMicLpf.TabIndex = 19;
            this.lblReverbMicLpf.Text = "混响麦克风低通";
            // 
            // lblReverbPreDelay
            // 
            this.lblReverbPreDelay.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblReverbPreDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblReverbPreDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblReverbPreDelay.Location = new System.Drawing.Point(7, 174);
            this.lblReverbPreDelay.Name = "lblReverbPreDelay";
            this.lblReverbPreDelay.Size = new System.Drawing.Size(137, 23);
            this.lblReverbPreDelay.TabIndex = 19;
            this.lblReverbPreDelay.Text = "混响预延时";
            // 
            // lblReverbDelay
            // 
            this.lblReverbDelay.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblReverbDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblReverbDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblReverbDelay.Location = new System.Drawing.Point(7, 202);
            this.lblReverbDelay.Name = "lblReverbDelay";
            this.lblReverbDelay.Size = new System.Drawing.Size(137, 23);
            this.lblReverbDelay.TabIndex = 19;
            this.lblReverbDelay.Text = "混响时间";
            // 
            // lblReverbBassDamp
            // 
            this.lblReverbBassDamp.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblReverbBassDamp.BackColor = System.Drawing.Color.Transparent;
            this.lblReverbBassDamp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblReverbBassDamp.Location = new System.Drawing.Point(7, 230);
            this.lblReverbBassDamp.Name = "lblReverbBassDamp";
            this.lblReverbBassDamp.Size = new System.Drawing.Size(137, 23);
            this.lblReverbBassDamp.TabIndex = 19;
            this.lblReverbBassDamp.Text = "混响低音衰减";
            // 
            // lblReverbTrebleDamp
            // 
            this.lblReverbTrebleDamp.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblReverbTrebleDamp.BackColor = System.Drawing.Color.Transparent;
            this.lblReverbTrebleDamp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblReverbTrebleDamp.Location = new System.Drawing.Point(7, 258);
            this.lblReverbTrebleDamp.Name = "lblReverbTrebleDamp";
            this.lblReverbTrebleDamp.Size = new System.Drawing.Size(137, 23);
            this.lblReverbTrebleDamp.TabIndex = 19;
            this.lblReverbTrebleDamp.Text = "混响高音衰减";
            // 
            // panelEcho
            // 
            this.panelEcho.BackColor = System.Drawing.Color.Transparent;
            this.panelEcho.Controls.Add(this.lblEchoHpfSlope);
            this.panelEcho.Controls.Add(this.sliderEchoHpfType);
            this.panelEcho.Controls.Add(this.lblEchoLpfSlope);
            this.panelEcho.Controls.Add(this.sliderEchoLpfType);
            this.panelEcho.Controls.Add(this.slideMicEcho);
            this.panelEcho.Controls.Add(this.lblMicEcho);
            this.panelEcho.Controls.Add(this.lblMicEchoDirect);
            this.panelEcho.Controls.Add(this.sliderMicEchoDirect);
            this.panelEcho.Controls.Add(this.lblEchoHpf);
            this.panelEcho.Controls.Add(this.sliderEchoHpf);
            this.panelEcho.Controls.Add(this.lblEchoPreDelay);
            this.panelEcho.Controls.Add(this.sliderEchoRChDelay);
            this.panelEcho.Controls.Add(this.lblEchoLpf);
            this.panelEcho.Controls.Add(this.lblEchoRChDelay);
            this.panelEcho.Controls.Add(this.sliderEchoPreDelay);
            this.panelEcho.Controls.Add(this.sliderEchoRChPreDelay);
            this.panelEcho.Controls.Add(this.sliderEchoLpf);
            this.panelEcho.Controls.Add(this.lblEchoRChPreDelay);
            this.panelEcho.Controls.Add(this.lblEchoDelay);
            this.panelEcho.Controls.Add(this.sliderEchoRepeat);
            this.panelEcho.Controls.Add(this.sliderEchoDelay);
            this.panelEcho.Controls.Add(this.lblEchoRepeat);
            this.panelEcho.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelEcho.ForeColor = System.Drawing.Color.LightGray;
            this.panelEcho.Location = new System.Drawing.Point(6, 8);
            this.panelEcho.Name = "panelEcho";
            this.panelEcho.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelEcho.Radius = 18;
            this.panelEcho.RectColor = System.Drawing.Color.Black;
            this.panelEcho.ShowRect = true;
            this.panelEcho.Size = new System.Drawing.Size(387, 298);
            this.panelEcho.TabIndex = 23;
            this.panelEcho.title = null;
            this.panelEcho.titleHeight = 20;
            // 
            // lblEchoHpfSlope
            // 
            this.lblEchoHpfSlope.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEchoHpfSlope.BackColor = System.Drawing.Color.Transparent;
            this.lblEchoHpfSlope.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEchoHpfSlope.Location = new System.Drawing.Point(7, 83);
            this.lblEchoHpfSlope.Name = "lblEchoHpfSlope";
            this.lblEchoHpfSlope.Size = new System.Drawing.Size(137, 23);
            this.lblEchoHpfSlope.TabIndex = 24;
            this.lblEchoHpfSlope.Text = "回声高通斜率";
            // 
            // lblEchoLpfSlope
            // 
            this.lblEchoLpfSlope.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEchoLpfSlope.BackColor = System.Drawing.Color.Transparent;
            this.lblEchoLpfSlope.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEchoLpfSlope.Location = new System.Drawing.Point(7, 135);
            this.lblEchoLpfSlope.Name = "lblEchoLpfSlope";
            this.lblEchoLpfSlope.Size = new System.Drawing.Size(137, 23);
            this.lblEchoLpfSlope.TabIndex = 22;
            this.lblEchoLpfSlope.Text = "回声低通斜率";
            // 
            // lblMicEcho
            // 
            this.lblMicEcho.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicEcho.BackColor = System.Drawing.Color.Transparent;
            this.lblMicEcho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMicEcho.Location = new System.Drawing.Point(7, 5);
            this.lblMicEcho.Name = "lblMicEcho";
            this.lblMicEcho.Size = new System.Drawing.Size(137, 23);
            this.lblMicEcho.TabIndex = 19;
            this.lblMicEcho.Text = "MIC回声效果电平";
            // 
            // lblMicEchoDirect
            // 
            this.lblMicEchoDirect.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMicEchoDirect.BackColor = System.Drawing.Color.Transparent;
            this.lblMicEchoDirect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMicEchoDirect.Location = new System.Drawing.Point(7, 31);
            this.lblMicEchoDirect.Name = "lblMicEchoDirect";
            this.lblMicEchoDirect.Size = new System.Drawing.Size(137, 23);
            this.lblMicEchoDirect.TabIndex = 19;
            this.lblMicEchoDirect.Text = "MIC回声直达电平";
            // 
            // lblEchoHpf
            // 
            this.lblEchoHpf.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEchoHpf.BackColor = System.Drawing.Color.Transparent;
            this.lblEchoHpf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEchoHpf.Location = new System.Drawing.Point(7, 57);
            this.lblEchoHpf.Name = "lblEchoHpf";
            this.lblEchoHpf.Size = new System.Drawing.Size(137, 23);
            this.lblEchoHpf.TabIndex = 19;
            this.lblEchoHpf.Text = "回声高通";
            // 
            // lblEchoPreDelay
            // 
            this.lblEchoPreDelay.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEchoPreDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblEchoPreDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEchoPreDelay.Location = new System.Drawing.Point(7, 161);
            this.lblEchoPreDelay.Name = "lblEchoPreDelay";
            this.lblEchoPreDelay.Size = new System.Drawing.Size(137, 23);
            this.lblEchoPreDelay.TabIndex = 19;
            this.lblEchoPreDelay.Text = "回声预延时";
            // 
            // lblEchoLpf
            // 
            this.lblEchoLpf.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEchoLpf.BackColor = System.Drawing.Color.Transparent;
            this.lblEchoLpf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEchoLpf.Location = new System.Drawing.Point(7, 109);
            this.lblEchoLpf.Name = "lblEchoLpf";
            this.lblEchoLpf.Size = new System.Drawing.Size(137, 23);
            this.lblEchoLpf.TabIndex = 19;
            this.lblEchoLpf.Text = "回声低通";
            // 
            // lblEchoRChDelay
            // 
            this.lblEchoRChDelay.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEchoRChDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblEchoRChDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEchoRChDelay.Location = new System.Drawing.Point(7, 265);
            this.lblEchoRChDelay.Name = "lblEchoRChDelay";
            this.lblEchoRChDelay.Size = new System.Drawing.Size(137, 23);
            this.lblEchoRChDelay.TabIndex = 19;
            this.lblEchoRChDelay.Text = "回声右通道延时";
            // 
            // lblEchoRChPreDelay
            // 
            this.lblEchoRChPreDelay.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEchoRChPreDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblEchoRChPreDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEchoRChPreDelay.Location = new System.Drawing.Point(7, 239);
            this.lblEchoRChPreDelay.Name = "lblEchoRChPreDelay";
            this.lblEchoRChPreDelay.Size = new System.Drawing.Size(137, 23);
            this.lblEchoRChPreDelay.TabIndex = 19;
            this.lblEchoRChPreDelay.Text = "回声右通道预延时";
            // 
            // lblEchoDelay
            // 
            this.lblEchoDelay.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEchoDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblEchoDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEchoDelay.Location = new System.Drawing.Point(7, 187);
            this.lblEchoDelay.Name = "lblEchoDelay";
            this.lblEchoDelay.Size = new System.Drawing.Size(137, 23);
            this.lblEchoDelay.TabIndex = 19;
            this.lblEchoDelay.Text = "回声延时";
            // 
            // lblEchoRepeat
            // 
            this.lblEchoRepeat.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblEchoRepeat.BackColor = System.Drawing.Color.Transparent;
            this.lblEchoRepeat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblEchoRepeat.Location = new System.Drawing.Point(7, 213);
            this.lblEchoRepeat.Name = "lblEchoRepeat";
            this.lblEchoRepeat.Size = new System.Drawing.Size(137, 23);
            this.lblEchoRepeat.TabIndex = 19;
            this.lblEchoRepeat.Text = "回声重复比率";
            // 
            // eqResetAndByPassPanel
            // 
            this.eqResetAndByPassPanel.BackColor = System.Drawing.Color.Transparent;
            this.eqResetAndByPassPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.eqResetAndByPassPanel.isPass = false;
            this.eqResetAndByPassPanel.Location = new System.Drawing.Point(792, 275);
            this.eqResetAndByPassPanel.Name = "eqResetAndByPassPanel";
            this.eqResetAndByPassPanel.Size = new System.Drawing.Size(168, 33);
            this.eqResetAndByPassPanel.TabIndex = 22;
            // 
            // eqGroupPanel
            // 
            this.eqGroupPanel.BackColor = System.Drawing.Color.Transparent;
            this.eqGroupPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.eqGroupPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.eqGroupPanel.Location = new System.Drawing.Point(792, 8);
            this.eqGroupPanel.Name = "eqGroupPanel";
            this.eqGroupPanel.Size = new System.Drawing.Size(478, 258);
            this.eqGroupPanel.TabIndex = 2;
            // 
            // sliderReverbMicHpfType
            // 
            this.sliderReverbMicHpfType.BackColor = System.Drawing.Color.Transparent;
            this.sliderReverbMicHpfType.backgroundDisabled = null;
            this.sliderReverbMicHpfType.backgroundNormal = global::AEC.Properties.Resources.reverb_slider_back;
            this.sliderReverbMicHpfType.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderReverbMicHpfType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderReverbMicHpfType.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderReverbMicHpfType.Location = new System.Drawing.Point(148, 90);
            this.sliderReverbMicHpfType.Margin = new System.Windows.Forms.Padding(0);
            this.sliderReverbMicHpfType.Name = "sliderReverbMicHpfType";
            this.sliderReverbMicHpfType.Size = new System.Drawing.Size(232, 23);
            this.sliderReverbMicHpfType.SliderWidth = 142;
            this.sliderReverbMicHpfType.TabIndex = 25;
            this.sliderReverbMicHpfType.thumbCenterY = 11;
            this.sliderReverbMicHpfType.ThumbEndX = 124;
            this.sliderReverbMicHpfType.upperLimit = ((short)(100));
            this.sliderReverbMicHpfType.value = ((short)(0));
            // 
            // sliderReverbMicLpfType
            // 
            this.sliderReverbMicLpfType.BackColor = System.Drawing.Color.Transparent;
            this.sliderReverbMicLpfType.backgroundDisabled = null;
            this.sliderReverbMicLpfType.backgroundNormal = global::AEC.Properties.Resources.reverb_slider_back;
            this.sliderReverbMicLpfType.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderReverbMicLpfType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderReverbMicLpfType.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderReverbMicLpfType.Location = new System.Drawing.Point(148, 146);
            this.sliderReverbMicLpfType.Margin = new System.Windows.Forms.Padding(0);
            this.sliderReverbMicLpfType.Name = "sliderReverbMicLpfType";
            this.sliderReverbMicLpfType.Size = new System.Drawing.Size(232, 23);
            this.sliderReverbMicLpfType.SliderWidth = 142;
            this.sliderReverbMicLpfType.TabIndex = 23;
            this.sliderReverbMicLpfType.thumbCenterY = 11;
            this.sliderReverbMicLpfType.ThumbEndX = 124;
            this.sliderReverbMicLpfType.upperLimit = ((short)(100));
            this.sliderReverbMicLpfType.value = ((short)(0));
            // 
            // sliderMicReverb
            // 
            this.sliderMicReverb.BackColor = System.Drawing.Color.Transparent;
            this.sliderMicReverb.backgroundDisabled = null;
            this.sliderMicReverb.backgroundNormal = global::AEC.Properties.Resources.reverb_slider_back;
            this.sliderMicReverb.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderMicReverb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderMicReverb.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderMicReverb.Location = new System.Drawing.Point(148, 6);
            this.sliderMicReverb.Name = "sliderMicReverb";
            this.sliderMicReverb.Size = new System.Drawing.Size(232, 23);
            this.sliderMicReverb.SliderWidth = 142;
            this.sliderMicReverb.TabIndex = 21;
            this.sliderMicReverb.thumbCenterY = 11;
            this.sliderMicReverb.ThumbEndX = 124;
            // 
            // sliderMicReverbDirect
            // 
            this.sliderMicReverbDirect.BackColor = System.Drawing.Color.Transparent;
            this.sliderMicReverbDirect.backgroundDisabled = null;
            this.sliderMicReverbDirect.backgroundNormal = global::AEC.Properties.Resources.reverb_slider_back;
            this.sliderMicReverbDirect.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderMicReverbDirect.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderMicReverbDirect.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderMicReverbDirect.Location = new System.Drawing.Point(148, 34);
            this.sliderMicReverbDirect.Name = "sliderMicReverbDirect";
            this.sliderMicReverbDirect.Size = new System.Drawing.Size(232, 23);
            this.sliderMicReverbDirect.SliderWidth = 142;
            this.sliderMicReverbDirect.TabIndex = 21;
            this.sliderMicReverbDirect.thumbCenterY = 11;
            this.sliderMicReverbDirect.ThumbEndX = 124;
            // 
            // sliderReverbMicHpf
            // 
            this.sliderReverbMicHpf.BackColor = System.Drawing.Color.Transparent;
            this.sliderReverbMicHpf.backgroundDisabled = null;
            this.sliderReverbMicHpf.backgroundNormal = global::AEC.Properties.Resources.reverb_slider_back;
            this.sliderReverbMicHpf.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderReverbMicHpf.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderReverbMicHpf.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderReverbMicHpf.Location = new System.Drawing.Point(148, 62);
            this.sliderReverbMicHpf.Margin = new System.Windows.Forms.Padding(0);
            this.sliderReverbMicHpf.Name = "sliderReverbMicHpf";
            this.sliderReverbMicHpf.Size = new System.Drawing.Size(232, 23);
            this.sliderReverbMicHpf.SliderWidth = 142;
            this.sliderReverbMicHpf.TabIndex = 20;
            this.sliderReverbMicHpf.thumbCenterY = 11;
            this.sliderReverbMicHpf.ThumbEndX = 124;
            this.sliderReverbMicHpf.upperLimit = ((short)(100));
            this.sliderReverbMicHpf.value = ((short)(0));
            // 
            // sliderReverbMicLpf
            // 
            this.sliderReverbMicLpf.BackColor = System.Drawing.Color.Transparent;
            this.sliderReverbMicLpf.backgroundDisabled = null;
            this.sliderReverbMicLpf.backgroundNormal = global::AEC.Properties.Resources.reverb_slider_back;
            this.sliderReverbMicLpf.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderReverbMicLpf.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderReverbMicLpf.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderReverbMicLpf.Location = new System.Drawing.Point(148, 118);
            this.sliderReverbMicLpf.Margin = new System.Windows.Forms.Padding(0);
            this.sliderReverbMicLpf.Name = "sliderReverbMicLpf";
            this.sliderReverbMicLpf.Size = new System.Drawing.Size(232, 23);
            this.sliderReverbMicLpf.SliderWidth = 142;
            this.sliderReverbMicLpf.TabIndex = 20;
            this.sliderReverbMicLpf.thumbCenterY = 11;
            this.sliderReverbMicLpf.ThumbEndX = 124;
            this.sliderReverbMicLpf.upperLimit = ((short)(100));
            this.sliderReverbMicLpf.value = ((short)(0));
            // 
            // sliderReverbTrebleDamp
            // 
            this.sliderReverbTrebleDamp.BackColor = System.Drawing.Color.Transparent;
            this.sliderReverbTrebleDamp.backgroundDisabled = null;
            this.sliderReverbTrebleDamp.backgroundNormal = global::AEC.Properties.Resources.reverb_slider_back;
            this.sliderReverbTrebleDamp.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderReverbTrebleDamp.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderReverbTrebleDamp.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderReverbTrebleDamp.Location = new System.Drawing.Point(148, 258);
            this.sliderReverbTrebleDamp.Margin = new System.Windows.Forms.Padding(0);
            this.sliderReverbTrebleDamp.Name = "sliderReverbTrebleDamp";
            this.sliderReverbTrebleDamp.Size = new System.Drawing.Size(232, 23);
            this.sliderReverbTrebleDamp.SliderWidth = 142;
            this.sliderReverbTrebleDamp.TabIndex = 20;
            this.sliderReverbTrebleDamp.thumbCenterY = 11;
            this.sliderReverbTrebleDamp.ThumbEndX = 124;
            this.sliderReverbTrebleDamp.upperLimit = ((short)(100));
            this.sliderReverbTrebleDamp.value = ((short)(0));
            // 
            // sliderReverbPreDelay
            // 
            this.sliderReverbPreDelay.BackColor = System.Drawing.Color.Transparent;
            this.sliderReverbPreDelay.backgroundDisabled = null;
            this.sliderReverbPreDelay.backgroundNormal = global::AEC.Properties.Resources.reverb_slider_back;
            this.sliderReverbPreDelay.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderReverbPreDelay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderReverbPreDelay.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderReverbPreDelay.Location = new System.Drawing.Point(148, 174);
            this.sliderReverbPreDelay.Margin = new System.Windows.Forms.Padding(0);
            this.sliderReverbPreDelay.Name = "sliderReverbPreDelay";
            this.sliderReverbPreDelay.Size = new System.Drawing.Size(232, 23);
            this.sliderReverbPreDelay.SliderWidth = 142;
            this.sliderReverbPreDelay.TabIndex = 20;
            this.sliderReverbPreDelay.thumbCenterY = 11;
            this.sliderReverbPreDelay.ThumbEndX = 124;
            this.sliderReverbPreDelay.upperLimit = ((short)(100));
            this.sliderReverbPreDelay.value = ((short)(0));
            // 
            // sliderReverbBassDamp
            // 
            this.sliderReverbBassDamp.BackColor = System.Drawing.Color.Transparent;
            this.sliderReverbBassDamp.backgroundDisabled = null;
            this.sliderReverbBassDamp.backgroundNormal = global::AEC.Properties.Resources.reverb_slider_back;
            this.sliderReverbBassDamp.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderReverbBassDamp.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderReverbBassDamp.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderReverbBassDamp.Location = new System.Drawing.Point(148, 230);
            this.sliderReverbBassDamp.Margin = new System.Windows.Forms.Padding(0);
            this.sliderReverbBassDamp.Name = "sliderReverbBassDamp";
            this.sliderReverbBassDamp.Size = new System.Drawing.Size(232, 23);
            this.sliderReverbBassDamp.SliderWidth = 142;
            this.sliderReverbBassDamp.TabIndex = 20;
            this.sliderReverbBassDamp.thumbCenterY = 11;
            this.sliderReverbBassDamp.ThumbEndX = 124;
            this.sliderReverbBassDamp.upperLimit = ((short)(100));
            this.sliderReverbBassDamp.value = ((short)(0));
            // 
            // sliderReverbDelay
            // 
            this.sliderReverbDelay.BackColor = System.Drawing.Color.Transparent;
            this.sliderReverbDelay.backgroundDisabled = null;
            this.sliderReverbDelay.backgroundNormal = global::AEC.Properties.Resources.reverb_slider_back;
            this.sliderReverbDelay.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderReverbDelay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderReverbDelay.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderReverbDelay.Location = new System.Drawing.Point(148, 202);
            this.sliderReverbDelay.Margin = new System.Windows.Forms.Padding(0);
            this.sliderReverbDelay.Name = "sliderReverbDelay";
            this.sliderReverbDelay.Size = new System.Drawing.Size(232, 23);
            this.sliderReverbDelay.SliderWidth = 142;
            this.sliderReverbDelay.TabIndex = 20;
            this.sliderReverbDelay.thumbCenterY = 11;
            this.sliderReverbDelay.ThumbEndX = 124;
            this.sliderReverbDelay.upperLimit = ((short)(100));
            this.sliderReverbDelay.value = ((short)(0));
            // 
            // sliderEchoHpfType
            // 
            this.sliderEchoHpfType.BackColor = System.Drawing.Color.Transparent;
            this.sliderEchoHpfType.backgroundDisabled = null;
            this.sliderEchoHpfType.backgroundNormal = global::AEC.Properties.Resources.effect_slider_back;
            this.sliderEchoHpfType.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderEchoHpfType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderEchoHpfType.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderEchoHpfType.Location = new System.Drawing.Point(148, 84);
            this.sliderEchoHpfType.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEchoHpfType.Name = "sliderEchoHpfType";
            this.sliderEchoHpfType.Size = new System.Drawing.Size(232, 23);
            this.sliderEchoHpfType.SliderWidth = 142;
            this.sliderEchoHpfType.TabIndex = 25;
            this.sliderEchoHpfType.thumbCenterY = 11;
            this.sliderEchoHpfType.ThumbEndX = 124;
            this.sliderEchoHpfType.upperLimit = ((short)(100));
            this.sliderEchoHpfType.value = ((short)(0));
            // 
            // sliderEchoLpfType
            // 
            this.sliderEchoLpfType.BackColor = System.Drawing.Color.Transparent;
            this.sliderEchoLpfType.backgroundDisabled = null;
            this.sliderEchoLpfType.backgroundNormal = global::AEC.Properties.Resources.effect_slider_back;
            this.sliderEchoLpfType.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderEchoLpfType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderEchoLpfType.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderEchoLpfType.Location = new System.Drawing.Point(148, 136);
            this.sliderEchoLpfType.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEchoLpfType.Name = "sliderEchoLpfType";
            this.sliderEchoLpfType.Size = new System.Drawing.Size(232, 23);
            this.sliderEchoLpfType.SliderWidth = 142;
            this.sliderEchoLpfType.TabIndex = 23;
            this.sliderEchoLpfType.thumbCenterY = 11;
            this.sliderEchoLpfType.ThumbEndX = 124;
            this.sliderEchoLpfType.upperLimit = ((short)(100));
            this.sliderEchoLpfType.value = ((short)(0));
            // 
            // slideMicEcho
            // 
            this.slideMicEcho.BackColor = System.Drawing.Color.Transparent;
            this.slideMicEcho.backgroundDisabled = null;
            this.slideMicEcho.backgroundNormal = global::AEC.Properties.Resources.effect_slider_back;
            this.slideMicEcho.backLeftTop = new System.Drawing.Point(0, 4);
            this.slideMicEcho.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.slideMicEcho.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.slideMicEcho.Location = new System.Drawing.Point(148, 6);
            this.slideMicEcho.Name = "slideMicEcho";
            this.slideMicEcho.Size = new System.Drawing.Size(232, 23);
            this.slideMicEcho.SliderWidth = 142;
            this.slideMicEcho.TabIndex = 21;
            this.slideMicEcho.thumbCenterY = 11;
            this.slideMicEcho.ThumbEndX = 124;
            // 
            // sliderMicEchoDirect
            // 
            this.sliderMicEchoDirect.BackColor = System.Drawing.Color.Transparent;
            this.sliderMicEchoDirect.backgroundDisabled = null;
            this.sliderMicEchoDirect.backgroundNormal = global::AEC.Properties.Resources.effect_slider_back;
            this.sliderMicEchoDirect.backLeftTop = new System.Drawing.Point(0, 6);
            this.sliderMicEchoDirect.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderMicEchoDirect.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderMicEchoDirect.Location = new System.Drawing.Point(148, 32);
            this.sliderMicEchoDirect.Name = "sliderMicEchoDirect";
            this.sliderMicEchoDirect.Size = new System.Drawing.Size(232, 23);
            this.sliderMicEchoDirect.SliderWidth = 142;
            this.sliderMicEchoDirect.TabIndex = 21;
            this.sliderMicEchoDirect.thumbCenterY = 11;
            this.sliderMicEchoDirect.ThumbEndX = 124;
            // 
            // sliderEchoHpf
            // 
            this.sliderEchoHpf.BackColor = System.Drawing.Color.Transparent;
            this.sliderEchoHpf.backgroundDisabled = null;
            this.sliderEchoHpf.backgroundNormal = global::AEC.Properties.Resources.effect_slider_back;
            this.sliderEchoHpf.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderEchoHpf.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderEchoHpf.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderEchoHpf.Location = new System.Drawing.Point(148, 58);
            this.sliderEchoHpf.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEchoHpf.Name = "sliderEchoHpf";
            this.sliderEchoHpf.Size = new System.Drawing.Size(232, 23);
            this.sliderEchoHpf.SliderWidth = 142;
            this.sliderEchoHpf.TabIndex = 20;
            this.sliderEchoHpf.thumbCenterY = 11;
            this.sliderEchoHpf.ThumbEndX = 124;
            this.sliderEchoHpf.upperLimit = ((short)(100));
            this.sliderEchoHpf.value = ((short)(0));
            // 
            // sliderEchoRChDelay
            // 
            this.sliderEchoRChDelay.BackColor = System.Drawing.Color.Transparent;
            this.sliderEchoRChDelay.backgroundDisabled = null;
            this.sliderEchoRChDelay.backgroundNormal = global::AEC.Properties.Resources.effect_slider_back;
            this.sliderEchoRChDelay.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderEchoRChDelay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderEchoRChDelay.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderEchoRChDelay.Location = new System.Drawing.Point(148, 266);
            this.sliderEchoRChDelay.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEchoRChDelay.Name = "sliderEchoRChDelay";
            this.sliderEchoRChDelay.Size = new System.Drawing.Size(232, 23);
            this.sliderEchoRChDelay.SliderWidth = 142;
            this.sliderEchoRChDelay.TabIndex = 20;
            this.sliderEchoRChDelay.thumbCenterY = 11;
            this.sliderEchoRChDelay.ThumbEndX = 124;
            this.sliderEchoRChDelay.upperLimit = ((short)(100));
            this.sliderEchoRChDelay.value = ((short)(0));
            // 
            // sliderEchoPreDelay
            // 
            this.sliderEchoPreDelay.BackColor = System.Drawing.Color.Transparent;
            this.sliderEchoPreDelay.backgroundDisabled = null;
            this.sliderEchoPreDelay.backgroundNormal = global::AEC.Properties.Resources.effect_slider_back;
            this.sliderEchoPreDelay.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderEchoPreDelay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderEchoPreDelay.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderEchoPreDelay.Location = new System.Drawing.Point(148, 162);
            this.sliderEchoPreDelay.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEchoPreDelay.Name = "sliderEchoPreDelay";
            this.sliderEchoPreDelay.Size = new System.Drawing.Size(232, 23);
            this.sliderEchoPreDelay.SliderWidth = 142;
            this.sliderEchoPreDelay.TabIndex = 20;
            this.sliderEchoPreDelay.thumbCenterY = 11;
            this.sliderEchoPreDelay.ThumbEndX = 124;
            this.sliderEchoPreDelay.upperLimit = ((short)(100));
            this.sliderEchoPreDelay.value = ((short)(0));
            // 
            // sliderEchoRChPreDelay
            // 
            this.sliderEchoRChPreDelay.BackColor = System.Drawing.Color.Transparent;
            this.sliderEchoRChPreDelay.backgroundDisabled = null;
            this.sliderEchoRChPreDelay.backgroundNormal = global::AEC.Properties.Resources.effect_slider_back;
            this.sliderEchoRChPreDelay.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderEchoRChPreDelay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderEchoRChPreDelay.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderEchoRChPreDelay.Location = new System.Drawing.Point(148, 240);
            this.sliderEchoRChPreDelay.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEchoRChPreDelay.Name = "sliderEchoRChPreDelay";
            this.sliderEchoRChPreDelay.Size = new System.Drawing.Size(232, 23);
            this.sliderEchoRChPreDelay.SliderWidth = 142;
            this.sliderEchoRChPreDelay.TabIndex = 20;
            this.sliderEchoRChPreDelay.thumbCenterY = 11;
            this.sliderEchoRChPreDelay.ThumbEndX = 124;
            this.sliderEchoRChPreDelay.upperLimit = ((short)(100));
            this.sliderEchoRChPreDelay.value = ((short)(0));
            // 
            // sliderEchoLpf
            // 
            this.sliderEchoLpf.BackColor = System.Drawing.Color.Transparent;
            this.sliderEchoLpf.backgroundDisabled = null;
            this.sliderEchoLpf.backgroundNormal = global::AEC.Properties.Resources.effect_slider_back;
            this.sliderEchoLpf.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderEchoLpf.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderEchoLpf.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderEchoLpf.Location = new System.Drawing.Point(148, 110);
            this.sliderEchoLpf.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEchoLpf.Name = "sliderEchoLpf";
            this.sliderEchoLpf.Size = new System.Drawing.Size(232, 23);
            this.sliderEchoLpf.SliderWidth = 142;
            this.sliderEchoLpf.TabIndex = 20;
            this.sliderEchoLpf.thumbCenterY = 11;
            this.sliderEchoLpf.ThumbEndX = 124;
            this.sliderEchoLpf.upperLimit = ((short)(100));
            this.sliderEchoLpf.value = ((short)(0));
            // 
            // sliderEchoRepeat
            // 
            this.sliderEchoRepeat.BackColor = System.Drawing.Color.Transparent;
            this.sliderEchoRepeat.backgroundDisabled = null;
            this.sliderEchoRepeat.backgroundNormal = global::AEC.Properties.Resources.effect_slider_back;
            this.sliderEchoRepeat.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderEchoRepeat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderEchoRepeat.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderEchoRepeat.Location = new System.Drawing.Point(148, 214);
            this.sliderEchoRepeat.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEchoRepeat.Name = "sliderEchoRepeat";
            this.sliderEchoRepeat.Size = new System.Drawing.Size(232, 23);
            this.sliderEchoRepeat.SliderWidth = 142;
            this.sliderEchoRepeat.TabIndex = 20;
            this.sliderEchoRepeat.thumbCenterY = 11;
            this.sliderEchoRepeat.ThumbEndX = 124;
            this.sliderEchoRepeat.upperLimit = ((short)(100));
            this.sliderEchoRepeat.value = ((short)(0));
            // 
            // sliderEchoDelay
            // 
            this.sliderEchoDelay.BackColor = System.Drawing.Color.Transparent;
            this.sliderEchoDelay.backgroundDisabled = null;
            this.sliderEchoDelay.backgroundNormal = global::AEC.Properties.Resources.effect_slider_back;
            this.sliderEchoDelay.backLeftTop = new System.Drawing.Point(0, 5);
            this.sliderEchoDelay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.sliderEchoDelay.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.sliderEchoDelay.Location = new System.Drawing.Point(148, 188);
            this.sliderEchoDelay.Margin = new System.Windows.Forms.Padding(0);
            this.sliderEchoDelay.Name = "sliderEchoDelay";
            this.sliderEchoDelay.Size = new System.Drawing.Size(232, 23);
            this.sliderEchoDelay.SliderWidth = 142;
            this.sliderEchoDelay.TabIndex = 20;
            this.sliderEchoDelay.thumbCenterY = 11;
            this.sliderEchoDelay.ThumbEndX = 124;
            this.sliderEchoDelay.upperLimit = ((short)(100));
            this.sliderEchoDelay.value = ((short)(0));
            // 
            // KXEffectPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.gtPanel1);
            this.Controls.Add(this.panelEcho);
            this.Controls.Add(this.eqResetAndByPassPanel);
            this.Controls.Add(this.eqGroupPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "KXEffectPanel";
            this.Size = new System.Drawing.Size(1276, 312);
            this.gtPanel1.ResumeLayout(false);
            this.panelEcho.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private EQGroupPanel eqGroupPanel;
        private gtLabel lblMicEcho;
        private gtLabel lblMicEchoDirect;
        private gtLabel lblEchoHpf;
        private HSliderParam sliderEchoHpf;
        private gtLabel lblEchoLpf;
        private HSliderParam sliderEchoLpf;
        private gtLabel lblReverbPreDelay;
        private HSliderParam sliderReverbPreDelay;
        private gtLabel lblReverbDelay;
        private HSliderParam sliderReverbDelay;
        private gtLabel lblEchoRepeat;
        private HSliderParam sliderEchoRepeat;
        private gtLabel lblEchoRChPreDelay;
        private HSliderParam sliderEchoRChPreDelay;
        private gtLabel lblEchoRChDelay;
        private HSliderParam sliderEchoRChDelay;
        private gtLabel lblMicReverb;
        private gtLabel lblMicReverbDirect;
        private gtLabel lblEchoPreDelay;
        private HSliderParam sliderEchoPreDelay;
        private gtLabel lblEchoDelay;
        private HSliderParam sliderEchoDelay;
        private gtLabel lblReverbMicHpf;
        private HSliderParam sliderReverbMicHpf;
        private gtLabel lblReverbMicLpf;
        private HSliderParam sliderReverbMicLpf;
        private HSliderParam sliderReverbBassDamp;
        private HSliderParam sliderReverbTrebleDamp;
        private gtLabel lblReverbBassDamp;
        private gtLabel lblReverbTrebleDamp;
        private LevelPhase slideMicEcho;
        private LevelPhase sliderMicEchoDirect;
        private LevelPhase sliderMicReverb;
        private LevelPhase sliderMicReverbDirect;
        private EQResetAndByPassPanel eqResetAndByPassPanel;
        private gtPanel panelEcho;
        private gtPanel gtPanel1;
        private gtLabel lblReverbMicLpfSlope;
        private HSliderParam sliderReverbMicLpfType;
        private gtLabel lblEchoLpfSlope;
        private HSliderParam sliderEchoLpfType;
        private gtLabel lblEchoHpfSlope;
        private HSliderParam sliderEchoHpfType;
        private gtLabel lblReverbMicHpfSlope;
        private HSliderParam sliderReverbMicHpfType;
    }
}
