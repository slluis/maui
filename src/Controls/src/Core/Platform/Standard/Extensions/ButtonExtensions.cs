
using System;
#if __MACOS__
using AppKit;
#endif

namespace Microsoft.Maui.Controls.Platform
{
	public static class ButtonExtensions
	{
#if __MACOS__
		public static void UpdateContentLayout(this NSButton nativeButton, Button button)
		{
		}
#else
		public static void UpdateContentLayout(this object nativeButton, Button button)
		{
		}
#endif
	}
}
