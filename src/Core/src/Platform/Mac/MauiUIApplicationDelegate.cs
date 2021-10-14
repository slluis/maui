using System;
using Foundation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.LifecycleEvents;
using AppKit;

namespace Microsoft.Maui
{
	public abstract class MauiUIApplicationDelegate : NSApplicationDelegate, INSApplicationDelegate
	{
		MauiContext _applicationContext = null!;
		WeakReference<IWindow>? _virtualWindow = null;
		NSDictionary launchOptions = new NSDictionary();

		internal IWindow? VirtualWindow
		{
			get
			{
				IWindow? window = null;
				_virtualWindow?.TryGetTarget(out window);
				return window;
			}
		}

		protected MauiUIApplicationDelegate()
		{
			Current = this;
		}

		protected abstract MauiApp CreateMauiApp();


		public override void WillFinishLaunching(NSNotification notification)
		{
			var mauiApp = CreateMauiApp();

			Services = mauiApp.Services;

			_applicationContext = new MauiContext(Services, this);

			Services?.InvokeLifecycleEvents<MacLifecycle.WillFinishLaunching>(del => del(NSApplication.SharedApplication, launchOptions));
		}

		public override void DidFinishLaunching(NSNotification notification)
		{
			Application = Services.GetRequiredService<IApplication>();

			this.SetApplicationHandler(Application, _applicationContext);

			var uiWindow = CreateNativeWindow();

			Window = uiWindow;

			Window.MakeKeyAndOrderFront(Window);

			NSApplication.SharedApplication.ActivateIgnoringOtherApps(true);

			Services?.InvokeLifecycleEvents<MacLifecycle.FinishedLaunching>(del => del(NSApplication.SharedApplication, launchOptions));
		}

		NSWindow CreateNativeWindow()
		{
			var uiWindow = new NSWindow();

			var mauiContext = _applicationContext.MakeScoped(uiWindow);

			Services?.InvokeLifecycleEvents<MacLifecycle.OnMauiContextCreated>(del => del(mauiContext));

			var activationState = new ActivationState(mauiContext);
			var window = Application.CreateWindow(activationState);
			_virtualWindow = new WeakReference<IWindow>(window);
			uiWindow.SetWindowHandler(window, mauiContext);

			return uiWindow;
		}

		public override void OpenUrls(NSApplication application, NSUrl[] urls)
		{
			//var wasHandled = false;

			//Services?.InvokeLifecycleEvents<MacLifecycle.OpenUrl>(del =>
			//{
			//	wasHandled = del(application, url, options) || wasHandled;
			//});

			//return wasHandled || base.OpenUrl(application, url, options);
		}

		//public override void PerformActionForShortcutItem(NSApplication application, UIApplicationShortcutItem shortcutItem, UIOperationHandler completionHandler)
		//{
		//	Services?.InvokeLifecycleEvents<iOSLifecycle.PerformActionForShortcutItem>(del => del(application, shortcutItem, completionHandler));
		//}

		//public override bool ContinueUserActivity(NSApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
		//{
		//	var wasHandled = false;

		//	Services?.InvokeLifecycleEvents<iOSLifecycle.ContinueUserActivity>(del =>
		//	{
		//		wasHandled = del(application, userActivity, completionHandler) || wasHandled;
		//	});

		//	return wasHandled || base.ContinueUserActivity(application, userActivity, completionHandler);
		//}

		//public override void OnActivated(NSApplication application)
		//{
		//	Services?.InvokeLifecycleEvents<MacLifecycle.OnActivated>(del => del(application));
		//}

		//public override void OnResignActivation(NSApplication application)
		//{
		//	Services?.InvokeLifecycleEvents<MacLifecycle.OnResignActivation>(del => del(application));
		//}

		//public override void WillTerminate(NSApplication application)
		//{
		//	Services?.InvokeLifecycleEvents<MacLifecycle.WillTerminate>(del => del(application));
		//}

		//public override void DidEnterBackground(NSApplication application)
		//{
		//	Services?.InvokeLifecycleEvents<MacLifecycle.DidEnterBackground>(del => del(application));
		//}

		//public override void WillEnterForeground(NSApplication application)
		//{
		//	Services?.InvokeLifecycleEvents<iOSLifecycle.WillEnterForeground>(del => del(application));
		//}*/

		public static MauiUIApplicationDelegate Current { get; private set; } = null!;

		public NSWindow? Window { get; set; }

		public IServiceProvider Services { get; protected set; } = null!;

		public IApplication Application { get; protected set; } = null!;
	}
}
