using Microsoft.Maui.Handlers;
using AppKit;

namespace Microsoft.Maui
{
	public class PageViewController : ContainerViewController
	{
		public PageViewController(IView page, IMauiContext mauiContext)
		{
			CurrentView = page;
			Context = mauiContext;

			LoadFirstView(page);
		}

		protected override NSView CreateNativeView(IElement view)
		{
			return new ContentView
			{
				CrossPlatformArrange = ((IContentView)view).CrossPlatformArrange,
				CrossPlatformMeasure = ((IContentView)view).CrossPlatformMeasure
			};
		}

		// TODO COCOA
/*		public override void TraitCollectionDidChange(UITraitCollection? previousTraitCollection)
		{
			if (CurrentView?.Handler is ElementHandler handler)
			{
				var application = handler.GetRequiredService<IApplication>();
				application?.ThemeChanged();
			}

			base.TraitCollectionDidChange(previousTraitCollection);
		}*/
	}
}