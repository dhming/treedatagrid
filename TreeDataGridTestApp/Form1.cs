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
            //CreateData(Guid.Empty, 0);
            


            Guid g1 = new Guid("00000000-0000-0000-0000-000000000001");
            Guid g2 = new Guid("00000000-0000-0000-0000-000000000002");
            Guid g3 = new Guid("00000000-0000-0000-0000-000000000003");
            Guid g4 = new Guid("00000000-0000-0000-0000-000000000004");
            Guid g5 = new Guid("00000000-0000-0000-0000-000000000005");

            this.dataSet11.TestTable.AddTestTableRow(g1, Guid.Empty, "111111111", new DateTime(2001, 1, 1));
            this.dataSet11.TestTable.AddTestTableRow(g3, g1, "33333333", new DateTime(2003, 1, 1));
            this.dataSet11.TestTable.AddTestTableRow(g4, g3, "44", new DateTime(2004, 1, 1));
            this.dataSet11.TestTable.AddTestTableRow(g2, g1, "22222", new DateTime(2002, 1, 1));
            
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