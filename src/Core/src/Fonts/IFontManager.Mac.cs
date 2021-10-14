﻿using System;
using AppKit;

namespace Microsoft.Maui
{
	public interface IFontManager
	{
		NSFont DefaultFont { get; }

		NSFont GetFont(Font font, double defaultFontSize = 0);
	}
}