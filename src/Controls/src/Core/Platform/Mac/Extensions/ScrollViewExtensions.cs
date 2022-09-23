using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using AppKit;

namespace Microsoft.Maui.Controls.Platform
{
	public static class ScrollViewExtensions
	{
		public static void UpdateShouldDelayContentTouches(this NSScrollView platformView, ScrollView scrollView)
		{
			//platformView.DelaysContentTouches = scrollView.OnThisPlatform().ShouldDelayContentTouches();
		}
	}
}