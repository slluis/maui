using System;
using Foundation;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Platform;
using AppKit;

namespace Microsoft.Maui.Platform
{
	public class PlatformTouchGraphicsView : PlatformGraphicsView
	{
		IGraphicsView? _graphicsView;

		public PlatformTouchGraphicsView()
		{
//			Opaque = false;
			BackgroundColor = null;
		}

		public void Connect(IGraphicsView graphicsView)
		{
			_graphicsView = graphicsView;
		}

		public void Disconnect()
		{
			_graphicsView = null;
		}
	}
}
