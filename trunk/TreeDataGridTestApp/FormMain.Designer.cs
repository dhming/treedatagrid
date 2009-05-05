namespace TreeDataGridTestApp
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            this.testDBDataSet1 = new TreeDataGridTestApp.TestDBDataSet();
            this.bsTestDB = new System.Windows.Forms.BindingSource(this.components);
            this.testTableTableAdapter = new TreeDataGridTestApp.TestDBDataSetTableAdapters.TestTableTableAdapter();
            this.treeDataGrid1 = new KDG.Forms.TreeDataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIdParent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMessages = new KDG.Forms.TreeDataGridViewColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.csmRow = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.testDBDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTestDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeDataGrid1)).BeginInit();
            this.csmRow.SuspendLayout();
            this.SuspendLayout();
            // 
            // testDBDataSet1
            // 
            this.testDBDataSet1.DataSetName = "TestDBDataSet";
            this.testDBDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bsTestDB
            // 
            this.bsTestDB.DataMember = "TestTable";
            this.bsTestDB.DataSource = this.testDBDataSet1;
            // 
            // testTableTableAdapter
            // 
            this.testTableTableAdapter.ClearBeforeFill = true;
            // 
            // treeDataGrid1
            // 
            this.treeDataGrid1.AllowUserToAddRows = false;
            this.treeDataGrid1.AllowUserToDeleteRows = false;
            this.treeDataGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeDataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.treeDataGrid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colIdParent,
            this.colMessages,
            this.colDate});
            this.treeDataGrid1.GridDataMember = "TestTable";
            this.treeDataGrid1.GridDataSource = this.bsTestDB;
            this.treeDataGrid1.Key = "Id";
            this.treeDataGrid1.Location = new System.Drawing.Point(12, 12);
            this.treeDataGrid1.Name = "treeDataGrid1";
            this.treeDataGrid1.ParentKey = "IdParent";
            this.treeDataGrid1.RowHeadersVisible = false;
            this.treeDataGrid1.RowTemplate.ContextMenuStrip = this.csmRow;
            this.treeDataGrid1.Size = new System.Drawing.Size(597, 230);
            this.treeDataGrid1.TabIndex = 0;
            this.treeDataGrid1.WaitForLoadData = true;
            this.treeDataGrid1.SetupedRows += new System.EventHandler(this.treeDataGrid1_SetupedRows);
            // 
            // colId
            // 
            this.colId.HeaderText = "Id";
            this.colId.Name = "colId";
            this.colId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colId.Visible = false;
            // 
            // colIdParent
            // 
            this.colIdParent.HeaderText = "colIdParent";
            this.colIdParent.Name = "colIdParent";
            this.colIdParent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colIdParent.Visible = false;
            // 
            // colMessages
            // 
            this.colMessages.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMessages.HeaderText = "Message";
            this.colMessages.Name = "colMessages";
            this.colMessages.ReadOnly = true;
            this.colMessages.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colDate
            // 
            this.colDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDate.FillWeight = 20F;
            this.colDate.HeaderText = "Date";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            this.colDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // csmRow
            // 
            this.csmRow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem});
            this.csmRow.Name = "csmRow";
            this.csmRow.Size = new System.Drawing.Size(117, 26);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.addToolStripMenuItem.Text = "Add...";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdd.Location = new System.Drawing.Point(534, 363);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(75, 23);
            this.btAdd.TabIndex = 2;
            this.btAdd.Text = "Add";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 398);
            this.Controls.Add(this.treeDataGrid1);
            this.Controls.Add(this.btAdd);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.testDBDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTestDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeDataGrid1)).EndInit();
            this.csmRow.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TestDBDataSet testDBDataSet1;
        private System.Windows.Forms.BindingSource bsTestDB;
        private TreeDataGridTestApp.TestDBDataSetTableAdapters.TestTableTableAdapter testTableTableAdapter;
        private KDG.Forms.TreeDataGridView treeDataGrid1;
        private System.Windows.Forms.ContextMenuStrip csmRow;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdParent;
        private KDG.Forms.TreeDataGridViewColumn colMessages;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
    }
}