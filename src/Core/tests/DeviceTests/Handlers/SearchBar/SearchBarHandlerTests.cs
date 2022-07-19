﻿using System.Threading.Tasks;
using Microsoft.Maui.DeviceTests.Stubs;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Handlers;
using Xunit;

namespace Microsoft.Maui.DeviceTests
{
	[Category(TestCategory.SearchBar)]
	public partial class SearchBarHandlerTests : HandlerTestBase<SearchBarHandler, SearchBarStub>
	{
		[Theory(DisplayName = "Background Initializes Correctly")]
		[InlineData(0xFF0000)]
		[InlineData(0x00FF00)]
		[InlineData(0x0000FF)]
		public async Task BackgroundInitializesCorrectly(uint color)
		{
			var expected = Color.FromUint(color);

			var searchBar = new SearchBarStub
			{
				Background = new SolidPaintStub(expected),
				Text = "Background"
			};

			await ValidateHasColor(searchBar, expected);
		}

		[Fact(DisplayName = "Text Initializes Correctly")]
		public async Task TextInitializesCorrectly()
		{
			var searchBar = new SearchBarStub
			{
				Text = "Test"
			};

			await ValidatePropertyInitValue(searchBar, () => searchBar.Text, GetNativeText, searchBar.Text);
		}

		[Theory(DisplayName = "Query Text Updates Correctly")]
		[InlineData(null, null)]
		[InlineData(null, "Query")]
		[InlineData("Query", null)]
		[InlineData("Query", "Another search query")]
		public async Task TextUpdatesCorrectly(string setValue, string unsetValue)
		{
			var searchBar = new SearchBarStub();

			await ValidatePropertyUpdatesValue(
				searchBar,
				nameof(ISearchBar.Text),
				h =>
				{
					var n = GetNativeText(h);
					if (string.IsNullOrEmpty(n))
						n = null; // Native platforms may not support null text
					return n;
				},
				setValue,
				unsetValue);
		}

		[Fact(DisplayName = "TextColor Initializes Correctly")]
		public async Task TextColorInitializesCorrectly()
		{
			var searchBar = new SearchBarStub
			{
				Text = "TextColor",
				TextColor = Colors.Red
			};

			await ValidatePropertyInitValue(searchBar, () => searchBar.TextColor, GetNativeTextColor, Colors.Red);
		}

		[Fact(DisplayName = "Null Text Color Doesn't Crash")]
		public async Task NullTextColorDoesntCrash()
		{
			var searchBar = new SearchBarStub
			{
				Text = "TextColor",
				TextColor = null,
			};

			await CreateHandlerAsync(searchBar);
		}

		[Fact(DisplayName = "Placeholder Initializes Correctly")]
		public async Task PlaceholderInitializesCorrectly()
		{
			var searchBar = new SearchBarStub
			{
				Placeholder = "Placeholder"
			};

			await ValidatePropertyInitValue(searchBar, () => searchBar.Placeholder, GetNativePlaceholder, searchBar.Placeholder);
		}

		[Theory(DisplayName = "MaxLength Initializes Correctly")]
		[InlineData(2)]
		[InlineData(5)]
		[InlineData(8)]
		[InlineData(10)]
		public async Task MaxLengthInitializesCorrectly(int maxLength)
		{
			const string text = "Lorem ipsum dolor sit amet";
			var expectedText = text.Substring(0, maxLength);

			var searchBar = new SearchBarStub()
			{
				MaxLength = maxLength,
				Text = text
			};

			var platformText = await GetValueAsync(searchBar, GetNativeText);

			Assert.Equal(expectedText, platformText);
		}

		[Fact(DisplayName = "CancelButtonColor Initialize Correctly")]
		public async Task CancelButtonColorInitializeCorrectly()
		{
			var searchBar = new SearchBarStub()
			{
				CancelButtonColor = Colors.MediumPurple
			};

			await ValidateHasColor(searchBar, Colors.MediumPurple, () => searchBar.CancelButtonColor = Colors.MediumPurple);
		}

		[Fact(DisplayName = "Null Cancel Button Color Doesn't Crash")]
		public async Task NullCancelButtonColorDoesntCrash()
		{
			var searchBar = new SearchBarStub
			{
				CancelButtonColor = null,
			};

			await CreateHandlerAsync(searchBar);
		}

		[Theory]
		[InlineData(true)]
		[InlineData(false)]
		public async Task IsReadOnlyInitializesCorrectly(bool isReadOnly)
		{
			var searchBar = new SearchBarStub()
			{
				IsReadOnly = isReadOnly,
				Text = "Test"
			};

			await ValidatePropertyInitValue(searchBar, () => searchBar.IsReadOnly, GetNativeIsReadOnly, searchBar.IsReadOnly);
		}

		[Category(TestCategory.SearchBar)]
		public class SearchBarTextInputTests : TextInputHandlerTests<SearchBarHandler, SearchBarStub>
		{
			protected override void SetNativeText(SearchBarHandler searchBarHandler, string text)
			{
				SearchBarHandlerTests.SetNativeText(searchBarHandler, text);
			}

			protected override int GetCursorStartPosition(SearchBarHandler searchBarHandler)
			{
				return SearchBarHandlerTests.GetCursorStartPosition(searchBarHandler);
			}

			protected override void UpdateCursorStartPosition(SearchBarHandler searchBarHandler, int position)
			{
				SearchBarHandlerTests.UpdateCursorStartPosition(searchBarHandler, position);
			}
		}
	}
}