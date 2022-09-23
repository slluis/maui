using System;
using System.Runtime.InteropServices;
using Foundation;
using Microsoft.Extensions.Logging;
using AppKit;

namespace Microsoft.Maui.Handlers
{
	public partial class ApplicationHandler : ElementHandler<IApplication, NSApplicationDelegate>
	{
		public static void MapTerminate(ApplicationHandler handler, IApplication application, object? args)
		{
			NSApplication.SharedApplication.Terminate();
		}

		public static void MapOpenWindow(ApplicationHandler handler, IApplication application, object? args)
		{
			// TODO COCOA
		}

		public static void MapCloseWindow(ApplicationHandler handler, IApplication application, object? args)
		{
			if (args is IWindow window)
			{
				// TODO COCOA
			}
		}

		class NSApplication
		{
			static IntPtr ClassHandle => ObjCRuntime.Class.GetHandle("NSApplication");
			static IntPtr SharedApplicationSelector => ObjCRuntime.Selector.GetHandle("sharedApplication");
			static IntPtr TerminateSelector => ObjCRuntime.Selector.GetHandle("terminate:");

			readonly IntPtr _handle;

			NSApplication(IntPtr handle)
			{
				_handle = handle;
			}

			public static NSApplication SharedApplication =>
				new(IntPtr_objc_msgSend(ClassHandle, SharedApplicationSelector));

			public void Terminate() =>
				void_objc_msgSend_IntPtr(_handle, TerminateSelector, IntPtr.Zero);

			[DllImport(ObjCRuntime.Constants.ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
			static extern IntPtr IntPtr_objc_msgSend(IntPtr receiver, IntPtr selector);

			[DllImport(ObjCRuntime.Constants.ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
			static extern void void_objc_msgSend_IntPtr(IntPtr receiver, IntPtr selector, IntPtr arg1);
		}
	}
}