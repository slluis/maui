using Foundation;
using AppKit;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls;

namespace Maui.Controls.Sample.Macos
{
	public class App: Application
	{
		public App()
		{
			var page = new ContentPage();
			page.Title = "Hello!";
			var label = new Label
			{
				Text = "Hi Maui"
			};
			page.Content = label;
			MainPage = page;
		}
	}
}