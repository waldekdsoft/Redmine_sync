using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Redmine_sync.GUI
{
    public partial class TMSActionsForm : Form, IOutputable
    {
        public TMSActionsForm()
        {
            InitializeComponent();
        }

        public void FlushWriteLines()
        {
            throw new NotImplementedException();
        }

        public bool GetIsRedisUse()
        {
            throw new NotImplementedException();
        }

        public void Write(string s, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string s, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void WriteLineToBuffer(string s, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void WriteToGrid(DataTable dt)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = dt;
            dataGridView1.Dock = DockStyle.Fill;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var dataGridView = sender as DataGridView;

            if (dataGridView != null)
            {
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView.Columns[dataGridView.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
    }
}
