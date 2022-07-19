﻿using System;
using System.Collections.Generic;
using System.Text;
using CoreGraphics;
using Microsoft.Extensions.Logging;
using ObjCRuntime;
using UIKit;

namespace Microsoft.Maui.Handlers
{
	public partial class SwipeItemMenuItemHandler : ElementHandler<ISwipeItemMenuItem, UIButton>
	{
		protected override UIButton CreatePlatformElement()
		{
			return new UIButton(UIButtonType.Custom)
			{
				RestorationIdentifier = VirtualView.Text,
				UserInteractionEnabled = false,
			};
		}

		public static void MapTextColor(ISwipeItemMenuItemHandler handler, ISwipeItemMenuItem view)
		{
			var color = view.GetTextColor();
			if (color != null)
				handler.PlatformView.SetTitleColor(color.ToPlatform(), UIControlState.Normal);
		}

		public static void MapCharacterSpacing(ISwipeItemMenuItemHandler handler, ITextStyle view) { }

		public static void MapFont(ISwipeItemMenuItemHandler handler, ITextStyle view) { }

		public static void MapText(ISwipeItemMenuItemHandler handler, ISwipeItemMenuItem view)
		{
			handler.PlatformView.RestorationIdentifier = view.Text;
			handler.PlatformView.SetTitle(view.Text, UIControlState.Normal);
		}

		public static void MapBackground(ISwipeItemMenuItemHandler handler, ISwipeItemMenuItem view)
		{
			handler.PlatformView.UpdateBackground(view.Background);
		}

		public static void MapVisibility(ISwipeItemMenuItemHandler handler, ISwipeItemMenuItem view)
		{
			var swipeView = handler.PlatformView.GetParentOfType<MauiSwipeView>();
			if (swipeView != null)
				swipeView.UpdateIsVisibleSwipeItem(view);

			handler.PlatformView.UpdateVisibility(view.Visibility);
		}

		void OnSetImageSource(UIImage? image)
		{
			if (image == null)
			{
				PlatformView.SetImage(null, UIControlState.Normal);
			}
			else
			{
				var maxWidth = PlatformView.Frame.Width * 0.5f;
				var maxHeight = PlatformView.Frame.Height * 0.5f;

				var resizedImage = MaxResizeSwipeItemIconImage(image, maxWidth, maxHeight);

				try
				{
					PlatformView.SetImage(resizedImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate), UIControlState.Normal);
					var tintColor = VirtualView.GetTextColor();

					if (tintColor != null)
						PlatformView.TintColor = tintColor.ToPlatform();
				}
				catch (Exception)
				{
					// UIImage ctor throws on file not found if MonoTouch.ObjCRuntime.Class.ThrowOnInitFailure is true;
					MauiContext?.CreateLogger<SwipeItemMenuItemHandler>()?.LogWarning("Can not load SwipeItem Icon");
				}
			}
		}

		UIImage MaxResizeSwipeItemIconImage(UIImage sourceImage, nfloat maxWidth, nfloat maxHeight)
		{
			var sourceSize = sourceImage.Size;
			var maxResizeFactor = Math.Min(maxWidth / sourceSize.Width, maxHeight / sourceSize.Height);

			if (maxResizeFactor > 1)
				return sourceImage;

			var width = maxResizeFactor * sourceSize.Width;
			var height = maxResizeFactor * sourceSize.Height;
			UIGraphics.BeginImageContextWithOptions(new CGSize((nfloat)width, (nfloat)height), false, 0);
			sourceImage.Draw(new CGRect(0, 0, (nfloat)width, (nfloat)height));
			var resultImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();

			return resultImage;
		}
	}
}
