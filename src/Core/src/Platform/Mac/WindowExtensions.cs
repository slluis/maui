using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Devices;
using AppKit;

namespace Microsoft.Maui.Platform
{
	public static partial class WindowExtensions
	{
		internal static void UpdateX(this NSWindow platformWindow, IWindow window) =>
			platformWindow.UpdateUnsupportedCoordinate(window);

		internal static void UpdateY(this NSWindow platformWindow, IWindow window) =>
			platformWindow.UpdateUnsupportedCoordinate(window);

		internal static void UpdateWidth(this NSWindow platformWindow, IWindow window) =>
			platformWindow.UpdateUnsupportedCoordinate(window);

		internal static void UpdateHeight(this NSWindow platformWindow, IWindow window) =>
			platformWindow.UpdateUnsupportedCoordinate(window);

		internal static void UpdateUnsupportedCoordinate(this NSWindow platformWindow, IWindow window) =>
			window.FrameChanged(platformWindow.Frame.ToRectangle());

		public static void UpdateMaximumWidth(this NSWindow platformWindow, IWindow window) =>
			platformWindow.UpdateMaximumSize(window.MaximumWidth, window.MaximumHeight);

		public static void UpdateMaximumHeight(this NSWindow platformWindow, IWindow window) =>
			platformWindow.UpdateMaximumSize(window.MaximumWidth, window.MaximumHeight);

		public static void UpdateMaximumSize(this NSWindow platformWindow, IWindow window) =>
			platformWindow.UpdateMaximumSize(window.MaximumWidth, window.MaximumHeight);

		internal static void UpdateMaximumSize(this NSWindow platformWindow, double width, double height)
		{
			platformWindow.MaxSize = new CoreGraphics.CGSize(width, height);
		}

		public static void UpdateMinimumWidth(this NSWindow platformWindow, IWindow window) =>
			platformWindow.UpdateMinimumSize(window.MinimumWidth, window.MinimumHeight);

		public static void UpdateMinimumHeight(this NSWindow platformWindow, IWindow window) =>
			platformWindow.UpdateMinimumSize(window.MinimumWidth, window.MinimumHeight);

		public static void UpdateMinimumSize(this NSWindow platformWindow, IWindow window) =>
			platformWindow.UpdateMinimumSize(window.MinimumWidth, window.MinimumHeight);

		internal static void UpdateMinimumSize(this NSWindow platformWindow, double width, double height)
		{
			if (!Primitives.Dimension.IsExplicitSet(width) || !Primitives.Dimension.IsMinimumSet(width))
				width = 0;
			if (!Primitives.Dimension.IsExplicitSet(height) || !Primitives.Dimension.IsMinimumSet(height))
				height = 0;

			platformWindow.MinSize = new CoreGraphics.CGSize(width, height);
		}

		internal static IWindow? GetHostedWindow(this NSWindow? NSWindow)
		{
			if (NSWindow is null)
				return null;

			var windows = WindowExtensions.GetWindows();
			foreach (var window in windows)
			{

				if (window.Handler?.PlatformView is NSWindow win)
				{
					if (win == NSWindow)
						return window;
				}
			}

			return null;
		}

		public static float GetDisplayDensity(this NSWindow NSWindow) =>
			(float)(NSWindow.Screen?.BackingScaleFactor ?? new nfloat(1.0f));

		internal static DisplayOrientation GetOrientation(this IWindow? window) =>
			DeviceDisplay.Current.MainDisplayInfo.Orientation;
	}
}
