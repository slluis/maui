using System;
using AppKit;

namespace Microsoft.Maui.Handlers
{
	public partial class LayoutHandler : ViewHandler<ILayout, LayoutView>
	{
		protected override LayoutView CreatePlatformView()
		{
			if (VirtualView == null)
			{
				throw new InvalidOperationException($"{nameof(VirtualView)} must be set to create a LayoutViewGroup");
			}

			var view = new LayoutView
			{
				CrossPlatformMeasure = VirtualView.CrossPlatformMeasure,
				CrossPlatformArrange = VirtualView.CrossPlatformArrange,
			};

			return view;
		}

		public override void SetVirtualView(IView view)
		{
			base.SetVirtualView(view);

			_ = PlatformView ?? throw new InvalidOperationException($"{nameof(PlatformView)} should have been set by base class.");
			_ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");
			_ = MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

			PlatformView.View = view;
			PlatformView.CrossPlatformMeasure = VirtualView.CrossPlatformMeasure;
			PlatformView.CrossPlatformArrange = VirtualView.CrossPlatformArrange;

			// Remove any previous children 
			PlatformView.ClearSubviews();

			foreach (var child in VirtualView)
			{
				PlatformView.AddSubview(child.ToPlatform(MauiContext));
			}
		}

		public void Add(IView child)
		{
			_ = PlatformView ?? throw new InvalidOperationException($"{nameof(PlatformView)} should have been set by base class.");
			_ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");
			_ = MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

			PlatformView.AddSubview(child.ToPlatform(MauiContext));
		}

		public void Remove(IView child)
		{
			_ = PlatformView ?? throw new InvalidOperationException($"{nameof(PlatformView)} should have been set by base class.");
			_ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");

			if (child?.Handler?.PlatformView is NSView childView)
			{
				childView.RemoveFromSuperview();
			}
		}

		public void Clear()
		{
			PlatformView.ClearSubviews();
		}

		public void Insert(int index, IView child)
		{
			_ = PlatformView ?? throw new InvalidOperationException($"{nameof(PlatformView)} should have been set by base class.");
			_ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");
			_ = MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

			PlatformView.Subviews.Insert(index, child.ToPlatform(MauiContext));
		}

		public void Update(int index, IView child)
		{
			_ = PlatformView ?? throw new InvalidOperationException($"{nameof(PlatformView)} should have been set by base class.");
			_ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");
			_ = MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

			var existing = PlatformView.Subviews[index];
			existing.RemoveFromSuperview();
			PlatformView.Subviews.Insert (index, child.ToPlatform(MauiContext));
			PlatformView.NeedsLayout = true;
		}

		public void UpdateZIndex(IView child)
		{
			_ = PlatformView ?? throw new InvalidOperationException($"{nameof(PlatformView)} should have been set by base class.");
			_ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");
			_ = MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

			EnsureZIndexOrder(child);
		}

		void EnsureZIndexOrder(IView child)
		{
			var nativeChildView = (NSView)child.ToPlatform(MauiContext!);
			var currentIndex = PlatformView.IndexOfSubview(nativeChildView);

			if (currentIndex == -1)
			{
				return;
			}

			var targetIndex = VirtualView.GetLayoutHandlerIndex(child);

			if (currentIndex != targetIndex)
			{
				nativeChildView.RemoveFromSuperview();
				var subviews = PlatformView.Subviews;

				if (targetIndex == subviews.Length)
				{
					PlatformView.AddSubview(nativeChildView);
				}
				else
				{
					PlatformView.AddSubview(nativeChildView, NSWindowOrderingMode.Below, subviews[targetIndex]);
				}
			}
		}
		protected override void DisconnectHandler(LayoutView nativeView)
		{
			base.DisconnectHandler(nativeView);
			nativeView.ClearSubviews();
		}
	}
}
