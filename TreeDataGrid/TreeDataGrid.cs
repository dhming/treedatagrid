using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace KDG.Forms.TreeDataGrid
{
    
    public partial class TreeDataGrid : DataGridView
    {
        public TreeDataGrid()
        {
            InitializeComponent();
            this.RowTemplate = new TreeRow() as DataGridViewRow;

            this.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(TreeDataGrid_DataBindingComplete);
        }

        void TreeDataGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded ||
                e.ListChangedType == ListChangedType.ItemDeleted)
            {
                
            }
        }
    }
}
