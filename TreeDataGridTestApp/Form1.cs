using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TreeDataGridTestApp
{
    public partial class Form1 : Form
    {
        const int _countRows = 5;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateData(Guid.Empty, 0);
            this.treeDataGrid1.BuildTree();
        }

        private void CreateData(Guid idParent, int level)
        {
            if (level > 4)
                return;

            int count = new Random().Next(1, _countRows);
            for (int i = 0; i < count; i++)
            {
                TreeDataGridTestApp.DataSet1.TestTableRow row = CreateRow(idParent);
                this.dataSet11.TestTable.AddTestTableRow(row);
            }
        }

        private TreeDataGridTestApp.DataSet1.TestTableRow CreateRow(Guid idParent)
        {
            Guid id = Guid.NewGuid();
            string s = id.ToString();
            DateTime dt = new DateTime(new Random().Next(1700, 2010), new Random().Next(1, 12), new Random().Next(1, 28));

            TreeDataGridTestApp.DataSet1.TestTableRow row = dataSet11.TestTable.NewTestTableRow();
            row.id = id;
            row.idParent = idParent;
            row.Name = s;
            row.Date = dt;

            return row;
        }
    }
}