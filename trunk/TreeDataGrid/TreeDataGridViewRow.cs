using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace KDG.Forms
{
    class TreeDataGridViewRow : DataGridViewRow
    {
        bool _expanded = false;
        private TreeDataGridViewRow _parentRow = null;
        private List<TreeDataGridViewRow> _child = null;
        private int _level = 0;
        private object _dataBountItem;

        //-------------------------------------------------------------------------------------------
        // Constructors
        //-------------------------------------------------------------------------------------------
        public TreeDataGridViewRow()
        {
            _child = new List<TreeDataGridViewRow>();
        }

        //-------------------------------------------------------------------------------------------
        // Methods
        //-------------------------------------------------------------------------------------------
        internal void Expand()
        {
            foreach (TreeDataGridViewRow tr in this._child)
                _expanded = tr.Visible = true;

        }
        internal void Collupse()
        {
            foreach (TreeDataGridViewRow tr in this._child)
            {
                _expanded = tr.Visible = false;
                tr.Collupse();
            }
        }

        //-------------------------------------------------------------------------------------------
        // Properties
        //-------------------------------------------------------------------------------------------
        [Browsable(false),
        Bindable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        EditorBrowsable(EditorBrowsableState.Never)]
        public List<TreeDataGridViewRow> Child
        {
            get { return _child; }
            set { _child = value; }
        }
        [Browsable(false),
        Bindable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        EditorBrowsable(EditorBrowsableState.Never)]
        public TreeDataGridViewRow ParentRow
        {
            get { return _parentRow; }
            set { _parentRow = value; }
        }
        [Browsable(false),
        Bindable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        EditorBrowsable(EditorBrowsableState.Never)]
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }
        [Browsable(false),
        Bindable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        EditorBrowsable(EditorBrowsableState.Never)]
        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                _expanded = value;

                foreach (TreeDataGridViewRow childRow in _child)
                    childRow.Visible = value;
            }
        }
        [Browsable(false),
        Bindable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        EditorBrowsable(EditorBrowsableState.Never)]
        public bool HasChildren
        {
            get { return this._child.Count > 0 ? true : false; }
        }
        [Browsable(false),
        Bindable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        EditorBrowsable(EditorBrowsableState.Never)]
        new public object DataBoundItem
        {
            get { return _dataBountItem; }
            set { _dataBountItem = value; }
        }
    }
}
