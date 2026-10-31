namespace AEC.UI.VX
{
    partial class VXMonitorPanel
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panelSurOutput = new AEC.UI.gtPanel();
            this.panelMonitorSurOutput = new AEC.UI.VX.VXMonitorDualChOutputPanel();
            this.gtLabel5 = new AEC.UI.gtLabel();
            this.eqThumbnailSurOutput = new AEC.UI.Controls.EQThumbnail();
            this.panelAux2Output = new AEC.UI.gtPanel();
            this.panelMonitorAux2Output = new AEC.UI.VX.VXMonitorSingleChOutputPanel();
            this.gtLabel4 = new AEC.UI.gtLabel();
            this.eqThumbnailAux2Output = new AEC.UI.Controls.EQThumbnail();
            this.panelAux1Output = new AEC.UI.gtPanel();
            this.panelMonitorAux1Output = new AEC.UI.VX.VXMonitorSingleChOutputPanel();
            this.gtLabel3 = new AEC.UI.gtLabel();
            this.eqThumbnailAux1Output = new AEC.UI.Controls.EQThumbnail();
            this.panelMainOutput = new AEC.UI.gtPanel();
            this.panelMonitorMainOutput = new AEC.UI.VX.VXMonitorDualChOutputPanel();
            this.gtLabel2 = new AEC.UI.gtLabel();
            this.eqThumbnailMainOutput = new AEC.UI.Controls.EQThumbnail();
            this.panelSurOutput.SuspendLayout();
            this.panelAux2Output.SuspendLayout();
            this.panelAux1Output.SuspendLayout();
            this.panelMainOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSurOutput
            // 
            this.panelSurOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelSurOutput.Controls.Add(this.panelMonitorSurOutput);
            this.panelSurOutput.Controls.Add(this.gtLabel5);
            this.panelSurOutput.Controls.Add(this.eqThumbnailSurOutput);
            this.panelSurOutput.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelSurOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.panelSurOutput.Location = new System.Drawing.Point(891, 20);
            this.panelSurOutput.Name = "panelSurOutput";
            this.panelSurOutput.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelSurOutput.Radius = 18;
            this.panelSurOutput.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.panelSurOutput.ShowRect = true;
            this.panelSurOutput.Size = new System.Drawing.Size(264, 612);
            this.panelSurOutput.TabIndex = 0;
            this.panelSurOutput.title = "后置输出";
            this.panelSurOutput.titleHeight = 20;
            // 
            // panelMonitorSurOutput
            // 
            this.panelMonitorSurOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelMonitorSurOutput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelMonitorSurOutput.Location = new System.Drawing.Point(4, 102);
            this.panelMonitorSurOutput.Name = "panelMonitorSurOutput";
            this.panelMonitorSurOutput.Size = new System.Drawing.Size(256, 502);
            this.panelMonitorSurOutput.TabIndex = 2;
            // 
            // gtLabel5
            // 
            this.gtLabel5.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.gtLabel5.BackColor = System.Drawing.Color.Transparent;
            this.gtLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.gtLabel5.Location = new System.Drawing.Point(89, 28);
            this.gtLabel5.Name = "gtLabel5";
            this.gtLabel5.Size = new System.Drawing.Size(86, 23);
            this.gtLabel5.TabIndex = 1;
            this.gtLabel5.Text = "EQ";
            // 
            // eqThumbnailSurOutput
            // 
            this.eqThumbnailSurOutput.BackColor = System.Drawing.Color.Transparent;
            this.eqThumbnailSurOutput.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.eqThumbnailSurOutput.BorderWidth = 1F;
            this.eqThumbnailSurOutput.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.eqThumbnailSurOutput.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.eqThumbnailSurOutput.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.eqThumbnailSurOutput.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.eqThumbnailSurOutput.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.eqThumbnailSurOutput.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.eqThumbnailSurOutput.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.eqThumbnailSurOutput.isShowBackEQLabel = false;
            this.eqThumbnailSurOutput.Location = new System.Drawing.Point(89, 51);
            this.eqThumbnailSurOutput.Name = "eqThumbnailSurOutput";
            this.eqThumbnailSurOutput.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.eqThumbnailSurOutput.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.eqThumbnailSurOutput.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.eqThumbnailSurOutput.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.eqThumbnailSurOutput.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.eqThumbnailSurOutput.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.eqThumbnailSurOutput.Radius = 12;
            this.eqThumbnailSurOutput.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.eqThumbnailSurOutput.ShowBorder = false;
            this.eqThumbnailSurOutput.ShowText = true;
            this.eqThumbnailSurOutput.Size = new System.Drawing.Size(86, 42);
            this.eqThumbnailSurOutput.TabIndex = 0;
            this.eqThumbnailSurOutput.Text = "eqThumbnail1";
            this.eqThumbnailSurOutput.MouseClick += new System.Windows.Forms.MouseEventHandler(this.eqThumbnail_MouseClick);
            // 
            // panelAux2Output
            // 
            this.panelAux2Output.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelAux2Output.Controls.Add(this.panelMonitorAux2Output);
            this.panelAux2Output.Controls.Add(this.gtLabel4);
            this.panelAux2Output.Controls.Add(this.eqThumbnailAux2Output);
            this.panelAux2Output.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelAux2Output.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.panelAux2Output.Location = new System.Drawing.Point(641, 20);
            this.panelAux2Output.Name = "panelAux2Output";
            this.panelAux2Output.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelAux2Output.Radius = 18;
            this.panelAux2Output.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.panelAux2Output.ShowRect = true;
            this.panelAux2Output.Size = new System.Drawing.Size(244, 612);
            this.panelAux2Output.TabIndex = 0;
            this.panelAux2Output.title = "辅助输出2";
            this.panelAux2Output.titleHeight = 20;
            // 
            // panelMonitorAux2Output
            // 
            this.panelMonitorAux2Output.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelMonitorAux2Output.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelMonitorAux2Output.Location = new System.Drawing.Point(4, 102);
            this.panelMonitorAux2Output.Name = "panelMonitorAux2Output";
            this.panelMonitorAux2Output.Size = new System.Drawing.Size(238, 502);
            this.panelMonitorAux2Output.TabIndex = 2;
            // 
            // gtLabel4
            // 
            this.gtLabel4.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.gtLabel4.BackColor = System.Drawing.Color.Transparent;
            this.gtLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.gtLabel4.Location = new System.Drawing.Point(79, 28);
            this.gtLabel4.Name = "gtLabel4";
            this.gtLabel4.Size = new System.Drawing.Size(86, 23);
            this.gtLabel4.TabIndex = 1;
            this.gtLabel4.Text = "EQ";
            // 
            // eqThumbnailAux2Output
            // 
            this.eqThumbnailAux2Output.BackColor = System.Drawing.Color.Transparent;
            this.eqThumbnailAux2Output.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.eqThumbnailAux2Output.BorderWidth = 1F;
            this.eqThumbnailAux2Output.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.eqThumbnailAux2Output.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.eqThumbnailAux2Output.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.eqThumbnailAux2Output.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.eqThumbnailAux2Output.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.eqThumbnailAux2Output.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.eqThumbnailAux2Output.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.eqThumbnailAux2Output.isShowBackEQLabel = false;
            this.eqThumbnailAux2Output.Location = new System.Drawing.Point(79, 51);
            this.eqThumbnailAux2Output.Name = "eqThumbnailAux2Output";
            this.eqThumbnailAux2Output.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.eqThumbnailAux2Output.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.eqThumbnailAux2Output.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.eqThumbnailAux2Output.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.eqThumbnailAux2Output.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.eqThumbnailAux2Output.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.eqThumbnailAux2Output.Radius = 12;
            this.eqThumbnailAux2Output.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.eqThumbnailAux2Output.ShowBorder = false;
            this.eqThumbnailAux2Output.ShowText = true;
            this.eqThumbnailAux2Output.Size = new System.Drawing.Size(86, 42);
            this.eqThumbnailAux2Output.TabIndex = 0;
            this.eqThumbnailAux2Output.Text = "eqThumbnail1";
            this.eqThumbnailAux2Output.MouseClick += new System.Windows.Forms.MouseEventHandler(this.eqThumbnail_MouseClick);
            // 
            // panelAux1Output
            // 
            this.panelAux1Output.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelAux1Output.Controls.Add(this.panelMonitorAux1Output);
            this.panelAux1Output.Controls.Add(this.gtLabel3);
            this.panelAux1Output.Controls.Add(this.eqThumbnailAux1Output);
            this.panelAux1Output.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelAux1Output.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.panelAux1Output.Location = new System.Drawing.Point(391, 20);
            this.panelAux1Output.Name = "panelAux1Output";
            this.panelAux1Output.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelAux1Output.Radius = 18;
            this.panelAux1Output.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.panelAux1Output.ShowRect = true;
            this.panelAux1Output.Size = new System.Drawing.Size(244, 612);
            this.panelAux1Output.TabIndex = 0;
            this.panelAux1Output.title = "辅助输出1";
            this.panelAux1Output.titleHeight = 20;
            // 
            // panelMonitorAux1Output
            // 
            this.panelMonitorAux1Output.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelMonitorAux1Output.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelMonitorAux1Output.Location = new System.Drawing.Point(3, 102);
            this.panelMonitorAux1Output.Name = "panelMonitorAux1Output";
            this.panelMonitorAux1Output.Size = new System.Drawing.Size(238, 502);
            this.panelMonitorAux1Output.TabIndex = 2;
            // 
            // gtLabel3
            // 
            this.gtLabel3.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.gtLabel3.BackColor = System.Drawing.Color.Transparent;
            this.gtLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.gtLabel3.Location = new System.Drawing.Point(79, 28);
            this.gtLabel3.Name = "gtLabel3";
            this.gtLabel3.Size = new System.Drawing.Size(86, 23);
            this.gtLabel3.TabIndex = 1;
            this.gtLabel3.Text = "EQ";
            // 
            // eqThumbnailAux1Output
            // 
            this.eqThumbnailAux1Output.BackColor = System.Drawing.Color.Transparent;
            this.eqThumbnailAux1Output.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.eqThumbnailAux1Output.BorderWidth = 1F;
            this.eqThumbnailAux1Output.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.eqThumbnailAux1Output.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.eqThumbnailAux1Output.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.eqThumbnailAux1Output.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.eqThumbnailAux1Output.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.eqThumbnailAux1Output.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.eqThumbnailAux1Output.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.eqThumbnailAux1Output.isShowBackEQLabel = false;
            this.eqThumbnailAux1Output.Location = new System.Drawing.Point(79, 51);
            this.eqThumbnailAux1Output.Name = "eqThumbnailAux1Output";
            this.eqThumbnailAux1Output.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.eqThumbnailAux1Output.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.eqThumbnailAux1Output.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.eqThumbnailAux1Output.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.eqThumbnailAux1Output.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.eqThumbnailAux1Output.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.eqThumbnailAux1Output.Radius = 12;
            this.eqThumbnailAux1Output.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.eqThumbnailAux1Output.ShowBorder = false;
            this.eqThumbnailAux1Output.ShowText = true;
            this.eqThumbnailAux1Output.Size = new System.Drawing.Size(86, 42);
            this.eqThumbnailAux1Output.TabIndex = 0;
            this.eqThumbnailAux1Output.Text = "eqThumbnail1";
            this.eqThumbnailAux1Output.MouseClick += new System.Windows.Forms.MouseEventHandler(this.eqThumbnail_MouseClick);
            // 
            // panelMainOutput
            // 
            this.panelMainOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelMainOutput.Controls.Add(this.panelMonitorMainOutput);
            this.panelMainOutput.Controls.Add(this.gtLabel2);
            this.panelMainOutput.Controls.Add(this.eqThumbnailMainOutput);
            this.panelMainOutput.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelMainOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.panelMainOutput.Location = new System.Drawing.Point(121, 20);
            this.panelMainOutput.Name = "panelMainOutput";
            this.panelMainOutput.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelMainOutput.Radius = 18;
            this.panelMainOutput.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.panelMainOutput.ShowRect = true;
            this.panelMainOutput.Size = new System.Drawing.Size(264, 612);
            this.panelMainOutput.TabIndex = 0;
            this.panelMainOutput.title = "主输出";
            this.panelMainOutput.titleHeight = 20;
            // 
            // panelMonitorMainOutput
            // 
            this.panelMonitorMainOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelMonitorMainOutput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.panelMonitorMainOutput.Location = new System.Drawing.Point(3, 102);
            this.panelMonitorMainOutput.Name = "panelMonitorMainOutput";
            this.panelMonitorMainOutput.Size = new System.Drawing.Size(258, 502);
            this.panelMonitorMainOutput.TabIndex = 2;
            // 
            // gtLabel2
            // 
            this.gtLabel2.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.gtLabel2.BackColor = System.Drawing.Color.Transparent;
            this.gtLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.gtLabel2.Location = new System.Drawing.Point(89, 28);
            this.gtLabel2.Name = "gtLabel2";
            this.gtLabel2.Size = new System.Drawing.Size(86, 23);
            this.gtLabel2.TabIndex = 1;
            this.gtLabel2.Text = "EQ";
            // 
            // eqThumbnailMainOutput
            // 
            this.eqThumbnailMainOutput.BackColor = System.Drawing.Color.Transparent;
            this.eqThumbnailMainOutput.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.eqThumbnailMainOutput.BorderWidth = 1F;
            this.eqThumbnailMainOutput.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.eqThumbnailMainOutput.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.eqThumbnailMainOutput.DisabledTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.eqThumbnailMainOutput.fillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.eqThumbnailMainOutput.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.eqThumbnailMainOutput.HoverFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(234)))));
            this.eqThumbnailMainOutput.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.eqThumbnailMainOutput.isShowBackEQLabel = false;
            this.eqThumbnailMainOutput.Location = new System.Drawing.Point(89, 51);
            this.eqThumbnailMainOutput.Name = "eqThumbnailMainOutput";
            this.eqThumbnailMainOutput.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.eqThumbnailMainOutput.NormalFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.eqThumbnailMainOutput.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.eqThumbnailMainOutput.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.eqThumbnailMainOutput.PressedFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.eqThumbnailMainOutput.PressedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.eqThumbnailMainOutput.Radius = 12;
            this.eqThumbnailMainOutput.RoundStyle = ((AEC.UI.Utility.RoundStyle)((((AEC.UI.Utility.RoundStyle.TopLeft | AEC.UI.Utility.RoundStyle.TopRight) 
            | AEC.UI.Utility.RoundStyle.BottomRight) 
            | AEC.UI.Utility.RoundStyle.BottomLeft)));
            this.eqThumbnailMainOutput.ShowBorder = false;
            this.eqThumbnailMainOutput.ShowText = true;
            this.eqThumbnailMainOutput.Size = new System.Drawing.Size(86, 42);
            this.eqThumbnailMainOutput.TabIndex = 0;
            this.eqThumbnailMainOutput.Text = "eqThumbnail1";
            this.eqThumbnailMainOutput.MouseClick += new System.Windows.Forms.MouseEventHandler(this.eqThumbnail_MouseClick);
            // 
            // VXMonitorPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelSurOutput);
            this.Controls.Add(this.panelAux2Output);
            this.Controls.Add(this.panelAux1Output);
            this.Controls.Add(this.panelMainOutput);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "VXMonitorPanel";
            this.Size = new System.Drawing.Size(1276, 648);
            this.panelSurOutput.ResumeLayout(false);
            this.panelAux2Output.ResumeLayout(false);
            this.panelAux1Output.ResumeLayout(false);
            this.panelMainOutput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private gtPanel panelMainOutput;
        private gtPanel panelSurOutput;
        private gtPanel panelAux1Output;
        private gtPanel panelAux2Output;
        private AEC.UI.Controls.EQThumbnail eqThumbnailMainOutput;
        private AEC.UI.Controls.EQThumbnail eqThumbnailSurOutput;
        private AEC.UI.Controls.EQThumbnail eqThumbnailAux1Output;
        private AEC.UI.Controls.EQThumbnail eqThumbnailAux2Output;
        private gtLabel gtLabel2;
        private gtLabel gtLabel5;
        private gtLabel gtLabel3;
        private gtLabel gtLabel4;
        private VXMonitorDualChOutputPanel panelMonitorMainOutput;
        private VXMonitorSingleChOutputPanel panelMonitorAux1Output;
        private VXMonitorDualChOutputPanel panelMonitorSurOutput;
        private VXMonitorSingleChOutputPanel panelMonitorAux2Output;
    }
}
