using AppKit;

namespace Microsoft.Maui
{
	public static class TextAlignmentExtensions
	{
		public static NSTextAlignment ToNative(this TextAlignment alignment, IView view)
			=> alignment.ToNative(view.FlowDirection == FlowDirection.LeftToRight);

		public static NSTextAlignment ToNative(this TextAlignment alignment, bool isLtr)
		{
			switch (alignment)
			{
				case TextAlignment.Center:
					return NSTextAlignment.Center;
				case TextAlignment.End:
					if (isLtr)
						return NSTextAlignment.Right;
					else
						return NSTextAlignment.Left;
				default:
					if (isLtr)
						return NSTextAlignment.Left;
					else
						return NSTextAlignment.Right;
			}
		}

/*		public static UIControlContentVerticalAlignment ToNative(this TextAlignment alignment)
		{
			switch (alignment)
			{
				case TextAlignment.Center:
					return UIControlContentVerticalAlignment.Center;
				case TextAlignment.End:
					return UIControlContentVerticalAlignment.Bottom;
				case TextAlignment.Start:
					return UIControlContentVerticalAlignment.Top;
				default:
					return UIControlContentVerticalAlignment.Top;
			}
		}*/
	}
}