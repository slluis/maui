using System;
using System.Collections.Generic;
using CoreAnimation;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Handlers;
using AppKit;
using static Microsoft.Maui.Primitives.Dimension;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using System.Numerics;

namespace Microsoft.Maui.Platform
{
	public static partial class ViewExtensions
	{
		internal const string BackgroundLayerName = "MauiBackgroundLayer";
		
		public static void UpdateIsEnabled(this NSView nativeView, IView view)
		{
			if (nativeView is not NSControl uiControl)
				return;

			uiControl.Enabled = view.IsEnabled;
		}

		public static void Focus(this NSView platformView, FocusRequest request)
		{
			request.IsFocused = platformView.BecomeFirstResponder();
		}

		public static void Unfocus(this NSView platformView, IView view)
		{
			platformView.ResignFirstResponder();
		}

		public static void UpdateVisibility(this NSView platformView, IView view) =>
			ViewExtensions.UpdateVisibility(platformView, view.Visibility);

		public static void UpdateVisibility(this NSView nativeView, Visibility visibility)
		{
			var shouldLayout = false;

			switch (visibility)
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

		public static void UpdateBackground(this ContentView nativeView, IBorderStroke border)
		{
			bool hasShape = border.Shape != null;

			if (hasShape)
			{
				nativeView.UpdateMauiCALayer(border);
			}
		}

		public static void UpdateBackground(this NSView platformView, IView view)
		{
			platformView.UpdateBackground(view.Background, view as IButtonStroke);
		}

		public static void UpdateBackground(this NSView platformView, Paint? paint, IButtonStroke? stroke = null)
		{
			// Remove previous background gradient layer if any
			platformView.RemoveBackgroundLayer();

			if (paint.IsNullOrEmpty())
			{
				if (platformView is LayoutView)
					platformView.SetBackground(null);
				else
					return;
			}


			if (paint is SolidPaint solidPaint)
			{
				Color backgroundColor = solidPaint.Color;

				if (backgroundColor == null)
					platformView.SetBackground(ColorExtensions.BackgroundColor);
				else
					platformView.SetBackground(backgroundColor.ToPlatform());

				return;
			}
			else if (paint is GradientPaint gradientPaint)
			{
				var backgroundLayer = gradientPaint?.ToCALayer(platformView.Bounds);

				if (backgroundLayer != null)
				{
					backgroundLayer.Name = BackgroundLayerName;
					platformView.SetBackground(NSColor.Clear);

					backgroundLayer.UpdateLayerBorder(stroke);

					platformView.InsertBackgroundLayer(backgroundLayer, 0);
				}
			}
		}

		public static void SetBackground(this NSView platformView, NSColor? color)
		{
			// TODO COCOA
		}

		public static void UpdateFlowDirection(this NSView platformView, IView view)
		{
			// TODO COCOA
			/*
			NSSemanticContentAttribute updateValue = platformView.SemanticContentAttribute;

			switch (view.FlowDirection)
			{
				case FlowDirection.MatchParent:
					updateValue = GetParentMatchingSemanticContentAttribute(view);
					break;
				case FlowDirection.LeftToRight:
					updateValue = UISemanticContentAttribute.ForceLeftToRight;
					break;
				case FlowDirection.RightToLeft:
					updateValue = UISemanticContentAttribute.ForceRightToLeft;
					break;
			}

			if (updateValue != platformView.SemanticContentAttribute)
			{
				platformView.SemanticContentAttribute = updateValue;

				if (view is ITextAlignment)
				{
					// A change in flow direction may mean a change in text alignment
					view.Handler?.UpdateValue(nameof(ITextAlignment.HorizontalTextAlignment));
				}

				PropagateFlowDirection(updateValue, view);
			}*/
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
		public static void UpdateBorder(this NSView platformView, IView view)
		{
			var border = (view as IBorder)?.Border;
			if (platformView is WrapperView wrapperView)
				wrapperView.Border = border;
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

		public static async Task UpdateBackgroundImageSourceAsync(this NSView platformView, IImageSource? imageSource, IImageSourceServiceProvider? provider)
		{
			if (provider == null)
				return;

			if (imageSource != null)
			{
				var service = provider.GetRequiredImageSourceService(imageSource);
				var result = await service.GetImageAsync(imageSource);
				var backgroundImage = result?.Value;

				if (backgroundImage == null)
					return;

				platformView.SetBackground(NSColor.FromPatternImage(backgroundImage));
			}
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

		internal static Rect GetPlatformViewBounds(this IView view)
		{
			var platformView = view?.ToPlatform();
			if (platformView == null)
			{
				return new Rect();
			}

			return platformView.GetPlatformViewBounds();
		}

		internal static Rect GetPlatformViewBounds(this NSView platformView)
		{
			if (platformView == null)
				return new Rect();

			var superview = platformView;
			while (superview.Superview is not null)
			{
				superview = superview.Superview;
			}

			var convertPoint = platformView.ConvertRectToView(platformView.Bounds, superview);

			var X = convertPoint.X;
			var Y = convertPoint.Y;
			var Width = convertPoint.Width;
			var Height = convertPoint.Height;

			return new Rect(X, Y, Width, Height);
		}

		internal static Matrix4x4 GetViewTransform(this IView view)
		{
			var platformView = view?.ToPlatform();
			if (platformView?.Layer == null)
				return new Matrix4x4();
			return platformView.Layer.GetViewTransform();
		}

		internal static Matrix4x4 GetViewTransform(this NSView view)
			=> view.Layer?.GetViewTransform() ?? new Matrix4x4();

		internal static Point GetLocationOnScreen(this NSView view) =>
			view.GetPlatformViewBounds().Location;

		internal static Point? GetLocationOnScreen(this IElement element)
		{
			if (element.Handler?.MauiContext == null)
				return null;

			return (element.ToPlatform())?.GetLocationOnScreen();
		}

		internal static Graphics.Rect GetBoundingBox(this IView view)
			=> view.ToPlatform().GetBoundingBox();

		internal static Graphics.Rect GetBoundingBox(this NSView? platformView)
		{
			if (platformView == null)
				return new Rect();
			var nvb = platformView.GetPlatformViewBounds();
			var transform = platformView.GetViewTransform();
			var radians = transform.ExtractAngleInRadians();
			var rotation = CoreGraphics.CGAffineTransform.MakeRotation((nfloat)radians);
			CGAffineTransform.CGRectApplyAffineTransform(nvb, rotation);
			return new Rect(nvb.X, nvb.Y, nvb.Width, nvb.Height);
		}

		internal static NSView? GetParent(this NSView? view)
		{
			return view?.Superview;
		}

		internal static Size LayoutToMeasuredSize(this IView view, double width, double height)
		{
			var size = view.Measure(width, height);
			var platformFrame = new CGRect(0, 0, size.Width, size.Height);

			if (view.Handler is IPlatformViewHandler viewHandler && viewHandler.PlatformView != null)
				viewHandler.PlatformView.Frame = platformFrame;

			view.Arrange(platformFrame.ToRectangle());
			return size;
		}

		public static void UpdateInputTransparent(this NSView platformView, IViewHandler handler, IView view)
		{
			if (view is ITextInput textInput)
			{
				platformView.UpdateInputTransparent(textInput.IsReadOnly, view.InputTransparent);
				return;
			}

			// TODO COCOA
			//platformView.UserInteractionEnabled = !view.InputTransparent;
		}

		public static void UpdateInputTransparent(this NSView platformView, bool isReadOnly, bool inputTransparent)
		{
			// TODO COCOA
			//platformView.UserInteractionEnabled = !(isReadOnly || inputTransparent);
		}

		public static void UpdateToolTip(this NSView platformView, ToolTip? tooltip)
		{
			// TODO COCOA
		}

		internal static bool IsLoaded(this NSView uiView)
		{
			if (uiView == null)
				return false;

			return uiView.Window != null;
		}

		internal static IDisposable OnLoaded(this NSView uiView, Action action)
		{
			if (uiView.IsLoaded())
			{
				action();
				return new ActionDisposable(() => { });
			}

			Dictionary<NSString, NSObject> observers = new Dictionary<NSString, NSObject>();
			ActionDisposable? disposable = null;
			disposable = new ActionDisposable(() =>
			{
				disposable = null;
				foreach (var observer in observers)
				{
					uiView.Layer!.RemoveObserver(observer.Value, observer.Key);
					observers.Remove(observer.Key);
				}
			});

			// Ideally we could wire into UIView.MovedToWindow but there's no way to do that without just inheriting from every single
			// UIView. So we just make our best attempt by observering some properties that are going to fire once UIView is attached to a window.			
			observers.Add(new NSString("bounds"), (NSObject)uiView.Layer!.AddObserver("bounds", Foundation.NSKeyValueObservingOptions.OldNew, (oc) => OnLoadedCheck(oc)));
			observers.Add(new NSString("frame"), (NSObject)uiView.Layer.AddObserver("frame", Foundation.NSKeyValueObservingOptions.OldNew, (oc) => OnLoadedCheck(oc)));

			// OnLoaded is called at the point in time where the xplat view knows it's going to be attached to the window.
			// So this just serves as a way to queue a call on the UI Thread to see if that's enough time for the window
			// to get attached.
			uiView.BeginInvokeOnMainThread(() => OnLoadedCheck(null));

			void OnLoadedCheck(NSObservedChange? nSObservedChange = null)
			{
				if (disposable != null)
				{
					if (uiView.IsLoaded())
					{
						disposable.Dispose();
						disposable = null;
						action();
					}
					else if (nSObservedChange != null)
					{
						// In some cases (FlyoutPage) the arrange and measure all take place before
						// the view is added to the screen so this queues up a second check that
						// hopefully will fire loaded once the view is added to the window.
						// None of this code is great but I haven't found a better way
						// for an outside observer to know when a subview is added to a window
						uiView.BeginInvokeOnMainThread(() => OnLoadedCheck(null));
					}
				}
			};

			return disposable;
		}

		internal static IDisposable OnUnloaded(this NSView uiView, Action action)
		{

			if (!uiView.IsLoaded())
			{
				action();
				return new ActionDisposable(() => { });
			}

			Dictionary<NSString, NSObject> observers = new Dictionary<NSString, NSObject>();
			ActionDisposable? disposable = null;
			disposable = new ActionDisposable(() =>
			{
				disposable = null;
				foreach (var observer in observers)
				{
					uiView.Layer!.RemoveObserver(observer.Value, observer.Key);
					observers.Remove(observer.Key);
				}
			});

			// Ideally we could wire into UIView.MovedToWindow but there's no way to do that without just inheriting from every single
			// UIView. So we just make our best attempt by observering some properties that are going to fire once UIView is attached to a window.	
			observers.Add(new NSString("bounds"), (NSObject)uiView.Layer!.AddObserver("bounds", Foundation.NSKeyValueObservingOptions.OldNew, (_) => UnLoadedCheck()));
			observers.Add(new NSString("frame"), (NSObject)uiView.Layer.AddObserver("frame", Foundation.NSKeyValueObservingOptions.OldNew, (_) => UnLoadedCheck()));

			// OnUnloaded is called at the point in time where the xplat view knows it's going to be detached from the window.
			// So this just serves as a way to queue a call on the UI Thread to see if that's enough time for the window
			// to get detached.
			uiView.BeginInvokeOnMainThread(UnLoadedCheck);

			void UnLoadedCheck()
			{
				if (!uiView.IsLoaded() && disposable != null)
				{
					disposable.Dispose();
					disposable = null;
					action();
				}
			};

			return disposable;
		}

		internal static void UpdateLayerBorder(this CoreAnimation.CALayer layer, IButtonStroke? stroke)
		{
			if (stroke == null)
				return;

			if (stroke.StrokeColor != null)
				layer.BorderColor = stroke.StrokeColor.ToCGColor();

			if (stroke.StrokeThickness >= 0)
				layer.BorderWidth = (float)stroke.StrokeThickness;

			if (stroke.CornerRadius >= 0)
				layer.CornerRadius = stroke.CornerRadius;
		}
	}
}
