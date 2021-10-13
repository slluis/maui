﻿using CoreAnimation;
using AppKit;

namespace Microsoft.Maui
{
	public static class LayerExtensions
	{
		public static void InsertBackgroundLayer(this NSView control, CALayer backgroundLayer, int index = -1)
		{
			control.RemoveBackgroundLayer();

			if (backgroundLayer != null)
			{
				var layer = control.Layer;

				if (index > -1)
					layer.InsertSublayer(backgroundLayer, index);
				else
					layer.AddSublayer(backgroundLayer);
			}
		}

		public static void RemoveBackgroundLayer(this NSView control)
		{
			var layer = control.Layer;

			if (layer == null)
				return;

			if (layer.Name == ViewExtensions.BackgroundLayerName)
			{
				layer.RemoveFromSuperLayer();
				return;
			}

			if (layer.Sublayers == null || layer.Sublayers.Length == 0)
				return;

			foreach (var subLayer in layer.Sublayers)
			{
				if (subLayer.Name == ViewExtensions.BackgroundLayerName)
				{
					subLayer.RemoveFromSuperLayer();
					break;
				}
			}
		}
	}
}