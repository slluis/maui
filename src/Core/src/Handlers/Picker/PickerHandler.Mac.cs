using System;
using System.Collections.Specialized;
using Microsoft.Extensions.DependencyInjection;
using AppKit;
using RectangleF = CoreGraphics.CGRect;

namespace Microsoft.Maui.Handlers
{
	public partial class PickerHandler : ViewHandler<IPicker, MauiPicker>
	{
		MauiPicker? _pickerView;

		protected override MauiPicker CreatePlatformView()
		{
			_pickerView = new MauiPicker();
			return _pickerView;
		}

		protected override void ConnectHandler(MauiPicker nativeView)
		{
			nativeView.Activated += OnControlSelectionChanged;
			base.ConnectHandler(nativeView);
		}

		protected override void DisconnectHandler(MauiPicker nativeView)
		{
			nativeView.Activated -= OnControlSelectionChanged;
			
			base.DisconnectHandler(nativeView);
		}

		void OnControlSelectionChanged(object? sender, EventArgs e)
		{
			if (VirtualView != null && PlatformView != null)
				VirtualView.SelectedIndex = (int)PlatformView.IndexOfSelectedItem;
		}

		static void Reload(IPickerHandler handler)
		{
			handler.PlatformView.UpdatePicker(handler.VirtualView);
		}

		public static void MapReload(PickerHandler handler, IPicker picker, object? args) => Reload(handler);

		public static void MapTitle(PickerHandler handler, IPicker picker)
		{
			handler.PlatformView?.UpdateTitle(picker);
		}

		public static void MapTitleColor(PickerHandler handler, IPicker picker)
		{
			handler.PlatformView?.UpdateTitleColor(picker);
		}

		public static void MapSelectedIndex(PickerHandler handler, IPicker picker)
		{
			handler.PlatformView?.UpdateSelectedIndex(picker);
		}

		public static void MapCharacterSpacing(PickerHandler handler, IPicker picker)
		{
			handler.PlatformView?.UpdateCharacterSpacing(picker);
		}

		public static void MapFont(PickerHandler handler, IPicker picker)
		{
			var fontManager = handler.GetRequiredService<IFontManager>();

			handler.PlatformView?.UpdateFont(picker, fontManager);
		}

		public static void MapHorizontalTextAlignment(PickerHandler handler, IPicker picker)
		{
			//handler.NativeView?.UpdateHorizontalTextAlignment(picker);
		}

		public static void MapTextColor(PickerHandler handler, IPicker picker)
		{
			handler.PlatformView?.UpdateTextColor(picker);
		}

		public static void MapVerticalTextAlignment(PickerHandler handler, IPicker picker)
		{
			//handler.NativeView?.UpdateVerticalTextAlignment(picker);
		}

		internal static void MapItems(IPickerHandler handler, IPicker picker) => Reload(handler);
	}
}