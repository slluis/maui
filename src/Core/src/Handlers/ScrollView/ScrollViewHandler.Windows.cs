﻿#nullable enable
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Maui.Graphics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using static Microsoft.Maui.Layouts.LayoutExtensions;

namespace Microsoft.Maui.Handlers
{
	public partial class ScrollViewHandler : ViewHandler<IScrollView, ScrollViewer>
	{
		const string ContentPanelTag = "MAUIScrollViewContentPanel";

		protected override ScrollViewer CreatePlatformView()
		{
			return new ScrollViewer();
		}

		protected override void ConnectHandler(ScrollViewer platformView)
		{
			base.ConnectHandler(platformView);
			platformView.ViewChanged += ViewChanged;
		}

		protected override void DisconnectHandler(ScrollViewer platformView)
		{
			base.DisconnectHandler(platformView);
			platformView.ViewChanged -= ViewChanged;
		}

		public static void MapContent(IScrollViewHandler handler, IScrollView scrollView)
		{
			if (handler.PlatformView == null || handler.MauiContext == null)
				return;

			UpdateContentPanel(scrollView, handler);
		}

		public static void MapHorizontalScrollBarVisibility(IScrollViewHandler handler, IScrollView scrollView)
		{
			handler.PlatformView?.UpdateScrollBarVisibility(scrollView.Orientation, scrollView.HorizontalScrollBarVisibility);
		}

		public static void MapVerticalScrollBarVisibility(IScrollViewHandler handler, IScrollView scrollView)
		{
			handler.PlatformView.VerticalScrollBarVisibility = scrollView.VerticalScrollBarVisibility.ToWindowsScrollBarVisibility();
		}

		public static void MapOrientation(IScrollViewHandler handler, IScrollView scrollView)
		{
			handler.PlatformView?.UpdateScrollBarVisibility(scrollView.Orientation, scrollView.HorizontalScrollBarVisibility);
		}

		public static void MapRequestScrollTo(IScrollViewHandler handler, IScrollView scrollView, object? args)
		{
			if (args is ScrollToRequest request)
			{
				handler.PlatformView.ChangeView(request.HorizontalOffset, request.VerticalOffset, null, request.Instant);
			}
		}

		void ViewChanged(object? sender, ScrollViewerViewChangedEventArgs e)
		{
			VirtualView.VerticalOffset = PlatformView.VerticalOffset;
			VirtualView.HorizontalOffset = PlatformView.HorizontalOffset;

			if (e.IsIntermediate == false)
			{
				VirtualView.ScrollFinished();
			}
		}

		/*
			Problem 1: Windows treats Padding differently than what we want for MAUI; Padding creates space
			_around_ the scrollable area, rather than padding the content inside of it. 

			Problem 2: The ScrollViewer will force any content to start at the origin (0,0), even if we ask 
			to arrange it at an offset. This defeats our content's Margin properties. 

			To handle this, we insert a container ContentPanel which always lays out at the origin but provides both 
			the Padding and the Margin for the content. The extra layer uses the native ContentPanel control we already 
			provide as the backing control for ContentView, Page, etc. 

			The extra layer also provides a place to call CrossPlatformArrange for the content, since we 
			can't subclass ScrollViewer.

			The methods below exist to support inserting/updating the padding/margin panel.
		 */

		static ContentPanel? GetContentPanel(ScrollViewer scrollViewer)
		{
			if (scrollViewer.Content is ContentPanel contentPanel)
			{
				if (contentPanel.Tag is string tag && tag == ContentPanelTag)
				{
					return contentPanel;
				}
			}

			return null;
		}

		static void UpdateContentPanel(IScrollView scrollView, IScrollViewHandler handler)
		{
			if (scrollView.PresentedContent == null || handler.MauiContext == null)
			{
				return;
			}

			var scrollViewer = handler.PlatformView;
			var nativeContent = scrollView.PresentedContent.ToPlatform(handler.MauiContext);

			if (GetContentPanel(scrollViewer) is ContentPanel currentPaddingLayer)
			{
				if (currentPaddingLayer.Children.Count == 0 || currentPaddingLayer.Children[0] != nativeContent)
				{
					currentPaddingLayer.Children.Clear();
					currentPaddingLayer.Children.Add(nativeContent);

				}
			}
			else
			{
				InsertContentPanel(scrollViewer, scrollView, nativeContent);
			}
		}

		static void InsertContentPanel(ScrollViewer scrollViewer, IScrollView scrollView, FrameworkElement nativeContent)
		{
			if (scrollView.PresentedContent == null)
			{
				return;
			}

			var paddingShim = new ContentPanel()
			{
				CrossPlatformMeasure = IncludeScrollViewInsets(scrollView.CrossPlatformMeasure, scrollView),
				CrossPlatformArrange = scrollView.CrossPlatformArrange,
				Tag = ContentPanelTag
			};

			scrollViewer.Content = null;
			paddingShim.Children.Add(nativeContent);
			scrollViewer.Content = paddingShim;
		}

		static Func<double, double, Size> IncludeScrollViewInsets(Func<double, double, Size> internalMeasure, IScrollView scrollView)
		{
			return (widthConstraint, heightConstraint) =>
			{
				return InsetScrollView(widthConstraint, heightConstraint, internalMeasure, scrollView);
			};
		}

		static Size InsetScrollView(double widthConstraint, double heightConstraint, Func<double, double, Size> internalMeasure, IScrollView scrollView)
		{
			var padding = scrollView.Padding;
			var presentedContent = scrollView.PresentedContent;

			if (presentedContent == null)
			{
				return new Size(padding.HorizontalThickness, padding.VerticalThickness);
			}

			// Exclude the padding while measuring the internal content ...
			var measurementWidth = widthConstraint - padding.HorizontalThickness;
			var measurementHeight = heightConstraint - padding.VerticalThickness;

			var result = internalMeasure.Invoke(measurementWidth, measurementHeight);

			// ... and add the padding back in to the final result
			var fullSize = new Size(result.Width + padding.HorizontalThickness, result.Height + padding.VerticalThickness);

			if (double.IsInfinity(widthConstraint))
			{
				widthConstraint = result.Width;
			}

			if (double.IsInfinity(heightConstraint))
			{
				heightConstraint = result.Height;
			}

			// If the presented content has LayoutAlignment Fill, we'll need to adjust the measurement to account for that
			return fullSize.AdjustForFill(new Rect(0, 0, widthConstraint, heightConstraint), presentedContent);
		}
	}
}
