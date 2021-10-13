#nullable enable
using System;
using AppKit;

namespace Microsoft.Maui.Controls
{
	public partial class Window
	{
		internal NSWindow NativeWindow =>
			(Handler?.NativeView as NSWindow) ?? throw new InvalidOperationException("Window should have a UIWindow set.");
	}
}