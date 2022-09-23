using System;
using CoreGraphics;
using Foundation;
using AppKit;
using RectangleF = CoreGraphics.CGRect;

namespace Microsoft.Maui.Platform
{
	public class NoCaretField : NSTextField
	{
		public NoCaretField() : base(new RectangleF())
		{
		/*	SpellCheckingType = NSTextSpellCheckingType.No;
			AutocorrectionType = NSTextAutocorrectionType.No;
			AutocapitalizationType = NSTextAutocapitalizationType.None;
	*/	}

		// TODO COCOA
/*		public override RectangleF GetCaretRectForPosition(NSTextPosition? position)
		{
			return RectangleF.Empty;
		}*/
	}
}