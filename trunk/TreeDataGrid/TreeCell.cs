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
        Brush br = null;

        public TreeCell()
            : base()
        {
            br = new SolidBrush(Color.Gray);
        }

        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            int markerWidth = INDENT_WIDTH * (Level + 1);
            Rectangle newCellBounds = new Rectangle(cellBounds.X + markerWidth, cellBounds.Y, 
                cellBounds.Width - markerWidth, cellBounds.Height);
            base.Paint(graphics, clipBounds, newCellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            Rectangle offset = new Rectangle(cellBounds.X, cellBounds.Y, markerWidth, cellBounds.Height);
            graphics.FillRectangle(br, offset);
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
