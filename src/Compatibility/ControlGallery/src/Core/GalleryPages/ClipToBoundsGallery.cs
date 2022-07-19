﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;
using AbsoluteLayoutFlags = Microsoft.Maui.Layouts.AbsoluteLayoutFlags;

namespace Microsoft.Maui.Controls.Compatibility.ControlGallery
{
	public class ClipToBoundsGallery : ContentPage
	{
		public ClipToBoundsGallery()
		{
			var child1 = new BoxView { Color = Colors.Red };
			var child2 = new BoxView { Color = Colors.Blue };
			var button = new Button { Text = "Clip", BackgroundColor = Colors.Green };

			Padding = new Thickness(55);
			var layout = new AbsoluteLayout
			{
				Children = {
					{child1, new Rect (-50, 0, 100, 100)},
					{child2, new Rect (0, -50, 100, 100)},
					{button, new Rect (1.0, 0.5, 100, 100), AbsoluteLayoutFlags.PositionProportional}
				}
			};

			button.Clicked += (sender, args) => layout.IsClippedToBounds = !layout.IsClippedToBounds;

			Content = layout;
		}
	}
}
