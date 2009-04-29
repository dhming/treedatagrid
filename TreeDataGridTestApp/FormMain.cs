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
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Guid g1 = new Guid("00000000-0000-0000-0000-000000000001");
            Guid g2 = new Guid("00000000-0000-0000-0000-000000000002");
            Guid g3 = new Guid("00000000-0000-0000-0000-000000000003");
            Guid g4 = new Guid("00000000-0000-0000-0000-000000000004");
            Guid g5 = new Guid("00000000-0000-0000-0000-000000000005");

            this.dataSet11.TestTable.AddTestTableRow(g1, Guid.Empty, "111111111", new DateTime(2001, 1, 1));
            this.dataSet11.TestTable.AddTestTableRow(g2, g1, "22222", new DateTime(2002, 1, 1));
            this.dataSet11.TestTable.AddTestTableRow(g3, g1, "33333333", new DateTime(2003, 1, 1));
            this.dataSet11.TestTable.AddTestTableRow(g4, g3, "44", new DateTime(2004, 1, 1));

            object obj = this.dataGridView1.BindingContext;
        }

        private void testTableBindingSource_BindingComplete(object sender, BindingCompleteEventArgs e)
        {

        }
    }
}