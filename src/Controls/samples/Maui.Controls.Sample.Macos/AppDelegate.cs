using AppKit;
using Foundation;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;

namespace Maui.Controls.Sample.Macos
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
		public AppDelegate()
		{
		}

		protected override MauiApp CreateMauiApp()
		{
			var appBuilder = MauiApp.CreateBuilder();
			appBuilder.UseMauiApp<App>();
			return appBuilder.Build();
		}
	}
}

