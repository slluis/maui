using Microsoft.Extensions.DependencyInjection;
using AppKit;

namespace Microsoft.Maui.Platform
{
	internal static partial class MauiContextExtensions
	{
		public static NSWindow GetNativeWindow(this IMauiContext mauiContext) =>
			mauiContext.Services.GetRequiredService<NSWindow>();

		public static IMauiContext MakeScoped(this IMauiContext mauiContext, NSWindow nativeWindow)
		{
			var scopedContext = new MauiContext(mauiContext.Services);
			scopedContext.AddSpecific(nativeWindow);
			return scopedContext;
		}
	}
}
