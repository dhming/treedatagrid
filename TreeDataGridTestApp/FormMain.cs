using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TreeDataGridTestApp
{
    public partial class FormMain : Form
    {
        int expandIndexRow = -1;
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'testDBDataSet1.TestTable' table. You can move, or remove it, as needed.
            LoadData();
        }

        private void LoadData()
        {
            this.testTableTableAdapter.Fill(this.testDBDataSet1.TestTable);

            treeDataGrid1.BuildTree();
            treeDataGrid1.WaitForLoadData = false;
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            expandIndexRow = -1;
            FormNewMessage fnm = new FormNewMessage();
            if (fnm.ShowDialog() == DialogResult.OK)
            {
                //this.testTableTableAdapter.Insert(
                //    Guid.NewGuid(),
                //    CurrentRow.id,
                //    fnm.Message.Message,
                //    fnm.Message.DateTime);

                this.testDBDataSet1.TestTable.AddTestTableRow(
                    Guid.NewGuid(),
                    CurrentRow.id,
                    fnm.Message.Message,
                    fnm.Message.DateTime);

                this.testTableTableAdapter.Update(this.testDBDataSet1.TestTable);

            }
        }

        public TestDBDataSet.TestTableRow CurrentRow
        {
            get { return (bsTestDB.Current as DataRowView).Row as TestDBDataSet.TestTableRow; }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            FormNewMessage fnm = new FormNewMessage();
            if (fnm.ShowDialog() == DialogResult.OK)
            {
                //this.testTableTableAdapter.Insert(
                //    Guid.NewGuid(),
                //    Guid.Empty,
                //    fnm.Message.Message,
                //    fnm.Message.DateTime);

                this.testDBDataSet1.TestTable.AddTestTableRow(
                    Guid.NewGuid(),
                    Guid.Empty,
                    fnm.Message.Message,
                    fnm.Message.DateTime);

                this.testTableTableAdapter.Update(this.testDBDataSet1.TestTable);
            }
        }

        private void treeDataGrid1_SetupedRows(object sender, EventArgs e)
        {
            treeDataGrid1.ExpandAll();
        }
    }
}