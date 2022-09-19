using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Xunit;

namespace Microsoft.Maui.Controls.Core.UnitTests
{
	using StackLayout = Microsoft.Maui.Controls.Compatibility.StackLayout;


	public class BindableLayoutTests : BaseTestFixture
	{
		[Fact]
		public void TracksEmpty()
		{
			var layout = new StackLayout
			{
				IsPlatformEnabled = true,
			};

			var itemsSource = new ObservableCollection<int>();
			BindableLayout.SetItemsSource(layout, itemsSource);

			Assert.True(IsLayoutWithItemsSource(itemsSource, layout));
		}

		[Fact]
		public void TracksAdd()
		{
			var layout = new StackLayout
			{
				IsPlatformEnabled = true,
			};

			var itemsSource = new ObservableCollection<int>();
			BindableLayout.SetItemsSource(layout, itemsSource);

			itemsSource.Add(1);
			Assert.True(IsLayoutWithItemsSource(itemsSource, layout));
		}

		[Fact]
		public void TracksInsert()
		{
			var layout = new StackLayout
			{
				IsPlatformEnabled = true,
			};

			var itemsSource = new ObservableCollection<int>() { 0, 1, 2, 3, 4 };
			BindableLayout.SetItemsSource(layout, itemsSource);

			itemsSource.Insert(2, 5);
			Assert.True(IsLayoutWithItemsSource(itemsSource, layout));
		}

		[Fact]
		public void TracksRemove()
		{
			var layout = new StackLayout
			{
				IsPlatformEnabled = true,
			};

			var itemsSource = new ObservableCollection<int>() { 0, 1 };
			BindableLayout.SetItemsSource(layout, itemsSource);

			itemsSource.RemoveAt(0);
			Assert.True(IsLayoutWithItemsSource(itemsSource, layout));

			itemsSource.Remove(1);
			Assert.True(IsLayoutWithItemsSource(itemsSource, layout));
		}

		[Fact]
		public void TracksRemoveAll()
		{
			var layout = new StackLayout
			{
				IsPlatformEnabled = true,
			};

			var itemsSource = new ObservableRangeCollection<int>(Enumerable.Range(0, 10));
			BindableLayout.SetItemsSource(layout, itemsSource);

			itemsSource.RemoveAll();
			Assert.True(IsLayoutWithItemsSource(itemsSource, layout));
		}

		[Fact]
		public void TracksReplace()
		{
			var layout = new StackLayout
			{
				IsPlatformEnabled = true,
			};

			var itemsSource = new ObservableCollection<int>() { 0, 1, 2 };
			BindableLayout.SetItemsSource(layout, itemsSource);

			itemsSource[0] = 3;
			itemsSource[1] = 4;
			itemsSource[2] = 5;
			Assert.True(IsLayoutWithItemsSource(itemsSource, layout));
		}

		[Fact]
		public void TracksMove()
		{
			var layout = new StackLayout
			{
				IsPlatformEnabled = true,
			};

			var itemsSource = new ObservableCollection<int>() { 0, 1 };
			BindableLayout.SetItemsSource(layout, itemsSource);

			itemsSource.Move(0, 1);
			Assert.True(IsLayoutWithItemsSource(itemsSource, layout));

			itemsSource.Move(1, 0);
			Assert.True(IsLayoutWithItemsSource(itemsSource, layout));
		}

		[Fact]
		public void TracksClear()
		{
			var layout = new StackLayout
			{
				IsPlatformEnabled = true,
			};

			var itemsSource = new ObservableCollection<int>() { 0, 1 };
			BindableLayout.SetItemsSource(layout, itemsSource);

			itemsSource.Clear();
			Assert.True(IsLayoutWithItemsSource(itemsSource, layout));
		}

		[Fact]
		public void TracksNull()
		{
			var layout = new StackLayout
			{
				IsPlatformEnabled = true,
			};

			var itemsSource = new ObservableCollection<int>(Enumerable.Range(0, 10));
			BindableLayout.SetItemsSource(layout, itemsSource);
			Assert.True(IsLayoutWithItemsSource(itemsSource, layout));

			itemsSource = null;
			BindableLayout.SetItemsSource(layout, itemsSource);
			Assert.True(IsLayoutWithItemsSource(itemsSource, layout));
		}

		[Fact]
		public void ItemTemplateIsSet()
		{
			var layout = new StackLayout
			{
				IsPlatformEnabled = true,
			};

			var itemsSource = new ObservableCollection<int>(Enumerable.Range(0, 10));
			BindableLayout.SetItemsSource(layout, itemsSource);

			BindableLayout.SetItemTemplate(layout, new DataTemplateBoxView());

			Assert.True(IsLayoutWithItemsSource(itemsSource, layout));
			Assert.Equal(itemsSource.Count, layout.Children.Cast<BoxView>().Count());
		}

