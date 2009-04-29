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
        Brush br = new SolidBrush(Color.Gray);
        Pen pen = new Pen(Color.Black);

        public TreeCell()
            : base()
        {

        }

        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            int markerWidth = INDENT_WIDTH * (Level + 1);
            Rectangle newCellBounds = new Rectangle(cellBounds.X + markerWidth, cellBounds.Y, 
                cellBounds.Width - markerWidth, cellBounds.Height);
            base.Paint(graphics, clipBounds, newCellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            {
                Rectangle offset = new Rectangle(cellBounds.X , cellBounds.Y , markerWidth, cellBounds.Height );
                graphics.FillRectangle(br, offset);
                graphics.DrawRectangle(pen, offset);
            }
        }

        protected override void OnMouseDoubleClick(DataGridViewCellMouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            int markerWidth = INDENT_WIDTH * (Level + 1);
            //Rectangle offset = new Rectangle(cellBounds.X, cellBounds.Y, markerWidth, cellBounds.Height);

            if (e.Button == MouseButtons.Left)
            {
              //  if (offset.Contains(e.Location))
                TreeRow tr=this.OwningRow as TreeRow;
                if (tr.Expanded)
                    tr.Collupse();
                else
                    tr.Expand();
            }

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
