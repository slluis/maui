using Microsoft.Maui;
using Microsoft.Maui.Controls;
using AppKit;

namespace Microsoft.Maui.Controls.Platform
{
	public static class LabelExtensions
	{
		public static void UpdateText(this MauiLabel nativeLabel, Label label)
		{
			switch (label.TextType)
			{
				case TextType.Html:
					nativeLabel.UpdateTextHtml(label);
					break;

				default:
					nativeLabel.UpdateTextPlainText(label);
					break;
			}
		}
	}
}