		[Fact]
		public void ItemTemplateSelectorIsSet()
		{
			var layout = new StackLayout
			{
				IsPlatformEnabled = true,
			};

			var itemsSource = new ObservableCollection<int>(Enumerable.Range(0, 10));
			BindableLayout.SetItemsSource(layout, itemsSource);
			BindableLayout.SetItemTemplateSelector(layout, new DataTemplateSelectorFrame());

			Assert.True(IsLayoutWithItemsSource(itemsSource, layout));
			Assert.Equal(itemsSource.Count, layout.Children.Cast<Frame>().Count());
		}

		[Fact]
		public void ContainerIsPassedInSelectTemplate()
		{
			var layout = new StackLayout
			{
				IsPlatformEnabled = true,
			};

			var itemsSource = new ObservableCollection<int>(Enumerable.Range(0, 10));
			BindableLayout.SetItemsSource(layout, itemsSource);

			int containerPassedCount = 0;
			BindableLayout.SetItemTemplateSelector(layout, new MyDataTemplateSelectorTest((item, container) =>
			{
				if (container == layout)
					++containerPassedCount;
				return null;
			}));

			Assert.Equal(containerPassedCount, itemsSource.Count);
		}

		[Fact]
		public void ItemTemplateTakesPrecendenceOverItemTemplateSelector()
		{
			var layout = new StackLayout
			{
				IsPlatformEnabled = true,
			};

			var itemsSource = new ObservableCollection<int>(Enumerable.Range(0, 10));
			BindableLayout.SetItemsSource(layout, itemsSource);
			BindableLayout.SetItemTemplate(layout, new DataTemplateBoxView());
			BindableLayout.SetItemTemplateSelector(layout, new DataTemplateSelectorFrame());

			Assert.True(IsLayoutWithItemsSource(itemsSource, layout));
			Assert.Equal(itemsSource.Count, layout.Children.Cast<BoxView>().Count());
		}

		[Fact]
		public void ItemsSourceTakePrecendenceOverLayoutChildren()
		{
			var layout = new StackLayout
			{
				IsPlatformEnabled = true,
			};

			layout.Children.Add(new Label());
			layout.Children.Add(new Label());
			layout.Children.Add(new Label());

			var itemsSource = new ObservableCollection<int>(Enumerable.Range(0, 10));
			BindableLayout.SetItemsSource(layout, itemsSource);
			Assert.True(IsLayoutWithItemsSource(itemsSource, layout));
		}

		[Fact(Skip = "https://github.com/dotnet/maui/issues/1524")]
		public void LayoutIsGarbageCollectedAfterItsRemoved()
		{
			var layout = new StackLayout
			{
				IsPlatformEnabled = true,
			};

			var itemsSource = new ObservableCollection<int>(Enumerable.Range(0, 10));
			BindableLayout.SetItemsSource(layout, itemsSource);

			var pageRoot = new Grid();
			pageRoot.Children.Add(layout);
			var page = new ContentPage() { Content = pageRoot };

			var weakReference = new WeakReference(layout);
			pageRoot.Children.Remove(layout);
			layout = null;

			GC.Collect();
			GC.WaitForPendingFinalizers();

			Assert.False(weakReference.IsAlive);
		}

		[Fact]
		public void ThrowsExceptionOnUsingDataTemplateSelectorForItemTemplate()
		{
			var layout = new StackLayout
			{
				IsPlatformEnabled = true,
			};

			var itemsSource = new ObservableCollection<int>(Enumerable.Range(0, 10));
			BindableLayout.SetItemsSource(layout, itemsSource);

			Assert.Throws<NotSupportedException>(() => BindableLayout.SetItemTemplate(layout, new DataTemplateSelectorFrame()));
		}

		[Fact]
		public void DontTrackAfterItemsSourceChanged()
		{
			var layout = new StackLayout
			{
				IsPlatformEnabled = true,
			};

			var itemsSource = new ObservableCollection<int>(Enumerable.Range(0, 10));
			BindableLayout.SetItemsSource(layout, itemsSource);
			BindableLayout.SetItemsSource(layout, new ObservableCollection<int>(Enumerable.Range(0, 10)));

			bool wasCalled = false;
			layout.ChildAdded += (_, __) => wasCalled = true;
			itemsSource.Add(11);
			Assert.False(wasCalled);
		}

		[Fact]
		public void WorksWithNullItems()
		{
			var layout = new StackLayout
			{
				IsPlatformEnabled = true,
			};

			var itemsSource = new ObservableCollection<int?>(Enumerable.Range(0, 10).Cast<int?>());
			itemsSource.Add(null);
			BindableLayout.SetItemsSource(layout, itemsSource);
			Assert.True(IsLayoutWithItemsSource(itemsSource, layout));
		}

