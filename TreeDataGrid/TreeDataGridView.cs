using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;

namespace KDG.Forms
{
    public partial class TreeDataGridView : DataGridView
    {
        private class _TreeNode
        {
            private object _node = null;
            private _TreeNode _parent = null;
            private List<_TreeNode> _child;

            public _TreeNode()
            {
                _child = new List<_TreeNode>();
            }
            public _TreeNode(object Node)
                : this()
            {
                _node = Node;
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
        }
        private List<_TreeNode> _nodes = new List<_TreeNode>();

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
            this.Rows.Clear();
            _nodes.Clear();

            List<DataRowView> l = new List<DataRowView>();
            foreach (DataRowView drv in _bs.List)
                l.Add(drv as DataRowView);

            foreach (DataRowView drv in l)
            {
                _TreeNode tn = new _TreeNode(drv);
                object idParent = drv.Row[_parentKey];
                object id = drv.Row[_key];

                _TreeNode parentTn = FindParent(idParent, _nodes, l);
                if (parentTn != null)
                {
                    tn.Parent = parentTn;
                    parentTn.Child.Add(tn);
                }
                else
                    _nodes.Add(tn);
            }

            InsertDataGridRow(_nodes, null);
        }
        private void InsertDataGridRow(List<_TreeNode> l, TreeDataGridViewRow parent)
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

                InsertDataGridRow(tn.Child, insertedRow);
            }
        }
        private _TreeNode FindParent(object idParent, List<_TreeNode> nodes, List<DataRowView> l)
        {
            foreach (DataRowView drv in l)
            {
                object id = drv.Row["id"];
                if (id != null && id.Equals(idParent))
                {
                    _TreeNode tn = FindInTreeNode(nodes, drv);
                    if (tn == null)
                        tn = new _TreeNode(drv);

                    return tn;
                }
            }
            return null;
        }
        private _TreeNode FindInTreeNode(List<_TreeNode> nodes, DataRowView drv)
        {
            foreach (_TreeNode tn in nodes)
            {
                if (tn.Node.Equals(drv))
                    return tn;

                _TreeNode fc = FindInTreeNode(tn.Child, drv);
                if (fc != null)
                    return fc;
            }


            return null;
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
    }
}
