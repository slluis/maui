using System.Collections.Generic;
using Microsoft.Maui.Controls.Xaml;
using NUnit.Framework;

namespace Microsoft.Maui.Controls.Core.UnitTests
{
	[TestFixture]
	public class TemplatedViewUnitTests : BaseTestFixture
	{
		[Test]
		public void TemplatedView_should_have_the_InternalChildren_correctly_when_ControlTemplate_changed()
		{
			var sut = new TemplatedView();
			IList<Element> internalChildren = ((IControlTemplated)sut).InternalChildren;
			internalChildren.Add(new VisualElement());
			internalChildren.Add(new VisualElement());
			internalChildren.Add(new VisualElement());

			sut.ControlTemplate = new ControlTemplate(typeof(ExpectedView));

			Assert.AreEqual(1, internalChildren.Count);
			Assert.IsInstanceOf<ExpectedView>(internalChildren[0]);
		}

		[Test]
		public void ShouldHaveTemplatedRootSet()
		{
			var tv = new TemplatedView();
			var ct = (IControlTemplated)tv;
			Assert.AreEqual(ct.TemplateRoot, null);

			tv.ControlTemplate = new ControlTemplate(typeof(ExpectedView));

			IList<Element> internalChildren = ct.InternalChildren;
			Assert.AreEqual(ct.TemplateRoot, internalChildren[0]);
		}

		[Test]
		public void GetContentViewTemplateChildShouldWork()
		{
			var xaml = @"<ContentView
					xmlns=""http://schemas.microsoft.com/dotnet/2021/maui""
					xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml""
					x:Class=""Microsoft.Maui.Controls.Core.UnitTests.MyTestContentView"">
                       <ContentView.ControlTemplate>
                         <ControlTemplate>
                           <Label x:Name=""label0""/>
                         </ControlTemplate>
						</ContentView.ControlTemplate>
					</ContentView>";

			var contentView = new MyTestContentView();
			contentView.LoadFromXaml(xaml);

			IList<Element> internalChildren = contentView.InternalChildren;
			Assert.AreEqual(internalChildren[0], contentView.TemplateChildObtained);
		}

		[Test]
		public void GetTemplatedViewTemplateChildShouldWork()
		{
			var xaml =
				@"<ContentView
					xmlns=""http://schemas.microsoft.com/dotnet/2021/maui""
					xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml""
					x:Class=""Microsoft.Maui.Controls.Core.UnitTests.MyTestTemplatedView"">
					<TemplatedView.ControlTemplate>
						<ControlTemplate>
							<Label x:Name=""label0""/>
						</ControlTemplate>
					</TemplatedView.ControlTemplate>
				</ContentView>";

			var contentView = new MyTestTemplatedView();
			contentView.LoadFromXaml(xaml);

			IList<Element> internalChildren = contentView.InternalChildren;
			Assert.AreEqual(internalChildren[0], contentView.TemplateChildObtained);
		}

		[Test]
		public void GetContentPageTemplateChildShouldWork()
		{
			var xaml = @"<ContentPage
					xmlns=""http://schemas.microsoft.com/dotnet/2021/maui""
					xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml""
					x:Class=""Microsoft.Maui.Controls.Core.UnitTests.MyTestContentPage"">
                       <ContentPage.ControlTemplate>
                         <ControlTemplate>
                           <Label x:Name=""label0""/>
                         </ControlTemplate>
						</ContentPage.ControlTemplate>
					</ContentPage>";

			var contentPage = new MyTestContentPage();
			contentPage.LoadFromXaml(xaml);

			IList<Element> internalChildren = contentPage.InternalChildren;
			Assert.AreEqual(internalChildren[0], contentPage.TemplateChildObtained);
		}

