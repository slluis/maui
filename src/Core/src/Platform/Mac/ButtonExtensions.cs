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

		public static void UpdateTextColor(this NSButton nativeButton, ITextStyle button) =>
			nativeButton.ContentTintColor = button.TextColor.ToNative();

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