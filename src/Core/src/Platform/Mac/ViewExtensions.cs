using System;
using System.Collections.Generic;
using CoreAnimation;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Handlers;
using AppKit;
using static Microsoft.Maui.Primitives.Dimension;

namespace Microsoft.Maui
{
	public static partial class ViewExtensions
	{
		internal const string BackgroundLayerName = "MauiBackgroundLayer";
		
		public static NSViewController ToNSViewController (this IView view, IMauiContext? context)
		{
			throw new Exception("not implemented");
		}

		public static void UpdateIsEnabled(this NSView nativeView, IView view)
		{
			if (nativeView is not NSControl uiControl)
				return;

			uiControl.Enabled = view.IsEnabled;
		}

		public static void UpdateVisibility(this NSView nativeView, IView view)
		{
			var shouldLayout = false;

			switch (view.Visibility)
			{
				case Visibility.Visible:
					shouldLayout = nativeView.Inflate();
					nativeView.Hidden = false;
					break;
				case Visibility.Hidden:
					shouldLayout = nativeView.Inflate();
					nativeView.Hidden = true;
					break;
				case Visibility.Collapsed:
					nativeView.Hidden = true;
					nativeView.Collapse();
					shouldLayout = true;
					break;
			}

			// If the view is just switching between Visible and Hidden, then a re-layout isn't necessary. The return value
			// from Inflate will tell us if the view was previously collapsed. If the view is switching to or from a collapsed
			// state, then we'll have to ask for a re-layout.

			if (shouldLayout)
			{
				if (nativeView.Superview != null)
				{
					nativeView.Superview.NeedsLayout = true;
				}
			}
		}

		public static void UpdateBackground(this ContentView nativeView, IBorder border)
		{
			bool hasBorder = border.Shape != null && border.Stroke != null;

			if (hasBorder)
			{
				nativeView.UpdateMauiCALayer(border);
			}
		}

		public static void UpdateBackground(this NSView nativeView, IView view)
		{
			// Remove previous background gradient layer if any
			nativeView.RemoveBackgroundLayer();

			var paint = view.Background;

			if (paint.IsNullOrEmpty())
				return;

			if (paint is SolidPaint solidPaint)
			{
				Color backgroundColor = solidPaint.Color;

				//TODO: To implement
				//if (backgroundColor == null)
				//	nativeView.BackgroundColor = ColorExtensions.BackgroundColor;
				//else
				//	nativeView.BackgroundColor = backgroundColor.ToNative();

				return;
			}
			else if (paint is GradientPaint gradientPaint)
			{
				//TODO: To implement
				//var backgroundLayer = gradientPaint?.ToCALayer(nativeView.Bounds);

				//if (backgroundLayer != null)
				//{
				//	backgroundLayer.Name = BackgroundLayerName;
				//	nativeView.BackgroundColor = NSColor.Clear;
				//	nativeView.InsertBackgroundLayer(backgroundLayer, 0);
				//}
			}
		}

		public static void UpdateFlowDirection(this NSView nativeView, IView view)
		{
			// TODO COCOA
			/*
			UISemanticContentAttribute updateValue = nativeView.SemanticContentAttribute;

			if (view.FlowDirection == view.Handler?.MauiContext?.GetFlowDirection() ||
				view.FlowDirection == FlowDirection.MatchParent)
			{
				updateValue = UISemanticContentAttribute.Unspecified;
			}
			else if (view.FlowDirection == FlowDirection.RightToLeft)
				updateValue = UISemanticContentAttribute.ForceRightToLeft;
			else if (view.FlowDirection == FlowDirection.LeftToRight)
				updateValue = UISemanticContentAttribute.ForceLeftToRight;

			if (updateValue != nativeView.SemanticContentAttribute)
				nativeView.SemanticContentAttribute = updateValue;
			*/
		}

		public static void UpdateOpacity(this NSView nativeView, IView view)
		{
			// TODO COCOA
//			nativeView.Alpha = (float)view.Opacity;
		}

		public static void UpdateAutomationId(this NSView nativeView, IView view) =>
			nativeView.AccessibilityIdentifier = view.AutomationId;

		public static void UpdateClip(this NSView nativeView, IView view)
		{
			// TODO COCOA
//			if (nativeView is WrapperView wrapper)
//				wrapper.Clip = view.Clip;
		}

		public static void UpdateShadow(this NSView nativeView, IView view)
		{
			// TODO COCOA
/*			var shadow = view.Shadow;
			var clip = view.Clip;

			// If there is a clip shape, then the shadow should be applied to the clip layer, not the view layer
			if (clip == null)
			{
				if (shadow == null)
					nativeView.ClearShadow();
				else
					nativeView.SetShadow(shadow);
			}
			else
			{
				if (nativeView is WrapperView wrapperView)
					wrapperView.Shadow = view.Shadow;
			}*/
		}

