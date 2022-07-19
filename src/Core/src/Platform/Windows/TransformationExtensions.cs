﻿using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace Microsoft.Maui.Platform
{
	public static class TransformationExtensions
	{
		public static void UpdateTransformation(this FrameworkElement frameworkElement, IView view)
		{
			double anchorX = view.AnchorX;
			double anchorY = view.AnchorY;
			double rotationX = view.RotationX;
			double rotationY = view.RotationY;
			double rotation = view.Rotation;
			double translationX = view.TranslationX;
			double translationY = view.TranslationY;
			double scaleX = view.Scale * view.ScaleX;
			double scaleY = view.Scale * view.ScaleY;

			frameworkElement.RenderTransformOrigin = new global::Windows.Foundation.Point(anchorX, anchorY);
			frameworkElement.RenderTransform = new ScaleTransform { ScaleX = scaleX, ScaleY = scaleY };

			if (rotationX % 360 == 0 && rotationY % 360 == 0 && rotation % 360 == 0 &&
				translationX == 0 && translationY == 0 && scaleX == 1 && scaleY == 1)
			{
				frameworkElement.Projection = null;
				frameworkElement.RenderTransform = null;
			}
			else
			{
				// PlaneProjection removes touch and scrollwheel functionality on scrollable views such
				// as ScrollView, ListView, and TableView. If neither RotationX or RotationY are set
				// (i.e. their absolute value is 0), a CompositeTransform is instead used to allow for
				// rotation of the control on a 2D plane, and the other values are set. Otherwise, the
				// rotation values are set, but the aforementioned functionality will be lost.
				if (Math.Abs(view.RotationX) != 0 || Math.Abs(view.RotationY) != 0)
				{
					frameworkElement.Projection = new PlaneProjection
					{
						CenterOfRotationX = anchorX,
						CenterOfRotationY = anchorY,
						GlobalOffsetX = translationX,
						GlobalOffsetY = translationY,
						RotationX = -rotationX,
						RotationY = -rotationY,
						RotationZ = -rotation
					};
				}
				else
				{
					frameworkElement.RenderTransform = new CompositeTransform
					{
						CenterX = anchorX,
						CenterY = anchorY,
						Rotation = rotation,
						ScaleX = scaleX,
						ScaleY = scaleY,
						TranslateX = translationX,
						TranslateY = translationY
					};
				}
			}
		}
	}
}