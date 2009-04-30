using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace KDG.Forms.TreeDataGrid
{
    public class TreeColumn : DataGridViewTextBoxColumn
    {
        public TreeColumn()
        {
            this.CellTemplate = new TreeCell();

            this.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        //public override DataGridViewCell CellTemplate
        //{
        //    get
        //    {
        //        return base.CellTemplate;
        //    }
        //    set
        //    {
        //        // Ensure that the cell used for the template is a CalendarCell.
        //        if (value != null &&
        //            !value.GetType().IsAssignableFrom(typeof(TreeCell)))
        //        {
        //            throw new InvalidCastException("Must be a CalendarCell");
        //        }
        //        base.CellTemplate = value;
        //    }
        //}
    }
}
