using Microsoft.Maui.Graphics.Platform;

namespace Microsoft.Maui.Platform
{
	public static class GraphicsViewExtensions
	{
		public static void UpdateDrawable(this PlatformGraphicsView nativeGraphicsView, IGraphicsView graphicsView)
		{
			nativeGraphicsView.Drawable = graphicsView.Drawable;
		}
	}
}