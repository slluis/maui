using Microsoft.Maui.Graphics.Native;
using AppKit;

namespace Microsoft.Maui
{
	public class MauiShapeView : NativeGraphicsView
	{
		public MauiShapeView()
		{
			BackgroundColor = NSColor.Clear;
		}
	}
}