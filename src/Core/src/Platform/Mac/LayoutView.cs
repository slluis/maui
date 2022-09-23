using System;
using CoreGraphics;
using Microsoft.Maui.Graphics;
using AppKit;

namespace Microsoft.Maui.Platform
{
	public class LayoutView : MauiView
	{
		public override CGSize SizeThatFits(CGSize size)
		{
			if (CrossPlatformMeasure == null)
			{
				return base.SizeThatFits(size);
			}

			var width = size.Width;
			var height = size.Height;

			var crossPlatformSize = CrossPlatformMeasure(width, height);

			return crossPlatformSize.ToCGSize();
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			var bounds = AdjustForSafeArea(Bounds).ToRectangle();

			CrossPlatformMeasure?.Invoke(bounds.Width, bounds.Height);
			CrossPlatformArrange?.Invoke(bounds);
		}

		// TODO COCOA
/*		public override void SubviewAdded(NSView? uiview)
		{
			base.SubviewAdded(uiview);
			Superview?.SetNeedsLayout();
		}*/

		public override void WillRemoveSubview(NSView? uiview)
		{
			base.WillRemoveSubview(uiview);
			if (Superview != null)
				Superview.NeedsLayout = true;
		}

		// TODO COCOA
		public bool ClipsToBounds { get; set; }

		internal Func<double, double, Size>? CrossPlatformMeasure { get; set; }
		internal Func<Rect, Size>? CrossPlatformArrange { get; set; }
	}
}