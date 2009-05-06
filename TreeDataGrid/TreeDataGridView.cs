using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Collections;

namespace KDG.Forms
{
    public partial class TreeDataGridView : DataGridView
    {
        private class _TreeNode
        {
            private object _node = null;
            private _TreeNode _parent = null;
            private List<_TreeNode> _child;
            private object _id = null;
            private object _idParent = null;

            public _TreeNode()
            {
                _child = new List<_TreeNode>();
            }
            public _TreeNode(object Node)
                : this()
            {
                _node = Node;
            }
            public _TreeNode(object Node, object Id, object IdParent)
                : this(Node)
            {
                _id = Id;
                _idParent = IdParent;
            }
            public object Node
            {
                get { return _node; }
                set { _node = value; }
            }
            public _TreeNode Parent
            {
                get { return _parent; }
                set { _parent = value; }
            }
            public List<_TreeNode> Child
            {
                get { return _child; }
                set { _child = value; }
            }
            public object Id
            {
                get { return _id; }
                set { _id = value; }
            }
            public object IdParent
            {
                get { return _idParent; }
                set { _idParent = value; }
            }
        }
        private List<_TreeNode> _nodes = new List<_TreeNode>();
        private List<_TreeNode> _topNodes = new List<_TreeNode>();

        private string _key;
        private string _parentKey;
        private object _gridDataSource = null;
        private string _gridDataMember = string.Empty;
        BindingSource _bs = null;
        private bool _waitForLoadData;
        bool _internalPositionChanged = false;

        //-------------------------------------------------------------------------------------------
        // Constructors
        //-------------------------------------------------------------------------------------------
        public TreeDataGridView()
        {
            InitializeComponent();
            this.RowTemplate = new TreeDataGridViewRow();

            this.AllowUserToOrderColumns = false;
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;

            base.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            base.ColumnAdded += new DataGridViewColumnEventHandler(TreeDataGrid_ColumnAdded);
        }

