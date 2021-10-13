using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.LifecycleEvents;

namespace Microsoft.Maui.Controls.Hosting
{
	public static partial class AppHostBuilderExtensions
	{
		internal static MauiAppBuilder ConfigureCompatibilityLifecycleEvents(this MauiAppBuilder builder) =>
			   builder.ConfigureLifecycleEvents(events => events.AddMac(OnConfigureLifeCycle));

		static void OnConfigureLifeCycle(IMacLifecycleBuilder mac)
		{
			//iOS.WillFinishLaunching((app, options) =>
			//	{
			//		// This is the initial Init to set up any system services registered by
			//		// Forms.Init(). This happens before any UI has appeared.
			//		// This creates a dummy MauiContext.

			//		var services = MauiUIApplicationDelegate.Current.Services;
			//		var mauiContext = new MauiContext(services);
			//		var state = new ActivationState(mauiContext);
			//		Forms.Init(state, new InitializationOptions { Flags = InitializationFlags.SkipRenderers });
			//		return true;
			//	})
			//	.OnMauiContextCreated((mauiContext) =>
			//	{
			//		// This is the final Init that sets up the real context from the application.

			//		var state = new ActivationState(mauiContext);
			//		Forms.Init(state);
			//	});
		}
	}
}
