﻿using CoreGraphics;
using AppKit;
using System;

namespace Microsoft.Maui.Handlers
{
	// TODO COCOA
	public partial class ActivityIndicatorHandler : ViewHandler<IActivityIndicator, MauiActivityIndicator>
	{
		protected override MauiActivityIndicator CreateNativeView() => new MauiActivityIndicator(CGRect.Empty, VirtualView)
		{
//			ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray
		};

		public static void MapIsRunning(ActivityIndicatorHandler handler, IActivityIndicator activityIndicator)
		{
			throw new NotImplementedException();
//			handler.NativeView?.UpdateIsRunning(activityIndicator);
		}

		public static void MapColor(ActivityIndicatorHandler handler, IActivityIndicator activityIndicator)
		{
			throw new NotImplementedException();
			// handler.NativeView?.UpdateColor(activityIndicator);
		}
	}
}