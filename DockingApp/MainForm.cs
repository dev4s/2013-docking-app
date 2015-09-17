using System;
using System.Windows.Forms;
using NLog;

namespace DockingApp
{
	public partial class MainForm : Form
	{
		private SystemWindow _firstWindow;
		private SystemWindow _secondWindow;

		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainFormLoad(object sender, EventArgs e)
		{
			if (Settings.Instance.EnableLogger)
				Logger.Debug("(MainForm - Load) Loading form.");

			var bitType = Environment.Is64BitOperatingSystem ? "64-bit" : "32-bit";
			var showText = string.Format("OS: {0} ({1})", Environment.OSVersion, bitType);

			if (Settings.Instance.EnableLogger)
				Logger.Debug("(MainForm - Load) Environment: OS {0} ({1})", Environment.OSVersion, bitType);

			toolStripStatusLabel.Text = showText;

			if (Settings.Instance.EnableLogger)
				Logger.Debug("(MainForm - Load) Application version: {0}", Settings.Instance.Version);

			Text += string.Format(" - {0}", Settings.Instance.Version);
		}

		private void MainFormShown(object sender, EventArgs e)
		{
			if (Settings.Instance.EnableLogger)
				Logger.Debug("(MainForm - Shown) Showing form.");

			var windows = Settings.Instance.Windows;

			if (Settings.Instance.EnableLogger)
				Logger.Debug("(MainForm - Shown) Windows names: {0}, {1}", windows[0] == null ? "(empty)" : windows[0].Title, windows[1] == null ? "(empty)" : windows[1].Title);

			if (Settings.Instance.EnableLogger)
				Logger.Debug("(MainForm - Shown) Docking window to first panel.");

			DockingSavedWindows(windows[0], PanelEnum.First);

			if (Settings.Instance.EnableLogger)
				Logger.Debug("(MainForm - Shown) Docking window to second panel.");

			DockingSavedWindows(windows[1], PanelEnum.Second);

			if (Settings.Instance.SplitterDistance == 0) return;
			if (Settings.Instance.EnableLogger)
				Logger.Debug("(MainForm - Shown) Setting splitter distance: {0}%", Settings.Instance.SplitterDistance);

			splitContainerApps.SplitterDistance = ConvertToPx(Settings.Instance.SplitterDistance, splitContainerApps.Width);
		}

		private int ConvertToPercentage(int x, int width)
		{
			return (x * 100) / width;
		}

		private static int ConvertToPx(int x, int width)
		{
			return (width * x) / 100;
		}

		private void DockingSavedWindows(Window window, PanelEnum panelEnum)
		{
			if (window == null) return;

			var w = new SystemWindow(window.ClassName, window.Title);

			if (Settings.Instance.EnableLogger)
				Logger.Debug("(MainForm - DockingSavedWindows) Window exists: {0}", w.Exists);

			if (!w.Exists) return;
			DockToPanel(w, panelEnum);

			switch (panelEnum)
			{
				case PanelEnum.First:
					_firstWindow = w;
					break;
				case PanelEnum.Second:
					_secondWindow = w;
					break;
			}
		}

		private void ToolStripMenuItemApplicationListClick(object sender, EventArgs e)
		{
			if (Settings.Instance.EnableLogger)
				Logger.Debug("(MainForm - ToolStripMenuItemApplicationListClick) Application menu click.");

			var appsList = new ApplicationList(_firstWindow, _secondWindow);
			if (appsList.ShowDialog() != DialogResult.OK) return;

			if (Settings.Instance.EnableLogger)
				Logger.Debug("(MainForm - ToolStripMenuItemApplicationListClick) Saving configuration.");

			Settings.Instance.Save(ConvertToPercentage(splitContainerApps.SplitterDistance, splitContainerApps.Width));

			_firstWindow = appsList.FirstWindow;
			_secondWindow = appsList.SecondWindow;

			if (Settings.Instance.EnableLogger)
			{
				Logger.Debug("(MainForm - ToolStripMenuItemApplicationListClick) Selected first window: {0}", _firstWindow == null ? "(empty)" : _firstWindow.FullName);
				Logger.Debug("(MainForm - ToolStripMenuItemApplicationListClick) Selected second window: {0}", _secondWindow == null ? "(empty)" : _secondWindow.FullName);
				
			}

			if (_firstWindow != null)
			{
				DockToPanel(_firstWindow, PanelEnum.First);
			}

			if (_secondWindow != null)
			{
				DockToPanel(_secondWindow, PanelEnum.Second);
			}
		}

