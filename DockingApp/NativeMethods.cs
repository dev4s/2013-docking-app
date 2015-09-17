using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using NLog;

namespace DockingApp
{
	public static class NativeMethods
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
		private delegate int EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

		public static IEnumerable<IntPtr> EnumWindows()
		{
			if (Settings.Instance.EnableLogger)
				Logger.Info("(Native) Enumerating all windows.");

			var allWindows = new List<IntPtr>();

			EnumWindows((hwnd, lParam) =>
				{
					allWindows.Add(hwnd);
					return 1;
				}, new IntPtr(0));

			return allWindows;
		}

		[DllImport("user32.dll", EntryPoint = "GetClassName", CharSet = CharSet.Unicode)]
		private static extern int GetClassName32(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

		public static int GetClassName(IntPtr hwnd, StringBuilder className, int maxCount)
		{
			return GetClassName32(hwnd, className, maxCount);
		}

		[DllImport("user32.dll", EntryPoint = "GetWindowText", SetLastError = true, CharSet = CharSet.Unicode)]
		private static extern int GetWindowText32(IntPtr hWnd, [Out] StringBuilder lpString, int nMaxCount);

		public static void GetWindowText(IntPtr hwnd, StringBuilder text, int maxCount)
		{
			GetWindowText32(hwnd, text, maxCount);
		}

		[DllImport("user32.dll", EntryPoint = "GetWindowTextLength", SetLastError = true, CharSet = CharSet.Unicode)]
		private static extern int GetWindowTextLength32(IntPtr hWnd);

		public static int GetWindowTextLength(IntPtr hwnd)
		{
			return GetWindowTextLength32(hwnd);
		} 

		[DllImport("user32.dll", EntryPoint = "IsWindowVisible", CharSet = CharSet.Auto)]
		private static extern bool IsWindowVisible32(IntPtr hWnd);

		public static bool IsWindowVisible(IntPtr hwnd)
		{
			return IsWindowVisible32(hwnd);
		}

		[DllImport("user32.dll", EntryPoint = "SetParent")]
		private static extern IntPtr SetParent32(IntPtr hWndChild, IntPtr hWndNewParent);

		public static IntPtr SetParent(IntPtr child, IntPtr newParent)
		{
			if (Settings.Instance.EnableLogger)
				Logger.Info("(Native) Setting parent (child: {0}, new parent: {1}).", child, newParent);

			return SetParent32(child, newParent);
		}

		[DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId", SetLastError = true)]
		private static extern int GetWindowThreadProcessId32(IntPtr hWnd, out int lpdwProcessId);

		public static int GetWindowThreadProcessId(IntPtr hwnd)
		{
			int id;
			GetWindowThreadProcessId32(hwnd, out id);
			return id;
		}

		[DllImport("user32.dll", EntryPoint = "MoveWindow", SetLastError = true)]
// ReSharper disable InconsistentNaming
		private static extern bool MoveWindow32(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
// ReSharper restore InconsistentNaming

		public static void MoveWindow(IntPtr hwnd, int x, int y, int width, int height)
		{
			if (Settings.Instance.EnableLogger)
				Logger.Info("(Native) Moving window (window: {0}, x: {1}, y: {2}, width: {3}px, height {4}px).", hwnd, x, y, width, height);

			MoveWindow32(hwnd, x, y, width, height, true);
		}

		[DllImport("user32.dll", EntryPoint = "GetWindowLong")]
		private static extern int GetWindowLong32(IntPtr hWnd, int nIndex);
		
		[DllImport("user32.dll", EntryPoint = "SetWindowLong")]
		private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);

		[DllImport("user32.dll", EntryPoint = "IsIconic")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsIconic32(IntPtr hWnd);

		public static bool IsIconic(IntPtr hwnd)
		{
			if (Settings.Instance.EnableLogger)
				Logger.Info("(Native) Is iconic (window handle: {0}).", hwnd);

			return IsIconic32(hwnd);
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
// ReSharper disable InconsistentNaming
		private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
// ReSharper restore InconsistentNaming
		private const UInt32 WmSyscommand = 0x0112;
		private static readonly IntPtr Maximize = new IntPtr(0xF030);
		private static readonly IntPtr Restore = new IntPtr(0xF120);

		public static void MaximizeWindow(IntPtr hwnd)
		{
			SendMessage(hwnd, WmSyscommand, Maximize, IntPtr.Zero);
		}
		
		public static void RestoreWindow(IntPtr hwnd)
		{
			SendMessage(hwnd, WmSyscommand, Restore, IntPtr.Zero);
		}

		public static long GetWindowLong(IntPtr hWnd, int nIndex)
		{
			if (Settings.Instance.EnableLogger)
				Logger.Info("(Native) Getting window long (window: {0}, index: {1}).", hWnd, nIndex);

			return GetWindowLong32(hWnd, nIndex);
		}

		public static void SetWindowLong(IntPtr hWnd, int nIndex, long dwNewLong)
		{
			if (Settings.Instance.EnableLogger)
				Logger.Info("(Native) Setting window long (window: {0}, index: {1}, dwNewLong: {2}).", hWnd, nIndex, dwNewLong);

			SetWindowLong32(hWnd, nIndex, (int) dwNewLong);
		}

		public const int GwlStyle = -16;
		private const int WsBorder = 0x00800000;
		private const int WsDlgframe = 0x00400000;
		private const int WsThickframe = 0x00040000;
		public const int WithoutTitleAndBorder = WsBorder | WsDlgframe | WsThickframe;
	}
}