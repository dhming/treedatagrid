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

        private CurrencyManager _currencyManager = null;


        public TreeDataGrid()
        {
            InitializeComponent();
            this.RowTemplate = new TreeRow();

        }



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
                    _bs.ListChanged += new ListChangedEventHandler(_bs_ListChanged);

                    this.DataSource = value;
                }
                catch (Exception)
                {

                    
                }
            }
        }

        [Category("Data")]
        public string GridDataMember
        {
            get { return _gridDataMember; }
            set { this.DataMember = _gridDataMember = value; }
        }

        void _bs_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
                SetupRows();

        }

        private void SetupRows()
        {
            this.Rows.Clear();
            foreach (DataRowView drv in _bs.List)
            {
                int index = this.Rows.Add(this.RowTemplate.Clone());
                TreeRow insertedRow = this.Rows[index] as TreeRow;
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

        // This sample does not support databinding
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
