using Microsoft.Maui.Graphics.Native;
using AppKit;

namespace Microsoft.Maui
{
	public class MauiBoxView : NativeGraphicsView
	{
		public MauiBoxView()
		{
			BackgroundColor = NSColor.Clear;
		}
	}
}