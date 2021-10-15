#nullable enable
using System;
using Foundation;
using Microsoft.Maui.Handlers;

namespace Microsoft.Maui
{
	public static class PickerExtensions
	{
		public static void UpdateTitle(this MauiPopUpButton nativePicker, IPicker picker) =>
			nativePicker.UpdatePicker(picker);

		public static void UpdateTitleColor(this MauiPopUpButton nativePicker, IPicker picker) =>
 			nativePicker.SetTitleColor(picker);

		public static void UpdateTextColor(this MauiPopUpButton nativePicker, IPicker picker)
		{
			var color = picker.TextColor?.ToNative();
			if (color != null)
			{
				var attributedValue = nativePicker.AttributedStringValue?.WithColor(color);
				if (attributedValue != null)
				{
					nativePicker.AttributedStringValue = attributedValue;
				}
			}
		}

		public static void UpdateSelectedIndex(this MauiPopUpButton nativePicker, IPicker picker) =>
			nativePicker.SetSelectedIndex(picker, picker.SelectedIndex);

		internal static void SetTitleColor(this MauiPopUpButton nativePicker, IPicker picker)
		{
			var title = picker.Title;

			if (string.IsNullOrEmpty(title))
				return;

			var titleColor = picker.TitleColor;

			if (titleColor == null)
				return;

			nativePicker.UpdateAttributedPlaceholder(new NSAttributedString(title, null, titleColor.ToNative()));
		}

		internal static void UpdateAttributedPlaceholder(this MauiPopUpButton nativePicker, NSAttributedString nsAttributedString)
		{
			//nativePicker.place = nsAttributedString;
		}

		internal static void UpdatePicker(this MauiPopUpButton nativePicker, IPicker picker)
		{
			var selectedIndex = picker.SelectedIndex;

			nativePicker.RemoveAllItems();
			nativePicker.AddItems(picker.GetItemsAsArray());

			if (picker.GetCount() == 0)
				return;

			nativePicker.SetSelectedIndex(picker, selectedIndex);
		}

		internal static void SetSelectedIndex(this MauiPopUpButton nativePicker, IPicker picker, int selectedIndex = 0)
		{
			if (selectedIndex > nativePicker.ItemCount - 1)
				return;

			picker.SelectedIndex = selectedIndex;
			nativePicker.SelectItem(Math.Max(selectedIndex, 0));
		}
	}
}