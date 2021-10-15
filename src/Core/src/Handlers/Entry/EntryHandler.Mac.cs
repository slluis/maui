using System;
using Foundation;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Platform.Mac;
using AppKit;

namespace Microsoft.Maui.Handlers
{
	public partial class EntryHandler : ViewHandler<IEntry, MauiTextField>
	{
		protected override MauiTextField CreateNativeView()
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

		public static void MapText(EntryHandler handler, IEntry entry)
		{
			handler.NativeView?.UpdateText(entry);

			// Any text update requires that we update any attributed string formatting
			MapFormatting(handler, entry);
		}

		public static void MapTextColor(EntryHandler handler, IEntry entry)
		{
			var _defaultTextColor = handler.NativeView.AttributedStringValue?.GetColor();
			if (_defaultTextColor != null)
			{
				handler.NativeView?.UpdateTextColorWithEntry(entry, _defaultTextColor);
			}
		}

		public static void MapIsPassword(EntryHandler handler, IEntry entry)
		{
			handler.NativeView?.UpdateIsPassword(entry);
		}

		public static void MapHorizontalTextAlignment(EntryHandler handler, IEntry entry)
		{
			handler.NativeView?.UpdateHorizontalTextAlignment(entry);
		}

		public static void MapVerticalTextAlignment(EntryHandler handler, IEntry entry)
		{
			handler?.NativeView?.UpdateVerticalTextAlignment(entry);
		}

		public static void MapIsTextPredictionEnabled(EntryHandler handler, IEntry entry)
		{
			handler.NativeView?.UpdateIsTextPredictionEnabled(entry);
		}

		public static void MapMaxLength(EntryHandler handler, IEntry entry)
		{
			handler.NativeView?.UpdateMaxLength(entry);
		}

		public static void MapPlaceholder(EntryHandler handler, IEntry entry)
		{
			handler.NativeView?.UpdatePlaceholder(entry);
		}

		public static void MapPlaceholderColor(EntryHandler handler, IEntry entry)
		{
			var _defaultTextColor = handler.NativeView.AttributedStringValue?.GetColor();
			if (_defaultTextColor != null)
			{
				handler.NativeView?.UpdatePlaceholder(entry, _defaultTextColor.ToColor());
			}
		}

		public static void MapIsReadOnly(EntryHandler handler, IEntry entry)
		{
			handler.NativeView?.UpdateIsReadOnly(entry);
		}

		public static void MapKeyboard(EntryHandler handler, IEntry entry)
		{
			handler.NativeView?.UpdateKeyboard(entry);
		}

		public static void MapReturnType(EntryHandler handler, IEntry entry)
		{
			handler.NativeView?.UpdateReturnType(entry);
		}

		public static void MapFont(EntryHandler handler, IEntry entry)
		{
			var fontManager = handler.GetRequiredService<IFontManager>();

			handler.NativeView?.UpdateFont(entry, fontManager);
		}

		public static void MapFormatting(EntryHandler handler, IEntry entry)
		{
			handler.NativeView?.UpdateMaxLength(entry);

			// Update all of the attributed text formatting properties
			handler.NativeView?.UpdateCharacterSpacing(entry);

			// Setting any of those may have removed text alignment settings,
			// so we need to make sure those are applied, too
			handler.NativeView?.UpdateHorizontalTextAlignment(entry);
		}

		public static void MapCharacterSpacing(EntryHandler handler, IEntry entry)
		{
			handler.NativeView?.UpdateCharacterSpacing(entry);
		}

		public static void MapCursorPosition(EntryHandler handler, IEntry entry)
		{
			handler.NativeView?.UpdateCursorPosition(entry);
		}

		public static void MapSelectionLength(EntryHandler handler, IEntry entry)
		{
			handler.NativeView?.UpdateSelectionLength(entry);
		}

		public static void MapClearButtonVisibility(EntryHandler handler, IEntry entry)
		{
			handler.NativeView?.UpdateClearButtonVisibility(entry);
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
			if (VirtualView == null || NativeView == null)
				return;

			VirtualView.UpdateText(NativeView.StringValue);
		}

		bool OnShouldChangeCharacters(NSTextField textField, NSRange range, string replacementString)
		{
			var currLength = textField?.StringValue?.Length ?? 0;

			// fix a crash on undo
			if (range.Length + range.Location > currLength)
				return false;

			if (VirtualView == null || NativeView == null)
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