        //-------------------------------------------------------------------------------------------
        // Events handlers
        //-------------------------------------------------------------------------------------------
        void _bs_PositionChanged(object sender, EventArgs e)
        {
            if (_internalPositionChanged)
            {
                _internalPositionChanged = false;
                return;
            }
            if (_bs.Position >= 0 && this.Rows.Count > _bs.Position)
            {
                TreeDataGridViewRow row = this.Rows[_bs.Position] as TreeDataGridViewRow;
                for (int i = 0; i < _bs.List.Count; i++)
                {
                    DataRowView drv = _bs.List[i] as DataRowView;
                    if (row.DataBoundItem != null && row.DataBoundItem.Equals(drv.Row))
                    {
                        for (int j = 0; j < _bs.List.Count; j++)
                            this.SetSelectedRowCore(j, false);

                        this.SetSelectedRowCore(i, true);
                        return;
                    }
                }
            }
        }
        void _bs_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (!_waitForLoadData)
                if (e.ListChangedType == ListChangedType.ItemAdded)
                    SetupRows();
        }
        private void TreeDataGrid_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Clicks == 1 && e.RowIndex >= 0)
            {
                TreeDataGridViewRow row = this.Rows[e.RowIndex] as TreeDataGridViewRow;
                for (int i = 0; i < _bs.List.Count; i++)
                {
                    DataRowView drv = _bs.List[i] as DataRowView;
                    if (row.DataBoundItem.Equals(drv.Row))
                    {
                        _internalPositionChanged = true;
                        _bs.Position = i;
                        return;
                    }
                }
            }
        }
        void TreeDataGrid_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        //-------------------------------------------------------------------------------------------
        // Methods
        //-------------------------------------------------------------------------------------------
        public void BuildTree()
        {
            SetupRows();
        }
        private void SetupRows()
        {
            CancelEventArgs cea = new CancelEventArgs();
            if (SetupingRows != null)
            {
                SetupingRows(this, cea);
                if (cea.Cancel)
                    return;
            }

            this.Rows.Clear();
            _nodes.Clear();
            _topNodes.Clear();

            foreach (DataRowView drv in _bs.List)
            {
                object id = drv.Row[_key];
                object idParent = drv.Row[_parentKey];

                _TreeNode tn = new _TreeNode(drv, id, idParent);
                _nodes.Add(tn);
            }

            foreach (_TreeNode tn in _nodes)
            {
                _TreeNode parentTn = FindParent(_nodes, tn);
                if (parentTn == null)
                    _topNodes.Add(tn);
            }

            foreach (_TreeNode tn in _topNodes)
            {
                _nodes.Remove(tn);
            }



            bool cont = true;
            while (cont)
            {
                cont = false;

                foreach (_TreeNode tn in _nodes)
                {
                    if (tn.Parent == null)
                    {
                        _TreeNode parentTn = FindParent(_topNodes, tn);
                        if (parentTn == null)
                            cont = true;
                        else
                        {
                            parentTn.Child.Add(tn);
                            tn.Parent = parentTn;
                        }
                    }
                }
            }

            InsertDataGridRows(_topNodes, null);

            if (SetupedRows != null)
                SetupedRows(this, new EventArgs());
        }

        private void InsertDataGridRows(List<_TreeNode> l, TreeDataGridViewRow parent)
        {
            foreach (_TreeNode tn in l)
            {
                int index = this.Rows.Add(this.RowTemplate.Clone());
                TreeDataGridViewRow insertedRow = this.Rows[index] as TreeDataGridViewRow;
                insertedRow.DataBoundItem = (tn.Node as DataRowView).Row;
                insertedRow.SetValues((tn.Node as DataRowView).Row.ItemArray);
                insertedRow.ParentRow = parent;
                if (parent != null)
                {
                    insertedRow.Level = parent.Level + 1;
                    parent.Child.Add(insertedRow);
                }

                if (insertedRow.Level > 0)
                    insertedRow.Visible = false;

                InsertDataGridRows(tn.Child, insertedRow);
            }
        }
        // Find node by 'IdParent' key in BindingSource's rows and in _TreeNode colection
        private DataRowView FindInAddedRows(object idParent, IList l)
        {
            foreach (DataRowView drv in l)
            {
                object id = drv.Row[_key];
                if (id != null && id.Equals(idParent))
                    return drv;
            }
            return null;
        }
        // Find 'DataRowView' in _TreeNode collection
        private _TreeNode FindParent(List<_TreeNode> nodes, _TreeNode node)
        {
            foreach (_TreeNode tn in nodes)
            {
                if (tn.Id.Equals(node.IdParent))
                    return tn;

                // Find in child nodes
                _TreeNode fc = FindParent(tn.Child, node);
                if (fc != null)
                    return fc;
            }

            return null;
        }
        public void ExpandRow(int Index)
        {
            TreeDataGridViewRow row = this.Rows[Index] as TreeDataGridViewRow;
            if (row != null && row.ParentRow != null)
            {
                ExpandRow(row.ParentRow.Index);
            }
        }
        public void ExpandAll()
        {
            foreach (TreeDataGridViewRow dgvr in this.Rows)
                dgvr.Expanded = true;
        }

        //-------------------------------------------------------------------------------------------
        // Properties
        //-------------------------------------------------------------------------------------------
        [Category("Data"), DefaultValue((string)null)]
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }
        [Category("Data"), DefaultValue((string)null)]
        public string ParentKey
        {
            get { return _parentKey; }
            set { _parentKey = value; }
        }
        [Category("Data")]
        public string GridDataMember
        {
            get { return _gridDataMember; }
            set
            { //this.DataMember = 
                _gridDataMember = value;
            }
        }
        [RefreshProperties(RefreshProperties.Repaint), DefaultValue((string)null), Category("Data"), AttributeProvider(typeof(IListSource))]
        public object GridDataSource
        {
            get { return _gridDataSource; }
            set
            {
                try
                {
                    _gridDataSource = value;
                    _bs = (_gridDataSource as BindingSource);
                    //_bs.BindingComplete += new BindingCompleteEventHandler(_bs_BindingComplete);
                    _bs.ListChanged += new ListChangedEventHandler(_bs_ListChanged);
                    _bs.PositionChanged += new EventHandler(_bs_PositionChanged);


                    //this.DataSource = value;
                }
                catch (Exception)
                {


                }
            }
        }
        [DefaultValue(false)]
        public bool WaitForLoadData
        {
            get { return _waitForLoadData; }
            set { _waitForLoadData = value; }
        }

        //---------------------
        // Override properties
        //---------------------
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        EditorBrowsable(EditorBrowsableState.Never)]
        new public DataGridViewSelectionMode SelectionMode
        {
            get { return base.SelectionMode; }

        }
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        EditorBrowsable(EditorBrowsableState.Never)]
        public new object DataSource
        {
            get { return null; }
            set { ; }
        }
        [Browsable(false),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        EditorBrowsable(EditorBrowsableState.Never)]
        public new string DataMember
        {
            get { return null; }
            set { ; }
        }

        //---------------------
        // Events
        //---------------------
        public event CancelEventHandler SetupingRows;
        public event EventHandler SetupedRows;
    }
}
