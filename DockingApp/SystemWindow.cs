using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;

namespace DockingApp
{
	public class SystemWindow : IDisposable
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		#region Statics
		private const string DelphiParentApplication = "TApplication";
		private static IList<SystemWindow> _possibleParents;

		public static IEnumerable<SystemWindow> GetAllWindows(bool showAll)
		{
			var allWindows = NativeMethods.EnumWindows().Select(n => new SystemWindow(n)).ToList();

			if (Settings.Instance.EnableLogger)
			{
				Logger.Debug("GetAllWindows - items Count: {0}", allWindows.Count);
			}

			_possibleParents = allWindows.Where(x => x.ClassName == DelphiParentApplication).ToList();

			if (Settings.Instance.EnableLogger)
			{
				Logger.Debug("GetAllWindows - possible parents Count: {0}", _possibleParents.Count);

				foreach (var parent in _possibleParents)
				{
					Logger.Debug("GetAllWindows - possible parent: {0}", parent.FullName);
				}
			}
			
			var filteredWindows = allWindows.Where(x => !string.IsNullOrEmpty(x.Title) && x.ClassName != DelphiParentApplication && x.ClassName != "MSCTFIME UI" && x.ClassName != "IME" && x.ClassName != "msseces_class" && x.ClassName != "Progman" && x.Process.ProcessName != "ctfmon" && x.Process.ProcessName != "DockingApp").ToList();

			if (Settings.Instance.EnableLogger)
				Logger.Debug("GetAllWindows - visible/filtered: {0}/{1}", filteredWindows.Count(x => x.IsVisible), filteredWindows.Count);

			return showAll ? filteredWindows : filteredWindows.Where(x => x.IsVisible);
		}
		#endregion

		private bool _disposed;
		private IntPtr _hwnd;
		private long _hwndStyle;
		private IntPtr _hwndOriginalParent;

		#region Binding Properties

		// ReSharper disable MemberCanBePrivate.Global
		public IntPtr Hwnd { get { return _hwnd; } }
		// ReSharper restore MemberCanBePrivate.Global

		public string FullName
		{
			get
			{
				return string.Format("{0} (Klasa: {1}, Nazwa procesu: {2}, Uchwyt okna: {3})", Title, ClassName, Process.ProcessName, _hwnd);
			}
		} 

		#endregion

		public bool Docked { get; private set; }
		public bool Exists { get; private set; }

		private bool IsVisible
		{
			get { return NativeMethods.IsWindowVisible(_hwnd); }
		}

		public string ClassName
		{
			get
			{
				var length = 64;
				while (true)
				{
					var sb = new StringBuilder(length);
					var value = NativeMethods.GetClassName(_hwnd, sb, sb.Capacity);

					if (value == 0)
					{
						return "(no class name)";
					}

					if (sb.Length != length - 1)
					{
						return sb.ToString();
					}

					length *= 2;
				}
			}
		}

		public string Title
		{
			get
			{
				var sb = new StringBuilder(NativeMethods.GetWindowTextLength(_hwnd) + 1);
				NativeMethods.GetWindowText(_hwnd, sb, sb.Capacity);
				return sb.ToString();
			}
		}

		public bool IsMinimized
		{
			get { return NativeMethods.IsIconic(_hwnd); }
		}

		public SystemWindow Parent 
		{
			get
			{
				var parent = _possibleParents.SingleOrDefault(x => x.Process.ProcessName == Process.ProcessName);

				if (Settings.Instance.EnableLogger)
					Logger.Debug("(object).Parent = {0}", parent == null ? "(empty)" : parent.FullName);

				return parent;
			}
		}

		private Process Process
		{
			get
			{
				return Process.GetProcessById(NativeMethods.GetWindowThreadProcessId(_hwnd));
			}
		}

		private SystemWindow(IntPtr hwnd)
		{
			_hwnd = hwnd;
			Docked = false;
		}

		public SystemWindow(string className, string title)
		{
			if (Settings.Instance.EnableLogger)
				Logger.Debug("Creating SystemWindow object from saved settings.");

			var window = GetAllWindows(true).Where(x => x.ClassName == className && x.Title == title);

			foreach (var w in window)
			{
				if (w != null && w._hwndOriginalParent == IntPtr.Zero)
				{
					_hwnd = w.Hwnd;
					Docked = false;
					Exists = true;
				}
				else
				{
					Exists = false;
				}

				if (Settings.Instance.EnableLogger)
					Logger.Debug("Window exists: {0}", Exists);
			}
		}

		public void UndockFromPanel()
		{
			Docked = false;

			if (Settings.Instance.EnableLogger)
				Logger.Debug("Undocking window: {0}", FullName);

			NativeMethods.SetWindowLong(_hwnd, NativeMethods.GwlStyle, _hwndStyle);
			NativeMethods.SetParent(_hwnd, _hwndOriginalParent);
			Process.Refresh();
		}

		public void DockToPanel(SplitterPanel formPanel)
		{
			if (Settings.Instance.EnableLogger)
				Logger.Debug("Docking window: {0}", FullName);

			Docked = true;

			_hwndStyle = NativeMethods.GetWindowLong(_hwnd, NativeMethods.GwlStyle);
			NativeMethods.SetWindowLong(_hwnd, NativeMethods.GwlStyle, (_hwndStyle & ~NativeMethods.WithoutTitleAndBorder));
			_hwndOriginalParent = NativeMethods.SetParent(_hwnd, formPanel.Handle);
		}

		public void PanelSizeChanged(object sender, EventArgs eventArgs)
		{
			var s = (SplitterPanel)sender;

			if (Settings.Instance.EnableLogger)
				Logger.Debug("Changing window size: {0} (width: {1}px, height: {2}px)", FullName, s.Width, s.Height);
			
			NativeMethods.MoveWindow(_hwnd, 0, 0, s.Width, s.Height);
		}

		public void Maximize()
		{
			if (Settings.Instance.EnableLogger)
				Logger.Debug("Maximizing window: {0}", FullName);
			
			NativeMethods.MaximizeWindow(_hwnd);
		}

		public void Restore()
		{
			if (Settings.Instance.EnableLogger)
				Logger.Debug("Minimizing window: {0}", FullName);

			NativeMethods.RestoreWindow(_hwnd);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (_disposed) return;

			if (disposing)
			{
				_possibleParents = null;
			}

			_hwnd = IntPtr.Zero;
			_hwndOriginalParent = IntPtr.Zero;
			

			_disposed = true;
		}

		~SystemWindow()
		{
			Dispose(false);
		}
	}
}