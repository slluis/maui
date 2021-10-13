﻿using Microsoft.Maui.Platform.iOS;
using AppKit;

namespace Microsoft.Maui.Handlers
{
	public partial class IndicatorViewHandler : ViewHandler<IIndicatorView, MauiPageControl>
	{
		//MauiPageControl? UIPager => NativeView as MauiPageControl;

		protected override MauiPageControl CreateNativeView() => new MauiPageControl();

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

		public static void MapCount(IndicatorViewHandler handler, IIndicatorView indicator)
		{
			handler.UIPager?.UpdateIndicatorCount();
		}

		public static void MapPosition(IndicatorViewHandler handler, IIndicatorView indicator)
		{
			handler.UIPager?.UpdatePosition();
		}

		public static void MapHideSingle(IndicatorViewHandler handler, IIndicatorView indicator)
		{
			handler.NativeView?.UpdateHideSingle(indicator);
		}

		public static void MapMaximumVisible(IndicatorViewHandler handler, IIndicatorView indicator)
		{
			handler.UIPager?.UpdateIndicatorCount();
		}

		public static void MapIndicatorSize(IndicatorViewHandler handler, IIndicatorView indicator)
		{
			handler.UIPager?.UpdateIndicatorSize(indicator);
		}

		public static void MapIndicatorColor(IndicatorViewHandler handler, IIndicatorView indicator)
		{
			handler.NativeView?.UpdatePagesIndicatorTintColor(indicator);
		}

		public static void MapSelectedIndicatorColor(IndicatorViewHandler handler, IIndicatorView indicator)
		{
			handler.NativeView?.UpdateCurrentPagesIndicatorTintColor(indicator);
		}

		public static void MapIndicatorShape(IndicatorViewHandler handler, IIndicatorView indicator)
		{
			handler.UIPager?.UpdateIndicatorShape(indicator);
		}

		void UpdateIndicator()
		{
			if (VirtualView is ITemplatedIndicatorView iTemplatedIndicatorView)
			{
				var indicatorsLayoutOverride = iTemplatedIndicatorView.IndicatorsLayoutOverride;
				UIView? handler;
				if (MauiContext != null && indicatorsLayoutOverride != null)
				{
					ClearIndicators();
					handler = indicatorsLayoutOverride.ToNative(MauiContext);
					NativeView.AddSubview(handler);
				}
			}

			void ClearIndicators()
			{
				foreach (var child in NativeView.Subviews)
					child.RemoveFromSuperview();
			}
		}
	}
}