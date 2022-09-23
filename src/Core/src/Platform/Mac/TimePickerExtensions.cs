using System;
using System.Globalization;
using Foundation;
using AppKit;

namespace Microsoft.Maui.Platform
{
	public static class TimePickerExtensions
	{
		public static void UpdateFormat(this MauiTimePicker mauiTimePicker, ITimePicker timePicker)
		{
			mauiTimePicker.UpdateTime(timePicker, null);
		}

		public static void UpdateFormat(this MauiTimePicker mauiTimePicker, ITimePicker timePicker, NSDatePicker? picker)
		{
			mauiTimePicker.UpdateTime(timePicker, picker);
		}

		public static void UpdateTime(this MauiTimePicker mauiTimePicker, ITimePicker timePicker)
		{
			mauiTimePicker.UpdateTime(timePicker, null);
		}

		public static void UpdateTime(this MauiTimePicker mauiTimePicker, ITimePicker timePicker, NSDatePicker? picker)
		{
			if (picker != null)
				picker.DateValue = new DateTime(1, 1, 1).Add(timePicker.Time).ToNSDate();

			var cultureInfo = Culture.CurrentCulture;

			if (string.IsNullOrEmpty(timePicker.Format))
			{
				NSLocale locale = new NSLocale(cultureInfo.TwoLetterISOLanguageName);

				if (picker != null)
					picker.Locale = locale;
			}

			var time = timePicker.Time;
			var format = timePicker.Format;

			mauiTimePicker.StringValue = time.ToFormattedString(format, cultureInfo);

			if (timePicker.Format?.Contains('H', StringComparison.Ordinal) == true)
			{
				var ci = new CultureInfo("de-DE");
				NSLocale locale = new NSLocale(ci.TwoLetterISOLanguageName);

				if (picker != null)
					picker.Locale = locale;
			}
			else if (timePicker.Format?.Contains('h', StringComparison.Ordinal) == true)
			{
				var ci = new CultureInfo("en-US");
				NSLocale locale = new NSLocale(ci.TwoLetterISOLanguageName);

				if (picker != null)
					picker.Locale = locale;
			}

			mauiTimePicker.UpdateCharacterSpacing(timePicker);
		}
	}
}