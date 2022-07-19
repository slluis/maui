﻿using Foundation;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Platform;
using ObjCRuntime;
using UIKit;

namespace Microsoft.Maui
{
	public class MauiUISceneDelegate : UIResponder, IUIWindowSceneDelegate
	{
		[Export("window")]
		public virtual UIWindow? Window { get; set; }

		[Export("scene:willConnectToSession:options:")]
		[System.Runtime.Versioning.SupportedOSPlatform("ios13.0")]
		[System.Runtime.Versioning.SupportedOSPlatform("tvos13.0")]
		public virtual void WillConnect(UIScene scene, UISceneSession session, UISceneConnectionOptions connectionOptions)
		{
			MauiUIApplicationDelegate.Current?.Services?.InvokeLifecycleEvents<iOSLifecycle.SceneWillConnect>(del => del(scene, session, connectionOptions));

			if (session.Configuration.Name == MauiUIApplicationDelegate.MauiSceneConfigurationKey && MauiUIApplicationDelegate.Current?.Application != null)
			{
				this.CreatePlatformWindow(MauiUIApplicationDelegate.Current.Application, scene, session, connectionOptions);
			}
		}

		[Export("sceneDidDisconnect:")]
		public virtual void DidDisconnect(UIScene scene)
		{
			MauiUIApplicationDelegate.Current?.Services?.InvokeLifecycleEvents<iOSLifecycle.SceneDidDisconnect>(del => del(scene));
		}

		[Export("stateRestorationActivityForScene:")]
		public virtual NSUserActivity? GetStateRestorationActivity(UIScene scene)
		{
			var window = Window.GetWindow();
			if (window is null)
				return null;

			var persistedState = new PersistedState();

			window.Backgrounding(persistedState);

			// the user saved nothing, so there is nothing to restore
			if (persistedState.Count == 0)
				return null;

			return persistedState.ToUserActivity(window.GetType().FullName!);
		}
	}
}