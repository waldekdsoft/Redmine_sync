
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
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mOMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateItemsbasedOnSingleXLSXFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateItemsbasedOnAllXLSXFileFromTheDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbMainOutput = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(55, 26);
            this.fileToolStripMenuItem.Text = "File...";
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mOMToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(72, 26);
            this.actionsToolStripMenuItem.Text = "Actions";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(73, 26);
            this.aboutToolStripMenuItem.Text = "About...";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
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
            // tbMainOutput
            // 
            this.tbMainOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMainOutput.Location = new System.Drawing.Point(0, 28);
            this.tbMainOutput.Multiline = true;
            this.tbMainOutput.Name = "tbMainOutput";
            this.tbMainOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbMainOutput.Size = new System.Drawing.Size(1366, 652);
            this.tbMainOutput.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 680);
            this.Controls.Add(this.tbMainOutput);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
    }
}