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
		internal static readonly NSColor SeventyPercentGrey = new NSColor(0.7f, 0.7f, 0.7f, 1);

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

				if (NativeVersion.IsAtLeast(13))
					return NSColor.PlaceholderTextColor;

				return SeventyPercentGrey;
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

				if (NativeVersion.IsAtLeast(13))
					return NSColor.SystemBackgroundColor;

				return NSColor.White;
			}
		}

		internal static NSColor SeparatorColor
		{
			get
			{
				if (NativeVersion.IsAtLeast(13))
					return NSColor.SeparatorColor;

				return NSColor.Gray;
			}
		}

		internal static NSColor OpaqueSeparatorColor
		{
			get
			{
				if (NativeVersion.IsAtLeast(13))
					return NSColor.OpaqueSeparatorColor;

				return NSColor.Black;
			}
		}

		internal static NSColor GroupedBackground
		{
			get
			{
				if (NativeVersion.IsAtLeast(13))
					return NSColor.SystemGroupedBackgroundColor;

				return new NSColor(247f / 255f, 247f / 255f, 247f / 255f, 1);
			}
		}

		internal static NSColor AccentColor
		{
			get
			{
				if (NativeVersion.IsAtLeast(13))
					return NSColor.SystemBlueColor;

				return Color.FromRgba(50, 79, 133, 255).ToNative();
			}
		}

		internal static NSColor Red
		{
			get
			{
				if (NativeVersion.IsAtLeast(13))
					return NSColor.SystemRedColor;

				return NSColor.FromRGBA(255, 0, 0, 255);
			}
		}

		internal static NSColor Gray
		{
			get
			{
				if (NativeVersion.IsAtLeast(13))
					return NSColor.SystemGrayColor;

				return NSColor.Gray;
			}
		}

		internal static NSColor LightGray
		{
			get
			{
				if (NativeVersion.IsAtLeast(13))
					return NSColor.SystemGray2Color;

				return NSColor.LightGray;
			}
		}

		public static CGColor ToCGColor(this Color color)
		{
			return color.ToNative().CGColor;
		}

		public static NSColor FromPatternImageFromBundle(string bgImage)
		{
			var image = UIImage.FromBundle(bgImage);
			if (image == null)
				return NSColor.White;

			return NSColor.FromPatternImage(image);
		}

		public static Color? ToColor(this NSColor color)
		{
			if (color == null)
				return null;

			color.GetRGBA(out nfloat red, out nfloat green, out nfloat blue, out nfloat alpha);

			return new Color((float)red, (float)green, (float)blue, (float)alpha);
		}

		public static NSColor ToNative(this Color color)
		{
			return new NSColor(color.Red, color.Green, color.Blue, color.Alpha);
		}

		public static NSColor? ToNative(this Color? color, Color? defaultColor)
			=> color?.ToNative() ?? defaultColor?.ToNative();

		public static NSColor ToNative(this Color? color, NSColor defaultColor)
			=> color?.ToNative() ?? defaultColor;
	}
}