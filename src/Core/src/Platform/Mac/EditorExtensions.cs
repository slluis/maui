using Microsoft.Maui.Graphics;
using Microsoft.Maui.Platform.Mac;
using AppKit;

namespace Microsoft.Maui
{
	public static class EditorExtensions
	{
		public static void UpdatePlaceholder(this MauiTextView textView, IEditor editor)
		{
			textView.PlaceholderText = editor.Placeholder;
		}

		public static void UpdatePlaceholderColor(this MauiTextView textView, IEditor editor, NSColor? defaultPlaceholderColor)
			=> textView.PlaceholderTextColor = editor.PlaceholderColor?.ToNative() ?? defaultPlaceholderColor;

		public static void UpdateHorizontalTextAlignment(this MauiTextView textView, IEditor editor)
		{
			// We don't have a FlowDirection yet, so there's nothing to pass in here. 
			// TODO ezhart Update this when FlowDirection is available 
			// (or update the extension to take an IEditor instead of an alignment and work it out from there)

			// TODO COCOA
		//	textView.TextAlignment = editor.HorizontalTextAlignment.ToNative(true);
		}
	}
}
