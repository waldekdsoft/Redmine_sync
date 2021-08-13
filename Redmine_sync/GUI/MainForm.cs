using Redmine.Net.Api;
using Redmine.Net.Api.Types;
using Redmine_sync.RM2XLS;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Redmine_sync.GUI
{
    public partial class MainForm : Form, IOutputable
    {
        private List<CheckBox> ReasonsForCheckingList = null;
        private static string TMS_LINK = "www.softcomputer.com/itms/gentaskdetails.php?Client={0}&ID={1}";
        private static string RM_LINK = "http://pcredmine:3000/issues/{0}";

        CheckBox testModeCheckBox = null;
        CheckBox redisStoreCheckBox = null;

        StringBuilder writeLineBuffer = new StringBuilder();

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
            AddNewItemsFromExcel();
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
           /* 
            tbMainOutput.Invoke(new MethodInvoker(delegate {
                tbMainOutput.AppendText(msg);
            }));            
           */
        }

        public void FlushWriteLines()
        {
            tbMainOutput.Invoke(new MethodInvoker(delegate {
                tbMainOutput.AppendText(writeLineBuffer.ToString());
                writeLineBuffer.Clear();
            }));
        }

        public void WriteLineToBuffer(string line, params object[] args)
        {
            string msg = string.Format(line, args);
            writeLineBuffer.Append(msg);
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
            if (redisStoreCheckBox == null)
            {
                redisStoreCheckBox = new CheckBox();
                redisStoreCheckBox.Text = "Redis store";
                ToolStripControlHost host = new ToolStripControlHost(redisStoreCheckBox);
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

            //default checking 
            foreach (CheckBox cb in ReasonsForCheckingList)
            {
                if (cb != cbBOTH_CLOSED && cb != cbBOTH_OK)
                {
                    cb.Checked = true;
                }
            }

            
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
                    case "RFC_ASSIGNED_TO_ME_IN_RM":
                        e.Value = "Me in RM";
                        break;
                    case "RFC_ASSIGNED_TO_ME_IN_TMS":
                        e.Value = "Me in TMS";
                        break;
//                              public static readonly string RFC_ASSIGNED_TO_ME_IN_RM = "RFC_ASSIGNED_TO_ME_IN_RM";
//      public static readonly string RFC_ASSIGNED_TO_ME_IN_TMS = "RFC_ASSIGNED_TO_ME_IN_TMS";

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

        private void PrintCurrentTime()
        {
            WriteLine(DateTime.Now.ToString("HH:mm") + ": ");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintCurrentTime();
            AddNewItemsFromExcel();
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
            ShowSyncInfo();
        }

        private void AddNewItemsFromExcel()
        {
            MOMActionsManager mom = new MOMActionsManager(this);
            mom.AddNewItemsFromExcel();
        }
        private void AddNewItemsFromTXT()
        {
            MOMActionsManager mom = new MOMActionsManager(this);
            mom.AddNewItemsFromTXT();
        }

        private void AddRMDataToDatabse()
        {
            RM2XLSManager manager = new RM2XLSManager(this);
            manager.ConvertRedMineIssuesToDB();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PrintCurrentTime();
            ShowSyncInfo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PrintCurrentTime();
            AddMissingTMSToRM();
        }

        public void WriteToGrid(DataTable dt)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = dt;
            dataGridView1.Dock = DockStyle.Fill;
            ApplyChosenFilters();
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

        private void button4_Click(object sender, EventArgs e)
        {
            PrintCurrentTime();
            AddRMDataToDatabse();
        }

    

        private void button5_Click(object sender, EventArgs e)
        {
            PrintCurrentTime();
            TMSTaskSynchronizer tmsTaskSynchronizer = TMSTaskSynchronizer.GetInstance("MACBI", this);
            tmsTaskSynchronizer.ClearCache();
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();

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

                if (!cbDISP_ALL.Checked)
                {
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
                }

                if (cbME.Checked)
                {
                    if (!string.IsNullOrWhiteSpace(filter))
                    {
                        filter = "(" + filter + ")";
                        filter += " AND ";
                    }
                    filter += "ME = 'Y'";
                }

                
                /*
                if (cbRM_NEW.Checked)
                {
                    if (!string.IsNullOrWhiteSpace(filter))
                    {
                        filter = "(" + filter + ")";
                        filter += " AND ";
                    }
                    filter += "(RM_STATUS = 'New' OR RM_STATUS = 'In Progress')";
                }
                */
                if (checkedListBox1.CheckedItems.Count > 0)
                {
                    string cbListFilter = string.Empty;
                    foreach (string s in checkedListBox1.CheckedItems)
                    {
                        if (!string.IsNullOrWhiteSpace(cbListFilter))
                        {
                            cbListFilter += " OR ";
                        }

                        if (s == "<empty>")
                        {
                            cbListFilter += "RM_STATUS IS NULL";
                        }
                        else
                        {
                            cbListFilter += string.Format("RM_STATUS = '{0}'", s);
                        }
                    }
                    cbListFilter = string.Format(" ({0}) ", cbListFilter);
                    if (!string.IsNullOrWhiteSpace(filter))
                    {
                        filter = string.Format(" ({0}) ", filter);
                        filter += " AND ";
                    }
                    filter += cbListFilter;
                }
                dt.DefaultView.RowFilter = filter;
                dataGridView1.Refresh();
            }
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
                if (e.ColumnIndex == 3) /* TMS ACTION HIST */
                {
                    DataGridViewButtonCell tmsTaskCell = dataGridView1.Rows[e.RowIndex].Cells[2] as DataGridViewButtonCell;
                    string tms = Convert.ToString(tmsTaskCell.Value);

                    if (!string.IsNullOrEmpty(tms))
                    {
                        string[] tmsTab = tms.Split('-');
                        if (tmsTab.Length == 2)
                        {
                            string client = tmsTab[0];
                            string numb = tmsTab[1];

                            TMSTaskSynchronizer tmsTaskSynchronizer = TMSTaskSynchronizer.GetInstance(client, this);
                            DataTable tmsActions = tmsTaskSynchronizer.GetActionsForTMS(numb);

                            TMSActionsForm form = new TMSActionsForm();
                            form.WriteToGrid(tmsActions);
                            form.ShowDialog();

                        }
                    }

                }
                else
                    if (e.ColumnIndex == 4) /* RM LINK*/
                {
                    string rmId = Convert.ToString(cell.Value);

                    if (!string.IsNullOrEmpty(rmId))
                    {
                        string finalLnk = string.Format(RM_LINK, rmId);

                        Process.Start("chrome.exe", finalLnk);
                    }
                }
                else
                if (e.ColumnIndex == 5) /* RM ACTION HIST */
                { }

                else
                if (e.ColumnIndex == 8)
                {
                    DataGridViewTextBoxCell assignedToCell = dataGridView1.Rows[e.RowIndex].Cells[4] as DataGridViewTextBoxCell;
                    DataGridViewButtonCell rmIssueNum = dataGridView1.Rows[e.RowIndex].Cells[3] as DataGridViewButtonCell;

                    if (assignedToCell != null && rmIssueNum != null)
                    {
                        string assignedInTMS = Convert.ToString(assignedToCell.Value);
                        string rmId = Convert.ToString(rmIssueNum.Value);

                        if (!string.IsNullOrEmpty(assignedInTMS) && !string.IsNullOrEmpty(rmId))
                        {
                            NameValueCollection parameters = new NameValueCollection {
                            { RedmineKeys.INCLUDE, $"{RedmineKeys.CHANGE_SETS},{RedmineKeys.JOURNALS},{RedmineKeys.NOTES}" } };
                            Issue rmIssue = RMManegerService.RMManager.GetObject<Issue>(rmId, parameters);


                            if (rmIssue != null)
                            {
                                
                            }
                            //var newIssue = new Issue { Subject = subject, Project = p, Description = details };
                            //MManegerService.RMManager.CreateObject(newIssue);


                            // fixture.RedmineManager.UpdateObject(UPDATED_ISSUE_ID, issue);

                            switch (assignedInTMS)
                            {
                                case "N/GENEBUG":
                                    break;

                            }
                        }
                    }
                    //string assignedInTMS = dataGridView1.Rows[e.RowIndex].Cells[4]; 
                }
            }

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyChosenFilters();
        }

        public bool GetIsRedisUse()
        {
            return redisStoreCheckBox.Checked;
        }

        async private void btnTest_Click(object sender, EventArgs e)
        {
            TMSTaskSynchronizer tmsTaskSynchronizer = TMSTaskSynchronizer.GetInstance("MACBI", this);
            tmsTaskSynchronizer.GetActionsForTMS("02866");


            //PrintCurrentTime();
            //WriteLine(DateTime.Now.ToString("HH:mm"));
            /*
            var cookieJar = new CookieContainer();
            var client = new RestClient("http://pcredmine:3000");
            client.AddDefaultQueryParameter("key", "0533a992d0c093b3b1592e57e10281156ea6afde");
            var request = new RestRequest("issues/7979.xml", Method.PUT);
            request.AddParameter("Content-Type", "application/xml");
            request.RequestFormat = DataFormat.Xml;

            string xml = "<?xml version=\"1.0\"?><issue><notes>The subject was changed 111</notes></issue>";
            */
            //request.Add

            //request.AddBody(xml);

            //client.Authenticator = new SimpleAuthenticator();
            //var response = client.Put(request);

            //var postUrl = "issues/7979.xml";
            //string rawXml = "<?xml version=\"1.0\"?><issue><notes>The subject was changed 111</notes></issue>";

            //var client = new RestClient("http://pcredmine:3000");
            //client.AddDefaultQueryParameter("key", "0533a992d0c093b3b1592e57e10281156ea6afde");
            //IRestRequest request = new RestRequest
            //{
            //    Resource = postUrl
            //};

            //request.AddHeader("Content-Type", "text/xml");
            //request.AddHeader("Accept", "text/xml");
            //request.AddParameter("text/xml", rawXml, ParameterType.RequestBody);


            //IRestResponse response = client.Put(request);
            //IRestResponse response = client.Execu//te(request);

            //  Assert.IsNotNull(response.Data);
            /*
            Dictionary<string, string> valuesToUpdate = new Dictionary<string, string>();
            valuesToUpdate.Add("notes", "some sample note");
            valuesToUpdate.Add("assigned_to_id", "549");
            UpdateRMTicket("8356", valuesToUpdate);
            */
        }

        /*
            project_id
            tracker_id
            status_id
            priority_id
            subject
            description
            category_id
            fixed_version_id - ID of the Target Versions (previously called 'Fixed Version' and still referred to as such in the API)
            assigned_to_id - ID of the user to assign the issue to (currently no mechanism to assign by name)
            parent_issue_id - ID of the parent issue
            custom_fields - See Custom fields
            watcher_user_ids - Array of user ids to add as watchers (since 2.3.0)
            is_private - Use true or false to indicate whether the issue is private or not
            estimated_hours - Number of hours estimated for issue
            notes - Comments about the update
            private_notes - true if notes are private
         */
        private bool UpdateRMTicket(string rmTicketId, Dictionary<string, string> valuesToUpdate)
        {
            if (valuesToUpdate.Count > 0)
            {
                string xmlBegin = "<?xml version=\"1.0\"?><issue>";
                string xmlEnd = "</issue>";

                var postUrl = string.Format("issues/{0}.xml", rmTicketId);
                string rawXml = string.Empty;

                foreach (string key in valuesToUpdate.Keys)
                {
                    rawXml += string.Format("<{0}>{1}</{0}>", key, valuesToUpdate[key], key);
                }

                rawXml = xmlBegin + rawXml + xmlEnd;

                var client = new RestClient("http://pcredmine:3000");
                client.AddDefaultQueryParameter("key", "0533a992d0c093b3b1592e57e10281156ea6afde");
                IRestRequest request = new RestRequest
                {
                    Resource = postUrl
                };

                request.AddHeader("Content-Type", "text/xml");
                request.AddHeader("Accept", "text/xml");
                request.AddParameter("text/xml", rawXml, ParameterType.RequestBody);


                IRestResponse response = client.Put(request);


                Write("UpdateRMTicket: Status: {0} Description {1}", response.ResponseStatus, response.StatusDescription);
                return response.ResponseStatus == ResponseStatus.Completed;
            }
            Write("UpdateRMTicket: No values provided!");
            return false;
        }

        private void SelectDeselectItems(bool select)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, select);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            SelectDeselectItems(true);
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            SelectDeselectItems(false);
        }

        private void btnAddExceptionsToRM_Click(object sender, EventArgs e)
        {
            PrintCurrentTime();
            AddNewExceptionItems();
        }

        private void AddNewExceptionItems()
        {
            ExceptionsActionsManager eam = new ExceptionsActionsManager(this);
            eam.AddNewItems();

        }

        private void btnAddMOMItemsTXT_Click(object sender, EventArgs e)
        {
            PrintCurrentTime();
            AddNewItemsFromTXT();

        }
    }
}
