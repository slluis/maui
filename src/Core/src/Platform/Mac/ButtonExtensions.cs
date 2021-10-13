using System;
using CoreGraphics;
using Foundation;
using AppKit;

namespace Microsoft.Maui
{
	public static class ButtonExtensions
	{
		public static void UpdateText(this NSButton nativeButton, IText button) =>
			nativeButton.SetTitle(button.Text, UIControlState.Normal);

		public static void UpdateTextColor(this NSButton nativeButton, ITextStyle button) =>
			nativeButton.UpdateTextColor(button);

		public static void UpdateTextColor(this NSButton nativeButton, ITextStyle button, NSColor? buttonTextColorDefaultNormal, NSColor? buttonTextColorDefaultHighlighted, NSColor? buttonTextColorDefaultDisabled)
		{
			if (button.TextColor == null)
			{
				nativeButton.SetTitleColor(buttonTextColorDefaultNormal, UIControlState.Normal);
				nativeButton.SetTitleColor(buttonTextColorDefaultHighlighted, UIControlState.Highlighted);
				nativeButton.SetTitleColor(buttonTextColorDefaultDisabled, UIControlState.Disabled);
			}
			else
			{
				var color = button.TextColor.ToNative();

				nativeButton.SetTitleColor(color, UIControlState.Normal);
				nativeButton.SetTitleColor(color, UIControlState.Highlighted);
				nativeButton.SetTitleColor(color, UIControlState.Disabled);

				nativeButton.TintColor = color;
			}
		}

		public static void UpdateCharacterSpacing(this NSButton nativeButton, ITextStyle textStyle)
		{
			nativeButton.TitleLabel.UpdateCharacterSpacing(textStyle);
		}

		public static void UpdateFont(this NSButton nativeButton, ITextStyle textStyle, IFontManager fontManager)
		{
			nativeButton.TitleLabel.UpdateFont(textStyle, fontManager, UIFont.ButtonFontSize);
		}

		public static void UpdatePadding(this NSButton nativeButton, IButton button)
		{
			nativeButton.ContentEdgeInsets = new UIEdgeInsets(
				(float)(button.Padding.Top),
				(float)(button.Padding.Left),
				(float)(button.Padding.Bottom),
				(float)(button.Padding.Right));
		}
	}
}