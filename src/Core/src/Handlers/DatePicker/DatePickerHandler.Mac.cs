using System;
using Foundation;
using AppKit;
using RectangleF = CoreGraphics.CGRect;

namespace Microsoft.Maui.Handlers
{
	public partial class DatePickerHandler : ViewHandler<IDatePicker, MauiDatePicker>
	{
		NSColor? _defaultTextColor;
		NSDatePicker? _picker;

		protected override MauiDatePicker CreatePlatformView()
		{
			MauiDatePicker nativeDatePicker = new MauiDatePicker();

			_picker = new NSDatePicker();

/*			_picker = new NSDatePicker { Mode = NSDatePickerMode.Date, TimeZone = new NSTimeZone("UTC") };

			if (NativeVersion.IsAtLeast(14))
			{
				_picker.PreferredDatePickerStyle = NSDatePickerStyle.Wheels;
			}

			var width = NSScreen.MainScreen.Bounds.Width;
			var toolbar = new NSToolbar(new RectangleF(0, 0, width, 44)) { BarStyle = NSBarStyle.Default, Translucent = true };
			var spacer = new NSBarButtonItem(NSBarButtonSystemItem.FlexibleSpace);
			var doneButton = new NSBarButtonItem(NSBarButtonSystemItem.Done, (o, a) =>
			{
				SetVirtualViewDate();
				nativeDatePicker.ResignFirstResponder();
			});

			toolbar.SetItems(new[] { spacer, doneButton }, false);

			nativeDatePicker.InputView = _picker;
			nativeDatePicker.InputAccessoryView = toolbar;

			nativeDatePicker.InputView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;
			nativeDatePicker.InputAccessoryView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;

			nativeDatePicker.InputAssistantItem.LeadingBarButtonGroups = null;
			nativeDatePicker.InputAssistantItem.TrailingBarButtonGroups = null;

			nativeDatePicker.AccessibilityTraits = UIAccessibilityTrait.Button;
*/
			return nativeDatePicker;
		}

		internal NSDatePicker? DatePickerDialog { get { return _picker; } }

		protected override void ConnectHandler(MauiDatePicker nativeView)
		{
			if (_picker != null)
				_picker.Activated += OnValueChanged;

			base.ConnectHandler(nativeView);
		}

		protected override void DisconnectHandler(MauiDatePicker nativeView)
		{
			if (_picker != null)
				_picker.Activated -= OnValueChanged;

			base.DisconnectHandler(nativeView);
		}

		void SetupDefaults(MauiDatePicker nativeView)
		{
			_defaultTextColor = nativeView.TextColor;
		}

		public static void MapFormat(IDatePickerHandler handler, IDatePicker datePicker)
		{
			handler.PlatformView?.UpdateFormat(datePicker);
		}

		public static void MapDate(IDatePickerHandler handler, IDatePicker datePicker)
		{
			handler.PlatformView?.UpdateDate(datePicker);
		}

		public static void MapMinimumDate(IDatePickerHandler handler, IDatePicker datePicker)
		{
			handler.PlatformView?.UpdateMinimumDate(datePicker, ((DatePickerHandler)handler)._picker);
		}

		public static void MapMaximumDate(IDatePickerHandler handler, IDatePicker datePicker)
		{
			handler.PlatformView?.UpdateMaximumDate(datePicker, ((DatePickerHandler)handler)._picker);
		}

		public static void MapCharacterSpacing(IDatePickerHandler handler, IDatePicker datePicker)
		{
			handler.PlatformView?.UpdateCharacterSpacing(datePicker);
		}

		public static void MapFont(IDatePickerHandler handler, IDatePicker datePicker)
		{
			var fontManager = handler.GetRequiredService<IFontManager>();

			handler.PlatformView?.UpdateFont(datePicker, fontManager);
		}

		public static void MapTextColor(IDatePickerHandler handler, IDatePicker datePicker)
		{
			handler.PlatformView?.UpdateTextColor(datePicker, ((DatePickerHandler)handler)._defaultTextColor);
		}

		void OnValueChanged(object? sender, EventArgs? e)
		{
			SetVirtualViewDate();
		}

		void SetVirtualViewDate()
		{
			if (VirtualView == null || _picker == null)
				return;

			VirtualView.Date = _picker.DateValue.ToDateTime().Date;
		}
	}
}