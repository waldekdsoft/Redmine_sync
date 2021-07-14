
namespace Redmine_sync.GUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mOMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateItemsbasedOnSingleXLSXFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateItemsbasedOnAllXLSXFileFromTheDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tMSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showSyncInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addMissingTMSToRMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateRMWithToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rM2XLSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getIssuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSampleDataToDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbMainOutput = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.reasonDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chDataGridViewCheckBoxColumnMe = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tMSDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.TMS_ACT_HIST = new System.Windows.Forms.DataGridViewButtonColumn();
            this.RM = new System.Windows.Forms.DataGridViewButtonColumn();
            this.RM_ACT_HIST = new System.Windows.Forms.DataGridViewButtonColumn();
            this.TMS_ASSIGNED = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TMS_STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RM_ASSIGNED = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RM_STATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AUTO_ACTION = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tMSWithReasonBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainDS = new Redmine_sync.DataSets.MainDS();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.cbDISP_ALL = new System.Windows.Forms.CheckBox();
            this.cbME = new System.Windows.Forms.CheckBox();
            this.cbBOTH_OK = new System.Windows.Forms.CheckBox();
            this.cbBOTH_CLOSED = new System.Windows.Forms.CheckBox();
            this.cbNOT_CONNECTED_WITH_TMS = new System.Windows.Forms.CheckBox();
            this.cbASSIGNED_TO_DIFFERENT_PERSON_IN_RM_AND_TMS = new System.Windows.Forms.CheckBox();
            this.cbDIFFERENT_STATUSES = new System.Windows.Forms.CheckBox();
            this.cbNOT_EXISTS_IN_RM = new System.Windows.Forms.CheckBox();
            this.cbNOT_EXISTS_IN_TMS = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tMSWithReasonBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDS)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.actionsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(2054, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(55, 26);
            this.fileToolStripMenuItem.Text = "File...";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mOMToolStripMenuItem,
            this.tMSToolStripMenuItem,
            this.rM2XLSToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(72, 26);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // mOMToolStripMenuItem
            // 
            this.mOMToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewItemsToolStripMenuItem,
            this.updateItemsbasedOnSingleXLSXFileToolStripMenuItem,
            this.updateItemsbasedOnAllXLSXFileFromTheDirectoryToolStripMenuItem});
            this.mOMToolStripMenuItem.Name = "mOMToolStripMenuItem";
            this.mOMToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.mOMToolStripMenuItem.Text = "MOM";
            // 
            // addNewItemsToolStripMenuItem
            // 
            this.addNewItemsToolStripMenuItem.Name = "addNewItemsToolStripMenuItem";
            this.addNewItemsToolStripMenuItem.Size = new System.Drawing.Size(462, 26);
            this.addNewItemsToolStripMenuItem.Text = "Add new items (moms.xml)";
            this.addNewItemsToolStripMenuItem.Click += new System.EventHandler(this.addNewItemsToolStripMenuItem_Click);
            // 
            // updateItemsbasedOnSingleXLSXFileToolStripMenuItem
            // 
            this.updateItemsbasedOnSingleXLSXFileToolStripMenuItem.Name = "updateItemsbasedOnSingleXLSXFileToolStripMenuItem";
            this.updateItemsbasedOnSingleXLSXFileToolStripMenuItem.Size = new System.Drawing.Size(462, 26);
            this.updateItemsbasedOnSingleXLSXFileToolStripMenuItem.Text = "Update items (based on single XLSX file)";
            this.updateItemsbasedOnSingleXLSXFileToolStripMenuItem.Click += new System.EventHandler(this.updateItemsbasedOnSingleXLSXFileToolStripMenuItem_Click);
            // 
            // updateItemsbasedOnAllXLSXFileFromTheDirectoryToolStripMenuItem
            // 
            this.updateItemsbasedOnAllXLSXFileFromTheDirectoryToolStripMenuItem.Name = "updateItemsbasedOnAllXLSXFileFromTheDirectoryToolStripMenuItem";
            this.updateItemsbasedOnAllXLSXFileFromTheDirectoryToolStripMenuItem.Size = new System.Drawing.Size(462, 26);
            this.updateItemsbasedOnAllXLSXFileFromTheDirectoryToolStripMenuItem.Text = "Update items (based on all XLSX file from the directory)";
            this.updateItemsbasedOnAllXLSXFileFromTheDirectoryToolStripMenuItem.Click += new System.EventHandler(this.updateItemsbasedOnAllXLSXFileFromTheDirectoryToolStripMenuItem_Click);
            // 
            // tMSToolStripMenuItem
            // 
            this.tMSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showSyncInfoToolStripMenuItem,
            this.addMissingTMSToRMToolStripMenuItem,
            this.updateRMWithToolStripMenuItem});
            this.tMSToolStripMenuItem.Name = "tMSToolStripMenuItem";
            this.tMSToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.tMSToolStripMenuItem.Text = "TMS";
            // 
            // showSyncInfoToolStripMenuItem
            // 
            this.showSyncInfoToolStripMenuItem.Name = "showSyncInfoToolStripMenuItem";
            this.showSyncInfoToolStripMenuItem.Size = new System.Drawing.Size(349, 26);
            this.showSyncInfoToolStripMenuItem.Text = "Show sync. info...";
            this.showSyncInfoToolStripMenuItem.Click += new System.EventHandler(this.showSyncInfoToolStripMenuItem_Click);
            // 
            // addMissingTMSToRMToolStripMenuItem
            // 
            this.addMissingTMSToRMToolStripMenuItem.Name = "addMissingTMSToRMToolStripMenuItem";
            this.addMissingTMSToRMToolStripMenuItem.Size = new System.Drawing.Size(349, 26);
            this.addMissingTMSToRMToolStripMenuItem.Text = "Add missing TMS to RM...";
            this.addMissingTMSToRMToolStripMenuItem.Click += new System.EventHandler(this.addMissingTMSToRMToolStripMenuItem_Click);
            // 
            // updateRMWithToolStripMenuItem
            // 
            this.updateRMWithToolStripMenuItem.Name = "updateRMWithToolStripMenuItem";
            this.updateRMWithToolStripMenuItem.Size = new System.Drawing.Size(349, 26);
            this.updateRMWithToolStripMenuItem.Text = "Update RM with last action from TMS...";
            this.updateRMWithToolStripMenuItem.Click += new System.EventHandler(this.updateRMWithToolStripMenuItem_Click);
            // 
            // rM2XLSToolStripMenuItem
            // 
            this.rM2XLSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getIssuesToolStripMenuItem,
            this.addSampleDataToDatabaseToolStripMenuItem});
            this.rM2XLSToolStripMenuItem.Name = "rM2XLSToolStripMenuItem";
            this.rM2XLSToolStripMenuItem.Size = new System.Drawing.Size(146, 26);
            this.rM2XLSToolStripMenuItem.Text = "RM2XLS";
            // 
            // getIssuesToolStripMenuItem
            // 
            this.getIssuesToolStripMenuItem.Name = "getIssuesToolStripMenuItem";
            this.getIssuesToolStripMenuItem.Size = new System.Drawing.Size(347, 26);
            this.getIssuesToolStripMenuItem.Text = "Assigned to DEV1 w/o MOM Problems";
            this.getIssuesToolStripMenuItem.Click += new System.EventHandler(this.getIssuesToolStripMenuItem_Click);
            // 
            // addSampleDataToDatabaseToolStripMenuItem
            // 
            this.addSampleDataToDatabaseToolStripMenuItem.Name = "addSampleDataToDatabaseToolStripMenuItem";
            this.addSampleDataToDatabaseToolStripMenuItem.Size = new System.Drawing.Size(347, 26);
            this.addSampleDataToDatabaseToolStripMenuItem.Text = "Add RM data to SD database";
            this.addSampleDataToDatabaseToolStripMenuItem.Click += new System.EventHandler(this.addSampleDataToDatabaseToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(73, 26);
            this.aboutToolStripMenuItem.Text = "About...";
            // 
            // tbMainOutput
            // 
            this.tbMainOutput.BackColor = System.Drawing.SystemColors.Window;
            this.tbMainOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMainOutput.Location = new System.Drawing.Point(0, 0);
            this.tbMainOutput.Multiline = true;
            this.tbMainOutput.Name = "tbMainOutput";
            this.tbMainOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbMainOutput.Size = new System.Drawing.Size(970, 508);
            this.tbMainOutput.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 744);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(2054, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsStatusLabel
            // 
            this.tsStatusLabel.Name = "tsStatusLabel";
            this.tsStatusLabel.Size = new System.Drawing.Size(0, 16);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 30);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnClearAll);
            this.splitContainer1.Panel2.Controls.Add(this.btnSelectAll);
            this.splitContainer1.Panel2.Controls.Add(this.btnTest);
            this.splitContainer1.Panel2.Controls.Add(this.checkedListBox1);
            this.splitContainer1.Panel2.Controls.Add(this.cbDISP_ALL);
            this.splitContainer1.Panel2.Controls.Add(this.cbME);
            this.splitContainer1.Panel2.Controls.Add(this.cbBOTH_OK);
            this.splitContainer1.Panel2.Controls.Add(this.cbBOTH_CLOSED);
            this.splitContainer1.Panel2.Controls.Add(this.cbNOT_CONNECTED_WITH_TMS);
            this.splitContainer1.Panel2.Controls.Add(this.cbASSIGNED_TO_DIFFERENT_PERSON_IN_RM_AND_TMS);
            this.splitContainer1.Panel2.Controls.Add(this.cbDIFFERENT_STATUSES);
            this.splitContainer1.Panel2.Controls.Add(this.cbNOT_EXISTS_IN_RM);
            this.splitContainer1.Panel2.Controls.Add(this.cbNOT_EXISTS_IN_TMS);
            this.splitContainer1.Panel2.Controls.Add(this.button5);
            this.splitContainer1.Panel2.Controls.Add(this.button4);
            this.splitContainer1.Panel2.Controls.Add(this.button3);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Size = new System.Drawing.Size(2054, 714);
            this.splitContainer1.SplitterDistance = 508;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tbMainOutput);
            this.splitContainer2.Size = new System.Drawing.Size(2054, 508);
            this.splitContainer2.SplitterDistance = 1080;
            this.splitContainer2.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.reasonDataGridViewTextBoxColumn,
            this.chDataGridViewCheckBoxColumnMe,
            this.tMSDataGridViewTextBoxColumn,
            this.TMS_ACT_HIST,
            this.RM,
            this.RM_ACT_HIST,
            this.TMS_ASSIGNED,
            this.TMS_STATUS,
            this.RM_ASSIGNED,
            this.RM_STATUS,
            this.AUTO_ACTION});
            this.dataGridView1.DataSource = this.tMSWithReasonBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1080, 508);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // reasonDataGridViewTextBoxColumn
            // 
            this.reasonDataGridViewTextBoxColumn.DataPropertyName = "Reason";
            this.reasonDataGridViewTextBoxColumn.HeaderText = "Reason";
            this.reasonDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.reasonDataGridViewTextBoxColumn.Name = "reasonDataGridViewTextBoxColumn";
            this.reasonDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.reasonDataGridViewTextBoxColumn.Width = 125;
            // 
            // chDataGridViewCheckBoxColumnMe
            // 
            this.chDataGridViewCheckBoxColumnMe.DataPropertyName = "Me";
            this.chDataGridViewCheckBoxColumnMe.FalseValue = "N";
            this.chDataGridViewCheckBoxColumnMe.HeaderText = "ME";
            this.chDataGridViewCheckBoxColumnMe.MinimumWidth = 6;
            this.chDataGridViewCheckBoxColumnMe.Name = "chDataGridViewCheckBoxColumnMe";
            this.chDataGridViewCheckBoxColumnMe.TrueValue = "Y";
            this.chDataGridViewCheckBoxColumnMe.Width = 125;
            // 
            // tMSDataGridViewTextBoxColumn
            // 
            this.tMSDataGridViewTextBoxColumn.DataPropertyName = "TMS";
            this.tMSDataGridViewTextBoxColumn.HeaderText = "TMS Link";
            this.tMSDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.tMSDataGridViewTextBoxColumn.Name = "tMSDataGridViewTextBoxColumn";
            this.tMSDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.tMSDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.tMSDataGridViewTextBoxColumn.Width = 125;
            // 
            // TMS_ACT_HIST
            // 
            this.TMS_ACT_HIST.DataPropertyName = "TMS_ACT_SHOW";
            this.TMS_ACT_HIST.HeaderText = "TMS_ACT";
            this.TMS_ACT_HIST.MinimumWidth = 6;
            this.TMS_ACT_HIST.Name = "TMS_ACT_HIST";
            this.TMS_ACT_HIST.Text = "Show...";
            this.TMS_ACT_HIST.Width = 125;
            // 
            // RM
            // 
            this.RM.DataPropertyName = "RM";
            this.RM.HeaderText = "RM Link";
            this.RM.MinimumWidth = 6;
            this.RM.Name = "RM";
            this.RM.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.RM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.RM.Width = 125;
            // 
            // RM_ACT_HIST
            // 
            this.RM_ACT_HIST.DataPropertyName = "RM_ACT_SHOW";
            this.RM_ACT_HIST.HeaderText = "RM_ACT";
            this.RM_ACT_HIST.MinimumWidth = 6;
            this.RM_ACT_HIST.Name = "RM_ACT_HIST";
            this.RM_ACT_HIST.Width = 125;
            // 
            // TMS_ASSIGNED
            // 
            this.TMS_ASSIGNED.DataPropertyName = "TMS_ASSIGNED";
            this.TMS_ASSIGNED.HeaderText = "TMS Assigned To";
            this.TMS_ASSIGNED.MinimumWidth = 6;
            this.TMS_ASSIGNED.Name = "TMS_ASSIGNED";
            this.TMS_ASSIGNED.Width = 125;
            // 
            // TMS_STATUS
            // 
            this.TMS_STATUS.DataPropertyName = "TMS_STATUS";
            this.TMS_STATUS.HeaderText = "TMS Status";
            this.TMS_STATUS.MinimumWidth = 6;
            this.TMS_STATUS.Name = "TMS_STATUS";
            this.TMS_STATUS.Width = 125;
            // 
            // RM_ASSIGNED
            // 
            this.RM_ASSIGNED.DataPropertyName = "RM_ASSIGNED";
            this.RM_ASSIGNED.HeaderText = "RM Assigned To";
            this.RM_ASSIGNED.MinimumWidth = 6;
            this.RM_ASSIGNED.Name = "RM_ASSIGNED";
            this.RM_ASSIGNED.Width = 125;
            // 
            // RM_STATUS
            // 
            this.RM_STATUS.DataPropertyName = "RM_STATUS";
            this.RM_STATUS.HeaderText = "RM Status";
            this.RM_STATUS.MinimumWidth = 6;
            this.RM_STATUS.Name = "RM_STATUS";
            this.RM_STATUS.Width = 125;
            // 
            // AUTO_ACTION
            // 
            this.AUTO_ACTION.HeaderText = "Auto action";
            this.AUTO_ACTION.MinimumWidth = 6;
            this.AUTO_ACTION.Name = "AUTO_ACTION";
            this.AUTO_ACTION.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AUTO_ACTION.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.AUTO_ACTION.Width = 125;
            // 
            // tMSWithReasonBindingSource
            // 
            this.tMSWithReasonBindingSource.DataMember = "TMSWithReason";
            this.tMSWithReasonBindingSource.DataSource = this.mainDS;
            // 
            // mainDS
            // 
            this.mainDS.DataSetName = "MainDS";
            this.mainDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(1503, 43);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(162, 23);
            this.btnClearAll.TabIndex = 19;
            this.btnClearAll.Text = "Clear all";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(1503, 15);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(162, 23);
            this.btnSelectAll.TabIndex = 18;
            this.btnSelectAll.Text = "Select all";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(507, 134);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(244, 23);
            this.btnTest.TabIndex = 17;
            this.btnTest.Text = "TEST";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "<empty>",
            "New",
            "In Progress",
            "Feedback",
            "Investigated",
            "Closed",
            "Rejected",
            "Reassigned",
            "On Hold"});
            this.checkedListBox1.Location = new System.Drawing.Point(1256, 18);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(241, 174);
            this.checkedListBox1.TabIndex = 16;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // cbDISP_ALL
            // 
            this.cbDISP_ALL.AutoSize = true;
            this.cbDISP_ALL.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cbDISP_ALL.Location = new System.Drawing.Point(1074, 110);
            this.cbDISP_ALL.Name = "cbDISP_ALL";
            this.cbDISP_ALL.Size = new System.Drawing.Size(105, 21);
            this.cbDISP_ALL.TabIndex = 13;
            this.cbDISP_ALL.Text = "Display all";
            this.cbDISP_ALL.UseVisualStyleBackColor = true;
            this.cbDISP_ALL.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // cbME
            // 
            this.cbME.AutoSize = true;
            this.cbME.Location = new System.Drawing.Point(1074, 137);
            this.cbME.Name = "cbME";
            this.cbME.Size = new System.Drawing.Size(152, 21);
            this.cbME.TabIndex = 12;
            this.cbME.Text = "Only Me mentioned";
            this.cbME.UseVisualStyleBackColor = true;
            this.cbME.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // cbBOTH_OK
            // 
            this.cbBOTH_OK.AutoSize = true;
            this.cbBOTH_OK.Location = new System.Drawing.Point(1074, 51);
            this.cbBOTH_OK.Name = "cbBOTH_OK";
            this.cbBOTH_OK.Size = new System.Drawing.Size(83, 21);
            this.cbBOTH_OK.TabIndex = 11;
            this.cbBOTH_OK.Text = "Both OK";
            this.cbBOTH_OK.UseVisualStyleBackColor = true;
            this.cbBOTH_OK.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // cbBOTH_CLOSED
            // 
            this.cbBOTH_CLOSED.AutoSize = true;
            this.cbBOTH_CLOSED.Location = new System.Drawing.Point(1074, 20);
            this.cbBOTH_CLOSED.Name = "cbBOTH_CLOSED";
            this.cbBOTH_CLOSED.Size = new System.Drawing.Size(104, 21);
            this.cbBOTH_CLOSED.TabIndex = 10;
            this.cbBOTH_CLOSED.Text = "Both closed";
            this.cbBOTH_CLOSED.UseVisualStyleBackColor = true;
            this.cbBOTH_CLOSED.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // cbNOT_CONNECTED_WITH_TMS
            // 
            this.cbNOT_CONNECTED_WITH_TMS.AutoSize = true;
            this.cbNOT_CONNECTED_WITH_TMS.Location = new System.Drawing.Point(850, 137);
            this.cbNOT_CONNECTED_WITH_TMS.Name = "cbNOT_CONNECTED_WITH_TMS";
            this.cbNOT_CONNECTED_WITH_TMS.Size = new System.Drawing.Size(183, 21);
            this.cbNOT_CONNECTED_WITH_TMS.TabIndex = 9;
            this.cbNOT_CONNECTED_WITH_TMS.Text = "Not connected with TMS";
            this.cbNOT_CONNECTED_WITH_TMS.UseVisualStyleBackColor = true;
            this.cbNOT_CONNECTED_WITH_TMS.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // cbASSIGNED_TO_DIFFERENT_PERSON_IN_RM_AND_TMS
            // 
            this.cbASSIGNED_TO_DIFFERENT_PERSON_IN_RM_AND_TMS.AutoSize = true;
            this.cbASSIGNED_TO_DIFFERENT_PERSON_IN_RM_AND_TMS.Location = new System.Drawing.Point(850, 78);
            this.cbASSIGNED_TO_DIFFERENT_PERSON_IN_RM_AND_TMS.Name = "cbASSIGNED_TO_DIFFERENT_PERSON_IN_RM_AND_TMS";
            this.cbASSIGNED_TO_DIFFERENT_PERSON_IN_RM_AND_TMS.Size = new System.Drawing.Size(309, 21);
            this.cbASSIGNED_TO_DIFFERENT_PERSON_IN_RM_AND_TMS.TabIndex = 8;
            this.cbASSIGNED_TO_DIFFERENT_PERSON_IN_RM_AND_TMS.Text = "Assigned to different person in RM and TMS";
            this.cbASSIGNED_TO_DIFFERENT_PERSON_IN_RM_AND_TMS.UseVisualStyleBackColor = true;
            this.cbASSIGNED_TO_DIFFERENT_PERSON_IN_RM_AND_TMS.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // cbDIFFERENT_STATUSES
            // 
            this.cbDIFFERENT_STATUSES.AutoSize = true;
            this.cbDIFFERENT_STATUSES.Location = new System.Drawing.Point(850, 110);
            this.cbDIFFERENT_STATUSES.Name = "cbDIFFERENT_STATUSES";
            this.cbDIFFERENT_STATUSES.Size = new System.Drawing.Size(141, 21);
            this.cbDIFFERENT_STATUSES.TabIndex = 7;
            this.cbDIFFERENT_STATUSES.Text = "Different statuses";
            this.cbDIFFERENT_STATUSES.UseVisualStyleBackColor = true;
            this.cbDIFFERENT_STATUSES.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // cbNOT_EXISTS_IN_RM
            // 
            this.cbNOT_EXISTS_IN_RM.AutoSize = true;
            this.cbNOT_EXISTS_IN_RM.Location = new System.Drawing.Point(850, 51);
            this.cbNOT_EXISTS_IN_RM.Name = "cbNOT_EXISTS_IN_RM";
            this.cbNOT_EXISTS_IN_RM.Size = new System.Drawing.Size(131, 21);
            this.cbNOT_EXISTS_IN_RM.TabIndex = 6;
            this.cbNOT_EXISTS_IN_RM.Text = "Not exists in RM";
            this.cbNOT_EXISTS_IN_RM.UseVisualStyleBackColor = true;
            this.cbNOT_EXISTS_IN_RM.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // cbNOT_EXISTS_IN_TMS
            // 
            this.cbNOT_EXISTS_IN_TMS.AutoSize = true;
            this.cbNOT_EXISTS_IN_TMS.Location = new System.Drawing.Point(850, 20);
            this.cbNOT_EXISTS_IN_TMS.Name = "cbNOT_EXISTS_IN_TMS";
            this.cbNOT_EXISTS_IN_TMS.Size = new System.Drawing.Size(139, 21);
            this.cbNOT_EXISTS_IN_TMS.TabIndex = 5;
            this.cbNOT_EXISTS_IN_TMS.Text = "Not exists in TMS";
            this.cbNOT_EXISTS_IN_TMS.UseVisualStyleBackColor = true;
            this.cbNOT_EXISTS_IN_TMS.CheckedChanged += new System.EventHandler(this.cb_CheckedChanged);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(260, 78);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(244, 29);
            this.button5.TabIndex = 4;
            this.button5.Text = "TMS: Clear cache";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(507, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(244, 29);
            this.button4.TabIndex = 3;
            this.button4.Text = "RM2XLS: Add RM data to SD database";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(260, 43);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(244, 29);
            this.button3.TabIndex = 2;
            this.button3.Text = "TMS: Add missing TMS to RM...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(260, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(244, 29);
            this.button2.TabIndex = 1;
            this.button2.Text = "TMS: Show sync. info...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(244, 29);
            this.button1.TabIndex = 0;
            this.button1.Text = "MOM: Add new items (moms.xml)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2054, 766);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tMSWithReasonBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mOMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateItemsbasedOnSingleXLSXFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateItemsbasedOnAllXLSXFileFromTheDirectoryToolStripMenuItem;
        private System.Windows.Forms.TextBox tbMainOutput;
        private System.Windows.Forms.ToolStripMenuItem tMSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showSyncInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addMissingTMSToRMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateRMWithToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem rM2XLSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getIssuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSampleDataToDatabaseToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource tMSWithReasonBindingSource;
        private DataSets.MainDS mainDS;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.CheckBox cbDIFFERENT_STATUSES;
        private System.Windows.Forms.CheckBox cbNOT_EXISTS_IN_RM;
        private System.Windows.Forms.CheckBox cbNOT_EXISTS_IN_TMS;
        private System.Windows.Forms.CheckBox cbASSIGNED_TO_DIFFERENT_PERSON_IN_RM_AND_TMS;
        private System.Windows.Forms.CheckBox cbBOTH_OK;
        private System.Windows.Forms.CheckBox cbBOTH_CLOSED;
        private System.Windows.Forms.CheckBox cbNOT_CONNECTED_WITH_TMS;
        private System.Windows.Forms.CheckBox cbME;
        private System.Windows.Forms.CheckBox cbDISP_ALL;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.DataGridViewTextBoxColumn reasonDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chDataGridViewCheckBoxColumnMe;
        private System.Windows.Forms.DataGridViewButtonColumn tMSDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn TMS_ACT_HIST;
        private System.Windows.Forms.DataGridViewButtonColumn RM;
        private System.Windows.Forms.DataGridViewButtonColumn RM_ACT_HIST;
        private System.Windows.Forms.DataGridViewTextBoxColumn TMS_ASSIGNED;
        private System.Windows.Forms.DataGridViewTextBoxColumn TMS_STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn RM_ASSIGNED;
        private System.Windows.Forms.DataGridViewTextBoxColumn RM_STATUS;
        private System.Windows.Forms.DataGridViewButtonColumn AUTO_ACTION;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnClearAll;
    }
}