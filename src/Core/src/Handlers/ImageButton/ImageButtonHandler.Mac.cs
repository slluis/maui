using System;
using AppKit;

namespace Microsoft.Maui.Handlers
{
	public partial class ImageButtonHandler : ViewHandler<IImageButton, MauiButton>
	{
		protected override MauiButton CreatePlatformView()
		{
			var platformView = new MauiButton();
			return platformView;
		}

		void OnSetImageSource(NSImage? obj)
		{
			PlatformView.Image = obj;
		}

		protected override void ConnectHandler(MauiButton platformView)
		{
			platformView.MouseLeftDown += PlatformView_MouseLeftDown;
			platformView.MouseLeftUp += PlatformView_MouseLeftUp;

			base.ConnectHandler(platformView);
		}

		protected override void DisconnectHandler(MauiButton platformView)
		{
			platformView.MouseLeftDown -= PlatformView_MouseLeftDown;
			platformView.MouseLeftUp -= PlatformView_MouseLeftUp;

			base.DisconnectHandler(platformView);

			SourceLoader.Reset();
		}

		public static void MapStrokeColor(IImageButtonHandler handler, IButtonStroke buttonStroke)
		{
			handler.PlatformView?.UpdateStrokeColor(buttonStroke);
		}

		public static void MapStrokeThickness(IImageButtonHandler handler, IButtonStroke buttonStroke)
		{
			handler.PlatformView?.UpdateStrokeThickness(buttonStroke);
		}

		public static void MapCornerRadius(IImageButtonHandler handler, IButtonStroke buttonStroke)
		{
			handler.PlatformView?.UpdateCornerRadius(buttonStroke);
		}

		public static void MapPadding(IImageButtonHandler handler, IImageButton imageButton)
		{
			(handler.PlatformView as NSButton)?.UpdatePadding(imageButton);
		}

		private void PlatformView_MouseLeftUp(object? sender, EventArgs e)
		{
			VirtualView?.Released();
			VirtualView?.Clicked();
		}

		private void PlatformView_MouseLeftDown(object? sender, EventArgs e)
		{
			VirtualView?.Pressed();
		}
	}
}
