using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using NUnit.Framework;

namespace Microsoft.Maui.Controls.Core.UnitTests
{
	using FlexLayout = Microsoft.Maui.Controls.Compatibility.FlexLayout;

	[TestFixture]
	public class FlexLayoutFlexDirectionTests : BaseTestFixture
	{
		[Test]
		public void TestFlexDirectionColumnWithoutHeight()
		{
			var view0 = new View { IsPlatformEnabled = true, HeightRequest = 10 };
			var view1 = new View { IsPlatformEnabled = true, HeightRequest = 10 };
			var view2 = new View { IsPlatformEnabled = true, HeightRequest = 10 };
			var layout = new FlexLayout
			{
				IsPlatformEnabled = true,
				Children = {
					view0,
					view1,
					view2,
				},

				Direction = FlexDirection.Column,
			};

			var sizeRequest = layout.Measure(100, double.PositiveInfinity);
			layout.Layout(new Rect(0, 0, sizeRequest.Request.Width, sizeRequest.Request.Height));
			Assert.That(layout.Bounds, Is.EqualTo(new Rect(0, 0, 100, 30)));
			Assert.That(view0.Bounds, Is.EqualTo(new Rect(0, 0, 100, 10)));
			Assert.That(view1.Bounds, Is.EqualTo(new Rect(0, 10, 100, 10)));
			Assert.That(view2.Bounds, Is.EqualTo(new Rect(0, 20, 100, 10)));
		}

		[Test]
		public void TestFlexDirectionRowNoWidth()
		{
			var view0 = new View { IsPlatformEnabled = true, WidthRequest = 10, };
			var view1 = new View { IsPlatformEnabled = true, WidthRequest = 10, };
			var view2 = new View { IsPlatformEnabled = true, WidthRequest = 10, };
			var layout = new FlexLayout
			{
				IsPlatformEnabled = true,
				Children = {
					view0,
					view1,
					view2,
				},

				Direction = FlexDirection.Row,
			};


			var measure = layout.Measure(double.PositiveInfinity, 100);
			layout.Layout(new Rect(0, 0, measure.Request.Width, measure.Request.Height));
			Assert.That(layout.Bounds, Is.EqualTo(new Rect(0, 0, 30, 100)));
			Assert.That(view0.Bounds, Is.EqualTo(new Rect(0, 0, 10, 100)));
			Assert.That(view1.Bounds, Is.EqualTo(new Rect(10, 0, 10, 100)));
			Assert.That(view2.Bounds, Is.EqualTo(new Rect(20, 0, 10, 100)));
		}

		[Test]
		public void TestFlexDirectionColumn()
		{
			var view0 = new View { IsPlatformEnabled = true, HeightRequest = 10 };
			var view1 = new View { IsPlatformEnabled = true, HeightRequest = 10 };
			var view2 = new View { IsPlatformEnabled = true, HeightRequest = 10 };
			var layout = new FlexLayout
			{
				IsPlatformEnabled = true,
				Children = {
					view0,
					view1,
					view2,
				},

				Direction = FlexDirection.Column,
			};

			layout.Layout(new Rect(0, 0, 100, 100));
			Assert.That(layout.Bounds, Is.EqualTo(new Rect(0, 0, 100, 100)));
			Assert.That(view0.Bounds, Is.EqualTo(new Rect(0, 0, 100, 10)));
			Assert.That(view1.Bounds, Is.EqualTo(new Rect(0, 10, 100, 10)));
			Assert.That(view2.Bounds, Is.EqualTo(new Rect(0, 20, 100, 10)));
		}

		[Test]
		public void TestFlexDirectionRow()
		{
			var view0 = new View { IsPlatformEnabled = true, WidthRequest = 10, };
			var view1 = new View { IsPlatformEnabled = true, WidthRequest = 10, };
			var view2 = new View { IsPlatformEnabled = true, WidthRequest = 10, };
			var layout = new FlexLayout
			{
				IsPlatformEnabled = true,
				Children = {
					view0,
					view1,
					view2,
				},

				Direction = FlexDirection.Row,
			};

			layout.Layout(new Rect(0, 0, 100, 100));
			Assert.That(layout.Bounds, Is.EqualTo(new Rect(0, 0, 100, 100)));
			Assert.That(view0.Bounds, Is.EqualTo(new Rect(0, 0, 10, 100)));
			Assert.That(view1.Bounds, Is.EqualTo(new Rect(10, 0, 10, 100)));
			Assert.That(view2.Bounds, Is.EqualTo(new Rect(20, 0, 10, 100)));
		}

		[Test]
		public void TestFlexDirectionColumnReverse()
		{
			var view0 = new View { IsPlatformEnabled = true, HeightRequest = 10 };
			var view1 = new View { IsPlatformEnabled = true, HeightRequest = 10 };
			var view2 = new View { IsPlatformEnabled = true, HeightRequest = 10 };
			var layout = new FlexLayout
			{
				IsPlatformEnabled = true,
				Children = {
					view0,
					view1,
					view2,
				},

				Direction = FlexDirection.ColumnReverse,
			};

			layout.Layout(new Rect(0, 0, 100, 100));
			Assert.That(layout.Bounds, Is.EqualTo(new Rect(0, 0, 100, 100)));
			Assert.That(view0.Bounds, Is.EqualTo(new Rect(0, 90, 100, 10)));
			Assert.That(view1.Bounds, Is.EqualTo(new Rect(0, 80, 100, 10)));
			Assert.That(view2.Bounds, Is.EqualTo(new Rect(0, 70, 100, 10)));
		}

		[Test]
		public void TestFlexDirectionRowReverse()
		{
			var view0 = new View { IsPlatformEnabled = true, WidthRequest = 10, };
			var view1 = new View { IsPlatformEnabled = true, WidthRequest = 10, };
			var view2 = new View { IsPlatformEnabled = true, WidthRequest = 10, };

			var layout = new FlexLayout
			{
				IsPlatformEnabled = true,
				Children = {
					view0,
					view1,
					view2,
				},

				Direction = FlexDirection.RowReverse,
			};

			layout.Layout(new Rect(0, 0, 100, 100));
			Assert.That(layout.Bounds, Is.EqualTo(new Rect(0, 0, 100, 100)));
			Assert.That(view0.Bounds, Is.EqualTo(new Rect(90, 0, 10, 100)));
			Assert.That(view1.Bounds, Is.EqualTo(new Rect(80, 0, 10, 100)));
			Assert.That(view2.Bounds, Is.EqualTo(new Rect(70, 0, 10, 100)));
		}
	}
}