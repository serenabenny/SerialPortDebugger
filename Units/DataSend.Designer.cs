﻿namespace LeafSoft.Units
{
    partial class DataSend
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgCMD = new System.Windows.Forms.DataGridView();
            this.IsAutoSend = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CMDText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSend = new System.Windows.Forms.DataGridViewButtonColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MS_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.MS_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.MS_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAutoSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nmDelay = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.ckbAutoSend = new System.Windows.Forms.CheckBox();
            this.numTimes = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgCMD)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimes)).BeginInit();
            this.SuspendLayout();
            // 
            // dgCMD
            // 
            this.dgCMD.AllowUserToAddRows = false;
            this.dgCMD.AllowUserToDeleteRows = false;
            this.dgCMD.AllowUserToResizeRows = false;
            this.dgCMD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgCMD.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgCMD.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgCMD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCMD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsAutoSend,
            this.CType,
            this.CMDText,
            this.btnSend});
            this.dgCMD.ContextMenuStrip = this.contextMenuStrip1;
            this.dgCMD.Location = new System.Drawing.Point(0, 0);
            this.dgCMD.MultiSelect = false;
            this.dgCMD.Name = "dgCMD";
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgCMD.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgCMD.RowHeadersVisible = false;
            this.dgCMD.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgCMD.RowTemplate.Height = 23;
            this.dgCMD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCMD.Size = new System.Drawing.Size(564, 151);
            this.dgCMD.TabIndex = 0;
            this.dgCMD.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCMD_CellContentClick);
            this.dgCMD.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgCMD_RowsAdded);
            // 
            // IsAutoSend
            // 
            this.IsAutoSend.HeaderText = "参与循环";
            this.IsAutoSend.Name = "IsAutoSend";
            this.IsAutoSend.Width = 60;
            // 
            // CType
            // 
            this.CType.DataPropertyName = "ContentTypeName";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CType.DefaultCellStyle = dataGridViewCellStyle2;
            this.CType.HeaderText = "类型";
            this.CType.Name = "CType";
            this.CType.ReadOnly = true;
            this.CType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CType.Width = 60;
            // 
            // CMDText
            // 
            this.CMDText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CMDText.DataPropertyName = "Text";
            this.CMDText.HeaderText = "命令";
            this.CMDText.Name = "CMDText";
            this.CMDText.ReadOnly = true;
            // 
            // btnSend
            // 
            this.btnSend.DataPropertyName = "ButtonText";
            this.btnSend.HeaderText = "发送";
            this.btnSend.Name = "btnSend";
            this.btnSend.ReadOnly = true;
            this.btnSend.Text = "发送";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MS_Add,
            this.MS_Edit,
            this.MS_Delete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 70);
            // 
            // MS_Add
            // 
            this.MS_Add.Name = "MS_Add";
            this.MS_Add.Size = new System.Drawing.Size(100, 22);
            this.MS_Add.Text = "添加";
            this.MS_Add.Click += new System.EventHandler(this.MS_Add_Click);
            // 
            // MS_Edit
            // 
            this.MS_Edit.Name = "MS_Edit";
            this.MS_Edit.Size = new System.Drawing.Size(100, 22);
            this.MS_Edit.Text = "编辑";
            this.MS_Edit.Click += new System.EventHandler(this.MS_Edit_Click);
            // 
            // MS_Delete
            // 
            this.MS_Delete.Name = "MS_Delete";
            this.MS_Delete.Size = new System.Drawing.Size(100, 22);
            this.MS_Delete.Text = "删除";
            this.MS_Delete.Click += new System.EventHandler(this.MS_Delete_Click);
            // 
            // btnAutoSend
            // 
            this.btnAutoSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAutoSend.Location = new System.Drawing.Point(0, 153);
            this.btnAutoSend.Name = "btnAutoSend";
            this.btnAutoSend.Size = new System.Drawing.Size(91, 33);
            this.btnAutoSend.TabIndex = 1;
            this.btnAutoSend.Text = "循环发送";
            this.btnAutoSend.UseVisualStyleBackColor = true;
            this.btnAutoSend.Click += new System.EventHandler(this.btnAutoSend_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "发送间隔：";
            // 
            // nmDelay
            // 
            this.nmDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nmDelay.Location = new System.Drawing.Point(156, 158);
            this.nmDelay.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nmDelay.Name = "nmDelay";
            this.nmDelay.Size = new System.Drawing.Size(70, 21);
            this.nmDelay.TabIndex = 3;
            this.nmDelay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "毫秒";
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(338, 161);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(11, 12);
            this.lblCount.TabIndex = 8;
            this.lblCount.TabStop = true;
            this.lblCount.Text = "0";
            this.lblCount.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblCount_LinkClicked);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(265, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "发送字节数：";
            // 
            // ckbAutoSend
            // 
            this.ckbAutoSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ckbAutoSend.AutoSize = true;
            this.ckbAutoSend.Location = new System.Drawing.Point(488, 162);
            this.ckbAutoSend.Name = "ckbAutoSend";
            this.ckbAutoSend.Size = new System.Drawing.Size(72, 16);
            this.ckbAutoSend.TabIndex = 9;
            this.ckbAutoSend.Text = "自动回发";
            this.ckbAutoSend.UseVisualStyleBackColor = true;
            // 
            // numTimes
            // 
            this.numTimes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numTimes.Location = new System.Drawing.Point(436, 159);
            this.numTimes.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numTimes.Name = "numTimes";
            this.numTimes.Size = new System.Drawing.Size(46, 21);
            this.numTimes.TabIndex = 11;
            this.numTimes.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(365, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "发送次数：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DataSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numTimes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ckbAutoSend);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nmDelay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAutoSend);
            this.Controls.Add(this.dgCMD);
            this.Name = "DataSend";
            this.Size = new System.Drawing.Size(564, 188);
            ((System.ComponentModel.ISupportInitialize)(this.dgCMD)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgCMD;
        private System.Windows.Forms.Button btnAutoSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmDelay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MS_Add;
        private System.Windows.Forms.ToolStripMenuItem MS_Edit;
        private System.Windows.Forms.ToolStripMenuItem MS_Delete;
        private System.Windows.Forms.LinkLabel lblCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsAutoSend;
        private System.Windows.Forms.DataGridViewTextBoxColumn CType;
        private System.Windows.Forms.DataGridViewTextBoxColumn CMDText;
        private System.Windows.Forms.DataGridViewButtonColumn btnSend;
        private System.Windows.Forms.CheckBox ckbAutoSend;
        private System.Windows.Forms.NumericUpDown numTimes;
        private System.Windows.Forms.Label label3;
    }
}
