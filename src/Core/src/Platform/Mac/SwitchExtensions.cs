using System;
using System.Linq;
using AppKit;
using Foundation;

namespace Microsoft.Maui
{
	public static class SwitchExtensions
	{
		public static void UpdateIsOn(this NSSwitch uiSwitch, ISwitch view)
		{
			// TODO COCOA
//			uiSwitch.SetState(view.IsOn, true);
		}

		public static void UpdateTrackColor(this NSSwitch uiSwitch, ISwitch view, NSColor? defaultOnTrackColor, NSColor? defaultOffTrackColor)
		{
			if (view == null)
				return;

			// TODO COCOA
/*			if (view.TrackColor == null)
				uiSwitch.OnTintColor = defaultOnTrackColor;
			else
				uiSwitch.OnTintColor = view.TrackColor.ToNative();

			NSView uIView;
			if (NativeVersion.IsAtLeast(13))
				uIView = uiSwitch.Subviews[0].Subviews[0];
			else
				uIView = uiSwitch.Subviews[0].Subviews[0].Subviews[0];

			if (view.TrackColor == null)
				uIView.BackgroundColor = defaultOffTrackColor;
			else
				uIView.BackgroundColor = uiSwitch.OnTintColor;*/
		}

		public static void UpdateThumbColor(this NSSwitch uiSwitch, ISwitch view, NSColor? defaultThumbColor)
		{
			if (view == null)
				return;

			Graphics.Color thumbColor = view.ThumbColor;
			// TODO COCOA
//			uiSwitch.ThumbTintColor = thumbColor?.ToNative() ?? defaultThumbColor;
		}

		internal static NSView GetTrackSubview(this NSSwitch uISwitch)
		{
			NSView uIView;
			if (NativeVersion.IsAtLeast(13))
				uIView = uISwitch.Subviews[0].Subviews[0];
			else
				uIView = uISwitch.Subviews[0].Subviews[0].Subviews[0];

			return uIView;
		}

		internal static NSColor? GetOffTrackColor(this NSSwitch uISwitch)
		{
			// TODO COCOA
			throw new NotImplementedException();
//			return uISwitch.GetTrackSubview().BackgroundColor;
		}
	}
}
