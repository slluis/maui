using System;
using AppKit;
using Microsoft.Maui.Platform;
namespace Microsoft.Maui.Handlers
{
	public partial class WindowHandler : ElementHandler<IWindow, NSWindow>
	{
		public static void MapTitle(IWindowHandler handler, IWindow window) { }

		public static void MapContent(IWindowHandler handler, IWindow window)
		{
			_ = handler.MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

			var originalFrame = handler.PlatformView.Frame;

			var nativeContent = window.Content.ToNSViewController(handler.MauiContext);
			handler.PlatformView.ContentViewController = nativeContent;

			handler.PlatformView.SetFrame(originalFrame, true);
		}

		public static void MapX(IWindowHandler handler, IWindow view) =>
			handler.PlatformView?.UpdateX(view);

		public static void MapY(IWindowHandler handler, IWindow view) =>
			handler.PlatformView?.UpdateY(view);

		public static void MapWidth(IWindowHandler handler, IWindow view) =>
			handler.PlatformView?.UpdateWidth(view);

		public static void MapHeight(IWindowHandler handler, IWindow view) =>
			handler.PlatformView?.UpdateHeight(view);

		public static void MapMaximumWidth(IWindowHandler handler, IWindow view) =>
			handler.PlatformView?.UpdateMaximumWidth(view);

		public static void MapMaximumHeight(IWindowHandler handler, IWindow view) =>
			handler.PlatformView?.UpdateMaximumHeight(view);

		public static void MapMinimumWidth(IWindowHandler handler, IWindow view) =>
			handler.PlatformView?.UpdateMinimumWidth(view);

		public static void MapMinimumHeight(IWindowHandler handler, IWindow view) =>
			handler.PlatformView?.UpdateMinimumHeight(view);

		public static void MapRequestDisplayDensity(IWindowHandler handler, IWindow window, object? args)
		{
			if (args is DisplayDensityRequest request)
				request.SetResult(handler.PlatformView.GetDisplayDensity());
		}
	}
}