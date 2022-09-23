using AppKit;

namespace Microsoft.Maui
{
	public interface IPlatformViewHandler : IViewHandler
	{
		new NSView? PlatformView { get; }
		new NSView? ContainerView { get; }
		NSViewController? ViewController { get; }
	}
}