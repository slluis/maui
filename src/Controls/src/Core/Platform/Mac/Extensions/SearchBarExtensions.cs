using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Controls.PlatformConfiguration.macOSSpecific;
using AppKit;

namespace Microsoft.Maui.Controls.Platform
{
	public static class SearchBarExtensions
	{
		public static void UpdateSearchBarStyle(this MauiSearchBar uiSearchBar, SearchBar searchBar)
		{
			// TODO COCOA
			//uiSearchBar.SearchBarStyle = searchBar.OnThisPlatform().GetSearchBarStyle().ToPlatformSearchBarStyle();
		}

		public static void UpdateText(this MauiSearchBar uiSearchBar, SearchBar searchBar)
		{
			uiSearchBar.Text = TextTransformUtilites.GetTransformedText(searchBar.Text, searchBar.TextTransform);
		}
	}
}
