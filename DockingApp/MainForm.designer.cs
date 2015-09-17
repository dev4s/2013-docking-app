namespace DockingApp
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
			this.splitContainerApps = new System.Windows.Forms.SplitContainer();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.toolStripMenuItemApplicationList = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemCleanApplication = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerApps)).BeginInit();
			this.splitContainerApps.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainerApps
			// 
			this.splitContainerApps.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerApps.Location = new System.Drawing.Point(0, 24);
			this.splitContainerApps.Name = "splitContainerApps";
			// 
			// splitContainerApps.Panel1
			// 
			this.splitContainerApps.Panel1.Tag = "Panel1";
			// 
			// splitContainerApps.Panel2
			// 
			this.splitContainerApps.Panel2.Tag = "Panel2";
			this.splitContainerApps.Size = new System.Drawing.Size(648, 414);
			this.splitContainerApps.SplitterDistance = 324;
			this.splitContainerApps.SplitterWidth = 5;
			this.splitContainerApps.TabIndex = 1;
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemApplicationList,
            this.ToolStripMenuItemCleanApplication});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(648, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip1";
			// 
			// toolStripMenuItemApplicationList
			// 
			this.toolStripMenuItemApplicationList.Name = "toolStripMenuItemApplicationList";
			this.toolStripMenuItemApplicationList.Size = new System.Drawing.Size(89, 20);
			this.toolStripMenuItemApplicationList.Text = "&Lista aplikacji";
			this.toolStripMenuItemApplicationList.Click += new System.EventHandler(this.ToolStripMenuItemApplicationListClick);
			// 
			// ToolStripMenuItemCleanApplication
			// 
			this.ToolStripMenuItemCleanApplication.Name = "ToolStripMenuItemCleanApplication";
			this.ToolStripMenuItemCleanApplication.Size = new System.Drawing.Size(142, 20);
			this.ToolStripMenuItemCleanApplication.Text = "&Wyczyść panel aplikacji";
			this.ToolStripMenuItemCleanApplication.Click += new System.EventHandler(this.ToolStripMenuItemCleanApplicationClick);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
			this.statusStrip.Location = new System.Drawing.Point(0, 438);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(648, 22);
			this.statusStrip.TabIndex = 2;
			this.statusStrip.Text = "statusStrip1";
			// 
			// toolStripStatusLabel
			// 
			this.toolStripStatusLabel.Name = "toolStripStatusLabel";
			this.toolStripStatusLabel.Size = new System.Drawing.Size(36, 17);
			this.toolStripStatusLabel.Text = "(info)";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(648, 460);
			this.Controls.Add(this.splitContainerApps);
			this.Controls.Add(this.menuStrip);
			this.Controls.Add(this.statusStrip);
			this.MainMenuStrip = this.menuStrip;
			this.Name = "MainForm";
			this.Text = "Docking app";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.Shown += new System.EventHandler(this.MainFormShown);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerApps)).EndInit();
			this.splitContainerApps.ResumeLayout(false);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainerApps;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemApplicationList;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCleanApplication;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
	}
}

