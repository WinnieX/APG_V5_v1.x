namespace AEC.UI
{
    partial class WorkModePanel
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
            this.panelWorkMode = new System.Windows.Forms.Panel();
            this.rdoAuto = new AEC.UI.gtRadioButton();
            this.rdoManual = new AEC.UI.gtRadioButton();
            this.panelWrokSelect = new System.Windows.Forms.Panel();
            this.rdoDisco = new AEC.UI.gtRadioButton();
            this.rdoSing = new AEC.UI.gtRadioButton();
            this.panelWorkMode.SuspendLayout();
            this.panelWrokSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelWorkMode
            // 
            this.panelWorkMode.BackColor = System.Drawing.Color.Transparent;
            this.panelWorkMode.Controls.Add(this.rdoAuto);
            this.panelWorkMode.Controls.Add(this.rdoManual);
            this.panelWorkMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.panelWorkMode.Location = new System.Drawing.Point(2, 0);
            this.panelWorkMode.Margin = new System.Windows.Forms.Padding(0);
            this.panelWorkMode.Name = "panelWorkMode";
            this.panelWorkMode.Size = new System.Drawing.Size(168, 32);
            this.panelWorkMode.TabIndex = 0;
            // 
            // rdoAuto
            // 
            this.rdoAuto.backChecked = global::AEC.Properties.Resources.btn_pressed;
            this.rdoAuto.BackColor = System.Drawing.Color.Transparent;
            this.rdoAuto.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.rdoAuto.backHover = global::AEC.Properties.Resources.btn_hover;
            this.rdoAuto.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.rdoAuto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.rdoAuto.isAutoCheck = false;
            this.rdoAuto.isChecked = false;
            this.rdoAuto.isCheckInit = false;
            this.rdoAuto.isShowCheckedBackWhenDsiabled = false;
            this.rdoAuto.isStretch = true;
            this.rdoAuto.Location = new System.Drawing.Point(84, 1);
            this.rdoAuto.Margin = new System.Windows.Forms.Padding(0);
            this.rdoAuto.Name = "rdoAuto";
            this.rdoAuto.Size = new System.Drawing.Size(80, 30);
            this.rdoAuto.TabIndex = 0;
            this.rdoAuto.Text = "自动";
            this.rdoAuto.userGDIPaint = true;
            // 
            // rdoManual
            // 
            this.rdoManual.backChecked = global::AEC.Properties.Resources.btn_pressed;
            this.rdoManual.BackColor = System.Drawing.Color.Transparent;
            this.rdoManual.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.rdoManual.backHover = global::AEC.Properties.Resources.btn_hover;
            this.rdoManual.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.rdoManual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.rdoManual.isAutoCheck = false;
            this.rdoManual.isChecked = false;
            this.rdoManual.isCheckInit = false;
            this.rdoManual.isShowCheckedBackWhenDsiabled = false;
            this.rdoManual.isStretch = true;
            this.rdoManual.Location = new System.Drawing.Point(4, 1);
            this.rdoManual.Margin = new System.Windows.Forms.Padding(0);
            this.rdoManual.Name = "rdoManual";
            this.rdoManual.Size = new System.Drawing.Size(80, 30);
            this.rdoManual.TabIndex = 0;
            this.rdoManual.Text = "手动";
            this.rdoManual.userGDIPaint = true;
            // 
            // panelWrokSelect
            // 
            this.panelWrokSelect.BackColor = System.Drawing.Color.Transparent;
            this.panelWrokSelect.Controls.Add(this.rdoDisco);
            this.panelWrokSelect.Controls.Add(this.rdoSing);
            this.panelWrokSelect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.panelWrokSelect.Location = new System.Drawing.Point(170, 0);
            this.panelWrokSelect.Margin = new System.Windows.Forms.Padding(0);
            this.panelWrokSelect.Name = "panelWrokSelect";
            this.panelWrokSelect.Size = new System.Drawing.Size(168, 32);
            this.panelWrokSelect.TabIndex = 0;
            // 
            // rdoDisco
            // 
            this.rdoDisco.backChecked = global::AEC.Properties.Resources.btn_pressed;
            this.rdoDisco.BackColor = System.Drawing.Color.Transparent;
            this.rdoDisco.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.rdoDisco.backHover = global::AEC.Properties.Resources.btn_hover;
            this.rdoDisco.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.rdoDisco.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.rdoDisco.isAutoCheck = false;
            this.rdoDisco.isChecked = false;
            this.rdoDisco.isCheckInit = false;
            this.rdoDisco.isShowCheckedBackWhenDsiabled = false;
            this.rdoDisco.isStretch = true;
            this.rdoDisco.Location = new System.Drawing.Point(84, 1);
            this.rdoDisco.Margin = new System.Windows.Forms.Padding(0);
            this.rdoDisco.Name = "rdoDisco";
            this.rdoDisco.Size = new System.Drawing.Size(80, 30);
            this.rdoDisco.TabIndex = 0;
            this.rdoDisco.Text = "热舞";
            this.rdoDisco.userGDIPaint = true;
            // 
            // rdoSing
            // 
            this.rdoSing.backChecked = global::AEC.Properties.Resources.btn_pressed;
            this.rdoSing.BackColor = System.Drawing.Color.Transparent;
            this.rdoSing.backDisabled = global::AEC.Properties.Resources.btn_disabled;
            this.rdoSing.backHover = global::AEC.Properties.Resources.btn_hover;
            this.rdoSing.backNormal = global::AEC.Properties.Resources.btn_normal;
            this.rdoSing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.rdoSing.isAutoCheck = false;
            this.rdoSing.isChecked = false;
            this.rdoSing.isCheckInit = false;
            this.rdoSing.isShowCheckedBackWhenDsiabled = false;
            this.rdoSing.isStretch = true;
            this.rdoSing.Location = new System.Drawing.Point(4, 1);
            this.rdoSing.Margin = new System.Windows.Forms.Padding(0);
            this.rdoSing.Name = "rdoSing";
            this.rdoSing.Size = new System.Drawing.Size(80, 30);
            this.rdoSing.TabIndex = 0;
            this.rdoSing.Text = "唱歌";
            this.rdoSing.userGDIPaint = true;
            // 
            // WorkModePanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelWrokSelect);
            this.Controls.Add(this.panelWorkMode);
            this.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(64)))));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "WorkModePanel";
            this.Size = new System.Drawing.Size(336, 32);
            this.panelWorkMode.ResumeLayout(false);
            this.panelWrokSelect.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelWorkMode;
        private gtRadioButton rdoManual;
        private gtRadioButton rdoAuto;
        private System.Windows.Forms.Panel panelWrokSelect;
        private gtRadioButton rdoDisco;
        private gtRadioButton rdoSing;
    }
}
