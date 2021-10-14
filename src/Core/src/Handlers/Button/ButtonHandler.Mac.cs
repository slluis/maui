using System;
using System.Threading.Tasks;
using Foundation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Graphics;
using AppKit;

namespace Microsoft.Maui.Handlers
{
	class MauiButton : NSButton
	{
		public event EventHandler? MouseLeftUp;
		public event EventHandler? MouseLeftDown;

		public override bool IsFlipped => true;

		public override void MouseDown(NSEvent theEvent)
		{
			base.MouseDown(theEvent);
			MouseLeftDown?.Invoke(this, EventArgs.Empty);
		}

		public override void MouseUp(NSEvent theEvent)
		{
			base.MouseUp(theEvent);
			MouseLeftUp?.Invoke(this, EventArgs.Empty);
		}
	}

	public partial class ButtonHandler : ViewHandler<IButton, NSButton>
	{
		protected override NSButton CreateNativeView()
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
			handler.TypedNativeView?.UpdateText(button);
			// Any text update requires that we update any attributed string formatting
			MapFormatting(handler, button);
		}

		public static void MapTextColor(IButtonHandler handler, ITextStyle button)
		{
			handler.TypedNativeView?.UpdateTextColor(button);
		}

		public static void MapCharacterSpacing(IButtonHandler handler, ITextStyle button)
		{
			handler.TypedNativeView?.UpdateCharacterSpacing(button);
		}

		public static void MapPadding(IButtonHandler handler, IButton button)
		{
			handler.TypedNativeView?.UpdatePadding(button);
		}

		public static void MapFont(IButtonHandler handler, ITextStyle button)
		{
			var fontManager = handler.GetRequiredService<IFontManager>();
			handler.TypedNativeView?.UpdateFont(button, fontManager);
		}

		public static void MapFormatting(IButtonHandler handler, IText button)
		{
			// Update all of the attributed text formatting properties
			handler.TypedNativeView?.UpdateCharacterSpacing(button);
		}

		void OnSetImageSource(NSImage? image)
		{
			if (image != null)
			{
				NativeView.Image = image;
				//TODO: Improve
				//SetImage(image.ImageWithRenderingMode(NSImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
			}
			else
			{
				NativeView.Image = null;
			}

			VirtualView.ImageSourceLoaded();
		}

		public static void MapImageSource(IButtonHandler handler, IButton image) =>
			MapImageSourceAsync(handler, image).FireAndForget(handler);

		public static Task MapImageSourceAsync(IButtonHandler handler, IButton image)
		{
			if (image.ImageSource == null)
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
	}
}