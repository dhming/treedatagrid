using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace KDG.Forms.TreeDataGrid
{
    public class TreeCell : DataGridViewTextBoxCell
    {
        private const int INDENT_WIDTH = 20;
        private const int INDENT_MARGIN = 5;
        Brush br = new SolidBrush(Color.LightGray);
        Pen pen = new Pen(Color.Black);

        public TreeCell()
            : base()
        {

        }

        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            int markerWidth = INDENT_WIDTH * (Level);
            int cellBoundsWidth = INDENT_WIDTH * (Level + 1);

            Rectangle newCellBounds = new Rectangle(cellBounds.X + cellBoundsWidth, cellBounds.Y,
                cellBounds.Width - cellBoundsWidth, cellBounds.Height);

            base.Paint(graphics, clipBounds, newCellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            {
                Rectangle offset = new Rectangle(cellBounds.X, cellBounds.Y, cellBoundsWidth, cellBounds.Height);
                graphics.FillRectangle(br, offset);
                graphics.DrawRectangle(pen, offset);

                Rectangle imageRect = new Rectangle(cellBounds.X + markerWidth, cellBounds.Y, INDENT_WIDTH, cellBounds.Height);
                if (!(this.OwningRow as TreeRow).HasChildren)
                    graphics.DrawImage(Properties.TreeDataGridResource.bHasNoChild, imageRect);
                else
                {
                    if ((this.OwningRow as TreeRow).Expanded)
                        graphics.DrawImage(Properties.TreeDataGridResource.bExpanded, imageRect);
                    else
                        graphics.DrawImage(Properties.TreeDataGridResource.bCollupsed, imageRect);
                }
            }
        }

        protected override void OnMouseDoubleClick(DataGridViewCellMouseEventArgs e)
        {


            int markerWidth = INDENT_WIDTH * (Level + 1);
            //Rectangle offset = new Rectangle(cellBounds.X, cellBounds.Y, markerWidth, cellBounds.Height);

            if (e.Button == MouseButtons.Left)
            {
                //  if (offset.Contains(e.Location))
                TreeRow tr = this.OwningRow as TreeRow;
                if (tr.Expanded)
                    tr.Collupse();
                else
                    tr.Expand();
            }
            else
                base.OnMouseDoubleClick(e);

        }

        public int Level
        {
            get
            {
                if (this.OwningRow is TreeRow)
                    return (this.OwningRow as TreeRow).Level;

                return 0;
            }
        }

    }
}
