using CoreGraphics;
using ObjCRuntime;
using AppKit;
using System;

namespace Microsoft.Maui
{
	class MauiHostView : NSView
	{
		NSView nativeView;
		IView view;

		public MauiHostView(IView view, NSView nativeView)
		{
			this.nativeView = nativeView;
			this.view = view;
			AddSubview(nativeView);
		}

		public override bool IsFlipped => true;

		public override CGSize IntrinsicContentSize
		{
			get
			{
				return view.Measure(double.PositiveInfinity, double.PositiveInfinity);
			}
		}

		public override CGRect Frame
		{
			get { return base.Frame; }
			set
			{
				base.Frame = value;
				nativeView.Frame = new CGRect(0, 0, value.Width, value.Height);
			}
		}
	}
}
