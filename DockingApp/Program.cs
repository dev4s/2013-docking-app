using System;
using System.Windows.Forms;
using NLog;

namespace DockingApp
{
	static class Program
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Logger.Debug("Application starting.");

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());

			Logger.Debug("Application stoping.");
		}
	}
}
