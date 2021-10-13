using Microsoft.Extensions.DependencyInjection;
using AppKit;

namespace Microsoft.Maui
{
	internal static partial class MauiContextExtensions
	{
		public static FlowDirection GetFlowDirection(this IMauiContext mauiContext)
		{
			var window = mauiContext.GetNativeWindow();
			if (window == null)
				return FlowDirection.LeftToRight;

			return window.EffectiveUserInterfaceLayoutDirection.ToFlowDirection();
		}

		public static NSWindow GetNativeWindow(this IMauiContext mauiContext) =>
			mauiContext.Services.GetRequiredService<NSWindow>();

		public static IMauiContext MakeScoped(this IMauiContext mauiContext, NSWindow nativeWindow)
		{
			var scopedContext = new MauiContext(mauiContext);
			scopedContext.AddSpecific(nativeWindow);
			return scopedContext;
		}
	}
}
