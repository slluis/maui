using System;
using CoreGraphics;
using Microsoft.Maui.Graphics;
using AppKit;
using NSColor = AppKit.NSColor;

namespace Microsoft.Maui
{
	public static class ColorExtensions
	{
		internal static readonly NSColor Black = NSColor.Black;
		internal static readonly NSColor SeventyPercentGrey = NSColor.FromRgba(0.7f, 0.7f, 0.7f, 1);

		internal static NSColor LabelColor
		{
			get
			{
				if (NativeVersion.IsAtLeast(13))
					return NSColor.LabelColor;

				return NSColor.Black;
			}
		}

		internal static NSColor PlaceholderColor
		{
			get
			{
				return NSColor.PlaceholderTextColor;
			}
		}

		internal static NSColor SecondaryLabelColor
		{
			get
			{

				if (NativeVersion.IsAtLeast(13))
					return NSColor.SecondaryLabelColor;

				return new Color(.32f, .4f, .57f).ToNative();
			}
		}

		internal static NSColor BackgroundColor
		{
			get
			{
				return NSColor.ControlBackground;
			}
		}

		internal static NSColor SeparatorColor
		{
			get
			{
				return NSColor.SeparatorColor;
			}
		}

		internal static NSColor OpaqueSeparatorColor
		{
			get
			{
				return NSColor.SeparatorColor;
			}
		}

		internal static NSColor GroupedBackground
		{
			get
			{
				return NSColor.FromRgba(247f / 255f, 247f / 255f, 247f / 255f, 1);
/*				if (NativeVersion.IsAtLeast(13))
					return NSColor.SystemGroupedBackgroundColor;

				return new NSColor(247f / 255f, 247f / 255f, 247f / 255f, 1);
*/
			}
		}

		internal static NSColor AccentColor
		{
			get
			{
				return NSColor.SystemBlueColor;
			}
		}

		internal static NSColor Red
		{
			get
			{
				return NSColor.SystemRedColor;
			}
		}

		internal static NSColor Gray
		{
			get
			{
				return NSColor.SystemGrayColor;
			}
		}

		internal static NSColor LightGray
		{
			get
			{
				return NSColor.SystemGrayColor;
			}
		}

		public static CGColor ToCGColor(this Color color)
		{
			return color.ToNative().CGColor;
		}

		public static NSColor FromPatternImageFromBundle(string bgImage)
		{
			var image = NSImage.ImageNamed(bgImage);
			if (image == null)
				return NSColor.White;

			return NSColor.FromPatternImage(image);
		}

		public static Color? ToColor(this NSColor color)
		{
			if (color == null)
				return null;

			color.GetRgba(out nfloat red, out nfloat green, out nfloat blue, out nfloat alpha);

			return new Color((float)red, (float)green, (float)blue, (float)alpha);
		}

		public static NSColor ToNative(this Color color)
		{
			return NSColor.FromRgba(color.Red, color.Green, color.Blue, color.Alpha);
		}

		public static NSColor? ToNative(this Color? color, Color? defaultColor)
			=> color?.ToNative() ?? defaultColor?.ToNative();

		public static NSColor ToNative(this Color? color, NSColor defaultColor)
			=> color?.ToNative() ?? defaultColor;
	}
}