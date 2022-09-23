#nullable enable
using System.Collections.Generic;
using Foundation;
using ObjCRuntime;
using AppKit;
using System;

namespace Microsoft.Maui.Platform
{
	public class MauiPicker : NSPopUpButton
	{
		public override bool IsFlipped => true;

		public MauiPicker()
		{
		}
	}
}
