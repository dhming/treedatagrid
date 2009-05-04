using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace KDG.Forms
{
    public class TreeDataGridViewColumn : DataGridViewTextBoxColumn
    {
        //-------------------------------------------------------------------------------------------
        // Constructors
        //-------------------------------------------------------------------------------------------
        public TreeDataGridViewColumn()
        {
            this.CellTemplate = new TreeDataGridViewCell();
            this.SortMode = DataGridViewColumnSortMode.NotSortable;
        }
    }
}
