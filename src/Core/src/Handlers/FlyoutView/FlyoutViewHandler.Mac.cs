using System;

namespace Microsoft.Maui.Handlers
{
	public partial class FlyoutViewHandler : ViewHandler<IFlyoutView, AppKit.NSView>
	{
		protected override AppKit.NSView CreatePlatformView()
		{
			throw new System.NotImplementedException();
		}
	}
}