		public static T? FindDescendantView<T>(this NSView view) where T : NSView
		{
			var queue = new Queue<NSView>();
			queue.Enqueue(view);

			while (queue.Count > 0)
			{
				var descendantView = queue.Dequeue();

				if (descendantView is T result)
					return result;

				for (var i = 0; i < descendantView.Subviews?.Length; i++)
					queue.Enqueue(descendantView.Subviews[i]);
			}

			return null;
		}

		public static void UpdateBackgroundLayerFrame(this NSView view)
		{
			if (view == null || view.Frame.IsEmpty)
				return;

			var layer = view.Layer;

			if (layer == null || layer.Sublayers == null || layer.Sublayers.Length == 0)
				return;

			foreach (var sublayer in layer.Sublayers)
			{
				if (sublayer.Name == BackgroundLayerName && sublayer.Frame != view.Bounds)
				{
					sublayer.Frame = view.Bounds;
					break;
				}
			}
		}

		public static void InvalidateMeasure(this NSView nativeView, IView view)
		{
			// TODO COCOA
			nativeView.NeedsLayout = true;
			if (nativeView.Superview != null)
				nativeView.Superview.NeedsLayout = true;
		}

		public static void UpdateWidth(this NSView nativeView, IView view)
		{
			UpdateFrame(nativeView, view);
		}

		public static void UpdateHeight(this NSView nativeView, IView view)
		{
			UpdateFrame(nativeView, view);
		}

		public static void UpdateMinimumHeight(this NSView nativeView, IView view)
		{
			UpdateFrame(nativeView, view);
		}

		public static void UpdateMaximumHeight(this NSView nativeView, IView view)
		{
			UpdateFrame(nativeView, view);
		}

		public static void UpdateMinimumWidth(this NSView nativeView, IView view)
		{
			UpdateFrame(nativeView, view);
		}

		public static void UpdateMaximumWidth(this NSView nativeView, IView view)
		{
			UpdateFrame(nativeView, view);
		}

		public static void UpdateFrame(NSView nativeView, IView view)
		{
			if (!IsExplicitSet(view.Width) || !IsExplicitSet(view.Height))
			{
				// Ignore the initial setting of the value; the initial layout will take care of it
				return;
			}

			// Updating the frame (assuming it's an actual change) will kick off a layout update
			// Handling of the default width/height will be taken care of by GetDesiredSize
			var currentFrame = nativeView.Frame;
			nativeView.Frame = new CoreGraphics.CGRect(currentFrame.X, currentFrame.Y, view.Width, view.Height);
		}

		public static int IndexOfSubview(this NSView nativeView, NSView subview)
		{
			if (nativeView.Subviews.Length == 0)
				return -1;

			return Array.IndexOf(nativeView.Subviews, subview);
		}

		public static NSImage? ConvertToImage(this NSView view)
		{
			/*			if (!NativeVersion.IsAtLeast(10))
						{
							UIGraphics.BeginImageContext(view.Frame.Size);
							view.Layer.RenderInContext(UIGraphics.GetCurrentContext());
							var image = UIGraphics.GetImageFromCurrentImageContext();
							UIGraphics.EndImageContext();

							if (image.CGImage == null)
								return null;

							return new UIImage(image.CGImage);
						}

						var imageRenderer = new UIGraphicsImageRenderer(view.Bounds.Size);

						return imageRenderer.CreateImage((a) =>
						{
							view.Layer.RenderInContext(a.CGContext);
						});
			*/
			// TODO COCOA
			throw new NotImplementedException();
		}

/*		public static UINavigationController? GetNavigationController(this NSView view)
		{
			var rootController = view.Window?.RootViewController;
			if (rootController is UINavigationController nc)
				return nc;

			return rootController?.NavigationController;
		}*/

		internal static void Collapse(this NSView view)
		{
			// See if this view already has a collapse constraint we can use
			foreach (var constraint in view.Constraints)
			{
				if (constraint is CollapseConstraint collapseConstraint)
				{
					// Active the collapse constraint; that will squish the view down to zero height
					collapseConstraint.Active = true;
					return;
				}
			}

			// Set up a collapse constraint and turn it on
			var collapse = new CollapseConstraint();
			view.AddConstraint(collapse);
			collapse.Active = true;
		}

		internal static bool Inflate(this NSView view)
		{
			// Find and deactivate the collapse constraint, if any; the view will go back to its normal height
			foreach (var constraint in view.Constraints)
			{
				if (constraint is CollapseConstraint collapseConstraint)
				{
					collapseConstraint.Active = false;
					return true;
				}
			}

			return false;
		}

		public static void ClearSubviews(this NSView view)
		{
			for (int n = view.Subviews.Length - 1; n >= 0; n--)
			{
				view.Subviews[n].RemoveFromSuperview();
			}
		}
	}
}
