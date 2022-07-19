using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.DeviceTests.Stubs;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Handlers;
using Xunit;

namespace Microsoft.Maui.DeviceTests
{
	[Category(TestCategory.ImageButton)]
	public partial class ImageButtonHandlerTests : HandlerTestBase<ImageButtonHandler, ImageButtonStub>
	{
		const int Precision = 4;

		[Fact(DisplayName = "Click event fires Correctly")]
		public async Task ClickEventFires()
		{
			var clicked = false;

			var button = new ImageButtonStub();

			button.Clicked += delegate
			{
				clicked = true;
			};

			await PerformClick(button);

			Assert.True(clicked);
		}

		[Theory(DisplayName = "Padding Initializes Correctly")]
		[InlineData(0, 0, 0, 0)]
		[InlineData(1, 1, 1, 1)]
		[InlineData(10, 10, 10, 10)]
		[InlineData(5, 10, 15, 20)]
		public async Task PaddingInitializesCorrectly(double left, double top, double right, double bottom)
		{
			var user = new Thickness(left, top, right, bottom);

			var button = new ImageButtonStub
			{
				Padding = user
			};

			var (expected, native) = await GetValueAsync(button, handler =>
			{
				var native = GetNativePadding(handler);
				var scaled = user;

#if __ANDROID__
				scaled = handler.PlatformView.Context!.ToPixels(scaled);
#endif

				return (scaled, native);
			});

			Assert.Equal(expected.Left, native.Left, Precision);
			Assert.Equal(expected.Top, native.Top, Precision);
			Assert.Equal(expected.Right, native.Right, Precision);
			Assert.Equal(expected.Bottom, native.Bottom, Precision);
		}

		[Category(TestCategory.ImageButton)]
		public partial class ImageButtonImageHandlerTests : ImageHandlerTests<ImageButtonHandler, ImageButtonStub>
		{
			public override Task AnimatedSourceInitializesCorrectly(string filename, bool isAnimating)
			{
				return Task.CompletedTask;
			}
		}
	}
}