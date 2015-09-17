namespace DockingApp
{
	partial class ApplicationList
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
			this.buttonDockToPanel1 = new System.Windows.Forms.Button();
			this.buttonDockToPanel2 = new System.Windows.Forms.Button();
			this.listBoxOfApps = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labelDockedApp2 = new System.Windows.Forms.Label();
			this.labelDockedApp1 = new System.Windows.Forms.Label();
			this.buttonCleanDock = new System.Windows.Forms.Button();
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonRefreshAppsList = new System.Windows.Forms.Button();
			this.checkBoxShowAll = new System.Windows.Forms.CheckBox();
			this.labelPleaseWaitLoading = new System.Windows.Forms.Label();
			this.listBoxOfFilters = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// buttonDockToPanel1
			// 
			this.buttonDockToPanel1.Location = new System.Drawing.Point(12, 10);
			this.buttonDockToPanel1.Name = "buttonDockToPanel1";
			this.buttonDockToPanel1.Size = new System.Drawing.Size(124, 23);
			this.buttonDockToPanel1.TabIndex = 1;
			this.buttonDockToPanel1.Text = "Przypnij do panelu &1";
			this.buttonDockToPanel1.UseVisualStyleBackColor = true;
			this.buttonDockToPanel1.Click += new System.EventHandler(this.ButtonDockToPanel1Click);
			// 
			// buttonDockToPanel2
			// 
			this.buttonDockToPanel2.Location = new System.Drawing.Point(142, 10);
			this.buttonDockToPanel2.Name = "buttonDockToPanel2";
			this.buttonDockToPanel2.Size = new System.Drawing.Size(124, 23);
			this.buttonDockToPanel2.TabIndex = 2;
			this.buttonDockToPanel2.Text = "Przypnij do panelu &2";
			this.buttonDockToPanel2.UseVisualStyleBackColor = true;
			this.buttonDockToPanel2.Click += new System.EventHandler(this.ButtonDockToPanel2Click);
			// 
			// listBoxOfApps
			// 
			this.listBoxOfApps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBoxOfApps.FormattingEnabled = true;
			this.listBoxOfApps.Location = new System.Drawing.Point(249, 39);
			this.listBoxOfApps.Name = "listBoxOfApps";
			this.listBoxOfApps.Size = new System.Drawing.Size(473, 186);
			this.listBoxOfApps.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 230);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(220, 13);
			this.label1.TabIndex = 10;
			this.label1.Text = "Przypięta aplikacja do panelu 1 (lewa strona):";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 247);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(227, 13);
			this.label2.TabIndex = 11;
			this.label2.Text = "Przypięta aplikacja do panelu 2 (prawa strona):";
			// 
			// labelDockedApp2
			// 
			this.labelDockedApp2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelDockedApp2.AutoSize = true;
			this.labelDockedApp2.Location = new System.Drawing.Point(246, 247);
			this.labelDockedApp2.Name = "labelDockedApp2";
			this.labelDockedApp2.Size = new System.Drawing.Size(39, 13);
			this.labelDockedApp2.TabIndex = 9;
			this.labelDockedApp2.Text = "(pusto)";
			// 
			// labelDockedApp1
			// 
			this.labelDockedApp1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelDockedApp1.AutoSize = true;
			this.labelDockedApp1.Location = new System.Drawing.Point(246, 230);
			this.labelDockedApp1.Name = "labelDockedApp1";
			this.labelDockedApp1.Size = new System.Drawing.Size(39, 13);
			this.labelDockedApp1.TabIndex = 8;
			this.labelDockedApp1.Text = "(pusto)";
			// 
			// buttonCleanDock
			// 
			this.buttonCleanDock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCleanDock.Location = new System.Drawing.Point(568, 10);
			this.buttonCleanDock.Name = "buttonCleanDock";
			this.buttonCleanDock.Size = new System.Drawing.Size(154, 23);
			this.buttonCleanDock.TabIndex = 5;
			this.buttonCleanDock.Text = "&Wyczyść przypięte aplikacje";
			this.buttonCleanDock.UseVisualStyleBackColor = true;
			this.buttonCleanDock.Click += new System.EventHandler(this.ButtonCleanDockClick);
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOk.Location = new System.Drawing.Point(566, 265);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 6;
			this.buttonOk.Text = "Ok";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.ButtonOkClick);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(647, 265);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 7;
			this.buttonCancel.Text = "Anuluj";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// buttonRefreshAppsList
			// 
			this.buttonRefreshAppsList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRefreshAppsList.Location = new System.Drawing.Point(436, 10);
			this.buttonRefreshAppsList.Name = "buttonRefreshAppsList";
			this.buttonRefreshAppsList.Size = new System.Drawing.Size(126, 23);
			this.buttonRefreshAppsList.TabIndex = 4;
			this.buttonRefreshAppsList.Text = "O&dśwież listę aplikacji";
			this.buttonRefreshAppsList.UseVisualStyleBackColor = true;
			this.buttonRefreshAppsList.Click += new System.EventHandler(this.ButtonRefreshAppsListClick);
			// 
			// checkBoxShowAll
			// 
			this.checkBoxShowAll.AutoSize = true;
			this.checkBoxShowAll.Location = new System.Drawing.Point(279, 14);
			this.checkBoxShowAll.Name = "checkBoxShowAll";
			this.checkBoxShowAll.Size = new System.Drawing.Size(141, 17);
			this.checkBoxShowAll.TabIndex = 3;
			this.checkBoxShowAll.Text = "Pokaż wszystkie \"okna\"";
			this.checkBoxShowAll.UseVisualStyleBackColor = true;
			this.checkBoxShowAll.CheckedChanged += new System.EventHandler(this.CheckBoxShowAllCheckedChanged);
			// 
			// labelPleaseWaitLoading
			// 
			this.labelPleaseWaitLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.labelPleaseWaitLoading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelPleaseWaitLoading.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.labelPleaseWaitLoading.ForeColor = System.Drawing.Color.Red;
			this.labelPleaseWaitLoading.Location = new System.Drawing.Point(138, 99);
			this.labelPleaseWaitLoading.Name = "labelPleaseWaitLoading";
			this.labelPleaseWaitLoading.Size = new System.Drawing.Size(466, 58);
			this.labelPleaseWaitLoading.TabIndex = 12;
			this.labelPleaseWaitLoading.Text = "Proszę czekać, trwa wczytywanie listy okien...";
			this.labelPleaseWaitLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// listBoxOfFilters
			// 
			this.listBoxOfFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.listBoxOfFilters.FormattingEnabled = true;
			this.listBoxOfFilters.Location = new System.Drawing.Point(12, 39);
			this.listBoxOfFilters.Name = "listBoxOfFilters";
			this.listBoxOfFilters.Size = new System.Drawing.Size(231, 186);
			this.listBoxOfFilters.TabIndex = 13;
			this.listBoxOfFilters.SelectedIndexChanged += new System.EventHandler(this.listBoxOfFilters_SelectedIndexChanged);
			// 
			// ApplicationList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(734, 302);
			this.Controls.Add(this.labelPleaseWaitLoading);
			this.Controls.Add(this.listBoxOfFilters);
			this.Controls.Add(this.checkBoxShowAll);
			this.Controls.Add(this.buttonRefreshAppsList);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.buttonCleanDock);
			this.Controls.Add(this.labelDockedApp1);
			this.Controls.Add(this.labelDockedApp2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listBoxOfApps);
			this.Controls.Add(this.buttonDockToPanel2);
			this.Controls.Add(this.buttonDockToPanel1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(750, 340);
			this.Name = "ApplicationList";
			this.Text = "Lista aplikacji";
			this.Load += new System.EventHandler(this.ApplicationListLoad);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonDockToPanel1;
		private System.Windows.Forms.Button buttonDockToPanel2;
		private System.Windows.Forms.ListBox listBoxOfApps;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelDockedApp2;
		private System.Windows.Forms.Label labelDockedApp1;
		private System.Windows.Forms.Button buttonCleanDock;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonRefreshAppsList;
		private System.Windows.Forms.CheckBox checkBoxShowAll;
		private System.Windows.Forms.Label labelPleaseWaitLoading;
		private System.Windows.Forms.ListBox listBoxOfFilters;
	}
}