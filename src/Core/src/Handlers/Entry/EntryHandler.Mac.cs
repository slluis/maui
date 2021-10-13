﻿using System;
using Foundation;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Platform.Mac;
using AppKit;

namespace Microsoft.Maui.Handlers
{
	public partial class EntryHandler : ViewHandler<IEntry, MauiTextField>
	{
		NSColor? _defaultTextColor;
		Color? _defaultPlaceholderColor;

		protected override MauiTextField CreateNativeView()
		{
			var nativeEntry = new MauiTextField
			{
				BorderStyle = NSTextBorderStyle.RoundedRect,
				ClipsToBounds = true
			};

			_defaultTextColor = nativeEntry.TextColor;

			// Placeholder default color is 70% gray					
			// https://developer.apple.com/library/prerelease/ios/documentation/UIKit/Reference/UITextField_Class/index.html#//apple_ref/occ/instp/UITextField/placeholder
			_defaultPlaceholderColor = ColorExtensions.SeventyPercentGrey.ToColor();

			return nativeEntry;
		}

		protected override void ConnectHandler(MauiTextField nativeView)
		{
			base.ConnectHandler(nativeView);

			nativeView.ShouldReturn = OnShouldReturn;
			nativeView.EditingChanged += OnEditingChanged;
			nativeView.EditingDidEnd += OnEditingEnded;
			nativeView.TextPropertySet += OnTextPropertySet;
			nativeView.ShouldChangeCharacters += OnShouldChangeCharacters;
		}

		protected override void DisconnectHandler(MauiTextField nativeView)
		{
			base.DisconnectHandler(nativeView);

			nativeView.EditingChanged -= OnEditingChanged;
			nativeView.EditingDidEnd -= OnEditingEnded;
			nativeView.TextPropertySet -= OnTextPropertySet;
			nativeView.ShouldChangeCharacters -= OnShouldChangeCharacters;
		}

		public static void MapText(EntryHandler handler, IEntry entry)
		{
			handler.NativeView?.UpdateText(entry);

			// Any text update requires that we update any attributed string formatting
			MapFormatting(handler, entry);
		}

		public static void MapTextColor(EntryHandler handler, IEntry entry)
		{
			handler.NativeView?.UpdateTextColor(entry, handler._defaultTextColor);
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
			handler.NativeView?.UpdatePlaceholder(entry, handler._defaultPlaceholderColor);
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

		void OnEditingEnded(object? sender, EventArgs e) => OnTextChanged();

		void OnTextPropertySet(object? sender, EventArgs e) => OnTextChanged();

		void OnTextChanged()
		{
			if (VirtualView == null || NativeView == null)
				return;

			VirtualView.UpdateText(NativeView.Text);
		}

		bool OnShouldChangeCharacters(NSTextField textField, NSRange range, string replacementString)
		{
			var currLength = textField?.Text?.Length ?? 0;

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