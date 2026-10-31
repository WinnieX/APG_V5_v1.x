namespace AEC.UI.RTA
{
    partial class FormRTAOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRTAOptions));
            this.panelRTAModePanel = new System.Windows.Forms.Panel();
            this.rdoTransferFunction = new AEC.UI.gtRadioButton();
            this.rdoSpectrum = new AEC.UI.gtRadioButton();
            this.rtaPause = new AEC.UI.RTASwitch();
            this.rtaSwitch = new AEC.UI.RTASwitch();
            this.panelRefTrace = new AEC.UI.gtPanel();
            this.refTraceSwitch = new AEC.UI.RTASwitch();
            this.btnDelete = new AEC.UI.gtButton();
            this.panelCapture = new System.Windows.Forms.Panel();
            this.btnCapture = new AEC.UI.gtButton();
            this.traceGroupE = new AEC.UI.RTA.TraceGroup();
            this.traceGroupD = new AEC.UI.RTA.TraceGroup();
            this.traceGroupC = new AEC.UI.RTA.TraceGroup();
            this.traceGroupB = new AEC.UI.RTA.TraceGroup();
            this.traceGroupA = new AEC.UI.RTA.TraceGroup();
            this.panelRTAParams = new AEC.UI.gtPanel();
            this.panelTFParam = new System.Windows.Forms.Panel();
            this.rdoPhaseResp = new AEC.UI.gtRadioButton();
            this.rdoAmpResp = new AEC.UI.gtRadioButton();
            this.paramLabelAmpOffset = new AEC.UI.ParamLabel();
            this.rdoAmpAndPhaseResp = new AEC.UI.gtRadioButton();
            this.lblFreqRespGraph = new AEC.UI.gtLabel();
            this.lblAmpOffset = new AEC.UI.gtLabel();
            this.panelSPParam = new System.Windows.Forms.Panel();
            this.rdoRightCh = new AEC.UI.gtRadioButton();
            this.rdoLeftCh = new AEC.UI.gtRadioButton();
            this.rdoDoubleCh = new AEC.UI.gtRadioButton();
            this.cmbScale = new AEC.UI.gtComboBox();
            this.lblSpectrumGraph = new AEC.UI.gtLabel();
            this.lblScale = new AEC.UI.gtLabel();
            this.cmbWindowFunction = new AEC.UI.gtComboBox();
            this.lblWindowFunction = new AEC.UI.gtLabel();
            this.cmbSmooth = new AEC.UI.gtComboBox();
            this.lblSmooth = new AEC.UI.gtLabel();
            this.cmbAverage = new AEC.UI.gtComboBox();
            this.lblAverage = new AEC.UI.gtLabel();
            this.cmbFFTSize = new AEC.UI.gtComboBox();
            this.lblFftSize = new AEC.UI.gtLabel();
            this.panelSignalGenerator = new AEC.UI.gtPanel();
            this.outputSwitch = new AEC.UI.RTASwitch();
            this.panelSine = new System.Windows.Forms.Panel();
            this.paramLabelSine2Freq = new AEC.UI.ParamLabel();
            this.lblSine2Freq = new AEC.UI.gtLabel();
            this.paramLabelSine1Freq = new AEC.UI.ParamLabel();
            this.paramLabelSine2Level = new AEC.UI.ParamLabel();
            this.lblSine1Freq = new AEC.UI.gtLabel();
            this.lblSine2Level = new AEC.UI.gtLabel();
            this.paramLabelSine1Level = new AEC.UI.ParamLabel();
            this.lblSine1Level = new AEC.UI.gtLabel();
            this.panelPink = new System.Windows.Forms.Panel();
            this.paramLabelPinkLevel = new AEC.UI.ParamLabel();
            this.lblLevel = new AEC.UI.gtLabel();
            this.cmbOutputSignal = new AEC.UI.gtComboBox();
            this.lblSignal = new AEC.UI.gtLabel();
            this.panelDelay = new AEC.UI.gtPanel();
            this.progressBar = new AEC.UI.RTA.ProgressBar();
            this.btnDeclineMeasResult = new AEC.UI.gtButton();
            this.btnApplyMeasResult = new AEC.UI.gtButton();
            this.btnMeasDelay = new AEC.UI.gtButton();
            this.lblMeasDelayResult = new AEC.UI.gtLabel();
            this.lblDelay = new AEC.UI.gtLabel();
            this.paramLabelMeasDelayResult = new AEC.UI.ParamLabel();
            this.paramLabelDelay = new AEC.UI.ParamLabel();
            this.panelSignalLevel = new AEC.UI.gtPanel();
            this.levelLeft = new AEC.UI.RTA.AudioInputLevel();
            this.levelRight = new AEC.UI.RTA.AudioInputLevel();
            this.lblRightCh = new AEC.UI.gtLabel();
            this.lblLeftCh = new AEC.UI.gtLabel();
            this.panelAudioIO = new AEC.UI.gtPanel();
            this.btnRefreshOutputDevice = new AEC.UI.gtButton();
            this.rdoBitsPerSample24 = new AEC.UI.gtRadioButton();
            this.rdoBitsPerSample16 = new AEC.UI.gtRadioButton();
            this.btnRefreshInputDevice = new AEC.UI.gtButton();
            this.splitLine1 = new AEC.UI.SplitLine();
            this.cmbAudioOutDevice = new AEC.UI.gtComboBox();
            this.cmbSampleRate = new AEC.UI.gtComboBox();
            this.cmbAudioInDevice = new AEC.UI.gtComboBox();
            this.lblSampleBitDepth = new AEC.UI.gtLabel();
            this.lblSampleRate = new AEC.UI.gtLabel();
            this.lblAudioOutDevice = new AEC.UI.gtLabel();
            this.lblAudioInDevice = new AEC.UI.gtLabel();
            this.title = new AEC.UI.Title();
            this.panelRTAModePanel.SuspendLayout();
            this.panelRefTrace.SuspendLayout();
            this.panelCapture.SuspendLayout();
            this.panelRTAParams.SuspendLayout();
            this.panelTFParam.SuspendLayout();
            this.panelSPParam.SuspendLayout();
            this.panelSignalGenerator.SuspendLayout();
            this.panelSine.SuspendLayout();
            this.panelPink.SuspendLayout();
            this.panelDelay.SuspendLayout();
            this.panelSignalLevel.SuspendLayout();
            this.panelAudioIO.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRTAModePanel
            // 
            this.panelRTAModePanel.BackColor = System.Drawing.Color.Transparent;
            this.panelRTAModePanel.Controls.Add(this.rdoTransferFunction);
            this.panelRTAModePanel.Controls.Add(this.rdoSpectrum);
            this.panelRTAModePanel.Location = new System.Drawing.Point(797, 42);
            this.panelRTAModePanel.Name = "panelRTAModePanel";
            this.panelRTAModePanel.Size = new System.Drawing.Size(301, 33);
            this.panelRTAModePanel.TabIndex = 7;
            // 
            // rdoTransferFunction
            // 
            this.rdoTransferFunction.backChecked = global::AEC.Properties.Resources.rta_tf_checked;
            this.rdoTransferFunction.BackColor = System.Drawing.Color.Transparent;
            this.rdoTransferFunction.backDisabled = global::AEC.Properties.Resources.rta_tf_disabled;
            this.rdoTransferFunction.backHover = global::AEC.Properties.Resources.rta_tf_hover;
            this.rdoTransferFunction.backNormal = global::AEC.Properties.Resources.rta_tf_normal;
            this.rdoTransferFunction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.rdoTransferFunction.isAutoCheck = true;
            this.rdoTransferFunction.isChecked = false;
            this.rdoTransferFunction.isCheckInit = false;
            this.rdoTransferFunction.isShowCheckedBackWhenDsiabled = false;
            this.rdoTransferFunction.isStretch = true;
            this.rdoTransferFunction.Location = new System.Drawing.Point(145, 1);
            this.rdoTransferFunction.Margin = new System.Windows.Forms.Padding(0);
            this.rdoTransferFunction.Name = "rdoTransferFunction";
            this.rdoTransferFunction.Size = new System.Drawing.Size(137, 31);
            this.rdoTransferFunction.TabIndex = 6;
            this.rdoTransferFunction.Text = "传递函数";
            this.rdoTransferFunction.userGDIPaint = false;
            // 
            // rdoSpectrum
            // 
            this.rdoSpectrum.backChecked = global::AEC.Properties.Resources.rta_sp_checked;
            this.rdoSpectrum.BackColor = System.Drawing.Color.Transparent;
            this.rdoSpectrum.backDisabled = global::AEC.Properties.Resources.rta_sp_disabled;
            this.rdoSpectrum.backHover = global::AEC.Properties.Resources.rta_sp_hover;
            this.rdoSpectrum.backNormal = global::AEC.Properties.Resources.rta_sp_normal;
            this.rdoSpectrum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.rdoSpectrum.isAutoCheck = true;
            this.rdoSpectrum.isChecked = false;
            this.rdoSpectrum.isCheckInit = false;
            this.rdoSpectrum.isShowCheckedBackWhenDsiabled = false;
            this.rdoSpectrum.isStretch = true;
            this.rdoSpectrum.Location = new System.Drawing.Point(8, 1);
            this.rdoSpectrum.Margin = new System.Windows.Forms.Padding(0);
            this.rdoSpectrum.Name = "rdoSpectrum";
            this.rdoSpectrum.Size = new System.Drawing.Size(137, 31);
            this.rdoSpectrum.TabIndex = 6;
            this.rdoSpectrum.Text = "频谱";
            this.rdoSpectrum.userGDIPaint = false;
            // 
            // rtaPause
            // 
            this.rtaPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rtaPause.BackColor = System.Drawing.Color.Transparent;
            this.rtaPause.backDisabled = global::AEC.Properties.Resources.rta_pause_disabled;
            this.rtaPause.backHover = global::AEC.Properties.Resources.rta_pause_hover;
            this.rtaPause.backNormal = global::AEC.Properties.Resources.rta_pause_normal;
            this.rtaPause.backPressed = global::AEC.Properties.Resources.rta_pause_pressed;
            this.rtaPause.Enabled = false;
            this.rtaPause.ForeColor = System.Drawing.Color.White;
            this.rtaPause.isOn = false;
            this.rtaPause.isStretch = false;
            this.rtaPause.Location = new System.Drawing.Point(700, 40);
            this.rtaPause.Name = "rtaPause";
            this.rtaPause.offHover = global::AEC.Properties.Resources.rta_pause_hover;
            this.rtaPause.offNormal = global::AEC.Properties.Resources.rta_pause_normal;
            this.rtaPause.offPressed = global::AEC.Properties.Resources.rta_pause_pressed;
            this.rtaPause.onHover = global::AEC.Properties.Resources.rta_continue_hover;
            this.rtaPause.onNormal = global::AEC.Properties.Resources.rta_continue_normal;
            this.rtaPause.onPressed = global::AEC.Properties.Resources.rta_continue_pressed;
            this.rtaPause.Size = new System.Drawing.Size(41, 41);
            this.rtaPause.TabIndex = 37;
            this.rtaPause.userGDIPaint = false;
            this.rtaPause.Click += new System.EventHandler(this.rtaPause_Click);
            // 
            // rtaSwitch
            // 
            this.rtaSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rtaSwitch.BackColor = System.Drawing.Color.Transparent;
            this.rtaSwitch.backDisabled = global::AEC.Properties.Resources.rta_switch2_disabled;
            this.rtaSwitch.backHover = global::AEC.Properties.Resources.rta_switch2_off_hover;
            this.rtaSwitch.backNormal = global::AEC.Properties.Resources.rta_switch2_off_normal;
            this.rtaSwitch.backPressed = global::AEC.Properties.Resources.rta_switch2_off_pressed;
            this.rtaSwitch.ForeColor = System.Drawing.Color.White;
            this.rtaSwitch.isOn = false;
            this.rtaSwitch.isStretch = false;
            this.rtaSwitch.Location = new System.Drawing.Point(637, 40);
            this.rtaSwitch.Name = "rtaSwitch";
            this.rtaSwitch.offHover = global::AEC.Properties.Resources.rta_switch2_off_hover;
            this.rtaSwitch.offNormal = global::AEC.Properties.Resources.rta_switch2_off_normal;
            this.rtaSwitch.offPressed = global::AEC.Properties.Resources.rta_switch2_off_pressed;
            this.rtaSwitch.onHover = global::AEC.Properties.Resources.rta_switch2_on_hover;
            this.rtaSwitch.onNormal = global::AEC.Properties.Resources.rta_switch2_on_normal;
            this.rtaSwitch.onPressed = global::AEC.Properties.Resources.rta_switch2_on_pressed;
            this.rtaSwitch.Size = new System.Drawing.Size(41, 41);
            this.rtaSwitch.TabIndex = 37;
            this.rtaSwitch.userGDIPaint = false;
            this.rtaSwitch.Click += new System.EventHandler(this.rtaSwitch_Click);
            // 
            // panelRefTrace
            // 
            this.panelRefTrace.Controls.Add(this.refTraceSwitch);
            this.panelRefTrace.Controls.Add(this.btnDelete);
            this.panelRefTrace.Controls.Add(this.panelCapture);
            this.panelRefTrace.Controls.Add(this.traceGroupE);
            this.panelRefTrace.Controls.Add(this.traceGroupD);
            this.panelRefTrace.Controls.Add(this.traceGroupC);
            this.panelRefTrace.Controls.Add(this.traceGroupB);
            this.panelRefTrace.Controls.Add(this.traceGroupA);
            this.panelRefTrace.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(240)))));
            this.panelRefTrace.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.panelRefTrace.Location = new System.Drawing.Point(12, 480);
            this.panelRefTrace.Name = "panelRefTrace";
            this.panelRefTrace.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelRefTrace.Radius = 18;
            this.panelRefTrace.RectColor = System.Drawing.Color.Black;
            this.panelRefTrace.ShowRect = true;
            this.panelRefTrace.Size = new System.Drawing.Size(1086, 84);
            this.panelRefTrace.TabIndex = 8;
            this.panelRefTrace.title = "参考曲线";
            this.panelRefTrace.titleHeight = 20;
            // 
            // refTraceSwitch
            // 
            this.refTraceSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refTraceSwitch.BackColor = System.Drawing.Color.Transparent;
            this.refTraceSwitch.backDisabled = global::AEC.Properties.Resources.rta_on_disabled;
            this.refTraceSwitch.backHover = global::AEC.Properties.Resources.rta_on_closed_hover;
            this.refTraceSwitch.backNormal = global::AEC.Properties.Resources.rta_on_closed_normal;
            this.refTraceSwitch.backPressed = global::AEC.Properties.Resources.rta_on_closed_pressed;
            this.refTraceSwitch.ForeColor = System.Drawing.Color.White;
            this.refTraceSwitch.isOn = false;
            this.refTraceSwitch.isStretch = false;
            this.refTraceSwitch.Location = new System.Drawing.Point(9, 31);
            this.refTraceSwitch.Name = "refTraceSwitch";
            this.refTraceSwitch.offHover = global::AEC.Properties.Resources.rta_on_closed_hover;
            this.refTraceSwitch.offNormal = global::AEC.Properties.Resources.rta_on_closed_normal;
            this.refTraceSwitch.offPressed = global::AEC.Properties.Resources.rta_on_closed_pressed;
            this.refTraceSwitch.onHover = global::AEC.Properties.Resources.rta_on_opened_hover;
            this.refTraceSwitch.onNormal = global::AEC.Properties.Resources.rta_on_opened_normal;
            this.refTraceSwitch.onPressed = global::AEC.Properties.Resources.rta_on_opened_pressed;
            this.refTraceSwitch.Size = new System.Drawing.Size(78, 31);
            this.refTraceSwitch.TabIndex = 36;
            this.refTraceSwitch.userGDIPaint = false;
            this.refTraceSwitch.Click += new System.EventHandler(this.refTraceSwitch_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.btnDelete.backHover = global::AEC.Properties.Resources.btn_hover;
            this.btnDelete.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.btnDelete.backPressed = global::AEC.Properties.Resources.btn_pressed;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.btnDelete.isStretch = true;
            this.btnDelete.Location = new System.Drawing.Point(998, 31);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 30);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "删除";
            this.btnDelete.userGDIPaint = true;
            this.btnDelete.Click += new System.EventHandler(this.btn_Click);
            // 
            // panelCapture
            // 
            this.panelCapture.Controls.Add(this.btnCapture);
            this.panelCapture.Enabled = false;
            this.panelCapture.Location = new System.Drawing.Point(905, 28);
            this.panelCapture.Margin = new System.Windows.Forms.Padding(0);
            this.panelCapture.Name = "panelCapture";
            this.panelCapture.Size = new System.Drawing.Size(86, 36);
            this.panelCapture.TabIndex = 39;
            // 
            // btnCapture
            // 
            this.btnCapture.BackColor = System.Drawing.Color.Transparent;
            this.btnCapture.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.btnCapture.backHover = global::AEC.Properties.Resources.btn_hover;
            this.btnCapture.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.btnCapture.backPressed = global::AEC.Properties.Resources.btn_pressed;
            this.btnCapture.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnCapture.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.btnCapture.isStretch = true;
            this.btnCapture.Location = new System.Drawing.Point(3, 3);
            this.btnCapture.Margin = new System.Windows.Forms.Padding(0);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(80, 30);
            this.btnCapture.TabIndex = 1;
            this.btnCapture.Text = "捕捉";
            this.btnCapture.userGDIPaint = true;
            this.btnCapture.Click += new System.EventHandler(this.btn_Click);
            // 
            // traceGroupE
            // 
            this.traceGroupE.BackColor = System.Drawing.Color.Transparent;
            this.traceGroupE.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.traceGroupE.group = AEC.RTA.Core.RefTraceGroupNum.GroupE;
            this.traceGroupE.isActive = false;
            this.traceGroupE.isShowGroup = false;
            this.traceGroupE.Location = new System.Drawing.Point(745, 30);
            this.traceGroupE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.traceGroupE.Name = "traceGroupE";
            this.traceGroupE.Size = new System.Drawing.Size(153, 36);
            this.traceGroupE.TabIndex = 38;
            // 
            // traceGroupD
            // 
            this.traceGroupD.BackColor = System.Drawing.Color.Transparent;
            this.traceGroupD.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.traceGroupD.group = AEC.RTA.Core.RefTraceGroupNum.GroupD;
            this.traceGroupD.isActive = false;
            this.traceGroupD.isShowGroup = false;
            this.traceGroupD.Location = new System.Drawing.Point(582, 30);
            this.traceGroupD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.traceGroupD.Name = "traceGroupD";
            this.traceGroupD.Size = new System.Drawing.Size(153, 36);
            this.traceGroupD.TabIndex = 38;
            // 
            // traceGroupC
            // 
            this.traceGroupC.BackColor = System.Drawing.Color.Transparent;
            this.traceGroupC.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.traceGroupC.group = AEC.RTA.Core.RefTraceGroupNum.GroupC;
            this.traceGroupC.isActive = false;
            this.traceGroupC.isShowGroup = false;
            this.traceGroupC.Location = new System.Drawing.Point(419, 30);
            this.traceGroupC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.traceGroupC.Name = "traceGroupC";
            this.traceGroupC.Size = new System.Drawing.Size(153, 36);
            this.traceGroupC.TabIndex = 38;
            // 
            // traceGroupB
            // 
            this.traceGroupB.BackColor = System.Drawing.Color.Transparent;
            this.traceGroupB.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.traceGroupB.group = AEC.RTA.Core.RefTraceGroupNum.GroupB;
            this.traceGroupB.isActive = false;
            this.traceGroupB.isShowGroup = false;
            this.traceGroupB.Location = new System.Drawing.Point(256, 30);
            this.traceGroupB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.traceGroupB.Name = "traceGroupB";
            this.traceGroupB.Size = new System.Drawing.Size(153, 36);
            this.traceGroupB.TabIndex = 38;
            // 
            // traceGroupA
            // 
            this.traceGroupA.BackColor = System.Drawing.Color.Transparent;
            this.traceGroupA.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.traceGroupA.group = AEC.RTA.Core.RefTraceGroupNum.GroupA;
            this.traceGroupA.isActive = false;
            this.traceGroupA.isShowGroup = false;
            this.traceGroupA.Location = new System.Drawing.Point(93, 30);
            this.traceGroupA.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.traceGroupA.Name = "traceGroupA";
            this.traceGroupA.Size = new System.Drawing.Size(153, 36);
            this.traceGroupA.TabIndex = 38;
            // 
            // panelRTAParams
            // 
            this.panelRTAParams.Controls.Add(this.panelTFParam);
            this.panelRTAParams.Controls.Add(this.panelSPParam);
            this.panelRTAParams.Controls.Add(this.cmbWindowFunction);
            this.panelRTAParams.Controls.Add(this.lblWindowFunction);
            this.panelRTAParams.Controls.Add(this.cmbSmooth);
            this.panelRTAParams.Controls.Add(this.lblSmooth);
            this.panelRTAParams.Controls.Add(this.cmbAverage);
            this.panelRTAParams.Controls.Add(this.lblAverage);
            this.panelRTAParams.Controls.Add(this.cmbFFTSize);
            this.panelRTAParams.Controls.Add(this.lblFftSize);
            this.panelRTAParams.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(240)))));
            this.panelRTAParams.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.panelRTAParams.Location = new System.Drawing.Point(12, 276);
            this.panelRTAParams.Name = "panelRTAParams";
            this.panelRTAParams.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelRTAParams.Radius = 18;
            this.panelRTAParams.RectColor = System.Drawing.Color.Black;
            this.panelRTAParams.ShowRect = true;
            this.panelRTAParams.Size = new System.Drawing.Size(555, 195);
            this.panelRTAParams.TabIndex = 6;
            this.panelRTAParams.title = "实时分析参数";
            this.panelRTAParams.titleHeight = 20;
            // 
            // panelTFParam
            // 
            this.panelTFParam.Controls.Add(this.rdoPhaseResp);
            this.panelTFParam.Controls.Add(this.rdoAmpResp);
            this.panelTFParam.Controls.Add(this.paramLabelAmpOffset);
            this.panelTFParam.Controls.Add(this.rdoAmpAndPhaseResp);
            this.panelTFParam.Controls.Add(this.lblFreqRespGraph);
            this.panelTFParam.Controls.Add(this.lblAmpOffset);
            this.panelTFParam.Location = new System.Drawing.Point(6, 120);
            this.panelTFParam.Name = "panelTFParam";
            this.panelTFParam.Size = new System.Drawing.Size(546, 64);
            this.panelTFParam.TabIndex = 5;
            // 
            // rdoPhaseResp
            // 
            this.rdoPhaseResp.backChecked = global::AEC.Properties.Resources.rta_bitdepth_checked;
            this.rdoPhaseResp.BackColor = System.Drawing.Color.Transparent;
            this.rdoPhaseResp.backDisabled = global::AEC.Properties.Resources.rta_bitdepth_disabled;
            this.rdoPhaseResp.backHover = global::AEC.Properties.Resources.rta_bitdepth_hover;
            this.rdoPhaseResp.backNormal = global::AEC.Properties.Resources.rta_bitdepth_normal;
            this.rdoPhaseResp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.rdoPhaseResp.isAutoCheck = true;
            this.rdoPhaseResp.isChecked = false;
            this.rdoPhaseResp.isCheckInit = false;
            this.rdoPhaseResp.isShowCheckedBackWhenDsiabled = false;
            this.rdoPhaseResp.isStretch = true;
            this.rdoPhaseResp.Location = new System.Drawing.Point(436, 31);
            this.rdoPhaseResp.Margin = new System.Windows.Forms.Padding(0);
            this.rdoPhaseResp.Name = "rdoPhaseResp";
            this.rdoPhaseResp.Size = new System.Drawing.Size(96, 31);
            this.rdoPhaseResp.TabIndex = 6;
            this.rdoPhaseResp.Text = "相频响应";
            this.rdoPhaseResp.userGDIPaint = false;
            // 
            // rdoAmpResp
            // 
            this.rdoAmpResp.backChecked = global::AEC.Properties.Resources.rta_bitdepth_checked;
            this.rdoAmpResp.BackColor = System.Drawing.Color.Transparent;
            this.rdoAmpResp.backDisabled = global::AEC.Properties.Resources.rta_bitdepth_disabled;
            this.rdoAmpResp.backHover = global::AEC.Properties.Resources.rta_bitdepth_hover;
            this.rdoAmpResp.backNormal = global::AEC.Properties.Resources.rta_bitdepth_normal;
            this.rdoAmpResp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.rdoAmpResp.isAutoCheck = true;
            this.rdoAmpResp.isChecked = false;
            this.rdoAmpResp.isCheckInit = false;
            this.rdoAmpResp.isShowCheckedBackWhenDsiabled = false;
            this.rdoAmpResp.isStretch = true;
            this.rdoAmpResp.Location = new System.Drawing.Point(313, 31);
            this.rdoAmpResp.Margin = new System.Windows.Forms.Padding(0);
            this.rdoAmpResp.Name = "rdoAmpResp";
            this.rdoAmpResp.Size = new System.Drawing.Size(96, 31);
            this.rdoAmpResp.TabIndex = 6;
            this.rdoAmpResp.Text = "幅频响应";
            this.rdoAmpResp.userGDIPaint = false;
            // 
            // paramLabelAmpOffset
            // 
            this.paramLabelAmpOffset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.paramLabelAmpOffset.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramLabelAmpOffset.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramLabelAmpOffset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.paramLabelAmpOffset.initV = ((short)(0));
            this.paramLabelAmpOffset.isShowUpdownButton = true;
            this.paramLabelAmpOffset.Location = new System.Drawing.Point(140, 0);
            this.paramLabelAmpOffset.Margin = new System.Windows.Forms.Padding(0);
            this.paramLabelAmpOffset.Name = "paramLabelAmpOffset";
            this.paramLabelAmpOffset.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramLabelAmpOffset.Padding = new System.Windows.Forms.Padding(2);
            this.paramLabelAmpOffset.Size = new System.Drawing.Size(100, 25);
            this.paramLabelAmpOffset.TabIndex = 5;
            this.paramLabelAmpOffset.upperLimit = ((short)(0));
            this.paramLabelAmpOffset.userGDIPaint = false;
            this.paramLabelAmpOffset.v = ((short)(0));
            // 
            // rdoAmpAndPhaseResp
            // 
            this.rdoAmpAndPhaseResp.backChecked = global::AEC.Properties.Resources.rta_graph_checked;
            this.rdoAmpAndPhaseResp.BackColor = System.Drawing.Color.Transparent;
            this.rdoAmpAndPhaseResp.backDisabled = global::AEC.Properties.Resources.rta_graph_disabled;
            this.rdoAmpAndPhaseResp.backHover = global::AEC.Properties.Resources.rta_graph_hover;
            this.rdoAmpAndPhaseResp.backNormal = global::AEC.Properties.Resources.rta_graph_normal;
            this.rdoAmpAndPhaseResp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.rdoAmpAndPhaseResp.isAutoCheck = true;
            this.rdoAmpAndPhaseResp.isChecked = false;
            this.rdoAmpAndPhaseResp.isCheckInit = false;
            this.rdoAmpAndPhaseResp.isShowCheckedBackWhenDsiabled = false;
            this.rdoAmpAndPhaseResp.isStretch = false;
            this.rdoAmpAndPhaseResp.Location = new System.Drawing.Point(138, 31);
            this.rdoAmpAndPhaseResp.Margin = new System.Windows.Forms.Padding(0);
            this.rdoAmpAndPhaseResp.Name = "rdoAmpAndPhaseResp";
            this.rdoAmpAndPhaseResp.Size = new System.Drawing.Size(148, 32);
            this.rdoAmpAndPhaseResp.TabIndex = 6;
            this.rdoAmpAndPhaseResp.Text = "幅频和相频响应";
            this.rdoAmpAndPhaseResp.userGDIPaint = false;
            // 
            // lblFreqRespGraph
            // 
            this.lblFreqRespGraph.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblFreqRespGraph.BackColor = System.Drawing.Color.Transparent;
            this.lblFreqRespGraph.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblFreqRespGraph.Location = new System.Drawing.Point(12, 37);
            this.lblFreqRespGraph.Name = "lblFreqRespGraph";
            this.lblFreqRespGraph.Size = new System.Drawing.Size(120, 23);
            this.lblFreqRespGraph.TabIndex = 0;
            this.lblFreqRespGraph.Text = "频率响应曲线";
            // 
            // lblAmpOffset
            // 
            this.lblAmpOffset.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblAmpOffset.BackColor = System.Drawing.Color.Transparent;
            this.lblAmpOffset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblAmpOffset.Location = new System.Drawing.Point(12, 1);
            this.lblAmpOffset.Name = "lblAmpOffset";
            this.lblAmpOffset.Size = new System.Drawing.Size(120, 23);
            this.lblAmpOffset.TabIndex = 0;
            this.lblAmpOffset.Text = "幅度偏移";
            // 
            // panelSPParam
            // 
            this.panelSPParam.Controls.Add(this.rdoRightCh);
            this.panelSPParam.Controls.Add(this.rdoLeftCh);
            this.panelSPParam.Controls.Add(this.rdoDoubleCh);
            this.panelSPParam.Controls.Add(this.cmbScale);
            this.panelSPParam.Controls.Add(this.lblSpectrumGraph);
            this.panelSPParam.Controls.Add(this.lblScale);
            this.panelSPParam.Location = new System.Drawing.Point(6, 120);
            this.panelSPParam.Name = "panelSPParam";
            this.panelSPParam.Size = new System.Drawing.Size(546, 64);
            this.panelSPParam.TabIndex = 2;
            // 
            // rdoRightCh
            // 
            this.rdoRightCh.backChecked = global::AEC.Properties.Resources.rta_bitdepth_checked;
            this.rdoRightCh.BackColor = System.Drawing.Color.Transparent;
            this.rdoRightCh.backDisabled = global::AEC.Properties.Resources.rta_bitdepth_disabled;
            this.rdoRightCh.backHover = global::AEC.Properties.Resources.rta_bitdepth_hover;
            this.rdoRightCh.backNormal = global::AEC.Properties.Resources.rta_bitdepth_normal;
            this.rdoRightCh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.rdoRightCh.isAutoCheck = true;
            this.rdoRightCh.isChecked = false;
            this.rdoRightCh.isCheckInit = false;
            this.rdoRightCh.isShowCheckedBackWhenDsiabled = false;
            this.rdoRightCh.isStretch = true;
            this.rdoRightCh.Location = new System.Drawing.Point(436, 31);
            this.rdoRightCh.Margin = new System.Windows.Forms.Padding(0);
            this.rdoRightCh.Name = "rdoRightCh";
            this.rdoRightCh.Size = new System.Drawing.Size(96, 31);
            this.rdoRightCh.TabIndex = 6;
            this.rdoRightCh.Text = "右声道";
            this.rdoRightCh.userGDIPaint = false;
            // 
            // rdoLeftCh
            // 
            this.rdoLeftCh.backChecked = global::AEC.Properties.Resources.rta_bitdepth_checked;
            this.rdoLeftCh.BackColor = System.Drawing.Color.Transparent;
            this.rdoLeftCh.backDisabled = global::AEC.Properties.Resources.rta_bitdepth_disabled;
            this.rdoLeftCh.backHover = global::AEC.Properties.Resources.rta_bitdepth_hover;
            this.rdoLeftCh.backNormal = global::AEC.Properties.Resources.rta_bitdepth_normal;
            this.rdoLeftCh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.rdoLeftCh.isAutoCheck = true;
            this.rdoLeftCh.isChecked = false;
            this.rdoLeftCh.isCheckInit = false;
            this.rdoLeftCh.isShowCheckedBackWhenDsiabled = false;
            this.rdoLeftCh.isStretch = true;
            this.rdoLeftCh.Location = new System.Drawing.Point(313, 31);
            this.rdoLeftCh.Margin = new System.Windows.Forms.Padding(0);
            this.rdoLeftCh.Name = "rdoLeftCh";
            this.rdoLeftCh.Size = new System.Drawing.Size(96, 31);
            this.rdoLeftCh.TabIndex = 6;
            this.rdoLeftCh.Text = "左声道";
            this.rdoLeftCh.userGDIPaint = false;
            // 
            // rdoDoubleCh
            // 
            this.rdoDoubleCh.backChecked = global::AEC.Properties.Resources.rta_graph_checked;
            this.rdoDoubleCh.BackColor = System.Drawing.Color.Transparent;
            this.rdoDoubleCh.backDisabled = global::AEC.Properties.Resources.rta_graph_disabled;
            this.rdoDoubleCh.backHover = global::AEC.Properties.Resources.rta_graph_hover;
            this.rdoDoubleCh.backNormal = global::AEC.Properties.Resources.rta_graph_normal;
            this.rdoDoubleCh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.rdoDoubleCh.isAutoCheck = true;
            this.rdoDoubleCh.isChecked = false;
            this.rdoDoubleCh.isCheckInit = false;
            this.rdoDoubleCh.isShowCheckedBackWhenDsiabled = false;
            this.rdoDoubleCh.isStretch = true;
            this.rdoDoubleCh.Location = new System.Drawing.Point(138, 31);
            this.rdoDoubleCh.Margin = new System.Windows.Forms.Padding(0);
            this.rdoDoubleCh.Name = "rdoDoubleCh";
            this.rdoDoubleCh.Size = new System.Drawing.Size(148, 32);
            this.rdoDoubleCh.TabIndex = 6;
            this.rdoDoubleCh.Text = "左右声道";
            this.rdoDoubleCh.userGDIPaint = false;
            // 
            // cmbScale
            // 
            this.cmbScale.ArrowColor = System.Drawing.Color.Gray;
            this.cmbScale.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScale.FormattingEnabled = true;
            this.cmbScale.Location = new System.Drawing.Point(140, 1);
            this.cmbScale.Name = "cmbScale";
            this.cmbScale.Size = new System.Drawing.Size(100, 23);
            this.cmbScale.TabIndex = 1;
            // 
            // lblSpectrumGraph
            // 
            this.lblSpectrumGraph.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblSpectrumGraph.BackColor = System.Drawing.Color.Transparent;
            this.lblSpectrumGraph.Location = new System.Drawing.Point(12, 37);
            this.lblSpectrumGraph.Name = "lblSpectrumGraph";
            this.lblSpectrumGraph.Size = new System.Drawing.Size(120, 23);
            this.lblSpectrumGraph.TabIndex = 0;
            this.lblSpectrumGraph.Text = "频谱图";
            // 
            // lblScale
            // 
            this.lblScale.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblScale.BackColor = System.Drawing.Color.Transparent;
            this.lblScale.Location = new System.Drawing.Point(12, 1);
            this.lblScale.Name = "lblScale";
            this.lblScale.Size = new System.Drawing.Size(120, 23);
            this.lblScale.TabIndex = 0;
            this.lblScale.Text = "刻度";
            // 
            // cmbWindowFunction
            // 
            this.cmbWindowFunction.ArrowColor = System.Drawing.Color.Gray;
            this.cmbWindowFunction.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbWindowFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWindowFunction.FormattingEnabled = true;
            this.cmbWindowFunction.Location = new System.Drawing.Point(146, 62);
            this.cmbWindowFunction.Name = "cmbWindowFunction";
            this.cmbWindowFunction.Size = new System.Drawing.Size(219, 23);
            this.cmbWindowFunction.TabIndex = 1;
            // 
            // lblWindowFunction
            // 
            this.lblWindowFunction.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblWindowFunction.BackColor = System.Drawing.Color.Transparent;
            this.lblWindowFunction.Location = new System.Drawing.Point(18, 62);
            this.lblWindowFunction.Name = "lblWindowFunction";
            this.lblWindowFunction.Size = new System.Drawing.Size(120, 23);
            this.lblWindowFunction.TabIndex = 0;
            this.lblWindowFunction.Text = "窗函数";
            // 
            // cmbSmooth
            // 
            this.cmbSmooth.ArrowColor = System.Drawing.Color.Gray;
            this.cmbSmooth.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbSmooth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSmooth.FormattingEnabled = true;
            this.cmbSmooth.Location = new System.Drawing.Point(369, 91);
            this.cmbSmooth.Name = "cmbSmooth";
            this.cmbSmooth.Size = new System.Drawing.Size(100, 23);
            this.cmbSmooth.TabIndex = 1;
            // 
            // lblSmooth
            // 
            this.lblSmooth.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblSmooth.BackColor = System.Drawing.Color.Transparent;
            this.lblSmooth.Location = new System.Drawing.Point(268, 91);
            this.lblSmooth.Name = "lblSmooth";
            this.lblSmooth.Size = new System.Drawing.Size(86, 23);
            this.lblSmooth.TabIndex = 0;
            this.lblSmooth.Text = "平滑处理";
            // 
            // cmbAverage
            // 
            this.cmbAverage.ArrowColor = System.Drawing.Color.Gray;
            this.cmbAverage.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbAverage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAverage.FormattingEnabled = true;
            this.cmbAverage.Location = new System.Drawing.Point(146, 91);
            this.cmbAverage.Name = "cmbAverage";
            this.cmbAverage.Size = new System.Drawing.Size(100, 23);
            this.cmbAverage.TabIndex = 1;
            // 
            // lblAverage
            // 
            this.lblAverage.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblAverage.BackColor = System.Drawing.Color.Transparent;
            this.lblAverage.Location = new System.Drawing.Point(18, 91);
            this.lblAverage.Name = "lblAverage";
            this.lblAverage.Size = new System.Drawing.Size(120, 23);
            this.lblAverage.TabIndex = 0;
            this.lblAverage.Text = "均值处理";
            // 
            // cmbFFTSize
            // 
            this.cmbFFTSize.ArrowColor = System.Drawing.Color.Gray;
            this.cmbFFTSize.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbFFTSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFFTSize.FormattingEnabled = true;
            this.cmbFFTSize.Location = new System.Drawing.Point(146, 33);
            this.cmbFFTSize.Name = "cmbFFTSize";
            this.cmbFFTSize.Size = new System.Drawing.Size(100, 23);
            this.cmbFFTSize.TabIndex = 1;
            // 
            // lblFftSize
            // 
            this.lblFftSize.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblFftSize.BackColor = System.Drawing.Color.Transparent;
            this.lblFftSize.Location = new System.Drawing.Point(18, 33);
            this.lblFftSize.Name = "lblFftSize";
            this.lblFftSize.Size = new System.Drawing.Size(120, 23);
            this.lblFftSize.TabIndex = 0;
            this.lblFftSize.Text = "FFT 长度";
            // 
            // panelSignalGenerator
            // 
            this.panelSignalGenerator.Controls.Add(this.outputSwitch);
            this.panelSignalGenerator.Controls.Add(this.panelSine);
            this.panelSignalGenerator.Controls.Add(this.panelPink);
            this.panelSignalGenerator.Controls.Add(this.cmbOutputSignal);
            this.panelSignalGenerator.Controls.Add(this.lblSignal);
            this.panelSignalGenerator.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(240)))));
            this.panelSignalGenerator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.panelSignalGenerator.Location = new System.Drawing.Point(578, 331);
            this.panelSignalGenerator.Name = "panelSignalGenerator";
            this.panelSignalGenerator.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelSignalGenerator.Radius = 18;
            this.panelSignalGenerator.RectColor = System.Drawing.Color.Black;
            this.panelSignalGenerator.ShowRect = true;
            this.panelSignalGenerator.Size = new System.Drawing.Size(520, 140);
            this.panelSignalGenerator.TabIndex = 6;
            this.panelSignalGenerator.title = "信号发生器";
            this.panelSignalGenerator.titleHeight = 20;
            // 
            // outputSwitch
            // 
            this.outputSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.outputSwitch.BackColor = System.Drawing.Color.Transparent;
            this.outputSwitch.backDisabled = global::AEC.Properties.Resources.rta_on_disabled;
            this.outputSwitch.backHover = global::AEC.Properties.Resources.rta_on_closed_hover;
            this.outputSwitch.backNormal = global::AEC.Properties.Resources.rta_on_closed_normal;
            this.outputSwitch.backPressed = global::AEC.Properties.Resources.rta_on_closed_pressed;
            this.outputSwitch.ForeColor = System.Drawing.Color.White;
            this.outputSwitch.isOn = false;
            this.outputSwitch.isStretch = false;
            this.outputSwitch.Location = new System.Drawing.Point(223, 31);
            this.outputSwitch.Name = "outputSwitch";
            this.outputSwitch.offHover = global::AEC.Properties.Resources.rta_on_closed_hover;
            this.outputSwitch.offNormal = global::AEC.Properties.Resources.rta_on_closed_normal;
            this.outputSwitch.offPressed = global::AEC.Properties.Resources.rta_on_closed_pressed;
            this.outputSwitch.onHover = global::AEC.Properties.Resources.rta_on_opened_hover;
            this.outputSwitch.onNormal = global::AEC.Properties.Resources.rta_on_opened_normal;
            this.outputSwitch.onPressed = global::AEC.Properties.Resources.rta_on_opened_pressed;
            this.outputSwitch.Size = new System.Drawing.Size(78, 31);
            this.outputSwitch.TabIndex = 36;
            this.outputSwitch.userGDIPaint = false;
            this.outputSwitch.Click += new System.EventHandler(this.outputSwitch_Click);
            // 
            // panelSine
            // 
            this.panelSine.Controls.Add(this.paramLabelSine2Freq);
            this.panelSine.Controls.Add(this.lblSine2Freq);
            this.panelSine.Controls.Add(this.paramLabelSine1Freq);
            this.panelSine.Controls.Add(this.paramLabelSine2Level);
            this.panelSine.Controls.Add(this.lblSine1Freq);
            this.panelSine.Controls.Add(this.lblSine2Level);
            this.panelSine.Controls.Add(this.paramLabelSine1Level);
            this.panelSine.Controls.Add(this.lblSine1Level);
            this.panelSine.Location = new System.Drawing.Point(2, 70);
            this.panelSine.Name = "panelSine";
            this.panelSine.Size = new System.Drawing.Size(493, 64);
            this.panelSine.TabIndex = 35;
            // 
            // paramLabelSine2Freq
            // 
            this.paramLabelSine2Freq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.paramLabelSine2Freq.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramLabelSine2Freq.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramLabelSine2Freq.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.paramLabelSine2Freq.initV = ((short)(0));
            this.paramLabelSine2Freq.isShowUpdownButton = true;
            this.paramLabelSine2Freq.Location = new System.Drawing.Point(268, 35);
            this.paramLabelSine2Freq.Margin = new System.Windows.Forms.Padding(0);
            this.paramLabelSine2Freq.Name = "paramLabelSine2Freq";
            this.paramLabelSine2Freq.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramLabelSine2Freq.Padding = new System.Windows.Forms.Padding(2);
            this.paramLabelSine2Freq.Size = new System.Drawing.Size(100, 25);
            this.paramLabelSine2Freq.TabIndex = 5;
            this.paramLabelSine2Freq.upperLimit = ((short)(0));
            this.paramLabelSine2Freq.userGDIPaint = false;
            this.paramLabelSine2Freq.v = ((short)(0));
            // 
            // lblSine2Freq
            // 
            this.lblSine2Freq.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblSine2Freq.BackColor = System.Drawing.Color.Transparent;
            this.lblSine2Freq.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblSine2Freq.Location = new System.Drawing.Point(200, 35);
            this.lblSine2Freq.Name = "lblSine2Freq";
            this.lblSine2Freq.Size = new System.Drawing.Size(54, 23);
            this.lblSine2Freq.TabIndex = 0;
            this.lblSine2Freq.Text = "频率2";
            // 
            // paramLabelSine1Freq
            // 
            this.paramLabelSine1Freq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.paramLabelSine1Freq.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramLabelSine1Freq.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramLabelSine1Freq.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.paramLabelSine1Freq.initV = ((short)(0));
            this.paramLabelSine1Freq.isShowUpdownButton = true;
            this.paramLabelSine1Freq.Location = new System.Drawing.Point(268, 1);
            this.paramLabelSine1Freq.Margin = new System.Windows.Forms.Padding(0);
            this.paramLabelSine1Freq.Name = "paramLabelSine1Freq";
            this.paramLabelSine1Freq.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramLabelSine1Freq.Padding = new System.Windows.Forms.Padding(2);
            this.paramLabelSine1Freq.Size = new System.Drawing.Size(100, 25);
            this.paramLabelSine1Freq.TabIndex = 5;
            this.paramLabelSine1Freq.upperLimit = ((short)(0));
            this.paramLabelSine1Freq.userGDIPaint = false;
            this.paramLabelSine1Freq.v = ((short)(0));
            // 
            // paramLabelSine2Level
            // 
            this.paramLabelSine2Level.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.paramLabelSine2Level.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramLabelSine2Level.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramLabelSine2Level.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.paramLabelSine2Level.initV = ((short)(0));
            this.paramLabelSine2Level.isShowUpdownButton = true;
            this.paramLabelSine2Level.Location = new System.Drawing.Point(77, 35);
            this.paramLabelSine2Level.Margin = new System.Windows.Forms.Padding(0);
            this.paramLabelSine2Level.Name = "paramLabelSine2Level";
            this.paramLabelSine2Level.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramLabelSine2Level.Padding = new System.Windows.Forms.Padding(2);
            this.paramLabelSine2Level.Size = new System.Drawing.Size(100, 25);
            this.paramLabelSine2Level.TabIndex = 5;
            this.paramLabelSine2Level.upperLimit = ((short)(0));
            this.paramLabelSine2Level.userGDIPaint = false;
            this.paramLabelSine2Level.v = ((short)(0));
            // 
            // lblSine1Freq
            // 
            this.lblSine1Freq.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblSine1Freq.BackColor = System.Drawing.Color.Transparent;
            this.lblSine1Freq.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblSine1Freq.Location = new System.Drawing.Point(200, 1);
            this.lblSine1Freq.Name = "lblSine1Freq";
            this.lblSine1Freq.Size = new System.Drawing.Size(54, 23);
            this.lblSine1Freq.TabIndex = 0;
            this.lblSine1Freq.Text = "频率1";
            // 
            // lblSine2Level
            // 
            this.lblSine2Level.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblSine2Level.BackColor = System.Drawing.Color.Transparent;
            this.lblSine2Level.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblSine2Level.Location = new System.Drawing.Point(9, 35);
            this.lblSine2Level.Name = "lblSine2Level";
            this.lblSine2Level.Size = new System.Drawing.Size(54, 23);
            this.lblSine2Level.TabIndex = 0;
            this.lblSine2Level.Text = "电平2";
            // 
            // paramLabelSine1Level
            // 
            this.paramLabelSine1Level.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.paramLabelSine1Level.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramLabelSine1Level.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramLabelSine1Level.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.paramLabelSine1Level.initV = ((short)(0));
            this.paramLabelSine1Level.isShowUpdownButton = true;
            this.paramLabelSine1Level.Location = new System.Drawing.Point(77, 1);
            this.paramLabelSine1Level.Margin = new System.Windows.Forms.Padding(0);
            this.paramLabelSine1Level.Name = "paramLabelSine1Level";
            this.paramLabelSine1Level.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramLabelSine1Level.Padding = new System.Windows.Forms.Padding(2);
            this.paramLabelSine1Level.Size = new System.Drawing.Size(100, 25);
            this.paramLabelSine1Level.TabIndex = 5;
            this.paramLabelSine1Level.upperLimit = ((short)(0));
            this.paramLabelSine1Level.userGDIPaint = false;
            this.paramLabelSine1Level.v = ((short)(0));
            // 
            // lblSine1Level
            // 
            this.lblSine1Level.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblSine1Level.BackColor = System.Drawing.Color.Transparent;
            this.lblSine1Level.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblSine1Level.Location = new System.Drawing.Point(9, 1);
            this.lblSine1Level.Name = "lblSine1Level";
            this.lblSine1Level.Size = new System.Drawing.Size(54, 23);
            this.lblSine1Level.TabIndex = 0;
            this.lblSine1Level.Text = "电平1";
            // 
            // panelPink
            // 
            this.panelPink.Controls.Add(this.paramLabelPinkLevel);
            this.panelPink.Controls.Add(this.lblLevel);
            this.panelPink.Location = new System.Drawing.Point(2, 70);
            this.panelPink.Name = "panelPink";
            this.panelPink.Size = new System.Drawing.Size(493, 64);
            this.panelPink.TabIndex = 34;
            // 
            // paramLabelPinkLevel
            // 
            this.paramLabelPinkLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.paramLabelPinkLevel.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramLabelPinkLevel.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramLabelPinkLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.paramLabelPinkLevel.initV = ((short)(0));
            this.paramLabelPinkLevel.isShowUpdownButton = true;
            this.paramLabelPinkLevel.Location = new System.Drawing.Point(77, 1);
            this.paramLabelPinkLevel.Margin = new System.Windows.Forms.Padding(0);
            this.paramLabelPinkLevel.Name = "paramLabelPinkLevel";
            this.paramLabelPinkLevel.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramLabelPinkLevel.Padding = new System.Windows.Forms.Padding(2);
            this.paramLabelPinkLevel.Size = new System.Drawing.Size(100, 25);
            this.paramLabelPinkLevel.TabIndex = 5;
            this.paramLabelPinkLevel.upperLimit = ((short)(0));
            this.paramLabelPinkLevel.userGDIPaint = false;
            this.paramLabelPinkLevel.v = ((short)(0));
            // 
            // lblLevel
            // 
            this.lblLevel.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblLevel.BackColor = System.Drawing.Color.Transparent;
            this.lblLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblLevel.Location = new System.Drawing.Point(9, 1);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(54, 23);
            this.lblLevel.TabIndex = 0;
            this.lblLevel.Text = "电平";
            // 
            // cmbOutputSignal
            // 
            this.cmbOutputSignal.ArrowColor = System.Drawing.Color.Gray;
            this.cmbOutputSignal.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbOutputSignal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOutputSignal.FormattingEnabled = true;
            this.cmbOutputSignal.Location = new System.Drawing.Point(79, 34);
            this.cmbOutputSignal.Name = "cmbOutputSignal";
            this.cmbOutputSignal.Size = new System.Drawing.Size(128, 23);
            this.cmbOutputSignal.TabIndex = 1;
            // 
            // lblSignal
            // 
            this.lblSignal.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblSignal.BackColor = System.Drawing.Color.Transparent;
            this.lblSignal.Location = new System.Drawing.Point(9, 34);
            this.lblSignal.Name = "lblSignal";
            this.lblSignal.Size = new System.Drawing.Size(54, 23);
            this.lblSignal.TabIndex = 0;
            this.lblSignal.Text = "信号";
            // 
            // panelDelay
            // 
            this.panelDelay.Controls.Add(this.progressBar);
            this.panelDelay.Controls.Add(this.btnDeclineMeasResult);
            this.panelDelay.Controls.Add(this.btnApplyMeasResult);
            this.panelDelay.Controls.Add(this.btnMeasDelay);
            this.panelDelay.Controls.Add(this.lblMeasDelayResult);
            this.panelDelay.Controls.Add(this.lblDelay);
            this.panelDelay.Controls.Add(this.paramLabelMeasDelayResult);
            this.panelDelay.Controls.Add(this.paramLabelDelay);
            this.panelDelay.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(240)))));
            this.panelDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.panelDelay.Location = new System.Drawing.Point(578, 202);
            this.panelDelay.Name = "panelDelay";
            this.panelDelay.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelDelay.Radius = 18;
            this.panelDelay.RectColor = System.Drawing.Color.Black;
            this.panelDelay.ShowRect = true;
            this.panelDelay.Size = new System.Drawing.Size(520, 120);
            this.panelDelay.TabIndex = 6;
            this.panelDelay.title = "延时";
            this.panelDelay.titleHeight = 20;
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.Color.Transparent;
            this.progressBar.Location = new System.Drawing.Point(344, 33);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(168, 23);
            this.progressBar.TabIndex = 7;
            this.progressBar.Text = "progressBar1";
            this.progressBar.Visible = false;
            // 
            // btnDeclineMeasResult
            // 
            this.btnDeclineMeasResult.BackColor = System.Drawing.Color.Transparent;
            this.btnDeclineMeasResult.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.btnDeclineMeasResult.backHover = global::AEC.Properties.Resources.btn_hover;
            this.btnDeclineMeasResult.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.btnDeclineMeasResult.backPressed = global::AEC.Properties.Resources.btn_pressed;
            this.btnDeclineMeasResult.Enabled = false;
            this.btnDeclineMeasResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.btnDeclineMeasResult.isStretch = true;
            this.btnDeclineMeasResult.Location = new System.Drawing.Point(345, 74);
            this.btnDeclineMeasResult.Name = "btnDeclineMeasResult";
            this.btnDeclineMeasResult.Size = new System.Drawing.Size(80, 30);
            this.btnDeclineMeasResult.TabIndex = 6;
            this.btnDeclineMeasResult.Text = "拒绝";
            this.btnDeclineMeasResult.userGDIPaint = true;
            this.btnDeclineMeasResult.Click += new System.EventHandler(this.btnDeclineMeasResult_Click);
            // 
            // btnApplyMeasResult
            // 
            this.btnApplyMeasResult.BackColor = System.Drawing.Color.Transparent;
            this.btnApplyMeasResult.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.btnApplyMeasResult.backHover = global::AEC.Properties.Resources.btn_hover;
            this.btnApplyMeasResult.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.btnApplyMeasResult.backPressed = global::AEC.Properties.Resources.btn_pressed;
            this.btnApplyMeasResult.Enabled = false;
            this.btnApplyMeasResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.btnApplyMeasResult.isStretch = true;
            this.btnApplyMeasResult.Location = new System.Drawing.Point(259, 74);
            this.btnApplyMeasResult.Name = "btnApplyMeasResult";
            this.btnApplyMeasResult.Size = new System.Drawing.Size(80, 30);
            this.btnApplyMeasResult.TabIndex = 6;
            this.btnApplyMeasResult.Text = "应用";
            this.btnApplyMeasResult.userGDIPaint = true;
            this.btnApplyMeasResult.Click += new System.EventHandler(this.btnApplyMeasResult_Click);
            // 
            // btnMeasDelay
            // 
            this.btnMeasDelay.BackColor = System.Drawing.Color.Transparent;
            this.btnMeasDelay.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.btnMeasDelay.backHover = global::AEC.Properties.Resources.btn_hover;
            this.btnMeasDelay.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.btnMeasDelay.backPressed = global::AEC.Properties.Resources.btn_pressed;
            this.btnMeasDelay.Enabled = false;
            this.btnMeasDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.btnMeasDelay.isStretch = true;
            this.btnMeasDelay.Location = new System.Drawing.Point(259, 30);
            this.btnMeasDelay.Name = "btnMeasDelay";
            this.btnMeasDelay.Size = new System.Drawing.Size(80, 30);
            this.btnMeasDelay.TabIndex = 6;
            this.btnMeasDelay.Text = "测量延时";
            this.btnMeasDelay.userGDIPaint = true;
            this.btnMeasDelay.Click += new System.EventHandler(this.btnMeasDelay_Click);
            // 
            // lblMeasDelayResult
            // 
            this.lblMeasDelayResult.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblMeasDelayResult.BackColor = System.Drawing.Color.Transparent;
            this.lblMeasDelayResult.Location = new System.Drawing.Point(4, 77);
            this.lblMeasDelayResult.Name = "lblMeasDelayResult";
            this.lblMeasDelayResult.Size = new System.Drawing.Size(78, 23);
            this.lblMeasDelayResult.TabIndex = 0;
            this.lblMeasDelayResult.Text = "测量结果";
            // 
            // lblDelay
            // 
            this.lblDelay.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblDelay.BackColor = System.Drawing.Color.Transparent;
            this.lblDelay.Location = new System.Drawing.Point(40, 34);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(42, 23);
            this.lblDelay.TabIndex = 0;
            this.lblDelay.Text = "延时";
            // 
            // paramLabelMeasDelayResult
            // 
            this.paramLabelMeasDelayResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.paramLabelMeasDelayResult.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramLabelMeasDelayResult.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramLabelMeasDelayResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.paramLabelMeasDelayResult.initV = ((short)(0));
            this.paramLabelMeasDelayResult.isShowUpdownButton = false;
            this.paramLabelMeasDelayResult.Location = new System.Drawing.Point(86, 76);
            this.paramLabelMeasDelayResult.Margin = new System.Windows.Forms.Padding(0);
            this.paramLabelMeasDelayResult.Name = "paramLabelMeasDelayResult";
            this.paramLabelMeasDelayResult.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramLabelMeasDelayResult.Padding = new System.Windows.Forms.Padding(2);
            this.paramLabelMeasDelayResult.Size = new System.Drawing.Size(155, 25);
            this.paramLabelMeasDelayResult.TabIndex = 5;
            this.paramLabelMeasDelayResult.upperLimit = ((short)(0));
            this.paramLabelMeasDelayResult.userGDIPaint = false;
            this.paramLabelMeasDelayResult.v = ((short)(0));
            // 
            // paramLabelDelay
            // 
            this.paramLabelDelay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.paramLabelDelay.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramLabelDelay.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramLabelDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.paramLabelDelay.initV = ((short)(0));
            this.paramLabelDelay.isShowUpdownButton = true;
            this.paramLabelDelay.Location = new System.Drawing.Point(86, 32);
            this.paramLabelDelay.Margin = new System.Windows.Forms.Padding(0);
            this.paramLabelDelay.Name = "paramLabelDelay";
            this.paramLabelDelay.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramLabelDelay.Padding = new System.Windows.Forms.Padding(2);
            this.paramLabelDelay.Size = new System.Drawing.Size(168, 25);
            this.paramLabelDelay.TabIndex = 5;
            this.paramLabelDelay.upperLimit = ((short)(0));
            this.paramLabelDelay.userGDIPaint = false;
            this.paramLabelDelay.v = ((short)(0));
            // 
            // panelSignalLevel
            // 
            this.panelSignalLevel.Controls.Add(this.levelLeft);
            this.panelSignalLevel.Controls.Add(this.levelRight);
            this.panelSignalLevel.Controls.Add(this.lblRightCh);
            this.panelSignalLevel.Controls.Add(this.lblLeftCh);
            this.panelSignalLevel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(240)))));
            this.panelSignalLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.panelSignalLevel.Location = new System.Drawing.Point(578, 85);
            this.panelSignalLevel.Name = "panelSignalLevel";
            this.panelSignalLevel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelSignalLevel.Radius = 18;
            this.panelSignalLevel.RectColor = System.Drawing.Color.Black;
            this.panelSignalLevel.ShowRect = true;
            this.panelSignalLevel.Size = new System.Drawing.Size(520, 108);
            this.panelSignalLevel.TabIndex = 6;
            this.panelSignalLevel.title = "信号电平";
            this.panelSignalLevel.titleHeight = 20;
            // 
            // levelLeft
            // 
            this.levelLeft.BackColor = System.Drawing.Color.Transparent;
            this.levelLeft.isShowLevel = false;
            this.levelLeft.isShowTick = false;
            this.levelLeft.Location = new System.Drawing.Point(168, 31);
            this.levelLeft.Name = "levelLeft";
            this.levelLeft.Size = new System.Drawing.Size(336, 25);
            this.levelLeft.TabIndex = 0;
            this.levelLeft.Text = "audioInputLevel1";
            // 
            // levelRight
            // 
            this.levelRight.BackColor = System.Drawing.Color.Transparent;
            this.levelRight.isShowLevel = false;
            this.levelRight.isShowTick = true;
            this.levelRight.Location = new System.Drawing.Point(168, 54);
            this.levelRight.Name = "levelRight";
            this.levelRight.Size = new System.Drawing.Size(336, 45);
            this.levelRight.TabIndex = 0;
            this.levelRight.Text = "audioInputLevel1";
            // 
            // lblRightCh
            // 
            this.lblRightCh.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblRightCh.BackColor = System.Drawing.Color.Transparent;
            this.lblRightCh.Location = new System.Drawing.Point(16, 55);
            this.lblRightCh.Name = "lblRightCh";
            this.lblRightCh.Size = new System.Drawing.Size(148, 23);
            this.lblRightCh.TabIndex = 0;
            this.lblRightCh.Text = "右声道（参考信号）";
            // 
            // lblLeftCh
            // 
            this.lblLeftCh.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblLeftCh.BackColor = System.Drawing.Color.Transparent;
            this.lblLeftCh.Location = new System.Drawing.Point(16, 32);
            this.lblLeftCh.Name = "lblLeftCh";
            this.lblLeftCh.Size = new System.Drawing.Size(148, 23);
            this.lblLeftCh.TabIndex = 0;
            this.lblLeftCh.Text = "左声道（测量信号）";
            // 
            // panelAudioIO
            // 
            this.panelAudioIO.Controls.Add(this.btnRefreshOutputDevice);
            this.panelAudioIO.Controls.Add(this.rdoBitsPerSample24);
            this.panelAudioIO.Controls.Add(this.rdoBitsPerSample16);
            this.panelAudioIO.Controls.Add(this.btnRefreshInputDevice);
            this.panelAudioIO.Controls.Add(this.splitLine1);
            this.panelAudioIO.Controls.Add(this.cmbAudioOutDevice);
            this.panelAudioIO.Controls.Add(this.cmbSampleRate);
            this.panelAudioIO.Controls.Add(this.cmbAudioInDevice);
            this.panelAudioIO.Controls.Add(this.lblSampleBitDepth);
            this.panelAudioIO.Controls.Add(this.lblSampleRate);
            this.panelAudioIO.Controls.Add(this.lblAudioOutDevice);
            this.panelAudioIO.Controls.Add(this.lblAudioInDevice);
            this.panelAudioIO.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(240)))));
            this.panelAudioIO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.panelAudioIO.Location = new System.Drawing.Point(12, 42);
            this.panelAudioIO.Name = "panelAudioIO";
            this.panelAudioIO.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelAudioIO.Radius = 18;
            this.panelAudioIO.RectColor = System.Drawing.Color.Black;
            this.panelAudioIO.ShowRect = true;
            this.panelAudioIO.Size = new System.Drawing.Size(555, 224);
            this.panelAudioIO.TabIndex = 6;
            this.panelAudioIO.title = "音频输入输出";
            this.panelAudioIO.titleHeight = 20;
            // 
            // btnRefreshOutputDevice
            // 
            this.btnRefreshOutputDevice.BackColor = System.Drawing.Color.Transparent;
            this.btnRefreshOutputDevice.backDisabled = global::AEC.Properties.Resources.rta_refresh_disabled;
            this.btnRefreshOutputDevice.backHover = global::AEC.Properties.Resources.rta_refresh_hover;
            this.btnRefreshOutputDevice.backNormal = global::AEC.Properties.Resources.rta_refresh_normal;
            this.btnRefreshOutputDevice.backPressed = global::AEC.Properties.Resources.rta_refresh_pressed;
            this.btnRefreshOutputDevice.ForeColor = System.Drawing.Color.White;
            this.btnRefreshOutputDevice.isStretch = false;
            this.btnRefreshOutputDevice.Location = new System.Drawing.Point(504, 184);
            this.btnRefreshOutputDevice.Name = "btnRefreshOutputDevice";
            this.btnRefreshOutputDevice.Size = new System.Drawing.Size(24, 24);
            this.btnRefreshOutputDevice.TabIndex = 5;
            this.btnRefreshOutputDevice.userGDIPaint = false;
            this.btnRefreshOutputDevice.Click += new System.EventHandler(this.btn_Click);
            // 
            // rdoBitsPerSample24
            // 
            this.rdoBitsPerSample24.backChecked = global::AEC.Properties.Resources.rta_bitdepth_checked;
            this.rdoBitsPerSample24.BackColor = System.Drawing.Color.Transparent;
            this.rdoBitsPerSample24.backDisabled = global::AEC.Properties.Resources.rta_bitdepth_disabled;
            this.rdoBitsPerSample24.backHover = global::AEC.Properties.Resources.rta_bitdepth_hover;
            this.rdoBitsPerSample24.backNormal = global::AEC.Properties.Resources.rta_bitdepth_normal;
            this.rdoBitsPerSample24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.rdoBitsPerSample24.isAutoCheck = true;
            this.rdoBitsPerSample24.isChecked = false;
            this.rdoBitsPerSample24.isCheckInit = false;
            this.rdoBitsPerSample24.isShowCheckedBackWhenDsiabled = false;
            this.rdoBitsPerSample24.isStretch = true;
            this.rdoBitsPerSample24.Location = new System.Drawing.Point(266, 122);
            this.rdoBitsPerSample24.Margin = new System.Windows.Forms.Padding(0);
            this.rdoBitsPerSample24.Name = "rdoBitsPerSample24";
            this.rdoBitsPerSample24.Size = new System.Drawing.Size(96, 31);
            this.rdoBitsPerSample24.TabIndex = 6;
            this.rdoBitsPerSample24.Text = "24 Bit";
            this.rdoBitsPerSample24.userGDIPaint = false;
            // 
            // rdoBitsPerSample16
            // 
            this.rdoBitsPerSample16.backChecked = global::AEC.Properties.Resources.rta_bitdepth_checked;
            this.rdoBitsPerSample16.BackColor = System.Drawing.Color.Transparent;
            this.rdoBitsPerSample16.backDisabled = global::AEC.Properties.Resources.rta_bitdepth_disabled;
            this.rdoBitsPerSample16.backHover = global::AEC.Properties.Resources.rta_bitdepth_hover;
            this.rdoBitsPerSample16.backNormal = global::AEC.Properties.Resources.rta_bitdepth_normal;
            this.rdoBitsPerSample16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.rdoBitsPerSample16.isAutoCheck = true;
            this.rdoBitsPerSample16.isChecked = false;
            this.rdoBitsPerSample16.isCheckInit = false;
            this.rdoBitsPerSample16.isShowCheckedBackWhenDsiabled = false;
            this.rdoBitsPerSample16.isStretch = true;
            this.rdoBitsPerSample16.Location = new System.Drawing.Point(143, 122);
            this.rdoBitsPerSample16.Margin = new System.Windows.Forms.Padding(0);
            this.rdoBitsPerSample16.Name = "rdoBitsPerSample16";
            this.rdoBitsPerSample16.Size = new System.Drawing.Size(96, 31);
            this.rdoBitsPerSample16.TabIndex = 6;
            this.rdoBitsPerSample16.Text = "16 Bit";
            this.rdoBitsPerSample16.userGDIPaint = false;
            // 
            // btnRefreshInputDevice
            // 
            this.btnRefreshInputDevice.BackColor = System.Drawing.Color.Transparent;
            this.btnRefreshInputDevice.backDisabled = global::AEC.Properties.Resources.rta_refresh_disabled;
            this.btnRefreshInputDevice.backHover = global::AEC.Properties.Resources.rta_refresh_hover;
            this.btnRefreshInputDevice.backNormal = global::AEC.Properties.Resources.rta_refresh_normal;
            this.btnRefreshInputDevice.backPressed = global::AEC.Properties.Resources.rta_refresh_pressed;
            this.btnRefreshInputDevice.ForeColor = System.Drawing.Color.White;
            this.btnRefreshInputDevice.isStretch = false;
            this.btnRefreshInputDevice.Location = new System.Drawing.Point(504, 43);
            this.btnRefreshInputDevice.Name = "btnRefreshInputDevice";
            this.btnRefreshInputDevice.Size = new System.Drawing.Size(24, 24);
            this.btnRefreshInputDevice.TabIndex = 5;
            this.btnRefreshInputDevice.userGDIPaint = false;
            this.btnRefreshInputDevice.Click += new System.EventHandler(this.btn_Click);
            // 
            // splitLine1
            // 
            this.splitLine1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.splitLine1.Location = new System.Drawing.Point(55, 163);
            this.splitLine1.Margin = new System.Windows.Forms.Padding(0);
            this.splitLine1.Name = "splitLine1";
            this.splitLine1.Size = new System.Drawing.Size(468, 2);
            this.splitLine1.TabIndex = 2;
            this.splitLine1.Text = "splitLine1";
            // 
            // cmbAudioOutDevice
            // 
            this.cmbAudioOutDevice.ArrowColor = System.Drawing.Color.Gray;
            this.cmbAudioOutDevice.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbAudioOutDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAudioOutDevice.FormattingEnabled = true;
            this.cmbAudioOutDevice.Location = new System.Drawing.Point(146, 184);
            this.cmbAudioOutDevice.Name = "cmbAudioOutDevice";
            this.cmbAudioOutDevice.Size = new System.Drawing.Size(350, 23);
            this.cmbAudioOutDevice.TabIndex = 1;
            // 
            // cmbSampleRate
            // 
            this.cmbSampleRate.ArrowColor = System.Drawing.Color.Gray;
            this.cmbSampleRate.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbSampleRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSampleRate.FormattingEnabled = true;
            this.cmbSampleRate.Location = new System.Drawing.Point(146, 83);
            this.cmbSampleRate.Name = "cmbSampleRate";
            this.cmbSampleRate.Size = new System.Drawing.Size(100, 23);
            this.cmbSampleRate.TabIndex = 1;
            // 
            // cmbAudioInDevice
            // 
            this.cmbAudioInDevice.ArrowColor = System.Drawing.Color.Gray;
            this.cmbAudioInDevice.DropDownBtnColor = System.Drawing.Color.White;
            this.cmbAudioInDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAudioInDevice.FormattingEnabled = true;
            this.cmbAudioInDevice.Location = new System.Drawing.Point(146, 43);
            this.cmbAudioInDevice.Name = "cmbAudioInDevice";
            this.cmbAudioInDevice.Size = new System.Drawing.Size(350, 23);
            this.cmbAudioInDevice.TabIndex = 1;
            // 
            // lblSampleBitDepth
            // 
            this.lblSampleBitDepth.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblSampleBitDepth.BackColor = System.Drawing.Color.Transparent;
            this.lblSampleBitDepth.Location = new System.Drawing.Point(18, 127);
            this.lblSampleBitDepth.Name = "lblSampleBitDepth";
            this.lblSampleBitDepth.Size = new System.Drawing.Size(120, 23);
            this.lblSampleBitDepth.TabIndex = 0;
            this.lblSampleBitDepth.Text = "采样位深度";
            // 
            // lblSampleRate
            // 
            this.lblSampleRate.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblSampleRate.BackColor = System.Drawing.Color.Transparent;
            this.lblSampleRate.Location = new System.Drawing.Point(18, 83);
            this.lblSampleRate.Name = "lblSampleRate";
            this.lblSampleRate.Size = new System.Drawing.Size(120, 23);
            this.lblSampleRate.TabIndex = 0;
            this.lblSampleRate.Text = "采样频率";
            // 
            // lblAudioOutDevice
            // 
            this.lblAudioOutDevice.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblAudioOutDevice.BackColor = System.Drawing.Color.Transparent;
            this.lblAudioOutDevice.Location = new System.Drawing.Point(10, 184);
            this.lblAudioOutDevice.Name = "lblAudioOutDevice";
            this.lblAudioOutDevice.Size = new System.Drawing.Size(128, 23);
            this.lblAudioOutDevice.TabIndex = 0;
            this.lblAudioOutDevice.Text = "音频输出设备";
            // 
            // lblAudioInDevice
            // 
            this.lblAudioInDevice.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblAudioInDevice.BackColor = System.Drawing.Color.Transparent;
            this.lblAudioInDevice.Location = new System.Drawing.Point(10, 43);
            this.lblAudioInDevice.Name = "lblAudioInDevice";
            this.lblAudioInDevice.Size = new System.Drawing.Size(128, 23);
            this.lblAudioInDevice.TabIndex = 0;
            this.lblAudioInDevice.Text = "音频输入设备";
            // 
            // title
            // 
            this.title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.title.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.isAllowDoubleClickMaximize = false;
            this.title.isShowCloseButton = true;
            this.title.isShowSystemBtns = false;
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Margin = new System.Windows.Forms.Padding(0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(1110, 32);
            this.title.TabIndex = 5;
            // 
            // FormRTAOptions
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1110, 576);
            this.Controls.Add(this.rtaPause);
            this.Controls.Add(this.rtaSwitch);
            this.Controls.Add(this.panelRefTrace);
            this.Controls.Add(this.panelRTAModePanel);
            this.Controls.Add(this.panelRTAParams);
            this.Controls.Add(this.panelSignalGenerator);
            this.Controls.Add(this.panelDelay);
            this.Controls.Add(this.panelSignalLevel);
            this.Controls.Add(this.panelAudioIO);
            this.Controls.Add(this.title);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormRTAOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RTA选项";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRTAOptions_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormRTAOptions_FormClosed);
            this.Load += new System.EventHandler(this.FormRTAOption_Load);
            this.panelRTAModePanel.ResumeLayout(false);
            this.panelRefTrace.ResumeLayout(false);
            this.panelCapture.ResumeLayout(false);
            this.panelRTAParams.ResumeLayout(false);
            this.panelTFParam.ResumeLayout(false);
            this.panelSPParam.ResumeLayout(false);
            this.panelSignalGenerator.ResumeLayout(false);
            this.panelSine.ResumeLayout(false);
            this.panelPink.ResumeLayout(false);
            this.panelDelay.ResumeLayout(false);
            this.panelSignalLevel.ResumeLayout(false);
            this.panelAudioIO.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Title title;
        private gtPanel panelAudioIO;
        private SplitLine splitLine1;
        private gtComboBox cmbAudioOutDevice;
        private gtComboBox cmbAudioInDevice;
        private gtLabel lblSampleBitDepth;
        private gtLabel lblSampleRate;
        private gtLabel lblAudioOutDevice;
        private gtLabel lblAudioInDevice;
        private gtComboBox cmbSampleRate;
        private gtPanel panelSignalLevel;
        private gtPanel panelDelay;
        private gtPanel panelSignalGenerator;
        private System.Windows.Forms.Panel panelRTAModePanel;
        private gtButton btnRefreshInputDevice;
        private gtPanel panelRTAParams;
        private System.Windows.Forms.Panel panelSPParam;
        private gtComboBox cmbScale;
        private gtLabel lblSpectrumGraph;
        private gtLabel lblScale;
        private gtComboBox cmbWindowFunction;
        private gtLabel lblWindowFunction;
        private gtComboBox cmbSmooth;
        private gtLabel lblSmooth;
        private gtComboBox cmbAverage;
        private gtLabel lblAverage;
        private gtComboBox cmbFFTSize;
        private gtLabel lblFftSize;
        private System.Windows.Forms.Panel panelTFParam;
        private gtLabel lblFreqRespGraph;
        private gtLabel lblAmpOffset;
        private ParamLabel paramLabelAmpOffset;
        private System.Windows.Forms.Panel panelPink;
        private ParamLabel paramLabelPinkLevel;
        private gtLabel lblLevel;
        private gtComboBox cmbOutputSignal;
        private gtLabel lblSignal;
        private System.Windows.Forms.Panel panelSine;
        private ParamLabel paramLabelSine2Freq;
        private gtLabel lblSine2Freq;
        private ParamLabel paramLabelSine1Freq;
        private ParamLabel paramLabelSine2Level;
        private gtLabel lblSine1Freq;
        private gtLabel lblSine2Level;
        private ParamLabel paramLabelSine1Level;
        private gtLabel lblSine1Level;
        private AudioInputLevel levelRight;
        private AudioInputLevel levelLeft;
        private gtLabel lblRightCh;
        private gtLabel lblLeftCh;
        private gtButton btnDeclineMeasResult;
        private gtButton btnApplyMeasResult;
        private gtButton btnMeasDelay;
        private gtLabel lblMeasDelayResult;
        private gtLabel lblDelay;
        private ParamLabel paramLabelMeasDelayResult;
        private ParamLabel paramLabelDelay;
        private ProgressBar progressBar;
        private gtPanel panelRefTrace;
        private RTASwitch outputSwitch;
        private TraceGroup traceGroupA;
        private TraceGroup traceGroupE;
        private TraceGroup traceGroupD;
        private TraceGroup traceGroupC;
        private TraceGroup traceGroupB;
        private System.Windows.Forms.Panel panelCapture;
        private gtButton btnCapture;
        private gtButton btnDelete;
        private gtButton btnRefreshOutputDevice;
        private RTASwitch refTraceSwitch;
        private gtRadioButton rdoTransferFunction;
        private gtRadioButton rdoSpectrum;
        private gtRadioButton rdoBitsPerSample16;
        private gtRadioButton rdoBitsPerSample24;
        private gtRadioButton rdoDoubleCh;
        private gtRadioButton rdoAmpAndPhaseResp;
        private gtRadioButton rdoPhaseResp;
        private gtRadioButton rdoAmpResp;
        private gtRadioButton rdoRightCh;
        private gtRadioButton rdoLeftCh;
        private RTASwitch rtaSwitch;
        private RTASwitch rtaPause;
    }
}