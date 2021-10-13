using AppKit;

namespace Microsoft.Maui
{
	public interface INativeViewHandler : IViewHandler
	{
		new NSView? NativeView { get; }
		new NSView? ContainerView { get; }
		NSViewController? ViewController { get; }
	}
}