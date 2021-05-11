using Redmine.Net.Api.Types;
using Redmine_sync.RM2XLS;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        public void WriteLine(string line, params object[] args)
        {
            
            Write(line + Consts.EOL_SPECIAL, args);
        }

        public void Write(string line, params object[] args)
        {
            string msg = string.Format(line, args);
            tbMainOutput.AppendText(msg);
        }

        private void showSyncInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TMSTaskSynchronizer tmsTaskSynchronizer = TMSTaskSynchronizer.GetInstance("MACBI", this);
            tmsTaskSynchronizer.GetherSyncData();
            tmsTaskSynchronizer.CreateSyncOutputList();
            tmsTaskSynchronizer.DisplayStatsForTMSSync();
        }

        private void addMissingTMSToRMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TMSTaskSynchronizer tmsTaskSynchronizer = TMSTaskSynchronizer.GetInstance("MACBI", this);
            tmsTaskSynchronizer.AddMissingTMSTasksToRedmine();
        }

        private void updateRMWithToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TMSTaskSynchronizer tmsTaskSynchronizer = TMSTaskSynchronizer.GetInstance("MACBI", this);
            tmsTaskSynchronizer.UpdateRMWithLastTMSInfo();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.tsStatusLabel.Text = "Test mode: " + Consts.TEST_MODE;
        }

        private void getIssuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NameValueCollection parameters = new NameValueCollection { { "query_id", "62" } };
            List<Issue> issues = RMManegerService.RMManager.GetObjects<Issue>(parameters);

            //insert data to database


        }

        private void addSampleDataToDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RM2XLSManager manager = new RM2XLSManager(this);
            manager.ConvertRedMineIssuesToDB();
        }
    }
}
