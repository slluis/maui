using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml;

namespace Microsoft.Maui.Handlers
{
	public partial class ApplicationHandler : ElementHandler<IApplication, UI.Xaml.Application>
	{
		public static void MapTerminate(ApplicationHandler handler, IApplication application, object? args)
		{
			handler.PlatformView.Exit();
		}

		public static void MapOpenWindow(ApplicationHandler handler, IApplication application, object? args)
		{
			handler.PlatformView?.CreatePlatformWindow(application, args as OpenWindowRequest);
		}

		public static void MapCloseWindow(ApplicationHandler handler, IApplication application, object? args)
		{
			if (args is IWindow window)
			{
				(window.Handler?.PlatformView as Window)?.Close();
			}
		}
	}
}