using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace KDG.Forms.TreeDataGrid
{
    public class TreeColumn : DataGridViewTextBoxColumn
    {
        //-------------------------------------------------------------------------------------------
        // Constructors
        //-------------------------------------------------------------------------------------------
        public TreeColumn()
        {
            this.CellTemplate = new TreeCell();
            this.SortMode = DataGridViewColumnSortMode.NotSortable;
        }
    }
}
