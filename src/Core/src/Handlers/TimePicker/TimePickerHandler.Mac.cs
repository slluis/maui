using System;
using AppKit;

namespace Microsoft.Maui.Handlers
{
	public partial class TimePickerHandler : ViewHandler<ITimePicker, MauiTimePicker>
	{
		static NSColor? DefaultTextColor;

		protected override MauiTimePicker CreatePlatformView()
		{
			return new MauiTimePicker(() =>
			{
				SetVirtualViewTime();
				PlatformView?.ResignFirstResponder();
			});
		}

		protected override void ConnectHandler(MauiTimePicker nativeView)
		{
			base.ConnectHandler(nativeView);

			if (nativeView != null)
				nativeView.ValueChanged += OnValueChanged;
		}

		protected override void DisconnectHandler(MauiTimePicker nativeView)
		{
			base.DisconnectHandler(nativeView);

			if (nativeView != null)
			{
				nativeView.RemoveFromSuperview();
				nativeView.ValueChanged -= OnValueChanged;
				nativeView.Dispose();
			}
		}

		void SetupDefaults(MauiTimePicker nativeView)
		{
			DefaultTextColor = nativeView.TextColor;
		}

		public static void MapFormat(ITimePickerHandler handler, ITimePicker timePicker)
		{
			handler.PlatformView?.UpdateFormat(timePicker);
		}

		public static void MapTime(ITimePickerHandler handler, ITimePicker timePicker)
		{
			handler.PlatformView?.UpdateTime(timePicker);
		}

		public static void MapCharacterSpacing(ITimePickerHandler handler, ITimePicker timePicker)
		{
			handler.PlatformView?.UpdateCharacterSpacing(timePicker);
		}

		public static void MapFont(ITimePickerHandler handler, ITimePicker timePicker)
		{
			var fontManager = handler.GetRequiredService<IFontManager>();

			handler.PlatformView?.UpdateFont(timePicker, fontManager);
		}

		public static void MapTextColor(ITimePickerHandler handler, ITimePicker timePicker)
		{
			handler.PlatformView?.UpdateTextColor(timePicker, DefaultTextColor);
		}

		void OnValueChanged(object? sender, EventArgs e)
		{
			SetVirtualViewTime();
		}

		void SetVirtualViewTime()
		{
			if (VirtualView == null || PlatformView == null)
				return;

			VirtualView.Time = PlatformView.Date.ToDateTime() - new DateTime(1, 1, 1);
		}
	}
}