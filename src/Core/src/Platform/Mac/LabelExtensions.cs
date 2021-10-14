using Foundation;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Platform.iOS;
using AppKit;

namespace Microsoft.Maui
{
	public static class LabelExtensions
	{
		public static void UpdateTextColor(this MauiLabel textView, ITextStyle textStyle, Graphics.Color defaultColor)
		{
			//var textColor = textStyle.TextColor?.ToNative() ?? defaultColor?.ToNative();

			//if (textColor != null)
			//	textView.SetTextColor(textColor.Value);
		}

		public static void UpdateTextColor(this MauiLabel textView, ITextStyle textStyle)
		{

		}

		public static void UpdateCharacterSpacing(this MauiLabel nativeLabel, ITextStyle textStyle)
		{
			//var textAttr = nativeLabel.AttributedText?.WithCharacterSpacing(textStyle.CharacterSpacing);

			//if (textAttr != null)
			//	nativeLabel.AttributedText = textAttr;
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
			//nativeLabel.TextAlignment = label.HorizontalTextAlignment.ToNative(label);
		}

		public static void UpdateLineBreakMode(this MauiLabel nativeLabel, ILabel label)
		{
			//nativeLabel.SetLineBreakMode(label);
		}

		public static void UpdateMaxLines(this MauiLabel nativeLabel, ILabel label)
		{
			//nativeLabel.SetLineBreakMode(label);
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
			//var modAttrText = nativeLabel.AttributedText?.WithDecorations(label.TextDecorations);

			//if (modAttrText != null)
			//	nativeLabel.AttributedText = modAttrText;
		}

		public static void UpdateLineHeight(this MauiLabel nativeLabel, ILabel label)
		{
			//var modAttrText = nativeLabel.AttributedText?.WithLineHeight(label.LineHeight);

			//if (modAttrText != null)
			//	nativeLabel.AttributedText = modAttrText;
		}

		public static void UpdateTextHtml(this MauiLabel textView, ILabel label)
		{
		
		}

		internal static void SetLineBreakMode(this MauiLabel nativeLabel, ILabel label)
		{
			//int maxLines = label.MaxLines;
			//if (maxLines < 0)
			//	maxLines = 0;

			//switch (label.LineBreakMode)
			//{
			//	case LineBreakMode.NoWrap:
			//		nativeLabel.LineBreakMode = UILineBreakMode.Clip;
			//		maxLines = 1;
			//		break;
			//	case LineBreakMode.WordWrap:
			//		nativeLabel.LineBreakMode = UILineBreakMode.WordWrap;
			//		break;
			//	case LineBreakMode.CharacterWrap:
			//		nativeLabel.LineBreakMode = UILineBreakMode.CharacterWrap;
			//		break;
			//	case LineBreakMode.HeadTruncation:
			//		nativeLabel.LineBreakMode = UILineBreakMode.HeadTruncation;
			//		maxLines = 1;
			//		break;
			//	case LineBreakMode.MiddleTruncation:
			//		nativeLabel.LineBreakMode = UILineBreakMode.MiddleTruncation;
			//		maxLines = 1;
			//		break;
			//	case LineBreakMode.TailTruncation:
			//		nativeLabel.LineBreakMode = UILineBreakMode.TailTruncation;
			//		maxLines = 1;
			//		break;
			//}

			//nativeLabel.Lines = maxLines;
		}
	}
}