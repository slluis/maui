using Foundation;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Samples.Mac
{
	[Register(nameof(AppDelegate))]
	public partial class AppDelegate : MauiUIApplicationDelegate
	{
		protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
	}

}
