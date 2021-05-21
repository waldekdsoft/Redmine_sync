
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
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.mainDS = new Redmine_sync.DataSets.MainDS();
            this.tMSWithReasonBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reasonDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tMSDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.textDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.mainDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tMSWithReasonBindingSource)).BeginInit();
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
            this.menuStrip1.Size = new System.Drawing.Size(1366, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
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
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
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
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
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
            this.tbMainOutput.Size = new System.Drawing.Size(643, 551);
            this.tbMainOutput.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 658);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1366, 22);
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button4);
            this.splitContainer1.Panel2.Controls.Add(this.button3);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Size = new System.Drawing.Size(1366, 630);
            this.splitContainer1.SplitterDistance = 551;
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
            this.splitContainer2.Size = new System.Drawing.Size(1366, 551);
            this.splitContainer2.SplitterDistance = 719;
            this.splitContainer2.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.reasonDataGridViewTextBoxColumn,
            this.tMSDataGridViewTextBoxColumn,
            this.textDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.tMSWithReasonBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(719, 551);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(255, 42);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(237, 29);
            this.button4.TabIndex = 3;
            this.button4.Text = "Add RM data to SD database";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(255, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(237, 29);
            this.button3.TabIndex = 2;
            this.button3.Text = "Add missing TMS to RM...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(14, 42);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(235, 29);
            this.button2.TabIndex = 1;
            this.button2.Text = "Show sync. info...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(237, 29);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add new items (moms.xml)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mainDS
            // 
            this.mainDS.DataSetName = "MainDS";
            this.mainDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tMSWithReasonBindingSource
            // 
            this.tMSWithReasonBindingSource.DataMember = "TMSWithReason";
            this.tMSWithReasonBindingSource.DataSource = this.mainDS;
            // 
            // reasonDataGridViewTextBoxColumn
            // 
            this.reasonDataGridViewTextBoxColumn.DataPropertyName = "Reason";
            this.reasonDataGridViewTextBoxColumn.HeaderText = "Reason";
            this.reasonDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.reasonDataGridViewTextBoxColumn.Name = "reasonDataGridViewTextBoxColumn";
            this.reasonDataGridViewTextBoxColumn.Width = 125;
            // 
            // tMSDataGridViewTextBoxColumn
            // 
            this.tMSDataGridViewTextBoxColumn.DataPropertyName = "TMS";
            this.tMSDataGridViewTextBoxColumn.HeaderText = "TMS";
            this.tMSDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.tMSDataGridViewTextBoxColumn.Name = "tMSDataGridViewTextBoxColumn";
            this.tMSDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.tMSDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.tMSDataGridViewTextBoxColumn.Width = 125;
            // 
            // textDataGridViewTextBoxColumn
            // 
            this.textDataGridViewTextBoxColumn.DataPropertyName = "Text";
            this.textDataGridViewTextBoxColumn.HeaderText = "Text";
            this.textDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.textDataGridViewTextBoxColumn.Name = "textDataGridViewTextBoxColumn";
            this.textDataGridViewTextBoxColumn.Width = 125;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 680);
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tMSWithReasonBindingSource)).EndInit();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn reasonDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewLinkColumn tMSDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn textDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource tMSWithReasonBindingSource;
        private DataSets.MainDS mainDS;
    }
}