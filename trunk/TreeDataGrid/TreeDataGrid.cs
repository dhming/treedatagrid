using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;

namespace KDG.Forms.TreeDataGrid
{
    public partial class TreeDataGrid : DataGridView
    {
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
        public TreeDataGrid()
        {
            InitializeComponent();
            this.RowTemplate = new TreeRow();

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
            if (this.Rows.Count > _bs.Position)
            {
                TreeRow row = this.Rows[_bs.Position] as TreeRow;
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
                TreeRow row = this.Rows[e.RowIndex] as TreeRow;
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
            foreach (DataRowView drv in _bs.List)
            {
                int index = this.Rows.Add(this.RowTemplate.Clone());
                TreeRow insertedRow = this.Rows[index] as TreeRow;
                insertedRow.DataBoundItem = drv.Row;
                insertedRow.SetValues(drv.Row.ItemArray);
            }

            foreach (TreeRow tr in this.Rows)
            {
                TreeRow parent = FindParent(tr.Cells[ParentKey].Value);
                if (parent != null)
                {
                    tr.ParentRow = parent;
                    parent.Child.Add(tr);
                    tr.Level = parent.Level + 1;

                    tr.Visible = false;
                }
            }
        }
        private TreeRow FindParent(object p)
        {
            foreach (TreeRow tr in this.Rows)
            {
                object val = tr.Cells[Key].Value;
                if (val != null && val.Equals(p))
                    return tr;
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
