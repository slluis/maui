﻿using System;
using System.Threading.Tasks;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using Microsoft.Maui.Graphics;
using AButton = AndroidX.AppCompat.Widget.AppCompatButton;
using ATextAlignment = Android.Views.TextAlignment;
using AView = Android.Views.View;

namespace Microsoft.Maui.Handlers
{
	public partial class SwipeItemMenuItemHandler : ElementHandler<ISwipeItemMenuItem, AView>
	{
		protected override void ConnectHandler(AView platformView)
		{
			base.ConnectHandler(platformView);
			PlatformView.ViewAttachedToWindow += OnViewAttachedToWindow;
		}

		void OnViewAttachedToWindow(object? sender, AView.ViewAttachedToWindowEventArgs e)
		{
			UpdateSize();
		}

		protected override void DisconnectHandler(AView platformView)
		{
			base.DisconnectHandler(platformView);
			platformView.ViewAttachedToWindow -= OnViewAttachedToWindow;
		}

		public static void MapTextColor(ISwipeItemMenuItemHandler handler, ITextStyle view)
		{
			(handler.PlatformView as TextView)?.UpdateTextColor(view);
		}

		public static void MapCharacterSpacing(ISwipeItemMenuItemHandler handler, ITextStyle view)
		{
			(handler.PlatformView as TextView)?.UpdateCharacterSpacing(view);
		}

		public static void MapFont(ISwipeItemMenuItemHandler handler, ITextStyle view)
		{
			var fontManager = handler.GetRequiredService<IFontManager>();

			(handler.PlatformView as TextView)?.UpdateFont(view, fontManager);
		}

		public static void MapText(ISwipeItemMenuItemHandler handler, ISwipeItemMenuItem view)
		{

			(handler.PlatformView as TextView)?.UpdateTextPlainText(view);

			if (handler is SwipeItemMenuItemHandler platformHandler)
				platformHandler.UpdateSize();
		}

		public static void MapBackground(ISwipeItemMenuItemHandler handler, ISwipeItemMenuItem view)
		{
			handler.PlatformView.UpdateBackground(handler.VirtualView.Background);

			var textColor = handler.VirtualView.GetTextColor()?.ToPlatform();

			if (handler.PlatformView is TextView textView)
			{
				if (textColor != null)
					textView.SetTextColor(textColor.Value);

				textView.TextAlignment = ATextAlignment.Center;
			}
		}

		public static void MapVisibility(ISwipeItemMenuItemHandler handler, ISwipeItemMenuItem view)
		{
			var swipeView = handler.PlatformView.Parent.GetParentOfType<MauiSwipeView>();
			if (swipeView != null)
				swipeView.UpdateIsVisibleSwipeItem(view);

			handler.PlatformView.Visibility = view.Visibility.ToPlatformVisibility();
		}

		protected override AView CreatePlatformElement()
		{
			_ = MauiContext?.Context ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

			var swipeButton = new AButton(MauiContext.Context);
			swipeButton.SetOnTouchListener(null);

			if (!string.IsNullOrEmpty(VirtualView.AutomationId))
				swipeButton.ContentDescription = VirtualView.AutomationId;

			return swipeButton;
		}

		int GetIconSize()
		{
			if (VirtualView is not IImageSourcePart imageSourcePart || imageSourcePart.Source == null)
				return 0;

			var mauiSwipeView = PlatformView.Parent.GetParentOfType<MauiSwipeView>();

			if (mauiSwipeView == null || MauiContext?.Context == null)
				return 0;

			int contentHeight = mauiSwipeView.Height;
			int contentWidth = (int)MauiContext.Context.ToPixels(SwipeViewExtensions.SwipeItemWidth);

			return Math.Min(contentHeight, contentWidth) / 2;
		}

		void UpdateSize()
		{
			var textSize = 0;
			var contentHeight = 0;

			var mauiSwipeView = PlatformView.Parent.GetParentOfType<MauiSwipeView>();
			if (mauiSwipeView == null)
				return;

			contentHeight = mauiSwipeView.Height;

			if (PlatformView is TextView textView)
			{
				textSize = !string.IsNullOrEmpty(textView.Text) ? (int)textView.TextSize : 0;
				var icons = textView.GetCompoundDrawables();
				if (icons.Length > 1 && icons[1] != null)
				{
					OnSetImageSource(icons[1]);
				}
			}

			var iconSize = GetIconSize();
			var buttonPadding = (contentHeight - (iconSize + textSize + 6)) / 2;
			PlatformView.SetPadding(0, buttonPadding, 0, buttonPadding);
		}

		void OnSetImageSource(Drawable? drawable)
		{
			if (drawable != null)
			{
				var iconSize = GetIconSize();
				var textColor = VirtualView.GetTextColor()?.ToPlatform();
				int drawableWidth = drawable.IntrinsicWidth;
				int drawableHeight = drawable.IntrinsicHeight;

				if (drawableWidth > drawableHeight)
				{
					var iconWidth = iconSize;
					var iconHeight = drawableHeight * iconWidth / drawableWidth;
					drawable.SetBounds(0, 0, iconWidth, iconHeight);
				}
				else
				{
					var iconHeight = iconSize;
					var iconWidth = drawableWidth * iconHeight / drawableHeight;
					drawable.SetBounds(0, 0, iconWidth, iconHeight);
				}

				if (textColor != null)
					drawable.SetColorFilter(textColor.Value, FilterMode.SrcAtop);
			}

			(PlatformView as TextView)?.SetCompoundDrawables(null, drawable, null, null);
		}
	}
}
