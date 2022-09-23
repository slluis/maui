using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Internals;
using ObjCRuntime;
using AppKit;

namespace Microsoft.Maui.Controls.Platform
{
	public static partial class FontExtensions
	{
		public static NSFont ToNSFont(this Font self, IFontManager fontManager)
		{
			if (self.IsDefault)
				return fontManager.DefaultFont;

			return fontManager.GetFont(self) ?? fontManager.DefaultFont;
		}

		public static NSFont ToNSFont<TFontElement>(this TFontElement fontElement) where TFontElement : Element, IFontElement
			=> fontElement.ToFont().ToNSFont(fontElement.RequireFontManager());
	}
}