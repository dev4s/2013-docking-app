using System.Reflection;
using System;
using System.Windows.Forms;

namespace DockingApp
{
	public static class Extensions
	{
		public static string GetVersion()
		{
			return Assembly.GetExecutingAssembly().GetName().Version.ToString();
		}

		public static void InvokeIfRequired(this Control control, Action action)
		{
			if (control.InvokeRequired)
			{
				control.Invoke(action);
			}
			else
			{
				action();
			}
		}

		public static void InvokeIfRequired<T>(this Control control, Action<T> action, T parameter)
		{
			 if (control.InvokeRequired)
			 {
				 control.Invoke(action, parameter);
			 }
			 else
			 {
				 action(parameter);
			 }
		}
	}
}