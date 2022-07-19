﻿using Microsoft.Maui.Handlers;
using Microsoft.Maui.LifecycleEvents;

namespace Microsoft.Maui.Platform
{
	public static class ApplicationExtensions
	{
		public static void CreatePlatformWindow(this UI.Xaml.Application platformApplication, IApplication application, UI.Xaml.LaunchActivatedEventArgs? args) =>
			platformApplication.CreatePlatformWindow(application, new OpenWindowRequest(LaunchArgs: args));

		public static void CreatePlatformWindow(this UI.Xaml.Application platformApplication, IApplication application, OpenWindowRequest? args)
		{
			if (application.Handler?.MauiContext is not IMauiContext applicationContext)
				return;

			var winuiWndow = new MauiWinUIWindow();

			var mauiContext = applicationContext!.MakeWindowScope(winuiWndow, out var windowScope);

			applicationContext.Services.InvokeLifecycleEvents<WindowsLifecycle.OnMauiContextCreated>(del => del(mauiContext));

			var activationState = args?.State is not null
				? new ActivationState(mauiContext, args.State)
				: new ActivationState(mauiContext, args?.LaunchArgs);

			var window = application.CreateWindow(activationState);

			winuiWndow.SetWindowHandler(window, mauiContext);

			applicationContext.Services.InvokeLifecycleEvents<WindowsLifecycle.OnWindowCreated>(del => del(winuiWndow));

			winuiWndow.Activate();
		}
	}
}