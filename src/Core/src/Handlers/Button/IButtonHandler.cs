#if __IOS__ || MACCATALYST
using NativeView = UIKit.UIButton;
#elif __MACOS__
using NativeView = AppKit.NSButton;
#elif MONOANDROID
using NativeView = Google.Android.Material.Button.MaterialButton;
#elif WINDOWS
using NativeView = Microsoft.Maui.MauiButton;
#elif NETSTANDARD || (NET6_0 && !IOS && !ANDROID)
using NativeView = System.Object;
#endif

namespace Microsoft.Maui.Handlers
{
	public partial interface IButtonHandler : IViewHandler
	{
		IButton TypedVirtualView { get; }
		NativeView TypedNativeView { get; }
		ImageSourcePartLoader ImageSourceLoader { get; }
	}
}