using CoreGraphics;
using AppKit;
using System;

namespace Microsoft.Maui.Platform
{
	public class MauiActivityIndicator : NSView
	{
		IActivityIndicator? _virtualView;

		// TODO
		public NSColor? Color { get; internal set; }

		public MauiActivityIndicator(CGRect rect, IActivityIndicator? virtualView) : base(rect)
			=> _virtualView = virtualView;

		internal void StartAnimating()
		{
			throw new NotImplementedException();
		}

		internal void StopAnimating()
		{
			throw new NotImplementedException();
		}

		// TODO COCOA
		/*		public override void Draw(CGRect rect)
				{
					base.Draw(rect);

					if (_virtualView?.IsRunning == true)
						StartAnimating();
				}

				public override void LayoutSubviews()
				{
					base.LayoutSubviews();

					if (_virtualView?.IsRunning == true)
						StartAnimating();
				}*/

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			_virtualView = null;
		}
	}
}