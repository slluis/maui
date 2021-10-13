using System;
using AppKit;
using RectangleF = CoreGraphics.CGRect;

namespace Microsoft.Maui.Handlers
{
	public partial class SwitchHandler : ViewHandler<ISwitch, NSView>
	{
		//static NSColor? DefaultOnTrackColor;
		//static NSColor? DefaultOffTrackColor;
		//static NSColor? DefaultThumbColor;

		protected override NSView CreateNativeView()
		{
			throw new NotImplementedException();
//			return new UISwitch(RectangleF.Empty);
		}

		protected override void ConnectHandler(NSView nativeView)
		{
			base.ConnectHandler(nativeView);

//			nativeView.ValueChanged += OnControlValueChanged;
		}

		protected override void DisconnectHandler(NSView nativeView)
		{
			base.DisconnectHandler(nativeView);

//			nativeView.ValueChanged -= OnControlValueChanged;
		}

/*		void SetupDefaults(UISwitch nativeView)
		{
			DefaultOnTrackColor = UISwitch.Appearance.OnTintColor;
			DefaultOffTrackColor = nativeView.GetOffTrackColor();
			DefaultThumbColor = UISwitch.Appearance.ThumbTintColor;
		}*/

		public static void MapIsOn(SwitchHandler handler, ISwitch view)
		{
//			handler.NativeView?.UpdateIsOn(view);
		}

		public static void MapTrackColor(SwitchHandler handler, ISwitch view)
		{
//			handler.NativeView?.UpdateTrackColor(view, DefaultOnTrackColor, DefaultOffTrackColor);
		}

		public static void MapThumbColor(SwitchHandler handler, ISwitch view)
		{
//			handler.NativeView?.UpdateThumbColor(view, DefaultThumbColor);
		}

/*		void OnControlValueChanged(object? sender, EventArgs e)
		{
			if (VirtualView == null)
				return;

			if (NativeView != null)
				VirtualView.IsOn = NativeView.On;
		}*/
	}
}