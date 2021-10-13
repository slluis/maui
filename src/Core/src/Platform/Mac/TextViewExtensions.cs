using AppKit;

namespace Microsoft.Maui
{
	public static class TextViewExtensions
	{
		public static void UpdateText(this NSTextView textView, IEditor editor)
		{
			string text = editor.Text;

/*			if (textView.Text != text)
			{
				textView.Text = text;
			}*/
		}

		public static void UpdateTextColor(this NSTextView textView, IEditor editor)
		{
			var textColor = editor.TextColor;

			if (textColor == null)
				textView.TextColor = ColorExtensions.LabelColor;
			else
				textView.TextColor = textColor.ToNative();
		}

		public static void UpdateCharacterSpacing(this NSTextView textView, ITextStyle textStyle)
		{
			// TODO COCOA
/*			var textAttr = textView.AttributedText?.WithCharacterSpacing(textStyle.CharacterSpacing);
			if (textAttr != null)
				textView.AttributedText = textAttr;*/

			// TODO: Include AttributedText to Label Placeholder
		}

		public static void UpdateMaxLength(this NSTextView textView, IEditor editor)
		{
/*			var newText = textView.AttributedText.TrimToMaxLength(editor.MaxLength);
			if (newText != null && textView.AttributedText != newText)
				textView.AttributedText = newText;*/
		}

		public static void UpdateIsTextPredictionEnabled(this NSTextView textView, IEditor editor)
		{
/*			if (editor.IsTextPredictionEnabled)
				textView.AutocorrectionType = UITextAutocorrectionType.Yes;
			else
				textView.AutocorrectionType = UITextAutocorrectionType.No;*/
		}

		public static void UpdateFont(this NSTextView textView, ITextStyle textStyle, IFontManager fontManager)
		{
/*			var font = textStyle.Font;
			var uiFont = fontManager.GetFont(font, NSFont.LabelFontSize);
			textView.Font = uiFont;*/
		}

		public static void UpdateIsReadOnly(this NSTextView textView, IEditor editor)
		{
	//		textView.UserInteractionEnabled = !editor.IsReadOnly;
		}

		public static void UpdateKeyboard(this NSTextView textView, IEditor editor)
		{
			var keyboard = editor.Keyboard;

			textView.ApplyKeyboard(keyboard);

			if (keyboard is not CustomKeyboard)
				textView.UpdateIsTextPredictionEnabled(editor);

	//		textView.ReloadInputViews();
		}
	}
}