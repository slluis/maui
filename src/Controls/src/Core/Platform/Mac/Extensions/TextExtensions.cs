using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Microsoft.Maui.Platform;
using AppKit;
using Specifics = Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Entry;

namespace Microsoft.Maui.Controls.Platform
{
	public static class TextExtensions
	{
		public static void UpdateCursorColor(this NSTextField textField, Entry entry)
		{
			// TODO COCOA
			/*
			if (entry.IsSet(Specifics.CursorColorProperty))
			{
				var color = entry.OnThisPlatform().GetCursorColor();

				if (color != null)
					textField.TintColor = color.ToPlatform();
			}*/
		}

		public static void UpdateAdjustsFontSizeToFitWidth(this NSTextField textField, Entry entry)
		{
			// TODO COCOA
			//textField.AdjustsFontSizeToFitWidth = entry.OnThisPlatform().AdjustsFontSizeToFitWidth();
		}

		public static void UpdateText(this NSTextView textView, InputView inputView)
		{
			// Setting the text causes the cursor to be reset to the end of the UITextView.
			// So, let's set back the cursor to the last known position and calculate a new
			// position if needed when the text was modified by a Converter.
			var oldText = textView.Value ?? string.Empty;
			var newText = TextTransformUtilites.GetTransformedText(
				inputView?.Text,
				/*textView.SecureTextEntry ? TextTransform.Default : TODO COCOA */inputView.TextTransform
				);

			// Re-calculate the cursor offset position if the text was modified by a Converter.
			// but if the text is being set by code, let's just move the cursor to the end.
			var cursorOffset = newText.Length - oldText.Length;
			var cursorPosition = textView.Window.FirstResponder == textView ? textView.GetCursorPosition(cursorOffset) : newText.Length;

			if (oldText != newText)
				textView.Value = newText;

			textView.SetTextRange(cursorPosition, 0);
		}

		public static void UpdateText(this NSTextField textField, InputView inputView)
		{
			// Setting the text causes the cursor to be reset to the end of the UITextView.
			// So, let's set back the cursor to the last known position and calculate a new
			// position if needed when the text was modified by a Converter.
			var oldText = textField.StringValue ?? string.Empty;
			var newText = TextTransformUtilites.GetTransformedText(
				inputView?.Text,
				/*textField.SecureTextEntry ? TextTransform.Default : TODO COCOA */inputView.TextTransform
				);

			// Re-calculate the cursor offset position if the text was modified by a Converter.
			// but if the text is being set by code, let's just move the cursor to the end.
			var cursorOffset = newText.Length - oldText.Length;
			var cursorPosition = textField.Window.FirstResponder == textField ? textField.GetCursorPosition(cursorOffset) : newText.Length;

			if (oldText != newText)
				textField.StringValue = newText;

			textField.SetTextRange(cursorPosition, 0);
		}

		public static void UpdateLineBreakMode(this NSTextField platformLabel, Label label)
		{
			platformLabel.SetLineBreakMode(label);
		}

		public static void UpdateMaxLines(this NSTextField platformLabel, Label label)
		{
			platformLabel.SetLineBreakMode(label);
		}

		internal static void SetLineBreakMode(this NSTextField platformLabel, Label label)
		{
			int maxLines = label.MaxLines;
			if (maxLines < 0)
				maxLines = 0;

			switch (label.LineBreakMode)
			{
				case LineBreakMode.NoWrap:
					platformLabel.LineBreakMode = NSLineBreakMode.Clipping;
					maxLines = 1;
					break;
				case LineBreakMode.WordWrap:
					platformLabel.LineBreakMode = NSLineBreakMode.ByWordWrapping;
					break;
				case LineBreakMode.CharacterWrap:
					platformLabel.LineBreakMode = NSLineBreakMode.CharWrapping;
					break;
				case LineBreakMode.HeadTruncation:
					platformLabel.LineBreakMode = NSLineBreakMode.TruncatingHead;
					maxLines = 1;
					break;
				case LineBreakMode.MiddleTruncation:
					platformLabel.LineBreakMode = NSLineBreakMode.TruncatingMiddle;
					maxLines = 1;
					break;
				case LineBreakMode.TailTruncation:
					platformLabel.LineBreakMode = NSLineBreakMode.TruncatingTail;
					maxLines = 1;
					break;
			}

			platformLabel.MaximumNumberOfLines = maxLines;
		}
	}
}