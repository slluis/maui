using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Graphics;
using AppKit;

namespace Microsoft.Maui.Handlers
{
	public partial class RefreshViewHandler : ViewHandler<IRefreshView, MauiRefreshView>
	{
		protected override MauiRefreshView CreatePlatformView()
		{
			return new MauiRefreshView();
		}

		protected override void ConnectHandler(MauiRefreshView nativeView)
		{
			//nativeView.RefreshControl.ValueChanged += OnRefresh;
			base.ConnectHandler(nativeView);
		}

		protected override void DisconnectHandler(MauiRefreshView nativeView)
		{
			//nativeView.RefreshControl.ValueChanged -= OnRefresh;
			base.DisconnectHandler(nativeView);
		}

		public static void MapBackground(RefreshViewHandler handler, IRefreshView view)
		{
			throw new NotImplementedException();
			//handler.NativeView.RefreshControl.UpdateBackground(view);
		}

		public static void MapIsRefreshing(IRefreshViewHandler handler, IRefreshView refreshView)
			=> UpdateIsRefreshing(handler);

		public static void MapContent(IRefreshViewHandler handler, IRefreshView refreshView)
			=> UpdateContent(handler);

		public static void MapRefreshColor(IRefreshViewHandler handler, IRefreshView refreshView)
			=> UpdateRefreshColor(handler);

		public static void MapIsEnabled(RefreshViewHandler handler, IRefreshView refreshView)
			=> handler.PlatformView?.UpdateIsEnabled(refreshView);

		void OnRefresh(object? sender, EventArgs e)
		{
			VirtualView.IsRefreshing = true;
		}

		static void UpdateIsRefreshing(IRefreshViewHandler handler)
		{
			handler.PlatformView.IsRefreshing = handler.VirtualView.IsRefreshing;
		}

		static void UpdateContent(IRefreshViewHandler handler) =>
			handler.PlatformView.UpdateContent(handler.VirtualView.Content, handler.MauiContext);

		static void UpdateRefreshColor(IRefreshViewHandler handler)
		{
			// TODO
/*			var color = handler.VirtualView?.RefreshColor?.ToColor()?.ToPlatform();

			if (color != null)
				handler.PlatformView.RefreshControl.TintColor = color;*/
		}
	}
}
