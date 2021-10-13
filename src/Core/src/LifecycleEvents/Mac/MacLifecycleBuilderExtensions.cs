namespace Microsoft.Maui.LifecycleEvents
{
	public static class MacLifecycleBuilderExtensions
	{
		//public static IMacLifecycleBuilder ContinueUserActivity(this IMacLifecycleBuilder lifecycle, MacLifecycle.ContinueUserActivity del) => lifecycle.OnEvent(del);
		public static IMacLifecycleBuilder DidEnterBackground(this IMacLifecycleBuilder lifecycle, MacLifecycle.DidEnterBackground del) => lifecycle.OnEvent(del);
		public static IMacLifecycleBuilder WillFinishLaunching(this IMacLifecycleBuilder lifecycle, MacLifecycle.WillFinishLaunching del) => lifecycle.OnEvent(del);
		public static IMacLifecycleBuilder FinishedLaunching(this IMacLifecycleBuilder lifecycle, MacLifecycle.FinishedLaunching del) => lifecycle.OnEvent(del);
		public static IMacLifecycleBuilder OnActivated(this IMacLifecycleBuilder lifecycle, MacLifecycle.OnActivated del) => lifecycle.OnEvent(del);
		public static IMacLifecycleBuilder OnResignActivation(this IMacLifecycleBuilder lifecycle, MacLifecycle.OnResignActivation del) => lifecycle.OnEvent(del);
		public static IMacLifecycleBuilder OpenUrl(this IMacLifecycleBuilder lifecycle, MacLifecycle.OpenUrl del) => lifecycle.OnEvent(del);
//		public static IMacLifecycleBuilder PerformActionForShortcutItem(this IMacLifecycleBuilder lifecycle, MacLifecycle.PerformActionForShortcutItem del) => lifecycle.OnEvent(del);
		public static IMacLifecycleBuilder WillEnterForeground(this IMacLifecycleBuilder lifecycle, MacLifecycle.WillEnterForeground del) => lifecycle.OnEvent(del);
		public static IMacLifecycleBuilder WillTerminate(this IMacLifecycleBuilder lifecycle, MacLifecycle.WillTerminate del) => lifecycle.OnEvent(del);

		internal static IMacLifecycleBuilder OnMauiContextCreated(this IMacLifecycleBuilder lifecycle, MacLifecycle.OnMauiContextCreated del) => lifecycle.OnEvent(del);
	}
}