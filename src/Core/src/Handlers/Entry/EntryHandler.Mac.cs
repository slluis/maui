using System;
using Foundation;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Platform;
using AppKit;

namespace Microsoft.Maui.Handlers
{
	public partial class EntryHandler : ViewHandler<IEntry, MauiTextField>
	{
		protected override MauiTextField CreatePlatformView()
		{
			var nativeEntry = new MauiTextField();
			return nativeEntry;
		}

		protected override void ConnectHandler(MauiTextField nativeView)
		{
			base.ConnectHandler(nativeView);

			nativeView.Changed += OnEditingChanged;
			nativeView.Activated += OnEditingChanged;

	/*		nativeView.ShouldReturn = OnShouldReturn;
			nativeView.EditingChanged += OnEditingChanged;
			nativeView.EditingDidEnd += OnEditingEnded;
			nativeView.TextPropertySet += OnTextPropertySet;
			nativeView.ShouldChangeCharacters += OnShouldChangeCharacters;*/
		}

		protected override void DisconnectHandler(MauiTextField nativeView)
		{
			base.DisconnectHandler(nativeView);

			nativeView.Changed -= OnEditingChanged;
			nativeView.Activated -= OnEditingChanged;
			/*			nativeView.EditingChanged -= OnEditingChanged;
						nativeView.EditingDidEnd -= OnEditingEnded;
						nativeView.TextPropertySet -= OnTextPropertySet;
						nativeView.ShouldChangeCharacters -= OnShouldChangeCharacters;*/
		}

		public static void MapText(IEntryHandler handler, IEntry entry)
		{
			handler.PlatformView?.UpdateText(entry);

			// Any text update requires that we update any attributed string formatting
			MapFormatting(handler, entry);
		}

		public static void MapTextColor(IEntryHandler handler, IEntry entry)
		{
			var _defaultTextColor = handler.PlatformView.AttributedStringValue?.GetColor();
			if (_defaultTextColor != null)
			{
				handler.PlatformView?.UpdateTextColorWithEntry(entry, _defaultTextColor);
			}
		}

		public static void MapIsPassword(IEntryHandler handler, IEntry entry)
		{
			handler.PlatformView?.UpdateIsPassword(entry);
		}

		public static void MapHorizontalTextAlignment(IEntryHandler handler, IEntry entry)
		{
			handler.PlatformView?.UpdateHorizontalTextAlignment(entry);
		}

		public static void MapVerticalTextAlignment(IEntryHandler handler, IEntry entry)
		{
			handler?.PlatformView?.UpdateVerticalTextAlignment(entry);
		}

		public static void MapIsTextPredictionEnabled(IEntryHandler handler, IEntry entry)
		{
			handler.PlatformView?.UpdateIsTextPredictionEnabled(entry);
		}

		public static void MapMaxLength(IEntryHandler handler, IEntry entry)
		{
			handler.PlatformView?.UpdateMaxLength(entry);
		}

		public static void MapPlaceholder(IEntryHandler handler, IEntry entry)
		{
			handler.PlatformView?.UpdatePlaceholder(entry);
		}

		public static void MapPlaceholderColor(IEntryHandler handler, IEntry entry)
		{
			var _defaultTextColor = handler.PlatformView.AttributedStringValue?.GetColor();
			if (_defaultTextColor != null)
			{
				handler.PlatformView?.UpdatePlaceholder(entry, _defaultTextColor.ToColor());
			}
		}

		public static void MapIsReadOnly(IEntryHandler handler, IEntry entry)
		{
			handler.PlatformView?.UpdateIsReadOnly(entry);
		}

		public static void MapKeyboard(IEntryHandler handler, IEntry entry)
		{
			handler.PlatformView?.UpdateKeyboard(entry);
		}

		public static void MapReturnType(IEntryHandler handler, IEntry entry)
		{
			handler.PlatformView?.UpdateReturnType(entry);
		}

		public static void MapFont(IEntryHandler handler, IEntry entry)
		{
			var fontManager = handler.GetRequiredService<IFontManager>();

			handler.PlatformView?.UpdateFont(entry, fontManager);
		}

		public static void MapFormatting(IEntryHandler handler, IEntry entry)
		{
			handler.PlatformView?.UpdateMaxLength(entry);

			// Update all of the attributed text formatting properties
			handler.PlatformView?.UpdateCharacterSpacing(entry);

			// Setting any of those may have removed text alignment settings,
			// so we need to make sure those are applied, too
			handler.PlatformView?.UpdateHorizontalTextAlignment(entry);
		}

		public static void MapCharacterSpacing(IEntryHandler handler, IEntry entry)
		{
			handler.PlatformView?.UpdateCharacterSpacing(entry);
		}

		public static void MapCursorPosition(IEntryHandler handler, IEntry entry)
		{
			handler.PlatformView?.UpdateCursorPosition(entry);
		}

		public static void MapSelectionLength(IEntryHandler handler, IEntry entry)
		{
			handler.PlatformView?.UpdateSelectionLength(entry);
		}

		public static void MapClearButtonVisibility(IEntryHandler handler, IEntry entry)
		{
			handler.PlatformView?.UpdateClearButtonVisibility(entry);
		}

		protected virtual bool OnShouldReturn(NSTextField view)
		{
			view.ResignFirstResponder();

			// TODO: Focus next View

			VirtualView?.Completed();

			return false;
		}

		void OnEditingChanged(object? sender, EventArgs e) => OnTextChanged();

		void OnTextChanged()
		{
			if (VirtualView == null || PlatformView == null)
				return;

			VirtualView.UpdateText(PlatformView.StringValue);
		}

		bool OnShouldChangeCharacters(NSTextField textField, NSRange range, string replacementString)
		{
			var currLength = textField?.StringValue?.Length ?? 0;

			// fix a crash on undo
			if (range.Length + range.Location > currLength)
				return false;

			if (VirtualView == null || PlatformView == null)
				return false;

			if (VirtualView.MaxLength < 0)
				return true;

			var addLength = replacementString?.Length ?? 0;
			var remLength = range.Length;

			var newLength = currLength + addLength - remLength;

			return newLength <= VirtualView.MaxLength;
		}
	}
}