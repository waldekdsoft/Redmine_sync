using Redmine.Net.Api.Types;
using Redmine_sync.RM2XLS;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace Redmine_sync.GUI
{
    public partial class MainForm : Form, IOutputable
    {
        private List<CheckBox> ReasonsForCheckingList = null;
        private static string TMS_LINK = "www.softcomputer.com/itms/gentaskdetails.php?Client={0}&ID={1}";
        private static string RM_LINK = "http://pcredmine:3000/issues/{0}";

        CheckBox testModeCheckBox = null;

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
            //this.tsStatusLabel.Text = "Test mode: " + Consts.TEST_MODE;
            if (testModeCheckBox == null)
            {
                testModeCheckBox = new CheckBox();
                testModeCheckBox.Text = "Test mode";
                ToolStripControlHost host = new ToolStripControlHost(testModeCheckBox);
                statusStrip1.Items.Add(host);
            }

            testModeCheckBox.CheckedChanged -= TestModeCheckBox_CheckedChanged;
            testModeCheckBox.CheckedChanged += TestModeCheckBox_CheckedChanged;

            ReasonsForCheckingList = new List<CheckBox>(new CheckBox[]{ cbNOT_CONNECTED_WITH_TMS,
                                                                        cbBOTH_CLOSED,
                                                                        cbBOTH_OK,
                                                                        cbASSIGNED_TO_DIFFERENT_PERSON_IN_RM_AND_TMS,
                                                                        cbNOT_EXISTS_IN_TMS,
                                                                        cbDIFFERENT_STATUSES,
                                                                        cbNOT_EXISTS_IN_RM });

            dataGridView1.CellFormatting -= DataGridView1_CellFormatting;
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                switch (e.Value)
                {
                    case "RFC_NOT_EXISTS_IN_TMS":
                        e.Value = "Not in TMS";
                        break;
                    case "RFC_NOT_EXISTS_IN_RM":
                        e.Value = "Not in RM";
                        break;
                    case "RFC_DIFFERENT_STATUSES":
                        e.Value = "Diff. statuses";
                        break;
                    case "RFC_ASSIGNED_TO_DIFFERENT_PERSON_IN_RM_AND_TMS":
                        e.Value = "Diff. PRGs";
                        break;
                    case "RFC_NOT_CONNECTED_WITH_TMS":
                        e.Value = "No TMS";
                        break;
                    case "RFC_BOTH_CLOSED":
                        e.Value = "Closed";
                        break;
                    case "RFC_BOTH_OK":
                        e.Value = "OK";
                        break;
                }
            }
        }

        private void TestModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Consts.TEST_MODE = testModeCheckBox.Checked;
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
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = dt;
            dataGridView1.Dock = DockStyle.Fill;
            ApplyChosenFilters();

            /*
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                // if (System.Uri.IsWellFormedUriString(r.Cells["Contact"].Value.ToString(), UriKind.Absolute))
                //{
                DataGridViewLinkCell c = r.Cells[1] as DataGridViewLinkCell;
                if (c != null)
                {
                    //c.Col
                    c.Value = "https://www.automatetheplanet.com/getting-started-webdriver/";
                   // c.UseColumnTextForLinkValue = true;         
                }
                //r.Cells["TMS"] = new DataGridViewLinkCell();
                // Note that if I want a different link colour for example it must go here

                //c.LinkColor = Color.Green;
                //}

            }*/

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

        private void button4_Click(object sender, EventArgs e)
        {
            AddRMDataToDatabse();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            TMSTaskSynchronizer tmsTaskSynchronizer = TMSTaskSynchronizer.GetInstance("MACBI", this);
            tmsTaskSynchronizer.ClearCache();
        }

        private void cb_CheckedChanged(object sender, EventArgs e)
        {
            ApplyChosenFilters();
        }

        private void ApplyChosenFilters()
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            if (dt != null)
            {
                string filter = string.Empty;

                foreach (CheckBox cb in ReasonsForCheckingList)
                {
                    if (cb.Checked)
                    {
                        if (!string.IsNullOrWhiteSpace(filter))
                        {
                            filter += " OR ";
                        }
                        filter += string.Format("Reason = 'RFC_{0}'", cb.Name.Substring(2));
                    }
                }

                if (cbME.Checked)
                {
                    if (!string.IsNullOrWhiteSpace(filter))
                    {
                        filter += " AND ";
                    }
                    filter += "ME = 'Y'";

                }
                dt.DefaultView.RowFilter = filter;
                dataGridView1.Refresh();
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            DataGridViewButtonCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
            if (cell != null)
            {
                if (e.ColumnIndex == 2)/*TMS*/
                {
                    string tms = Convert.ToString(cell.Value);

                    if (!string.IsNullOrEmpty(tms))
                    {
                        string[] tmsTab = tms.Split('-');
                        if (tmsTab.Length == 2)
                        {
                            string client = tmsTab[0];
                            string numb = tmsTab[1];

                            string finalLnk = string.Format(TMS_LINK, client, numb);

                            Process.Start("chrome.exe", finalLnk);
                        }
                    }
                }
                else
                    if (e.ColumnIndex == 3)
                {
                    string rmId = Convert.ToString(cell.Value);

                    if (!string.IsNullOrEmpty(rmId))
                    {
                        string finalLnk = string.Format(RM_LINK, rmId);

                        Process.Start("chrome.exe", finalLnk);
                    }
                }
            }

        }
    }
}
