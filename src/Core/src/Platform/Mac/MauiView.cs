using CoreGraphics;
using ObjCRuntime;
using AppKit;
using System;

namespace Microsoft.Maui
{
	public abstract class MauiView : NSView
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

		public override CGSize FittingSize
		{
			get
			{
				return SizeThatFits(new CGSize (double.PositiveInfinity, double.PositiveInfinity));
			}
		}

		protected CGRect AdjustForSafeArea(CGRect bounds)
		{
			if (View is not ISafeAreaView sav || sav.IgnoreSafeArea || !RespondsToSafeArea())
			{
				return bounds;
			}

			var insets = SafeAreaInsets;
			return new CGRect(bounds.Left + insets.Left, bounds.Top + insets.Top, bounds.Width - insets.Left - insets.Right, bounds.Height - insets.Top - insets.Bottom);
		}

		public virtual CGSize SizeThatFits(CGSize size)
		{
			return base.FittingSize;
		}

		public virtual void LayoutSubviews()
		{
		}
	}
}