		private void ToolStripMenuItemCleanApplicationClick(object sender, EventArgs e)
		{
			if (Settings.Instance.EnableLogger)
				Logger.Debug("(MainForm - ToolStripMenuItemCleanApplicationClick) Clean application click.");

			UndockAll();

			Settings.Instance.Clean();
			Settings.Instance.Save(ConvertToPercentage(splitContainerApps.SplitterDistance, splitContainerApps.Width));
		}

		private void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (Settings.Instance.EnableLogger)
				Logger.Debug("(MainForm - Closing) Closing application.");

			Settings.Instance.Save(ConvertToPercentage(splitContainerApps.SplitterDistance, splitContainerApps.Width));
			UndockAll();
		}

		private void UndockAll()
		{
			if (Settings.Instance.EnableLogger)
				Logger.Debug("(MainForm - UndockAll) Undocking applications.");

			if (_firstWindow != null)
			{
				UndockFromPanel(ref _firstWindow);
			}

			if (_secondWindow != null)
			{
				UndockFromPanel(ref _secondWindow);
			}
		}

		private bool _firstEventAssigned;
		private bool _secondEventAssigned;
		private void DockToPanel(SystemWindow systemWindow, PanelEnum panel)
		{
			if (Settings.Instance.EnableLogger)
				Logger.Debug("(MainForm - DockToPanel) Docking window: {0}.", systemWindow.FullName);

			if (Settings.Instance.EnableLogger)
				Logger.Debug("(MainForm - DockToPanel) Maximizing window.");

			if (systemWindow.Parent != null && systemWindow.Parent.IsMinimized)
			{
				if (Settings.Instance.EnableLogger)
					Logger.Debug("(MainForm - DockToPanel) Maximizing parent for Delphi applications.");

				systemWindow.Parent.Restore();
			}
			else
			{
				systemWindow.Restore();
			}
			systemWindow.Maximize();

			switch (panel)
			{
				case PanelEnum.First:
					if (Settings.Instance.EnableLogger)
						Logger.Debug("(MainForm - DockToPanel) Docking first window: {0}.", systemWindow.FullName);

					systemWindow.DockToPanel(splitContainerApps.Panel1);
					if (_firstEventAssigned == false)
					{
						splitContainerApps.Panel1.SizeChanged += systemWindow.PanelSizeChanged;
						_firstEventAssigned = true;
					}
					break;

				case PanelEnum.Second:
					if (Settings.Instance.EnableLogger)
						Logger.Debug("(MainForm - DockToPanel) Docking second window: {0}.", systemWindow.FullName);

					systemWindow.DockToPanel(splitContainerApps.Panel2);
					if (_secondEventAssigned == false)
					{
						splitContainerApps.Panel2.SizeChanged += systemWindow.PanelSizeChanged;
						_secondEventAssigned = true;
					}
					break;
			}

			systemWindow.PanelSizeChanged(panel == PanelEnum.First ? splitContainerApps.Panel1 : splitContainerApps.Panel2, new EventArgs());

			if (Settings.Instance.EnableLogger)
				Logger.Debug("(MainForm - DockToPanel) Docking windows ends.");
		}

		private static void UndockFromPanel(ref SystemWindow systemWindow)
		{
			systemWindow.UndockFromPanel();

			if (Settings.Instance.EnableLogger)
				Logger.Debug("(MainForm - UndockFromPanel) Maximizing window after undocking.");

			systemWindow.Maximize();

			systemWindow = null;
		}
	}
}
