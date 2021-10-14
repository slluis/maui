using Foundation;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Platform.iOS;
using AppKit;

namespace Microsoft.Maui
{
	public static class LabelExtensions
	{
		static bool SetColor(MauiLabel textView, Graphics.Color color)
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

		public static void UpdateTextColor(this MauiLabel textView, ITextStyle textStyle, Graphics.Color? defaultColor)
		{
			if (!SetColor(textView, textStyle.TextColor) && defaultColor != null)
			{
				SetColor(textView, defaultColor);
			}
		}

		public static void UpdateTextColor(this MauiLabel textView, ITextStyle textStyle)
		{
			UpdateTextColor (textView, textStyle, null);
		}

		public static void UpdateCharacterSpacing(this MauiLabel nativeLabel, ITextStyle textStyle)
		{
			var textAttr = nativeLabel.AttributedStringValue?.WithCharacterSpacing(textStyle.CharacterSpacing);
			if (textAttr != null)
				nativeLabel.AttributedStringValue = textAttr;
		}

		//public static void UpdateFont(this UILabel nativeLabel, ITextStyle textStyle, IFontManager fontManager) =>
		//	nativeLabel.UpdateFont(textStyle, fontManager, UIFont.LabelFontSize);

		public static void UpdateFont(this MauiLabel textView, ITextStyle textStyle, IFontManager fontManager)
		{
			//var font = textStyle.Font;

			//var tf = fontManager.GetTypeface(font);
			//textView.Typeface = tf;

			//var fontSize = fontManager.GetFontSize(font);
			//textView.SetTextSize(fontSize.Unit, fontSize.Value);
		}

		public static void UpdateHorizontalTextAlignment(this MauiLabel nativeLabel, ILabel label)
		{
			nativeLabel.Alignment = label.HorizontalTextAlignment.ToNative(label);
		}

		public static void UpdateLineBreakMode(this MauiLabel nativeLabel, ILabel label)
		{
			nativeLabel.SetLineBreakMode(label);
		}

		public static void UpdateMaxLines(this MauiLabel nativeLabel, ILabel label)
		{
			nativeLabel.MaximumNumberOfLines = label.MaxLines;
		}

		public static void UpdatePadding(this MauiLabel nativeLabel, ILabel label)
		{
			//nativeLabel.TextInsets = new UIEdgeInsets(
			//	(float)label.Padding.Top,
			//	(float)label.Padding.Left,
			//	(float)label.Padding.Bottom,
			//	(float)label.Padding.Right);
		}

		public static void UpdateTextPlainText(this MauiLabel textView, IText label)
		{
			textView.StringValue = label.Text;
		}

		public static void UpdateTextDecorations(this MauiLabel nativeLabel, ILabel label)
		{
			var modAttrText = nativeLabel.AttributedStringValue?.WithDecorations(label.TextDecorations);

			if (modAttrText != null)
				nativeLabel.AttributedStringValue = modAttrText;
		}

		public static void UpdateLineHeight(this MauiLabel nativeLabel, ILabel label)
		{
			var modAttrText = nativeLabel.AttributedStringValue?.WithLineHeight(label.LineHeight);

			if (modAttrText != null)
				nativeLabel.AttributedStringValue = modAttrText;
		}

		public static void UpdateTextHtml(this MauiLabel textView, ILabel label)
		{
			//throw new System.NotImplementedException();
		}

		internal static void SetLineBreakMode(this MauiLabel nativeLabel, ILabel label)
		{
			int maxLines = label.MaxLines;
			if (maxLines < 0)
				maxLines = 0;

			switch (label.LineBreakMode)
			{
				case LineBreakMode.NoWrap:
					nativeLabel.LineBreakMode = NSLineBreakMode.Clipping;
					maxLines = 1;
					break;
				case LineBreakMode.WordWrap:
					nativeLabel.LineBreakMode = NSLineBreakMode.ByWordWrapping;
					break;
				case LineBreakMode.CharacterWrap:
					nativeLabel.LineBreakMode = NSLineBreakMode.CharWrapping;
					break;
				case LineBreakMode.HeadTruncation:
					nativeLabel.LineBreakMode = NSLineBreakMode.TruncatingHead;
					maxLines = 1;
					break;
				case LineBreakMode.MiddleTruncation:
					nativeLabel.LineBreakMode = NSLineBreakMode.TruncatingMiddle;
					maxLines = 1;
					break;
				case LineBreakMode.TailTruncation:
					nativeLabel.LineBreakMode = NSLineBreakMode.TruncatingTail;
					maxLines = 1;
					break;
			}

			nativeLabel.MaximumNumberOfLines = maxLines;
		}
	}
}