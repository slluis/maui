﻿using System;
using Foundation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.LifecycleEvents;
using AppKit;
using CoreGraphics;

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
			var uiWindow = new NSWindow(new CGRect(0, 0, 500, 500), NSWindowStyle.Titled | NSWindowStyle.Resizable | NSWindowStyle.Closable, NSBackingStore.Buffered, false);

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
			var wasHandled = false;

			Services?.InvokeLifecycleEvents<MacLifecycle.OpenUrls>(del =>
			{
				wasHandled = del(application, urls);
			});

			if (!wasHandled)
				base.OpenUrls(application, urls);
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

		public override void DidBecomeActive(NSNotification notification)
		{
			Services?.InvokeLifecycleEvents<MacLifecycle.OnActivated>(del => del(NSApplication.SharedApplication));
		}

		public override void DidResignActive(NSNotification notification)
		{
			Services?.InvokeLifecycleEvents<MacLifecycle.OnResignActivation>(del => del(NSApplication.SharedApplication));
		}

		public override void WillTerminate(NSNotification notification)
		{
			Services?.InvokeLifecycleEvents<MacLifecycle.WillTerminate>(del => del(NSApplication.SharedApplication));
		}

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