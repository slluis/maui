using Foundation;
using AppKit;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

#if !NET6_0
using Microsoft.Maui.Controls;
#endif

namespace Sample.Mac
{
	[Register("AppDelegate")]
	public class AppDelegate : MauiUIApplicationDelegate
	{
		protected override MauiApp CreateMauiApp()
		{
			var appBuilder = MauiApp.CreateBuilder();
			return appBuilder.Build();
		}
	}
}