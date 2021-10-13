using Foundation;
using AppKit;

namespace Microsoft.Maui.LifecycleEvents
{
	public static class MacLifecycle
	{
		//public delegate bool ContinueUserActivity(NSApplication application, NSUserActivity userActivity, NSApplicationRestorationHandler completionHandler);
		public delegate void DidEnterBackground(NSApplication application);
		public delegate bool WillFinishLaunching(NSApplication application, NSDictionary launchOptions);
		public delegate bool FinishedLaunching(NSApplication application, NSDictionary launchOptions);
		public delegate void OnActivated(NSApplication application);
		public delegate void OnResignActivation(NSApplication application);
		public delegate bool OpenUrl(NSApplication app, NSUrl url, NSDictionary options);
		//public delegate void PerformActionForShortcutItem(NSApplication application, NSApplicationShortcutItem shortcutItem, UIOperationHandler completionHandler);
		public delegate void WillEnterForeground(NSApplication application);
		public delegate void WillTerminate(NSApplication application);

		// Internal events
		internal delegate void OnMauiContextCreated(IMauiContext mauiContext);
	}
}