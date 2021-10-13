﻿using System;
using Foundation;
using Microsoft.Maui.Graphics;
using AppKit;

namespace Microsoft.Maui
{
	public static class TextFieldExtensions
	{
		public static void UpdateText(this NSTextField textField, IEntry entry)
		{
		//	textField.Text = entry.Text;
		}

		public static void UpdateTextColor(this NSTextField textField, ITextStyle textStyle, NSColor? defaultTextColor = null)
		{
			// Default value of color documented to be black in iOS docs

			var textColor = textStyle.TextColor;
			textField.TextColor = textColor.ToNative(defaultTextColor ?? ColorExtensions.LabelColor);
		}

		public static void UpdateIsPassword(this NSTextField textField, IEntry entry)
		{
/*			if (entry.IsPassword && textField.IsFirstResponder)
			{
				textField.Enabled = false;
				textField.SecureTextEntry = true;
				textField.Enabled = entry.IsEnabled;
				textField.BecomeFirstResponder();
			}
			else
				textField.SecureTextEntry = entry.IsPassword;*/
		}

		public static void UpdateHorizontalTextAlignment(this NSTextField textField, ITextAlignment textAlignment)
		{
/*			bool isLtr;

			if (textAlignment is IView v && v.FlowDirection == FlowDirection.LeftToRight)
				isLtr = true;
			else
				isLtr = false;

			textField.TextAlignment = textAlignment.HorizontalTextAlignment.ToNative(isLtr);*/
		}

		public static void UpdateVerticalTextAlignment(this NSTextField textField, ITextAlignment textAlignment)
		{
	//		textField.VerticalAlignment = textAlignment.VerticalTextAlignment.ToNative();
		}

		public static void UpdateIsTextPredictionEnabled(this NSTextField textField, IEntry entry)
		{
	/*		if (entry.IsTextPredictionEnabled)
				textField.AutocorrectionType = UITextAutocorrectionType.Yes;
			else
				textField.AutocorrectionType = UITextAutocorrectionType.No;
		*/}

		public static void UpdateMaxLength(this NSTextField textField, IEntry entry)
		{
/*			var newText = textField.AttributedText.TrimToMaxLength(entry.MaxLength);
			if (newText != null && textField.AttributedText != newText)
				textField.AttributedText = newText;
	*/	}

		public static void UpdatePlaceholder(this NSTextField textField, IEntry entry)
		{
			textField.UpdatePlaceholder(entry, null);
		}

		public static void UpdatePlaceholder(this NSTextField textField, IEntry entry, Color? defaultPlaceholderColor)
		{
/*			var placeholder = entry.Placeholder;

			if (placeholder == null)
				return;

			var placeholderColor = entry.PlaceholderColor;
			var foregroundColor = placeholderColor ?? defaultPlaceholderColor;

			textField.AttributedPlaceholder = foregroundColor == null
 				? new NSAttributedString(placeholder)
 				: new NSAttributedString(str: placeholder, foregroundColor: foregroundColor.ToNative());

			textField.AttributedPlaceholder.WithCharacterSpacing(entry.CharacterSpacing);
	*/	}

		public static void UpdateIsReadOnly(this NSTextField textField, IEntry entry)
		{
//			textField.UserInteractionEnabled = !entry.IsReadOnly;
		}

		public static void UpdateFont(this NSTextField textField, ITextStyle textStyle, IFontManager fontManager)
		{
			var uiFont = fontManager.GetFont(textStyle.Font, NSFont.LabelFontSize);
			textField.Font = uiFont;
		}

		public static void UpdateReturnType(this NSTextField textField, IEntry entry)
		{
//			textField.ReturnKeyType = entry.ReturnType.ToNative();
		}

		public static void UpdateCharacterSpacing(this NSTextField textField, ITextStyle textStyle)
		{
//			var textAttr = textField.AttributedText?.WithCharacterSpacing(textStyle.CharacterSpacing);
//			if (textAttr != null)
//				textField.AttributedText = textAttr;
		}

		public static void UpdateKeyboard(this NSTextField textField, IEntry entry)
		{
/*			var keyboard = entry.Keyboard;

			textField.ApplyKeyboard(keyboard);

			if (keyboard is not CustomKeyboard)
				textField.UpdateIsTextPredictionEnabled(entry);

			textField.ReloadInputViews();
	*/	}

		[PortHandler]
		public static void UpdateCursorPosition(this NSTextField textField, IEntry entry)
		{
/*			var selectedTextRange = textField.SelectedTextRange;
			if (selectedTextRange == null)
				return;
			if (textField.GetOffsetFromPosition(textField.BeginningOfDocument, selectedTextRange.Start) != entry.CursorPosition)
				UpdateCursorSelection(textField, entry);
*/
		}

		[PortHandler]
		public static void UpdateSelectionLength(this NSTextField textField, IEntry entry)
		{
	/*		var selectedTextRange = textField.SelectedTextRange;
			if (selectedTextRange == null)
				return;
			if (textField.GetOffsetFromPosition(selectedTextRange.Start, selectedTextRange.End) != entry.SelectionLength)
				UpdateCursorSelection(textField, entry);
	*/	}

		/* Updates both the IEntry.CursorPosition and IEntry.SelectionLength properties. */
		static void UpdateCursorSelection(this NSTextField textField, IEntry entry)
		{
/*			if (!entry.IsReadOnly)
			{
				if (!textField.IsFirstResponder)
					textField.BecomeFirstResponder();
				UITextPosition start = GetSelectionStart(textField, entry, out int startOffset);
				UITextPosition end = GetSelectionEnd(textField, entry, start, startOffset);

				textField.SelectedTextRange = textField.GetTextRange(start, end);
			}*/
		}

/*		static UITextPosition GetSelectionStart(NSTextField textField, IEntry entry, out int startOffset)
		{
			int cursorPosition = entry.CursorPosition;

			UITextPosition start = textField.GetPosition(textField.BeginningOfDocument, cursorPosition) ?? textField.EndOfDocument;
			startOffset = Math.Max(0, (int)textField.GetOffsetFromPosition(textField.BeginningOfDocument, start));

			if (startOffset != cursorPosition)
				entry.CursorPosition = startOffset;

			return start;
		}

		static UITextPosition GetSelectionEnd(NSTextField textField, IEntry entry, UITextPosition start, int startOffset)
		{
			int selectionLength = entry.SelectionLength;
			int textFieldLength = textField.Text == null ? 0 : textField.Text.Length;
			// Get the desired range in respect to the actual length of the text we are working with
			UITextPosition end = textField.GetPosition(start, Math.Min(textFieldLength - entry.CursorPosition, selectionLength)) ?? start;
			int endOffset = Math.Max(startOffset, (int)textField.GetOffsetFromPosition(textField.BeginningOfDocument, end));

			int newSelectionLength = Math.Max(0, endOffset - startOffset);
			if (newSelectionLength != selectionLength)
				entry.SelectionLength = newSelectionLength;

			return end;
		}*/

		public static void UpdateClearButtonVisibility(this NSTextField textField, IEntry entry)
		{
	//		textField.ClearButtonMode = entry.ClearButtonVisibility == ClearButtonVisibility.WhileEditing ? UITextFieldViewMode.WhileEditing : UITextFieldViewMode.Never;
		}
	}
}
