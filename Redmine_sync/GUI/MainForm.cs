using Redmine.Net.Api.Types;
using Redmine_sync.RM2XLS;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
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
            AddNewItems();
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
            ShowSyncInfo();
        }

        private void addMissingTMSToRMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddMissingTMSToRM();
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
            AddRMDataToDatabse();
        }        

        private void button1_Click(object sender, EventArgs e)
        {
            AddNewItems();
        }


        private void ShowSyncInfo()
        {
            TMSTaskSynchronizer tmsTaskSynchronizer = TMSTaskSynchronizer.GetInstance("MACBI", this);
            tmsTaskSynchronizer.GetherSyncData();
            tmsTaskSynchronizer.CreateSyncOutputList();
            tmsTaskSynchronizer.DisplayStatsForTMSSync();
        }

        private void AddMissingTMSToRM()
        {
            TMSTaskSynchronizer tmsTaskSynchronizer = TMSTaskSynchronizer.GetInstance("MACBI", this);
            tmsTaskSynchronizer.AddMissingTMSTasksToRedmine();
        }

        private void AddNewItems()
        {
            MOMActionsManager mom = new MOMActionsManager(this);
            mom.AddNewItems();
        }
        private void AddRMDataToDatabse()
        {
            RM2XLSManager manager = new RM2XLSManager(this);
            manager.ConvertRedMineIssuesToDB();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowSyncInfo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddMissingTMSToRM();
        }

        public void WriteToGrid(DataTable dt)
        {
            dataGridView1.DataSource = dt;
            dataGridView1.Dock = DockStyle.Fill;

            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                // if (System.Uri.IsWellFormedUriString(r.Cells["Contact"].Value.ToString(), UriKind.Absolute))
                //{
                DataGridViewLinkCell c = r.Cells[1] as DataGridViewLinkCell;
                if (c != null)
                {
                    //c.Col
                    c.Value = "https://www.automatetheplanet.com/getting-started-webdriver/";
                    c.UseColumnTextForLinkValue = true;         
                }
                //r.Cells["TMS"] = new DataGridViewLinkCell();
                // Note that if I want a different link colour for example it must go here

                //c.LinkColor = Color.Green;
                //}

            }
        }


            private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var dataGridView = sender as DataGridView;

            if (dataGridView != null)
            {
                /*
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    // if (System.Uri.IsWellFormedUriString(r.Cells["Contact"].Value.ToString(), UriKind.Absolute))
                    //{
                    DataGridViewLinkCell c = r.Cells["TMS"] as DataGridViewLinkCell;
                    //r.Cells["TMS"] = new DataGridViewLinkCell();
                    // Note that if I want a different link colour for example it must go here

                    //c.LinkColor = Color.Green;
                    //}
                }
                */
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView.Columns[dataGridView.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            
        }
    }
}
