using System;
using CoreGraphics;
using Foundation;
using AppKit;

namespace Microsoft.Maui
{
	public static class ButtonExtensions
	{
		public static void UpdateText(this NSButton nativeButton, IText button)
		{
			nativeButton.Title = button.Text;
		}

		static bool SetColor (NSButton textView, Graphics.Color color)
		{
			var textColor = color?.ToNative();
			if (textColor != null)
			{
				var attributedValue = textView.AttributedStringValue?.WithColor(textColor);
				if (attributedValue != null)
				{
					textView.AttributedStringValue = attributedValue;
					return true;
				}
			}
			return false;
		}

		public static void UpdateTextColor(this NSButton textView, ITextStyle textStyle, Graphics.Color? defaultColor)
		{
			if (!SetColor(textView, textStyle.TextColor) && defaultColor != null)
			{
				SetColor(textView, defaultColor);
			}
		}

		public static void UpdateTextColor(this NSButton textView, ITextStyle textStyle)
		{
			UpdateTextColor(textView, textStyle, null);
		}

		public static void UpdateCharacterSpacing(this NSButton nativeButton, ITextStyle textStyle)
		{
			var textAttr = nativeButton.AttributedTitle?.WithCharacterSpacing(textStyle.CharacterSpacing);

			if (textAttr != null)
				nativeButton.AttributedTitle = textAttr;
		}

		public static void UpdateFont(this NSButton nativeButton, ITextStyle textStyle, IFontManager fontManager)
		{
	//		nativeButton.TitleLabel.UpdateFont(textStyle, fontManager, UIFont.ButtonFontSize);
		}

		public static void UpdatePadding(this NSButton nativeButton, IButton button)
		{
	/*		nativeButton.ContentEdgeInsets = new UIEdgeInsets(
				(float)(button.Padding.Top),
				(float)(button.Padding.Left),
				(float)(button.Padding.Bottom),
				(float)(button.Padding.Right));
	*/	}
	}
}