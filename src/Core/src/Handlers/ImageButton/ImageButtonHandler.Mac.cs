﻿using System;
using System.Collections.Generic;
using System.Text;
using AppKit;

namespace Microsoft.Maui.Handlers
{
	// TODO COCOA
	public partial class ImageButtonHandler : ViewHandler<IImageButton, NSButton>
	{
		AppKit.NSImageView dummy = new NSImageView();

		protected override NSButton CreateNativeView()
		{
			throw new NotImplementedException();
//			return new UIButton(UIButtonType.System);
		}

		void OnSetImageSource(NSImage? obj)
		{
/*			NativeView.SetImage(obj?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
			NativeView.HorizontalAlignment = UIControlContentHorizontalAlignment.Fill;
			NativeView.VerticalAlignment = UIControlContentVerticalAlignment.Fill;*/
		}

		protected override void ConnectHandler(NSButton nativeView)
		{
/*			nativeView.TouchUpInside += OnButtonTouchUpInside;
			nativeView.TouchUpOutside += OnButtonTouchUpOutside;
			nativeView.TouchDown += OnButtonTouchDown;*/
			base.ConnectHandler(nativeView);
		}

		protected override void DisconnectHandler(NSButton nativeView)
		{
/*			nativeView.TouchUpInside -= OnButtonTouchUpInside;
			nativeView.TouchUpOutside -= OnButtonTouchUpOutside;
			nativeView.TouchDown -= OnButtonTouchDown;*/
			base.DisconnectHandler(nativeView);
			SourceLoader.Reset();
		}

		void OnButtonTouchUpInside(object? sender, EventArgs e)
		{
			VirtualView?.Released();
			VirtualView?.Clicked();
		}

		void OnButtonTouchUpOutside(object? sender, EventArgs e)
		{
			VirtualView?.Released();
		}

		void OnButtonTouchDown(object? sender, EventArgs e)
		{
			VirtualView?.Pressed();
		}

		// TODO COCOA
		AppKit.NSImageView IImageHandler.TypedNativeView => dummy;
	}
}
