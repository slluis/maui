using System;
using AppKit;

namespace Microsoft.Maui.Handlers
{
	public partial class SliderHandler : ViewHandler<ISlider, NSSlider>
	{
		static NSColor? DefaultMinTrackColor;
		static NSColor? DefaultMaxTrackColor;
		static NSColor? DefaultThumbColor;

		protected override NSSlider CreateNativeView() => new NSSlider { Continuous = true };

		protected override void ConnectHandler(NSSlider nativeView)
		{
			base.ConnectHandler(nativeView);

/*			nativeView.ValueChanged += OnControlValueChanged;
			nativeView.AddTarget(OnTouchDownControlEvent, UIControlEvent.TouchDown);
			nativeView.AddTarget(OnTouchUpControlEvent, UIControlEvent.TouchUpInside | UIControlEvent.TouchUpOutside);*/
		}

		protected override void DisconnectHandler(NSSlider nativeView)
		{
			base.DisconnectHandler(nativeView);

/*			nativeView.ValueChanged -= OnControlValueChanged;
			nativeView.RemoveTarget(OnTouchDownControlEvent, UIControlEvent.TouchDown);
			nativeView.RemoveTarget(OnTouchUpControlEvent, UIControlEvent.TouchUpInside | UIControlEvent.TouchUpOutside);*/
		}

		void SetupDefaults(NSSlider nativeView)
		{
/*			DefaultMinTrackColor = nativeView.MinimumTrackTintColor;
			DefaultMaxTrackColor = nativeView.MaximumTrackTintColor;
			DefaultThumbColor = nativeView.ThumbTintColor;*/
		}

		public static void MapMinimum(SliderHandler handler, ISlider slider)
		{
//			handler.NativeView?.UpdateMinimum(slider);
		}

		public static void MapMaximum(SliderHandler handler, ISlider slider)
		{
//			handler.NativeView?.UpdateMaximum(slider);
		}

		public static void MapValue(SliderHandler handler, ISlider slider)
		{
//			handler.NativeView?.UpdateValue(slider);
		}

		public static void MapMinimumTrackColor(SliderHandler handler, ISlider slider)
		{
//			handler.NativeView?.UpdateMinimumTrackColor(slider, DefaultMinTrackColor);
		}

		public static void MapMaximumTrackColor(SliderHandler handler, ISlider slider)
		{
//			handler.NativeView?.UpdateMaximumTrackColor(slider, DefaultMaxTrackColor);
		}

		public static void MapThumbColor(SliderHandler handler, ISlider slider)
		{
//			handler.NativeView?.UpdateThumbColor(slider, DefaultThumbColor);
		}

		public static void MapThumbImageSource(SliderHandler handler, ISlider slider)
		{
/*			var provider = handler.GetRequiredService<IImageSourceServiceProvider>();

			handler.NativeView?.UpdateThumbImageSourceAsync(slider, provider)
				.FireAndForget(handler);*/
		}

		void OnControlValueChanged(object? sender, EventArgs eventArgs)
		{
/*			if (NativeView == null || VirtualView == null)
				return;

			VirtualView.Value = NativeView.Value;*/
		}

		void OnTouchDownControlEvent(object? sender, EventArgs e)
		{
			VirtualView?.DragStarted();
		}

		void OnTouchUpControlEvent(object? sender, EventArgs e)
		{
			VirtualView?.DragCompleted();
		}
	}
}