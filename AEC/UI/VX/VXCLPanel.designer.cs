namespace AEC.UI.VX
{
    partial class VXCLPanel
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
            this.lblClipRelease = new AEC.UI.gtLabel();
            this.lblClipThreshold = new AEC.UI.gtLabel();
            this.lblRelease = new AEC.UI.gtLabel();
            this.lblAttack = new AEC.UI.gtLabel();
            this.lblThreshold = new AEC.UI.gtLabel();
            this.lblTtile = new AEC.UI.gtLabel();
            this.paramClipRelease = new AEC.UI.ParamLabel();
            this.paramClipThreshold = new AEC.UI.ParamLabel();
            this.paramRelease = new AEC.UI.ParamLabel();
            this.paramAttack = new AEC.UI.ParamLabel();
            this.paramThreshold = new AEC.UI.ParamLabel();
            this.chkAuto = new AEC.UI.gtCheckBox();
            this.btnSwitchUnit = new AEC.UI.gtButton();
            this.SuspendLayout();
            // 
            // lblClipRelease
            // 
            this.lblClipRelease.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblClipRelease.BackColor = System.Drawing.Color.Transparent;
            this.lblClipRelease.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblClipRelease.Location = new System.Drawing.Point(0, 320);
            this.lblClipRelease.Name = "lblClipRelease";
            this.lblClipRelease.Size = new System.Drawing.Size(108, 23);
            this.lblClipRelease.TabIndex = 0;
            this.lblClipRelease.Text = "削波恢复时间";
            // 
            // lblClipThreshold
            // 
            this.lblClipThreshold.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblClipThreshold.BackColor = System.Drawing.Color.Transparent;
            this.lblClipThreshold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblClipThreshold.Location = new System.Drawing.Point(0, 258);
            this.lblClipThreshold.Name = "lblClipThreshold";
            this.lblClipThreshold.Size = new System.Drawing.Size(108, 23);
            this.lblClipThreshold.TabIndex = 0;
            this.lblClipThreshold.Text = "削波启动电平";
            // 
            // lblRelease
            // 
            this.lblRelease.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblRelease.BackColor = System.Drawing.Color.Transparent;
            this.lblRelease.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblRelease.Location = new System.Drawing.Point(0, 196);
            this.lblRelease.Name = "lblRelease";
            this.lblRelease.Size = new System.Drawing.Size(108, 23);
            this.lblRelease.TabIndex = 0;
            this.lblRelease.Text = "恢复时间";
            // 
            // lblAttack
            // 
            this.lblAttack.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblAttack.BackColor = System.Drawing.Color.Transparent;
            this.lblAttack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblAttack.Location = new System.Drawing.Point(0, 134);
            this.lblAttack.Name = "lblAttack";
            this.lblAttack.Size = new System.Drawing.Size(108, 23);
            this.lblAttack.TabIndex = 0;
            this.lblAttack.Text = "启动时间";
            // 
            // lblThreshold
            // 
            this.lblThreshold.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblThreshold.BackColor = System.Drawing.Color.Transparent;
            this.lblThreshold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblThreshold.Location = new System.Drawing.Point(0, 72);
            this.lblThreshold.Name = "lblThreshold";
            this.lblThreshold.Size = new System.Drawing.Size(108, 23);
            this.lblThreshold.TabIndex = 0;
            this.lblThreshold.Text = "启动电平";
            // 
            // lblTtile
            // 
            this.lblTtile.alignment = AEC.UI.gtLabel.AlignMent.Center;
            this.lblTtile.BackColor = System.Drawing.Color.Transparent;
            this.lblTtile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.lblTtile.Location = new System.Drawing.Point(0, 4);
            this.lblTtile.Name = "lblTtile";
            this.lblTtile.Size = new System.Drawing.Size(108, 23);
            this.lblTtile.TabIndex = 0;
            this.lblTtile.Text = "压限";
            // 
            // paramClipRelease
            // 
            this.paramClipRelease.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.paramClipRelease.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramClipRelease.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramClipRelease.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.paramClipRelease.initV = ((short)(0));
            this.paramClipRelease.isShowUpdownButton = true;
            this.paramClipRelease.Location = new System.Drawing.Point(9, 346);
            this.paramClipRelease.Margin = new System.Windows.Forms.Padding(0);
            this.paramClipRelease.Name = "paramClipRelease";
            this.paramClipRelease.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramClipRelease.Padding = new System.Windows.Forms.Padding(2);
            this.paramClipRelease.Size = new System.Drawing.Size(90, 25);
            this.paramClipRelease.TabIndex = 6;
            this.paramClipRelease.upperLimit = ((short)(0));
            this.paramClipRelease.userGDIPaint = true;
            this.paramClipRelease.v = ((short)(0));
            // 
            // paramClipThreshold
            // 
            this.paramClipThreshold.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.paramClipThreshold.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramClipThreshold.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramClipThreshold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.paramClipThreshold.initV = ((short)(0));
            this.paramClipThreshold.isShowUpdownButton = true;
            this.paramClipThreshold.Location = new System.Drawing.Point(9, 284);
            this.paramClipThreshold.Margin = new System.Windows.Forms.Padding(0);
            this.paramClipThreshold.Name = "paramClipThreshold";
            this.paramClipThreshold.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramClipThreshold.Padding = new System.Windows.Forms.Padding(2);
            this.paramClipThreshold.Size = new System.Drawing.Size(90, 25);
            this.paramClipThreshold.TabIndex = 6;
            this.paramClipThreshold.upperLimit = ((short)(0));
            this.paramClipThreshold.userGDIPaint = true;
            this.paramClipThreshold.v = ((short)(0));
            // 
            // paramRelease
            // 
            this.paramRelease.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.paramRelease.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramRelease.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramRelease.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.paramRelease.initV = ((short)(0));
            this.paramRelease.isShowUpdownButton = true;
            this.paramRelease.Location = new System.Drawing.Point(9, 222);
            this.paramRelease.Margin = new System.Windows.Forms.Padding(0);
            this.paramRelease.Name = "paramRelease";
            this.paramRelease.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramRelease.Padding = new System.Windows.Forms.Padding(2);
            this.paramRelease.Size = new System.Drawing.Size(90, 25);
            this.paramRelease.TabIndex = 6;
            this.paramRelease.upperLimit = ((short)(0));
            this.paramRelease.userGDIPaint = true;
            this.paramRelease.v = ((short)(0));
            // 
            // paramAttack
            // 
            this.paramAttack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.paramAttack.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramAttack.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramAttack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.paramAttack.initV = ((short)(0));
            this.paramAttack.isShowUpdownButton = true;
            this.paramAttack.Location = new System.Drawing.Point(9, 160);
            this.paramAttack.Margin = new System.Windows.Forms.Padding(0);
            this.paramAttack.Name = "paramAttack";
            this.paramAttack.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramAttack.Padding = new System.Windows.Forms.Padding(2);
            this.paramAttack.Size = new System.Drawing.Size(90, 25);
            this.paramAttack.TabIndex = 6;
            this.paramAttack.upperLimit = ((short)(0));
            this.paramAttack.userGDIPaint = true;
            this.paramAttack.v = ((short)(0));
            // 
            // paramThreshold
            // 
            this.paramThreshold.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.paramThreshold.disabledBack = global::AEC.Properties.Resources.param_label_disabled;
            this.paramThreshold.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.paramThreshold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.paramThreshold.initV = ((short)(0));
            this.paramThreshold.isShowUpdownButton = true;
            this.paramThreshold.Location = new System.Drawing.Point(9, 98);
            this.paramThreshold.Margin = new System.Windows.Forms.Padding(0);
            this.paramThreshold.Name = "paramThreshold";
            this.paramThreshold.normalBack = global::AEC.Properties.Resources.param_label_normal;
            this.paramThreshold.Padding = new System.Windows.Forms.Padding(2);
            this.paramThreshold.Size = new System.Drawing.Size(90, 25);
            this.paramThreshold.TabIndex = 6;
            this.paramThreshold.upperLimit = ((short)(0));
            this.paramThreshold.userGDIPaint = true;
            this.paramThreshold.v = ((short)(0));
            // 
            // chkAuto
            // 
            this.chkAuto.BackColor = System.Drawing.Color.Transparent;
            this.chkAuto.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.chkAuto.backHover = global::AEC.Properties.Resources.btn_hover;
            this.chkAuto.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.chkAuto.backPressed = global::AEC.Properties.Resources.auto_btn_pressed;
            this.chkAuto.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.chkAuto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.chkAuto.isAutoCheck = true;
            this.chkAuto.isChecked = false;
            this.chkAuto.isShowCheckedBackWhenDsiabled = false;
            this.chkAuto.isStretch = true;
            this.chkAuto.Location = new System.Drawing.Point(9, 30);
            this.chkAuto.Margin = new System.Windows.Forms.Padding(0);
            this.chkAuto.Name = "chkAuto";
            this.chkAuto.Size = new System.Drawing.Size(90, 30);
            this.chkAuto.TabIndex = 2;
            this.chkAuto.Text = "Auto";
            this.chkAuto.userGDIPaint = true;
            this.chkAuto.CheckedChanged += new System.EventHandler<System.EventArgs>(this.chkAuto_CheckedChanged);
            // 
            // btnSwitchUnit
            // 
            this.btnSwitchUnit.BackColor = System.Drawing.Color.Transparent;
            this.btnSwitchUnit.backDisabled = global::AEC.Properties.Resources.switch_unit_disabled;
            this.btnSwitchUnit.backHover = global::AEC.Properties.Resources.switch_unit_hover;
            this.btnSwitchUnit.backNormal = global::AEC.Properties.Resources.switch_unit_normal;
            this.btnSwitchUnit.backPressed = global::AEC.Properties.Resources.switch_unit_pressed;
            this.btnSwitchUnit.ForeColor = System.Drawing.Color.White;
            this.btnSwitchUnit.isStretch = false;
            this.btnSwitchUnit.Location = new System.Drawing.Point(102, 102);
            this.btnSwitchUnit.Name = "btnSwitchUnit";
            this.btnSwitchUnit.Size = new System.Drawing.Size(18, 18);
            this.btnSwitchUnit.TabIndex = 7;
            this.btnSwitchUnit.userGDIPaint = false;
            this.btnSwitchUnit.Click += new System.EventHandler(this.btnSwitchUnit_Click);
            // 
            // CLPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.Controls.Add(this.btnSwitchUnit);
            this.Controls.Add(this.paramClipRelease);
            this.Controls.Add(this.paramClipThreshold);
            this.Controls.Add(this.paramRelease);
            this.Controls.Add(this.lblClipRelease);
            this.Controls.Add(this.lblClipThreshold);
            this.Controls.Add(this.paramAttack);
            this.Controls.Add(this.lblRelease);
            this.Controls.Add(this.paramThreshold);
            this.Controls.Add(this.lblAttack);
            this.Controls.Add(this.chkAuto);
            this.Controls.Add(this.lblThreshold);
            this.Controls.Add(this.lblTtile);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "CLPanel";
            this.Size = new System.Drawing.Size(128, 390);
            this.ResumeLayout(false);

        }

        #endregion

        private gtLabel lblTtile;
        private gtCheckBox chkAuto;
        private gtLabel lblThreshold;
        private ParamLabel paramThreshold;
        private gtLabel lblAttack;
        private ParamLabel paramAttack;
        private gtLabel lblRelease;
        private ParamLabel paramRelease;
        private gtLabel lblClipThreshold;
        private ParamLabel paramClipThreshold;
        private gtLabel lblClipRelease;
        private ParamLabel paramClipRelease;
        private gtButton btnSwitchUnit;
    }
}
