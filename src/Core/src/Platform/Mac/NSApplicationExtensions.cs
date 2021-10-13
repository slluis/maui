using System;
using AppKit;

namespace Microsoft.Maui
{
	internal static class UIApplicationExtensions
	{
		public static NSWindow? GetKeyWindow(this NSApplication application)
		{
			return application.KeyWindow;
		}

		public static IWindow? GetWindow(this NSApplication application)
		{
			if (MauiUIApplicationDelegate.Current.VirtualWindow != null)
				return MauiUIApplicationDelegate.Current.VirtualWindow;

			var nativeWindow = application.GetKeyWindow();
			foreach (var window in MauiUIApplicationDelegate.Current.Application.Windows)
			{
				if (window?.Handler?.NativeView is NSWindow win && win == nativeWindow)
					return window;
			}

			return null;
		}
	}
}
