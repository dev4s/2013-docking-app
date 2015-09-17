using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace DockingApp
{
	public partial class ApplicationList : Form
	{
		public SystemWindow FirstWindow { get; private set; }
		public SystemWindow SecondWindow { get; private set; }

		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		public ApplicationList()
		{
			InitializeComponent();
		}

		public ApplicationList(SystemWindow firstWindow, SystemWindow secondWindow)
		{
			InitializeComponent();

			if (Settings.Instance.EnableLogger)
				Logger.Debug("(ApplicationList - Constructor) Loading form.");

			FirstWindow = firstWindow;
			SecondWindow = secondWindow;
			
			labelDockedApp1.Text = FirstWindow == null ? "(pusto)" : FirstWindow.FullName;
			labelDockedApp2.Text = SecondWindow == null ? "(pusto)" : SecondWindow.FullName;

			if (Settings.Instance.EnableLogger)
			{
				Logger.Debug("(ApplicationList - Constructor) Previously docked first window: {0}.", labelDockedApp1.Text);
				Logger.Debug("(ApplicationList - Constructor) Previously docked second window: {0}.", labelDockedApp2.Text);
			}

			listBoxOfApps.ValueMember = "Hwnd";
			listBoxOfApps.DisplayMember = "FullName";
		}

		private void ApplicationListLoad(object sender, EventArgs e)
		{
			if (Settings.Instance.EnableLogger)
				Logger.Debug("(ApplicationList - Load) Fill list box.");

			FillListBoxWithApps(false);
			FillListBoxWithFilters();
		}
		
		private void ButtonRefreshAppsListClick(object sender, EventArgs e)
		{
			if (Settings.Instance.EnableLogger)
				Logger.Debug("(ApplicationList - ButtonRefreshAppsListClick) Refreshing apps list (show all = {0}).", checkBoxShowAll.CheckState == CheckState.Checked);

			var checkBoxState = checkBoxShowAll.CheckState;
			FillListBoxDependsOnCheckBoxState(checkBoxState);
		}

		private void ButtonDockToPanel1Click(object sender, EventArgs e)
		{
			if (Settings.Instance.EnableLogger)
				Logger.Debug("(ApplicationList - ButtonDockToPanel1Click) Dock to panel1 click.");

			SetProcessAndLabel(labelDockedApp1, PanelEnum.First);
		}

		private void ButtonDockToPanel2Click(object sender, EventArgs e)
		{
			if (Settings.Instance.EnableLogger)
				Logger.Debug("(ApplicationList - ButtonDockToPanel2Click) Dock to panel2 click.");

			SetProcessAndLabel(labelDockedApp2, PanelEnum.Second);
		}

		private void ButtonCleanDockClick(object sender, EventArgs e)
		{
			if (Settings.Instance.EnableLogger)
				Logger.Debug("(ApplicationList - ButtonCleanDockClick) Clean dockings click.");

			if (FirstWindow != null && FirstWindow.Docked)
			{
				FirstWindow.UndockFromPanel();
			}

			if (SecondWindow != null && SecondWindow.Docked)
			{
				SecondWindow.UndockFromPanel();
			}

			FirstWindow = SecondWindow = null;
			labelDockedApp1.Text = labelDockedApp2.Text = @"(pusto)";
		}

		private void CheckBoxShowAllCheckedChanged(object sender, EventArgs e)
		{
			var checkBoxState = ((CheckBox)sender).CheckState;

			if (Settings.Instance.EnableLogger)
				Logger.Debug("(ApplicationList - CheckBoxShowAllCheckedChanged) Check changed (show all: {0}).", checkBoxState == CheckState.Checked);
			
			FillListBoxDependsOnCheckBoxState(checkBoxState);
		}

		private void ButtonOkClick(object sender, EventArgs e)
		{
			if (Settings.Instance.EnableLogger)
				Logger.Debug("(ApplicationList - ButtonOkClick) Saving first window settings.");

			if (FirstWindow != null)
			{
				Settings.Instance.Windows[0] = new Window(FirstWindow.Title, FirstWindow.ClassName);
			}
			else
			{
				Settings.Instance.Windows[0] = null;
			}

			if (Settings.Instance.EnableLogger)
				Logger.Debug("(ApplicationList - ButtonOkClick) Saving second window settings.");

			if (SecondWindow != null)
			{
				Settings.Instance.Windows[1] = new Window(SecondWindow.Title, SecondWindow.ClassName);
			}
			else
			{
				Settings.Instance.Windows[1] = null;
			}
		}

		private bool _firstRun = true;
		private bool _checkBoxState;
		private void listBoxOfFilters_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_firstRun)
			{
				_firstRun = false;
			}
			else
			{
				var selectedItem = (string) listBoxOfFilters.SelectedItem;
				
				if (selectedItem == "(wszystkie)")
				{
					FillListBoxWithApps(_checkBoxState);
				}
				else
				{
					FillListBoxWithApps(_checkBoxState, selectedItem);
				}
			}
		}

		private void SetProcessAndLabel(Control label, PanelEnum panelEnum)
		{
			if (listBoxOfApps.SelectedItem == null) return;
			var selectedItem = (SystemWindow)listBoxOfApps.SelectedItem;

			if (Settings.Instance.EnableLogger)
				Logger.Debug("(ApplicationList - SetProcessAndLabel) Preparing app to dock ({0})", selectedItem.FullName);

			switch (panelEnum)
			{
				case PanelEnum.First:
					if (!selectedItem.Equals(SecondWindow))
					{
						FirstWindow = (SystemWindow)listBoxOfApps.SelectedItem;
						label.Text = FirstWindow.FullName;
					}
					break;

				case PanelEnum.Second:
					if (!selectedItem.Equals(FirstWindow))
					{
						SecondWindow = (SystemWindow)listBoxOfApps.SelectedItem;
						label.Text = SecondWindow.FullName;
					}
					break;
			}
		}

		private void FillListBoxWithFilters()
		{
			listBoxOfFilters.Items.Add("(wszystkie)");

			foreach (var filter in Settings.Instance.Filters)
			{
				listBoxOfFilters.Items.Add(filter);
			}

			listBoxOfFilters.SelectedIndex = 0;
		}

		private void FillListBoxWithApps(bool showAll, string selectedItem = null)
		{
			Task.Factory.StartNew(() =>
			{
				labelPleaseWaitLoading.InvokeIfRequired(() => labelPleaseWaitLoading.Show());

				listBoxOfApps.InvokeIfRequired(() => listBoxOfApps.Items.Clear());

				var appsList = selectedItem != null 
					? SystemWindow.GetAllWindows(showAll).OrderBy(x => x.FullName).Where(x => x.FullName.Contains(selectedItem)) 
					: SystemWindow.GetAllWindows(showAll).OrderBy(x => x.FullName);
				
				foreach (var window in appsList)
				{
					listBoxOfApps.InvokeIfRequired(x => listBoxOfApps.Items.Add(x), window);
				}

				if (listBoxOfApps.Items.Count > 0)
				{
					listBoxOfApps.InvokeIfRequired(() => listBoxOfApps.SelectedIndex = 0);
				}

				labelPleaseWaitLoading.InvokeIfRequired(() => labelPleaseWaitLoading.Hide());
			});
		}

		private void FillListBoxDependsOnCheckBoxState(CheckState checkBoxState)
		{
			switch (checkBoxState)
			{
				case CheckState.Checked:
					_checkBoxState = true;
					break;

				case CheckState.Unchecked:
					_checkBoxState = false;
					break;
			}

			listBoxOfFilters_SelectedIndexChanged("(wszystkie)", EventArgs.Empty);
		}
	}
}
