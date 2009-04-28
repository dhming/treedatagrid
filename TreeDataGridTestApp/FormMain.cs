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
            this.dataSet11.TestTable.AddTestTableRow(Guid.NewGuid(), "111111111", new DateTime(2001, 1, 1));
            this.dataSet11.TestTable.AddTestTableRow(Guid.NewGuid(), "22222", new DateTime(2002, 1, 1));
            this.dataSet11.TestTable.AddTestTableRow(Guid.NewGuid(), "33333333", new DateTime(2003, 1, 1));
            this.dataSet11.TestTable.AddTestTableRow(Guid.NewGuid(), "44", new DateTime(2004, 1, 1));
        }
    }
}