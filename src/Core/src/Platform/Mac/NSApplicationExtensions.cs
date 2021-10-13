using System;
using AppKit;

namespace Microsoft.Maui
{
	internal static class UIApplicationExtensions
	{
		public static NSWindow? GetKeyWindow(this NSApplication application)
		{
			var windows = application.Windows;

			for (int i = 0; i < windows.Length; i++)
			{
				var window = windows[i];
				if (window.IsKeyWindow)
					return window;
			}

			return null;
		}

		public static IWindow? GetWindow(this NSApplication application)
		{
			if (MauiUIApplicationDelegate.Current.VirtualWindow != null)
				return MauiUIApplicationDelegate.Current.VirtualWindow;

			var nativeWindow = application.GetKeyWindow();
			foreach (var window in MauiUIApplicationDelegate.Current.Application.Windows)
			{
				if (window?.Handler?.NativeView is UIWindow win && win == nativeWindow)
					return window;
			}

			return null;
		}
	}
}
