﻿using System;
using AppKit;

namespace Microsoft.Maui.Handlers
{
	public partial class TimePickerHandler : ViewHandler<ITimePicker, MauiTimePicker>
	{
		static NSColor? DefaultTextColor;

		protected override MauiTimePicker CreateNativeView()
		{
			return new MauiTimePicker(() =>
			{
				SetVirtualViewTime();
				NativeView?.ResignFirstResponder();
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

		public static void MapFormat(TimePickerHandler handler, ITimePicker timePicker)
		{
			handler.NativeView?.UpdateFormat(timePicker);
		}

		public static void MapTime(TimePickerHandler handler, ITimePicker timePicker)
		{
			handler.NativeView?.UpdateTime(timePicker);
		}

		public static void MapCharacterSpacing(TimePickerHandler handler, ITimePicker timePicker)
		{
			handler.NativeView?.UpdateCharacterSpacing(timePicker);
		}

		public static void MapFont(TimePickerHandler handler, ITimePicker timePicker)
		{
			var fontManager = handler.GetRequiredService<IFontManager>();

			handler.NativeView?.UpdateFont(timePicker, fontManager);
		}

		public static void MapTextColor(TimePickerHandler handler, ITimePicker timePicker)
		{
			handler.NativeView?.UpdateTextColor(timePicker, DefaultTextColor);
		}

		void OnValueChanged(object? sender, EventArgs e)
		{
			SetVirtualViewTime();
		}

		void SetVirtualViewTime()
		{
			if (VirtualView == null || NativeView == null)
				return;

			VirtualView.Time = NativeView.Date.ToDateTime() - new DateTime(1, 1, 1);
		}
	}
}