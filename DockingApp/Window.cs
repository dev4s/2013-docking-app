namespace DockingApp
{
	public class Window
	{
		public string Title { get; private set; }
		public string ClassName { get; private set; }

		public Window(string title, string className)
		{
			Title = title;
			ClassName = className;
		}
	}
}