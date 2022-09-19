﻿using System;
using System.Threading.Tasks;
using Microsoft.Maui.DeviceTests.Stubs;
using Xunit;

namespace Microsoft.Maui.DeviceTests
{
	[Category(TestCategory.Slider)]
	public partial class SliderHandlerTests : HandlerTestBase<SliderHandler, SliderStub>
	{
		[Theory(DisplayName = "Percent Value Initializes Correctly")]
		[InlineData(0, 1, 0)]
		[InlineData(0, 1, 0.5)]
		[InlineData(0, 1, 1)]
		[InlineData(0, 100, 0)]
		[InlineData(0, 100, 1)]
		[InlineData(0, 100, 5)]
		[InlineData(0, 100, 50)]
		[InlineData(0, 100, 100)]
		[InlineData(0, 100, 10000)]
		[InlineData(0, 100, -10000)]
		[InlineData(0, 10000, 10000)]
		[InlineData(0, 10000, -10000)]
		public async Task PercentValueInitializesCorrectly(double min, double max, double value)
		{
			var expectedValue = Math.Clamp(value, min, max);
			var expectedPercent = (expectedValue - min) / (max - min);

			var slider = new SliderStub()
			{
				Maximum = max,
				Minimum = min,
				Value = value
			};

			Assert.Equal(min, slider.Minimum);
			Assert.Equal(max, slider.Maximum);
			Assert.Equal(expectedValue, slider.Value);

			var native = await GetValueAsync(slider, (handler) =>
			{
				return new
				{
					Min = GetNativeMinimum(handler),
					Max = GetNativeMaximum(handler),
					Val = GetNativeProgress(handler),
				};
			});
			var nativePercent = (native.Val - native.Min) / (native.Max - native.Min);

			Assert.Equal(expectedPercent, nativePercent, 5);
		}

#if !WINDOWS
		[Fact(DisplayName = "Thumb Color Initializes Correctly", Skip = "There seems to be an issue, so disable for now: https://github.com/dotnet/maui/issues/1275")]
		public async Task ThumbColorInitializesCorrectly()
		{
			var slider = new SliderStub()
			{
				ThumbColor = Colors.Purple
			};

			await ValidateNativeThumbColor(slider, Colors.Purple);
		}
#endif

		[Fact(DisplayName = "Null Thumb Color Doesn't Crash")]
		public async Task NullThumbColorDoesntCrash()
		{
			var slider = new SliderStub()
			{
				ThumbColor = null,
			};

			await CreateHandlerAsync(slider);
		}

#if !__ANDROID__ // Android native control behavior works differently; see SliderHandlerTests.Android.cs
		[Fact(DisplayName = "Value Initializes Correctly")]
		public async Task ValueInitializesCorrectly()
		{
			var slider = new SliderStub()
			{
				Maximum = 1,
				Minimum = 0,
				Value = 0.5
			};

			await ValidatePropertyInitValue(slider, () => slider.Value, GetNativeProgress, slider.Value);
		}

		[Fact(DisplayName = "Maximum Value Initializes Correctly")]
		public async Task MaximumInitializesCorrectly()
		{
			var slider = new SliderStub()
			{
				Maximum = 1
			};

			await ValidatePropertyInitValue(slider, () => slider.Maximum, GetNativeMaximum, 1);
		}
#endif

		[Fact]
		public async Task NativeMeasurementFiniteGivenInfiniteConstraints()
		{
			var slider = new SliderStub();

			var size = await GetValueAsync(slider, (h) => h.GetDesiredSize(double.PositiveInfinity, double.PositiveInfinity));

			Assert.False(double.IsInfinity(size.Width));
			Assert.False(double.IsInfinity(size.Height));
		}
	}
}