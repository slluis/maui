﻿#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;

namespace Microsoft.Maui.Handlers
{
	public partial class NavigationViewHandler : ViewHandler<IStackNavigationView, UIView>, IPlatformViewHandler
	{
		ControlsNavigationController? _controlsNavigationController;
		UIViewController? IPlatformViewHandler.ViewController => _controlsNavigationController;

		public IStackNavigationView NavigationView => ((IStackNavigationView)VirtualView);

		public IReadOnlyList<IView> NavigationStack { get; private set; } = new List<IView>();

		protected override UIView CreatePlatformView()
		{
			_controlsNavigationController = new ControlsNavigationController(this);

			if (_controlsNavigationController.View == null)
				throw new NullReferenceException("ControlsNavigationController.View is null");

			return _controlsNavigationController.View;
		}

		public static void RequestNavigation(INavigationViewHandler arg1, IStackNavigation arg2, object? arg3)
		{
			if (arg1 is NavigationViewHandler platformHandler && arg3 is NavigationRequest navigationRequest)
			{
				platformHandler.NavigationStack = navigationRequest.NavigationStack;
			}

			//if (arg3 is NavigationRequest args)
			//	arg1.OnPushRequested(args);
		}

		//void OnPushRequested(NavigationRequest e)
		//{
		//	_controlsNavigationController?
		//		.OnPushRequested(e, this.MauiContext!);
		//}

		//void OnPopRequested(NavigationRequest e)
		//{
		//	_controlsNavigationController?
		//		.OnPopRequestedAsync(e)
		//		.FireAndForget((exc) => { });
		//}

		internal void SendPopping(Task popTask)
		{
			//if (VirtualView is not INavigationView nvi)
			//	return;

			//// TODO MAUI
			//nvi
			//	.PopAsync()
			//	.FireAndForget((e) =>
			//	{
			//		//Log.Warning(nameof(NavigationViewHandler), $"{e}");
			//	});
		}

		//protected override void ConnectHandler(UIView platformView)
		//{
		//	base.ConnectHandler(platformView);

		//	if (VirtualView == null || MauiContext == null || _controlsNavigationController == null)
		//		return;

		//	_controlsNavigationController.LoadPages(this.MauiContext);
		//}

		//public static void MapPadding(NavigationViewHandler handler, INavigationView view) { }

		//public static void MapTitleIcon(NavigationViewHandler handler, INavigationView view) { }

		//public static void MapTitleView(NavigationViewHandler handler, INavigationView view) { }

		////public static void MapBarBackground(NavigationViewHandler handler, INavigationView view)
		////{
		////	var NavPage = handler.VirtualView;
		////	var barBackgroundBrush = NavPage.BarBackground;

		////	if (Brush.IsNullOrEmpty(barBackgroundBrush) &&
		////		NavPage.BarBackgroundColor != null)
		////		barBackgroundBrush = new SolidColorBrush(NavPage.BarBackgroundColor);

		////	if (barBackgroundBrush == null)
		////		return;

		////	var navController = handler._controlsNavigationController;
		////	var NavigationBar = navController.NavigationBar;

		////	if (OperatingSystem.IsIOSVersionAtLeast(13) || OperatingSystem.IsTvOSVersionAtLeast(13))
		////	{
		////		var navigationBarAppearance = NavigationBar.StandardAppearance;

		////		navigationBarAppearance.ConfigureWithOpaqueBackground();

		////		//if (barBackgroundColor == null)
		////		//{
		////		//	navigationBarAppearance.BackgroundColor = ColorExtensions.BackgroundColor;

		////		//	var parentingViewController = GetParentingViewController();
		////		//	parentingViewController?.SetupDefaultNavigationBarAppearance();
		////		//}
		////		//else
		////		//	navigationBarAppearance.BackgroundColor = barBackgroundColor.ToPlatform();

		////		var backgroundImage = NavigationBar.GetBackgroundImage(barBackgroundBrush);
		////		navigationBarAppearance.BackgroundImage = backgroundImage;

		////		NavigationBar.CompactAppearance = navigationBarAppearance;
		////		NavigationBar.StandardAppearance = navigationBarAppearance;
		////		NavigationBar.ScrollEdgeAppearance = navigationBarAppearance;
		////	}
		////	else
		////	{
		////		var backgroundImage = NavigationBar.GetBackgroundImage(barBackgroundBrush);
		////		NavigationBar.SetBackgroundImage(backgroundImage, UIBarMetrics.Default);
		////	}
		////}

		////public static void MapBarTextColor(NavigationViewHandler handler, NavigationView view)
		////{
		////	var NavPage = handler.VirtualView;

		////	var navController = handler._controlsNavigationController;
		////	var NavigationBar = navController.NavigationBar;

		////	var barTextColor = NavPage.BarTextColor;
		////	if (NavigationBar == null)
		////		return;

