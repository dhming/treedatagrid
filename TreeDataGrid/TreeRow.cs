using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace KDG.Forms.TreeDataGrid
{
    class TreeRow : DataGridViewRow
    {
        bool _expanded = false;
        private TreeRow _parentRow = null;
        private List<TreeRow> _child = null;
        private int _level = 0;

        public TreeRow()
        {
            _child = new List<TreeRow>();
        }
        public TreeRow(DataGridView dgv)
            : this()
        {
            //this.DataGridView = dgv;
            //this.Cells.AddRange(dgv.Cell)
        }

        [Bindable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
        ]
        public List<TreeRow> Child
        {
            get { return _child; }
            set { _child = value; }
        }

        [Bindable(false)
            //, DesignerSerializationVisibility(DesignerSerializationVisibility.Content)
        ]
        public TreeRow ParentRow
        {
            get { return _parentRow; }
            set { _parentRow = value; }
        }
        [Bindable(false)
            //, DesignerSerializationVisibility(DesignerSerializationVisibility.Content)
        ]
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                _expanded = value;

                foreach (TreeRow childRow in _child)
                    childRow.Visible = value;
            }
        }

        internal void Expand()
        {
            foreach (TreeRow tr in this._child)
                _expanded = tr.Visible = true;

        }

        internal void Collupse()
        {
            foreach (TreeRow tr in this._child)
            {
                _expanded = tr.Visible = false;
                tr.Collupse();
            }
        }

        public bool HasChildren
        {
            get { return this._child.Count > 0 ? true : false; }
        }
    }
}
