using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TreeDataGridTestApp
{
    public partial class FormNewMessage : Form
    {
        public FormNewMessage()
        {
            InitializeComponent();
        }

        public FormNewMessage(CMessage Message):this()
        {
            SetControlsData(Message);
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SetControlsData(CMessage value)
        {
            this.tbMessage.Text = value.Message;
            this.dtpDateTime.Value = value.DateTime;
        }

        public CMessage Message
        {
            get { return new CMessage(this.tbMessage.Text, this.dtpDateTime.Value); }
            set { SetControlsData(value); }
        }
    }
}