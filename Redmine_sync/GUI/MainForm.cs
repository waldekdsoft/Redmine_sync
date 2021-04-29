using System;
using System.Windows.Forms;

namespace Redmine_sync.GUI
{
    public partial class MainForm : Form , IOutputable
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addNewItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MOMActionsManager mom = new MOMActionsManager(this);
            mom.AddNewItems();
        }

        private void updateItemsbasedOnSingleXLSXFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MOMActionsManager mom = new MOMActionsManager(this);
            mom.UpdateItems();
        }

        private void updateItemsbasedOnAllXLSXFileFromTheDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MOMActionsManager mom = new MOMActionsManager(this);
            mom.UpdateItems(true);
        }

        public void WriteLine(string line)
        {
            Write(line + Consts.EOL_SPECIAL);
        }

        public void Write(string line)
        {
            tbMainOutput.AppendText(line);
        }

    }
}
