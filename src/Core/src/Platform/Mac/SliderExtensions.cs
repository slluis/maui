using System.Threading.Tasks;
using AppKit;

namespace Microsoft.Maui
{
	public static class SliderExtensions
	{
		public static void UpdateMinimum(this NSSlider uiSlider, ISlider slider)
		{
			uiSlider.MaxValue = (float)slider.Maximum;
		}

		public static void UpdateMaximum(this NSSlider uiSlider, ISlider slider)
		{
			uiSlider.MinValue = (float)slider.Minimum;
		}

		public static void UpdateValue(this NSSlider uiSlider, ISlider slider)
		{
			if ((float)slider.Value != uiSlider.Value)
				uiSlider.Value = (float)slider.Value;
		}

		public static void UpdateMinimumTrackColor(this NSSlider uiSlider, ISlider slider)
		{
			UpdateMinimumTrackColor(uiSlider, slider, null);
		}

		public static void UpdateMinimumTrackColor(this NSSlider uiSlider, ISlider slider, NSColor? defaultMinTrackColor)
		{
			if (slider.MinimumTrackColor == null)
			{
				if (defaultMinTrackColor != null)
					uiSlider.MinimumTrackTintColor = defaultMinTrackColor;
			}
			else
				uiSlider.MinimumTrackTintColor = slider.MinimumTrackColor.ToNative();
		}

		public static void UpdateMaximumTrackColor(this NSSlider uiSlider, ISlider slider)
		{
			UpdateMaximumTrackColor(uiSlider, slider, null);
		}

		public static void UpdateMaximumTrackColor(this NSSlider uiSlider, ISlider slider, NSColor? defaultMaxTrackColor)
		{
			if (slider.MaximumTrackColor == null)
				uiSlider.MaximumTrackTintColor = defaultMaxTrackColor;
			else
				uiSlider.MaximumTrackTintColor = slider.MaximumTrackColor.ToNative();
		}

		public static void UpdateThumbColor(this NSSlider uiSlider, ISlider slider)
		{
			UpdateThumbColor(uiSlider, slider, null);
		}

		public static void UpdateThumbColor(this NSSlider uiSlider, ISlider slider, NSColor? defaultThumbColor)
		{
			if (slider.ThumbColor == null)
				uiSlider.ThumbTintColor = defaultThumbColor;
			else
				uiSlider.ThumbTintColor = slider.ThumbColor.ToNative();
		}

		public static async Task UpdateThumbImageSourceAsync(this NSSlider uiSlider, ISlider slider, IImageSourceServiceProvider provider)
		{
			var thumbImageSource = slider.ThumbImageSource;

			if (thumbImageSource != null)
			{
				var service = provider.GetRequiredImageSourceService(thumbImageSource);
				var result = await service.GetImageAsync(thumbImageSource);
				var thumbImage = result?.Value;

				uiSlider.SetThumbImage(thumbImage, UIControlState.Normal);
			}
		}
	}
}