		[Fact]
		public void WorksWithDuplicateItems()
		{
			var layout = new StackLayout
			{
				IsPlatformEnabled = true,
			};

			var itemsSource = new ObservableCollection<int>(Enumerable.Range(0, 10));
			foreach (int item in itemsSource.ToList())
			{
				itemsSource.Add(item);
			}

			BindableLayout.SetItemsSource(layout, itemsSource);
			Assert.True(IsLayoutWithItemsSource(itemsSource, layout));

			itemsSource.Remove(0);
			Assert.True(IsLayoutWithItemsSource(itemsSource, layout));
		}


		[Fact]
		public void ValidateBindableProperties()
		{
			var layout = new StackLayout
			{
				IsPlatformEnabled = true,
			};

			// EmptyView
			object emptyView = new object();
			BindableLayout.SetEmptyView(layout, emptyView);

			Assert.Equal(emptyView, BindableLayout.GetEmptyView(layout));
			Assert.Equal(emptyView, layout.GetValue(BindableLayout.EmptyViewProperty));

			// EmptyViewTemplateProperty
			DataTemplate emptyViewTemplate = new DataTemplate(typeof(Label));
			BindableLayout.SetEmptyViewTemplate(layout, emptyViewTemplate);

			Assert.Equal(emptyViewTemplate, BindableLayout.GetEmptyViewTemplate(layout));
			Assert.Equal(emptyViewTemplate, layout.GetValue(BindableLayout.EmptyViewTemplateProperty));


			// ItemsSourceProperty
			IEnumerable itemsSource = new object[0];
			BindableLayout.SetItemsSource(layout, itemsSource);

			Assert.Equal(itemsSource, BindableLayout.GetItemsSource(layout));
			Assert.Equal(itemsSource, layout.GetValue(BindableLayout.ItemsSourceProperty));

			// ItemTemplateProperty
			DataTemplate itemTemplate = new DataTemplate(typeof(Label));
			BindableLayout.SetItemTemplate(layout, itemTemplate);

			Assert.Equal(itemTemplate, BindableLayout.GetItemTemplate(layout));
			Assert.Equal(itemTemplate, layout.GetValue(BindableLayout.ItemTemplateProperty));


			// ItemTemplateSelectorProperty
			var itemTemplateSelector = new DataTemplateSelectorFrame();
			BindableLayout.SetItemTemplateSelector(layout, itemTemplateSelector);

			Assert.Equal(itemTemplateSelector, BindableLayout.GetItemTemplateSelector(layout));
			Assert.Equal(itemTemplateSelector, layout.GetValue(BindableLayout.ItemTemplateSelectorProperty));
		}

		// Checks if for every item in the items source there's a corresponding view
		static bool IsLayoutWithItemsSource(IEnumerable itemsSource, Compatibility.Layout layout)
		{
			if (itemsSource == null)
			{
				return layout.Children.Count() == 0;
			}

			int i = 0;
			foreach (object item in itemsSource)
			{
				if (BindableLayout.GetItemTemplate(layout) is DataTemplate dataTemplate ||
					BindableLayout.GetItemTemplateSelector(layout) is DataTemplateSelector dataTemplateSelector)
				{
					if (!Equals(item, layout.Children[i].BindingContext))
					{
						return false;
					}
				}
				else
				{
					if (!Equals(item?.ToString(), ((Label)layout.Children[i]).Text))
					{
						return false;
					}
				}

				++i;
			}

			return layout.Children.Count == i;
		}

		class DataTemplateBoxView : DataTemplate
		{
			public DataTemplateBoxView() : base(() => new BoxView())
			{
			}
		}

		class DataTemplateFrame : DataTemplate
		{
			public DataTemplateFrame() : base(() => new Frame())
			{
			}
		}

		class DataTemplateSelectorFrame : DataTemplateSelector
		{
			DataTemplateFrame dt = new DataTemplateFrame();

			protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
			{
				return dt;
			}
		}

		class ObservableRangeCollection<T> : ObservableCollection<T>
		{
			public ObservableRangeCollection(IEnumerable<T> collection)
				: base(collection)
			{
			}

			public void RemoveAll()
			{
				CheckReentrancy();

				var changedItems = new List<T>(Items);
				foreach (var i in changedItems)
					Items.Remove(i);

				OnPropertyChanged(new PropertyChangedEventArgs("Count"));
				OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, changedItems, 0));
			}
		}

		class MyDataTemplateSelectorTest : DataTemplateSelector
		{
			readonly Func<object, BindableObject, DataTemplate> _func;

			public MyDataTemplateSelectorTest(Func<object, BindableObject, DataTemplate> func)
				=> _func = func;

			protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
				=> _func(item, container);
		}
	}
}
