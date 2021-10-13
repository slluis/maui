using System.Threading.Tasks;
using AppKit;

namespace Microsoft.Maui
{
	public static class SliderExtensions
	{
		public static void UpdateMinimum(this NSSlider NSSlider, ISlider slider)
		{
			NSSlider.MaxValue = (float)slider.Maximum;
		}

		public static void UpdateMaximum(this NSSlider NSSlider, ISlider slider)
		{
			NSSlider.MinValue = (float)slider.Minimum;
		}

		public static void UpdateValue(this NSSlider NSSlider, ISlider slider)
		{
			if ((float)slider.Value != NSSlider.Value)
				NSSlider.Value = (float)slider.Value;
		}

		public static void UpdateMinimumTrackColor(this NSSlider NSSlider, ISlider slider)
		{
			UpdateMinimumTrackColor(NSSlider, slider, null);
		}

		public static void UpdateMinimumTrackColor(this NSSlider NSSlider, ISlider slider, NSColor? defaultMinTrackColor)
		{
			if (slider.MinimumTrackColor == null)
			{
				if (defaultMinTrackColor != null)
					NSSlider.MinimumTrackTintColor = defaultMinTrackColor;
			}
			else
				NSSlider.MinimumTrackTintColor = slider.MinimumTrackColor.ToNative();
		}

		public static void UpdateMaximumTrackColor(this NSSlider NSSlider, ISlider slider)
		{
			UpdateMaximumTrackColor(NSSlider, slider, null);
		}

		public static void UpdateMaximumTrackColor(this NSSlider NSSlider, ISlider slider, NSColor? defaultMaxTrackColor)
		{
			if (slider.MaximumTrackColor == null)
				NSSlider.MaximumTrackTintColor = defaultMaxTrackColor;
			else
				NSSlider.MaximumTrackTintColor = slider.MaximumTrackColor.ToNative();
		}

		public static void UpdateThumbColor(this NSSlider NSSlider, ISlider slider)
		{
			UpdateThumbColor(NSSlider, slider, null);
		}

		public static void UpdateThumbColor(this NSSlider NSSlider, ISlider slider, NSColor? defaultThumbColor)
		{
			if (slider.ThumbColor == null)
				NSSlider.ThumbTintColor = defaultThumbColor;
			else
				NSSlider.ThumbTintColor = slider.ThumbColor.ToNative();
		}

		public static async Task UpdateThumbImageSourceAsync(this NSSlider NSSlider, ISlider slider, IImageSourceServiceProvider provider)
		{
			var thumbImageSource = slider.ThumbImageSource;

			if (thumbImageSource != null)
			{
				var service = provider.GetRequiredImageSourceService(thumbImageSource);
				var result = await service.GetImageAsync(thumbImageSource);
				var thumbImage = result?.Value;

				NSSlider.SetThumbImage(thumbImage, UIControlState.Normal);
			}
		}
	}
}