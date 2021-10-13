using System;
using CoreGraphics;
using AppKit;

namespace Microsoft.Maui
{
	public class MauiImageView : NSImageView
	{
		public MauiImageView()
		{
		}

		public MauiImageView(CGRect frame)
			: base(frame)
		{
		}

		// TODO COCOA
//		public override void MovedToWindow() =>
//			WindowChanged?.Invoke(this, EventArgs.Empty);

		public event EventHandler? WindowChanged
		{
			add { }
			remove { }
		}
	}
}