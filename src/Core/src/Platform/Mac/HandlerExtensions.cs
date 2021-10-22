using System;
using Foundation;
using AppKit;

namespace Microsoft.Maui
{
	public static class HandlerExtensions
	{
		public static NSViewController ToNSViewController(this IElement view, IMauiContext context)
		{
			var nativeView = view.ToNative(context);
			if (view?.Handler is INativeViewHandler nvh && nvh.ViewController != null)
				return nvh.ViewController;

			return new ContainerViewController { CurrentView = view, Context = context };
		}

		public static NSView ToNative(this IElement view, IMauiContext context)
		{
			return ToNative(view, context, false);
		}

		/// <summary>
		/// Returns the native view that renders the provided view
		/// </summary>
		/// <param name="view">The view</param>
		/// <param name="context">Maui context</param>
		/// <param name="embedable">True if the view is going to be added to a view or window external to Maui</param>
		/// <returns>The native view</returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="Exception"></exception>
		/// <exception cref="InvalidOperationException"></exception>
		public static NSView ToNative(this IElement view, IMauiContext context, bool embedable)
		{
			_ = view ?? throw new ArgumentNullException(nameof(view));
			_ = context ?? throw new ArgumentNullException(nameof(context));

			//This is how MVU works. It collapses views down
			if (view is IReplaceableView ir)
				view = ir.ReplacedView;

			var handler = view.Handler;
			if (handler == null)
				handler = context.Handlers.GetHandler(view.GetType());

			if (handler == null)
				throw new Exception($"Handler not found for view {view}.");

			handler.SetMauiContext(context);

			view.Handler = handler;

			if (handler.VirtualView != view)
				handler.SetVirtualView(view);

			if (((INativeViewHandler)handler).NativeView is not NSView result)
			{
				throw new InvalidOperationException($"Unable to convert {view} to {typeof(NSView)}");
			}

			if (embedable && view is IView iview)
			{
				return new MauiHostView(iview, result);
			}
			return result;
		}

		public static void SetApplicationHandler(this NSApplicationDelegate nativeApplication, IApplication application, IMauiContext context) =>
			SetHandler(nativeApplication, application, context);

		public static void SetWindowHandler(this NSWindow nativeWindow, IWindow window, IMauiContext context) =>
			SetHandler(nativeWindow, window, context);

		static void SetHandler(this NSObject nativeElement, IElement element, IMauiContext mauiContext)
		{
			_ = nativeElement ?? throw new ArgumentNullException(nameof(nativeElement));
			_ = element ?? throw new ArgumentNullException(nameof(element));
			_ = mauiContext ?? throw new ArgumentNullException(nameof(mauiContext));

			var handler = element.Handler;
			if (handler == null)
				handler = mauiContext.Handlers.GetHandler(element.GetType());

			if (handler == null)
				throw new Exception($"Handler not found for window {element}.");

			handler.SetMauiContext(mauiContext);

			element.Handler = handler;

			if (handler.VirtualView != element)
				handler.SetVirtualView(element);
		}
	}
}