		[Test]
		public void OnContentViewApplyTemplateShouldBeCalled()
		{
			var xaml = @"<ContentView
					xmlns=""http://schemas.microsoft.com/dotnet/2021/maui""
					xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml""
					x:Class=""Microsoft.Maui.Controls.Core.UnitTests.MyTestContentView"">
                       <ContentView.ControlTemplate>
                         <ControlTemplate>
                           <Label x:Name=""label0""/>
                         </ControlTemplate>
						</ContentView.ControlTemplate>
					</ContentView>";

			var contentView = new MyTestContentView();
			contentView.LoadFromXaml(xaml);
			Assert.IsTrue(contentView.WasOnApplyTemplateCalled);
		}

		[Test]
		public void OnTemplatedViewApplyTemplateShouldBeCalled()
		{
			var xaml =
				@"<ContentView
					xmlns=""http://schemas.microsoft.com/dotnet/2021/maui""
					xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml""
					x:Class=""Microsoft.Maui.Controls.Core.UnitTests.MyTestTemplatedView"">
					<ContentView.ControlTemplate>
						<ControlTemplate>
							<Label x:Name=""label0""/>
						</ControlTemplate>
					</ContentView.ControlTemplate>
				</ContentView>";

			var contentView = new MyTestTemplatedView();
			contentView.LoadFromXaml(xaml);

			Assert.IsTrue(contentView.WasOnApplyTemplateCalled);
		}

		[Test]
		public void OnContentPageApplyTemplateShouldBeCalled()
		{
			var xaml = @"<ContentPage
					xmlns=""http://schemas.microsoft.com/dotnet/2021/maui""
					xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml""
					x:Class=""Microsoft.Maui.Controls.Core.UnitTests.MyTestContentPage"">
                       <ContentPage.ControlTemplate>
                         <ControlTemplate>
                           <Label x:Name=""label0""/>
                         </ControlTemplate>
						</ContentPage.ControlTemplate>
					</ContentPage>";

			var contentPage = new MyTestContentPage();
			contentPage.LoadFromXaml(xaml);
			Assert.IsTrue(contentPage.WasOnApplyTemplateCalled);
		}

		private class ExpectedView : View
		{
			public ExpectedView()
			{
			}
		}

		public class MyTemplate : StackLayout
		{
			public MyTemplate()
			{
				Children.Add(new ContentPresenter());
			}
		}

		[Test]
		public void BindingsShouldBeAppliedOnTemplateChange()
		{
			var template0 = new ControlTemplate(typeof(MyTemplate));
			var template1 = new ControlTemplate(typeof(MyTemplate));
			var label = new Label();
			label.SetBinding(Label.TextProperty, ".");
			var cv = new ContentView
			{
				ControlTemplate = template0,
				Content = label
			};
			cv.BindingContext = "Foo";

			Assume.That(label.Text, Is.EqualTo("Foo"));
			cv.ControlTemplate = template1;
			Assert.That(label.Text, Is.EqualTo("Foo"));
		}
	}

	class MyTestTemplatedView : TemplatedView
	{
		public bool WasOnApplyTemplateCalled { get; private set; }

		public Element TemplateChildObtained { get; private set; }

		protected override void OnApplyTemplate()
		{
			WasOnApplyTemplateCalled = true;
			TemplateChildObtained = (Element)GetTemplateChild("label0");
		}
	}

	class MyTestContentView : ContentView
	{
		public bool WasOnApplyTemplateCalled { get; private set; }

		public Element TemplateChildObtained { get; private set; }

		protected override void OnApplyTemplate()
		{
			WasOnApplyTemplateCalled = true;
			TemplateChildObtained = (Element)GetTemplateChild("label0");
		}
	}

	class MyTestContentPage : ContentPage
	{
		public bool WasOnApplyTemplateCalled { get; private set; }

		public Element TemplateChildObtained { get; private set; }

		protected override void OnApplyTemplate()
		{
			WasOnApplyTemplateCalled = true;
			TemplateChildObtained = (Element)GetTemplateChild("label0");
		}
	}
}