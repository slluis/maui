﻿using System;
using System.Threading.Tasks;
using Microsoft.Maui.Handlers;

#if __IOS__ || MACCATALYST
using NativeImage = UIKit.UIImage;
using NativeView = UIKit.UIView;
#elif __MACOS__
using NativeImage = AppKit.NSImage;
using NativeView = AppKit.NSView;
#elif MONOANDROID
using NativeImage = Android.Graphics.Drawables.Drawable;
using NativeView = Android.Views.View;
#elif WINDOWS
using NativeImage = Microsoft.UI.Xaml.Media.ImageSource;
using NativeView = Microsoft.UI.Xaml.FrameworkElement;
#elif NETSTANDARD || (NET6_0 && !IOS && !ANDROID)
using NativeView = System.Object;
using NativeImage = System.Object;
#endif

namespace Microsoft.Maui
{
	public partial class ImageSourcePartLoader
	{
		IImageSourceServiceProvider? _imageSourceServiceProvider;
		IImageSourceServiceProvider ImageSourceServiceProvider =>
			_imageSourceServiceProvider ??= Handler.GetRequiredService<IImageSourceServiceProvider>();

		readonly Func<IImageSourcePart?> _imageSourcePart;
		Action<NativeImage?>? SetImage { get; }
		NativeView? NativeView => Handler.NativeView as NativeView;

		internal ImageSourceServiceResultManager SourceManager { get; } = new ImageSourceServiceResultManager();

		IElementHandler Handler { get; }

		public ImageSourcePartLoader(
			IElementHandler handler,
			Func<IImageSourcePart?> imageSourcePart,
			Action<NativeImage?> setImage)
		{
			Handler = handler;
			_imageSourcePart = imageSourcePart;
			SetImage = setImage;
		}

		internal ImageSourcePartLoader(
			IElementHandler handler,
			Func<IImageSource?> imageSource,
			Action<NativeImage?> setImage)
		{
			Handler = handler;
			var wrapper = new ImageSourcePartWrapper(imageSource);
			_imageSourcePart = () => wrapper;
			SetImage = setImage;
		}

		public void Reset()
		{
			SourceManager.Reset();
		}

		public async Task UpdateImageSourceAsync()
		{
			if (NativeView != null)
			{
				var token = this.SourceManager.BeginLoad();
				var imageSource = _imageSourcePart();

				if (imageSource != null)
				{
#if __IOS__ || __ANDROID__ || WINDOWS
					var result = await imageSource.UpdateSourceAsync(NativeView, ImageSourceServiceProvider, SetImage!, token)
						.ConfigureAwait(false);

					SourceManager.CompleteLoad(result);
#else
					await Task.CompletedTask;
#endif
				}
				else
				{
					SetImage?.Invoke(null);
					SourceManager.CompleteLoad(null);
				}
			}
		}

		// TODO MAUI: This is currently here so that Button can continue to use IImageSource
		// At a later point once we further define the interface for IButtonHandler we will probably
		// change IButton to return an IImageSourcePart and we can get rid of this class
		class ImageSourcePartWrapper : IImageSourcePart
		{
			readonly Func<IImageSource?> _imageSource;

			public ImageSourcePartWrapper(Func<IImageSource?> imageSource)
			{
				_imageSource = imageSource;
			}

			IImageSource? IImageSourcePart.Source => _imageSource.Invoke();

			bool IImageSourcePart.IsAnimationPlaying => false;

			void IImageSourcePart.UpdateIsLoading(bool isLoading)
			{
			}
		}
	}
}