		////	// Determine new title text attributes via global static data
		////	var globalTitleTextAttributes = UINavigationBar.Appearance.TitleTextAttributes;
		////	var titleTextAttributes = new UIStringAttributes
		////	{
		////		ForegroundColor = barTextColor == null ? globalTitleTextAttributes?.ForegroundColor : barTextColor.ToPlatform(),
		////		Font = globalTitleTextAttributes?.Font
		////	};

		////	// Determine new large title text attributes via global static data
		////	var largeTitleTextAttributes = titleTextAttributes;
		////	if (OperatingSystem.IsIOSVersionAtLeast(11) || OperatingSystem.IsTvOSVersionAtLeast(11))
		////	{
		////		var globalLargeTitleTextAttributes = UINavigationBar.Appearance.LargeTitleTextAttributes;

		////		largeTitleTextAttributes = new UIStringAttributes
		////		{
		////			ForegroundColor = barTextColor == null ? globalLargeTitleTextAttributes?.ForegroundColor : barTextColor.ToPlatform(),
		////			Font = globalLargeTitleTextAttributes?.Font
		////		};
		////	}

		////	if (OperatingSystem.IsIOSVersionAtLeast(13) || OperatingSystem.IsTvOSVersionAtLeast(13))
		////	{
		////		if (NavigationBar.CompactAppearance != null)
		////		{
		////			NavigationBar.CompactAppearance.TitleTextAttributes = titleTextAttributes;
		////			NavigationBar.CompactAppearance.LargeTitleTextAttributes = largeTitleTextAttributes;
		////		}

		////		NavigationBar.StandardAppearance.TitleTextAttributes = titleTextAttributes;
		////		NavigationBar.StandardAppearance.LargeTitleTextAttributes = largeTitleTextAttributes;

		////		if (NavigationBar.ScrollEdgeAppearance != null)
		////		{
		////			NavigationBar.ScrollEdgeAppearance.TitleTextAttributes = titleTextAttributes;
		////			NavigationBar.ScrollEdgeAppearance.LargeTitleTextAttributes = largeTitleTextAttributes;
		////		}
		////	}
		////	else
		////	{
		////		NavigationBar.TitleTextAttributes = titleTextAttributes;

		////		if (OperatingSystem.IsIOSVersionAtLeast(11) || OperatingSystem.IsTvOSVersionAtLeast(11))
		////			NavigationBar.LargeTitleTextAttributes = largeTitleTextAttributes;
		////	}

		////	//// set Tint color (i. e. Back Button arrow and Text)
		////	//var iconColor = Current != null ? NavigationView.GetIconColor(Current) : null;
		////	//if (iconColor == null)
		////	//	iconColor = barTextColor;

		////	//NavigationBar.TintColor = iconColor == null || NavPage.OnThisPlatform().GetStatusBarTextColorMode() == StatusBarTextColorMode.DoNotAdjust
		////	//	? UINavigationBar.Appearance.TintColor
		////	//	: iconColor.ToPlatform();
		////}


		//protected override void ConnectHandler(UIView platformView)
		//{
		//	base.ConnectHandler(platformView);

		//	if (VirtualView == null)
		//		return;

		//	VirtualView.PushRequested += OnPushRequested;
		//	VirtualView.PopRequested += OnPopRequested;
		//	_controlsNavigationController.LoadPages(this.MauiContext);

		//	//VirtualView.PopToRootRequested += OnPopToRootRequested;
		//	//VirtualView.RemovePageRequested += OnRemovedPageRequested;
		//	//VirtualView.InsertPageBeforeRequested += OnInsertPageBeforeRequested;
		//}

		//protected override void DisconnectHandler(UIView platformView)
		//{
		//	base.DisconnectHandler(platformView);

		//	if (VirtualView == null)
		//		return;

		//	VirtualView.PushRequested -= OnPushRequested;
		//	VirtualView.PopRequested -= OnPopRequested;
		//	//VirtualView.PopToRootRequested -= OnPopToRootRequested;
		//	//VirtualView.RemovePageRequested -= OnRemovedPageRequested;
		//	//VirtualView.InsertPageBeforeRequested -= OnInsertPageBeforeRequested;
		//}

		//void OnPushRequested(object? sender, NavigationRequestedEventArgs e)
		//{
		//	_controlsNavigationController?
		//		.OnPushRequested(e, this.MauiContext);
		//}

		//void OnPopRequested(object? sender, NavigationRequestedEventArgs e)
		//{
		//	_controlsNavigationController?
		//		.OnPopRequestedAsync(e)
		//		.FireAndForget((exc) => Log.Warning(nameof(NavigationView), $"{exc}"));
		//}

		//internal void SendPopping(Task popTask)
		//{
		//	if (VirtualView == null)
		//		return;

		//	VirtualView.PopAsyncInner(false, true, true)
		//		.FireAndForget((exc) => Log.Warning(nameof(NavigationView), $"{exc}"));
		//}
	}
}
