using System;
using AppKit;

namespace Microsoft.Maui.Handlers
{
	public partial class WindowHandler : ElementHandler<IWindow, NSWindow>
	{
		public static void MapTitle(WindowHandler handler, IWindow window) { }

		public static void MapContent(WindowHandler handler, IWindow window)
		{
			_ = handler.MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

			var nativeContent = window.Content.ToNSViewController(handler.MauiContext);

			handler.NativeView.RootViewController = nativeContent;
		}
	}
}