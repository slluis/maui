using CoreGraphics;
using ObjCRuntime;
using AppKit;
using System;

namespace Microsoft.Maui.Platform
{
	public class MauiView : NSView
	{
		static bool? _respondsToSafeArea;

		public IView? View { get; set; }

		public override bool IsFlipped => true;

		bool RespondsToSafeArea()
		{
			if (_respondsToSafeArea.HasValue)
				return _respondsToSafeArea.Value;
			return (bool)(_respondsToSafeArea = RespondsToSelector(new Selector("safeAreaInsets")));
		}

		public override void Layout()
		{
			base.Layout();
			LayoutSubviews();
		}

		public override CGSize IntrinsicContentSize {
			get {
				return SizeThatFits(new CGSize(double.PositiveInfinity, double.PositiveInfinity));
			}
		}

		protected CGRect AdjustForSafeArea(CGRect bounds)
		{
			if (View is ISafeAreaView sav && !sav.IgnoreSafeArea && RespondsToSafeArea() && OperatingSystem.IsMacOSVersionAtLeast(11))
			{
				var insets = SafeAreaInsets;
				return new CGRect(bounds.Left + insets.Left, bounds.Top + insets.Top, bounds.Width - insets.Left - insets.Right, bounds.Height - insets.Top - insets.Bottom);
			}
			else
				return bounds;
		}

		public virtual CGSize SizeThatFits(CGSize size)
		{
			return base.IntrinsicContentSize;
		}

		public virtual void LayoutSubviews()
		{
		}
	}
}
