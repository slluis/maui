using AppKit;

namespace Microsoft.Maui
{
	public static class DatePickerExtensions
	{
		public static void UpdateFormat(this MauiDatePicker nativeDatePicker, IDatePicker datePicker)
		{
			nativeDatePicker.UpdateDate(datePicker, null);
		}

		public static void UpdateFormat(this MauiDatePicker nativeDatePicker, IDatePicker datePicker, NSDatePicker? picker)
		{
			nativeDatePicker.UpdateDate(datePicker, picker);
		}

		public static void UpdateDate(this MauiDatePicker nativeDatePicker, IDatePicker datePicker)
		{
			nativeDatePicker.UpdateDate(datePicker, null);
		}

		public static void UpdateTextColor(this MauiDatePicker nativeDatePicker, IDatePicker datePicker, NSColor? defaultTextColor)
		{
			var textColor = datePicker.TextColor;

			if (textColor == null)
				nativeDatePicker.TextColor = defaultTextColor ?? NSColor.Text;
			else
				nativeDatePicker.TextColor = textColor.ToNative();

			// HACK This forces the color to update; there's probably a more elegant way to make this happen
			nativeDatePicker.UpdateDate(datePicker);
		}

		public static void UpdateDate(this MauiDatePicker nativeDatePicker, IDatePicker datePicker, NSDatePicker? picker)
		{
			if (picker != null && picker.DateValue.ToDateTime().Date != datePicker.Date.Date)
				picker.DateValue = datePicker.Date.ToNSDate();

//			nativeDatePicker.Text = datePicker.Date.ToString(datePicker.Format);

			nativeDatePicker.UpdateCharacterSpacing(datePicker);
		}

		public static void UpdateMinimumDate(this MauiDatePicker nativeDatePicker, IDatePicker datePicker)
		{
			nativeDatePicker.UpdateMinimumDate(datePicker, null);
		}

		public static void UpdateMinimumDate(this MauiDatePicker nativeDatePicker, IDatePicker datePicker, NSDatePicker? picker)
		{
			if (picker != null)
			{
				picker.MinDate = datePicker.MinimumDate.ToNSDate();
			}
		}

		public static void UpdateMaximumDate(this MauiDatePicker nativeDatePicker, IDatePicker datePicker)
		{
			nativeDatePicker.UpdateMaximumDate(datePicker, null);
		}

		public static void UpdateMaximumDate(this MauiDatePicker nativeDatePicker, IDatePicker datePicker, NSDatePicker? picker)
		{
			if (picker != null)
			{
				picker.MaxDate = datePicker.MaximumDate.ToNSDate();
			}
		}
	}
}