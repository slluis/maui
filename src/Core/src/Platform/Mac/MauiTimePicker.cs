using System;
using Foundation;
using AppKit;
using RectangleF = CoreGraphics.CGRect;

namespace Microsoft.Maui
{
	public class MauiTimePicker : NoCaretField
	{
		public event EventHandler ValueChanged;

		readonly Action _dateSelected;
		readonly NSDatePicker _picker;

		public MauiTimePicker(Action dateSelected)
		{
			// TODO COCOA
/*			BorderStyle = UITextBorderStyle.RoundedRect;

			_picker = new UIDatePicker { Mode = NSDatePickerMode.Time, TimeZone = new NSTimeZone("UTC") };
			_dateSelected = dateSelected;

			if (NativeVersion.IsAtLeast(14))
			{
				_picker.PreferredDatePickerStyle = UIDatePickerStyle.Wheels;
			}

			var width = UIScreen.MainScreen.Bounds.Width;
			var toolbar = new UIToolbar(new RectangleF(0, 0, width, 44)) { BarStyle = UIBarStyle.Default, Translucent = true };
			var spacer = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);

			var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, (o, a) =>
			{
				_dateSelected?.Invoke();
			});

			toolbar.SetItems(new[] { spacer, doneButton }, false);

			InputView = _picker;
			InputAccessoryView = toolbar;

			InputView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;
			InputAccessoryView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;

			InputAssistantItem.LeadingBarButtonGroups = null;
			InputAssistantItem.TrailingBarButtonGroups = null;

			AccessibilityTraits = UIAccessibilityTrait.Button;*/
		}

		internal void UpdateCharacterSpacing(ITimePicker timePicker)
		{
			throw new NotImplementedException();
		}

		internal void UpdateTime(ITimePicker timePicker)
		{
			throw new NotImplementedException();
		}

		internal void UpdateFormat(ITimePicker timePicker)
		{
			throw new NotImplementedException();
		}

		internal void UpdateTextColor(ITimePicker timePicker, NSColor? defaultTextColor)
		{
			throw new NotImplementedException();
		}

		internal void UpdateFont(ITimePicker timePicker, IFontManager fontManager)
		{
			throw new NotImplementedException();
		}

		public NSDate Date => _picker.DateValue;
	}
}