using System;
using System.Threading.Tasks;
using Foundation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Graphics;
using AppKit;

namespace Microsoft.Maui.Handlers
{
	public partial class ButtonHandler : ViewHandler<IButton, NSButton>
	{
		protected override NSButton CreatePlatformView()
		{
			var button = new MauiButton();
			return button;
		}

		protected override void ConnectHandler(NSButton nativeView)
		{
			if (nativeView is MauiButton mauiButton)
			{
				mauiButton.MouseLeftDown += OnButtonTouchDown;
				mauiButton.MouseLeftUp += OnButtonTouchUpInside;

			}
			base.ConnectHandler(nativeView);
		}

		protected override void DisconnectHandler(NSButton nativeView)
		{
			if (nativeView is MauiButton mauiButton)
			{
				mauiButton.MouseLeftDown -= OnButtonTouchDown;
				mauiButton.MouseLeftUp -= OnButtonTouchUpInside;

			}
			base.DisconnectHandler(nativeView);
		}

		public static void MapText(IButtonHandler handler, IText button)
		{
			handler.PlatformView?.UpdateText(button);
			// Any text update requires that we update any attributed string formatting
			MapFormatting(handler, button);
		}

		public static void MapTextColor(IButtonHandler handler, ITextStyle button)
		{
			handler.PlatformView?.UpdateTextColor(button);
		}

		public static void MapCharacterSpacing(IButtonHandler handler, ITextStyle button)
		{
			handler.PlatformView?.UpdateCharacterSpacing(button);
		}

		public static void MapPadding(IButtonHandler handler, IButton button)
		{
			handler.PlatformView?.UpdatePadding(button);
		}

		public static void MapFont(IButtonHandler handler, ITextStyle button)
		{
			var fontManager = handler.GetRequiredService<IFontManager>();
			handler.PlatformView?.UpdateFont(button, fontManager);
		}

		public static void MapFormatting(IButtonHandler handler, IText button)
		{
			// Update all of the attributed text formatting properties
			handler.PlatformView?.UpdateCharacterSpacing(button);
		}

		void OnSetImageSource(NSImage? image)
		{
			if (image != null)
			{
				PlatformView.Image = image;
				//TODO: Improve
				//SetImage(image.ImageWithRenderingMode(NSImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
			}
			else
			{
				PlatformView.Image = null;
			}
		}

		public static void MapImageSource(IButtonHandler handler, IImage image) =>
			MapImageSourceAsync(handler, image).FireAndForget(handler);

		public static Task MapImageSourceAsync(IButtonHandler handler, IImage image)
		{
			if (image.Source == null)
			{
				return Task.CompletedTask;
			}

			return handler.ImageSourceLoader.UpdateImageSourceAsync();
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

		public static void MapStrokeColor(IButtonHandler handler, IButtonStroke buttonStroke)
		{
			handler.PlatformView?.UpdateStrokeColor(buttonStroke);
		}

		public static void MapStrokeThickness(IButtonHandler handler, IButtonStroke buttonStroke)
		{
			handler.PlatformView?.UpdateStrokeThickness(buttonStroke);
		}

		public static void MapCornerRadius(IButtonHandler handler, IButtonStroke buttonStroke)
		{
			handler.PlatformView?.UpdateCornerRadius(buttonStroke);
		}
	}
}