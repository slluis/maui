using Microsoft.Maui.Platform;
using AppKit;

namespace Microsoft.Maui.Handlers
{
	public partial class IndicatorViewHandler : ViewHandler<IIndicatorView, MauiPageControl>
	{
		MauiPageControl? UIPager => PlatformView;

		protected override MauiPageControl CreatePlatformView() => new MauiPageControl();

		protected override void ConnectHandler(MauiPageControl nativeView)
		{
			base.ConnectHandler(nativeView);
			UIPager?.SetIndicatorView(VirtualView);
			UpdateIndicator();
		}

		protected override void DisconnectHandler(MauiPageControl nativeView)
		{
			base.DisconnectHandler(nativeView);
			UIPager?.SetIndicatorView(null);
		}

		public static void MapCount(IIndicatorViewHandler handler, IIndicatorView indicator)
		{
			handler.PlatformView?.UpdateIndicatorCount();
		}

		public static void MapPosition(IIndicatorViewHandler handler, IIndicatorView indicator)
		{
			handler.PlatformView?.UpdatePosition();
		}

		public static void MapHideSingle(IIndicatorViewHandler handler, IIndicatorView indicator)
		{
			handler.PlatformView?.UpdateHideSingle(indicator);
		}

		public static void MapMaximumVisible(IIndicatorViewHandler handler, IIndicatorView indicator)
		{
			handler.PlatformView?.UpdateIndicatorCount();
		}

		public static void MapIndicatorSize(IIndicatorViewHandler handler, IIndicatorView indicator)
		{
			handler.PlatformView?.UpdateIndicatorSize(indicator);
		}

		public static void MapIndicatorColor(IIndicatorViewHandler handler, IIndicatorView indicator)
		{
			handler.PlatformView?.UpdatePagesIndicatorTintColor(indicator);
		}

		public static void MapSelectedIndicatorColor(IIndicatorViewHandler handler, IIndicatorView indicator)
		{
			handler.PlatformView?.UpdateCurrentPagesIndicatorTintColor(indicator);
		}

		public static void MapIndicatorShape(IIndicatorViewHandler handler, IIndicatorView indicator)
		{
			handler.PlatformView?.UpdateIndicatorShape(indicator);
		}

		void UpdateIndicator()
		{
			if (VirtualView is ITemplatedIndicatorView iTemplatedIndicatorView)
			{
				var indicatorsLayoutOverride = iTemplatedIndicatorView.IndicatorsLayoutOverride;
				NSView? handler;
				if (MauiContext != null && indicatorsLayoutOverride != null)
				{
					ClearIndicators();
					handler = indicatorsLayoutOverride.ToPlatform(MauiContext);
					PlatformView.AddSubview(handler);
				}
			}

			void ClearIndicators()
			{
				foreach (var child in PlatformView.Subviews)
					child.RemoveFromSuperview();
			}
		}
	}
}