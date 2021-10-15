using System;
using System.Collections.Specialized;
using Microsoft.Extensions.DependencyInjection;
using AppKit;
using RectangleF = CoreGraphics.CGRect;

namespace Microsoft.Maui.Handlers
{
	public partial class PickerHandler : ViewHandler<IPicker, MauiPopUpButton>
	{
		MauiPopUpButton? _pickerView;

		protected override MauiPopUpButton CreateNativeView()
		{
			_pickerView = new MauiPopUpButton();
			return _pickerView;
		}

		protected override void ConnectHandler(MauiPopUpButton nativeView)
		{
			nativeView.Activated += OnControlSelectionChanged;
			base.ConnectHandler(nativeView);
		}

		protected override void DisconnectHandler(MauiPopUpButton nativeView)
		{
			nativeView.Activated -= OnControlSelectionChanged;
			
			base.DisconnectHandler(nativeView);
		}

		void OnControlSelectionChanged(object? sender, EventArgs e)
		{
			if (VirtualView != null && NativeView != null)
				VirtualView.SelectedIndex = (int) NativeView.IndexOfSelectedItem;
		}

		void Reload()
		{
			if (VirtualView == null || NativeView == null)
				return;

			NativeView?.UpdatePicker(VirtualView);
		}

		public static void MapReload(PickerHandler handler, IPicker picker, object? args) => handler.Reload();

		public static void MapTitle(PickerHandler handler, IPicker picker)
		{
			handler.NativeView?.UpdateTitle(picker);
		}

		public static void MapTitleColor(PickerHandler handler, IPicker picker)
		{
			handler.NativeView?.UpdateTitleColor(picker);
		}

		public static void MapSelectedIndex(PickerHandler handler, IPicker picker)
		{
			handler.NativeView?.UpdateSelectedIndex(picker);
		}

		public static void MapCharacterSpacing(PickerHandler handler, IPicker picker)
		{
			handler.NativeView?.UpdateCharacterSpacing(picker);
		}

		public static void MapFont(PickerHandler handler, IPicker picker)
		{
			var fontManager = handler.GetRequiredService<IFontManager>();

			handler.NativeView?.UpdateFont(picker, fontManager);
		}

		public static void MapHorizontalTextAlignment(PickerHandler handler, IPicker picker)
		{
			//handler.NativeView?.UpdateHorizontalTextAlignment(picker);
		}

		public static void MapTextColor(PickerHandler handler, IPicker picker)
		{
			handler.NativeView?.UpdateTextColor(picker);
		}

		public static void MapVerticalTextAlignment(PickerHandler handler, IPicker picker)
		{
			//handler.NativeView?.UpdateVerticalTextAlignment(picker);
		}
	}
}