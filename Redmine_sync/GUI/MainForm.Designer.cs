
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
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbMainOutput = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.addSampleDataToDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
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
            this.mOMToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.mOMToolStripMenuItem.Text = "MOM";
            // 
            // addNewItemsToolStripMenuItem
            // 
            this.addNewItemsToolStripMenuItem.Name = "addNewItemsToolStripMenuItem";
            this.addNewItemsToolStripMenuItem.Size = new System.Drawing.Size(462, 26);
            this.addNewItemsToolStripMenuItem.Text = "Add new items...";
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
            this.tMSToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
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
            this.rM2XLSToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.rM2XLSToolStripMenuItem.Text = "RM2XLS";
            // 
            // getIssuesToolStripMenuItem
            // 
            this.getIssuesToolStripMenuItem.Name = "getIssuesToolStripMenuItem";
            this.getIssuesToolStripMenuItem.Size = new System.Drawing.Size(347, 26);
            this.getIssuesToolStripMenuItem.Text = "Assigned to DEV1 w/o MOM Problems";
            this.getIssuesToolStripMenuItem.Click += new System.EventHandler(this.getIssuesToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.aboutToolStripMenuItem.Text = "About...";
            // 
            // tbMainOutput
            // 
            this.tbMainOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMainOutput.Location = new System.Drawing.Point(0, 30);
            this.tbMainOutput.Multiline = true;
            this.tbMainOutput.Name = "tbMainOutput";
            this.tbMainOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbMainOutput.Size = new System.Drawing.Size(1366, 625);
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
            // addSampleDataToDatabaseToolStripMenuItem
            // 
            this.addSampleDataToDatabaseToolStripMenuItem.Name = "addSampleDataToDatabaseToolStripMenuItem";
            this.addSampleDataToDatabaseToolStripMenuItem.Size = new System.Drawing.Size(347, 26);
            this.addSampleDataToDatabaseToolStripMenuItem.Text = "Add sample data to database";
            this.addSampleDataToDatabaseToolStripMenuItem.Click += new System.EventHandler(this.addSampleDataToDatabaseToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 680);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tbMainOutput);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
    }
}