using System.IO;
using System.Windows.Forms;
using NLog;
using Newtonsoft.Json;

namespace DockingApp
{
	public class Settings
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		
		private class TempSettings
		{
			public Window[] Windows { get; private set; }
			public int SplitterDistance { get; private set; }
			public bool EnableLogger { get; private set; }
			public string Version { get; private set; }
			public string[] Filters { get; private set; }

			public TempSettings(Window[] windows, int splitterDistance, bool enableLogger, string version, string[] filters)
			{
				Windows = windows;
				SplitterDistance = splitterDistance;
				EnableLogger = enableLogger;
				Version = version;
				Filters = filters;
			}
		}

		private const int WindowsLength = 2;

		private Settings() {}

		private static bool _firstRead = true;

		private static Settings _instance;
		public static Settings Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new Settings();

					if (_firstRead)
					{
						CreateEmptySettingsFile();
						ReadingFromFile();
					}

					if (_instance.Version != Extensions.GetVersion() || string.IsNullOrEmpty(_instance.Version) || _instance.Windows.Length > WindowsLength)
					{
						DeleteOldSettingsFile();
						CreateEmptySettingsFile();
						ReadingFromFile();
					}
				}

				return _instance;
			}
		}

		private static void ReadingFromFile()
		{
			Logger.Debug("(Settings - ReadingFromFile) Reading settings file.");

			TempSettings tempSettings;
			
			if (!DeserializeTempSettings(out tempSettings))
			{
				DeleteOldSettingsFile();
				CreateEmptySettingsFile();
				DeserializeTempSettings(out tempSettings);
			}

			_instance.Windows = tempSettings.Windows;
			_instance.SplitterDistance = tempSettings.SplitterDistance;
			_instance.EnableLogger = tempSettings.EnableLogger;
			_instance.Version = tempSettings.Version;
			_instance.Filters = tempSettings.Filters;

			_firstRead = false;
		}

		private static bool DeserializeTempSettings(out TempSettings tempSettings)
		{
			var serializer = new JsonSerializer();
			StreamReader sr = null;

			try
			{
				sr = new StreamReader(FullFilePath);
				using (var jreader = new JsonTextReader(sr))
				{
					sr = null;
					try
					{
						tempSettings = (TempSettings)serializer.Deserialize(jreader, typeof(TempSettings));
					}
					catch (JsonSerializationException)
					{
						tempSettings = null;
						return false;
					}
				}
			}
			finally
			{
				if (sr != null)
				{
					sr.Dispose();
				}
			}

			return true;
		}

		private static void DeleteOldSettingsFile()
		{
			var file = new FileInfo(FullFilePath);
			file.Delete();
		}

		private static void CreateEmptySettingsFile()
		{
			var file = new FileInfo(FullFilePath);
			if (file.Exists) return;

			Logger.Debug("(Settings - CreateEmptySettingsFile) Creating empty settings.");

			using (var sw = file.CreateText())
			{
				sw.WriteLine("{{\"Windows\":[null,null],\"SplitterDistance\":0,\"EnableLogger\":true," +
							 "\"Version\":\"{0}\",\"Filters\":[\"Chrome\",\"Excel\",\"Streamsoft\"]}}", 
							 Extensions.GetVersion());
			}
		}

		public Window[] Windows { get; private set; }
		public int SplitterDistance { get; private set; }
		public bool EnableLogger { get; private set; }
		public string Version { get; private set; }
		public string[] Filters { get; private set; }

		private static string FullFilePath
		{
			get { return Application.StartupPath + "\\settings.config"; }
		}

		public void Save(int splitterDistance)
		{
			Logger.Debug("(Settings - Save) Saving settings.");

			SplitterDistance = splitterDistance;

			var serializer = new JsonSerializer();
			StreamWriter sw = null;

			try
			{
				sw = new StreamWriter(FullFilePath);

				using (var jwriter = new JsonTextWriter(sw))
				{
					sw = null;
					var tempSettings = new TempSettings(Windows, SplitterDistance, EnableLogger, Extensions.GetVersion(), Filters);
					serializer.Serialize(jwriter, tempSettings);
				}
			}
			finally
			{
				if (sw != null)
				{
					sw.Dispose();
				}
			}
		}

		public void Clean()
		{
			Logger.Debug("(Settings - Clean) Cleaning settings.");

			Windows = new Window[WindowsLength];
		}
	}
}