using System;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.LifecycleEvents;
using UIKit;

namespace Microsoft.Maui.LifecycleEvents
{
	public static partial class AppHostBuilderExtensions
	{
		internal static MauiAppBuilder ConfigureCrossPlatformLifecycleEvents(this MauiAppBuilder builder) =>
			builder.ConfigureLifecycleEvents(events => events.AddiOS(OnConfigureLifeCycle));

		static void OnConfigureLifeCycle(IiOSLifecycleBuilder iOS)
		{
			iOS
				.FinishedLaunching((app, launchOptions) =>
				{
					app.GetWindow()?.Created();
					return true;
				})
				.WillEnterForeground(app =>
				{
					app.GetWindow()?.Resumed();
				})
				.OnActivated(app =>
				{
					app.GetWindow()?.Activated();
				})
				.OnResignActivation(app =>
				{
					app.GetWindow()?.Deactivated();
				})
				.DidEnterBackground(app =>
				{
					app.GetWindow()?.Stopped();
				})
				.WillTerminate(app =>
				{
					// By this point if we were a multi window app, the GetWindow would be null anyway
					app.GetWindow()?.Destroying();
				})
				.SceneDidDisconnect(scene =>
				{
					if (scene is UIWindowScene windowScene)
					{
						windowScene.GetWindow()?.Destroying();
					}
				});
		}
	}
}
