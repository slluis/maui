﻿using CoreAnimation;
using CoreGraphics;
using Microsoft.Maui.Graphics;
using AppKit;

namespace Microsoft.Maui.Platform
{
	internal static class ShadowExtensions
	{
		public static void SetShadow(this NSView nativeView, IShadow? shadow)
		{
			if (shadow == null || shadow.Paint == null)
				return;

			var layer = nativeView.Layer;
			layer?.SetShadow(shadow);
		}

		public static void SetShadow(this CALayer layer, IShadow? shadow)
		{
			if (shadow == null || shadow.Paint?.ToColor() == null)
				return;

			var radius = shadow.Radius;
			var opacity = shadow.Opacity;
			var color = shadow.Paint.ToColor()?.ToPlatform();

			var offset = new CGSize((double)shadow.Offset.X, (double)shadow.Offset.Y);

			layer.ShadowColor = color?.CGColor;
			layer.ShadowOpacity = opacity;
			layer.ShadowRadius = radius;
			layer.ShadowOffset = offset;

			layer.SetNeedsDisplay();
		}

		public static void ClearShadow(this NSView nativeView)
		{
			var layer = nativeView.Layer;
			layer?.ClearShadow();
		}

		public static void ClearShadow(this CALayer layer)
		{
			layer.ShadowColor = new CGColor(0, 0, 0, 0);
			layer.ShadowRadius = 0;
			layer.ShadowOffset = new CGSize();
			layer.ShadowOpacity = 0;
		}
	}
}