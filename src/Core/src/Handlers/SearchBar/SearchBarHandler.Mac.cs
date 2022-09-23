using System;
using System.Drawing;
using Foundation;
using Microsoft.Extensions.DependencyInjection;
using AppKit;

namespace Microsoft.Maui.Handlers
{
	public partial class SearchBarHandler : ViewHandler<ISearchBar, MauiSearchBar>
	{
		NSColor? _defaultTextColor;

		//NSColor? _cancelButtonTextColorDefaultDisabled;
		//NSColor? _cancelButtonTextColorDefaultHighlighted;
		//NSColor? _cancelButtonTextColorDefaultNormal;

		NSTextField? _editor;

		public NSTextField? QueryEditor => _editor;

		protected override MauiSearchBar CreatePlatformView()
		{
			var searchBar = new MauiSearchBar() {
				ShowsCancelButton = true,
				//BarStyle = UIBarStyle.Default
			};

			if (NativeVersion.IsAtLeast(13))
				_editor = searchBar.SearchTextField;
			else
				_editor = searchBar.FindDescendantView<NSTextField>();

			return searchBar;
		}

		protected override void ConnectHandler(MauiSearchBar nativeView)
		{
			//nativeView.CancelButtonClicked += OnCancelClicked;
			//nativeView.SearchButtonClicked += OnSearchButtonClicked;
			//nativeView.TextPropertySet += OnTextPropertySet;
			//nativeView.ShouldChangeTextInRange += ShouldChangeText;
			base.ConnectHandler(nativeView);
			SetupDefaults(nativeView);
		}

		protected override void DisconnectHandler(MauiSearchBar nativeView)
		{
			//nativeView.CancelButtonClicked -= OnCancelClicked;
			//nativeView.SearchButtonClicked -= OnSearchButtonClicked;
			//nativeView.TextPropertySet -= OnTextPropertySet;
			//nativeView.ShouldChangeTextInRange -= ShouldChangeText;
			base.DisconnectHandler(nativeView);
		}

		void SetupDefaults(MauiSearchBar nativeView)
		{
			_defaultTextColor = QueryEditor?.TextColor;

			var cancelButton = nativeView.FindDescendantView<NSButton>();

			//if (cancelButton != null)
			//{
			//	_cancelButtonTextColorDefaultNormal = cancelButton.TitleColor(UIControlState.Normal);
			//	_cancelButtonTextColorDefaultHighlighted = cancelButton.TitleColor(UIControlState.Highlighted);
			//	_cancelButtonTextColorDefaultDisabled = cancelButton.TitleColor(UIControlState.Disabled);
			//}


		}

		public static void MapText(ISearchBarHandler handler, ISearchBar searchBar)
		{
			//handler.NativeView?.UpdateText(searchBar);

			// Any text update requires that we update any attributed string formatting
			MapFormatting(handler, searchBar);
		}

		public static void MapPlaceholder(ISearchBarHandler handler, ISearchBar searchBar)
		{
			//handler.NativeView?.UpdatePlaceholder(searchBar, handler._editor);
		}

		public static void MapPlaceholderColor(ISearchBarHandler handler, ISearchBar searchBar)
		{
			//handler.NativeView?.UpdatePlaceholder(searchBar, handler._editor);
		}

		public static void MapFont(ISearchBarHandler handler, ISearchBar searchBar)
		{
			var fontManager = handler.GetRequiredService<IFontManager>();

			handler.QueryEditor?.UpdateFont(searchBar, fontManager);
		}

		public static void MapHorizontalTextAlignment(ISearchBarHandler handler, ISearchBar searchBar)
		{
			handler.QueryEditor?.UpdateHorizontalTextAlignment(searchBar);
		}

		public static void MapVerticalTextAlignment(ISearchBarHandler handler, ISearchBar searchBar)
		{
			//handler.NativeView?.UpdateVerticalTextAlignment(searchBar, handler?._editor);
		}

		public static void MapCharacterSpacing(ISearchBarHandler handler, ISearchBar searchBar)
		{
			handler.QueryEditor?.UpdateCharacterSpacing(searchBar);
		}

		public static void MapFormatting(ISearchBarHandler handler, ISearchBar searchBar)
		{
			// Update all of the attributed text formatting properties
			handler.QueryEditor?.UpdateCharacterSpacing(searchBar);

			// Setting any of those may have removed text alignment settings,
			// so we need to make sure those are applied, too
			handler.QueryEditor?.UpdateHorizontalTextAlignment(searchBar);

			// We also update MaxLength which depends on the text
			//handler.NativeView?.UpdateMaxLength(searchBar);
		}

		public static void MapTextColor(ISearchBarHandler handler, ISearchBar searchBar)
		{
			//handler.QueryEditor?.UpdateTextColor(searchBar, handler._defaultTextColor);
		}

		[MissingMapper]
		public static void MapIsTextPredictionEnabled(IViewHandler handler, ISearchBar searchBar) { }

		public static void MapMaxLength(ISearchBarHandler handler, ISearchBar searchBar)
		{
			handler.PlatformView?.UpdateMaxLength(searchBar);
		}

		[MissingMapper]
		public static void MapIsReadOnly(IViewHandler handler, ISearchBar searchBar) { }

		public static void MapCancelButtonColor(ISearchBarHandler handler, ISearchBar searchBar)
		{
			//handler.NativeView?.UpdateCancelButton(searchBar,
			//	handler._cancelButtonTextColorDefaultNormal,
			//	handler._cancelButtonTextColorDefaultHighlighted,
			//	handler._cancelButtonTextColorDefaultDisabled);
		}

		void OnCancelClicked(object? sender, EventArgs args)
		{
			if (VirtualView != null)
				VirtualView.Text = string.Empty;

			PlatformView?.ResignFirstResponder();
		}

		void OnSearchButtonClicked(object? sender, EventArgs e)
		{
			VirtualView?.SearchButtonPressed();
			PlatformView?.ResignFirstResponder();
		}

		void OnTextPropertySet(object? sender, EventArgs e)
		{
			if (VirtualView != null)
				VirtualView.UpdateText(PlatformView?.Text);
		}

		bool ShouldChangeText(MauiSearchBar searchBar, NSRange range, string text)
		{
			var newLength = searchBar?.Text?.Length + text.Length - range.Length;
			return newLength <= VirtualView?.MaxLength;
		}
	}
}
