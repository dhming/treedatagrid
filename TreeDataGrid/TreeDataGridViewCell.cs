using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace KDG.Forms
{
    public class TreeDataGridViewCell : DataGridViewTextBoxCell
    {
        private const int INDENT_WIDTH = 20;
        private const int INDENT_MARGIN = 5;

        //-------------------------------------------------------------------------------------------
        // Constructors
        //-------------------------------------------------------------------------------------------
        public TreeDataGridViewCell() : base() { }

        //-------------------------------------------------------------------------------------------
        // Overriders
        //-------------------------------------------------------------------------------------------
        protected override void Paint(System.Drawing.Graphics graphics, 
            System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, 
            int rowIndex, DataGridViewElementStates cellState, object value, 
            object formattedValue, string errorText, DataGridViewCellStyle cellStyle, 
            DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            int markerWidth = INDENT_WIDTH * (Level);
            int cellBoundsWidth = INDENT_WIDTH * (Level + 1);

            Rectangle newCellBounds = new Rectangle(cellBounds.X + cellBoundsWidth, cellBounds.Y,
                cellBounds.Width - cellBoundsWidth, cellBounds.Height);

            DataGridViewAdvancedBorderStyle abs = new DataGridViewAdvancedBorderStyle();
            abs.Bottom = advancedBorderStyle.Bottom;

            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, DataGridViewPaintParts.Background | DataGridViewPaintParts.SelectionBackground);
            base.Paint(graphics, clipBounds, newCellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, abs, paintParts);
            try
            {
                Rectangle offset = new Rectangle(cellBounds.X, cellBounds.Y, cellBoundsWidth, cellBounds.Height);
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, abs, DataGridViewPaintParts.Border);

                Image im = Properties.TreeDataGridResource.bHasNoChild;
                Rectangle imageRect = new Rectangle(cellBounds.X + markerWidth, cellBounds.Y, im.Width, im.Height);

                Point p = new Point(cellBounds.X + markerWidth + (INDENT_WIDTH - im.Width) / 2, (cellBounds.Height - im.Height) / 2 + cellBounds.Y);

                if (!(this.OwningRow as TreeDataGridViewRow).HasChildren)
                    graphics.DrawImage(Properties.TreeDataGridResource.bHasNoChild, p);
                else
                {
                    if ((this.OwningRow as TreeDataGridViewRow).Expanded)
                        graphics.DrawImage(Properties.TreeDataGridResource.bExpanded, p);
                    else
                        graphics.DrawImage(Properties.TreeDataGridResource.bCollupsed, p);
                }
            }
            catch (Exception)
            {
            }
        }
        protected override void OnMouseDoubleClick(DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //  if (offset.Contains(e.Location))
                TreeDataGridViewRow tr = this.OwningRow as TreeDataGridViewRow;
                if (tr.Expanded)
                    tr.Collupse();
                else
                    tr.Expand();
            }
            else
                base.OnMouseDoubleClick(e);
        }

        //-------------------------------------------------------------------------------------------
        // Properties
        //-------------------------------------------------------------------------------------------
        public int Level
        {
            get
            {
                if (this.OwningRow is TreeDataGridViewRow)
                    return (this.OwningRow as TreeDataGridViewRow).Level;

                return 0;
            }
        }

    }
}
