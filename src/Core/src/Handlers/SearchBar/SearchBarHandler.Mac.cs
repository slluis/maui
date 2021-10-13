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

		NSColor? _cancelButtonTextColorDefaultDisabled;
		NSColor? _cancelButtonTextColorDefaultHighlighted;
		NSColor? _cancelButtonTextColorDefaultNormal;

		NSTextField? _editor;

		public NSTextField? QueryEditor => _editor;

		protected override MauiSearchBar CreateNativeView()
		{
			var searchBar = new MauiSearchBar() { ShowsCancelButton = true, BarStyle = UIBarStyle.Default };

			if (NativeVersion.IsAtLeast(13))
				_editor = searchBar.SearchTextField;
			else
				_editor = searchBar.FindDescendantView<UITextField>();

			return searchBar;
		}

		protected override void ConnectHandler(MauiSearchBar nativeView)
		{
			nativeView.CancelButtonClicked += OnCancelClicked;
			nativeView.SearchButtonClicked += OnSearchButtonClicked;
			nativeView.TextPropertySet += OnTextPropertySet;
			nativeView.ShouldChangeTextInRange += ShouldChangeText;
			base.ConnectHandler(nativeView);
			SetupDefaults(nativeView);
		}

		protected override void DisconnectHandler(MauiSearchBar nativeView)
		{
			nativeView.CancelButtonClicked -= OnCancelClicked;
			nativeView.SearchButtonClicked -= OnSearchButtonClicked;
			nativeView.TextPropertySet -= OnTextPropertySet;
			nativeView.ShouldChangeTextInRange -= ShouldChangeText;
			base.DisconnectHandler(nativeView);
		}

		void SetupDefaults(MauiSearchBar nativeView)
		{
			_defaultTextColor = QueryEditor?.TextColor;

			var cancelButton = nativeView.FindDescendantView<UIButton>();

			if (cancelButton != null)
			{
				_cancelButtonTextColorDefaultNormal = cancelButton.TitleColor(UIControlState.Normal);
				_cancelButtonTextColorDefaultHighlighted = cancelButton.TitleColor(UIControlState.Highlighted);
				_cancelButtonTextColorDefaultDisabled = cancelButton.TitleColor(UIControlState.Disabled);
			}


		}

		public static void MapText(SearchBarHandler handler, ISearchBar searchBar)
		{
			handler.NativeView?.UpdateText(searchBar);

			// Any text update requires that we update any attributed string formatting
			MapFormatting(handler, searchBar);
		}

		public static void MapPlaceholder(SearchBarHandler handler, ISearchBar searchBar)
		{
			handler.NativeView?.UpdatePlaceholder(searchBar, handler._editor);
		}

		public static void MapPlaceholderColor(SearchBarHandler handler, ISearchBar searchBar)
		{
			handler.NativeView?.UpdatePlaceholder(searchBar, handler._editor);
		}

		public static void MapFont(SearchBarHandler handler, ISearchBar searchBar)
		{
			var fontManager = handler.GetRequiredService<IFontManager>();

			handler.QueryEditor?.UpdateFont(searchBar, fontManager);
		}

		public static void MapHorizontalTextAlignment(SearchBarHandler handler, ISearchBar searchBar)
		{
			handler.QueryEditor?.UpdateHorizontalTextAlignment(searchBar);
		}

		public static void MapVerticalTextAlignment(SearchBarHandler handler, ISearchBar searchBar)
		{
			handler.NativeView?.UpdateVerticalTextAlignment(searchBar, handler?._editor);
		}

		public static void MapCharacterSpacing(SearchBarHandler handler, ISearchBar searchBar)
		{
			handler.QueryEditor?.UpdateCharacterSpacing(searchBar);
		}

		public static void MapFormatting(SearchBarHandler handler, ISearchBar searchBar)
		{
			// Update all of the attributed text formatting properties
			handler.QueryEditor?.UpdateCharacterSpacing(searchBar);

			// Setting any of those may have removed text alignment settings,
			// so we need to make sure those are applied, too
			handler.QueryEditor?.UpdateHorizontalTextAlignment(searchBar);

			// We also update MaxLength which depends on the text
			handler.NativeView?.UpdateMaxLength(searchBar);
		}

		public static void MapTextColor(SearchBarHandler handler, ISearchBar searchBar)
		{
			handler.QueryEditor?.UpdateTextColor(searchBar, handler._defaultTextColor);
		}

		[MissingMapper]
		public static void MapIsTextPredictionEnabled(IViewHandler handler, ISearchBar searchBar) { }

		public static void MapMaxLength(SearchBarHandler handler, ISearchBar searchBar)
		{
			handler.NativeView?.UpdateMaxLength(searchBar);
		}

		[MissingMapper]
		public static void MapIsReadOnly(IViewHandler handler, ISearchBar searchBar) { }

		public static void MapCancelButtonColor(SearchBarHandler handler, ISearchBar searchBar)
		{
			handler.NativeView?.UpdateCancelButton(searchBar, handler._cancelButtonTextColorDefaultNormal, handler._cancelButtonTextColorDefaultHighlighted, handler._cancelButtonTextColorDefaultDisabled);
		}

		void OnCancelClicked(object? sender, EventArgs args)
		{
			if (VirtualView != null)
				VirtualView.Text = string.Empty;

			NativeView?.ResignFirstResponder();
		}

		void OnSearchButtonClicked(object? sender, EventArgs e)
		{
			VirtualView?.SearchButtonPressed();
			NativeView?.ResignFirstResponder();
		}

		void OnTextPropertySet(object? sender, EventArgs e)
		{
			if (VirtualView != null)
				VirtualView.UpdateText(NativeView?.Text);
		}

		bool ShouldChangeText(MauiSearchBar searchBar, NSRange range, string text)
		{
			var newLength = searchBar?.Text?.Length + text.Length - range.Length;
			return newLength <= VirtualView?.MaxLength;
		}
	}
}
