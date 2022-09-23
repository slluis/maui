using System;
using AppKit;

namespace Microsoft.Maui.Platform
{
	// TODO: NET7 issoto - Revisit this, marking this class as `internal` to avoid breaking public API changes
	internal static class TextInputExtensions
	{
		internal static int GetCursorPosition(this INSTextInput platformView, int cursorOffset = 0)
		{
			return Math.Max(0, cursorOffset + (int)platformView.SelectedRange.Location);
		}

		internal static void SetTextRange(this INSTextInput platformView, int start, int selectedTextLength)
		{
			// TODO COCOA
/*			int end = start + selectedTextLength;

			// Let's be sure we have positive positions
			start = Math.Max(start, 0);
			end = Math.Max(end, 0);

			// Switch start and end positions if necessary
			start = Math.Min(start, end);
			end = Math.Max(start, end);

			var startPosition = platformView.GetPosition(platformView.BeginningOfDocument, start);
			var endPosition = platformView.GetPosition(platformView.BeginningOfDocument, end);
			platformView.SelectedTextRange = platformView.GetTextRange(startPosition, endPosition);*/
		}

		internal static int GetSelectedTextLength(this INSTextInput platformView)
		{
			var selectedTextRange = platformView.SelectedRange;

			return (int)selectedTextRange.Length;
		}
	}
}

