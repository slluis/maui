using System;
using CoreGraphics;
using Foundation;
using AppKit;

namespace Microsoft.Maui.Platform.Mac
{
	public class MauiTextField : NSTextField
	{
		public MauiTextField(CGRect frame)
			: base(frame)
		{
		}

		public MauiTextField()
		{
		}
	}
}