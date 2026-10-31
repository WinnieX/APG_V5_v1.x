namespace AEC.UI
{
    partial class DataBackupMgtPanel
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiLoadData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenFileLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.vScrollbar = new AEC.UI.VScrollbar();
            this.dgvFile = new AEC.UI.gtDataGridView();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new AEC.UI.gtButton();
            this.btnLoad = new AEC.UI.gtButton();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.lblBackupFileName = new AEC.UI.gtLabel();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFile)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLoadData,
            this.tsmiOpenFileLocation,
            this.tsmiDelete});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(149, 70);
            // 
            // tsmiLoadData
            // 
            this.tsmiLoadData.Name = "tsmiLoadData";
            this.tsmiLoadData.Size = new System.Drawing.Size(148, 22);
            this.tsmiLoadData.Text = "加载数据";
            this.tsmiLoadData.Click += new System.EventHandler(this.tsmi_Click);
            // 
            // tsmiOpenFileLocation
            // 
            this.tsmiOpenFileLocation.Name = "tsmiOpenFileLocation";
            this.tsmiOpenFileLocation.Size = new System.Drawing.Size(148, 22);
            this.tsmiOpenFileLocation.Text = "打开所在目录";
            this.tsmiOpenFileLocation.Click += new System.EventHandler(this.tsmi_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(148, 22);
            this.tsmiDelete.Text = "删除";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmi_Click);
            // 
            // vScrollbar
            // 
            this.vScrollbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.vScrollbar.BtnHeight = 18;
            this.vScrollbar.ConerRadius = 2;
            this.vScrollbar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.vScrollbar.IsRadius = true;
            this.vScrollbar.IsShowRect = false;
            this.vScrollbar.LargeChange = 184;
            this.vScrollbar.Location = new System.Drawing.Point(677, 39);
            this.vScrollbar.Maximum = 276;
            this.vScrollbar.Minimum = 0;
            this.vScrollbar.Name = "vScrollbar";
            this.vScrollbar.RectColor = System.Drawing.Color.Silver;
            this.vScrollbar.RectWidth = 1;
            this.vScrollbar.Size = new System.Drawing.Size(19, 207);
            this.vScrollbar.SmallChange = 23;
            this.vScrollbar.TabIndex = 31;
            this.vScrollbar.Text = "vScrollbar";
            this.vScrollbar.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(177)))), ((int)(((byte)(178)))));
            this.vScrollbar.Value = 0;
            this.vScrollbar.wheelStep = 23;
            // 
            // dgvFile
            // 
            this.dgvFile.AllowDrop = true;
            this.dgvFile.AllowUserToAddRows = false;
            this.dgvFile.AllowUserToDeleteRows = false;
            this.dgvFile.AllowUserToResizeColumns = false;
            this.dgvFile.AllowUserToResizeRows = false;
            this.dgvFile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFile.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(109)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFile.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.FileName});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFile.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFile.EnableHeadersVisualStyles = false;
            this.dgvFile.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.dgvFile.Location = new System.Drawing.Point(27, 38);
            this.dgvFile.MultiSelect = false;
            this.dgvFile.Name = "dgvFile";
            this.dgvFile.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFile.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvFile.RowHeadersVisible = false;
            this.dgvFile.RowTemplate.Height = 23;
            this.dgvFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvFile.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFile.Size = new System.Drawing.Size(669, 209);
            this.dgvFile.TabIndex = 6;
            this.dgvFile.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFile_CellMouseDoubleClick);
            this.dgvFile.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFile_CellMouseDown);
            this.dgvFile.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFile_CellMouseMove);
            this.dgvFile.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFile_CellMouseUp);
            this.dgvFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgvFile_DragDrop);
            this.dgvFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgvFile_DragEnter);
            this.dgvFile.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.dgvFile_QueryContinueDrag);
            this.dgvFile.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvFile_MouseClick);
            // 
            // Number
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Number.DefaultCellStyle = dataGridViewCellStyle2;
            this.Number.FillWeight = 1F;
            this.Number.HeaderText = "序号";
            this.Number.Name = "Number";
            this.Number.ReadOnly = true;
            this.Number.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FileName
            // 
            this.FileName.FillWeight = 5F;
            this.FileName.HeaderText = "数据名称";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.backDisabled = global::AEC.Properties.Resources.save_data_to_file_disabled;
            this.btnSave.backHover = global::AEC.Properties.Resources.save_data_to_file_hover;
            this.btnSave.backNormal = global::AEC.Properties.Resources.save_data_to_file_normal;
            this.btnSave.backPressed = global::AEC.Properties.Resources.save_data_to_file_pressed;
            this.btnSave.Enabled = false;
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSave.isStretch = true;
            this.btnSave.Location = new System.Drawing.Point(387, 262);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(148, 32);
            this.btnSave.TabIndex = 29;
            this.btnSave.Text = "保存数据到本地文件";
            this.btnSave.userGDIPaint = true;
            this.btnSave.Click += new System.EventHandler(this.button_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.Color.Transparent;
            this.btnLoad.backDisabled = global::AEC.Properties.Resources.load_data_to_device_disabled;
            this.btnLoad.backHover = global::AEC.Properties.Resources.load_data_to_device_hover;
            this.btnLoad.backNormal = global::AEC.Properties.Resources.load_data_to_device_normal;
            this.btnLoad.backPressed = global::AEC.Properties.Resources.load_data_to_device_pressed;
            this.btnLoad.Enabled = false;
            this.btnLoad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLoad.isStretch = true;
            this.btnLoad.Location = new System.Drawing.Point(550, 262);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(0);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(148, 32);
            this.btnLoad.TabIndex = 30;
            this.btnLoad.Text = "从本地文件加载数据";
            this.btnLoad.userGDIPaint = true;
            this.btnLoad.Click += new System.EventHandler(this.button_Click);
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.textBoxFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxFileName.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.textBoxFileName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBoxFileName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBoxFileName.Location = new System.Drawing.Point(156, 265);
            this.textBoxFileName.MaxLength = 256;
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(216, 27);
            this.textBoxFileName.TabIndex = 28;
            this.textBoxFileName.TextChanged += new System.EventHandler(this.textBoxFileName_TextChanged);
            this.textBoxFileName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxFileName_KeyDown);
            // 
            // lblBackupFileName
            // 
            this.lblBackupFileName.alignment = AEC.UI.gtLabel.AlignMent.Right;
            this.lblBackupFileName.BackColor = System.Drawing.Color.Transparent;
            this.lblBackupFileName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblBackupFileName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblBackupFileName.Location = new System.Drawing.Point(29, 265);
            this.lblBackupFileName.Name = "lblBackupFileName";
            this.lblBackupFileName.Size = new System.Drawing.Size(121, 24);
            this.lblBackupFileName.TabIndex = 7;
            this.lblBackupFileName.Text = "备份文件名";
            // 
            // DataBackupMgtPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.Controls.Add(this.vScrollbar);
            this.Controls.Add(this.dgvFile);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.textBoxFileName);
            this.Controls.Add(this.lblBackupFileName);
            this.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(236)))));
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DataBackupMgtPanel";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(190)))));
            this.Size = new System.Drawing.Size(724, 308);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private gtDataGridView dgvFile;
        private gtLabel lblBackupFileName;
        private System.Windows.Forms.TextBox textBoxFileName;
        private gtButton btnSave;
        private gtButton btnLoad;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadData;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFileLocation;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private VScrollbar vScrollbar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
    }
}
